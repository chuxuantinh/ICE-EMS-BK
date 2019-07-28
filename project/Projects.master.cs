using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class project_Projects : System.Web.UI.MasterPage
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
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
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
                            lblWelcome.Text = "Administrator";
                        else if (lvl == 1)
                            lblWelcome.Text = "Admin";
                        else if (lvl == 2)
                        {
                            lblWelcome.Text = "User ID";
                            panelAICTEIns.Visible = false; panelEvaluate.Visible = false; panelApprove.Visible = true;
                            tdNewUser.Visible = false; tdAICTE.Visible = false; tdA.Visible = false; tdB.Visible = false; tdC.Visible = false;
                            if (reader["ProSynopsis"].ToString() == "Sys")
                            {
                                tdA.Visible = true;
                            }
                            if (reader["ProApprove"].ToString() == "ProApp")
                            {
                                tdB.Visible = true;
                            }
                            if (reader["ProSubmit"].ToString() == "ProSub")
                            {
                            }
                            if (reader["ProEvaluate"].ToString() == "ProEva")
                            {
                                tdC.Visible = true;
                                panelEvaluate.Visible = true;
                            }
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ibtnHome_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login.aspx");
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void ibtnAICTE_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("InstitutionRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void btnManageInsFees_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageFees.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnkProDiary_Click(object sender, EventArgs e)
    {
        Response.Redirect("DiaryReceive.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnkProjectAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectAccount.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void ibtnSynopsis_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SubmitProformaA.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void ibtnApprove_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ApprovePerfA.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void ibtnProEvalute_Onclick(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("projectEval.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void btnRegisterNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitutionRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void btnIMHaed_Click1(object sender, EventArgs e)
    {
        Response.Redirect("InstitutionChange.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnEvalutionView_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewProject.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnManageTitle_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ProjectTitle.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnManagePProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("Allocateproject.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnProjectCopyDispatch_Click(Object sender, EventArgs e)
    {
        Response.Redirect("ProCopyDispatch.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnViewProfile_Click1(object sender, EventArgs e)
    {
        Response.Redirect("projectEval.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void btnBuildinfo_Click1(object sender, EventArgs e)
    {
        Response.Redirect("InstitutionView.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnBuildingManage_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectEntry.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnkSubmitPerFA_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubmitProformaA.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnlApprovePerfA_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovePerfA.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateStudList.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnProformaB_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubmitProfB.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnProformaBApp_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveProfB.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["dev"]);
    }
    protected void lnlProGrad_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectGrade.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void lnlUpdProStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdProStatus.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
    protected void ibtnD_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/Project/Default.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro&lvl=&id=");
    }
}
