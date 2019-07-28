using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using System.Configuration;
using MasterLibrary;
using System.Globalization;
using System.Runtime.InteropServices;


public partial class Reports_Project_ProjectApproved : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                if (ddlProjectCopies.SelectedValue == "CopySubmitted")
                    date.Visible = false;
                else date.Visible = true;
                LoadReport(ReportState.FromStart, ProjectApproved_Report, "ProjectApproved.rpt");
            }
            else
            {
                LoadReport(ReportState.FromPostBack, ProjectApproved_Report, "ProjectApproved.rpt");
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void ddlSEssion_SelectedIndexChanged(object sende, EventArgs e)
    {
        lblSessoin.Text = ddlExamSeason.SelectedValue.ToString() + txtYearSeason.Text;
        txtYearSeason.Focus();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        lblSessoin.Text = ddlExamSeason.SelectedValue.ToString() + txtYearSeason.Text;
        ddlProjectCopies.Focus();
    }
    public DataTable getdata()
    {
        SqlDataAdapter adapter;
        try
        {
            DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            string qry = "";
            if(ddlProjectCopies.SelectedValue=="CopySubmitted")
                qry = "select * from Project  where  SynopsisStatus='Approved' and Status='CopySubmitted' and Grade!='N/A'  and Grade!='ABSENT' and ApprovalFees!=0 and EvalutionFees!=0 and NoofCopies!=0  and GroupID not in(select Distinct(GroupID) from Project where NoofCopies=0) order by GroupID,SID";
            else
                qry = "select * from Project  where  SendDate='"+Convert.ToDateTime( txtDate.Text.ToString(),dtinfo)+"' order by GroupID,SID";
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
                paramDValue.Value = "Session:"+ddlExamSeason.SelectedValue +txtYearSeason.Text;
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
protected void  btnView_Click(object sender, EventArgs e)
    {
        ProjectApproved_Report.Visible = true;
        LoadReport(ReportState.FromStart, ProjectApproved_Report, "ProjectApproved.rpt");
    }

protected void ddlProjectCopies_SelectedIndexChanged(object sender, EventArgs e)
{
    if (ddlProjectCopies.SelectedValue == "CopySubmitted")
    date.Visible = false;
    else date.Visible = true;
}
}