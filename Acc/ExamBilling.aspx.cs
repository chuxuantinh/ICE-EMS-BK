using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class Acc_ExamBilling : System.Web.UI.Page
{
    SqlConnection con=new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataAdapter ad;
    DataSet ds = null;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal mk = new maikal();
                lblDDNNO.Text = "DD No."; lblAccountNo.Text = "A/C No.";
                lblCurrancyName.Text = " One Rupee Equal To";
                lblCurrancyText.Text = "Currency Value:";
                txtyear.Text = DateTime.Now.Year.ToString();
                lblSeason.Text = ddlSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
                con.Close(); con.Open();
                cmd = new SqlCommand("select Max(SN) from ExamBill", con);
                string sn = cmd.ExecuteScalar().ToString();
                if (sn == "")
                {
                    cmd = new SqlCommand("insert into ExamBill (Name,Code) values('ice','ice')", con);
                    cmd.ExecuteNonQuery();
                    lblException.Text = "Null Record Inserted."; lblSN.Text = "1";
                }
                else
                {
                    lblSN.Text = sn.ToString();
                }
                con.Close(); con.Dispose();
                if (!IsPostBack)
                {
                    tblOther.Visible = false;
                    pnlProfile.Visible = false;
                    panelSubmitAmt.Visible = false;

                }
                ddlSeason.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
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
    protected void txtAmt_textChanged(object sender, EventArgs e)
    {
        totalamt.Visible = true;
        if (txtAmt.Text == "") txtAmt.Text = "1"; if (txtCurrancyValue.Text == "") txtCurrancyValue.Text = "1";
        lblTAmt.Text = txtAmt.Text.ToString();
        lblTAmt.Visible = true;
        double amt = Convert.ToDouble(txtAmt.Text);
        double camt = Convert.ToDouble(txtCurrancyValue.Text);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
        txtAmt.Focus();
    }
    protected void ddlAmtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAmtType.SelectedValue == "DD")
        {
            lblDDNNO.Text = "DD No:";
            lblAccountNo.Text = "Account No:";
            txtDDNO.Visible = true; txtACNO.Visible = true;
            txtDDNO.Focus();
        }
        else if (ddlAmtType.SelectedValue == "Cash")
        {
            lblDDNNO.Text = ""; lblAccountNo.Text = ""; txtACNO.Text = ""; txtDDNO.Text = "";
            txtDDNO.Visible = false; txtACNO.Visible = false;
            txtBank.Focus();
        }
        else if (ddlAmtType.SelectedValue == "CC")
        {
            lblDDNNO.Text = "Chaque No."; txtDDNO.Visible = true; txtDDNO.Text = "";
            lblAccountNo.Text = "Account No:"; txtACNO.Visible = true;
            txtDDNO.Focus();
        }
    }
    protected void ddlBillingType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillingType.SelectedItem.Text == "---Select Billing Type-------")
        {
            pnlProfile.Visible = false;
            panelSubmitAmt.Visible = false;
        }
        pnlProfile.Visible = true;
        tblOther.Visible = false;
        tblProfile.Visible = false;
        if (ddlBillingType.SelectedValue.ToString() == "Documents")
        {
            tblOther.Visible = false;
        }
        else if (ddlBillingType.SelectedValue.ToString() == "PaperSetter")
        {
            tblOther.Visible = false;
        }
        else if (ddlBillingType.SelectedValue.ToString() == "ExamCenter")
        {
            tblOther.Visible = false;
        }
        else if (ddlBillingType.SelectedValue.ToString() == "Invigilator")
        {
            tblOther.Visible = false;
        }
        else if (ddlBillingType.SelectedValue.ToString() == "Other")
        {
            tblOther.Visible = true;
            pnlProfile.Visible = false;
        }
        fillNameDropDown();
        ddlBillingType.Focus();
        bindName();
    }
    protected void ddlName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindName();
    }
    private void fillNameDropDown()
    {
        string qry = "select Name from PaperSetter where Session='" + lblSeason.Text.ToString() + "'";
        if (ddlBillingType.SelectedValue.ToString() == "PaperSetter")
        {
            qry = "select Name from PaperSetter where Session='" + lblSeason.Text.ToString() + "'";
        }
        else if (ddlBillingType.SelectedValue.ToString() == "ExamCenter")
        {
            qry = "select Name from ExamCenter where Season='" + lblSeason.Text.ToString() + "'";
        }
        else if (ddlBillingType.SelectedValue.ToString() == "Invigilator")
        {
            qry = "select Name from Invigilator where Session='" + lblSeason.Text.ToString() + "'";
        }
        else if (ddlBillingType.SelectedValue.ToString() == "Documents")
        {
            qry = "select Name from Supplier";
        }
        ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ds = new DataSet();
        ad.Fill(dt);
        ddlName.Dispose();
        ddlName.DataSource = dt;
        ddlName.DataTextField = "Name";
        ddlName.DataValueField = "Name";
        ddlName.DataBind();
       // ddlName.Items[ddlName.Items.Count - 1].Selected = true;
    }
    protected void txtCurrancyValue_TextChanged(object sender, EventArgs e)
    {
        if (txtAmt.Text == "") txtAmt.Text = "1"; if (txtCurrancyValue.Text == "") txtCurrancyValue.Text = "1";
        lblTAmt.Visible = true;
        double amt = Convert.ToDouble(txtAmt.Text);
        double camt = Convert.ToDouble(txtCurrancyValue.Text);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
        txtCurrancyValue.Focus();
    }
    private void calculate(string amtt, string camtt)
    {
        double amt = Convert.ToDouble(amtt);
        double camt = Convert.ToDouble(camtt);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
    }
    protected void btnSubmitAmt_Click(object sender, EventArgs e)
    {
        DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("insert into ExamBill (ExamSeason,BillType,Name,Code,Date,Address1,Address2,City,State,Pin,Contact,Email,AmtType,DDNO,Bank,ACC,Narration,CurrancyName,CurrancyValue,Amount,BillNo) values(@ExamSeason,@BillType,@Name,@Code,@Date,@Address1,@Address2,@City,@State,@Pin,@Contact,@Email,@AmtType,@DDNO,@Bank,@ACC,@Narration,@CurrancyName,@CurrancyValue,@Amount,@BillNo)", con);
            cmd.Parameters.AddWithValue("@ExamSeason", lblSeason.Text.ToString());
            cmd.Parameters.AddWithValue("@BillType", ddlBillingType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Name", txtPPName.Text.ToString());
            cmd.Parameters.AddWithValue("@Code", txtcode.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDOB.Text.ToString(), dtinfo));
            cmd.Parameters.AddWithValue("@Address1", txtPAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@Address2", txtPPAddress2.Text.ToString());
            cmd.Parameters.AddWithValue("@City", txtPCity.Text.ToString());
            cmd.Parameters.AddWithValue("@State", txtPState.Text.ToString());
            cmd.Parameters.AddWithValue("@Pin", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("@Contact", txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@AmtType", ddlAmtType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@DDNO", txtDDNO.Text.ToString());
            cmd.Parameters.AddWithValue("@Bank", txtBank.Text.ToString());
            cmd.Parameters.AddWithValue("@ACC", txtACNO.Text.ToString());
            cmd.Parameters.AddWithValue("@Narration", txtNarration.Text.ToString());
            cmd.Parameters.AddWithValue("@CurrancyName", ddlCurrancy.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CurrancyValue", txtCurrancyValue.Text.ToString());
            cmd.Parameters.AddWithValue("@Amount", txtAmt.Text.ToString());
            cmd.Parameters.AddWithValue("@BillNo", lblSN.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            lblException.Text = "Amount Save Successfully.";
            lblException.ForeColor = System.Drawing.Color.Green;
            txtPPName.Text = ""; txtcode.Text = ""; txtDOB.Text = ""; txtPAddress.Text = ""; txtPPAddress2.Text = ""; txtPCity.Text = "";
            txtPState.Text = ""; txtPPincode.Text = ""; txtPhoneNo.Text = ""; txtEmail.Text = ""; txtDDNO.Text = ""; txtBank.Text = "";
            txtACNO.Text = ""; txtNarration.Text = ""; txtCurrancyValue.Text = ""; txtAmt.Text = "";
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        catch (FormatException ex)
        {
            lblException.Text = "Incorrect Date Format";
        }

        finally
        {
            con.Dispose();
            ddlSeason.Focus();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtAmt.Text = "0";
        txtNarration.Text = "";
        txtDDNO.Text = "";
        txtCurrancyValue.Text = "1";
        ddlCurrancy.SelectedValue = "RS";
        txtDDNO.Focus();
    }
    protected void ddlCurrancy_Changed(object sender, EventArgs e)
    {
        if (ddlCurrancy.SelectedValue == "RS")
        {
            lblCurrancyText.Visible = false;
            lblCurrancyName.Visible = false;
            txtCurrancyValue.Visible = false;
            txtCurrancyValue.Text = "1";
            calculate(txtAmt.Text, txtCurrancyValue.Text);
        }
        else if (ddlCurrancy.SelectedValue == "DL")
        {
            lblCurrancyName.Text = " One Dolar Equal To";
            lblCurrancyText.Text = "Presenet Currency Value:";
            lblCurrancyText.Visible = true;
            lblCurrancyName.Visible = true;
            txtCurrancyValue.Visible = true;
        }
        else if (ddlCurrancy.SelectedValue == "OT")
        {
            lblCurrancyName.Text = " One Unit Equal To";
            lblCurrancyText.Text = "Presenet Currency Value:";
            lblCurrancyText.Visible = true;
            lblCurrancyName.Visible = true;
            txtCurrancyValue.Visible = true;
        }
        else
        {

        }
        txtAmt.Focus();
    }
    protected void txtyear_TextChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
        ddlSeason.Focus();
    }
    protected void ddlseasion_SelectedINdexChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
        ddlSeason.Focus();
    }
    private void bindName()
    {
        pnlProfile.Visible = true;
        string qry = "";
        if (ddlBillingType.SelectedValue.ToString() == "PaperSetter")
        {
            qry = "select Code,PAddress,PCity,PState,PPin,Mobile,Email from PaperSetter where Name='" + ddlName.SelectedValue.ToString() + "'";
        }
        else if (ddlBillingType.SelectedValue.ToString() == "ExamCenter")
        {
            qry = "select ID,Address,City,State,Pin,Mobile,Email from ExamCenter where Name='" + ddlName.SelectedValue.ToString() + "'";
        }
        else if (ddlBillingType.SelectedValue.ToString() == "Invigilator")
        {
            qry = "select ID,Address,City,State,Pincode,Mobile,Email from Invigilator where Name='" + ddlName.SelectedValue.ToString() + "'";
        }
        else if (ddlBillingType.SelectedValue.ToString() == "Documents")
        {
            qry = "select ID,Address,City,State,PinCode,Mobile,Email from Supplier where Name='" + ddlName.SelectedValue.ToString() + "'";
        }
        if (ddlBillingType.SelectedValue.ToString()== "Other" || ddlBillingType.SelectedValue.ToString()=="Select")
        {
        }
        else
        {
        cmd = new SqlCommand(qry, con);
        con.Close(); con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            txtPPName.Text = ddlName.SelectedItem.Text;
            lblCode.Text = reader[0].ToString();
            lblAddress1.Text = reader[1].ToString();
            lblCity.Text = reader[2].ToString();
            lblState.Text = reader[3].ToString();
            lblPinCode.Text = reader[4].ToString();
            lblConatctNo.Text = reader[5].ToString();
            lblEmail.Text = reader[6].ToString();
            txtcode.Text = lblCode.Text.ToString();
            txtPAddress.Text = lblAddress1.Text.ToString();
            txtPCity.Text = lblCity.Text.ToString();
            txtPState.Text = lblState.Text.ToString();
            txtPPincode.Text = lblPinCode.Text.ToString();
            txtPhoneNo.Text = lblConatctNo.Text.ToString();
            txtEmail.Text = lblEmail.Text.ToString();
            tblProfile.Visible = true;
            panelSubmitAmt.Visible = true;
        }
        reader.Close();
        con.Close();
        con.Dispose();
        }
        if (ddlBillingType.SelectedValue == "Select")
        {
            pnlProfile.Visible = false;
            tblOther.Visible = false;
            panelSubmitAmt.Visible = false;
        }
        if (ddlBillingType.SelectedValue == "Other")
        {
            txtPPName.Text = "";
            txtcode.Text = "";
            txtPAddress.Text = "";
            txtPCity.Text = "";
            txtPState.Text = "";
            txtPPincode.Text = "";
            txtPhoneNo.Text = "";
            txtEmail.Text = "";
            pnlProfile.Visible = false;
            panelSubmitAmt.Visible = true;
        }
        ddlName.Focus();
    }
    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {

    }
}
