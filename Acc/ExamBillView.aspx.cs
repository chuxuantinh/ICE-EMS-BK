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
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Acc_ExamBillView : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo=new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                rbtnOne.Text = "Documents"; txtSession.Text = DateTime.Now.Year.ToString();
                maikal mk = new maikal();
                int sn = mk.chksession();
                if (sn == 0) ddlsession.SelectedValue = "Sum"; else ddlsession.SelectedValue = "Win";
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
            if (lvl == 0)
            {

                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {

                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {

                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    string cmqry;
    protected void btnViewRollNo_Click(object sender, EventArgs e)
    {
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
    }
    protected void GridView2_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (rbtnOne.Checked == true)
            {
                e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.TrimEnd('0').TrimEnd('.');
            }
            else if (rbtnAll.Checked == true)
            {
                e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[6].Text = e.Row.Cells[6].Text.TrimEnd('0').TrimEnd('.');
            }
        }
    }
    private DataSet GetDataSource()
    {
        if (rbtnOne.Checked == true)
        {
         cmqry="select Name,Code,Date,City,Contact,Amount from ExamBill where BillType='"+ddlBillingType.SelectedValue.ToString()+"' and ExamSeason='"+lblSessionHiddend.Text.ToString()+"'";
        }
        else if (rbtnAll.Checked == true)
        {
            cmqry = "select BillType, Name,Code,Date,City,Contact,Amount from ExamBill where ExamSeason='" + lblSessionHiddend.Text.ToString() + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }
    protected void ddlBilling_SelectedINdexChanged(object sedeer, EventArgs e)
    {
        if (ddlBillingType.SelectedValue.ToString() == "Documents")
        {
            rbtnOne.Text = "Paper & Docs.";
        }
        else if(ddlBillingType.SelectedValue.ToString()=="PaperSetter")
        {
            rbtnOne.Text = "Paper Setter";
        }
          else if(ddlBillingType.SelectedValue.ToString()=="ExamCenter")
        {
            rbtnOne.Text = "Exam Center";
        }
          else if(ddlBillingType.SelectedValue.ToString()=="Invigilator")
        {
            rbtnOne.Text = "Invigilator";
        }
          else if(ddlBillingType.SelectedValue.ToString()=="Other")
        {
            rbtnOne.Text = "Other Fees";
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition",
         "attachment;filename=ExaminationBillingReport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
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
    protected void ibtnExportExcelAppTableDoc_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
        "attachment;filename=ExaminationBillingReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        GridView2.HeaderRow.Style.Add("background-color", "#FFFFFF");
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            GridViewRow row = GridView2.Rows[i];
            row.BackColor = System.Drawing.Color.White;
            row.Attributes.Add("class", "textmode");
            if (i % 2 != 0)
            {
                row.Cells[0].Style.Add("background-color", "#C2D69B");
                row.Cells[1].Style.Add("background-color", "#C2D69B");
                row.Cells[2].Style.Add("background-color", "#C2D69B");
                row.Cells[3].Style.Add("background-color", "#C2D69B");
            }
        }
        GridView2.RenderControl(hw);
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
        "attachment;filename=ExaminationBillingReport.doc");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-word ";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView2.AllowPaging = false;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
        GridView2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataSource = GetDataSource();
        GridView2.DataBind();
    }
}
