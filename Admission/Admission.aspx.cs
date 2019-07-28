using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Data;
using System.Globalization;

public partial class Admission_Admission : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    ClsStateCity clstate = new ClsStateCity();
    private static string  statusApp;
    double fee1, fee2;
    SqlCommand cmd;
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
                if (!IsPostBack)
                {
                    clstate.xmlstate(ddlState, "XMLState.xml");
                    clstate.xmlstate(ddlState2, "XMLState.xml");
                    txtEnrolment.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    panelMemberIDIS.Visible = false; pnlAdmission.Visible = false;
                    panleSpace.Visible = true;
                    BindCountry();
                    con.Close(); con.Open();
                    lblFeeLevel.Text = getcurrentfees();
                    con.Close(); con.Dispose();
                    txticesn.Focus();
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
    private void BindCountry()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("countries.xml"));
        foreach (XmlNode node in doc.SelectNodes("//country"))
        {
            ddlNationality.Items.Add(new ListItem(node.InnerText, node.Attributes["code"].InnerText));
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    private static string appStream, appCourse, appPart;
    private void chkEligibility()
    {
        if (lblstream.Text != appStream)
            btnSAVE.Enabled = false;
       else if (lblcourse.Text != appCourse)
            btnSAVE.Enabled = false;
        else if (lblPart.Text != appPart)
            btnSAVE.Enabled = false;
        else
            btnSAVE.Enabled =true;
    }
    protected void btnOK_clicck(object sendeer, EventArgs e)
    {
        if (txticesn.Text == "")
            lblException.Text = "Please Insert Serial No. !";
        else
            pnlIMIDapp.Visible = true;
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            int i = 0;
            if (txticesn.Text == "")
            {
                lblExceptionApp.Text = "Please Insert Serial No.";
                txticesn.Focus();
            }
            else
            {
                string sn = txticesn.Text.ToString() + "NewAdmission,";
                ClsStatus clStatus = new ClsStatus();
                bool bl = clStatus.changeStatus("Admission", txticesn.Text.ToString(), "", lblSeasonHidden.Text.ToString());
                if (bl == false)
                {
                    lblExceptionApp.Text = "Record Not Found !";
                    lblExceptionApp.ForeColor = System.Drawing.Color.Red;
                    lblStreamApp.Text = ""; lblCourseApp.Text = ""; lblIMIDAPP.Text = ""; lblPartApp.Text = ""; txtName.Text = "";
                    txtFather.Text = ""; lblExceptDob.Text = ""; txtDOB.Text = ""; txtAge.Text = ""; pnlAdmission.Visible = false;
                    panleSpace.Visible = true;
                    txticesn.Focus();
                }
                else
                {
                    cmd = new SqlCommand("select AppNo from AppRecord where FormType like '%" + sn.ToString() + "%'  and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
                    con.Close(); con.Open();
                    string apno = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                    if (apno != "")
                    {
                        i += 1;
                        lblLavelApp.Text = apno.ToString();
                    }
                }
            }
            if (i == 1)
            {
                pnlAdmission.Visible = true;
                panleSpace.Visible = false;
                con.Close(); con.Open();
                cmd = new SqlCommand("select * from AppRecord where AppNo='" + Convert.ToInt32(lblLavelApp.Text) + "' and Status!='NotApproved' and Status!='Hold' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    
                    lblEnrolment.Text = sdr["Enrolment"].ToString();
                    lblIMIDAPP.Text = sdr["IMID"].ToString();
                    lblstream.Text = sdr["Stream"].ToString();
                    if (lblstream.Text == "Asso")
                    {
                        lblStreamApp.Text = "Associate Examination";
                    }
                    else if (lblstream.Text == "Tech")
                    {
                        lblStreamApp.Text = "Technician Examination";
                    }
                    lblcourse.Text = sdr["Course"].ToString();
                    if (lblcourse.Text == "Civil") lblCourseApp.Text = "Civil Engineering";
                    else if (lblcourse.Text == "Architecture") lblCourseApp.Text = "Architectural Engineering";
                    lblPart.Text = sdr["Part"].ToString();
                    if (lblPart.Text == "PartI")
                    {
                        lblPartApp.Text = "PartI";
                        ddlAdmissionStatus.SelectedValue = "Regular";
                    }
                    else if (lblPart.Text == "PartII")
                    {
                        ddlAdmissionStatus.SelectedValue = "Direct";
                        lblPartApp.Text = "PartII";
                    }
                    else if (lblPart.Text == "SectionA")
                    {
                        ddlAdmissionStatus.SelectedValue = "Regular";
                        lblPartApp.Text = "SectionA";
                    }
                    else if (lblPart.Text == "SectionB")
                    {
                        ddlAdmissionStatus.SelectedValue = "Direct";
                        lblPartApp.Text = "SectionB";
                    }
                    appStream = lblstream.Text.ToString();
                    appCourse = lblcourse.Text.ToString();
                    appPart = lblPart.Text.ToString();
                    txtName.Text = sdr["Name"].ToString();
                    txtFather.Text = sdr["FName"].ToString();
                    txtDOB.Text = Convert.ToDateTime(sdr["DOB"].ToString()).ToString("dd/MM/yyyy");
                    //--------
                    hello(); //Date of Birth Age
                    //---------
                    statusApp = sdr["Status"].ToString();
                }
                sdr.Close();
                cmd = new SqlCommand("select * from FeeMaster where FeeType='" + appStream.ToString() + "' and FeeLevel='" +lblFeeLevel.Text.ToString() + "' and  Status='Active'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    fee1 = Convert.ToDouble(sdr[1].ToString());
                    fee2 = Convert.ToDouble(sdr[3].ToString());
                }
                sdr.Close();
                if (lblPartApp.Text == "PartI" || lblPartApp.Text == "SectionA")
                    lblEnrollFee.Text = fee1.ToString();
                else lblEnrollFee.Text = fee2.ToString();
                sdr.Dispose();
                chkEligibility();
                con.Close();
            }
        }
        catch (SqlException ex)
        {
            lblExceptionApp.Text = ex.ToString();
        }
        finally
        {
            con.Dispose();
            txtName.Focus();
        }
    }
    protected void chkSameAddress_CheckChanged(object sender, EventArgs e)
    {
        if (chkSameAddress.Checked == true)
        {
            CAddress.Text = txtPAddress.Text.ToString();
            CAddress2.Text = txtPaddress2.Text.ToString(); 
            ddlState2.SelectedItem.Text = ddlState.SelectedItem.Text.ToString();
            txtCCity.Text = txtPCity.Text.ToString();
        }
        else if (chkSameAddress.Checked == false)
        {
            CAddress.Text = "";
            CAddress2.Text = "";
        }
      txtPhoneNo.Focus();
    }
    protected void txtDOB_OntextChanged(object sender, EventArgs e)
    {
        hello();
        txtAge.Focus();
    }
    private void hello()
    {
        try
        {
            DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            DateTime dt = Convert.ToDateTime(txtDOB.Text, dtinfo);
            lblExceptDob.Text = "";

            int year = DateTime.Now.Year - dt.Year;
            if (DateTime.Now.Month < dt.Month || DateTime.Now.Month == dt.Month && DateTime.Now.Day < dt.Day)
                --year;
            if (lblPart.Text == "PartI" | lblPart.Text == "PartII")
            {
                if (year < 16)
                {
                    lblExceptDob.Text = "Can't under 16 year old  for Technical Examination";
                }
                else lblExceptDob.Text = year.ToString() + "th Year Old.";
            }
            else if (lblPart.Text == "SectionA" | lblPart.Text == "SectionB")
            {
                if (year < 18)
                {
                    lblExceptDob.Text = "Can't under 18 year old  for Associate Examination";
                }
                else lblExceptDob.Text = year.ToString() + "th Year Old.";
            }
            txtAge.Text = year.ToString();
        }
        catch (FormatException ex)
        {
            lblExceptDob.Text = "Invalid Date of Birth. " + txtDOB.Text.ToString();
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        btnOK.Focus();
    }
    private double f1, f2, amt, amt2, amtdiff; string stream;
    protected void btnSendForApproval_Onclick(object sender, EventArgs e)
    {
    if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
    {
        stream = "Tech";
    }
    else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
    {
        stream = "Asso";
    }
        if (lblCourseApp.Text.Contains(ddlCourse.SelectedValue) && lblPartApp.Text.Contains(ddlPart.SelectedValue))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Course/Part same as before')", true);
        }
        else
        {
            con.Close();
            con.Open();
            string type = "N/A";
            f1 = 0; f2 = 0; amt2 = 0; amtdiff = 0;
            cmd = new SqlCommand("select Status from RecoverApp where Session='" + lblSeasonHidden.Text.ToString() + "' and FormType='Admission' and SerialNo='" + txticesn.Text.ToString() + "'", con);
            string getsn = Convert.ToString(cmd.ExecuteScalar());
            if (getsn != "" | getsn == "NotApproved" && getsn!="Approved")
            {
                lblExceptionEli.Text = "Already Submitted for Approval, and it is " + getsn.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Already Submitted for Approval, and it is " + getsn.ToString() + ".')", true);
            }
            else
            {
                if (lblLavelApp.Text == "")
                {
                    lblExceptionApp.Text = "Please insert Serial No.!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Insert Serial No.')", true);
                }
                else
                {
                    cmd = new SqlCommand("select * from FeeMaster where FeeType='" +stream+ "' and FeeLevel='" + lblFeeLevel.Text.ToString() + "' and Status='Active'", con);
                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        f1 = Convert.ToDouble(reader[1].ToString());
                        f2 = Convert.ToDouble(reader[3].ToString());
                    }
                    reader.Close();
                    if (lblPartApp.Text == "PartI" | lblPartApp.Text == "SectionA")
                    {
                        amt2 = f1;
                    }
                    else
                    {
                        amt2 = f2;
                    }
                    amt = Convert.ToDouble(lblEnrollFee.Text);
                    if (amt2 > amt)
                    {
                        type = "Debit";
                        amtdiff = amt2 - amt;
                    }
                    else if (amt2 <= amt)
                    {
                        type = "Credit";
                        amtdiff = amt - amt2;
                    }
                    cmd = new SqlCommand("Insert into RecoverApp(FormType,Amount,Type,Enrolment,IMID,SerialNo,Session,Course,Part,Status,Remark) values(@FormType,@Amount,@Type,@Enrolment,@IMID,@SerialNo,@Session,@Course,@Part,@Status,@Remark)", con);
                    cmd.Parameters.AddWithValue("@FormType", "Admission");
                    cmd.Parameters.AddWithValue("@Amount", amtdiff.ToString());
                    cmd.Parameters.AddWithValue("@Type", type.ToString());
                    cmd.Parameters.AddWithValue("@Enrolment", lblEnrolment.Text.ToString());
                    cmd.Parameters.AddWithValue("@IMID", lblIMIDAPP.Text.ToString());
                    cmd.Parameters.AddWithValue("@SerialNo", txticesn.Text.ToString());
                    cmd.Parameters.AddWithValue("@Session", lblSeasonHidden.Text.ToString());
                    cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Status", "NotApproved");
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Submitted for Approval.')", true);
                    lblExceptionEli.Text = "Successfully Submitted for Approval.";
                }
                con.Close();
                con.Dispose();
            }
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        string url;
        url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void btnSAVE_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Close();
            con.Open();
            cmd = new SqlCommand("select * from IM where ID='" + lblIMIDAPP.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                lblIMAddress.Text = reader[3].ToString();
                lblIMCity.Text = "City: " + reader[4].ToString() + " State: " + reader[5].ToString();
            }
            reader.Close();
            cmd = new SqlCommand("spStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SID", SqlDbType.NVarChar).Value = lblEnrolment.Text.ToString();
            cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = txtName.Text.ToString();
            cmd.Parameters.AddWithValue("@FName", SqlDbType.NVarChar).Value = txtFather.Text.ToString();
            cmd.Parameters.AddWithValue("@MName", SqlDbType.NVarChar).Value = txtMother.Text.ToString();
            cmd.Parameters.AddWithValue("@PAddress", SqlDbType.NVarChar).Value = txtPAddress.Text.ToString();
            cmd.Parameters.AddWithValue("@PAddress2", SqlDbType.NVarChar).Value = txtPaddress2.Text.ToString();
            cmd.Parameters.AddWithValue("@PCity", SqlDbType.NVarChar).Value = txtPCity.Text.ToString();
            cmd.Parameters.AddWithValue("@PState", SqlDbType.NVarChar).Value = ddlState.SelectedItem.Text.ToString();
            cmd.Parameters.AddWithValue("@CAddress", SqlDbType.NVarChar).Value = CAddress.Text.ToString();
            cmd.Parameters.AddWithValue("@CAddress2", SqlDbType.NVarChar).Value = CAddress2.Text.ToString();
            cmd.Parameters.AddWithValue("@CCity", SqlDbType.NVarChar).Value = txtCCity.Text.ToString();
            cmd.Parameters.AddWithValue("@CState", SqlDbType.NVarChar).Value = ddlState2.SelectedItem.Text.ToString();
            cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = txtPhoneNo.Text.ToString();
            cmd.Parameters.AddWithValue("@Mobile", SqlDbType.NVarChar).Value = txtMobile.Text.ToString();
            cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.ToString();
            cmd.Parameters.AddWithValue("@DOB", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDOB.Text.ToString(), dtinfo);
            if (txtAge.Text == "") txtAge.Text = "0";
            cmd.Parameters.AddWithValue("@Age", SqlDbType.NVarChar).Value = Convert.ToDecimal(txtAge.Text);
            cmd.Parameters.AddWithValue("@Nationality", SqlDbType.NVarChar).Value = ddlNationality.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = ddlCategory.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@IMID", SqlDbType.NVarChar).Value = lblIMIDAPP.Text.ToString();
            cmd.Parameters.AddWithValue("@IMName", SqlDbType.NVarChar).Value = lblIMName.Text.ToString();
            cmd.Parameters.AddWithValue("@IMCity", SqlDbType.NVarChar).Value = lblIMCity.Text.ToString();
            cmd.Parameters.AddWithValue("@EnrollDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtEnrolment.Text, dtinfo);
            cmd.Parameters.AddWithValue("@AppId", SqlDbType.NVarChar).Value = lblLavelApp.Text.ToString();
            cmd.Parameters.AddWithValue("@Stream", SqlDbType.NVarChar).Value = lblstream.Text.ToString();
            cmd.Parameters.AddWithValue("@Course", SqlDbType.NVarChar).Value = lblcourse.Text.ToString();
            cmd.Parameters.AddWithValue("@Part", SqlDbType.NVarChar).Value = lblPart.Text.ToString();
            cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "NotApprove";
            cmd.Parameters.AddWithValue("@Gender", SqlDbType.NVarChar).Value = ddlGender.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@Session", SqlDbType.NVarChar).Value = lblSeasonHidden.Text.ToString();
            cmd.Parameters.AddWithValue("@AnnualSubscription", SqlDbType.NVarChar).Value = lblSeasonHidden.Text.ToString();
            cmd.Parameters.AddWithValue("@FeeLevel", SqlDbType.NVarChar).Value = lblFeeLevel.Text;
            if (ddlPrifix.SelectedValue.ToString() == "Father")
            {
                if (ddlGender.SelectedValue.ToString() == "Male")
                    cmd.Parameters.AddWithValue("@Prifix", SqlDbType.NVarChar).Value = "s/o";
                else if (ddlGender.SelectedValue.ToString() == "Female")
                    cmd.Parameters.AddWithValue("@Prifix", SqlDbType.NVarChar).Value = "d/o";
                else
                    cmd.Parameters.AddWithValue("@Prifix", SqlDbType.NVarChar).Value = "s/o";
            }
            else if (ddlPrifix.SelectedValue.ToString() == "Husband")
            {
                if (ddlGender.SelectedValue.ToString() == "Male")
                    cmd.Parameters.AddWithValue("@Prifix", SqlDbType.NVarChar).Value = "s/o";
                else if (ddlGender.SelectedValue.ToString() == "Female")
                    cmd.Parameters.AddWithValue("@Prifix", SqlDbType.NVarChar).Value = "w/o";
                else
                    cmd.Parameters.AddWithValue("@Prifix", "s/o");
            }
            cmd.Parameters.AddWithValue("@Remarks", SqlDbType.NVarChar).Value = txtRemarks.Text.ToString();
            cmd.Parameters.AddWithValue("@ExmpRemarks", SqlDbType.NVarChar).Value = txtExmpRemarks.Text.ToString();
            if (chkExp.Checked == true) cmd.Parameters.AddWithValue("@ExpStatus", SqlDbType.NVarChar).Value = "yes";
            else cmd.Parameters.AddWithValue("@ExpStatus", SqlDbType.NVarChar).Value = "no";
            if (chkDoc.Checked == true) cmd.Parameters.AddWithValue("@DocStatus", SqlDbType.NVarChar).Value = "yes";
            else cmd.Parameters.AddWithValue("@DocStatus", SqlDbType.NVarChar).Value = "no";
            if (ddlPart.SelectedValue == "PartI" || ddlPart.SelectedValue == "SectionA")
                cmd.Parameters.AddWithValue("@AdmissionStatus", SqlDbType.NVarChar).Value =ddlAdmissionStatus.SelectedValue.ToString();
            else cmd.Parameters.AddWithValue("@AdmissionStatus", SqlDbType.NVarChar).Value = ddlAdmissionStatus.SelectedValue.ToString();
            con.Close(); con.Open();
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("select max(SN) from Student", con);
            string snid = Convert.ToString(cmd.ExecuteScalar());
            cmd = new SqlCommand("insert into Docs(SID,DocsStatus) values(@SID,@DocsStatus)", con);
            cmd.Parameters.AddWithValue("@SID", snid);
            cmd.Parameters.AddWithValue("@DocsStatus", 0);
            cmd.ExecuteNonQuery();
            lblException.Text = lblLavelApp.Text.ToString(); Session["sid"] = lblLavelApp.Text.ToString();
            lblException.Visible = false;
            cmd = new SqlCommand("update AppRecord set Status=@Status where AppNo='" + Convert.ToInt32(lblLavelApp.Text) + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Status", statusApp + " Admission");
            cmd.ExecuteNonQuery();
            panelMemberIDIS.Visible = true;
            btnSAVE.Enabled = false;
        }
        catch (SqlException ex)
        {
            btnSAVE.Enabled = false;
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
            btnSAVE.Enabled = false;
            btnclear.Focus();
        }
    }
    private string getcurrentfees()
    {
        cmd = new SqlCommand("select Max(FeeLevel) from FeeMaster where Status='Active'", con);
        string level = Convert.ToString(cmd.ExecuteScalar());
        lblFeeLevel.Text = level.ToString();
        return level;
    }
}