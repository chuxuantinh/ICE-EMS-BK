using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;

/// <summary>
/// Summary description for ClsStateCity
/// </summary>
public class ClsStateCity
{
    public void xmlCity(DropDownList ddlCity, string txtValue, string xml1)
    {
        ddlCity.Items.Clear();
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/Xml/" + xml1));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = el.ChildNodes;
        foreach(XmlNode n in nl)
        {
            if (n.Attributes["name"].InnerText == txtValue)
            {
                XmlNodeList l = n.ChildNodes;
                foreach (XmlNode xn in l)
                {
                    ddlCity.Items.Add(xn.InnerText);
                }
            }
        }
    }
    public void xmlstate(DropDownList ddlState, string xml5)
    {
       ddlState.Items.Clear();
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/Xml/" + xml5));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = el.ChildNodes;
        foreach (XmlNode n in nl)
        {
            ddlState.Items.Add(n.Attributes["name"].InnerText);
        }
    }
}