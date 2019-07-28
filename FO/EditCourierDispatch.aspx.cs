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

public partial class FO_EditCourierDispatch : System.Web.UI.Page
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
                    lblGridTitle.Text = "Last 30 Couriers " + txtNo.Text.ToString();
                    query = "select CourierType,SendTo,Name,Date,CourierService,Session,CourierSerialno,Consignmentno,Weight,Amount from CourierRD where Date BETWEEN '" + DateTime.Now.AddDays(-30) + "' and '" + DateTime.Now + "'";
                    GridCourier.DataSource = fillGrid();
                    DataBind();
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
            e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MM/yyyy");
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
            pnlsession.Visible = false;
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
        txtName.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void btnView_OnClick(object sender, EventArgs e)
    {
        GridCourier.DataSource = fillGrid();
        DataBind();
        btnView.Focus();
    }
    private DataTable  fillGrid()
    {
        string query = "";
        if (ddlsearch.SelectedValue == "IMID")
        {
            lblGridTitle.Text = "IMID: " + txtName.Text.ToString() + " Session:" + lblHiddenSeason.Text.ToString();
            query = "select CourierType,SendTo,Name,Date,CourierService,Session,CourierSerialno,Consignmentno,Weight,Amount from CourierRD where Session='" + lblHiddenSeason.Text.ToString() + "' and Name='" + txtName.Text.ToString() + "'";
        }
        else if (ddlsearch.SelectedValue == "CourierService")
        {
            lblGridTitle.Text = ddlCourier.SelectedValue.ToString() + " Session:" + lblHiddenSeason.Text.ToString();
            query = "select CourierType,SendTo,Name,Date,CourierService,Session,CourierSerialno,Consignmentno,Weight,Amount from CourierRD where Session='" + lblHiddenSeason.Text.ToString() + "' and CourierService='" + ddlCourier.SelectedValue.ToString() + "'";
        }
        else if (ddlsearch.SelectedValue == "RefrenceNo")
        {
            lblGridTitle.Text = "Reference No: " + txtNo.Text.ToString();
            query = "select CourierType,SendTo,Name,Date,CourierService,Session,CourierSerialno,Consignmentno,Weight,Amount from CourierRD where CourierSerialno='" + txtNo.Text.ToString() + "'";
        }
        else
        {
            lblGridTitle.Text = "Consignment No. " + txtNo.Text.ToString();
            query = "select CourierType,SendTo,Name,Date,CourierService,Session,CourierSerialno,Consignmentno,Weight,Amount from CourierRD where ConsignmentNo='" + txtNo.Text.ToString() + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        return dt;
    }
    protected void GridCourier_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlEdit.Visible = true;
        Panel1.Visible = false;
        GridViewRow row;
        row = GridCourier.SelectedRow;
        lblCourierNo.Text = row.Cells[7].Text;
        DropDownList1.SelectedValue = row.Cells[6].Text.Substring(0, 3);
        TextBox1.Text = row.Cells[6].Text.Substring(3, 4);
        txtRecivefrom.Text = row.Cells[2].Text;
        if (row.Cells[2].Text == "Other") pnlOther.Visible = true;
        txtDiraryType.Text = row.Cells[1].Text;
        txtCourierService.Text = row.Cells[5].Text;
        txtSName.Text = row.Cells[3].Text;
        txtConsignmentNo.Text = row.Cells[8].Text;
        txtWt.Text = row.Cells[9].Text;
        txtAmt.Text = row.Cells[10].Text;
        txtDiaryDate.Text = row.Cells[4].Text;
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from CourierRD where CourierSerialno='"+lblCourierNo.Text.ToString()+"'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            txtPhoneNo.Text = reader["Phone"].ToString();
            txtPincode.Text = reader["Pincode"].ToString();
            txtState.Text = reader["State"].ToString();
            txtCity.Text = reader["City"].ToString();
            txtAddress1.Text = reader["PAddress"].ToString();
            txtAddress2.Text = reader["CAddress"].ToString();
        }
        reader.Close();
        con.Close();
    }
   
    protected void txtSName_TExtChnaged(object sender, EventArgs e)
    {
        btnView.Focus();
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Open();
        SqlCommand cmd = new SqlCommand("update CourierRD set CourierType=@CourierType,SendTo=@SendTo,Name=@Name,PAddress=@PAddress,CAddress=@CAddress,City=@City,State=@State,Pincode=@Pincode,Phone=@Phone,Date=@Date,CourierService=@CourierService,Session=@Session,CourierSerialno=@CourierSerialno,Consignmentno=@Consignmentno,Weight=@Weight,Amount=@Amount where CourierSerialno='"+lblCourierNo.Text.ToString()+"'", con);
        cmd.Parameters.AddWithValue("@CourierType", txtDiraryType.Text.ToString());
        cmd.Parameters.AddWithValue("@SendTo", txtRecivefrom.Text);
        cmd.Parameters.AddWithValue("@Name", txtSName.Text);
        cmd.Parameters.AddWithValue("@PAddress", txtAddress1.Text);
        cmd.Parameters.AddWithValue("@CAddress", txtAddress2.Text);
        cmd.Parameters.AddWithValue("@City", txtCity.Text);
        cmd.Parameters.AddWithValue("@State", txtState.Text);
        cmd.Parameters.AddWithValue("@Pincode", txtPincode.Text);
        cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
        if (txtDiaryDate.Text == "") txtDiaryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDiaryDate.Text, dtinfo));
        cmd.Parameters.AddWithValue("@CourierService", txtCourierService.Text);
        cmd.Parameters.AddWithValue("@Session", (ddlExamSeason.SelectedValue + TextBox1.Text.ToString()));
        cmd.Parameters.AddWithValue("@CourierAddress", lblHiddenSeason.Text.ToString());
        cmd.Parameters.AddWithValue("@CourierSerialno", lblCourierNo.Text.ToString());
        cmd.Parameters.AddWithValue("@Consignmentno", txtConsignmentNo.Text.ToString());
        cmd.Parameters.AddWithValue("@Weight", txtWt.Text.ToString());
        cmd.Parameters.AddWithValue("@Amount", txtAmt.Text);
        cmd.ExecuteNonQuery();
        GridCourier.DataSource = fillGrid();
        DataBind();
        con.Close();
        con.Dispose();
        pnlEdit.Visible = false;
        lblException.Text = "Successfully Updated";
        btnView.Focus();
    }
}