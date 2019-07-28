using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Data.SqlClient;


public partial class Acc_AutoCAD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    Student st = new Student();
    ClsExamForm p2 = new ClsExamForm();
    ClsAutoCAD AutoCAD = new ClsAutoCAD();
   // string FormType;
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                PnlMembership.Visible = false; btnSubmit.Enabled = false;
                lblDupForm.Text="NO";
                lblMsg.Text = "";
                ddlStatus.Enabled = false;            
                con.Close(); con.Open();               
                lblCurrentBatch.Text = (AutoCAD.currentBatch(con)).ToString();
                con.Close(); con.Dispose();
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
        lblMsg.Text = "";
        con.Close(); con.Open();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        cmd = new SqlCommand("select IMID,Date from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "' and Status='Open'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblIMID.Text = rd["IMID"].ToString();          
            txtDiaryRcvDate.Text = Convert.ToDateTime(rd["Date"]).ToString("dd/MM/yyyy");
            lblDate.Text = Convert.ToDateTime(rd["Date"]).ToString("MM/dd/yyyy");
            showcount(lblSessionHiddend.Text.ToString(), txtDiaryNo.Text.ToString());
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
        lblCADSub.Text = (Convert.ToInt32(lblCADSub.Text) + 1).ToString();          
        cmd = new SqlCommand("update DairyCount set CADSub=@CADSub where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        cmd.Parameters.AddWithValue("@CADSub", Convert.ToInt32(lblCADSub.Text));      
        cmd.ExecuteNonQuery();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        lblMsg.Text = "";
        cmd = new SqlCommand("select Student.Part as studentpart,ExamCurrent.Course,ExamCurrent.Stream,ExamCurrent.Part as ExamCurrentpart,ExamCurrent.IMID,Student.FeeLevel,Student.Name,Student.FName,Student.FeeLevel,Student.DOB from ExamCurrent inner join Student on ExamCurrent.SID=Student.SID where ExamCurrent.SID='" + txtMem.Text.ToString() + "' ", con);
        SqlDataReader reader;
        bool flag = false;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
         string a=   reader["studentpart"].ToString();
      a =   reader["ExamCurrentpart"].ToString();
        if ((reader["studentpart"].ToString() == reader["ExamCurrentpart"].ToString()) && (reader["studentpart"].ToString() == "SectionA"))
        {
            lblExceptionOK.Text = "Invalid Membership No.(SectionA) for AutoCAD Registration"; lblExceptionOK.ForeColor = System.Drawing.Color.Red; pnlSpace.Visible = true; btnSubmit.Enabled = false; btnView.Focus();
            }
            else{
                pnlMain.Visible = true;
                PnlMembership.Visible = true; pnlSpace.Visible = false;
                lblName.Text = reader["Name"].ToString(); lblFName.Text = reader["FName"].ToString(); lblDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("dd/MM/yyyy");
                lblCourse.Text = reader["Course"].ToString(); lblPart.Text = reader["ExamCurrentPart"].ToString(); lblStream.Text = reader["Stream"].ToString();
                lblLvl.Text = reader["FeeLevel"].ToString();
                if (lblIMID.Text.ToString() != reader["IMID"].ToString())
                    lblExceptionOK.Text = "This Membership Not Associated to: " + lblIMID.Text.ToString();
                else
                    flag = true;
                btnSubmit.Enabled = true;
                PnlMembership.Visible = true;
            }
        }
        else
        {
            pnlMain.Visible = false; PnlMembership.Visible = false;
            lblExceptionOK.Text = "Invalid Membership No."; lblExceptionOK.ForeColor = System.Drawing.Color.Red; pnlSpace.Visible = true; btnSubmit.Enabled = false; btnView.Focus();
        }
        reader.Close(); reader.Dispose();
        if (flag == true & (lblPart.Text == "PartII" | lblPart.Text == "SectionB" | lblPart.Text=="SectionA"))
        {
            lblExceptionOK.Text = "";
            lblMsg.Text = ""; lblDate1.Text = ""; lblDate2.Text = ""; lblBatchID.Text = "";
            lblStatus.Text = "";
                lblFee.Text = "";
                string status = AutoCAD.Status(txtMem.Text, con);
                int BatchID = AutoCAD.BatchID(txtMem.Text, con);
                if (BatchID == 0)
                {
                    BatchID = Convert.ToInt32(lblCurrentBatch.Text);
                }
                    string[] BFee = new string[2];
                    BFee = AutoCAD.BatchFee(ddlFeeMaster.SelectedValue, BatchID, con);
                    DateTime[] Date = new DateTime[2];
                    Date = AutoCAD.BatchDate(BatchID, con);
                    lblDate1.Text = "Batch Start:" + Convert.ToDateTime(Date[0]).ToString("dd/MM/yyyy");
                    lblDate2.Text = "Batch End:" + Convert.ToDateTime(Date[1]).ToString("dd/MM/yyyy");
                    lblBatchID.Text = Convert.ToString(BatchID);
                    if (status == "LateFee")
                    {
                        DateTime dt1;
                        DateTime dt2;
                        string Enterdate = Convert.ToDateTime(Date[1]).ToString("MM/dd/yyyy");
                        dt1 = Convert.ToDateTime(lblDate.Text.Trim());
                        dt2 = Convert.ToDateTime(Enterdate.Trim());
                        DateTime date1 = new DateTime(dt1.Year, dt1.Month, dt1.Day);
                        DateTime date2 = new DateTime(dt2.Year, dt2.Month, dt2.Day);
                        int result = DateTime.Compare(date1, date2);
                        if (result > 0)
                        {
                            lblAmount.Text = BFee[1].TrimEnd('0').TrimEnd('.');
                            lblFormType.Text = "MCADRegistration";
                            ddlStatus.SelectedValue = "Registered";
                            lblFee.Text = "Re-Registered Fee";
                            lblStatus.Text = "LateFee is Out of Batch Duration Range. Re-Registration Fee is applicable.";
                        }
                        else
                        {
                            lblAmount.Text = BFee[0].TrimEnd('0').TrimEnd('.');
                            lblFormType.Text = "MCADLateFee";
                            ddlStatus.SelectedValue = "LateFee";
                            lblFee.Text = "LateFee";
                            lblStatus.Text = "Late Fee is applicable.";
                        }
                    }
                    else if (status == "Re-Registered")
                    {
                        lblAmount.Text = BFee[1].TrimEnd('0').TrimEnd('.');
                        lblFormType.Text = "MCADRegistration";
                        ddlStatus.SelectedValue = "Registered";
                        lblFee.Text = "Re-Registered Fee";
                        lblStatus.Text = "Re-Registration Fee is applicable.";
                    }
                    else
                    {
                        lblBatchID.Text = Convert.ToString(BatchID);
                        lblAmount.Text = BFee[1].TrimEnd('0').TrimEnd('.');
                        lblFormType.Text = "MCADRegistration";
                        ddlStatus.SelectedValue = "Registered";
                        lblFee.Text = "Registered Fee";
                        lblStatus.Text = "Registration Fee is applicable.";
                    }
                    lblappno.Text = (apno()).ToString();
              lblCADSerialNo.Text = serialno("MCAD".ToString());
         }
        else if (flag == false)
        {
            lblExceptionOK.Text = "Invalid Membership No, Could be wrong IMID( This Membership Not Associated to: " + lblIMID.Text.ToString()+" )";
            pnlMain.Visible = true;
            PnlMembership.Visible = false; pnlSpace.Visible = false;
        }
        else
        {
            lblExceptionOK.Text = "InValid Membership, Please Select PartII or SectionB Student.";
            pnlMain.Visible = true;
            PnlMembership.Visible = false; pnlSpace.Visible = false;
        }
        con.Close(); con.Dispose();
    }
    private int apno()
    {
        cmd = new SqlCommand("select Max(AppNo) from AppRecord", con);
        string appno = Convert.ToString(cmd.ExecuteScalar());
        return Convert.ToInt32(appno) + 1;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        con.Close(); con.Open();
        lblMsg.Text = "";
        if ((Convert.ToInt32(lblCADRcv.Text) > Convert.ToInt32(lblCADSub.Text)))
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
            cmdnnagar.Parameters.AddWithValue("@FormType", lblFormType.Text);
            cmdnnagar.Parameters.AddWithValue("@FeeType",lblFee.Text);
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
            cmdnnagar.Parameters.AddWithValue("@CAD", lblCADSerialNo.Text);
            cmdnnagar.Parameters.AddWithValue("@Exam", "");
            cmdnnagar.Parameters.AddWithValue("@ITI", "");
            cmdnnagar.Parameters.AddWithValue("@Project", "");
            cmdnnagar.Parameters.AddWithValue("@SID", txtMem.Text.ToString());
            cmdnnagar.ExecuteNonQuery();
            updateCount(lblSessionHiddend.Text, txtDiaryNo.Text);
            SqlDataAdapter ad = new SqlDataAdapter("select Enrolment,Name,FName,DOB,FormType,FeeType,Amount,DNo,SubDate,AppNo from AppRecord where  DNo='" + txtDiaryNo.Text.ToString() + "' order by AppNo DESC", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridAppTable.DataSource = dt;
            GridAppTable.DataBind();
            lblMsg.CssClass = "success"; lblMsg.Text = "Successfully Submitted";
            lblFormType.Text = "";
            updateserialno("MCAD", lblSessionHiddend.Text.ToString());
       }
        else
        {
             lblMsg.Text = "All Forms Already Submitted";
             lblMsg.ForeColor = System.Drawing.Color.Red;
        }
        btnSubmit.Enabled = false; pnlMain.Visible = false;lblCADSerialNo.Text="";
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
   protected void chkStatus_CheckedChanged(object sender, EventArgs e)
   {
       if (chkStatus.Checked == true)
           ddlStatus.Enabled = true;
       else
           ddlStatus.Enabled= false;
   }
   protected void chkDupForm_CheckedChanged(object sender, EventArgs e)
   {
       pnlMain.Visible = true;
       if (chkDupForm.Checked == true)
           lblDupForm.Text = "NO";
       else
           lblDupForm.Text = "YES";
   }
   protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       pnlMain.Visible = true;
       con.Open();
       string[] BFee = new string[2];
       BFee = AutoCAD.BatchFee(ddlFeeMaster.SelectedValue, Convert.ToInt32(lblBatchID.Text), con);
       con.Close();
       if (ddlStatus.SelectedValue == "LateFee")
       {
           lblAmount.Text = BFee[0].TrimEnd('0').TrimEnd('.');
           lblFormType.Text = "MCADLateFee";
           lblFee.Text = "Late Fee";
           lblStatus.Text = "Late Fee is applicable.";
       }
       else
       {
           lblAmount.Text = BFee[1].TrimEnd('0').TrimEnd('.');
           lblFormType.Text = "MCADRegistration";
           lblFee.Text = "Registered Fee";
           lblStatus.Text = "RegistrationFee is applicable.";
       }
   }
   private string serialno(string type)
   {
       cmd = new SqlCommand("select SerialNo from FeeList where FeeName='" + type.ToString() + "' and Status='NO' and Session='" + lblSessionHiddend.Text.ToString() + "'", con);
       string rtnsn = Convert.ToString(cmd.ExecuteScalar());
       if (rtnsn == "")
       {
           cmd = new SqlCommand("insert into FeeList (FeeName,Status,Session) values(@FeeName,@Status,@Session)", con);
           cmd.Parameters.AddWithValue("@FeeName", type.ToString());
           cmd.Parameters.AddWithValue("@Status", "NO");
           cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text.ToString());
           cmd.ExecuteNonQuery();
           rtnsn = "0";
       }
       string fw = type.Substring(0, 1);
       int intsn = Convert.ToInt32(rtnsn) + 1;
       rtnsn = intsn.ToString();
       return fw.ToString() + "" + rtnsn.ToString();
   }
   private void updateserialno(string type, string session)
   {
       lblMsg.Text = "";
       try
       {
           cmd = new SqlCommand("update Feelist set SerialNo=SerialNo+1 where FeeName='" + type.ToString() + "' and Session='" + session.ToString() + "'", con);
           cmd.ExecuteNonQuery();
       }
       catch (SqlException ex)
       {
         lblMsg.Text += type.ToString() + " SerialNo. Not Updated.";
       }
   }
}        