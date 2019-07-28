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

public partial class Reports_FO_CourierRpt : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    static string rptTitle;
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
                CourierReport.Visible = false;
                rbtnlstSelect.SelectedValue = "ICE";
                pnlDairy.Visible = false;
                txtIMID.Visible = false;
                txtIMID.Text = "Insert IMID Here";
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

            if (!IsPostBack)
            {

            }
        
               
    }
    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            if (rbtnlstSelect.SelectedValue == "ICE")
            {
                rptTitle = "Date From: " + txtDateFrom.Text.ToString() + " To: " + txtDateto.Text.ToString();
                cmd.CommandText = "select * from CourierRD where Date Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "'";
            }
            else if (rbtnlstSelect.SelectedValue == "IM")
            {
                rptTitle = "IM Membership No.: " + txtIMID.Text.ToString() + " and Date: " + txtDateFrom.Text.ToString() + " To " + txtDateto.Text.ToString();
                cmd.CommandText = "select * from CourierRD where Date Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "' and Name like '%" + txtIMID.Text.ToString() + "%'";
            }
            else if (rbtnlstSelect.SelectedValue == "Courier")
            {
                rptTitle = "Courier No. " + txtDNo.Text.ToString();
                cmd.CommandText = "select * from CourierRD where CourierSerialno='" + txtDNo.Text.ToString() + "'";
            }

            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            cmd.Dispose();
            con.Close();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            lblExceptioN.Text = "";
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
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();

                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("CourierCrt.rpt"));
                rptdoc.SetDataSource(dt);
                CourierReport.ReportSource = rptdoc;
                paramField.Name = "RptTitle";
                paramDValue.Value = "Courier Dispatch Details";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                CourierReport.ParameterFieldInfo = paramFields;

                CourierReport.EnableDatabaseLogonPrompt = false;
                CourierReport.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                //  lblExceptioN.Text = "Null Date .";
            }
        }
        else
        {
            CourierReport.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        CourierReport.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlstSelect.SelectedValue == "ICE")
        {
            pnlDate.Visible = true;
            pnlDairy.Visible = false;
            txtIMID.Visible = false;
        }
        else if (rbtnlstSelect.SelectedValue == "IM")
        {
            pnlDate.Visible = true;
            pnlDairy.Visible = false;
            txtIMID.Visible = true;
        }
        else if (rbtnlstSelect.SelectedValue == "Courier")
        {
            pnlDairy.Visible = true;
            pnlDate.Visible = false;
        }
    }
}
