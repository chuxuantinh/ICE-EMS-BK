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

public partial class Reports_FO_FormOnHold : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue;
    protected void Page_Load(object sender, EventArgs e)
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
                    txtYear.Text = DateTime.Now.Year.ToString();
                    panDepartmentName.Visible = false;
                    //  txtIMID.Visible = false;
                    //  lblImid.Visible = false;
                    FormOnHold.Visible = false;
                    LoadReport(ReportState.FromStart);
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlSession.SelectedValue = "Sum";
                    else ddlSession.SelectedValue = "Win";
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
      
        paramDValue = new ParameterDiscreteValue();
        try
        {
            con.Open();
            if (ddltype.SelectedValue == "Forms")
            {
                paramDValue.Value = "Form Count " + ddlSession.SelectedItem.Text+" " + txtYear.Text;
                if (ddlselect.SelectedValue == "IMID")
                {
                    cmd.CommandText = "Select DairyCount.IMID,DairyCount.FinalPassRcv,DairyCount.ExamFormRcv,DairyCount.ExamFormSub,DairyCount.FinalPassSub,DairyCount.DuplicateDocsRcv,DairyCount.DuplicateDocsSub, DairyCount.EnrollFormRcv,DairyCount.EnrollFormSub,DairyCount.ITIRcv,DairyCount.ITISub,DairyCount.OtherFormRcv,DairyCount.OtherFormSub,DairyCount.ProvisionalRcv,DairyCount.ProvisionalSub,DairyCount.CADRcv,DairyCount.CADSub,ProjectCount.ProformaARcv,ProjectCount.ProformaASub,ProjectCount.ProformaBRcv,ProjectCount.ProformaBSub,DairyCount.LatterTo FROM  DairyCount inner join ProjectCount on DairyCount.DairyNo= ProjectCount.DairyNo where ((DairyCount.EnrollFormRcv!=DairyCount.EnrollFormSub) or (DairyCount.ITIRcv!=DairyCount.ITISub) or (DairyCount.OtherFormRcv!=DairyCount.OtherFormSub) or (DairyCount.ProvisionalRcv!=DairyCount.ProvisionalSub) or (DairyCount.FinalPassRcv!=DairyCount.FinalPassSub) or (DairyCount.ReCheckingRcv!=DairyCount.ReCheckingSub)or(DairyCount.ExamFormRcv!=DairyCount.ExamFormSub) or (DairyCount.DuplicateDocsRcv!=DairyCount.DuplicateDocsSub)  or (ProjectCount.ProformaARcv!=ProjectCount.ProformaaSub) or (ProjectCount.ProformaBRcv!=ProjectCount.ProformaBSub) or (DairyCount.CADRcv!=DairyCount.CADSub)) and  DairyCount.Session='" + ddlSession.SelectedValue + txtYear.Text + "' and  DairyCount.IMID='" + txtIMID.Text + "'";
                }
                else
                    cmd.CommandText = "Select DairyCount.IMID, DairyCount.EnrollFormRcv,DairyCount.FinalPassRcv,DairyCount.ExamFormRcv,DairyCount.ExamFormSub,DairyCount.FinalPassSub,DairyCount.DuplicateDocsRcv,DairyCount.DuplicateDocsSub,DairyCount.EnrollFormSub,DairyCount.ITIRcv,DairyCount.ITISub,DairyCount.OtherFormRcv,DairyCount.OtherFormSub,DairyCount.ProvisionalRcv,DairyCount.ProvisionalSub,DairyCount.CADRcv,DairyCount.CADSub,ProjectCount.ProformaARcv,ProjectCount.ProformaASub,ProjectCount.ProformaBRcv,ProjectCount.ProformaBSub,DairyCount.LatterTo FROM  DairyCount inner join ProjectCount on DairyCount.DairyNo= ProjectCount.DairyNo where ((DairyCount.EnrollFormRcv!=DairyCount.EnrollFormSub) or (DairyCount.ITIRcv!=DairyCount.ITISub) or (DairyCount.OtherFormRcv!=DairyCount.OtherFormSub) or (DairyCount.ProvisionalRcv!=DairyCount.ProvisionalSub) or (DairyCount.FinalPassRcv!=DairyCount.FinalPassSub) or (DairyCount.ReCheckingRcv!=DairyCount.ReCheckingSub) or (DairyCount.ExamFormRcv!=DairyCount.ExamFormSub) or (DairyCount.DuplicateDocsRcv!=DairyCount.DuplicateDocsSub)  or (ProjectCount.ProformaaRcv!=ProjectCount.ProformaASub) or (ProjectCount.ProformaBRcv!=ProjectCount.ProformaBSub) or (DairyCount.CADRcv!=DairyCount.CADSub)) and  DairyCount.Session='" + ddlSession.SelectedValue + txtYear.Text + "' and  DairyCount.DairyNo='" + txtIMID.Text + "'";
            }
            else
            {
                paramDValue.Value = "DD Count " + ddlSession.SelectedItem.Text+ " " + txtYear.Text;
                if (ddlselect.SelectedValue == "IMID")
                {
                    cmd.CommandText = "Select DairyCount.IMID,DairyCount.ODDrcv,DairyCount.ODDsub, DairyCount.ADDRcv,DairyCount.ADDSub,DairyCount.TotalDDRcv,DairyCount.TotalDDSub,ProjectCount.DDRcv,DairyCount.MemberRcv,DairyCount.MemberSub,DairyCount.BooksRcv,DairyCount.BooksSub,DairyCount.ProspectusRcv,DairyCount.ProspectusSub,ProjectCount.DDSub,ProjectCount.DDSub,DairyCount.LatterTo,ProjectCount.DDRcv,ProjectCount.DDSub FROM  DairyCount inner join ProjectCount on DairyCount.DairyNo= ProjectCount.DairyNo where ((DairyCount.ADDRcv!=DairyCount.ADDSub) or (DairyCount.TotalDDRcv!=DairyCount.TotalDDSub) or (DairyCount.ODDSub!=DairyCount.ODDrcv) or (ProjectCount.DDRcv!=ProjectCount.DDSub) or (DairyCount.MemberRcv!=DairyCount.MemberSub) or (DairyCount.BooksRcv!=DairyCount.BooksSub) or (DairyCount.ProspectusRcv!=DairyCount.ProspectusSub) or (ProjectCount.DDRcv!=ProjectCount.DDSub)) and  DairyCount.Session='" + ddlSession.SelectedValue + txtYear.Text + "' and  DairyCount.IMID='" + txtIMID.Text + "'";
                }
                else
                    cmd.CommandText = "Select DairyCount.IMID,DairyCount.ODDrcv,DairyCount.ADDRcv,DairyCount.ODDsub,DairyCount.ADDSub,DairyCount.TotalDDRcv,ProjectCount.DDRcv,ProjectCount.DDSub,DairyCount.TotalDDSub,DairyCount.MemberRcv,DairyCount.MemberSub,DairyCount.BooksRcv,DairyCount.BooksSub,DairyCount.ProspectusRcv,DairyCount.ProspectusSub,ProjectCount.DDRcv,ProjectCount.DDSub,DairyCount.LatterTo FROM  DairyCount inner jOIN ProjectCount on DairyCount.DairyNo= ProjectCount.DairyNo where ((DairyCount.ADDRcv!=DairyCount.ADDSub) or (DairyCount.TotalDDRcv!=DairyCount.TotalDDSub) or (DairyCount.ODDSub!=DairyCount.ODDrcv)  or (ProjectCount.DDRcv!=ProjectCount.DDSub) or (DairyCount.MemberRcv!=DairyCount.MemberSub) or (DairyCount.BooksRcv!=DairyCount.BooksSub) or (DairyCount.ProspectusRcv!=DairyCount.ProspectusSub) or (ProjectCount.DDRcv!=ProjectCount.DDSub)) and  DairyCount.Session='" + ddlSession.SelectedValue + txtYear.Text + "' and DairyCount.DairyNo='" + txtIMID.Text + "'";
            }
             cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            //  int a = ds.Tables[0].Columns.Count;         
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
                rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
               
                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("FormOnHoldCrt.rpt"));
                rptdoc.SetDataSource(dt);
                FormOnHold.ReportSource = rptdoc;
                paramField.Name = "tittle";
               
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                FormOnHold.ParameterFieldInfo = paramFields;
                FormOnHold.EnableDatabaseLogonPrompt = false;
                FormOnHold.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
                //  rptdoc.Dispose();
            }
            catch (NullReferenceException ex)
            {
                FormOnHold.Visible = false;
            }
            catch (CrystalReportsException ex)
            {
                FormOnHold.Visible = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                FormOnHold.Visible = false;
            }
            catch (SqlException ex)
            {
                FormOnHold.Visible = false;
            }
            catch (ArgumentNullException ex)
            {
                FormOnHold.Visible = false;

            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }

        else
        {
            FormOnHold.ReportSource = (ReportDocument)Session["cr"];
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        FormOnHold.Visible = true;
        LoadReport(ReportState.FromStart);
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.FormOnHold.Dispose();
        this.FormOnHold = null;
        rptdoc = new ReportDocument();
        rptdoc.Close();
        rptdoc.Dispose();
        GC.Collect();

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlselect.SelectedValue == "IMID")
        {
            lblImid.Text = "IMID";
        }
        else
            lblImid.Text = "Diary No";
    }
}