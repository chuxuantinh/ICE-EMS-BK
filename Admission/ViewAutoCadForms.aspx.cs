using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admission_ViewAutoCadForms : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd;
    SqlDataAdapter adp;
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
                BindStatus();
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
    //private void BindAutoCad()
    //{
    //    adp = new SqlDataAdapter("select * from MCAD", con);  DataTable dt = new DataTable();  adp.Fill(dt); GridAutoCad.DataSource = dt; GridAutoCad.DataBind();
    //}
    protected void GridAutoCad_SelectedIndexChanged(object sender, EventArgs e)
    {
        adp = new SqlDataAdapter("select RegNo,FeeType,Date,Amount from MCADFee where SID='" + GridAutoCad.SelectedRow.Cells[2].Text.ToString() + "' order by Date Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridViewAutoCad.DataSource = dt;
        GridViewAutoCad.DataBind(); pnlFees.Visible = true; pnlspc.Visible = false;
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStatus();
    }
    private void BindStatus()
    {
        clear(); if (ddlSelect.SelectedValue == "BatchID") { ddlStatus.Visible = true; lblStatus.Visible = true; }
        else { ddlStatus.Visible = false; lblStatus.Visible = false; }
    }
    private void BindTb()
    {
        DataTable dt = new DataTable(); adp.Fill(dt); GridAutoCad.DataSource = dt; GridAutoCad.DataBind();
    }
    private void clear()
    {
        txtSID.Text = ""; txtSID.Focus(); GridAutoCad.DataBind(); pnlFees.Visible = false; pnlspc.Visible = true;
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (txtSID.Text == "") { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Found Please enter details!')", true); clear(); }
        else { con.Open();
            if (ddlSelect.SelectedValue == "Membership")
            {
                cmd = new SqlCommand("select SID from MCAD where SID='" + txtSID.Text + "'", con);
                string strSID = Convert.ToString(cmd.ExecuteScalar());
                if (strSID == "") { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Found Please enter details!')", true); clear(); }
                else { adp = new SqlDataAdapter("select Batch_ID,SID,Name,RegNo,Grade,RegDate,GradeDate,Status,CurrentStatus from MCAD where SID='" + txtSID.Text + "'  order by RegDate Desc", con); BindTb(); }
            }
            else if (ddlSelect.SelectedValue == "RegistrationNo")
            {
                cmd = new SqlCommand("select RegNo from MCAD where RegNo='" + txtSID.Text + "'", con);
                string strSID = Convert.ToString(cmd.ExecuteScalar());
                if (strSID == "") { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Found Please enter details!')", true); clear(); }
                else { adp = new SqlDataAdapter("select Batch_ID,SID,Name,RegNo,Grade,RegDate,GradeDate,Status,CurrentStatus from MCAD where RegNo='" + txtSID.Text + "' order by RegDate Desc", con); BindTb(); }
            }
            else if (ddlSelect.SelectedValue == "BatchID")
            {
                cmd = new SqlCommand("select Batch_ID from MCAD where Batch_ID='" + txtSID.Text + "' and Status='" + ddlStatus.SelectedValue.ToString() + "'", con);
                string strSID = Convert.ToString(cmd.ExecuteScalar());
                if (strSID == "") { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Found Please enter details!')", true); clear(); }
                else { adp = new SqlDataAdapter("select Batch_ID,SID,Name,RegNo,Grade,RegDate,GradeDate,Status,CurrentStatus from MCAD where Batch_ID='" + txtSID.Text + "' and Status='" + ddlStatus.SelectedValue.ToString() + "' order by RegDate Desc", con); BindTb(); }
            }
            con.Close(); con.Dispose();
        }
    }
    protected void GridAutoCad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text != null & e.Row.Cells[6].Text != "" & e.Row.Cells[6].Text != "&nbsp;")
                e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[7].Text != null & e.Row.Cells[7].Text != "" & e.Row.Cells[7].Text != "&nbsp;")
                e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void GridViewAutoCad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToString().TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[2].Text != null & e.Row.Cells[2].Text != "" & e.Row.Cells[2].Text != "&nbsp;")
                e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("dd/MM/yyyy");
        }
    }
}