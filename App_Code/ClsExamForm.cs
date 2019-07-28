using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for ClsExamForm
/// </summary>
public class ClsExamForm
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);   
    public string[] ComplSubjects(string sid)
    {
        string[] subject = new string[2];
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select SID from PartIIStudent where SID='" + sid + "'", con);
        bool flg = false;
        if (Convert.ToString(cmd.ExecuteScalar()) == "")
        {
            subject[0] = "NULL";
            subject[1] = "NULL";
        }
        else
        {
            subject[0] = sid;
            flg = true;
            subject[1] = "NotSubmitted";
        }
        return subject;
    }
    public void ComplSubjectsUpdate(string sid)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/ExamMembership.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = el.ChildNodes;
        foreach (XmlNode nd in nl)
        {
            if ((nd.Attributes["Name"].InnerText == sid) && (nd.Attributes["Subject1"].InnerText == "NotSubmitted"))
            {
                nd.Attributes["Subject1"].InnerText = "Submitted";
            }
        }
        doc.Save(HttpContext.Current.Server.MapPath("~/XML/ExamMembership.xml"));
    }
    public DataTable ResultPro(string session, string course, string part)
    {
        DataTable dt = new DataTable();
        DataTable dtrs;
        DataTable dtcourse;
        SqlDataAdapter ad = new SqlDataAdapter("select distinct(SID) from SExamMarks where ExamSeason='" + session + "'", con);
        ad.Fill(dt);
        if (course == "Civil")
            ad = new SqlDataAdapter("select SubID,SubjectType from CivilSubMaster where Section='" + part + "' and CourseID='081'", con);
        else ad = new SqlDataAdapter("select SubId,SubjectType from ArchiSubMaster where Section='" + part + "' and CourseID='081'", con);
        dtcourse = new DataTable();
        ad.Fill(dtcourse);
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            DataRow rw=dt.Rows[i];
            int count=0,reg=0,ex=0;
            ad = new SqlDataAdapter("select SubID,Status from SExamMarks where SID='" + rw["SID"].ToString() + "' and Part='" + part + "' and Course='"+course+"' and ExamSeason='"+session+"'", con);
            dtrs = new DataTable();
            ad.Fill(dtrs);
            if (dtrs.Rows.Count > 0)
            {
                for (int j = 0; i <= dtcourse.Rows.Count-1; j++)
                {
                    DataRow rwc = dtcourse.Rows[j];
                    for (int k = 0; k <= dtrs.Rows.Count-1; k++)
                    {
                        DataRow rwr = dtrs.Rows[k];
                        if (rwc["SubID"].ToString() == rwr["SubID"].ToString())
                        {
                            if (rwr["Status"] == "Pass")
                            {
                                count += 1;
                                if ((rwc["SubID"] == "TC 2.10") || (rwc["SubID"] == "TC 2.11") || (rwc["SubID"] == "TA 2.11") || (rwc["SubID"] == "TA 2.12"))
                                {
                                    count = count - 1;
                                }
                                if (rwc["SubjectType"].ToString() == "Extra")
                                    ex = ex + 1;
                                else reg = reg + 1;
                            }
                        }
                    }
                }
            }
            if (part == "PartI" || part == "SectionA")
            {
                if (count >= 6)
                {
                }
                else rw.Delete();
            }
            else if (part == "PartII" && course == "Civil")
            {
                if (count >= 9)
                {
                }
                else rw.Delete();

            }
            else if (part == "PartII" && course == "Architecture")
            {
                if (count >= 10)
                {
                }
                else rw.Delete();
            }
            else if (part == "SectionB")
            {
                if (ex >= 5 && reg == 5)
                {
                }
                else rw.Delete();
            }
        }
        return dt;
    }
}