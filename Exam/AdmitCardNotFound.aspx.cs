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

public partial class Exam_AdmitCardNotFound : System.Web.UI.Page
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
                ddlSelect.SelectedValue = "IM";
                ddlList.Visible = false;
                txtIMID.Visible = true;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                {
                    ddlExamSeason.SelectedValue = "Sum";
                }
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
            {

                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
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

    private DataSet GetDataSource()
    {
        string cmqry = ""; 
        if (ddlSelect.SelectedValue == "IM")
        {
            if (rbtnGenerated.Checked == true)
                cmqry = "select DISTINCT SID,RollNo,ExamSession,IMID,Stream,Course,Part,City,Status from ExamForm where ExamSession='" + lblSeasonHidden.Text.ToString() + "' and RollStatus='yes' and IMID='"+txtIMID.Text+"'";
            else if (rbtnNotGenerated.Checked == true)
                cmqry = "select DISTINCT SID,RollNo,ExamSession,IMID,Stream,Course,Part,City,Status from ExamForm where ExamSession='" + lblSeasonHidden.Text.ToString() + "' and RollStatus!='yes' and IMID='" + txtIMID.Text + "'";

        }
        else if (ddlSelect.SelectedValue == "ExamCenter")
        {
            if (rbtnGenerated.Checked == true)
                cmqry = "select DISTINCT SID,RollNo,ExamSession,IMID,Stream,Course,Part,City,Status from ExamForm where ExamSession='" + lblSeasonHidden.Text.ToString() + "' and RollStatus='yes' and Centername='"+ddlList.SelectedValue+"'";
            else if (rbtnNotGenerated.Checked == true)
                cmqry = "select DISTINCT SID,RollNo,ExamSession,IMID,Stream,Course,Part,City,Status from ExamForm where ExamSession='" + lblSeasonHidden.Text.ToString() + "' and RollStatus!='yes' and Centername='" + ddlList.SelectedValue + "'";

        }
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, EventArgs e)
    {
        GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=AdmitCard.pdf");
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
    {
        GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=AdmitCard.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView2.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=AdmitCard.doc");
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
    protected void rbtnGenerated_CheckedChanged(object sender, EventArgs e)
    {
        //lblAdmit.Text = "Admit Card Generated List";
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        rbtnGenerated.Focus();
    }
    protected void rbtnNotGenerated_CheckedChanged(object sender, EventArgs e)
    {
        //lblAdmit.Text = "Admit Card Not Generated List";
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        rbtnNotGenerated.Focus();

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "" | e.Row.Cells[1].Text == "&nbsp;")
            {
                e.Row.Cells[1].Text = "N/A";
            }
            if (rbtnNotGenerated.Checked == true)
            {
                e.Row.Cells[8].Text = "Admit Card Not Found";
            }
            else if (rbtnGenerated.Checked == true)
            {
                e.Row.Cells[8].Text = "Admit Card Found";
            }
        }
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        string qry = "";
        if (ddlSelect.SelectedValue == "IM")
        {
            ddlList.Visible = false;
            txtIMID.Visible = true;
            txtIMID.Focus();
        }
        if (ddlSelect.SelectedValue == "ExamCenter")
        {
            ddlList.Visible = true;
            txtIMID.Visible = false;
            ddlList.Focus();
        }
    
    }
    protected void ddlList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        rbtnGenerated.Focus();
    }
    protected void txtIMID_TextChanged(object sender, EventArgs e)
    {
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        rbtnGenerated.Focus();
    }
}