using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
using System.Runtime.InteropServices;


public partial class Reports_AC_MainACRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
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
                MainAc_Report .Visible = false;
                rbtnlstSelect.SelectedValue = "IMID";
                pnlDiary.Visible = false;
            //    rptTitle = "";
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
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
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            cmd = new SqlCommand();       
            if (rbtnlstSelect.SelectedValue == "Diary")
            {
                 paramDValue.Value  = "Diary No.:" + txtDiary.Text.ToString();
                cmd.CommandText = "SELECT FeeAC.*, IMAC.* FROM IMAC inner JOIN FeeAc ON IMAC.IMID = FeeAC.IMID and FeeAC.DiaryNo='"+txtDiary.Text+"' and AmtFor='"+ddlAmtForMs.SelectedValue.ToString()+"'";
            }
            else if (rbtnlstSelect.SelectedValue == "IMID")
            {
                paramDValue.Value  = "IMID:" + txtIMID.Text.ToString() + " Session:" + lblhiddenSession.Text.ToString();
                cmd.CommandText = "SELECT FeeAC.*, IMAC.* FROM IMAC inner JOIN FeeAC ON IMAC.IMID = FeeAC.IMID where IMAC.IMID='" + txtIMID.Text.ToString() + "' and FeeAC.Session='" + lblhiddenSession.Text.ToString() + "' and AmtFor='" + ddlAmtForMs.SelectedValue.ToString() + "'";
            }
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {         
            
           
        }
        return ds.Tables[0];
    }


    public void LoadReport(ReportState rptState)
    {
        ReportDocument rptdoc = new ReportDocument();
        ParameterField paramField = new ParameterField();
        ParameterFields paramFields = new ParameterFields();
        if (rptState != ReportState.FromPostBack)
        {
            try
            {
                MainAc_Report.Visible = true;
               //  rptTitle = " ";
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("MainACCrt.rpt"));
                rptdoc.SetDataSource(dt);
                MainAc_Report.ReportSource = rptdoc;
                paramField.Name = "title2";
              //  paramDValue.Value = ddlAmtForMs.SelectedValue.ToString() ;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                MainAc_Report.ParameterFieldInfo = paramFields;
                 MainAc_Report.ParameterFieldInfo = paramFields;
                 MainAc_Report.EnableDatabaseLogonPrompt = false;
                 MainAc_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
              //  rptdoc.PrintToPrinter(1, false, 0, 0);

            }

             catch (NullReferenceException ex)
            {
                MainAc_Report.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                MainAc_Report.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                MainAc_Report.Visible = false;
            }
            catch (SqlException ex)
            {
                MainAc_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                MainAc_Report.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            MainAc_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    
        
    }
        


    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        MainAc_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        txtDiary.Text = "";
        MainAc_Report.Visible = false;
        if (rbtnlstSelect.SelectedValue == "Diary")
        {
            pnlDiary.Visible = true; pnlIM.Visible = false;
        }
        else if (rbtnlstSelect.SelectedValue == "IMID")
        {
            pnlIM.Visible = true; pnlDiary.Visible = false;
        }
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void btnPrint_click(object sender, EventArgs e)
    {
      
    }
}