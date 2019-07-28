using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Administrator_Administrator : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
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
                try
                {
                    SqlDataReader reader;
                    con.Open();
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
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
                            lblWelcome.Text = "User ID";
                        }
                    }
                    reader.Close();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
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
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
            { 
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                     Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                 Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
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
    /*Create User*/
    protected void btnManageAdmin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    /*Membership Home*/
    protected void ibtnHome_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Membership.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*Manage Groups*/
    protected void ibtnManagegroups_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ManageGroups.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*Letters*/
    protected void ibtnLetters_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Profile/Letters.aspx?dev=" + Request.QueryString["dev"] + "&lvl=zero&typ=" + Request.QueryString["ain"]);
    }
    /*Reports*/
    protected void ibtnReports_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/IM/Default.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Mslvl=" + Request.QueryString["lvl"] + "&id=");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=" + Request.QueryString["lvl"] + "&typ=Admin&name=" + Request.QueryString["dev"]);
    }
    protected void ibtnDocuments_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("IMDocuments.aspx");
    }
}
