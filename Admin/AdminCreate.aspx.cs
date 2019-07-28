using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Admin_AdminCreate : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                panelAdmin.Visible = false;
                panelAdminDelete.Visible = false;
               paneldelete.Visible = false;
              panelCreate.Visible = false;  panelupdate.Visible = false;
                if (Request.QueryString["lnk"] == "delete")
                {
                    paneldelete.Visible = true;
                }
                else if (Request.QueryString["lnk"] == "update")
                {
                    panelAdminDelete.Visible = false;
                    panelupdate.Visible = true;
                }
                else
                {
                    panelCreate.Visible = true;
                }
                SqlDataReader reader;
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(MyLogin.login[1]) + "' and Password='" + Convert.ToString(MyLogin.login[0]) + "'", con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   int lvl = Convert.ToInt32(reader[20].ToString());
                    if (lvl == 0)
                    {
                    }
                    else if (lvl == 1)
                    {
                        reader.Close();
                        con.Close();
                        con.Dispose();
                        Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
                    }
                    else if (lvl == 2)
                    {
                        reader.Close();
                        con.Close();
                        con.Dispose();
                        Response.Redirect("Admin/SuperAdminManage.aspx");
                    }
                }
                reader.Close();
                con.Close();
                con.Dispose();
                EnableUpdate();
                if (Request.QueryString["lnk"] == "delete")
                    GridView1.Focus();
                else if (Request.QueryString["lnk"] == "update")
                    txtPasswordUp.Focus();
                else
                    txtUserName.Focus();
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


   private void EnableUpdate()
    {
        lblAdminNameUp.Enabled = false; lblPasswordUp.Enabled = false; lblAdminDelete.Enabled = false; btndelete.Enabled = false;
        lblselectNote.Text = "Insert New Password and Information";
        lblselectNote.ForeColor = System.Drawing.Color.Brown;
        lblselecttext.Text = "Select Admin to update the detials";
        lblselecttext.ForeColor = System.Drawing.Color.Brown;
        lblDeletetitle.Text = "Select Admin to delete Account.";
        txtPasswordUp.Enabled = false; txtConfirmPassUp.Enabled = false;
        btnUpdate.Enabled = false; btnClearUp.Enabled = false;
    }
    protected void btnClearCreate_Click(object sender, EventArgs e)
    {

        lblException.Text = "";
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";
        txtUserName.Text = "";
        txtFacultyId.Text = "";
        txtDesignation.Text = "";
        txtName.Text = "";
        txtEmail.Text = "";
        lblActiveId.Text = "";
        chkFOffice.Checked = false;
        chkInventory.Checked = false; chkAdmission.Checked = false; chkExam.Checked = false; chkAcc.Checked = false;
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if(chkAcc.Checked==true||chkAdmission.Checked==true||chkExam.Checked==true||chkFOffice.Checked==true||chkInventory.Checked==true||chkmembership.Checked==true||chkProject.Checked==true||chkReports.Checked==true)
            {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Login(LogName, Password,LDate,Email,Admin,Exam1,Exam2,Admission,Accounts,Inven,FOffice,Enquiry,Courier,MFee,ExamFee,ExamCenter,AdmitCard,Certi,MarksFeed,Lavel,Name,Designation,EmpId,Project,Report) values(@LogName,@Password,@Date,@Email,@Admin,@Exam1,@Exam2,@Admission,@Accounts,@Inven,@FOffice,@Enquiry,@Courier,@MFee,@Examfee,@ExamCenter,@AdminCard,@Certi,@MarksFeed,@Level,@Name,@Designation,@EmpId,@Project,@Report)", con);
            cmd.Parameters.AddWithValue("@LogName", txtUserName.Text.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"),dtinfo));
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            if (chkFOffice.Checked == true) cmd.Parameters.AddWithValue("@Admin", "Admin");
            else cmd.Parameters.AddWithValue("@Admin", "");
            if (chkExam.Checked == true)
                cmd.Parameters.AddWithValue("@Exam1", "E");
            else
                cmd.Parameters.AddWithValue("@Exam1", "");
            if (chkmembership.Checked == true)
                cmd.Parameters.AddWithValue("@Exam2", "E2");       // Membership.
            else cmd.Parameters.AddWithValue("@Exam2", "");
            if (chkAdmission.Checked == true)
                cmd.Parameters.AddWithValue("@Admission", "Ad");
            else cmd.Parameters.AddWithValue("@Admission", "");
            if(chkAcc.Checked==true)
            cmd.Parameters.AddWithValue("@Accounts", "AC");
                else cmd.Parameters.AddWithValue("@Accounts","");
            if (chkInventory.Checked == true)
                cmd.Parameters.AddWithValue("@Inven", "IN");
            else cmd.Parameters.AddWithValue("@Inven", "");
            cmd.Parameters.AddWithValue("@FOffice", "");    
            cmd.Parameters.AddWithValue("@Enquiry", "");
            cmd.Parameters.AddWithValue("@Courier", "");
            cmd.Parameters.AddWithValue("@MFee", "");
            cmd.Parameters.AddWithValue("@Examfee", "");
            cmd.Parameters.AddWithValue("@ExamCenter", "");
            cmd.Parameters.AddWithValue("@AdminCard", "");
            cmd.Parameters.AddWithValue("@Certi", "");
            cmd.Parameters.AddWithValue("@MarksFeed","");
            cmd.Parameters.AddWithValue("@Level","1");
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Designation",txtDesignation.Text.ToString());
            cmd.Parameters.AddWithValue("@EmpId", txtFacultyId.Text.ToString());
            if (chkProject.Checked == true)
                cmd.Parameters.AddWithValue("@Project", "Pro");
            else cmd.Parameters.AddWithValue("@Project", "");
            if (chkReports.Checked == true)
                cmd.Parameters.AddWithValue("@Report", "rpt");
            else cmd.Parameters.AddWithValue("@Report", "");
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            GridView1.DataBind();
            GridView1.Visible = true;
            lblException.Text = "New Admin Created";
            lblException.Visible = true;
            lblException.ForeColor = System.Drawing.Color.Green;
            txtUserName.Text = ""; txtPassword.Text = ""; txtDesignation.Text = ""; txtEmail.Text = ""; txtName.Text = ""; lblActiveId.Text = "";
            chkAcc.Checked = false; chkAdmission.Checked = false; chkExam.Checked = false; chkFOffice.Checked = false; chkInventory.Checked = false; chkmembership.Checked = false; chkProject.Checked = false; chkReports.Checked = false;
        }
        else lblException.Text="Plese Select Module.";
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {

        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {


        lblmesg.Text = "";
        panelAdminDelete.Visible = false;
        panelupdate.Visible = false;
        panelAdmin.Visible = false;
        paneldelete.Visible = false;
        if (Request.QueryString["lnk"] == "update")
        {
            panelupdate.Visible = true;
            panelAdmin.Visible = true;
        }
        if (Request.QueryString["lnk"] == "delete")
        {
            panelAdminDelete.Visible = true;
            paneldelete.Visible = true;
        }
       
        lblAdminNameUp.Enabled = true; lblPasswordUp.Enabled = true; lblAdminDelete.Enabled = true; btndelete.Enabled = true;
        lblselectNote.Text = "Insert New Password and Information";
        lblDeletetitle.Text = " Deleted Account Will Not Recoverable.";
        lblselecttext.Text = "";
        txtPasswordUp.Enabled = true; txtConfirmPassUp.Enabled = true;
        btnUpdate.Enabled = true; btnClearUp.Enabled = true;        GridViewRow row = GridView1.SelectedRow;
        lblAdminNameUp.Text = row.Cells[2].Text.ToString(); lblAdminDelete.Text = lblAdminNameUp.Text.ToString();
        lblPasswordUp.Text = "*****";
        lblEmailDelete.Text = row.Cells[5].Text.ToString();
        if (Request.QueryString["lnk"] == "update") txtPasswordUp.Focus();
        else btndelete.Focus();
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (Request.QueryString["lnk"] == "create")
            {
                e.Row.Cells[0].Visible = false;
            }
            else e.Row.Cells[0].Visible = true;
        }
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            if (Request.QueryString["lnk"] == "create")
            {
                e.Row.Cells[0].Visible = false;
            }
            else e.Row.Cells[0].Visible = true;
        }
    }
    protected void btnClearUp_Click(object sender, EventArgs e)
    {
        txtPasswordUp.Text = "";
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd=new SqlCommand("update Login set Password=@Password where LogName='"+lblAdminNameUp.Text.ToString()+"'",con);
            cmd.Parameters.AddWithValue("@Password",txtPasswordUp.Text.ToString());
            cmd.ExecuteNonQuery();
            EnableUpdate();
            lblselecttext.Visible = false;
            lblmesg.Text = "Password is Updated"; lblmesg.ForeColor = System.Drawing.Color.Green;
            lblselecttext.Text = "Admin Information is updated";
            lblselecttext.ForeColor = System.Drawing.Color.Green;
            GridView1.DataBind();
            con.Close();
            con.Dispose();
            }
        catch(SqlException ex)
        {
            lblException.Text=ex.ToString();
        }
        finally{
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
            con.Close();
            con.Dispose();
            Response.Redirect("../Admin/AdminCreate.aspx?lnk=delete&lvl=zero");
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
        }
    }
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        lblException.Text = "";
        RegExp Reg = new RegExp();
        bool Name = Reg.IsAlpha(txtUserName.Text);
        if (Name)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select LogName from Login where LogName='" + txtUserName.Text.ToString() + "'", con);
            string chk = Convert.ToString(cmd.ExecuteScalar());
            if (chk == txtUserName.Text.ToString().ToUpper())
            {
                lblActiveId.Text = txtUserName.Text.ToString() + " is  Not available.";
                lblActiveId.ForeColor = System.Drawing.Color.Maroon;
                txtUserName.Text = "";
                txtUserName.Focus();
            }
            else
            {
                lblActiveId.Text = txtUserName.Text.ToString() + " is  available.";
                lblActiveId.ForeColor = System.Drawing.Color.Green;
                txtPassword.Focus();
            }
            con.Close();
            con.Dispose();
        }

        else
        {
            lblActiveId.Text = txtUserName.Text.ToString() + " is  Not available.";
            lblActiveId.ForeColor = System.Drawing.Color.Maroon;
            txtUserName.Focus();
            txtUserName.Text = "";
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
   
}
