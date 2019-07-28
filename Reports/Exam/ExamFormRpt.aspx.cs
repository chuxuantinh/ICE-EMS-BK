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


public partial class Reports_Exam_ExamFormRpt : System.Web.UI.Page
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
                      txtIMID.Visible = false;
                      lblImid.Visible = false;
                      ExamForm.Visible = false;
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
            cmd.CommandText = "Select ExamForms.City,ExamForms.City2,ExamForms.SID ,ExamForms.IMID,ExamForms.Course ,ExamForms.Part,AppRecord.FormType,ExamForm.SubId,AppRecord.Exam  FROM  ExamForms inner  JOIN ExamForm ON ExamForms.SN = ExamForm.SN inner join AppRecord on AppRecord.Enrolment=ExamForms.SID where ExamForms.Examseason='" + ddlSession.SelectedValue.ToString() + txtYear.Text + "' and ExamForms.Part=AppRecord.Part and AppRecord.FormType like '%Exam%' and AppRecord.Session='"+ ddlSession.SelectedValue.ToString() + txtYear.Text +"' order by ExamForm.SID";                    
            cmd.Connection = con;        
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
          //  int a = ds.Tables[0].Columns.Count;         
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
                  rptdoc.Load(Server.MapPath("ExamFormCrt.rpt"));
                  rptdoc.SetDataSource(dt);
                  ExamForm.ReportSource = rptdoc;
                  paramField.Name = "tittle";
                  paramDValue.Value = "Session:" + ddlSession.SelectedItem.Text + txtYear.Text; 
                  paramField.CurrentValues.Add(paramDValue);
                  paramField.HasCurrentValue = true;
                  paramFields.Add(paramField);
                  ExamForm.ParameterFieldInfo = paramFields;
                  ExamForm.EnableDatabaseLogonPrompt = false;
                  ExamForm.EnableParameterPrompt = false;
                  Session["cr"] = rptdoc;
                //  rptdoc.Dispose();
              }
              catch (NullReferenceException ex)
              {
                  ExamForm.Visible = false;
              }
              catch (CrystalReportsException ex)
              {
                  ExamForm.Visible = false;
              }
              catch (IndexOutOfRangeException ex)
              {
                  ExamForm.Visible = false;
              }
              catch (SqlException ex)
              {
                  ExamForm.Visible = false;
              }
              catch (ArgumentNullException ex)
              {
                  ExamForm.Visible = false;
          
              }
              catch (COMException ex)
              {
                  Response.Redirect("../../Login.aspx");
              }
          }

          else
          {
              ExamForm.ReportSource = (ReportDocument)Session["cr"];
          }
      }
        
    protected void  btnView_Click(object sender, EventArgs e)
{
    ExamForm.Visible = true;
    LoadReport(ReportState.FromStart);
}

   

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.ExamForm.Dispose();
        this.ExamForm = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }

}