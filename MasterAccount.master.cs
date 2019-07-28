using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterAccount : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
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
                showimg();
                try
                {
                    if (!IsPostBack)
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
                                    panelD2D.Visible = false; panelDiaryEntry.Visible = false; panelCounselling.Visible = false;
                                    lbtnCourierDispatch.Enabled = false; lbtncourierDptView.Enabled = false; lbtnCourierRpt.Enabled = false;
                                    lbtncouriere.Enabled = false; lbtnEditCourier.Enabled = false; lbtnsearchc.Enabled = false; 
                                    if (reader["FOffice"].ToString() == "FO")
                                        panelCounselling.Visible = true;
                                    if (reader["Enquiry"].ToString() == "Enq") {
                                        panelDiaryEntry.Visible = true;
                                        lbtncouriere.Enabled = true;
                                        lbtnsearchc.Enabled = true;
                                        lbtnEditCourier.Enabled = true;
                                    }     
                                    if (reader["Courier"].ToString() == "Cou") { 
                                        panelCounselling.Visible = true; 
                                        lbtnCourierDispatch.Enabled = true; 
                                        lbtncourierDptView.Enabled = true; 
                                        lbtnCourierRpt.Enabled = true;
                                    }
                                    if (reader["D2D"].ToString() == "D2D") { panelD2D.Visible = true; }
                                }
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        con.Close();
                        con.Dispose();
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
            Response.Redirect("Login.aspx");
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" +Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void ibtnAdmin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ibtnFrontOffice_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void lbtnDiaryCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("../FO/DiaryCount.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnEditCourier_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/EditCourier.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnfrontoff_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/frontOffceHome.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnVisitorsView_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/VisitorView.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtncouriere_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/CourierHome.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    
    protected void lbtnsearchc_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/ViewDiaryEntry.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCourierDepartment_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/ViewCourierDetail.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCourierDispatchView_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/ViewCourierDispatch.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtncounseling_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/Counseling.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtncounselingView_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/CounselingView.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtncounselingFollow_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/CounselingFollow.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCourierDispatch_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/DiaryEntry.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCourierSupply_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/CourierSuply.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnViewPerort_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../Reports/FO/DiaryTypeRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbttCourierRpt_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../Reports/FO/CourierRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnD2DReport_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../Reports/FO/D2DRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnCounsellingReport_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../Reports/FO/CounsellingRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnVisitorsReport_OnClick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../Reports/FO/VisitorsRpt.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/FO/Default.aspx?maikal="+Request.QueryString["maikal"]+"&lnk=rpt&lvl=zero&typ=Admin");
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());

            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            {
                Response.Redirect("../User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());

            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            {
                Response.Redirect("../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
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
    protected void lbtnCourierDispatchEdit_Onclick(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/EditCourierDispatch.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");

    }
    protected void lbtnEditCourierSupply_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Cookies["redi"]["2"] = url.ToString();
        Response.Redirect("../FO/EditCourierSupply.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["maikal"]);
    }
}
