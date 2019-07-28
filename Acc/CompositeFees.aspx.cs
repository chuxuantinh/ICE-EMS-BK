using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Acc_CompositeFees : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlDataAdapter adp; SqlCommand cmd;
    SessionDuration sd = new SessionDuration();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0)
                ddlExamSeason.SelectedValue = "Sum";
            else { ddlExamSeason.SelectedValue = "Win"; }
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            lblSessionID.Text = sd.SessionToSessionID(lblExamSeasonHidden.Text).ToString();
            txtSID.Focus();
        }
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString(); BindGrid();
        lblSessionID.Text = sd.SessionToSessionID(lblExamSeasonHidden.Text).ToString();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString(); BindGrid(); txtYearSeason.Focus();
        lblSessionID.Text = sd.SessionToSessionID(lblExamSeasonHidden.Text).ToString();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
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
        finally
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblException.Text = "";
        if (txtAmount.Text == "" | txtSID.Text == "")
        {
            lblException.Text = "please fill Details !";
        }
        else
        {
            con.Open();
            cmd = new SqlCommand("insert into CompositeFees(SID,Amount,Status,Date,SessionID,Type) values(@SID,@Amount,@Status,@Date,@SessionID,@Type)", con);
            cmd.Parameters.AddWithValue("@SID", txtSID.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@SessionID", lblSessionID.Text);
            cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Composite Fees Submitted Successfully!')", true); txtAmount.Text = ""; txtAmount.Focus();
            BindGrid(); con.Close();
        }
    }
    private void BindGrid()
    {
        adp = new SqlDataAdapter("select * from CompositeFees where SID='" + txtSID.Text + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridFees.DataSource = dt;
        GridFees.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        lblException.Text = ""; con.Open();
        cmd = new SqlCommand("select SID from CompositeFees where SID='" + txtSID.Text + "'", con);
        string strSID = Convert.ToString(cmd.ExecuteScalar());
        if (strSID != "")
        {
            BindGrid(); txtAmount.Focus();
        }
    }
    protected void GridFees_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
    {
        con.Open();
        if (e.CommandName == "Del")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rw = GridFees.Rows[index];
            cmd = new SqlCommand("delete from CompositeFees where SN='" + rw.Cells[1].Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close(); BindGrid(); txtSID.Focus();
        }
    }
    protected void GridFees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text != null & e.Row.Cells[5].Text != "" & e.Row.Cells[5].Text != "&nbsp;")
                e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.TrimEnd('0').TrimEnd('.');
        }
    }
}