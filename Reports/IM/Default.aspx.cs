using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Reports_IM_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
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
               try
                {
                   
                    con.Open();
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);

                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                      int  lvl = Convert.ToInt32(reader[20].ToString());
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
            reader.Close();
            reader.Dispose();
            con.Close();
            con.Dispose();
        }
    
    }


   
    protected void lbtnIM_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("IMProfileRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=IM");
    }
    protected void lbtnSubscription_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("SubscriptionRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=IM");
    }
    protected void lkbtnStudentAccount_click(object sender, EventArgs e)
    {

    }
    
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ReportDefault.aspx?name=" + Request.QueryString["maikal"] + "&lnk=null&typ=IM");
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
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
 

   
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {

    }
}