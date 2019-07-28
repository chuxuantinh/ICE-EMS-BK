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

public partial class Reports_IM_IMProfileRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string tit;
    string str;
    SqlCommand cmd;
    SqlDataAdapter adapter;
    ParameterDiscreteValue paramDValue;
    ReportDocument rptdoc;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
        finally
        {
        }
        if (!IsPostBack && !IsCallback)
        {
            Session["cr"] = null;
             cmd = new SqlCommand();      
            cmd.CommandText = "select distinct Member.LName from Member";        
          cmd.Connection = con;       
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            ddlCity.DataSource=ds.Tables[0];           
            ddlCity.DataTextField  = "LName";
            ddlCity.DataValueField = "LName";
            ddlCity.DataBind();
            cmd.Dispose();         
            panStatus.Visible = false;
            lblName.Visible = false;
             txtName.Visible = false;
             lblImid.Visible = true;
             txtIMID.Visible = true;
            IMProfileReport.Visible = false;        
            rblICE.SelectedValue = "IMID";
            txtIMID.Focus();
            ds.Dispose();       
            lblCity.Visible = false;
            ddlCity.Visible = false;          
            LoadReport(ReportState.FromStart);
           
        }
        else
        {
            
            LoadReport(ReportState.FromPostBack);
        }
    }

   


    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;

        try
        {
         
            if (rblICE.SelectedValue == "IMID")
            {
                cmd.CommandText =  "select Member.*, IMInspection.* from Member Left Outer Join IMInspection on Member.ID= IMInspection.ID where Member.ID  ='" + txtIMID.Text+ "'";
                paramDValue.Value = "IMID:" + txtIMID.Text;
                txtIMID.Focus();
            }
            else if (rblICE.SelectedValue == "Status")
            {

                cmd.CommandText = "select  Member.*, IMInspection.* from Member Left Outer Join IMInspection on Member.ID = IMInspection.ID where  IMInspection.Status='" + ddlStatus.SelectedValue.ToString() + "'";
                paramDValue.Value = "Status:" + ddlStatus.SelectedItem.Text;
                ddlStatus.Focus();
            }
            else if (rblICE.SelectedValue == "Name" )
            {
                cmd.CommandText = "select  Member.*, IMInspection.* from Member Left Outer Join IMInspection  on Member.ID = IMInspection.ID where Member.Name='" + txtName.Text + "'";
                paramDValue.Value = "Name:" + txtName.Text;
                txtName.Focus();

            }
            if (rblICE.SelectedValue == "City")
            {
                cmd.CommandText = "select Member.*, IMInspection.* from Member left Outer Join IMInspection on  Member.ID= IMInspection.ID where Member.LName ='"+ddlCity.SelectedItem.Text+"'";
                paramDValue.Value = "City:" + ddlCity.SelectedItem.Text;
                ddlCity.Focus();
            }

            cmd.Connection = con;         
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;                     
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

        public void LoadReport(ReportState rptState)
    {
        if (rptState != ReportState.FromPostBack)
        {
            try
            {
               

                rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue(); 
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);              
                rptdoc.Load(Server.MapPath("IMProfileCrt.rpt"));
                rptdoc.SetDataSource(dt);
                IMProfileReport.ReportSource = rptdoc;
                ds.Dispose();
                IMProfileReport.EnableDatabaseLogonPrompt = false;
                IMProfileReport.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                IMProfileReport.ParameterFieldInfo = paramFields;
                IMProfileReport.EnableDatabaseLogonPrompt = false;
                IMProfileReport.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                IMProfileReport.Visible = false;
               
            }
            catch (CrystalReportsException ex)
            {
                IMProfileReport.Visible = false;
             
            }
            catch (IndexOutOfRangeException ex)
            {
                IMProfileReport.Visible = false;
              
            }
            catch (SqlException ex)
            {
                IMProfileReport.Visible = false;
               
            }
            catch (ArgumentNullException ex)
            {
                IMProfileReport.Visible = false;
               
            }
        }
        else
        {
            IMProfileReport.ReportSource = (ReportDocument)Session["cr"];
        }
       

    }
        


    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        IMProfileReport.Visible = false;
        lblImid.Visible = false;
        txtIMID.Visible = false;
        txtName.Visible = false;
       lblName.Visible = false;
       panStatus.Visible = false;   
       lblCity.Visible = false;
       ddlCity.Visible = false;
        if (rblICE.SelectedValue == "IMID")
        {
            txtIMID.Focus();
            lblImid.Visible = true;
            txtIMID.Visible = true;
        }
        if (rblICE.SelectedValue == "Name")
        {
            txtName.Focus();
           lblName.Visible = true; 
         txtName .Visible = true;

        }
        if (rblICE.SelectedValue == "Status")
        {
            panStatus.Visible = true;
            ddlStatus.Focus();
           
        }
        if (rblICE.SelectedValue == "City")
        {        
            lblCity.Visible = true;
            ddlCity.Visible = true;
            ddlCity.Focus();
        }


    }
         protected void btnView_Click(object sender, EventArgs e)
    {
        IMProfileReport.Visible = true;
        LoadReport(ReportState.FromStart);
     
    }

         protected void IMProfileReport_Init(object sender, EventArgs e)
         {

         }
         protected void CrystalReportViewer1_Init(object sender, EventArgs e)
         {

         }
}