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

public partial class Reports_FO_VisitorsRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadReport();
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack)
            {
                Session["cr"] = null;
                VisitorReport.Visible = false;
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
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            cmd.CommandText = "Select * from Reception where Date Between '" + Convert.ToDateTime(txtDateFrom.Text,dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text,dtinfo) + "'";
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
            lblExceptioN.Text = "Please select right date format.";
            return null;
        }
        finally
        {

        }
    }


    protected void LoadReport()
    {
        try
        {
            ReportDocument rptdoc = new ReportDocument();
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();

         
        

         
            DataTable dt = new DataTable();
      
            dt = getdata(); //This function is located below this function
          
            ds.Tables[0].Merge(dt);
            // Your .rpt file path will be below
            rptdoc.Load(Server.MapPath("VisitorsCrt.rpt"));
            //set dataset to the report viewer.
            rptdoc.SetDataSource(dt);
            VisitorReport.ReportSource = rptdoc;
            lblExceptioN.Text = "";

            paramField.Name = "RptTitle";
            paramDValue.Value = "Visitors Report from: " + txtDateFrom.Text + " To: " + txtDateto.Text.ToString();
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            VisitorReport.ParameterFieldInfo = paramFields;

        }
        catch (NullReferenceException ex)
        {
            lblExceptioN.Text = "";
            VisitorReport.Visible = false;
        }
        catch (IndexOutOfRangeException ex)
        {
            //  lblExceptioN.Text = "Null Date .";
        }

    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        VisitorReport.Visible = true;
        LoadReport();
       
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}
