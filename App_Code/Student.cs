using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for Student
/// </summary>
public class Student:FMarksheet
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
	public Student()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string[] status(string sid)
    {
        string[] strget = new string[6]; string stream, course, part, session;
      //  DateTime endate=new DateTime();
        DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select SID from Student where SID='" + sid.ToString() + "'", con);
            string name = Convert.ToString(cmd.ExecuteScalar());
            int i = 0;
            if (name == sid & sid!="")
            {
                i = i + 1;
            }
            else
            {
                strget[0] = "No"; strget[1] = "No";
                cmd = new SqlCommand("select FeeLevel from FeeMaster where Status='Active'", con);
                strget[2] = Convert.ToString(cmd.ExecuteScalar());
                strget[3] = "Tech"; strget[4] = "No";
            }
            if (i == 1)
            {
                cmd = new SqlCommand("select * from Student where SID='" + sid.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stream = reader["Stream"].ToString();
                    course = reader["Course"].ToString();
                    part = reader["Part"].ToString();
                    session = reader["Session"].ToString();
                    if (part == "PartII" | part == "SectionB")
                    {
                        strget[0] = "Direct";
                    }
                    if (part == "PartI" | part == "SectionA")
                    {
                        strget[0] = "Regular";
                    }
                    strget[1] = session.ToString();
                    strget[2] = reader["FeeLevel"].ToString();
                    strget[3] = stream;
                    strget[4] =Convert.ToDateTime(reader["DOB"].ToString()).ToString("dd/MM/yyyy");
                    strget[5] = reader["Name"].ToString();

                }
                reader.Close(); con.Close();
            }
        }
        catch (SqlException ex)
        {
            strget[0] = "Exception";
            strget[1] = ex.ToString();
            strget[2] = "Exception";
            strget[3] = "Exception";
            strget[4] = "Exception";
        }
        catch (NullReferenceException ex)
        {
            strget[0] = "Exception";
            strget[1] = ex.ToString();
            strget[2] = "Exception";
            strget[3] = "Exception";
            strget[4] = "Exception";
        }
        finally
        {
           // con.Close();
        }
        
            return strget;
    }
    public bool islastattempt(string sid,string course,string part,string session)
    {
        string mtime = "";
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("../Exam/SessionDuration.xml"));
        XmlNode nodroot = doc.DocumentElement;
        XmlNodeList xlist = doc.LastChild.ChildNodes;
        for (int i = 0; i < xlist.Count; i++)
        {
            if (xlist[i].Name == course)
            {
                XmlNodeList xli = xlist[i].ChildNodes;
                for (int j = 0; j < xli.Count; j++)
                {
                    if (xli[j].Name == part)
                    {
                        mtime = xli[j].InnerText.ToString();
                    }
                }
            }
        }
        cmd = new SqlCommand("select Session from ExamCurrent where SID='" + sid.ToString() + "'", con);
        con.Close(); con.Open();
        string oldsession = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        string stime = studentduration(oldsession, session);
        bool bl = false;
        if (float.Parse(stime) == float.Parse(mtime))
            bl = true;
        return bl;
    }
    public bool isSessionExp(string sid, string course, string part, string session)
    {
        string mtime = "";
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("../Exam/SessionDuration.xml"));
        XmlNode nodroot = doc.DocumentElement;
        XmlNodeList xlist = doc.LastChild.ChildNodes;
        for (int i = 0; i < xlist.Count; i++)
        {
            if (xlist[i].Name == course)
            {
                XmlNodeList xli = xlist[i].ChildNodes;
                for (int j = 0; j < xli.Count; j++)
                {
                    if (xli[j].Name == part)
                    {
                        mtime = xli[j].InnerText.ToString();
                    }
                }
            }
        }
        con.Close(); con.Open();
        cmd = new SqlCommand("select Session from ExamCurrent where SID='" + sid.ToString() + "'", con);
        string oldsession = Convert.ToString(cmd.ExecuteScalar());
        string stime = studentduration(oldsession, session);
        bool bl = false;
        if (float.Parse(stime) > float.Parse(mtime))
            bl = true;
        return bl;
    }
    public string AdmissionType(string sid,SqlTransaction sTR,SqlCommand cmd)
    {
        cmd.CommandText = "select Part from Student where SID='" + sid + "'";
        string part = Convert.ToString(cmd.ExecuteScalar());
        if (part == "PartII" || part == "SectionB")
            return "Direct";
        else return "Regular";
    }
}
