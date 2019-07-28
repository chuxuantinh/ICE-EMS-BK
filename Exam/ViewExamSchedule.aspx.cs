using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Exam_ViewExamSchedule : System.Web.UI.Page
{
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
                 lblStreamHidden.Text = "Tech"; lblStreamName.Text = "Technician Engineering";
               txtyear.Text = DateTime.Now.Year.ToString();
                lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
                string qry = "";
                ddlCourse.SelectedValue = "Civil";
                if (ddlCourse.SelectedValue.ToString() == "Civil")
                {
                    qry = "select Distinct CourseID from CivilSubMaster";
                }
                else if (ddlCourse.SelectedValue.ToString() == "Architecture")
                {
                    qry = "select Distinct CourseID from ArchiSubMaster";
                }
                SqlDataAdapter ad = new SqlDataAdapter(qry, con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                ddlSyllabus.DataSource = ds;
                ddlSyllabus.DataTextField = "CourseID";
                ddlSyllabus.DataValueField = "CourseID";
                ddlSyllabus.DataBind();
                FeeMaster fm = new FeeMaster();
                ddlSyllabus.SelectedValue = fm.currentCourse(ddlCourse.SelectedValue.ToString());
                ddlType.Focus();
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
    protected void txtyear_TextChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
        ddlCourse.Focus();
    }
    protected void ddlSyllabus_OnslelectdIndexChanged(object sender, EventArgs e)
    {
        GridView3.DataBind();
        ddlExamSeason.Focus();
    }
    protected void ddlCourse_OnTextChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            qry = "select Distinct CourseID from CivilSubMaster";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            qry = "select Distinct CourseID from ArchiSubMaster";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddlSyllabus.DataSource = ds;
        ddlSyllabus.DataTextField = "CourseID";
        ddlSyllabus.DataValueField = "CourseID";
        ddlSyllabus.DataBind();
        ddlPart.Focus();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
        {
            lblStreamName.Text = "Technician Engineering";
            lblStreamHidden.Text = "Tech";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
        {
            lblStreamHidden.Text = "Asso";
            lblStreamName.Text = "Associate Engineering";
        }
        btnShow.Focus();
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btmShow_Grid(object sender, EventArgs e)
    {
        GridView3.DataBind();
        GridView3.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
        txtyear.Focus();
    }
}