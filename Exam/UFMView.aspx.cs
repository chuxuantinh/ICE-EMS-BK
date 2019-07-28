using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Exam_UFMView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0) ddlExamSeason.SelectedValue = "Sum";
            else ddlExamSeason.SelectedValue = "Win";
            lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            ddlExamSeason.Focus();
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
            pnlBtn.Visible = true;
            fillCourse(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString(), ddlSyllabus.SelectedValue.ToString());
            fillpart(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString(), ddlSyllabus.SelectedValue.ToString());
            lblStreamHidden.Text = "Tech"; lblStreamName.Text = "Technician Engineering";
            if (ddlSearch.SelectedItem.Text == "Roll No")
            {
                pnlSearch.Visible = true; txtRollNo.Visible = true; txtCenterCode.Visible = false; pnlSubj.Visible = false;
                txtIMID.Visible = false; txtMID.Visible = false; pnlCourse.Visible = false;
            }
            fillgrvUfm();
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
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearch.SelectedItem.Text == "Roll No")
        {
            txtRollNo.Text = "";
            pnlSearch.Visible = true; txtRollNo.Visible = true; txtCenterCode.Visible = false; pnlSubj.Visible = false;
            txtIMID.Visible = false; txtMID.Visible = false; pnlBtn.Visible = true; pnlCourse.Visible = false;
            pnlLvl.Visible = false; ddlSyllabus.Visible = false;
        }
        if (ddlSearch.SelectedItem.Text == "Center Code")
        {
            txtCenterCode.Text = "";
            pnlSearch.Visible = true; txtRollNo.Visible = false; txtCenterCode.Visible = true; pnlSubj.Visible = false;
            txtIMID.Visible = false; txtMID.Visible = false; pnlBtn.Visible = true; pnlCourse.Visible = false;
            pnlLvl.Visible = false; ddlSyllabus.Visible = false;
        }
        if (ddlSearch.SelectedItem.Text == "IMID")
        {
            txtIMID.Text = "";
            pnlSearch.Visible = true; txtRollNo.Visible = false; txtCenterCode.Visible = false; txtIMID.Visible = true;
            txtMID.Visible = false; pnlBtn.Visible = true; pnlCourse.Visible = false; pnlSubj.Visible = false;
            pnlLvl.Visible = false; ddlSyllabus.Visible = false;
        }
        if (ddlSearch.SelectedItem.Text == "Membership ID")
        {
            txtMID.Text = "";
            pnlSearch.Visible = true; txtRollNo.Visible = false; txtCenterCode.Visible = false; pnlSubj.Visible = false;
            txtIMID.Visible = false; txtMID.Visible = true; pnlBtn.Visible = true; pnlCourse.Visible = false;
            pnlLvl.Visible = false; ddlSyllabus.Visible = false;
        }
        if (ddlSearch.SelectedItem.Text == "Course")
        {
            pnlSearch.Visible = false; pnlCourse.Visible = true; pnlSubj.Visible = false;
            txtRollNo.Visible = false; txtCenterCode.Visible = false; txtIMID.Visible = false;
            txtMID.Visible = false; pnlBtn.Visible = true;
            pnlLvl.Visible = true; ddlSyllabus.Visible = true;
        }
        if (ddlSearch.SelectedItem.Text == "Subject")
        {
            pnlSearch.Visible = false; pnlSubj.Visible = true; pnlCourse.Visible = true; txtRollNo.Visible = false;
            txtCenterCode.Visible = false; txtIMID.Visible = false; txtMID.Visible = false; pnlBtn.Visible = true;
            pnlLvl.Visible = true; ddlSyllabus.Visible = true;
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
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
        fillCourse(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString(), ddlSyllabus.SelectedValue.ToString());
    }
    private void fillCourse(string course, string part,string courseid)
    {
        string qry = "";
        if (course == "Civil")
        {
            qry = "select distinct SubID from CivilSubMaster where CourseID='" + courseid + "'";
        }
        else if (course == "Architecture")
        {
            qry = "select distinct SubID from ArchiSubMaster where CourseID='" + courseid + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        ddlSub.DataSource = dt;
        ddlSub.DataValueField = "SubID";
        ddlSub.DataTextField = "SubID";
        ddlSub.DataBind();
    }
    protected void ddlPart_SelectedIndexChanged1(object sender, EventArgs e)
    {
        fillCourse(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString(), ddlSyllabus.SelectedValue.ToString());
        fillpart(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString(), ddlSyllabus.SelectedValue.ToString());
    }
    private void fillpart(string course, string part, string courseid)
    {
        string qry = "";
        if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
        {
            qry = "select distinct SubID from CivilSubMaster where CourseID='" + courseid + "'";
            pnlCourse.Visible = true;
            lblStreamName.Text = "Technician Engineering";
            lblStreamHidden.Text = "Tech";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
        {
            qry = "select distinct SubID from ArchiSubMaster where CourseID='" + courseid + "'";
            pnlCourse.Visible = true;
            lblStreamHidden.Text = "Asso";
            lblStreamName.Text = "Associate Engineering";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        ddlSub.DataSource = dt;
        ddlSub.DataValueField = "SubID";
        ddlSub.DataTextField = "SubID";
        ddlSub.DataBind();
    }
    private void fillgrvUfm()
    {
        string strfillgrvUfm = "select RollNo,Course,Part, SubID, SubName, ExamDate,Details,Status from ExamUFM where Session='" + lblHiddenSeason.Text.ToString() + "'";
        SqlDataAdapter adp = new SqlDataAdapter(strfillgrvUfm, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        grvUfm.DataSource = dt;
        grvUfm.DataBind();
    }
    private void fillgrv()
    {
        string qry = "";
        if (ddlSearch.SelectedItem.Text == "Roll No")
        {
            qry = "select RollNo,Course,Part, SubID, SubName, ExamDate,CenterCode,CenterName,Details,Status from ExamUFM where RollNo='" + txtRollNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        if (ddlSearch.SelectedItem.Text == "Center Code")
        {
            qry = "select Course,Part, SubID, SubName, ExamDate,CenterCode,CenterName,Details,Status from ExamUFM where CenterCode='" + txtCenterCode.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        if (ddlSearch.SelectedItem.Text == "IMID")
        {
            qry = "select SID,Course,Part, SubID, SubName, ExamDate,CenterCode,CenterName,Shift,Details,Status from ExamUFM where SID in (select SID from Student where IMID='" + txtIMID.Text.ToString() + "') and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        if (ddlSearch.SelectedItem.Text == "Membership ID")
        {
            qry = "select SID,Course,Part, SubID, SubName, ExamDate,CenterCode,CenterName,Shift,Details,Status from ExamUFM where SID='" + txtMID.Text.ToString() + "'";
        }
        if (ddlSearch.SelectedItem.Text == "Course")
        {
            qry = "select Course,Part,ExamDate,CenterCode,CenterName,Details,Status from ExamUFM where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        if (ddlSearch.SelectedItem.Text == "Subject")
        {
            qry = "select Course,Part, SubID, SubName, ExamDate,CenterCode,CenterName,Shift,Details,Status from ExamUFM where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and SubID='" + ddlSub.SelectedItem.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        SqlDataAdapter adp = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        grvUfm.DataSource = ds;
        grvUfm.DataBind();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        fillgrv();
    }
    protected void ddlSyllabus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCourse(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString(), ddlSyllabus.SelectedValue.ToString());
    }
}