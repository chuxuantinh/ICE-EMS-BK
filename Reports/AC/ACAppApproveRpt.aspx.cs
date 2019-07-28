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

public partial class Reports_AC_ACAppApproveRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
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
                if (!IsPostBack && !IsCallback)
                {
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
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
  
    public DataTable getdata()
    {
     
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            string qry = "";
            if (ddlStatus.SelectedValue.ToString() == "NotApproved")
            {
                rptTitle = "Status: NotApproved Application Forms";
                qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and Status='NotApproved' order by SN Desc";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Hold")
            {
                rptTitle = "Status:Hold Application Forms";
                qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and Status='Hold' order by SN Desc";
            }
            else
            {
                rptTitle = "Status:Approved Application Forms";
                qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and Status!='Hold' and Status!='NotApproved' order by SN Desc";
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
                rptdoc.Load(Server.MapPath("ACAppAppCrt.rpt"));
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
                ApprovedApplicationForms.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
               // ApprovedApplicationForms.Visible = false;
                lblExceptioN.Text = "Null Value .";
            }
            catch (SqlException ex)
            {
               // ApprovedApplicationForms.Visible = false;
                 lblExceptioN.Text = "Null Value .";
            }
            catch (ArgumentNullException ex)
            {
                ApprovedApplicationForms.Visible = false;
                lblExceptioN.Text = "Null Value .";
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