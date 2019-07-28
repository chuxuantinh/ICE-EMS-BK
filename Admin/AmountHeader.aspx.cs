using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;


public partial class AmountHeader : System.Web.UI.Page
{
    #region Connection
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlDataAdapter ad = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
   
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        string st = Server.MapPath("~/XML/AmountHeader.xml");
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
            }
            if (Page.IsPostBack == false)
            {
                if (File.Exists(st) == false)
                    createxml();
                    getxml();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        DropDownList1.Visible = false;

    }

    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (i == 0 | i == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2)
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Loign.aspx");
        }
    }

    private void createxml()
    {
        DataTable tb = new DataTable("Amount");
        tb.Columns.Add("Aname", Type.GetType("System.String"));                   
        String st = Server.MapPath("~/XML/AmountHeader.xml");
        tb.WriteXml(st);
    }

    private void getxml()
    {
        String st = Server.MapPath("~/XML/AmountHeader.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(st);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "save")
        {
            String st = Server.MapPath("~/XML/AmountHeader.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(st);
            DataRow r = ds.Tables[0].NewRow();
            r[0] = ((TextBox)(GridView1.FooterRow.FindControl("TextBox3"))).Text;
            ds.Tables[0].Rows.Add(r);
            ds.WriteXml(st);
            getxml();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        String st = Server.MapPath("~/XML/AmountHeader.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(st);
        ds.Tables[0].Rows.RemoveAt(e.RowIndex);
        ds.WriteXml(st);
        getxml();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        getxml();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        getxml();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String st = Server.MapPath("~/XML/AmountHeader.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(st);
        ds.Tables[0].Rows[e.RowIndex][0] = ((TextBox)(GridView1.Rows[e.RowIndex].FindControl("TextBox2"))).Text;
        GridView1.EditIndex = -1;
        ds.WriteXml(st);
        getxml();
    }

    private void LoadDropdownList()
    {
        try
        {
            DataSet dsHeader = new DataSet();
            dsHeader.ReadXml(Server.MapPath("~/XML/AmountHeader.xml"));
            DropDownList1.DataSource = dsHeader;
            DropDownList1.DataTextField = "Aname";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("-- Select --", "0"));
            DropDownList1.DataSource = dsHeader;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE IMAC SET Limit='" + txtLimit.Text + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Limit successfully Updated.........!!!');", true);
        txtLimit.Text = string.Empty;
    }
}