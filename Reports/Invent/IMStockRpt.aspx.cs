using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using System.Configuration;
using MasterLibrary;
using System.Globalization;
using System.Runtime.InteropServices;

public partial class Reports_Invent_IMStockRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
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
                panIMID.Visible = false;
                rblICE.SelectedValue = "ALL";
                rblICE.Focus();
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
            if (rblICE.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "SELECT * FROM IMStock where Stock>0 order by IMID";
                paramDValue.Value = "ALL IM Stock List Details";
            }
            if (rblICE.SelectedItem.Text == "IMID")
            {
                cmd.CommandText = "SELECT * FROM IMStock where IMID='"+txtIMID.Text+"' and Stock>0";
                paramDValue.Value = "IM Stock List Detail ,Membership ID:" + txtIMID.Text;
            }
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
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
                rptdoc.Load(Server.MapPath("IMStockCrt.rpt"));
                rptdoc.SetDataSource(dt);
                IMStockList_Details_Report.ReportSource = rptdoc;
                ds.Dispose();
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                IMStockList_Details_Report.ParameterFieldInfo = paramFields;
                IMStockList_Details_Report.EnableDatabaseLogonPrompt = false;
                IMStockList_Details_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                IMStockList_Details_Report.Visible = false;

            }
            catch (CrystalReportsException ex)
            {
                IMStockList_Details_Report.Visible = false;

            }
            catch (IndexOutOfRangeException ex)
            {
                IMStockList_Details_Report.Visible = false;
            }
            catch (SqlException ex)
            {
                IMStockList_Details_Report.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                IMStockList_Details_Report.Visible = false;
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            IMStockList_Details_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        IMStockList_Details_Report.Visible = true;
        LoadReport(ReportState.FromStart);
        txtIMID.Text = "";
    }


    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        IMStockList_Details_Report.Visible = false;
        panIMID.Visible = false;
        if (rblICE.SelectedValue == "IMID")
        {
            txtIMID.Focus();
            panIMID.Visible = true;

        }
    }
}