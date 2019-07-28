using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;

public partial class FO_DiaryEntry : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
   
    ClsStateCity clstate = new ClsStateCity();
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
                    lblOtherCity.Visible = false;
                    
                    txtNewCity.Visible = false;
                    clstate.xmlstate(ddlState, "XMLState.xml");
                    clstate.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
                    maikal dev = new maikal();
                    maikal mk = new maikal();
                    int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtDiaryDate.Text = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    tbltext.Visible = false;
                    lblFromName.Text = "IM ID"; panelCourier.Visible = false;
                    getrefno();
                }
                ddlExamSeason.Focus();
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

    protected void btnCencel_Onclick(object sender, EventArgs e)
    {            
       
        txtSName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtPincode.Text = "";
        txtPhonecode.Text = "";
        txtPhoneNo.Text = "";
        txtCourierNo.Text = "";
        txtDiaryDate.Text = "";
        txtConsignmentNo.Text = "";
        txtRemoark.Text = "";
        txtWt.Text = "";
        txtAmt.Text = "";
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
       
            if (lvl == 0)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
   
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        ddlExamSeason.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    public string formstatus;
    protected void txtSName_TExtChnaged(object sender, EventArgs e)
    {
        lblExcepitonDiary.Text = "";
        btnSaveDiary.Enabled = true;
        lblExceptiontbl.Text = "";
        lblName.Text = "";
        lblCode.Text = "";
        lblCourseAddress.Text = "";
        lblState.Text = "";
        maikal mk = new maikal();
        if (ddlRecivefrom.SelectedValue == "IM")
        {
            string strimsts = mk.SearchProfile(txtSName.Text.ToString());
            if (strimsts == "No") { lblExceptiontbl.Text = "Invalid IM Membership No."; txtSName.Focus(); formstatus = "No"; }
            else
            {
                lblExceptiontbl.Text = ""; formstatus = "Yes";
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("select * from IM where ID='" + txtSName.Text.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtAddress1.Text = reader["PAddress"].ToString();
                    txtAddress2.Text = Convert.ToString(reader["Address2"].ToString());
                    lblState.Text =Convert.ToString( reader["PState"].ToString());
                    txtPhoneNo.Text =Convert.ToString( reader["Mobile"].ToString());
                    txtPincode.Text = Convert.ToString(reader["PPinCode"].ToString());
                    lblName.Text = Convert.ToString(reader["Name"].ToString());
                    lblCode.Text = Convert.ToString(reader["PCity"].ToString());
                 
                }
                reader.Close();
                con.Close(); txtDiaryDate.Focus();
            }
        }
        else if (ddlRecivefrom.SelectedValue == "Student")
        {
            lblCode.Text = "";
            lblCourseAddress.Text = "";
            lblExceptiontbl.Text = "";


            string[] stdsts = new string[2];
            Student std = new Student();
            stdsts = std.status(txtSName.Text.ToString());
            if (stdsts[1].ToString() == "No")
            {
                lblExceptiontbl.Text = "Invalid Student Membership No";
                formstatus = "No"; txtSName.Focus();
            }
            else
            {
                bool bl = mk.chkstatusStu(txtSName.Text.ToString());
                if (bl == false) { lblExceptiontbl.Text = "Student Membership Not Activated"; formstatus = "No"; txtSName.Focus(); }
                else
                {
                    lblExceptiontbl.Text = ""; formstatus = "Yes";
                    con.Close(); con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Student where SID='" + txtSName.Text.ToString() + "'", con);
                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txtAddress1.Text = reader["PAddress"].ToString();
                        txtAddress2.Text = Convert.ToString(reader["PAddress2"].ToString());
                        lblState.Text = Convert.ToString(reader["PState"].ToString());
                        txtPhoneNo.Text = Convert.ToString(reader["Mobile"].ToString());

                        lblName.Text = "Student Name: " + Convert.ToString(reader["Name"].ToString());
                        lblCode.Text = Convert.ToString(reader["PCity"].ToString());
                        lblCourseAddress.Text = "Status: " + Convert.ToString(reader["Status"].ToString());
                    }
                    reader.Close();
                    con.Close(); txtDiaryDate.Focus();
                }
            }
        }
        else if (ddlRecivefrom.SelectedValue == "Other")
        {

        }
        con.Close();
      
    }
    protected void ddlRecive_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        txtNewCity.Text = "";
        txtAddress2.Text = "";
        txtAddress1.Text = "";
        lblName.Text = "";
        lblCode.Text = "";
        lblCourseAddress.Text = "";
        lblState.Text = "";
        btnSaveDiary.Enabled = true;
        txtSName.Text = "";
        txtPincode.Text = "";
        txtPhoneNo.Text = "";
    
        if (ddlRecivefrom.SelectedValue == "IM")
        {
            lblFromName.Text = "IM ID";
            tbllabel.Visible = true;
            tbltext.Visible = false;
            tbllabel.Visible = true;
            txtSName.Visible = true;
           
        }
        else if (ddlRecivefrom.SelectedValue == "Student")
        {
            tbllabel.Visible = true;
            tbltext.Visible = false;           
            txtSName.Visible = true;
            lblFromName.Text = "Membership ID";
        }
        else if (ddlRecivefrom.SelectedValue == "Other")
        {
            tbllabel.Visible = false; txtSName.Text = "";
           tbltext.Visible = true;
            lblFromName.Text = "Name Personal";
           
           
        }
        txtSName.Focus();
    }
    protected void btnSAveDiray_Click(object sender, EventArgs e)
    {
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Close();
        con.Open();
        string strr = "insert into CourierRD(CourierType,SendTo,Name,PAddress,CAddress,City,State,Pincode,Phone,Date,CourierService,Session,CourierSerialno,Consignmentno,Weight,Amount)values(@CourierType,@SenderName,@ShipperName,@PAddress,@CAddress,@City,@State,@Pincode,@Phone,@Date,@CourierService,@CourierAddress,@CourierSerialno,@Consignmentno,@Weight,@Rupees) ";
        SqlCommand cmd = new SqlCommand(strr, con);
        cmd.Parameters.AddWithValue("@CourierType", txtDiraryType.Text.ToString());
        cmd.Parameters.AddWithValue("@SenderName", ddlRecivefrom.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@ShipperName", txtSName.Text);
        if (ddlRecivefrom.SelectedItem.Text == "Other")
        {
            cmd.Parameters.AddWithValue("@PAddress", txtAddress1.Text);
            cmd.Parameters.AddWithValue("@CAddress", txtAddress2.Text);
          
            if (ddlCity.SelectedItem.Text == "Other")
            {
                cmd.Parameters.AddWithValue("@City", txtNewCity.Text);
               
            }
            else
                cmd.Parameters.AddWithValue("@City", ddlCity.SelectedItem.Text);
               

            cmd.Parameters.AddWithValue("@State", ddlState.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@Pincode", Convert.ToString(txtPincode.Text));
            cmd.Parameters.AddWithValue("@Phone", Convert.ToString(txtPhonecode.Text + txtPhoneNo.Text));
        }
        else
        {
            cmd.Parameters.AddWithValue("@PAddress", Convert.ToString(txtAddress1.Text));
            cmd.Parameters.AddWithValue("@CAddress",Convert.ToString(txtAddress2.Text));
            cmd.Parameters.AddWithValue("@City", lblCode.Text);
            cmd.Parameters.AddWithValue("@State", lblState.Text);
            cmd.Parameters.AddWithValue("@Pincode",Convert.ToString(txtPincode.Text));
            cmd.Parameters.AddWithValue("@Phone", Convert.ToString(txtPhoneNo.Text));

        }
        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDiaryDate.Text, dtinfo));
        cmd.Parameters.AddWithValue("@CourierService",ddlCourierService.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@CourierAddress",lblHiddenSeason.Text.ToString());
        cmd.Parameters.AddWithValue("@CourierSerialno",txtCourierNo.Text.ToString());
        cmd.Parameters.AddWithValue("@Consignmentno", txtConsignmentNo.Text.ToString());
        cmd.Parameters.AddWithValue("@Weight",Convert.ToDecimal(txtWt.Text.ToString()));    
        cmd.Parameters.AddWithValue("@Rupees",txtAmt.Text);
        cmd.ExecuteNonQuery();
        con.Close();
        btnSaveDiary.Enabled = false;
        txtSName.Text = "";
        tbllabel.Visible = false; txtDiraryType.Text = "";
        txtConsignmentNo.Text = ""; txtRemoark.Text = ""; txtWt.Text = ""; txtAmt.Text = ""; tbltext.Visible = false; 
        lblExcepitonDiary.Text = "Data Saved Succesfully";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Sucessfully Submitted')", true);
        lblState.Text = ""; txtAddress1.Text = ""; txtAddress2.Text = ""; txtPincode.Text = "";
        ddlCity.SelectedIndex = 0;
        ddlRecivefrom .SelectedIndex = 0;
        tbltext.Visible = true;
        
    }
    protected void ibtnNewCourier_Onclick(object sender, EventArgs e)
    {
        panelCourier.Visible = true;
        txtNewCourier.Focus();
    }
    protected void btnSAveNew_Onclick(object sender, EventArgs e)
    {
        if (txtNewCourier.Text == "" )
        {
            lblExceptionNewCourier.Text = "Please Insert Name ";
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ServiceNameMaster(Name,Type) values(@Name,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtNewCourier.Text.ToString());
          
            cmd.Parameters.AddWithValue("@Type", "Courier");
            cmd.ExecuteNonQuery();
            lblExceptionNewCourier.Text = "Successfull Saved New Courier Service Name";
            ddlCourierService.DataBind();
            con.Close();
        }
        btnCencel.Focus();
    }
    protected void btnCencelnew_Onclick(object sender, EventArgs e)
    {
        btnSaveDiary.Enabled = true;
        panelCourier.Visible = false;
    }
    private void getrefno()
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("select Max(CourierSerialno) from CourierRD", con);
            int refno = Convert.ToInt32(cmd.ExecuteScalar());
            txtCourierNo.Text = (refno + 1).ToString();
            con.Close();
        }
        catch (InvalidCastException ex)
        {
            txtCourierNo.Text = "1";
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        clstate.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNewCity.Visible = false;
        lblOtherCity.Visible = false;
        if (ddlCity.SelectedItem.Text == "Other")
        {
            txtNewCity.Visible = true;
            lblOtherCity.Visible = true;
            txtNewCity.Focus();
        }
           

       
    }
    protected void txtPincode_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtAddress1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlExamSeason_SelectedIndexChanged1(object sender, EventArgs e)
    {
        txtYearSeason.Focus();
    }
}
