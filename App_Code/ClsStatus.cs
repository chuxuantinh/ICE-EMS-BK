using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ClsStatus
/// </summary>
public class ClsStatus
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
	public ClsStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public Boolean changeStatus(string formtype,string sn, string status, string session)
    {
        string snname = "";
        bool bl = false;
        if (formtype == "Admission")
        {
            snname = sn + "NewAdmission";
        }
        else if (formtype == "Exam")
        {
            sn = sn+ "Exam";
        }
        else if (formtype == "ITI")
        {
            sn = sn + "ITI";
        }
        con.Close(); con.Open();
        cmd = new SqlCommand("select Status from AppRecord where FormType like '%" + snname.ToString() + "%'  and Session='" +session.ToString() + "'", con);
        string sts = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        if (sts == "NotApproved")
        {
            bl = false;
        }
        else if (sts == "Filled")
        {
            bl = false;
        }
        else if (sts.Contains("no"))
        {
            if (sts.Contains(formtype))
            {
                bl = false;
            }
            else
            {
                bl = true;
            }
        }
        return bl;
    }
}