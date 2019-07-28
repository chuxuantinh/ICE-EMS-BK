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

public partial class Reports_Student_ReAdmissionRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
    ReportDocument rptdoc;
    protected void Page_init(object sender, EventArgs e)
    { try
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
                    txtYear.Text = DateTime.Now.Year.ToString();
                    ReAdmission_Report.Visible = false;
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
            if(ddlCourse.SelectedValue=="Approved")
          cmd.CommandText = "select * from AppRecord where Session ='" + ddlSession.SelectedValue + txtYear.Text + "' and AdmissionFees>0 and Enrolment<'" + txtEnrol.Text + "' and Status!='Hold' and Status!='NotApproved' order by SID";
            
            else
                cmd.CommandText = "select * from AppRecord where Session ='" + ddlSession.SelectedValue + txtYear.Text + "' and AdmissionFees>0 and Enrolment<'" + txtEnrol.Text + "' and Status='" + ddlCourse.SelectedValue + "' order by SID";
            paramDValue.Value = "Session:" + ddlSession.SelectedValue + txtYear.Text + " and Status:" + ddlCourse.SelectedItem.Text;                 
             cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            //    int a = ds.Tables[0].Columns.Count;              
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
                // Course_Report.Visible = true;
                rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ReAdmission.rpt"));
                rptdoc.SetDataSource(dt);
                ReAdmission_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                ReAdmission_Report.ParameterFieldInfo = paramFields;
                ReAdmission_Report.ParameterFieldInfo = paramFields;
                ReAdmission_Report.EnableDatabaseLogonPrompt = false;
                ReAdmission_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                ReAdmission_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
            }
            catch (IndexOutOfRangeException ex)
            {
                ReAdmission_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (CrystalReportsException ex)
            {
                ReAdmission_Report.Visible = false;
                // Response.Write(ex);
            }

            catch (ArgumentNullException ex)
            {
                ReAdmission_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
           
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }

        }
        else
        {
            ReAdmission_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        ReAdmission_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}