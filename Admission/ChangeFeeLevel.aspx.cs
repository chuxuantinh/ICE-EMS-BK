using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admission_Chenge_FeeLevel : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
               
            }
            
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
    protected void btnOk_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select FeeLevel from Student where SID='" + txtMembership.Text + "'", con);
        string feelevel = Convert.ToString(cmd.ExecuteScalar());
        if (feelevel != "")
        {
            lblEnrolment.Text = txtMembership.Text;

            lblFee.Text = feelevel; ddlFeeLevel.SelectedValue = feelevel;
        }
        else lblFee.Text = "Incorrect Membership";
        con.Close();
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (lblEnrolment.Text != "")
        {
            con.Open();
            cmd = new SqlCommand("update Student set FeeLevel='" + ddlFeeLevel.SelectedValue + "' where SId='" + lblEnrolment.Text + "'", con);
            cmd.ExecuteNonQuery(); txtMembership.Text = ""; lblEnrolment.Text = ""; lblFee.Text = "updated";
            con.Close();
        }
    }
}