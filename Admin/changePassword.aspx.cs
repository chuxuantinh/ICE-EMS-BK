using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Text;
using System.Web.Security;
using System.IO;

public partial class Admin_changePassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtChangePassword.Focus();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtConfirmPassUp.Text != txtPasswordUp.Text )
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Password Not matched.')", true);
                txtChangePassword.Focus();
            }
            else
            {
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("update Login set Password='" + txtPasswordUp.Text.ToString() + "' where LogName='" + Request.Cookies["MyLogin"]["UID"] + "' and Password='" + txtChangePassword.Text + "'", con);
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Password Changed successfully')", true);
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Password Not Updated.')", true);
                    txtChangePassword.Focus();
                }
                con.Close(); con.Dispose();
            }
        }
        catch (SqlException ex)
        {
            lblselecttext.Text = ex.ToString();
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void btnClearUp_Click(object sender, EventArgs e)
    {
        txtChangePassword.Text = ""; txtConfirmPassUp.Text = ""; txtPasswordUp.Text = ""; lblselecttext.Text = "";
        txtChangePassword.Focus();
    }
}