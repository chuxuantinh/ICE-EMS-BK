using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;

public partial class Exam_ManageECity : System.Web.UI.Page
{
   DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    ClsECenterCity ecity = new ClsECenterCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
                Response.Redirect("../Login.aspx");
            if (!IsPostBack)
            {
                ecity.getItems(ddlCity);
                txtCity.Text = ddlCity.SelectedItem.Text.ToString();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void ddlCity_SelectedInexChanged(object sender, EventArgs e)
    {
        txtCity.Text = ddlCity.SelectedItem.Text.ToString();
        txtCoentecode.Text= ecity.getCenterCode(txtCity.Text.ToString());
    }
    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        ecity.updateCity(ddlCity.SelectedItem.Text.ToString(), txtCity.Text.ToString(), txtCoentecode.Text.ToString());
        ddlCity.Items.Clear();
        ecity.getItems(ddlCity);
        txtCity.Text = ddlCity.SelectedItem.Text.ToString();
          
    }
    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        ecity.deleteCity(ddlCity.SelectedItem.Text.ToString());
        ddlCity.Items.Clear();
        ecity.getItems(ddlCity);
        txtCity.Text = ddlCity.SelectedItem.Text.ToString();
          
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ecity.addCity(txtCity.Text.ToString(), txtCoentecode.Text.ToString());
        ddlCity.Items.Clear();
        ecity.getItems(ddlCity);
        txtCity.Text = ddlCity.SelectedItem.Text.ToString();
    }
}