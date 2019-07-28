<%@ WebHandler Language="C#" Class="imageHandler" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.IO;

public class imageHandler : IHttpHandler {

    
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string imageid = context.Request.QueryString["ImID"];
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Conn"]);

            con.Open();
            SqlCommand command = new SqlCommand("select Sign from Docs where SID='" + imageid + "'", con);
            SqlDataReader dr = command.ExecuteReader();
            dr.Read();
            context.Response.BinaryWrite((Byte[])dr[0]);
            con.Close();
            context.Response.End();
        }
        catch (InvalidCastException ex)
        {
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}