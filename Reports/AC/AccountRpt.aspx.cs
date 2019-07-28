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


public partial class Reports_AC_AccountRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlCommand cmd;

    static string rptTitle;
    ReportDocument rptdoc;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    txtSession.Text = DateTime.Now.Year.ToString();
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum";
                    else ddlsession.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
                    lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                }
                if (!IsPostBack && !IsCallback)
                {
                    Session["cr"] = null;
                    CrystalReportViewer1.Visible = false;
                    rbtnlstSelect.SelectedValue = "IMID";
                    txtIMID.Focus();
                    pnlDiary.Visible = false;
                    rptTitle = "";
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
        SqlDataAdapter adapter;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            cmd = new SqlCommand();
         
            if (rbtnlstSelect.SelectedValue == "Diary")
            {
                rptTitle = "Diary No:" + txtDiary.Text.ToString();
             
                cmd.CommandText = "SELECT IMAC.*, Account.* FROM IMAC LEFT JOIN Account ON IMAC.IMID = Account.IMID where Account.DiaryNo='" + txtDiary.Text + "' order by Account.SN Desc";
            }
            else if (rbtnlstSelect.SelectedValue == "IMID")
            {
                rptTitle = "IMID:" + txtIMID.Text.ToString() + ",Session:" + lblhiddenSession.Text.ToString();
                cmd.CommandText = "SELECT IMAC.*, Account.* FROM IMAC LEFT JOIN Account ON IMAC.IMID = Account.IMID where IMAC.IMID='" + txtIMID.Text.ToString() + "' and Account.Session='" + lblhiddenSession.Text.ToString() + "'  order by Account.SN Desc";
            }
            else if (rbtnlstSelect.SelectedValue == "Group ID")
            {
                rptTitle = "Group ID:" + txtIMID.Text.ToString() + ",Session:" + lblhiddenSession.Text.ToString();
              
                cmd.CommandText = "SELECT IMAC.*, Account.* FROM IMAC LEFT JOIN Account ON IMAC.IMID = Account.IMID where IMAC.GID='" + txtIMID.Text.ToString() + "' and Account.Session='" + lblhiddenSession.Text.ToString() + "'  order by Account.SN Desc";
            
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
            rptdoc.Load(Server.MapPath("AccountCrt.rpt"));
            rptdoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rptdoc;
            paramField.Name = "tittle";
            paramDValue.Value = rptTitle;
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            CrystalReportViewer1.ParameterFieldInfo = paramFields;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["cr"] = rptdoc;
        }
        catch (NullReferenceException ex)
        {
            CrystalReportViewer1.Visible = false;           

        }
        catch (CrystalReportsException ex)
        {
            CrystalReportViewer1.Visible = false;          
        }
        catch (IndexOutOfRangeException ex)
        {
            CrystalReportViewer1.Visible = false;
           
        }
        catch (SqlException ex)
        {
            CrystalReportViewer1.Visible = false;           
        }
        catch (ArgumentNullException ex)
        {
            CrystalReportViewer1.Visible = false;           
        }
        catch (COMException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    else
    {
        CrystalReportViewer1.ReportSource = (ReportDocument)Session["cr"];
    }
}

    protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlstSelect.SelectedValue == "Diary")
        {
            pnlDiary.Visible = true; pnlIM.Visible = false;
            txtDiary.Focus();
        }
        else if (rbtnlstSelect.SelectedValue == "IMID")
        {
            pnlIM.Visible = true; pnlDiary.Visible = false;
            txtIMID.Text = "";
            txtIMID.Focus();
        }
        else if (rbtnlstSelect.SelectedValue == "Group ID")
        {
            
            pnlIM.Visible = true; pnlDiary.Visible = false;
            txtIMID.Text = "";
            txtIMID.Focus();
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
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        CrystalReportViewer1.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}
