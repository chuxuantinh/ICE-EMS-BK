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

public partial class Reports_Exam_FormTypeRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    //static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument Rptdoc;

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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
                txtYear.Text = DateTime.Now.Year.ToString();
                Type();
                Session["cr"] = null;
                FormType.Visible = false;
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




    public void LoadReport(ReportState rptState)
    {
        Rptdoc = new ReportDocument();
        try
        {
             string st;
             string str;
            if (rptState != ReportState.FromPostBack)
            {
                Reportdata data = new Reportdata();
                if (ddlStatus.SelectedValue == "Approved")
                {
                    st = "select * from Apprecord where Session='" + ddlSession.SelectedValue + txtYear.Text + "' and FormType like '%" + ddlSelect.SelectedValue + "%' and Status!='NotApproved' and Status!='Hold' order by AppNo";
                     str = ddlSession.SelectedValue + txtYear.Text + " " + ddlSelect.SelectedValue+" Approved";
                }
                else
                {
                    st = "select * from Apprecord where Session='" + ddlSession.SelectedValue + txtYear.Text + "' and FormType like '%" + ddlSelect.SelectedValue + "%' and Status='" + ddlStatus.SelectedValue + "' order by AppNo";
                    str =  ddlSession.SelectedValue + txtYear.Text + " " + ddlSelect.SelectedValue + " " + ddlStatus.SelectedValue  ;
                }
                string FileName = "FormTypeCrt.rpt";
                data.Report(Rptdoc, str, con, st, FileName, FormType);
                Session["cr"] = Rptdoc;
            }
            else
            {
                FormType.ReportSource = (ReportDocument)Session["cr"];
            }
        }
        catch (Exception ex)
        {
            FormType.Visible = false;
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        FormType.Visible = true;
        LoadReport(ReportState.FromStart);
    }
    protected void Stuident_Profile_Report_Init(object sender, EventArgs e)
    {

    }

    private void Type()
    {
        SqlCommand cmd = new SqlCommand();
        DataSet ds;
        SqlDataAdapter adapter;
       // con.Close();
        cmd = new SqlCommand("select distinct(FeeName) from FeeList where Status='YES'", con);
        ds = new DataSet();
        adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds);
       // con.Close();      
       ddlSelect.DataSource= ds.Tables[0];     
       ddlSelect.DataTextField = "FeeName";
       ddlSelect.DataValueField = "FeeName";
       ddlSelect.DataBind();
       ddlSelect.Items.Add(new ListItem("Change of ExamCenter", "ChangeofExamCenter"));
       ddlSelect.Items.Add(new ListItem("Rechecking", "Rechecking"));
       ddlSelect.Items.Add(new ListItem("ID Card", "IDCard"));
       ddlSelect.Items.Add(new ListItem("Final Marksheet", "FinalMarksheet"));
       ddlSelect.Items.Add(new ListItem("AdmitCard", "AdmitCard"));
       ddlSelect.Items.Add(new ListItem("MarksStatement", "MarksStatement"));
       ddlSelect.Items.Add(new ListItem("Membership Certificate", "MembershipCertificate"));
       ddlSelect.Items.Add(new ListItem("Provisional Certificate", "ProvisionalCertificate"));
       cmd.Dispose();
       ds.Dispose();
    }
}