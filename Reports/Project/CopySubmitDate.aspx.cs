using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using MasterLibrary;
using System.Globalization;


public partial class Reports_Project_CopySubmitDate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    
  
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
            cmd.CommandText = "select * from Project  where CopySubmitDate Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "' order by  GroupID, SID";         
            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            cmd.Dispose();
            con.Close();
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
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
             
            try
            {
                ReportDocument rptdoc = new ReportDocument();              
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("CopySubmitCrt.rpt"));
                rptdoc.SetDataSource(dt);
                CopySubmitReport.ReportSource = rptdoc;
                paramField.Name = "title";
                paramDValue.Value = "Copy Submit Date: "+txtDateFrom.Text + " to " + txtDateto.Text;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                CopySubmitReport.ParameterFieldInfo = paramFields;
                CopySubmitReport.EnableDatabaseLogonPrompt = false;
                CopySubmitReport.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                CopySubmitReport.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                CopySubmitReport.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                CopySubmitReport.Visible = false;
            }
            catch (SqlException ex)
            {
                CopySubmitReport.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                CopySubmitReport.Visible = false;
            }
        }
        else
        {
            CopySubmitReport.ReportSource = (ReportDocument)Session["cr"];
        }
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        CopySubmitReport.Visible = true;
        LoadReport(ReportState.FromStart);

    }
}