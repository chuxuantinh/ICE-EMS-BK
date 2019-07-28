using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
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
using System.Runtime.InteropServices;

public partial class Reports_AC_ApplicationStatusReportRpt : System.Web.UI.Page
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
          
            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
               // lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ApplicationStatus.Visible = false;
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

        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            string qry = "";

            if (ddlApp.SelectedValue == "ALL")
            {
                if (ddlStatus.SelectedValue.ToString() == "NotApproved")
                {
                    rptTitle = "" + ddlApp.SelectedItem.Text + " NotApproved Forms " + ddlExamSeason.SelectedValue + txtYearSeason.Text ;
                    qry = "select * from AppRecord where  Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status='NotApproved' order by Enrolment";
                }
                else if (ddlStatus.SelectedValue.ToString() == "Hold")
                {
                    rptTitle = "" + ddlApp.SelectedItem.Text + " Hold Fporms " + ddlExamSeason.SelectedValue + txtYearSeason.Text ;
                    qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status='Hold' order by Enrolment";
                }
                else
                {
                    rptTitle = "" + ddlApp.SelectedItem.Text + " Approved Forms " + ddlExamSeason.SelectedValue + txtYearSeason.Text ;
                    qry = "select * from AppRecord where Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status!='Hold' and Status!='NotApproved' order by Enrolment";
                }
            }

            else
            {
                if (ddlStatus.SelectedValue.ToString() == "NotApproved")
                {
                    rptTitle = "Not Approved " + ddlApp.SelectedItem.Text + " Forms " + ddlExamSeason.SelectedValue + txtYearSeason.Text;
                    qry = "select * from AppRecord where FormType like '%" + ddlApp.SelectedValue + "%' and Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status='NotApproved' order by Enrolment";
                }
                else if (ddlStatus.SelectedValue.ToString() == "Hold")
                {
                    rptTitle = "Hold" + ddlApp.SelectedItem.Text + " Forms " + ddlExamSeason.SelectedValue + txtYearSeason.Text ;
                    qry = "select * from AppRecord where FormType like '%" + ddlApp.SelectedValue + "%' and Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status='Hold' order by Enrolment";
                }
                else
                {
                    rptTitle = "Approved " + ddlApp.SelectedItem.Text + " Forms " + ddlExamSeason.SelectedValue + txtYearSeason.Text ;
                    qry = "select * from AppRecord where FormType like '%" + ddlApp.SelectedValue + "%' and Session ='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Status!='Hold' and Status!='NotApproved' order by Enrolment";
                }
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
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ApplicationStatusCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ApplicationStatus.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = rptTitle;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ApplicationStatus.ParameterFieldInfo = paramFields;
                ApplicationStatus.EnableDatabaseLogonPrompt = false;
                ApplicationStatus.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ApplicationStatus.Visible = false;
            //    lblExceptioN.Text = "";
            }
            catch (CrystalReportsException ex)
            {
                ApplicationStatus.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                ApplicationStatus.Visible = false;
               // lblExceptioN.Text = "Null Value .";
            }
            catch (SqlException ex)
            {
                ApplicationStatus.Visible = false;
              //  lblExceptioN.Text = "Null Value .";
            }
            catch (ArgumentNullException ex)
            {
                ApplicationStatus.Visible = false;
               // lblExceptioN.Text = "Null Value .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ApplicationStatus.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        ApplicationStatus.Visible = true;
        LoadReport(ReportState.FromStart);
    }

   
}