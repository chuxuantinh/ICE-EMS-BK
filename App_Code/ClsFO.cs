using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for ClsFO
/// </summary>
public class ClsFO
{
    SqlConnection con=new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
	public ClsFO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int maxSN()
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select max(CID) from Counselling", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return i;
        }
        catch (SqlException ex)
        {
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}