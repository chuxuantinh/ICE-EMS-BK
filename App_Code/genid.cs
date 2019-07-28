using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for genid
/// </summary>
public class genid
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
	public genid()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string sid, sn;
    public string getsid(string im, string stream, string part)
    {
        con.Close();
        con.Open();
        SqlCommand co = new SqlCommand("select IMID from Student where IMID='" + im.ToString() + "'", con);
        string chkim = Convert.ToString(co.ExecuteScalar());
        if (chkim.ToString() == "")
        {
            sn = "0";
        }
        else
        {
            SqlCommand cmd = new SqlCommand("select Max(SN) from Student where IMID='" + im.ToString() + "'", con);
            sn = Convert.ToString(cmd.ExecuteScalar());
            // string id;
        }
        int i = Convert.ToInt32(sn) + 1; DateTime dt = DateTime.Now; string strdt = dt.ToString("yy");
        if (i <= 9)
        {

            sid = "000" + i;
        }
        else if (i <= 99)
        {
            sid = "00" + i;
        }
        else if (i <= 999)
        {
            sid = "0" + i;
        }
        else
        {
            sid = i.ToString();
        }
        if (stream == "Tech")
            stream = "T";
        if (stream == "Asso") stream = "A";
        string imcd = im + "" + stream + "" + part + ""+strdt+"" + sid.ToString();
        con.Close();
        return imcd;
    }
}
