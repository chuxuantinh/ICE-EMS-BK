using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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


public partial class Reports_Exam_ExamFormsRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
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
                    txtYear.Text = DateTime.Now.Year.ToString();
                    panDepartmentName.Visible = false;
                    ExamForms.Visible = false;
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
            cmd.CommandText = "select ExamForm.SID as EFSID,ExamForms.RollNo,Student.Name,Student.Fname,ExamForms.IMID,ExamForms.Course,ExamForms.Part,ExamForm.SubID,ExamForm.Date,ExamForms.ExamSeason,ExamForm.Shift,ExamForm.SubName,ExamForms.City  from ExamForm inner join ExamForms on ExamForm.SN=ExamForms.SN inner join Student on ExamForms.Sid=Student.SID and ExamForms.ExamSeason='"+ddlSession.SelectedValue+txtYear.Text+"'";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        //   int a = ds.Tables[0].Rows.Count;
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
                rptdoc.Load(Server.MapPath("ExamDateCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ExamForms.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = "Session:" + ddlSession.SelectedItem.Text + txtYear.Text;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ExamForms.ParameterFieldInfo = paramFields;
                ExamForms.EnableDatabaseLogonPrompt = false;
                ExamForms.EnableParameterPrompt = false;
                Session["cr"] = rptdoc; 
            }
            catch (NullReferenceException ex)
            {
                ExamForms.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                ExamForms.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                ExamForms.Visible = false;
            }
            catch (SqlException ex)
            {
                ExamForms.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                ExamForms.Visible = false;
        
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            ExamForms.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ExamForms.Visible = true;
        LoadReport(ReportState.FromStart);
    }
  
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.ExamForms.Dispose();
        this.ExamForms = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}