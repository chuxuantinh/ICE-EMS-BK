using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Reports_Student_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            else
            {
                try
                {
                    SqlDataReader reader;
                    con.Open();
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                        int lvl = Convert.ToInt32(reader[20].ToString());
                        if (lvl == 0)
                        {
                            lblWelcome.Text = "Administrator";
                        }
                        else if (lvl == 1)
                        {
                            lblWelcome.Text = "Admin";
                        }
                        else if (lvl == 2)
                        {
                            lblWelcome.Text = "User ID";
                            panelHeader.Visible = false;
                            if (Request.QueryString["typ"] == "AD")
                            {
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
                }
                finally
                {
                  
                    con.Close();
                    con.Dispose();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
        finally
        {
        }
    }
    protected void lbtnGenMembership_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("StudentDetailsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnCourse_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CourseRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnStudentProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("Student.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lkbtnStudentAccount_click(object sender, EventArgs e)
    {
        Response.Redirect("StudentaccountRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
            if (lvl == 0)
            {
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                Response.Redirect("../../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
           catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void ibtnHome_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
            if (lvl == 0)
            {
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                Response.Redirect("../../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }

    protected void lbtnITIForms_click(object sender, EventArgs e)
    {
        Response.Redirect("ITIFormrpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnITILetters_click(object sender, EventArgs e)
    {
        Response.Redirect("ITILetters.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");

    }
    protected void lbtnITIExam_click(object sender, EventArgs e)
    {
        Response.Redirect("ITIExamrpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");

    }
    protected void lbtnITIResult_click(object sender, EventArgs e)
    {
        Response.Redirect("ITIResult.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnMembershipGtd_click(object sender, EventArgs e)
    {
        Response.Redirect("StuMembershipGtd.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnStudentRemark_click(object sender, EventArgs e)
    {
        Response.Redirect("StudentRemarks.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lbtnReAdmission_click(object sender, EventArgs e)
    {
        Response.Redirect("ReAdmissionRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
    protected void lblStuExp_Click(object sender, EventArgs e)
    {
        Response.Redirect("StuExpRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AD");
    }
}