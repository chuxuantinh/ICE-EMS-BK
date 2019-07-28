using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Data;
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
using System.Runtime.InteropServices;


public partial class Reports_AC_DDReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    string rptTitle;
    ReportDocument rptdoc;
    protected void Page_Load(object sender, EventArgs e)
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
                DD_Entry_Reports.Visible = false;
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

    }

    public DataTable getdata()
    {

        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            rptTitle = "Date:" + txtDate1.Text.ToString() + " To " + txtDate2.Text;
            string qry = "select * from FeeAC where SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' order by IMID";
            ds = new DataSet();
            adapter = new SqlDataAdapter(qry, con);
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
          //  lblExceptioN.Text = "Please select right date format.";
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
                rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("DDReportCrt.rpt"));
                rptdoc.SetDataSource(dt);
                DD_Entry_Reports.ReportSource = rptdoc;
                paramField.Name = "title";
                paramDValue.Value = rptTitle;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                DD_Entry_Reports.ParameterFieldInfo = paramFields;
                DD_Entry_Reports.EnableDatabaseLogonPrompt = false;
                DD_Entry_Reports.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                DD_Entry_Reports.Visible = false;
                lblExceptioN.Text = "";
            }
            catch (CrystalReportsException ex)
            {
                DD_Entry_Reports.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                DD_Entry_Reports.Visible = false;
                //   Response.Write(ex);
            }
            catch (SqlException ex)
            {
                DD_Entry_Reports.Visible = false;
                Response.Write(ex);
            }
            catch (ArgumentNullException ex)
            {
                DD_Entry_Reports.Visible = false;
                //    Response.Write(ex);
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            DD_Entry_Reports.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        DD_Entry_Reports.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}