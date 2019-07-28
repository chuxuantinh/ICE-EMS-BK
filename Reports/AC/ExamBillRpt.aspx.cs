using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
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

public partial class Reports_AC_ExamBillRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlCommand cmd;

    static string rptTitle;
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
                ExamBill_Report.Visible = false;
                rbtnlstSelect.SelectedValue = "Session";
                pnlBillType.Visible = false;
                rptTitle = "";
                txtSession.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";// lblFromName.Text = "Membership No:"; 
                lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();

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

    protected void Page_Unload(object sender, EventArgs e)
    {
        try
        {
            rptdoc.Close();
            rptdoc.Dispose();
        }
        catch (NullReferenceException ex)
        {


        }
    } 

public DataTable getdata()
    {
        SqlDataAdapter adapter;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            cmd = new SqlCommand();
          

 if (rbtnlstSelect.SelectedValue == "Session")
            {
                rptTitle = "Session:" + ddlsession.SelectedValue + txtSession.Text.ToString();
              
                cmd.CommandText = "SELECT * from ExamBill where ExamSeason='" +ddlsession.SelectedValue +""+ txtSession.Text+"'";
            }
            else if (rbtnlstSelect.SelectedValue == "Bill Type")
            {
                rptTitle = "Bill Type:" + ddlBillingType.SelectedItem.Text + " and Session:" + ddlsession.SelectedValue + "" + txtSession.Text;
                cmd.CommandText = "SELECT * from ExamBill where BillType='" + ddlBillingType.SelectedItem.Text + "' and ExamSeason='" + ddlsession.SelectedValue + "" + txtSession.Text + "'";
            }
           
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
           // throw new Exception(ex.Message);
        }
        finally
        {
           
            
        }
        return ds.Tables[0];
    }
protected void LoadReport(ReportState rptState)
{
    if (rptState != ReportState.FromPostBack)
    {
        try
        {
           rptdoc = new ReportDocument();
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
            rptTitle = "Hello World ";
            DataTable dt = new DataTable();
            dt = getdata();
            ds.Tables[0].Merge(dt);
            rptdoc.Load(Server.MapPath("ExamBillCrt.rpt"));
            rptdoc.SetDataSource(dt);
            ExamBill_Report.ReportSource = rptdoc;
            paramField.Name = "tittle";
            paramDValue.Value = rptTitle;
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            ExamBill_Report.ParameterFieldInfo = paramFields;
            ExamBill_Report.EnableDatabaseLogonPrompt = false;
            ExamBill_Report.EnableParameterPrompt = false;
            Session["cr"] = rptdoc;
        }
        catch (NullReferenceException ex)
        {
            ExamBill_Report.Visible = false;          
        }
        catch (CrystalReportsException ex)
        {
            ExamBill_Report.Visible = false;           
        }
        catch (IndexOutOfRangeException ex)
        {
            ExamBill_Report.Visible = false;            
        }
        catch (SqlException ex)
        {
            ExamBill_Report.Visible = false;          
        }
        catch (ArgumentNullException ex)
        {
            ExamBill_Report.Visible = false;          
        }
        catch (COMException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    else
    {
        ExamBill_Report.ReportSource = (ReportDocument)Session["cr"];
    }
}

    protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (rbtnlstSelect.SelectedItem.Text=="Session")
        {
            pnlBillType.Visible = false;
            pnlSession.Visible = true;           
        }
       if (rbtnlstSelect.SelectedItem.Text == "Bill Type")
        {
            pnlBillType.Visible = true;
         //   pnlSession.Visible = false;
        }
       
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }

    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        ExamBill_Report.Visible = true;

        LoadReport(ReportState.FromStart);
       
    }


    protected void ExamBill_Report_Init(object sender, EventArgs e)
    {

    }
}