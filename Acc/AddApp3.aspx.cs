using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Acc_AddApp3 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    Student st = new Student();
    ClsExamForm p2 = new ClsExamForm();
    DataSet ds;
    SqlDataAdapter adapter;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                PnlMembership.Visible = false; btnSubmit.Enabled = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                FeeName();
                FeeSession();
                FeeFeeType();
                lblMsg.Text = "";
                ddlsession.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = "";
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtDiaryNo.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = "";
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        lblIMID.Text = ""; txtDiaryRcvDate.Text = "";             
        lblExceptionOK.Text = "";
        con.Close(); con.Open();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        cmd = new SqlCommand("select IMID,Date from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "' and Status='Open'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblIMID.Text = rd["IMID"].ToString();
            txtDiaryRcvDate.Text = Convert.ToDateTime(rd["Date"]).ToString("dd/MM/yyyy");
            showcount(lblSessionHiddend.Text.ToString(), txtDiaryNo.Text.ToString());
          //  btnView.Enabled = true;
            btnView.Focus();
        }
        else
        {
            lblExceptionOK.Text = "Invalid Diary No. for " + lblIMName.Text.ToString();
            lblExceptionOK.ForeColor = System.Drawing.Color.Red;
            lblExceptionOK.Font.Bold = true;
            txtDiaryNo.Focus();
          //  btnView.Enabled = false;
        }
        rd.Close(); rd.Dispose();
        con.Close(); con.Dispose();
        
    }

    private void showcount(string session, string dairy)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from DairyCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        lblMsg.Text = "";
        while (reader.Read())
        {
            lblADDRcv.Text = reader["ADDRcv"].ToString();
            lblADDSub.Text = reader["ADDSub"].ToString();
            lblODDRcv.Text = reader["ODDRcv"].ToString();
            lblODDSub.Text = reader["ODDSub"].ToString();
            lblAdmissionRcv.Text = reader["EnrollFormRcv"].ToString();
            lblAdmissionSub.Text = reader["EnrollFormSub"].ToString();
            lblExamRcv.Text = reader["ExamFormRcv"].ToString();
            lblExamSub.Text = reader["ExamFormSub"].ToString();
            lblITIRcv.Text = reader["ITIRcv"].ToString(); lblITISub.Text = reader["ITISub"].ToString();
            lblCADRcv.Text = reader["CADRcv"].ToString(); lblCADSub.Text = reader["CADSub"].ToString();
            lblOthersFormRcv.Text = reader["OtherFormRcv"].ToString(); lblOthersFormSub.Text = reader["OtherFormSub"].ToString();
            lblProvisionalRcv.Text = reader["ProvisionalRcv"].ToString(); lblProvisionalSub.Text = reader["ProvisionalSub"].ToString();
            lblFinalPassRcv.Text = reader["FinalPassRcv"].ToString(); lblFinalPassSub.Text = reader["FinalPassSub"].ToString();
            lblReCheckingRcv.Text = reader["ReCheckingRcv"].ToString(); lblReCheckingSub.Text = reader["ReCheckingSub"].ToString();
            lblDuplicateRcv.Text = reader["DuplicateDocsRcv"].ToString(); lblDuplicateSub.Text = reader["DuplicateDocsSub"].ToString();
            lblMembershipRcv.Text = reader["MemberRcv"].ToString(); lblMembershipSub.Text = reader["MemberSub"].ToString();
            lblBooksRcv.Text = reader["BooksRcv"].ToString(); lblBooksSub.Text = reader["BooksSub"].ToString();
            lblProsRcv.Text = reader["ProspectusRcv"].ToString(); lblProsSub.Text = reader["ProspectusSub"].ToString();
        }
        reader.Close();
        cmd = new SqlCommand("select * from ProjectCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        SqlDataReader read;
        read = cmd.ExecuteReader();
        while (read.Read())
        {
            lblProjectRcv.Text = read["DDRcv"].ToString(); lblProjectSub.Text = read["DDSub"].ToString();
            lblProformaCRcv.Text = read["ProformaARcv"].ToString(); lblProformaCSub.Text = read["ProformaASub"].ToString();
            lblProformaBRcv.Text = read["ProformaBRcv"].ToString(); lblProformaBSub.Text = read["ProformaBSub"].ToString();
        }
        read.Close();
        con.Close();
        con.Dispose();
    }
    private void updateCount(string session, string dairy)
    {
        lblOthersFormSub.Text = (Convert.ToInt32(lblOthersFormSub.Text) + 1).ToString();
        cmd = new SqlCommand("update DairyCount set OtherFormSub=@OtherFormSub where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        cmd.Parameters.AddWithValue("@OtherFormSub", Convert.ToInt32(lblOthersFormSub.Text));
        cmd.ExecuteNonQuery();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        lblExceptionOK.Text = "";
        lblMsg.Text = ""; lblMsg.CssClass = "";
        con.Close(); con.Open();
        lblappno.Text = (apno()).ToString();
        cmd = new SqlCommand("select ExamCurrent.Course,ExamCurrent.Stream,ExamCurrent.Part,Student.FeeLevel,Student.Name,Student.FName,Student.FeeLevel,Student.DOB from ExamCurrent inner join Student on ExamCurrent.SID=Student.SID where ExamCurrent.SID='" + txtMem.Text.ToString() + "' and ExamCurrent.IMID='" + lblIMID.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            pnlMain.Visible = true;
            PnlMembership.Visible = true; pnlSpace.Visible = false;
            lblName.Text = reader["Name"].ToString(); lblFName.Text = reader["FName"].ToString(); lblDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("dd/MM/yyyy");
            lblCourse.Text = reader["Course"].ToString(); lblPart.Text = reader["Part"].ToString(); lblStream.Text = reader["Stream"].ToString();
            lblLvl.Text = reader["FeeLevel"].ToString();
            reader.Close();      
            btnSubmit.Enabled = true;
            PnlMembership.Visible = true;
            btnSubmit.Focus();
        }
        else { pnlMain.Visible = false; PnlMembership.Visible = false; lblExceptionOK.Text = "Invalid Membership"; lblExceptionOK.ForeColor = System.Drawing.Color.Red; pnlSpace.Visible = true; btnSubmit.Enabled = false; btnView.Focus(); }
        reader.Close(); reader.Dispose();
        con.Close(); con.Dispose();
       
    }
    private int apno()
    {
        cmd = new SqlCommand("select Max(AppNo) from AppRecord", con);
        string appno = Convert.ToString(cmd.ExecuteScalar());
        return Convert.ToInt32(appno) + 1;
    }
    protected void ddlForms_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeSession();
        FeeFeeType();      
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        con.Close(); con.Open();
        if ((Convert.ToInt32(lblOthersFormRcv.Text) > Convert.ToInt32(lblOthersFormSub.Text)))
        {
            SqlCommand cmdnnagar = new SqlCommand("insert into AppRecord(IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,Lavel,CompositeFees,AnnualSubFees,ITIFees,ExamFees,CADFees,underage,DupForm,SID,Exam,ITI,CAD,Project) values(@IMID,@AppNo,@Stream,@Course,@Part,@Name,@FName,@DOB,@DNo,@Session,@SubDate,@Status,@FormType,@FeeType,@Amount,@LateFee,@Exempted,@Enrolment,@AdmissionFees,@Lavel,@CompositeFees,@AnnualSubFees,@ITIFees,@ExamFees,@CADFees,@UnderAge,@DupForm,@SID,@Exam,@ITI,@CAD,@Project)", con);
            cmdnnagar.Parameters.AddWithValue("@IMID", lblIMID.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@AppNo", apno().ToString());
            cmdnnagar.Parameters.AddWithValue("@Stream", lblStream.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Course", lblCourse.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Part", lblPart.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Name", lblName.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@FName", lblFName.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@DOB", Convert.ToDateTime(lblDOB.Text.ToString(), dtinfo));
            cmdnnagar.Parameters.AddWithValue("@DNo", txtDiaryNo.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Session", lblSessionHiddend.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@SubDate", DateTime.Now);
            cmdnnagar.Parameters.AddWithValue("@Status", "NotApproved");
            cmdnnagar.Parameters.AddWithValue("@FormType", ddlForms.SelectedValue.ToString());
            cmdnnagar.Parameters.AddWithValue("@FeeType", ddlForms.SelectedValue.ToString());
            cmdnnagar.Parameters.AddWithValue("@Amount",lblFee.Text);
            cmdnnagar.Parameters.AddWithValue("@LateFee", "0");
            cmdnnagar.Parameters.AddWithValue("@Exempted", "0");
            cmdnnagar.Parameters.AddWithValue("@Enrolment", txtMem.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@AdmissionFees", "0");
            cmdnnagar.Parameters.AddWithValue("@Lavel", "0");
            cmdnnagar.Parameters.AddWithValue("@CompositeFees", "0");
            cmdnnagar.Parameters.AddWithValue("@AnnualSubFees", "0");
            cmdnnagar.Parameters.AddWithValue("@ITIFees", "0");
            cmdnnagar.Parameters.AddWithValue("@ExamFees", "0");
            cmdnnagar.Parameters.AddWithValue("@CADFees", "0");
            cmdnnagar.Parameters.AddWithValue("@UnderAge", "NO");
            cmdnnagar.Parameters.AddWithValue("@DupForm", "");
            cmdnnagar.Parameters.AddWithValue("@CAD", "");
            cmdnnagar.Parameters.AddWithValue("@Exam", "");
            cmdnnagar.Parameters.AddWithValue("@ITI", "");
            cmdnnagar.Parameters.AddWithValue("@Project", "");
            cmdnnagar.Parameters.AddWithValue("@SID", txtMem.Text.ToString());
            cmdnnagar.ExecuteNonQuery(); updateCount(lblSessionHiddend.Text, txtDiaryNo.Text);
            SqlDataAdapter ad = new SqlDataAdapter("select Enrolment,Name,FName,DOB,FormType,FeeType,Amount,DNo,SubDate,AppNo from AppRecord where  DNo='" + txtDiaryNo.Text.ToString() + "' order by AppNo DESC", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridAppTable.DataSource = dt;
            GridAppTable.DataBind();
            lblMsg.CssClass = "success"; lblMsg.Text = "Successfully Submitted";
       }
        else
        {
             lblMsg.Text = "All Forms Already Submitted";
        }
        btnSubmit.Enabled = false; pnlMain.Visible = false;
        txtMem.Text = "";
        con.Close();
        con.Dispose();
        btnView.Focus();
    }
    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = "DiaryNo";
            e.Row.Cells[0].Text = "Membership";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.ToString().TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[3].Text != null & e.Row.Cells[3].Text != "" & e.Row.Cells[3].Text != "&nbsp;")
                e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[8].Text != null & e.Row.Cells[8].Text != "" & e.Row.Cells[8].Text != "&nbsp;")
                e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void txtRechkNo_TextChanged(object sender, EventArgs e)
    {
     
    }
   private void  FeeName()
    {
  cmd = new SqlCommand("select FeeName from FeeList where Status='Yes' and Type='"+ddlFeeMaster.SelectedValue+"'",con); 
  ds = new DataSet();
  adapter = new SqlDataAdapter(cmd);
  adapter.Fill(ds);
  ddlForms.DataSource = ds.Tables[0];
  ddlForms.DataTextField = "FeeName";
  ddlForms.DataValueField = "FeeName";
  ddlForms.DataBind();
    }


   private void FeeSession()
   {
       cmd = new SqlCommand("select Session from FeeList where FeeName='" + ddlForms.SelectedItem.Text + "' and Type='"+ddlFeeMaster.SelectedValue+"'", con);
       ds = new DataSet();
       adapter = new SqlDataAdapter(cmd);
       adapter.Fill(ds);
       ddlFeeTypeSession.DataSource = ds.Tables[0];
       ddlFeeTypeSession.DataTextField = "Session";
       ddlFeeTypeSession.DataValueField = "Session";
       ddlFeeTypeSession.DataBind();
   }

   private void FeeFeeType()
   {
       con.Open();
       cmd = new SqlCommand("select Amount from FeeList where FeeName='" + ddlForms.SelectedItem.Text + "' and Session='"+ddlFeeTypeSession.SelectedItem.Text+"'", con);
       lblFee.Text =Convert.ToString(cmd.ExecuteScalar());
       con.Close();
   }
   protected void ddlFeeTypeSession_SelectedIndexChanged(object sender, EventArgs e)
   {
       FeeFeeType();
   }
   protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
   {
       txtDiaryNo.Text = "";
       lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
       txtSession.Focus();
   }
   protected void ddlFeeMaster_SelectedIndexChanged(object sender, EventArgs e)
   {
       txtDiaryNo.Text = "";
       ddlsession.Focus();
   }
}