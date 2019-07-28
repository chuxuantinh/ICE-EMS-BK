using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class Reports_FO_CourierServiceRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    private enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string strt;
    string str;
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

                txtYear.Text = DateTime.Now.Year.ToString();
                dispDepartmentName();

                CourierServiceReport.Visible = false;
                LoadReport(ReportState.FromStart);

                //  rblICE.SelectedValue = "ICE";

                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";

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
        ReportDocument RptDoc = new ReportDocument();
        if (RptDoc != null)
        {
            RptDoc.Close();
            RptDoc.Dispose();
        //    RptDoc = null;
          //  GC.Collect();

            CourierServiceReport.Dispose();
            CourierServiceReport = null;
        

        }
    }

    protected void dispDepartmentName()
    {
        string qry5 = "select Name from ServiceNameMaster where Type = 'Courier'";
        SqlDataAdapter adpt = new SqlDataAdapter(qry5, con);
  

        DataTable dt = new DataTable();

        adpt.Fill(dt);
        //    ddlOrgId.Items.Insert(0, new ListItem("select", "0"));
        ddlCourierService.DataSource = dt;

        ddlCourierService.DataBind();
        ddlCourierService.DataTextField = "Name";
        ddlCourierService.DataValueField = "Name";

        ddlCourierService.DataBind();

    }

    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;

        try
        {
            str = ddlSession.SelectedValue + txtYear.Text;
            strt = ddlCourierService.SelectedItem.Text;

            cmd.CommandText = "select DiaryEntry.* , DairyCount.* from DiaryEntry, DairyCount where DiaryEntry.CourierService='" + ddlCourierService.SelectedItem.Text + "' and  DiaryEntry.ExamSession ='" + ddlSession.SelectedValue + txtYear.Text + "' and DiaryEntry.DiaryNo= DairyCount.DairyNo";

            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;

            cmd.Dispose();
            con.Close();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            //   lblExceptioN.Text="Please select right date format.";
            return null;
        }
        finally
        {

        }

    }


 

          private void LoadReport(ReportState rptState)
    {
        if (rptState != ReportState.FromPostBack)
        {

           
        try
        {
      
           // CrystalReportViewer1.Visible = true;
            ReportDocument rptdoc = new ReportDocument();
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();

            DataTable dt = new DataTable();
            dt = getdata();
            ds.Tables[0].Merge(dt);
            rptdoc.Load(Server.MapPath("CourierServiceCrt.rpt"));
            rptdoc.SetDataSource(dt);
            CourierServiceReport.ReportSource = rptdoc;
            paramField.Name = "tittle";
            paramDValue.Value = "Courier Service:" + strt + " and Session:" + str ;
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            CourierServiceReport.ParameterFieldInfo = paramFields;

            CourierServiceReport.EnableDatabaseLogonPrompt = false;
            CourierServiceReport.EnableParameterPrompt = false;
            Session["cr"] = rptdoc;
        }

        catch (NullReferenceException ex)
        {
            //lblExceptioN.Text = "Null Date .";
        }
        catch (CrystalReportsException ex)
        {
            // Response.Write(ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            //  lblExceptioN.Text = "Null Date .";
        }
        catch (SqlException ex)
        {
            //  lblExceptioN.Text = "Null Date .";
        }
        catch (ArgumentNullException ex)
        {
            //  lblExceptioN.Text = "Null Date .";
        }
       
            }

        else
        {
            CourierServiceReport.ReportSource = (ReportDocument)Session["cr"];
        }
        
    }



       protected void btnView_Click(object sender, EventArgs e)
    {
        CourierServiceReport.Visible = true;
        LoadReport(ReportState.FromStart);

       }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}