using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for clsIMAC
/// </summary>
public class clsIMAC
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);

    //public string[] getFeeHeader()
    //{
    //    //return;
    //}

    public void insertIMAccount(string imid)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("../XML/AmountHeader.xml");
    }

    // Update IMAccount.
    public void submitAmount(string imid, int amount, string type,SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand("update IMAccount set Amount=Amount+'" + amount + "' where IMID='" + imid + "' and Fees='" + type + "'", con);
        cmd.ExecuteNonQuery();
    }

    // Debit Amount.
    public void DebitAmount(string imid, int amount, string type, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand("Update IMAccount set Amount=Amount-'" + amount + "' where IMID='" + imid + "' and Fees='" + type + "'", con);
        cmd.ExecuteNonQuery();
    }

    public void DebitAmount(string imid, int amount, string type, SqlTransaction sTR,SqlCommand cmd)
    {
       cmd.CommandText="Update IMAccount set Amount=Amount-'" + amount + "' where IMID='" + imid + "' and Fees='" + type + "'";
       cmd.ExecuteNonQuery();
    }


    // Get Total Amount of IM
    public string totalamount(string imid,SqlTransaction sTR,SqlCommand cmd)
    {
        cmd.CommandText = "select SUM(Amount) from IMAccount where IMID='"+imid+"'";
        string amt = Convert.ToString(cmd.ExecuteScalar());
        if (amt == "")
            amt = "0";
        else amt = amt.TrimEnd('0').TrimEnd('.');
        return amt;
    }

    // Get Specific Amount of IM Account.
    public string specificAmount(string imid, string type, SqlTransaction sTR, SqlCommand cmd)
    {
        cmd.CommandText = "select Amount from IMAccount where IMID='" + imid + "' and Fees='" + type + "'";
        string amt = Convert.ToString(cmd.ExecuteScalar());
        if (amt == "")
            amt = "0";
        else amt = amt.TrimEnd('0').TrimEnd('.');
        return amt;
    }

    // Get Group IMID from One IMID
    public DataTable GroupMate(string imid)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select distinct(IMID) from IMAccount where GID in(select GID from IMAccount where imid='"+imid+"')", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        return dt;
    }

    // Debit Note Limit
    public string DebitNoteLimit(string imid,SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand("select Limit from IMAC where IMID='" + imid + "'", con);
        string amt = Convert.ToString(cmd.ExecuteScalar());
        if (amt == "")
            amt = "0";
        else amt = amt.TrimEnd('0').TrimEnd('.');
        return amt;
    }

    public string DebitNoteRange(string imid,SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand("select Range from IMAC where IMID='" + imid + "'", con);
        string amt = Convert.ToString(cmd.ExecuteScalar());
        if (amt == "")
            amt = "0";
        else amt = amt.TrimEnd('0').TrimEnd('.');
        return amt;
    }
}