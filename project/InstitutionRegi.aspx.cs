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

public partial class project_InstitutionRegi : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    ClsStateCity clstate = new ClsStateCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    clstate.xmlstate(ddlState, "XMLState.xml");
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum";
                    else ddlsession.SelectedValue = "Win";
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    if (se == 0) ddlsession3.SelectedValue = "Sum";
                    else ddlsession3.SelectedValue = "Win";
                    txtSession3.Text = DateTime.Now.Year.ToString();
                    lblSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    txtreg.Text = DateTime.Now.ToString("dd/MM/yyyy"); txtName.Focus();
                }
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
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
        DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("insert into InstitutionReg(ID,Name,Address,Address2,City,State,Pincode,Contact,Mobile,Email,RegisDate,Session,Type,CivilPartII,CivilSectionB,ArchiPartII,ArchiSectionB)Values(@ID,@Name,@Address,@Address2,@City,@State,@Pincode,@Contact,@Mobile,@Email,@RegisDate,@Session,@Type,@CivilPartII,@CivilSectionB,@ArchiPartII,@ArchiSectionB)", con);
            lblcode.Text = gidd();
            Label1.Visible = true;
            cmd.Parameters.AddWithValue("@ID", lblcode.Text.ToString());
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Address", txtPAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@Address2", txtAddressHead2.Text.ToString());
            cmd.Parameters.AddWithValue("@City", txtCity.Text.ToString());
            cmd.Parameters.AddWithValue("@State", ddlState.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@Pincode", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("@Contact", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@RegisDate", Convert.ToDateTime(txtreg.Text, dtinfo));
            cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", lblSession.Text.ToString());
            if (chkCPartII.Checked == true) cmd.Parameters.AddWithValue("@CivilPartII", "YES"); else cmd.Parameters.AddWithValue("@CivilPartII", "NO");
            if (chkCSectionB.Checked == true) cmd.Parameters.AddWithValue("@CivilSectionB", "YES"); else cmd.Parameters.AddWithValue("@CivilSectionB", "NO");
            if (chkAPartII.Checked == true) cmd.Parameters.AddWithValue("@ArchiPartII", "YES"); else cmd.Parameters.AddWithValue("@ArchiPartII", "NO");
            if (chkASectionB.Checked == true) cmd.Parameters.AddWithValue("@ArchiSectionB", "YES"); else cmd.Parameters.AddWithValue("@ArchiSectionB", "NO");
            cmd.ExecuteNonQuery();
            lblExceptionSave.Text = "[" + txtName.Text.ToString() + "] " + "Registered Successfully \n Inst. ID: [" + lblcode.Text.ToString() + "]";
            lblExceptionSave.ForeColor = System.Drawing.Color.Green;
            txtName.Text = ""; txtPAddress.Text = ""; txtCity.Text = "";
            txtEmail.Text = ""; txtPhonecode.Text = ""; txtPhoneNo.Text = "";
            txtMobile.Text = "";
            txtPPincode.Text = ""; chkCPartII.Checked = false; chkCSectionB.Checked = false; chkAPartII.Checked = false; chkASectionB.Checked = false;
            clstate.xmlstate(ddlState, "XMLState.xml");
            txtAddressHead2.Text = "";
            txtName.Focus();
        }
        catch (SqlException ex) { lblExceptionSave.Text = ex.ToString(); lblcode.Visible = false; Label1.Visible = false; }
        catch (FormatException ex) { lblExceptionSave.Text = "Please Enter Valid Date."; lblcode.Visible = false; Label1.Visible = false; txtreg.Focus(); }
        finally { con.Close(); con.Dispose(); }
    }
    public string gidd()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(ID) from InstitutionReg", con);
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
        else
        {
            id = Convert.ToString(i);
        }
        id = id.ToString();
        return id;
    }
    protected void ddlsession3_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSession.Text = ddlsession3.SelectedValue.ToString() + "" + txtSession3.Text.ToString();
    }
    protected void txtSession3_TextChanged(object sender, EventArgs e)
    {
        lblSession.Text = ddlsession3.SelectedValue.ToString() + "" + txtSession3.Text.ToString(); chkAPartII.Focus();
    }
}