﻿using System;
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


public partial class Reports_Exam_ExemptionFormSubmitted : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
            else
            {
                if (!IsPostBack && !IsCallback)
                {
                    Session["cr"] = null;
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    ExamFormSubmitted.Visible = false;
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
       
    public void LoadReport(ReportState rptState)
    {
        if (rptState != ReportState.FromPostBack)
        {
            try
            {
                //rptdoc.Close();
                //rptdoc.Dispose();
                //GC.Collect();
                rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);            
                rptdoc.Load(Server.MapPath("EFSubmitted.rpt"));               
                rptdoc.SetDataSource(dt);
                ExamFormSubmitted.ReportSource = rptdoc;
            //  UpdateSectionHeight(rptdoc, "GroupHeaderSection1", 2400);
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ExamFormSubmitted.ParameterFieldInfo = paramFields;
                ExamFormSubmitted.EnableDatabaseLogonPrompt = false;
                ExamFormSubmitted.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ExamFormSubmitted.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                ExamFormSubmitted.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                ExamFormSubmitted.Visible = false;
            }
            catch (SqlException ex)
            {
                ExamFormSubmitted.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                ExamFormSubmitted.Visible = false;
           
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ExamFormSubmitted.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            string qry = "SELECT * from AppRecord where Session='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Exempted>0 and Enrolment between '" + txtSID.Text + "' and '" + txtSIDTo.Text + "' order by Enrolment";
            paramDValue.Value = "Enrolment From: " + txtSID.Text + " to " + txtSIDTo.Text + " and Session:" + ddlExamSeason.SelectedValue + txtYearSeason.Text;
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
            ds.Dispose();
        }
    }
   
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        ExamFormSubmitted.Visible = true;
        LoadReport(ReportState.FromStart);
    }

}