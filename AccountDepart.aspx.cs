using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AccountDepart : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    ClsEdit clsts = new ClsEdit();
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
                if (!IsPostBack)
                {
                    dispaly();
                    lbtnUserName.Text = Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString();
                    lblWelcome.Text = "Welcome";
                    SqlDataReader reader;
                    con.Close(); con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                       int lvl = Convert.ToInt32(reader[20].ToString());
                        if (lvl == 0)
                        {
                            lblWelcome.Text = "Administrator";
                        }
                        else if (lvl == 1)
                        {
                            lblWelcome.Text = "Admin";
                            // usermanage.Visible = false;
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
                    con.Dispose();
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
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
      
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void imgbtnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Reports/AC/Default.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=rpt&lvl=zero&typ=AC");
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        maikal mk = new maikal();
        int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
        if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            Response.Redirect("Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
        else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            Response.Redirect("User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
        protected void ibtnMemberFee_Click(object sender, EventArgs e)
          {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // main account.
        Response.Redirect("Acc/ACManage.aspx?maikal=" + Request.QueryString["name"]);
          }
    protected void ibtnmembershipAC_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/MembershipAC.aspx?maikal=" + Request.QueryString["name"]);
    }
    
    protected void ibtnAppApprove_Clickz(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/AppApprove.aspx?id=&maikal=" + Request.QueryString["name"]);
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
        maikal mk = new maikal();
        int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
        if (lvl == 0)
            Response.Redirect("SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
        else if (lvl == 1)
            Response.Redirect("SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
        else if (lvl == 2)
            Response.Redirect("UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
          }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnAddAppForm_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/AddApps1.aspx?maikal=" + Request.QueryString["name"]);
    }
    protected void ibtnExaminationFee_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // main account.
        Response.Redirect("Acc/Aount.aspx?maikal="+Request.QueryString["name"]);
    }
    protected void ibtnMemberFee_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // main account.
        Response.Redirect("Acc/LateFeeAC.aspx?maikal=" + Request.QueryString["name"]);
    }
    //LinkButtons
    protected void lbtnMainIMAcc_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // main account.
        Response.Redirect("Acc/Aount.aspx?maikal=" + Request.QueryString["name"]);
    }
    protected void lbtnLateFee_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // main account.
        Response.Redirect("Acc/LateFeeAC.aspx?maikal=" + Request.QueryString["name"]);
    }
    protected void lbtnExamBill_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/ExamBilling.aspx?maikal=" + Request.QueryString["name"]);
    }
    protected void lbtnMembershipAcc_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/MembershipAC.aspx?maikal=" + Request.QueryString["name"]);
    }
    protected void lbtnAddApps_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/ApproveApps.aspx?maikal=" + Request.QueryString["name"]);
    }
    protected void lbtnApproveApps_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/AppApprove.aspx?id=&maikal=" + Request.QueryString["name"]);
    }
    protected void btnExamBill_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/ExamBilling.aspx?maikal=" + Request.QueryString["name"]);
    }
    protected void ibtnMembershipAC_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/MembershipAC.aspx?maikal="+Request.QueryString["name"]);
    }
    protected void ibtnAppApprove_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();  // admission form
        Response.Redirect("Acc/AppApprove.aspx?id=&maikal=" + Request.QueryString["name"]);
    }

    private void dispaly()
    {
        string mon;
        maikal dev = new maikal();
        int se = dev.chksession();
        if (se == 1)
             mon = "Win";
        else
             mon = "Sum";
        lblApproval.Text = clsts.AcApproval(mon+ DateTime.Now.Year.ToString());
        lblHold.Text = clsts.AcHold(mon + DateTime.Now.Year.ToString());
        lblDDEntry.Text = clsts.AcDDEntry();
        lblDebitNote.Text = clsts.AcDebitNote();
        lblReApproval.Text = clsts.AcReApproval();
    }
}