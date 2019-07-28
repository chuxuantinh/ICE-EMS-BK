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


public partial class Reports_Project_InstituteReRpt : System.Web.UI.Page
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



                Institutes_Regisrtation_Report.Visible = false;

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


        // paramDValue =  new ParameterDiscreteValue();
        try
        {
           
            if (rblICE.SelectedValue == "Diploma")
            {
                paramDValue.Value = "Diploma";

                cmd.CommandText = "select * from InstitutionReg where Type = 'Diploma'";

            
            }
               if (rblICE.SelectedValue == "Degree")
            {
                cmd.CommandText = "select * from InstitutionReg where Type = 'Degree'";

                paramDValue.Value = "Degree";
                }



            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
           

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
                //  Institutes_Regisrtation_Report.Visible = true;
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();

                DataTable dt = new DataTable();
                //  paramDValue.Value = "Project Dtails:";
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("InstituteRe.rpt"));
                rptdoc.SetDataSource(dt);
                Institutes_Regisrtation_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";

                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Institutes_Regisrtation_Report.ParameterFieldInfo = paramFields;
                // rptdoc.SetParameterValue("tittle", paramDValue.Value, "View Details");

                Institutes_Regisrtation_Report.EnableDatabaseLogonPrompt = false;
                Institutes_Regisrtation_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Institutes_Regisrtation_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
            }
            catch (CrystalReportsException ex)
            {
                // Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                Institutes_Regisrtation_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (SqlException ex)
            {
                Institutes_Regisrtation_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (ArgumentNullException ex)
            {
                Institutes_Regisrtation_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }

        }
        else
        {
            Institutes_Regisrtation_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Institutes_Regisrtation_Report.Visible = true;
        LoadReport(ReportState.FromStart);

    }
}