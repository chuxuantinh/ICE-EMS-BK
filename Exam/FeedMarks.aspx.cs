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

public partial class Exam_FeedMarks : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ddlCourse.SelectedValue = "Civil";
                ddlPart.SelectedValue = "PartI";
                string qyry = "";
                if (ddlCourse.SelectedValue.ToString() == "Civil")
                {
                    qyry = "select distinct CourseID from CivilSubMaster";
                }
                else if (ddlCourse.SelectedValue.ToString() == "Architecture")
                {
                    qyry = "select distinct CourseID from ArchiSubMaster";
                }
                SqlDataAdapter ad = new SqlDataAdapter(qyry, con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                ddlSyllabus.DataSource = ds;
                ddlSyllabus.DataTextField = "CourseID";
                ddlSyllabus.DataValueField = "CourseID";
                ddlSyllabus.DataBind();
                showcourse();
                details();
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
            Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
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
     lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
     txtYearSeason.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void ddlSyllabus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            cmd = "select * from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            cmd = "select * from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(cmd, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        ddlsubcode.DataSource = dt;
        ddlsubcode.DataValueField = "SubID";
        ddlsubcode.DataTextField = "SubID";
        ddlsubcode.DataBind();
        showcourse();
        ddlCourse.Focus();
    }
    string stcmss;
    private void details()
    {
        con.Close();
        con.Open();
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            stcmss = "select * from CivilSubMaster where SubID='" + ddlsubcode.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            stcmss = "select * from ArchiSubMaster where SubID='" + ddlsubcode.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        SqlCommand cms = new SqlCommand(stcmss, con);
        SqlDataReader rd;
        rd = cms.ExecuteReader();
        while (rd.Read())
        {
            lblSubNamess.Text = rd["SubName"].ToString();
            lblMinMarsk.Text = rd["MinMark"].ToString();
            lblToMarks.Text = rd["MaxMark"].ToString();
            lblFirstMarks.Text = rd["First"].ToString();
        }
        rd.Close();
        rd.Dispose();
        con.Close();
        con.Dispose();
    }
    protected void ddlSubCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        details();
        btnShowEnrolment.Focus();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
        {
            lblStreamCode.Text = "Tech";
            lblStreamName.Text = "Technician Engineering";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
        {
            lblStreamName.Text = "Associate Engineering";
            lblStreamCode.Text = "Asso";
        }
        showcourse();
        ddlsubcode.Focus();
    }
    protected void ddlCourse_SeelctedIndexchanged(object sender, EventArgs e)
    {
        string qyry = "";
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            qyry = "select distinct CourseID from CivilSubMaster";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            qyry = "select distinct CourseID from ArchiSubMaster";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qyry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddlSyllabus.DataSource = ds;
        ddlSyllabus.DataTextField = "CourseID";
        ddlSyllabus.DataValueField = "CourseID";
        ddlSyllabus.DataBind();
        showcourse();
        ddlPart.Focus();
    }
    string cmd;
    private void showcourse()
    {
        try
        {
            if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
            {
                lblStreamCode.Text = "Tech";
                lblStreamName.Text = "Technician Engineering";
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
            {
                lblStreamName.Text = "Associate Engineering";
                lblStreamCode.Text = "Asso";
            }
          
            if (ddlCourse.SelectedValue.ToString() == "Civil")
            {
                cmd = "select * from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='"+ddlSyllabus.SelectedValue.ToString()+"'";
            }
            else if (ddlCourse.SelectedValue.ToString() == "Architecture")
            {
                cmd = "select * from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
            }
            SqlDataAdapter ad = new SqlDataAdapter(cmd, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            ddlsubcode.DataSource = dt;
            ddlsubcode.DataValueField = "SubID";
            ddlsubcode.DataTextField = "SubID";
            ddlsubcode.DataBind();
        }
        catch (SqlException ex)
        {
        }
        finally
        {
        }
    }
    protected void btnShowSubjects_Onclick(object sender, EventArgs e)
    {
        try
        {
            SqlDataAdapter ad = new SqlDataAdapter("select IMID,SID,RollNo from ExamForm where SubID='" + ddlsubcode.SelectedValue.ToString() + "' and RollStatus='AdmitCardGenerated' and ExamSession='" + lblSeasonHidden.Text.ToString() + "' and MarkStatus='NotSubmitted'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            GridMarks.DataSource = ds;
            GridMarks.DataBind();
        }
        catch (SqlException ex)
        {
            lblExceptionShowSubject.Text = ex.ToString();
        }
        if (GridMarks.Rows.Count > 0) GridMarks.Focus(); else btnShowEnrolment.Focus();
    }
    protected void GridMarks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridMarks.PageIndex = e.NewPageIndex;
        GridMarks.DataBind();
    }
    protected void btnSave_ONclick(object sender,EventArgs e)
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            SqlCommand cmd = new SqlCommand();
            GridMarks.AllowPaging = false;
            string strStatus = "";
            con.Close(); con.Open();
            for (int i = 0; i < GridMarks.Rows.Count; i++)
            {
                TextBox box = (TextBox)GridMarks.Rows[i].Cells[3].FindControl("txtObtainMarks");
                if (box.Text != "")
                {
                    try
                    {
                        if (box.Text == "A" | box.Text == "RW")
                        {
                            strStatus = "Fail";
                        }
                        else if (box.Text == "EXMP" | box.Text == "exmp" | box.Text == "Exmp")
                        {
                            strStatus = "Fail";
                        }
                        else if (box.Text == "UFM" | box.Text == "ufm" | box.Text == "Ufm")
                        {
                            strStatus = "Fail";
                        }
                        else if (Convert.ToInt32(box.Text.ToString()) < 50)
                        {
                            strStatus = "Fail";
                        }
                        else if (Convert.ToInt32(box.Text.ToString()) >= 50)
                        {
                            strStatus = "Pass";
                        }
                        if (chkduplicate(GridMarks.Rows[i].Cells[1].Text.ToString(), ddlsubcode.SelectedValue.ToString(), lblSeasonHidden.Text.ToString(), GridMarks.Rows[i].Cells[0].Text.ToString(), GridMarks.Rows[i].Cells[2].Text.ToString(), ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()) == "NO")
                        {
                            string time = atime(GridMarks.Rows[i].Cells[1].Text.ToString(), ddlsubcode.SelectedValue.ToString(), GridMarks.Rows[i].Cells[0].Text.ToString(), ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString());
                            cmd = new SqlCommand("insert into SExamMarks(SID,SubID,SubName,GetMarks,Status,ExamSeason,ATime,IMID,Course,Part,RollNo,Center) values(@SID,@SubID,@SubName,@GetMarks,@Status,@ExamSeason,@ATime,@IMID,@Course,@Part,@RollNo,@Center)", con);
                            cmd.Parameters.AddWithValue("@SID", GridMarks.Rows[i].Cells[1].Text.ToString());
                            cmd.Parameters.AddWithValue("@SubID", ddlsubcode.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SubName", lblSubNamess.Text.ToString());
                            cmd.Parameters.AddWithValue("@GetMarks", box.Text);
                            cmd.Parameters.AddWithValue("@Status", strStatus.ToString());
                            cmd.Parameters.AddWithValue("@ExamSeason", lblSeasonHidden.Text.ToString());
                            if (txtDOB.Text == "") txtDOB.Text = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
                            cmd.Parameters.AddWithValue("@ATime", time.ToString());
                            cmd.Parameters.AddWithValue("@IMID", GridMarks.Rows[i].Cells[0].Text.ToString());
                            cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@RollNo", GridMarks.Rows[i].Cells[2].Text.ToString());
                            cmd.Parameters.AddWithValue("@Center", "");
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("update ExamForm Set MarkStatus='Submitted' where IMID='" + GridMarks.Rows[i].Cells[0].Text.ToString() + "' and SubID='" + ddlsubcode.SelectedValue.ToString() + "' and ExamSession='" + lblSeasonHidden.Text.ToString() + "' and SID='" + GridMarks.Rows[i].Cells[1].Text.ToString() + "' and RollNo='" + GridMarks.Rows[i].Cells[2].Text.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (FormatException ex)
                    {
                        strStatus = "Fail";
                    }
                }
            }
            lblExceptionShowSubject.Text = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Marks Successfully Submitted.')", true);
        }
        catch (SqlException ex)
        {
            lblExceptionShowSubject.Text ="Server Not Responding, Please Refresh Page and try again.";
        }
        catch (Exception ex)
        {
            lblExceptionShowSubject.Text = "Server Not Responding, Please Refresh Page and try again.";
        }
        finally
        {
            con.Close();
            con.Dispose();
            ddlsubcode.Focus();
        }
    }
    private string chkduplicate(string enrol,string subid, string session,string imid,string roll,string course,string part)
    {
        SqlCommand cmd = new SqlCommand("select SubID from SExamMarks where SID='" +enrol.ToString() + "' and  SubID='" +subid.ToString() + "' and ExamSeason='" +session.ToString() + "' and IMID='" + imid.ToString() + "' and Course='" +course.ToString() + "' and Part='" +part.ToString() + "' and RollNo='" +roll.ToString() + "'", con);
        string value = Convert.ToString(cmd.ExecuteScalar());
        if (value == "")
        {
            value = "NO";
        }
        else if (value == ddlsubcode.SelectedValue.ToString())
        {
            value = "YES";
        }
        return value;
    }
    string atme;
    private string atime(string enrol, string subcode, string imid, string course, string part)
    {
        SqlCommand cmd = new SqlCommand("select max(ATime) from SExamMarks where SID='" + enrol.ToString() + "' and SubID='" + subcode.ToString() + "' and Status='Fail'  and IMID='" + imid.ToString() + "' and Course='" + course.ToString() + "' and Part='" + part.ToString() + "'", con);
        string tm = Convert.ToString(cmd.ExecuteScalar());
        if (tm == "")
        {
            tm = "0";
        }
        int im = Convert.ToInt32(tm) + 1;
        return atme = im.ToString();
    }
   
}
