using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class SuperAdministrator : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    private static int lvl;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                showimg();
                leftpanel();
                try
                {
                    if (!IsPostBack)
                    {
                        SqlDataReader reader;
                        con.Open();
                        lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
                        SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                            lvl = Convert.ToInt32(reader[20].ToString());
                            if (lvl == 0)
                            {
                                lblWelcome.Text = "Administrator";
                            }
                            else if (lvl == 1)
                            {
                                lblWelcome.Text = "Admin";
                                panelFeeMaster.Visible = false;
                                usermanage.Visible = true;
                            }
                            else if (lvl == 2)
                            {
                                lblWelcome.Text = "User ID";
                                usermanage.Visible = false;
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        con.Close();
                    }
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
                }
            }
        }
        catch(NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    public void leftpanel()
    {
        panelAdmission.Visible = false;
        panelMembership.Visible = false;
        panelInventory.Visible = false;
        panelExaminaiton.Visible = false;
        panelFeeMaster.Visible = false;
        panelFrontOffice.Visible = false;
        panelProject.Visible = false;
        panelAdmin.Visible = false;
        panelAccount.Visible = false;
        if (Request.QueryString["typ"] == "Admin")
            panelAdmin.Visible = true;
        else if (Request.QueryString["typ"] == "FO")
            panelFrontOffice.Visible = true;
        else if (Request.QueryString["typ"] == "Ac")
            panelAccount.Visible = true;
        else if (Request.QueryString["typ"] == "Ex")
            panelExaminaiton.Visible = true;
        else if (Request.QueryString["typ"] == "In")
            panelInventory.Visible = true;
        else if (Request.QueryString["typ"] == "Ms")
            panelMembership.Visible = true;
        else if (Request.QueryString["typ"] == "Ad")
            panelAdmission.Visible = true;
        else if (Request.QueryString["typ"] == "Pro")
            panelProject.Visible = true;
        else if (Request.QueryString["typ"] == "Fee")
            panelFeeMaster.Visible = true;
        else
        {
            panelAdmission.Visible = false;
            panelMembership.Visible = false;
            panelInventory.Visible = false;
            panelExaminaiton.Visible = false;
            panelFeeMaster.Visible = false;
            panelFrontOffice.Visible = false;
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
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());

            if (lvl == 0 && Request.QueryString["lvl"] != "one")
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null" | Request.QueryString["lvl"].ToString() == "one"))
            {
                Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());

            if (lvl == 0 && Request.QueryString["lvl"] != "one")
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null" | Request.QueryString["lvl"].ToString() == "one"))
            {
                Response.Redirect("../User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());

            if (lvl == 0 && Request.QueryString["lvl"]!="one")
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=update&lvl=zero&typ=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null" | Request.QueryString["lvl"].ToString() == "one"))
            {
                Response.Redirect("../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    public void showimg()
    {
        if (Request.QueryString["lnk"].ToString() == "create")
        {
            imgbtnCreate.ImageUrl = "~/images/createcolor.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "update")
        {
            imgbtnRecover.ImageUrl = "~/images/user_update.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "delete")
        {
            imgbtnDelete.ImageUrl = "~/images/user_delete.png";
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void lbtnMS_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/ViewAppProfiles.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=zero");
    }
    protected void lbtnFO_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/SuperAdminManage.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&lvl=one&typ=FO");
    }
    protected void lbtnAD_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/AdmissionDefault.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnAC_Click(object sender, EventArgs e)
    {
        Response.Redirect("../AccountDepart.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ac");
    }
    protected void lbtnEX_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Exam/ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void lbtnIN_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Invent/Default.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");        
    }
    protected void lbtnPO_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Project/projectss.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro");
    }
    protected void lnkDiaryEntry_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/CourierHome.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lnkDiarySupplyDept_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/CourierSuply.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lnkNewVistr_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/frontOffceHome.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lnkCoDisptch_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/DiaryEntry.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lnkCounselling_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/Counseling.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lnlMainImAc_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // main account.
        Response.Redirect("../Acc/Aount.aspx");
    }
    protected void lnkExamBilling_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("../Acc/ExamBilling.aspx");
    }
    protected void lnlMemAc_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("../Acc/MembershipAC.aspx");
    }
    protected void lnkAddApp_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("../Acc/ApproveApps.aspx");
    }
    protected void lnkApprApp_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("../Acc/AppApprove.aspx?id=");
    }
    protected void lnkExamHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Exam/ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void lnkExamRpt_Click(object sender, EventArgs e)
    {
    }
    protected void lnkStockMgmt_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Invent/PlaceOrder.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lnkSupplMgmt_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Invent/IMOrderEntry.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lnkNwMembsp_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/Register.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&" + Request.QueryString["lvl"] + "=zero");
    }
    protected void lnkImInspcn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/IMInspection.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    protected void lnkSubscrpn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/Subscription.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    protected void lnkViewProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/ViewAppProfiles.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Mslvl=" + Request.QueryString["lvl"] + "&id=");
    }
    protected void lnkNwAdmsn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/Admission.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lnkApprAdmsn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ApproveAdmission.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lnkStuProf_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/AdmissionDepart.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lnkSearchStu_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/SearchStudent.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lnkAicteInstProf_Click(object sender, EventArgs e)
    {
        Response.Redirect("../project/InstitutionRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnkPerformaA_Click(object sender, EventArgs e)
    {
        Response.Redirect("../project/SubmitSynopsis.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnkPerformaB_Click(object sender, EventArgs e)
    {
        Response.Redirect("../project/Allocateproject.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnkPerformaC_Click(object sender, EventArgs e)
    {
        Response.Redirect("../project/projectapprove.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnlMFee_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/MemberFeeMaster.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Fee&&lvl=zero&mst=member&sec=eef");
    }
    protected void lnkAFee_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select FeeLevel from FeeMaster where Status= 'Active' and FeeType='Asso'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("../Administrator/Fees/AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&lvl=" + id + "&sec=eef");
    }
    protected void lnlTFee_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select FeeLevel from FeeMaster where Status= 'Active' and FeeType='Tech'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("../Administrator/Fees/TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&lvl=" + id + "&sec=eef");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admin/changePassword.aspx?lnk=update&lvl=" + Request.QueryString["lvl"] + "&typ=Admin&name=" + Request.QueryString["name"]);
    }
    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
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
            Response.Redirect("Login.aspx");
        }
    }
}
