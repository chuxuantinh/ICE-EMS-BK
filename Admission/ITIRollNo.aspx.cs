using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Globalization;


public partial class Admission_ITIRollNo : System.Web.UI.Page
{ 
    SqlDataAdapter adp;
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                VerifyRenderingInServerForm(this);
                if (!IsPostBack)
                {
                    txtYear.Text = DateTime.Now.Year.ToString();
                    maikal mk = new maikal();
                    int sn = mk.chksession();
                    if (sn == 0) ddlSessionSelect.SelectedValue = "Sum"; else ddlSessionSelect.SelectedValue = "Win";
                    lblSessionHidden.Text = ddlSessionSelect.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                    ddlSessionSelect.Focus();
                    grviti.DataSource = disp(); grviti.DataBind();
                    panRollNo.Visible = true;
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected DataTable disp()
    {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            string strqry = "select SNO,RollNo, Name,FName,SID,IMID,AppNo,Stream,Course,Part,DOB,Session,SubDate, Amount,Status,ExamDate,Password from ITIForm where Status='" + ddlStatus.SelectedValue + "' and Session='"+lblSessionHidden.Text+"' order by convert(int, RollNo)";         
            adp = new SqlDataAdapter(strqry, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
    }
    protected void ddlSession_ONSelectediNdexCanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSessionSelect.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        grviti.DataSource = disp(); grviti.DataBind();
        txtYear.Focus();
    }
    protected void txtYear_OnTextChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSessionSelect.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        grviti.DataSource = disp(); grviti.DataBind();

    }
    protected void grviti_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grviti.PageIndex = e.NewPageIndex;
        grviti.DataSource = disp();
        grviti.DataBind();
    }
    int flag = 0;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        lblStatus.Text = "";
        try
        {
            lblStatus.Text = "";
            con.Open();
            string pwd;
            for (int i = 0; i < grviti.Rows.Count; i++)
            {
                string a = (string)grviti.DataKeys[i][0];
                CheckBox cd = (CheckBox)grviti.Rows[i].FindControl("chkStatus");
                if (cd.Checked)
                {
                    flag = 1;
                    pwd = grviti.Rows[i].Cells[3].Text.ToString();
                    pwd = pwd.Substring(0, 1);
                    string strqry = "select max(RollNo) from ITIForm where Session='" + grviti.Rows[i].Cells[12].Text.ToString() + "'";
                    cmd = new SqlCommand(strqry, con);
                    string str = Convert.ToString(cmd.ExecuteScalar());
                    if (str == "")
                    {
                        str = "1000";
                    }
                    if (str != "")
                    {
                        int roll = Convert.ToInt32(str);
                        roll = roll + 1;
                        pwd = pwd + DateTime.Now.ToString("hhss");
                        pwd = pwd + roll;
                        cmd = new SqlCommand("update ITIForm set RollNo=@RollNo, ExamDate=@ExamDate, Status=@Status, Password=@password where SID=@SID", con);
                        cmd.Parameters.AddWithValue("@RollNo", roll);
                        cmd.Parameters.AddWithValue("@Password", pwd);
                        cmd.Parameters.AddWithValue("@Status", "RollNoGenerated");
                        cmd.Parameters.AddWithValue("@ExamDate", Convert.ToDateTime(txtExamDate.Text, dtinfo));
                        cmd.Parameters.AddWithValue("@SID", grviti.Rows[i].Cells[5].Text.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
           }
            if (flag == 1)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('RollNo successfully Generated')", true);                  
            if (flag == 0)
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select atleast One Record')", true);  
            grviti.DataSource = disp();
            grviti.DataBind();
        }              
         catch (SqlException ex)
        {
            lblStatus.Text = ex.ToString();           
        }
        catch (System.FormatException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Wrong Date Format')", true);                   
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    }
    protected void grviti_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Text = Convert.ToDateTime(e.Row.Cells[11].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[13].Text = Convert.ToDateTime(e.Row.Cells[13].Text).ToString("dd/MM/yyyy");
            if (ddlStatus.SelectedValue == "RollNoGenerated")
                e.Row.Cells[16].Text = Convert.ToDateTime(e.Row.Cells[16].Text).ToString("dd/MM/yyyy");
        }
    }
    int flag2 = 0;
    protected void btnApprove_Click(object sender, EventArgs e)
    {     
        con.Open();
        for (int i = 0; i < grviti. Rows.Count; i++)
        {
            string a = (string)grviti.DataKeys[i][0];
            CheckBox cd = (CheckBox)grviti.Rows[i].FindControl("chkStatus");
            if (cd.Checked)
            {
                flag2 = 1;
                cmd = new SqlCommand("update ITIForm set Status=@Status where SID=@SID", con);
                cmd.Parameters.AddWithValue("@Status", "Qualified");
                cmd.Parameters.AddWithValue("@SID", grviti.Rows[i].Cells[5].Text.ToString());
                cmd.ExecuteNonQuery();
            }            
        }
        grviti.DataSource = disp();
        grviti.DataBind();
        if (flag2 == 1)
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Updated')", true);

        else ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please select atleast one record')", true);  
        con.Close();
        con.Dispose();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        grviti.DataSource = disp();
        grviti.DataBind();
        if (ddlStatus.SelectedValue == "RollNoGenerated")
        {
            pnlStudent.Visible = true;
            panRollNo.Visible = false;
        }
        else
        {
            pnlStudent.Visible = false;
            panRollNo.Visible = true;
        }
    }
    protected void btnfail_Click(object sender, EventArgs e)
    {
        flag2 = 0; 
         con.Open();
         for (int i = 0; i < grviti.Rows.Count; i++)
         {
             string a = (string)grviti.DataKeys[i][0];
             CheckBox cd = (CheckBox)grviti.Rows[i].FindControl("chkStatus");
             if (cd.Checked)
             {
                 flag2 = 1;
                 cmd = new SqlCommand("update ITIForm set Status=@Status where SID=@SID", con);
                 cmd.Parameters.AddWithValue("@Status", "Disqualify");
                 cmd.Parameters.AddWithValue("@SID", grviti.Rows[i].Cells[5].Text.ToString());
                 cmd.ExecuteNonQuery();

             }
         }
                  grviti.DataSource = disp();
                   grviti.DataBind();
      if(flag2==1)
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Updated')", true);                 
     if(flag2==0)
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please select atleast one record')", true);              
         con.Close();
         con.Dispose();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ibtnExportPDFAppTable_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition",
         "attachment;filename=ApplicationAdded.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grviti.AllowPaging = false;
        grviti.DataSource = disp();
        grviti.DataBind();
        grviti.RenderControl(hw);
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
        Response.AddHeader("content-disposition",
        "attachment;filename=ApplicationFormAdded.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grviti.AllowPaging = false;
        grviti.DataSource = disp();
        grviti.DataBind();
        grviti.RenderControl(hw);
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void ibtnExportDocAppTable_click(object sender, EventArgs e)
    {
        grviti.AllowPaging = false;
        
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
        "attachment;filename=ApplicationFormSerialNumber.doc");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-word ";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grviti.DataSource = disp();
        grviti.DataBind();
        grviti.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
}