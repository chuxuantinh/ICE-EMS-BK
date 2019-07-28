using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Administrator_BuildingView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
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
                buildupdate();
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
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
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {


    }

  

    private string idcenter()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(SN) from Building", con);
        con.Close();
        con.Open();
        string id;
        int i = Convert.ToInt32(cmdsn.ExecuteScalar().ToString());
        con.Close();
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

        return id;
    }
   
    protected void lbbtnIMHeadTitel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMHead.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnRegistretitel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }

    private void buildupdate()
    {
        if (lblEnrolment.Text != "")
        {
            try
            {
                con.Close();
                con.Open();
                string strbuild = "select Name,BName,Type,PArea,CArea,ClassRoom,CRCapacity,CRArea,LabsNo,LCapicity,LArea,CompLabs,CompLabsCapacity,CompLabsArea,Library,LibCapacity,LibArea,Reception,RecCapacity,RecArea,AdminBlock,BlockCapacity,BlockArea,OtherNo,OCapacity,OArea,Address,City,State,Pin,Phone,Fax,Mobile,Email,ToSeat from building where ID = '" + lblEnrolment.Text + "'";
                SqlCommand cmd = new SqlCommand(strbuild, con);
                SqlDataReader bdr;
                bdr = cmd.ExecuteReader();
                if (bdr.Read() == true)
                {
                    txtName.Text = bdr["Name"].ToString();
                    txtBName.Text = bdr["BName"].ToString();
                    ddlBType.Text = bdr["Type"].ToString();
                    txtBPArea.Text = bdr["PArea"].ToString();
                    txtBCArea.Text = bdr["CArea"].ToString();
                    txtBNRoom1.Text = bdr["ClassRoom"].ToString();
                    txtBSCapacity1.Text = bdr["CRCapacity"].ToString();
                    txtBTotal1.Text = bdr["CRArea"].ToString();
                    txtBNRoom2.Text = bdr["LabsNo"].ToString();
                    txtBSCapacity2.Text = bdr["LCapicity"].ToString();
                    txtBTotal2.Text = bdr["LArea"].ToString();
                    txtBNRoom3.Text = bdr["CompLabs"].ToString();
                    txtBSCapacity3.Text = bdr["CompLabsCapacity"].ToString();
                    txtBTotal3.Text = bdr["CompLabsArea"].ToString();
                    txtBNRoom4.Text = bdr["Library"].ToString();
                    txtBSCapacity4.Text = bdr["LibCapacity"].ToString();
                    txtBTotal4.Text = bdr["LibArea"].ToString();
                    txtBNRoom5.Text = bdr["Reception"].ToString();
                    txtBSCapacity5.Text = bdr["RecCapacity"].ToString();
                    txtBTotal5.Text = bdr["RecArea"].ToString();
                    txtBNRoom6.Text = bdr["AdminBlock"].ToString();
                    txtBSCapacity6.Text = bdr["BlockCapacity"].ToString();
                    txtBTotal6.Text = bdr["BlockArea"].ToString();
                    txtBNRoom7.Text = bdr["OtherNo"].ToString();
                    txtBSCapacity7.Text = bdr["OCapacity"].ToString();
                    txtBTotal7.Text = bdr["OArea"].ToString();
                    txtPAddress.Text = bdr["Address"].ToString();
                    txtPCity.Text = bdr["City"].ToString();
                    txtPState.Text = bdr["State"].ToString();
                    txtPPincode.Text = bdr["Pin"].ToString();
                    txtPhoneNo.Text = bdr["Phone"].ToString();
                    txtFaxNo.Text = bdr["Fax"].ToString();
                    txtMobile.Text = bdr["Mobile"].ToString();
                    txtEmail.Text = bdr["Email"].ToString();
                    
                }
                bdr.Close();
                con.Close(); con.Dispose();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
