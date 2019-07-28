using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Reports_FO_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    public static int lvl;
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
              //  showimg();
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
                        lvl = Convert.ToInt32(reader[20].ToString());
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
                            // Response.Redirect("Admin/SuperAdminManage.aspx");
                            lblWelcome.Text = "User ID";
                            panelHeader.Visible = false;
                            if (Request.QueryString["typ"] == "FO")
                            {

                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
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
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
         //Session.Remove("admin");
        Response.Redirect("../../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
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
            // Response.Redirect("Admin/SuperAdminManage.aspx");
            
            Response.Redirect("../../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        //imgbtnCreate.ImageUrl = "~/images/createtrans.png";
        if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
        {
            Response.Redirect("../../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
        }
        else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
        {
            Response.Redirect("../../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        else
        {
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
        {
            Response.Redirect("../../Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
        }
        else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
        {
            Response.Redirect("../../User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        else
        {
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        
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
   
    protected void lbtnHome_Click(object sender, EventArgs e)
    {
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

    protected void lbtnRedirectAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name="+Request.QueryString["maikal"]+"&lnk=null&typ=FO");
    }

   protected void lbtnD2DRpt_Click(object sender,EventArgs e)
    {
       string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("D2DRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnViewPerort_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("DiaryRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnDiaryTypeRpt_click(object sender,EventArgs e)
    {
         string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("DiaryTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbttCourierRpt_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("CourierRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnD2DReport_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("D2DRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCounsellingReport_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("CounsellingRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnVisitorsReport_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("VisitorsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnDiaryLetterRpt_Click(object sender, EventArgs e)
    {
         string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("DiaryLetterRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCourierServiceRpt_click(object sender, EventArgs e)
    {
         string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("CourierServiceRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnDiaryStatusRpt_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("DiaryStatusRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnMemberType_Click(object sender,EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("MemberTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnForm_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("FormOnHold.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
        
    }
}