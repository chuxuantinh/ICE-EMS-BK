using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class UserHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    private DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                try
                {
                    if (!IsPostBack)
                    {
                        lblWelcome.Text = "User";
                        lbtnUserName.Text = Request.QueryString["dev"];
                        con.Close();
                        con.Open();
                        SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString() + "' and Password='" + Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString() + "'", con);
                        SqlDataReader reader;
                        reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dr = dt.Rows[0];
                        reader.Close();
                        lbtnAdmission.Visible = false;
                        ibtnFrontOffice.Visible = false; ibtnEnquiry.Visible = false; ibtnCourier.Visible = false; ibtnLateFees.Visible = false; ibtnMainAC.Visible = false;
                        ibtnMemberRegister.Visible = false; ibtnMemReNewal.Visible = false; ibtnAddApps.Visible = false; ibtnMembershipAC.Visible = false; ibtnExamBill.Visible = false;
                        ibtnExamSchedule.Visible = false; ibtnExamCEnter.Visible = false; ibtnAppApprove.Visible = false; ibtnD2D.Visible = false;
                        ibtnExamFrom.Visible = false; ibtnRollNO.Visible = false; ibtnAdmitCard.Visible = false; ibtnMarkFeed.Visible = false;
                        ibtnExamPaper.Visible = false; ibtnPaperSetter.Visible = false; ibtnSeating.Visible = false; ibtnMarkSheet.Visible = false;
                        ibtnCertificate.Visible = false; ibtnMarking.Visible = false; ibtnUFM.Visible = false; ibtnRecheking.Visible = false; lbtnAdmissionApprove.Visible = false;
                        ibtnStock.Visible = false; ibtnIMSupplier.Visible = false;
                        ibtnProSynopsis.Visible = false; ibtnProApprove.Visible = false; ibtnProSubmit.Visible = false; ibtnProEvaluate.Visible = false;
                        if (Request.QueryString["i94en67"] == "Ad")
                        {
                            panelAdmission.Visible = true;
                            if (dr["Admission"].ToString() == "Ad")
                                lbtnAdmission.Visible = true;
                            if (dr["AdmissionApprove"].ToString() == "AdmissionApprove")
                                lbtnAdmissionApprove.Visible = true;
                        }
                        else if (Request.QueryString["i94en67"] == "FO")
                        {
                            panelFrontOffice.Visible = true;
                            if (dr["FOffice"].ToString() == "FO") ibtnFrontOffice.Visible = true; // Visitors and counselling.
                            if (dr["Enquiry"].ToString() == "Enq") ibtnEnquiry.Visible = true;  // Diary Entry
                            if (dr["Courier"].ToString() == "Cou") ibtnCourier.Visible = true; // courier Dispetch
                            if (dr["D2D"].ToString() == "D2D") ibtnD2D.Visible = true; // Dairy To Department.
                        }
                        else if (Request.QueryString["i94en67"] == "Ac")
                        {
                            panelAccount.Visible = true;
                            if (dr["MFee"].ToString() == "LateFees") ibtnLateFees.Visible = true;   // Beneficiary Account.
                            if (dr["ExamFee"].ToString() == "MainAC") ibtnMainAC.Visible = true;   // main account
                            if (dr["ACMember"].ToString() == "ACMember") ibtnMembershipAC.Visible = true;
                            if (dr["ACExam"].ToString() == "AddApps") ibtnAddApps.Visible = true;   // Add Application Form
                            if (dr["ExamAdmin2"].ToString() == "ExamBill") ibtnExamBill.Visible = true;
                            if (dr["Accounts"].ToString() == "AppApprove") ibtnAppApprove.Visible = true; // Apps Approve
                        }
                        else if (Request.QueryString["i94en67"] == "Ex")
                        {
                            panelExamination.Visible = true;
                            if (dr["ExamAdmin1"].ToString() == "ExamSchedule") ibtnExamSchedule.Visible = true;
                            if (dr["ExamCenter"].ToString() == "ECenter") ibtnExamCEnter.Visible = true;
                            if (dr["ExamForm"].ToString() == "ExamForm") ibtnExamFrom.Visible = true;
                            if (dr["RollNO"].ToString() == "RollNO") ibtnRollNO.Visible = true;
                            if (dr["AdmitCard"].ToString() == "AdmitCard") ibtnAdmitCard.Visible = true;
                            if (dr["MarksFeed"].ToString() == "MarksFeed") ibtnMarkFeed.Visible = true;
                            if (dr["ExamPaper"].ToString() == "ExamPaper") ibtnExamPaper.Visible = true;
                            if (dr["PaperSetter"].ToString() == "PaperSetter") ibtnPaperSetter.Visible = true;
                            if (dr["Seating"].ToString() == "Seating") ibtnSeating.Visible = true;
                            if (dr["Marksheet"].ToString() == "Marksheet") ibtnMarkSheet.Visible = true;
                            if (dr["Certi"].ToString() == "Certi") ibtnCertificate.Visible = true;
                            if (dr["Marking"].ToString() == "Marking") ibtnMarking.Visible = true;
                            if (dr["UFM"].ToString() == "UFM") ibtnUFM.Visible = true;
                            if (dr["Rechecking"].ToString() == "Rechecking") ibtnRecheking.Visible = true;
                        }
                        else if (Request.QueryString["i94en67"] == "In")
                        {
                            panelInventory.Visible = true;
                            if (dr["InvenAdmin1"].ToString() == "Stock") ibtnStock.Visible = true;
                            if (dr["InvenAdmin2"].ToString() == "Suplier") ibtnIMSupplier.Visible = true;
                        }
                        else if (Request.QueryString["i94en67"] == "Ms")
                        {
                            panelMembership.Visible = true;
                            if (dr["MemberAdmin1"].ToString() == "MRegister") ibtnMemberRegister.Visible = true;
                            if (dr["MemberAdmin2"].ToString() == "MRenewal") ibtnMemReNewal.Visible = true;
                        }
                        else if (Request.QueryString["i94en67"] == "Pro")
                        {
                            panelProject.Visible = true;
                            if (dr["ProSynopsis"].ToString() == "Sys") ibtnProSynopsis.Visible = true;
                            if (dr["ProApprove"].ToString() == "ProApp") ibtnProApprove.Visible = true;
                            if (dr["ProSubmit"].ToString() == "ProSub") ibtnProSubmit.Visible = true;
                            if (dr["ProEvaluate"].ToString() == "ProEva") ibtnProEvaluate.Visible = true;
                        }
                        con.Close();
                        con.Dispose();
                    }
                }
                catch (SqlException ex)
                {
                }
                finally
                {
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void lbtnAdmissionAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admission/Admission.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnAdmissionApprove_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admission/ApproveAdmission.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ad");
    }
    protected void ibtnMemRenewal_Click(object sender, EventArgs e)
    {
        Response.Redirect("Administrator/IMInspection.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=two&id=");
    }
    protected void ibtnMemberRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect("Administrator/Register.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&&lvl=two");
    }
    protected void ibtnMainAC_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // Main AC 
        Response.Redirect("Acc/Aount.aspx");
    }
    protected void ibtnLateFees_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // Late Fees account.
        Response.Redirect("Acc/LateFeeAC.aspx");
    }
    protected void ibtnAppApprove_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // App Approve
        Response.Redirect("Acc/AppApprove.aspx?id=");
    }
    protected void ibtnAddApps_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();    // Add Apps
        Response.Redirect("Acc/ApproveApps.aspx");
    }
    protected void btnExamBill_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // Exam Billing form
        Response.Redirect("Acc/ExamBilling.aspx");
    }
    protected void ibtnExamSchedule_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Exam/ExamSchedule.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void ibtnExamCenter_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Exam/ExamCenter.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void ibtnExamForm_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Exam/ExamForm.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void ibtnRollNo_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Exam/GenerateRollNo.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void ibtnAdmitCard_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
    }
    protected void ibtnMarkFeed_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Exam/FeedMarks.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void ibtnExamPaper_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
    }
    protected void ibtnPaperSEtter_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Exam/ExamPaperSetter.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void ibtnSeating_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Exam/ExamSeating.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void ibtnMarksheet_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
    }
    protected void ibtnCertificate_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
    }
    protected void ibtnUFM_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
    }
    protected void ibtnMarking_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
    }
    protected void ibtnRechecking_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
    }
    protected void ibtnmembershipAC_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/MembershipAC.aspx");
    }
    protected void ibtnCourier_Click(object sender, EventArgs e) // Courier Dispetch
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("FO/DiaryEntry.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=FO");
    }
    protected void ibtnEnquiry_Click(object sender, EventArgs e) // Dairy Entry
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("FO/CourierHome.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=FO");
    }
    protected void ibtnFrontOffice_Click(object sender, EventArgs e) // Visitors and Counselling
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("FO/frontOffceHome.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=FO");
    }
    protected void ibtnD2D_Click(object sender, EventArgs e) // Dairy To Department
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("FO/CourierSuply.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=FO");
    }
    protected void ibtnStock_OnClick(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("Invent/Default.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=In");
    }
    protected void ibtnIMSupplier_OnClick(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("Invent/Default.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=In");
    }
    protected void ibtnProEvaluate_OnClick(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("Project/Projectss.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro");
    }
    protected void ibtnProSubmit_OnClick(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("Project/Projectss.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro");
    }
    protected void ibtnProApprove_Onclick(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("Project/Projectss.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro");
    }
    protected void ibtnProSynopsis_OnClick(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("Project/Projectss.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["name"]);
    }
}
