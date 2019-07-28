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

public partial class Reports_AC_MemberFees : System.Web.UI.Page
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

            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;
                MemberFees_Report.Visible = false;               
                txtIMID.Focus();            
                rptTitle = "Report";            
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
            rptTitle = "Report";
            DataTable dt = new DataTable();
            dt = getdata();
            ds.Tables[0].Merge(dt);
            rptdoc.Load(Server.MapPath("MemberFeesCrt.rpt"));
            rptdoc.SetDataSource(dt);
            MemberFees_Report.ReportSource = rptdoc;
            paramField.Name = "tittle";
            paramDValue.Value = rptTitle;
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            MemberFees_Report.ParameterFieldInfo = paramFields;
            MemberFees_Report.EnableDatabaseLogonPrompt = false;
            MemberFees_Report.EnableParameterPrompt = false;
            Session["cr"] = rptdoc;
        }
        catch (NullReferenceException ex)
        {
            MemberFees_Report.Visible = false;
            
        }
        catch (CrystalReportsException ex)
        {
            MemberFees_Report.Visible = false;
           
        }
        catch (IndexOutOfRangeException ex)
        {
            MemberFees_Report.Visible = false;           
        }
        catch (SqlException ex)
        {
            MemberFees_Report.Visible = false;          
        }
        catch (ArgumentNullException ex)
        {
            MemberFees_Report.Visible = false;         
        }
        catch (COMException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    else
    {
        MemberFees_Report.ReportSource = (ReportDocument)Session["cr"];
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
            if (rbtnlstSelect.SelectedValue == "Group ID")
            {
                rptTitle = "Group ID:" + txtIMID.Text.ToString();
                cmd.CommandText = "SELECT MemberFee.*, IMAC.* FROM MemberFee LEFT JOIN IMAC ON IMAC.IMID = MemberFee.ID where IMAC.GID='" + txtIMID.Text.ToString() + "'";
            }
            else if (rbtnlstSelect.SelectedValue == "IMID")
            {
                rptTitle = "IMID:" + txtIMID.Text.ToString();
                cmd.CommandText = "SELECT  MemberFee.*, IMAC.* FROM IMAC LEFT JOIN  MemberFee ON IMAC.IMID = MemberFee.ID where MemberFee.ID='" + txtIMID.Text.ToString()+"'";// +"' and MemberFee.Session='" + ddlsession.SelectedValue.ToString() + txtSession.Text.ToString() + "'";
            }
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
          //  throw new Exception(ex.Message);
        }
        finally
        {
        }
        return ds.Tables[0];
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        MemberFees_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        if (rbtnlstSelect.SelectedValue == "IMID")
        {
            pnlIM.Visible = true;
            lblIMID.Text = "IMID:";
            txtIMID.Focus();
        }
        else if (rbtnlstSelect.SelectedValue == "Group ID")
        {
            pnlIM.Visible = true;
            lblIMID.Text = "Group ID:";
            txtIMID.Focus();
        }
    }
}