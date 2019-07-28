using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;

public partial class Create : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
            if (IsPostBack == false)
            {
                if (Request.QueryString["lnk"] == "delete")
                {
                    paneldelete.Visible = false;
                    panelCreate.Visible = false;
                    lblHeadertitle.Text = "Delete Account.";
                    panelUpdatePassword.Visible = false;
                    btndelete.Focus();
                }
                else if (Request.QueryString["lnk"] == "update")
                {
                    panelCreate.Visible = false;
                    panelUpdatePassword.Visible = true;
                    btnCreate.Visible = false;
                    btnUpdate.Visible = true; paneldelete.Visible = false;
                    panelAdmin.Visible = false;
                    lblHeadertitle.Text = "Recover Admin Password.";
                    txtPasswordUp.Focus();
                }
                else if (Request.QueryString["lnk"] == "create")
                {
                    panelUpdatePassword.Visible = false;
                    paneldelete.Visible = false;
                    btnUpdate.Visible = false;
                    btnCreate.Visible = true;
                    lblHeadertitle.Text = "Create New Admin";
                    txtUserName.Focus();
                }
                EnableUpdate();
            }
            panelVisible();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    public void panelVisible()
    {
        panelAdmission.Visible = false; panelFrontOffice.Visible = false; panelFees.Visible = false;
        panelExamInsert.Visible = false; panelInventory.Visible = false; panelMembership.Visible = false; panelProject.Visible = false;
       
        if (Request.QueryString["typ"] == "FO")
        {
            panelFrontOffice.Visible = true;
        }
        else if (Request.QueryString["typ"] == "Ad")
        {
            panelAdmission.Visible = true;
        }
        else if (Request.QueryString["typ"] == "Ac")
        {
            panelFees.Visible = true;
        }
        else if (Request.QueryString["typ"] == "Ex")
        {
            panelExamInsert.Visible = true;
        }
        else if (Request.QueryString["typ"] == "In")
        {
            panelInventory.Visible = true;
        }
        else if(Request.QueryString["typ"]=="Ms")
        {
            panelMembership.Visible = true;
        }
        else if (Request.QueryString["typ"] == "Pro")
        {
            panelProject.Visible = true;
        }
    }
    private void EnableUpdate()
    {
        lblselectNote.Text = "Insert New Password and Information";
        lblselecttext.Text = "Select User to Update the Details";
        lblDeletetitle.Text = "Select User to Delete Account.";
    }
    protected void btnClearCreate_Click(object sender, EventArgs e)
    {
        lblActiveId.Text = "";
        lblException.Text = "";
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";
        txtUserName.Text = "";
        txtDesignation.Text = "";
        txtName.Text = "";
        txtEmail.Text = "";
        chkFOffice.Checked = false;
    }
  
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkMainACFees.Checked == true || chkLateFees.Checked == true || chkAddApp.Checked == true || chkAppApprove.Checked == true || chkRegisterMember.Checked == true || chkRenewalReg.Checked == true || chkFOffice.Checked == true || chkEnquiry.Checked == true || chkCourier.Checked == true || chkD2D.Checked == true || chkAdmission.Checked == true || chkAdmissionApprove.Checked == true || chkMembershipAC.Checked == true || chkExamBill.Checked == true || chkExamCenter.Checked == true || chkExamSchedule.Checked == true || chkMarkFeed.Checked == true || chkExamForm.Checked == true || chkAdminCard.Checked == true || chkExamPaper.Checked == true || chkPaperSetter.Checked == true || chkRollNO.Checked == true || chkCertificate.Checked == true || chkMarking.Checked == true || chkMarksheet.Checked == true || chkSeating.Checked == true || chkUFM.Checked == true || chkRechecking.Checked == true || CheckBox4.Checked == true || CheckBox5.Checked == true || chkStockInout.Checked == true || chkSuplier.Checked == true || chkPurchesOrder.Checked==true||chkSynopsis.Checked==true||chkProApprove.Checked==true||chkCopySubmit.Checked==true||chkProEvaluation.Checked==true)
            {
            con.Close();
            con.Open(); dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            SqlCommand cmd = new SqlCommand("insert into Login(LogName, Password,LDate,Email,Admin,Exam1,Exam2,Admission,Accounts,Inven,FOffice,Enquiry,Courier,MFee,ExamFee,ExamCenter,AdmitCard,Certi,MarksFeed,Lavel,Name,Designation,EmpId,Type,InvenAdmin1,InvenAdmin2,InvenAdmin3,MemberAdmin1,MemberAdmin2,MemberAdmin3,ExamAdmin1,ExamAdmin2,ExamAdmin3,ExamAdmin4,ExamAdmin5,ACMember,ACExam,ExamForm,RollNO,ExamPaper,PaperSetter,Seating,Marksheet,Marking,UFM,AdmissionApprove,D2D,ProSynopsis,ProApprove,ProSubmit,ProEvaluate) values(@LogName,@Password,@Date,@Email,@Admin,@Exam1,@Exam2,@Admission,@Accounts,@Inven,@FOffice,@Enquiry,@Courier,@MFee,@Examfee,@ExamCenter,@AdminCard,@Certi,@MarksFeed,@Level,@Name,@Designation,@EmpId,@Type,@InvenAdmin1,@InvenAdmin2,@InvenAdmin3,@MemberAdmin1,@MemberAdmin2,@MemberAdmin3,@ExamAdmin1,@ExamAdmin2,@ExamAdmin3,@ExamAdmin4,@ExamAdmin5,@ACMember,@ACExam,@ExamForm,@RollNO,@ExamPaper,@PaperSetter,@Seating,@Marksheet,@Marking,@UFM,@AdmissionApprove,@D2D,@ProSynopsis,@ProApprove,@ProSubmit,@ProEvaluate)", con);
            cmd.Parameters.AddWithValue("@LogName", txtUserName.Text.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());
            string date = Convert.ToString(DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@Admin", "User");
            cmd.Parameters.AddWithValue("@Exam1", "");
            cmd.Parameters.AddWithValue("@Exam2", "");
            if (chkAdmission.Checked == true)cmd.Parameters.AddWithValue("@Admission", "Ad");else if (chkAdmission.Checked == false) cmd.Parameters.AddWithValue("@Admission", "");
            cmd.Parameters.AddWithValue("@Inven", "");

            if (chkFOffice.Checked == true) cmd.Parameters.AddWithValue("@FOffice", "FO"); else cmd.Parameters.AddWithValue("@FOffice", "");
            if (chkEnquiry.Checked == true) cmd.Parameters.AddWithValue("@Enquiry", "Enq"); else cmd.Parameters.AddWithValue("@Enquiry", "");
            if (chkCourier.Checked == true) cmd.Parameters.AddWithValue("@Courier", "Cou"); else cmd.Parameters.AddWithValue("@Courier", "");
            if (chkMainACFees.Checked == true) cmd.Parameters.AddWithValue("@Examfee", "MainAC"); else cmd.Parameters.AddWithValue("@Examfee", "");
            if (chkLateFees.Checked == true) cmd.Parameters.AddWithValue("@MFee", "LateFees"); else cmd.Parameters.AddWithValue("@MFee", "");            
            if (chkAppApprove.Checked == true) cmd.Parameters.AddWithValue("@Accounts", "AppApprove"); else cmd.Parameters.AddWithValue("@Accounts", "");
            if (chkAddApp.Checked == true) cmd.Parameters.AddWithValue("@ACExam", "AddApps"); else cmd.Parameters.AddWithValue("@ACExam", "");
            if (chkExamCenter.Checked == true) cmd.Parameters.AddWithValue("@ExamCenter", "ECenter"); else cmd.Parameters.AddWithValue("@ExamCenter", "");
            if (chkAdminCard.Checked == true) cmd.Parameters.AddWithValue("@AdminCard", "AdmitCard"); else cmd.Parameters.AddWithValue("@AdminCard", "");
            if (chkCertificate.Checked == true) cmd.Parameters.AddWithValue("@Certi", "Certi"); else cmd.Parameters.AddWithValue("@Certi", "");
            if (chkMarkFeed.Checked == true) cmd.Parameters.AddWithValue("@MarksFeed", "MarksFeed"); else cmd.Parameters.AddWithValue("@MarksFeed", "");
            cmd.Parameters.AddWithValue("@Level", "2");
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.ToString());
            cmd.Parameters.AddWithValue("@EmpId", "");
            cmd.Parameters.AddWithValue("@Type", Request.QueryString["typ"].ToString());
            if (chkStockInout.Checked == true) cmd.Parameters.AddWithValue("@InvenAdmin1", "Stock"); else cmd.Parameters.AddWithValue("@InvenAdmin1", "");
            if (chkSuplier.Checked == true) cmd.Parameters.AddWithValue("@InvenAdmin2", "Suplier"); else cmd.Parameters.AddWithValue("@InvenAdmin2", "");
            if (chkPurchesOrder.Checked == true) cmd.Parameters.AddWithValue("@InvenAdmin3", "Purches"); else cmd.Parameters.AddWithValue("@InvenAdmin3", "");
            if (chkRegisterMember.Checked == true) cmd.Parameters.AddWithValue("@MemberAdmin1", "MRegister"); else cmd.Parameters.AddWithValue("@MemberAdmin1", "");
            if (chkRenewalReg.Checked == true) cmd.Parameters.AddWithValue("@MemberAdmin2", "MRenewal"); else cmd.Parameters.AddWithValue("@MemberAdmin2", "");
            cmd.Parameters.AddWithValue("@MemberAdmin3", "Member3");
            if (chkExamSchedule.Checked == true) cmd.Parameters.AddWithValue("@ExamAdmin1", "ExamSchedule"); else cmd.Parameters.AddWithValue("@ExamAdmin1", "");
            if (chkExamBill.Checked == true) cmd.Parameters.AddWithValue("@ExamAdmin2", "ExamBill"); else cmd.Parameters.AddWithValue("@ExamAdmin2", "");
            if (chkRechecking.Checked == true) cmd.Parameters.AddWithValue("@ExamAdmin3", "Rechecking"); else cmd.Parameters.AddWithValue("@ExamAdmin3", "");
            cmd.Parameters.AddWithValue("@ExamAdmin4", "");
            cmd.Parameters.AddWithValue("@ExamAdmin5", "");
            if (chkMembershipAC.Checked == true) cmd.Parameters.AddWithValue("@ACMember", "ACMember"); else cmd.Parameters.AddWithValue("@ACMember", "");
            if (chkExamForm.Checked == true) cmd.Parameters.AddWithValue("@ExamForm", "ExamForm"); else cmd.Parameters.AddWithValue("@ExamForm", "");
            if (chkRollNO.Checked == true) cmd.Parameters.AddWithValue("@RollNO", "RollNO"); else cmd.Parameters.AddWithValue("@RollNO", "");
            if (chkExamPaper.Checked == true) cmd.Parameters.AddWithValue("@ExamPaper", "ExamPaper"); else cmd.Parameters.AddWithValue("@ExamPaper", "");
            if (chkPaperSetter.Checked == true) cmd.Parameters.AddWithValue("@PaperSetter", "PaperSetter"); else cmd.Parameters.AddWithValue("@PaperSetter", "");
            if (chkSeating.Checked == true) cmd.Parameters.AddWithValue("@Seating", "Seating"); else cmd.Parameters.AddWithValue("@Seating", "");
            if (chkMarksheet.Checked == true) cmd.Parameters.AddWithValue("@Marksheet", "Marksheet"); else cmd.Parameters.AddWithValue("@Marksheet", "");
            if (chkMarking.Checked == true) cmd.Parameters.AddWithValue("@Marking", "Marking"); else cmd.Parameters.AddWithValue("@Marking", "");
            if (chkUFM.Checked == true) cmd.Parameters.AddWithValue("@UFM", "UFM"); else cmd.Parameters.AddWithValue("@UFM", "");
            if (chkAdmissionApprove.Checked == true) cmd.Parameters.AddWithValue("@AdmissionApprove", "AdmissionApprove"); else cmd.Parameters.AddWithValue("@AdmissionApprove", "");
            if (chkD2D.Checked == true) cmd.Parameters.AddWithValue("@D2D", "D2D"); else cmd.Parameters.AddWithValue("@D2D", "");
            if (chkSynopsis.Checked == true) cmd.Parameters.AddWithValue("@ProSynopsis", "Sys"); else cmd.Parameters.AddWithValue("@ProSynopsis", "");
            if (chkProApprove.Checked == true) cmd.Parameters.AddWithValue("@ProApprove", "ProApp"); else cmd.Parameters.AddWithValue("@ProApprove", "");
            if (chkCopySubmit.Checked == true) cmd.Parameters.AddWithValue("@ProSubmit", "ProSub"); else cmd.Parameters.AddWithValue("@ProSubmit", "");
            if (chkProEvaluation.Checked == true) cmd.Parameters.AddWithValue("@ProEvaluate", "ProEva"); else cmd.Parameters.AddWithValue("@ProEvaluate", "");
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            lblException.Text = "User Created Successfully.";
            GridView1.DataBind();
            lblException.Text = "New User Created";
            lblException.Visible = true;
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtUserName.Text = "";
            txtDesignation.Text = "";
            txtName.Text = "";
            txtEmail.Text = ""; lblActiveId.Text = "";
            chkMainACFees.Checked = false; chkLateFees.Checked = false; chkAddApp.Checked = false; chkAppApprove.Checked = false;
            chkRegisterMember.Checked = false; chkRenewalReg.Checked = false; chkFOffice.Checked = false; chkEnquiry.Checked = false; chkCourier.Checked = false; chkD2D.Checked = false; chkAdmission.Checked = false; chkAdmissionApprove.Checked = false; chkMembershipAC.Checked = false; chkExamBill.Checked = false; chkExamCenter.Checked = false; chkExamSchedule.Checked = false; chkMarkFeed.Checked = false; chkExamForm.Checked = false; chkAdminCard.Checked = false; chkExamPaper.Checked = false; chkPaperSetter.Checked = false; chkRollNO.Checked = false; chkCertificate.Checked = false; chkMarking.Checked = false; chkMarksheet.Checked = false; chkSeating.Checked = false; chkUFM.Checked = false; chkRechecking.Checked = false; CheckBox4.Checked = false; CheckBox5.Checked = false; chkStockInout.Checked = false; chkSuplier.Checked = false; chkPurchesOrder.Checked = false;
            chkSynopsis.Checked = false; chkProApprove.Checked = false; chkCopySubmit.Checked = false; chkProEvaluation.Checked = false;
           }
            else lblException.Text="Please Select Module..!";
        }
        catch (SqlException ex)
        {
            lblException.Text = "Null Value";
        }
        catch (NullReferenceException ex)
        {
            lblException.Text = "Null Value";
        }
        finally
        {
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Request.QueryString["lnk"] == "update")
        {
            panelAdmin.Visible = true;
        }
        if (Request.QueryString["lnk"] == "delete")
        {
            paneldelete.Visible = true;
        }
       
        lblAdminNameUp.Enabled = true; lblPasswordUp.Enabled = true; lblAdminDelete.Enabled = true; btndelete.Enabled = true;
        lblselectNote.Text = "Insert New Password and Information";
        lblDeletetitle.Text = " Deleted accounts are not recoverable.";
        lblselecttext.Text = "";
        txtPasswordUp.Enabled = true; txtConfirmPassUp.Enabled = true;
        txtName.Enabled = true; txtDesignation.Enabled = true;
        btnUpdate.Enabled = true; GridViewRow row = GridView1.SelectedRow;
        lblAdminNameUp.Text = row.Cells[2].Text.ToString(); lblAdminDelete.Text = lblAdminNameUp.Text.ToString();
        lblPasswordUp.Text = "*****";
        txtName.Text = row.Cells[6].Text.ToString(); lblName.Text = txtName.Text;
        txtEmail.Text = row.Cells[5].Text.ToString(); lblEmailDelete.Text = txtEmail.Text.ToString();
        txtDesignation.Text = row.Cells[7].Text.ToString(); lblDesignation.Text = txtDesignation.Text;
        lblselecttext.Text = "";
        if (Request.QueryString["lnk"] == "create")
            txtUserName.Focus();
        else if (Request.QueryString["lnk"] == "update")
            txtPasswordUp.Focus();
        else if (Request.QueryString["lnk"] == "delete")
            btndelete.Focus();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (panelAdmin.Visible == true)
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("update Login set Password=@Password where LogName='" + lblAdminNameUp.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@Password", txtPasswordUp.Text.ToString());
                cmd.ExecuteNonQuery();

                lblselecttext.Text = "Password Updated";
                lblException.ForeColor = System.Drawing.Color.Green;
                lblselecttext.ForeColor = System.Drawing.Color.Green;
                lblselecttext.Visible = true;
                GridView1.DataBind();

                con.Close();
                con.Dispose();
            }
            else lblselecttext.Text = "Please select User";

        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
           
         catch (NullReferenceException ex)
        {
            lblException.Text = ex.ToString();
           
        }
        finally
        {
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete Login where LogName='" + lblAdminDelete.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            GridView1.DataBind();
            con.Close();
            con.Dispose();
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
            Lblmesg.Text = "Account Deleted";
        }
        catch (NullReferenceException ex)
        {
            lblException.Text = ex.ToString();
        }

        finally
        {
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)  
    {
        Response.Redirect(Response.Cookies["redi"]["2"].ToString());
    }

    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        RegExp Reg = new RegExp();
        bool aa = Reg.IsAlpha(txtUserName.Text);
        con.Close();
        con.Open();
        if (aa)
        {

            SqlCommand cmd = new SqlCommand("select LogName from Login where LogName='" + txtUserName.Text.ToString() + "'", con);
            string chk = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            con.Dispose();

            if (chk == txtUserName.Text.ToString().ToUpper())
            {
                lblActiveId.Text = txtUserName.Text.ToString() + " is  Not available.";
                lblActiveId.ForeColor = System.Drawing.Color.Maroon;
                txtUserName.Focus();
                txtUserName.Text = "";
            }
            else
            {
                lblActiveId.Text = txtUserName.Text.ToString() + " is  available.";
                lblActiveId.ForeColor = System.Drawing.Color.Green;
                txtPassword.Focus();
            }
        }
        else
        {
            lblActiveId.Text = txtUserName.Text.ToString() + " is  Not available.";
            lblActiveId.ForeColor = System.Drawing.Color.Maroon;
            txtUserName.Focus();
            txtUserName.Text = "";
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (Request.QueryString["lnk"] == "create")
            {
                e.Row.Cells[0].Visible = false;
            }
            else e.Row.Cells[0].Visible = true;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString["lnk"] == "create")
            {
                e.Row.Cells[0].Visible = false;
            }
            else e.Row.Cells[0].Visible = true;
        }
    }
}
