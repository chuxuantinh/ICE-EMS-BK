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

public partial class Exam_ViewSeating : System.Web.UI.Page
{
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
        if (!IsPostBack)
        {
            
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0)
            {
                ddlExamSeason.SelectedValue = "Sum";
            }
            else { ddlExamSeason.SelectedValue = "Win"; }
            lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void ddlExamDate_SelectedIndexChanged(object sneder, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        lblExamDAte.Text = Convert.ToDateTime(ddlExaminationdate.SelectedValue.ToString()).ToString("dd/MM/yyy");
        lblDate.Text = lblExamDAte.Text;
        lblshift.Text = ddlShift.SelectedValue.ToString();
        GridView1.DataSource = null;
        GridView1.DataBind();
        ddlShift.Focus();
    }
    protected void ddlShift_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblExamDAte.Text = Convert.ToDateTime(ddlExaminationdate.SelectedValue.ToString()).ToString("dd/MM/yyy");
        lblDate.Text = lblExamDAte.Text;
        lblshift.Text = ddlShift.SelectedValue.ToString();
    }
    protected void GridExamCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridView2.SelectedRow;
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        lblCenterCode.Text = gr.Cells[1].Text.ToString();
        lblCity.Text = gr.Cells[3].Text.ToString();
        con.Close();
        con.Open(); SqlCommand cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        con.Close(); con.Dispose();
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        else 
        {
            lblCapacity.Text = sum.ToString();
        }
    }
    protected void btnCenterCode_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        SqlCommand cmd = new SqlCommand("select * from  ExamCenter where ID='" + Convert.ToInt32(txtExamCode.Text) + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblCenterCode.Text = reader["ID"].ToString();
            lblCenteNaem.Text = reader["Name"].ToString();
            lblCity.Text = reader["City"].ToString();
            lblExceptionCode.Text = "";
        }
        else
        {
            lblExceptionCode.Text = "Invalid Exam Center Code";
        }
        reader.Close();
        reader.Dispose();
        SqlCommand cmdw = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmdw.ExecuteScalar());
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        {
            lblCapacity.Text = sum.ToString();
        }
        lblExamDAte.Text = Convert.ToDateTime(ddlExaminationdate.SelectedValue.ToString()).ToString("dd/MM/yyy");
        lblDate.Text = lblExamDAte.Text;
        lblshift.Text = ddlShift.SelectedValue.ToString();
        GridView1.DataSource = null;
        GridView1.DataBind();
        con.Close();
        con.Dispose();
        ddlExaminationdate.Focus();
    }
    protected void GridView2_PageIndexChangeing(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, EventArgs e)
    {GridView1.AllowPaging = false;
        GridView1.DataSource = GetDataSource();
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=ExaminationSeatingArrangement.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
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
    {GridView1.AllowPaging = false;
        GridView1.DataSource = GetDataSource();
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationSeatingArrangement.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, EventArgs e)
    {GridView1.AllowPaging = false;
        GridView1.DataSource = GetDataSource();
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ExaminationSeatingArrangement.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = GetDataSource();
        GridView1.DataBind();
    }
    private  string col1,col2,col3;
    DataTable dt = new DataTable();
    private DataTable GetDataSource()
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("select RoomNo from SeatingArrange where CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue,dtinfo) + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
        string roomno = Convert.ToString(cmd.ExecuteScalar());
        if(roomno!="")
        {
            for (int j = 1; j <= Convert.ToInt32(lblcolumn.Text); j++)   //Add Column
            {
                dt.Columns.Add(j.ToString());
            }
            for (int j = 1; j <= Convert.ToInt32(lblrow.Text); j++)  //Add Row
            {
                dt.Rows.Add();
            }


            cmd = new SqlCommand("select * from SeatingArrange where CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue, dtinfo) + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                col1 = reader["ColumnNo"].ToString();
                col2 = reader["row"].ToString();
                if (reader["RollNo"].ToString() != "")
                    col3 = reader["RollNo"].ToString() + " / " + reader["SubCode"].ToString();
                else col3 = "";
                dt.Rows[Convert.ToInt32(col2)][Convert.ToInt32(col1)] = col3;

            } reader.Close();
            DataRow dr = dt.NewRow();
        }
        con.Close();

        return dt;
    }
    protected void GridExamSub_OnRowDateBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;
        rw = GridView3.SelectedRow;
        lblRoomNo.Text = rw.Cells[2].Text.ToString();
        lblRoomName.Text = rw.Cells[3].Text.ToString();
        lblRoomCapacity.Text = rw.Cells[4].Text.ToString();
        lblRoomColumn.Text = rw.Cells[5].Text.ToString();
        lblcolumn.Text = lblRoomColumn.Text; lblrow.Text = (Convert.ToInt32(lblRoomCapacity.Text) / Convert.ToInt32(lblcolumn.Text)).ToString();
        GridView1.DataSource = GetDataSource();
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            btnPrint.Visible = true; btndelete.Visible = true;
        }
        else
        {
            btnPrint.Visible = false; btndelete.Visible = false;
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("delete from SeatingArrange where CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue, dtinfo) + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery(); GridView1.DataSource = GetDataSource();
            GridView1.DataBind();
            lblExceptionCode.Text = "Successfully Deleted.";
        }
        catch (SqlException ex)
        {
            lblExceptionCode.Text = "Error " + ex.ToString();
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
}