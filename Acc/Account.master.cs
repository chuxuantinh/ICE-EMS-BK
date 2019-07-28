using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using System.Linq;

public partial class Acc_Account : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                lbtnUserName.Text = Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString();
                lblWelcome.Text = "Welcome";
                SqlDataReader reader;
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                  int  lvl = Convert.ToInt32(reader[20].ToString());
                    if (lvl == 0)
                    {
                        lblWelcome.Text = "Administrator";
                    }
                    else if (lvl == 1)
                    {
                        lblWelcome.Text = "Admin";
                    }
                    else if (lvl == 2)
                    {
                        lbtnAppAproval.Visible = false;
                        lbtnLateFeeAC.Visible = false; lbtnAppEdit.Visible = false; lbtnAppAproval.Visible = false; lbtnPendingApprove.Visible = false;
                        ibtnSubmitMemForm.Visible = false; lbtnManageDues.Visible = false; lbtnEditMainAC.Visible = false; lbtnLateFeeAC.Visible = false;
                        lblWelcome.Text = "User ID";
                        usermanage.Visible = false;
                        panelProfile.Visible = false;
                        panelAdminManage.Visible = false;
                        panelAdmission.Visible = false;
                        PnlMembership.Visible = false;
                        if (reader["ExamAdmin2"].ToString() == "ExamBill")
                        {
                            panelProfile.Visible = true;
                        }
                        if (reader["ACExam"].ToString() == "AddApps")
                        {
                            panelApproval.Visible = true;
                            lbtnAppEdit.Visible = true; PnlMembership.Visible = false;
                        }
                        if (reader["Accounts"].ToString() == "AppApprove")
                        {
                            panelApproval.Visible = true; lbtnAppAproval.Visible = true; lbtnPendingApprove.Visible = true;
                            PnlMembership.Visible = false;
                        }
                        if (reader["MFee"].ToString() == "LateFee")
                        {
                            lbtnLateFeeAC.Visible = true;
                            panelAdminManage.Visible = true;
                        }
                        if (reader["ExamFee"].ToString() == "MainAC")
                        {
                            panelAdminManage.Visible = true; panelProfile.Visible = true; PnlMembership.Visible = true;
                            ibtnSubmitMemForm.Visible = true; lbtnManageDues.Visible = true; lbtnEditMainAC.Visible = true;
                        }
                    }
                }
                reader.Close();
                reader.Dispose();
                con.Close();
               con.Dispose();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login.aspx");
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void ibtnUpdateApppRecord_Click(object sender, EventArgs e)
    {
    }
    protected void ibtnUpdateExamBilling_Click(object sender, EventArgs e)
    {
    }
    protected void ibtnSubmitAmount_Click(object sender, EventArgs e)
    {
        Response.Redirect("Aount.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnSubmitMembership_click(object sender, EventArgs e)
    {
        Response.Redirect("MembershipAC.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnInfectionAmt_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("InfectionAmount.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnEditAC_Onclick(object sendre, EventArgs e)
    {
        Response.Redirect("EditMainAC.aspx?acid=&DNo=");
    }
    protected void ibtnLateFeeAC_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("LateFeeAC.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnInventoryReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventoryReport.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnExaminationBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamBilling.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnViewExamBill_click(object sender, EventArgs e)
    {
        Response.Redirect("ExamBillView.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAppAproval_Click(object sender, EventArgs e)
    {
        Response.Redirect("AppApprove.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnDiaryOnHold_Click(object sender, EventArgs e)
    {
        Response.Redirect("DiaryHold.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnViewAppForms_click(object sender, EventArgs e)
    {
        Response.Redirect("AppApproveView.aspx?id=");
    }
    protected void lbtnAddApp1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddApps1.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAddApp1_2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddApplication.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAddApp2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddApps2.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAddAppPro_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddAppsPro.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAddAutoCAD_Click(object sender, EventArgs e)
    {
        Response.Redirect("AutoCAD.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnViewAC_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ViewAC.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnManage_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ACManage.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnPendingApprove_Click(object sender, EventArgs e)
    {
        Response.Redirect("PendingFormApprove.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAppEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("AppEdit.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnUser_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=Ac&maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=Ac&?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnMainAC_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Aount.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnMainIMAcc_Click(object sender, EventArgs e)
    {
        Response.Redirect("Aount.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnLateFees_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("LateFeeAC.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnLateFee_Click(object sender, EventArgs e)
    {
        Response.Redirect("LateFeeAC.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnMemberFee_Click(object sender, EventArgs e)
    {
        Response.Redirect("ACManage.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnExamBill_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ExamBilling.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnExamBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamBilling.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnMembershipAcc_Click(object sender, EventArgs e)
    {
        Response.Redirect("MembershipAC.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnmembershipAC_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("MembershipAC.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnAddAppForm_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AddApp1.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAddApps_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddApp1.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnAddApp3_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddApp3.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnAppApprove_Clickz(object sender, EventArgs e)
    {
        Response.Redirect("AppApprove.aspx?id=&maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnApproveApps_Click(object sender, EventArgs e)
    {
        Response.Redirect("AppApprove.aspx?id=&maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/AC/Default.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=rpt&lvl=zero&typ=AC");
    }
    protected void lbtnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/AC/Default.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=rpt&lvl=zero&typ=AC");
    }
    protected void lbtnAccountDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewAccount.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnViewMember_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewMember.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnUpdateAmount_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateAmount.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void ibtnHome_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            con.Open();
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
    }
    protected void lbtnUpdateLateFees_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateLateFees.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnUpdateExamFees_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateExamFees.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["maikal"]);
    }
    protected void lbtnDebitNote_Click(object sender, EventArgs e)
    {
        Response.Redirect("DebitNote.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnCancelAppForm2_Click(object sender, EventArgs e)
    {
        Response.Redirect("CancelAcademicForm2.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnCancelAppForm3_Click(object sender, EventArgs e)
    {
        Response.Redirect("CancelAcademicForm3.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnCancelProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("CancelProjectForm.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnRecheckingForm_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveRechecking.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnCancelRechecking_Click(object sender, EventArgs e)
    {
        Response.Redirect("CancelReChecking.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnEditExamCurrent_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamCurrent.aspx?maikal=" + Request.QueryString["maikal"]);
    }
    protected void lbtnManageCompositeFees_Click(object sender, EventArgs e)
    {
        Response.Redirect("compositeFees.aspx?maikal=" + Request.QueryString["maikal"]);
    }
}