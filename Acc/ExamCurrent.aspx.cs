using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;
using System.Data;


public partial class Exam_ExamCurrent : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd; SqlDataAdapter adp;
    ClsECenterCity ecity = new ClsECenterCity();
    ClsStateCity clstate = new ClsStateCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
                invisible.Visible = true; visible.Visible = false;
            }
            if (Request.QueryString["maikal"].ToString() == "")
            {
                invisible.Visible = true;
            }
            else
            {
                lblEnrolment.Text = Request.QueryString["maikal"].ToString();
                invisible.Visible = false;
            }
            if (!IsPostBack)
            {
                //txtYear.Text = DateTime.Now.Year.ToString();
                //maikal dev = new maikal();
                //int se = dev.chksession();
                //if (se == 0) ddlSession.SelectedValue = "Sum";
                //else ddlSession.SelectedValue = "Win";
                //lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                //visisble.Visible = true; invisible.Visible = true;
                //room();
                //txtExamID.Text = idcenter();
                //ddlExamSeason.Focus();
            }
            maikal mk = new maikal();
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (i == 0 | i == 1)
                invisible.Visible = true;
            else if (i == 2)
            {
                invisible.Visible = false;
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
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPart.SelectedValue == "PartI" | ddlPart.SelectedValue == "PartII") { lblStream.Text = "Technician Examination"; lblstreamhidden.Text = "Tech"; }
        if (ddlPart.SelectedValue == "SectionA" | ddlPart.SelectedValue == "SectionB") { lblStream.Text = "Associate Examination"; lblstreamhidden.Text = "Asso"; }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        con.Open();
        ExamCurrent();
        con.Close(); con.Dispose(); txtMembership.ReadOnly = true;
    }
    private void ExamCurrent()
    {
        cmd = new SqlCommand("select * from ExamCurrent where SID='" + txtMembership.Text + "'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            viewdetails.Visible = true; lblerror.Text = "";
            txtIMID.Text = rd["IMID"].ToString(); lblName.Text = rd["SName"].ToString(); ddlCourse.SelectedValue = rd["Course"].ToString(); ddlPart.SelectedValue = rd["Part"].ToString();
            ddlExamStatus.SelectedValue = rd["ExamStatus"].ToString();  ddlCourseStatus.SelectedValue = rd["CourseStatus"].ToString();
            if (rd["Stream"].ToString() == "Tech") lblStream.Text = "Technician"; else if (rd["Stream"].ToString() == "Asso") lblStream.Text = "Associate"; txtCousreID.Text = rd["CourseID"].ToString();
            ddlSession.SelectedValue = rd["Session"].ToString().Substring(0, 3); txtYear.Text = rd["Session"].ToString().Substring(3, 4); lblCourse.Text = rd["Course"].ToString(); lblstreamhidden.Text = rd["Stream"].ToString();
            rd.Close();
            SessionDuration duration = new SessionDuration();
            lblSessionDuration.Text = duration.duration(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString());
            FillGrid();
        }
        else { rd.Close(); viewdetails.Visible = false;lblerror.Text="Membership Not Found"; }
    }
    private void FillGrid()
    {
        adp = new SqlDataAdapter("select SessionID,Course,Part,Average,PaperPass from SFinalPass where SID='" + txtMembership.Text + "'", con);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        grdsFinal.DataSource = ds;
        grdsFinal.DataBind();


        adp = new SqlDataAdapter("select SubID,GetMarks,Status,ExamSeason,ExmpID from SExamMarks where SID='" + txtMembership.Text + "' and Course='" + ddlCourse.SelectedValue + "' and Part='PartI' order by SubID", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        grdPartI.DataSource = dt;
        grdPartI.DataBind();

        adp = new SqlDataAdapter("select SubID,GetMarks,Status,ExamSeason,ExmpID from SExamMarks where SID='" + txtMembership.Text + "' and Course='" + ddlCourse.SelectedValue + "' and Part='PartII' order by SubID", con);
        DataTable dt1 = new DataTable();
        adp.Fill(dt1);
        grdPartII.DataSource = dt1;
        grdPartII.DataBind();

        adp = new SqlDataAdapter("select SubID,GetMarks,Status,ExamSeason,ExmpID from SExamMarks where SID='" + txtMembership.Text + "' and Course='" + ddlCourse.SelectedValue + "' and Part='SectionA' order by SubID", con);
        DataTable dt2 = new DataTable();
        adp.Fill(dt2);
        grdSectionA.DataSource = dt2;
        grdSectionA.DataBind();

        adp = new SqlDataAdapter("select SubID,GetMarks,Status,ExamSeason,ExmpID from SExamMarks where SID='" + txtMembership.Text + "' and Course='" + ddlCourse.SelectedValue + "' and Part='SectionB' order by SubID", con);
        DataTable dt3 = new DataTable();
        adp.Fill(dt3);
        grdSectionB.DataSource = dt3;
        grdSectionB.DataBind();
    }

    protected void grdSectionA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
        }
    }
    protected void grdPartI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
               
        }
    }
    protected void grdPartII_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
        }
    }
    protected void grdSectionB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtMembership.Text != "")
        {
            con.Open();
            cmd = new SqlCommand("select SID from ExamCurrent where SID='"+txtMembership.Text+"'", con);
            string sid = Convert.ToString(cmd.ExecuteScalar());
            if (sid != "")
            {
                lblerror.Text = "";
                cmd = new SqlCommand("update ExamCurrent set IMID=@IMID,Course=@Course,Part=@Part,SessionDuration=@SessionDuration,Session=@Session,ExamStatus=@ExamStatus,Stream=@Stream,CourseID=@CourseID,CourseStatus=@CourseStatus where SID='" + txtMembership.Text + "'", con);
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.Parameters.AddWithValue("@Stream", lblstreamhidden.Text.ToString());
                cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ExamStatus", ddlExamStatus.SelectedValue);
             
                cmd.Parameters.AddWithValue("@Session", ddlSession.SelectedValue + txtYear.Text);
                cmd.Parameters.AddWithValue("@CourseID", txtCousreID.Text);
                cmd.Parameters.AddWithValue("@SessionDuration", lblSessionDuration.Text);
              
                cmd.Parameters.AddWithValue("@CourseStatus", ddlCourseStatus.SelectedValue);
                cmd.ExecuteNonQuery(); lblerror.Text = "Updated Successfully :" + txtMembership.Text; txtMembership.Text = ""; txtMembership.ReadOnly = false; viewdetails.Visible = false;
            }
            else
            {
                lblerror.Text = "Incorrect Membership";
            }
            con.Close();
        }
    }
}
