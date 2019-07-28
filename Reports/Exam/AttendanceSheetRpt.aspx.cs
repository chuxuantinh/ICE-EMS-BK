using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using MasterLibrary;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.Runtime.InteropServices;


public partial class Reports_Exam_AttendanceSheetRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
    protected void Page_init(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            else
            {
                if (!IsPostBack && !IsCallback)
                {

                    Session["cr"] = null;
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    AttendanceSheet.Visible = false;
                    LoadReport(ReportState.FromStart);
                }
                else
                {
                    LoadReport(ReportState.FromPostBack);
                }

            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
        finally
        {
        }
    }
  
    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            string qry2 = "SELECT ExamForm.SID, ExamForm.SubID, ExamForm.SubName, ExamForm.Date, ExamForm.Shift,  ExamForms.City, ExamForms.RollNo, ExamForms.CenterCode, Docs.Image, Student.Name, Student.SID AS Expr2 FROM Docs INNER JOIN  Student ON Docs.SID = Student.SID INNER JOIN  ExamForm INNER JOIN  ExamForms ON ExamForm.SN = ExamForms.SN ON Student.SID = ExamForms.SID and ExamForms.ExamSeason='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "'  and ExamForms.CenterCode='" + txtSID.Text + "' and ExamForm.Date=CONVERT(datetime,'" + txtDate.Text + "',105) and ExamForm.Shift='" + ddlShift.SelectedValue.ToString() + "'  order by ExamForm.SubID, ExamForms.Rollno";
            string qry = "SELECT ExamForm.SN, Student.SID, ExamForms.CenterCode, ExamCenter.ID, ExamForm.Shift, ExamForm.Date, ExamForm.SubName, ExamForm.SubID, Student.Name,  Student.FName,ExamForms.IMID, ExamForms.ExamSeason, ExamForms.Status, ExamForms.RollNo, Docs.Image, ExamCenter.Address, ExamCenter.Address2, ExamCenter.City, ExamCenter.Pin, ExamCenter.State, ExamForms.Course, ExamForms.Part, ExamCenter.Name AS ECenterName FROM ExamForm INNER JOIN  ExamForms ON ExamForm.SN = ExamForms.SN INNER JOIN  Student ON ExamForms.SID = Student.SID INNER JOIN  ExamCenter ON ExamForms.CenterCode = ExamCenter.ID INNER JOIN Docs on Docs.SID=ExamForms.SID where ExamForms.Status='AdmitCardGenerated' and ExamForms.ExamSeason='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "'  and ExamForms.CenterCode='"+txtSID.Text+"' order by ExamForm.SubID, ExamForms.Rollno";
            ds = new DataSet();
            adapter = new SqlDataAdapter(qry, con);
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            ds.Dispose();
            con.Close();
            con.Dispose();
        }
    }
    public void LoadReport(ReportState rptState)
    {
        if (rptState != ReportState.FromPostBack)
        {
            try
            {
               
                rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("AttendanceSheetCrt.rpt"));
                rptdoc.SetDataSource(dt);
                AttendanceSheet.ReportSource = rptdoc;
                paramField.Name = "title";
                paramDValue.Value = "Attendance Sheet";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                AttendanceSheet.ParameterFieldInfo = paramFields;
                AttendanceSheet.EnableDatabaseLogonPrompt = false;
                AttendanceSheet.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                AttendanceSheet.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                AttendanceSheet.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                AttendanceSheet.Visible = false;
            }
            catch (SqlException ex)
            {
                AttendanceSheet.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                AttendanceSheet.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            AttendanceSheet.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        AttendanceSheet.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.AttendanceSheet.Dispose();
        this.AttendanceSheet = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}