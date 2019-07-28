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


public partial class Reports_Student_CourseRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string str;
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
    ReportDocument rptdoc;
   
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
                txtYear.Text = DateTime.Now.Year.ToString();
                Course_Report.Visible = false;
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
            con.Open();
            str = ddlSession.SelectedValue + txtYear.Text;
            if (ddlCourse.SelectedValue == "ALL")
            {
                cmd.CommandText = "select * from Student where Session ='" + ddlSession.SelectedValue + txtYear.Text+"' order by SID";
                paramDValue.Value = "Session:" + str + " and Course:" + ddlCourse.SelectedItem.Text;
            }
            else
            {

                cmd.CommandText = "select * from Student where Course='" + ddlCourse.SelectedItem.Text + "' and Part='" + ddlPart.SelectedValue + "' and Session ='" + ddlSession.SelectedValue + txtYear.Text+"' order by SID" ;
                paramDValue.Value = "Session:" + str + " and Course:" + ddlCourse.SelectedItem.Text + ddlPart.SelectedItem.Text;
            
            } cmd.Connection = con;        
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        //    int a = ds.Tables[0].Columns.Count;              
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            //   lblExceptioN.Text="Please select right date format.";
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
                // Course_Report.Visible = true;
                rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("CourseCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Course_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Course_Report.ParameterFieldInfo = paramFields;
                Course_Report.ParameterFieldInfo = paramFields;
                Course_Report.EnableDatabaseLogonPrompt = false;
                Course_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Course_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
            }
            catch (IndexOutOfRangeException ex)
            {
                Course_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (CrystalReportsException ex)
            {
                Course_Report.Visible = false;
                // Response.Write(ex);
            }

            catch (ArgumentNullException ex)
            {
                Course_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
          else
        {
            Course_Report.ReportSource = (ReportDocument)Session["cr"];
        }
   
    }



    protected void btnView_Click(object sender, EventArgs e)
    {
        Course_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Course_Report_Init(object sender, EventArgs e)
    {

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Course_Report.Dispose();
        this.Course_Report = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
   
}