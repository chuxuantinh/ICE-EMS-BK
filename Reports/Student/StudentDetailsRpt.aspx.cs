using System;
using System.Collections.Generic;
using System.Linq;
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

public partial class Reports_Student_StudentDetailsRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
     DataSet ds = null;
    DateTimeFormatInfo dtinfo;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
    string str;

  
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
                panDate.Visible = true;
               panSession.Visible = false;                      
                Student_Details_Report.Visible = false;             
                lblImid.Visible = false;
                lblSID.Visible = false;
                txtIMID.Visible = false;                         
                rblICE.SelectedValue = "ICE";           
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
         dtinfo = new DateTimeFormatInfo();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";

        try
        {
            con.Open();  
            if (rblICE.SelectedValue == "Date")
            {
                paramDValue.Value = "Enroll Date: From " +txtDateFrom.Text + " To " + txtDateto.Text +" and Status:"+ddlStatus.SelectedValue.ToString();
                cmd.CommandText = "select * from Student where EnrollDate Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "' and Status='"+ddlStatus.SelectedValue.ToString()+"' order by SID";
            }
            else if (rblICE.SelectedValue == "SID")
            {
                paramDValue.Value = "SID: From " + txtYear.Text + " and "+txtIMID.Text+" ,Status:" + ddlStatus.SelectedValue.ToString();
                cmd.CommandText = "select * from Student where SID between '" + txtYear.Text + "' and '" + txtIMID.Text + "' and Status='" + ddlStatus.SelectedValue.ToString() + "' order by SID";
            }
            else if (rblICE.SelectedValue == "IMID")
            {

                paramDValue.Value = "IMID " + txtIMID.Text;
                cmd.CommandText = "select * from Student where IMID = '"+ txtIMID.Text + "' order by SID";
            }         

            cmd.Connection = con;         
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
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
            Student_Details_Report.Visible = true;          
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();          
            DataTable dt = new DataTable();
            dt = getdata();
            ds.Tables[0].Merge(dt);           
            rptdoc.Load(Server.MapPath("StudentDetailsCrt.rpt"));
            rptdoc.SetDataSource(dt);
            Student_Details_Report.ReportSource = rptdoc;
            ds.Dispose();           
            paramField.Name = "tittle";          
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            Student_Details_Report.ParameterFieldInfo = paramFields;
            Student_Details_Report.EnableDatabaseLogonPrompt = false;
            Student_Details_Report.EnableParameterPrompt = false;
            Session["cr"] = rptdoc;
        }
        catch (NullReferenceException ex)
        {
            Student_Details_Report.Visible = false;
          
        }
        catch (CrystalReportsException ex)
        {
            Student_Details_Report.Visible = false;
           
        }
        catch (IndexOutOfRangeException ex)
        {
            Student_Details_Report.Visible = false;
          
        }
        catch (ArgumentNullException ex)
        {
            Student_Details_Report.Visible = false;
            
        }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Student_Details_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }



    protected void btnView_Click(object sender, EventArgs e)
    {
        Student_Details_Report.Visible = true;
        LoadReport(ReportState.FromStart);
        
    }
    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {             
        panDate.Visible = true;
        txtDateFrom.Visible = true;
        txtDateto.Visible = true;
        txtYear.Visible = true;
        lblSID.Visible = false;
        lblStatus.Visible = true;
        ddlStatus.Visible = true;
        
        if (rblICE.SelectedValue == "Date")
        {
            panSession.Visible = false;
        }
        if (rblICE.SelectedValue == "SID")
        {          
            panDate.Visible = false;                      
           panSession.Visible = true;
          //  txtIMID.Focus();
            lblSID.Visible = true;
           lblImid.Visible = true;
           lblImid.Text = "To:";
            txtIMID.Visible = true;
            txtIMID.Text = "";
            txtYear.Text = "";

        }
        if (rblICE.SelectedValue == "IMID")
        {
            panDate.Visible = false;
            panSession.Visible = true;
            txtYear.Visible = false;
            txtIMID.Focus();            
              lblImid.Visible = true;
              lblImid.Text = "IMID";
            txtIMID.Visible = true;
            lblStatus.Visible = false;
            ddlStatus.Visible = false;
            txtIMID.Text = "";

        }
        Student_Details_Report.Visible = false;
      
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Student_Details_Report.Dispose();
        this.Student_Details_Report = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();
    }
    protected void Student_Details_Report_Init(object sender, EventArgs e)
    {

    }
}