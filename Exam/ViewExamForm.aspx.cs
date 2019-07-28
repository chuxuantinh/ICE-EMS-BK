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
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Globalization;
public partial class Exam_ViewExamForm : System.Web.UI.Page
{

    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                ClsEdit cledit = new ClsEdit();
                lblToExamDiary.Text = cledit.ExamToDiary(lblExamSeasonHidden.Text.ToString());
                lblExamFormSub.Text = cledit.ExamFormSubmitted(lblExamSeasonHidden.Text.ToString());
                lblExamFormApproved.Text = cledit.ExamFormApproved(lblExamSeasonHidden.Text.ToString());
                lblExamFormFilled.Text = cledit.ExamFormFilled(lblExamSeasonHidden.Text.ToString());
                lblExamHold.Text = cledit.ExamFormHold(lblExamSeasonHidden.Text.ToString());
                lblExamFormRollNo.Text = cledit.ExamFormRollNO(lblExamSeasonHidden.Text.ToString());
                lblExamFormAdmitCard.Text = cledit.ExamFormAdmitCard(lblExamSeasonHidden.Text.ToString());
                btnViewMembership.Visible = false;
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
    protected void btnViewMembershipNo_Onclick(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select ExamForm.SubID,ExamForm.SubName,ExamForms.Status,ExamForms.City, ExamForms.RollNo from ExamForm inner join ExamForms on ExamForm.SN=ExamForms.SN Where ExamForms.SID='" + lblEnrolment.Text.ToString() + "' and ExamForms.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridExamForms.DataSource = ds;
        GridExamForms.DataBind();
        btnSubmit.Focus();
    }
    protected void txtSerialNo_TextChanged(object sender, EventArgs e)
    {
        ok();
    }
    private void ok()
    {
        try
        {
            con.Close(); con.Open();
            int i = 0;
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select SID from ExamForms where SID='" + txticesn.Text.ToString() + "' and  ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
            string apno = "", Part = "";
            apno = Convert.ToString(cmd.ExecuteScalar());
            if (apno != "")
            {
                lblEnrolment.Text = apno.ToString();
                apno = "1";
                btnSubmit.Text = "Submit For Re-Submission";
                btnSubmit.Visible = true; btnViewMembership.Visible = true;
            }
            else
            {
                string sn = txticesn.Text.ToString();
                cmd = new SqlCommand("select Enrolment,Part from AppRecord where Exam= 'E" + sn.ToString() + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    apno = reader["Enrolment"].ToString();
                    Part = reader["Part"].ToString();
                }
                reader.Close();
                if (apno != "")
                {
                    lblEnrolment.Text = apno.ToString();
                    apno = "2";
                    btnSubmit.Visible = true; btnViewMembership.Visible = true;
                    cmd = new SqlCommand("select SN from ExamForms where SID='" + lblEnrolment.Text.ToString() + "' and Part='" + Part.ToString() + "' and  ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                    string chk = Convert.ToString(cmd.ExecuteScalar());
                    if (chk != "")
                    {
                        btnSubmit.Text = "Send For Re-Submission";
                        lblSN.Text = chk.ToString();
                    }
                    else
                    {
                        btnSubmit.Text = "Exam Form Not Found.";
                    }
                }
                else
                {
                    lblExceptionOK.Text = "Record Not Found !";
                    btnViewMembership.Visible = false;
                    lblName.Text = ""; lblCourse.Text = "";
                    lblIMID.Text = ""; btnSubmit.Visible = false;
                }
                if (apno == "2")
                {
                    lblExceptionOK.Text = "";
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
                        if (lblEnrolment.Text == sdr["AppNo"].ToString())
                        {
                            lblTempEnrol.Text = "[TEMP]";
                        }
                        else
                        {
                            lblTempEnrol.Text = "";
                        }
                        lblExceptionOK.Text = "";
                    } sdr.Close();
                    sdr.Dispose();
                }
            }
            if (apno == "1")
            {
                cmd = new SqlCommand("select * from ExamForms where SID='" + txticesn.Text.ToString() + "' and Part='" + lblPart.Text.ToString() + "' and  ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                SqlDataReader reader;
                lblExceptionOK.Text = "";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // lblIMID.Text = reader["IMID"].ToString();
                    lblName.Text = "";
                    // lblName.Text = reader["Name"].ToString() + " s/o " + reader["FName"].ToString();
                    lblCourse.Text = reader["Course"].ToString() + "- " + reader["Part"].ToString();
                    lblPart.Text = reader["Part"].ToString();
                    lblIMID.Text = "IMID: " + reader["IMID"].ToString();
                    lblEnrolment.Text = reader["SID"].ToString();
                    lblSN.Text = reader["SN"].ToString();
                }
                reader.Close();
                reader.Dispose();
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
        }
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txticesn.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    DataTable dm;
    private DataTable GetDataSource()
    {
        SqlDataAdapter ad;
        string qry = ""; 
        if (txtIMID.Text != "")
        {
            if (rbtnGenerated.Checked == true)
            {
                qry = "select ExamForm.SubID,ExamForm.SubName,ExamForms.Status,ExamForms.City, ExamForms.RollNo from ExamForm inner join ExamForms on ExamForm.SN=ExamForms.SN and ExamForms.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ExamForms.IMID='" + txtIMID.Text + "'";
                ad = new SqlDataAdapter(qry, con);
                DataTable ds = new DataTable();
                ad.Fill(ds);
                dm = ds;
            }
            else   if (rbtnNotGenerated.Checked==true)
            {
                qry = "select IMID,Enrolment as SID,Course,Part,Session,Status from AppRecord where Enrolment Not IN(select SID from ExamForms where ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and IMID='" + txtIMID.Text + "') and Session='" + lblExamSeasonHidden.Text.ToString() + "' and IMID='" + txtIMID.Text + "' and FeeType like'%Exam%'";
                ad = new SqlDataAdapter(qry, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dm = dt;
            }
        }
        return dm;
    }
    protected void GridExamForms_OnSelectedIndexChangd(object sender, EventArgs e)
    {
        lblEnrolment.Text = GridExamForms.SelectedRow.Cells[1].Text.ToString();
        txticesn.Text = lblEnrolment.Text.ToString();
        ok();
        btnViewMembership.Focus();
    }
    string dt;
    protected void GridExamForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (e.Row.Cells[1].Text == "SID")
                    {
                        dt = e.Row.Cells[9].Text.ToString();
                    }
                    else if (e.Row.Cells[1].Text == "IMID")
                    {
                        dt = e.Row.Cells[1].Text.ToString();
                    }

                }
                if (dt == "Date")
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[0].Visible = false;
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[0].Visible = false;
                    }
                   if (e.Row.RowType == DataControlRowType.DataRow)
                   {
                    e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToShortDateString();
                   }
                }
                if (dt == "IMID")
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[6].Text = "Exam Form Not Submitted";
                    }
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[0].Visible = false;
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[0].Visible = false;
                    }
                }
     }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    
    protected void ibtnExportPDFAppTableDoc_Click(object sender, EventArgs e)
    {
        GridExamForms.AllowPaging = false;
        GridExamForms.DataSource = GetDataSource();
        GridExamForms.Columns[0].Visible = false;
        GridExamForms.DataBind();
        if (GridExamForms.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=ExaminationFormRollNumberNotGenerated.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridExamForms.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, EventArgs e)
    {
        GridExamForms.AllowPaging = false;
        GridExamForms.DataSource = GetDataSource();
        GridExamForms.Columns[0].Visible = false;
        GridExamForms.DataBind();
        if (GridExamForms.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationFormRollNumberNotGenerated.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridExamForms.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    
   
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        GridExamForms.AllowPaging = false;
        GridExamForms.DataSource = GetDataSource();
        GridExamForms.Columns[0].Visible = false;
        GridExamForms.DataBind();
        if (GridExamForms.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationFormRollNumberNotGenerated.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridExamForms.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void rbtnGenerated_CheckedChanged(object sender, EventArgs e)
    {
        if (txtIMID.Text != "")
        {
            GridExamForms.DataSource = GetDataSource();
            GridExamForms.DataBind();
            lblMsg.Text = "";
        }
        else
            lblMsg.Text = "Enter IMID";
        if (GridExamForms.Rows.Count > 0) GridExamForms.Focus();
        else txtIMID.Focus();
    }
    protected void rbtnNotGenerated_CheckedChanged(object sender, EventArgs e)
    {
        if (txtIMID.Text != "")
        {
            GridExamForms.DataSource = GetDataSource();
            GridExamForms.DataBind();
            lblMsg.Text = "";
        }
        else
            lblMsg.Text = "Enter IMID";
        if (GridExamForms.Rows.Count > 0) GridExamForms.Focus();
        else txtIMID.Focus();
    }
}