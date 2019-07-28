using System;
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


public partial class Reports_Exam_ExamSN : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
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
                    txtYear.Text = DateTime.Now.Year.ToString();
                    ExamFormSerialNoApp.Visible = false;
                    LoadReport(ReportState.FromStart);
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlSession.SelectedValue = "Sum";
                    else ddlSession.SelectedValue = "Win";
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
       
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
       
        try
        {
            con.Open();
            if (ddlSelect.SelectedValue == "ALL")
            {
                cmd.CommandText = "select * from AppRecord where Session='" + ddlSession.SelectedValue.ToString() + txtYear.Text + "' and SubDate between convert(datetime,'" + txtDateFrom.Text + "',105) and convert(datetime,'" + txtDateto.Text + "',105)  and  FormType like '%Exam%' order by AppNo";
                paramDValue.Value = "All Examination Series Report.";
            }
            else
            {
                cmd.CommandText = "select * from AppRecord where Session='" + ddlSession.SelectedValue.ToString() + txtYear.Text + "' and SubDate between convert(datetime,'" + txtDateFrom.Text + "',105) and convert(datetime,'" + txtDateto.Text + "',105)  and (Status='NotApproved' or Status='Hold') and  FormType like '%Exam%' order by AppNo";
                paramDValue.Value = "Not Approved Examination Series Report.";
            }
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
           // int a = ds.Tables[0].Columns.Count;
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            ds.Dispose();
            con.Close();
            con.Dispose();
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
                rptdoc.Load(Server.MapPath("ExamSNCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ExamFormSerialNoApp.ReportSource = rptdoc;
                paramField.Name = "tittle";              
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ExamFormSerialNoApp.ParameterFieldInfo = paramFields;
                ExamFormSerialNoApp.EnableDatabaseLogonPrompt = false;
                ExamFormSerialNoApp.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ExamFormSerialNoApp.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                ExamFormSerialNoApp.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                ExamFormSerialNoApp.Visible = false;
            }
            catch (SqlException ex)
            {
                ExamFormSerialNoApp.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                ExamFormSerialNoApp.Visible = false;
            }
        }
        else
        {
            ExamFormSerialNoApp.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ExamFormSerialNoApp.Visible = true;
        LoadReport(ReportState.FromStart);
    }

  


    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.ExamFormSerialNoApp.Dispose();
        this.ExamFormSerialNoApp = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}