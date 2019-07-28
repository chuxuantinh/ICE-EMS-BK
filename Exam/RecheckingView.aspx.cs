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

public partial class Exam_RecheckingView : System.Web.UI.Page
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
            txtEnter.Text = "";
            SubjectCode.Visible = false;
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void ddlSession_ONSelectediNdexCanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
    }
    protected void txtYear_OnTextChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
    }
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    { 
        string qry="";
        if (ddlSelect.SelectedValue == "Membership")
        {
            qry = "select * from Rechecking where SID='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "'";
        }
        else if (ddlSelect.SelectedValue.ToString() == "SerialNo")
        {
            qry = "select * from Rechecking where SubjectNo='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "'";
        }
        else if (ddlSelect.SelectedValue == "RollNo")
        {
            qry = "select * from Rechecking where RollNo='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "'";
        }
        else if (ddlSelect.SelectedValue == "IMID")
        {
            qry = "select * from Rechecking where IMID='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "'";
        }
        else if (ddlSelect.SelectedValue == "Name")
        {
            qry = "select * from Rechecking where SID IN(select SID from Student where Name like '%" + txtEnter.Text + "%') and Session='" + lblSessionHidden.Text + "'";
        }
        else if (ddlSelect.SelectedValue == "SubjectCode")
        {
            qry = "select * from Rechecking where SubID='"+ddlSubjectCode.SelectedValue+"' and Session='" + lblSessionHidden.Text + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        ViewState["dev"] = dt;
        GridView.DataSource = dt;
        GridView.DataBind();
    }
    private void fillCourse()
    {
        string qry = "";
        if (ddlCourse.SelectedValue == "Civil")
        {
            qry = "select distinct SubID from CivilSubMaster where CourseID='" + ddlCourseId.SelectedValue + "' and Section='"+ddlPart.SelectedValue+"'";
        }
        else if (ddlCourse.SelectedValue == "Architecture")
        {
            qry = "select distinct SubID from ArchiSubMaster where CourseID='" + ddlCourseId.SelectedValue + "'and Section='" + ddlPart.SelectedValue + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        ddlSubjectCode.DataSource = dt;
        ddlSubjectCode.DataValueField = "SubID";
        ddlSubjectCode.DataTextField = "SubID";
        ddlSubjectCode.DataBind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelect.SelectedValue == "SubjectCode")
        {
            fillCourse();
            SubjectCode.Visible = true;
            txtEnter.Visible = false;
        }
        else { SubjectCode.Visible = false; txtEnter.Visible = true; }
    }
    private DataTable GetDataSource()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["dev"];
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        GridView.AllowPaging = false;
        GridView.DataSource = GetDataSource();
        GridView.DataBind();
        if (GridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationFormRollNumber.doc");
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
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {GridView.AllowPaging = false;
        GridView.DataSource = GetDataSource();
        GridView.DataBind();
        if (GridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationFormRollNumber.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView.AllowPaging = false;
            GridView.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {GridView.AllowPaging = false;
        GridView.DataSource = GetDataSource();
        GridView.DataBind();
        if (GridView.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=ExaminationFormRollNumber.pdf");
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
    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
        lblTotalForms.Text=GridView.Rows.Count.ToString();
    }
    protected void ddlCourseId_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCourse();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCourse();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCourse();
    }
}