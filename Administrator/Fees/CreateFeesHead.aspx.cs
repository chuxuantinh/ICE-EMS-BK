using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Administrator_Fees_CreateFeesHead : System.Web.UI.Page
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
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ddlExamSeason.Focus();
                ddlBind();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString(); ddlBind(); txtYearSeason.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        ddlBind(); ddlExamSeason.Focus();
    }
    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        if (txtFeesName.Text == "" | txtYearSeason.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please enter Details!')", true);
            txtFeesName.Text = ""; txtAmount.Text = ""; ddlExamSeason.Focus();
        }
        else
        {
            con.Open();
            cmd = new SqlCommand("spCreateFeesHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FeeName", SqlDbType.NVarChar).Value = txtFeesName.Text.ToString();
            if (txtAmount.Text == "")
                cmd.Parameters.AddWithValue("@Amount", SqlDbType.Money).Value = 0;
            else
                cmd.Parameters.AddWithValue("@Amount", SqlDbType.Money).Value = txtAmount.Text;
            cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "YES";
            cmd.Parameters.AddWithValue("@SerialNo", SqlDbType.Int).Value = 0;
            cmd.Parameters.AddWithValue("@Session", SqlDbType.NVarChar).Value = lblExamSeasonHidden.Text.ToString();
            cmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = ddlType.SelectedValue.ToString();
            cmd.Parameters.Add("@ERROR", SqlDbType.Char, 50);
            cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + (string)cmd.Parameters["@ERROR"].Value + "')", true);
            txtAmount.Text = ""; txtFeesName.Text = ""; ddlExamSeason.Focus();
            con.Close();
            ddlBind();
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFeesName.Text == "" | txtYearSeason.Text == "")
            {
                //System.Windows.Forms.MessageBox.Show("DEEPAK");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please enter Details!')", true);
                txtFeesName.Text = ""; txtAmount.Text = ""; txtFeesName.Focus();
            }
            else
            {

                con.Open();
                cmd = new SqlCommand("spUpdFeesHeader", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeName", SqlDbType.NVarChar).Value = txtFeesName.Text.ToString();
                cmd.Parameters.AddWithValue("@FeeName1", SqlDbType.NVarChar).Value = ddlFees.SelectedValue.ToString();
                if (txtAmount.Text == "")
                    cmd.Parameters.AddWithValue("@Amount", SqlDbType.Money).Value = 0;
                else
                    cmd.Parameters.AddWithValue("@Amount", SqlDbType.Money).Value = txtAmount.Text;
                cmd.Parameters.AddWithValue("@Session", SqlDbType.NVarChar).Value = lblExamSeasonHidden.Text.ToString();
                cmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = ddlType.SelectedValue.ToString();
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + txtFeesName.Text.ToString() + " FeeName Header Successfully Updated !')", true);
                txtAmount.Text = ""; txtFeesName.Text = ""; 
                con.Close(); ddlBind();
            }
        }
        catch (NullReferenceException ex)
        {
            lblExceptionID.Text = ex.ToString();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("delete from FeeList where FeeName='" + ddlFees.SelectedValue.ToString() + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "' and Status='YES'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        ddlBind();
        txtAmount.Text = ""; txtFeesName.Text = ""; ddlExamSeason.Focus();
    }
    protected void ddlFees_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmount.Text = ""; txtFeesName.Text = ""; 
        con.Open();
        cmd = new SqlCommand("select * from FeeList where Session='" + lblExamSeasonHidden.Text.ToString() + "' and Status='YES' and FeeName='" + ddlFees.SelectedValue.ToString() + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            txtFeesName.Text = dr["FeeName"].ToString();
            txtAmount.Text = dr["Amount"].ToString();
            txtAmount.Text = dr["Amount"].ToString().TrimEnd('0').TrimEnd('.');
        }
        dr.Close(); con.Close(); ddlExamSeason.Focus();
    }
    private void ddlBind()
    {
        adp = new SqlDataAdapter("select * from FeeList where Status='YES' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlFees.DataSource = dt;
        GridFeesHeader.DataSource = dt;
        ddlFees.DataValueField = "FeeName";
        GridFeesHeader.DataBind();
        ddlFees.DataBind(); if (ddlFees.Items.Count == 0) ddlFees.Visible = false; else ddlFees.Visible = true;
    }
    protected void GridFeesHeader_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
}