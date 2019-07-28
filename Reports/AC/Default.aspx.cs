using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Reports_AC_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlDataReader reader;
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
            if (!IsPostBack)
            {
                showimg();
                try
                {
                    con.Close(); con.Open();
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
                            if (Request.QueryString["typ"] == "IM")
                            {
                            }
                        }
                    }
                    reader.Close();
                    reader.Dispose();
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
      }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
        finally
        {
        }
    }
    public void showimg()
    {
        if (Request.QueryString["lnk"] == "create")
        {
            imgbtnCreate.ImageUrl = "~/images/createcolor.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "update")
        {
            imgbtnRecover.ImageUrl = "~/images/user_update.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "delete")
        {
            imgbtnDelete.ImageUrl = "~/images/user_delete.png";
        }
    }
    protected void lbtnDDDateAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DDDateRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnDDAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MainACRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AccountRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnMSAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MemberFees.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnAppApproveAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ACAppApproveRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnConsolidateRptAcc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ConsolidatedAmtRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnAllExportAcc_OnClick(object sender,EventArgs e)
    {
        Response.Redirect("AllExport.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnExamBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamBillRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnApplicationStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApplicationStatusReportRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void ibtnHOme_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
         try
        {
        maikal mk = new maikal();
        int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
        if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
        {
            Response.Redirect("../../Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
        }
        else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
        {
            Response.Redirect("../../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        else
        {
        }
        }
         catch (NullReferenceException ex)
         {
             Response.Redirect("../../Login.aspx");
         }
    }

    protected void lbtnApplicationStatusSum_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApplicationStatusSum.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
    protected void lbtnApplicationStatusCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApplicationStatusCourseRpt.aspx?maikal="+Request.QueryString["maikal"]+"&lnk=null&typ=AC");
    }
    protected void lbtnFormType_Click(object sender, EventArgs e)
    {
        Response.Redirect("FormTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");

    }
    protected void lblDebitNot_Click(object sender, EventArgs e)
    {
        Response.Redirect("DebitNoteRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=AC");
    }
}