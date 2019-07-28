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

public partial class Exam_GenViewRollNo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
            lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridView1.SelectedRow;
        txtExamCenter.Text = gr.Cells[1].Text.ToString();
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        lblCenterCode.Text = gr.Cells[1].Text.ToString();
        con.Close();
        con.Open(); SqlCommand cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        con.Close(); con.Dispose();
        if (sum == "")
            lblCapacity.Text = "0";
       else 
            lblCapacity.Text = sum.ToString();
        }
    string cmqry;
   
    private DataSet GetDataSource()
    {
        
     if (ddlPart.SelectedValue == "All")
        {
            cmqry = "select Part,IMID,SID,RollNo,ExamSeason,City from ExamForms where CenterCode='" + lblCenterCode.Text.ToString() + "'  and ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and (Status='RollNoGenerated' or Status='AdmitCardGenerated') order by RollNo";

        }
        else
            cmqry = "select Part,IMID,SID,RollNo,ExamSeason,City from ExamForms where CenterCode='" + lblCenterCode.Text.ToString() + "'  and ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and (Status='RollNoGenerated' or Status='AdmitCardGenerated') and Course='" + ddlCourse.SelectedValue + "' and Part='" + ddlPart.SelectedValue + "' order by RollNo";
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
      
        return dt;
      
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, EventArgs e)
    {GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=ExaminationFormRollNumber.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView2.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, EventArgs e)
    {GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationFormRollNumber.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView2.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, EventArgs e)
    {
        GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationFormRollNumber.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView2.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataSource = GetDataSource();
            GridView2.DataBind();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd=new SqlCommand("SELECT * FROM [ExamCenter] WHERE ID='"+txtExamCenter.Text+"'",con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblCenteNaem.Text = rd["Name"].ToString();
            lblCenterCode.Text = rd["ID"].ToString();

        }
        rd.Close();
        rd.Dispose();
         cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        con.Close(); con.Dispose();
        if (sum == "")
            lblCapacity.Text = "0";
        else
            lblCapacity.Text = sum.ToString();
                con.Close(); con.Dispose();

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCount.Text = "";
        if (lblCenterCode.Text != "")
        {
            GridView2.DataSource = GetDataSource();
            GridView2.DataBind();
        }
        GridView2.Focus();
        ddlPart.Focus();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCount.Text = "";
        if (lblCenterCode.Text != "")
        {
            GridView2.DataSource = GetDataSource();
            GridView2.DataBind();
        }
        GridView2.Focus();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        lblCount.Text = GridView2.Rows.Count.ToString();
    }
}
