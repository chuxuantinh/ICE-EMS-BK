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

public partial class Admission_AdmissionDepart : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEnrolment.Focus();
        if ((Session["sid"] == null) | (Session["sid"]=="")) { invisible.Visible = true; visisble.Visible = false;}
        else { visisble.Visible = true; invisible.Visible = false;
        if (!IsPostBack) 
        showprofile(Session["sid"].ToString());
        txtEnrolment.Focus();
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    public void showprofile(string str)
    {
        try
        {
            ViewImg(str);
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student where SID='" + str + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Status"].ToString() == "Active" | reader["Status"].ToString() == "Disactive")
                {
                    pnlProfile.Visible = true;
                    Panel1.Visible = false;
                    lblEnrolment.Text = reader["SID"].ToString();
                    txtName.Text = reader["Name"].ToString();
                    txtFather.Text = reader["FName"].ToString();
                    txtMother.Text = reader["MName"].ToString();
                    txtPAddress.Text = reader["PAddress"].ToString();
                    lblPAddress2.Text = reader["PAddress2"].ToString();
                    txtPCity.Text = reader["PCity"].ToString();
                    txtPState.Text = reader["PState"].ToString();
                  
                    txtCAddress.Text = reader["CAddress"].ToString();
                    txtCCity.Text = reader["CCity"].ToString();
                    txtCState.Text = reader["CState"].ToString();
                  
                    txtPhoneNo.Text = reader["Phone"].ToString();
                    txtMobile.Text = reader["Mobile"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    DateTime dob = Convert.ToDateTime(reader["DOB"].ToString());
                    txtDOB.Text = dob.ToString("dd/MM/yyyy");
                    txtAge.Text = reader["Age"].ToString();
                    ddlNationality.Text = reader["Nationality"].ToString();
                    ddlCategory.Text = reader["Category"].ToString();
                    txtIDIM.Text = reader["IMID"].ToString();
                    lblIMName.Text = reader["IMName"].ToString();
                    lblIMCity.Text = reader["IMCity"].ToString();
                    lblRegisDate.Text = reader["EnrollDate"].ToString();
                    lblExmpRemarks.Text = reader["ExmpRemarks"].ToString();
                    lblRemarks.Text = reader["Remarks"].ToString();
                    if (lblRegisDate.Text == "" | lblRegisDate.Text == null)
                        lblRegisDate.Text = "01/01/2008";
                    lblRegisDate.Text = Convert.ToDateTime(lblRegisDate.Text).ToString("dd/MM/yyyy");
                    lblAppID.Text = reader["AppId"].ToString();
                    string strStream = reader["Stream"].ToString(); if (strStream == "Tech") lblStream.Text = "Technician"; else if (strStream == "Asso") lblStream.Text = "Associate"; else lblStream.Text = " Course Detials Not Submitted";
                    string strCourse = reader["Course"].ToString(); if (strCourse == "Civil") lblNameCourse.Text = "Civil Engineering"; else if (strCourse == "Architecture") lblNameCourse.Text = "Architecture Engineering"; else lblNameCourse.Text = "";
                    lblPartNo.Text = reader["Part"].ToString();
                }
                else
                {
                    lblEnrolment.Text = "not active";
                    pnlProfile.Visible = false;
                    Panel1.Visible = true;
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            lblAppID.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            visisble.Visible = true; invisible.Visible = false;
        }
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
        catch (NullReferenceException EX)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Session.Remove("sid");
        Session["sid"] = txtEnrolment.Text.ToString();
        showprofile(txtEnrolment.Text);
    }

    public void ViewImg(string str)
    {
        SqlCommand command = new SqlCommand("SELECT ImageName,SID from [Docs] where SID='" + str + "'", con);
        SqlDataAdapter daimages = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }
}