using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using MasterLibrary;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Data;


public partial class Reports_Project_SynopsisApprovalRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
             //   lblSessionHiddend.Text = ddlSession.SelectedValue + txtSession.Text;
                PanSession.Visible = false;
                PanStatus.Visible = false;
                PanStatus.Visible = true;
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
        SqlDataAdapter adapter;
        try
        {
            string qry = "";
            if (rbtnlstSelect.SelectedValue  == "Session")
            {
                paramDValue.Value = ddlSession.SelectedValue + txtSession.Text + "  " + ddlStatus.SelectedValue+" "+ddlEntryStatus.SelectedValue;
                if (ddlEntryStatus.SelectedValue == "ALL")
                {
                    qry = "select * from Project where Session='" + ddlSession.SelectedValue + txtSession.Text + "' and Status='" + ddlStatus.SelectedValue + "' order by GroupID,SID,ProjectNo";                  
                }
                else
                {
                    qry = "select * from Project where Session='" + ddlSession.SelectedValue + txtSession.Text + "' and EntryStatus='" + ddlEntryStatus.SelectedValue + "' and Status='" + ddlStatus.SelectedValue + "'  order by GroupID,SID,ProjectNo";
                }
            }
            else
            {
                paramDValue.Value = "Date:" + txtDate1.Text.ToString() + " To " + txtDate2.Text;
                if (ddlEntryStatus.SelectedValue == "ALL")
                {
                    qry = "select * from Project where SynopsisDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "'  order by GroupID,SID,ProjectNo";             
                }
                else
                {
                    qry = "select * from Project where EntryStatus='" + ddlEntryStatus.SelectedValue + "' and SynopsisDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "'  order by GroupID,SID,ProjectNo";
                }
            }
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
                 ds.Tables[0].Merge(dt);
                 rptdoc.Load(Server.MapPath("SynopsisApprovalCrt.rpt"));
                 rptdoc.SetDataSource(dt);
                 Project_Synopsis_Report.ReportSource = rptdoc;
                 paramField.Name = "title";
                 paramField.CurrentValues.Add(paramDValue);
                 paramField.HasCurrentValue = true;
                 paramFields.Add(paramField);
                 Project_Synopsis_Report.ParameterFieldInfo = paramFields;
                 // rptdoc.SetParameterValue("tittle", paramDValue.Value, "View Details");
                 Project_Synopsis_Report.EnableDatabaseLogonPrompt = false;
                 Project_Synopsis_Report.EnableParameterPrompt = false;
                 Session["cr"] = rptdoc;
             }
             catch (NullReferenceException ex)
             {
                 Project_Synopsis_Report.Visible = false;
             }
             catch (CrystalReportsException ex)
             {
                 Project_Synopsis_Report.Visible = false;                
             }
             catch (IndexOutOfRangeException ex)
             {
                 Project_Synopsis_Report.Visible = false;                
             }
             catch (SqlException ex)
             {
                 Project_Synopsis_Report.Visible = false;             
             }
             catch (ArgumentNullException ex)
             {
                 Project_Synopsis_Report.Visible = false;                
             }
         }
         else
         {
             Project_Synopsis_Report.ReportSource = (ReportDocument)Session["cr"];
         }
     }
     protected void btnView_Click(object sender, EventArgs e)
     {
         Project_Synopsis_Report.Visible = true;
         LoadReport(ReportState.FromStart);
     }
     protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
     {
         PanSession.Visible = false;
         PanStatus.Visible = true;
         ddlEntryStatus.SelectedValue = "Running";
         if (rbtnlstSelect.SelectedValue == "Session")
         {
             PanSession.Visible = true;
             panDate.Visible = false;
         }
         else
         {
             panDate.Visible = true;
         }
     }
}