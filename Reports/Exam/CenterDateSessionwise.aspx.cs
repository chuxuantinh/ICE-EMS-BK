using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class Reports_Exam_CenterDateSessionwise : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
    protected void Page_Load(object sender, EventArgs e)
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
                    txtYear.Text = DateTime.Now.Year.ToString();
                    subwiseReport.Visible = false;
                    LoadReport(ReportState.FromStart);
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlSession.SelectedValue = "Sum";
                    else ddlSession.SelectedValue = "Win";
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
            con.Open();
            // cmd.CommandText = "select * from AppRecord where Enrolment in (select SID from Student where Status='Active' and Session='" + ddlSession.SelectedValue.ToString() + txtYear.Text + "') order by Enrolment;";
            cmd.CommandText = "SELECT ExamForm.*, ExamForms.City, ExamForms.ExamSeason FROM ExamForm INNER JOIN ExamForms ON ExamForm.SN = ExamForms.SN where ExamForms.ExamSeason='" + ddlSession.SelectedValue.ToString() + txtYear.Text + "'";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            //   int a = ds.Tables[0].Columns.Count;
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
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("CenterDateSessionwiseCrt.rpt"));
                rptdoc.SetDataSource(dt);
                subwiseReport.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = "Session:" + ddlSession.SelectedItem.Text + " " + txtYear.Text;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                subwiseReport.ParameterFieldInfo = paramFields;
                subwiseReport.EnableDatabaseLogonPrompt = false;
                subwiseReport.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                subwiseReport.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                subwiseReport.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                subwiseReport.Visible = false;
            }
            catch (SqlException ex)
            {
                subwiseReport.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                subwiseReport.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            subwiseReport.ReportSource = (ReportDocument)Session["cr"];
        }
    }


     protected void btnView_Click(object sender, EventArgs e)
    {
        subwiseReport.Visible = true;
        LoadReport(ReportState.FromStart);
       
    }
}