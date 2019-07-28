using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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

public partial class Reports_Exam_ExportResult : System.Web.UI.Page
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
                    txtYear.Text = DateTime.Now.Year.ToString();
                    Exam_Result.Visible = false;
                    LoadReport(ReportState.FromStart);
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlSession.SelectedValue = "Sum";
                    else ddlSession.SelectedValue = "Win";
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
        SqlDataAdapter adapter;
        try
        {
            con.Open();
            if (ddlCourse.SelectedValue.ToString() == "All")
                cmd.CommandText = "select * from SExamMarks where ExamSeason='" + ddlSession.SelectedValue.ToString() + txtYear.Text.ToString() + "'";
            else
                cmd.CommandText = "select * from SExamMarks where ExamSeason='" + ddlSession.SelectedValue.ToString() + txtYear.Text + "' and Course='"+ddlCourse.SelectedValue.ToString()+"' and Part='"+ddlPart.SelectedValue.ToString()+"'";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            //   int a = ds.Tables[0].Columns.Count;
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
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ExportResultCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Exam_Result.ReportSource = rptdoc;
                Exam_Result.EnableDatabaseLogonPrompt = false;
                Exam_Result.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Exam_Result.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                Exam_Result.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                Exam_Result.Visible = false;
            }
            catch (SqlException ex)
            {
                Exam_Result.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                Exam_Result.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Exam_Result.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Exam_Result.Visible = true;
        LoadReport(ReportState.FromStart);

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Exam_Result.Dispose();
        this.Exam_Result = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}