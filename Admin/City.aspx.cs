using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_Default2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    ClsStateCity state = new ClsStateCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
            }
            if (!IsPostBack)
            {
                state.xmlstate(ddlState, "XMLState.xml");
                state.xmlCity(ddlcity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
                ddlState.Focus();
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
    protected void btnAddd_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/XMLState.xml"));
        string x = ddlState.SelectedItem.Text.ToString();
        XmlNode xmlNodeSettings = xmlDoc.SelectSingleNode("states/State[@name='"+x+"']");
        XmlElement xmlNodeCustomSettings = xmlDoc.CreateElement("city");
        xmlNodeCustomSettings.InnerText = Txtcity.Text.ToString();
        xmlNodeSettings.AppendChild(xmlNodeCustomSettings); 
        xmlDoc.Save(MapPath("~/Xml/XMLState.xml"));
        Label5.Text = "City Added Successfully.!";
        ddlcity.Focus();
    }
    protected void btnEditt_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/XMLState.xml"));
        XmlElement elmRoot = xmlDoc.DocumentElement;
        XmlNodeList lstVideos = xmlDoc.GetElementsByTagName("State");
        foreach (XmlNode node in lstVideos)
        {
            XmlNodeList lstChildren = node.ChildNodes;
            foreach (XmlNode dir in lstChildren)
            {
                if (dir.InnerText == ddlcity.SelectedValue)
                {
                    dir.InnerText = Txtcity.Text;
                    xmlDoc.Save(MapPath("~/Xml/XMLState.xml"));
                }
            }
        }
        Label5.Text = "City Updated Successfully.!";
        ddlcity.Focus();
    }
    protected void btnDeletee_Click(object sender, EventArgs e)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(MapPath("~/Xml/XMLState.xml"));
            XmlElement elmRoot = xmlDoc.DocumentElement;
            XmlNodeList lstVideos = xmlDoc.GetElementsByTagName("city");
            foreach (XmlNode node in lstVideos)
            {
                XmlNodeList lstChildren = node.ChildNodes;
                foreach (XmlNode dir in lstChildren)
                {
                    if (dir.InnerText == ddlcity.SelectedValue)
                    {
                        node.ParentNode.RemoveChild(dir.ParentNode);
                        xmlDoc.Save(MapPath("~/Xml/XMLState.xml"));
                        Label5.Text = "City Deleted Successfully.!";
                        ddlcity.Focus();
                    }
                }
            }
        }
        catch (InvalidOperationException ex)
        { }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label5.Text = ""; state.xmlCity(ddlcity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
    }
    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label5.Text = "";
        Txtcity.Text = ddlcity.SelectedItem.Text.ToString();
    }
}