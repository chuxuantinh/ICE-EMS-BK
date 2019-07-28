using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_SystemName : System.Web.UI.Page
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
                lblName.Text = "Select" + " " + Request.QueryString["page"] + " " + "Name";
                lblName2.Text = Request.QueryString["page"] + " " + "Name";
                if (ddlType.SelectedItem.Selected == true)
                {
                    txtDept.Text = ddlType.SelectedValue.ToString();
                }
                lblException.Text = "";
                lblHomelink.Text = Request.QueryString["page"];
                ddlType.Focus();
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
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblException.Text = "";
        txtDept.Text = ddlType.SelectedValue.ToString();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select * from ServiceNameMaster where Type='" + Request.QueryString["page"] + "' and Name='" + txtDept.Text + "'", con);
        SqlDataReader read = cmd.ExecuteReader();
        if (read.Read())
        {
            lblException.Text = "Already Exists";
            txtDept.Text = "";
        }
        else
        {
            read.Close();
            cmd = new SqlCommand("insert into ServiceNameMaster(Name,Type) values(@Name,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtDept.Text);
            cmd.Parameters.AddWithValue("@Type", Request.QueryString["page"]);
            cmd.ExecuteNonQuery();
            lblException.Text = "Successfully Added";
            txtDept.Text = "";
            ddlbind();
            txtDept.Text = ddlType.SelectedValue.ToString();
        }
        con.Close();
        con.Dispose();
        ddlType.Focus();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update ServiceNameMaster set Name='" + txtDept.Text + "' where Name='" + ddlType.SelectedValue + "' and Type='" + Request.QueryString["page"] + "' ", con);
        cmd.ExecuteNonQuery();
        con.Close();
        txtDept.Text = "";
        lblException.Text = "Updated";
        ddlbind();
        txtDept.Text = ddlType.SelectedValue.ToString();
        ddlType.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("delete from ServiceNameMaster where Name='" + txtDept.Text + "' and Type='" + Request.QueryString["page"] + "' ", con);
        cmd.ExecuteNonQuery();
        con.Close();
        txtDept.Text = "";
        lblException.Text = "Deleted";
        ddlbind();
        txtDept.Text = ddlType.SelectedValue.ToString();
        ddlType.Focus();
    }
    private void ddlbind()
    {
        SqlDataAdapter ad3 = new SqlDataAdapter("select DISTINCT Name from ServiceNameMaster where Type='" + Request.QueryString["page"] + "'", con);
        DataTable dt3 = new DataTable();
        ad3.Fill(dt3);
        ddlType.DataSource = dt3;
        ddlType.DataTextField = "Name";
        ddlType.DataValueField = "Name";
        ddlType.DataBind();
    }
}