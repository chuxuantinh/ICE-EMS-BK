using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

public partial class frontOffceHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                maikal mk = new maikal();
                int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
            }
            if (!IsPostBack)
            {
                txtName.Focus();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblGridTitle.Text = " Today Visitor List ";
                SqlDataAdapter ad = new SqlDataAdapter("select Name,Phone,Mobile,Email,Reason,Detail from Reception where Date='" + DateTime.Now.Date + "' ORDER BY SN DESC", con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                GrvVisitorview.DataSource = ds;
                GrvVisitorview.DataBind();
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
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
       
            if (lvl == 0)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnRedirectAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect(Response.Cookies["redi"]["2"].ToString());
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd/MM/yyyy";
        dtfi.DateSeparator = "/";
        con.Close();
        con.Open();
        string strinsert = "insert into Reception(Name,Phone,Mobile,Email,Date,Reason,Detail)Values(@Name,@Phone,@Mobile,@Email,@Date,@Reason,@Detail)";
        SqlCommand cmd = new SqlCommand(strinsert,con);
        cmd.Parameters.AddWithValue("Name",txtName.Text.ToString());
        cmd.Parameters.AddWithValue("Phone",txtPhonecode.Text+"-"+txtPhoneNo.Text);
        cmd.Parameters.AddWithValue("Mobile",txtMobile.Text.ToString());
        cmd.Parameters.AddWithValue("Email", txtEmail.Text.ToString());
        cmd.Parameters.AddWithValue("Date",Convert.ToDateTime(txtDate.Text.ToString(),dtfi));
        cmd.Parameters.AddWithValue("Reason",Txtia.Text.ToString());
        cmd.Parameters.AddWithValue("Detail",TxtComment.Text.ToString());
        cmd.ExecuteNonQuery();
        btnsave.Enabled = false;
        lblexception.Text = "Information Saved";
        grid();
        GrvVisitorview.Focus();
        ClearTextBoxes();
        con.Close();
        con.Dispose();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
        ClearTextBoxes();
    }
    public void ClearTextBoxes()
    {
        txtName.Text = string.Empty;
        txtPhonecode.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtDate.Text = string.Empty;
        Txtia.Text = string.Empty;
        TxtComment.Text = string.Empty;
        txtMobile.Text = string.Empty;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid();
        GrvVisitorview.PageIndex = e.NewPageIndex;
        GrvVisitorview.DataBind();
    }
    public void grid()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select Name,Phone,Mobile,Email,Reason,Detail from Reception where Date='" + DateTime.Now.Date + "' ORDER BY SN DESC", con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GrvVisitorview.DataSource = ds;
        GrvVisitorview.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

    

}
