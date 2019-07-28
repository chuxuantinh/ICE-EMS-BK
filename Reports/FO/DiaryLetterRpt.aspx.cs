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

public partial class Reports_FO_DiaryLetterRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
               ddlLetterType.Focus();
               txtYear.Text = DateTime.Now.Year.ToString();
               panDepartmentName.Visible = false;
               dispDepartmentName();
               txtIMID.Visible = false;
               lblImid.Visible = false;
               DiaryType.Visible = false;
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


    protected void dispDepartmentName()
    {
        string qry5 = "select Name from ServiceNameMaster where Type = 'Department'";
        SqlDataAdapter adpt = new SqlDataAdapter(qry5, con);
        con.Open();

        DataTable dt = new DataTable();

        adpt.Fill(dt);
        //    ddlOrgId.Items.Insert(0, new ListItem("select", "0"));
       ddlDepartmentName.DataSource = dt;

       ddlDepartmentName.DataBind();
       ddlDepartmentName.DataTextField = "Name";
       ddlDepartmentName.DataValueField = "Name";

       ddlDepartmentName.DataBind();
      
    }
    public DataTable getdata()
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;



        try
        {
            str = ddlSession.SelectedValue + txtYear.Text;
            tit = ddlLetterType.SelectedItem.Text;
            if (ddlLetterType.SelectedValue == "IM")
            {
               

                cmd.CommandText = "select  DairyCount.LatterTo, DairyCount.LatterFrom,DairyCount.Department from DiaryEntry,DairyCount where DiaryEntry.DiaryType='Letters' and DiaryEntry.MemberType = '" + ddlLetterType.SelectedItem.Text + "' and  DiaryEntry.ExamSession ='" + ddlSession.SelectedValue + txtYear.Text + "' and  DiaryEntry.IMID  = '" + txtIMID.Text + "'";
            }
            else if (ddlLetterType.SelectedValue == "ICE")
            {


                cmd.CommandText = "select DairyCount.LatterTo, DairyCount.LatterFrom,DairyCount.Department from DiaryEntry,DairyCount where DiaryEntry.DiaryType='Letters' and DiaryEntry.MemberType = '" + ddlLetterType.SelectedItem.Text + "' and  DiaryEntry.ExamSession ='" + ddlSession.SelectedValue + txtYear.Text + "'";
            }
            else if (ddlLetterType.SelectedValue == "Department")
            {


                cmd.CommandText = "select DairyCount.LatterTo, DairyCount.LatterFrom,DairyCount.Department from DiaryEntry,DairyCount where DiaryEntry.DiaryType='Letters'and DiaryEntry.MemberType = '" + ddlLetterType.SelectedItem.Text + "' and  DairyCount.Department = '"+
                    ddlDepartmentName.SelectedItem.Text+"' and DiaryEntry.ExamSession ='" + ddlSession.SelectedValue + txtYear.Text + "'";
            }


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
              //  CrystalReportViewer1.Visible = true;
                ReportDocument rptdoc = new ReportDocument();
                ParameterField paramField = new ParameterField();
                ParameterFields paramFields = new ParameterFields();
                ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();

                DataTable dt = new DataTable();
                dt = getdata();
                ds.Tables[0].Merge(dt);
                rptdoc.Load(Server.MapPath("DiaryLetterCrt.rpt"));
                rptdoc.SetDataSource(dt);
                DiaryType.ReportSource = rptdoc;
                paramField.Name = "tittle";
                paramDValue.Value = "Diary Letter:" + tit + " and Session:" + str;
                paramField.CurrentValues.Add(paramDValue);
                paramField.HasCurrentValue = true;
                paramFields.Add(paramField);
                DiaryType.ParameterFieldInfo = paramFields;

                DiaryType.EnableDatabaseLogonPrompt = false;
                DiaryType.EnableParameterPrompt = false;
                Session["cr"] = rptdoc;
            }
            catch (NullReferenceException ex)
            {
                DiaryType.Visible = false;
                //lblExceptioN.Text = "Null Date .";
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
            DiaryType.ReportSource = (ReportDocument)Session["cr"];
        }
        }





    protected void btnView_Click(object sender, EventArgs e)
    {
        DiaryType.Visible = true;
        LoadReport(ReportState.FromStart);

  }


protected void  ddlLetterType_SelectedIndexChanged(object sender, EventArgs e)
{
    DiaryType.Visible = false;
    lblImid.Visible = false;
    txtIMID.Visible = false;
    panDepartmentName.Visible = false;
    ddlLetterType.Focus();
  
    if(ddlLetterType.SelectedValue=="IM")
    {
        lblImid.Visible = true;
      txtIMID.Visible = true;
    }
    if (ddlLetterType.SelectedValue == "Department")
    {
        ddlDepartmentName.Visible = true;
        panDepartmentName.Visible = true;
    }
}
protected void CrystalReportViewer1_Init(object sender, EventArgs e)
{

}
protected void ddlDepartmentName_SelectedIndexChanged(object sender, EventArgs e)
{
    ddlDepartmentName.Focus();
}
}