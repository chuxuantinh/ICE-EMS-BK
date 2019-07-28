using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;
using System.IO;
using System.Xml;

public partial class FO_Counseling : System.Web.UI.Page
{
   
   SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
   ClsStateCity clState = new ClsStateCity();
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
               //showimg();
           }
           txtsessionyear.Text = DateTime.Now.Year.ToString();
           if (!IsPostBack)
           {
               clState.xmlstate(ddlState, "XMLState.xml");
               clState.xmlCity(ddlCity, ddlState.SelectedItem.Text, "XMLState.xml");
               txtcounselor.Focus();
               txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
               maikal mk = new maikal();
               int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
               ClsFO clsfo = new ClsFO();
               lblCID.Text = (clsfo.maxSN() + 1).ToString();
             
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
    
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            dtfi.DateSeparator = "/";
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("insert into Counselling(Name,Course,Address1,Address2,City,State,PinCode,Contact,Mobile,Email,Date,Status,Session)Values(@Name,@Course,@Address1,@Address2,@City,@State,@PinCode,@Contact,@Mobile,@Email,@Date,@Status,@Session)", con);
            cmd.Parameters.AddWithValue("@Name", txtname.Text.ToString());
            if (ddlCourse.SelectedValue == "AutoCAD")
                cmd.Parameters.AddWithValue("@Course", ddlCourse.Text);
            else
                cmd.Parameters.AddWithValue("@Course", ddlCourse.Text + ddlPart.Text.ToString());
            cmd.Parameters.AddWithValue("@Address1", txtaddress.Text.ToString());
            cmd.Parameters.AddWithValue("@Address2", txtaddress2.Text.ToString());
            cmd.Parameters.AddWithValue("@City", ddlCity.Text.ToString());
            cmd.Parameters.AddWithValue("@State", ddlState.Text.ToString());
            cmd.Parameters.AddWithValue("@PinCode", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("@Contact", txtcontact.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtmobile.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtemail.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtdate.Text, dtfi));
            cmd.Parameters.AddWithValue("@Status", "Running");
            cmd.Parameters.AddWithValue("@Session", txtsession.Text + txtsessionyear.Text);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("insert into Followup(CounsellingNo,FollowUpDate,Response,Comments,Counselor,CID) Values(@CounsellingNo,@FollowUpDate,@Response,@Comments,@Counselor,@CID)", con);
            cmd.Parameters.AddWithValue("@CounsellingNo", 1);
            cmd.Parameters.AddWithValue("@FollowUpDate", Convert.ToDateTime(txtdate.Text, dtfi));
            cmd.Parameters.AddWithValue("@Response", ddlResponse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Comments", txtdetail.Text.ToString());
            cmd.Parameters.AddWithValue("@Counselor", txtcounselor.Text.ToString());
            cmd.Parameters.AddWithValue("@CID", Convert.ToInt32(lblCID.Text));
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("insert into Followup(CounsellingNo,FollowUpDate,Response,Comments,Counselor,CID) Values(@CounsellingNo,@FollowUpDate,@Response,@Comments,@Counselor,@CID)", con);
            cmd.Parameters.AddWithValue("@CounsellingNo", 2);
            cmd.Parameters.AddWithValue("@FollowUpDate", Convert.ToDateTime(txtNextDate.Text, dtfi));
            cmd.Parameters.AddWithValue("@Response", ddlResponse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Comments", "");
            cmd.Parameters.AddWithValue("@Counselor", "");
            cmd.Parameters.AddWithValue("@CID", Convert.ToInt32(lblCID.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            lblexception.Text = "Information Saved";
            lblexception.ForeColor = System.Drawing.Color.Green;
            lblexception.Focus();
            btnsave.Enabled = false;
            btncancel.Focus();
            txtcounselor.Text = "";
            txtname.Text = "";
            txtcontact.Text = "";
            txtmobile.Text = "";
            txtemail.Text = "";
            txtaddress.Text = "";
            txtaddress2.Text = "";
            txtPPincode.Text = "";
            txtdetail.Text = "";
        }
        catch (FormatException ex)
        {
            lblexception.Text = "Date Format Invalid.";
        }
        catch (Exception ex)
        {
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtcounselor.Text = "";
        txtname.Text = "";
        txtcontact.Text = "";
        txtmobile.Text = "";
        txtemail.Text = "";
        txtaddress.Text = "";
        txtaddress2.Text = "";
        txtPPincode.Text = "";
        txtdetail.Text = "";
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void ddlResponse_SeelctedIndexChanged(object sender, EventArgs e)
    {
        if (ddlResponse.SelectedValue.ToString() == "Positive")
            ddlResponse.Attributes.Add("class", "hot");
        if (ddlResponse.SelectedValue.ToString() == "Negative")
            ddlResponse.Attributes.Add("class", "cold");
        if (ddlResponse.SelectedValue.ToString() == "Normal")
            ddlResponse.Attributes.Add("class", "warm");
        txtNextDate.Focus();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        clState.xmlCity(ddlCity, ddlState.SelectedItem.Text, "XMLState.xml");
        ddlCity.Focus();
    }
    protected void DdlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedValue == "Civil")
        {
            ddlPart.Visible = true;
            lblPart.Visible = true;
        }
        else if (ddlCourse.SelectedValue == "Architecture")
        {
            ddlPart.Visible = true;
            lblPart.Visible = true;
        }
        else if (ddlCourse.SelectedValue == "AutoCAD")
        {
            ddlPart.Visible = false;
            lblPart.Visible = false;
        }
        ddlCourse.Focus();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
      

    }
}
