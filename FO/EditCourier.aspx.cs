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

public partial class FO_EditCourier : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                    clstate.xmlstate(ddlState, "XMLState.xml");
                    maikal dev = new maikal();
                    maikal mk = new maikal();
                    int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win"; lblFromName.Text = "Diary No.:";
                    txtYearSeason.Text = DateTime.Now.Year.ToString(); tbllabel.Visible = true;
                    formstatus = "Yes";
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    panelhide.Visible = true; panelshow.Visible = false;
                    ddlExamSeason.Focus();
                    panelCourier.Visible = false; panelStatus.Visible = false;
                    ddlExamSeason.Focus();
                }
                btnBlock.Visible = false;
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
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
       try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtSName.Text = ""; txtYearSeason.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtSName.Text = ""; txtYearSeason.Focus();
    }
    public string formstatus;
    protected void txtSName_TExtChnaged(object sender, EventArgs e)
    {
        try
        {
            btnBlock.Visible = false; otherpnl.Visible = false;
            con.Close(); con.Open();
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select * from DiaryEntry where DiaryNo='" + txtSName.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtDate.Text = Convert.ToDateTime(reader["Date"]).ToString("dd/MM/yyyy");
                txtSName1.Text = reader["IMID"].ToString();
                txtDiraryType.Text = reader["DiaryType"].ToString();
                txtMEmUP.Text = reader["IMID"].ToString();
                txtRemoark.Text = reader["Remark"].ToString();
                lblName.Text = reader["Name"].ToString();
                lblRefNO.Text = reader["CourierNo"].ToString();
                lblStatus.Text = reader["Status"].ToString();
                txtConsignment.Text = reader["ConsignmentNo"].ToString();
                string myString = reader["Weight"].ToString();
                char[] separator = new char[] { '.' };
                string[] str = myString.Split(separator);
                txtwtkg.Text = str[0];
                if (str.Length == 1) txtwtgm.Text = "0";
                else txtwtgm.Text = str[1];
                if (btnBlock.Text != "Blocked")
                {
                    btnBlock.Text = "Block";
                }
                else
                    btnBlock.Visible = false;
                ddlRecivefrom.SelectedValue = reader["MemberType"].ToString();
                //ddlSupplyTo.SelectedValue = reader["SubmittedTo"].ToString();
                ddlCourierService.SelectedIndex = ddlCourierService.Items.IndexOf(ddlCourierService.Items.FindByValue(reader["CourierService"].ToString()));
                lblExceptiontbl.Text = ""; lblFromName1.Text = ddlRecivefrom.SelectedItem.Text.ToString();
                panelhide.Visible = false; panelshow.Visible = true;
                ddlRecivefrom.Focus();
            }
            else
            {
                lblExceptiontbl.Text = "Invalid Diary No.";
                lblExceptiontbl.ForeColor = System.Drawing.Color.Red;
                lblName.Text = ""; txtDiraryType.Text = ""; txtRemoark.Text = "";
                txtDate.Text = ""; lblRefNO.Text = "";
                panelhide.Visible = true; panelshow.Visible = false;
                txtSName.Focus();
            }
            if (lblStatus.Text == "NotOpen")
            {
                panelStatus.Visible = false;
            }
            else panelStatus.Visible = false;
            }
            catch (SqlException ex)
            {
                lblExceptiontbl.Text = ex.ToString();
            }
            finally
            {
                ddlBindRec();
                con.Close();
                con.Dispose();
            }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void btnSAveDiray_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            dtfi.DateSeparator = "/";
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("update DiaryEntry set ExamSession=@ExamSession,IMID=@IMID,DiaryType=@DiaryType,Remark=@Remark,Date=@Date,Name=@Name,MembershipNo=@MembershipNo,MemberType=@MemberType,CourierService=@CourierService,CourierNo=@CourierNo,ConsignmentNo=@ConsignmentNo,Weight=@Weight where DiaryNo='" + txtSName.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@ExamSession", lblHiddenSeason.Text.ToString());
            if (ddlRecivefrom.SelectedValue == "Student")
                cmd.Parameters.AddWithValue("@IMID", txtMEmUP.Text.ToString());
            else
                cmd.Parameters.AddWithValue("@IMID", txtSName1.Text.ToString());
            cmd.Parameters.AddWithValue("@DiaryType", txtDiraryType.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemoark.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text, dtfi));
            cmd.Parameters.AddWithValue("@Name", lblName.Text.ToString());
            cmd.Parameters.AddWithValue("@MembershipNo", txtSName1.Text);
            cmd.Parameters.AddWithValue("@MemberType", ddlRecivefrom.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CourierService", ddlCourierService.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CourierNo", lblRefNO.Text.ToString());
            cmd.Parameters.AddWithValue("@ConsignmentNo", txtConsignment.Text.ToString());
            cmd.Parameters.AddWithValue("@Weight", txtwtkg.Text + "." + txtwtgm.Text);
           
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update DairyCount set IMID='" + txtSName1.Text.ToString() + "',DairyType='" + txtDiraryType.Text.ToString() + "' where DairyNo='" + txtSName.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update ProjectCount set IMID='" + txtSName1.Text.ToString() + "' where DairyNo='" + txtSName.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            lblExcepitonDiary.Text = "Diary Updated Successfully.";
            lblExcepitonDiary.ForeColor = System.Drawing.Color.Green;
            txtSName.Text = "";
            txtSName.Text = "";
            txtConsignment.Text = "";
            txtDiraryType.Text = "";
            txtRemoark.Text = "";
            panelhide.Visible = true;
            panelshow.Visible = false;
        }
        catch (SqlException ex)
        {
            lblExcepitonDiary.Text = ex.ToString();
            lblExcepitonDiary.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            con.Close();
            con.Dispose();
            txtDiraryType.Focus();
        }
    }
    protected void ibtnNewCourier_Onclick(object sender, EventArgs e)
    {
        panelCourier.Visible = true;
        txtNewCourier.Focus();
    }
    protected void btnSAveNew_Onclick(object sender, EventArgs e)
    {
        if (txtNewCourier.Text == "")
        {
            lblExceptionNewCourier.Text = "Please Insert Courier Service Name.";
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ServiceNameMaster(Name,City,Type) values(@Name,@City,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtNewCourier.Text.ToString());
            cmd.Parameters.AddWithValue("@City", txtNewCity.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", "Courier");
            cmd.ExecuteNonQuery();
            lblExceptionNewCourier.Text = "Successfull Saved New Courier Service Name";
            con.Close();
        }
        panelCourier.Visible = false;
    }
    protected void btnCencelnew_Onclick(object sender, EventArgs e)
    {
        txtSName.Text = "";
        txtConsignment.Text = "";
        txtDiraryType.Text = "";
        txtRemoark.Text = ""; 
        panelCourier.Visible = false;
    }
    protected void btnBlock_Onclick(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("update DiaryEntry set Status=@Status where DiaryNo='" + txtSName.Text.ToString() + "'", con);
        cmd.Parameters.AddWithValue("@Status", "Blocked");
        con.Close(); con.Open();
        lblExcepitonDiary.Text = "Dairy No.: " + txtSName.Text.ToString() + " Blocked.";
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    protected void ddlRecive_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBindRec();
    }
    private void ddlBindRec()
    {
        if (ddlRecivefrom.SelectedValue == "IM")
        {
            lblFromName1.Text = "IM ID";
            txtStName.Visible = false; txtSName1.Visible = true; otherpnl.Visible = false;
            pnlIMIDUp.Visible = false;
            txtSName1.Focus();
        }
        else if (ddlRecivefrom.SelectedValue == "Student")
        {
            con.Close(); con.Open();
            SqlCommand cmd2 = new SqlCommand("select MembershipNo from DiaryEntry where DiaryNo='" + txtSName.Text.ToString() + "'", con);
            string strMU = Convert.ToString(cmd2.ExecuteScalar());
            txtSName1.Text = strMU;
            lblFromName1.Text = "Membership";
            txtStName.Visible = false; txtSName1.Visible = true; otherpnl.Visible = false;
            pnlIMIDUp.Visible = true;
            txtSName.Focus();
            con.Close(); con.Dispose();
        }
        else if (ddlRecivefrom.SelectedValue == "Other")
        {
            lblFromName1.Text = "Name From"; txtStName.Visible = true; txtSName1.Visible = false; otherpnl.Visible = true; pnlIMIDUp.Visible = false;
            txtStName.Focus();
        }
    }
    protected void ddlState_SelectedIndexChanged1(object sender, EventArgs e)
    {
        txtCity.Focus();
    }
    protected void txtSName1_TextChanged(object sender, EventArgs e)
    {
        maikal mk = new maikal();
        if (ddlRecivefrom.SelectedValue == "IM")
        {
            string strimsts = mk.SearchProfile(txtSName1.Text.ToString());
            if (strimsts == "No")
            {
                btnSaveDiary.Enabled = false;
                lblExceptiontbl.Text = "Invalid IM Membership No."; txtSName1.Focus(); formstatus = "No";
                lblName.Text = ""; txtSName1.Text = "";
            }
            else
            {
                btnSaveDiary.Enabled = true;
                lblExceptiontbl.Text = ""; formstatus = "Yes";
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("select * from IM where ID='" + txtSName1.Text.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblName.Text = reader["Name"].ToString();
                    txtCity.Text = "City: " + reader["PCity"].ToString();
                    txtAddress1.Text = "Status: " + reader["Active"].ToString();
                }
                reader.Close();
                con.Close();
                con.Dispose();
                txtDate.Focus();
            }
        }
        else if (ddlRecivefrom.SelectedValue == "Student")
        {
            string[] stdsts = new string[2];
            Student std = new Student();
            stdsts = std.status(txtSName1.Text.ToString());
            if (stdsts[1].ToString() == "No")
            {
                lblExceptiontbl.Text = "Invalid Student Membership No"; txtSName1.Text = "";
                formstatus = "No"; txtSName.Focus();
                btnSaveDiary.Enabled = false;
            }
            else
            {
                btnSaveDiary.Enabled = true;
                bool bl = mk.chkstatusStu(txtSName1.Text.ToString());
                if (bl == false) { lblExceptiontbl.Text = "Student Membership Not Activated"; formstatus = "No"; txtSName1.Focus(); }
                else
                {
                    lblExceptiontbl.Text = ""; formstatus = "Yes";
                    con.Close(); con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Student where SID='" + txtSName1.Text.ToString() + "'", con);
                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lblName.Text = "Student Name: " + reader["Name"].ToString();
                        txtCity.Text = "City: " + reader["PCity"].ToString();
                        txtAddress1.Text = "Status: " + reader["Status"].ToString();
                    }
                    reader.Close();
                    con.Close();
                    con.Dispose();
                    txtDate.Focus();
                }
            }
        }
        else if (ddlRecivefrom.SelectedValue == "Other")
        {
            ddlRecivefrom.Focus();
        }
    }
}
