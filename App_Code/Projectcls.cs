using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for Projectcls
/// </summary>
public class Projectcls
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
	public Projectcls()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string CountStatusProject(string diary, string session)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select Status from ProjectCount where Session='" + session.ToString() + "' and DairyNo='" + diary.ToString() + "'", con);
        string sts = Convert.ToString(cmd.ExecuteScalar());
        if(sts=="")
        {
            return "NotSubmitted";
        }
        else 
        {
            return sts;
        }
    }
    public string CountStatusDairy(string diary, string session)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select Status from DairyCount where Session='" + session.ToString() + "' and DairyNo='" + diary.ToString() + "'", con);
        string sts = Convert.ToString(cmd.ExecuteScalar());
        if (sts == "")
        {
            return "NotSubmitted";
        }
        else
        {
            return sts;
        }
    }

    public string CountStatusDiaryLetter(string diary, string session)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select Status from DiaryLetter where Session='" + session.ToString() + "' and DiaryNo='" + diary.ToString() + "'", con);
        string sts = Convert.ToString(cmd.ExecuteScalar());
        if (sts == "")
        {
            return "NotSubmitted";
        }
        else
        {
            return sts;
        }
    }

    public string CheckSID(string id)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select * from Project where SID='" + id.ToString() + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            return "true";
        }
        else
            return "false";
        con.Close();
    }
}