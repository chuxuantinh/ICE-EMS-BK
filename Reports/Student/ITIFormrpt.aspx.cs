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

public partial class Reports_Student_ITIFormrpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
   
         protected void Page_Init(object sender, EventArgs e)
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
                    ITI_Form_Report.Visible = false;
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
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";      
        try
        {
            con.Open();
            cmd.CommandText = "select * from ITIForm where ExamDate='" + Convert.ToDateTime(txtDate.Text, dtinfo) + "'  order by convert(int,AppNo)";           
            cmd.Connection = con;        
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
         //   int a = ds.Tables[0].Rows.Count;
            cmd.Dispose();
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
                rptdoc.Load(Server.MapPath("ITIForms.rpt"));
                rptdoc.SetDataSource(dt);
                ITI_Form_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = "Exam Date: "+ txtDate.Text;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ITI_Form_Report.ParameterFieldInfo = paramFields;
                ITI_Form_Report.EnableDatabaseLogonPrompt = false;
                ITI_Form_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ITI_Form_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                ITI_Form_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                ITI_Form_Report.Visible = false;

            }

            catch (ArgumentNullException ex)
            {
                ITI_Form_Report.Visible = false;

            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ITI_Form_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
       ITI_Form_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Stuident_Profile_Report_Init(object sender, EventArgs e)
    {

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.ITI_Form_Report.Dispose();
        this.ITI_Form_Report = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}