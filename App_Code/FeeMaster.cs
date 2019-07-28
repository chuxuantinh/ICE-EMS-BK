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
using System.Data.SqlClient;

/// <summary>
/// Summary description for FeeMaster
/// </summary>
public class FeeMaster
{
     SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
	public FeeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string rtnlvl(string session)
    {
        string fw = session.Substring(0, 1);
        string lw = session.Substring(5, 2);
        string yr = DateTime.Now.Year.ToString();
        yr=yr.Substring(2,2);
        string lvl;
        if (Convert.ToInt32(lw) < Convert.ToInt32(yr))
        {
            lvl = "Typed Year Can Not Less To Current Year: " + DateTime.Now.Year;
        }
        else
        {
            if (fw == "S")
                lvl = lw + "1";
            else if (fw == "W")
                lvl = lw + "2";
            else
                lvl = "0";
        }
        return lvl;
    }
    public string sessionid(string session)
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
        
        return lvl;
    }
    public string feelvl(string session)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select FeeLevel from FeeMaster where Status='Active'", con);
        string lvl = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        return lvl;
    }
    public string currentCourse(string stream)
    {
        con.Close(); con.Open();
        string qry = "";
        if (stream == "Civil")
            qry = "select CourseID from CivilSubMaster where Status='Active'";
        else if (stream == "Architecture")
            qry = "select CourseID from ArchiSubMaster where Status='Active'";
        SqlCommand cmd = new SqlCommand(qry, con);
        qry = Convert.ToString(cmd.ExecuteScalar());
        return qry;
    }
    public string nextSession(string session)
    {
        string fw = session.Substring(0, 1);
        string lw = session.Substring(3, 4);
        string lvl;

        if (fw == "S")
        {
            lvl = "Win" + lw.ToString();
        }
        else if (fw == "W")
        {
            lvl = "Sum" + (Convert.ToInt32(lw) + 1).ToString();
        }
        else
            lvl = session.ToString();
        return lvl;
    }
}
