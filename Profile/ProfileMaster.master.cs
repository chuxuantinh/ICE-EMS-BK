using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Profile_ProfileMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                if (!IsPostBack)
                {
                    SqlDataReader reader;
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                        int lvl = Convert.ToInt32(reader[20].ToString());
                        if (lvl == 0)
                        {
                            lblWelcome.Text = "Administrator";
                            if (reader["Admin"].ToString() == "SuperAdmin")
                                tdDebitNote.Visible = true;
                            else tdDebitNote.Visible = false;
                        }
                        else if (lvl == 1)
                        {
                            lblWelcome.Text = "Admin"; tdDebitNote.Visible = false;
                        }
                        else if (lvl == 2)
                        {
                            lblWelcome.Text = "User ID"; tdDebitNote.Visible = false;
                        }
                    }
                    reader.Close(); reader.Dispose(); con.Close(); con.Dispose();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnLetters_Click(object sender, EventArgs e)
    {
        Response.Redirect("Letters.aspx");
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
    protected void imgbtnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/ReportDefault.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=rpt&lvl=zero&typ=In");
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void imgbtnProfile_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Letters.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void imgbtnDebitNote_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DebitNoteReq.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void imgbtnStatus_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Admin/ViewStatus.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["dev"]);
    }
    protected void lbtnDebitNote_Click(object sender, EventArgs e)
    {
        Response.Redirect("DebitNoteReq.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void lbtnDebitEport_Click(object sender, EventArgs e)
    {
     
    }
}
