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

public partial class Reports_Exam_SeatingArrangementRoomPrt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc = new ReportDocument();
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
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    AttendanceSheetRooms.Visible = false;
                    LoadReport(ReportState.FromStart);
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
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            string qry = "SELECT Student.SID, Student.Name, ExamForm.Shift, ExamForm.Date, ExamForm.SubID, ExamForm.SubName, ExamForms.City, ExamForms.CenterCode, SeatingArrange.RoomName, ExamForms.RollNo, Docs.Image FROM  ExamForms INNER JOIN ExamForm ON ExamForms.SN = ExamForm.SN and ExamForms.ExamSeason='"+ddlExamSeason.SelectedValue.ToString()+txtYearSeason.Text.ToString()+"' and ExamForm.Date=CONVERT(datetime,'"+txtDate.Text+"',105) and ExamForm.Shift='"+ddlShift.SelectedValue.ToString()+"' and ExamForms.CenterCode='"+txtCentercode.Text+"'  INNER JOIN  Student ON ExamForms.SID = Student.SID INNER JOIN  Docs ON Student.SID = Docs.SID INNER JOIN SeatingArrange ON ExamForms.RollNo = SeatingArrange.RollNo  and SeatingArrange.SubCode=ExamForm.SubID and SeatingArrange.Session=Examforms.ExamSeason ";
            ds = new DataSet();
            adapter = new SqlDataAdapter(qry, con);
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
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("AttendanceSheetRoomCrt.rpt"));
                rptdoc.SetDataSource(dt);
                AttendanceSheetRooms.ReportSource = rptdoc;
                paramField.Name = "title";
                paramDValue.Value = "Attendance Sheet";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                AttendanceSheetRooms.ParameterFieldInfo = paramFields;
                AttendanceSheetRooms.EnableDatabaseLogonPrompt = false;
                AttendanceSheetRooms.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                AttendanceSheetRooms.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                AttendanceSheetRooms.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                AttendanceSheetRooms.Visible = false;
            }
            catch (SqlException ex)
            {
                AttendanceSheetRooms.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                AttendanceSheetRooms.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            AttendanceSheetRooms.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        AttendanceSheetRooms.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.AttendanceSheetRooms.Dispose();
        this.AttendanceSheetRooms = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}