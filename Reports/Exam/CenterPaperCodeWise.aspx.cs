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

public partial class Reports_Exam_CenterPaperCodeWise : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ParameterDiscreteValue paramDValue;
    protected void Page_init(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;
                CenterPaperCodewise.Visible = false;
                txtSession.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
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
            cmd.CommandText = "SELECT ExamForms.SID, ExamForms.Status,ExamForm.Date, ExamForms.ExamSeason, ExamForms.CenterCode, ExamForms.City, ExamForms.City2,  ExamForms.SN,  ExamForm.Shift,ExamForm.SubID, ExamForm.SN AS EFSN FROM ExamForms INNER JOIN ExamForm ON ExamForms.SN = ExamForm.SN where  ExamForms.ExamSeason='" + ddlSession.SelectedValue + txtSession.Text + "' order by ExamForm.Shift desc";
            paramDValue.Value = "Number of candidates appearing in the " + ddlSession.SelectedValue + txtSession.Text + " Examination date, and Session and paper code wise";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;
            cmd.Dispose();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
        }

    }
    public void LoadReport(ReportState rptState)
    {
        if (rptState != ReportState.FromPostBack)
        {
            try
            {
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("CenterPaperCodewiseCrt.rpt"));
                rptdoc.SetDataSource(dt);
                CenterPaperCodewise.ReportSource = rptdoc;
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                CenterPaperCodewise.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("title", paramDValue.Value);
                CenterPaperCodewise.EnableDatabaseLogonPrompt = false;
                CenterPaperCodewise.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                CenterPaperCodewise.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                CenterPaperCodewise.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                CenterPaperCodewise.Visible = false;
            }
            catch (SqlException ex)
            {
                CenterPaperCodewise.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                CenterPaperCodewise.Visible = false;
            }
        }

        else
        {
            CenterPaperCodewise.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        CenterPaperCodewise.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}