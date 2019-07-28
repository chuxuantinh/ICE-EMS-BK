using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Globalization;

public partial class Admission_EditITIForms : System.Web.UI.Page
{
    SqlDataAdapter adp, adapter;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    ddlCourse.Items.Add("Cancel");
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  grviti.Visible = false;
        ddlCourse.Items.Clear();
        if (ddlStatus.SelectedItem.Text == "Approved")
            ddlCourse.Items.Add("Cancel");
        else if (ddlStatus.SelectedItem.Text == "ReadyForExam")
        {
            ddlCourse.Items.Add("Approved");
            ddlCourse.Items.Add("Cancel");
        }
        else if (ddlStatus.SelectedItem.Text == "RollNoGenerated")
        {
            ddlCourse.Items.Add("Approved");
            ddlCourse.Items.Add("ReadyForExam");
            ddlCourse.Items.Add("Cancel");            
        }
        else if(ddlStatus.SelectedItem.Text == "Qualified")
        {
            ddlCourse.Items.Add("DisQualify");
            ddlCourse.Items.Add("Cancel");
        }
        else if(ddlStatus.SelectedItem.Text == "Disqualify")
        {
            ddlCourse.Items.Add("Qualified");
            ddlCourse.Items.Add("Cancel");
        }
        else if (ddlStatus.SelectedItem.Text == "Cancel")
        {
            ddlCourse.Items.Add("Approved");
        }
    }
    protected DataTable disp()
    {
        adp = new SqlDataAdapter("select SNO, Name,FName,SID,IMID,AppNo,Stream,Course,Part,DOB,Session,SubDate, Amount,Status,ExamDate, RollNo,Password from ITIForm where Status='" + ddlStatus.SelectedValue + "' and Session='"+lblSeasonHidden.Text.ToString()+"' order by convert(int, AppNo)", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    protected void grviti_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grviti.PageIndex = e.NewPageIndex;
        grviti.DataSource = disp();
        grviti.DataBind();
    }
    int flag = 0;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        lblStatus.Text = "";
        try
        {
            lblStatus.Text = "";
            con.Open();
            string pwd;
            SqlCommand cmd;
            for (int i = 0; i < grviti.Rows.Count; i++)
            {
                string a = (string)grviti.DataKeys[i][0];
                CheckBox cd = (CheckBox)grviti.Rows[i].FindControl("chkStatus");
                if (cd.Checked)
                {
                    flag = 1;
                    pwd = grviti.Rows[i].Cells[2].Text.ToString();
                    pwd = pwd.Substring(0, 2);
                    string strqry = "select max(RollNo) from ITIForm where Session='" + grviti.Rows[i].Cells[11].Text.ToString() + "'";
                    cmd = new SqlCommand(strqry, con);
                    string str = Convert.ToString(cmd.ExecuteScalar());
                    if (ddlStatus.SelectedItem.Text == "RollNoGenerated")
                    {                     
                            cmd = new SqlCommand("update ITIForm set RollNo=@RollNo, Status=@Status  where SID=@SID", con);
                            cmd.Parameters.AddWithValue("@RollNo", 1000);                           
                            cmd.Parameters.AddWithValue("@Status", ddlCourse.SelectedValue);
                            cmd.Parameters.AddWithValue("@SID", grviti.Rows[i].Cells[4].Text.ToString());
                            cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("update ITIForm set Status=@Status  where SID=@SID", con);
                        cmd.Parameters.AddWithValue("@Status", ddlCourse.SelectedValue);
                        cmd.Parameters.AddWithValue("@SID", grviti.Rows[i].Cells[4].Text.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
                if (flag == 1)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('successfully Updated')", true);
                if (flag == 0)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select atleast One Record')", true);
            }
            grviti.DataSource = disp();
            grviti.DataBind();
        }
        catch (SqlException ex)
        {
            lblStatus.Text = ex.ToString();
        }
        catch (System.FormatException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Wrong Date Format')", true);
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void grviti_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[12].Text = Convert.ToDateTime(e.Row.Cells[12].Text).ToString("dd/MM/yyyy");
            if (ddlStatus.SelectedValue == "RollNoGenerated")
                e.Row.Cells[15].Text = Convert.ToDateTime(e.Row.Cells[15].Text).ToString("dd/MM/yyyy");
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        grviti.DataSource = disp();
        grviti.DataBind();
        grviti.Visible = true;
    }
}