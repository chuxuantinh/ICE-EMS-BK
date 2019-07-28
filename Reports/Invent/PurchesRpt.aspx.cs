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

public partial class Reports_Invent_PurchesRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
     DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };   
    SqlCommand cmd;
    SqlDataAdapter adapter;
    ParameterDiscreteValue paramDValue;
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
                txtSession.Text = DateTime.Now.Year.ToString();
                cmd = new SqlCommand();
                ddlSession.Focus();
                cmd.CommandText = "select Supplier from Purches";
                cmd.Connection = con;        
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                ddlName.DataSource = ds.Tables[0];
                ddlName.DataTextField = "Supplier";
                ddlName.DataValueField = "Supplier";
                ddlName.DataBind();                              
                Purchase_Order_Report.Visible = false;
                rblICE.SelectedValue = "Supplier";               
                panCity.Visible = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";            
                lblOrderNo.Visible = false;
                txtOrderNo.Visible = false;
                lblType.Visible = false;
                ddlOrderType.Visible = false;
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
         
            if (rblICE.SelectedValue == "Supplier")
            {
                cmd.CommandText = "select Purches.*, PurchesList.* from  Purches  Left Outer Join PurchesList on Purches.OrderNo= PurchesList.OrderNo where Purches.Supplier  ='" + ddlName.SelectedItem.Text + "' and Purches.Session ='" + ddlSession.SelectedValue + txtSession.Text + "'";
                paramDValue.Value = "Supplier:" +ddlName.SelectedItem.Text;             
            }
            else if (rblICE.SelectedValue == "OrderType")
            {

                cmd.CommandText = "select  Purches.*, PurchesList.* from Purches Left Outer Join PurchesList on Purches.OrderNo= PurchesList.OrderNo where Purches.OrderType  ='" + ddlOrderType.SelectedItem.Text + "' and Purches.Session ='" + ddlSession.SelectedValue + txtSession.Text + "'";
                paramDValue.Value = "Order Type:"+ddlOrderType.SelectedItem.Text;
             
            }
            else if (rblICE.SelectedValue == "OrderNo" )
            {
                cmd.CommandText = "select Purches.*, PurchesList.* from Purches Left Outer Join PurchesList on Purches.OrderNo= PurchesList.OrderNo where Purches.OrderNo  ='" +txtOrderNo.Text + "'";
                paramDValue.Value = "Order No"+txtOrderNo.Text;
             
            }
           
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;
            cmd.Dispose();         
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
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);              
                rptdoc.Load(Server.MapPath("PurchesCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Purchase_Order_Report.ReportSource = rptdoc;
                ds.Dispose();             
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Purchase_Order_Report.ParameterFieldInfo = paramFields;
                Purchase_Order_Report.EnableDatabaseLogonPrompt = false;
                Purchase_Order_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Purchase_Order_Report.Visible = false;               
            }
            catch (CrystalReportsException ex)
            {
                Purchase_Order_Report.Visible = false;               
            }
            catch (IndexOutOfRangeException ex)
            {
                Purchase_Order_Report.Visible = false;               
            }
            catch (SqlException ex)
            {
                Purchase_Order_Report.Visible = false;            
            }
            catch (ArgumentNullException ex)
            {
                Purchase_Order_Report.Visible = false;             
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Purchase_Order_Report.ReportSource = (ReportDocument)Session["cr"];
        }
       

    

    }
    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
      {
          Purchase_Order_Report.Visible = false;
          lblName.Visible = false;
          ddlName.Visible = false;
          lblOrderNo.Visible = false;
          txtOrderNo.Visible = false;
          lblType.Visible = false;
          ddlOrderType.Visible = false;
                         
          if (rblICE.SelectedValue == "Supplier")
          {              
              lblName.Visible = true;
              ddlName.Visible = true;             
          }
          if (rblICE.SelectedValue == "OrderType")
          {
              panSession.Visible = true;
              lblType.Visible = true;
              ddlOrderType.Visible = true;                        
          }
          if (rblICE.SelectedValue == "OrderNo")
          {
              lblOrderNo.Visible = true;
              txtOrderNo.Visible = true;
              panSession.Visible = false;             
          }
         

    }
    protected void Purchase_Order_Report_Init(object sender, EventArgs e)
    {

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Purchase_Order_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
}