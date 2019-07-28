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


public partial class Reports_FO_DiaryTypeRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
   string strt;
   string str;

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
               DiaryTypeReport.Visible = false;
               rblICE.SelectedValue = "ICE";
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
            if (rblICE.SelectedValue == "Date")
            {
                cmd.CommandText = "select * from DiaryEntry where DiaryEntry.Date Between '" + Convert.ToDateTime(txtDate.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate.Text, dtinfo).AddDays(1) + "'";
            }
            else if (rblICE.SelectedValue == "Diary")
            {
                cmd.CommandText = "select * from DiaryEntry where  DiaryEntry.DiaryNo = '" + txtDate.Text.ToString() + "'";
            }
            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a =   ds.Tables[0].Columns.Count;
            cmd.Dispose();
            con.Close();
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
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();

                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("DiaryTypeCrt.rpt"));
                rptdoc.SetDataSource(dt);
                DiaryTypeReport.ReportSource = rptdoc;
                paramField.Name = "tittle";
                if (rblICE.SelectedValue == "Date")
                {
                    paramDValue.Value = "Date: " + txtDate.Text;
                }
                if (rblICE.SelectedValue== "Diary")
                {
                    paramDValue.Value = "Diary No: " + txtDate.Text.ToString();
                }
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                DiaryTypeReport.ParameterFieldInfo = paramFields;
                DiaryTypeReport.EnableDatabaseLogonPrompt = false;
                DiaryTypeReport.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                //lblExceptioN.Text = "Null Date .";
            }
            catch (CrystalReportsException ex)
            {
                // Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (SqlException ex)
            {
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (ArgumentNullException ex)
            {
                //  lblExceptioN.Text = "Null Date .";
            }
        }
        else
        {
            DiaryTypeReport.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        DiaryTypeReport.Visible = true;
            LoadReport(ReportState.FromStart);

    }
    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        DiaryTypeReport.Visible = false;
        if (rblICE.SelectedValue == "Date")
            cald.Visible = true;
        else cald.Visible = false;
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}
