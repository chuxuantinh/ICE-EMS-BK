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
using System.Runtime.InteropServices;


public partial class Reports_Student_StuMembershipGtd : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ParameterDiscreteValue paramDValue;
    ParameterField paramField;
    ReportDocument rptdoc;
    ParameterFields paramFields = new ParameterFields();
  //  SqlCommand cmd;
   // SqlDataAdapter adapter;
   
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
                Session["cr"] = null;            
                Stu_MembershipGtd_Report.Visible = false;
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
            if (ddlMembershipGtd.SelectedValue == "Active")
            {
                cmd.CommandText = "select * from Student where Status='Active' and Session='" + ddlSession.SelectedValue + txtYear.Text + "' and SID Between '"+txtSIDForm.Text+"' and '"+txtSIDTo.Text+"' order by SID";
                paramDValue.Value = "session:" + ddlSession.SelectedItem.Text  + txtYear.Text + " and status:Active  SID "+txtSIDForm.Text+" To "+txtSIDTo.Text;
            }
            else
            {
                cmd.CommandText = "select * from student where Status!='Active' and Session='" +ddlSession.SelectedValue + txtYear.Text + "' and SID Between '"+txtSIDForm.Text+"' and '"+txtSIDTo.Text+"' order by SID";
                paramDValue.Value = "Session:" +ddlSession.SelectedItem.Text + txtYear.Text + " and Status:Not Active SID"+txtSIDForm.Text+" To "+txtSIDTo.Text;
            }
           cmd.Connection = con;
          ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
          //  int a = ds.Tables[0].Rows.Count;
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
                rptdoc.Load(Server.MapPath("StuMembershipGtd.rpt"));
                rptdoc.SetDataSource(dt);
                Stu_MembershipGtd_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Stu_MembershipGtd_Report.ParameterFieldInfo = paramFields;
                Stu_MembershipGtd_Report.EnableDatabaseLogonPrompt = false;
                Stu_MembershipGtd_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Stu_MembershipGtd_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                Stu_MembershipGtd_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                Stu_MembershipGtd_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                Stu_MembershipGtd_Report.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Stu_MembershipGtd_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
       Stu_MembershipGtd_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Stu_MembershipGtd_Report.Dispose();
        this.Stu_MembershipGtd_Report = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
   
   
}