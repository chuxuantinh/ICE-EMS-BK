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

public partial class Reports_Student_Student : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
   
    string str;

    static int flag;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (flag == 1)
            {
                disp();
            }

            if (!IsPostBack)
            {
                Session["cr"] = null;
                flag = 0;
                Panel1.Visible = true;
                lblSID.Visible = false;
                txtSID.Visible = false;
                txtYear.Text = DateTime.Now.Year.ToString();
                Stuident_Profile_Report.Visible = false;              
                lblImid.Visible = false;
                txtIMID.Visible = false;
                Stuident_Profile_Report.Visible = false;
                txtSID.Visible = false;
                rblICE.SelectedValue = "ICE";
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
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
            str = ddlSession.SelectedValue + txtYear.Text;
       
            if (rblICE.SelectedValue == "ICE")
            {

                cmd.CommandText = "select Student.*, ExamCurrent.* from Student, ExamCurrent where Student.SID= ExamCurrent.SID and Student.Session ='" + ddlSession.SelectedValue + txtYear.Text + "'";
            }
            else if (rblICE.SelectedValue == "IMID")
            {


                cmd.CommandText = "select Student.*, ExamCurrent.* from Student, ExamCurrent where Student.SID = ExamCurrent.SID and Student.Session='" + ddlSession.SelectedValue + txtYear.Text + "' and Student.IMID  = '" + txtIMID.Text + "'";
            }
            else if (rblICE.SelectedValue == "SID")
            {

                cmd.CommandText = "select Student.*, ExamCurrent.* from Student, ExamCurrent where Student.SID = ExamCurrent.SID and Student.SID = '" + txtSID.Text + "'";
                    
            }
            cmd.Connection = con;          
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            int a = ds.Tables[0].Columns.Count;
            cmd.Dispose();           
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


    protected void disp()
    {
         try
        {
            Stuident_Profile_Report.Visible = true;
            ReportDocument rptdoc = new ReportDocument();
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
            DataTable dt = new DataTable();
            dt = getdata();
            ds.Tables[0].Merge(dt);           
            rptdoc.Load(Server.MapPath("StudentCrtrpt.rpt"));
            rptdoc.SetDataSource(dt);
            Stuident_Profile_Report.ReportSource = rptdoc;
            ds.Dispose();         
            paramField.Name = "tittle";
            if (rblICE.SelectedItem.Text == "IMID")
            {
                paramDValue.Value = "IMID:" + txtIMID.Text + " and Session:" + str;
            }
            if (rblICE.SelectedItem.Text == "ICE")
            {

                paramDValue.Value = "Session:" + str;

            }
            if (rblICE.SelectedValue == "SID")
            {

                paramDValue.Value = "Student Membership:"+ txtSID.Text;

            }
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            Stuident_Profile_Report.ParameterFieldInfo = paramFields;
        }
        catch (NullReferenceException ex)
         {
             Stuident_Profile_Report.Visible = false;
           
        }
        catch (CrystalReportsException ex)
         {
             Stuident_Profile_Report.Visible = false;
          
        }
         catch (IndexOutOfRangeException ex)
         {
             Stuident_Profile_Report.Visible = false;
             
         }
         catch (ArgumentNullException  ex)
         {
             Stuident_Profile_Report.Visible = false;
            
         }


    }





    protected void btnView_Click(object sender, EventArgs e)
    {
       disp();
       flag = 1;
    }
    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        Stuident_Profile_Report.Visible = false;
        lblImid.Visible = false;
        txtIMID.Visible = false;
        txtSID.Visible = false;
        lblSID.Visible = false;
        if (rblICE.SelectedValue == "ICE")
        {
            Panel1.Visible = true;
        }
        if (rblICE.SelectedValue == "IMID")
        {
            Panel1.Visible = true;
            txtIMID.Focus();
            lblImid.Visible = true;
            txtIMID.Visible = true;

        }
        if (rblICE.SelectedValue == "SID")
        {
            Panel1.Visible = false;
            txtSID.Focus();
            lblSID.Visible = true;
            txtSID.Visible = true;
        }
    }
    protected void Stuident_Profile_Report_Init(object sender, EventArgs e)
    {

    }
}