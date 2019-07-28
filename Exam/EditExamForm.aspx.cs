using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Exam_EditExamForm : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd; SqlDataAdapter adp;
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
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    ddlExamSeason.Focus();
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
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void txtSerialNo_TextChanged(object sender, EventArgs e)
    {
         ok(); 
    }
    private void ok()
    {
        lblExceptionOK.Text = "";
        try
        {
            con.Close(); con.Open();
            int i = 0;
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select SID,Part from ExamForms where SID='" + txticesn.Text.ToString() + "' and  ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
            string apno = "", Part = "";
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                apno = reader["SID"].ToString(); lblPart.Text = reader["Part"].ToString();
            }
            reader.Close();
            if (apno != "")
            {
                lblEnrolment.Text = apno.ToString();
                apno = "1";
            }
            else
            {
                string sn = txticesn.Text.ToString();
                cmd = new SqlCommand("select Enrolment,Part from AppRecord where Exam= 'E" + sn.ToString() + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    apno = reader["Enrolment"].ToString(); Part = reader["Part"].ToString();
                }
                reader.Close();
                if (apno != "")
                {
                    lblEnrolment.Text = apno.ToString();
                    apno = "2";
                    cmd = new SqlCommand("select SN from ExamForms where SID='" + lblEnrolment.Text.ToString() + "' and Part='" + Part.ToString() + "' and  ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                    string chk = Convert.ToString(cmd.ExecuteScalar());
                    if (chk != "") { lblSN.Text = chk.ToString(); }
                    else{}
                }
                else
                {
                    lblExceptionOK.Text = "Record Not Found !"; lblName.Text = ""; lblCourse.Text = ""; lblIMID.Text = ""; BindExamGrid(); GridExamForms.DataBind(); pnlDetails.Visible = false; 
                }
                if (apno == "2")
                {
                    SqlCommand cmdg = new SqlCommand("select * from AppRecord where Exam ='E" + sn + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                    SqlDataReader sdr;
                    sdr = cmdg.ExecuteReader();
                    while (sdr.Read())
                    {
                        lblIMID.Text = sdr["IMID"].ToString();
                        lblName.Text = sdr["Name"].ToString() + " s/o " + sdr["FName"].ToString();
                        lblCourse.Text = sdr["Stream"].ToString() + ", " + sdr["Course"].ToString();
                        lblPart.Text = sdr["Part"].ToString();
                        lblIMID.Text = "IMID: " + sdr["IMID"].ToString();
                        lblEnrolment.Text = sdr["Enrolment"].ToString();
                        if (lblEnrolment.Text == sdr["AppNo"].ToString()) { lblTempEnrol.Text = "[TEMP]"; }
                        else { lblTempEnrol.Text = ""; }
                        lblExceptionOK.Text = "";
                    } sdr.Close(); BindExamGrid(); GridExam.Focus();
                    sdr.Dispose();
                }
            }
            if (apno == "1")
            {
                cmd = new SqlCommand("select * from ExamForms where SID='" + txticesn.Text.ToString() + "' and Part='" + lblPart.Text.ToString() + "' and  ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblName.Text = "";
                    lblCourse.Text = reader["Course"].ToString() + "- " + reader["Part"].ToString();
                    lblPart.Text = reader["Part"].ToString();
                    lblIMID.Text = "IMID: " + reader["IMID"].ToString();
                    lblEnrolment.Text = reader["SID"].ToString();
                    lblSN.Text = reader["SN"].ToString();
                }
                reader.Close(); BindExamGrid(); GridExam.Focus();
                reader.Dispose();
            }
        }
        catch (SqlException ex) { lblExceptionOK.Text = ex.ToString(); }
        catch (FormatException ex) { lblExceptionOK.Text = ex.ToString(); }
        finally { con.Close(); con.Dispose(); }
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString(); txtYearSeason.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString(); txtYearSeason.Focus();
    }
    private void BindExamGrid()
    {
        adp = new SqlDataAdapter("select SN,SID,IMID,Course,Part,City from ExamForms where SID='" + txticesn.Text.ToString() + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridExam.DataSource = dt;
        GridExam.DataBind();
    }
    protected void DeleteRecord(object sender, GridViewDeleteEventArgs e)
    {
        con.Open();
        string SN = GridExam.DataKeys[e.RowIndex].Value.ToString();
        cmd = con.CreateCommand();
        SqlTransaction trans;
        trans = con.BeginTransaction("RangeTrans");
        cmd.Connection = con;
        cmd.Transaction = trans;
        try
        {
            cmd.CommandText = "delete ExamForms where SN='" + SN + "' and Part='" + lblPart.Text + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete ExamForm where SN='" + SN + "'";
            cmd.ExecuteNonQuery();
            if (lblPart.Text == "PartII")
            {
                cmd.CommandText = "update ExamCurrent set CourseStatus=@CourseStatus where SId='" + lblEnrolment.Text.ToString() + "'";
                cmd.Parameters.AddWithValue("@CourseStatus", "Approved");
            }
            else
            {
                cmd.CommandText = "update ExamCurrent set ExamStatus=@ExamStatus where SId='" + lblEnrolment.Text.ToString() + "'";
                cmd.Parameters.AddWithValue("@ExamStatus", "Approved");
            }
            cmd.ExecuteNonQuery();
            trans.Commit();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Record Re-Submit successfully !')", true); BindExamGrid(); GridExamForms.DataBind(); pnlDetails.Visible = false; lblEnrolment.Text = ""; txticesn.Text = ""; txticesn.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not deleted.!')", true); BindExamGrid(); pnlDetails.Visible = false;  lblEnrolment.Text = ""; txticesn.Text = ""; txticesn.Focus();
            try { trans.Rollback(); }
            catch (Exception ex2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not deleted due to transaction rollback fail.!')", true); BindExamGrid(); pnlDetails.Visible = false; BindGridExamForms(); lblEnrolment.Text = ""; txticesn.Text = ""; txticesn.Focus();
            }
        }
        finally { con.Close(); con.Dispose(); }
    }
    protected void btnSubmit_Onclick(object sender, EventArgs e)
    {
        cmd = new SqlCommand("select CourseStatus from ExamCurrent where SId='" + lblEnrolment.Text.ToString() + "'", con);
        string sts = Convert.ToString(cmd.ExecuteScalar());
        if (sts == "Promotted" || sts == "Submitted")
        {
        }
        else sts = "Approved";
        cmd = new SqlCommand("update ExamCurrent set ExamStatus=@ExamStatus where SId='" + lblEnrolment.Text.ToString() + "'", con);
        cmd.Parameters.AddWithValue("@ExamStatus", sts);
        cmd.ExecuteNonQuery();
    }
    private void BindGridExamForms()
    {
        adp = new SqlDataAdapter("select ExamForm.SubID,ExamForm.SubName,ExamForms.Status,ExamForms.City, ExamForms.RollNo from ExamForm inner join ExamForms on ExamForm.SN=ExamForms.SN and ExamForms.SID='" + lblEnrolment.Text.ToString() + "' and  ExamForms.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ExamForms.SN='" + GridExam.SelectedDataKey.Value + "'", con);
        DataSet ds = new DataSet(); adp.Fill(ds); GridExamForms.DataSource = ds; GridExamForms.DataBind();
    }
    protected void GridExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSN = GridExam.SelectedDataKey.Value.ToString(); if (strSN == "") { lblExceptionOK.Text = "Not Found!"; }
        else { BindGridExamForms(); }
    }
}
