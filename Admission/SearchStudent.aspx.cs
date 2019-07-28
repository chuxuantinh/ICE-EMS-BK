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

public partial class Admission_SearchStudent : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0) ddlExamSeason.SelectedValue = "Sum";
            else ddlExamSeason.SelectedValue = "Win";
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
             bindName();
            ddlExamSeason.Focus();
            GridView1.Visible = false;
            txtSearch.Text = "";
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
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindName();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        con.Open();
        GridView1.Visible = true;
        string qry = "";
        if (CheckBox1.Checked == true)
        {
            if (DropDownList1.SelectedValue.ToString() == "SID")
            {
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where SID='"+txtSearch.Text+"' and Session='"+lblSeasonHidden.Text+"' ";
            }
            if (DropDownList1.SelectedValue.ToString() == "IMID")
            {
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where IMID='" + txtSearch.Text + "' and Session='" + lblSeasonHidden.Text + "'";
            }
            if (DropDownList1.SelectedValue.ToString() == "Name")
            {
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where  Name like '%" + txtSearch.Text + "%' and Session='" + lblSeasonHidden.Text + "'";
            }
            if (DropDownList1.SelectedValue.ToString() == "SerialNo")
            {
                SqlCommand cm = new SqlCommand("select Enrolment from AppRecord where FormType like '%" + txtSearch.Text + "NewAdmission%' and Session='" + lblSeasonHidden.Text + "'", con);
                string stcm = Convert.ToString(cm.ExecuteScalar());
                cm.ExecuteNonQuery();
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where SID='" + stcm + "'";
            }
            if (DropDownList1.SelectedValue.ToString() == "Status")
            {
                if (chkIMID.Checked == true)
                {
                    qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where Status='" + DropDownList2.SelectedValue + "' and IMID='" + txtSearch.Text + "' and  Session='" + lblSeasonHidden.Text + "' ";
                }
                else if (chkIMID.Checked == false)
                {
                    qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where Status='" + DropDownList2.SelectedValue + "' and Session='" + lblSeasonHidden.Text + "' ";
                }
            }
        }
        else if (CheckBox1.Checked == false)
        {
            if (DropDownList1.SelectedValue.ToString() == "SID")
            {
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where SID='" + txtSearch.Text + "' ";
            }
            if (DropDownList1.SelectedValue.ToString() == "IMID")
            {
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where IMID='" + txtSearch.Text + "' and Session='" + lblSeasonHidden.Text + "'";
            }
            if (DropDownList1.SelectedValue.ToString() == "Name")
            {
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where Name like '%" + txtSearch.Text + "%'  ";
            }
            if (DropDownList1.SelectedValue.ToString() == "SerialNo")
            {
                SqlCommand cm = new SqlCommand("select Enrolment from AppRecord where FormType like '%" + txtSearch.Text + "NewAdmission%' and Session='" + lblSeasonHidden.Text + "'", con);
                string stcm = Convert.ToString(cm.ExecuteScalar());
                cm.ExecuteNonQuery();
                qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where SID='" + stcm + "'";
            }
            if (DropDownList1.SelectedValue.ToString() == "Status")
            {
                if (chkIMID.Checked == true)
                {
                    qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where Status='" + DropDownList2.SelectedValue + "' and IMID='" + txtSearch.Text + "'  ";
                }
                else if (chkIMID.Checked == false)
                {
                    qry = "select SID,Name,FName,MName,Mobile,Email,DOB,IMID,IMName,EnrollDate from Student where Status='" + DropDownList2.SelectedValue + "'  ";
                }
            }
        }
        SqlDataAdapter ad = new SqlDataAdapter();
        ad = new SqlDataAdapter(qry, con);
        DataTable at = new DataTable();
        ad.Fill(at);
        GridView1.DataSource = at;
        GridView1.DataBind();
        con.Close();
        GridView1.Focus();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewImg();
        con.Open();
        GridViewRow row;
        row = GridView1.SelectedRow;
        SqlCommand cmd = new SqlCommand("select * from ExamCurrent where SId='"+row.Cells[1].Text+"'", con);
        SqlDataReader read;
        read = cmd.ExecuteReader();
        while (read.Read())
        {
            lblCourse.Text = read["Course"].ToString() + ","; 
            lblName.Text = read["SName"].ToString();
            lblPart.Text = read["Part"].ToString();
            lblIMID.Text = read["IMID"].ToString();
            lblComp.Text = read["CompositeStatus"].ToString();
            lblSess.Text = read["Session"].ToString();
            lblExamStatus.Text = read["ExamStatus"].ToString();
            lblSessionDuration.Text = read["SessionDuration"].ToString();
            lblMembership.Text = read["SId"].ToString();
            string stream = read["Stream"].ToString();
            if (stream == "Tech")
            {
                lblStream.Text = "Technician" + ",";
            }
            else if (stream == "Asso")
            {
                lblStream.Text = "Associate" +",";
            }
        }
        read.Close();
        SqlCommand cmd2 = new SqlCommand("select * from Student where SID='" + row.Cells[1].Text + "'", con);
        read = cmd2.ExecuteReader();
        while (read.Read())
        {
            lblPAddress.Text = read["PAddress"].ToString();
            lblPAddress2.Text = read["PAddress2"].ToString();
            lblPCity.Text = read["PCity"].ToString();
            lblPState.Text = read["PState"].ToString();
            lblCAddress.Text = read["CAddress"].ToString();
            lblCCity.Text = read["CCity"].ToString();
            lblCState.Text = read["CState"].ToString();
            lblPhone.Text = read["Phone"].ToString();
            lblMob.Text = read["Mobile"].ToString();
            lblAdStatus.Text = read["Status"].ToString();
            lblAdStream.Text = read["Stream"].ToString();
            lblAdCourse.Text = read["Course"].ToString();
            lblAdPart.Text = read["Part"].ToString();
        }
        read.Close();
        cmd = new SqlCommand("select sum(Amount) from AppRecord where Enrolment='" + row.Cells[1].Text + "'", con);
        lblTotalSubAmnt.Text = cmd.ExecuteScalar().ToString().TrimEnd('0').TrimEnd('.');
        cmd = new SqlCommand("select sum(Amount) from AppRecord where Enrolment='" + row.Cells[1].Text + "' and Session='" + lblSeasonHidden.Text + "' ", con);
        lblAnnualSubsSession.Text = cmd.ExecuteScalar().ToString().TrimEnd('0').TrimEnd('.');
        cmd = new SqlCommand("select sum(LateFee) from AppRecord where Enrolment='" + row.Cells[1].Text + "'", con);
        lblTotalLAteAmnt.Text = cmd.ExecuteScalar().ToString().TrimEnd('0').TrimEnd('.');
        cmd = new SqlCommand("select AnnualSubscription from Student where SID='" + row.Cells[1].Text + "'", con);
        lblAnnualSubs.Text = cmd.ExecuteScalar().ToString().TrimEnd('0').TrimEnd('.');
        cmd = new SqlCommand("select Status from Project where SID='" + row.Cells[1].Text + "'", con);
        lblProjectStatus.Text = Convert.ToString(cmd.ExecuteScalar());
        if (lblProjectStatus.Text == "")
        {
            lblProjectStatus.Text = "NotSubmitted";
        }
        SqlDataAdapter cmd1 = new SqlDataAdapter("select Enrolment,DNo,Session,FormType,Amount,LateFee,AdmissionFees,ITIFees,CADFees,ExamFees,CompositeFees,AnnualSubFees,DupForm,SubDate,Status from AppRecord where Enrolment='" + row.Cells[1].Text + "' ORDER By SN Desc", con);
        DataTable at = new DataTable();
        cmd1.Fill(at);
        grdStudentAccount.DataSource = at;
        grdStudentAccount.DataBind();
        fillResult(row.Cells[1].Text.ToString(), con);
        con.Close();
        ddlExamSeason.Focus();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text == "" | e.Row.Cells[10].Text == "&nbsp;")
            {
            }
            else
            {
                e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");
            }
            if (e.Row.Cells[7].Text == "" | e.Row.Cells[7].Text == "&nbsp;")
            {
            }
            else
            {
                e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).ToString("dd/MM/yyyy");
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "Membership";
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        GridView1.Visible = false;
        txtSearch.Focus();
    }
    protected void grdStudentAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[9].Text = e.Row.Cells[9].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[10].Text = e.Row.Cells[10].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[11].Text = e.Row.Cells[11].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[12].Text = e.Row.Cells[12].Text.TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[12].Text == "" | e.Row.Cells[12].Text == "&nbsp;")
            {
            }
            else
            {
                e.Row.Cells[13].Text = Convert.ToDateTime(e.Row.Cells[13].Text).ToString("dd/MM/yyyy");
            }
            if (e.Row.Cells[14].Text == "NotApproved")
            {
                e.Row.Cells[14].Text = "FormEntry";
            }
            else if (e.Row.Cells[14].Text == "Filled")
            {
                e.Row.Cells[14].Text = "Submitted";
            }
            else
            {
                e.Row.Cells[14].Text = "Approved By A/C";
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Membership";
            e.Row.Cells[1].Text = "Diary No.";
            e.Row.Cells[10].Text = "CompFee";
            e.Row.Cells[11].Text = "ASF";
        }
    }
    private void bindName()
    {
        txtSearch.Text = "";
        GridView1.Visible = false;
        if (DropDownList1.SelectedValue.ToString() == "SID")
        {
            lblDrpName.Text = DropDownList1.SelectedItem.Text.ToString() + " No. :";
            DropDownList2.Visible = false;
            txtSearch.Visible = true;
            chkIMID.Visible = false;
        }
        if (DropDownList1.SelectedValue.ToString() == "IMID")
        {
            lblDrpName.Text = DropDownList1.SelectedItem.Text.ToString() + " :";
            DropDownList2.Visible = false;
            txtSearch.Visible = true;
            chkIMID.Visible = false;
        }
        if (DropDownList1.SelectedValue.ToString() == "Name")
        {
            lblDrpName.Text = "Student " + DropDownList1.SelectedItem.Text.ToString() + " :";
            DropDownList2.Visible = false;
            txtSearch.Visible = true;
            chkIMID.Visible = false;
        }
        if (DropDownList1.SelectedValue.ToString() == "Status")
        {
            lblDrpName.Text = "IMID :";
            DropDownList2.Visible = true;
            txtSearch.Visible = true;
            chkIMID.Visible = true;
        }
        if (DropDownList1.SelectedValue.ToString() == "SerialNo")
        {
            lblDrpName.Text = DropDownList1.SelectedItem.Text.ToString() + " :";
            DropDownList2.Visible = false;
            txtSearch.Visible = true;
            chkIMID.Visible = false;
        }
        DropDownList1.Focus();
    }
    private void fillResult(string sid, SqlConnection con)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select SID,ExamSeason,Course,Part,SubID,Status from SExamMarks where SID='" + sid + "' order by  Part,ExamSeason desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridResult.DataSource = dt;
        GridResult.DataBind();
    }
    int PI = 0, PII = 0, SA = 0, SB = 0;
    protected void GridResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "PartI")
            {
                if (e.Row.Cells[5].Text == "Pass") PI += 1;
                e.Row.BackColor = System.Drawing.Color.AntiqueWhite;
            }
            else if (e.Row.Cells[3].Text == "PartII")
            {
                if (e.Row.Cells[5].Text == "Pass") PII += 1;
                e.Row.BackColor = System.Drawing.Color.Aquamarine;
            }
            else if (e.Row.Cells[3].Text == "SectionA")
            {
                if (e.Row.Cells[5].Text == "Pass") SA += 1;
                e.Row.BackColor = System.Drawing.Color.AliceBlue;
            }
            else if (e.Row.Cells[3].Text == "SectionB")
            {
                if (e.Row.Cells[5].Text == "Pass") SB += 1;
                e.Row.BackColor = System.Drawing.Color.LightCoral;
            }
        }
        lblPartICount.Text = PI.ToString();
        lblPartIICount.Text = PII.ToString();
        lblSectionACount.Text = SA.ToString();
        lblSectionBCount.Text = SB.ToString();
    }

    public void ViewImg()
    {
        GridViewRow row;
        row = GridView1.SelectedRow;
        SqlCommand command = new SqlCommand("SELECT ImageName,SID from [Docs] where SID='" + row.Cells[1].Text + "'", con);
        SqlDataAdapter daimages = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }
}