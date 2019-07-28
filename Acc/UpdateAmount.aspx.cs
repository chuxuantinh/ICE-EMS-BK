using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Globalization;


public partial class Acc_UpdateAmount : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                LoadDropdownList();
            }
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
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    private void LoadDropdownList()
    {
        try
        {
            DataSet dsHeader = new DataSet();
            dsHeader.ReadXml(Server.MapPath("~/XML/AmountHeader.xml"));
            ddlFee.DataSource = dsHeader;
            ddlFee.DataTextField = "Aname";
            ddlFee.DataBind();
            ddlFee.Items.Insert(0, new ListItem("--------- Select -----------", "0"));
            ddlFee.DataSource = dsHeader;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE IMAccount SET Amount='" + txtAmounts.Text.Trim() + "' WHERE IMID='" + Txtmember.Text + "' and  Fees='" + ddlFee.SelectedValue.ToString() + "'", con);
            int result = cmd.ExecuteNonQuery();
            bindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('IMAccount Successfully Updated.........!!!');", true);
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error ! Can't updated amount.');", true);
        }
        finally
        {
            con.Close(); con.Dispose();
            ddlFee.Focus();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd3 = new SqlCommand();
        SqlDataReader reader;
        cmd3.Connection = con;
        try
        {
            cmd3.CommandText = "select ID from Member where ID='" + Txtmember.Text.ToString() + "'";
            string chk = Convert.ToString(cmd3.ExecuteScalar());
            int i = 0;
            if (chk == Txtmember.Text.ToString())
            {
                i += 1;
                pnlAmt.Visible = true;
                bindGrid();
            }
            else
            {
                Txtmember.Text = "Invalid ID";
                Txtmember.Focus();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            con.Close();
            con.Dispose();
            ddlFee.Focus();
        }
    }
    
    protected void ddlFee_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT Amount FROM IMAccount  WHERE IMID='" + Txtmember.Text + "' and  Fees='" + ddlFee.SelectedItem.Text + "'", con);
        txtAmounts.Text = Convert.ToString(cmd.ExecuteScalar());
        txtAmounts.Text=  txtAmounts.Text.TrimEnd('0').TrimEnd('.');
        con.Close();
        con.Dispose();
        txtAmounts.Focus();

    }
    private void bindGrid()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select * from IMAccount where IMID='" + Txtmember.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        GridAmount.DataSource = dt;
        GridAmount.DataBind();
    }
}