using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class project_ManageFees : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd; 
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
                    Panel1.Visible = false; Panel2.Visible = true; nam(); dropname.Focus();
                }
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
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
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void dropname_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEnrolment(); Panel1.Visible = false; Panel2.Visible = true;
    }
    public void nam()
    {
        con.Open();
        string strQr = "select Name from InstitutionReg";
        cmd = new SqlCommand(strQr, con);
        string strad = Convert.ToString(cmd.ExecuteScalar());
        if (strad == "") { btnshow.Enabled = false; }
        else { SqlDataAdapter ad1 = new SqlDataAdapter(strQr, con); DataTable dt = new DataTable(); ad1.Fill(dt); dropname.DataSource = dt; dropname.DataTextField = "Name"; dropname.DataValueField = "Name"; dropname.DataBind(); } con.Close(); BindEnrolment();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        maikal dev = new maikal();
        con.Close(); con.Open();
        cmd = new SqlCommand("select CivilIITraining,CivilBTraining,ArchiIITraining,ArchiBTraining,CivilIIGuidance,CivilBGuidance,ArchiIIGuidance,ArchiBGuidance from InstitutionReg where Name='" + dropname.SelectedItem.Text.ToString() + "' and ID='" + lblEnrolment.Text + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            Panel1.Visible = true; Panel2.Visible = false;
            txtTCPartII.Text = dr["CivilIITraining"].ToString().TrimEnd('0').TrimEnd('.');
            txtTCSectionB.Text = dr["CivilBTraining"].ToString().TrimEnd('0').TrimEnd('.');
            txtTArchiPartII.Text = dr["ArchiIITraining"].ToString().TrimEnd('0').TrimEnd('.');
            txtTArchiSectionB.Text = dr["ArchiBTraining"].ToString().TrimEnd('0').TrimEnd('.');
            txtGCPartII.Text = dr["CivilIIGuidance"].ToString().TrimEnd('0').TrimEnd('.');
            txtGCSectionB.Text = dr["CivilBGuidance"].ToString().TrimEnd('0').TrimEnd('.');
            txtGArchiPartII.Text = dr["ArchiIIGuidance"].ToString().TrimEnd('0').TrimEnd('.');
            txtGArchiSectionB.Text = dr["ArchiBGuidance"].ToString().TrimEnd('0').TrimEnd('.');

        }
        dr.Close(); con.Close(); con.Dispose(); txtTCPartII.Focus();
    }
    private void BindEnrolment()
    {
        con.Open(); cmd = new SqlCommand("select ID from InstitutionReg where Name='" + dropname.SelectedValue.ToString() + "'", con); lblEnrolment.Text = Convert.ToString(cmd.ExecuteScalar()); con.Close();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lblEnrolment.Text == "") { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('ID not Found!')", true); Panel1.Visible = false; Panel2.Visible = true; dropname.Focus(); }
        else
        {
            con.Open();
            cmd = new SqlCommand("update InstitutionReg set CivilIITraining=@CivilIITraining,CivilBTraining=@CivilBTraining,ArchiIITraining=@ArchiIITraining,ArchiBTraining=@ArchiBTraining,CivilIIGuidance=@CivilIIGuidance,CivilBGuidance=@CivilBGuidance,ArchiIIGuidance=@ArchiIIGuidance,ArchiBGuidance=@ArchiBGuidance where Name='" + dropname.SelectedItem.Text.ToString() + "' and ID='" + lblEnrolment.Text + "'", con);
            cmd.Parameters.AddWithValue("CivilIITraining", txtTCPartII.Text);
            cmd.Parameters.AddWithValue("CivilBTraining", txtTCSectionB.Text);
            cmd.Parameters.AddWithValue("ArchiIITraining", txtTArchiPartII.Text);
            cmd.Parameters.AddWithValue("ArchiBTraining", txtTArchiSectionB.Text);
            cmd.Parameters.AddWithValue("CivilIIGuidance", txtGCPartII.Text);
            cmd.Parameters.AddWithValue("CivilBGuidance", txtGCSectionB.Text);
            cmd.Parameters.AddWithValue("ArchiIIGuidance", txtGArchiPartII.Text);
            cmd.Parameters.AddWithValue("ArchiBGuidance", txtGArchiSectionB.Text);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Fees Updated Successfully!')", true);
        }
        Panel1.Visible = false; Panel2.Visible = true; dropname.Focus();
    }
}