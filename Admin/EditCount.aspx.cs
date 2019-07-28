using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_EditCount : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    
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

                xml();
               ddlEdit.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    private void xml()
    {
        ddlEdit.Items.Clear();
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/Xml/" + "EditCount.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = el.ChildNodes;
        foreach (XmlNode n in nl)
        {
            ddlEdit.Items.Add(n.Name);
            if (ddlEdit.SelectedItem.ToString() == n.Name.ToString())
            {
                lblCount.Text = n.Attributes["Counter"].Value.ToString();
                lblTotal.Text = n.Attributes["Total"].Value.ToString();
            }
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
    protected void ddlEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCount.Text = "";
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/Xml/EditCount.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = el.ChildNodes;
        foreach (XmlNode n in nl)
        {
    
            if (ddlEdit.SelectedItem.ToString() == n.Name.ToString())
            {
                lblCount.Text = n.Attributes["Counter"].Value.ToString();
                lblTotal.Text = n.Attributes["Total"].Value.ToString();
            }

        }
    }
    int count, total;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtCount.Text != "")
        {
            Label5.Text = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(MapPath("~/Xml/EditCount.xml"));
            XmlElement elmRoot = xmlDoc.DocumentElement;
            XmlNodeList lstnode = elmRoot.ChildNodes;
            foreach (XmlNode node in lstnode)
            {
                if (node.Name == ddlEdit.SelectedValue)
                {
                    count = Convert.ToInt32(lblCount.Text) + Convert.ToInt32(txtCount.Text);
                    total = Convert.ToInt32(lblTotal.Text) + Convert.ToInt32(txtCount.Text);
                    if (count >= 0 && total >= 0)
                    {
                        node.Attributes["Counter"].Value = count.ToString();
                        node.Attributes["Total"].Value = total.ToString();
                        xmlDoc.Save(MapPath("~/Xml/EditCount.xml"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Sucessfully Updated')", true);
                    }
                    else
                    {
                        Label5.Text = "Counter Can't be less than 0";
                    }

                }
            }
            lblCount.Text = count.ToString();
            lblTotal.Text = total.ToString();
            xml(); txtCount.Text = "";
            ddlEdit.Focus();
        }
        else
        {
            Label5.Text = "Enter Counter Value";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Label5.Text = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/EditCount.xml"));
        XmlElement elmRoot = xmlDoc.DocumentElement;
        XmlNodeList lstnode = elmRoot.ChildNodes;
        foreach (XmlNode node in lstnode)
        {
            if (node.Name == ddlEdit.SelectedValue)
            {
                node.Attributes["Counter"].Value = "0";
                node.Attributes["Total"].Value = "0";
                xmlDoc.Save(MapPath("~/Xml/EditCount.xml"));
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Values Reset')", true);
        lblCount.Text = "0"; lblTotal.Text = "0"; xml();
        ddlEdit.Focus();
    }
}