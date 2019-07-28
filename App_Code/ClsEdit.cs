using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsEdit
/// </summary>
public class ClsEdit
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    public String[] EditCount(string node)
    {
        string[] count = new string[2];
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/Xml/EditCount.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = doc.GetElementsByTagName(node);
        XmlNode nd = nl.Item(0);
        count[0] = nd.Attributes["Counter"].InnerText;
        if (Convert.ToInt32(nd.Attributes["Counter"].InnerText) >0)
            count[1] = "True";
        else count[1] = "False";
        return count;
    }
    public void CountUp(string node)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/Xml/EditCount.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nl = doc.GetElementsByTagName(node);
        XmlNode nd = nl.Item(0);
        int cntr = Convert.ToInt32(nd.Attributes["Counter"].InnerText);
        cntr = cntr - 1;
        nd.Attributes["Counter"].InnerText = cntr.ToString();
        doc.Save(HttpContext.Current.Server.MapPath("~/Xml/EditCount.xml"));
    }
    public string ToAppAddCount()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AppAddCount(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select Count(SN) from AppRecord where Session='"+session+"'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AppAppCount(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select Count(SN) from AppRecord where Status!='Hold' and Status!='NotApproved' and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string RecheckingCount()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string UFMCount()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string MSubscriptionCount()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string DiaryEntry(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from DiaryEntry where Status='DiaryEntry' and ExamSession='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string CountRcv(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from DiaryEntry where Status='CountReceive' and ExamSession='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string CountDispatch(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from DiaryEntry where Status='CountDispatch' and ExamSession='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AccReceive(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from DiaryEntry where Status='AccReceive' and ExamSession='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AccSupply(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from DiaryEntry where Status='Open' and ExamSession='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamToDiary(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select sum(ExamFormRcv) from DairyCount where Session='"+session+"'", con);
        return Convert.ToString(cmd.ExecuteScalar());
    }
    public string ExamFormSubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from AppRecord where FormType like '%Exam%' and Session='"+session+"'" , con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamFormApproved(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from AppRecord where FormType like '%Exam%' and Session='" + session + "' and Status!='NotApproved' and Status!='Hold'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamFormFilled(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SID) from ExamForms where ExamSeason='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamFormRollNO(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SID) from ExamForms where ExamSeason='"+session+"' and Status='RollNoGenerated' ", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamFormAdmitCard(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SID) from ExamForms where ExamSeason='" + session + "' and Status='AdmitCardGenerated'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamMarksNotSubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SID) from ExamForms where ExamSeason='" + session + "' and Status='AdmitCardGenerated'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamMarksSubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SID) from ExamForms where ExamSeason='" + session + "' and Status='MarkSubmitted'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ExamFormHold(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SN) from ExamForms where ExamSeason='"+session+"' and Status='Hold'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AddToDiary(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select sum(EnrollFormRcv) from DairyCount where Session='"+session+"'", con);
        return Convert.ToString(cmd.ExecuteScalar());
    }
    public string AddFormSubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from AppRecord where FormType like '%NewAdmission%' and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AddFormApproved(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SN) from AppRecord where FormType like '%NewAdmission%' and Session='" + session + "' and Status!='NotApproved' and Status!='Hold'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AddFormFilled(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(SID) from Student where Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AddActive(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(distinct(SID)) from Student where Session='" + session + "' and Status='Active'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AddDisActive(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(distinct(SID)) from Student where  Status='Disactive'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }

    public string ASFAmount(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select sum(AnnualSubFees) from AppRecord where Status!='NotApproved' and Status!='Hold' and AnnualSubFees!=0 and Session='"+session+"'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }

    public string ASFSubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(AnnualSubFees) from AppRecord where AnnualSubFees!=0 and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }

    public string ASFApproved(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(AnnualSubFees) from AppRecord where Status!='NotApproved' and Status!='Hold' and AnnualSubFees!=0 and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }

    public string CompAmount(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select sum(CompositeFees) from AppRecord where Status!='NotApproved' and Status!='Hold' and CompositeFees!=0 and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }

    public string CompSubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(CompositeFees) from AppRecord where CompositeFees!=0 and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string CompApproved(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(CompositeFees) from AppRecord where Status!='NotApproved' and Status!='Hold' and CompositeFees!=0 and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProTotalStudent(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(sid) from Project where Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProformaASubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where Status!='Selected' and (SynopsisStatus!='ReSubmit' or SynopsisStatus!='DisApproved') and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProformaAApproved(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where Status!='Selected' and SynopsisStatus='Approved' and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProformaBSubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where Status!='Selected' and Status!='SynopsisSubmitted' and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProformaBApproved(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where Status!='Selected' and Status!='SynopsisSubmitted' and Status!='ProformaBSubmitted' and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProCopySubmitted(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where (Status='Evaluated'  or Status='CopySubmitted') and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProCopyPending(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where Status='CopyPending' and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProformaC(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where Status='Evaluated' and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string ProResubmit(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select COUNT(SID) from Project where (SynopsisStatus='ReSubmit' or  SynopsisStatus='ReSubmit') and Session='" + session + "'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AcApproval(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(AppNO) from AppRecord where Status='NotApproved' and Session='"+session+"'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        return count;
    }
    public string AcHold(string session)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(AppNo) from AppRecord where Status='Hold' and Session='"+session+"'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        return count;
    }
    public string AcDebitNote()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select count(Status) from DebitNote where Status='Approved'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AcDDEntry()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select sum((DairyCount.AddRcv-DairyCount.AddSub)+(DairyCount.OddRcv-DairyCount.OddSub)+(DairyCount.MemberRcv-DairyCount.MemberSub)+(DairyCount.BooksRcv-DairyCount.BooksSub) +(DairyCount.ProspectusRcv-DairyCount.ProspectusSub)+(ProjectCount.DDRCv-ProjectCount.DDSub) ) from DairyCount inner join DiaryEntry on DairyCount.DairyNo=DiaryEntry.DiaryNo inner join ProjectCount on DairyCount.DairyNo=ProjectCount.DairyNo where (DairyCount.AddRcv>DairyCount.AddSub) Or (DairyCount.ODDRcv>DairyCount.ODDSub) Or (DairyCount.MemberRcv>DairyCount.MemberSub) Or (DairyCount.BooksRcv>DairyCount.BooksSub) Or (DairyCount.ProspectusRcv>DairyCount.ProspectusSub) Or (ProjectCount.DDRcv>ProjectCount.DDSub) and DiaryEntry.Status='Open'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
    public string AcReApproval()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("Select count(status) from RecoverApp where Status='NotApproved'", con);
        string count = Convert.ToString(cmd.ExecuteScalar());
        return count;
    }
}