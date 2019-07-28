using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

public partial class Reports_Project_ProjectApprovedRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    string rptTitle;
    ReportDocument rptdoc;
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
                LoadReport(ReportState.FromStart,Project_Approval_Report, "ProjectApprovedCrt.rpt");
            }                      
            else
            {

                LoadReport(ReportState.FromPostBack, Project_Approval_Report, "ProjectApprovedCrt.rpt");
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

    protected void btnView_Click(object sender, EventArgs e)
    {
       // Session["cr"] = null;
        Project_Approval_Report.Visible = true;
            LoadReport(ReportState.FromStart, Project_Approval_Report, "ProjectApprovedCrt.rpt");
    }
    public DataTable getdata()
    {
        //  SqlCommand cmd = new SqlCommand();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            rptTitle = "Date:" + txtDate1.Text.ToString() + " To " + txtDate2.Text;
            string qry = "";
            qry = "select * from AppRecord where FormType='"+ddlFeeType.SelectedValue+"' and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' order by AppNo";
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

        }
    }
    public void LoadReport(ReportState rptState, CrystalDecisions.Web.CrystalReportViewer crivew, string report)
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
                rptdoc.Load(Server.MapPath(report));
                rptdoc.SetDataSource(dt);
                crivew.ReportSource = rptdoc;
                paramField.Name = "title";
                paramDValue.Value = "Date:" + txtDate1.Text.ToString() + " To " + txtDate2.Text;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                crivew.ParameterFieldInfo = paramFields;
                crivew.EnableDatabaseLogonPrompt = false;
                crivew.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                crivew.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                crivew.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                crivew.Visible = false;
                Response.Write(ex);
            }
            catch (SqlException ex)
            {
                crivew.Visible = false;
                Response.Write(ex);
            }
            catch (ArgumentNullException ex)
            {
                crivew.Visible = false;
                Response.Write(ex);
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            crivew.ReportSource = (ReportDocument)Session["cr"];
        }
    }
}