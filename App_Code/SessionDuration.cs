using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for SessionDuration
/// </summary>
public class SessionDuration
{
	public SessionDuration()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string x;
    public string duration(string course, string part)
    {
        XmlDocument xdoc = new XmlDocument();
        string rtn = null;
        string file = HttpContext.Current.Server.MapPath("../Exam/SessionDuration.xml");
        if (File.Exists(file))
        {
            xdoc.Load(file);
            XmlElement xelm = xdoc.DocumentElement;
            XmlNodeList xnode = xelm.ChildNodes;
            foreach (XmlNode xn in xnode)
            {
                if (xn.Name == course)
                {
                    xnode = xn.ChildNodes;
                    foreach (XmlNode x in xnode)
                    {
                        if (x.Name == part)
                            rtn= x.InnerText;
                    }
                }
            }
        }
        else
            rtn= null;
        return rtn;
    }
    public int sessionid(string session)
    {
        string fw = session.Substring(0, 1);
        string lw = session.Substring(5, 2);
        string lvl;

        if (fw == "S")
            lvl = lw + "1";
        else if (fw == "W")
            lvl = lw + "2";
        else
            lvl = "0";

        return Convert.ToInt32(lvl) ;
    }
    public int SessionToSessionID(string session)
    {
        int sum = 0,diff=0,yeardiff=0;
        if (session.Contains("Sum"))
        {
            int year = Convert.ToInt32(session.Substring(3, 4));
            if (year >= 2010)
                year = Convert.ToInt32(year.ToString().Substring(2, 2));
            else
                year = Convert.ToInt32(year.ToString().Substring(3, 1));
            diff = year - 7;
            yeardiff = year - 8;
             sum = diff + yeardiff;
        }
        else if(session.Contains("Win"))
        {
            int year = Convert.ToInt32(session.Substring(3, 4));
            if (year >= 2010)
                year = Convert.ToInt32(year.ToString().Substring(2, 2));
            else
                year = Convert.ToInt32(year.ToString().Substring(3, 1));
            diff = year - 7;
            yeardiff = year - 8;
            sum = diff + yeardiff+1;
        }
        return sum;

    }
      //XmlTextReader reader=new XmlTextReader(HttpContext.Current.Server.MapPath("../Exam/SessionDuration.xml"));
      //      if(course=="Civil")
      //      {
      //          if(reader.NodeType==XmlNodeType.Element)
      //          {
      //              if(reader.Name=="Civil" && part=="PartI")
      //              {
      //                  x=reader.GetAttribute("PartI");
      //              }
      //              else if(reader.Name=="Civil" && part=="PartII")
      //              {
      //                   x = reader.GetAttribute("PartII");
      //              }
      //              else if(reader.Name=="Civil" && part=="SectionA")
      //              {
      //                 x = reader.GetAttribute("SectionA");
      //              }
      //              else if(reader.Name=="Civil" && part=="SectionB")
      //              {
      //                  x = reader.GetAttribute("SectionB");
      //              }
      //      }
      //      else if (course == "Architecture")
      //      {
      //           if(reader.Name=="Architecture" && part=="PartI")
      //              {
      //                  x=reader.GetAttribute("PartI");
      //              }
      //              else if(reader.Name=="Architecture" && part=="PartI")
      //              {
      //                    x=reader.GetAttribute("PartI");
      //              }
      //              else if(reader.Name=="Architecture" && part=="PartII")
      //              {
      //                  x = reader.GetAttribute("PartII");
      //              }
      //              else if(reader.Name=="Architecture" && part=="SectionA")
      //              {
      //                  x = reader.GetAttribute("SectionA");
      //              }
      //              else if(reader.Name=="Architecture" && part=="SectionB")
      //              {
      //                  x = reader.GetAttribute("SectionB");
      //              }
      //      }
      //  }
      //      return x;
      //}
}