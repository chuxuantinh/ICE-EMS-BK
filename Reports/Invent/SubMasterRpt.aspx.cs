using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using System.Configuration;
using MasterLibrary;
using System.Globalization;
using System.Runtime.InteropServices;

public partial class Reports_Invent_SubMasterRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ParameterDiscreteValue paramDValue;
    SqlCommand cmd;
    SqlDataAdapter adapter;
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
                cmd = new SqlCommand();
                cmd.CommandText = "select distinct CourseID from SubjectMaster";
                cmd.Connection = con;          
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                ddlCoursrID.DataSource = ds.Tables[0];
                ddlCoursrID.DataTextField = "CourseID";
                ddlCoursrID.DataValueField = "CourseID";
                ddlCoursrID.DataBind();                           
                Course_Report.Visible = false;
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
         cmd = new SqlCommand();      
        try
        {
            cmd.CommandText = "select * from SubjectMaster where course='" + ddlCourse.SelectedValue + "' and Part='" + ddlPart.SelectedItem.Text + "' and CourseID='" + ddlCoursrID.SelectedItem.Text + "'";
            paramDValue.Value = "Course Stock Details";       
            cmd.Connection = con;    
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
          //  int a = ds.Tables[0].Columns.Count;
            cmd.Dispose();
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

    public void LoadReport(ReportState rptState)
    {
        if (rptState != ReportState.FromPostBack)
        {
            try
            {
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("SubjectMasterCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Course_Report.ReportSource = rptdoc;
                ds.Dispose();                
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Course_Report.ParameterFieldInfo = paramFields;
                Course_Report.EnableDatabaseLogonPrompt = false;
                Course_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Course_Report.Visible = false;                
            }
            catch (CrystalReportsException ex)
            {
                Course_Report.Visible = false;                
            }
            catch (IndexOutOfRangeException ex)
            {
                Course_Report.Visible = false;               
            }
            catch (SqlException ex)
            {
                Course_Report.Visible = false;                
            }
            catch (ArgumentNullException ex)
            {
                Course_Report.Visible = false;                
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Course_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Course_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Course_Report_Init(object sender, EventArgs e)
    {

    }
}