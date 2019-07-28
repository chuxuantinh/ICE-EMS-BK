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
using System.Xml;

public partial class Acc_AddApps2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    Student st = new Student();
    ClsExamForm p2 = new ClsExamForm();
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
                PnlMembership.Visible = false; pnlSpace.Visible = true; btnSubmit.Enabled = false; invisible(); pnlMain.Visible = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                SessionDuration sd=new SessionDuration();
                lblSessionID.Text = sd.SessionToSessionID(lblSessionHiddend.Text.ToString()).ToString();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = "";
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = "";
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        cmd = new SqlCommand("select IMID,Date from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "' and Status='Open'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblIMID.Text = rd["IMID"].ToString();
            txtDiaryRcvDate.Text = Convert.ToDateTime(rd["Date"]).ToString("dd/MM/yyyy");
            showcount(lblSessionHiddend.Text.ToString(), txtDiaryNo.Text.ToString()); visible();
            ddlForms.Enabled = true;
        }
        else
        {
            lblExceptionOK.Text = "Invalid Diary No. for " + lblIMName.Text.ToString();
            lblExceptionOK.ForeColor = System.Drawing.Color.Red;
            lblExceptionOK.Font.Bold = true;
            txtDiaryNo.Focus();
        }
        rd.Close(); rd.Dispose();
        con.Close(); con.Dispose();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        lblMsg.Text = ""; lblMsg.CssClass = ""; bool flag = false; ddlFinalPassCourse.SelectedIndex = 0;
        con.Close(); con.Open();
        btnSubmit.Enabled = true;
        lblappno.Text = (apno()).ToString();
        cmd = new SqlCommand("select ExamCurrent.Course,ExamCurrent.Stream,ExamCurrent.Part,Student.FeeLevel,Student.Name,Student.FName,Student.FeeLevel,Student.DOB from ExamCurrent inner join Student on ExamCurrent.SID=Student.SID where ExamCurrent.SID='" + txtMem.Text.ToString() + "' and ExamCurrent.IMID='"+lblIMID.Text.ToString()+"'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            pnlMain.Visible = true;
            PnlMembership.Visible = true;  pnlSpace.Visible = false;
            lblName.Text = reader["Name"].ToString(); lblFName.Text = reader["FName"].ToString(); lblDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("dd/MM/yyyy");
            lblCourse.Text = reader["Course"].ToString(); lblPart.Text = reader["Part"].ToString(); lblStream.Text = reader["Stream"].ToString();
            lblLvl.Text = reader["FeeLevel"].ToString();
            reader.Close();
            feemaster();    // Select Fees master         
            btnSubmit.Enabled = true; btnSubmit.Visible = true;
            flag = true;
            visible();
        }
        else { pnlMain.Visible = false; PnlMembership.Visible = false; lblExceptionOK.Text = "Invalid Membership"; pnlSpace.Visible = true; btnSubmit.Enabled = false; }
        reader.Close(); 
        if (flag == true && ddlForms.SelectedValue == "FinalPass")
        {
            FinalPassCourse();
        }
        else
        {

        }
        reader.Close(); reader.Dispose();
        con.Close(); con.Dispose();
    }
    protected void ddlFinalPassCoure_SelectedIndexChenged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        FinalPassCourse();
        con.Close(); con.Dispose();       
    }

    private void FinalPassCourse()
    {
        string query = "";
        bool flag = true;
        if (ddlFinalPassCourse.SelectedValue == "CivilPartII")
        {
            query = "select SessionID,Part,Certificate from SFinalPass where SID='" + txtMem.Text.ToString() + "' and Course='Civil' and Part='PartII' ";
            lblPart.Text = "PartII";
        }
        else if (ddlFinalPassCourse.SelectedValue == "CivilSectionB")
        {
            query = "select SessionID,Part,Certificate from SFinalPass where SID='" + txtMem.Text.ToString() + "' and Course='Civil' and Part='SectionB' ";
            lblPart.Text = "SectionB";
        }
        else if (ddlFinalPassCourse.SelectedValue == "ArchiPartII")
        {
            query = "select SessionID,Part,Certificate from SFinalPass where SID='" + txtMem.Text.ToString() + "' and Course='Architecture' and Part='PartII'";
            lblPart.Text = "PartII";
        }
        else if (ddlFinalPassCourse.SelectedValue == "ArchiSectionB")
        {
            query = "select SessionID,Part,Certificate from SFinalPass where SID='" + txtMem.Text.ToString() + "' and Course='Architecture' and Part='SectionB' ";
            lblPart.Text = "SectionB";
        }
        else
        {
            lblExceptionOK.Text = "Please Select Course";
            flag = false;
        }
        if (flag == true)
        {
            cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            string strPart = "";
            if (reader.Read())
            {
                lblSessionID.Text = reader["SessionID"].ToString();
                strPart = reader["Part"].ToString();
                finalpassFees(lblSessionID.Text, strPart); // Get FinalPass(Certificate Fees) according to SessionID
                btnSubmit.Enabled = true;
                lblExceptionOK.Text = "";
            }
            else
            {
                lblExceptionOK.Text = "";
                lblExceptionOK.ForeColor = System.Drawing.Color.Red;
                SessionDuration sd = new SessionDuration();
                lblSessionID.Text = sd.SessionToSessionID(lblSessionHiddend.Text.ToString()).ToString();
                finalpassFees(lblSessionID.Text, lblPart.Text); // Get FinalPass(Certificate Fees) according to SessionID
                btnSubmit.Enabled = true;
            }
        }
       
    }
    private int apno()
    { 
        cmd=new SqlCommand("select Max(AppNo) from AppRecord",con);
        string appno=Convert.ToString(cmd.ExecuteScalar());
        return Convert.ToInt32(appno)+1;
    }
    private void feemaster()
    {
        string strStream = "";
        if (lblPart.Text == "PartI" || lblPart.Text == "PartII")
            strStream = "Tech";
        else strStream = "Asso";
        cmd = new SqlCommand("select * from FeeMaster where FeeType='" +strStream.ToString()+ "' and FeeLevel='" + lblLvl.Text.ToString() + "' and Type='" + ddlFeeMaster.SelectedValue.ToString() + "'", con);
        SqlDataReader read = cmd.ExecuteReader();
        if (read.Read())
        {
            lblExamCenter.Text = read["ExamCChange"].ToString().TrimEnd('0').TrimEnd('.');
            lblReChecking.Text = read["ReChacking"].ToString().TrimEnd('0').TrimEnd('.');
            lblProCerti.Text = read["PCertificate"].ToString().TrimEnd('0').TrimEnd('.');
            //lblFinal.Text = read["Certification"].ToString().TrimEnd('0').TrimEnd('.');
            lblIDCard.Text = read["IDCard"].ToString().TrimEnd('0').TrimEnd('.');
            lblAdmitCard.Text = read["AdminCard"].ToString().TrimEnd('0').TrimEnd('.');
            lblMStatement.Text = read["MStatement"].ToString().TrimEnd('0').TrimEnd('.');
            lblMemCertificate.Text = read["MCertificate"].ToString().TrimEnd('0').TrimEnd('.');
        }
        read.Close(); read.Dispose();
    }
    protected void ddlForms_SelectedIndexChanged(object sender, EventArgs e)
    {
        visible();
        txtMem.Text = ""; PnlMembership.Visible = false; btnSubmit.Visible = false;
        txtMem.Focus();
    }
    private void invisible()
    {
        PanelAdmit.Visible=false;PanelCertificate.Visible=false;PanelExamCenter.Visible=false;PanelFinalPass.Visible=false;PanelID.Visible=false;PanelMembership.Visible=false;PanelMStatement.Visible=false;PanelRechecking.Visible=false;
    }
    private void visible()
    {
        if (ddlForms.SelectedValue.ToString() == "ExamCenter")
        {
            lblExceptionOK.Text = "";
            if (Convert.ToInt32(lblOthersFormRcv.Text) > Convert.ToInt32(lblOthersFormSub.Text))
            {
                PanelExamCenter.Visible = true;
                lbllavel.Text = "1"; lblAmount.Text = lblExamCenter.Text;
            }
            else lblExceptionOK.Text = "All Form Submitted.";
        }
        else{ PanelExamCenter.Visible = false;}
        
         if (ddlForms.SelectedValue.ToString() == "Rechecking")
        {
            lblExceptionOK.Text = "";
            if (Convert.ToInt32(lblReCheckingRcv.Text) > Convert.ToInt32(lblReCheckingSub.Text))
            {
                txtRechkNo.Text = "1";
                PanelRechecking.Visible = true; lblAmount.Text = lblReChecking.Text;
                lbllavel.Text = "1";
            }
            else lblExceptionOK.Text = "All Form Submitted.";
           
        }
        else { PanelRechecking.Visible = false; }
         if (ddlForms.SelectedValue.ToString() == "FinalPass")
         {
             if (Convert.ToInt32(lblFinalPassRcv.Text) > Convert.ToInt32(lblFinalPassSub.Text))
             {
                 lblExceptionOK.Text = "";
                 PanelFinalPass.Visible = true; lblAmount.Text = lblFinal.Text;
                 lbllavel.Text = "3";
             }
             else lblExceptionOK.Text = "All Final Marksheet Form Submitted.";
         }
         else
         {
             PanelFinalPass.Visible = false;
         }
         if (ddlForms.SelectedValue.ToString() == "ProvisionalCertificate")
        {
            if (Convert.ToInt32(lblProvisionalRcv.Text) > Convert.ToInt32(lblProvisionalSub.Text))
            {
                lblExceptionOK.Text = "";
                PanelCertificate.Visible = true; lblAmount.Text = lblProCerti.Text;
                lbllavel.Text = "3";
            }
            else  lblExceptionOK.Text = "All Form Submitted.";
         }
         else
         {
             PanelCertificate.Visible = false;
         }
         if (ddlForms.SelectedValue.ToString() == "IDCard" | ddlForms.SelectedValue.ToString() == "AdmitCard" | ddlForms.SelectedValue.ToString() == "MarksStatement" | ddlForms.SelectedValue.ToString() == "MembershipCertificate")
        {
            if (Convert.ToInt32(lblDuplicateRcv.Text) > Convert.ToInt32(lblDuplicateSub.Text))
            {
                lblExceptionOK.Text = "";
                if (ddlForms.SelectedValue == "IDCard") { PanelID.Visible = true; lblAmount.Text = lblIDCard.Text; } else PanelID.Visible=false;
                if (ddlForms.SelectedValue == "AdmitCard") { PanelAdmit.Visible = true; lblAmount.Text = lblAdmitCard.Text; } else PanelAdmit.Visible=false;
                 if (ddlForms.SelectedValue == "MarksStatement") { PanelMStatement.Visible = true; lblAmount.Text = lblMStatement.Text; } else PanelMStatement.Visible=false;
                 if (ddlForms.SelectedValue == "MembershipCertificate") { PanelMembership.Visible = true;lblAmount.Text=lblMemCertificate.Text; } else PanelMembership.Visible=false;
                
            }
            else lblExceptionOK.Text = "All Duplicate Docs Submitted";
         }
        else {PanelID.Visible = false; PanelAdmit.Visible = false; PanelMStatement.Visible = false; PanelMembership.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    { 
        dtinfo.ShortDatePattern="dd/MM/yyyy";
        dtinfo.DateSeparator="/";
        con.Close(); con.Open();
        if (lblExceptionOK.Text == "")
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
            cmdnnagar.Parameters.AddWithValue("@Amount", lblAmount.Text);
            cmdnnagar.Parameters.AddWithValue("@LateFee", "0");
            cmdnnagar.Parameters.AddWithValue("@Exempted", "0");
            cmdnnagar.Parameters.AddWithValue("@Enrolment", txtMem.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@AdmissionFees", "0");
            cmdnnagar.Parameters.AddWithValue("@Lavel", lblDupForm.Text.ToString());
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
            if (ddlForms.SelectedValue == "FinalPass")
            {
                cmd = new SqlCommand("update SFinalPass set Certificate='Process' where SessionID='" + lblSessionID.Text.ToString() + "' and SID='" + txtMem.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
            }
            SqlDataAdapter ad = new SqlDataAdapter("select Enrolment,Name,FName,DOB,FormType,FeeType,Amount,DNo,SubDate,AppNo from AppRecord where  DNo='" + txtDiaryNo.Text.ToString() + "' order by AppNo DESC", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridAppTable.DataSource = dt;
            GridAppTable.DataBind();
            lblMsg.CssClass = "success"; lblMsg.Text = "Successfully Submitted";
        }
        else
        {
            lblMsg.CssClass = "error"; lblMsg.Text = "All Forms Already Submitted";
        }
        btnSubmit.Enabled = false; pnlMain.Visible = false;
        txtMem.Text = "";
        con.Close();
        con.Dispose();
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
        lblAmount.Text = (Convert.ToInt32(lblReChecking.Text) * Convert.ToInt32(txtRechkNo.Text)).ToString();
    }
    protected void chkDupForm_CheckedChanged(object sender, EventArgs e)
    {
        pnlMain.Visible = true;
        if (chkDupForm.Checked == true)
            lblDupForm.Text = "NO";
        else
            lblDupForm.Text = "YES";
    }
    #region methods
    private void showcount(string session, string dairy)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from DairyCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
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
        if (ddlForms.SelectedValue.ToString() == "ExamCenter")
        {
            lblOthersFormSub.Text = (Convert.ToInt32(lblOthersFormSub.Text) + 1).ToString();
        }
        if (ddlForms.SelectedValue.ToString() == "ProvisionalCertificate")
        {
            lblProvisionalSub.Text = (Convert.ToInt32(lblProvisionalSub.Text) + 1).ToString();
        }
        if (ddlForms.SelectedValue.ToString() == "Rechecking")
        {
            lblReCheckingSub.Text = (Convert.ToInt32(lblReCheckingSub.Text) + 1).ToString();
        }
        if (ddlForms.SelectedValue.ToString() == "FinalPass")
        {
            lblFinalPassSub.Text = (Convert.ToInt32(lblFinalPassSub.Text) + 1).ToString();
        }
        if (ddlForms.SelectedValue.ToString() == "IDCard" | ddlForms.SelectedValue.ToString() == "AdmitCard" | ddlForms.SelectedValue.ToString() == "MarksStatement" | ddlForms.SelectedValue.ToString() == "MembershipCertificate")
        {
            lblDuplicateSub.Text = (Convert.ToInt32(lblDuplicateSub.Text) + 1).ToString();
        }
        cmd = new SqlCommand("update DairyCount set EnrollFormSub=@EnrollFormSub,CADSub=@CADSub,ExamFormSub=@ExamFormSub,ITISub=@ITISub,OtherFormSub=@OtherFormSub,ProvisionalSub=@ProvisionalSub,FinalPassSub=@FinalPassSub,ReCheckingSub=@ReCheckingSub,DuplicateDocsSub=@DuplicateDocsSub where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        cmd.Parameters.AddWithValue("@EnrollFormSub", Convert.ToInt32(lblAdmissionSub.Text));
        cmd.Parameters.AddWithValue("@ExamFormSub", Convert.ToInt32(lblExamSub.Text));
        cmd.Parameters.AddWithValue("@CADSub", Convert.ToInt32(lblCADSub.Text));
        cmd.Parameters.AddWithValue("@ITISub", Convert.ToInt32(lblITISub.Text));
        cmd.Parameters.AddWithValue("@OtherFormSub", Convert.ToInt32(lblOthersFormSub.Text));
        cmd.Parameters.AddWithValue("@ProvisionalSub", Convert.ToInt32(lblProvisionalSub.Text));
        cmd.Parameters.AddWithValue("@FinalPassSub", Convert.ToInt32(lblFinalPassSub.Text));
        cmd.Parameters.AddWithValue("@ReCheckingSub", Convert.ToInt32(lblReCheckingSub.Text));
        cmd.Parameters.AddWithValue("@DuplicateDocsSub", Convert.ToInt32(lblDuplicateSub.Text));
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("update ProjectCount set ProformaASub='" + lblProformaCSub.Text + "',ProformaBSub='" + lblProformaBSub.Text + "' where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        cmd.ExecuteNonQuery();
    }
    private void finalpassFees(string sessionid,string stream)
    {
        if (stream == "PartI" || stream == "PartII")
            stream = "Technician";
        else stream = "Associate";
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/FinalPassFees.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        string price = "0";
        foreach (XmlNode nd in nlist)
        {
            if (sessionid == nd.Attributes["SessionID"].Value.ToString())
            {
                price = nd.Attributes[stream].Value.ToString();
            }
        }
        lblFinal.Text = price.ToString();
        lblAmount.Text = lblFinal.Text.ToString();
    }
    #endregion
}