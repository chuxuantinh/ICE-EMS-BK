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

public partial class Exam_ExamFormImprovement : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SessionDuration sd;
    string strstatus;
    ClsECenterCity ecity = new ClsECenterCity();

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
        maikal dev = new maikal();
        ecity.getItems(ddlExamCity);
        ecity.getItems(ddlExamCity2);
        int se = dev.chksession();
        if (se == 0) ddlExamSeason.SelectedValue = "Sum";
        else ddlExamSeason.SelectedValue = "Win";
        txtYearSeason.Text = DateTime.Now.Year.ToString();
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        sd = new SessionDuration();
                lblSessionID.Text = sd.SessionToSessionID(lblExamSeasonHidden.Text.ToString()).ToString();
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
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (i == 0 | i == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmissionDefault.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        txticesn.Text = ""; lblEnrolment.Text = "";
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txticesn.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txticesn.Text = "";
        lblEnrolment.Text = "";
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }

    string stream, course, part, examStatus;

    protected void btnOK_TextChanged(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            int i = 0;  ddlExamCity.SelectedIndex = 0;
            cmd = new SqlCommand("select AppNo from AppRecord where Exam='E" + txticesn.Text + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "' and Status!='NotApproved' and Status!='Hold'", con);
            string apno = Convert.ToString(cmd.ExecuteScalar());
            if (apno != "")
            {
                i += 1;
                txtSerialNo.Text = apno.ToString();
            }
            else
            {
                cmd = new SqlCommand("select AppNo from AppRecord where Enrolment='" + txticesn.Text.ToString() + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "' and FormType like '%Exam%' and Status!='NotApproved' and Status!='Hold'", con);
                apno = Convert.ToString(cmd.ExecuteScalar());
                if (apno != "")
                {
                    i += 1;
                    txtSerialNo.Text = apno.ToString();
                }
                else
                    lblExceptionOK.Text = "Record Not Found !";
            }
            if (i == 1)
            {
                cmd = new SqlCommand("select * from AppRecord where AppNo='" + apno + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    stream = sdr["Stream"].ToString(); lblStream.Text = stream.ToString();
                    course = sdr["Course"].ToString(); lblCourse.Text = course.ToString();
                    part = sdr["Part"].ToString(); lblPartName.Text = part.ToString();
                    lblIMID.Text = sdr["IMID"].ToString();
                    lblName.Text = sdr["Name"].ToString();
                    lblFName.Text = sdr["FName"].ToString();
                    lblEnrolment.Text = sdr["Enrolment"].ToString();
                    lblExceptionOK.Text = "";
                }
                sdr.Close();
                sdr.Dispose();
                sfinalPass(lblEnrolment.Text, ddlCoure.SelectedValue.ToString(), ddlPart.SelectedValue.ToString());
            }
        }
        catch (SqlException ex)
        {
            lblExceptionOK.Text = ex.ToString();
        }
        catch (FormatException ex)
        {
            lblExceptionOK.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
            ddlExamCity.Focus();
        }
    }
    private void sfinalPass(string sidd, string course, string part)
    {
        cmd = new SqlCommand("select sid from SFinalPass where SID='" + sidd + "' and Course='" + ddlCoure.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "'", con);
        string sid = Convert.ToString(cmd.ExecuteNonQuery());
        if (sid != "" || sid != null)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select SubID from SExammarks where sid='" + sidd + "' and Course='" + course + "' and Part='" + part + "'", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridCivil.DataSource = dt;
            GridCivil.DataBind();
            panelCivilGrid.Visible = true;
        }
        else
        {
            lblExceptionOK.Text = "Final Pass Data Not Found.";
        }
    }
    protected void btnSaveForm_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select SN from ExamForms where SID='" + lblEnrolment.Text + "' and Part='" + ddlPart.SelectedValue + "' and ExamSeason='" + lblExamSeasonHidden.Text + "'", con);
        string sn = Convert.ToString(cmd.ExecuteScalar());
        if (sn == "" | sn == null)
        {
            int maxsn = 0;
            string qry = "insert into ExamForms(SID,Status,ExamSeason,IMID,Course,Part,CenterCode,RollNo,City,City2,Remarks) values(@SID,@Status,@ExamSeason,@IMID,@Course,@Part,@CenterCode,@RollNo,@City,@City2,@Remarks)";
            SqlCommand dmc = new SqlCommand(qry, con);
            dmc.Parameters.AddWithValue("@SID", lblEnrolment.Text.ToString());
            dmc.Parameters.AddWithValue("@Status", "Submitted");
            dmc.Parameters.AddWithValue("@ExamSeason", ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString());
            dmc.Parameters.AddWithValue("@IMID", lblIMID.Text.ToString());
            dmc.Parameters.AddWithValue("@Course", ddlCoure.SelectedValue.ToString());
            dmc.Parameters.AddWithValue("@Part",ddlPart.SelectedValue.ToString());
            dmc.Parameters.AddWithValue("@CenterCode", "N/A");
            dmc.Parameters.AddWithValue("@RollNo", "N/A");
            dmc.Parameters.AddWithValue("@City", ddlExamCity.SelectedValue.ToString());
            if (ddlExamCity2.SelectedValue.ToString() == "---Select---") ddlExamCity2.SelectedItem.Text = "";
            dmc.Parameters.AddWithValue("@City2", ddlExamCity2.SelectedItem.Text.ToString());
            dmc.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
            dmc.ExecuteNonQuery();
                int i = 0;
                cmd = new SqlCommand("select max(SN) from ExamForms where SID='" + lblEnrolment.Text.ToString() + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "'", con);
                maxsn = Convert.ToInt32(cmd.ExecuteScalar());
                while (i < GridCivil.Rows.Count)
                {
                    CheckBox rbApp = (CheckBox)GridCivil.Rows[i].FindControl("chkAppSubject");
                    if (rbApp.Checked)
                    {
                        insertdata(GridCivil.Rows[i].Cells[0].Text.ToString(),"Ex", maxsn);
                    }
                    i++;
                }
            cmd = new SqlCommand("select Status from AppRecord where Enrolment='" + lblEnrolment.Text.ToString() + "' and FormType like '%Exam%'", con);
            string sts = Convert.ToString(cmd.ExecuteScalar());
            cmd = new SqlCommand("update AppRecord set Status=@Status where AppNo='" + Convert.ToInt32(txtSerialNo.Text) + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Status", sts + " Exam");
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update ExamCurrent set ExamStatus=@ExamStatus where SId='" + lblEnrolment.Text.ToString() + "'", con);
            if (ddlPart.SelectedValue == "PartII")
            {
                if (lblCourseStatus.Text == "Promotted" | lblCourseStatus.Text == "Submitted")
                    cmd.Parameters.AddWithValue("@ExamStatus", lblexamstatus.Text);
                else cmd.Parameters.AddWithValue("@ExamStatus", "Filled");
            }
            else cmd.Parameters.AddWithValue("@ExamStatus", "Filled");
            cmd.ExecuteNonQuery();
            GridCivil.Visible = false; 
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Exam Form Sucessfully Submitted')", true);
            con.Close(); con.Dispose();
            //Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Exam Form Already Submitted')", true);
    }
    private void insertdata(string code,  string stype, int sn)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd = new SqlCommand("insert into ExamForm(SID,SubID,SubName,Status,Date,Time,SubType,SN,SessionID) values(@SID,@SubID,@SubName,@Status,@Date,@Time,@SubType,@SN,@SessionID)", con);
            cmd.Parameters.AddWithValue("@SID", lblEnrolment.Text.ToString());
            cmd.Parameters.AddWithValue("@SubID", code.ToString());
            cmd.Parameters.AddWithValue("@SubName", "");
            cmd.Parameters.AddWithValue("@Status", "Ex");
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Time", "");
            cmd.Parameters.AddWithValue("@SubType","Ex");
            cmd.Parameters.AddWithValue("@SN", sn);
            cmd.Parameters.AddWithValue("@SessionID", Convert.ToInt32(lblSessionID.Text));
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
        }
        finally
        {
        }
    }
}