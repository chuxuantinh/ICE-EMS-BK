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
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Acc_AppEdit : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
               
                ClsEdit clEdit = new ClsEdit();
                string[] stredit = clEdit.EditCount("ApplicationAdd");
                lblEnrolment.Text = stredit[0].ToString();
                if (stredit[1] == "False") pnlEdit.Enabled = false;
                else pnlEdit.Enabled = true;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                panelFee1.Visible = false; panelFee2.Visible = false;
                lblSeasonHidden.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
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
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter();
        if (ddlFormType.SelectedValue == "Exam")
        {
           ad = new SqlDataAdapter("select Name,FName,DOB,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,CADFees,DupForm,AppNo,Status,Course,Part,IMID,Exam,DNo from AppRecord where Session='" + lblSeasonHidden.Text.ToString() + "' and Enrolment='" + txtSID.Text.ToString() + "' and  ExamFees!=0 ORDER BY SN DESC", con);
        }
        else if (ddlFormType.SelectedValue == "Admission")
        {
            ad = new SqlDataAdapter("select Name,FName,DOB,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,CADFees,DupForm,AppNo,Status,Course,Part,IMID,Exam ,DNo from AppRecord where Session='" + lblSeasonHidden.Text.ToString() + "' and Enrolment='" + txtSID.Text.ToString() + "' and  AdmissionFees!=0 ORDER BY SN DESC", con);
        }
        else
        {
            ad = new SqlDataAdapter("select Name,FName,DOB,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,CADFees,DupForm,AppNo,Status,Course,Part,IMID,Exam ,DNo from AppRecord where Session='" + lblSeasonHidden.Text.ToString() + "' and Enrolment='" + txtSID.Text.ToString() + "' and  AdmissionFees!=0 ORDER BY SN DESC", con);
        }
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridAppTable.DataSource = dt;
        GridAppTable.DataBind();
        panelFee1.Visible = false;
        panelFee2.Visible = false;
    }
    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = "Father's Name";
            e.Row.Cells[4].Text = "Serial No.";
            e.Row.Cells[7].Text = "Late Fee";
            e.Row.Cells[8].Text = "ExmpFee";
            e.Row.Cells[9].Text = "Membership";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text != "&nbsp;" || e.Row.Cells[3].Text != "")
                e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.TrimEnd('0').Trim('.');
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[10].Text = e.Row.Cells[10].Text.TrimEnd('0').Trim('.');
            e.Row.Cells[11].Text = e.Row.Cells[11].Text.TrimEnd('0').Trim('.');
            e.Row.Cells[12].Text = e.Row.Cells[12].Text.TrimEnd('0').Trim('.');
            e.Row.Cells[13].Text = e.Row.Cells[13].Text.TrimEnd('0').Trim('.');
            e.Row.Cells[14].Text = e.Row.Cells[14].Text.TrimEnd('0').Trim('.');
            e.Row.Cells[15].Text = e.Row.Cells[15].Text.TrimEnd('0').Trim('.');
            e.Row.Cells[16].Text = e.Row.Cells[16].Text.TrimEnd('0').Trim('.');
        }
    }
    protected void GridAppTable_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open(); uncheckAll();
        lblException.Text = "";
        lblID.Text = GridAppTable.SelectedRow.Cells[9].Text.ToString();
        lblName.Text = GridAppTable.SelectedRow.Cells[1].Text.ToString();
        ddlCourse.SelectedValue = GridAppTable.SelectedRow.Cells[19].Text.ToString();
        ddlPart.SelectedValue = GridAppTable.SelectedRow.Cells[20].Text.ToString();
        lblTAmount.Text = (Convert.ToInt32(GridAppTable.SelectedRow.Cells[6].Text.ToString()) + Convert.ToInt32(GridAppTable.SelectedRow.Cells[7].Text.ToString())).ToString();
        if (chkExamForms(txtSID.Text, ddlPart.SelectedValue.ToString(), lblSeasonHidden.Text) == false)
            lblException.Text = "Please First ReSubmit Exam Form.";
        con.Close(); con.Dispose();
        panelFee1.Visible = true;
        System.Drawing.Color red = System.Drawing.Color.Red;
        System.Drawing.Color black = System.Drawing.Color.Black;
        if (GridAppTable.SelectedRow.Cells[10].Text != "0")
        {
            if(GridAppTable.SelectedRow.Cells[4].Text.Contains("ReAdmission"))
            {
                chkRedmission.Checked=true;chkRedmission.ForeColor=red;
                chkAdmissinForm.Checked = false; chkAdmissinForm.ForeColor = black;
            }
            else
            {
               chkAdmissinForm.Checked = true;
               chkAdmissinForm.ForeColor = red;
               chkRedmission.Checked = false;
               chkRedmission.ForeColor = black;
            }
        }
        else { chkAdmissinForm.Checked = false; chkAdmissinForm.ForeColor = black; }
        if (GridAppTable.SelectedRow.Cells[11].Text != "0")
        {
            panelFee2.Visible = true;
            chkComposite.Checked = true; chkComposite.ForeColor = red;
        }
        else { chkComposite.Checked = false; chkComposite.ForeColor = black; }
        if (GridAppTable.SelectedRow.Cells[12].Text != "0")
        {
            panelFee2.Visible = true;
            chkASF.Checked = true; chkASF.ForeColor = red;
        }
        else { chkASF.Checked = false; chkASF.ForeColor = black; }
        if (GridAppTable.SelectedRow.Cells[13].Text != "0")
        {
            chkITI.Checked = true; chkITI.ForeColor = red;
        }
        else { chkITI.Checked = false; chkITI.ForeColor = black; }
        if (GridAppTable.SelectedRow.Cells[14].Text != "0")
        {
            chkExamForm.Checked = true; chkExamForm.ForeColor = red;
        }
        else { chkExamForm.Checked = false; chkExamForm.ForeColor = black; }
        if (GridAppTable.SelectedRow.Cells[8].Text != "0")
        {
            chkExmp.Checked = true;
            chkExmp.ForeColor = red;
            panelFee2.Visible = true;
        }
        else
        {
            chkExmp.ForeColor = black;
            chkExmp.Checked = false;
        }
        if ((GridAppTable.SelectedRow.Cells[18].Text != "NotApproved") || (GridAppTable.SelectedRow.Cells[18].Text != "Hold"))
        {
            lblAppsStatus.Text="Approved";
        }
        else
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open(); 
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        if (chkExamForm.Checked == true)  // Included Admission or ReAdmission
        {
            updateExamcurrent(txtSID.Text,GridAppTable.SelectedRow.Cells[19].Text.ToString());
        }
        if (chkAdmissinForm.Checked == true)  // Single Admission
        {
            deleteExamCurrent(txtSID.Text);
        }
        else if (chkRedmission.Checked == true)  // Single ReAdmission 
        {
            updateExamcurrent(txtSID.Text, GridAppTable.SelectedRow.Cells[19].Text.ToString());
        }
        if (chkComposite.Checked == true)
            updateCompositeStatus(txtSID.Text.ToString());
        if (chkITI.Checked == true)
            deleteITIForm();
        if (chkASF.Checked == true)
            updateASF(txtSID.Text.ToString(), GridAppTable.SelectedRow.Cells[12].Text.ToString());
            cmd = new SqlCommand("delete AppRecord where AppNo='" + GridAppTable.SelectedRow.Cells[17].Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Insert into RecoverApp(FormType,APPNo,Amount,Type,Enrolment,IMID,SerialNo,Session,Course,Part,Name,FName,DOB,Status,Remark,Date) values(@FormType,@APPNo,@Amount,@Type,@Enrolment,@IMID,@SerialNo,@Session,@Course,@Part,@Name,@FName,@DOB,@Status,@Remark,@Date)", con);
            cmd.Parameters.AddWithValue("@FormType", ddlFormType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@AppNo", GridAppTable.SelectedRow.Cells[17].Text.ToString());
            cmd.Parameters.AddWithValue("@Amount", lblTAmount.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", "Credit");
            cmd.Parameters.AddWithValue("@Enrolment",lblID.Text);
            cmd.Parameters.AddWithValue("@IMID", GridAppTable.SelectedRow.Cells[21].Text.ToString());
            cmd.Parameters.AddWithValue("@SerialNo", GridAppTable.SelectedRow.Cells[22].Text.ToString());
            cmd.Parameters.AddWithValue("@Session", lblSeasonHidden.Text);
            cmd.Parameters.AddWithValue("@Course", GridAppTable.SelectedRow.Cells[19].Text.ToString());
            cmd.Parameters.AddWithValue("@Part", GridAppTable.SelectedRow.Cells[20].Text.ToString());
            cmd.Parameters.AddWithValue("@Name", GridAppTable.SelectedRow.Cells[1].Text.ToString());
            cmd.Parameters.AddWithValue("@FName",GridAppTable.SelectedRow.Cells[2].Text.ToString());
            cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(GridAppTable.SelectedRow.Cells[3].Text.ToString(), dtinfo));
            if (lblAppsStatus.Text.ToString() != "NotApproved" && lblAppsStatus.Text.ToString() != "Hold")
            cmd.Parameters.AddWithValue("@Status", "NotApproved");
            else
            cmd.Parameters.AddWithValue("@Status", "Approved");
            cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();
            lblException.Text = "Form's Successfully submitted for Cancelation.";
            lblException.Attributes.Add("class", "success");
            con.Close(); con.Dispose();
    }
    private bool chkStudent(string sid)
    {
        cmd = new SqlCommand("select sid from Student where SID='" + sid + "'", con);
        string id = Convert.ToString(cmd.ExecuteNonQuery());
        if (id == "")
            return false;
        else return true;
    }
    private bool chkExamForms(string sid, string part, string session)
    {
        cmd = new SqlCommand("select sid from ExamForms where SID='" + sid + "' and Part='" + part + "' and ExamSeason='" + session + "'", con);
        string id = Convert.ToString(cmd.ExecuteNonQuery());
        if (id == "")
            return false;
        else return true;
    }
    private void updateExamcurrent(string sid, string Part)
    {
        string stream = "";
        if (ddlPart.SelectedValue.ToString() == "PartI" || ddlPart.SelectedValue.ToString() == "PartII")
            stream = "Tech";
        else stream = "Asso";
        if (Part == "PartII")
        {
            cmd = new SqlCommand("update ExamCurrent Set Stream=@Stream, Course=@Course,Part=@Part, CourseStatus=@CourseStatus where SID='" + sid + "'", con);
            cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CourseStatus", "NotSubmitted");
            cmd.Parameters.AddWithValue("@Stream", stream);
            cmd.ExecuteNonQuery();
        }
        else
        {
            cmd = new SqlCommand("update ExamCurrent Set  Stream=@Stream, Course=@Course,Part=@Part, ExamStatus=@ExamStatus where SID='" + sid + "'", con);
            cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@ExamStatus", "NotSubmitted");
            cmd.Parameters.AddWithValue("@Stream", stream);
            cmd.ExecuteNonQuery();
        }
    }
    private void updateCompositeStatus(string sid)
    {
        cmd = new SqlCommand("update CompositeFees set Status='NotSubmitted' where SID='" + sid + "' and SessionID in(select max(SessionID) from CompositeFees where SiD='" + sid + "') ", con);
        cmd.ExecuteNonQuery();
    }
    private void deleteExamCurrent(string sid)
    {
        cmd = new SqlCommand("delete ExamCurrent where Sid='" + sid + "'", con);
        cmd.ExecuteNonQuery();
    }
    private void deleteITIForm()
    {
        cmd = new SqlCommand("delete ITIForm where AppNo='" + GridAppTable.SelectedRow.Cells[17].Text.ToString() + "' and SID='" + txtSID.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();
    }
    private void updateASF(string sid, string amt)
    {
        cmd = new SqlCommand("select AnnualSubscription from Student where SID='" + sid + "'", con);
        string ann = Convert.ToString(cmd.ExecuteScalar());
        int a = Convert.ToInt32(amt) / 500;
        int yr = Convert.ToInt32(ann.Substring(3, 4));
        string sess = ann.Substring(0, 3);
        if (ann != "")
        {
            yr = yr - a;
            ann = sess + yr.ToString();
            cmd = new SqlCommand("update Student set AnnualSubscription='" + ann + "' where sid='" + sid + "'", con);
            cmd.ExecuteNonQuery();
        }
    }
    private void uncheckAll()
    {
        chkAdmissinForm.Checked = false; chkExamForm.Checked = false; chkRedmission.Checked = false;
        chkITI.Checked = false; chkExmp.Checked = false; chkComposite.Checked = false; chkASF.Checked = false;
    }
}