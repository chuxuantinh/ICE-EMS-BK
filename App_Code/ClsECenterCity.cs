using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsECenterCity
/// </summary>
public class ClsECenterCity
{
    public void getItems(DropDownList ddlDown)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
            XmlElement el = doc.DocumentElement;
            XmlNodeList nlist = el.ChildNodes;
            foreach (XmlNode nd in nlist)
            {
                ddlDown.Items.Add(nd.InnerText.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
           
        }
    }
    public void updateCity(string item, string name,string code)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        foreach (XmlNode nd in nlist)
        {
            if (nd.InnerText == item)
            {
                nd.InnerText = name;
                nd.Attributes["Code"].Value = code;
            }
        }
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
    }
    public void deleteCity(string item)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        for (int i = nlist.Count - 1; i >= 0; i--)
        {
            if(nlist[i].InnerText==item)
            nlist[i].ParentNode.RemoveChild(nlist[i]);
        }
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
    }
    public void addCity(string city,string code)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
        XmlNode nd = doc.CreateNode(XmlNodeType.Element, "City", null);
        nd.InnerText = city;
        XmlAttribute at=doc.CreateAttribute("Code");
        nd.Attributes.Append(at);
        nd.Attributes["Code"].Value = code;
        doc.DocumentElement.AppendChild(nd);
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
    }
    public string getCenterCode(string city)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/ExamCenterCity.xml"));
        XmlElement el = doc.DocumentElement;
        string code = "";
        XmlNodeList nlist = el.ChildNodes;
        for (int i = nlist.Count - 1; i >= 0; i--)
        {
            if (nlist[i].InnerText == city)
                code = nlist[i].Attributes["Code"].Value.ToString();
        }
        return code;
    }
}