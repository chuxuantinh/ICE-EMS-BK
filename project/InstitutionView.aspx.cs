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


public partial class project_AllocatedProject : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    grdDetails.DataSource = GetDataSource();
                    grdDetails.DataBind();
                    droptype.Focus();
                }
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
    protected void lbtnBuildingTitel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IMBuilding.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
    {
       grdDetails.DataSource= GetDataSource();
       grdDetails.DataBind();
    }
    private DataSet GetDataSource()
    {
        string qry = "";
        if (droptype.SelectedValue == "CivilPartII")
        {
            qry = "select * from InstitutionReg where CivilpartII='YES' ";
        }
        else if (droptype.SelectedValue == "CivilSectionB")
        {
            qry = "select * from InstitutionReg where CivilSectionB='YES' ";
        }
        else if (droptype.SelectedValue == "ArchiPartII")
        {
            qry = "select * from InstitutionReg where ArchiPartII='YES' ";
        }
        else if (droptype.SelectedValue == "ArchiSectionB")
        {
            qry = "select * from InstitutionReg where ArchiSectionB='YES'";
        }
        SqlDataAdapter adp = new SqlDataAdapter(qry, con);
        DataSet dt = new DataSet();
        adp.Fill(dt);
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        try
        {
            grdDetails.AllowPaging = false;
            grdDetails.DataSource = GetDataSource();
            grdDetails.DataBind();
            if (grdDetails.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                "attachment;filename=AccountDDReport.doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                grdDetails.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (FormatException ex)
        {

        }
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            grdDetails.AllowPaging = false;
            grdDetails.DataSource = GetDataSource();
            grdDetails.DataBind();
            if (grdDetails.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                "attachment;filename=AccountDDReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grdDetails.RenderControl(hw);
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (FormatException ex)
        {

        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            grdDetails.AllowPaging = false;
            grdDetails.DataSource = GetDataSource();
            grdDetails.DataBind();
            if (grdDetails.Rows.Count > 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition",
                 "attachment;filename=AccountDDReport.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grdDetails.RenderControl(hw);
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
        catch (FormatException ex)
        {
        }
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i = 0; 
        while (i < grdDetails.Rows.Count)
        {
            i++;
        }
        lblCount.Text = "Total Count :"+ i.ToString();
    }
}
