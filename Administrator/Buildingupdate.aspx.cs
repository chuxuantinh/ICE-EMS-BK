using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Administrator_Buildingupdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
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
                con.Close(); con.Open();
                buildupdate(con);
                con.Close(); con.Dispose();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Close(); con.Dispose();
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
            string stn = "update Building set Name=@Name,BName=@BName,Type=@Type,PArea=@PArea,CArea=@CArea,ClassRoom=@ClassRoom,CRCapacity=@CRCapacity,CRArea=@CRArea,LabsNo=@LabsNo,LCapicity=@LCapicity,LArea=@LArea,CompLabs=@CompLabs,CompLabsCapacity=@CompLabsCapacity,CompLabsArea=@CompLabsArea,Library=@Library,LibCapacity=@LibCapacity,LibArea=@LibArea,Reception=@Reception,RecCapacity=@RecCapacity,RecArea=@RecArea,AdminBlock=@AdminBlock,BlockCapacity=@BlockCapacity,BlockArea=@BlockArea,OtherNo=@OtherNo,OCapacity=@OCapacity,OArea=@OArea,Address=@Address,City=@City,State=@State,Pin=@Pin,Phone=@Phone,Fax=@Fax,Mobile=@Mobile,Email=@Email where ID = '" + lblEnrolment.Text + "'";
            SqlCommand cmd = new SqlCommand(stn, con);
            // string strid = idcenter();
            // cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(strid));
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
            cmd.Parameters.AddWithValue("@City", txtPCity.Text.ToString());
            cmd.Parameters.AddWithValue("@State", txtPState.Text.ToString());
            cmd.Parameters.AddWithValue("@Pin", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("Phone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.ExecuteNonQuery();
            lblException.Text = "IM Building Info Update";
            btnSave.Enabled = false;
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
        finally
        {
            buildupdate(con);
            con.Close(); con.Dispose();
        }
    }
    private string idcenter()
    {
        con.Close();
        con.Open();
        SqlCommand cmdsn = new SqlCommand("select Max(SN) from Building", con);
       
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

    private void buildupdate(SqlConnection con)
    {
        if (lblEnrolment.Text != "")
        {
            try
            {

                string strbuild = "select Name,BName,Type,PArea,CArea,ClassRoom,CRCapacity,CRArea,LabsNo,LCapicity,LArea,CompLabs,CompLabsCapacity,CompLabsArea,Library,LibCapacity,LibArea,Reception,RecCapacity,RecArea,AdminBlock,BlockCapacity,BlockArea,OtherNo,OCapacity,OArea,Address,City,State,Pin,Phone,Fax,Mobile,Email,ToSeat from building where ID = '" + lblEnrolment.Text + "'";
                SqlCommand cmd = new SqlCommand(strbuild,con);
                SqlDataReader bdr;
                bdr = cmd.ExecuteReader();
                if (bdr.Read() == true)
                {
                     txtName.Text=bdr["Name"].ToString();
                     txtBName.Text=bdr["BName"].ToString();
                     ddlBType.Text=bdr["Type"].ToString();
                     txtBPArea.Text=bdr["PArea"].ToString();
                     txtBCArea.Text=bdr["CArea"].ToString();
                     txtBNRoom1.Text=bdr["ClassRoom"].ToString();
                     txtBSCapacity1.Text=bdr["CRCapacity"].ToString();
                     txtBTotal1.Text=bdr["CRArea"].ToString();
                     txtBNRoom2.Text=bdr["LabsNo"].ToString();
                     txtBSCapacity2.Text=bdr["LCapicity"].ToString();
                     txtBTotal2.Text=bdr["LArea"].ToString();
                    txtBNRoom3.Text=bdr["CompLabs"].ToString();
                    txtBSCapacity3.Text=bdr["CompLabsCapacity"].ToString();
                    txtBTotal3.Text=bdr["CompLabsArea"].ToString();
                    txtBNRoom4.Text=bdr["Library"].ToString();
                    txtBSCapacity4.Text=bdr["LibCapacity"].ToString();
                    txtBTotal4.Text=bdr["LibArea"].ToString();
                    txtBNRoom5.Text=bdr["Reception"].ToString();
                    txtBSCapacity5.Text=bdr["RecCapacity"].ToString();
                    txtBTotal5.Text=bdr["RecArea"].ToString();
                    txtBNRoom6.Text=bdr["AdminBlock"].ToString();
                    txtBSCapacity6.Text=bdr["BlockCapacity"].ToString();
                    txtBTotal6.Text=bdr["BlockArea"].ToString();
                    txtBNRoom7.Text=bdr["OtherNo"].ToString();
                    txtBSCapacity7.Text=bdr["OCapacity"].ToString();
                    txtBTotal7.Text=bdr["OArea"].ToString();
                    txtPAddress.Text=bdr["Address"].ToString();
                    txtPCity.Text=bdr["City"].ToString();
                    txtPState.Text = bdr["State"].ToString();
                    txtPPincode.Text = bdr["Pin"].ToString();
                     txtPhoneNo.Text=bdr["Phone"].ToString();
                    txtFaxNo.Text = bdr["Fax"].ToString();
                    txtMobile.Text = bdr["Mobile"].ToString();
                    txtEmail.Text = bdr["Email"].ToString();
                }
                bdr.Close(); bdr.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
