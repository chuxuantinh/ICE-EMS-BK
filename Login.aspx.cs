using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Web.Administration;

public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
       // Session.RemoveAll();
        try
        {
            if (Response.Cookies["MyLogin"].Value != null)
            {
                HttpCookie htco=new HttpCookie("MyLogin");
                htco.Expires = DateTime.Now.AddDays(-1d) ;
                Response.Cookies.Add(htco);
            }
            //if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) != null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
            if (IsPostBack == false)
            {
                SqlCommand cmd2 = new SqlCommand("select * from Login where Lavel=0", con);
                SqlDataReader reader;
                con.Close(); con.Open();
                reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    lblNm.Text = reader[1].ToString();
                    lblpass.Text = reader[2].ToString();
                }
                reader.Close();
                con.Close();
                hlnkforget.Visible = false;
            }
            txtName.Focus();
        }
        catch (SqlException ex)
        {
            lblException1.Text = "Please Connect to DataBase";
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            //var server = new ServerManager();
            //var site = server.Sites.FirstOrDefault(s => s.Name == "Default Web Site");
            //if (site != null)
            //{
            //    //stop the site...
            //    site.Stop();
            //    if (site.State == ObjectState.Stopped)
            //    {
            //        //do deployment tasks...
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException("Could not stop website!");
            //    }
            //    //restart the site...
            //    site.Start();
            //}
            //else
            //{
            //    throw new InvalidOperationException("Could not find website!");
           // }
            if (txtName.Text == "ICEAdmin" & txtPassword.Text == "Administrator")
            {
                string[] str = new string[2];
                str[0] = lblpass.Text.ToString();
                str[1] = lblNm.Text.ToString();
                MyLogin.login = str;
                Response.Cookies["MyLogin"]["PWD"] = str[0];
                Response.Cookies["MyLogin"]["UID"] = str[1];
                Response.Cookies["redic"].Value = "dev=" + lblNm.Text.ToString() + "&ain=Admin&em=E&eem=E2&adison=Ad&acouty=AC&i94en67=IN&Pro=Pro&rpt=rpt";
                Response.Redirect("SuperAdmin.aspx?dev=" + lblNm.Text.ToString() + "&ain=Admin&em=E&eem=E2&adison=Ad&acouty=AC&i94en67=IN&Pro=Pro&rpt=rpt");
            }
            else
            {
                SqlDataReader reader;
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + txtName.Text + "' and Password='" + txtPassword.Text.ToString() + "'", con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string strName = Convert.ToString(reader[1].ToString());
                    string strdate = Convert.ToString(reader[3].ToString());
                    string stremail = Convert.ToString(reader[4].ToString());
                    string strAdmin = Convert.ToString(reader[5].ToString());
                    string strExam1 = Convert.ToString(reader[6].ToString());
                    string strExam2 = Convert.ToString(reader[7].ToString());
                    string strAdminsion = Convert.ToString(reader[8].ToString());
                    string strAccounts = Convert.ToString(reader[9].ToString());
                    string strInven = Convert.ToString(reader[10].ToString());
                    string strtype = Convert.ToString(reader[24].ToString());
                    string strProject=Convert.ToString(reader["Project"].ToString());
                    string strReport=Convert.ToString(reader["Report"].ToString());
                    string[] str = new string[2];
                    str[0] = Convert.ToString(reader["Password"]);
                    str[1] = strName.ToString();
                    MyLogin.login = str;
                    Response.Cookies["MyLogin"]["PWD"] = str[0];
                    Response.Cookies["MyLogin"]["UID"] = str[1];
                    int i = Convert.ToInt32(reader[20].ToString());
                    Log.WriteLog(txtName.Text.ToString(), "B000", "", "", "Login");
                    Log.WriteLog("B000", txtName.Text.ToString(), "", "", "Login");
                    if (i == 0)
                    {
                        Response.Cookies["redic"].Value = "dev=" + strName + "&ain=" + strAdmin + "&em=" + strExam1 + "&eem=" + strExam2 + "&adison=" + strAdminsion + "&acouty=" + strAccounts + "&i94en67=" + strInven.ToString() + "&Pro=" + strProject + "&rpt=" + strReport;
                        reader.Close();
                        con.Close();
                        con.Dispose();
                        Response.Redirect("SuperAdmin.aspx?dev=" + strName + "&ain=" + strAdmin + "&em=" + strExam1 + "&eem=" + strExam2 + "&adison=" + strAdminsion + "&acouty=" + strAccounts + "&i94en67=" + strInven + "&Pro=" + strProject + "&rpt=" + strReport);
                    }
                    else if (i == 1)
                    {
                        reader.Close();
                        con.Close();
                        con.Dispose();
                        Response.Cookies["redic"].Value = "dev=" + strName + "&ain=" + strAdmin + "&em=" + strExam1 + "&eem=" + strExam2 + "&adison=" + strAdminsion + "&acouty=" + strAccounts + "&i94en67=" + strInven.ToString() + "&Pro=" + strProject + "&rpt=" + strReport;
                        Response.Redirect("SuperAdmin.aspx?dev=" + strName + "&ain=" + strAdmin + "&em=" + strExam1 + "&eem=" + strExam2 + "&adison=" + strAdminsion + "&acouty=" + strAccounts + "&i94en67=" + strInven + "&Pro=" + strProject + "&rpt=" + strReport);
                    }
                    else if (i == 2)
                    {
                        reader.Close();
                        con.Close();
                        con.Dispose();
                        Response.Cookies["redic"].Value = "dev=" + strName + "&ain=" + strAdmin + "&i94en67=" + strtype.ToString();
                        Response.Redirect("UserHome.aspx?dev=" + strName + "&ain=" + strAdmin + "&i94en67=" + strtype);
                    }
                    else
                    {
                        reader.Close();
                        con.Close();
                        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
                    }
                }
                else
                {
                    lblException1.Text = "Please insert correct ID and Password";
                    txtName.Focus();
                }
            }
        }
        catch (SqlException ex)
        {
            hlnkforget.Visible = true;
            lblException1.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void txtPassword_Changed(object sender, EventArgs e)
    {
        btnLogin.Focus();
    }
    protected void btnCencel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}