using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Administrator_IMBuilding : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    ClsStateCity statecity = new ClsStateCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");

                invisible.Visible = true; visisble.Visible = false;
            }
            if (Request.QueryString["id"].ToString() == "") { invisible.Visible = true; visisble.Visible = false; }
            else
            {
                visisble.Visible = true; invisible.Visible = false;
                lblEnrolment.Text = Request.QueryString["id"].ToString();
            }
            if (!IsPostBack)
            {
                txtothercity.Visible = false; lblcity.Visible = false;
                statecity.xmlstate(ddlstate, "XMLState.xml");
                statecity.xmlCity(ddlcity, ddlstate.SelectedItem.Text.ToString(), "XMLState.xml");
                txtName.Focus();
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
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            string strid = idcenter();
            string stn = "insert into Building(ID,Name,BName,Type,PArea,CArea,ClassRoom,CRCapacity,CRArea,LabsNo,LCapicity,LArea,CompLabs,CompLabsCapacity,CompLabsArea,Library,LibCapacity,LibArea,Reception,RecCapacity,RecArea,AdminBlock,BlockCapacity,BlockArea,OtherNo,OCapacity,OArea,Address,City,State,Pin,Phone,Fax,Mobile,Email,ToSeat)values(@ID,@Name,@BName,@Type,@PArea,@CArea,@ClassRoom,@CRCapacity,@CRArea,@LabsNo,@LCapicity,@LArea,@CompLabs,@CompLabsCapacity,@CompLabsArea,@Library,@LibCapacity,@LibArea,@Reception,@RecCapacity,@RecArea,@AdminBlock,@BlockCapacity,@BlockArea,@OtherNo,@OCapacity,@OArea,@Address,@City,@State,@Pin,@Phone,@Fax,@Mobile,@Email,@ToSeat)";
            SqlCommand cmd = new SqlCommand(stn, con);
            cmd.Parameters.AddWithValue("@ID",lblEnrolment.Text.ToString());
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@BName", txtBName.Text);
            cmd.Parameters.AddWithValue("@Type", ddlBType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@PArea", txtBPArea.Text.ToString());
            cmd.Parameters.AddWithValue("@CArea", txtBCArea.Text.ToString());
            cmd.Parameters.AddWithValue("@ClassRoom", txtBNRoom1.Text.ToString());
            cmd.Parameters.AddWithValue("@CRCapacity", txtBSCapacity1.Text.ToString());
            cmd.Parameters.AddWithValue("@CRArea", txtBTotal1.Text.ToString());
            cmd.Parameters.AddWithValue("@LabsNo", txtBNRoom2.Text.ToString());
            cmd.Parameters.AddWithValue("@LCapicity", txtBSCapacity2.Text.ToString());
            cmd.Parameters.AddWithValue("@LArea", txtBTotal2.Text.ToString());
            cmd.Parameters.AddWithValue("@CompLabs", txtBNRoom3.Text.ToString());
            cmd.Parameters.AddWithValue("@CompLabsCapacity", txtBSCapacity3.Text.ToString());
            cmd.Parameters.AddWithValue("@CompLabsArea", txtBTotal3.Text.ToString());
            cmd.Parameters.AddWithValue("@Library", txtBNRoom4.Text.ToString());
            cmd.Parameters.AddWithValue("@LibCapacity", txtBSCapacity4.Text.ToString());
            cmd.Parameters.AddWithValue("@LibArea", txtBTotal4.Text.ToString());
            cmd.Parameters.AddWithValue("@Reception", txtBNRoom5.Text.ToString());
            cmd.Parameters.AddWithValue("@RecCapacity", txtBSCapacity5.Text.ToString());
            cmd.Parameters.AddWithValue("@RecArea", txtBTotal5.Text.ToString());
            cmd.Parameters.AddWithValue("@AdminBlock", txtBNRoom6.Text.ToString());
            cmd.Parameters.AddWithValue("@BlockCapacity", txtBSCapacity6.Text.ToString());
            cmd.Parameters.AddWithValue("@BlockArea", txtBTotal6.Text.ToString());
            cmd.Parameters.AddWithValue("@OtherNo", txtBNRoom7.Text.ToString());
            cmd.Parameters.AddWithValue("@OCapacity", txtBSCapacity7.Text.ToString());
            cmd.Parameters.AddWithValue("@OArea", txtBTotal7.Text.ToString());
            cmd.Parameters.AddWithValue("@Address", txtPAddress.Text.ToString());
            if(ddlcity.SelectedValue=="Other")
            cmd.Parameters.AddWithValue("@City", txtothercity.Text.ToString());
            else cmd.Parameters.AddWithValue("@City", ddlcity.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@State", ddlstate.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Pin", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("Phone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("ToSeat","");
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            lblException.Text = "IM Building Info saved";
            btnSave.Enabled = false;
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
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
    protected void txtPPincode_TextChanged(object sender, EventArgs e)
    {
        txtPhonecode.Focus();

    }
    protected void txtothercity_TextChanged(object sender, EventArgs e)
    {
        txtPPincode.Focus();
    }
    private string idcenter()
    {
            string id;
            try
            {
                SqlCommand cmdsn = new SqlCommand("select Max(SN) from Building", con);
               
                int i = Convert.ToInt32(cmdsn.ExecuteScalar().ToString());
                i = i + 1;
                if (i <= 9)
                {

                    id = "0" + i;
                }
                else if (i <= 99)
                {
                    id = "" + i;
                }

                else
                {
                    id = Convert.ToString(i + 1);
                }
            }
            catch (FormatException ex)
            {
                id = "1";
            }
        return id;
    }
    protected void btnCencel_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void lbbtnIMHeadTitel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMHead.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnRegistretitel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        statecity.xmlCity(ddlcity, ddlstate.SelectedItem.Text.ToString(), "XMLState.xml");
        ddlcity.Focus();
    }
}

