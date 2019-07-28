using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using System.Configuration;
using MasterLibrary;
using System.Globalization;
using System.Runtime.InteropServices;

public partial class Reports_Student_ITIResult : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
     DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue;
    ParameterField paramField;
    ParameterFields paramFields = new ParameterFields();
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
                cmd.CommandText = "select distinct(Convert(datetime,ExamDate,105)) as ExamDate from ITIForm";
                cmd.Connection = con;
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                ddlCoursrID.DataSource = ds.Tables[0];
                ddlCoursrID.DataTextField = "ExamDate";
                ddlCoursrID.DataValueField = "ExamDate";
                ddlCoursrID.DataBind();
                ITI_Result_Report.Visible = false;
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
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            cmd.CommandText = "select * from ITIForm where ExamDate='" + Convert.ToDateTime(ddlCoursrID.SelectedItem.Text, dtinfo) + "' and (Status='qualified' or Status='Not qualified') order by convert(int,AppNo)";
            cmd.Connection = con;          
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
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
                paramField = new ParameterField();
                paramDValue = new ParameterDiscreteValue();                    
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ITIResult.rpt"));
                rptdoc.SetDataSource(dt);
                ITI_Result_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value ="ExamDate: "+ ddlCoursrID.SelectedItem.Text+".";              
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ITI_Result_Report.ParameterFieldInfo = paramFields;
                ITI_Result_Report.EnableDatabaseLogonPrompt = false;
                ITI_Result_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ITI_Result_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                ITI_Result_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                ITI_Result_Report.Visible = false;

            }

            catch (ArgumentNullException ex)
            {
                ITI_Result_Report.Visible = false;

            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ITI_Result_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
       ITI_Result_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.CrystalReportSource1.Dispose();
        this.CrystalReportSource1 = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
    
}

    
