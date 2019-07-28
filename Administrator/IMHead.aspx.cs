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

public partial class Administrator_IMHead : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    ClsStateCity statecity = new ClsStateCity();
   protected void Page_Load(object sender, EventArgs e)
    {
        try
        {if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");

            }
            else
            {
               
                if (!IsPostBack)
                {
                    
                    txtothercity.Visible = false; lblcity.Visible = false;
                    lblEnrolment.Text = Request.QueryString["id"].ToString();
                    statecity.xmlstate(ddlstate, "XMLState.xml");
                    statecity.xmlCity(ddlcity, ddlstate.SelectedItem.Text.ToString(), "XMLState.xml");
                    btnClear.Visible = false;
                    
                    if (Request.QueryString["id"].ToString() == "") panelRight.Enabled = false;
                    ViewImg();
                    txtName.Focus();
                }

                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("select * from IM where ID='" + Request.QueryString["id"].ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                string himgname = "", hsignname = "", aimgname = "", asingname = "";
                while (reader.Read())
                {
                    himgname = reader["HImgName"].ToString();
                    hsignname = reader["HSignName"].ToString();
                    aimgname = reader["AImgName"].ToString();
                    asingname = reader["ASignName"].ToString();
                }
                reader.Close();
                con.Close();
                if (himgname == "")
                {
                    lblImgTitle.Text = "Upload Profile Picture.";
                    imgDefault.Visible = true;
                }
                else
                {
                    if (hsignname == "")
                        imgDefault.Visible = true;
                    else
                        imgDefault.Visible = false;
                }

                if (aimgname == "")
                {
                    lblImgTitle.Text = "Change Profile Image";
                    Image2.Visible = true;
                }
                else
                {
                    if (asingname == "")
                        Image2.Visible = true;
                    else Image2.Visible = false;
                }
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
    public void age()
    {
        try
        {
            DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            DateTime dt = Convert.ToDateTime(txtDOB.Text, dtinfo);
           

            int year = DateTime.Now.Year - dt.Year;
            if (DateTime.Now.Month < dt.Month || DateTime.Now.Month == dt.Month && DateTime.Now.Day < dt.Day)
                --year;
            txtAge.Text = year.ToString();
        }
        catch (FormatException ex)
        {
           
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        // upload for Head of Management.
        if (fileuploadImage.HasFile)
        {
            int length = fileuploadImage.PostedFile.ContentLength;
            byte[] imgbyte = new byte[length];
            HttpPostedFile img = fileuploadImage.PostedFile;
            img.InputStream.Read(imgbyte, 0, length);
            string imagename = fileuploadImage.PostedFile.FileName;
            string strex = Path.GetExtension(imagename);
            if (strex == ".jpg" | strex == ".gif" | strex == ".png")
            {
                if (rbtnHImage.Checked == true)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update IM set HImg=@HImg, HImgName=@HImgName where ID='" + lblEnrolment.Text.ToString() + "'", con);
                    // SqlCommand cmd = new SqlCommand("INSERT INTO Docs (ImageName,Image) VALUES (@imagename,@imagedata)", con);
                    cmd.Parameters.Add("@HImg", SqlDbType.Image).Value = imgbyte;
                    cmd.Parameters.Add("@HImgName", SqlDbType.VarChar, 50).Value = imagename;

                    int count = cmd.ExecuteNonQuery();
                    con.Close();
                    lblImgException.Text = "Profile Image Saved.";
                    if (lblImgStatus.Text == "") lblImgTitle.Text = "upload Signature."; else lblImgTitle.Text = "Change Signature.";

                   
                }
                else if (rbtnHSign.Checked == true)
                {
                    con.Close(); con.Open();
                    SqlCommand cmd1 = new SqlCommand("update IM set HSign=@HSign, HSignName=@HSignName where ID='" + lblEnrolment.Text.ToString() + "'", con);
                    // SqlCommand cmd = new SqlCommand("INSERT INTO Docs (ImageName,Image) VALUES (@imagename,@imagedata)", con);
                    cmd1.Parameters.Add("@HSign", SqlDbType.Image).Value = imgbyte;
                    cmd1.Parameters.Add("@HSignName", SqlDbType.VarChar, 50).Value = imagename;
                    int count = cmd1.ExecuteNonQuery();
                    con.Close(); 
                    lblImgException.Text = "Profile Signature Saved.";
                    lblImgTitle.Text = "Change Profile Picture.";
                }
                else
                {
                    lblImgTitle.Text = "Change Profile Image.";
                }
            }
            else
            {
                lblImgException.Text = "Wrong image format." + strex.ToString();
            }
            ViewImg();
        }
    }
    protected void btnUpload2_Click(object sender, EventArgs e)
    {
        // Upload for Adademic Head of Institute.
        if (fileupload1.HasFile)
        {
            int length = fileupload1.PostedFile.ContentLength;
            byte[] imgbyte = new byte[length];
            HttpPostedFile img = fileupload1.PostedFile;
            img.InputStream.Read(imgbyte, 0, length);
            string imagename = fileupload1.PostedFile.FileName;
            string strex = Path.GetExtension(imagename);
            if (strex == ".jpg" | strex == ".gif" | strex == ".png")
            {
               if(rbtnAImg.Checked==true)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update IM set AImg=@AImg, AImgName=@AImgName where ID='" + lblEnrolment.Text.ToString() + "'", con);
                    // SqlCommand cmd = new SqlCommand("INSERT INTO Docs (ImageName,Image) VALUES (@imagename,@imagedata)", con);
                    cmd.Parameters.Add("@AImg", SqlDbType.Image).Value = imgbyte;
                    cmd.Parameters.Add("@AImgName", SqlDbType.VarChar, 50).Value = imagename;
                   
                    int count = cmd.ExecuteNonQuery();
                    con.Close();
                    lblImgException.Text = "Profile Image Saved.";
                    if (lblImgStatus.Text == "") lblImgTitle.Text = "upload Signature."; else lblImgTitle.Text = "Change Signature.";

                    
                    // show img form database;
                }
                else if (rbtnASign.Checked==true)
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("update IM set ASign=@ASign, ASignName=@ASignName where ID='" + lblEnrolment.Text.ToString() + "'", con);
                    // SqlCommand cmd = new SqlCommand("INSERT INTO Docs (ImageName,Image) VALUES (@imagename,@imagedata)", con);
                    cmd1.Parameters.Add("@ASign", SqlDbType.Image).Value = imgbyte;
                    cmd1.Parameters.Add("@ASignName", SqlDbType.VarChar, 50).Value = imagename;
                    int count = cmd1.ExecuteNonQuery();
                    con.Close(); 
                    lblImgException.Text = "Profile Image Saved.";
                    lblImgTitle.Text = "Change Profile Picture.";
                    // upload signature;
                }
                else
                {
                    lblImgTitle.Text = "Change Profile Image.";
                }
            }
            else
            {
                lblImgException.Text = "Wrong image format." + strex.ToString();
            }
            ViewImg();
        }
    }
    public void ViewImg()
    {
       
        SqlCommand command = new SqlCommand("SELECT HImgName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter daimages = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        DataList1.DataSource = dt;
        DataList1.DataBind();


        SqlCommand cmdhsign = new SqlCommand("SELECT HSignName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter dasign = new SqlDataAdapter(cmdhsign);
        DataTable dtsign = new DataTable();
        dasign.Fill(dtsign);
        dlsign.DataSource = dtsign;
        dlsign.DataBind();

        SqlCommand cmd2 = new SqlCommand("SELECT AImgName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt22 = new DataTable();
        da2.Fill(dt22);
        DataList3.DataSource = dt22;
        DataList3.DataBind();


        SqlCommand cmd3 = new SqlCommand("SELECT ASignName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        DataTable dt23 = new DataTable();
        da3.Fill(dt23);
        DataList4.DataSource = dt23;
        DataList4.DataBind();


        imgDefault.Visible = false;
        panelImage.Visible = true;
        Image2.Visible = false;
       
    }
    protected void lbtnBuildingTitel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMBuilding.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
             con.Open();
            SqlCommand cmd = new SqlCommand("update IM set HName=@HName,HDesignation=@HDesignation,HAddress=@HAddress,HCity=@HCity,HPhone=@HPhone,HFax=@HFax,HMobile=@HMobile,HEmail=@HEmail,HDOB=@HDOB,HAge=@HAge,HEdu=@HEdu,HExp=@HExp,AddressHead2=@AddressHead2 where ID='"+lblEnrolment.Text.ToString()+"'", con);
            cmd.Parameters.AddWithValue("HName", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@HDesignation", txtDEsignation.Text.ToString());
            cmd.Parameters.AddWithValue("@HAddress", txtPAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@HCity", ddlcity.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@HPhone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@HFax", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
            cmd.Parameters.AddWithValue("@HMobile", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@HEmail", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@HDOB", txtDOB.Text.ToString());
            cmd.Parameters.AddWithValue("@HAge", txtAge.Text.ToString());
            cmd.Parameters.AddWithValue("@HEdu", txtEducationQ.Text.ToString());
            cmd.Parameters.AddWithValue("@HExp", txtExperience.Text.ToString());
            cmd.Parameters.AddWithValue("@AddressHead2", txtAddressHead2.Text.ToString());

            cmd.ExecuteNonQuery();
            lblExceptionSave.Text = "IM Head Information Saved";
            txtName.Text = ""; txtDEsignation.Text = ""; txtPAddress.Text = "";  txtCAddress.Text = ""; txtCCity.Text = ""; txtCState.Text = "";
            txtEmail.Text = ""; txtPhonecode.Text = ""; txtPhoneNo.Text = ""; txtFaxCode.Text = ""; txtFaxNo.Text = "";
            txtAge.Text = ""; txtEducationQ.Text = ""; txtExperience.Text = "";
            txtAddressHead2.Text = ""; txtCAddressHead2.Text = "";
            btnSave.Enabled = false;
            btnClear.Visible = true;
            btnClear.Focus();
        }
        catch (SqlException ex)
        {
            lblExceptionSave.Text = ex.ToString();
        }
        finally
        {
            con.Close(); 
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        Response.Redirect("IMBuilding.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void chkBothAddressSame_CheckChanged(object sender, EventArgs e)
    {
        if (chkBothAddressSame.Checked == true)
        {
            txtCAddress.Text = txtPAddress.Text.ToString();
            txtCCity.Text = ddlcity.Text.ToString();
            txtCState.Text = ddlstate.Text.ToString();
            txtCPin.Text = txtPPincode.Text.ToString();
            txtCAddressHead2.Text = txtAddressHead2.Text.ToString();
            txtPhonecode.Focus();
        }
        else if (chkBothAddressSame.Checked == false)
        {
            txtCAddress.Text = "";
            txtCCity.Text = "";
            txtCState.Text = "";
            txtCAddressHead2.Text = "";
            txtCPin.Text = "";
            txtCAddress.Focus();
        }
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        statecity.xmlCity(ddlcity, ddlstate.SelectedItem.Text.ToString(), "XMLState.xml");
        ddlcity.Focus();
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
        chkBothAddressSame.Focus();
        
    }
    protected void txtothercity_TextChanged(object sender, EventArgs e)
    {
        txtPPincode.Focus();
    }
    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        age();
        txtAge.Focus();
    }
    protected void txtAge_TextChanged(object sender, EventArgs e)
    {
        txtEducationQ.Focus();
    }
}
