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

public partial class Reports_Project_ProjectStatusRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    SqlCommand cmd;
    SqlDataAdapter adapter;
    ParameterDiscreteValue paramDValue;
    protected void Page_Load(object sender, EventArgs e)
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
                panIMID.Visible = false;
                Project_Status_Report.Visible = false;
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

    protected void  ddlProject_SelectedIndexChanged(object sender, EventArgs e)
{
}


    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
       
        try
        {
            //  str = ddlSession.SelectedValue + txtYear.Text;
            //  tit = ddlLetterType.SelectedItem.Text;
            if (rblICE.SelectedValue == "ICE")
            {
                paramDValue.Value = "Required Project Submission Report";
                if (ddlProject.SelectedItem.Text == "PartII+SectionB")
                {

                    cmd.CommandText = "select * from ExamCurrent where SID not in (select SID from Project) and Part='PartII' or part='SectionB'";                   
                }
               
                else
                {
                    cmd.CommandText = "select * from ExamCurrent where SID not in (select SID from Project) and Part='" + ddlProject.SelectedItem.Text + "' and Course='"+ddlCourse.SelectedItem.Text+"'";
                   
                }
               

            }
            if (rblICE.SelectedValue == "IMID")
            {
                paramDValue.Value = "Required Project Submission Report of IMID:" + txtIMID.Text;
                if (ddlProject.SelectedItem.Text == "PartII+SectionB")
                {

                    cmd.CommandText = "select * from ExamCurrent where SID not in (select SID from Project) and IMID='"+txtIMID.Text+"' and (part='SectionB' or Part='PartII')";
                   
                }

                else
                {

                    cmd.CommandText = "select * from ExamCurrent where SID not in (select SID from Project) and Part='" + ddlProject.SelectedItem.Text + "' and Course='" + ddlCourse.SelectedItem.Text + "' and IMID='"+txtIMID.Text+"'";
                  
                }                          

            }

            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;

            cmd.Dispose();

            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            //   lblExceptioN.Text="Please select right date format.";
            return null;
        }
        finally
        {
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
                //  Project_Status_Report.Visible = true;
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                //  paramDValue.Value = "Project Dtails:";
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ProjectStatusCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Project_Status_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";

                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Project_Status_Report.ParameterFieldInfo = paramFields;
               // rptdoc.SetParameterValue("tittle", paramDValue.Value, "View Details");

                Project_Status_Report.EnableDatabaseLogonPrompt = false;
                Project_Status_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Project_Status_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
            }
            catch (CrystalReportsException ex)
            {
                Project_Status_Report.Visible = false;
                // Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                Project_Status_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (SqlException ex)
            {
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (ArgumentNullException ex)
            {
                Project_Status_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Project_Status_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Project_Status_Report.Visible = true;

        LoadReport(ReportState.FromStart);
    }
  
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        Project_Status_Report.Visible = false;       
        
        if (rblICE.SelectedValue == "ICE")
        {
           panIMID.Visible = false;
            txtIMID.Visible = false;          
        }
        if (rblICE.SelectedValue == "IMID")
        {   
            panIMID.Visible = true;
            txtIMID.Visible = true;
        }
    }
}