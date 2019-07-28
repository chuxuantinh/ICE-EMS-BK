using System;
using System.Collections.Generic;
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

public partial class Reports_Student_StuExpRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
    ParameterField paramField;
    ParameterFields paramFields = new ParameterFields();
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
                txtYear.Text = DateTime.Now.Year.ToString();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
                Session["cr"] = null;
                txtIMID.Visible = false;
                lblIMID.Visible = false;
                Stu_Exp_Report.Visible = false;
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
            if (ddlselect.SelectedValue == "ALL")
            {
                if (ddlType.SelectedValue == "ExpStatus")
                {
                    cmd.CommandText = "SELECT * from student where ExpStatus='" + ddlMembershipGtd.SelectedValue + "' and Session='" + ddlSession.SelectedValue + txtYear.Text + "'  order by Student.SID";
                    paramDValue.Value = "Session:" + ddlSession.SelectedValue + txtYear.Text + " and Exp Status:" + ddlMembershipGtd.SelectedValue;
                }
                else if (ddlType.SelectedValue == "DocStatus")
                {
                    cmd.CommandText = "SELECT * from student where DocStatus='" + ddlMembershipGtd.SelectedValue + "' and Session='" + ddlSession.SelectedValue + txtYear.Text + "'  order by Student.SID";
                    paramDValue.Value = "Session:" + ddlSession.SelectedValue + txtYear.Text + " and Doc Status:" + ddlMembershipGtd.SelectedValue;
                }
            }
            else
            {
                if (ddlType.SelectedValue == "ExpStatus")
                {
                    cmd.CommandText = "SELECT * from student where ExpStatus='" + ddlMembershipGtd.SelectedValue + "' and Session='" + ddlSession.SelectedValue + txtYear.Text + "' and IMID='"+txtIMID.Text+"' order by Student.SID";
                    paramDValue.Value = "Session:" + ddlSession.SelectedValue + txtYear.Text + " , Exp Status:" + ddlMembershipGtd.SelectedValue+" and IMID:"+txtIMID.Text;

                }
                else if (ddlType.SelectedValue == "DocStatus")
                {
                    cmd.CommandText = "SELECT * from student where DocStatus='" + ddlMembershipGtd.SelectedValue + "' and Session='" + ddlSession.SelectedValue + txtYear.Text + "' and IMID='"+txtIMID.Text+"' order by Student.SID";
                    paramDValue.Value = "Session:" + ddlSession.SelectedValue + txtYear.Text + " and Doc Status:" + ddlMembershipGtd.SelectedValue + " and IMID:" + txtIMID.Text;
                }
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
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("StuExperience.rpt"));
                rptdoc.SetDataSource(dt);
                Stu_Exp_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Stu_Exp_Report.ParameterFieldInfo = paramFields;
                Stu_Exp_Report.EnableDatabaseLogonPrompt = false;
                Stu_Exp_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Stu_Exp_Report.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                Stu_Exp_Report.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                Stu_Exp_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                Stu_Exp_Report.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Stu_Exp_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Stu_Exp_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }

   
    protected void ddlselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        txtIMID.Visible=false;
    lblIMID.Visible = false;
        if (ddlselect.SelectedValue == "IMID")
        {
            txtIMID.Visible=true;
            lblIMID.Visible = true;
        }

    }
}