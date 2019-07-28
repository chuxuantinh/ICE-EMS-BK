using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;

using iTextSharp.text.pdf;

using iTextSharp.text.html;

using iTextSharp.text.html.simpleparser;
public partial class Administrator_IMProfile : System.Web.UI.Page
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
                lblEnrolment.Text = Request.QueryString["id"].ToString();
                getdata();
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
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + txtEnrolment.Text.ToString() + "'", con);
        string strstatus = Convert.ToString(cmd.ExecuteScalar());
        if (strstatus == txtEnrolment.Text.ToString())
        {
            lblEnrolment.Text = txtEnrolment.Text.ToString();
            Response.Redirect("IMRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + lblEnrolment.Text.ToString());
        }
        else if (strstatus != txtEnrolment.Text.ToString())
        {
            txtEnrolment.Text = "Invalid IM ID.";
        }
        con.Close();
        con.Dispose();
    }

    protected void GridDuplicacy_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;
        rw=GridDuplicacy.SelectedRow;
      
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect("IMProfile.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" +rw.Cells[2].Text.ToString());
       
        
    }
    private void getdata()
    {
        if (Request.QueryString["id"].ToString() == "")
        {

        }
        else
        {
            con.Close(); con.Open();

            SqlCommand cmd = new SqlCommand("select * from IM where ID='" + lblEnrolment.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtName.Text = reader["Name"].ToString();
                lblGID.Text = reader["GID"].ToString();
                txtPAddress.Text = reader["PAddress"].ToString();
                txtAddress2.Text = reader["Address2"].ToString();
                txtPCity.Text = reader["PCity"].ToString();
                txtPState.Text = reader["PState"].ToString();
                txtPPincode.Text = reader["PPinCode"].ToString();
               txtCAddress.Text=reader["RAddress"].ToString();
               txtCCity.Text = reader["RCity"].ToString();
               txtCState.Text = reader["RState"].ToString();
               txtCPin.Text = reader["RPinCode"].ToString();
               txtPhonecode.Text = reader["Phone"].ToString();
               txtFaxCode.Text = reader["Fax"].ToString();
               txtMobile.Text = reader["Mobile"].ToString();
               txtEmail.Text = reader["Email"].ToString();
               if (reader["RemArea"].ToString() == "No") lblRemoteArea.Text = "Not Remote Area";
               if (reader["RemArea"].ToString() == "Yes") lblRemoteArea.Text = "Remote Area";
               if (reader["EAccess"].ToString() == "No") lblEassayAccess.Text = "Not Easily Access";
               if (reader["EAccess"].ToString() == "Yes") lblEassayAccess.Text = "Easily Access";
               if (reader["ResArea"].ToString() == "No") lblResidentialArea.Text = " Not Residential Area";
               if (reader["ResArea"].ToString() == "Yes") lblResidentialArea.Text = "Residential Area";
               if (reader["ComArea"].ToString() == "No") lblCommArea.Text = "Not Commercial Area";
               if (reader["ComArea"].ToString() == "Yes") lblCommArea.Text = "Commercial Area";
               if (reader["InCity"].ToString() == "No") lblWithCity.Text = "Out of City";
               if (reader["InCity"].ToString() == "Yes") lblWithCity.Text = "With In City";
               txtDRStn.Text = reader["Railway"].ToString();
               txtNCity.Text = reader["ToCity"].ToString();
               txtBStop.Text = reader["BusStop"].ToString();
               txtNArea.Text = reader["ToBusArea"].ToString();
               txtYEstablishment.Text = reader["Estd"].ToString();
               txtASInstitution.Text = reader["AcademicStatus"].ToString();
               txttypeig.Text = reader["InsType"].ToString();
               txtCRecognizedby.Text = reader["CourseConduct"].ToString();
               txtNSPresent.Text = reader["StdNo"].ToString();
               
                
            }
            reader.Close(); reader.Dispose();
            con.Close();
            
        }

    }
}
