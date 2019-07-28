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

public partial class Reports_FO_MemberTypeRpt : System.Web.UI.Page
{ SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string tit;
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

                ddlMemberType.Focus();
                MemberTypeReport.Visible = false;
                LoadReport(ReportState.FromStart);
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";

                //  rblICE.SelectedValue = "ICE";

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
           
            tit = ddlMemberType.SelectedItem.Text;
         str = ddlSession.SelectedValue + txtYear.Text;


            cmd.CommandText = "select * from DiaryEntry where MemberType='"+ddlMemberType.SelectedItem.Text+"' and ExamSession ='" + ddlSession.SelectedValue + txtYear.Text + "'";

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
   


           public void LoadReport(ReportState rptState)
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
            //if (dt.Rows.Count != 0)
            //{
               
                rptdoc.Load(Server.MapPath("MemberTypeCrt.rpt"));
                rptdoc.SetDataSource(dt);
                MemberTypeReport.ReportSource = rptdoc;
                ds.Dispose();
           // }
                paramField.Name = "tittle";
                paramDValue.Value = "Member Type:"+tit+ " and Session:"+str;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                MemberTypeReport.ParameterFieldInfo = paramFields;

                MemberTypeReport.EnableDatabaseLogonPrompt = false;
                MemberTypeReport.EnableParameterPrompt = false;
            Session["cr"] = rptdoc;
        }
        catch (NullReferenceException ex)
        {
            MemberTypeReport.Visible = false;
            Response.Write(ex);
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
            MemberTypeReport.ReportSource = (ReportDocument)Session["cr"];
        }
       
    }


           protected void btnView_Click(object sender, EventArgs e)
           {
               MemberTypeReport.Visible = true;
               LoadReport(ReportState.FromStart);

           }




    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}