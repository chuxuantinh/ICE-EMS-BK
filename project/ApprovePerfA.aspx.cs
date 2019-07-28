using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;

public partial class project_ApprovePerfA : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataAdapter adp;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null) { Response.Redirect("../Login.aspx"); }
            else
            {
                if (!IsPostBack)
                {
                    bindGrid(); txtSID.Focus();
                }
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
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
            if (i == 0 | i == 1) Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2) { Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString()); }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void txtSID_TextChanged(object sender, EventArgs e)
    {
        if (txtSID.Text == "") { lblExceptionOK.Text = "Please insert Membership No."; txtSID.Focus(); }
        else { okk(txtSID.Text.ToString()); }
    }
    private void okk(string strid)
    {
        lblExceptionOK.Text = "";
        adp = new SqlDataAdapter("select DiaryA,SID,Course,Part,SynopsisDate,Status from Project where sid='" + strid + "' and Status='ProformaASubmitted' and EntryStatus='Running'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridAppPerfmA.DataSource = dt;
        GridAppPerfmA.DataBind();
        GridAppPerfmA.Focus();
        if (GridAppPerfmA.Rows.Count == 0) { txtSID.Text = ""; lblExceptionOK.Text = "Please Enter Correct Membership ID!"; txtSID.Focus(); }
    }
    private void bindGrid()
    {
        adp = new SqlDataAdapter("select DiaryA,SID,Course,Part,SynopsisDate,Session,Status from Project where Status='ProformaASubmitted' and EntryStatus='Running'", con);
        DataTable dt = new DataTable(); adp.Fill(dt); GridAppPerfmA.DataSource = dt; GridAppPerfmA.DataBind();
    }
    protected void GridAppPerfmA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header) { e.Row.Cells[2].Text = "Membership"; }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text != null & e.Row.Cells[5].Text != "" & e.Row.Cells[5].Text != "&nbsp;")
                e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).ToString("dd/MM/yyyy");
        }
    }
    private void bindRadio()
    {
        try
        {
            cmd = new SqlCommand("select ID from InstitutionReg where Name='" + ddlFinalOpn.SelectedValue.ToString() + "'", con);
            string Insid = Convert.ToString(cmd.ExecuteScalar());
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update Project set Status='ProformaAApproved',Institution='" + ddlFinalOpn.SelectedValue.ToString() + "', InstitutionID='" + Insid + "',Remark='" + txtSynRemarks.Text.ToString() + "', ProjectNo='" + txtProjectNo.Text.ToString() + "' where EntryStatus='Running' and SID='" + GridAppPerfmA.SelectedRow.Cells[2].Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Approved Successfully!')", true);
            bindGrid(); pnlData.Visible = false; GridAppPerfmA.Focus();
        }
        catch (NullReferenceException ex) { lblApprvExcep.Text = "Invalid!"; }
    }
    protected void GridAppPerfmA_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select * from Project where EntryStatus='Running' and SID='" + GridAppPerfmA.SelectedRow.Cells[2].Text.ToString() + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblName.Text = dr["StudentName"].ToString();
                lblOpn1.Text = dr["Option1"].ToString();
                lblOpn2.Text = dr["Option2"].ToString();
                lblOpn3.Text = dr["Option3"].ToString();
                lblCourSt.Text = dr["CourseStatus"].ToString();
                txtSynRemarks.Text = dr["Remark"].ToString();
                lblSessionHiddend.Text = dr["Session"].ToString();
                txtProjectNo.Text = dr["ProjectNo"].ToString();
            }
            dr.Close(); bindFinalOpn(); con.Close(); pnlData.Visible = true; lblApprvExcep.Text = ""; lblExceptionOK.Text = ""; ddlFinalOpn.Focus();
        }
        catch (NullReferenceException ex) { lblApprvExcep.Text = "Invalid!"; }
    }
    private void bindFinalOpn()
    {
        adp = new SqlDataAdapter("select Name from InstitutionReg order by Name", con);
        DataTable dt = new DataTable(); adp.Fill(dt); ddlFinalOpn.DataSource = dt; ddlFinalOpn.DataValueField = "Name"; ddlFinalOpn.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open(); bindRadio(); con.Close(); con.Dispose();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        adp = new SqlDataAdapter("Select IMID,SID,Course,Part,DiaryA,Option1,Option2,Option3 from Project where IMID='" + txtIMID.Text.ToString() + "' and Status='ProformaASubmitted' and EntryStatus='Running'", con);
        DataTable dt = new DataTable(); adp.Fill(dt); GridAppPerfmA.DataSource = dt; GridAppPerfmA.Columns[0].Visible = false; GridAppPerfmA.DataBind(); lblNotIM.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlData.Visible = false;
    }
    protected void lbtnNewProjectNo_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        string session = lblSessionHiddend.Text.ToString().Substring(0, 1) + lblSessionHiddend.Text.ToString().Substring(5, 2);
        cmd = new SqlCommand("select max(ProjectNo) from Project where Session='" + lblSessionHiddend.Text.ToString() + "' and (ProjectNo like '%S%' or ProjectNo like '%W%') ", con);
        string count = Convert.ToString(cmd.ExecuteScalar().ToString());
        string ncount = "";
        if (count == "" | count == null)
            ncount = session + "0001";
        else
        {
            count = (Convert.ToInt32(count.Substring(3, count.Length - 3)) + 1).ToString();
            count = count.PadLeft(4, '0');
            ncount = session + count;
        }
        con.Close(); con.Dispose();
        txtProjectNo.Text = ncount.ToString();
        txtProjectNo.Focus();
    }
}