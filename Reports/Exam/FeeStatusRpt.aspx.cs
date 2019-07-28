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
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using MasterLibrary;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.Runtime.InteropServices;



public partial class Reports_AC_FeeStatusRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
            if (!IsPostBack)
            {
                lblIMID.Visible = false;
                txtIMID.Visible = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();

                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                FeeStatusForm.Visible = false;
            }
            if (!IsPostBack && !IsCallback)
            {
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
       
        SqlCommand cmd = new SqlCommand();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            con.Open();
            string qry = "";
            if (ddlSelect.SelectedValue.ToString() == "All")
            {
                rptTitle = "Fee Type:"+ddlAppType.SelectedItem.Text;
                qry = "select * from AppRecord where FeeType='"+ddlAppType.SelectedValue+"' and Session ='" + lblHiddenSeason.Text.ToString() +"' and Status like '%no%'  order by SN Desc";
            }
            else if (ddlSelect.SelectedValue.ToString() == "IMID")
            {
                rptTitle = "Fee Type:"+ddlAppType.SelectedItem.Text+ " and IMID:"+txtIMID.Text;
                qry = "select * from AppRecord where FeeType='" + ddlAppType.SelectedValue + "' and Session ='" + lblHiddenSeason.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and Status like '%no%' order by SN Desc";
            }
            else
            {
                rptTitle = "Fee Type:" + ddlAppType.SelectedItem.Text+" and SID:"+txtIMID.Text;
                qry = "select * from AppRecord where FeeType='" + ddlAppType.SelectedValue + "' and Session ='" + lblHiddenSeason.Text.ToString() + "' and Enrolment='" + txtIMID.Text.ToString() + "' and Status like '%no%' order by SN Desc";
            }
            ds = new DataSet();
            adapter = new SqlDataAdapter(qry, con);
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            lblExceptioN.Text = "Please select right date format.";
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
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();

                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("FeeStatusCrt.rpt"));
                rptdoc.SetDataSource(dt);
              FeeStatusForm.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = rptTitle;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
              FeeStatusForm.ParameterFieldInfo = paramFields;
              FeeStatusForm.EnableDatabaseLogonPrompt = false;
             FeeStatusForm .EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
               FeeStatusForm. Visible = false;
                lblExceptioN.Text = "";
            }
            catch (CrystalReportsException ex)
            {
                FeeStatusForm.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                FeeStatusForm.Visible = false;
               // lblExceptioN.Text = "Null Value .";
            }
            catch (SqlException ex)
            {
                FeeStatusForm.Visible = false;
               // lblExceptioN.Text = "Null Value .";
            }
            catch (ArgumentNullException ex)
            {
                FeeStatusForm.Visible = false;
              //  lblExceptioN.Text = "Null Value .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }

        }
        else
        {
          FeeStatusForm .ReportSource = (ReportDocument)Session["cr"];
        }
    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblIMID.Visible = false;
        txtIMID.Visible = false;
        
        if (ddlSelect.SelectedItem.Text == "SID")
        {
            lblIMID.Text= "Student ID";

            lblIMID.Visible = true;
            txtIMID.Visible = true;

        }
        if (ddlSelect.SelectedItem.Text == "IMID")
        {
            lblIMID.Text = "IMID";

            lblIMID.Visible = true;
            txtIMID.Visible = true;

        }
    }
    protected void FeeStatusForm_Init(object sender, EventArgs e)
    {

    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
      FeeStatusForm.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.FeeStatusForm.Dispose();
        this.FeeStatusForm = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
}