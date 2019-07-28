using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class MasterInventory : System.Web.UI.MasterPage
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
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
                try
                {
                    SqlDataReader reader;
                    con.Close(); con.Open();
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                        int lvl = Convert.ToInt32(reader[20].ToString());
                        if (lvl == 0)
                        {
                            lblWelcome.Text = "Administrator";
                        }
                        else if (lvl == 1)
                        {
                            lblWelcome.Text = "Admin";
                        }
                        else if (lvl == 2)
                        {
                            lblWelcome.Text = "User ID";
                            usermanage.Visible = false;
                            liOrderBooks.Visible = false; liDelivery.Visible = false; liSearchPO.Visible = false; liStock.Visible = false;
                            liIMOrder.Visible = false; liViewIMOrder.Visible = false;
                            panelProfile.Visible = false;
                            if (reader["InvenAdmin1"].ToString() == "Stock")
                            {
                                liOrderBooks.Visible = true; liDelivery.Visible = true; liSearchPO.Visible = true; liStock.Visible = true;
                            }
                            if (reader["InvenAdmin2"].ToString() == "Suplier")
                            {
                                panelProfile.Visible = true;
                                liStock.Visible = true; liIMOrder.Visible = true; liViewIMOrder.Visible = true;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    }
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/Invent/Default.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=rpt&lvl=zero&typ=In");
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());

            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            {
                Response.Redirect("../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void lbtnorderb_Click(object sender, EventArgs e)
    {
        Response.Redirect("PlaceOrder.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtndeliveryf_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiveOrder.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnsearcho_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchOrder.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnManageItems_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageItems.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnIMOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMOrderEntry.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnViewIMOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewIMOrder.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnStock_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockView.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnSupply_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMOrderSupply.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnIMOrderSecB_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMOrderSecB.aspx?maikal=&lnk=null&typ=In");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["dev"]);
    }
}
