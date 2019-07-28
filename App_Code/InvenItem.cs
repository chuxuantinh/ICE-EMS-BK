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
/// Summary description for InvenItem
/// </summary>
public class InvenItem
{
    public void getItems(DropDownList ddlDown)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        foreach (XmlNode nd in nlist)
        {
            ddlDown.Items.Add(nd.Attributes["Name"].Value.ToString());
        }
    }
    public string getSellPrice(string item)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        string price = "0";
        foreach (XmlNode nd in nlist)
        {
            if (item == nd.Attributes["Name"].Value.ToString())
            {
                price = nd.Attributes["SellPrice"].Value.ToString();
            }
        }
        return price;
    }
    public string getPurchesPrice(string item)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        string price = "0";
        foreach (XmlNode nd in nlist)
        {
            if (item == nd.Attributes["Name"].Value.ToString())
            {
                price = nd.Attributes["PurchesPrice"].Value.ToString();
            }
        }
        return price;
    }
    public void addItem(string item, string pprice,string sprice)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
        XmlNode nd = doc.CreateNode(XmlNodeType.Element, "Item", null);
        XmlAttribute att = doc.CreateAttribute("Name");
        att.Value = item;
        XmlAttribute att2 = doc.CreateAttribute("PurchesPrice");
        att2.Value = pprice;
        XmlAttribute attSPrice = doc.CreateAttribute("SellPrice");
        attSPrice.Value = sprice;
        nd.Attributes.Append(att); nd.Attributes.Append(att2); nd.Attributes.Append(attSPrice);
        doc.DocumentElement.AppendChild(nd);
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
    }
    public void updateSellPrice(string item, string price)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        foreach (XmlNode nd in nlist)
        {
            if (nd.Attributes["Name"].Value == item)
            {
                nd.Attributes["SellPrice"].Value = price;
            }
        }
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
    }

    public void updatePurchesPrice(string item, string price)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        foreach (XmlNode nd in nlist)
        {
            if (nd.Attributes["Name"].Value == item)
            {
                nd.Attributes["PurchesPrice"].Value = price;
            }
        }
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
    }
    public void deleteItem(string item)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist=el.ChildNodes;
        foreach (XmlNode nd in nlist)
        {
            if (nd.Attributes["Name"].Value == item)
            {
                nd.RemoveAll();
            }
        }
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/InvenItems.xml"));
    }
}