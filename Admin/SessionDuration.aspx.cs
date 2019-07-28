using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;

public partial class Admin_SessionDuration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd; SessionDuration sess = new SessionDuration(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Lblmsg.Visible = false;
            XmlDocument xdoc = new XmlDocument();
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
            }
            if (!IsPostBack)
            {
                durationn();
            }
            ddlCourse.Focus();
        }
        catch (NullReferenceException ex)
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
            maikal mk = new maikal();
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (i == 0 | i == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2)
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        durationn();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        durationn();
    }
    public void durationn()
    {
        string r, s;
        r = ddlCourse.SelectedItem.Text;
        s = ddlPart.SelectedItem.Text;
        Lblsubjectname.Text = sess.duration(r, s);
    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        string r, s;
     
            r = ddlCourse.SelectedItem.Text;
            s = ddlPart.SelectedItem.Text;
          //  Lblsubjectname.Text = sess.updateduration(r, s, Txtupdate.Text);
            Lblmsg.Text = "Course Duration Updated";
        Lblmsg.Visible = true;

    }
}