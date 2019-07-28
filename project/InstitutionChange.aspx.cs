using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Globalization;
using System.Xml;

public partial class project_InstitutionChange : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    ClsStateCity clstate = new ClsStateCity(); DateTimeFormatInfo dtinfo = new DateTimeFormatInfo(); SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else {
                if (!IsPostBack)
                {
                    clstate.xmlstate(ddlState, "XMLState.xml");
                    clstate.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
                    Panel1.Visible = false; Panel2.Visible = true; nam();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ddlsession3_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSession.Text = ddlsession3.SelectedValue.ToString() + "" + txtSession3.Text.ToString();
    }
    protected void txtSession3_TextChanged(object sender, EventArgs e)
    {
        lblSession.Text = ddlsession3.SelectedValue.ToString() + "" + txtSession3.Text.ToString(); chkAPartII.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
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
            if (i == 0 | i == 1) Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString()); else if (i == 2) { Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString()); }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    public void nam()
    {
        con.Open();
        string strQr = "select Name from InstitutionReg";
        cmd = new SqlCommand(strQr, con);
        string strad= Convert.ToString(cmd.ExecuteScalar());
        if (strad == "") { btnshow.Enabled = false; }
        else { SqlDataAdapter ad1 = new SqlDataAdapter(strQr, con); DataTable dt = new DataTable(); ad1.Fill(dt); dropname.DataSource = dt; dropname.DataTextField = "Name"; dropname.DataValueField = "Name"; dropname.DataBind(); } con.Close(); BindEnrolment(); 
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        maikal dev = new maikal();
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        txtName.Focus(); con.Close(); con.Open();
        cmd = new SqlCommand("select ID,Name,Address,Address2,City,State,Pincode,Contact,Mobile,Email,RegisDate,Session,Type,CivilPartII,CivilSectionB,ArchiPartII,ArchiSectionB,Person,Designation,HOD from InstitutionReg where Name='" + dropname.SelectedItem.Text.ToString() + "' and ID='" + lblEnrolment.Text + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            Panel1.Visible = true;  Panel2.Visible = false;
            txtName.Text = dr["Name"].ToString(); txtAdd1.Text = dr["Address"].ToString(); txtAdd2.Text = dr["Address2"].ToString(); ddlCity.SelectedItem.Text = dr["City"].ToString();
            ddlState.SelectedItem.Text = dr["State"].ToString(); txtPPincode.Text = dr["Pincode"].ToString(); txtPhoneNo.Text = dr["Contact"].ToString(); txtMobile.Text = dr["Mobile"].ToString(); txtEmail.Text = dr["Email"].ToString();
            if (dr["RegisDate"] ==DBNull.Value)  txtreg.Text = "";  else  txtreg.Text = Convert.ToDateTime(dr["RegisDate"]).ToString("dd/MM/yyyy");
            string str1 = dr["Session"].ToString();  str1 = str1.Substring(0, 3); ddlsession.SelectedValue = str1;
            str1 = dr["Session"].ToString();  str1 = str1.Substring(3, 4); txtSession.Text = str1.ToString();
            string str2 = dr["Type"].ToString(); str2 = str2.Substring(0, 3); ddlsession3.SelectedValue = str2;
            str2 = dr["Type"].ToString(); str2 = str2.Substring(3, 4);  txtSession3.Text = str2.ToString();
            lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
            lblSession.Text = ddlsession3.SelectedValue.ToString() + "" + txtSession3.Text.ToString();
            string strchkCPartII = dr["CivilPartII"].ToString(); if (strchkCPartII == "YES") chkCPartII.Checked = true; else chkCPartII.Checked = false;
            string strchkCSectionB = dr["CivilSectionB"].ToString(); if (strchkCSectionB == "YES") chkCSectionB.Checked = true; else chkCSectionB.Checked = false;
            string strchkAPartII = dr["ArchiPartII"].ToString(); if (strchkAPartII == "YES") chkAPartII.Checked = true; else chkAPartII.Checked = false;
            string strchkASectionB = dr["ArchiSectionB"].ToString(); if (strchkASectionB == "YES") chkASectionB.Checked = true; else chkASectionB.Checked = false;
            txtPerson.Text = dr["Person"].ToString();
            txtDesig.Text = dr["Designation"].ToString();
            txtHOD.Text = dr["HOD"].ToString();
        }
        dr.Close(); con.Close(); con.Dispose();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            con.Close(); con.Open();
            if (txtName.Text != "" && txtAdd1.Text != "" && txtreg.Text != "")
            {
                cmd=new SqlCommand("update InstitutionReg set Name=@Name,Address=@Address,City=@City,State=@State,Pincode=@Pincode,Contact=@Contact,Mobile=@Mobile,Email=@Email,RegisDate=@RegisDate,Session=@Session,Type=@Type,CivilPartII=@CivilPartII,CivilSectionB=@CivilSectionB,ArchiPartII=@ArchiPartII,ArchiSectionB=@ArchiSectionB,Person=@Person,Designation=@Designation,HOD=@HOD where ID='" + lblEnrolment.Text + "'", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString()); cmd.Parameters.AddWithValue("@Address", txtAdd1.Text.ToString()); cmd.Parameters.AddWithValue("Address2",txtAdd2.Text.ToString()); cmd.Parameters.AddWithValue("@City", ddlCity.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@State", ddlState.SelectedValue.ToString()); cmd.Parameters.AddWithValue("@Pincode", txtPPincode.Text.ToString()); cmd.Parameters.AddWithValue("@Contact", txtPhoneNo.Text.ToString());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString()); cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString()); cmd.Parameters.AddWithValue("@RegisDate", Convert.ToDateTime(txtreg.Text, dtinfo));
                cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text.ToString()); cmd.Parameters.AddWithValue("@Type", lblSession.Text.ToString());
                if (chkCPartII.Checked == true) cmd.Parameters.AddWithValue("@CivilPartII", "YES"); else cmd.Parameters.AddWithValue("@CivilPartII", "NO");
                if (chkCPartII.Checked == true) cmd.Parameters.AddWithValue("@CivilSectionB", "YES"); else cmd.Parameters.AddWithValue("@CivilSectionB", "NO");
                if (chkCPartII.Checked == true) cmd.Parameters.AddWithValue("@ArchiPartII", "YES"); else cmd.Parameters.AddWithValue("@ArchiPartII", "NO");
                if (chkCPartII.Checked == true) cmd.Parameters.AddWithValue("@ArchiSectionB", "YES"); else cmd.Parameters.AddWithValue("@ArchiSectionB", "NO");
                cmd.Parameters.AddWithValue("Person", txtPerson.Text.ToString()); cmd.Parameters.AddWithValue("Designation", txtDesig.Text.ToString()); cmd.Parameters.AddWithValue("HOD", txtHOD.Text.ToString());
                cmd.ExecuteNonQuery();
                lblExceptionSave.Text = "[" + txtName.Text.ToString() + "] " + "Updated Successfully \n Ins. ID: [" + lblEnrolment.Text.ToString() + "]"; lblExceptionSave.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblExceptionSave.Text = "Please Enter Details"; lblExceptionSave.ForeColor = System.Drawing.Color.Green; txtName.Focus();
            }
            txtName.Text = ""; txtAdd1.Text = ""; txtAdd2.Text = ""; txtEmail.Text = ""; txtPhoneNo.Text = ""; txtreg.Text = ""; txtMobile.Text = ""; txtPPincode.Text = ""; txtPerson.Text = ""; txtDesig.Text = ""; txtHOD.Text = ""; chkAPartII.Checked = false; chkASectionB.Checked = false; chkCPartII.Checked = false; chkCSectionB.Checked = false;
        }
        catch (FormatException ex) { lblExceptionSave.Text = "Please Insert Valid Date."; }  catch (SqlException ex) { lblExceptionSave.Text = ex.ToString(); } finally { con.Close(); con.Dispose(); }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        clstate.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
    }
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        nam();
    }
    protected void dropname_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEnrolment(); Panel1.Visible = false; Panel2.Visible = true;
    }
    private void BindEnrolment()
    {
        con.Open(); cmd = new SqlCommand("select ID from InstitutionReg where Name='" + dropname.SelectedValue.ToString() + "'", con);  lblEnrolment.Text = Convert.ToString(cmd.ExecuteScalar()); con.Close();
    }
}