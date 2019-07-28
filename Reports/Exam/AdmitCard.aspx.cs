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

public partial class Reports_Exam_AdmitCard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
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
                AdmitCard.Visible = false;
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
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            con.Open();
           // string qry = "SELECT  * From AdmitCard where sid between '" + txtSID.Text + "' and '" + txtSIDTo.Text + "' order by Date";
            string qry = "SELECT  * From AdmitCard order by RollNo,Date";
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
            con.Close();
            con.Dispose();
        }
    }
     ReportDocument rptdoc =new ReportDocument();
    public void LoadReport(ReportState rptState)
    {
        if (rptState != ReportState.FromPostBack)
        {
            try
            {
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                int i =   dt.Rows.Count;
               // rptdoc.Close();
                //  ds.Tables[0].Merge(dt);            
                rptdoc.Load(Server.MapPath("AdmitCardd.rpt"));               
                rptdoc.SetDataSource(dt);
                Section section = rptdoc.ReportDefinition.Sections["GroupHeaderSection1"];
                section.Height = 5740;
                Section section5 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["GroupFooterSection10"];
                section5.Height = 3180;
                Section section3 = rptdoc.ReportDefinition.Sections["Section3"];
                section3.Height = 300;
                Section F55 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["GroupHeaderSection1"];
                F55.Height = 300;
                Section F3 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["GroupFooterSection1"];
                F3.Height = 2878;
                Section F5 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["GroupFooterSection7"];
                F5.Height = 2578;
                Section F6 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["GroupFooterSection8"];
                F6.Height = 2278;
                Section F7 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["GroupFooterSection9"];
                F7.Height = 1978;
                Section F8 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["GroupFooterSection11"];
                F8.Height = 1678;

                Section Sub1 = rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.Sections["DetailSection1"];
                Sub1.Height = 300;

                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["IMID1"].Top = 2120;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["IMID3"].Top = 1820;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["IMID4"].Top = 1520;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["IMID5"].Top = 1220;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["IMID2"].Top = 2420;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["IMID6"].Top = 920;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["Picture1"].Top = 1430;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["Picture2"].Top = 1220;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["Picture3"].Top = 1420;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["Picture4"].Top = 420;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["Picture5"].Top = 1920;
                rptdoc.Subreports["AdmitCardt.rpt"].ReportDefinition.ReportObjects["Picture6"].Top = 220;

                AdmitCard.ReportSource = rptdoc;
               // UpdateSectionHeight(rptdoc, "Section3", 800);
                paramField.Name = "tittle";
                paramDValue.Value = " ";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                AdmitCard.ParameterFieldInfo = paramFields;
                AdmitCard.EnableDatabaseLogonPrompt = false;
                AdmitCard.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                AdmitCard.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                AdmitCard.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                AdmitCard.Visible = false;
            }
            catch (SqlException ex)
            {
                AdmitCard.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                AdmitCard.Visible = false;
            }
        }
        else
        {
            AdmitCard.ReportSource = (ReportDocument)Session["cr"];
        }
    }
   
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        AdmitCard.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.AdmitCard.Dispose();
        this.AdmitCard = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}