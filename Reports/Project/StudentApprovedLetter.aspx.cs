using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using System.Configuration;
using MasterLibrary;
using System.Globalization;

public partial class Reports_Project_StudentApprovedLetter : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ParameterDiscreteValue paramDValue;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    protected void Page_init(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;            
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
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        try
        {
            con.Open();
            cmd.CommandText = "SELECT Project.StudentName, Project.Course, Project.Part, Project.SID, Project.Session,Project.IMID,Project.ProjectTitle,Project.LetterRemarks,Project.SynopsisTitle,Project.LetterIssueDate FROM Project where Project.LetterIssueDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' and EntryStatus='Running' and SynopsisStatus='Approved' and LetterRemarks='N/A'";
            paramDValue.Value = "";
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            //    int tt = ds.Tables[0].Rows.Count;
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
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                //   ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("StudentApprovedLetterCrt.rpt"));
                rptdoc.SetDataSource(dt);
                StuApprovedLetter_Report.ReportSource = rptdoc;
                ds.Dispose();
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                StuApprovedLetter_Report.ParameterFieldInfo = paramFields;
                StuApprovedLetter_Report.EnableDatabaseLogonPrompt = false;
                StuApprovedLetter_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                StuApprovedLetter_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                StuApprovedLetter_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                StuApprovedLetter_Report.Visible = false;
            }
            catch (SqlException ex)
            {
                StuApprovedLetter_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                StuApprovedLetter_Report.Visible = false;
            }
        }
        else
        {
            StuApprovedLetter_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        StuApprovedLetter_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}