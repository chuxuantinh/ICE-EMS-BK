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

public partial class Reports_Exam_BookletShortRpt : System.Web.UI.Page
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
                Booklet_Range_Short.Visible = false;
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
            cmd.CommandText = "select * from BookletPRange where Session='" + ddlSession.SelectedValue.ToString() + txtSession.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "'  and BookletPRange.Required> BookletPRange.Supply";
            //cmd.CommandText = "SELECT  BookletPRange.SubID, BookletPRange.Session, BookletPRange.CenterCode, BookletPRange.Type, BookletPRange.Required, BookletPRange.Supply, BookletPRange.EDate, BookletPRange.Shift, PaperRange.StartRange, PaperRange.EndRange, PaperRange.Qty, PaperRange.SN, PaperRange.No FROM BookletPRange INNER JOIN PaperRange ON BookletPRange.SN = PaperRange.SN  where Session='" + ddlSession.SelectedValue.ToString() + txtSession.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "' and CenterCode='" + txtExamCenter.Text + "' and BookletPRange.Required> BookletPRange.Supply";
            paramDValue.Value = "Booklet Range ICE(I)";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
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
                rptdoc.Load(Server.MapPath("BookletRangeShortCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Booklet_Range_Short.ReportSource = rptdoc;
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Booklet_Range_Short.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("title", paramDValue.Value);
                Booklet_Range_Short.EnableDatabaseLogonPrompt = false;
                Booklet_Range_Short.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Booklet_Range_Short.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                Booklet_Range_Short.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                Booklet_Range_Short.Visible = false;
            }
            catch (SqlException ex)
            {
                Booklet_Range_Short.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                Booklet_Range_Short.Visible = false;
            }
        }
        else
        {
            Booklet_Range_Short.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Booklet_Range_Short.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}