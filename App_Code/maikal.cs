using System;
using System.Collections.Generic;

using System.Web;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for maikal
/// </summary>
public class maikal
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    public static string strUserName;
    public static string UserName
    {
        get
        {
            return strUserName;
        }
        set
        {
            strUserName = value;
        }
    }
    public static int stri;
    public static int i
    {
        get
        {
            return stri;
        }
        set
        {
            stri = value;
        }
    }
	public maikal()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string SearchProfile(string urlid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("select ID from Member where ID='" + urlid.ToString() + "'", con);
            con.Close();
            con.Open();
         //   if(cmd.ExecuteScalar().ToString()!="")
            string id = Convert.ToString(cmd.ExecuteScalar());
            if (urlid==id)
                //if (txtNewIdView.Text.ToString() == cmd.ExecuteScalar().ToString())
                return id.ToString();
            else
                return "No";

        }
        catch (SqlException ex)
        {
            return "No";
        }
            catch(NullReferenceException ex)
        {
            return "No";
            }
        finally
        {
            con.Close();
        }
    }
    public string SearchIM(string urlid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("select ID from Member where ID='" + urlid.ToString() + "'", con);
            con.Close();
            con.Open();
            //   if(cmd.ExecuteScalar().ToString()!="")
            string id = Convert.ToString(cmd.ExecuteScalar());
            if (urlid == id)
                //if (txtNewIdView.Text.ToString() == cmd.ExecuteScalar().ToString())
                return id.ToString();
            else
                return "No";

        }
        catch (SqlException ex)
        {
            return "";
        }
        catch (NullReferenceException ex)
        {
            return "";
        }
        finally
        {
            con.Close();
        }
    }
    public int  devnagar(int i)
    {
        
        //SqlDataReader reader;
        // SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Request.QueryString["dev"] + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
              //  reader = cmd.ExecuteReader();
        return 0;
    }
    public int returnlevel(string name, string password)
    {
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + name.ToString() + "' and Password='" + password.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            int i = 2;
            while (reader.Read())
            {
                i = Convert.ToInt32(reader[20]);
            }
            reader.Close();
            con.Close();
            con.Dispose();
            if (i == 0)
            {
                i = 0;
            }
            else if (i == 1)
            {
                i = 1;
            }
            else if (i == 2)
            {
                i = 2;
            }
            else
            {
                i = 2;
            }
            return i;
        }
        catch (SqlException ex)
        {
            return 3;
       
        }
    }
    public string refresh()
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        return url;
    }
    public static string strsn;
    public string psid()
    {
        con.Close();
        con.Open();
        // btnUpdate.Visible = false;
        // SqlDataReader reader;
        SqlCommand cmd = new SqlCommand("select Max(SN) from PaperSetter", con);
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        if (i <= 9)
        {
            strsn = "000" + i;
        }
        else if (i <= 99)
        {
            strsn = "00" + i;
        }
        else if (i <= 999)
        {
            strsn = "0" + i;
        }
        strsn = "PS" + strsn.ToString();
        return strsn;
    }
    public string chkstatusIM(string im)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select Active from IM where ID='" + im.ToString() + "'", con);
        string str = Convert.ToString(cmd.ExecuteScalar());
        return str.ToString();
    }
    public bool chkstatusStu(string sid)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select Status from Student where SID='" + sid.ToString() + "'", con);
        string str = Convert.ToString(cmd.ExecuteScalar());
        if (str == "DisActive")
            return false;
        else if (str == "Active")
            return true;
        return false;
    }
    public int chksession()
    {
        if (DateTime.Now.Month <= 6)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
