using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using System.Configuration;
using MasterLibrary;
using System.Globalization;
using System.Runtime.InteropServices;

public partial class Reports_Student_ITIExamrpt : System.Web.UI.Page
{
   SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string strt;
    string str;
    static int flag = 0;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
   
   
         protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {  maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
                Session["cr"] = null;
                ITI_Exam_Report.Visible = false;
                LoadReport(ReportState.FromStart);
            }
            else
            {

                LoadReport(ReportState.FromPostBack);
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
        SqlDataAdapter adapter;
       
        try
        {
            cmd.CommandText = "select distinct ExamDate from ITIForm where Session='" + ddlSession.SelectedValue + txtYear.Text +"'";
            con.Open();
            cmd.Connection = con;         
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
         //   int a = ds.Tables[0].Rows.Count;       
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
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ITIExam.rpt"));
                rptdoc.SetDataSource(dt);
               ITI_Exam_Report .ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = "Session:" + ddlSession.SelectedValue + txtYear.Text;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
               ITI_Exam_Report. ParameterFieldInfo = paramFields;
               ITI_Exam_Report.EnableDatabaseLogonPrompt = false;
              ITI_Exam_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ITI_Exam_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                ITI_Exam_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                ITI_Exam_Report.Visible = false;
            }

            catch (ArgumentNullException ex)
            {
                ITI_Exam_Report.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ITI_Exam_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ITI_Exam_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Stuident_Profile_Report_Init(object sender, EventArgs e)
    {

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.ITI_Exam_Report.Dispose();
        this.ITI_Exam_Report = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}
