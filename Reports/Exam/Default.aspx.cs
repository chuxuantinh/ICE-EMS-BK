using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Reports_Exam_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            else
            {
                showimg();
                try
                {
                    con.Open();
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                        int lvl = Convert.ToInt32(reader[20].ToString());
                        if (lvl == 0)
                            lblWelcome.Text = "Administrator";
                        else if (lvl == 1)
                            lblWelcome.Text = "Admin";
                        else if (lvl == 2)
                        {
                            lblWelcome.Text = "User ID";
                            panelHeader.Visible = false;
                            if (Request.QueryString["typ"] == "IM")
                            {
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
        finally
        {
        }
    }
    public void showimg()
    {
        if (Request.QueryString["lnk"] == "create")
            imgbtnCreate.ImageUrl = "~/images/createcolor.png";
        else if (Request.QueryString["lnk"].ToString() == "update")
            imgbtnRecover.ImageUrl = "~/images/user_update.png";
        else if (Request.QueryString["lnk"].ToString() == "delete")
            imgbtnDelete.ImageUrl = "~/images/user_delete.png";
    }
    protected void lbtnFeeStatusExam_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("FeeStatus.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamForm_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExamFormRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnAdmitCardExam_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCardCrt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnAdmitCard_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCard.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnAdmitCardDuplicate_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCard.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnAttendanceSheet_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AttendanceSheetRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnAttendanceSheetRoom_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AttendancesheetRoomRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamCenterSummary_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SeatingArrangeSum.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnMarksStatementsExam_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MarksStatementsCrt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamAdmissionRpt_OnClick(object sendder, EventArgs e)
    {
        Response.Redirect("ExamApps.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamCenter_OnClick(object sendder, EventArgs e)
    {
        Response.Redirect("ExamCenterStudent.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamSNRpt_OnClick(object sender, EventArgs ee)
    {
        Response.Redirect("ExamSN.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnToBeExamFormFill_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ToBeFillExamFormRpt.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }

    protected void ibtnHOme_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../../Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }

    protected void lbtnExamSNApp_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExamSNApp.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamDate_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExamFormsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void llbtnExamFormSubmitted_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExemptionFormSubmitted.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExemSubject_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExemptionSubRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnFormType_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("FormTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExportResult_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExportResult.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnCenterDateStudentCount_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CenterDateStudentCount.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lblCenterPaperwise_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CenterPaperCodewise.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lblSubjectStudentCount_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("StuSubjectwise.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lblMatrixCenterReport_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CenterDateSessionwise.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lblPaperCode_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BookletRangeICE.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lblPaperCodeExamCenter_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BookletRangeExamCenter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    #region ReChecking
    protected void lbtnReCheckingFormSubmitted_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ReCheckingSubmitted.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnReCheckingResult_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ReCheckingResultCrt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    #endregion
    protected void PrintExam_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ExamCenterStudent.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
}