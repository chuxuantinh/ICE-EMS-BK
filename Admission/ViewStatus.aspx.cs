using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Admission_Viewstatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] == null | Session["sid"] == "")
        {
            Response.Redirect("AdmissionDepart.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
        }
        else
        {
            lblEnrolment.Text = Session["sid"].ToString();
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student where SID='" + Convert.ToString(Session["sid"].ToString()) + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                if (reader["Status"].ToString() == "Active" | reader["Status"].ToString() == "Disactive")
                {
                    lblId.Text = reader[1].ToString();
                    lblName.Text = reader[2].ToString();
                    lblStatus.Text = reader["Status"].ToString();
                }
                else
                {
                    lblId.Text = "not valid";
                }
            }
            reader.Close();
            con.Close();
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {

        con.Dispose();

    }


    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {

    }
    protected void btnStatus_Click(object sender, EventArgs e)
    {
        con.Open();
        if (lblStatus.Text == "Active")
        {
            lblStatus.Text = "Disactive";
        }
        else if (lblStatus.Text == "Disactive")
        {
            lblStatus.Text = "Active";
        }
        SqlCommand cmd = new SqlCommand("update Student set Status='"+lblStatus.Text.ToString()+"' where SID='" + Convert.ToString(Session["sid"].ToString()) + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
    }
}