using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class Reports_Student_StudentAccountRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    string strt;
    string str;
    static int flag = 0;
    ReportDocument rptdoc = new ReportDocument();
    ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (flag == 1)
        //{
        //    disp();
        //}

        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;
                lblImid.Visible = true;
                txtIMID.Visible = true;
                lblSID.Visible = false;
                txtSID.Visible = false;
                lblExamStatus.Visible = false;
                lblSName.Visible = false;
                txtSName.Visible = false;
                StudentAccount_Report.Visible = false;
                rblICE.SelectedValue = "ICE";
                ddlExamStatus.Visible = false;
                lblExamStatus.Visible = false;
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
            strt = ddlType.SelectedItem.Text;
            if (rblICE.SelectedValue == "ICE")
            {              
                if (ddlType.SelectedItem.Text == "IMID")
                {
                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.IMID='" + txtIMID.Text + "'";
                    paramDValue.Value = "IMID:" + txtIMID.Text;                                     
                }

                if (ddlType.SelectedItem.Text == "Exam Status")
                {
                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedItem.Text + "'";
                    paramDValue.Value = "Exam Status:" + ddlExamStatus.SelectedItem.Text;
                }
                if (ddlType.SelectedItem.Text == "Composite Fees Status")
                {

                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedItem.Text + "'";
                    paramDValue.Value = "Composite Fees Status:" + ddlExamStatus.SelectedItem.Text;
                }
                if (ddlType.SelectedItem.Text == "Student Membership ID")
                {

                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.SID='" + txtSID.Text + "'";
                    paramDValue.Value = "Member ID:" + txtSID.Text;
                }
                if (ddlType.SelectedItem.Text == "Student Name")
                {
                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.SName='" + txtSName.Text + "'";
                    paramDValue.Value = "Student Name:" + txtSName.Text;
                }

            }

            if (rblICE.SelectedValue == "IMID")
            {
                if (ddlType.SelectedItem.Text == "IMID")
                {

                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.IMID='" + txtIMID.Text + "'";
                    paramDValue.Value = "IMID:" + txtIMID.Text;
                }

                if (ddlType.SelectedItem.Text == "Exam Status")
                {

                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedItem.Text + "' and ExamCurrent.IMID='" + txtIMID.Text + "'";
                    paramDValue.Value = "IMID:" + txtIMID.Text + " and Exam Status:" + ddlExamStatus.SelectedItem.Text;
                }
                if (ddlType.SelectedItem.Text == "Composite Fees Status")
                {
                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.ExamStatus='" + ddlExamStatus.SelectedItem.Text + "' and ExamCurrent.IMID='" + txtIMID.Text + "'";
                    paramDValue.Value = "IMID:" + txtIMID.Text + " and Composite Fess Status:" + ddlExamStatus.SelectedItem.Text;
                }
                if (ddlType.SelectedItem.Text == "Student Membership ID")
                {

                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.SID='" + txtSID.Text + "'";
                    paramDValue.Value = "IMID:" + txtIMID.Text + " Member ID:" + txtSID.Text;
                }
                if (ddlType.SelectedItem.Text == "Student Name")
                {
                    cmd.CommandText = "select AppRecord.*, ExamCurrent.* from ExamCurrent Left Outer Join AppRecord on AppRecord.Enrolment = ExamCurrent.SId where ExamCurrent.SName='" + txtSName.Text + "'";
                    paramDValue.Value = "Student Name:" + txtSName.Text;
                }
            }

            cmd.Connection = con;    
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        //    int a = ds.Tables[0].Columns.Count;                  
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
        lblSID.Visible = false;
        txtSID.Visible = false;
        lblExamStatus.Visible = false;
        lblImid.Visible = false;
        txtIMID.Visible = false;
        lblSName.Visible = false;
        txtSName.Visible = false;
        txtSName.Text = "";
        txtSID.Text = "";
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
                ddlExamStatus.Items.Add("NO");
                ddlExamStatus.Items.Add("YES");
                ddlExamStatus.Items.Add("Filled");
                lblExamStatus.Visible = true;
            }


            if (ddlType.SelectedValue == "CFeesStatus")
            {              
                ddlExamStatus.Visible = true;
                ddlExamStatus.Items.Add("NO");
                ddlExamStatus.Items.Add("YES");
                ddlExamStatus.Items.Add("Approved");
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
                ddlExamStatus.Items.Add("NO");
                ddlExamStatus.Items.Add("YES");
                ddlExamStatus.Items.Add("Filled");
                lblExamStatus.Visible = true;
            }


            if (ddlType.SelectedValue == "CFeesStatus")
            {             
                ddlExamStatus.Visible = true;
                ddlExamStatus.Items.Add("NO");
                ddlExamStatus.Items.Add("YES");
                ddlExamStatus.Items.Add("Approved");
                lblExamStatus.Visible = true;


            }
            if (ddlType.SelectedValue == "IMID")
            {              
                txtIMID.Visible = true;
                lblImid.Visible = true;

            }
        }




        if (ddlType.SelectedValue == "SName")
        {           
            lblSName.Visible = true;
            txtSName.Visible = true;

        }
        if (ddlType.SelectedValue == "SID")
        {
            lblSID.Visible = true;
            txtSID.Visible = true;

        }





    }

    protected void StudentAccount_Report_Init(object sender, EventArgs e)
    {

    }
}