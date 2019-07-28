using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for FMarksheet
/// </summary>
public class FMarksheet:FeeMaster
{
	public FMarksheet()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string studentduration(string oldssn, string current)
    {
        float time = 0F;
        string oid = sessionid(oldssn);
        string cid = sessionid(current);
        //string oid = "101";
        //string cid = "122";
        if ((cid.Substring(2, 1) == "2") & (oid.Substring(2, 1) == "1"))
        {
            time = float.Parse(cid.Substring(0, 2)) - float.Parse(oid.Substring(0, 2));
            time = time + 1.0F;
        }
        else if (cid.Substring(2, 1) == oid.Substring(2, 1))
        {
            time = float.Parse(cid.Substring(0, 2)) - float.Parse(oid.Substring(0, 2));
            time = time + 0.5F;
        }
        else
        {
            time = float.Parse(cid.Substring(0, 2)) - float.Parse(oid.Substring(0, 2));
        }
        return time.ToString();
    }

    public string PartIIStudent(string sid)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/PartIIStudent.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = el.ChildNodes;
        string sidd = "";
        foreach (XmlNode nd in nl)
        {
            if ((nd.Attributes["Name"].InnerText == sid) && (nd.Attributes["Status"].InnerText=="NotSubmitted"))
            {
                sidd = sid;
            }
        }
        return sidd;
    }
    public void PartIIExamUpdate(string sid)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/PartIIStudent.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = el.ChildNodes;
        foreach (XmlNode nd in nl)
        {
            if ((nd.Attributes["Name"].InnerText == sid) && (nd.Attributes["Status"].InnerText == "NotSubmitted"))
            {
                nd.Attributes["Status"].InnerText = "Submitted";
            }
        }
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/PartIIStudent.xml"));
    }
}
