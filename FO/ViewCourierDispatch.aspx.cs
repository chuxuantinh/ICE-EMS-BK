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
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class FO_ViewCourierDispatch : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    
    public static string query;
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
                    lblName.Text = "IMID";
                    pnlNo.Visible = false; pnlsession.Visible = true; ddlCourier.Visible = false;
                    lblGridTitle.Text = "Courier Dispatch Details " + txtNo.Text.ToString();
                    query = "select SNo,CourierType,Name,SendTo,City,State,CourierService,Weight,Amount,Date,CourierSerialno from CourierRD ";
                    fillGrid(query);
                    ddlsearch.Focus();
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
   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridCourier.PageIndex = e.NewPageIndex;
        GridCourier.DataBind();
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[9].Text != "&nbsp;")
                    e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToString("dd/MM/yyyy");
            }
        }
    }
    protected void ddlSearch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsearch.SelectedValue == "IMID")
        {
            lblName.Text = "IMID";
            ddlCourier.Visible = false;
            txtName.Visible = true;
            pnlNo.Visible = false;
            pnlsession.Visible = true;
            ddlExamSeason.Focus();
        }
        else if (ddlsearch.SelectedValue == "CourierService")
        {
            pnlNo.Visible = false;
            pnlsession.Visible = true;
            lblName.Text = "Courier Service";
            ddlCourier.Visible = true;
            txtName.Visible = false;
            ddlExamSeason.Focus();
        }
        else if (ddlsearch.SelectedValue == "RefrenceNo")
        {
            pnlNo.Visible = true;
            pnlsession.Visible =false ;
            lblNo.Text = "Reference No.";
            txtNo.Focus();
        }
        else if (ddlsearch.SelectedValue == "ConsignmentNo")
        {
            pnlNo.Visible = true;
            pnlsession.Visible = false;
            lblNo.Text = "Consignment No.";
            txtNo.Focus();
        }
        
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void btnView_OnClick(object sender, EventArgs e)
    {
        if (ddlsearch.SelectedValue == "IMID")
        {
            lblGridTitle.Text = "IMID: " + txtName.Text.ToString() + " Session:" + lblHiddenSeason.Text.ToString();
            query = "select SNo,CourierType,Name,SendTo,City,State,CourierService,Weight,Amount,Date,CourierSerialno from CourierRD where Session='" + lblHiddenSeason.Text.ToString() + "' and Name='" + txtName.Text.ToString() + "'";
            fillGrid(query);
        }
        else if (ddlsearch.SelectedValue == "CourierService")
        {
            lblGridTitle.Text = ddlCourier.SelectedValue.ToString() + " Session:" + lblHiddenSeason.Text.ToString();
            query = "select SNo,CourierType,Name,SendTo,City,State,CourierService,Weight,Amount,Date,CourierSerialno from CourierRD where Session='" + lblHiddenSeason.Text.ToString() + "' and CourierService='" + ddlCourier.SelectedValue.ToString() + "'";
            fillGrid(query);
        }
        else if (ddlsearch.SelectedValue == "RefrenceNo")
        {
            lblGridTitle.Text = "Reference No: " + txtNo.Text.ToString();
            query = "select SNo,CourierType,Name,SendTo,City,State,CourierService,Weight,Amount,Date,Consignmentno from CourierRD where CourierSerialno='" + txtNo.Text.ToString() + "'";
            fillGrid(query);
        }
        else if (ddlsearch.SelectedValue == "ConsignmentNo")
        {
            lblGridTitle.Text = "Consignment No. " + txtNo.Text.ToString();
            query = "select SNo,CourierType,Name,SendTo,City,State,CourierService,Weight,Amount,Date,CourierSerialno from CourierRD where ConsignmentNo='" + txtNo.Text.ToString() + "'";
            fillGrid(query);
        }
        GridCourier.Focus();
    }
    private void fillGrid(string qry)
    {
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridCourier.DataSource = ds;
        GridCourier.DataBind();
    }
}