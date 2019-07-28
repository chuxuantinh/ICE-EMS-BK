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

public partial class Admission_ITIApplication : System.Web.UI.Page
{    
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                txtEnter.Text = "";
                txtEnter.Visible = false;
                pnlDate.Visible = true;
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
    protected void ddlSession_ONSelectediNdexCanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void txtYear_OnTextChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        ddlSelect.Focus();
    }
    private DataSet GetDataSource()
    {
        string qry = "";
        if (ddlSelect.SelectedValue == "FormNo")
        {
            string formtype = txtEnter.Text + "ITI";
            qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm  where  Session='" + lblSessionHidden.Text + "' and SID='" + txtEnter.Text + "'  order by convert(int,AppNo)";
        }
        else if (ddlSelect.SelectedValue == "IMID")
        {
            qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm  where IMID='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "'  order by convert(int,AppNo)";
        }
        else if (ddlSelect.SelectedValue == "All")
        {
            qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm  where  Session='" + lblSessionHidden.Text + "' order by convert(int,AppNo) ";
        }
        else if (ddlSelect.SelectedValue == "Date")
        {
            qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm  where  SubDate Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "'  order by convert(int,AppNo)";
        } SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridView.AllowPaging = false;
            GridView.DataSource = GetDataSource();
            GridView.DataBind();
            if (GridView.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                "attachment;filename=ITIApplicationForms.doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (FormatException ex)
        {
            lblMessageExc.Text = "Please Insert Correct Date.";
        }
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridView.AllowPaging = false;
            GridView.DataSource = GetDataSource();
            GridView.DataBind();
            if (GridView.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                "attachment;filename=ITIApplicationForms.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (FormatException ex)
        {
            lblMessageExc.Text = "Please Insert Correct Date.";
        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridView.AllowPaging = false;
            GridView.DataSource = GetDataSource();
            GridView.DataBind();
            if (GridView.Rows.Count > 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition",
                 "attachment;filename=ITIApplicationForms.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView.RenderControl(hw);
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
            lblMessageExc.Text = "Please Insert Correct Date.";
        }
    }
    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).ToShortDateString();
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToShortDateString();
        }
        int i = 0;
        while (i < GridView.Rows.Count)
        {
            i++;
        }
        lblTotalForms.Text = i.ToString();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        lblTotalForms.Text = "";
        try
        {
            if (txtEnter.Text == "" && txtDateFrom.Text == "" && txtDateto.Text == "" && ddlSelect.SelectedValue.ToString()!="All")
            {
                lblMessageExc.Text = "Please Fill Details.!";
            }
            else
            {
                string qry = "";
                if (ddlSelect.SelectedValue == "FormNo")
                {
                    qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm where  Session='" + lblSessionHidden.Text + "' and SID='" + txtEnter.Text + "' order by convert(int,AppNo) ";
                }
                else if (ddlSelect.SelectedValue == "IMID")
                {
                    qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm where IMID='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "' order by convert(int,AppNo) ";
                }
                else if (ddlSelect.SelectedValue == "All")
                {
                    qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm  where  Session='" + lblSessionHidden.Text + "'  order by convert(int,AppNo)";
                }
                else if (ddlSelect.SelectedValue == "Date")
                {
                    qry = "select SID as SerialNo,IMID,Name,Course,Part,DOB,SubDate,Session from ITIForm  where Session='" + lblSessionHidden.Text + "' and SubDate Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "'  order by convert(int,AppNo)";
                }
                SqlDataAdapter ad = new SqlDataAdapter(qry, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                GridView.DataSource = dt;
                GridView.DataBind();
                lblMessageExc.Text = "";
            }
        }
        catch (FormatException ex)
        {
            lblMessageExc.Text = "Please Insert Correct Date.";
        }
        ddlSession.Focus();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelect.SelectedValue == "All")
        {
            txtEnter.Visible = false; txtEnter.Text = ""; pnlDate.Visible = false;
        }
        else if (ddlSelect.SelectedValue == "Date")
        {
            txtEnter.Visible = false; txtEnter.Text = ""; pnlDate.Visible = true;
        }
        else
        {
            txtEnter.Visible = true; txtEnter.Text = ""; pnlDate.Visible = false;
        }
        ddlSelect.Focus();
        lblMessageExc.Text = "";
    }
}