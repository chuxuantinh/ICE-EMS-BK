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

public partial class Acc_InventoryReport : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                getdatatable();
                ddlsession.Focus();
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
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    private void getdatatable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("IMID");
        dt.Columns.Add("Sets");
        dt.Columns.Add("Type");
        ViewState["BookData"] = dt;
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {

        GridBooks.DataSource = getbooks() ;
        GridBooks.DataBind();
    }
    private DataTable getbooks()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["BookData"];
            dt.Clear();
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            SqlDataAdapter ad = new SqlDataAdapter("select IMID,COUNT(Enrolment) as Sets,'CivilPartI' as Type from AppRecord where Status !='NotApproved' and Status!='Hold' and FormType like '%Admission%' and Course='Civil' and Part='PartI' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "'  group by IMID order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID ,COUNT(Enrolment) as Sets,'CivilPartII' as Type from AppRecord where  Status !='NotApproved' and Status!='Hold'  and FormType like '%Admission%' and Course='Civil' and Part='PartII' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "'  group by IMID  order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID,COUNT(Enrolment) as Sets, 'CivilSectionA' as Type from AppRecord where  Status !='NotApproved' and Status!='Hold'  and FormType like '%Admission%' and Course='Civil' and Part='SectionA' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID  order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID,COUNT(Enrolment)as Sets,'CivilSectionB' as Type from AppRecord where  Status !='NotApproved' and Status!='Hold'  and FormType like '%Admission%' and Course='Civil' and Part='SectionB' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID  order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID,COUNT(Enrolment) as Sets,'ArchiPartI' as Type from AppRecord where  Status !='NotApproved' and Status!='Hold'  and FormType like '%Admission%' and Course='Architecture' and Part='PartI' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID,COUNT(Enrolment) as Sets,'ArchiPartII' as Type from AppRecord where  Status !='NotApproved' and Status!='Hold'  and FormType like '%Admission%' and Course='Architecture' and Part='PartII' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID  order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID,COUNT(Enrolment) as Sets,'ArchiSectionA' as Type from AppRecord where  Status !='NotApproved' and Status!='Hold'  and FormType like '%Admission%' and Course='Architecture' and Part='SectionA' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID  order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID,COUNT(Enrolment) as Sets,'ArchiSectionB' as Type from AppRecord where  Status !='NotApproved' and Status!='Hold'  and FormType like '%Admission%' and Course='Architecture' and Part='SectionB' and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID  order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select imid,COUNT(Enrolment) as Sets,'ArchiPartIIComp' as Type from AppRecord where Part='PartII' and Course='Architecture' and Status!='NotApproved' and Status!='Hold' and CompositeFees!=0 and Enrolment in(Select sid from Student where Part!='PartII' ) and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID ,COUNT(Enrolment) as Sets,'CivilPartIIComp' as Type from AppRecord where Part='PartII' and  Course='Civil' and  Status !='NotApproved' and Status!='Hold'  and CompositeFees!=0 and Enrolment in(Select sid from Student where Part!='PartII' )  and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "'  group by IMID  order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select imid,COUNT(Enrolment) as Sets,'ArchiSectionBComp' as Type from AppRecord where Part='SectionB' and Course='Architecture' and Status!='NotApproved' and Status!='Hold' and CompositeFees!=0 and Enrolment in(Select sid from Student where Part!='SectionB' ) and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "' group by IMID order by IMID", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select IMID ,COUNT(Enrolment) as Sets,'CivilSectionBComp' as Type from AppRecord where Part='SectionB' and  Course='Civil' and  Status !='NotApproved' and Status!='Hold'  and CompositeFees!=0 and Enrolment in(Select sid from Student where Part!='SectionB' )  and Session='" + lblhiddenSession.Text.ToString() + "'  and SubDate between '" + Convert.ToDateTime(txtDate1.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDate2.Text, dtinfo) + "'  group by IMID  order by IMID", con);
            ad.Fill(dt);
            return dt;
        }
        catch (FormatException ex)
        {
            lblException.Text = "Invalid Date Format";
            return null;
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ibtnExportPDFAppTable_Click(object sender, EventArgs e)
    {
        string strfilename = "BooksReportFrom" + txtDate1.Text + "To" + txtDate2.Text;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition",
         "attachment;filename="+strfilename+".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridBooks.AllowPaging = false;
        GridBooks.DataSource = getbooks();
        GridBooks.DataBind();
        GridBooks.RenderControl(hw);
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
    protected void ibtnExportExcelAppTable_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        string strfilename = "BooksReportFrom" + txtDate1.Text + "To" + txtDate2.Text;
        Response.AddHeader("content-disposition",
        "attachment;filename="+strfilename+".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridBooks.AllowPaging = false;
        GridBooks.DataSource = getbooks();
        GridBooks.DataBind();
        GridBooks.RenderControl(hw);
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void ibtnExportDocAppTable_click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        string strfilename = "BooksReportFrom" + txtDate1.Text + "To" + txtDate2.Text;
        Response.AddHeader("content-disposition",
        "attachment;filename="+strfilename+".doc");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-word ";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridBooks.AllowPaging = false;
        GridBooks.DataSource = getbooks();
        GridBooks.DataBind();
        GridBooks.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
   
}