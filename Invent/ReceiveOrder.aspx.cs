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
using System.Globalization;
 
public partial class Invent_ReceiveOrder : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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

                ddlExamSeason.Focus();
                pnlSupplier.Visible = false;
                pnlProspectus.Visible = false;
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                pnlList.Visible = false;
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
        pnlList.Visible = false;
        txtYear.Focus();
    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlList.Visible = false;
        ddlSupplier.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        pnlList.Visible = false;
        ddlSupplier.Focus();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblException.Visible = false;
        GridViewRow row;
        row = GridView1.SelectedRow; 
        if (row.Cells[3].Text.ToString() == "Books")
        {
            pnlList.Visible = true;
            pnlList.Focus(); 
            pnlProspectus.Visible = false;
            lblOrderNo.Text = row.Cells[4].Text.ToString();
            grdShow.DataBind();
        }
        else 
        {
            pnlList.Visible = false;
            pnlProspectus.Visible = true;
            lblPros.Text = row.Cells[4].Text.ToString();
            lblReqPros.Text = row.Cells[5].Text.ToString();
            pnlProspectus.Focus();
        }
    }
    int totalQt, sup, stoke,req; string status;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblException.Visible = false;
        con.Open();
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        pnlList.Visible = true;
        int i = 0;
         cmd = new SqlCommand("select * from Purches where OrderNo='"+lblOrderNo.Text+"' and OrderType='Books'", con);
        SqlDataReader read;
        read = cmd.ExecuteReader();
        while (read.Read())
        {    req=Convert.ToInt32(read["RequiredQt"].ToString());
            if (read["SupplyQt"].ToString()=="")
                totalQt = 0;
            else
            totalQt = Convert.ToInt32(read["SupplyQt"].ToString());
        }
        read.Close(); read.Dispose();
        lblException.Text = "Re-Enter Quantity for";
        while (i < grdShow.Rows.Count)
        {
            TextBox txtQuan = (TextBox)grdShow.Rows[i].FindControl("txtQuantity");
            Label lblQuan = (Label)grdShow.Rows[i].FindControl("lblQuantity");
            if (txtQuan.Text == "")
            {
                txtQuan.Text = "0";
            }
            if (txtQuan.Text != "")
            {
                if (Convert.ToInt32(grdShow.Rows[i].Cells[3].Text) == Convert.ToInt32(grdShow.Rows[i].Cells[4].Text))
                {
                }
                else if (Convert.ToInt32(grdShow.Rows[i].Cells[3].Text) >= Convert.ToInt32(txtQuan.Text) + Convert.ToInt32(grdShow.Rows[i].Cells[4].Text))
                {
                    cmd = new SqlCommand("select * from SubjectMaster where CourseID='" + grdShow.Rows[i].Cells[6].Text + "' and SubjectCode='" + grdShow.Rows[i].Cells[1].Text + "'", con);
                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["Stoke"].ToString() == "")
                            stoke = 0;
                        else
                            stoke = Convert.ToInt32(reader["Stoke"].ToString());
                    }
                    reader.Close(); reader.Dispose();
                    sup = Convert.ToInt32(grdShow.Rows[i].Cells[4].Text.ToString());
                        int totalSupply = Convert.ToInt32(txtQuan.Text) + sup;
                        cmd = new SqlCommand("update PurchesList set SupplyQt='" + totalSupply.ToString() + "' where OrderNo='" + lblOrderNo.Text + "' and SubjectCode='" + grdShow.Rows[i].Cells[1].Text + "'", con);
                        cmd.ExecuteNonQuery();
                        stoke=stoke+totalSupply;
                        cmd = new SqlCommand("update SubjectMaster set Stoke='" + stoke.ToString() + "' where CourseID='" + grdShow.Rows[i].Cells[6].Text + "' and SubjectCode='" + grdShow.Rows[i].Cells[1].Text + "'", con);
                        cmd.ExecuteNonQuery();
                        totalQt += Convert.ToInt32(txtQuan.Text);
                }
                else
                {
                    lblException.Visible = true;
                    lblException.Text += " " + grdShow.Rows[i].Cells[1].Text+", ";
                }
            }
            i++;
        }
          lblSupply.Text = totalQt.ToString();
          if (req==Convert.ToInt32(lblSupply.Text))
        {
            status = "Delivered";
        }
        else
            status = "Ordered";
        cmd = new SqlCommand("update Purches set SupplyQt='"+lblSupply.Text+"',DeliverDate='"+Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"),dtinfo)+"',Status='"+status+"' where OrderNo='"+lblOrderNo.Text+"' and OrderType='Books'", con);
        cmd.ExecuteNonQuery();
        con.Close(); con.Dispose();
        GridView1.DataBind();
        grdShow.DataBind();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successful Save.!')", true);
        GridView1.Focus();
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        pnlList.Visible = false;
    }
    protected void grdShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //grdShow.Columns[6].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Visible = false;
            TextBox txtQuan = (TextBox)e.Row.FindControl("txtQuantity");
            Label lblQuan = (Label)e.Row.FindControl("lblQuantity");
            if (e.Row.Cells[4].Text=="&nbsp;")
                e.Row.Cells[4].Text = "0";
            if (Convert.ToInt32(e.Row.Cells[3].Text) == Convert.ToInt32(e.Row.Cells[4].Text))
            {
                txtQuan.Visible = false;
                lblQuan.Visible = true;
                lblQuan.Text = e.Row.Cells[4].Text.ToString();
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Visible = false;
        }
    }
    protected void btnPros_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        lblExcep.Visible = false;
        con.Open();
        cmd = new SqlCommand("select * from Purches where OrderNo='" + lblPros.Text + "'", con);
        SqlDataReader read;
        read = cmd.ExecuteReader();
        while (read.Read())
        {
            if (read["SupplyQt"].ToString() == "")
                totalQt = 0;
            else
                totalQt = Convert.ToInt32(read["SupplyQt"].ToString());
        }
        read.Close(); read.Dispose();
        int supplyPros = totalQt + Convert.ToInt32(txtSupPros.Text);
        if (supplyPros > Convert.ToInt32(lblReqPros.Text))
        {
            lblExcep.Text = "Re-enter Data";
            lblExcep.Visible = true;
            txtSupPros.Text = "";
            txtSupPros.Focus();
        }
        else
        { string stats;
            if(supplyPros==Convert.ToInt32(lblReqPros.Text))
            {
                stats="Delivered";
            }
            else
                stats="Ordered";

            cmd = new SqlCommand("update Purches set SupplyQt='" + supplyPros.ToString() + "',DeliverDate='" + Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"), dtinfo) + "',Status='" + stats + "' where OrderNo='" + lblPros.Text + "'", con);
            cmd.ExecuteNonQuery();
            txtSupPros.Text = "";
            pnlProspectus.Visible = false;
            ddlExamSeason.Focus();
        }
        con.Close(); con.Dispose();
        GridView1.DataBind();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successful Added')", true);
    }
    protected void btnOrder_Click(object sender, EventArgs e)
    {    
        con.Open();
        cmd = new SqlCommand("select * from Purches where OrderNo='"+txtOrder.Text+"'", con);
        SqlDataReader read = cmd.ExecuteReader();
        while (read.Read())
        {
            if (read["OrderType"].ToString() == "Books")
            {
                pnlList.Visible = true;
                pnlProspectus.Visible = false; 
                pnlList.Focus();
                pnlProspectus.Visible = false;
                lblOrderNo.Text = txtOrder.Text;
                grdShow.DataBind();
            }
            else
            {
                pnlList.Visible = false;
                pnlProspectus.Visible = true;
                lblPros.Text = txtOrder.Text;
                lblReqPros.Text = read["RequiredQt"].ToString();
                pnlProspectus.Focus();
            }
        }
        read.Close(); read.Dispose();
        txtOrder.Text = "";
        con.Close(); con.Dispose();
        txtOrder.Focus();
    }

}