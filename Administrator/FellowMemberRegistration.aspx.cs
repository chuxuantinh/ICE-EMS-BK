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
using System.Globalization;
using iTextSharp.text.pdf;

using iTextSharp.text.html;

using iTextSharp.text.html.simpleparser;
public partial class Administrator_FellowMemberRegistration : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    ClsStateCity clsstatecity = new ClsStateCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(MyLogin.login[0]) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            lblMemberType.Text = Request.QueryString["id"].ToString();
            Session.Remove("FeeID");
            Session["FeeID"] = lblMemberType.Text.ToString();
            viewprofile(); NarrationBox.Visible = false;
            ViewProfiel();
            panelShowExpEdit.Visible = false;
            panelOther.Visible = false;
            panelSubspendAcc.Visible = false;
            panelViewRenewAcc.Visible = false;
            panelViewAccount.Visible = false;
            panelview.Visible = true;

            paneleditExperice.Visible = false;
            panelEditPersonaldate.Visible = false;
            panelEditQualification.Visible = false;
            lbltitle.Text = "Personal Profile";
            viewimg();
            if (!IsPostBack)
            {
                clsstatecity.xmlstate(ddlState, "XMLState.xml");
            }
            txtNewIdView.Focus();
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    private void viewimg()
    {
        SqlDataAdapter daimages = new SqlDataAdapter("SELECT ID from [Member] where ID='" + lblMemberType.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        DataList1.DataSource = dt;
        DataList1.DataBind();
        DataList2.DataSource = dt;
        DataList2.DataBind();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //Condition to check if the file uploaded or not
        if (fileuploadImage.HasFile)
        {
            //getting length of uploaded file
            int length = fileuploadImage.PostedFile.ContentLength;
            //create a byte array to store the binary image data
            byte[] imgbyte = new byte[length];
            //store the currently selected file in memeory
            HttpPostedFile img = fileuploadImage.PostedFile;
            //set the binary data
            img.InputStream.Read(imgbyte, 0, length);
            string imagename = fileuploadImage.PostedFile.FileName;
            string strex = Path.GetExtension(imagename);
            if (strex == ".jpg" | strex == ".gif" | strex == ".png" | strex==".JPG")
            {
                //use the web.config to store the connection string
              
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("update Member set Img=@Img, ImgName=@ImgName where ID='" + lblMemberType.Text.ToString() + "'", con);
                    // SqlCommand cmd = new SqlCommand("INSERT INTO Docs (ImageName,Image) VALUES (@imagename,@imagedata)", con);
                    cmd1.Parameters.Add("@Img", SqlDbType.Image).Value = imgbyte;
                    cmd1.Parameters.Add("@ImgName", SqlDbType.VarChar, 50).Value = imagename;
                    int count = cmd1.ExecuteNonQuery();
                    con.Close(); 
                    lblImgException.Text = "Profile Image Saved.";
                    lblImgTitle.Text = "Change Profile Picture.";


                    // upload signature;
              
            }
            else
            {
                lblImgException.Text = "Wrong image format." + strex.ToString();
            }
            viewimg();
        }
    }
    public void viewprofile()
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Member where ID='" + lblMemberType.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblName.Text = reader[3].ToString() + "\0\0 " + reader[4].ToString();
                lblFName.Text = reader[5].ToString();
                lblDesignation.Text = reader[6].ToString();
                lblAddress.Text = reader[7].ToString();
                lblState.Text = reader[8].ToString();
                lblPincode.Text = reader[9].ToString();
                lblTelNo.Text = reader[10].ToString();
                lblOffice.Text = reader[11].ToString();
                lblResidential.Text = reader[12].ToString();
                lblMobile.Text = reader[13].ToString();
                lblEmail.Text = reader[14].ToString();
                lblDateBirth.Text = Convert.ToString(reader[15].ToString());
                lblAge.Text = reader[16].ToString();
                lblHeigestQualification.Text = reader[17].ToString();
                lblDesipliceBE.Text = reader[19].ToString();
                lblDesipliineMTech.Text = reader[20].ToString();
                lblDesiplinePhd.Text = reader[21].ToString();
                lbldesipleineohter.Text = reader[22].ToString();
                lblCollegeBE.Text = reader[23].ToString();
                lblCollegeMTech.Text = reader[24].ToString();
                lblCollegePhd.Text = reader[25].ToString();
                lblCollegeohter.Text = reader[26].ToString();
                lblPercentageBE.Text = reader[27].ToString();
                lblPercentageMTech.Text = reader[28].ToString();
                lblPercentagePhd.Text = reader[29].ToString();
                lblpercentageohter.Text = reader[30].ToString();
                lblUniversityBE.Text = reader[31].ToString();
                lbluniversityMTech.Text = reader[32].ToString();
                lblUniversityPhd.Text = reader[33].ToString();
                lbluniversigtyohter.Text = reader[34].ToString();
                lblYearBE.Text = reader[35].ToString();
                lblyearMTech.Text = reader[36].ToString();
                lblyearphd.Text = reader[37].ToString();
                lblyearother.Text = reader[38].ToString();
                lblPost1.Text = reader[40].ToString(); lblPost2.Text = reader[41].ToString(); lblPost3.Text = reader[42].ToString(); lblPost4.Text = reader[43].ToString(); lblPost5.Text = reader[44].ToString();
                lblOrg1.Text = reader[45].ToString(); lblOrg2.Text = reader[46].ToString(); lblOrg3.Text = reader[47].ToString(); lblOrg4.Text = reader[48].ToString(); lblOrg5.Text = reader[49].ToString();
                lblAdd1.Text = reader[50].ToString(); lblAdd2.Text = reader[51].ToString(); lblAdd3.Text = reader[52].ToString(); lblAdd4.Text = reader[53].ToString(); lblAdd5.Text = reader[54].ToString();
                lblf1.Text = reader[55].ToString(); lblf2.Text = reader[56].ToString(); lblf3.Text = reader[57].ToString(); lblf4.Text = reader[58].ToString(); lblf5.Text = reader[59].ToString();
                lblt1.Text = reader[60].ToString(); lblt2.Text = reader[61].ToString(); lblt3.Text = reader[62].ToString(); lblt4.Text = reader[63].ToString(); lblt5.Text = reader[64].ToString();
                lbtnExpStatus.Text = reader[77].ToString();
                if (reader[71].ToString() == "yes" | reader[71].ToString()=="Active") { lblStatus.Text = "Active"; lblStatus2.Text = "Active"; } else  { lblStatus.Text = "Disactive"; lblStatus2.Text = "Disactive"; }
                lblEnrollDate.Text =Convert.ToDateTime(reader[72].ToString()).ToString("dd/MM/yyyy");
                lblRenuwalDate.Text =Convert.ToDateTime(reader[73].ToString()).ToString("dd/MM/yyyy");
                lblExpDate.Text =Convert.ToDateTime(reader[74].ToString()).ToString("dd/MM/yyyy");
                lblyear.Text = reader[75].ToString()+ " TO " + reader[76].ToString() ;
                lblExpCase.Text = reader[78].ToString();

            }
            reader.Close();
            reader.Dispose();

        }
        catch (SqlException ex)
        {
            lblExceptionViewProfiel.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            if (lblStatus.Text == "Disactive")
            {
                btnchangeStatus.Text = "Active";
            }
            else if (lblStatus.Text == "Active")
            {
                btnchangeStatus.Text = "Disactive";
            }
        }

    }
  
    public void feestatus()
    {

    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ibtnRegisterMem_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/Register.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=In&" + Request.QueryString["lvl"] + "=zero");
    }
    protected void ibtnvewProfile_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Administrator/ViewAppProfiles.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=In&lvl=zero");
    }
    protected void lbtnDeactivate_Click(object sender, EventArgs e)  // experience.
    {
        ExperienceClick();

    }
    public void ExperienceClick()
    {
        paneleditExperice.Visible = true;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        panelview.Visible = false;

        lbltitle.Text = "Edit Experience";

        panelOther.Visible = false;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = false;
        panelview.Visible = false;
    }
    protected void lbtnCreateAdmin_Click(object sender, EventArgs e)  // personal profile.
    {

        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = true;
        panelEditQualification.Visible = false;
        panelview.Visible = false;
        lbltitle.Text = "Edit Profile";

        panelOther.Visible = false;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = false;
        panelview.Visible = false;
    }
    protected void lbtnChagePassword_Click(object sender, EventArgs e) //educaiton.
    {
        EditEducation();

    }
    public void EditEducation()
    {
        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = true;
        panelview.Visible = false;
        lbltitle.Text = "Edit Education Qualification";

        panelOther.Visible = false;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = false;
        panelview.Visible = false;

    }
    protected void lbtnViewPersonal_Click(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            viewprofile();
            ViewProfiel();
        }
    }
    public void ViewProfiel()
    {
        panelOther.Visible = false;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = false;
        panelview.Visible = true;

        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        lbltitle.Text = "Personal Profile";

    }
    protected void lbtnViewAccount_Click(object sender, EventArgs e)
    {
        panelOther.Visible = false;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = true;
        panelview.Visible = false;
        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        lbltitle.Text = "Membership Profile";

    }
    protected void lbtnRenewAcc_Click(object sender, EventArgs e)
    {
        panelOther.Visible = false;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = true;
        panelViewAccount.Visible = false;
        panelview.Visible = false;
        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        lbltitle.Text = "Experience Profile";

    }
    protected void lbtnSuspendAcc_Click(object sender, EventArgs e)
    {
        panelOther.Visible = false;
        panelSubspendAcc.Visible = true;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = true;
        panelview.Visible = false;
        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        lbltitle.Text = "Active/Disactive Membership";
    }
    protected void btnSavePersonal_Click(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Member set Name=@Name,LName=@LName,FName=@FName,Designation=@Designation,Address=@Address,State=@State,PinCode=@PinCode,TelNo=@TelNo,Office=@Office,ResNo=@ResNo,Mobile=@Mobile,Email=@Email,DOB=@DOB,Age=@Age,Address2=@Address2 where ID='" + Request.QueryString["id"].ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@LName", txtLName.Text.ToString());
            cmd.Parameters.AddWithValue("@FName", txtFather.Text.ToString());
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.ToString());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@State",ddlState.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@PinCode", txtPincode.Text.ToString());
            cmd.Parameters.AddWithValue("@TelNo", txtTelNo.Text.ToString()); cmd.Parameters.AddWithValue("@Office", txtOfficeNo.Text.ToString()); cmd.Parameters.AddWithValue("@ResNo", txtResidentialNo.Text.ToString()); cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString()); cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString()); cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(txtDOB.Text,dtinfo)); cmd.Parameters.AddWithValue("@Age", txtAge.Text.ToString());
            cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.ToString());
            cmd.ExecuteNonQuery();
           
            lblException1.Text = "Personal Profile Saved";
            clearnow();

        }
        catch (SqlException ex)
        {
            lblException1.Text = ex.ToString();
        }
        finally
        {
            con.Close(); 
        }
    }
    protected void btnClearPersonal_Click(object sender, EventArgs e)
    {
        clearnow();
    }
    public void clearnow()
    {
        txtName.Text = "";
        txtLName.Text = "";
        txtFather.Text = ""; txtAddress.Text = ""; ddlState.SelectedItem.Text = ""; txtPincode.Text = "";
        txtDesignation.Text = ""; txtTelNo.Text = ""; txtOfficeNo.Text = ""; txtResidentialNo.Text = ""; txtMobile.Text = ""; txtEmail.Text = ""; txtAge.Text = "";
    }
    protected void btnViewNewId_Click(object sender, EventArgs e)
    {
        maikal mk = new maikal();

        string id = mk.SearchProfile(txtNewIdView.Text.ToString());
        if (id == txtNewIdView.Text.ToString())

            Response.Redirect("../Administrator/FellowMemberRegistration.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=In&" + Request.QueryString["lvl"] + "=zero&id=" + txtNewIdView.Text.ToString());
        else txtNewIdView.Text = "Invalid ID";
    }

    /// <summary>
    ///  Rad Grid for Profile Picture.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
   

    protected void ButtonSaveEducaitoin_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close();
            con.Open();

            SqlCommand cmd = new SqlCommand("update Member set HQualification=@HQualification, BDesp=@BDesp, MDesp=@MDesp, PDesp=@PDesp, ODesp=@ODesp, BClg=@BClg, MClg=@MClg, PClg=@PClg, OClg=@OClg, BPercent=@BPercent, MPercent=@MPercent, PPercent=@PPercent, OPercent=@OPercent, BUniversity=@BUniversity, MUniversity=@MUniversity, PUniversity=@PUniversity, OUniversity=@OUniversity, BYPass=@BYPass, MYPass=@MYPass, PYPass=@PYPass, OYPass=@OYPass where ID='" + lblMemberType.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@HQualification", txtQualicicatin.Text.ToString());
            cmd.Parameters.AddWithValue("@BDesp", txtBDiscipline.Text.ToString());
            cmd.Parameters.AddWithValue("@MDesp", txtMDisipline.Text.ToString());
            cmd.Parameters.AddWithValue("@PDesp", txtPDiscipline.Text.ToString());
            cmd.Parameters.AddWithValue("@ODesp", txtODiscipline.Text.ToString());
            cmd.Parameters.AddWithValue("@BClg", txtBCollege.Text.ToString());
            cmd.Parameters.AddWithValue("@MClg", txtMCollege.Text.ToString());
            cmd.Parameters.AddWithValue("@PClg", txtPcollege.Text.ToString());
            cmd.Parameters.AddWithValue("@OClg", txtOCollege.Text.ToString());
            cmd.Parameters.AddWithValue("@BPercent", txtBPercentage.Text.ToString());
            cmd.Parameters.AddWithValue("@MPercent", txtMPercentage.Text.ToString());
            cmd.Parameters.AddWithValue("@PPercent", txtPPercentage.Text.ToString());
            cmd.Parameters.AddWithValue("@OPercent", txtOPercentage.Text.ToString());
            cmd.Parameters.AddWithValue("@BUniversity", txtBUviversity.Text.ToString());
            cmd.Parameters.AddWithValue("@MUniversity", txtMUviversity.Text.ToString());
            cmd.Parameters.AddWithValue("@PUniversity", txtPUniversity.Text.ToString());
            cmd.Parameters.AddWithValue("@OUniversity", txtOUniveristy.Text.ToString());
            cmd.Parameters.AddWithValue("@BYPass", txtBPassingyear.Text.ToString());
            cmd.Parameters.AddWithValue("@MYPass", txtMPassingyear.Text.ToString());
            cmd.Parameters.AddWithValue("@PYPass", txtPPassingyear.Text.ToString());
            cmd.Parameters.AddWithValue("@OYPass", txtOPassingyear.Text.ToString());

            cmd.ExecuteNonQuery();
            lblExepEditEducation.Text = "Qualification Information Saved.";
            EditEducation();
        }
        catch (SqlException ex)
        {
            lblExepEditEducation.Text = ex.ToString();
        }
        finally
        {
            con.Close(); 
        }
    }
    protected void lbtnEditPicture_Click(object sender, EventArgs e)  // Active Membership Account.
    {

        panelOther.Visible = true;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = false;
        panelview.Visible = false;


        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        lbltitle.Text = "Profile Image Upload";

    }
    protected void btnSaveExp_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            con.Close();
            con.Open();
            if (Convert.ToInt32(lbtnExpStatus.Text) == 0)
            {

                SqlCommand cmd0 = new SqlCommand("update Member set Experience=@Experience,ExpPost1=@ExpPost1,ExpName1=@ExpName1,ExpFrom1=@ExpFrom1,ExpTo1=@ExpTo1,ExpStatus=@ExpStatus where ID='" + Convert.ToString(Session["FeeID"].ToString()) + "'", con);
                cmd0.Parameters.AddWithValue("@Experience", "Year: " + txtExpYear.Text.ToString() + "." + txtExpMonth.Text.ToString());
                cmd0.Parameters.AddWithValue("@ExpPost1", txtPostHeld.Text.ToString());
                cmd0.Parameters.AddWithValue("@ExpName1", txtEmployerName.Text.ToString() + " Address: " + txtExpAddress.Text.ToString());
                cmd0.Parameters.AddWithValue("@ExpFrom1", Convert.ToDateTime(txtExpDAteFrom.SelectedDate.Value.ToShortDateString(),dtinfo));
                cmd0.Parameters.AddWithValue("@ExpTo1", Convert.ToDateTime(txtExpDateTo.SelectedDate.Value.ToShortDateString(),dtinfo));
                int i = Convert.ToInt32(lbtnExpStatus.Text) + 1;
                cmd0.Parameters.AddWithValue("@ExpStatus", i);
                cmd0.ExecuteNonQuery();
                lblInsertExpInfo.Text = "Experience Posted " + Convert.ToInt32(lbtnExpStatus.Text.ToString());

            }
            else if (Convert.ToInt32(lbtnExpStatus.Text) == 1)
            {
                SqlCommand cmd1 = new SqlCommand("update Member set Experience=@Experience,ExpPost2=@ExpPost2,ExpName2=@ExpName2,ExpFrom2=@ExpFrom2,ExpTo2=@ExpTo2,ExpStatus=@ExpStatus where ID='" + Convert.ToString(Session["FeeID"].ToString()) + "'", con);
                cmd1.Parameters.AddWithValue("@Experience", "Year: " + txtExpYear.Text.ToString() + "." + txtExpMonth.Text.ToString());
                cmd1.Parameters.AddWithValue("@ExpPost2", txtPostHeld.Text.ToString());
                cmd1.Parameters.AddWithValue("@ExpName2", txtEmployerName.Text.ToString() + " Address: " + txtExpAddress.Text.ToString());
                cmd1.Parameters.AddWithValue("@ExpFrom2", Convert.ToDateTime(txtExpDAteFrom.SelectedDate.Value.ToShortDateString(),dtinfo));
                cmd1.Parameters.AddWithValue("@ExpTo2", Convert.ToDateTime(txtExpDateTo.SelectedDate.Value.ToShortDateString(),dtinfo));
                int j = Convert.ToInt32(lbtnExpStatus.Text) + 1;
                cmd1.Parameters.AddWithValue("@ExpStatus", j);
                cmd1.ExecuteNonQuery();
                lblInsertExpInfo.Text = "Experience Posted " + Convert.ToInt32(lbtnExpStatus.Text.ToString());

            }
            else if (Convert.ToInt32(lbtnExpStatus.Text) == 2)
            {
                SqlCommand cmd2 = new SqlCommand("update Member set Experience=@Experience,ExpPost3=@ExpPost3,ExpName3=@ExpName3,ExpFrom3=@ExpFrom3,ExpTo3=@ExpTo3,ExpStatus=@ExpStatus where ID='" + Convert.ToString(Session["FeeID"].ToString()) + "'", con);
                cmd2.Parameters.AddWithValue("@Experience", "Year: " + txtExpYear.Text.ToString() + "." + txtExpMonth.Text.ToString());
                cmd2.Parameters.AddWithValue("@ExpPost3", txtPostHeld.Text.ToString());
                cmd2.Parameters.AddWithValue("@ExpName3", txtEmployerName.Text.ToString() + " Address: " + txtExpAddress.Text.ToString());
                cmd2.Parameters.AddWithValue("@ExpFrom3", Convert.ToDateTime(txtExpDAteFrom.SelectedDate.Value.ToShortDateString(),dtinfo));
                cmd2.Parameters.AddWithValue("@ExpTo3", Convert.ToDateTime(txtExpDateTo.SelectedDate.Value.ToShortDateString(),dtinfo));
                int k = Convert.ToInt32(lbtnExpStatus.Text) + 1;
                cmd2.Parameters.AddWithValue("@ExpStatus", k);
                cmd2.ExecuteNonQuery();
                lblInsertExpInfo.Text = "Experience Posted " + Convert.ToInt32(lbtnExpStatus.Text.ToString());
            }
            else if (Convert.ToInt32(lbtnExpStatus.Text) == 3)
            {
                SqlCommand cmd3 = new SqlCommand("update Member set Experience=@Experience,ExpPost4=@ExpPost4,ExpName4=@ExpName4,ExpFrom4=@ExpFrom4,ExpTo4=@ExpTo4,ExpStatus=@ExpStatus where ID='" + Convert.ToString(Session["FeeID"].ToString()) + "'", con);
                cmd3.Parameters.AddWithValue("@Experience", "Year: " + txtExpYear.Text.ToString() + "." + txtExpMonth.Text.ToString());
                cmd3.Parameters.AddWithValue("@ExpPost4", txtPostHeld.Text.ToString());
                cmd3.Parameters.AddWithValue("@ExpName4", txtEmployerName.Text.ToString() + " Address: " + txtExpAddress.Text.ToString());
                cmd3.Parameters.AddWithValue("@ExpFrom4", Convert.ToDateTime(txtExpDAteFrom.SelectedDate.Value.ToShortDateString(),dtinfo));
                cmd3.Parameters.AddWithValue("@ExpTo4", Convert.ToDateTime(txtExpDateTo.SelectedDate.Value.ToShortDateString(),dtinfo));
                int l = Convert.ToInt32(lbtnExpStatus.Text) + 1;
                cmd3.Parameters.AddWithValue("@ExpStatus", l);
                cmd3.ExecuteNonQuery();
                lblInsertExpInfo.Text = "Experience Posted " + Convert.ToInt32(lbtnExpStatus.Text.ToString());
            }
            else if (Convert.ToInt32(lbtnExpStatus.Text) == 4)
            {
                SqlCommand cmd4 = new SqlCommand("update Member set Experience=@Experience,ExpPost5=@ExpPost5,ExpName5=@ExpName5,ExpFrom5=@ExpFrom5,ExpTo5=ExpTo5,ExpStatus=@ExpStatus,ExpNarration=@ExpNarration where ID='" + Convert.ToString(Session["FeeID"].ToString()) + "'", con);
                cmd4.Parameters.AddWithValue("@Experience", "Year: " + txtExpYear.Text.ToString() + "." + txtExpMonth.Text.ToString());
                cmd4.Parameters.AddWithValue("@ExpPost5", txtPostHeld.Text.ToString());
                cmd4.Parameters.AddWithValue("@ExpName5", txtEmployerName.Text.ToString() + " Address: " + txtExpAddress.Text.ToString());
                cmd4.Parameters.AddWithValue("@ExpFrom5", Convert.ToDateTime(txtExpDAteFrom.SelectedDate.Value.ToShortDateString(),dtinfo));
                cmd4.Parameters.AddWithValue("@ExpTo5", Convert.ToDateTime(txtExpDateTo.SelectedDate.Value.ToShortDateString(),dtinfo));
                int m = Convert.ToInt32(lbtnExpStatus.Text) - 4;
                cmd4.Parameters.AddWithValue("@ExpStatus", m);
                cmd4.Parameters.AddWithValue("@ExpNarration", txtNarration.Text.ToString());
                cmd4.ExecuteNonQuery();
                lblInsertExpInfo.Text = "Experience Posted " + Convert.ToInt32(lbtnExpStatus.Text.ToString());

            }
            lblShowPostHeld.Text = txtPostHeld.Text.ToString();
            lblShowExpName.Text = txtEmployerName.Text.ToString();
            lblShowExpAddress.Text = txtExpAddress.Text.ToString();
            lblShowExpDateFrom.Text = txtExpDAteFrom.SelectedDate.ToString();
            lblShowDateExpTo.Text = txtExpDateTo.SelectedDate.ToString();
            paneleditExperice.Visible = true;
            panelEditPersonaldate.Visible = false;
            panelEditQualification.Visible = false;
            panelview.Visible = false;

            lbltitle.Text = "Edit Experience";

            panelOther.Visible = false;
            panelSubspendAcc.Visible = false;
            panelViewRenewAcc.Visible = false;
            panelViewAccount.Visible = false;
            panelview.Visible = false;
        }
        catch (SqlException ex)
        {
            lblInsertExpInfo.Text = ex.ToString();
        }
        finally
        {
            panelShowExpEdit.Visible = true;

            btnSaveExp.Enabled = false;
            con.Close();
        }

    }

    protected void btnAddMoreExp_Click(object sender, EventArgs e)
    {
        paneleditExperice.Visible = true;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        panelview.Visible = false;

        lbltitle.Text = "Edit Experience";

        panelOther.Visible = false;
        panelSubspendAcc.Visible = false;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = false;
        panelview.Visible = false;
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        panelShowExpEdit.Visible = false;
        btnSaveExp.Enabled = true;
        txtPostHeld.Text = "";
        txtEmployerName.Text = "";
        txtExpAddress.Text = "";
        // Response.Redirect(url.ToString());

    }


    protected void txtExpYears_TextChanged(object sender, EventArgs e)
    {
        ExperienceClick();

    }
    public string getMembertype()
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select Type from Member where ID='" + Request.QueryString["id"].ToString() + "'", con);
        string type = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        return type;

    }

    protected void btnNarration_Click(object sender, EventArgs e)
    {
        NarrationBox.Visible = false;

        string type = getMembertype();
        if (type == "Honorary")
        {
            txtExpYear.ToolTip = "Eminent scholars in Science and Technology and associated with Civil/Architectural Engineering activities.";
            lblEligibilgy.Text = "Eminent scholars in Science and Technology and associated with Civil/Architectural Engineering activities.";
            lblEligibilityTitle.Text = "Honorary Member";
        }
        else if (type == "Fellow")
        {
            txtExpYear.ToolTip = "Professionally qualified and in profession for 15 years.";
            lblEligibilgy.Text = "Professionally qualified and in profession for 15 years.";
            lblEligibilityTitle.Text = "Fellow Member";
        }
        else if (type == "Member")
        {
            txtExpYear.ToolTip = "Minimum 3 years experience in the discipline of Civil/Architectural Engineering in Education Institutions/Industry/Field.";
            lblEligibilgy.Text = "Minimum 3 years experience in the discipline of Civil/Architectural Engineering in Education Institutions/Industry/Field.";
            lblEligibilityTitle.Text = "Member";

        }
        else if (type == "IM")
        {
        }
        else
        {
        }
        ExperienceClick();
    }
    protected void ibtnShowEligibilgy_Click(object sender, EventArgs e)
    {

        ExperienceClick();
        NarrationBox.Visible = true;
    }
    string strstatus;
    protected void btnChnageStatsu(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        if (lblStatus.Text == "Disactive")
        {
            btnchangeStatus.Text = "Active"; strstatus = "yes";
        }
        else if (lblStatus.Text == "Active")
        {
            btnchangeStatus.Text = "Disactive"; strstatus = "no";
        }
        SqlCommand cmd = new SqlCommand("update Member set Active='" + strstatus.ToString() + "' where ID='" + lblMemberType.Text + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        panelOther.Visible = false;
        panelSubspendAcc.Visible = true;
        panelViewRenewAcc.Visible = false;
        panelViewAccount.Visible = true;
        panelview.Visible = false;


        paneleditExperice.Visible = false;
        panelEditPersonaldate.Visible = false;
        panelEditQualification.Visible = false;
        lbltitle.Text = "Active/Disactive Membership";

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsstatecity.xmlstate(ddlState, "XMLState.xml");
    }
}
