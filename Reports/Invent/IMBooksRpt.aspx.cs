using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;
using System.Configuration;
using MasterLibrary;
using System.Globalization;
using System.Configuration;
using System.Runtime.InteropServices;

public partial class Reports_Invent_IMBooksRpt : System.Web.UI.Page
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
            else
            {
                if (!IsPostBack && !IsCallback)
                {
                    Session["cr"] = null;
                    panIMID.Visible = false;
                    IMBooks_Details_Report.Visible = false;
                    rblICE.SelectedValue = "ALL";
                    rblICE.Focus();
                }
                else
                {
                    LoadReport(ReportState.FromPostBack);
                }
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
                cmd.CommandText = "SELECT DISTINCT IMID, CPartI, CPartII, CSectionA, CSectionB, APartI, APartII, ASectionA, ASectionB, CourseID, CPartIIE,APartIIE FROM IMBooks WHERE (CPartI>0) OR (CPartII>0) OR (CSectionA>0) OR (CSectionB>0) OR (APartI>0) OR (APartII>0) OR (ASectionA>0) OR (ASectionB>0)";
                paramDValue.Value = "Required IM Course Details";
            }
            if (rblICE.SelectedItem.Text == "IMID")
            {
                cmd.CommandText = "select * from IMBooks where IMID='" + txtIMID.Text + "'";
                paramDValue.Value = "Membership ID:" + txtIMID.Text;
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
                rptdoc.Load(Server.MapPath("IMBooksCrt.rpt"));
                rptdoc.SetDataSource(dt);
                IMBooks_Details_Report.ReportSource = rptdoc;
                ds.Dispose();                
                paramField.Name = "tittle";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                IMBooks_Details_Report.ParameterFieldInfo = paramFields;
                IMBooks_Details_Report.EnableDatabaseLogonPrompt = false;
                IMBooks_Details_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                IMBooks_Details_Report.Visible = false;              
            }
            catch (CrystalReportsException ex)
            {
                IMBooks_Details_Report.Visible = false;               
            }
            catch (IndexOutOfRangeException ex)
            {
                IMBooks_Details_Report.Visible = false;              
            }
            catch (SqlException ex)
            {
                IMBooks_Details_Report.Visible = false;               
            }
            catch (ArgumentNullException ex)
            {
                IMBooks_Details_Report.Visible = false;               
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            IMBooks_Details_Report.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        IMBooks_Details_Report.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        IMBooks_Details_Report.Visible = false;      
        panIMID.Visible = false;                  
        if (rblICE.SelectedValue == "IMID")
        {           
            txtIMID.Focus();
            panIMID.Visible = true;
        }
       
    }
}