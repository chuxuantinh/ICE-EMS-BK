using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Administrator_Membership : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /*Im Membership*/
    protected void ibtnmembership_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/Register.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&" + Request.QueryString["lvl"] + "=zero");
    }
    /*Im Inspection*/
    protected void ibtnInspection_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("IMInspection.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*Subscription*/
    protected void ibtnSubscription_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/Subscription.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*View Profile*/
    protected void ibtnViewProfile_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/ViewAppProfiles.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Mslvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*Certificates*/
    protected void ibtnCertificate_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/Certificate.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    /*Imm Reports*/
    protected void ibtnReportss_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/IM/Default.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Mslvl=" + Request.QueryString["lvl"] + "&id=");
    }
}