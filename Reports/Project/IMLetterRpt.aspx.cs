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

public partial class Reports_project_IMLeteerRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    SqlCommand cmd;
    SqlDataAdapter adapter;
    ParameterDiscreteValue paramDValue;
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                IM_Letter_Report.Visible = false;
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

    }

    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;

        // paramDValue =  new ParameterDiscreteValue();
        try
        {
            cmd.CommandText = "SELECT Project.InstitutionID,Project.SID,Project.StudentName,InstitutionReg.Designation, InstitutionReg.Person, InstitutionReg.Mobile, Project.Part, Project.Course, Project.Session, Project.GuidanceFees, Project.TrainingFees,IM.PPinCode, Project.SID, InstitutionReg.Name, InstitutionReg.Address, InstitutionReg.City, InstitutionReg.Pincode, InstitutionReg.State, InstitutionReg.Contact, Project.IMID, IM.PAddress, IM.PCity, IM.PState, IM.Name AS Expr1, IM.HName FROM Project INNER JOIN InstitutionReg ON Project.InstitutionID = InstitutionReg.ID INNER JOIN IM ON Project.IMID = IM.ID where Project.session='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and Project.IMID='" + txtIMID.Text + "' and Project.EntryStatus='Running' order by Project.InstitutionID";
            paramDValue.Value = "";
            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            cmd.Dispose();
            int a = ds.Tables[0].Rows.Count;
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
                //  IM_Letter_Report.Visible = true;
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                //  paramDValue.Value = "Project Dtails:";
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("IMLetterCrt.rpt"));
                rptdoc.SetDataSource(dt);
                IM_Letter_Report.ReportSource = rptdoc;
                paramField.Name = "title";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                IM_Letter_Report.ParameterFieldInfo = paramFields;
                // rptdoc.SetParameterValue("tittle", paramDValue.Value, "View Details");
                IM_Letter_Report.EnableDatabaseLogonPrompt = false;
                IM_Letter_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                IM_Letter_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
            }
            catch (CrystalReportsException ex)
            {
                // Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                IM_Letter_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (SqlException ex)
            {
                IM_Letter_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (ArgumentNullException ex)
            {
                IM_Letter_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
        }
        else
        {
            IM_Letter_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        IM_Letter_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}