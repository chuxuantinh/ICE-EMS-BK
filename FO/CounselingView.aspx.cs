using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.IO;

public partial class FO_CounselingView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
   
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
                    dtinfo.ShortDatePattern = "dd/MM/yyyy";
                    dtinfo.DateSeparator = "/";
                    pnlhome.Visible = true; pnlCounselling.Visible = false;
                    rbtndate.Checked = true;
                    maikal mk = new maikal();
                    int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    SqlDataAdapter ad = new SqlDataAdapter("select * from Counselling where Date Between '" + Convert.ToDateTime(txtDate.Text, dtinfo).AddMonths(-1) + "' and '" + Convert.ToDateTime(txtDate.Text, dtinfo) + "'", con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    GridCounselling.DataSource = ds;
                    GridCounselling.DataBind();
                    GridCounselling.Focus();

                    if (rbtnFollowupDate.Checked == true)
                    {
                        ad = new SqlDataAdapter("select * from Followup where CID='" + Convert.ToInt32(lblCID.Text) + "'", con);
                        ds = new DataSet();
                        ad.Fill(ds);

                        GridCounselling.DataSource = ds;
                        GridCounselling.DataBind();
                    }
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
   
    protected void Grid_OnselectedIndexChanged(object sender, EventArgs e)
    {
        if (GridCounselling.Rows.Count > 0)
        {
            pnlCounselling.Visible = true; pnlhome.Visible = false;
            fillProfile(Convert.ToInt32(GridCounselling.SelectedRow.Cells[1].Text.ToString()));
            rbtnFollowupDate.Checked = true;
            SqlDataAdapter ad = new SqlDataAdapter("select * from Followup where CID='" + Convert.ToInt32(GridCounselling.SelectedRow.Cells[1].Text.ToString()) + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            GridCounselling.DataSource = ds;
            GridCounselling.DataBind();
        }
    }
    protected void GridCounselling_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (rbtnFollowupDate.Checked == true)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false; e.Row.Cells[1].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false; e.Row.Cells[1].Visible = false;
                if (e.Row.Cells[3].Text != "&nbsp;")
                e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[12].Text != "&nbsp;")
                    e.Row.Cells[12].Text = Convert.ToDateTime(e.Row.Cells[12].Text).ToString("dd/MM/yyyy");
            }
        }
    }
    protected void btnView_OnClick(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (rbtndate.Checked == true)
            fillgrid("date", Convert.ToDateTime(txtDate.Text, dtinfo));
        else if (rbtnFollowupDate.Checked == true)
            fillgrid("nextdate", Convert.ToDateTime(txtDate.Text, dtinfo));
    }
    protected void btnchangeStatus_Onclick(object sender, EventArgs e)
    {
        lblStatus.Text = ddlStatus.SelectedValue.ToString();
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("update Counselling set Status=@Status where CID='" + Convert.ToInt32(lblCID.Text) + "'", con);
        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.ToString());
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
  
    private void fillgrid(string type, DateTime dt)
    {
        string qry = "";
        if (type == "date")
        {
            qry = "select * from Counselling where Date='" + dt + "'";
        }
        else if (type == "nextdate")
        {
            qry = "select * from Followup where FollowUpDate='" + dt + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridCounselling.DataSource = ds;
        GridCounselling.DataBind();
    }
    private void fillProfile(int id)
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Counselling where CID='" + id + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblSName.Text = reader["Name"].ToString();
            lblCourse.Text = reader["Course"].ToString();
            lblAddress1.Text = reader["Address1"].ToString();
            lblAddress2.Text = reader["Address2"].ToString();
            lblCity.Text = reader["City"].ToString();
            lblState.Text = reader["State"].ToString();
            lblPincode.Text = reader["PinCode"].ToString();
            lblcontact.Text = reader["Contact"].ToString();
            lblMobile.Text = reader["Mobile"].ToString();
            lblEmail.Text = reader["Email"].ToString();
            lblDate.Text = Convert.ToDateTime(reader["Date"].ToString()).ToString("dd/MM/yyyy");
            lblStatus.Text = reader["Status"].ToString();
            lblSession.Text = reader["Session"].ToString();
            lblCID.Text = reader["CID"].ToString();
        }
        reader.Close();
        con.Close();
    }

}