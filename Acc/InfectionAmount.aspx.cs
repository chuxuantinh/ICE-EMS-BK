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
public partial class Acc_InfectionAmount : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
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
                  pnlAmount.Visible = false;
                  viewGrid();
                  txtIMID.Visible = false;
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
    protected void ddlViewBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddViewBy.SelectedValue.ToString() == "All")
        {
            txtIMID.Visible = false;
        }
        else if (ddViewBy.SelectedValue.ToString() == "IMID")
        {
            txtIMID.Visible = true;
        }
        ddlIMStatus.Focus();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        viewGrid();
    }
    private void viewGrid()
    {
        string qry = "";
        if (ddViewBy.SelectedValue.ToString() == "All")
        {
            qry = "SELECT [SN], [ID], [Name], [Investigator], [Designation], [Date], [Address], [Phone], [Status], [FeedBack], [BuildingStatus], [EduStatus] FROM [IMInspection] WHERE ([Status] = '" + ddlIMStatus.SelectedValue.ToString() + "')";
        }
        else qry = "SELECT [SN], [ID], [Name], [Investigator], [Designation], [Date], [Address], [Phone], [Status], [FeedBack], [BuildingStatus], [EduStatus] FROM [IMInspection] WHERE ([Status] = '"+ddlIMStatus.SelectedValue.ToString()+"') and ID='"+txtIMID.Text.ToString()+"'";
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ViewAC(txtIMID.Text.ToString());
    }
    private void ViewAC(string imid)
    {
       SqlDataAdapter ad = new SqlDataAdapter("select SubDate as Date,DD as DDNo,Bank,SubType,YearFrom as Session,Amt as Amount,TransType,Balance from MemberFee where ID='" + imid.ToString() + "' ORDER BY TransID DESC", con);
       DataTable dt = new DataTable();       
        ad.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[0].Text = Convert.ToDateTime(e.Row.Cells[0].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void GridView2_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlIMStatus.SelectedValue.ToString() != "NotApprove")
                e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text, dtinfo).ToString("dd/MM/yyyy");
            if (ddlIMStatus.SelectedValue.ToString() != "NotApprove")
                e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
    protected void GridView2_OnSelectedIndexChanged(object sendcer, EventArgs e)
    {
        con.Close();
        con.Open();
        pnlAmount.Visible = true;
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("select Amount from DiaryEntry where IMID='" + GridView2.SelectedRow.Cells[2].Text.ToString() + "'", con);
        lblDiaryAmt.Text = Convert.ToString(cmd.ExecuteScalar());
        lblDiaryAmt.Text = lblDiaryAmt.Text.TrimEnd('0').TrimEnd('.');
        cmd = new SqlCommand("Select Balance from MemberFee where ID='" + GridView2.SelectedRow.Cells[2].Text.ToString() + "' and TransID = (select max(TransID) from MemberFee where ID='"+GridView2.SelectedRow.Cells[2].Text+"')", con);
        lblToAmt.Text = Convert.ToString(cmd.ExecuteScalar());
       lblToAmt.Text= lblToAmt.Text.TrimEnd('0').TrimEnd('.');
       lblEnrolment.Text = GridView2.SelectedRow.Cells[2].Text.ToString();
       txtRefund.Focus();
       con.Close();
       ViewAC(GridView2.SelectedRow.Cells[2].Text.ToString());
       con.Dispose();
    }
    protected void txtRefund_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            int Tamt = Convert.ToInt32(lblDiaryAmt.Text);
            int amt = Convert.ToInt32(txtRefund.Text);
            if (amt > Tamt)
            {
                lblException.Text = "Refund can't be greater then Diary Amount.";
            }
            else
            {
                lblException.Text = "";
            }
        }
        catch (FormatException ex)
        {
            lblException.Text = "Please Insert Correct Amount.";
        }
    }
    protected void btnSAve_Onclick(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            int x=0,y=0, w=0; string v="";
            int amount = Convert.ToInt32(txtRefund.Text);
            string str = "select IMID,Total,Credit,GID,GTotal from IMAC where IMID='" + lblEnrolment.Text.ToString() + "'  ";
            cmd = new SqlCommand(str, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                x = Convert.ToInt32(rd["Credit"]);
                y = Convert.ToInt32(rd["LateFeeTaken"]);
                w = Convert.ToInt32(rd["GTotal"]);
                v = (rd["GID"]).ToString();
                w = w - x-y;                
            }
            rd.Close();
            string str1 = "update  IMAC set Total=@imactotal,Credit=@credit where IMID='" + lblEnrolment.Text + "'";
            cmd = new SqlCommand(str1, con);
            cmd.Parameters.AddWithValue("@imactotal", 0);
            cmd.Parameters.AddWithValue("@credit", 0);
            cmd.ExecuteNonQuery();
            string str2 = "update IMAC set GTotal=@gtotal where GID='" + v + "'";
            cmd = new SqlCommand(str2, con);
            cmd.Parameters.AddWithValue("@gtotal", w);
            cmd.ExecuteNonQuery();
            maikal dev = new maikal();
            int se = dev.chksession(); string session;
            if (se == 0) { session = "Sum"; }
            else { session = "Win"; }
            session = session + DateTime.Now.Year.ToString();
            ClsAccount cl = new ClsAccount();
            cl.AmountSubmit(lblEnrolment.Text.ToString(), "", DateTime.Now, "Debit",lblToAmt.Text.ToString(), session.ToString(), "Membership ReFund");
            cmd = new SqlCommand("select max(TransID) from  MemberFee where ID='" + lblEnrolment.Text.ToString() + "'", con);
            string tid = Convert.ToString(cmd.ExecuteScalar());
            cmd = new SqlCommand("insert into MemberFee (MType, ID, Amt, FeeType, SubDate, SubType, AcountNo, DD, Bank, YearFrom, YearTo,TransType,Balance,TransID) values(@MType, @ID, @Amt, @FeeType, @SubDate, @SubType, @AcountNo, @DD, @Bank, @YearFrom, @YearTo, @TransType, @Balance,@TransID)", con);
            cmd.Parameters.AddWithValue("@MType", "IM");
            cmd.Parameters.AddWithValue("@ID", lblEnrolment.Text.ToString());
            cmd.Parameters.AddWithValue("@Amt",txtRefund.Text);
            cmd.Parameters.AddWithValue("@FeeType", "Membership ReFund");
            cmd.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(DateTime.Now, dtinfo));
            cmd.Parameters.AddWithValue("@SubType", "N/A");
            cmd.Parameters.AddWithValue("@AcountNo", "N/A");
            cmd.Parameters.AddWithValue("@DD", "N/A");
            cmd.Parameters.AddWithValue("@Bank", "N/A");
            cmd.Parameters.AddWithValue("@YearFrom", session);
            cmd.Parameters.AddWithValue("@YearTo", "");
            cmd.Parameters.AddWithValue("@TransType", "Credit");
            cmd.Parameters.AddWithValue("@Balance", 0);
            cmd.Parameters.AddWithValue("@TransID", tid + 1);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update IMInspection set Status=@Status,Refund=@Refund,TotalAmount=@TotalAmount where ID='" + lblEnrolment.Text.ToString() + "' and SN='" + GridView2.SelectedRow.Cells[1].Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Status", "ReFund");
            cmd.Parameters.AddWithValue("@Refund", txtRefund.Text.ToString());
            cmd.Parameters.AddWithValue("@TotalAmount", lblToAmt.Text.ToString());
            cmd.ExecuteNonQuery();
            lblException.Text = "Refund Amount Saved.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Refund Amount Saved.')", true);
            con.Close();
            con.Dispose();
        }
        catch (Exception ex)
        {

        }
    
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, EventArgs e)
    {
        GridView2.AllowPaging = false;
        viewGrid();
        if (GridView2.Rows.Count > 0)
        {
            GridView2.Columns[0].Visible = false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=Inspection.pdf");
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
        viewGrid();
        if (GridView2.Rows.Count > 0)
        {
            GridView2.Columns[0].Visible = false;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=Inspection.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView2.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, EventArgs e)
    {
         GridView2.AllowPaging = false;
        viewGrid();
        if (GridView2.Rows.Count > 0)
        {
            GridView2.Columns[0].Visible = false;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=Inspection.doc");
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
}
