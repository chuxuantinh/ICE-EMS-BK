using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Xml.Linq;
using System.Globalization;
using System.Xml;

public partial class Admission_MCADBatch : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
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
                btnupdate.Enabled = false;
                grid();
                con.Close();
                con.Open();
                cmd = new SqlCommand("Select Max(Batch_ID) from MCADBatch", con);
                lblCurrentBatch.Text = Convert.ToString(cmd.ExecuteScalar());
                if (lblCurrentBatch.Text == "")
                {
                    lblCurrentBatch.Text = "1";
                }
                else
                {
                    lblCurrentBatch.Text = (Convert.ToInt32(lblCurrentBatch.Text)+1).ToString();
                }
                lblBatchNo.Text = lblCurrentBatch.Text.ToString();
                con.Close();
                lblBatchNo.Focus();
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
            Response.Redirect("../Loign.aspx");
        }
    }
    //Insert Data
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close();
            con.Open();
            string str = "insert into MCADBatch(HLateFee,OLateFee,HRegFee,ORegFee,StartDate,EndDate)values(@HLateFee,@OLateFee,@RegNo,@ORegFee,@StartDate,@EndDate)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@HLateFee", txtLatefee.Text.ToString());
            cmd.Parameters.AddWithValue("@OLateFee", txtLatefeeOverseas.Text.ToString());
            cmd.Parameters.AddWithValue("@RegNo", txtRegistrationFee.Text.ToString());
            cmd.Parameters.AddWithValue("@ORegFee", txtRegistrationFeeOverseas.Text.ToString());
            if (txtStartingDate.Text == "")
                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartingDate.Text, dtinfo));
            if (txtEndingDate.Text == "")
                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtEndingDate.Text, dtinfo));
            cmd.ExecuteNonQuery();
            con.Close();
            grid();
            txtLatefee.Text = string.Empty;
            txtLatefeeOverseas.Text = string.Empty;
            txtRegistrationFee.Text = string.Empty;
            txtRegistrationFeeOverseas.Text = string.Empty;
            txtStartingDate.Text = string.Empty;
            txtEndingDate.Text = string.Empty;
            grvAutocad.Focus();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Create New Batch ')", true);
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('incorrect Input Format')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('incorrect input format ')", true);
        }
        finally
        {
            con.Dispose();
        }
    }
    //Update data
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Open();
        cmd = new SqlCommand("update MCADBatch set HLateFee=@HLateFee,OLateFee=@OLateFee,HRegFee=@HRegFee,ORegFee=@ORegFee,StartDate=@StartDate,EndDate=@EndDate where Batch_ID='" + lblBatchNo.Text.ToString() + "'", con);
        cmd.Parameters.AddWithValue("@HLateFee", txtLatefee.Text.ToString());
        cmd.Parameters.AddWithValue("@OLateFee", txtLatefeeOverseas.Text.ToString());
        cmd.Parameters.AddWithValue("@HRegFee", txtRegistrationFee.Text.ToString());
        cmd.Parameters.AddWithValue("@ORegFee", txtRegistrationFeeOverseas.Text.ToString());
        if (txtStartingDate.Text == "" | txtStartingDate.Text=="&nbsp;")
            cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartingDate.Text, dtinfo));
        if (txtEndingDate.Text == "" | txtStartingDate.Text == "&nbsp;")
            cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtEndingDate.Text, dtinfo));
        cmd.ExecuteNonQuery();
        con.Close();
        gridupdate("update");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Updated ')", true);
        lblBatchNo.Text = string.Empty;
        txtLatefee.Text = string.Empty;
        txtLatefeeOverseas.Text = string.Empty;
        txtRegistrationFee.Text = string.Empty;
        txtRegistrationFeeOverseas.Text = string.Empty;
        txtStartingDate.Text = string.Empty;
        txtEndingDate.Text = string.Empty;
        grvAutocad.Focus();
    }
   private void gridupdate(string type)
    {
        if(type=="input")
        {
            //lblBatchNo.Text = grvAutocad.SelectedRow.Cells[2].Text.ToString();
            //txtRegistrationFee.Text = grvAutocad.SelectedRow.Cells[3].Text.ToString();
            //txtLatefee.Text = grvAutocad.SelectedRow.Cells[4].Text.ToString();
            //txtRegistrationFeeOverseas.Text = grvAutocad.SelectedRow.Cells[5].Text.ToString();
            //txtLatefeeOverseas.Text = grvAutocad.SelectedRow.Cells[6].Text.ToString();
        }
        else if(type=="update")
        {
            grvAutocad.SelectedRow.Cells[1].Text = lblBatchNo.Text.ToString();
            grvAutocad.SelectedRow.Cells[2].Text = txtRegistrationFee.Text.ToString();
            grvAutocad.SelectedRow.Cells[3].Text = txtLatefee.Text.ToString();
            grvAutocad.SelectedRow.Cells[4].Text = txtRegistrationFeeOverseas.Text.ToString();
            grvAutocad.SelectedRow.Cells[5].Text = txtLatefeeOverseas.Text.ToString();
        }
    }
    // Gridview
    public void grid()
    {
        da = new SqlDataAdapter("select Batch_ID as Batch,HRegFee as Home_Registration,HLateFee as Home_LateFee,ORegFee as Overseas_Registration,OLateFee as Overseas_LateFee ,StartDate,EndDate from MCADBatch order by Batch_ID desc", con);
        ds = new DataSet();
        da.Fill(ds);
        grvAutocad.DataSource = ds;
        grvAutocad.DataBind();
    }
    protected void grvAutocad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid();
        grvAutocad.PageIndex = e.NewPageIndex;
        grvAutocad.DataBind();
    }
    protected void grvAutocad_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.TrimEnd('0').TrimEnd('.');
            if(e.Row.Cells[6].Text!="&nbsp;" & e.Row.Cells[6].Text!="")
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[7].Text != "&nbsp;" & e.Row.Cells[7].Text != "")
            e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).ToString("dd/MM/yyy");
        }
    }
    protected void grvAutocad_OnselectedIndexChanged(object sender, EventArgs e)
    {
        lblBatchNo.Text = grvAutocad.SelectedRow.Cells[1].Text.ToString();
        txtLatefee.Text = grvAutocad.SelectedRow.Cells[3].Text.ToString();
        txtLatefeeOverseas.Text = grvAutocad.SelectedRow.Cells[5].Text.ToString();
        txtRegistrationFee.Text = grvAutocad.SelectedRow.Cells[2].Text.ToString();
        txtRegistrationFeeOverseas.Text = grvAutocad.SelectedRow.Cells[4].Text.ToString();
        txtStartingDate.Text = grvAutocad.SelectedRow.Cells[6].Text.ToString();
        txtEndingDate.Text = grvAutocad.SelectedRow.Cells[7].Text.ToString();

        btnupdate.Enabled = true;
    }
}