using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
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

public partial class Reports_AC_DebitNoteRpt : System.Web.UI.Page
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
                    LoadReport(ReportState.FromStart);
                    DebitNoteRpt.Visible = false;
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
            String diary = ddlExamSeason.SelectedValue.ToString().Substring(0, 1) + txtYearSeason.Text.ToString().Substring(2, 2);
            con.Open();
            cmd.CommandText = "SELECT DebitNote.SN, DebitNote.ReqDate, DebitNote.Amount, DebitNote.DiaryNo, DebitNote.Status, DebitNote.Remarks, DebitNote.Admission, DebitNote.Exam,DebitNote.Others, DebitNote.Balance, IMAC.Total, DebitNote.IMID, IMAC.IMID AS IMACIMID FROM IMAC INNER JOIN DebitNote ON IMAC.IMID = DebitNote.IMID where DebitNote.IMID='" + txtSearch.Text + "' and DebitNote.Status='" + ddlStatus.SelectedValue + "' and DebitNote.DiaryNo like '" + diary + "%'";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
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
                rptdoc.Load(Server.MapPath("DebitNoteCrt.rpt"));
                rptdoc.SetDataSource(dt);
                DebitNoteRpt.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = "Membership: " + txtSearch.Text;
               paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                DebitNoteRpt.ParameterFieldInfo = paramFields;
                DebitNoteRpt.EnableDatabaseLogonPrompt = false;
                DebitNoteRpt.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                DebitNoteRpt.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                DebitNoteRpt.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                DebitNoteRpt.Visible = false;
            }
            catch (SqlException ex)
            {
                DebitNoteRpt.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                DebitNoteRpt.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            DebitNoteRpt.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        DebitNoteRpt.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}