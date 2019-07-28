using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;

public partial class Administrator_Register : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          
            rbtnHFellow.Focus();
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            //panelNewMem.Visible = false;
            lblstep1.Visible = false; lblStep2.Visible = false;
            btnViewProfile.Visible = false; Stripview.Visible = false;
            btnRegisterIM.Visible = false;
            if (!IsPostBack)
            {
                ddlExamSeason.Focus();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                rbtnHFellow.Checked = true;
                panelNewMem.Visible = true;
                lblstep1.Visible = true; lblStep2.Visible = true;
                lblName.Text = "Honorary Fellow:";
                lblDesignation.Text = "Designation";
                lblRegID.Text = genid();
                btnRegisterIM.Visible = false; btnRegister.Visible = true;
                txtNaem.Visible = true; txtEmail.Visible = true; txtDesignation.Visible = true;
              
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
   
    string ddno;
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
     
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
        
    }
    protected void rbtnHFellow_CheckedChanged(object sender, EventArgs e)
    {
        panelNewMem.Visible = true;
        lblstep1.Visible = true; lblStep2.Visible = true;
        lblName.Text = "Honorary Fellow:";
        lblDesignation.Text = "Designation";
        lblRegID.Text = genid();
        btnRegisterIM.Visible = false; btnRegister.Visible = true;
        txtNaem.Visible = true; txtEmail.Visible = true; txtDesignation.Visible = true;
    }
    protected void rbtnFellow_CheckedChanged(object sender, EventArgs e)
    {
        panelNewMem.Visible = true;
        lblstep1.Visible = true; lblStep2.Visible = true;
        lblName.Text = "Fellow Member Name:";
        lblDesignation.Text = "Designation";
        lblRegID.Text = genid();
        btnRegisterIM.Visible = false; btnRegister.Visible = true;
        txtNaem.Visible = true; txtEmail.Visible = true; txtDesignation.Visible = true;
    }
    protected void rbtMember_CheckedChanged(object sender, EventArgs e)
    {
        panelNewMem.Visible = true;
        lblstep1.Visible = true; lblStep2.Visible = true;
        lblName.Text = "Member Name:"; lblDesignation.Text = "Designation";
        lblRegID.Text = genid();
        btnRegisterIM.Visible = false; btnRegister.Visible = true;
        txtNaem.Visible = true; txtEmail.Visible = true; txtDesignation.Visible = true;
    }
    protected void rbtnIM_CheckedChanged(object sender, EventArgs e)
    {
        panelNewMem.Visible = true;
        lblstep1.Visible = true; lblStep2.Visible = true;
        btnRegisterIM.Visible = true;
        btnRegister.Visible = false;
        lblDesignation.Text = "";
        lblEmail.Text = "";
        lblName.Text = "";
        txtNaem.Visible = false; txtEmail.Visible = false; txtDesignation.Visible = false;
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            ddno = txtdiary.Text.ToString();
            con.Close();
            con.Open();
            string imid= genid();
            SqlCommand cmd = new SqlCommand("Insert into Member(Name,LName,Designation,Email,ID,HQualification,Experience,Type,Active,RegDate,RenewDate,ExpDate,YearFrom,YearTo,ExpStatus) Values(@Name,@LName,@Designation,@Email,@ID,@HQualification,@Experience,@Type,@Active,@RegDate,@RenewDate,@ExpDate,@YearFrom,@YearTo,@ExpStatus)", con);
            //SqlCommand cmd = new SqlCommand("Insert into Member(Name,LName,Designation,Email,ID,HQualification,Experience,Type,Active,RegDate,YearFrom,YearTo,ExpStatus) Values(@Name,@LName,@Designation,@Email,@ID,@HQualification,@Experience,@Type,@Active,@RegDate,@YearFrom,@YearTo,@ExpStatus)", con);
            cmd.Parameters.AddWithValue("@Name", txtNaem.Text.ToString());
            cmd.Parameters.AddWithValue("@LName", "");
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@ID",imid.ToString());
            cmd.Parameters.AddWithValue("@HQualification", "Education");
            cmd.Parameters.AddWithValue("@Experience", "Experience");
            if (rbtnHFellow.Checked == true) cmd.Parameters.AddWithValue("@Type", "Honorary");
            else if (rbtnFellow.Checked == true) cmd.Parameters.AddWithValue("@Type", "Fellow");
            else if (rbtMember.Checked == true) cmd.Parameters.AddWithValue("@Type", "Member");
            else cmd.Parameters.AddWithValue("@Type", "IM");
            cmd.Parameters.AddWithValue("@Active", "Register");
            string dateEnroll = System.DateTime.Now.ToShortDateString();
            cmd.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(dateEnroll.ToString()));
            cmd.Parameters.AddWithValue("@RenewDate", Convert.ToDateTime(dateEnroll.ToString()).AddYears(1));
            cmd.Parameters.AddWithValue("@ExpDate", Convert.ToDateTime(dateEnroll.ToString()).AddYears(1).AddMonths(6));
            maikal dev = new maikal();
            int se = dev.chksession();
            string ssn = ""; int yr = DateTime.Now.Year;
            if (se == 0) ssn = "Sum";
            else ssn = "Win";
            cmd.Parameters.AddWithValue("@YearFrom", ssn + "" + yr.ToString());
            cmd.Parameters.AddWithValue("@YearTo", ssn + "" + (yr + 1).ToString());
            cmd.Parameters.AddWithValue("@ExpStatus", 0);
            lblstep1.ForeColor = System.Drawing.Color.Green;
            lblStep2.ForeColor = System.Drawing.Color.Black;
            btnRegister.Enabled = false;
            panelNewMem.Visible = true;
            btnViewProfile.Visible = true; Stripview.Visible = true;
            btnViewProfile.Text = "View Profile";
            lblstep1.Visible = true; lblStep2.Visible = true;
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("update DiaryEntry set Status='Blocked',IMID='"+imid.ToString()+"',MembershipNo='"+imid.ToString()+"'  where DiaryNo='" + ddno + "'", con);
            cmd1.ExecuteNonQuery();
            SqlCommand cm = new SqlCommand("update DairyCount set IMID='" + imid.ToString() + "' where DairyNo='" + ddno + "'", con);
            cm.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
            btnViewProfile.Visible = false;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    public string genid()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(SN) from Member", con);
        con.Close();
        con.Open();
        string id;
        int i = Convert.ToInt32(cmdsn.ExecuteScalar().ToString());
        i = i + 1;
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
            id = Convert.ToString(i);
        }
        if (rbtnHFellow.Checked == true)
            id = "ICE" + id.ToString();
        else if (rbtMember.Checked == true)
            id = "ICE" + id.ToString();
        else if (rbtnFellow.Checked == true)
            id = "ICE" + id.ToString();
        else if (rbtnIM.Checked == true)
            id = "ICE" + id.ToString();
        else id = id.ToString();
        return id;
    }
    
    protected void btnViewProfile_Click(object sender, EventArgs e)
    {
            Response.Redirect("../Administrator/FellowMemberRegistration.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ms&" + Request.QueryString["lvl"] + "=zero&id=" + lblRegID.Text.ToString());
    }
    protected void btnRegNewIM_Click(object sender, EventArgs e)
    {
            ddno = txtdiary.Text.ToString();
            Response.Redirect("IMRegi.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + ddno);
    } 
    protected void btndiary_Click(object sender, EventArgs e)
    {
        ddno = txtdiary.Text.ToString();
        con.Close(); con.Open();
        cmd = new SqlCommand("select Status from DiaryEntry where DiaryNo='" + ddno + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "' and SubmittedTo='Director' and Status='CountDispatch'", con);
        string strdno = Convert.ToString(cmd.ExecuteScalar());
        if (strdno == "")
        {
            lblExceptionDiary.Text = "Diary Not Found."; pnldetails.Visible = false;
        }
        else
        {
            pnldetails.Visible = true;
        }
        con.Close(); con.Dispose();
    }
}