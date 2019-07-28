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

public partial class Reports_Project_ProjectRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    static DataSet ds = null;
    public enum ReportState { NotSet, FromStart, FromSession, FromPostBack };
    SqlCommand cmd;
    SqlDataAdapter adapter;
    ParameterDiscreteValue paramDValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                Session["cr"] = null;
                lblMembershipID.Visible = false;
                txtMembershipID.Visible = false;
                txtDuration.Visible = false;
                lblDuration.Visible = false;
                panIMID.Visible = false;


                Project_Submission_Report.Visible = false;
                txtSession.Text = DateTime.Now.Year.ToString();

                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";

                //  rblICE.SelectedValue = "ICE";
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


     // paramDValue =  new ParameterDiscreteValue();
        try
        {
          //  str = ddlSession.SelectedValue + txtYear.Text;
            //  tit = ddlLetterType.SelectedItem.Text;
            if (rblICE.SelectedValue == "ICE")
            {

                if (ddlProject.SelectedItem.Text == "Student ID")
                {

                    cmd.CommandText = "select * from Project where SID='" + txtMembershipID.Text + "' and session='" + ddlSession.SelectedValue + txtSession.Text + "'";
                    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + " and Student ID:" + txtMembershipID.Text;   
                   }
                if (ddlProject.SelectedItem.Text == "Group ID")
                {

                    cmd.CommandText = "select * from Project where GroupID='" + txtMembershipID.Text + "' and session='" + ddlSession.SelectedValue + txtSession.Text + "'";
                    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + " and Group ID:" + txtMembershipID.Text;
                }
                if (ddlProject.SelectedItem.Text == "Status")
                {
                    cmd.CommandText = "select * from Project where Status='" + ddlOrder.SelectedValue + "' and session='" + ddlSession.SelectedValue + txtSession.Text + "'";
                    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + " and Status:" + ddlOrder.SelectedItem.Text;
                }
                if (ddlProject.SelectedItem.Text == "Duration")
                {
                    cmd.CommandText = "select * from Project where Duration='" + txtDuration.Text + "' and session='" + ddlSession.SelectedValue + txtSession.Text + "'";

                    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + " and Duration:" + txtDuration.Text;
                }

            }
            if (rblICE.SelectedValue == "IMID")
            {

                if (ddlProject.SelectedItem.Text == "Student ID")
                {
                    cmd.CommandText = "select * from Project where SID='" + txtMembershipID.Text + "' and session='"+ddlSession.SelectedValue+txtSession.Text+"' and IMID='"+txtIMID.Text+"'";
                    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + ",Student ID:" + txtMembershipID.Text+" and IMID:"+txtIMID.Text;
                }
                if (ddlProject.SelectedItem.Text == "Group ID")
                {
                    cmd.CommandText = "select * from Project where GroupID='" + txtMembershipID.Text + "' and session='" + ddlSession.SelectedValue + txtSession.Text + "' and IMID='" + txtIMID.Text + "'";
                    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + ",Group ID:" + txtMembershipID.Text+"and IMID:"+txtIMID.Text;
                }
                if (ddlProject.SelectedItem.Text == "Status")
                {
                    cmd.CommandText = "select * from Project where Status='" + ddlOrder.SelectedValue + "' and session='" + ddlSession.SelectedValue + txtSession.Text + "' and IMID='" + txtIMID.Text + "'";
                    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + ",Status:" + ddlOrder.SelectedItem.Text + " and IMID:" + txtIMID.Text;
                }
                //if (ddlProject.SelectedItem.Text == "Duration")
                //{
                //    cmd.CommandText = "select * from Project where Duration='" + txtDuration.Text + "' and session='"+ddlSession.SelectedValue+txtSession.Text+"' and IMID='" + txtIMID.Text + "'";
                //    paramDValue.Value = "session:" + ddlSession.SelectedValue + txtSession.Text + ",Duration:" + txtDuration.Text+" and IMID=" + txtIMID.Text;
                //}
            }
            cmd.Connection = con;
            con.Open();
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
              //  Project_Submission_Report.Visible = true;
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                 paramDValue = new ParameterDiscreteValue();
                DataTable dt = new DataTable();
              //  paramDValue.Value = "Project Dtails:";
               dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("ProjectCrt.rpt"));
                rptdoc.SetDataSource(dt);
                Project_Submission_Report.ReportSource = rptdoc;
                paramField.Name = "tittle";
              
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                Project_Submission_Report.ParameterFieldInfo = paramFields;
                rptdoc.SetParameterValue("tittle", paramDValue.Value, "View Details");

                Project_Submission_Report.EnableDatabaseLogonPrompt = false;
                Project_Submission_Report.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                Project_Submission_Report.Visible = false;
                //lblExceptioN.Text = "Null Date .";
            }
            catch (CrystalReportsException ex)
            {
                // Response.Write(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                Project_Submission_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (SqlException ex)
            {
                Project_Submission_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (ArgumentNullException ex)
            {
                Project_Submission_Report.Visible = false;
                //  lblExceptioN.Text = "Null Date .";
            }
            catch (COMException ex)
            {
                Response.Redirect("../../Login.aspx");
            }
        }
        else
        {
            Project_Submission_Report.ReportSource = (ReportDocument)Session["cr"];
        }
        }

    protected void rblICE_SelectedIndexChanged(object sender, EventArgs e)
    {
        Project_Submission_Report.Visible = false;
        lblMembershipID.Visible = false;
        txtMembershipID.Visible = false;
        PanSession.Visible = true;
       
        if (rblICE.SelectedValue == "ICE")
        {
            
            panIMID.Visible = false;
          
            txtIMID.Visible = false;
            if (ddlProject.SelectedItem.Text == "Student ID")
            {
                PanSession.Visible = false;
                lblMembershipID.Text = "Student ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;
                PanSession.Visible = false;
            
                

            }
            if (ddlProject.SelectedItem.Text == "Group ID")
            {
                lblMembershipID.Text = "Group ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;

            }
            if (ddlProject.SelectedItem.Text == "Status")
            {
                lblStatus.Visible = true;
                ddlOrder.Visible = true;

            }
            //if (ddlProject.SelectedItem.Text == "Duration")
            //{
            //    lblDuration.Visible = true;
            //    txtDuration.Visible = true;
            //}
           
           // lblImid.Visible = true;
           
            //lblMembershipID.Visible = true;
            //txtMembershipID.Visible = true;
            //txtIMID.Visible = true;

        }
        if (rblICE.SelectedValue == "IMID")
        {
           
            if (ddlProject.SelectedItem.Text == "Student ID")
            {
                PanSession.Visible = false;
                lblMembershipID.Text = "Student ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;
                

            }
            if (ddlProject.SelectedItem.Text == "Group ID")
            {
                lblMembershipID.Text = "Group ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;

            }
            if (ddlProject.SelectedItem.Text == "Status")
            {
                lblStatus.Visible = true;
                ddlOrder.Visible = true;

            }
            //if (ddlProject.SelectedItem.Text == "Duration")
            //{
            //    lblDuration.Visible = true;
            //    txtDuration.Visible = true;
            //}
            panIMID.Visible = true;
            txtIMID.Visible = true;

        }
     
    }
    protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Project_Submission_Report.Visible = false;
        lblMembershipID.Visible = false;
        txtMembershipID.Visible = false;
        lblStatus.Visible = false;
        ddlOrder.Visible = false;
        panIMID.Visible = false;
        txtDuration.Visible = false;
        lblDuration.Visible = false;
        PanSession.Visible = true;
        //  panIMID.Visible = false;
        if (rblICE.SelectedItem.Text == "ICE")
        {


            if (ddlProject.SelectedItem.Text == "Student ID")
            {
                PanSession.Visible = false;
                lblMembershipID.Text = "Student ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;
            }
            if (ddlProject.SelectedItem.Text == "Status")
            {
                lblStatus.Visible = true;
                ddlOrder.Visible = true;
                
            }
            if (ddlProject.SelectedItem.Text == "Group ID")
            {
                lblMembershipID.Text = "Group ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;
            }
            if (ddlProject.SelectedItem.Text == "Duration")
            {
               
                lblDuration.Visible = true;
                txtDuration.Visible = true;
            }
        }
        if (rblICE.SelectedItem.Text == "IMID")
        {
            panIMID.Visible = true;


            if (ddlProject.SelectedItem.Text == "Student ID")
            {
                PanSession.Visible = false;
                lblMembershipID.Text = "Student ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;
            }
            if (ddlProject.SelectedItem.Text == "Group ID")
            {
                lblMembershipID.Text = "Group ID:";
                lblMembershipID.Visible = true;
                txtMembershipID.Visible = true;
            }
            if (ddlProject.SelectedItem.Text == "Status")
            {
                lblStatus.Visible = true;
                ddlOrder.Visible = true;

            }
            //if (ddlProject.SelectedItem.Text == "Duration")
            //{
            //    lblDuration.Visible = true;
            //    txtDuration.Visible = true;
            //}
        }

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Project_Submission_Report.Visible = true;

        LoadReport(ReportState.FromStart);
    }
    protected void Project_Submission_Report_Init(object sender, EventArgs e)
    {

    }
}