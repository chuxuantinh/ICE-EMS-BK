using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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

public partial class Reports_Invent_SupplierRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };    
    ParameterDiscreteValue paramDValue;
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
                Supplier_Details_Report.Visible = true;
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
        try
        {                   
              cmd.CommandText = "select * from Supplier";
                paramDValue.Value = "Supplier Details";                        
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;
            cmd.Dispose();        
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            //   lblExceptioN.Text="Please select right date format.";
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
                rptdoc.Load(Server.MapPath("SupplierCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Supplier_Details_Report.ReportSource = rptdoc;
                ds.Dispose();
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Supplier_Details_Report.ParameterFieldInfo = paramFields;
                Supplier_Details_Report.EnableDatabaseLogonPrompt = false;
                Supplier_Details_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Supplier_Details_Report.Visible = false;
                
            }
            catch (CrystalReportsException ex)
            {
                Supplier_Details_Report.Visible = false;
              
            }
            catch (IndexOutOfRangeException ex)
            {
                Supplier_Details_Report.Visible = false;
                
            }
            catch (SqlException ex)
            {
                Supplier_Details_Report.Visible = false;
               
            }
            catch (ArgumentNullException ex)
            {
                Supplier_Details_Report.Visible = false;
               
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Supplier_Details_Report.ReportSource = (ReportDocument)Session["cr"];
        }
       
    }


        
        protected void Supplier_Details_Report_Init(object sender, EventArgs e)
        {

        }
}
