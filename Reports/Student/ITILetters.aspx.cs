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

public partial class Reports_Student_ITILetters : System.Web.UI.Page
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
            else
            {
                if (!IsPostBack && !IsCallback)
                {
                    Session["cr"] = null;
                    cmd = new SqlCommand();
                    cmd.CommandText = "select distinct(Convert(datetime,ExamDate,105)) as ExamDate from ITIForm order by ExamDate desc";
                    cmd.Connection = con;
                    ds = new DataSet();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    ddlCoursrID.DataSource = ds.Tables[0];
                    ddlCoursrID.DataTextField = "ExamDate";
                    ddlCoursrID.DataValueField = "ExamDate";
                    ddlCoursrID.DataBind();
                    ITI_Letters_Report.Visible = false;
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
            cmd.CommandText = "select * from ITIForm where ExamDate='" + Convert.ToDateTime(ddlCoursrID.SelectedItem.Text, dtinfo) + "' and Status='RollNoGenerated'  order by convert(int,AppNo)";
            cmd.Connection = con;           
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
          //  int a = ds.Tables[0].Rows.Count;
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
                paramField = new ParameterField();
                paramDValue = new ParameterDiscreteValue();                  
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ITILettersCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ITI_Letters_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value =  ddlCoursrID.SelectedItem.Text+" from "+txtfrom.Text+" "+ddlFrom.SelectedValue.ToString()+" to " +txtTo.Text+ " "+ddlTo.SelectedValue.ToString()+".";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                paramField = new ParameterField();
                paramDValue = new ParameterDiscreteValue();
                paramField.Name = "tittle2";
                paramDValue.Value =  "Sub: Online Examination for ITI holders on "+ddlCoursrID.SelectedItem.Text;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                 paramField = new ParameterField();
                 paramDValue = new ParameterDiscreteValue();
                paramField.Name = "tittle3";
                paramDValue.Value = ddlCoursrID.SelectedItem.Text+" "+txtTo.Text+" "+ddlTo.SelectedValue+".";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ITI_Letters_Report.ParameterFieldInfo = paramFields;
                ITI_Letters_Report.EnableDatabaseLogonPrompt = false;
                ITI_Letters_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ITI_Letters_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                ITI_Letters_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                ITI_Letters_Report.Visible = false;

            }

            catch (ArgumentNullException ex)
            {
                ITI_Letters_Report.Visible = false;

            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ITI_Letters_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
       ITI_Letters_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.ITI_Letters_Report.Dispose();
        this.ITI_Letters_Report = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
    
}