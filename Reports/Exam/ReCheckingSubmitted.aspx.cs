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


public partial class Reports_Exam_ReChecking : System.Web.UI.Page
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
                ReChecking_Form_Submitted.Visible = false;
                txtSession.Text = (DateTime.Now.Year-1).ToString();
                txtCurrentSession.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                {
                    ddlSession.SelectedValue = "Win";
                    ddlCurrentSession.SelectedValue = "Sum";
                }
                else
                {
                    ddlSession.SelectedValue = "Sum";
                    ddlCurrentSession.SelectedValue = "Win";
                }
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
            string strcmd = "";
            if(rbtnOnlyApproved.Checked==true)
            strcmd = "select AppRecord.CAD as SN,SExamMarks.RollNo, Rechecking.SID,AppRecord.Name,AppRecord.Course,AppRecord.Part,AppRecord.IMID,Rechecking.SubID,SExamMarks.GetMarks,SExamMarks.Center from AppRecord inner join Rechecking on AppRecord.AppNo=Rechecking.AppNo inner join SExamMarks on SExamMarks.SID=AppRecord.SID where Rechecking.Session='" + ddlSession.SelectedValue.ToString() + txtSession.Text.ToString() + "' and AppRecord.Session='" + ddlCurrentSession.SelectedValue.ToString() + txtCurrentSession.Text.ToString() + "' and Rechecking.SubID=SExamMarks.SubID and SExamMarks.ExamSeason='" + ddlSession.SelectedValue.ToString() + txtSession.Text.ToString() + "' and AppRecord.Status!='NotApproved' and AppRecord.Status!='Hold' order by Rechecking.sid,CAD";
            else 
            strcmd = "select AppRecord.CAD as SN,SExamMarks.RollNo, Rechecking.SID,AppRecord.Name,AppRecord.Course,AppRecord.Part,AppRecord.IMID,Rechecking.SubID,SExamMarks.GetMarks,SExamMarks.Center from AppRecord inner join Rechecking on AppRecord.AppNo=Rechecking.AppNo inner join SExamMarks on SExamMarks.SID=AppRecord.SID where Rechecking.Session='" + ddlSession.SelectedValue.ToString() + txtSession.Text.ToString() + "' and AppRecord.Session='"+ddlCurrentSession.SelectedValue.ToString()+txtCurrentSession.Text.ToString()+"' and Rechecking.SubID=SExamMarks.SubID and SExamMarks.ExamSeason='"+ddlSession.SelectedValue.ToString()+txtSession.Text.ToString()+"' and AppRecord.Status!='NotApproved' order by Rechecking.sid,CAD";
            cmd.CommandText = strcmd;
            paramDValue.Value = "Re-Checking Form Submitted";
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
                rptdoc.Load(Server.MapPath("ReCheckingSubmittedCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ReChecking_Form_Submitted.ReportSource = rptdoc;
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ReChecking_Form_Submitted.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("title", paramDValue.Value);
                ReChecking_Form_Submitted.EnableDatabaseLogonPrompt = false;
                ReChecking_Form_Submitted.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ReChecking_Form_Submitted.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                ReChecking_Form_Submitted.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                ReChecking_Form_Submitted.Visible = false;
            }
            catch (SqlException ex)
            {
                ReChecking_Form_Submitted.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                ReChecking_Form_Submitted.Visible = false;
            }
        }
        else
        {
            ReChecking_Form_Submitted.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        ReChecking_Form_Submitted.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}