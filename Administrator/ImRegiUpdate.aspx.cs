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

public partial class Administrator_ImRegiUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
   ClsStateCity statescity = new ClsStateCity();
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
                if (Request.QueryString["id"].ToString() != "")
                {


                }
                else
                {


                }
                if (!IsPostBack)
                {
                    txtothercity.Visible = false; lblcity.Visible = false;
                    txtOther.Visible = false; lblOtherCITy.Visible = false;
                    statescity.xmlstate(ddlState, "XMLState.xml");
                    statescity.xmlCity(ddlcity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
                    statescity.xmlstate(ddlstate1, "XMLState.xml");
                    statescity.xmlCity(ddlcity1, ddlstate1.SelectedItem.Text.ToString(), "XMLState.xml");
                    fetchdata();
                    txtName.Focus();
                }


            }

            check();
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
                // Response.Redirect("Admin/SuperAdminManage.aspx");

                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }



    private void fetchdata()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select Name,PAddress,PCity,PState,PPincode,RAddress,RCity,RState,RPincode,Phone,Fax,Mobile,Email,RemArea,EAccess,ResArea,ComArea,InCity,OutCity,Airport,ToCountry,Railway,ToCity,BusStop,ToBusArea,Estd,AcademicStatus,InsType,CourseConduct,StdNo,RegDate,Active,DisActiveDate,GID,PandingDate,Address2 from IM where ID ='" + lblEnrolment.Text + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtName.Text = dr["Name"].ToString();
            txtPAddress.Text = dr["PAddress"].ToString();
            ddlcity.SelectedItem.Text = dr["PCity"].ToString();
            ddlState.Text = dr["PState"].ToString();
            txtPPincode.Text = dr["PPincode"].ToString();
            txtCAddress.Text = dr["RAddress"].ToString();
            ddlcity1.SelectedItem.Text = dr["RCity"].ToString();
            ddlstate1.Text = dr["RState"].ToString();
            txtCPin.Text = dr["RPincode"].ToString();
            txtPhoneNo.Text = dr["Phone"].ToString();
            txtFaxNo.Text = dr["Fax"].ToString();
            txtMobile.Text = dr["Mobile"].ToString();
            txtEmail.Text = dr["Email"].ToString();
            if (dr["RemArea"].ToString() == "Yes")
            {
                chkRArea.Checked = true;
            }

            else if (dr["EAccess"].ToString() == "Yes")
            {
                chkEAccessible.Checked = true;
            }
            else if (dr["ResArea"].ToString() == "Yes")
            {
                chkResidential.Checked = true;
            }
            else if (dr["ComArea"].ToString() == "Yes")
            {
                chkCommercial.Checked = true;
            }
            else if (dr["InCity"].ToString() == "Yes")
            {
                chkWCity.Checked = true;
            }
            else if (dr["OutCity"].ToString() == "Yes")
            {
                chkOCity.Checked = true;
            }

            txtDRStn.Text = dr["Railway"].ToString();
            txtNCity.Text = dr["ToCity"].ToString();
            txtBStop.Text = dr["BusStop"].ToString();
            txtNArea.Text = dr["ToBusArea"].ToString();
            txtYEstablishment.Text = dr["Estd"].ToString();
            txtASInstitution.Text = dr["AcademicStatus"].ToString();
            txttypeig.Text = dr["InsType"].ToString();
            txtCRecognizedby.Text = dr["CourseConduct"].ToString();
            txtNSPresent.Text = dr["StdNo"].ToString();
            lblGInfo.Text = dr["GID"].ToString();
            txtAddress2.Text = dr["Address2"].ToString();
        }
        con.Close(); con.Dispose();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            con.Close();
            string remotearea;
            if (chkRArea.Checked == true)
            {
                remotearea = "Yes";
            }
            else
            {
                remotearea = "No";
            }
            string easilyaccessible;
            if (chkEAccessible.Checked == true)
            {
                easilyaccessible = "Yes";
            }
            else
            {
                easilyaccessible = "No";
            }
            string residentialarea;
            if (chkResidential.Checked == true)
            {
                residentialarea = "Yes";
            }
            else
            {
                residentialarea = "No";
            }
            string commercialarea;
            if (chkCommercial.Checked == true)
            {
                commercialarea = "Yes";
            }
            else
            {
                commercialarea = "No";
            }
            string withinthecity;
            if (chkWCity.Checked == true)
            {
                withinthecity = "Yes";
            }
            else
            {
                withinthecity = "No";
            }
            string outskirtsofthecity;
            if (chkOCity.Checked == true)
            {
                outskirtsofthecity = "Yes";
            }
            else
            {
                outskirtsofthecity = "No";
            }

            con.Open();
            string stn = "update IM set Name=@Name,PAddress=@PAddress,PCity=@PCity,PState=@PState,PPincode=@PPincode,RAddress=@RAddress,RCity=@RCity,RState=@RState,RPincode=@RPincode,Phone=@Phone,Fax=@Fax,Mobile=@Mobile,Email=@Email,RemArea=@RemArea,EAccess=@EAccess,ResArea=@ResArea,ComArea=@ComArea,InCity=@InCity,OutCity=@OutCity,Airport=@Airport,ToCountry=@ToCountry,Railway=@Railway,ToCity=@ToCity,BusStop=@BusStop,ToBusArea=@ToBusArea,Estd=@Estd,AcademicStatus=@AcademicStatus,InsType=@InsType,CourseConduct=@CourseConduct,StdNo=@StdNo,GID=@GID,Address2=@Address2 where ID='" + lblEnrolment.Text + "'";
            SqlCommand cmd = new SqlCommand(stn, con);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@PAddress", txtPAddress.Text.ToString());
            if (ddlcity.SelectedValue == "Other")
            {
                cmd.Parameters.AddWithValue("@PCity", txtothercity.Text.ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@PCity", ddlcity.SelectedItem.Text.ToString());
            }
            //cmd.Parameters.AddWithValue("@PCity", ddlcity.Text.ToString());
            cmd.Parameters.AddWithValue("@PState", ddlState.Text.ToString());
            cmd.Parameters.AddWithValue("@PPincode", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("@RAddress", txtCAddress.Text.ToString());
            if (ddlcity1.SelectedValue == "Other")
            {
                cmd.Parameters.AddWithValue("@RCity", txtOther.Text.ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@RCity", ddlcity1.SelectedItem.Text.ToString());
            }
            //cmd.Parameters.AddWithValue("@RCity", ddlcity1.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@RState", ddlstate1.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@RPincode", txtCPin.Text.ToString());
            cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFaxNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@RemArea", remotearea.ToString());
            cmd.Parameters.AddWithValue("@EAccess", easilyaccessible.ToString());
            cmd.Parameters.AddWithValue("@ResArea", residentialarea.ToString());
            cmd.Parameters.AddWithValue("@ComArea", commercialarea.ToString());
            cmd.Parameters.AddWithValue("@InCity", withinthecity.ToString());
            cmd.Parameters.AddWithValue("@OutCity", outskirtsofthecity.ToString());
            cmd.Parameters.AddWithValue("@Airport", "");
            cmd.Parameters.AddWithValue("@ToCountry", "");
            cmd.Parameters.AddWithValue("@Railway", txtDRStn.Text.ToString());
            cmd.Parameters.AddWithValue("@ToCity", txtNCity.Text.ToString());
            cmd.Parameters.AddWithValue("@BusStop", txtBStop.Text.ToString());
            cmd.Parameters.AddWithValue("@ToBusArea", txtNArea.Text.ToString());
            cmd.Parameters.AddWithValue("@Estd", txtYEstablishment.Text.ToString());
            cmd.Parameters.AddWithValue("@AcademicStatus", txtASInstitution.Text.ToString());
            cmd.Parameters.AddWithValue("@InsType", txttypeig.Text.ToString());
            cmd.Parameters.AddWithValue("@CourseConduct", txtCRecognizedby.Text.ToString());

            cmd.Parameters.AddWithValue("@StdNo", txtNSPresent.Text.ToString());


            cmd.Parameters.AddWithValue("@GID", lblGInfo.Text.ToString());

            cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            lblerror.Text = "update";
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.ToString();
        }





    }
    public void check()
    {
        if (chkSameAddress.Checked == true)
        {
            txtCAddress.Text = txtPAddress.Text.ToString() + " " + txtAddress2.Text.ToString();
            ddlcity1.SelectedItem.Text = ddlcity.SelectedItem.Text.ToString();
            ddlstate1.SelectedItem.Text = ddlState.Text.ToString();
            txtCPin.Text = txtPPincode.Text.ToString();
        }
    }

    protected void btnViewEnroll_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + txtEnrolment.Text.ToString() + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Response.Redirect("../Administrator/ImRegiUpdate.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + txtEnrolment.Text.ToString() + "");
        }
        else
        {
            txtEnrolment.Text = "Invalid Id";
            lblgroupid.Visible = false;
           
        }
        dr.Close();
        con.Close(); con.Dispose();
    }
    protected void txtEnrolment_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcity.Focus();
        if (ddlcity.SelectedItem.Text == "Other")
        {
            txtothercity.Visible = true;
            lblcity.Visible = true;
            txtothercity.Focus();
        }
       
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        statescity.xmlCity(ddlcity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
        ddlcity.Focus();
    }
    protected void ddlcity1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcity1.Focus();
        if (ddlcity1.SelectedItem.Text == "Other")
        {
            txtOther.Visible = true;
            lblOtherCITy.Visible = true;
            txtOther.Focus();
        }
       
    }
    protected void ddlstate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        statescity.xmlCity(ddlcity1, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
        ddlcity1.Focus();
    }
    protected void txtPPincode_TextChanged(object sender, EventArgs e)
    {
        chkSameAddress.Focus();
    }
    protected void txtothercity_TextChanged(object sender, EventArgs e)
    {
        txtPPincode.Focus();
    }
    protected void txtCPin_TextChanged(object sender, EventArgs e)
    {
        txtPhoneNo.Focus();
    }
    protected void txtOther_TextChanged(object sender, EventArgs e)
    {
        txtCPin.Focus();
    }
}
