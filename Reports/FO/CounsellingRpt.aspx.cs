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

public partial class Reports_FO_CounsellingRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataSet ds = null;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlCommand cmd;
    static string rptTitle;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
           
            LoadReport();
           
            if (!IsPostBack)
            {
                Session["cr"] = null;
                CounsellingReport.Visible = false;
                rbtnlstSelect.SelectedValue = "Status";
                ddlResponse.Visible = false; pnlCourier.Visible = false;
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
        SqlDataAdapter adapter;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            cmd = new SqlCommand();
            con.Open();
            if (rbtnlstSelect.SelectedValue == "Status")
            {
                rptTitle = "Status: " + ddlStatus.SelectedItem.Text.ToString() + " From: " + txtDateFrom.Text.ToString() + " To: " + txtDateto.Text.ToString();
                cmd.CommandText = "SELECT Counselling.CID AS Expr1, Counselling.Name, Counselling.Course, Counselling.Address1, Counselling.Address2, Counselling.City, Counselling.State, Counselling.PinCode, Counselling.Contact, Counselling.Mobile, Counselling.Email, Counselling.Date, Counselling.Status, Counselling.Session, Followup.SN, Followup.CounsellingNo, Followup.FollowUpDate, Followup.Response, Followup.Comments, Followup.Counselor, Followup.CID FROM Counselling Left JOIN Followup ON Counselling.CID = Followup.CID where Counselling.Status='"+ddlStatus.SelectedValue.ToString()+"'";
            }
            else if (rbtnlstSelect.SelectedValue == "Response")
            {
                rptTitle = "Response: " + ddlResponse.SelectedItem.Text + " From: " + txtDateFrom.Text.ToString() + " To: " + txtDateto.Text.ToString();
                cmd.CommandText = "SELECT Counselling.CID AS Expr1, Counselling.Name, Counselling.Course, Counselling.Address1, Counselling.Address2, Counselling.City, Counselling.State, Counselling.PinCode, Counselling.Contact, Counselling.Mobile, Counselling.Email, Counselling.Date, Counselling.Status, Counselling.Session, Followup.SN, Followup.CounsellingNo, Followup.FollowUpDate, Followup.Response, Followup.Comments, Followup.Counselor, Followup.CID FROM Counselling Left JOIN Followup ON Counselling.CID = Followup.CID where  Followup.Response='" + ddlResponse.SelectedValue.ToString() + "' and Counselling.Date Between '"+Convert.ToDateTime(txtDateFrom.Text,dtinfo)+"' and '"+Convert.ToDateTime(txtDateto.Text,dtinfo)+"'";
            }
            else if (rbtnlstSelect.SelectedValue == "Student")
            {
                rptTitle = "Student Name(s): " + txtSName.Text.ToString();
                cmd.CommandText = "SELECT Counselling.CID AS Expr1, Counselling.Name, Counselling.Course, Counselling.Address1, Counselling.Address2, Counselling.City, Counselling.State, Counselling.PinCode, Counselling.Contact, Counselling.Mobile, Counselling.Email, Counselling.Date, Counselling.Status, Counselling.Session, Followup.SN, Followup.CounsellingNo, Followup.FollowUpDate, Followup.Response, Followup.Comments, Followup.Counselor, Followup.CID FROM Counselling INNER JOIN Followup ON Counselling.CID = Followup.CID where  Counselling.Name like '%"+txtSName.Text.ToString()+"%'";
            }
            cmd.Connection = con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }
        return ds.Tables[0];
    }



    protected void LoadReport()
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
            rptdoc.Load(Server.MapPath("CounsellingCrt.rpt"));
            rptdoc.SetDataSource(dt);
            CounsellingReport.ReportSource = rptdoc;
            paramField.Name = "title2";
            paramDValue.Value = rptTitle;
            paramField.CurrentValues.Add(paramDValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
            CounsellingReport.ParameterFieldInfo = paramFields;
        }
        catch (NullReferenceException ex)
        {
            CounsellingReport.Visible = false;
            //lblExceptioN.Text = "Null Date .";
        }

        catch (IndexOutOfRangeException ex)
        {
            //  lblExceptioN.Text = "Null Date .";
        }

    }



    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {

        CounsellingReport.Visible = true;
        LoadReport();




    }
    protected void rbtnlstSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlstSelect.SelectedValue == "Status")
        {
            pnlDate.Visible = true;
            pnlCourier.Visible = false;
            ddlStatus.Visible = true;
            ddlResponse.Visible = false;
        }
        else if (rbtnlstSelect.SelectedValue == "Response")
        {
            pnlDate.Visible = true;
            pnlCourier.Visible = false;
            ddlStatus.Visible = false;
            ddlResponse.Visible = true;
        }
        else if (rbtnlstSelect.SelectedValue == "Student")
        {
            pnlCourier.Visible = true;
            pnlDate.Visible = false;
            ddlStatus.Visible = false;
            ddlResponse.Visible = false;
        }
    }
    protected void ddlResponse_SeelctedIndexChanged(object sender, EventArgs e)
    {
        if (ddlResponse.SelectedValue.ToString() == "Hot")
            ddlResponse.Attributes.Add("class", "hot");
        if (ddlResponse.SelectedValue.ToString() == "Cold")
            ddlResponse.Attributes.Add("class", "cold");
        if (ddlResponse.SelectedValue.ToString() == "Warm")
            ddlResponse.Attributes.Add("class", "warm");
    }
}
