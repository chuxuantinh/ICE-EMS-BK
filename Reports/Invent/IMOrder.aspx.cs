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

public partial class Reports_Invent_IMOrder : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
     DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ParameterDiscreteValue paramDValue;
      SqlCommand cmd;
   
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
                ddlSession.Visible = true;
                txtSession.Visible = true;
                panIMID.Visible = false;
                lblType.Visible = false;
                ddlType.Visible = false;
                lblOrderNo.Visible = false;
                txtOrderNo.Visible = false;
                txtSession.Text = DateTime.Now.Year.ToString();
                cmd = new SqlCommand();
                ddlSession.Focus();
                IMOrder_Report.Visible = false;           
                panCity.Visible = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";             
                lblOrderNo.Visible = false;
                txtOrderNo.Visible = false;            
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
            if (ddlOrder.SelectedItem.Text == "Session")
            {
                cmd.CommandText = "select IMOrder.*,IMOrderList.* from IMOrder LEFT OUTER JOIN IMOrderList ON IMOrder.OrderNo = IMOrderList.OrderNo where IMOrder.Session='" + ddlSession.SelectedValue + txtSession.Text + "'";
                paramDValue.Value = "IMID:" + txtIMID.Text + " and Session:" + ddlSession.SelectedValue + txtSession.Text;
            }
            if (ddlOrder.SelectedItem.Text == "IMID & Session")
            {
                cmd.CommandText = "select IMOrder.*,IMOrderList.* from IMOrder LEFT OUTER JOIN IMOrderList ON IMOrder.OrderNo = IMOrderList.OrderNo where IMOrder.IMID ='" + txtIMID.Text + "' and IMOrder.Session='" + ddlSession.SelectedValue + txtSession.Text+"'";
                paramDValue.Value = "IMID:" + txtIMID.Text + " and Session:" + ddlSession.SelectedValue + txtSession.Text;
            }
            if (ddlOrder.SelectedItem.Text == "IMID & Type & Session")
            {
                cmd.CommandText = "select IMOrder.*,IMOrderList.* from IMOrder LEFT OUTER JOIN IMOrderList ON IMOrder.OrderNo = IMOrderList.OrderNo where IMOrder.IMID ='" + txtIMID.Text + "' and IMOrder.Session='" + ddlSession.SelectedValue + txtSession.Text + "' and IMOrder.Type='" + ddlType.SelectedItem.Text + "'";
                paramDValue.Value = "IMID:" + txtIMID.Text + ",Session:" + ddlSession.SelectedValue + txtSession.Text+" and Type:"+ddlType.SelectedItem.Text;
            }
            if (ddlOrder.SelectedItem.Text == "Order No")
            {
                cmd.CommandText = "select IMOrder.*,IMOrderList.* from IMOrder LEFT OUTER JOIN IMOrderList ON IMOrder.OrderNo = IMOrderList.OrderNo where IMOrder.OrderNo='"+txtOrderNo.Text+"'";
                paramDValue.Value = "Order No:" + txtOrderNo.Text;
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
                rptdoc.Load(Server.MapPath("IMOrderCrt.rpt"));
                rptdoc.SetDataSource(dt);
                IMOrder_Report.ReportSource = rptdoc;
                ds.Dispose();             
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                IMOrder_Report.ParameterFieldInfo = paramFields;
                IMOrder_Report.EnableDatabaseLogonPrompt = false;
                IMOrder_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                IMOrder_Report.Visible = false;
                
            }
            catch (CrystalReportsException ex)
            {
                IMOrder_Report.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                IMOrder_Report.Visible = false;
                
            }
            catch (SqlException ex)
            {
                IMOrder_Report.Visible = false;
              
            }
            catch (ArgumentNullException ex)
            {
                IMOrder_Report.Visible = false;
                
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            IMOrder_Report.ReportSource = (ReportDocument)Session["cr"];
        }
       

    

    }
   protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
   {
       ddlSession.Visible = true;
       txtSession.Visible = true;
       panIMID.Visible = false;
       panSession.Visible = true;
       lblType.Visible = false;
       ddlType.Visible = false;
       lblOrderNo.Visible = false;
       txtOrderNo.Visible = false;
                   
       if (ddlOrder.SelectedItem.Text == "IMID & Session")
       {
           panIMID.Visible = true;
       }
       if (ddlOrder.SelectedItem.Text == "IMID & Type & Session")
       {
           panIMID.Visible = true;
           lblType.Visible = true;
           ddlType.Visible = true;
           panSession.Visible = true;           
       }
       if (ddlOrder.SelectedItem.Text == "Order No")
       {
           panSession.Visible = false;
           ddlSession.Visible = false;
           txtSession.Visible = false;
           lblOrderNo.Visible = true;
           txtOrderNo.Visible = true;
       }
   }
   protected void btnView_Click(object sender, EventArgs e)
   {
       IMOrder_Report.Visible = true;
       LoadReport(ReportState.FromStart);
   }
}