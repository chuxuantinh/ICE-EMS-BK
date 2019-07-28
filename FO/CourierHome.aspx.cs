using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Xml;

public partial class FO_CourierHome : System.Web.UI.Page
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
                    grvCourierHomeFill();
                    lblGridTitle.Text = "Today Diary Entries";
                    btnshow_Onclick();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                    txtDate.Enabled = false;
                    maikal dev = new maikal();
                    maikal mk = new maikal();
                    int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win"; lblFromName.Text = "Membership No:";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    formstatus = "Yes";
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    ddlExamSeason.Focus();
                    panelCourier.Visible = false;
                    otherpnl.Visible = false; lblFromName.Text = ddlRecivefrom.SelectedItem.Text.ToString();
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
    private void getrefno()
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("select Max(CourierNo) from DiaryEntry", con);
            int refno = Convert.ToInt32(cmd.ExecuteScalar());
            txtCourierNo.Text = (refno + 1).ToString();
            con.Close();
        }
        catch (InvalidCastException e)
        {
            txtCourierNo.Text = "1";
        }
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
        txtSName.Text = ""; ddlRecivefrom.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtSName.Text = ""; txtYearSeason.Focus();
    }
    protected void ddlRecive_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSName.Text = "";
        if (ddlRecivefrom.SelectedValue == "IM")
        {
            lblFromName.Text = "IM ID";
            txtStName.Visible = false; txtSName.Visible = true; otherpnl.Visible = false;
            txtSName.Focus();
        }
        else if (ddlRecivefrom.SelectedValue == "Student")
        {
            lblFromName.Text = "Membership";
            txtStName.Visible = false; txtSName.Visible = true; otherpnl.Visible = false;
            txtSName.Focus();
        }
        else if (ddlRecivefrom.SelectedValue == "Other")
        {
            lblFromName.Text = "Name From"; txtStName.Visible = true; txtSName.Visible = false; otherpnl.Visible = true;
            txtStName.Focus();

        }
    }
    public string formstatus;
    protected void txtSName_TExtChnaged(object sender, EventArgs e)
    {
        maikal mk = new maikal();
        if (ddlRecivefrom.SelectedValue == "IM")
        {
            string strimsts = mk.SearchProfile(txtSName.Text.ToString());
            if (strimsts == "")
            {
                btnSaveDiary.Enabled = false;
                lblExceptiontbl.Text = "Invalid IM Membership No."; txtSName.Focus(); formstatus = "No";
                lblName.Text = ""; txtSName.Text = "";
                lblCode.Text = ""; lblCourseAddress.Text = "";
            }
            else
            {
                btnSaveDiary.Enabled = true;
                lblExceptiontbl.Text = ""; formstatus = "Yes";
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("select * from IM where ID='" + txtSName.Text.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblName.Text = reader["Name"].ToString();
                    lblCode.Text = "City: " + reader["PCity"].ToString();
                    lblCourseAddress.Text = "Status: " + reader["Active"].ToString();
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
            stdsts = std.status(txtSName.Text.ToString());
            if (stdsts[1].ToString() == "No")
            {
                lblExceptiontbl.Text = "Invalid Student Membership No"; txtSName.Text = "";
                formstatus = "No"; txtSName.Focus();
                btnSaveDiary.Enabled = false;
            }
            else
            {
                btnSaveDiary.Enabled = true;
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
                        lblName.Text = "Student Name: " + reader["Name"].ToString();
                        lblCode.Text = "City: " + reader["PCity"].ToString();
                        lblCourseAddress.Text = "Status: " + reader["Status"].ToString();
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
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void ibtnGenDiary_ONClick(object sender, EventArgs e)
    {
        lblDiaryNo.Text = getdno();
        lblDiaryNo.Focus();
    }
    string id, did;
    private string getdno()
    {
        con.Close(); con.Open();
        string yr = txtYearSeason.Text.Substring(2, 2);
        SqlCommand cmd = new SqlCommand("select Max(Diary) from DiaryEntry where ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
        did = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        if (did.ToString() == "" | did.ToString() == null)
        {
            if (ddlExamSeason.SelectedValue.ToString() == "Win")
            {
                id = "W" + yr.ToString() + "1";
                lblDiaryyHiddend.Text = "1";
            }
            else if (ddlExamSeason.SelectedValue.ToString() == "Sum")
            {
                id = "S" + yr.ToString() + "1";
                lblDiaryyHiddend.Text = "1";
            }
        }
        else if (did.ToString() != "")
        {
            int i = Convert.ToInt32(did);
            i = i + 1;
            lblDiaryyHiddend.Text = i.ToString();
            if (ddlExamSeason.SelectedValue.ToString() == "Win")
            {
                id = "W" + yr.ToString() + "" + lblDiaryyHiddend.Text.ToString();
            }
            else if (ddlExamSeason.SelectedValue.ToString() == "Sum")
            {
                id = "S" + yr.ToString() + "" + lblDiaryyHiddend.Text.ToString();
            }
        }
        lblDiaryNo.Text = id.ToString();
        return id;
    }
    private void btnshow_Onclick()
    {
        DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime now = DateTime.Now;
        DateTime tdate = Convert.ToDateTime("20/04/2012", dtinfo);
        int i = DateTime.Compare(now, tdate);
        if (i == 0 || i == -1)
        {
        }
        else
        {
        }
    }
    protected void btnCancel_Onclick(object sender, EventArgs e)
    {
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }
    protected void btnSAveDiray_Click(object sender, EventArgs e)
    {
        try
        {
            lblgh.Text = "";
            if (chkdupli())
            {
                con.Close(); con.Open();
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd/MM/yyyy";
                dtfi.DateSeparator = "/";
                SqlCommand cmd = new SqlCommand("insert into DiaryEntry (ExamSession,DiaryNo,IMID,DiaryType,Remark,Diary,Amount,Date,Name,MembershipNo,MemberType,Address1,Address2,City,Phone,CourierService,CourierNo,Status,DispatchDate,ConsignmentNo,SubmittedTo,Weight) values(@ExamSession,@DiaryNo,@IMID,@DiaryType,@Remark,@Diary,@Amount,@Date,@Name,@MembershipNo,@MemberType,@Address1,@Address2,@City,@Phone,@CourierService,@CourierNo,@Status,@DispatchDate,@ConsignmentNo,@SubmittedTo,@Weight)", con);
                cmd.Parameters.AddWithValue("@ExamSession", lblHiddenSeason.Text.ToString());
                cmd.Parameters.AddWithValue("@DiaryNo", lblDiaryNo.Text.ToString());
                if (ddlRecivefrom.SelectedValue == "Student")
                {
                    SqlCommand cmd1 = new SqlCommand("select IMID from Student where SID='" + txtSName.Text.ToString() + "'", con);
                    string strMem = Convert.ToString(cmd1.ExecuteScalar());
                    cmd1.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@IMID", strMem);
                    cmd.Parameters.AddWithValue("@Name", lblName.Text.ToString());
                    cmd.Parameters.AddWithValue("@MembershipNo", txtSName.Text.ToString());
                }
                else if (ddlRecivefrom.SelectedValue == "IM")
                {
                    cmd.Parameters.AddWithValue("@IMID", txtSName.Text.ToString());
                    cmd.Parameters.AddWithValue("@Name", lblName.Text.ToString());
                    cmd.Parameters.AddWithValue("@MembershipNo", txtSName.Text.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IMID", "N/A");
                    cmd.Parameters.AddWithValue("@Name", txtStName.Text.ToString());
                    cmd.Parameters.AddWithValue("@MembershipNo", "Other");
                }
                cmd.Parameters.AddWithValue("@DiaryType", "");
                cmd.Parameters.AddWithValue("@Remark", txtRemoark.Text.ToString());
                cmd.Parameters.AddWithValue("@Amount", "0");
                cmd.Parameters.AddWithValue("@Diary", Convert.ToInt32(lblDiaryyHiddend.Text.ToString()));
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@MemberType", ddlRecivefrom.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text.ToString());
                cmd.Parameters.AddWithValue("@Address2", ddlState.Text.ToString());
                cmd.Parameters.AddWithValue("@City", txtCity.Text.ToString());
                cmd.Parameters.AddWithValue("@Phone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
                cmd.Parameters.AddWithValue("@CourierService", ddlCourierService.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CourierNo", Convert.ToInt32(txtCourierNo.Text.ToString()));
                cmd.Parameters.AddWithValue("@Status", "DiaryEntry");
                cmd.Parameters.AddWithValue("@SubmittedTo", "N/A");
                if (txtDDate.Text == "") txtDDate.Text = txtDate.Text.ToString();
                cmd.Parameters.AddWithValue("@DispatchDate", Convert.ToDateTime(txtDDate.Text, dtfi));
                cmd.Parameters.AddWithValue("@ConsignmentNo",txtConsignment.Text);
                if (txtwtgm.Text == "") txtwtgm.Text = "0"; if (txtwtkg.Text == "") txtwtkg.Text = "0";
                cmd.Parameters.AddWithValue("@Weight", txtwtkg.Text + "." + txtwtgm.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Log.WriteLog(Request.QueryString["maikal"], "B001", lblDiaryNo.Text.ToString(), txtSName.Text.ToString(), "Diary Entry");
                Log.WriteLog("B001", Request.QueryString["maikal"], lblDiaryNo.Text.ToString(), txtSName.Text.ToString(), "Diary Entry");
                lblExcepitonDiary.Text = "submitted at No.: " + lblDiaryNo.Text.ToString();
                lblExcepitonDiary.ForeColor = System.Drawing.Color.Green;
                txtSName.Text = "";
                txtRemoark.Text = ""; txtCourierNo.Text = ""; txtDDate.Text = ""; txtCourierNo.Text = ""; lblDiaryNo.Text = ""; lblName.Visible = false;
                lblCode.Visible = false; lblCourseAddress.Visible = false;
                txtAddress1.Text = "";  txtPincode.Text = "";
                txtPhonecode.Text = ""; txtPhoneNo.Text = ""; txtRemoark.Text = ""; txtStName.Text = "";
                grvCourierHomeFill();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary successfully " + lblExcepitonDiary.Text + "')", true);
                lblExcepitonDiary.Text = "";
                ddlRecivefrom.Focus();
            }
            else
            {
                lblExcepitonDiary.Text = "Already Submitted this Diary No.";
                lblExcepitonDiary.ForeColor = System.Drawing.Color.Red;
                ddlRecivefrom.Focus();
            }
        }
        catch (SqlException ex)
        {
            lblExcepitonDiary.Text = ex.ToString();
            lblExcepitonDiary.ForeColor = System.Drawing.Color.Red;
        }
        catch (FormatException ex)
        {
            lblExcepitonDiary.Text = "Invalid Date.";
            lblExcepitonDiary.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            getrefno();
        }
    }
    private Boolean chkdupli()
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("select DiaryNo from DiaryEntry where DiaryNo='" + lblDiaryNo.Text.ToString() + "'", con);
        string sid = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        if (sid == "")
        {
            return true;
        }
        else
        {
            return false;
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
            ddlCourierService.DataBind();
            con.Close();
            con.Dispose();
        }
        btnCencel.Focus();
    }
    protected void btnCencelnew_Onclick(object sender, EventArgs e)
    {
        panelCourier.Visible = false;
        ddlCourierService.Focus();
    }
    protected void grvCourierHome_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
            grvCourierHomeFill();
            grvCourierHome.PageIndex = e.NewPageIndex;
            grvCourierHome.DataBind();
    }
    private void grvCourierHomeFill()
    {
        string strFillGridview = "select DiaryNo,MemberType as ReceiveFrom,MembershipNo,Name,CourierService as Courier,CourierNo as RefNo,Remark from DiaryEntry where Status='DiaryEntry' ";
        SqlDataAdapter adp = new SqlDataAdapter(strFillGridview, con);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        grvCourierHome.DataSource = ds;
        grvCourierHome.DataBind();
    }
    protected void txtDDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            int[] diff = new int[3];
            DateTime dt = Convert.ToDateTime(txtDDate.Text, dtinfo);
            DateTime now = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"), dtinfo);
            diff = chkdob(now, dt);
            if (diff[0] == 0 & diff[1] == 0 & diff[2] == 100)
            {
                lblExceptiondAte.Text = "Receiving Date is earlier than Dispatch Date.";
                btnSaveDiary.Enabled = false;
            }
            else
            {
                lblExceptiondAte.Text = "";
                btnSaveDiary.Enabled = true;
            }

        }
        catch (FormatException ex)
        {
            lblExceptiondAte.Text = "Invalid Dispatch Date Format.";
        }
        txtDDate.Focus();
    }
    private int[] chkdob(DateTime now, DateTime dt)
    {
        int[] dif = new int[3];
        int mo, dy;
        if (dt.Year == now.Year & now.Month == dt.Month & dt.Day > now.Day)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else if (dt.Year == now.Year & dt.Month > now.Month)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else if (dt.Year > now.Year)
        {

            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else
        {
            int yr = now.Year - dt.Year;
            if (now.Month < dt.Month || now.Month == dt.Month && now.Day < dt.Day)
            {
                --yr;
            }
            dif[0] = yr;
            if (now.Month < dt.Month)
            {
                mo = (12 - dt.Month) + now.Month;
                if (now.Day < dt.Day)
                    --mo;
            }
            else
            {
                mo = now.Month - dt.Month;
                if (now.Month == dt.Month & now.Day < dt.Day)
                {
                    --mo;
                }
            }
            dif[1] = mo;
            if (now.Day < dt.Day)
            {
                if (now.Month == 1)
                {
                    int ddy = DateTime.DaysInMonth(now.Year, now.Month);
                    dy = (ddy - dt.Day) + now.Day;
                }
                else
                {
                    int ddy = DateTime.DaysInMonth(now.Year, now.Month - 1);
                    dy = (ddy - dt.Day) + now.Day;
                }
            }
            else
            {
                dy = now.Day - dt.Day;
            }
            dif[2] = dy;
        }

        return dif;
    }
    protected void grvCourierHome_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (lblgh.Text != "1")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].Text = e.Row.Cells[6].Text.ToString().TrimEnd('0').TrimEnd('.');
            }
        }
        if (lblgh.Text == "1")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[4].Text = "Membership";
            }
        }
    }
    string strqry;
    protected void btnSearchDiary_Click(object sender, EventArgs e)
    {
        binddata();
    }
    private void binddata()
    {
        lblgh.Text = "1";
        if (txtNameSearch.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Enter Name.')", true);
        }
        else
        {
            if (ddlSearchIn.SelectedItem.Text == "Student" && txtNameSearch.Text != "")
            {
                strqry = "select IMID,Name,IMName,IMCity,SID from student where Name like '%" + txtNameSearch.Text.ToString() + "%'";
            }
            else if (ddlSearchIn.SelectedItem.Text == "IM" && txtNameSearch.Text != "")
            {
                strqry = "select ID,Name,HName,HCity,ID from IM where ID like '%" + txtNameSearch.Text.ToString() + "%'";
            }
            SqlDataAdapter adp = new SqlDataAdapter(strqry, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            grvCourierHome.DataSource = dt;
            grvCourierHome.DataBind();
            grvCourierHome.Focus();
        }

    }
}
