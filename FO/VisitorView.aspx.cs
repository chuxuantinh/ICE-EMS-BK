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
using System.Globalization;
using System.Data.SqlClient;


public partial class VisitorView : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                if (!IsPostBack)
                {
                    maikal dev = new maikal();
                    maikal mk = new maikal();
                    int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    txtDiaryNo.Visible = false; ddlExamSeason.Visible = false; txtYearSeason.Visible = false;
                    lblGridTitle.Text = "This Week Diary";
                    SqlDataAdapter ad = new SqlDataAdapter("select Name,Phone,Mobile,Email,Date,Reason,Detail from Reception where Date BETWEEN '" + DateTime.Now.AddDays(-7) + "' and '" + DateTime.Now + "' order by SN DESC", con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    ddlType.Focus();
                }
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


    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
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
    
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text != "&nbsp;" || e.Row.Cells[4].Text != "")
                e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MM/yyyy");
        }
    }
    public void grid()
    {
        string qry = "";
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            if (ddlType.SelectedValue.ToString() == "Date")
            {
                lblGridTitle.Text = "List of Visitor,Date between " + dropp.Text.ToString() + " and " + drop2.Text.ToString();
                qry = "select  Name,Phone,Mobile,Email,Date,Reason,Detail from Reception where Date BETWEEN '" + Convert.ToDateTime(dropp.Text, dtinfo) + "' and '" + Convert.ToDateTime(drop2.Text, dtinfo) + "' ";
            }
            else if (ddlType.SelectedValue.ToString() == "Name")
            {
                lblGridTitle.Text = "Name: " + txtDiaryNo.Text.ToString();
                qry = " select Name,Phone,Mobile,Email,Date,Reason,Detail from Reception where Name ='" + txtDiaryNo.Text.ToString() + "'";
            }
        }
        catch (SqlException ex)
        {
            lblGridTitle.Text = "Previous 30 Days Diaries.";
            qry = "select  Name,Phone,Mobile,Email,Date,Reason,Detail from Reception where Date BETWEEN '" + DateTime.Now.AddDays(-30) + "' and '" + DateTime.Now + "'";
        }
        catch (FormatException ex)
        {
            lblGridTitle.Text = "Previous 30 Days Diaries.";
            qry = "select  Name,Phone,Mobile,Email,Date,Reason,Detail from Reception where Date BETWEEN '" + DateTime.Now.AddDays(-30) + "' and '" + DateTime.Now + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        grid();
        GridView1.Focus();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue.ToString() == "Date")
        {
            lblName.Text = "";
            dates.Visible = true;
            txtDiaryNo.Visible = false;
            txtYearSeason.Visible = false;
            ddlExamSeason.Visible = false;
            dropp.Focus();
        }
        else if (ddlType.SelectedValue.ToString() == "Name")
        {
            dates.Visible = false;
            txtDiaryNo.Visible = true; 
            txtYearSeason.Visible = false;
            ddlExamSeason.Visible = false;
            lblName.Text = "Name.";
            txtDiaryNo.Focus();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}
