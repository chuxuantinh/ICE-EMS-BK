using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class Acc_AddAppsPro : System.Web.UI.Page
{
    #region var;
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    IMInfo IMcls;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    #endregion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDiaryNo.Focus();
            clear(0);
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    protected void btnViewDiary_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select DiaryEntry.IMID,DiaryEntry.Date,IM.Name,ProjectCount.Session from ProjectCount inner join DiaryEntry on ProjectCount.DairyNo=DiaryEntry.DiaryNo inner join IM on DiaryEntry.IMID=IM.ID and ProjectCount.DairyNo='" + txtDiaryNo.Text.ToString() + "' and (ProjectCount.ProformaARcv!=ProjectCount.ProformaASub or ProjectCount.ProformaBRcv!=ProjectCount.ProformaBSub)", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblIMID.Text = reader["IMID"].ToString();
            lblIMName.Text = reader["Name"].ToString();
            lblDairyDate.Text = Convert.ToDateTime(reader["Date"].ToString()).ToString("dd/MM/yyyy");
            lblSession.Text = reader["Session"].ToString();
            lblException.Attributes.Add("class", "hide");
            clear(3);
            IMcls = new IMInfo();
            lblFeemaster.Text = IMcls.imFeeMaster(lblIMID.Text.ToString());
            txtSID.Focus();
        }
        else
        {
            lblException.Text = "Diary Not Found.";
            lblException.Attributes.Add("class", "error");
            clear(1);
        }
        showcount(lblSession.Text, txtDiaryNo.Text, reader);
        reader.Close();
        reader.Dispose();
        con.Close(); con.Dispose();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select Project.Status,Project.SynopsisStatus,Project.ApprovalFees,Project.EvalutionFees,Student.Name,Student.FName,Student.FeeLevel,Student.DOB,Student.Prifix,Project.Course,project.Part from Project inner join Student on Project.SID=Student.SID where Project.SID='" + txtSID.Text.ToString() + "' and Project.EntryStatus='Running'", con);
        SqlDataReader reader;
        bool flag = false; int count = 0;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblProStatus.Text = reader["Status"].ToString();
            lblSynopsisStatus.Text = reader["SynopsisStatus"].ToString();
            lblProformaBFees.Text = reader["ApprovalFees"].ToString().TrimEnd('0').TrimEnd('.');
            lblProformaCFees.Text = reader["EvalutionFees"].ToString().TrimEnd('0').TrimEnd('.');
            lblStudentName.Text = reader["Name"].ToString();
            lblPrifix.Text= reader["Prifix"].ToString();
            lblFName.Text= reader["FName"].ToString();
            lblFeeLevel.Text = reader["FeeLevel"].ToString();
            lblCourse.Text=reader["Course"].ToString();
            lblPart.Text=reader["Part"].ToString();
            lblDoB.Text = reader["DOB"].ToString();
            lblException.Attributes.Add("class", "hide");
            flag = true; count = count + 1;
            ddlProforma.Focus();
        }
        reader.Close();
        if (flag == true)
        {
            esngn();  // AppNo: Application No.
           
            feemaster(lblPart.Text.ToString(),lblFeeLevel.Text.ToString(),lblFeemaster.Text.ToString());
            chkDuplicate();
            if (lblProformaBFees.Text == "0") lblSubmitFees.Text = lblProjectApproval.Text.ToString();
            else if (lblProformaCFees.Text == "0") lblSubmitFees.Text = lblProjectEvaluation.Text.ToString();
            else
            {
                lblException.Text = "AleradyProject Fees Submitted."; lblException.Attributes.Add("class", "error");
            }
            if (ddlProforma.SelectedValue.ToString() == "ProformaB")
                lblProjSerialNo.Text = serialno("BProforma");
            else
                lblProjSerialNo.Text = serialno("CProforma");
        }
        else
        {
            lblException.Text = "Project Record Not Found.";
            lblException.Attributes.Add("class", "error");
            clear(2);
            txtSID.Focus();
        }
        if (count >= 2)
        {
            lblException.Text = "Duplicate Record Found: Please Set Entry Status:OldProject of Previous Project Record.";
            lblException.Attributes.Add("class", "error");
            btnSubmit.Enabled = false;
        }
         reader.Dispose();
        con.Close(); con.Dispose();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        if (chkDuplicate() == true)
        {
            con.Close(); con.Open();
            string stream = "";
            if (lblPart.Text == "PartI" | lblPart.Text == "PartII")
                stream = "Tech";
            else stream = "Asso";
            if (ddlProforma.SelectedValue.ToString() == "ProformaB")
            {
                updateserialno("BProforma", lblSession.Text.ToString());
            }
            else if (ddlProforma.SelectedValue.ToString() == "ProformaC")
            {
                updateserialno("CProforma", lblSession.Text.ToString());
            }
            SqlCommand cmdnnagar = new SqlCommand("insert into AppRecord(IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,Lavel,CompositeFees,AnnualSubFees,ITIFees,ExamFees,CADFees,underage,DupForm,SID,Exam,ITI,CAD,Project) values(@IMID,@AppNo,@Stream,@Course,@Part,@Name,@FName,@DOB,@DNo,@Session,@SubDate,@Status,@FormType,@FeeType,@Amount,@LateFee,@Exempted,@Enrolment,@AdmissionFees,@Lavel,@CompositeFees,@AnnualSubFees,@ITIFees,@ExamFees,@CADFees,@UnderAge,@DupForm,@SID,@Exam,@ITI,@CAD,@Project)", con);
            cmdnnagar.Parameters.AddWithValue("@IMID", lblIMID.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@AppNo", Convert.ToInt32(lblserialNo.Text.ToString()));
            cmdnnagar.Parameters.AddWithValue("@Stream", stream);
            cmdnnagar.Parameters.AddWithValue("@Course", lblCourse.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Part", lblPart.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Name", lblStudentName.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@FName", lblFName.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@DOB", lblDoB.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@DNo", txtDiaryNo.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Session", lblSession.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(lblDairyDate.Text, dtinfo));
            cmdnnagar.Parameters.AddWithValue("@Status", "NotApproved");
            cmdnnagar.Parameters.AddWithValue("@FormType",ddlProforma.SelectedValue.ToString());
            cmdnnagar.Parameters.AddWithValue("@FeeType", ddlProforma.SelectedValue.ToString());
            cmdnnagar.Parameters.AddWithValue("@Amount", lblSubmitFees.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@LateFee", 0);
            cmdnnagar.Parameters.AddWithValue("@Exempted", 0);
            cmdnnagar.Parameters.AddWithValue("@Enrolment", txtSID.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@AdmissionFees", 0);
            cmdnnagar.Parameters.AddWithValue("@Lavel", lblDupForm.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@CompositeFees", 0);
            cmdnnagar.Parameters.AddWithValue("@AnnualSubFees", 0);
            cmdnnagar.Parameters.AddWithValue("@ITIFees", 0);
            cmdnnagar.Parameters.AddWithValue("@ExamFees", 0);
            cmdnnagar.Parameters.AddWithValue("@CADFees", 0);
            cmdnnagar.Parameters.AddWithValue("@UnderAge", 0);
            cmdnnagar.Parameters.AddWithValue("@DupForm", 0);
            cmdnnagar.Parameters.AddWithValue("@SID", txtSID.Text.ToString());
            cmdnnagar.Parameters.AddWithValue("@Exam", "");
            cmdnnagar.Parameters.AddWithValue("@ITI", "");
            cmdnnagar.Parameters.AddWithValue("@CAD", "");
            cmdnnagar.Parameters.AddWithValue("@Project", lblProjSerialNo.Text.ToString());
            cmdnnagar.ExecuteNonQuery();
            string qry = "", qry1 = "";
            if (ddlProforma.SelectedValue.ToString() == "ProformaB")
            {
                qry = "Update Project set ApprovalFees='" + lblSubmitFees.Text.ToString() + "',DiaryB='"+lblserialNo.Text+"/"+lblProjSerialNo.Text.ToString()+"' where SID='" + txtSID.Text.ToString() + "'  and EntryStatus='Running'";
                qry1 = "update ProjectCount set ProformaBSub=ProformaBSub+1 where DairyNo='" + txtDiaryNo.Text + "'";
                lblProformaBFees.Text = lblProjectApproval.Text.ToString();
            }
            else
            {
                qry = "Update Project set EvalutionFees='" + lblSubmitFees.Text.ToString() + "',DiaryC='" + lblserialNo.Text + "/" + lblProjSerialNo.Text.ToString() + "' where SID='" + txtSID.Text.ToString() + "'  and EntryStatus='Running'";
                qry1 = "update ProjectCount set ProformaASub=ProformaASub+1 where DairyNo='" + txtDiaryNo.Text + "'";
                lblProformaCFees.Text = lblProjectEvaluation.Text.ToString();
            }
            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(qry1, con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter ad = new SqlDataAdapter("select Enrolment,Name,FName,DOB,FormType,FeeType,Amount,DNo,SubDate,AppNo from AppRecord where  DNo='" + txtDiaryNo.Text.ToString() + "' and (FormType='ProformaB' or FormType='ProformaC') order by AppNo DESC", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridAppTable.DataSource = dt;
            GridAppTable.DataBind();
            con.Close(); con.Dispose();
            btnSubmit.Enabled = false;
        }
        else
        {
            lblException.Text = "Duplicate Record Found.";
            lblException.Attributes.Add("class", "error");
        }
    }
    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
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

    #region methods
    private void clear(int id)
    {
        pnlMain.Visible = true; pnlSub.Visible = true;
        if (id == 1)  // Diary Not Found.
        {
            pnlMain.Visible = false;
        }
        else if(id==2) // SID Not found.
        {
            pnlSub.Visible = false;
        }
        else if (id == 0)  // Page_Load
        {
            pnlMain.Visible = false;
            pnlHeight.Visible = true;
        }
        else if (id == 3) // Read Successfully.
        {
            pnlMain.Visible = true;
            pnlSub.Visible = true;
            lblException.Attributes.Add("class", "hide");
        }
    }
    private void showcount(string session, string dairy,SqlDataReader reader)
    {
        reader.Close();
        cmd = new SqlCommand("select * from DairyCount where DairyNo='" + dairy.ToString() + "'", con);
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
        cmd = new SqlCommand("select * from ProjectCount where DairyNo='" + dairy.ToString() + "'", con);
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblProjectRcv.Text = reader["DDRcv"].ToString(); lblProjectSub.Text = reader["DDSub"].ToString();
            lblProformaARcv.Text = reader["EmpName"].ToString(); lblProformaASub.Text = reader["EmpCode"].ToString();
            lblProformaCRcv.Text = reader["ProformaARcv"].ToString(); lblProformaCSub.Text = reader["ProformaASub"].ToString();
            lblProformaBRcv.Text = reader["ProformaBRcv"].ToString(); lblProformaBSub.Text = reader["ProformaBSub"].ToString();
        }
        reader.Close(); reader.Dispose();
    }
    private void feemaster(string feetype, string feelvl, string type)
    {
        string strStream;
        if (feetype== "PartII")
            strStream = "Tech";
        else strStream = "Asso";
        cmd = new SqlCommand("select ProApproval,ProEvaluation from FeeMaster where FeeLevel='"+feelvl+"' and Type='"+type+"' and FeeType='"+strStream+"'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblProjectApproval.Text = reader["ProApproval"].ToString();
            lblProjectEvaluation.Text = reader["ProEvaluation"].ToString();
        }
        reader.Close();
    }
    private bool chkDuplicate()
    {
        bool flg = false;
        btnSubmit.Enabled = true;
        if (lblProformaBFees.Text == "0")
        {
            flg = true;
            ddlProforma.SelectedValue = "ProformaB";
        }
        else if (lblProformaCFees.Text == "0")
        {
            flg = true;
            ddlProforma.SelectedValue = "ProformaC";
        }
        else btnSubmit.Enabled = false;
        return flg;
    }
    private string esngn()
    {
        int sn;
        cmd = new SqlCommand("select Max(AppNo) from AppRecord", con);
       string asn = cmd.ExecuteScalar().ToString();
        if (asn == "")
            sn = 1;
        else
        {
            sn = Convert.ToInt32(asn);
            sn = sn + 1;
        }
        lblserialNo.Text = sn.ToString();
        return sn.ToString();
    }
    private string serialno(string type)
    {
        cmd = new SqlCommand("select SerialNo from FeeList where FeeName='" + type.ToString() + "' and Status='NO' and Session='" + lblSession.Text.ToString() + "'", con);
        string rtnsn = Convert.ToString(cmd.ExecuteScalar());
        if (rtnsn == "")
        {
            cmd = new SqlCommand("insert into FeeList (FeeName,Status,Session) values(@FeeName,@Status,@Session)", con);
            cmd.Parameters.AddWithValue("@FeeName", type.ToString());
            cmd.Parameters.AddWithValue("@Status", "NO");
            cmd.Parameters.AddWithValue("@Session", lblSession.Text.ToString());
            cmd.ExecuteNonQuery();
            rtnsn = "0";
        }
        string fw = type.Substring(0, 1);
        int intsn = Convert.ToInt32(rtnsn) + 1;
        rtnsn = intsn.ToString();
        return fw.ToString() + "" + rtnsn.ToString();

    }  //Generete Serial No of froms.

    private void updateserialno(string type, string session)
    {
        try
        {
            cmd = new SqlCommand("update Feelist set SerialNo=SerialNo+1 where FeeName='" + type.ToString() + "' and Session='" + session.ToString() + "' and Status='NO'", con);
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            lblException.Text += type.ToString() + " SerialNo. Not Updated.";
        }
    }
    #endregion
    protected void chkDupForm_CheckedChanged(object sender, EventArgs e)
    {
        pnlMain.Visible = true;
        if (chkDupForm.Checked == true)
            lblDupForm.Text = "NO";
        else
            lblDupForm.Text = "YES";
    }
    protected void ddlProforma_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        if (ddlProforma.SelectedValue.ToString() == "ProformaB")
        {
            lblSubmitFees.Text = lblProjectApproval.Text.ToString();
            lblProjSerialNo.Text = serialno("BProforma");
        }
        else if (ddlProforma.SelectedValue.ToString() == "ProformaC")
        {
            lblProjSerialNo.Text = serialno("CProforma");
            lblSubmitFees.Text = lblProjectEvaluation.Text.ToString();
        }
        con.Close(); con.Dispose();
    }
}