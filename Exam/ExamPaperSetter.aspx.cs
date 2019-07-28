using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Globalization;

public partial class Exam_ExamPaperSetter : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    private string strsn;
    ClsStateCity clsState = new ClsStateCity();
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
          if(!IsPostBack)
          {
              clsState.xmlstate(ddlPState, "XMLState.xml");
              clsState.xmlCity(ddlPCity, ddlPState.SelectedItem.Text, "XMLState.xml");
              clsState.xmlstate(ddlCState, "XMLState.xml");
              clsState.xmlCity(ddlCCity, ddlCState.SelectedItem.Text, "XMLState.xml");
            con.Close();
            con.Open();
            btnUpdate.Visible = false;
            //txtDOB.Text = DateTime.Now.ToString("dd/mm/yyyy");
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win"; 
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            SqlCommand cmd = new SqlCommand("select Max(SN) from PaperSetter", con);
            strsn =Convert.ToString(cmd.ExecuteScalar());
            if(strsn.ToString()=="")
            {
                SqlCommand cmdin=new SqlCommand("insert into PaperSetter(Name) values('ICE')",con);
                cmdin.ExecuteNonQuery();
            }
            
            txtID.Focus();
            con.Close();
            con.Dispose();
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void btnSave_click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            maikal mk = new maikal();
            strsn = mk.psid();
            SqlCommand cmd = new SqlCommand("insert into PaperSetter(Code,Name,Designation,PAddress,PCity,PState,PPin,CAddress,CCity,CState,CPin,Phone,Fax,Mobile,DOB,Email,Age,Education,Experience) values(@Code,@Name,@Designation,@PAddress,@PCity,@PState,@PPin,@CAddress,@CCity,@CState,@CPin,@Phone,@Fax,@Mobile,@DOB,@Email,@Age,@Education,@Experience)", con);
            cmd.Parameters.AddWithValue("@Code",strsn.ToString());
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Designation", txtDEsignation.Text.ToString());
            cmd.Parameters.AddWithValue("@PAddress", txtPAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@PState", ddlPState.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PCity", ddlPCity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PPin", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("@CAddress", txtCAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@CState", ddlCState.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@CCity", ddlCCity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@CPin", txtCPin.Text.ToString());
            cmd.Parameters.AddWithValue("@Phone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
            cmd.Parameters.AddWithValue("Mobile", txtMobile.Text.ToString()); cmd.Parameters.AddWithValue("@DOB", txtDOB.Text.ToString()); cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString()); cmd.Parameters.AddWithValue("@Age", txtAge.Text.ToString()); cmd.Parameters.AddWithValue("@Education", txtEducationQ.Text.ToString()); cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.ToString());
            cmd.ExecuteNonQuery();
            lblExcepiton.Text = "ID No.: " + strsn.ToString();
        }
        catch (SqlException ex)
        {
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    private DataRow dr;
    protected void btnShowID_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select * from PaperSetter where Code='" + txtID.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader[1].ToString() == "")
            {
                lblException.Text = "Invalid ID No.";
                lblExcepiton.Text = "Invalid ID No.";
                btnSave.Visible = true;
            }
            else
            {
                txtName.Text = reader[2].ToString();
                txtDEsignation.Text = reader[3].ToString();
                txtPAddress.Text = reader[4].ToString();
                ddlPState.SelectedItem.Text= reader[5].ToString();
                ddlPCity.SelectedItem.Text = reader[6].ToString();
                txtPPincode.Text = reader[7].ToString();
                txtCAddress.Text = reader[8].ToString();
                ddlCState.SelectedItem.Text = reader[9].ToString();
                ddlCCity.SelectedItem.Text = reader[10].ToString();
                txtCPin.Text = reader[11].ToString();
                txtPhoneNo.Text = reader[12].ToString();
                txtFaxNo.Text = reader[13].ToString();
                txtMobile.Text = reader[14].ToString();
                txtDOB.Text = reader[15].ToString();
                txtEmail.Text = reader[16].ToString();
                txtAge.Text = reader[17].ToString();
                txtEducationQ.Text = reader[18].ToString();
                txtExperience.Text = reader[19].ToString();
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
            reader.Close();
            reader.Dispose();
        }
        con.Close();
        con.Dispose();
    }
    protected void btnUpdate_click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update PaperSetter set Name=@Name,Designation=@Designation,PAddress=@PAddress,PCity=@PCity,PState=@PState,PPin=@PPin,CAddress=@CAddress,CCity=@CCity,CState=@CState,CPin=@CPin,Phone=@Phone,Fax=@Fax,Mobile=@Mobile,DOB=@DOB,Email=@Email,Age=@Age,Education=@Education,Experience=@Experience where Code='" + txtID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Designation", txtDEsignation.Text.ToString());
            cmd.Parameters.AddWithValue("@PAddress", txtPAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@PState", ddlPState.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PCity", ddlPCity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PPin", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("@CAddress", txtCAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@CState", ddlCState.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@CCity", ddlCCity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@CPin", txtCPin.Text.ToString());
            cmd.Parameters.AddWithValue("@Phone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
            cmd.Parameters.AddWithValue("Mobile", txtMobile.Text.ToString()); cmd.Parameters.AddWithValue("@DOB", txtDOB.Text.ToString()); cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString()); cmd.Parameters.AddWithValue("@Age", txtAge.Text.ToString()); cmd.Parameters.AddWithValue("@Education", txtEducationQ.Text.ToString()); cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.ToString());
            cmd.ExecuteNonQuery();
            btbClear.Text = "Refresh";
            lblExcepiton.Text = "ID No.: "+txtID.Text.ToString()+" Updated.";
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void ddlPState_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsState.xmlCity(ddlPCity, ddlPState.SelectedItem.Text, "XMLState.xml");
        ddlPCity.Focus();
    }
  
    protected void ddlCState_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsState.xmlCity(ddlCCity, ddlCState.SelectedItem.Text, "XMLState.xml");
        ddlCCity.Focus();
    }
   
    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            btnSave.Enabled = true;
            DateTime dt = Convert.ToDateTime(txtDOB.Text, dtinfo);
            int year = DateTime.Now.Year - dt.Year;

            txtAge.Text = year.ToString();
        }
        catch (FormatException ex)
        {
            lblExcepiton.Text = "DOB not in correct format";
            btnSave.Enabled = false;
        }
    }
}
