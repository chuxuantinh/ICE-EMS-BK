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

public partial class Reports_AC_FormTypeRpt : System.Web.UI.Page
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
             dtinfo.DateSeparator = "/";
             dtinfo.ShortDatePattern = "dd/MM/yyyy";
            if (rptState != ReportState.FromPostBack)
            {
                Reportdata data = new Reportdata();
                if (ddlStatus.SelectedValue == "Approved")
                {
                    st = "select * from Apprecord where (subDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' and FormType like '%" + ddlSelect.SelectedValue + "%') and Status!='NotApproved' and Status!='Hold' order by AppNo";
                     str =  ddlSelect.SelectedValue+" Approved";
                }
                else
                {
                    st = "select * from Apprecord where (subDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo)+ "' and FormType like '%" + ddlSelect.SelectedValue + "%') and Status='" + ddlStatus.SelectedValue + "' order by AppNo";
                    str =  ddlSelect.SelectedValue + " " + ddlStatus.SelectedValue  ;
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
        cmd = new SqlCommand("select distinct(FeeName) from FeeList where status='YES'", con);
        ds = new DataSet();
        adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds);
       // con.Close();      
       ddlSelect.DataSource= ds.Tables[0];     
       ddlSelect.DataTextField = "FeeName";
       ddlSelect.DataValueField = "FeeName";
       ddlSelect.DataBind();
       ddlSelect.Items.Add(new ListItem("New Admission Application Forms","NewAdmission")); 
        ddlSelect.Items.Add(new ListItem("Promotted Admission Form(SectionA)","ReAdmission"));     
        ddlSelect.Items.Add(new ListItem("Examination Application Forms","Exam"));     
        ddlSelect.Items.Add(new ListItem("ITI Application Forms","ITI"));     
        ddlSelect.Items.Add(new ListItem("Composite Fees","Composite"));     
        ddlSelect.Items.Add(new ListItem("Annual Subscription","Subscription"));     
       ddlSelect.Items.Add(new ListItem("Change of ExamCenter", "ExamCenter"));
       ddlSelect.Items.Add(new ListItem("Rechecking", "Rechecking"));
       ddlSelect.Items.Add(new ListItem("ID Card", "IDCard"));
       ddlSelect.Items.Add(new ListItem("FinalPass", "FinalPass"));
       ddlSelect.Items.Add(new ListItem("AdmitCard", "AdmitCard"));
       ddlSelect.Items.Add(new ListItem("MarksStatement", "MarksStatement"));
       ddlSelect.Items.Add(new ListItem("Membership Certificate", "MembershipCertificate"));
       ddlSelect.Items.Add(new ListItem("Provisional Certificate", "ProvisionalCertificate"));
       ddlSelect.Items.Add(new ListItem("MCADRegistration", "MCADRegistration"));
       ddlSelect.Items.Add(new ListItem("MCADLateFee", "MCADLateFee"));
       ddlSelect.Items.Add(new ListItem("ProformaB", "ProformaB"));
       ddlSelect.Items.Add(new ListItem("ProformaC", "ProformaC"));
        ddlSelect.Items.Add(new ListItem("Old Question Papers","OldSet"));     
       cmd.Dispose();
       ds.Dispose();
    }
}