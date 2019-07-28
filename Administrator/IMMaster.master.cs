using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class Administrator_IMMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            lbtnUserName.Text = Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString();
            lblWelcome.Text = "Welcome";
            SqlDataReader reader; con.Close(); con.Open();
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
                    usermanage.Visible = false;
                    panelAdminManage.Visible = false;
                }
            }
            reader.Close();
            if (!IsPostBack)
            {
            }
        }
        catch (NullReferenceException ex)
        {
            lblWelcome.Text = "Session End, Please Login again.";
            Response.Redirect("../accountDepart.aspx?&lnk=null&typ=Ac");
        }
        finally
        {
            con.Close();
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
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void btnRegisterNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    protected void btnIMHaed_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("IMHead.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void btnBuildinfo_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("IMBuilding.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void lbtnViewProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMProfile.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnHeadofIM_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("IMHeadView.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void lbtnBuildingInfo_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("BuildingView.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void lbtnReport_Click(object sender, EventArgs e)
    {
    }
    // Manage Profile 
    protected void lbtnMangeIMHead_OnClick(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("IMHead.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void lbtnChangeImage_OnClick(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
            Response.Redirect("IMHead.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnUpdatedate_OnClick(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
            Response.Redirect("UpdateDates.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnMangeProfile_Onclick(object sendr, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("ImRegiUpdate.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void lbtnMangeBuilding_Onclick(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("Buildingupdate.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void lbtnChangeStatus_Onclick(object sendr, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("IMStatus.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void lbtnViewProfileImage_Onclick(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("IMHeadView.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void btnInfection_Click(object seder, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "")
        {
        }
        else
        {
            Response.Redirect("IMInspection.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
        }
    }
    protected void txtIMID_TextChaned(object sender, EventArgs e)
    {
    }
    /*Create New User*/
    protected void btnManageAdmin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    /*New Member Registration*/
    protected void ibtnRegisterMem_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/Register.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&" + Request.QueryString["lvl"] + "=zero");
    }
    /*Im inspection*/
    protected void imgbtnManageMembership_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("IMInspection.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*IM Subscription*/
    protected void btnSubscription_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/Subscription.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*View Profile*/
    protected void ibtnvewProfile_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/ViewAppProfiles.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*View Reports*/
    protected void ibtnReports_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/IM/Default.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    protected void ibtnCertificate_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/Certificate.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
}
