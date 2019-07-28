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

public partial class project_SubmitProformaA : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                    txtSID.Focus();
                }
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
    protected void txtSID_TextChanged(object sender, EventArgs e)
    {
        if (txtSID.Text == "")
        {
            lblExceptionOK.Text = "Please insert IMID";
            txtSID.Focus();
        }
        else
        {
            okk(txtSID.Text.ToString());
        }
    }
    private void okk(string strid)
    {
            con.Close(); con.Open();
            lblExceptionOK.Text = "";
            SqlDataReader reader;
            bool flg = false;
            cmd = new SqlCommand("select StudentName,Course,Part,CourseStatus,SynopsisRemarks,DiaryA from Project where sid='" + strid + "' and Status='Selected' and EntryStatus='Running'", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblStuName.Text = reader["StudentName"].ToString();
                lblCourse.Text = reader["Course"].ToString();
                lblPart.Text = reader["Part"].ToString();
                lblStatus.Text = reader["CourseStatus"].ToString();
                txtRemarks.Text = reader["SynopsisRemarks"].ToString();
                txtDiaryno.Text = reader["DiaryA"].ToString();
                lblCourseStatus.Text = reader["CourseStatus"].ToString();
                flg = true;
            }
            else
            {
                txtSID.Text = "";
                lblExceptionOK.Text = "Membership Not Found!";
                pnlCompl.Visible = false; txtSID.Focus();
            }
            reader.Close(); reader.Dispose();
            if (flg == true)
            {
                bindOpn();
                pnlCompl.Visible = true;
                txtDiaryno.Focus();
            }
        con.Close();
        con.Dispose();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            if (txtDiaryno.Text == "")
            {
                lblException.Text = "Fill Details !";
            }
            else
            {
                con.Close(); con.Open();
                cmd = new SqlCommand("update Project set DiaryA=@DiaryA,Option1=@Option1,Option2=@Option2,Option3=@Option3,Remark=@Remarks,Status=@Status where sid='" + txtSID.Text + "' and EntryStatus='Running'", con);
                cmd.Parameters.AddWithValue("@DiaryA", txtDiaryno.Text.ToString());
                cmd.Parameters.AddWithValue("@Option1", ddlOpn1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Option2", ddlOpn2.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Option3", ddlOpn3.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                cmd.Parameters.AddWithValue("@Status", "ProformaASubmitted");
                cmd.ExecuteNonQuery();
                con.Close(); con.Dispose();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Submitted.')", true);
                pnlCompl.Visible = false; txtSID.Text = ""; txtDiaryno.Text = ""; txtRemarks.Text = ""; lblException.Text = ""; txtSID.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Invalid!')", true);
        }
        catch (FormatException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Invalid Date Format!')", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlCompl.Visible = false;
    }
    string strBind;
    private void bindOpn()
    {
        strBind = "select Name from InstitutionReg order by Name";
        adp = new SqlDataAdapter(strBind, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlOpn1.DataSource = dt;
        ddlOpn2.DataSource = dt;
        ddlOpn3.DataSource = dt;
        ddlOpn1.DataValueField = "Name";
        ddlOpn2.DataValueField = "Name";
        ddlOpn3.DataValueField = "Name";
        ddlOpn1.DataBind();
        ddlOpn2.DataBind();
        ddlOpn3.DataBind();
    }
}