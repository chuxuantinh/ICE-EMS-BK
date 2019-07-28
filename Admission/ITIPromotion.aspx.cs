using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;


public partial class Admission_ITIPromotion : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataAdapter adp;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    pnlInfo.Visible = false;
                    txtMembership.Focus();                    
                }
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
            maikal mk = new maikal();
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (i == 0 | i == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtMembership.Focus();
    }
    protected void txtMembership_TextChanged(object sender, EventArgs e)
    {
        pnlInfo.Visible = true;
        con.Open();
        cmd = new SqlCommand("select * from Student where SID='" + txtMembership.Text + "'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblName.Text = rd["Name"].ToString();
            lblFatherName.Text = rd["FName"].ToString();
            rd.Close();
            cmd = new SqlCommand("select * from ExamCurrent where SId='" + txtMembership.Text + "'", con);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                lblCourse.Text = rd["Course"].ToString();
                lblPart.Text = rd["Part"].ToString();
            }
            rd.Close();
            cmd = new SqlCommand("select Status from ITIForm where SID='" + txtMembership.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
            string status = Convert.ToString(cmd.ExecuteScalar());
            if (status == "") { btnPromote.Enabled = false; lblStatus.Text = "Not Found"; }
            else
            {
                lblStatus.Text = status; btnPromote.Enabled = true;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Membership Not Found')", true);
            pnlInfo.Visible = false;
        }
        rd.Close();
        con.Close();
        con.Dispose();
    }
    protected void btnPromote_Click(object sender, EventArgs e)
    {
        con.Open();
        if (lblPart.Text == "PartI")
        {
            cmd = new SqlCommand("update ExamCurrent set Part='"+ddlPart.Text+"' where SId='" + txtMembership.Text + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Updated')", true);
            txtMembership.Text = ""; lblPart.Text = ""; lblCourse.Text = ""; lblName.Text = ""; lblFatherName.Text = ""; pnlInfo.Visible = false;
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Current PartI Not Found.')", true);
        con.Close();
        con.Dispose();
    }
}