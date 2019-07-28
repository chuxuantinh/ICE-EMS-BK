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

public partial class Reports_AC_DDDateRpt : System.Web.UI.Page
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
            else
            {
                if (!IsPostBack && !IsCallback)
                {

                    Session["cr"] = null;
                    CrystalReportViewer1.Visible = false;
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
     
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            rptTitle = "Date:" + txtDate1.Text.ToString()+" To "+txtDate2.Text;
           string qry = "select * from FeeAC where SubDate between '" +Convert.ToDateTime(txtDate1.Text,dtinfo) + "' and '"+Convert.ToDateTime(txtDate2.Text,dtinfo)+"' order by IMID";
            ds = new DataSet();
            adapter = new SqlDataAdapter(qry,con);
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
                rptdoc.Load(Server.MapPath("ACDDDate.rpt"));
                rptdoc.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rptdoc;
                paramField.Name = "title";
                paramDValue.Value = rptTitle;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                CrystalReportViewer1.ParameterFieldInfo = paramFields;              
                CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
                CrystalReportViewer1.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                CrystalReportViewer1.Visible = false;
                lblExceptioN.Text = "";
            }
            catch (CrystalReportsException ex)
            {
                CrystalReportViewer1.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                CrystalReportViewer1.Visible = false;
                Response.Write(ex);              
            }
            catch (SqlException ex)
            {
                CrystalReportViewer1.Visible = false;
                Response.Write(ex);                
            }
            catch (ArgumentNullException ex)
            {
                CrystalReportViewer1.Visible = false;
                Response.Write(ex);              
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            CrystalReportViewer1.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        CrystalReportViewer1.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void rbtnDate_CheckChanged(object sender, EventArgs e)
    {

    }
    protected void rbtnDiary_CheckChnaged(object sender, EventArgs e)
    {

    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}