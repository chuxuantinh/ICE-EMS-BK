using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Data;
using System.Globalization;

public partial class Admission_ChangeCourse : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    lblHiddenStream.Text = "Tech";
                    ddlCourse.Enabled = false; ddlPart.Enabled = false; btnUpdate.Enabled = false;
                    txtOldSID.Visible = false; ibtnOldSIDInfo.Visible = false; pnlOldSIDInfo.Visible = false;
                    rbtnNewAdmission.Checked = true; tblApps.Visible = false; rbtnMembership.Checked = true;
                    txtSID.Focus();
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
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void ddlExamSeason_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtSID.Focus();
    }
    protected void txtSID_TextChanged(object sender, EventArgs e)
    {
        if (rbtnMembership.Checked == true && txtSID.Text.ToString().Contains("A") == true)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Insert Correct Membership No')", true);
        }
        else if (rbtnSerailNo.Checked == true && txtSID.Text.ToString().Contains("A") != true)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Insert Correct Serial No.')", true);
        }
        else
        {
            tblApps.Visible = true;
            con.Close(); con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            SqlDataReader reader;
            fillResult(txtSID.Text.ToString(), con);
            //if (rbtnMembership.Checked == true)
            //    cmd.CommandText = "select count(Enrolment) from AppRecord where Enrolment='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and (FormType like '%Exam%') ";
            //else
            //    cmd.CommandText = "select  count(Enrolment) from AppRecord where Sid='" + txtSID.Text + "'  and Session='" + lblSeasonHidden.Text.ToString() + "' and (FormType like '%Exam%') ";
            //cmd.Connection = con;
            //lblExamCount.Text = cmd.ExecuteScalar().ToString();
                if (rbtnMembership.Checked == true)
                    cmd.CommandText = "select * from AppRecord where Enrolment='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and (FormType like '%Admission%') ";
                else
                    cmd.CommandText = "select * from AppRecord where Sid='" + txtSID.Text + "'  and Session='" + lblSeasonHidden.Text.ToString() + "' and (FormType like '%Admission%') ";
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    if (reader["Status"].ToString() == "NotApproved")
                        lblApplication.Text = "NotApproved";
                    else lblApplication.Text = "Approved";
                    lblExamSerialNo.Text = reader["Exam"].ToString();
                    lblITISerialNo.Text = reader["ITI"].ToString();
                    lblName.Text = reader["Name"].ToString();
                    lblFatherName.Text = reader["FName"].ToString();
                    ddlCourse.Enabled = true; ddlPart.Enabled = true; btnUpdate.Enabled = true;
                }
            else lblApplication.Text = "Application Not Found";
                reader.Close();
            if (rbtnNewAdmission.Checked == true || rbtnNewAdmissiontoOld.Checked == true)
                cmd.CommandText = "select * from Student where SID='" + txtSID.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
            else
                cmd.CommandText = "select * from Student where SID='" + txtSID.Text.ToString() + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblCourseAdmisison.Text = reader["Course"].ToString();
                lblPartAdmission.Text = reader["Part"].ToString();
                lblAdmissionStatus.Text = reader["Status"].ToString();
                lblName.Text = reader["Name"].ToString();
                lblFatherName.Text = reader["FName"].ToString();
            }
            else
            {
                lblAdmissionStatus.Text = "NotSubmitted";
            }
            reader.Close();
            cmd = new SqlCommand("select * from ExamCurrent where sid='" + txtSID.Text.ToString() + "'", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblExamFormStatus.Text = reader["ExamStatus"].ToString();
                lblCourseCurrent.Text = reader["Course"].ToString();
                lblPartCurrent.Text = reader["Part"].ToString();
                txtRemarks.Text = reader["CourseRemarks"].ToString();
                lblEnrolment.Text = reader["IMID"].ToString();
                ddlCourse.SelectedValue = lblCourseCurrent.Text;
                ddlPart.SelectedValue = lblPartCurrent.Text;
                ddlCourseStatus.SelectedValue = reader["CourseStatus"].ToString();
                ddlCourse.Enabled = true; ddlPart.Enabled = true; btnUpdate.Enabled = true;
                lblException.Text = "";
                btnUpdate.Enabled = true;
            }
            else lblException.Text = "Membership Not Found.";
            reader.Close(); reader.Dispose();
            if (ddlCourseStatus.SelectedValue == "Promotted" || ddlCourseStatus.SelectedValue == "Submitted")
                lblExamCount.Text = "2";
            if (lblExamFormStatus.Text == "Filled")
            {
                pnlExamForm.Visible = true;
                SqlDataAdapter ad = new SqlDataAdapter("select Course,Part,Status,ExamSeason,IMID,CenterCode,RollNo,City,City2,Remarks from ExamForms where SID='" + txtSID.Text.ToString() + "' and ExamSeason='" + lblSeasonHidden.Text.ToString() + "'", con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                GridExam.DataSource = dt;
                GridExam.DataBind();
            }
            lblITIFormStatus.Text = ITIForm();
            chkAmount();
            chkAdditionalPaper();
            con.Close(); con.Dispose();
        }
      txtSID.Focus();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkAmount();
        if(ddlPart.SelectedValue.ToString()=="PartI" || ddlPart.SelectedValue.ToString()=="PartII") lblHiddenStream.Text="Tech";
        else lblHiddenStream.Text="Asso";
        txtRemarks.Focus();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkAmount();
        ddlPart.Focus();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (lblExamFormStatus.Text == "Filled" || ddlAdditionalPaper.SelectedValue=="")
        {
            lblException.Text = "Please First Delete ExamForm.";
            if (ddlAdditionalPaper.SelectedValue == "")
                lblException.Text = "Please Select Additional Paper Option.";
        }
        else
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("update ExamCurrent set Stream=@Stream , Course=@Course ,Part=@Part, CourseStatus=@CourseStatus,CourseRemarks=@CourseRemarks where SID='" + txtSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Stream", lblHiddenStream.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CourseStatus", ddlCourseStatus.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CourseRemarks", txtRemarks.Text.ToString());
            cmd.ExecuteNonQuery();
            string qry = "";
            if (Convert.ToInt32(lblExamCount.Text) == 2)
            {
                if (rbtnMembership.Checked == true)
                    qry = "update AppRecord set Stream='" + lblHiddenStream.Text.ToString() + "' , Course='" + ddlCourse.SelectedValue.ToString() + "',Part='" + ddlPart.SelectedValue.ToString() + "' where Enrolment='" + txtSID.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and Part!='PartII'";
                else
                    qry = "update AppRecord set Stream='" + lblHiddenStream.Text.ToString() + "' , Course='" + ddlCourse.SelectedValue.ToString() + "',Part='" + ddlPart.SelectedValue.ToString() + "' where SID='" + txtSID.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and Part!='PartII'";
            }
            else
            {
                if (rbtnMembership.Checked == true)
                    qry = "update AppRecord set Stream='" + lblHiddenStream.Text.ToString() + "' , Course='" + ddlCourse.SelectedValue.ToString() + "',Part='" + ddlPart.SelectedValue.ToString() + "' where Enrolment='" + txtSID.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                else
                    qry = "update AppRecord set Stream='" + lblHiddenStream.Text.ToString() + "' , Course='" + ddlCourse.SelectedValue.ToString() + "',Part='" + ddlPart.SelectedValue.ToString() + "' where SID='" + txtSID.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
            }
            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            if (rbtnNewAdmission.Checked == true)
            {
                cmd = new SqlCommand("update Student set Stream='" + lblHiddenStream.Text.ToString() + "' , Course='" + ddlCourse.SelectedValue.ToString() + "',Part='" + ddlPart.SelectedValue.ToString() + "' where SID='" + txtSID.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
            }
            else if (rbtnNewAdmissiontoOld.Checked == true)
            {
                if (lblApplication.Text != "Not Found" && txtOldSID.Text != txtSID.Text)
                {
                    cmd = new SqlCommand("delete ExamCurrent where SID='" + txtOldSID.Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                }
                if (lblAdmissionStatus.Text != "NotSubmitted")
                {
                    cmd = new SqlCommand("delete Student where SID='" + txtSID.Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                }
                if (lblExamFormStatus.Text != "NotSubmitted")
                {
                    if (chkDeleteExamForm.Checked == true)
                    {
                        //   cmd = new SqlCommand("delete ExamForm  From ExamForm,ExamForms where ExamForm.SID='" + txtSID.Text.ToString() + "' and ExamForm.SN=ExamForms.SN and ExamForms.ExamSeason='" + lblSeasonHidden.Text.ToString() + "'", con);
                        //  cmd.ExecuteNonQuery();
                    }
                }
                cmd = new SqlCommand("Update ExamCurrent set SID='" + txtOldSID.Text.ToString() + "' where SID='" + txtSID.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                string FormType = "", FeeType = "";
                if (lblExamFormStatus.Text != "NotSubmitted")
                {
                    FeeType = "ExamFees, OldAdmission Fees";
                    FormType = lblExamSerialNo.Text + "Exam, ReAdmission, ";
                }
                else { FormType = "ReAdmission, "; FeeType = "OldAdmission Fees"; }
                if (lblITIFormStatus.Text != "NotSubmitted")
                {
                    FormType = FormType + lblITISerialNo.Text + "ITI,";
                    FeeType = FeeType + " ITIFees";
                }
                if (rbtnMembership.Checked == true)
                    qry = "update AppRecord set FormType=@FormType,FeeType=@FeeType,Enrolment=@Enrolment where Enrolment='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                else
                    qry = "update AppRecord set FormType=@FormType,FeeType=@FeeType,Enrolment=@Enrolment where SID='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@FormType", FormType.ToString());
                cmd.Parameters.AddWithValue("@FeeType", FeeType.ToString());
                cmd.Parameters.AddWithValue("@Enrolment", txtOldSID.Text.ToString());
                cmd.ExecuteNonQuery();
            }
            if (lblITIFormStatus.Text != "NotSubmitted")
            {
                cmd = new SqlCommand("update ITIForm set Stream='" + lblHiddenStream.Text.ToString() + "' , Course='" + ddlCourse.SelectedValue.ToString() + "',Part='" + ddlPart.SelectedValue.ToString() + "' where SID='" + txtSID.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
            }
            if (chkAmount() == true & (rbtnNewAdmission.Checked==true | rbtnNewAdmissiontoOld.Checked==true))
            {
                if (lblAmountType.Text == "Credit")
                {
                    if (rbtnMembership.Checked == true)
                        qry = "Update AppRecord set Amount=Amount-'" + Convert.ToInt32(lblAmount.Text) + "',AdmissionFees=AdmissionFees-'" + Convert.ToInt32(lblAmount.Text) + "' where Enrolment='" + txtSID.Text + "' and FormType like '%Admission%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                    else
                        qry = "Update AppRecord set Amount=Amount-'" + Convert.ToInt32(lblAmount.Text) + "',AdmissionFees=AdmissionFees-'" + Convert.ToInt32(lblAmount.Text) + "' where SID='" + txtSID.Text + "' and FormType like '%Admission%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                }
                else
                {
                    if (rbtnMembership.Checked == true)
                        qry = "Update AppRecord set Amount=Amount+'" + Convert.ToInt32(lblAmount.Text) + "',AdmissionFees=AdmissionFees+'" + Convert.ToInt32(lblAmount.Text) + "' where Enrolment='" + txtSID.Text + "' and FormType like '%Admission%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                    else
                        qry = "Update AppRecord set Amount=Amount+'" + Convert.ToInt32(lblAmount.Text) + "',AdmissionFees=AdmissionFees+'" + Convert.ToInt32(lblAmount.Text) + "' where SID='" + txtSID.Text + "' and FormType like '%Admission%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                }
                cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                if (Convert.ToInt32(lblExamCount.Text) == 2)
                {
                    if (lblAmountType.Text == "Credit")
                    {
                        if (rbtnMembership.Checked == true)
                            qry = "Update AppRecord set Amount=Amount-'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees-'" + Convert.ToInt32(lblExamAmount.Text) + "' where Enrolment='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and Part!='PartII' and  FormType like '%Exam%' ";
                        else
                            qry = "Update AppRecord set Amount=Amount-'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees-'" + Convert.ToInt32(lblExamAmount.Text) + "' where SID='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and Part!='PartII' and FormType like '%Exam%'";
                    }
                    else
                    {
                        if (rbtnMembership.Checked == true)
                            qry = "Update AppRecord set Amount=Amount+'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees+'" + Convert.ToInt32(lblExamAmount.Text) + "' where Enrolment='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and Part!='PartII' and FormType like '%Exam%'";
                        else
                            qry = "Update AppRecord set Amount=Amount+'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees+'" + Convert.ToInt32(lblExamAmount.Text) + "' where SID='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and Part!='PartII' and FormType like '%Exam%'";
                    }
                }
                else
                {
                    if (lblAmountType.Text == "Credit")
                    {
                        if (rbtnMembership.Checked == true)
                            qry = "Update AppRecord set Amount=Amount-'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees-'" + Convert.ToInt32(lblExamAmount.Text) + "' where Enrolment='" + txtSID.Text + "' and FormType like '%Exam%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        else
                            qry = "Update AppRecord set Amount=Amount-'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees-'" + Convert.ToInt32(lblExamAmount.Text) + "' where SID='" + txtSID.Text + "' and FormType like '%Exam%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                    }
                    else
                    {
                        if (rbtnMembership.Checked == true)
                            qry = "Update AppRecord set Amount=Amount+'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees+'" + Convert.ToInt32(lblExamAmount.Text) + "' where Enrolment='" + txtSID.Text + "' and FormType like '%Exam%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        else
                            qry = "Update AppRecord set Amount=Amount+'" + Convert.ToInt32(lblExamAmount.Text) + "',ExamFees=ExamFees+'" + Convert.ToInt32(lblExamAmount.Text) + "' where SID='" + txtSID.Text + "' and FormType like '%Exam%' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                    }
                }
                cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Insert into RecoverApp(FormType,Amount,Type,Enrolment,IMID,SerialNo,Session,Course,Part,Status,Remark,Date) values(@FormType,@Amount,@Type,@Enrolment,@IMID,@SerialNo,@Session,@Course,@Part,@Status,@Remark,@Date)", con);
                cmd.Parameters.AddWithValue("@FormType", "Admission");
                cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(lblAmount.Text) + Convert.ToInt32(lblExamAmount.Text));
                cmd.Parameters.AddWithValue("@Type", lblAmountType.Text);
                cmd.Parameters.AddWithValue("@Enrolment", txtSID.Text);
                cmd.Parameters.AddWithValue("@IMID", lblEnrolment.Text.ToString());
                cmd.Parameters.AddWithValue("@SerialNo", txtSID.Text);
                cmd.Parameters.AddWithValue("@Session", lblSeasonHidden.Text.ToString());
                cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Status", "NotApproved");
                cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();

                Log.WriteLog(Request.QueryString["name"], "B008", txtSID.Text.ToString(), lblEnrolment.Text.ToString(), "Course Changed");
                Log.WriteLog("B008", Request.QueryString["name"], txtSID.Text.ToString(), lblEnrolment.Text.ToString(), "Course Changed");
            }
            con.Close(); con.Dispose();
            btnUpdate.Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Course Updated.')", true);
        }
    }
    private string ITIForm()
    {
        cmd = new SqlCommand("select Status from ITIForm where SID='" + txtSID.Text + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
        string status = Convert.ToString(cmd.ExecuteScalar());
        if (status == "")
            status = "NotSubmitted";
        return status;
    }
    protected void rbtnNewAdmissionToOld_CheckChanged(object sender, EventArgs e)
    {
        clear();
        txtOldSID.Visible = true; ibtnOldSIDInfo.Visible = true; pnlOldSIDInfo.Visible = true;
        rbtnMembership.Checked = false; rbtnSerailNo.Checked = true;
        rbtnMembership.Enabled = false;
        txtSID.Focus();
    }
    protected void rbtnNewAdmission_CheckedChanged(object sender, EventArgs e)
    {
        clear();
        txtOldSID.Visible = false; ibtnOldSIDInfo.Visible = false; pnlOldSIDInfo.Visible = false;
        rbtnMembership.Enabled = true;
        txtSID.Focus();
    }
    protected void rbtnOldAdmission_CheckedChanged(object sender, EventArgs e)
    {
        clear();
        txtOldSID.Visible = false; ibtnOldSIDInfo.Visible = false; pnlOldSIDInfo.Visible = false;
        rbtnMembership.Enabled = true;
        txtSID.Focus();
    }
    protected void txtOldSID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            fillResult(txtOldSID.Text.ToString(),con);
            cmd = new SqlCommand("select * from Student where sid='" + txtOldSID.Text.ToString() + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblOldName.Text = reader["Name"].ToString();
                lblOldFatherName.Text = reader["FName"].ToString();
                lblOldDOB.Text = Convert.ToDateTime(reader["DOB"].ToString()).ToString("dd/MM/yyyy");
            }
            else
                lblOldName.Text = "Application Not Found";
            reader.Close();
            cmd = new SqlCommand("select * from ExamCurrent where sid='" + txtOldSID.Text.ToString() + "'", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblOldCourse.Text = reader["Course"].ToString();
                lblOldPart.Text = reader["Part"].ToString();
                lblOldExamStatus.Text = reader["ExamStatus"].ToString();
                lblOldCompositeStatus.Text = reader["CompositeStatus"].ToString();
                lblOldCourseStatus.Text = reader["CourseStatus"].ToString();
                txtRemarks.Text = reader["CourseRemarks"].ToString();
                ddlCourseStatus.SelectedValue = lblOldCourseStatus.Text;
                ddlCourse.SelectedValue = lblOldCourse.Text;
                ddlPart.SelectedValue = lblOldPart.Text;
            }
            reader.Close();
            reader.Dispose();
            if (lblOldCourseStatus.Text == "Promotted" || lblOldCourseStatus.Text == "Submitted")
                lblExamCount.Text = "2";
        }
        catch (SqlException ex)
        {
            lblException.Text = "Refresh Page and Try Again.";
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    private void clear()
    {
        txtSID.Text = ""; lblName.Text = ""; lblFatherName.Text = ""; GridExam.DataSource = null; GridExam.DataBind();
        lblApplication.Text = ""; lblAdmissionStatus.Text = ""; lblExamFormStatus.Text = ""; lblITIFormStatus.Text = ""; lblExamSerialNo.Text = ""; lblITISerialNo.Text = "";
        lblPartICount.Text = "0"; lblPartIICount.Text = "0"; lblSectionACount.Text = "0"; lblSectionBCount.Text = "0"; lblOldName.Text = ""; lblOldFatherName.Text = ""; lblOldPart.Text = ""; lblOldCourse.Text = ""; lblOldCourseStatus.Text = ""; lblOldExamStatus.Text = ""; lblOldCompositeStatus.Text = ""; lblOldDOB.Text = "";
        lblExamCount.Text = "0";
        pnlExamForm.Visible = false; tblApps.Visible = false;
    }
    private void fillResult(string sid,SqlConnection con)
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
    private bool chkAmount()
    {
        bool bl=false;
        lblAmount.Text = "0";
        if ((lblPartCurrent.Text == "PartI" || lblPartCurrent.Text == "PartII") && (ddlPart.SelectedValue.ToString() == "SectionA" || ddlPart.SelectedValue.ToString() == "SectionB"))
        {
            bl = true;
            lblAmount.Text = "2000";
        }
        else if ((lblPartCurrent.Text == "SectionA" || lblPartCurrent.Text == "SectionB") && (ddlPart.SelectedValue.ToString() == "PartI" || ddlPart.SelectedValue.ToString() == "PartII"))
        {
            bl = true;
            lblAmount.Text = "2000";
        }
        else
        {
            bl = true;
            lblAmount.Text = "0";
        }
        if (bl == true)
        {
            if (lblPartCurrent.Text != ddlPart.SelectedValue.ToString())
            {
                int CurrentFee = 0, NewFee = 0, csts = 0, nsts = 0, amt = 0;
                if (lblPartCurrent.Text == "PartI")
                {
                    CurrentFee = partIFees("Tech"); csts = 1;
                }
                else if (lblPartCurrent.Text == "PartII")
                {
                    csts = 2; CurrentFee = partIIFees("Tech");
                }
                else if (lblPartCurrent.Text == "SectionA")
                {
                    csts = 3; CurrentFee = sectionAFees("Asso");
                }
                else if (lblPartCurrent.Text == "SectionB")
                {
                    csts = 4; CurrentFee = sectionBFees("Asso");
                }
                if (ddlPart.SelectedValue.ToString() == "PartI")
                {
                    nsts = 1; NewFee = partIFees("Tech");
                }
                else if (ddlPart.SelectedValue.ToString() == "PartII")
                {
                    nsts = 2; NewFee = partIIFees("Tech");
                }
                else if (ddlPart.SelectedValue.ToString() == "SectionA")
                {
                    nsts = 3; NewFee = sectionAFees("Asso");
                }
                else if (ddlPart.SelectedValue.ToString() == "SectionB")
                {
                    nsts = 4; NewFee = sectionBFees("Asso");
                }
                if (csts > nsts)
                {
                    amt = CurrentFee - NewFee; lblAmountType.Text = "Credit";
                }
                else
                {
                    amt = NewFee - CurrentFee; lblAmountType.Text = "Debit";
                }
                lblExamAmount.Text = amt.ToString();
            }
            else
                lblExamAmount.Text = "0";
        }
        if (lblAmount.Text == "0" && lblExamAmount.Text == "0")
            bl = false;
        else bl = true;
        return bl;
    }
    private int partIFees(string stream)
    {
        return 2500;    
    }
    private int partIIFees(string stream)
    {
        return 2750;
    }
    private int sectionAFees(string stream)
    {
        return 3000;
    }
    private int sectionBFees(string stream)
    {
        return 3250;
    }
    private void chkAdditionalPaper()
    {
        cmd = new SqlCommand("select Course from PartIIStudent where SID='" + txtSID.Text.ToString() + "'", con);
        string strfound = Convert.ToString(cmd.ExecuteScalar());
        if (strfound == "")
        {
            lblAdditionalPaper.Text = "Not Found";
        }
        else
        {
            lblAdditionalPaper.Text ="Course "+ strfound.ToString() + " Found";
        }
    }
    string[] subid;
    private void UpdateAdditional()
    {
        if (lblAdditionalPaper.Text == "Not Found")
        {
            if (ddlAdditionalPaper.SelectedValue == "YES")
            {
                string[] civil = { "TC 2.10", "TC 2.11" };
                string[] arch = { "TA 2.11", "TA 2.12" };
                if (ddlCourse.SelectedValue == "Civil")
                {
                    subid = civil;
                }
                else if (ddlCourse.SelectedValue == "Architecture")
                {
                    subid = arch;
                }
                for (int i = 0; i < subid.Length; i++)
                {
                    cmd = new SqlCommand("insert into PartIIStudent (SID,Course,Part,SubID,Status,Remarks,Operator,Date) values(@SID,@Course,@Part,@SubID,@Status,@Remarks,@Operator,@Date)", con);
                    cmd.Parameters.AddWithValue("@SID", lblEnrolment.Text);
                    cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Part", "PartII");
                    cmd.Parameters.AddWithValue("@SubID", subid[i]);
                    cmd.Parameters.AddWithValue("@Status", "Fail");
                    //cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
                    cmd.Parameters.AddWithValue("@Operator", Request.Cookies["MyLogin"]["UID"].ToString());
                    cmd.Parameters.AddWithValue("Date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        else
        {
        }
    }

    #region Admission Rollback
    private void BindGridAdmission()
    {
        adp = new SqlDataAdapter("select SN,SID,Name,Course,Part,Session,Status from Student where SID='" + txtStudentSID.Text + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridAdmissionRollback.DataSource = dt;
        GridAdmissionRollback.DataBind();
    }
    protected void DeleteRecord(object sender, GridViewDeleteEventArgs e)
    {
        con.Open();
        string SID = GridAdmissionRollback.DataKeys[e.RowIndex].Value.ToString();
        cmd = con.CreateCommand();
        SqlTransaction trans;
        trans = con.BeginTransaction("RangeTrans");
        cmd.Connection = con;
        cmd.Transaction = trans;
        try
        {
            cmd.CommandText = "select SN from Student where SID='" + SID + "'";
            string strSN = Convert.ToString(cmd.ExecuteScalar());
            cmd.CommandText = "delete from Docs where SID='" + strSN + "'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from Student where SID='" + SID + "'";
            cmd.ExecuteNonQuery();
            trans.Commit();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Record deleted successfully.!')", true); con.Close(); BindGridAdmission(); 
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not deleted.!')", true); BindGridAdmission();
            try { trans.Rollback(); }
            catch (Exception ex2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not deleted due to transaction rollback fail.!')", true); BindGridAdmission();
            }
        }
        finally { con.Close(); con.Dispose(); }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        con.Open(); lblMessage.Text = "";
        if (txtStudentSID.Text == "") { lblMessage.Text = "Please enter Membership No. !"; BindGridAdmission(); }
        else
        {
            cmd = new SqlCommand("select SID from Student where Status='DisActive' and SID='"+txtStudentSID.Text+"'", con);
            string strsid = Convert.ToString(cmd.ExecuteScalar());
            if (strsid == "") { lblMessage.Text = "Membership Not Found !"; BindGridAdmission(); }
            else { BindGridAdmission(); } con.Close();
        }
    }
    #endregion 
}