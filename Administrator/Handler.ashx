<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public class Handler : IHttpHandler 
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string imageid = context.Request.QueryString["ImID"];
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Conn"]);
            con.Open();
            SqlCommand command = new SqlCommand("select Img from Member where ID='" + imageid + "'", con);
            SqlDataReader dr = command.ExecuteReader();
            dr.Read();
            context.Response.BinaryWrite((Byte[])dr[0]);
            con.Close();
            context.Response.End();
        }
        catch (InvalidCastException ex)
        {

        }
        catch (NullReferenceException ex)
        {

        }
        catch (Exception ex)
        {
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}