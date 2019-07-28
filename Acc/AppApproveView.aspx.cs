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

public partial class Acc_AppApproveView : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
  
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                ddlbindSearch();
                bindddlApptype();
                ddlsession.Focus();
                con.Close(); con.Dispose();
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
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        ddlsession.Focus();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlbindSearch();
        txtAll.Text = "";
        lblExceptionOK.Text = "";
        grvViewApp.DataBind();
        bindddlApptype();
        ddlSearch.Focus();
    }
    private void ddlbindSearch()
    {
        if (ddlSearch.SelectedValue.ToString() == "IMID")
        {
            lblDiaryNo.Visible = false; lblMembsID.Visible = false; lblSrNo.Visible = false; lblName.Visible = false;
            lblIMID.Text = ddlSearch.SelectedValue.ToString() + ": ";
            lblIMID.Visible = true; txtAll.Visible = true; panelselectapp.Visible = true; panelStatus.Visible = true;
        }
        else if (ddlSearch.SelectedValue.ToString() == "DiaryNo")
        {
            lblMembsID.Visible = false; lblSrNo.Visible = false; lblIMID.Visible = false; lblName.Visible = false;
            lblDiaryNo.Text = ddlSearch.SelectedValue.ToString() + ": ";
            lblDiaryNo.Visible = true; txtAll.Visible = true; panelselectapp.Visible = true; panelStatus.Visible = true;
        }
        else if (ddlSearch.SelectedValue.ToString() == "MembershipNo")
        {
            panelselectapp.Visible = false; lblSrNo.Visible = false; lblIMID.Visible = false; lblDiaryNo.Visible = false; lblName.Visible = false;
            lblMembsID.Text = ddlSearch.SelectedValue.ToString() + ": ";
            lblMembsID.Visible = true; txtAll.Visible = true; panelStatus.Visible = false; 
        }
        else if (ddlSearch.SelectedValue.ToString() == "SerialNo")
        {
            panelselectapp.Visible = true; lblIMID.Visible = false; lblDiaryNo.Visible = false; lblMembsID.Visible = false; lblName.Visible = false;
            lblSrNo.Text = ddlSearch.SelectedValue.ToString() + ": ";
            lblSrNo.Visible = true; txtAll.Visible = true; panelStatus.Visible = false;
        }
        else if (ddlSearch.SelectedValue.ToString() == "Name")
        {
            panelselectapp.Visible = false; lblIMID.Visible = false; lblDiaryNo.Visible = false; lblMembsID.Visible = false; lblSrNo.Visible = false;
            lblName.Text = ddlSearch.SelectedValue.ToString() + ": ";
            lblName.Visible = true; panelStatus.Visible = false;
            txtAll.Visible = true;
        }
        else if (ddlSearch.SelectedValue.ToString() == "All")
        {
            lblDiaryNo.Visible = false; lblMembsID.Visible = false; lblSrNo.Visible = false; lblName.Visible = false;
            lblIMID.Text = ddlSearch.SelectedValue.ToString() + ": ";
            lblIMID.Visible = false; txtAll.Visible = false; panelselectapp.Visible = true; panelStatus.Visible = true;
        }
    }
    private void fillGrv()
    {
        string strFill="";
        if (txtAll.Text != "" |ddlSearch.SelectedValue.ToString()=="All")
        {
                if (ddlSearch.SelectedValue.ToString() == "IMID" )
                {
                    strFill = "select IMID,Enrolment,Name,FName,Course,Part,DOB,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,DupForm,DNo,SubDate,Session,Status,FormType from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and FormType like '%" + ddlAppType.SelectedValue.ToString() + "%' and IMID='" + txtAll.Text.ToString() + "' and Status like '%" + ddlStatus.SelectedValue.ToString() + "%'  order by SN Desc";
                }
                else if (ddlSearch.SelectedValue.ToString() == "DiaryNo")
                {
                    strFill = "select IMID,Enrolment,Name,FName,Course,Part,DOB,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,DupForm,DNo,SubDate,Session,Status,FormType from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and FormType like '%" + ddlAppType.SelectedValue.ToString() + "%' and DNo='" + txtAll.Text.ToString() + "' and Status like '%" + ddlStatus.SelectedValue.ToString() + "%'  order by SN Desc";
                }
                else if (ddlSearch.SelectedValue.ToString() == "MembershipNo")
                {
                    strFill = "select IMID,Enrolment,Name,FName,Course,Part,DOB,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,DupForm,DNo,SubDate,Session,Status,FormType from AppRecord where Enrolment='" + txtAll.Text.ToString() + "' order by SN Desc";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Name")
                {
                    strFill = "select IMID,Enrolment,Name,FName,Course,Part,DOB,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,DupForm,DNo,SubDate,Session,Status,FormType from AppRecord where Name like '" + txtAll.Text.ToString() + "%' and Session='" + lblSessionHiddend.Text.ToString() + "'  order by SN Desc ";
                }
                else if (ddlSearch.SelectedValue.ToString() == "All")
                {
                    strFill = "select IMID,Enrolment,Name,FName,Course,Part,DOB,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,DupForm,DNo,SubDate,Session,Status,FormType from AppRecord where FormType like '%" + ddlAppType.SelectedValue.ToString() + "%' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status like '%" + ddlStatus.SelectedValue.ToString() + "%'  order by SN Desc";
                }
                else if (ddlSearch.SelectedValue.ToString() == "SerialNo")
                {
                     if (ddlAppType.SelectedValue.ToString() == "NewAdmission" | ddlAppType.SelectedValue.ToString() == "Exam" | ddlAppType.SelectedValue.ToString() == "ITI" | ddlAppType.SelectedValue.ToString() == "CAD" | ddlAppType.SelectedValue.ToString() == "Approval" | ddlAppType.SelectedValue.ToString() == "Evaluation")
                    {
                        strFill = "select IMID,Enrolment,Name,FName,Course,Part,DOB,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,DupForm,DNo,SubDate,Session,Status,FormType from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and FormType Like '%" + txtAll.Text.ToString() + ddlAppType.SelectedValue.ToString() + "%'  order by SN Desc";
                    }
                    else
                        strFill = "select IMID,Enrolment,Name,FName,Course,Part,DOB,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,DupForm,DNo,SubDate,Session,Status,FormType from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and AppNo = '" + txtAll.Text.ToString() + "'  order by SN Desc";
                }
            SqlDataAdapter adp = new SqlDataAdapter(strFill, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count < 500)
            {
                lblExceptionOK.Text = "Total No of " + ddlAppType.SelectedValue.ToString() + " Form " + dt.Rows.Count.ToString();
                grvViewApp.DataSource = dt;
                grvViewApp.DataBind();
            }
            else lblExceptionOK.Text = "Total No of " + ddlAppType.SelectedValue.ToString() + " Form " + dt.Rows.Count.ToString();
        }
        else
        {
            lblExceptionOK.Text = "Please Enter Correct Value.";
            grvViewApp.DataBind();
        }
    }
    int count = 0;
    protected void grvViewApp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "Membership";
            e.Row.Cells[6].Text = "Father's Name";
            e.Row.Cells[17].Text = "Diary No.";
            e.Row.Cells[18].Text = "Date";
            e.Row.Cells[12].Text = "ASF";
            e.Row.Cells[5].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            count = count + 1;
            if (e.Row.Cells[18].Text == "" | e.Row.Cells[18].Text == "&nbsp;" && e.Row.Cells[6].Text == "" | e.Row.Cells[6].Text == "&nbsp;")
            {
            }
            else
            {
                e.Row.Cells[18].Text = Convert.ToDateTime(e.Row.Cells[18].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[7].Text = e.Row.Cells[7].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[8].Text = e.Row.Cells[8].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[9].Text = e.Row.Cells[9].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[10].Text = e.Row.Cells[10].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[11].Text = e.Row.Cells[11].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[12].Text = e.Row.Cells[12].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[13].Text = e.Row.Cells[13].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[14].Text = e.Row.Cells[14].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[16].Text = e.Row.Cells[16].Text.TrimEnd('0').TrimEnd('.');
                e.Row.Cells[4].Text = e.Row.Cells[4].Text +" "+ e.Row.Cells[5].Text;
                e.Row.Cells[5].Visible = false;
            }
        }
        lblGridTitle.Text = count.ToString();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAll.Text = "";
        txtAll.Focus();
    }
    private void checkTextAll()
    {
        if (ddlSearch.SelectedValue.ToString() == "IMID")
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select IMID from AppRecord where IMID='" + txtAll.Text.ToString() + "'", con);
            string chk = Convert.ToString(cmd.ExecuteScalar());
            int i = 0;
            if (chk == txtAll.Text.ToString())
            {
                i += 1;
                lblExceptionOK.Text = "";
            }
            else
            {
                lblExceptionOK.Text = "Please Insert Valid IMID.";
                txtAll.Text = "";
                txtAll.Focus();
            }
            con.Close(); 
            btnOK.Focus();
        }
        if (ddlSearch.SelectedValue.ToString() == "DiaryNo")
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select DNo from AppRecord where DNo='" + txtAll.Text.ToString() + "'", con);
            string chk = Convert.ToString(cmd.ExecuteScalar());
            int i = 0;
            if (chk == txtAll.Text.ToString().ToUpper())
            {
                i += 1;
                lblExceptionOK.Text = "";
            }
            else
            {
                lblExceptionOK.Text = "Please Insert Valid Dairy No.";
                txtAll.Text = "";
                txtAll.Focus();
            }
            con.Close(); 
            btnOK.Focus();
        }
        if (ddlSearch.SelectedValue.ToString() == "MembershipNo")
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select Enrolment from AppRecord where Enrolment='" + txtAll.Text.ToString() + "'", con);
            string chk = Convert.ToString(cmd.ExecuteScalar());
            int i = 0;
            if (chk == txtAll.Text.ToString())
            {
                i += 1;
                lblExceptionOK.Text = "";
            }
            else
            {
                lblExceptionOK.Text = "Please Insert Valid Enrollement No....!";
                txtAll.Text = "";
            }
            con.Close(); 
            btnOK.Focus();
        }
    }
    private void bindddlApptype()
    {
        ddlAppType.SelectedValue = "no";
    }
    protected void btnOK_Click1(object sender, EventArgs e)
    {
        fillGrv();
        checkTextAll();
        txtAll.Focus();
    }
}
