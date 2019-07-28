using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Invent_StockView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlCourseId.Focus();
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
    protected void Page_Unload(object sender, EventArgs e)
    {

        con.Dispose();

    }
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
        int i = 0,  totalqt = 0;
        while (i < GridView1.Rows.Count)
        {
            totalqt += Convert.ToInt32(GridView1.Rows[i].Cells[3].Text);
            i++;
        }
       
        lblQuantity.Text = totalqt.ToString();
    }
    protected void ddlCourseId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.Focus();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPart.Focus();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.Focus();
    }
}