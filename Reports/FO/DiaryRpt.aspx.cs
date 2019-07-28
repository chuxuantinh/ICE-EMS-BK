using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

public partial class Reports_FO_DiaryRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    ConnectionInfo cinfo;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    static string rptTitle;
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
                DiaryReport.Visible = false;
                rbtnlstSelect.SelectedValue = "ICE";
                pnlDairy.Visible = false;
                txtIMID.Visible = false;
                txtIMID.Text = "Insert IMID Here";
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
    protected void Page_Unload(object sender, EventArgs e)
    {
        ReportDocument RptDoc = new ReportDocument();
        if (RptDoc != null)
        {
            RptDoc.Close();
            RptDoc.Dispose();
            RptDoc = null;


            DiaryReport.Dispose();
            DiaryReport = null;


        }
    }
    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            if (rbtnlstSelect.SelectedValue == "ICE")
            {
                rptTitle = "Date: " + txtDateFrom.Text + " To: " + txtDateto.Text.ToString();
                cmd.CommandText = "select dc.DairyNo,dc.IMID,de.Date, dc.Session,dc.ADDRcv,dc.ADDSub,dc.ODDRcv,dc.ODDSub,dc.EnrollFormSub,dc.EnrollFormRcv,dc.ITIRcv,dc.ITISub,dc.OtherFormRcv,dc.OtherFormSub,dc.ProvisionalRcv,dc.ProvisionalSub,dc.FinalPassRcv,dc.FinalPassSub,dc.RecheckingRcv,dc.RecheckingSub,dc.DuplicateDocsRcv,dc.DuplicateDocsSub,dc.ExamFormRcv,dc.ExamFormSub,de.Name,de.CourierNo,de.MembershipNo,de.Amount,de.SubmittedTo from DiaryEntry as de, DairyCount as dc where de.DiaryNo=dc.DairyNo and de.Date Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "'";
            }
            else if (rbtnlstSelect.SelectedValue == "IM")
            {
                rptTitle = "Membershi No.: " + txtIMID.Text.ToString() + " Date: " + txtDateFrom.Text.ToString() + " To: " + txtDateto.Text.ToString();
                cmd.CommandText = "select dc.DairyNo,dc.IMID,de.Date, dc.Session,dc.ADDRcv,dc.ADDSub,dc.ODDRcv,dc.ODDSub,dc.EnrollFormSub,dc.EnrollFormRcv,dc.ITIRcv,dc.ITISub,dc.OtherFormRcv,dc.OtherFormSub,dc.ProvisionalRcv,dc.ProvisionalSub,dc.FinalPassRcv,dc.FinalPassSub,dc.RecheckingRcv,dc.RecheckingSub,dc.DuplicateDocsRcv,dc.DuplicateDocsSub,dc.ExamFormRcv,dc.ExamFormSub,de.Name,de.CourierNo,de.MembershipNo,de.Amount,de.SubmittedTo from DiaryEntry as de, DairyCount as dc where de.DiaryNo=dc.DairyNo and de.Date Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "' and de.IMID='" + txtIMID.Text.ToString() + "'";
            }
            else if (rbtnlstSelect.SelectedValue == "Dairy")
            {
                rptTitle = "Dairy No: " + txtDNo.Text.ToString();
                cmd.CommandText = "select dc.DairyNo,dc.IMID,de.Date, dc.Session,dc.ADDRcv,dc.ADDSub,dc.ODDRcv,dc.ODDSub,dc.EnrollFormSub,dc.EnrollFormRcv,dc.ITIRcv,dc.ITISub,dc.OtherFormRcv,dc.OtherFormSub,dc.ProvisionalRcv,dc.ProvisionalSub,dc.FinalPassRcv,dc.FinalPassSub,dc.RecheckingRcv,dc.RecheckingSub,dc.DuplicateDocsRcv,dc.DuplicateDocsSub,dc.ExamFormRcv,dc.ExamFormSub,de.Name,de.CourierNo,de.MembershipNo,de.Amount,de.SubmittedTo from DiaryEntry as de, DairyCount as dc where de.DiaryNo=dc.DairyNo and de.DiaryNo='" + txtDNo.Text.ToString() + "' ";
            }

            cmd.Connection = con;
            con.Open();
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            cmd.Dispose();
            con.Close();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            lblExceptioN.Text = "";
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
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();

                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("DiaryCrt.rpt"));
                rptdoc.SetDataSource(dt);
                DiaryReport.ReportSource = rptdoc;
                paramField.Name = "title2";
                paramDValue.Value = rptTitle;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                DiaryReport.ParameterFieldInfo = paramFields;
              //  SetBDLoginInfo(cinfo);
                DiaryReport.EnableDatabaseLogonPrompt = false;
                DiaryReport.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                DiaryReport.Visible = false;
                lblExceptioN.Text = "";
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
            DiaryReport.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        DiaryReport.Visible = true;
        LoadReport(ReportState.FromStart);
       
    }

    protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlstSelect.SelectedValue == "ICE")
        {
            pnlDate.Visible = true;
            pnlDairy.Visible = false;
            txtIMID.Visible = false;
        }
        else if (rbtnlstSelect.SelectedValue == "IM")
        {
            pnlDate.Visible = true;
            pnlDairy.Visible = false;
            txtIMID.Visible = true;
        }
        else if (rbtnlstSelect.SelectedValue == "Dairy")
        {
            pnlDairy.Visible = true;
            pnlDate.Visible = false;
        }
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}
