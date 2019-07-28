using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Admission_AdmissionDefault : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtEnrolment.Focus();
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
        }
        catch (NullReferenceException EX)
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {

        con.Dispose();

    }

    protected void lblHomeRedirect_Click(object sender, EventArgs e)
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        Session.Remove("sid");
        Session["sid"] = txtEnrolment.Text.ToString();
        Response.Redirect("AdmissionDepart.aspx?name="+Request.QueryString["name"]+"&lnk=null&typ=Ad");
    }
}