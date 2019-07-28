using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Data.OleDb;
using System.Data.Linq;


public partial class Exam_ChangeExamCity : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataContext conLinq = new DataContext(System.Configuration.ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    ClsECenterCity ecity = new ClsECenterCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ecity.getItems(ddlExamCity);
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                    ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win"; 
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                readexcel();
                ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (i == 0 | i == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ddlCity_SelectedInexChanged(object sender, EventArgs e)
    {
        txtExamID.Text = ecity.getCenterCode(ddlExamCity.SelectedValue.ToString());
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
         string extension,fileName,fileName1;
         extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
         if (extension == ".xls" || extension == ".xlsx")                                        //Get extension of uploaded file
         {
             fileName = FileUpload1.PostedFile.FileName;
             fileName1 = fileName.Replace(fileName, "ChangeCity");
             FileUpload1.SaveAs(Server.MapPath("uploads/") + fileName1 +".xls");
         }
    }
    protected void btnChangeCity_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtExcel = readexcel();
            con.Close(); con.Open();
            for (int i = 0; i < dtExcel.Rows.Count; i++)
            {
                cmd = new SqlCommand("update ExamForms set City='" + ddlExamCity.SelectedValue.ToString() + "',CenterCode='"+txtExamID.Text.ToString()+"' Where SID='" + dtExcel.Rows[i][0].ToString() + "' and ExamSeason='" + lblSeasonHidden.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            lblmessage.Text = ex.ToString();
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    private DataTable readexcel()
    {
         string Path = (Server.MapPath ("~/Exam/uploads/ChangeCity.xls"));           //For retrive file for export into Database
            try
            {
                DataTable dtExcel = new DataTable();
                string SourceConstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Path + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'";
                OleDbConnection con1 = new OleDbConnection(SourceConstr);
                string query = "Select * from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, con1);
                data.Fill(dtExcel);
                GridSID.DataSource = dtExcel;
                GridSID.DataBind();
                return dtExcel;
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
                return null;
            }
            finally
            {
                conLinq.Dispose();
            }
    }
}