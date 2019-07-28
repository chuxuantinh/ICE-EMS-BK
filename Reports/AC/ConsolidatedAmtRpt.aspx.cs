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

public partial class Reports_AC_ConsolidatedAmtRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    static string rptTitle;
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
                if (!IsPostBack)
                {
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                }
                if (!IsPostBack && !IsCallback)
                {
                    Session["cr"] = null;
                    ApprovedApplicationForms.Visible = false;
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    public DataTable getdata()
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            string qry = "";
                rptTitle = "Session: "+lblHiddenSeason.Text;
                qry = "select FeeACsum.imid, feeacsum.Feeamount, AppRecordsum.appAmount,(Feeacsum.feeamount-AppRecordsum.AppAmount) as Balance from Feeacsum inner join AppRecordSum on Feeacsum.imid=AppRecordsum.imid and FeeACsum.session='"+lblHiddenSeason.Text+"' and Feeacsum.Session=AppRecordsum.Session";
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
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ConsolidatedAmtCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ApprovedApplicationForms.ReportSource = rptdoc;
                paramField.Name = "title";
                paramDValue.Value = rptTitle;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ApprovedApplicationForms.ParameterFieldInfo = paramFields;
                ApprovedApplicationForms.EnableDatabaseLogonPrompt = false;
                ApprovedApplicationForms.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ApprovedApplicationForms.Visible = false;
                lblExceptioN.Text = "";
            }
            catch (CrystalReportsException ex)
            {
                //ApprovedApplicationForms.Visible = false;
                lblExceptioN.Text = "Null Date .";
            }
            catch (IndexOutOfRangeException ex)
            {
                ApprovedApplicationForms.Visible = false;
               lblExceptioN.Text = "Null Date .";
            }
            catch (SqlException ex)
            {
                ApprovedApplicationForms.Visible = false;
                lblExceptioN.Text = "Null Date .";
            }
            catch (ArgumentNullException ex)
            {
                ApprovedApplicationForms.Visible = false;
               lblExceptioN.Text = "Null Date .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ApprovedApplicationForms.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        ApprovedApplicationForms.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}