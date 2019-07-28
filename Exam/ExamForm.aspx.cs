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
using Microsoft.VisualBasic;

public partial class Exam_ExamForm : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    string strstatus;
    ClsECenterCity ecity = new ClsECenterCity();
    ClsExamForm clExamForm = new ClsExamForm();
    SessionDuration fm = new SessionDuration();
    SqlCommand cmd;
    string[] subjects =new string[2];
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
                    pnlSubList.Visible = false;
                    maikal dev = new maikal();
                    ecity.getItems(ddlExamCity);
                    ecity.getItems(ddlExamCity2);
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    panelCivilGrid.Visible = false; panelArchiGrid.Visible = false;
                    panelInVisible.Visible = true;
                    panelVisible.Visible = false;
                    lblException2.ForeColor = System.Drawing.Color.Red; lblException.ForeColor = System.Drawing.Color.Red;
                    txticesn.Focus();
                }
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
        Response.Redirect("ExamForm.aspx?Dev=" + Request.QueryString["Dev"] + "&lnk=null&typ=Ad");
    }
    string stream, course, part, examStatus;
    protected void txtSerialNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            int i = 0; lblFianlPass.Text = ""; ddlExamCity.SelectedIndex = 0;
            cmd = new SqlCommand("select AppNo from AppRecord where Exam='E"+txticesn.Text+"' and Session='" + lblExamSeasonHidden.Text.ToString() + "' and Status!='NotApproved' and Status!='Hold'", con);
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
                cmd = new SqlCommand("select * from AppRecord where AppNo='"+apno+"' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    stream = sdr["Stream"].ToString(); lblHiddendStream.Text = stream.ToString();
                    course =sdr["Course"].ToString(); lblCourseHidden.Text = course.ToString();
                    part = sdr["Part"].ToString(); lblPartHidden.Text = part.ToString();
                    lblIMID.Text = sdr["IMID"].ToString();
                    lblName.Text = sdr["Name"].ToString();
                    lblFName.Text = sdr["FName"].ToString();
                    txtDOB.Text = Convert.ToDateTime(sdr["DOB"].ToString()).ToString("dd/MM/yyyy");
                    lblEnrolment.Text = sdr["Enrolment"].ToString();
                    Examformno.Text = sdr["Exam"].ToString();
                    lblExceptionOK.Text = "";
                    
                }
                sdr.Close();
                sdr.Dispose();
                okk();
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
            Grid2Bind();
            finalpass();
            datesheet(lblCourseHidden.Text.ToString(), lblPartHidden.Text.ToString(), ddlExamSchedule.SelectedValue.ToString(), lblExamSeasonHidden.Text.ToString());
            con.Dispose();
            ddlExamCity.Focus();
        }
    }
    private void okk()
    {
        try
        {
            con.Close(); con.Open();
            lblException.Text = "";
            cmd = new SqlCommand("select * from ExamCurrent where SId='" + lblEnrolment.Text.ToString() + "'", con);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lblEnrolment.Text = rd["SID"].ToString();
                lblEnrolNo.Text = lblEnrolment.Text.ToString();
                //stream = rd["Stream"].ToString(); lblHiddendStream.Text = stream.ToString();
                //course = rd["Course"].ToString(); lblCourseHidden.Text = course.ToString();
                //part = rd["Part"].ToString(); lblPartHidden.Text = part.ToString();
                examStatus = rd["ExamStatus"].ToString();
                lblexamstatus.Text = examStatus;
                lblCourseID.Text = rd["CourseID"].ToString();
                lblCourseStatus.Text = rd["CourseStatus"].ToString();
            }
            rd.Close();
            if (lblPartHidden.Text != "PartII")
            {
                if (examStatus == "NotSubmitted")
                {
                    lblException2.Text = "Examination Form Currently not Submitted. !!!"; lblException.Text = "";
                    panelVisible.Visible = false;
                    panelInVisible.Visible = true;
                }
                else if (examStatus == "Submitted")
                {
                    lblException2.Text = "Examination Form Submitted and  Currently not Approved. !!!"; lblException.Text = "";
                    panelVisible.Visible = false;
                    panelInVisible.Visible = true;
                }
                else if (examStatus == "Filled")
                {
                    lblException2.Text = "Examination Form  Already Submitted !!!"; lblException.Text = "";
                    panelVisible.Visible = false;
                    panelInVisible.Visible = true;
                }
               }
                if (examStatus == "Approved" | lblPartHidden.Text=="PartII")
                {
                    if (chk(lblEnrolment.Text) == "Hold") lblException.Text = "On Hold";
                    else 
                    lblException.Text = "New Examination Form";
                    lblException2.Text = "";
                    lblException.ForeColor = System.Drawing.Color.Green;
                    panelVisible.Visible = true;
                    panelInVisible.Visible = false;
                    strstatus = studentdata(lblEnrolment.Text.ToString());
                    //if (strstatus == "Active")
                    //{
                    if (stream == "Tech")
                        lblStream.Text = "Technician Membership Examination, ";
                    else if (stream == "Asso")
                        lblStream.Text = "Associate Membership Examination, ";
                    if (course == "Civil")
                    {
                        lblCourse.Text = "Civil Engineering";
                        panelCivilGrid.Visible = true;
                        panelArchiGrid.Visible = false;
                    }
                    else if (course == "Architecture")
                    {
                        lblCourse.Text = "Architectural Engineering";
                        panelArchiGrid.Visible = true;
                       panelCivilGrid.Visible = false;
                    }
                    if (part.ToString() == "PartI")
                    {
                         lblPartName.Text = "Part I"; lblpart.Text = part.ToString();
                    }
                    else if (part.ToString() == "PartII")
                    {
                         lblPartName.Text = "Part II"; lblpart.Text = part.ToString();
                    }
                    else if (part.ToString() == "SectionA")
                    {
                         lblPartName.Text = "Section A"; lblpart.Text = part.ToString();
                    }
                    else if (part.ToString() == "SectionB")
                    {
                         lblPartName.Text = "Section B"; lblpart.Text = part.ToString();
                    }
                    //}
                    //else if (strstatus != "Active")
                    //{
                    //    lblException2.Text = "Membership Not Activated, Please Active Membership";
                    //    panelVisible.Visible = false;
                    //    panelInVisible.Visible = true; lblEnrolment.Focus();
                    //}
                }
            }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            lblEnrolment.Focus();
        }
    }
    private string studentdata(string sid)
    {
        cmd=new SqlCommand("select * from Student where SID='"+sid.ToString()+"'",con);
        SqlDataReader reader;
        reader=cmd.ExecuteReader();
        while(reader.Read())
        {
            lblFName.Text=reader[3].ToString();
            lblIMName.Text=reader["IMName"].ToString();
            lblIMCity.Text=reader["IMCity"].ToString();
            strstatus = reader["Status"].ToString();
            lblAdmissionSession.Text = reader["Session"].ToString();
            if ((reader["Part"].ToString() == "PartII") || (reader["Part"].ToString() == "SectionB"))
                lblAdmissionType.Text = "Direct";
            else lblAdmissionType.Text = "Regular";
        }
    reader.Close();
    if (strstatus == "" | strstatus == null)
    {
        cmd = new SqlCommand("select * from AppRecord where Enrolment='" + sid.ToString() + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblAdmissionSession.Text = lblExamSeasonHidden.Text;
            lblName.Text = reader["Name"].ToString();
            lblIMName.Text = reader["IMID"].ToString();
            lblIMCity.Text = "";
            strstatus = "Active";
            if ((reader["Part"].ToString() == "PartII") || (reader["Part"].ToString() == "SectionB"))
                lblAdmissionType.Text = "Direct";
            else lblAdmissionType.Text = "Regular";
        }
        reader.Close();
    }
    reader.Dispose();
    return strstatus;
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        txticesn.Text = ""; lblEnrolment.Text = "";
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txticesn.Focus();
    }
    protected void ddlCity_SelectedInexChanged(object sender, EventArgs e)
    {
        txtExamID.Text = ecity.getCenterCode(ddlExamCity.SelectedValue.ToString());
        ddlExamCity2.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txticesn.Text = "";
        lblEnrolment.Text = "";
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    private void Grid2Bind()
    {
        try
        {
          SqlDataAdapter ad=new SqlDataAdapter("select SubID,GetMarks,ExamSeason from SExamMarks where SID='" + lblEnrolment.Text.ToString() + "' and Part='" + lblpart.Text.ToString() + "' and Status='Pass' order by SubID", con);
          DataTable dt = new DataTable();
          ad.Fill(dt);
            GridView3.DataSource = dt;
            GridView3.DataBind();
          ad=new SqlDataAdapter("select SubID,GetMarks,ExamSeason from SExamMarks where SID='" + lblEnrolment.Text.ToString() + "' and Part='" + lblpart.Text.ToString() + "' and Status='Fail'", con);
          dt = new DataTable();
          ad.Fill(dt);
            GridView4.DataSource = dt;
            GridView4.DataBind();
            if (lblCourseHidden.Text == "Civil")
            {
                ad = new SqlDataAdapter("select * from CivilSubMaster where Section='" + lblpart.Text.ToString() + "' and CourseID='081' and SubID not in(select SubID from SExamMarks where Section='" + lblpart.Text.ToString() + "' and Status='Pass' and SID='" + lblEnrolment.Text.ToString() + "') ORDER BY lvl", con);
                //ad = new SqlDataAdapter("SELECT [SubID], [SubName], [SubjectType] FROM [CivilSubMaster] WHERE (([Section] = '" + lblpart.Text.ToString() + "') AND ([CourseID]='081')) ORDER BY lvl", con);
                dt = new DataTable();
                ad.Fill(dt);
                GridCivil.DataSource = dt;
                GridCivil.DataBind();
            }
            else
            {
                ad = new SqlDataAdapter("select * from ArchiSubMaster where Section='" + lblpart.Text.ToString() + "' and CourseID='081' and SubID not in(select SubID from SExamMarks where Section='" + lblpart.Text.ToString() + "' and Status='Pass' and SID='" + lblEnrolment.Text.ToString() + "') ORDER BY lvl", con);
               // ad = new SqlDataAdapter("SELECT [SubID], [SubName], [SubjectType] FROM [ArchiSubMaster] WHERE (([Section] = '" + lblpart.Text.ToString() + "') AND ([CourseID]='081')) ORDER BY lvl", con);
                dt = new DataTable();
                ad.Fill(dt);
                GridArchi.DataSource = dt;
                GridArchi.DataBind();
            }
          Compare();
        }
        catch (SqlException ex)
        {
        }
        finally
        {
        }
    }  // Get List of subject Fail and pass to Enrolment.text
    private void Compare()
    {
        int reg = 0, ex = 0;
        if (lblCourseHidden.Text == "Architecture")
        {
            foreach (GridViewRow rw2 in GridArchi.Rows)
            {
                if (rw2.Cells[3].Text == "Regular") reg = reg + 1;
                else ex = ex + 1;
            }
            //if (GridArchi.Rows.Count >= GridView3.Rows.Count)
            //{
            //    foreach (GridViewRow rw in GridArchi.Rows)
            //    {
            //        foreach (GridViewRow rw2 in GridView3.Rows)
            //        {
            //            if (rw2.Cells[0].Text.Equals(rw.Cells[0].Text))
            //            {
            //                rw.Enabled = false;
            //                rw.Visible = false;
            //            }
            //        }
            //    }
            //}
            //else if (GridView3.Rows.Count > GridArchi.Rows.Count)
            //{
            //    foreach (GridViewRow rw in GridView3.Rows)
            //    {
            //        foreach (GridViewRow rw2 in GridArchi.Rows)
            //        {
            //            if (rw.Cells[0].Text.Equals(rw2.Cells[0].Text))
            //            {
            //                rw2.Enabled = false;
            //                rw2.Visible = false;
            //            }
            //        }
            //    }
            //    //GridArchi.DataBind();
            //}
            if (GridArchi.Rows.Count >= GridView4.Rows.Count)
            {
                foreach (GridViewRow rw in GridArchi.Rows)
                {
                    foreach (GridViewRow rw2 in GridView4.Rows)
                    {
                        if (rw2.Cells[0].Text.Equals(rw.Cells[0].Text))
                        {
                            rw.BackColor = System.Drawing.Color.Gray;
                            rw.ForeColor = System.Drawing.Color.Maroon;
                        }
                    }
                }
            }
            else if (GridView4.Rows.Count > GridArchi.Rows.Count)
            {
                foreach (GridViewRow rw in GridView4.Rows)
                {
                    foreach (GridViewRow rw2 in GridArchi.Rows)
                    {
                        if (rw.Cells[0].Text.Equals(rw2.Cells[0].Text))
                        {
                            rw2.BackColor = System.Drawing.Color.Gray;
                            rw2.ForeColor = System.Drawing.Color.Maroon;
                        }
                    }
                }
            }
        }
        else
        {
            foreach (GridViewRow rw2 in GridCivil.Rows)
            {
                if (rw2.Cells[3].Text == "Regular") reg = reg + 1;
                else ex = ex + 1;
            }
            //if (GridCivil.Rows.Count >= GridView3.Rows.Count)
            //{
            //    foreach (GridViewRow rw in GridCivil.Rows)
            //    {
            //        foreach (GridViewRow rw2 in GridView3.Rows)
            //        {
            //            if (rw2.Cells[0].Text.Equals(rw.Cells[0].Text))
            //            {
            //                rw.Enabled = false;
            //                rw.Visible = false;
            //            }
            //        }
            //    }
            //}
            //else if (GridView3.Rows.Count > GridCivil.Rows.Count)
            //{
            //    foreach (GridViewRow rw in GridView3.Rows)
            //    {
            //        foreach (GridViewRow rw2 in GridCivil.Rows)
            //        {
            //            if (rw.Cells[0].Text.Equals(rw2.Cells[0].Text))
            //            {
            //                rw2.Enabled = false;
            //                rw2.Visible = false;
            //            }
            //        }
            //    }
            //    //GridArchi.DataBind();
            //}
            if (GridCivil.Rows.Count >= GridView4.Rows.Count)
            {
                foreach (GridViewRow rw in GridCivil.Rows)
                {
                    foreach (GridViewRow rw2 in GridView4.Rows)
                    {
                        if (rw2.Cells[0].Text.Equals(rw.Cells[0].Text))
                        {
                            rw.BackColor = System.Drawing.Color.Gray;
                            rw.ForeColor = System.Drawing.Color.Maroon;
                        }
                    }
                }
            }
            else if (GridView4.Rows.Count > GridCivil.Rows.Count)
            {
                foreach (GridViewRow rw in GridView4.Rows)
                {
                    foreach (GridViewRow rw2 in GridCivil.Rows)
                    {
                        if (rw.Cells[0].Text.Equals(rw2.Cells[0].Text))
                        {
                            rw2.BackColor = System.Drawing.Color.Gray;
                            rw2.ForeColor = System.Drawing.Color.Maroon;
                        }
                    }
                }
            }
        }
        if (lblpart.Text == "PartI" | lblpart.Text == "SectionA")
        {
            lblReg.Text = (6 - reg).ToString();
            lblEx.Text = (0).ToString();
        }
        else if (lblpart.Text == "PartII" && lblCourseHidden.Text == "Civil")
        {
            lblReg.Text = (9 - reg).ToString();
            lblEx.Text = (2 - ex).ToString();
        }
        else if (lblpart.Text == "PartII" && lblCourseHidden.Text == "Architecture")
        {
            lblReg.Text = (10 - reg).ToString();
            lblEx.Text = (2 - ex).ToString();
        }
        else if (lblpart.Text == "SectionB" && lblCourseHidden.Text == "Civil")
        {
            lblReg.Text = (5 - reg).ToString();
            lblEx.Text = (10 - ex).ToString();
        }
        else if (lblpart.Text == "SectionB" && lblCourseHidden.Text == "Architecture")
        {
            lblReg.Text = (5 - reg).ToString();
            lblEx.Text = (8 - ex).ToString();
        }

    }
    string sid;
    protected void btnSaveExamForm_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            int maxsn = 0;
                if (ddlExamCity.SelectedItem.Text != "---Select---")
                {
                    if (lblPartHidden.Text == "PartII")
                    {
                        sid = chk(lblEnrolment.Text);
                    }
                    if (sid == "Hold" | sid == "" | sid==null)
                    {
                        bool bl = false;
                        if (lblFianlPass.Text == "Final Pass")
                        {
                            bl = varifyfinalpass();
                        }
                        if (bl == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Final Pass: Please select proper subject')", true);
                        }
                        else
                        {
                            bool flg = true, flg2 = true;
                            if (lblCourseHidden.Text.ToString() == "Architecture")
                            {
                                int i = 0, reg = 0, ex = 0, chkreg = 0, chkex = 0;
                                while (i < GridArchi.Rows.Count)
                                {
                                    if (GridArchi.Rows[i].Cells[0].Visible == true)
                                    {
                                        CheckBox chkbx = (CheckBox)GridArchi.Rows[i].FindControl("chkAppSubjectA");
                                        if (GridArchi.Rows[i].Cells[3].Text == "Regular")
                                            reg = reg + 1;
                                        if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                                            ex = ex + 1;
                                        if (chkbx.Checked == true)
                                        {
                                            if (GridArchi.Rows[i].Cells[3].Text == "Regular")
                                                chkreg = chkreg + 1;
                                            if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                                                chkex = chkex + 1;
                                        }
                                    }
                                    i++;
                                }
                                if (((lblpart.Text == "PartI") || (lblpart.Text == "SectionA")) && (reg >= 3 && chkreg < 3))
                                {
                                    flg = false;
                                }
                                else if (lblpart.Text == "PartII")
                                {
                                    if (fm.sessionid(lblAdmissionSession.Text) > 121)
                                    {
                                        if (lblAdmissionType.Text == "Direct")
                                        {
                                            if (reg >= 3 && chkreg < 3)
                                                flg = false;
                                            if (reg + ex >= 3 && chkreg + chkex < 3)
                                                flg = false;
                                            if (chkreg + chkex > 6)
                                                flg2 = false;
                                        }
                                        else
                                        {
                                            if (reg >= 3 && chkreg < 3)
                                                flg = false;
                                            if (chkex > 0)
                                                flg2 = false;
                                            if (chkreg > 4)
                                                flg2 = false;
                                        }
                                    }
                                    else if ((lblSpecialSiD.Text == "NULL") && (fm.sessionid(lblAdmissionSession.Text) <= 121))
                                    {
                                        if (reg >= 3 && chkreg < 3)
                                            flg = false;
                                        if (chkex > 0)
                                            flg2 = false;
                                        if (chkreg > 4)
                                            flg2 = false;
                                    }
                                    else if ((lblSpecialSiD.Text != "NULL") && (fm.sessionid(lblAdmissionSession.Text) <= 121))
                                    {
                                        if (reg >= 3 && chkreg < 3)
                                            flg = false;
                                        if (reg + ex >= 3 && chkreg + chkex < 3)
                                            flg = false;
                                        if (chkreg + chkex > 6)
                                            flg2 = false;
                                    }
                                }
                                else if (lblpart.Text == "SectionB")
                                {
                                    if (8 - ex + chkex > 5)
                                        flg2 = false;
                                    if (((reg + 5 - (8 - ex)) >= 3) && (chkreg + chkex < 3))
                                        flg = false;
                                }
                                
                            }
                            else if (lblCourseHidden.Text.ToString() == "Civil")
                            {
                                int i = 0, reg = 0, ex = 0, chkex = 0, chkreg = 0;
                                while (i < GridCivil.Rows.Count)
                                {
                                    if (GridCivil.Rows[i].Cells[0].Visible == true)
                                    {
                                        CheckBox chkbx = (CheckBox)GridCivil.Rows[i].FindControl("chkAppSubject");
                                        if (GridCivil.Rows[i].Cells[3].Text == "Regular")
                                            reg = reg + 1;
                                        if (GridCivil.Rows[i].Cells[3].Text == "Extra")
                                            ex = ex + 1;
                                        if (chkbx.Checked == true)
                                        {
                                            if (GridCivil.Rows[i].Cells[3].Text == "Regular")
                                                chkreg = chkreg + 1;
                                            if (GridCivil.Rows[i].Cells[3].Text == "Extra")
                                                chkex = chkex + 1;
                                        }
                                    }
                                    i++;
                                }
                                if (((lblpart.Text == "PartI") || (lblpart.Text == "SectionA")) && (reg >= 3 && chkreg < 3))
                                {
                                    flg = false;
                                }
                                else if (lblpart.Text == "PartII")
                                {
                                    if (fm.sessionid(lblAdmissionSession.Text) > 121)
                                    {
                                        if (lblAdmissionType.Text == "Direct")
                                        {
                                            if (reg >= 3 && chkreg < 3)
                                                flg = false;
                                            if (reg + ex >= 3 && chkreg + chkex < 3)
                                                flg = false;
                                            if (chkreg + chkex > 6)
                                                flg2 = false;
                                        }
                                        else
                                        {
                                            if (reg >= 3 && chkreg < 3)
                                                flg = false;
                                            if (chkex > 0)
                                                flg2 = false;
                                            if (chkreg > 4)
                                                flg2 = false;
                                        }
                                    }
                                    else if ((lblSpecialSiD.Text == "NULL") && (fm.sessionid(lblAdmissionSession.Text) <= 121))
                                    {
                                        if (reg >= 3 && chkreg < 3)
                                            flg = false;
                                        if (chkex > 0)
                                            flg2 = false;
                                        if (chkreg > 4)
                                            flg2 = false;
                                    }
                                    else if ((lblSpecialSiD.Text != "NULL") && (fm.sessionid(lblAdmissionSession.Text) <= 121))
                                    {
                                        if (reg >= 3 && chkreg < 3)
                                            flg = false;
                                        if (reg + ex >= 3 && chkreg + chkex < 3)
                                            flg = false;
                                        if (chkreg + chkex > 6)
                                            flg2 = false;
                                    }
                                }
                                else if (lblpart.Text == "SectionB")
                                {
                                    if (10 - ex + chkex > 5)
                                        flg2 = false;
                                    if (((reg + 5 - (10 - ex)) >= 3) && (chkreg + chkex < 3))
                                        flg = false;
                                }
                               
                            }
                            if (flg == true && flg2 == true)
                            {
                               string status=chk(lblEnrolment.Text);
                               string qry = "";
                               if (status == "Hold")
                                   qry = "Update ExamForms set SID=@SID,Status=@Status,ExamSeason=@ExamSeason,IMID=@IMID,Course=@Course,Part=@Part,CenterCode=@CenterCode,RollNo=@RollNo,City=@City,City2=@City2,Remarks=@Remarks where SID='" + lblEnrolment.Text.ToString() + "' and ExamSeason='"+lblExamSeasonHidden.Text.ToString()+"'";
                               else
                                   qry = "insert into ExamForms(SID,Status,ExamSeason,IMID,Course,Part,CenterCode,RollNo,City,City2,Remarks) values(@SID,@Status,@ExamSeason,@IMID,@Course,@Part,@CenterCode,@RollNo,@City,@City2,@Remarks)";
                                SqlCommand dmc = new SqlCommand(qry, con);
                                dmc.Parameters.AddWithValue("@SID", lblEnrolment.Text.ToString());
                                dmc.Parameters.AddWithValue("@Status", "Submitted");
                                dmc.Parameters.AddWithValue("@ExamSeason", ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString());
                                dmc.Parameters.AddWithValue("@IMID", lblIMID.Text.ToString());
                                dmc.Parameters.AddWithValue("@Course", lblCourseHidden.Text.ToString());
                                dmc.Parameters.AddWithValue("@Part", lblPartHidden.Text.ToString());
                                dmc.Parameters.AddWithValue("@CenterCode", txtExamID.Text.ToString());
                                dmc.Parameters.AddWithValue("@RollNo", "N/A");
                                dmc.Parameters.AddWithValue("@City", ddlExamCity.SelectedValue.ToString());
                                if (ddlExamCity2.SelectedValue.ToString() == "---Select---") ddlExamCity2.SelectedItem.Text = "";
                                dmc.Parameters.AddWithValue("@City2", ddlExamCity2.SelectedItem.Text.ToString());
                                dmc.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                                dmc.ExecuteNonQuery();

                                if (lblCourseHidden.Text.ToString() == "Civil")
                                {
                                    int i = 0;
                                    cmd = new SqlCommand("select max(SN) from ExamForms where SID='" + lblEnrolment.Text.ToString() + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and Part='" + lblPartHidden.Text.ToString() + "'", con);
                                    maxsn = Convert.ToInt32(cmd.ExecuteScalar());
                                    while (i < GridCivil.Rows.Count)
                                    {
                                        CheckBox rbApp = (CheckBox)GridCivil.Rows[i].FindControl("chkAppSubject");
                                        if (rbApp.Checked)
                                        {
                                            insertdata(GridCivil.Rows[i].Cells[0].Text.ToString(), GridCivil.Rows[i].Cells[2].Text.ToString(), GridCivil.Rows[i].Cells[3].Text.ToString(), maxsn);
                                        }
                                        i++;
                                    }
                                }
                                else
                                {
                                    int i = 0;
                                    cmd = new SqlCommand("select max(SN) from ExamForms where SID='" + lblEnrolment.Text.ToString() + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and Part='" + lblPartHidden.Text.ToString() + "'", con);
                                    maxsn = Convert.ToInt32(cmd.ExecuteScalar());
                                    while (i < GridArchi.Rows.Count)
                                    {
                                        CheckBox rbApp = (CheckBox)GridArchi.Rows[i].FindControl("chkAppSubjectA");
                                        if (rbApp.Checked)
                                        {
                                            insertdata(GridArchi.Rows[i].Cells[0].Text.ToString(), GridArchi.Rows[i].Cells[2].Text.ToString(), GridArchi.Rows[i].Cells[3].Text.ToString(), maxsn);
                                        }
                                        i++;
                                    }
                                }
                                cmd = new SqlCommand("select Status from AppRecord where Enrolment='" + lblEnrolment.Text.ToString() + "' and FormType like '%Exam%'", con);
                                string sts = Convert.ToString(cmd.ExecuteScalar());
                                cmd = new SqlCommand("update AppRecord set Status=@Status where AppNo='" + Convert.ToInt32(txtSerialNo.Text) + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                                cmd.Parameters.AddWithValue("@Status", sts + " Exam");
                                cmd.ExecuteNonQuery();
                                if (lblPartHidden.Text == "PartII")
                                {
                                    cmd = new SqlCommand("update ExamCurrent set CourseStatus=@CourseStatus where SId='" + lblEnrolment.Text.ToString() + "'", con);
                                    cmd.Parameters.AddWithValue("@CourseStatus", "Filled");
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd = new SqlCommand("update ExamCurrent set ExamStatus=@ExamStatus where SId='" + lblEnrolment.Text.ToString() + "'", con);
                                    cmd.Parameters.AddWithValue("@ExamStatus", "Filled");
                                    cmd.ExecuteNonQuery();
                                }
                               
                                GridCivil.Visible = false; GridArchi.Visible = false;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Exam Form Sucessfully Submitted')", true);
                               
                                con.Dispose();
                                
                                Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
                               
                            }
                            else
                            {
                                if (flg == false)
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Select Minimum Three Subjects')", true);
                                else if (flg2 == false)
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select Required Subjects')", true);
                            }
                        }
                        con.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Form Already Submitted')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select City')", true);
                    ddlExamCity.Focus();
                }
            }
        catch (ArgumentOutOfRangeException ex)
        {
            lblException.Text = ex.ToString();
        }
        catch (Exception ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {

           
        }
    
    }
    private void insertdata(string code, string name, string stype, int sn)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.FullDateTimePattern = "dd/MM/yyyy";
        SqlCommand cmd = new SqlCommand();
        try
        {
            DataTable dt = (DataTable)ViewState["datesheet"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow rw = dt.Rows[i];
                if (rw["SCode"].ToString() == code)
                {
                    cmd = new SqlCommand("insert into ExamForm(SID,SubID,SubName,Status,Date,Time,Shift,SubType,SN) values(@SID,@SubID,@SubName,@Status,@Date,@Time,@shift,@SubType,@SN)", con);
                    cmd.Parameters.AddWithValue("@SID", lblEnrolment.Text.ToString());
                    cmd.Parameters.AddWithValue("@SubID", code.ToString());
                    cmd.Parameters.AddWithValue("@SubName", name.ToString());
                    string stat = status(code);
                    if (stat == "EX")
                    {
                        cmd.Parameters.AddWithValue("@Status", "EX");
                    }
                    else if (stat == "Regular")
                    {
                        cmd.Parameters.AddWithValue("@Status", "Regular");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Status", "Regular");
                    }
                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(rw["Date"].ToString(), dtinfo));
                    cmd.Parameters.AddWithValue("@Time", rw["Time"].ToString());
                    cmd.Parameters.AddWithValue("@Shift", rw["Shift"].ToString());
                    cmd.Parameters.AddWithValue("@SubType", stype.ToString());
                    cmd.Parameters.AddWithValue("@SN", sn);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (SqlException ex)
        {
        }
        finally
        {
        }
    }
    string sts;
    private string status(string code)
    {
        if (GridArchi.Rows.Count > GridView4.Rows.Count)
        {
            foreach (GridViewRow rw in GridArchi.Rows)
            {
                foreach (GridViewRow rw2 in GridView4.Rows)
                {
                    if (rw2.Cells[0].Text.Equals(rw.Cells[0].Text))
                    {
                        if (code.ToString() == rw2.Cells[0].Text.ToString())
                        {
                            sts = "EX";
                        }
                        else
                        {
                            sts = "Regular";
                        }
                    }
                }
            }
        }
        else if (GridView4.Rows.Count > GridArchi.Rows.Count)
        {
            foreach (GridViewRow rw in GridView4.Rows)
            {
                foreach (GridViewRow rw2 in GridArchi.Rows)
                {
                    if (rw.Cells[0].Text.Equals(rw2.Cells[0].Text))
                    {
                        if (code.ToString() == rw.Cells[0].Text.ToString())
                        {
                            sts = "EX";
                        }
                        else
                        {
                            sts = "Regular";
                        }
                    }
                }
            }
            //GridArchi.DataBind();
        }
        if (GridCivil.Rows.Count > GridView4.Rows.Count)
        {
            foreach (GridViewRow rw in GridCivil.Rows)
            {
                foreach (GridViewRow rw2 in GridView4.Rows)
                {
                    if (rw2.Cells[0].Text.Equals(rw.Cells[0].Text))
                    {
                        if (code.ToString() == rw2.Cells[0].Text.ToString())
                        {
                            sts = "EX";
                        }
                        else
                        {
                            sts = "Regular";
                        }
                    }
                }
            }
        }
        else if (GridView4.Rows.Count > GridCivil.Rows.Count)
        {
            foreach (GridViewRow rw in GridView4.Rows)
            {
                foreach (GridViewRow rw2 in GridCivil.Rows)
                {
                    if (rw.Cells[0].Text.Equals(rw2.Cells[0].Text))
                    {
                        if (code.ToString() == rw.Cells[0].Text.ToString())
                        {
                            sts = "EX";
                        }
                        else
                        {
                            sts = "Regular";
                        }
                    }
                }
            }
            //GridArchi.DataBind();
        }
        return sts;
    }
    protected void chkChkAppSubjectA_CheckChanged(object sender, EventArgs e)
    {
        if (ddlExamCity.SelectedItem.Text != "---Select---")
        {
        GridViewRow rw = ((CheckBox)sender).NamingContainer as GridViewRow;
        CheckBox cb = (CheckBox)rw.FindControl("chkAppSubjectA");
        if (cb.Checked == true)
        {
        DataTable dt = (DataTable)ViewState["datesheet"];
        DateTime datethis = DateTime.Now;
        string shift = "N";
        for (int j = 0; j <= dt.Rows.Count - 1; j++)
        {
            DataRow dr = dt.Rows[j];
            if (dr["SCode"].ToString() == rw.Cells[0].Text.ToString())
            {
                datethis = (DateTime)dr["Date"];
                shift = dr["Shift"].ToString();
            }
        }
        int k = 0;
        while (k < GridArchi.Rows.Count)
        {
            CheckBox chkbx = (CheckBox)GridArchi.Rows[k].FindControl("chkAppSubjectA");
            if (chkbx.Checked == true)
            {
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    DataRow dr = dt.Rows[j];
                    if ((dr["SCode"].ToString() != rw.Cells[0].Text.ToString()))
                    {
                        if ((GridArchi.Rows[k].Cells[0].Visible == true) && (GridArchi.Rows[k].Cells[0].Text == dr["SCode"].ToString()))
                        {
                            if ((DateTime)dr["Date"] == datethis && shift == dr["Shift"].ToString())
                            {
                                cb.Checked = false;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Same Date and Session')", true);
                            }
                        }
                    }
                }
            }
            k++;
        }
        int i = 0, reg = 0,chkreg=0, ex = 0,chkex=0;
        while (i < GridArchi.Rows.Count)
        {
            if (GridArchi.Rows[i].Cells[0].Visible == true)
            {
                CheckBox chkbx=(CheckBox)GridArchi.Rows[i].FindControl("chkAppSubjectA");
                if (GridArchi.Rows[i].Cells[3].Text == "Regular")
                {
                    reg = reg + 1;
                    if (chkbx.Checked == true)
                        chkreg = chkreg + 1;
                }
                else
                {
                    ex = ex + 1;
                    if (chkbx.Checked == true)
                        chkex = chkex + 1;
                }
            }
            i++;
           }
            if (chkreg > 4)
            {
                cb.Checked = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Subject Can not more then four')", true);
            }
            else if (chkreg + chkex > 4)
            {
                if (lblPartHidden.Text != "PartII")
                {
                    cb.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Subject Can not more then four')", true);
                }
            }
            else if(lblPartHidden.Text=="SectionB")
            {
                if (8 - ex + chkex > 5)
                {
                    cb.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Optional Subject Can not more then five')", true);
                }
            }
            if (lblPartHidden.Text.ToString() == "SectionB")
            {
                if (ex <= 3)
                {
                    if (rw.Cells[3].Text == "Extra")
                    {
                        cb.Checked = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('All Optional Subject Passed')", true);
                    }
                }
            }
        }
        cb.Focus();
         }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select City')", true);
            ddlExamCity.Focus();
        }
    }
    protected void chkAppSubjectC_CheckChanged(object sender, EventArgs e)
    {
        if (ddlExamCity.SelectedItem.Text != "---Select---")
        {
            GridViewRow rw = ((CheckBox)sender).NamingContainer as GridViewRow;
            CheckBox cb = (CheckBox)rw.FindControl("chkAppSubject");
            if (cb.Checked == true)
            {
                 DataTable dt = (DataTable)ViewState["datesheet"];
                DateTime datethis = DateTime.Now;
                string shift = "N";
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    DataRow dr = dt.Rows[j];
                    if (dr["SCode"].ToString() == rw.Cells[0].Text.ToString())
                    {
                        datethis = (DateTime)dr["Date"];
                        shift = dr["Shift"].ToString();
                    }
                }
                int k = 0;
                while (k < GridCivil.Rows.Count)
                {
                    CheckBox chkbx = (CheckBox)GridCivil.Rows[k].FindControl("chkAppSubject");
                    if (chkbx.Checked == true)
                    {
                        for (int j = 0; j <= dt.Rows.Count - 1; j++)
                        {
                            DataRow dr = dt.Rows[j];
                            if ((dr["SCode"].ToString() != rw.Cells[0].Text.ToString()))
                            {
                                if ((GridCivil.Rows[k].Cells[0].Visible == true) && (GridCivil.Rows[k].Cells[0].Text == dr["SCode"].ToString()))
                                {
                                    if ((DateTime)dr["Date"] == datethis && shift == dr["Shift"].ToString())
                                    {
                                        cb.Checked = false;
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Same Date and Session')", true);
                                    }
                                }

                            }
                        }
                    }
                    k++;
                }
                int i = 0, reg = 0, chkreg = 0, ex = 0, chkex = 0;
                while (i < GridCivil.Rows.Count)
                {
                    if (GridCivil.Rows[i].Cells[0].Visible == true)
                    {
                        CheckBox chkbx = (CheckBox)GridCivil.Rows[i].FindControl("chkAppSubject");
                        if (GridCivil.Rows[i].Cells[3].Text == "Regular")
                        {
                            reg = reg + 1;
                            if (chkbx.Checked == true)
                                chkreg = chkreg + 1;
                        }
                        else
                        {
                            ex = ex + 1;
                            if (chkbx.Checked == true)
                                chkex = chkex + 1;
                        }
                    }
                    i++;
                }
                if (chkreg > 4)
                {
                    cb.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Subject Can not more then four')", true);
                }
                else if (chkreg + chkex > 4)
                {
                    if (lblPartHidden.Text != "PartII")
                    {
                        cb.Checked = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Subject Can not more then four')", true);
                    }
                }
                else if (lblPartHidden.Text == "SectionB")
                {
                    if (10 - ex + chkex > 5)
                    {
                        cb.Checked = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Optional Subject Can not more then five')", true);
                    }
                }
                if (lblPartHidden.Text.ToString() == "SectionB")
                {
                    if (ex <= 5)
                    {
                        if (rw.Cells[3].Text == "Extra")
                        {
                            cb.Checked = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('All Optional Subject Passed')", true);
                        }
                    }
                }
            }
            cb.Focus();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select City')", true);
            ddlExamCity.Focus();
        }
    }
    private void datesheet(string course,string part,string type,string session)
    {
      SqlDataAdapter ad= new SqlDataAdapter("select * from ExamDate where Course='" + course + "' and Part='" + part + "' and Type='" + type + "' and Season='" + session + "'", con);
      DataTable dt = new DataTable();
      ad.Fill(dt);
      ViewState["datesheet"] = dt;
    }
    private void finalpass()
    {
        int i = 0, reg = 0, ex = 0;
        subjects = clExamForm.ComplSubjects(lblEnrolment.Text.ToString());
        lblSpecialSiD.Text = subjects[0];
        if (lblCourseHidden.Text == "Civil")
        {
            while (i < GridCivil.Rows.Count)
            {
                if (GridCivil.Rows[i].Cells[0].Visible == true)
                {
                    if (GridCivil.Rows[i].Cells[3].Text == "Regular")
                        reg = reg + 1;
                    else ex = ex + 1;
                }
                i++;
            }
        }
        else
        {
            i = 0;
            while (i < GridArchi.Rows.Count)
            {
                if (GridArchi.Rows[i].Cells[0].Visible == true)
                {
                    if (GridArchi.Rows[i].Cells[3].Text == "Regular")
                        reg = reg + 1;
                    else ex = ex + 1;
                }
                i++;
            }
        }
        bool bl = false;
        if (lblPartHidden.Text == "PartI" || lblPartHidden.Text=="SectionA" || lblPartHidden.Text=="PartII")
        {
            if (reg <= 4)
               bl= true;
        }
        else 
        {
            if (lblCourseHidden.Text == "Civil")
            {
                if (reg <= 4 && ex <= 5)
                    bl = true;
                if (reg <= 3 && ex <= 6)
                    bl = true;
                if (reg <= 2 && ex <= 7)
                    bl = true;
                if (reg <= 1 && ex <= 8)
                    bl = true;
                if (reg == 0 && ex <= 9)
                    bl = true;
            }
            else
            {
                if (reg <= 4 && ex <= 3)
                    bl = true;
                if (reg <= 3 && ex <= 4)
                    bl = true;
                if (reg <= 2 && ex <= 5)
                    bl = true;
                if (reg <= 1 && ex <= 6)
                    bl = true;
                if (reg == 0 && ex <= 7)
                    bl = true;
            }
        }
        if (bl == true)
        {
            lblFianlPass.Text = "Final Pass";
            i = 0;
            int count = 0, excnt = 0;
            if (lblCourseHidden.Text == "Civil")
            {
                while (i < GridCivil.Rows.Count)
                {
                    CheckBox rbApp = (CheckBox)GridCivil.Rows[i].FindControl("chkAppSubject");
                    if (rbApp.Visible == true)
                    {
                        count += 1;
                        if (lblPartHidden.Text == "PartII")
                        {
                            rbApp.Checked = true;
                            if (subjects[0] == "NULL")
                            {
                                if (GridCivil.Rows[i].Cells[3].Text == "Extra")
                                {
                                    rbApp.Checked = false;
                                    GridCivil.Rows[i].Visible = false;
                                }
                            }
                            else if (fm.sessionid(lblAdmissionSession.Text) > 121)
                            {
                                if (lblAdmissionType.Text == "Regular")
                                {
                                    if (GridCivil.Rows[i].Cells[3].Text == "Extra")
                                        rbApp.Checked = false;
                                    GridCivil.Rows[i].Visible = false;
                                }
                            }
                        }
                        else if (lblPartHidden.Text == "SectionB")
                        {
                            if (GridCivil.Rows[i].Cells[3].Text == "Regular")
                            {
                                if (count <= 4)
                                    rbApp.Checked = true;
                                if (5 - reg + count > 5)
                                    rbApp.Checked = false;
                            }
                            else
                            {
                                excnt += 1;
                                if (10 - ex + excnt > 5)
                                    rbApp.Checked = false;
                            }
                        }
                        else if (count <= 4)
                            rbApp.Checked = true;
                    }
                    i++;
                }
            }
            else
            {
                while (i < GridArchi.Rows.Count)
                {
                    CheckBox rbApp = (CheckBox)GridArchi.Rows[i].FindControl("chkAppSubjectA");
                    if (rbApp.Visible == true)
                    {
                        count += 1;
                        if (lblPartHidden.Text == "PartII")
                        {
                            rbApp.Checked = true;
                            if (subjects[0] == "NULL")
                            {
                                if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                                {
                                    rbApp.Checked = false;
                                    GridArchi.Rows[i].Visible = false;
                                }
                            }
                            else if (fm.sessionid(lblAdmissionSession.Text) > 121)
                            {
                                if (lblAdmissionType.Text == "Regular")
                                {
                                    if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                                    {
                                        rbApp.Checked = false;
                                        GridArchi.Rows[i].Visible = false;
                                    }
                                }
                            }
                        }
                        else if (lblPartHidden.Text == "SectionB")
                        {
                            if (count <= 4)
                                rbApp.Checked = true;
                            if (GridArchi.Rows[i].Cells[3].Text == "Regular")
                            {
                                if (5 - reg + count > 5)
                                    rbApp.Checked = false;
                            }
                            else
                            {
                                excnt += 1;
                                if (8 - ex + excnt > 5)
                                    rbApp.Checked = false;
                            }

                        }
                        else if (count <= 4)
                            rbApp.Checked = true;
                    }
                    i++;
                }
            }
        }
        
    }
    protected void btnselectFour_Click(object sender, EventArgs e)
    {
            int m = 0;
            if (lblCourseHidden.Text == "Civil")
            {
                while (m < GridCivil.Rows.Count)
                {
                    CheckBox rbApp = (CheckBox)GridCivil.Rows[m].FindControl("chkAppSubject");
                    if (m < 4)
                        rbApp.Checked = true;
                    else rbApp.Checked = false;
                    m++;
                }
                
            }
            else
            {
                while (m < GridArchi.Rows.Count)
                {
                    CheckBox rbApp = (CheckBox)GridArchi.Rows[m].FindControl("chkAppSubjectA");
                    if (m < 4)
                        rbApp.Checked = true;
                    else rbApp.Checked = false;
                    m++;
                }
            }
    }
    private bool varifyfinalpass()
    {
       int i = 0;
       int count = 0,ex=0, chkex = 0;
        bool bl=false;
        if (lblCourseHidden.Text == "Civil")
        {
            while (i < GridCivil.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GridCivil.Rows[i].FindControl("chkAppSubject");
                if (rbApp.Visible == true)
                {
                    count += 1;
                    if (lblPartHidden.Text == "PartI" || lblPartHidden.Text == "SectionA")
                    {
                        if (rbApp.Checked == false)
                            bl = true;
                    }
                    else if (lblPartHidden.Text == "PartII")
                    {
                        if (GridCivil.Rows[i].Cells[3].Text=="Regular")
                        {
                            if (rbApp.Checked == false)
                                bl = true;
                        }
                        if (lblSpecialSiD.Text != "NULL")
                        {
                                if (rbApp.Checked == false)
                                    bl = true;
                        }
                        else if (fm.sessionid(lblAdmissionSession.Text) > 121)
                        {
                            if (lblAdmissionType.Text == "Direct")
                            {
                                    if (rbApp.Checked == false)
                                        bl = true;
                            }
                            else
                            {
                                if (GridCivil.Rows[i].Cells[3].Text == "Extra")
                                {
                                    if (rbApp.Checked == true)
                                        bl = true;
                                }
                                else
                                {
                                    if (rbApp.Checked == false)
                                        bl = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GridCivil.Rows[i].Cells[3].Text == "Regular")
                        {
                            if (rbApp.Checked == false)
                                bl = true;
                        }
                        if (GridCivil.Rows[i].Cells[3].Text == "Extra")
                        {
                            
                            ex = ex + 1;
                            if (rbApp.Checked == true)
                                chkex = chkex + 1;
                        }
                    }
                }
                i++;
            }
            if (lblPartHidden.Text == "SectionB")
            {
                if (10 - ex + chkex < 5)
                    bl = true;
            }
        }
        else
        {
            while (i < GridArchi.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GridArchi.Rows[i].FindControl("chkAppSubjectA");
                if (rbApp.Visible == true)
                {
                    count += 1;
                    if (lblPartHidden.Text == "PartI" || lblPartHidden.Text == "SectionA")
                    {
                        if (rbApp.Checked == false)
                            bl = true;
                    }
                    else if (lblPartHidden.Text == "PartII")
                    {
                        if (GridArchi.Rows[i].Cells[3].Text == "Regular")
                        {
                            if (rbApp.Checked == false)
                                bl = true;
                        }
                        if (lblSpecialSiD.Text != "NULL")
                        {
                            if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                            {
                                if (rbApp.Checked == false)
                                    bl = true;
                            }
                        }
                        if (fm.sessionid(lblAdmissionSession.Text.ToString()) > 121)
                        {
                            if (lblAdmissionType.Text == "Direct")
                            {
                                if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                                {
                                    if (rbApp.Checked == false)
                                        bl = true;
                                }
                                else
                                {
                                    if (rbApp.Checked == false)
                                        bl = true;
                                }
                            }
                            else
                            {
                                if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                                {
                                    if (rbApp.Checked == true)
                                        bl = true;
                                }
                                else
                                {
                                    if (rbApp.Checked == false)
                                        bl = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GridArchi.Rows[i].Cells[3].Text == "Regular")
                        {
                            if (rbApp.Checked == false)
                                bl = true;
                        }
                        if (GridArchi.Rows[i].Cells[3].Text == "Extra")
                        {
                           
                            ex = ex + 1;
                            if (rbApp.Checked == true)
                                chkex = chkex + 1;
                        }
                    }
                }
                i++;
            }
            if (lblPartHidden.Text == "SectionB")
            {
                if (8 - ex + chkex < 5)
                    bl = true;
            }
        }
        return bl;
    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        if (chk(lblEnrolment.Text) == "Hold")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('This Exam Form already on hold')", true);
         }
        else if (chk(lblEnrolment.Text) == "Regular")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('ExamForm Filled')", true);
        }
        else if (chk(lblEnrolment.Text) == "")
        {
            SqlCommand dmc = new SqlCommand("insert into ExamForms(SID,Status,ExamSeason,IMID,Course,Part,CenterCode,RollNo,City,City2,Remarks) values(@SID,@Status,@ExamSeason,@IMID,@Course,@Part,@CenterCode,@RollNo,@City,@City2,@Remarks)", con);
            dmc.Parameters.AddWithValue("@SID", lblEnrolment.Text.ToString());
            dmc.Parameters.AddWithValue("@Status", "Hold");
            dmc.Parameters.AddWithValue("@ExamSeason", ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString());
            dmc.Parameters.AddWithValue("@IMID", lblIMID.Text.ToString());
            dmc.Parameters.AddWithValue("@Course", lblCourseHidden.Text.ToString());
            dmc.Parameters.AddWithValue("@Part", lblPartHidden.Text.ToString());
            dmc.Parameters.AddWithValue("@CenterCode", "N/A");
            dmc.Parameters.AddWithValue("@RollNo", "N/A");
            dmc.Parameters.AddWithValue("@City", ddlExamCity.SelectedValue.ToString());
            if (ddlExamCity2.SelectedValue.ToString() == "---Select---") ddlExamCity2.SelectedItem.Text = "";
            dmc.Parameters.AddWithValue("@City2", ddlExamCity2.SelectedItem.Text.ToString());
            dmc.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
            dmc.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully on Hold.')", true);
        }
        con.Close(); con.Dispose();
    }
    private string chk(string enrol)
    {
        SqlCommand cmd = new SqlCommand("select Status from ExamForms where SID='" + enrol + "' and ExamSeason='" + lblExamSeasonHidden.Text + "' and Part='" + lblPartHidden.Text + "'", con);
        string status = Convert.ToString(cmd.ExecuteScalar());
        return status;
    }
    protected void btnSaveForm_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        
        cmd = new SqlCommand("select SN from ExamForms where SID='" + lblEnrolment.Text + "' and Part='" + lblPartHidden.Text + "' and ExamSeason='" + lblExamSeasonHidden.Text + "'", con);
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
            dmc.Parameters.AddWithValue("@Course", lblCourseHidden.Text.ToString());
            dmc.Parameters.AddWithValue("@Part", lblPartHidden.Text.ToString());
            dmc.Parameters.AddWithValue("@CenterCode", "N/A");
            dmc.Parameters.AddWithValue("@RollNo", "N/A");
            dmc.Parameters.AddWithValue("@City", ddlExamCity.SelectedValue.ToString());
            if (ddlExamCity2.SelectedValue.ToString() == "---Select---") ddlExamCity2.SelectedItem.Text = "";
            dmc.Parameters.AddWithValue("@City2", ddlExamCity2.SelectedItem.Text.ToString());
            dmc.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
            dmc.ExecuteNonQuery();

            if (lblCourseHidden.Text.ToString() == "Civil")
            {
                int i = 0;
                cmd = new SqlCommand("select max(SN) from ExamForms where SID='" + lblEnrolment.Text.ToString() + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and Part='" + lblPartHidden.Text.ToString() + "'", con);
                maxsn = Convert.ToInt32(cmd.ExecuteScalar());
                while (i < GridCivil.Rows.Count)
                {
                    CheckBox rbApp = (CheckBox)GridCivil.Rows[i].FindControl("chkAppSubject");
                    if (rbApp.Checked)
                    {
                        insertdata(GridCivil.Rows[i].Cells[0].Text.ToString(), GridCivil.Rows[i].Cells[2].Text.ToString(), GridCivil.Rows[i].Cells[3].Text.ToString(), maxsn);
                    }
                    i++;
                }
            }
            else
            {
                int i = 0;
                cmd = new SqlCommand("select max(SN) from ExamForms where SID='" + lblEnrolment.Text.ToString() + "' and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and Part='" + lblPartHidden.Text.ToString() + "'", con);
                maxsn = Convert.ToInt32(cmd.ExecuteScalar());
                while (i < GridArchi.Rows.Count)
                {
                    CheckBox rbApp = (CheckBox)GridArchi.Rows[i].FindControl("chkAppSubjectA");
                    if (rbApp.Checked)
                    {
                        insertdata(GridArchi.Rows[i].Cells[0].Text.ToString(), GridArchi.Rows[i].Cells[2].Text.ToString(), GridArchi.Rows[i].Cells[3].Text.ToString(), maxsn);
                    }
                    i++;
                }
            }
            cmd = new SqlCommand("select Status from AppRecord where Enrolment='" + lblEnrolment.Text.ToString() + "' and FormType like '%Exam%'", con);
            string sts = Convert.ToString(cmd.ExecuteScalar());
            cmd = new SqlCommand("update AppRecord set Status=@Status where AppNo='" + Convert.ToInt32(txtSerialNo.Text) + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Status", sts + " Exam");
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update ExamCurrent set ExamStatus=@ExamStatus where SId='" + lblEnrolment.Text.ToString() + "'", con);
            if (lblPartHidden.Text == "PartII")
            {
                if (lblCourseStatus.Text == "Promotted" | lblCourseStatus.Text == "Submitted")
                    cmd.Parameters.AddWithValue("@ExamStatus", lblexamstatus.Text);
                else cmd.Parameters.AddWithValue("@ExamStatus", "Filled");
            }
            else cmd.Parameters.AddWithValue("@ExamStatus", "Filled");
            cmd.ExecuteNonQuery();


            GridCivil.Visible = false; GridArchi.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Exam Form Sucessfully Submitted')", true);
            con.Close(); con.Dispose();
            //Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
        }
        else 
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Exam Form Already Submitted')", true);
       
    }


    protected void lbtnUpdateExamForm_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("UpdateExamForm.aspx?sid=" + txticesn.Text + "&lnk=null&typ=Ex");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamFormImprovement.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }




}
