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

public partial class Reports_Exam_BookletRangeExamCenter : System.Web.UI.Page
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
                Booklet_Range_ExamCenter.Visible = false;
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
          cmd.CommandText = "SELECT  BookletPRange.SubID, BookletPRange.Session, BookletPRange.CenterCode, BookletPRange.Type, BookletPRange.Required, BookletPRange.Supply, BookletPRange.EDate, BookletPRange.Shift, PaperRange.StartRange, PaperRange.EndRange, PaperRange.Qty, PaperRange.SN, PaperRange.No FROM BookletPRange INNER JOIN PaperRange ON BookletPRange.SN = PaperRange.SN  where Session='" + ddlSession.SelectedValue.ToString() + txtSession.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "' and CenterCode='"+txtExamCenter.Text+"'";
           // cmd.CommandText = "select BookletPRange.SubID,BookletPRange.Session,BookletPRange.CenterCode,BookletPRange.Type,PaperRange.StartRange,PaperRange.EndRange from BookletPRange inner join PaperRange on BookletPRange.SN=PaperRange.SN where Session='" + ddlSession.SelectedValue.ToString() + txtSession.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "' order by SubID";
            //cmd.CommandText = "select * from BookletPRange";
            paramDValue.Value = "Booklet Range ICE(I)";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //ds.Merge(dt);
            //dt.Clear();
            //adapter=new SqlDataAdapter("select * from PaperRange",con);
            //adapter.Fill(dt);
            //ds.Merge(dt);
            //int a = ds.Tables[0].Columns.Count;
            cmd.Dispose();
            return dt;
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
                DataSet ds = new DataSet();
                dt = getdata();
                rptdoc.Load(Server.MapPath("BookletRangeExamCenterCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Booklet_Range_ExamCenter.ReportSource = rptdoc;
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Booklet_Range_ExamCenter.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("title", paramDValue.Value);
                Booklet_Range_ExamCenter.EnableDatabaseLogonPrompt = false;
                Booklet_Range_ExamCenter.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Booklet_Range_ExamCenter.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                Booklet_Range_ExamCenter.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                Booklet_Range_ExamCenter.Visible = false;
            }
            catch (SqlException ex)
            {
                Booklet_Range_ExamCenter.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                Booklet_Range_ExamCenter.Visible = false;
            }
        }
        else
        {
            Booklet_Range_ExamCenter.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Booklet_Range_ExamCenter.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}