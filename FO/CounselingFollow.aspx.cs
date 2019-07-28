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


public partial class FO_CounselingFollow : System.Web.UI.Page
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
                   pnlCounselling.Visible = false;
                   rbtndate.Checked = true;
                   maikal mk = new maikal();
                   int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                   txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                   txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                   SqlDataAdapter ad = new SqlDataAdapter("select * from Counselling where Date Between '" + Convert.ToDateTime(txtDate.Text, dtinfo).AddMonths(-1) + "' and '" + Convert.ToDateTime(txtDate.Text, dtinfo) + "'", con);
                   DataSet ds = new DataSet();
                   ad.Fill(ds);
                   GridCounselling.DataSource = ds;
                   GridCounselling.DataBind();
                   GridCounselling.Focus();
               }
               if (rbtnFollowupDate.Checked == true)
               {
                   if (lblCID.Text.ToString() == null)
                   {
                       SqlDataAdapter ad = new SqlDataAdapter("select * from Followup where CID='" + Convert.ToInt32(lblCID.Text) + "'", con);
                       DataSet ds = new DataSet();
                       ad.Fill(ds);
                       GridCounselling.DataSource = ds;
                       GridCounselling.DataBind();
                   }
               }
               btnView.Focus();

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
    protected void ddlResponse_SeelctedIndexChanged(object sender, EventArgs e)
    {
        if (ddlResponse.SelectedValue.ToString() == "Positive")
            ddlResponse.Attributes.Add("class", "hot");
        if (ddlResponse.SelectedValue.ToString() == "Negative")
            ddlResponse.Attributes.Add("class", "cold");
        if (ddlResponse.SelectedValue.ToString() == "Normal")
            ddlResponse.Attributes.Add("class", "warm");
        txtCurrentDate.Focus();
    }
   
    protected void Grid_OnselectedIndexChanged(object sender, EventArgs e)
    {
        if(GridCounselling.Rows.Count>0)
        {
            pnlCounselling.Visible=true;
            fillProfile(Convert.ToInt32(GridCounselling.SelectedRow.Cells[1].Text.ToString()));
            rbtnFollowupDate.Checked = true;
            SqlDataAdapter ad = new SqlDataAdapter("select * from Followup where CID='" + Convert.ToInt32(GridCounselling.SelectedRow.Cells[1].Text.ToString()) + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            GridCounselling.DataSource = ds;
            GridCounselling.DataBind();
            ddlStatus.Focus();
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
        else if(rbtnFollowupDate.Checked==true)
            fillgrid("nextdate",Convert.ToDateTime(txtDate.Text,dtinfo));
        GridCounselling.Focus();
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
    protected void btnSubmit_Onclick(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        dtinfo.DateSeparator="/";
        dtinfo.ShortDatePattern="dd/MM/yyyy";
        SqlCommand cmd = new SqlCommand("select max(CounsellingNo) from Followup where CID='" + lblCID.Text.ToString() + "'", con);
        int inCid = Convert.ToInt32(cmd.ExecuteScalar());

        cmd = new SqlCommand("update Followup set Response=@Response,Comments=@Comments,Counselor=@Counselor where CID='" + Convert.ToInt32(lblCID.Text) + "' and CounsellingNo='"+inCid+"'", con);
        cmd.Parameters.AddWithValue("@Response", ddlResponse.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Comments", txtdetail.Text.ToString());
        cmd.Parameters.AddWithValue("@Counselor",txtCounselor.Text.ToString());
        cmd.ExecuteNonQuery();
       
        cmd = new SqlCommand("insert into Followup(CounsellingNo,FollowUpDate,Response,Comments,Counselor,CID) Values(@CounsellingNo,@FollowUpDate,@Response,@Comments,@Counselor,@CID)", con);
        cmd.Parameters.AddWithValue("@CounsellingNo", inCid+1);
        if(string.IsNullOrEmpty(txtNextDate.Text) || txtNextDate.Text=="")
        {
            cmd.Parameters.AddWithValue("@FollowUpDate",DBNull.Value);
        }
        else
        {
            cmd.Parameters.AddWithValue("@FollowUpDate",Convert.ToDateTime(txtNextDate.Text.ToString(),dtinfo));
        }
        //cmd.Parameters.AddWithValue("@FollowUpDate", Convert.ToDateTime(txtNextDate.Text.ToString(), dtinfo));
        cmd.Parameters.AddWithValue("@Response", ddlResponse.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Comments", txtdetail.Text.ToString());
        cmd.Parameters.AddWithValue("@Counselor",txtCounselor.Text.ToString());
        cmd.Parameters.AddWithValue("@CID", Convert.ToInt32(lblCID.Text));
        cmd.ExecuteNonQuery();
        con.Close();
        GridCounselling.Focus();
        txtdetail.Text = "";
        txtCounselor.Text = "";
    }
    private void fillgrid(string type,DateTime dt)
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