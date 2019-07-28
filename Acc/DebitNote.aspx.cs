using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Acc_DebitNote : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0) ddlsession.SelectedValue = "Sum";
            else ddlsession.SelectedValue = "Win";
            txtSession.Text = DateTime.Now.Year.ToString();
            lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
            bindGrid();
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void gridDebitNote_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "&nbsp;" | e.Row.Cells[0].Text == "")
            {
                e.Row.Cells[0].Text = "";
            }
            else e.Row.Cells[0].Text = Convert.ToDateTime(e.Row.Cells[0].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[3].Text == "Requested")
            {
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            if (e.Row.Cells[3].Text == "Hold")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            if (e.Row.Cells[3].Text == "Processed")
            {
                e.Row.ForeColor = System.Drawing.Color.Green;
            }
            if (e.Row.Cells[3].Text == "Approved")
            {
                e.Row.ForeColor = System.Drawing.Color.Maroon;
            }
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.ToString().TrimEnd('0').TrimEnd('.');
            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[9].Text = e.Row.Cells[9].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    private void bindGrid()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where Status='Approved' and DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "') order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        bindGrid();
        txtSession.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        bindGrid();
    }
    protected void lnlRequest_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "') and Status='Requested' order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();
    }
    protected void lnlApproved_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "')  and Status='Approved'  order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();
    }
    protected void lnkProcess_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "')  and Status='Processed'  order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();

    }
    protected void imgHold_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "')  and Status='Hold'  order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();

    }
}