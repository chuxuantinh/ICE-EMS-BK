using System;
using System.Collections.Generic;
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

public partial class Reports_Project_ProjectDetailsRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
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
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;
                Project_Detail_Report.Visible = false;
                txtSession.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";        
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
            cmd.CommandText = "SELECT Project.*, Student.FName FROM  Project INNER JOIN Student ON Project.SID = Student.SID where  Project.Session='"+ddlSession.SelectedValue+txtSession.Text+"'";
            paramDValue.Value = "Session:" + ddlSession.SelectedValue + txtSession.Text;                                           
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
                rptdoc.Load(Server.MapPath("ProjectDetailCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Project_Detail_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Project_Detail_Report.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("tittle", paramDValue.Value);
                Project_Detail_Report.EnableDatabaseLogonPrompt = false;
                Project_Detail_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Project_Detail_Report.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                Project_Detail_Report.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                Project_Detail_Report.Visible = false;
            }
            catch (SqlException ex)
            {
                Project_Detail_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                Project_Detail_Report.Visible = false;
            }
        }

        else
        {
            Project_Detail_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Project_Detail_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}