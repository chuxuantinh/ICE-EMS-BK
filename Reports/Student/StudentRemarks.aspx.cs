using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Runtime.InteropServices;

public partial class Reports_Student_StudentRemarks : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue;
    ParameterField paramField;
    ParameterFields paramFields = new ParameterFields();
   
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                txtYear.Text = DateTime.Now.Year.ToString();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
                Session["cr"] = null;
                Stu_Remarks_Report.Visible = false;
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
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            con.Open();
            if (ddlMembershipGtd.SelectedValue == "Remarks")
            {

                cmd.CommandText = "SELECT  Student.IMID,Student.SID, Student.Name,Student.FName,Student.Remarks, ExamCurrent.Course As Expr5, ExamCurrent.Part as Expr6 FROM Student INNER JOIN ExamCurrent ON Student.SID = ExamCurrent.SId where Student.session='" + ddlSession.SelectedValue + txtYear.Text + "' and Student.Remarks!='' order by Student.SID";
                paramDValue.Value = "Session:" + ddlSession.SelectedItem.Text + txtYear.Text + " and Remarks:Exists";
            }
            if (ddlMembershipGtd.SelectedValue == "ExmpRemarks")
            {
                cmd.CommandText = "SELECT  Student.IMID,Student.SID,Student.Name,Student.FName,Student.ExmpRemarks, ExamCurrent.Course As Expr5, ExamCurrent.Part as Expr6 FROM Student INNER JOIN ExamCurrent ON Student.SID = ExamCurrent.SId where Student.session='" + ddlSession.SelectedValue + txtYear.Text + "' and Student.ExmpRemarks!='' order by Student.SID";
                paramDValue.Value = "Session:" + ddlSession.SelectedItem.Text + txtYear.Text + " and ExmpRemarks:Exists";
            }
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Rows.Count;
            cmd.Dispose();
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
                paramField = new ParameterField();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("StudentRemarkCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Stu_Remarks_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Stu_Remarks_Report.ParameterFieldInfo = paramFields;
                Stu_Remarks_Report.EnableDatabaseLogonPrompt = false;
                Stu_Remarks_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Stu_Remarks_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                Stu_Remarks_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                Stu_Remarks_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                Stu_Remarks_Report.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Stu_Remarks_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Stu_Remarks_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Stu_Remarks_Report.Dispose();
        this.Stu_Remarks_Report = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}