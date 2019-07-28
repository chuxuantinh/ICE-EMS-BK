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
using System.IO;
using System.Globalization;

public partial class Admission_EditAdmission : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    public static int[] imcradit;
    ClsStateCity clstate = new ClsStateCity();
    public int fee;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                clstate.xmlstate(ddlState, "XMLState.xml");
                clstate.xmlstate(ddlState2, "XMLState.xml");
                if(Session["sid"] == null | Session["sid"] == "")
                {
                }
                else
                {
                    showprofile(Session["sid"].ToString());
                }
                txtEnrolment.Focus();
            }
            BindCountry();
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
    private void BindCountry()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("countries.xml"));
        foreach (XmlNode node in doc.SelectNodes("//country"))
        {
            ddlNationality.Items.Add(new ListItem(node.InnerText, node.Attributes["code"].InnerText));
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
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
    public void showprofile(string str)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student where SID='" + str + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblEnrolment.Text = dr["SID"].ToString();
                txtName.Text = dr["Name"].ToString();
                ddlPrefix.SelectedValue= dr["Prifix"].ToString();
                txtFather.Text = dr["FName"].ToString();
                ddlgender.SelectedValue= dr["Gender"].ToString();
                txtMother.Text = dr["MName"].ToString();
                txtPAddress.Text = dr["PAddress"].ToString();
                txtPAddress2.Text = dr["PAddress2"].ToString();
                txtPCity.Text = dr["PCity"].ToString();
                ddlState.SelectedItem.Text = dr["PState"].ToString();
                CAddress.Text = dr["CAddress"].ToString();
                txtCAddress2.Text = dr["CAddress2"].ToString();
                txtCCity.Text = dr["CCity"].ToString();
                ddlState2.SelectedItem.Text = dr["CState"].ToString();
                txtPhoneNo.Text = dr["Phone"].ToString();
                txtMobile.Text = dr["Mobile"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                DateTime dob = Convert.ToDateTime(dr["DOB"].ToString());
                txtDOB.Text = dob.ToString("dd/MM/yyyy");
                txtAge.Text = dr["Age"].ToString();
                ddlCategory.Text = dr["Category"].ToString();
                txtAppNo.Text = dr["AppId"].ToString();
                lblApp.Visible = true;
                string strStream = dr["Stream"].ToString(); if (strStream == "Tech") { lblStrm.Visible = true; lblStream.Text = "Technician"; } else if (strStream == "Asso") lblStream.Text = "Associate"; else lblStream.Text = " Course Detials Not Submitted";
                string strCourse = dr["Course"].ToString(); if (strCourse == "Civil") { lblCor.Visible = true; lblCourse.Text = "Civil Engineering"; } else if (strCourse == "Architecture") lblCourse.Text = "Architecture Engineering"; else lblCourse.Text = "";
                lblPart.Text = dr["Part"].ToString();
                txtRemarks.Text = dr["Remarks"].ToString();
                txtExmpRemarks.Text = dr["ExmpRemarks"].ToString();
                string strExpStatus = dr["ExpStatus"].ToString(); if (strExpStatus == "yes") chkExp.Checked = true; else chkExp.Checked = false;
                string strDocStatus = dr["DocStatus"].ToString(); if (strDocStatus == "yes") chkDoc.Checked = true; else chkDoc.Checked = false;
                ddlAdmissionStatus.SelectedItem.Text = dr["AdmissionStatus"].ToString();
            }
            dr.Close();
        }
        catch (SqlException ex)
        {
            lblProfileExceptioN.Text = ex.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Session.Remove("sid");
        Session["sid"] = txtEnrolment.Text.ToString();
        showprofile(txtEnrolment.Text);
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void btnUPdateProfile_Click(object sender, EventArgs e)
    {
        if (txtName.Text == "" && txtFather.Text == "" && txtMother.Text == "" && txtPAddress.Text == "" && txtPAddress2.Text == "" && ddlState.SelectedItem.Text == "" &&  CAddress.Text == "" && ddlState2.SelectedItem.Text == "" &&  txtPhoneNo.Text == "" && txtMobile.Text == "" && txtEmail.Text == "" && txtDOB.Text == "" && txtAge.Text == "" && lblStream.Text=="" && lblCourse.Text=="" && lblPart.Text=="")
        {
            lblmessage.Text = "Please Fill Details.!"; lblProfileExceptioN.Text = "";
            lblmessage.Focus();
        }
        else
        {
            try
            {
                dtinfo.ShortDatePattern = "dd/MM/yyyy";
                dtinfo.DateSeparator = "/";
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("update Student set Name=@Name,Prifix=@Prifix,Gender=@Gender,FName=@FName,MName=@MName,PAddress=@PAddress,PAddress2=@PAddress2,PCity=@PCity,PState=@PState,CAddress=@CAddress,CCity=@CCity,CState=@CState,Phone=@Phone,Mobile=@Mobile,Email=@Email,DOB=@DOB,Age=@Age,Nationality=@Nationality,Category=@Category,AppId=@AppID,CAddress2=@CAddress2,Remarks=@Remarks,ExmpRemarks=@ExmpRemarks,ExpStatus=@ExpStatus,DocStatus=@DocStatus,AdmissionStatus=@AdmissionStatus where SID='" + lblEnrolment.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
                cmd.Parameters.AddWithValue("Prifix", ddlPrefix.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Gender",ddlgender.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@FName", txtFather.Text.ToString());
                cmd.Parameters.AddWithValue("@MName", txtMother.Text.ToString());
                cmd.Parameters.AddWithValue("@PAddress", txtPAddress.Text.ToString());
                cmd.Parameters.AddWithValue("@PAddress2", txtPAddress2.Text.ToString());
                cmd.Parameters.AddWithValue("@PCity", txtPCity.Text.ToString());
                cmd.Parameters.AddWithValue("@PState", ddlState.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CAddress", CAddress.Text.ToString());
                cmd.Parameters.AddWithValue("@CAddress2", txtCAddress2.Text.ToString());
                cmd.Parameters.AddWithValue("@CCity", txtCCity.Text.ToString());
                cmd.Parameters.AddWithValue("@CState",ddlState2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text.ToString());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString()); cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(txtDOB.Text.ToString(),dtinfo));
                if (txtAge.Text == "") txtAge.Text = "0";
                cmd.Parameters.AddWithValue("@Age", Convert.ToDecimal(txtAge.Text)); cmd.Parameters.AddWithValue("@Nationality", ddlNationality.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Category", ddlCategory.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@AppId", txtAppNo.Text.ToString());
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                cmd.Parameters.AddWithValue("@ExmpRemarks", txtExmpRemarks.Text.ToString());
                if (chkExp.Checked == true) cmd.Parameters.AddWithValue("@ExpStatus","yes");
                else cmd.Parameters.AddWithValue("@ExpStatus","no");
                if (chkDoc.Checked == true) cmd.Parameters.AddWithValue("@DocStatus","yes");
                else cmd.Parameters.AddWithValue("@DocStatus","no");
                cmd.Parameters.AddWithValue("@AdmissionStatus", ddlAdmissionStatus.SelectedItem.Text);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Student Profile Updated Successfully!')", true);
                txtName.Text = "";
                txtFather.Text = ""; txtMobile.Text = ""; txtMother.Text = ""; txtPAddress.Text = "";
                CAddress.Text = ""; txtPhoneNo.Text = ""; txtPCity.Text = ""; txtCCity.Text = "";
                txtEmail.Text = ""; txtAge.Text = ""; txtDOB.Text = ""; txtAppNo.Text = ""; lblStream.Text = ""; lblCourse.Text = ""; lblPart.Text = ""; txtRemarks.Text = ""; txtExmpRemarks.Text = ""; txtPAddress2.Text = ""; txtCAddress2.Text = "";
            }
            catch (SqlException ex)
            {
                lblProfileExceptioN.Text = ex.ToString();
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string url;
        url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
}