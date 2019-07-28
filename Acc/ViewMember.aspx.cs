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
using System.Globalization;

public partial class Acc_ViewMember : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal mk = new maikal();
                int sn = mk.chksession();
                if (sn == 0) ddlSession.SelectedValue = "Sum"; else ddlSession.SelectedValue = "Win";
                lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                ddlSession.Focus();
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
    protected void ddlSession_ONSelectediNdexCanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void txtYear_OnTextChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        string qry = "";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        lblGridTitle.Text = "IMID " + txtIMID.Text.ToString();
        if (ddlType.SelectedValue == "All")
        {
            qry = "select FeeType,Amt as Amount,SubDate,SubType,YearFrom,YearTo,Bank,AcountNo,DD,Balance,TransType from MemberFee where ID='" + txtIMID.Text + "' and YearFrom='" + lblSessionHidden.Text + "' ORDER BY TransID DESC ";
        }
        else
        {
            qry = "select FeeType,Amt as Amount,SubDate,SubType,YearFrom,YearTo,Bank,AcountNo,DD,Balance,TransType from MemberFee where ID='" + txtIMID.Text + "' and YearFrom='" + lblSessionHidden.Text + "' and SubType='" + ddlType.SelectedValue + "' ORDER BY TransID DESC ";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridAC.DataSource = dt;
        GridAC.DataBind();
    }
    protected void GridAC_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GridViewRow row;
        row = GridAC.SelectedRow;
    }
    protected void GridAC_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmdEdit")
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            Response.Redirect("EditMainAC.aspx?acid=" + arg[0] + "&DNo=" + arg[1]);
        }
    }
    protected void GridAC_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[9].Text = e.Row.Cells[9].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    protected void GridAC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridAC.PageIndex = e.NewPageIndex;
        GridAC.DataSource = GetDataSource();
        GridAC.DataBind();
    }
    private DataSet GetDataSource()
    {
        string cqry = "";
        if (ddlType.SelectedValue == "All")
        {
            cqry = "select FeeType,Amt as Amount,SubDate,SubType,YearFrom,YearTo,Bank,AcountNo,DD,Balance,TransType from MemberFee where ID='" + txtIMID.Text + "' and YearFrom='" + lblSessionHidden.Text + "' ORDER BY TransID DESC ";
        }
        else
        {
            cqry = "select FeeType,Amt as Amount,SubDate,SubType,YearFrom,YearTo,Bank,AcountNo,DD,Balance,TransType from MemberFee where ID='" + txtIMID.Text + "' and YearFrom='" + lblSessionHidden.Text + "' and SubType='" + ddlType.SelectedValue + "' ORDER BY TransID DESC ";
        }
        SqlDataAdapter ad = new SqlDataAdapter(cqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        if (GridAC.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=MembershipAccount.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridAC.AllowPaging = false;
            GridAC.DataSource = GetDataSource();
            GridAC.DataBind();
            GridAC.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        if (GridAC.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=MembershipAccount.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridAC.AllowPaging = false;
            GridAC.DataSource = GetDataSource();
            GridAC.DataBind();

            GridAC.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        if (GridAC.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=MembershipAccount.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridAC.AllowPaging = false;
            GridAC.DataSource = GetDataSource();
            GridAC.DataBind();
            GridAC.RenderControl(hw);
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
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnView.Focus();
    }
}