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

public partial class Reports_Exam_ReCheckingResultCrt : System.Web.UI.Page
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
                bindPaperCode();
                txtYear.Text = DateTime.Now.Year.ToString();
                ReChecking_Result.Visible = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                {
                    ddlSession.SelectedValue = "Win";
                }
                else
                {
                    ddlSession.SelectedValue = "Sum";
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

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPaperCode();
        txtYear.Focus();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        bindPaperCode();
        btnView.Focus();
    }
    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        try
        {
            string strcmd = "";
            //strcmd = "select AppRecord.CAD as SN,SExamMarks.RollNo, Rechecking.SID,AppRecord.Name,AppRecord.Course,AppRecord.Part,AppRecord.IMID,Rechecking.SubID,SExamMarks.GetMarks,SExamMarks.Center from AppRecord inner join Rechecking on AppRecord.AppNo=Rechecking.AppNo inner join SExamMarks on SExamMarks.SID=AppRecord.SID where Rechecking.Session='" + ddlSession.SelectedValue.ToString() + txtYear.Text.ToString() + "' and AppRecord.Session='"+ddlCurrentSession.SelectedValue.ToString()+txtCurrentSession.Text.ToString()+"' and Rechecking.SubID=SExamMarks.SubID and SExamMarks.ExamSeason='"+ddlSession.SelectedValue.ToString()+txtSession.Text.ToString()+"' and AppRecord.Status!='NotApproved' order by Rechecking.sid,CAD";
            cmd.CommandText = "select * from ReChecking where Session='"+ddlSession.SelectedValue.ToString()+txtYear.Text.ToString()+"' and SubID='"+ddlpaperCode.SelectedValue.ToString()+"'";
            paramDValue.Value = "Re-Checking Result";
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
                rptdoc.Load(Server.MapPath("ReCheckingResultCrt.rpt"));
                rptdoc.SetDataSource(dt);
                ReChecking_Result.ReportSource = rptdoc;
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ReChecking_Result.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("title", paramDValue.Value);
                ReChecking_Result.EnableDatabaseLogonPrompt = false;
                ReChecking_Result.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ReChecking_Result.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                ReChecking_Result.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                ReChecking_Result.Visible = false;
            }
            catch (SqlException ex)
            {
                ReChecking_Result.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                ReChecking_Result.Visible = false;
            }
        }
        else
        {
            ReChecking_Result.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        ReChecking_Result.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    private void bindPaperCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select distinct(SubID) from ReChecking where Session='" +ddlSession.SelectedValue.ToString()+txtYear.Text.ToString()+ "'", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        ddlpaperCode.DataSource = dt;
        ddlpaperCode.DataTextField = "SubID";
        ddlpaperCode.DataValueField = "SubID";
        ddlpaperCode.DataBind();
    }
}