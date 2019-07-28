using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;

public partial class project_ProjectGrade : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlCommand cmd;
    SqlDataAdapter adp;
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
                if (!IsPostBack)
                {
                    txtView.Focus();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
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
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void GridProjGrade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = "Membership";
            e.Row.Cells[3].Text = "Name";
            e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlGrade");
            ddl.SelectedValue = e.Row.Cells[7].Text.ToString();
        }
    }
    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect(url.ToString());
    }
    string strqry;
    protected void btnView_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
    private void bindGrid()
    {
        try
        {
            if (txtView.Text == "") { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('You can not letf text blank!')", true); pnlComplete.Visible = false; }
            else
            {
                con.Close(); con.Open();
                if (ddlSelect.SelectedValue.ToString() == "Enroll" && txtView.Text != "")
                {
                    strqry = "select Project.SID,Project.StudentName,Student.FName,Project.Course,Project.Part,Project.Grade,Project.Session from Project inner join Student on Project.SID=Student.SID and Project.SID='" + txtView.Text.ToString() + "' and Project.EntryStatus='Running'";
                }
                else if (ddlSelect.SelectedValue.ToString() == "IMID" && txtView.Text != "")
                {
                    strqry = "select Project.SID,Project.StudentName,Student.FName,Project.Course,Project.Part,Project.Grade,Project.Session from Project inner join Student ON Project.SID=Student.SID and Project.IMID='" + txtView.Text.ToString() + "' and Project.EntryStatus='Running'";
                }
                else if (ddlSelect.SelectedValue.ToString() == "InsID" && txtView.Text != "")
                {
                    strqry = "select Project.SID,Project.StudentName,Student.FName,Project.Course,Project.Part,Project.Grade,Project.Session from Project,Student where Project.InstitutionID='" + txtView.Text.ToString() + "' and Project.EntryStatus='Running'";
                }
                adp = new SqlDataAdapter(strqry, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                GridProjGrade.DataSource = dt;
                GridProjGrade.DataBind();
                pnlComplete.Visible = true;
                con.Close(); con.Dispose();
                GridProjGrade.Focus();
            }
        }
        catch (NullReferenceException ex) { lblExceptionOK.Text = ex.ToString(); pnlComplete.Visible = false; }
        catch (SqlException ex) { lblExceptionOK.Text = ex.ToString(); pnlComplete.Visible = false; }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            int i = 0; con.Close(); con.Open();
            while (i < GridProjGrade.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GridProjGrade.Rows[i].FindControl("chkapp");
                DropDownList ddlGrading = (DropDownList)GridProjGrade.Rows[i].FindControl("ddlGrade");
                if (rbApp.Checked)
                {
                    cmd = new SqlCommand("update Project set Grade=@Grade, GradeDate=@GradeDate where SID='" + GridProjGrade.Rows[i].Cells[2].Text + "' and EntryStatus='Running'", con);
                    cmd.Parameters.AddWithValue("@Grade", ddlGrading.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@GradeDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.ExecuteNonQuery();
                }
                i++;
            }
            con.Close(); con.Dispose();
            if (i > 0)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Grade Successfully Updated.')", true);
            txtView.Text = ""; bindGrid(); txtView.Focus();
        }
        catch (SqlException ex) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true); pnlComplete.Visible = false; }
        catch (NullReferenceException ex) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true); pnlComplete.Visible = false; }
        catch (OutOfMemoryException ex) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true); pnlComplete.Visible = false; }
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtView.Text = ""; txtView.Focus();
    }
}