using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_SubjectPrices : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
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
            }
            if (!IsPostBack)
            {
                ddlbind();
                Subject();
                lblexception.Text = "";
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
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update SubjectMaster set Price='" + txtPrice.Text + "' where SubjectCode='" + ddlSubCode.SelectedValue + "' and Course='" + ddlcourse.SelectedValue + "' and Part='" + ddlPart.SelectedValue + "' and CourseID='" + ddlCourseLevel.SelectedValue + "' ", con);
        cmd.ExecuteNonQuery();
        lblexception.Text = "Updated";
        con.Close();
        con.Dispose();
        ddlcourse.Focus();
    }
    private void Subject()
    {
        lblexception.Text = "";
        SqlDataAdapter ad3 = new SqlDataAdapter("select DISTINCT SubjectCode from SubjectMaster where Course='" + ddlcourse.SelectedValue + "' and Part='" + ddlPart.SelectedValue + "' and CourseID='" + ddlCourseLevel.SelectedValue + "'", con);
        DataTable dt3 = new DataTable();
        ad3.Fill(dt3);
        ddlSubCode.DataSource = dt3;
        ddlSubCode.DataTextField = "SubjectCode";
        ddlSubCode.DataValueField = "SubjectCode";
        ddlSubCode.DataBind();
        con.Open();
        cmd = new SqlCommand("select * from SubjectMaster where SubjectCode='" + ddlSubCode.SelectedValue + "' and Course='" + ddlcourse.SelectedValue + "' and Part='" + ddlPart.SelectedValue + "' and CourseID='" + ddlCourseLevel.SelectedValue + "'", con);
        SqlDataReader read = cmd.ExecuteReader();
        if (read.Read())
        {
            lblSubjectName.Text = read["SubjectName"].ToString();
            lblSubName.Visible = true;
            txtPrice.Text = read["Price"].ToString().TrimEnd('0').TrimEnd('.');
        }
        else
        {
            lblexception.Text = "data not found";
            lblSubjectName.Text = "";
            txtPrice.Text = "";
        }
        read.Close(); con.Close();
        con.Dispose();
    }

    protected void ddlcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Subject();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        Subject();
    }
    protected void ddlCourseLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Subject();
    }
    protected void ddlSubCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select * from SubjectMaster where SubjectCode='" + ddlSubCode.SelectedValue + "' and Course='" + ddlcourse.SelectedValue + "' and Part='" + ddlPart.SelectedValue + "' and CourseID='" + ddlCourseLevel.SelectedValue + "'", con);
        SqlDataReader read = cmd.ExecuteReader();
        if (read.Read())
        {
            lblSubjectName.Text = read["SubjectName"].ToString();
            lblSubName.Visible = true;
            txtPrice.Text = read["Price"].ToString().TrimEnd('0').TrimEnd('.');
            lblexception.Text = "";
        }
        else
        {
            lblexception.Text = "data not found";
            lblSubjectName.Text = "";
            txtPrice.Text = "";
        }
        read.Close();
        con.Close();
        con.Dispose();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPrice.Text = "";
        lblSubjectName.Text = "";
        txtPrice.Focus();
    }
    private void ddlbind()
    {
        SqlDataAdapter ad3 = new SqlDataAdapter("select DISTINCT CourseID from CivilSubMaster ", con);
        DataTable dt3 = new DataTable();
        ad3.Fill(dt3);
        ddlCourseLevel.DataSource = dt3;
        ddlCourseLevel.DataTextField = "CourseID";
        ddlCourseLevel.DataValueField = "CourseID";
        ddlCourseLevel.DataBind();
    }
}