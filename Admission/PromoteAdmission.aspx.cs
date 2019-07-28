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

public partial class Admission_Promote_Admission : System.Web.UI.Page
{
    SqlCommand cmd;
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
    string enrol;
    protected void btnVeiw_OnClick(object sender, EventArgs e)
    {
        ddlSelect.Focus();
        int i = 0;
        con.Open(); 
        while (i < GridView1.Rows.Count)
        {
            CheckBox rbApp=(CheckBox)GridView1.Rows[i].FindControl("chkapp");
            if (rbApp.Checked)
            {
               cmd = new SqlCommand("update AppRecord set Status='Filled' where AppNo='" + GridView1.Rows[i].Cells[1].Text + "'", con);
                cmd.ExecuteNonQuery();
                cmd=new SqlCommand("select * from AppRecord where AppNo='" + GridView1.Rows[i].Cells[1].Text + "' and Status='Filled'",con);
                SqlDataReader read=cmd.ExecuteReader();
                while(read.Read())
                {
                    enrol=read["Enrolment"].ToString();
                }
                read.Close(); 
                cmd=new SqlCommand("update ExamCurrent set EnrollStatus='Submitted' where SId='"+enrol+"'",con);
                cmd.ExecuteNonQuery();
            }
            i++;
        }
        con.Close();
    }
    private DataSet GetDataSource()
    {
        string qry = "";
        if (ddlSelect.SelectedValue == "Membership")
        {
            qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where Enrolment='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "' and FormType='ReAdmission' and Status='no'";
        }
        else if (ddlSelect.SelectedValue == "IMID")
        {
            qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where IMID='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "' and FormType='ReAdmission' and Status='no'";
        }
        else if (ddlSelect.SelectedValue == "SerialNo")
        {
            qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where AppNo='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "' and FormType ='ReAdmission' and Status='no'";
        }
        else if (ddlSelect.SelectedValue == "All")
        {
            qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where Session='" + lblSessionHidden.Text + "' and FormType ='ReAdmission' and Status='no'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=StudentPromotion.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grd.AllowPaging = false;
            grd.DataSource = GetDataSource();
            grd.DataBind();
            grd.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=StudentPromotion.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grd.AllowPaging = false;
            grd.DataSource = GetDataSource();
            grd.DataBind();
            grd.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=StudentPromotion.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grd.AllowPaging = false;
            grd.DataSource = GetDataSource();
            grd.DataBind();
            grd.RenderControl(hw);
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
        int i = 0;
        while (i < GridView1.Rows.Count)
        {
            i++;
        }
        lblTotalForms.Text = i.ToString();
    }
    protected void btnSelectAll_Onclick(object sender, EventArgs e)
    {
        int i = 0;
        while (i < GridView1.Rows.Count)
        {
            CheckBox rbApp = (CheckBox)GridView1.Rows[i].FindControl("chkapp");
            rbApp.Checked = true;
            i++;
        }
        GridView1.Focus();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
            string qry = "";
            if (ddlSelect.SelectedValue == "Membership")
            {
                qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where Enrolment='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "' and FormType='ReAdmission' and Status='no'";
            }
            else if (ddlSelect.SelectedValue == "IMID")
            {
                qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where IMID='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "' and FormType='ReAdmission' and Status='no'";
            }
            else if (ddlSelect.SelectedValue == "SerialNo")
            {
                qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where AppNo='" + txtEnter.Text + "' and Session='" + lblSessionHidden.Text + "' and FormType ='ReAdmission' and Status='no'";
            }
            else if (ddlSelect.SelectedValue == "All")
            {
                qry = "select AppNo as SerialNo,IMID,Enrolment as Membership,Name,Course,Part,DNo,FormType from AppRecord where Session='" + lblSessionHidden.Text + "' and FormType ='ReAdmission' and Status='no'";
            }
            SqlDataAdapter ad = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            btnView.Focus();
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelect.SelectedValue == "All")
        {
            txtEnter.Visible = false; txtEnter.Text = ""; btnOk.Focus();
        }
        else
        {
            txtEnter.Visible = true; txtEnter.Text = ""; txtEnter.Focus();
        }
    }
    protected void GridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}