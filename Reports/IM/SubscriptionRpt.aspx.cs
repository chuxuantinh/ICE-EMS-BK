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

public partial class Reports_IM_SubscriptionRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string tit;
    string str;
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();   
    ParameterDiscreteValue paramDValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        try{
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
            Subscription_Details_Report.Visible = false;        
 
            LoadReport(ReportState.FromStart);
            ddlSubscription.Focus();
        
        }
        else
        {
           
            LoadReport(ReportState.FromPostBack);
            ddlSubscription.Focus();
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
            if (ddlSubscription.SelectedItem.Text  == "Subscription Dues")
            {
                cmd.CommandText = "select * from Member where RenewDate<='" + Convert.ToDateTime(txtDateFrom.Text, dtinfo).AddMonths(3) + "'";
                paramDValue.Value = "Subscription Dues On:" + Convert.ToDateTime(txtDateFrom.Text, dtinfo).AddMonths(3).ToString("dd/MMM/yyyy");
            }
            else if (ddlSubscription.SelectedItem.Text  == "Subscription Expired")
            {
                cmd.CommandText = "select * from Member where ExpDate<='" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "'";
                paramDValue.Value = "Subscription Expired before:" + Convert.ToDateTime(txtDateFrom.Text, dtinfo).ToString("dd/MMM/yyyy");
            }
            else if (ddlSubscription.SelectedItem.Text == "Subscription Required")
            {
                cmd.CommandText = "select * from Member where RenewDate<='" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "'";
                paramDValue.Value = "Subscription Required On:" + Convert.ToDateTime(txtDateFrom.Text, dtinfo).ToString("dd/MMM/yyyy");
            }
          
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;         
            return ds.Tables[0];
        }
        catch (NullReferenceException Ex)
        {
            Subscription_Details_Report.Visible = false;
            return null;
        }
        catch (Exception ex)
        {
            Subscription_Details_Report.Visible = false;
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
                rptdoc.Load(Server.MapPath("SubscriptionCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Subscription_Details_Report.ReportSource = rptdoc;
                ds.Dispose();
                Subscription_Details_Report.EnableDatabaseLogonPrompt = false;
                Subscription_Details_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Subscription_Details_Report.ParameterFieldInfo = paramFields;
                Subscription_Details_Report.EnableDatabaseLogonPrompt = false;
                Subscription_Details_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Subscription_Details_Report.Visible = false;
               
            }
            catch (CrystalReportsException ex)
            {
                Subscription_Details_Report.Visible = false;
               
               
            }
            catch (IndexOutOfRangeException ex)
            {
                Subscription_Details_Report.Visible = false;
              
            }
            catch (SqlException ex)
            {
                Subscription_Details_Report.Visible = false;
              
            }
            catch (ArgumentNullException ex)
            {
                Subscription_Details_Report.Visible = false;
               
            }
        }
        else
        {
            Subscription_Details_Report.ReportSource = (ReportDocument)Session["cr"];
        }
       

    }


  protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
  {
     
  }


 protected void btnView_Click(object sender, EventArgs e)
    {
        Subscription_Details_Report.Visible = true;
        LoadReport(ReportState.FromStart);      
        ddlSubscription.Focus();
       
     
    }

 protected void txtDateFrom_TextChanged(object sender, EventArgs e)
 {
     btnView.Focus();
 }
}