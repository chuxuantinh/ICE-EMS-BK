using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Reports_ReportDefault : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            try
            {
                SqlDataReader reader;
                con.Open();
                lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
                SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int lvl = Convert.ToInt32(reader[20].ToString());
                    if (lvl == 0)
                        lblWelcome.Text = "Administrator";
                    else if (lvl == 1)
                        lblWelcome.Text = "Admin";
                    else if (lvl == 2)
                    {
                        lblWelcome.Text = "User ID";
                        panelHeader.Visible = false;
                        if (Request.QueryString["typ"] == "Report")
                        {
                           
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Response.Redirect("../Login.aspx");
            }
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal dev = new maikal();
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void lbtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal dev = new maikal();
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    }
    protected void lbtnMembership_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/IM/IMProfileRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
   
    protected void lbtnD2DRpt_Click(object sender,EventArgs e)
    {
        Response.Redirect("../Reports/FO/D2DRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnViewPerort_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/DiaryRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnDiaryTypeRpt_click(object sender,EventArgs e)
    {
        Response.Redirect("../Reports/FO/DiaryTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbttCourierRpt_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/CourierRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnD2DReport_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/D2DRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCounsellingReport_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/CounsellingRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnVisitorsReport_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/VisitorsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnDiaryLetterRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/DiaryLetterRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCourierServiceRpt_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/CourierServiceRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnDiaryStatusRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/FO/DiaryStatusRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=IM");
    }
    protected void lbtnMemberType_Click(object sender,EventArgs e)
    {
        Response.Redirect("../Reports/FO/MemberTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnForm_Click(object sender, EventArgs e)
    {

        Response.Redirect("../Reports/FO/FormOnHold.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");

    }




    //students
    protected void lbtnGenMembership_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Student/StudentDetailsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnCourse_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Student/CourseRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnStudentProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Student/Student.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lkbtnStudentAccount_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Student/StudentaccountRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnDDDateAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/DDDateRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnDDAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/MainACRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnApplicationStatusSum_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/ApplicationStatusSum.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnApplicationStatusCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/ApplicationStatusCourseRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }




    protected void lbtnFeeStatusExam_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/FeeStatus.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamForm_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExamFormRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnAdmitCardExam_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/AdmitCardCrt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnAttendanceSheet_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/AttendanceSheetRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnMarksStatementsExam_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/MarksStatementsCrt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamAdmissionRpt_OnClick(object sendder, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExamApps.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamSNRpt_OnClick(object sender, EventArgs ee)
    {
        Response.Redirect("../Reports/Exam/ExamSN.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    
   
    protected void lbtnToBeExamFormFill_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ToBeFillExamFormRpt.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }

    protected void lbtnExamSNApp_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExamSNApp.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExamDate_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExamFormsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void llbtnExamFormSubmitted_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExemptionFormSubmitted.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnExemSubject_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExemptionSubRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }
    protected void lbtnFormType_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/FormTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }


    //MemberShip

    protected void lbtnIM_OnClick(object sender, EventArgs e)
    {
      
        Response.Redirect("../Reports/IM/IMProfileRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=IM");
    }
    protected void lbtnSubscription_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("../Reports/IM/SubscriptionRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=IM");
    }


   
  
    
  

    protected void lbtnExamForms_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExamFormRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnExportResult_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Exam/ExportResult.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=Exam");
    }

    //Account

    protected void lbtnAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/AccountRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnMSAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/MemberFees.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnAppApproveAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/ACAppApproveRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnConsolidateRptAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/ConsolidatedAmtRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnAllExportAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/AllExport.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnExamBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/ExamBillRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnApplicationStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/ApplicationStatusReportRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }




    protected void lbtnSupplier_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Invent/SupplierRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");
    }
    protected void lbtnIMBooks_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Invent/IMBooksRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");
    }
    protected void lbtnSumMaster_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Invent/SubMasterRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");
    }
    protected void lbtnPurches_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Invent/PurchesRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");
    }
    protected void lbtnIMOrder_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Invent/IMOrder.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");
    }
    protected void lbtnIMOrderList_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Invent/ImorderList.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");

    }
    protected void lbtnIMStock_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Invent/IMStockRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");

    }



    protected void lbtnITIForms_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/ITIFormrpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnITILetters_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/ITILetters.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");

    }
    protected void lbtnITIExam_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/ITIExamrpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");

    }
    protected void lbtnITIResult_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/ITIResult.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnMembershipGtd_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/StuMembershipGtd.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnStudentRemark_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/StudentRemarks.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnStuExp_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/StuExpRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnReAdmission_click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/student/ReAdmissionRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }




    

    protected void lbtnProject_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Project/ProjectRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnProjectStatus_OnClick(object sender, EventArgs e)
    {

        Response.Redirect("../Reports/Project/ProjectStatusRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }

    protected void lbtnInstituteRe_OnClick(object sender, EventArgs e)
    {

        Response.Redirect("../Reports/Project/InstituteReRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnProjectDe_OnClick(object sender, EventArgs e)
    {

        Response.Redirect("../Reports/Project/ProjectDetailsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnIMLetter_OnClick(object sender, EventArgs e)
    {

        Response.Redirect("../Reports/Project/IMLetterRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }

    protected void lbtnAicteLetter_Click(object sender, EventArgs e)
    {

        Response.Redirect("../Reports/Project/AicteProjectRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnStuApproved_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Project/StudentApprovedLetter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnStuAppRemarks_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Project/StuAppRemarksLetter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
  
    protected void lbtnStuRejectedLetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Project/StudentRejectedLetter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnProjectApprovedAc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Project/ProjectApprovedRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnSynopsisApproveRpt_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/Project/SynopsisApprovalRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
}