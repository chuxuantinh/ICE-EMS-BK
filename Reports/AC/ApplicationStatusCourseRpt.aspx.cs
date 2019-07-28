using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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
using System.Configuration;
using System.Runtime.InteropServices;

public partial class Reports_AC_ApplicationStatusCourseRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    static string rptTitle;
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue;
    protected void Page_init(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }

            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                // lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ApplicationStatusCourse.Visible = false;
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
        paramDValue = new ParameterDiscreteValue();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            string qry = "";

            if (ddlStatus.SelectedValue.ToString() == "NotApproved")
            {
                paramDValue.Value = "Status: NotApproved  and Session:" + ddlExamSeason.SelectedValue + txtYearSeason.Text;
                qry = "select * from AppRecord where  Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status='NotApproved'  and FormType like '%" + ddlAppType.SelectedValue.ToString() + "%'  order by Enrolment";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Hold")
            {
                paramDValue.Value = "Status: Hold and Session:" + ddlExamSeason.SelectedValue + txtYearSeason.Text;
                qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status='Hold'  and FormType like '%" + ddlAppType.SelectedValue.ToString() + "%'  order by Enrolment";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Approved")
            {
                paramDValue.Value = "Status: Approved and Session:" + ddlExamSeason.SelectedValue + txtYearSeason.Text;
                qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status!='Hold' and Status!='NotApproved'  and FormType like '%" + ddlAppType.SelectedValue.ToString() + "%'  order by Enrolment";
            }
            else if (ddlStatus.SelectedValue.ToString() == "All")
            {
                paramDValue.Value = "Status: All and Session:" + ddlExamSeason.SelectedValue + txtYearSeason.Text;
                qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and FormType like '%" + ddlAppType.SelectedValue.ToString() + "%'  order by Enrolment";
            }
            ds = new DataSet();
            adapter = new SqlDataAdapter(qry, con);
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            lblExceptioN.Text = "Please select right date format.";
            return null;
        }
        finally
        {
            ds.Dispose();
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
                rptdoc.Load(Server.MapPath("ApplicationStatusCourseCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ApplicationStatusCourse.ReportSource = rptdoc;
                paramField.Name = "tittle";
                // paramDValue.Value ="";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ApplicationStatusCourse.ParameterFieldInfo = paramFields;
                ApplicationStatusCourse.EnableDatabaseLogonPrompt = false;
                ApplicationStatusCourse.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ApplicationStatusCourse.Visible = false;
                //    lblExceptioN.Text = "";
            }
            catch (CrystalReportsException ex)
            {
                ApplicationStatusCourse.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                ApplicationStatusCourse.Visible = false;
                // lblExceptioN.Text = "Null Value .";
            }
            catch (SqlException ex)
            {
                ApplicationStatusCourse.Visible = false;
                //  lblExceptioN.Text = "Null Value .";
            }
            catch (ArgumentNullException ex)
            {
                ApplicationStatusCourse.Visible = false;
                // lblExceptioN.Text = "Null Value .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ApplicationStatusCourse.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        ApplicationStatusCourse.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}