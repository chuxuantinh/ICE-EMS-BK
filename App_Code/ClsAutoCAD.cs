using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for ClsAutoCAD
/// </summary>
public class ClsAutoCAD
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataReader dr;
    string[] Fee = new string[2];
	public ClsAutoCAD()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //
    // Description: Return Current Batch Fees.
    //
    public string[] CurrentFee(string FeeType, SqlConnection con)
    {
            
        if (FeeType == "Home")
        {
            cmd = new SqlCommand("select HLateFee,HRegFee from MCADBATCH where Batch_ID in (select max(Batch_ID) from MCADBatch)", con);
        }
        else
            cmd = new SqlCommand("select OLateFee, ORegFee from MCADBATCH where Batch_ID in (select max(Batch_ID) from MCADBatch)", con);
      
        dr = cmd.ExecuteReader();
        Fee[0] = dr[0].ToString();
        Fee[1]= dr[1].ToString();
        return Fee;  
    }


    public int BatchID(string SID, SqlConnection con)
    {
        int bid = 0;
        cmd = new SqlCommand("select Batch_ID from MCAD where SID='"+SID+"' and CurrentStatus='Current'", con);
        string BatchID=Convert.ToString(cmd.ExecuteScalar());
        if (BatchID == "")
            bid = 0;
        else bid = Convert.ToInt32(BatchID);
        return bid;
    }
    public string[] BatchFee(string FeeType, int BatchID, SqlConnection con)
    {
        if (FeeType == "Home")
        {
            cmd = new SqlCommand("select HLateFee, HRegFee from MCADBATCH where Batch_ID ='" + BatchID+"'", con);
        }
        else
            cmd = new SqlCommand("select OLateFee, ORegFee from MCADBATCH where Batch_ID ='"+BatchID+"'", con);
        dr = cmd.ExecuteReader();
        if (dr.Read()) 
        {
            Fee[0] = dr[0].ToString();
            Fee[1] = dr[1].ToString();
        }
        dr.Close();
        return Fee;
    }

    public DateTime[] BatchDate(int BatchID, SqlConnection con)
    {
        DateTime[] Date = new DateTime[2];
        cmd = new SqlCommand("select StartDate, EndDate from MCADBATCH where Batch_ID ='" + BatchID + "'", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Date[0] =Convert.ToDateTime(dr[0].ToString());
            Date[1] = Convert.ToDateTime(dr[1].ToString());
        }
        dr.Close();
        return Date;
    }

    public string Status(string SID, SqlConnection con)
    {
        cmd = new SqlCommand("select Status from MCAD where SID='" + SID + "' and CurrentStatus='Current'", con);
        string Status = Convert.ToString(cmd.ExecuteScalar());
        return Status;
    }

    public int currentBatch(SqlConnection con)
    {
        cmd = new SqlCommand("select max(Batch_ID) from MCADBatch", con);
        string batch = Convert.ToString(cmd.ExecuteScalar());
        if (batch == "") batch = "1";
        return Convert.ToInt32(batch);
    }
}