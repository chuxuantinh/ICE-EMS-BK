using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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


public partial class Reports_Exam_AdmitCardCrt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                AdmitCard.Visible = false;                
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
    }
    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adapter;
        try
        {
            con.Open();
            string qry = "SELECT  ExamForm.SN, Student.SID, ExamForms.CenterCode, ExamCenter.ID, ExamForm.Shift, ExamForm.Date, ExamForm.SubName, ExamForm.SubID, Student.Name,  Student.FName,ExamForms.IMID, ExamForms.ExamSeason, ExamForms.Status, ExamForms.RollNo, Docs.Image, ExamCenter.Address, ExamCenter.Address2, ExamCenter.City, ExamCenter.Pin, ExamCenter.State, ExamForms.Course, ExamForms.Part, ExamCenter.Name AS ECenterName FROM ExamForm INNER JOIN  ExamForms ON ExamForm.SN = ExamForms.SN INNER JOIN  Student ON ExamForms.SID = Student.SID INNER JOIN  ExamCenter ON ExamForms.CenterCode = ExamCenter.ID INNER JOIN Docs on Docs.SID=ExamForms.SID where ExamCenter.Season='"+ddlExamSeason.SelectedValue + txtYearSeason.Text+"' and ExamForms.Status='AdmitCardGenerated' and ExamForms.ExamSeason='" + ddlExamSeason.SelectedValue + txtYearSeason.Text + "' and ExamForms.SID between '" + txtSID.Text + "' and '" + txtSIDTo.Text + "' order by  Student.SID,ExamForm.Date";
            ds = new DataSet();
            adapter = new SqlDataAdapter(qry, con);
            adapter.Fill(ds);
            
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
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
                dt = getdata();
              //  ds.Tables[0].Merge(dt);            
                rptdoc.Load(Server.MapPath("AdmitCards.rpt"));               
                rptdoc.SetDataSource(dt);
                Section section = rptdoc.ReportDefinition.Sections["GroupHeaderSection1"];
                section.Height = 5740;
                Section section5 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["GroupFooterSection3"];
                section5.Height = 3180;
                Section section3 = rptdoc.ReportDefinition.Sections["Section3"];
                section3.Height = 300;
                Section F55 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["GroupHeaderSection1"];
                F55.Height = 300;
                Section F3 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["GroupFooterSection1"];
                F3.Height = 2880;
                Section F5 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["GroupFooterSection7"];
                F5.Height = 2580;
                Section F6 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["GroupFooterSection8"];
                F6.Height = 2280;
                Section F7 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["GroupFooterSection6"];
                F7.Height = 1980;
                Section F8 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["GroupFooterSection4"];
                F8.Height = 1680;

                Section Sub1 = rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.Sections["Section3"];
                Sub1.Height = 300;
                //Section Sub2 = rptdoc.Subreports["dt"].ReportDefinition.Sections["DetailSection1"];
                //Sub2.Height = 300;
                //Section Sub3 = rptdoc.Subreports["dd"].ReportDefinition.Sections["DetailSection1"];
                //Sub3.Height = 300;
                //Section Sub = rptdoc.Subreports["da1"].ReportDefinition.Sections["DetailSection1"];
                //Sub.Height = 300;
                rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["IMID1"].Top = 2120;
                rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["IMID3"].Top = 1820;
                rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["IMID4"].Top = 1520;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["IMID5"].Top = 1220;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["IMID2"].Top = 2420;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["IMID6"].Top = 920;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Text3"].Top = 2120;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Text4"].Top = 1820;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Text5"].Top = 1520;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Text6"].Top = 1220;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Text7"].Top = 2420;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Text8"].Top = 920;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Picture1"].Top = 1760;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Picture2"].Top = 1450;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Picture3"].Top = 1150;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Picture4"].Top = 850;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Picture5"].Top = 2050;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["Picture6"].Top = 550;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["t1"].Top = 2120;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["t2"].Top = 1820;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["t3"].Top = 1520;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["t4"].Top = 1220;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["t5"].Top = 2420;
               rptdoc.Subreports["AdmitCardRpt.rpt"].ReportDefinition.ReportObjects["t6"].Top = 920;
         


                AdmitCard.ReportSource = rptdoc;
               // UpdateSectionHeight(rptdoc, "Section3", 800);
                paramField.Name = "tittle";
                paramDValue.Value = " ";
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                AdmitCard.ParameterFieldInfo = paramFields;
                AdmitCard.EnableDatabaseLogonPrompt = false;
                AdmitCard.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                AdmitCard.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                AdmitCard.Visible = false;
                Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                AdmitCard.Visible = false;
            }
            catch (SqlException ex)
            {
                AdmitCard.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                AdmitCard.Visible = false;
            }
        }
        else
        {
            AdmitCard.ReportSource = (ReportDocument)Session["cr"];
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        AdmitCard.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    //private void UpdateSectionHeight(ReportDocument reportDocument, String sectionName, int height)
    //{
    //    Section section = reportDocument.ReportDefinition.Sections[sectionName];
    //    section.Height = height;
    //} 

}