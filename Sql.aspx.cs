using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Sql : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]))
        {
            try
            {
                SqlCommand cmd = new SqlCommand(txtQry.Text.ToString(), con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                lblMessage.Text = "Query Successfully Executed.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }

        }
    }
}