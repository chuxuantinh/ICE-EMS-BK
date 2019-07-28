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
using System.Xml;
using iTextSharp.text.html.simpleparser;

public partial class Administrator_IMRegi : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    ClsStateCity statecity = new ClsStateCity(); string name;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            rbtnAlone.Focus();
            if (Convert.ToString(MyLogin.login[0]) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (Request.QueryString["id"].ToString() != "")
                {
                    lbtnIMHeadTitel.ForeColor = System.Drawing.Color.Red;
                    btnCancel.Visible = true;
                }
                else
                {
                    lbtnIMHeadTitel.ForeColor = System.Drawing.Color.Gray;
                    btnCancel.Visible = false;
                }
                if (!IsPostBack)
                {
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlSession.SelectedValue = "Sum";
                    else ddlSession.SelectedValue = "Win";
                    txtYear.Text = DateTime.Now.Year.ToString();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    lblEnrolment.Text = Request.QueryString["id"].ToString();
                    grouptable.Visible = false;
                    statecity.xmlstate(ddlState, "XMLState.xml");
                    rbtnAlone.Focus();
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
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(MyLogin.login[1].ToString(), MyLogin.login[0].ToString());
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
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
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + txtEnrolment.Text.ToString() + "'", con);
        string strstatus = Convert.ToString(cmd.ExecuteScalar());
      
        if (strstatus == txtEnrolment.Text.ToString())
        {
            lblEnrolment.Text = txtEnrolment.Text.ToString();
            Response.Redirect("IMProfile.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + lblEnrolment.Text.ToString());
        }
        else if (strstatus != txtEnrolment.Text.ToString())
        {
            txtEnrolment.Text = "Invalid IM ID.";
        }
        con.Close(); con.Dispose();
    }
     string flag ;
     SqlCommand cmd = new SqlCommand();
     SqlTransaction sTR;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
      try
        {
            SqlCommand cmd1 = new SqlCommand("select ID from Member where Name='" + txtName.Text + "'", con);
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                dr.Close(); dr.Dispose();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('IM already Registered')", true);
            }
            else
            {
                dr.Close(); dr.Dispose();
                if (rbtnAlone.Checked == false & rbtnGroup.Checked == false)
                {
                    lblException.Text = "Please Select Membership Type.";
                    btnSave.Focus();
                }
                else
                {
                    sTR = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = sTR;
                    dtinfo.ShortDatePattern = "dd/MM/yyyy";
                    dtinfo.DateSeparator = "/";
                    string remotearea;
                    if (chkRArea.Checked == true)
                        remotearea = "Yes";
                    else
                        remotearea = "No";
                    string easilyaccessible;
                    if (chkEAccessible.Checked == true)
                        easilyaccessible = "Yes";
                    else
                        easilyaccessible = "No";
                    string residentialarea;
                    if (chkResidential.Checked == true)
                        residentialarea = "Yes";
                    else
                        residentialarea = "No";
                    string commercialarea;
                    if (chkCommercial.Checked == true)
                        commercialarea = "Yes";
                    else
                        commercialarea = "No";
                    string withinthecity;
                    if (chkWCity.Checked == true)
                        withinthecity = "Yes";
                    else
                        withinthecity = "No";
                    string outskirtsofthecity;
                    if (chkOCity.Checked == true)
                        outskirtsofthecity = "Yes";
                    else
                        outskirtsofthecity = "No";
                    cmd.CommandText = "insert into IM(Name,ID,PAddress,PCity,PState,PPincode,RAddress,RCity,RState,RPincode,Phone,Fax,Mobile,Email,RemArea,EAccess,ResArea,ComArea,InCity,OutCity,Airport,ToCountry,Railway,ToCity,BusStop,ToBusArea,Estd,AcademicStatus,InsType,CourseConduct,StdNo,RegDate,Active,DisActiveDate,GID,PandingDate,Address2)values(@Name,@ID,@PAddress,@PCity,@PState,@PPincode,@RAddress,@RCity,@RState,@RPincode,@Phone,@Fax,@Mobile,@Email,@RemArea,@EAccess,@ResArea,@ComArea,@InCity,@OutCity,@Airport,@ToCountry,@Railway,@ToCity,@BusStop,@ToBusArea,@Estd,@AcademicStatus,@InsType,@CourseConduct,@StdNo,@RegDate,@Active,@DisActiveDate,@GID,@PandingDate,@Address2)";
                    // cmd = new SqlCommand(stn, con);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
                    string imid = genid();
                    if (rbtnAlone.Checked == true)
                        lblGInfo.Text = imid.ToString();
                    lblEnrolment.Text = imid.ToString();
                    cmd.Parameters.AddWithValue("@ID", imid.ToString());
                    cmd.Parameters.AddWithValue("@PAddress", txtPAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@PCity", ddlCity.Text.ToString());
                    cmd.Parameters.AddWithValue("@PState", ddlState.SelectedItem.Text.ToString());
                    cmd.Parameters.AddWithValue("@PPincode", txtPPincode.Text.ToString());
                    cmd.Parameters.AddWithValue("@RAddress", txtCAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@RCity", txtCCity.Text.ToString());
                    cmd.Parameters.AddWithValue("@RState", txtCState.Text.ToString());
                    cmd.Parameters.AddWithValue("@RPincode", txtCPin.Text.ToString());
                    cmd.Parameters.AddWithValue("@Phone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Fax", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
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
                    cmd.Parameters.AddWithValue("@ToBusArea ", txtNArea.Text.ToString());
                    cmd.Parameters.AddWithValue("@Estd", txtYEstablishment.Text.ToString());
                    cmd.Parameters.AddWithValue("@AcademicStatus", txtASInstitution.Text.ToString());
                    cmd.Parameters.AddWithValue("@InsType", txttypeig.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CourseConduct", txtCRecognizedby.Text.ToString());
                    cmd.Parameters.AddWithValue("@StdNo", txtNSPresent.Text.ToString());
                    if (txtDate.Text == "") txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cmd.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@Active", "Register");
                    cmd.Parameters.AddWithValue("@DisActiveDate", Convert.ToDateTime(txtDate.Text, dtinfo).AddMonths(12));
                    cmd.Parameters.AddWithValue("@GID", lblGInfo.Text.ToString());
                    cmd.Parameters.AddWithValue("@PandingDate", Convert.ToDateTime(txtDate.Text, dtinfo).AddMonths(20));
                    cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.ToString());
                    con.Close(); con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "insert into MemberFee(ID,MType,Amt, FeeType,SubDate,SubType,AcountNo,DD,Bank,YearFrom,YearTo,TransType,Balance,TransID) Values(@IMID,@MType,@Amt,@FeeType,@SubDate,@SubType,@AcountNo,@DD,@Bank,@YearFrom,@YearTo, @TransType,@Balance,@TransID)";
                    cmd.Parameters.AddWithValue("@IMID", imid.ToString());
                    cmd.Parameters.AddWithValue("@MType", "NULL");
                    cmd.Parameters.AddWithValue("@Amt", 0);
                    cmd.Parameters.AddWithValue("@FeeType", "NULL");
                    cmd.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@SubType", "Default");
                    cmd.Parameters.AddWithValue("@AcountNo", "N/A");
                    cmd.Parameters.AddWithValue("@DD", "N/A");
                    cmd.Parameters.AddWithValue("@Bank", "N/A");
                    cmd.Parameters.AddWithValue("@YearFrom", ddlSession.SelectedValue.ToString() + txtYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@YearTo", ddlSession.SelectedValue.ToString() + (Convert.ToInt32(txtYear.Text) + 1).ToString());
                    cmd.Parameters.AddWithValue("@TransType", "Credit");
                    cmd.Parameters.AddWithValue("@Balance", 0);
                    cmd.Parameters.AddWithValue("@TransID", 0);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "insert into IMAC(IMID,Limit,Range) Values(@IMI,@Limit,@Range)";
                    cmd.Parameters.AddWithValue("@IMI", imid.ToString());
                    cmd.Parameters.AddWithValue("@Limit", txtAcApprovalLimit.Text);
                    cmd.Parameters.AddWithValue("@Range", 0);
                    string gidd = lblGInfo.Text.ToString();
                    cmd.ExecuteNonQuery();

                    string dateEnroll = DateTime.Now.ToShortDateString();
                    cmd.CommandText = "Insert into Member(Name,LName,Designation,Address,State,PinCode,TelNo,Office,ResNo,Mobile,Email,ID,HQualification,Experience,Type,Active,RegDate,RenewDate,ExpDate,YearFrom,YearTo,ExpStatus,Address2) Values(@Nme,@LName,@Designation,@Address,@State,@PinCode,@TelNo,@Office,@ResNo,@Mbile,@Emal,@IDu,@HQualification,@Experience,@Type,@Actve,@RgDate,@RnewDate,@ExpDat,@YrFrom,@YrTo,@ExpSttus,@Adress2)";
                    cmd.Parameters.AddWithValue("@Nme", txtName.Text.ToString());
                    cmd.Parameters.AddWithValue("@Designation", "");
                    cmd.Parameters.AddWithValue("@Address", txtPAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@LName", ddlCity.Text.ToString());
                    cmd.Parameters.AddWithValue("@State", ddlState.SelectedItem.Text.ToString());
                    cmd.Parameters.AddWithValue("@PinCode", txtPPincode.Text.ToString());
                    cmd.Parameters.AddWithValue("@TelNo", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Office", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@ResNo", txtMobile.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mbile", txtMobile.Text.ToString());
                    cmd.Parameters.AddWithValue("@Emal", txtEmail.Text.ToString());
                    cmd.Parameters.AddWithValue("@IDu", imid.ToString());
                    cmd.Parameters.AddWithValue("@HQualification", "Education");
                    cmd.Parameters.AddWithValue("@Experience", "Experience");
                    cmd.Parameters.AddWithValue("@Type", "IM");
                    cmd.Parameters.AddWithValue("@Actve", "Register");
                    cmd.Parameters.AddWithValue("@RgDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@RnewDate", Convert.ToDateTime(txtDate.Text, dtinfo).AddMonths(12));
                    cmd.Parameters.AddWithValue("@ExpDat", Convert.ToDateTime(txtDate.Text, dtinfo).AddMonths(20));
                    cmd.Parameters.AddWithValue("@YrFrom", ddlSession.SelectedValue.ToString() + txtYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@YrTo", ddlSession.SelectedValue.ToString() + (Convert.ToInt32(txtYear.Text) + 1).ToString());
                    cmd.Parameters.AddWithValue("@ExpSttus", 0);
                    cmd.Parameters.AddWithValue("@Adress2", txtAddress2.Text.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Insert into IMBooks(IMID,CPartI,CPartII,CSectionA,CSectionB,APartI,APartII,ASectionA,ASectionB,CPartIIE,APartIIE,CourseID) Values('" + imid.ToString() + "',0,0,0,0,0,0,0,0,0,0,'081')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "update DiaryEntry set Status='CountDispatch',IMID='" + imid.ToString() + "',MembershipNo='" + imid.ToString() + "',DiaryType='Forms' where DiaryNo='" + Request.QueryString["id"] + "'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText="select session from DairyCount where DairyNo='" + Request.QueryString["id"] + "'";
                    string sn = Convert.ToString(cmd.ExecuteScalar());
                    if (sn == "")
                    {
           cmd.CommandText = "insert into DairyCount(DairyNo,Session,Department,CurrentDate,DairyType,IMID,Status) Values(@DiaryNo,@Session,@Department,@CurrentDate,@DairyType,@Imcode,@Status)";
           cmd.Parameters.AddWithValue("@DiaryNo", Request.QueryString["id"].ToString());
           cmd.Parameters.AddWithValue("@Session", ddlSession.SelectedValue.ToString() + txtYear.Text);
           cmd.Parameters.AddWithValue("@Department","Director");
           cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);
           cmd.Parameters.AddWithValue("@DairyType", "Forms");
           cmd.Parameters.AddWithValue("@Imcode", imid.ToString());
           cmd.Parameters.AddWithValue("@Status", "NotOpen");
           cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "update DairyCount set IMID='" + imid.ToString() + "' where DairyNo='" + Request.QueryString["id"] + "'";
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = "insert into IMStock(SubCode) select SubjectMaster.SubjectCode from SubjectMaster where SubjectMaster.CourseID='081'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "update IMStock set IMID='" + imid.ToString() + "',Total=0,Stock=0,Advance=0 where IMID is NULL";
                    cmd.ExecuteNonQuery();

                    // Insert into IMAccount.
                    XElement feelist=XElement.Load(HttpContext.Current.Server.MapPath("~/XML/AmountHeader.xml"));
                    IEnumerable<XElement> feelistele = feelist.Elements();
                    foreach (var fee in feelistele)
                    {
                        cmd.CommandText = "Insert into IMAccount(IMID,Fees,GID) VAlues('" + lblEnrolment.Text + "','"+fee.Element("Aname").Value.ToString()+"','"+lblEnrolment.Text.ToString()+"')";
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = "";
                    sTR.Commit();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Sucessfully Registered')", true);
                }
            }
        }
        catch (SqlException ex)
        {
            sTR.Rollback();
            lblException.Text = ex.ToString();
        }
        catch (InvalidOperationException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Registered')", true);
        }
        finally
        {

            con.Close(); btnCancel.Focus();
        }
    }
    protected void chkSameAddress_CheckChanged(object sender, EventArgs e)
    {
        if (chkSameAddress.Checked == true)
        {
            txtCAddress.Text = txtPAddress.Text.ToString() + "  " + txtAddress2.Text.ToString();
            txtCCity.Text = ddlCity.Text.ToString();
            txtCState.Text = ddlState.SelectedItem.Text.ToString();
            txtCPin.Text = txtPPincode.Text.ToString();
        }
        else if (chkSameAddress.Checked == false)
        {
            txtCAddress.Text = "";
            txtCCity.Text = ""; txtCState.Text = ""; txtCPin.Text = "";
        }
        txtCAddress.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMHead.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    public string genid()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(SN) from Member", con);
        con.Close();
        con.Open();
        int i=0;
        string id = Convert.ToString(cmdsn.ExecuteScalar());
        con.Close();
        if (id == "")
        {
            i = 1;
        }
        else
        {
            i = Convert.ToInt32(id);
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
            id = Convert.ToString(i + 1);
        }
        id = "ICE" + id.ToString() + "I";
        return id;
    }
    public string gidd()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(SN) from IM", con);
        con.Close();
        con.Open();
        int i = 0;
        string id = Convert.ToString(cmdsn.ExecuteScalar().ToString());
        con.Close();
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
            id = Convert.ToString(i + 1);
        }
        id = "ICE" + id.ToString();
        return id;
    }
    protected void rbtnAlone_CheckedChanged(object sender, EventArgs e)
    {
        grouptable.Visible = false;
        lblGInfo.Visible = true;
        txtName.Focus();
    }
    protected void rbtnGroup_CheckedChanged(object sender, EventArgs e)
    {
        grouptable.Visible = true;
        lblGInfo.Visible = false;
        txtRefIM.Focus();
    }
    protected void lbtnViewGrup_click(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        lblGInfo.Visible = true;
        SqlCommand cmd = new SqlCommand("select GID from IM where ID='" + txtRefIM.Text.ToString() + "'", con);
        string gid = Convert.ToString(cmd.ExecuteScalar());
        if (gid == "")
        {
            lblGInfo.Text = "IM ID Invalid !!!";
            txtRefIM.Text="";
        }
        else
        {
            lblGInfo.Text = gid.ToString();
        }
        con.Close();
    }
    protected void lbtnIMHeadTitle_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "" && lblEnrolment.Text=="")
        {
            lblTitleInfo.Text = "Please First Register IM.";
        }
        else
        {
            Response.Redirect("IMHead.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id="+lblEnrolment.Text.ToString());
        }
    }
}