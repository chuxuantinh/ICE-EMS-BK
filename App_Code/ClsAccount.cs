using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for ClsAccount
/// </summary>
public class ClsAccount
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    public void AmountSubmit(string imiddev,string diarynagar, DateTime mydate,string type,string amt,string session,string details)
    {
        con.Close(); con.Open();
        SqlCommand cmd;
        cmd = new SqlCommand("insert into Account (IMID,DiaryNo,Date,Balance,Type,Amount,Session,Details,Status) values('" + imiddev.ToString() + "','" + diarynagar.ToString() + "','" + Convert.ToDateTime(mydate.ToString()) + "',0,'" + type.ToString() + "','" + Convert.ToInt32(amt) + "','" + session.ToString() + "','" + details.ToString() + "','Current')", con);
        cmd.ExecuteNonQuery();
    }
    public void AmountSubmit(string imiddev, string diarynagar, DateTime mydate, string type, string amt, string session, string details,SqlTransaction sTR,SqlCommand cmd)
    {
        cmd.CommandText ="insert into Account (IMID,DiaryNo,Date,Balance,Type,Amount,Session,Details,Status) values('" + imiddev.ToString() + "','" + diarynagar.ToString() + "','" + Convert.ToDateTime(mydate.ToString()) + "',0,'" + type.ToString() + "','" + Convert.ToInt32(amt) + "','" + session.ToString() + "','" + details.ToString() + "','Current')";
        cmd.ExecuteNonQuery();
    }
    public void AmountUpdate(int SNo, string amt, SqlCommand cmd, string bank , string ddno)
    {               
        cmd.CommandText = "update Account set Details=@Details,Amount=@Amount  where SN=@SN";
        cmd.Parameters.AddWithValue("Details",ddno.ToString()+":"+bank.ToString());
        cmd.Parameters.AddWithValue("@Amount", amt.ToString());
        cmd.Parameters.AddWithValue("@SN", SNo);
        cmd.ExecuteNonQuery();
    }
}