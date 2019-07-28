using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;

public partial class Exam_ExamSponsor : System.Web.UI.Page
{
    
 SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
 ClsStateCity C1 = new ClsStateCity(); SqlCommand cmd;
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
            }
            if (!IsPostBack)
            {
                
                C1.xmlstate(ddlState, "XMLState.xml");
                C1.xmlCity(ddlCity, ddlState.SelectedItem.Text, "XMLState.xml");
                dropsession.Focus();
                string se = Request.QueryString["session"];
                string fw = se.Substring(0, 1);
                if (fw == "W")
                {
                    dropsession.SelectedIndex = 1;
                }
                else
                {
                    dropsession.SelectedIndex = 0;
                }
                string lw = se.Substring(3, 4);
                txtsession.Text = lw.ToString();
                lblSeason.Text = dropsession.SelectedItem.Value + txtsession.Text;
                ddlExamCenterName.DataBind();
                if (ddlExamCenterName.Text!= null || ddlExamCenterName.Text != "")
                {
                    code();
                }
                else
                {
                    if (Request.QueryString["Id"] != "")
                    {
                        string str = "select Name from Examcenter where Season='" + lblSeason.Text + "' and Id='" + Request.QueryString["Id"] + "'";
                        cmd = new SqlCommand(str, con);
                        con.Open();
                        str = Convert.ToString(cmd.ExecuteScalar());
                        con.Close();
                        ddlExamCenterName.Text = str;
                        txtcenterCode.Text = Request.QueryString["id"].ToString();

                    }
               
                }
              
                lblsession.Text = dropsession.SelectedItem.Value + txtsession.Text;
                
               
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
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblException.Text = "";
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd/MM/yyyy";
        dtfi.DateSeparator = "/";
        con.Close();
        con.Open(); string id = gidd(); lblsession.Text = dropsession.Text + txtsession.Text; lblcode.Text = droptype.SelectedValue.ToString() + " " + "Code is:" + " " + id;
        SqlCommand cmd1 = new SqlCommand("insert into Invigilator(Name,ID,Type,Designation,ExamCenter,CenterCode,Address,City,State,Pincode,Phone,Mobile,Email,Session,Date)Values(@Name,@ID,@Type,@Designation,@ExamCenter,@CenterCode,@Address,@City,@State,@Pincode,@Phone,@Mobile,@Email,@Session,@Date)", con);
        cmd1.Parameters.AddWithValue("Name", txtName.Text.ToString());
        cmd1.Parameters.AddWithValue("ID",id);
        cmd1.Parameters.AddWithValue("Type",droptype.SelectedValue.ToString());
        cmd1.Parameters.AddWithValue("Designation",txtDEsignation.Text);
        cmd1.Parameters.AddWithValue("ExamCenter",ddlExamCenterName.SelectedValue.ToString());
        cmd1.Parameters.AddWithValue("CenterCode", txtcenterCode.Text.ToString());
        cmd1.Parameters.AddWithValue("Address",txtAddress1.Text+" "+txtAddress2.Text);
        cmd1.Parameters.AddWithValue("City",ddlCity.SelectedValue.ToString());
        cmd1.Parameters.AddWithValue("State",ddlState.SelectedValue.ToString());
        cmd1.Parameters.AddWithValue("Pincode",txtPPincode.Text.ToString());
        cmd1.Parameters.AddWithValue("Phone", txtPhonecode.Text+txtPhoneNo.Text);
        cmd1.Parameters.AddWithValue("Mobile",txtMobile.Text.ToString());
        cmd1.Parameters.AddWithValue("Email",txtEmail.Text.ToString());
        cmd1.Parameters.AddWithValue("Session",lblSeason.Text.ToString());
        cmd1.Parameters.AddWithValue("Date", DateTime.Now.ToString());
        cmd1.ExecuteNonQuery();
        btnSave.Enabled = false;
        lblException.Text = "Successfully Saved";
        con.Close();
        con.Dispose();
    }
    public string gidd()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(SN) from Invigilator", con);
        int i = 0;
        string id = Convert.ToString(cmdsn.ExecuteScalar().ToString());
        if (id == "")
        {
            i = 1;
        }
        else
        {
            i = Convert.ToInt32(id.ToString());
            i = i + 1;
        }
        if(i==1)
       id = Convert.ToString(i + 1);
    if (i <= 9)
    {

        id = "000" + i;
    }
    else if (i <= 99)
    {
        id = "00" + i;
    }
    else if (i <= 999)
    {
        id = "0" + i;
    }
        id = id.ToString();
        return id;
    }
    protected void btbClear_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void ddlExamCenter_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        code();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        C1.xmlCity(ddlCity, ddlState.SelectedItem.Text, "XMLState.xml");
    }
   private void code()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select ID from ExamCenter where Name='" + ddlExamCenterName.Text.ToString() + "'", con);
            txtcenterCode.Text = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            con.Dispose();
    }
   protected void dropsession_SelectedIndexChanged(object sender, EventArgs e)
   {
       lblsession.Text = dropsession.SelectedItem.Value + txtsession.Text;
   }
}
            