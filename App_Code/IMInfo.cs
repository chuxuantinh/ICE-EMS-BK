using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Collections;

/// <summary>
/// Summary description for IMInfo
/// </summary>
public class IMInfo
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;

	public IMInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bool isIMHave(string imid)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select Active from Member where ID='" + imid.ToString() + "'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        if (id == "" || id == string.Empty)
            return false;
        else return true;
    }
    public string[] info(string imid)
    {
        string[] striminfo=new string[6];
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select * from IM where ID='" + imid.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                striminfo[0] = reader["Name"].ToString();
                striminfo[1] = reader["PAddress"].ToString();
                striminfo[2] = reader["Address2"].ToString();
                striminfo[3] = reader["PCity"].ToString();
                striminfo[4] = reader["PState"].ToString();
                striminfo[5] = reader["PPinCode"].ToString();
            }
            reader.Close();
            con.Close();
        }
        catch (SqlException ex)
        {
            striminfo[0] = "Exception";
            striminfo[1] = ex.ToString();
            striminfo[2] = "Exception";
            striminfo[3] = "Exception";
            striminfo[4] = "Exception";
            striminfo[5] = "Exception";
        }
        finally
        {
        }

        return striminfo;
    }
    public string[] imac(string imid)
    {
        string[] strimac = new string[7];
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select * from IMAC where IMID='" + imid.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strimac[0] = reader["Late"].ToString();
                strimac[1] = reader["Total"].ToString();
                strimac[2] = reader["GTotal"].ToString();
                strimac[3] = reader["GID"].ToString();
                strimac[4] = reader["Credit"].ToString();
                strimac[5] = reader["LateFeeTaken"].ToString();
                strimac[6] = reader["IMTotal"].ToString();
            }
            reader.Close(); con.Close();
        }
        catch (SqlException ex)
        {
            strimac[0] = "Exception";
            strimac[1] = ex.ToString();
            strimac[2] = "Exception";
            strimac[3] = "Exception";
            strimac[4] = "Exception";
            strimac[5] = "Exception";
        }
        finally
        {
        }
        return strimac;
    }
    public ArrayList overseasIM()
    {
        XmlDocument xdoc = new XmlDocument();
        ArrayList al = new ArrayList();
        string file = HttpContext.Current.Server.MapPath("../xml/OverseasIM.xml");
        if (File.Exists(file))
        {
            xdoc.Load(file);
            XmlElement xelm = xdoc.DocumentElement;
            XmlNodeList xnode = xelm.ChildNodes;
            for (int xn = 0; xn <= xnode.Count-1;xn++ )
            {
                al.Add(xnode[xn].InnerText);
            }
        }
      return al;
    }
    public string imFeeMaster(string imid)
    {
            ArrayList str = overseasIM();
            bool flag = false;
            foreach (string value in str)
            {
                if (value == imid)
                    flag = true;
            }
            if (flag == true)
                return "Overseas";
            else
                return  "Home";
    }
}