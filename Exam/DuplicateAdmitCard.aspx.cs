using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;

public partial class Exam_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                    ddlExamSeason.SelectedValue = "Sum";
                else { ddlExamSeason.SelectedValue = "Win"; }
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ddlExamSeason.Focus();
                // TxtDate.Text = Convert.ToDateTime(DateTime.Now, CultureInfo.GetCultureInfo("en-US")).ToString("dd/MM/yyyy HH/MM/SS");
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        lblMessage.Text = "Please select an excel file first";
        lblMessage.Visible = false;

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if ((txtFilePath.HasFile))
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            string query = null;
            string connString = "";
            string strFileName = "DuplicateExcel";
            string strFileType = System.IO.Path.GetExtension(txtFilePath.FileName).ToString().ToLower();
            if (strFileType == ".xls" || strFileType == ".xlsx")
            {
                txtFilePath.SaveAs(Server.MapPath("~/Exam/UploadedExcel/" + strFileName + strFileType));
            }
            else
            {
                lblMessage.Text = "Only excel files allowed & format .xls, .xlsx";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
                return;
            }

            string strNewPath = Server.MapPath("~/Exam/UploadedExcel/" + strFileName + strFileType);
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            query = "SELECT * FROM [Sheet1$]";
            conn = new OleDbConnection(connString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            grvExcelData.DataBind();
            lblMessage.Text = "Data retrieved successfully!" ;
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Visible = true;
            da.Dispose();
            conn.Close();
            conn.Dispose();
        }
        else
        {
            lblMessage.Text = "Please select an excel file first";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Visible = true;
        }
    }
    protected void Padmit_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        string query = null;
        string connString = "";
        string strFileName = "DuplicateExcel";
        string strFileType = System.IO.Path.GetExtension(txtFilePath.FileName).ToString().ToLower();
        string strNewPath = Server.MapPath("~/Exam/UploadedExcel/" + strFileName + strFileType);
        if (strFileType.Trim() == ".xls")
        {
            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        }
        else if (strFileType.Trim() == ".xlsx")
        {
            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        }

        con = new SqlConnection(connString);
        if (con.State == ConnectionState.Closed) con.Open();
        cmd = new SqlCommand(query,con);
        da=new SqlDataAdapter();
        DataTable ds = new DataTable();
        da.Fill(ds);
        grvExcelData.DataBind();
        SqlDataReader dr= cmd.ExecuteReader();
        while (dr.Read())
        {
            query = "SELECT * FROM [Sheet1$]";
            SqlCommand cmd1 = new SqlCommand("spAdmitCard", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            SqlParameter sp = cmd1.Parameters.AddWithValue("@sid", ds);
            cmd1.Parameters.AddWithValue("@RSN", ddlRSN.SelectedValue.ToString());
            cmd1.Parameters.AddWithValue("@Session", lblExamSeasonHidden.Text);
            cmd1.Parameters.AddWithValue("@Type", "Duplicate"); // used for Admit Card Type Original/Duplicate
            //SqlCommand cmd = new SqlCommand("insert into AdmitCard(sid,rollNo,Name,Father,IMNo,SubId,Date,Session,Subcode,ExamCenter,Lotno,ImageName,address1,address2,address3,Course,part)select examforms.SID,examforms.RollNo,Student.Name,student.FName,examforms.IMID,ExamForm.SubID,examform.Date,examform.Shift,examform.SubName,examforms.City,examforms.RSN,docs.Image,ExamCenter.Address,ExamCenter.Address2,ExamCenter.City,examforms.Course,examforms.part from ExamForm,ExamForms,Docs,Student,ExamCenter where examform.SN=examforms.SN and ExamForm.SID=docs.SID and examform.SID=student.sid  and examforms.City=ExamCenter.City and examforms.RSN='"+ddlRSN.SelectedValue.ToString()+"' and examforms.Status='AdmitCardPrinted'  and examforms.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
            cmd1.CommandTimeout = 60 * 60;
            cmd1.ExecuteNonQuery();
            da.Dispose();
            conn.Close();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update Sucees Ready To AdmitCard Print')", true);
        }
       
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        ddlRSN.DataBind();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
        ddlRSN.DataBind();
    }

    protected void grvExcelData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExcelData.PageIndex = e.NewPageIndex;
    }
}