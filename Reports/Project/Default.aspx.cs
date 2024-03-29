﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Reports_Project_Default : System.Web.UI.Page
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
        Response.Redirect("../../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
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
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
   
    protected void lbtnHome_Click(object sender, EventArgs e)
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

    protected void lbtnRedirectAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
   

    protected void lbtnViewPerort_OnClick(object sender, EventArgs e)
    {
     
        Response.Redirect("DiaryRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
  
    protected void lbtnProject_OnClick(object sender, EventArgs e)
    {
       
        Response.Redirect("ProjectRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnProjectStatus_OnClick(object sender, EventArgs e)
    {
       
        Response.Redirect("ProjectStatusRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnInstituteRe_OnClick(object sender, EventArgs e)
    {
       
        Response.Redirect("InstituteReRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnStudentLetter_Click(object sender, EventArgs e)
    {
    //    string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
    //    Response.Cookies["redi"]["2"] = url.ToString();
    //    Response.Redirect("StudentLetter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnStuRejectedLetter_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("StudentRejectedLetter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");

    }
   
       
    protected void lbtnStuApproved_Click(object sender, EventArgs e)
     {
        Response.Redirect("StudentApprovedLetter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
     }
    protected void lbtnStuAppRemarks_Click(object sender, EventArgs e)
    {
        Response.Redirect("StuAppRemarksLetter.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnProjectDataEntry_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ProjectDetailsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnProjectApprovedAc_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ProjectApprovedRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnIMLetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMLetterRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnAicteLetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("AicteProjectRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnSynopsisApproveRpt_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SynopsisApprovalRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnApproveApproveRpt_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ProjectApproved.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");
    }
    protected void lbtnCopysubmit_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CopySubmitDate.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=PRO");

    }
}