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

public partial class Acc_ViewAccount : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAC.Visible = false;
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal mk = new maikal();
                int sn = mk.chksession();
                if (sn == 0) ddlSession.SelectedValue = "Sum"; else ddlSession.SelectedValue = "Win";
                lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                pnlDiary.Visible = false; panlSession.Visible = false; rbtnICE.Checked = true;
                rbtnICE.Focus();
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
    protected void rbtnDiary_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlDiary.Visible = true; pnlDate.Visible = false; panlSession.Visible = false;
        txtDateFrom.Text = ""; txtDateto.Text = ""; txtIMID.Text = "";
        pnlAC.Visible = false;
        rbtnDiary.Focus();
    }
    protected void rbtnICE_OnCheckedChanged(object sender, EventArgs e)
    {
        txtIMID.Text = ""; txtDiary.Text = "";
        pnlDate.Visible = true; pnlDiary.Visible = false; panlSession.Visible = false;
        pnlAC.Visible = false;
        rbtnICE.Focus();
    }
    protected void rbtnIMD_CheckedChanged(object sender, EventArgs e)
    {
        txtDateFrom.Text = ""; txtDateto.Text = ""; txtIMID.Text = "";
        panlSession.Visible = true; pnlDiary.Visible = false; pnlDate.Visible = false;
        pnlAC.Visible = true;
        rbtnIM.Focus();
    }
    protected void ddlSession_ONSelectediNdexCanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void txtYear_OnTextChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtIMID.Focus();
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
            string qry = "";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Close(); con.Open();
            if (rbtnICE.Checked == true)
            {
                lblGridTitle.Text = "Date: " + txtDateFrom.Text + " To: " + txtDateto.Text.ToString();
                qry = "select IMID,DiaryNo,Session,Date,Amount,Type,Balance,Details from Account where Date Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "' ORDER BY SN DESC";
            }
            else if (rbtnIM.Checked == true)
            {
                lblGridTitle.Text = "IMID: " + txtIMID.Text.ToString() + " and Session: " + lblSessionHidden.Text.ToString();
                qry = "select IMID,DiaryNo,Session,Date,Amount,Type,Balance,Details from Account where Session='" + lblSessionHidden.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' ORDER BY SN DESC";
            }
            else if (rbtnDiary.Checked == true)
            {
                lblGridTitle.Text = "Diary No. " + txtDiary.Text.ToString();
                qry = "select IMID,DiaryNo,Session,Date,Amount,Type,Balance,Details from Account where DiaryNo='" + txtDiary.Text.ToString() + "' ORDER BY SN DESC";
            }
            SqlDataAdapter ad = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridAC.DataSource = dt;
            GridAC.DataBind();
            GridAC.Focus();
        }
        catch (SqlException ex)
        {
        }
        catch (FormatException ex)
        {
        }
    }
    protected void GridAC_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    private DataSet GetDataSource()
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        string cqry = "";
        if (rbtnICE.Checked == true)
        {
            lblGridTitle.Text = "Date: " + txtDateFrom.Text + " To: " + txtDateto.Text.ToString();
            cqry = "select IMID,DiaryNo,Session,Date,Amount,Type,Balance from Account where Date Between '" + Convert.ToDateTime(txtDateFrom.Text, dtinfo) + "' and '" + Convert.ToDateTime(txtDateto.Text, dtinfo) + "' ORDER BY SN DESC";
        }
        else if (rbtnIM.Checked == true)
        {
            lblGridTitle.Text = "IMID: " + txtIMID.Text.ToString() + " and Session: " + lblSessionHidden.Text.ToString();
            cqry = "select IMID,DiaryNo,Session,Date,Amount,Type,Balance from Account where Session='" + lblSessionHidden.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' ORDER BY SN DESC";
        }
        else if (rbtnDiary.Checked == true)
        {
            lblGridTitle.Text = "Diary No. " + txtDiary.Text.ToString();
            cqry = "select IMID,DiaryNo,Session,Date,Amount,Type,Balance from Account where DiaryNo='" + txtDiary.Text.ToString() + "' ORDER BY SN DESC";
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
            "attachment;filename=ExaminationFormRollNumber.doc");
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
            "attachment;filename=ExaminationFormRollNumber.xls");
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
             "attachment;filename=ExaminationFormRollNumber.pdf");
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
    protected void GridAC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridAC.DataSource = GetDataSource();
        GridAC.PageIndex = e.NewPageIndex;
        GridAC.DataBind();
    }
    protected void txtIMID_TextChanged(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from IMAC where IMID='" + txtIMID.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            pnlAC.Visible = true;
            lblIMID.Text = reader["IMID"].ToString();
            lbltotal.Text = reader["Total"].ToString().TrimEnd('0').TrimEnd('.');
            lblGtotal.Text = reader["GTotal"].ToString().TrimEnd('0').TrimEnd('.');
            lbllateFees.Text = reader["LateFeeTaken"].ToString().TrimEnd('0').TrimEnd('.');
            lblDues.Text = reader["Credit"].ToString().TrimEnd('0').TrimEnd('.');
            lblBooksAmount.Text = reader["IMTotal"].ToString().TrimEnd('0').TrimEnd('.');
            lblProspectus.Text = reader["Prospectus"].ToString().TrimEnd('0').TrimEnd('.');
        }
        else pnlAC.Visible = false;
        reader.Close();
        con.Close(); con.Dispose();
        txtIMID.Focus();
    }
}