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
using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Linq;

public partial class Exam_ViewExamSponcer : System.Web.UI.Page
{
   
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
        }
        if (!IsPostBack)
        {
            txtsession.Text = DateTime.Now.Year.ToString();
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0) dropsession.SelectedValue = "Sum";
            else dropsession.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
            lblSeason.Text = dropsession.SelectedValue.ToString() + "" + txtsession.Text.ToString();
            dop2();
            dropsession.Focus();
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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        pan.Visible = true;
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("Select Name,ID,Type,Designation,CenterCode,Address,City,State,Pincode,Phone,Mobile,Email From Invigilator where Session='" + lblSeason.Text + "' AND Type='" + droptype.Text.ToString() + "' AND ExamCenter='" + txtExamCentrName.Text.ToString()+ "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            pan.Visible = true;
            txtName.Text = dr["Name"].ToString();
            txtDEsignation.Text = dr["Designation"].ToString();
            txtcenterCode.Text = dr["CenterCode"].ToString();
            txtAddress1.Text = dr["Address"].ToString();
            txtPCity.Text = dr["City"].ToString();
            txtPState.Text = dr["State"].ToString();
            txtPPincode.Text = dr["Pincode"].ToString();
            txtPhoneNo.Text = dr["Phone"].ToString();
            txtMobile.Text = dr["Mobile"].ToString();
            txtEmail.Text = dr["Email"].ToString();

        }
        else
            pan.Visible = false;
        dr.Close(); dr.Dispose();
        con.Close();
        con.Dispose();
    }
    protected void dropsession_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    private void dop2()
    {
        SqlDataAdapter ad3 = new SqlDataAdapter("select DISTINCT ExamCenter from Invigilator where Session='"+lblSeason.Text+"' and Type='"+droptype.SelectedValue.ToString()+"'", con);
        DataTable dt3 = new DataTable();
        ad3.Fill(dt3);
        txtExamCentrName.DataSource = dt3;
        txtExamCentrName.DataTextField = "ExamCenter";
        txtExamCentrName.DataValueField = "ExamCenter";
        txtExamCentrName.DataBind();
    }
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        dop2();
    }
}
