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

public partial class Reports_Student_StudentExamCurrent : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string strt;
    ReportDocument rptdoc;
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
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

                lblImid.Visible = false;
                txtIMID.Visible = false;
                lblExamStatus.Visible = false;              
                StudentAccount_Report.Visible = false;
                rblICE.SelectedValue = "ICE";               
                ddlExamStatus.Visible = true;
                ddlExamStatus.Items.Clear();
                ddlExamStatus.Items.Add(new ListItem("Not Submitted", "No"));
                ddlExamStatus.Items.Add(new ListItem("Submitted", "Yes"));
                ddlExamStatus.Items.Add(new ListItem("Filled", "Filled"));
                lblExamStatus.Visible = true;
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
            con.Open();
            strt = ddlType.SelectedItem.Text;
            if (rblICE.SelectedValue == "ICE")
            {
              
                if (ddlType.SelectedItem.Text == "Exam Status")
                {
                    cmd.CommandText = "select Student.*, ExamCurrent.* from ExamCurrent Inner Join Student on Student.SID = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedValue + "'";
                    paramDValue.Value = "Exam Form " + ddlExamStatus.SelectedItem.Text;
                }
                if (ddlType.SelectedItem.Text == "Composite Fees Status")
                {

                    cmd.CommandText = "select Student.*, ExamCurrent.* from ExamCurrent Inner Join Student on Student.SID = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedValue + "'";
                    paramDValue.Value = "Composite Fees " + ddlExamStatus.SelectedItem.Text;
                }
             
            }

            if (rblICE.SelectedValue == "IMID")
            {
                
                if (ddlType.SelectedItem.Text == "Exam Status")
                {
                    cmd.CommandText = "select Student.*, ExamCurrent.* from ExamCurrent Inner Join Student on Student.SID = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedValue + "' and ExamCurrent.IMID='" + txtIMID.Text+"'";
                    paramDValue.Value = "Exam Form " + ddlExamStatus.SelectedItem.Text;
                }
                if (ddlType.SelectedItem.Text == "Composite Fees Status")
                {

                    cmd.CommandText = "select Student.*, ExamCurrent.* from ExamCurrent Inner Join Student on Student.SID = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedValue + "' and ExamCurrent.IMID='" + txtIMID.Text + "'";
                    paramDValue.Value = "Composite Fees " + ddlExamStatus.SelectedItem.Text;
                }               
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
                rptdoc.Load(Server.MapPath("StudentaccountCrt.rpt"));
                rptdoc.SetDataSource(dt);
                StudentAccount_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";           
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                StudentAccount_Report.ParameterFieldInfo = paramFields;
                StudentAccount_Report.EnableDatabaseLogonPrompt = false;
                StudentAccount_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                StudentAccount_Report.Visible = false;
               
            }
            catch (IndexOutOfRangeException ex)
            {
                StudentAccount_Report.Visible = false;
               
            }
            catch (CrystalReportsException ex)
            {
                StudentAccount_Report.Visible = false;
               
            }

            catch (ArgumentNullException ex)
            {
                StudentAccount_Report.Visible = false;
           
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            StudentAccount_Report.ReportSource = (ReportDocument)Session["cr"];
        }


    }
    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlType.Visible = true;
        StudentAccount_Report.Visible = false;
        lblImid.Visible = false;
        txtIMID.Visible = false;
        rblICE.Focus();
        if (rblICE.SelectedValue == "ICE")
        {

            if (ddlType.SelectedValue == "IMID")
            {


                lblImid.Visible = true;

                txtIMID.Visible = true;
            }
        }

        if (rblICE.SelectedValue == "IMID")
        {
            lblImid.Visible = true;
            txtIMID.Visible = true;

        }
    
}
  protected void btnView_Click(object sender, EventArgs e)
    {
        rblICE.Focus();
        StudentAccount_Report.Visible = true;
        LoadReport(ReportState.FromStart);

    }

  protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
  {
      StudentAccount_Report.Visible = false;
      ddlExamStatus.Items.Clear();
      ddlExamStatus.Visible = false;
      lblExamStatus.Visible = false;
      lblImid.Visible = false;
      txtIMID.Visible = false;
      txtIMID.Text = "";
      rblICE.Focus();
      if (rblICE.SelectedItem.Text == "IMID")
      {
          txtIMID.Focus();
          lblImid.Visible = true;

          txtIMID.Visible = true;


          if (ddlType.SelectedValue == "Exam Status")
          {
              
              ddlExamStatus.Visible = true;
              ddlExamStatus.Items.Add( new ListItem( "Not Submitted", "NO" ) );
              ddlExamStatus.Items.Add(new ListItem("Submitted", "YES"));
              ddlExamStatus.Items.Add(new ListItem("Filled", "Filled"));           
              lblExamStatus.Visible = true;
          }


          if (ddlType.SelectedValue == "CFeesStatus")
          {
             
              ddlExamStatus.Visible = true;
              ddlExamStatus.Items.Add(new ListItem("Not Submitted", "NO"));
              ddlExamStatus.Items.Add(new ListItem("Submitted", "YES"));
              ddlExamStatus.Items.Add(new ListItem("Approved", "Filled"));
              lblExamStatus.Visible = true;
          }

      }


      if (rblICE.SelectedValue == "ICE")
      {
          lblImid.Visible = false;
          txtIMID.Visible = false;

          if (ddlType.SelectedValue == "Exam Status")
          {
             
              ddlExamStatus.Visible = true;
              ddlExamStatus.Visible = true;
              ddlExamStatus.Items.Add(new ListItem("Not Submitted", "No"));
              ddlExamStatus.Items.Add(new ListItem("Submitted", "Yes"));
              ddlExamStatus.Items.Add(new ListItem("Filled", "Filled"));
              lblExamStatus.Visible = true;
          }


          if (ddlType.SelectedValue == "CFeesStatus")
          {              
              ddlExamStatus.Visible = true;
              ddlExamStatus.Items.Add(new ListItem("Not Submitted", "No"));
              ddlExamStatus.Items.Add(new ListItem("Submitted", "Yes"));
              ddlExamStatus.Items.Add(new ListItem("Approved", "Filled"));
              lblExamStatus.Visible = true;
          }
          if (ddlType.SelectedValue == "IMID")
          {
            
              txtIMID.Visible = true;
              lblImid.Visible = true;

          }
      }






  }

  protected void Page_UnLoad(object sender, EventArgs e)
  {
      this.StudentAccount_Report.Dispose();
      this.StudentAccount_Report = null;
      rptdoc = new ReportDocument();
      rptdoc.Close();
      rptdoc.Dispose();
      GC.Collect();
  }
}