using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Exam_ViewMarksDetails : System.Web.UI.Page
{

    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                    lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    btnApprove.Visible = false; txtStudent.Visible = false;
                    ddlExamSeason.Focus();
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
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");       
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtIMID.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void ddltype_OnselectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlViewType.SelectedValue.ToString() == "All")
        {
            txtStudent.Visible = false;
        }
        else
        {
            txtStudent.Visible = true;
        }
        btnView.Focus();
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
        string query = "";
        if (ddlmarks.SelectedValue.ToString() == "Approved")
        {
            if (ddlViewType.SelectedValue.ToString() == "All")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID=ef.SID and  efs.MarkStatus='Approved' or efs.MarkStatus='Submitted') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID";
            }
            else if (ddlViewType.SelectedValue.ToString() == "IM")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID=ef.SID and  efs.MarkStatus='Approved' or efs.MarkStatus='Submitted'  and efs.IMID='" + txtIMID.Text.ToString() + "') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID and ef.IMID='" + txtIMID.Text.ToString() + "'";
            }
            else if (ddlViewType.SelectedValue.ToString() == "Student")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID='" + txtStudent.Text.ToString() + "' and  efs.MarkStatus='Approved' or efs.MarkStatus='Submitted') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID ";
            }
        }
        else if (ddlmarks.SelectedValue.ToString() == "Submitted")
        {
            if (ddlViewType.SelectedValue.ToString() == "All")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID=ef.SID and  efs.MarkStatus='Submitted') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID";
            }
            else if (ddlViewType.SelectedValue.ToString() == "IM")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID=ef.SID and  efs.MarkStatus='Submitted'  and efs.IMID='" + txtIMID.Text.ToString() + "') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID and ef.IMID='" + txtIMID.Text.ToString() + "'";
            }
            else if (ddlViewType.SelectedValue.ToString() == "Student")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID='" + txtStudent.Text.ToString() + "' and  efs.MarkStatus='Submitted') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID ";
            }
        }
        else if (ddlmarks.SelectedValue.ToString() == "NotSubmitted")
        {
            if (ddlViewType.SelectedValue.ToString() == "All")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID=ef.SID and  efs.MarkStatus='NotSubmitted') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID";
            }
            else if (ddlViewType.SelectedValue.ToString() == "IM")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID=ef.SID and  efs.MarkStatus='NotSubmitted'  and efs.IMID='" + txtIMID.Text.ToString() + "') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID and ef.IMID='" + txtIMID.Text.ToString() + "'";
            }
            else if (ddlViewType.SelectedValue.ToString() == "Student")
            {
                query = "select ef.SID,ef.IMID, std.Name ,ef.Stream,ef.Course,ef.Part,ef.CenterCode,ef.CenterName,ef.RollNo,ef.City from ExamForms ef, Student std where ef.SID  in(select efs.SID from ExamForm efs where efs.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and  efs.SID='" + txtStudent.Text.ToString() + "' and  efs.MarkStatus='NotSubmitted') and ef.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ef.SID=std.SID ";
            }
        }
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridExamForms.DataSource = ds;
        GridExamForms.DataBind();
        btnView.Focus();
    }
    protected void btnViewMarks_Onclick(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select * from SExamMarks where IMID='" + GridExamForms.SelectedRow.Cells[2].Text.ToString() + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and SID='" + GridExamForms.SelectedRow.Cells[1].Text.ToString() + "'", con);
      DataSet ds = new DataSet();
        ad.Fill(ds);
        GridExamForms.DataSource = ds;
        GridExamForms.DataBind();
        btnApprove.Focus();
    }
    protected void GridExamForms_OnSelectedIndexChangd(object sender, EventArgs e)
    {
        GridViewRow rw = GridExamForms.SelectedRow; btnApprove.Visible = true;
     lblFulldName.Text="["+rw.Cells[1].Text.ToString()+"]";
        lblFullCourse.Text = rw.Cells[1].Text.ToString() + ", " + rw.Cells[2].Text.ToString() + "-" + rw.Cells[3].Text.ToString();
    }
    protected void txtIMID_TextChanged(object sender, EventArgs e)
    {
        okk(txtIMID.Text.ToString());
        ddlmarks.Focus();
    }
    private void okk(string strid)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + strid.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        int i = 0;
        if (chk == strid.ToString())
        {
            i += 1;
        }
        else
        {
            txtIMID.Text = "Invalid ID";
            lblExceptionOK.Text = "Please Insert Valid IM ID.";
        }
        if (i == 1)
        {
            lblExceptionOK.Text = "";
            cmd = new SqlCommand("select * from IM where ID='" + strid + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                //lblIMAddress.Text = reader[3].ToString();
                //lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                //lblEnrolment.Text = txtEnrolID.Text;
                //lblGroupID.Text = reader["GID"].ToString();
            }
            reader.Close();
            reader.Dispose();
        }
        con.Close();
        con.Dispose();
    }
    string dt;
    protected void GridExamForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            dt = e.Row.Cells[2].Text.ToString();
        }
        if (dt != "IMID")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
           
        }
    }
}
