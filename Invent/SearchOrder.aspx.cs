using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

public partial class Invent_SearchOrder : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    InvenItem Iitems;
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
                Iitems = new InvenItem();
                Iitems.getItems(ddlType);
                pnlList.Visible = false;
                ddlType.Visible = true;
                lblName.Text = "select Order Type:";
                ddlExamSeason.Focus();
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSupplier.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        ddlSupplier.Focus();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.ToString().TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[2].Text != "Books")
            {
                GridView1.Columns[0].Visible = false;
            }
            else GridView1.Columns[0].Visible = true;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlList.Visible = true;
         GridViewRow row;
        row = GridView1.SelectedRow;
        if (row.Cells[2].Text.ToString() == "Books")
        {
            pnlList.Visible = true;
            pnlList.Focus();
            lblOrderNo.Text = row.Cells[1].Text.ToString();
            grdShow.DataBind();
        }
        else
        {
            pnlList.Visible = false;
        }
        ddlExamSeason.Focus();
    }
    string query;
    protected void btnOrder_Click(object sender, EventArgs e)
    {
        if (ddlSelect.SelectedValue == "OrderNo")
        {
            query = "select OrderNo,OrderType,RequiredQt,SupplyQt,Amount from Purches where OrderNo='"+txtOrder.Text+"' and Session='"+lblHiddenSeason.Text+"'";
        }
        else if (ddlSelect.SelectedValue == "Sup")
        {
            query = "select OrderNo,OrderType,RequiredQt,SupplyQt,Amount from Purches where Supplier='" + ddlSupplier.SelectedValue + "' and Session='" + lblHiddenSeason.Text + "'";
        }
        else if (ddlSelect.SelectedValue == "Type")
        {
            query = "select OrderNo,OrderType,RequiredQt,SupplyQt,Amount from Purches where OrderType='" + ddlType.SelectedValue + "' and Session='" + lblHiddenSeason.Text + "'";
        }
        else if (ddlSelect.SelectedValue == "Status")
        {
            query = "select OrderNo,OrderType,RequiredQt,SupplyQt,Amount from Purches where Status='"+ddlStatus.SelectedValue+"'and Session='" + lblHiddenSeason.Text + "'";
        }
        SqlDataAdapter ad3 = new SqlDataAdapter(query, con);
        DataTable dt3 = new DataTable();
        ad3.Fill(dt3);
        GridView1.DataSource = dt3;
        GridView1.DataBind();
        GridView1.Focus();
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlList.Visible = false;
        if (ddlSelect.SelectedValue == "OrderNo")
        {
            lblName.Text = "Enter Order No:";
            txtOrder.Visible = true;
            ddlSupplier.Visible = false;
            ddlType.Visible = false;
            ddlStatus.Visible = false;
        }
        else if (ddlSelect.SelectedValue == "Sup")
        {
            lblName.Text = "Supplier Name:";
            ddlSupplier.Visible = true;
            txtOrder.Visible = false;
            ddlType.Visible = false;
            ddlStatus.Visible = false;
        }
         else if (ddlSelect.SelectedValue == "Type")
        {
            lblName.Text = "Order Type:";
            ddlType.Visible = true;
            ddlSupplier.Visible = false;
            txtOrder.Visible = false;
            ddlStatus.Visible = false;
        }
        else if (ddlSelect.SelectedValue == "Status")
        {
            lblName.Text = "Status Type:";
            ddlStatus.Visible = true;
            ddlSupplier.Visible = false;
            ddlType.Visible = false;
            txtOrder.Visible = false;
        }
        ddlSelect.Focus();
    }
}