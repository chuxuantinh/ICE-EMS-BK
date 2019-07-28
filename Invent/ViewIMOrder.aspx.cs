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

public partial class Invent_ViewIMOrder : System.Web.UI.Page
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
                grd();
            }
        }
        catch(NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {

        con.Dispose();

    }
    private void grd()
    {
        SqlDataAdapter ad3 = new SqlDataAdapter("select IMID,OrderNo,Type,RequiredQt,OrderType,Amount,Refund as RefundedAmount from IMOrder where Session='" + lblHiddenSeason.Text + "' ORDER BY OrderNo DESC", con);
        DataTable dt3 = new DataTable();
        ad3.Fill(dt3);
        GridView1.DataSource = dt3;
        GridView1.DataBind();
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
        grd();
    }
   

    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        grd();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlList.Visible = true;
        GridViewRow row;
        row = GridView1.SelectedRow;
        lblOrderNo.Text = row.Cells[2].Text.ToString();
        lblIMID.Text = row.Cells[1].Text.ToString();
        if (row.Cells[3].Text.ToString() == "Prospectus")
        {
            pnlList.Visible = false;
            pnlUpdate.Visible = true;
            ddlStatusUp.SelectedValue = "Delivered";
        }
        else if (row.Cells[3].Text.ToString() == "Books")
        {
            pnlList.Visible = true;
            pnlList.Focus();
            pnlUpdate.Visible = true;
            ddlStatusUp.SelectedValue = "Delivered";
            grdShow.DataBind();

        }
        ddlStatusUp.Focus();
        lblUpdate.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnPros_Click(object sender, EventArgs e)
    {

    }
    string query;
    protected void btnOrder_Click(object sender, EventArgs e)
    {

        if (ddlSelect.SelectedValue == "OrderNo")
        {
            query = "select IMID,OrderNo,Type,RequiredQt,OrderType,Amount,Refund as RefundedAmount from IMOrder where OrderNo='" + txtOrder.Text + "' and Session='" + lblHiddenSeason.Text + "' ORDER BY OrderNo DESC";
        }
        else if (ddlSelect.SelectedValue == "IMID")
        {
            query = "select IMID,OrderNo,Type,RequiredQt,OrderType,Amount,Refund as RefundedAmount from IMOrder where IMID='" + txtIMID.Text + "' and Session='" + lblHiddenSeason.Text + "' ORDER BY OrderNo DESC";
        }
        else if (ddlSelect.SelectedValue == "Type")
        {
            query = "select IMID,OrderNo,Type,RequiredQt,OrderType,Amount,Refund as RefundedAmount from IMOrder where Type='" + ddlType.SelectedValue + "' and Session='" + lblHiddenSeason.Text + "' ORDER BY OrderNo DESC";
        }
        else if (ddlSelect.SelectedValue == "Status")
        {
            query = "select IMID,OrderNo,Type,RequiredQt,OrderType,Amount,Refund as RefundedAmount from IMOrder where Status='" + ddlStatus.SelectedValue + "' and Session='" + lblHiddenSeason.Text + "' ORDER BY OrderNo DESC";
        }
        else if (ddlSelect.SelectedValue == "Consignment")
        {
            query = "select IMID,OrderNo,Type,RequiredQt,OrderType,Amount,Refund as RefundedAmount from IMOrder where ConsignmentNo='" + txtOrder.Text + "' and Session='" + lblHiddenSeason.Text + "' ORDER BY OrderNo DESC";
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
            ddlType.Visible = false;
            txtIMID.Visible = false;
            ddlStatus.Visible = false;
        }
      
        else if (ddlSelect.SelectedValue == "Type")
        {
            lblName.Text = "Order Type:";
            ddlType.Visible = true;
            txtOrder.Visible = false;
            txtIMID.Visible = false;
            ddlStatus.Visible = false;
        }
        else if (ddlSelect.SelectedValue == "IMID")
        {
            lblName.Text = "Enter IMID:";
            ddlType.Visible = false;
            txtOrder.Visible = false;
            txtIMID.Visible = true;
            ddlStatus.Visible = false;
        }
        else if (ddlSelect.SelectedValue == "Status")
        {
            lblName.Text = "Select Status:";
            ddlType.Visible = false;
            txtOrder.Visible = false;
            txtIMID.Visible = false;
            ddlStatus.Visible = true;
        }
        else if (ddlSelect.SelectedValue == "Consignment")
        {
            lblName.Text = "Enter Consignment No:";
            ddlType.Visible = false;
            txtOrder.Visible = true;
            txtIMID.Visible = false ;
            ddlStatus.Visible = false;
        }
        ddlSelect.Focus();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update IMOrder set Status='"+ddlStatusUp.SelectedValue+"',ConsignmentNo='"+txtConsignment.Text+"' where OrderNo='"+lblOrderNo.Text+"' and IMID='"+lblIMID.Text+"'", con);
        cmd.ExecuteNonQuery();
        lblUpdate.Text = "Updated";
        txtConsignment.Text = ""; grd();
        ddlExamSeason.Focus();
        con.Close(); con.Dispose();
    }
}