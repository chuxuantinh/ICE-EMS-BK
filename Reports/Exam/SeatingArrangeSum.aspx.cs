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


public partial class Reports_Exam_SeatingArrangeSum : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
       ParameterDiscreteValue paramDValue;
    protected void Page_Load(object sender, EventArgs e)
    { try
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
                    SeatingArrange_Report.Visible = false;
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
        try
        {           
            cmd.CommandText = "SELECT * FROM SeatingArrange where  Date = '" + Convert.ToDateTime(txtDate1.Text.ToString()) + "' and CenterCode='" + txtCenterCode.Text + "' and shift='" + ddlSession.SelectedValue + "'";
            paramDValue.Value = "Room Summary";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
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
                rptdoc.Load(Server.MapPath("SeatingArragSum.rpt"));
                rptdoc.SetDataSource(dt);
                SeatingArrange_Report.ReportSource = rptdoc;
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                SeatingArrange_Report.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("title", paramDValue.Value);
                SeatingArrange_Report.EnableDatabaseLogonPrompt = false;
                SeatingArrange_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                SeatingArrange_Report.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                SeatingArrange_Report.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                SeatingArrange_Report.Visible = false;
            }
            catch (SqlException ex)
            {
                SeatingArrange_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                SeatingArrange_Report.Visible = false;
            }
        }

        else
        {
            SeatingArrange_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        SeatingArrange_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }

}