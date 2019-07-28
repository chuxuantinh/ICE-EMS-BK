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

public partial class Acc_UpdateLateFees : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlSession.SelectedValue = "Sum";
                else ddlSession.SelectedValue = "Win";
                 txtYear.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
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
    protected void btnok_Click(object sender, EventArgs e)
    {
        lblExceptionOK.Text = "";
        if (txtDiary.Text != "")
            grd();
        else lblExceptionOK.Text = "Please Enter Diary No.";

    }
    private void grd()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select Enrolment as Mem_No,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted as ExmpFee,AdmissionFees,CompositeFees,AnnualSubFees as ASF,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where FormType like '%Exam%' and DNo='" + txtDiary.Text + "' and Status='" + ddlStatus.SelectedValue + "' and Session='" + lblSessionHiddend.Text + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        grdRecord.DataSource = dt;
        grdRecord.DataBind();
    }
    protected void grdRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        lblTotal.Text = grdRecord.Rows.Count.ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[16].Text = e.Row.Cells[16].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[17].Text = e.Row.Cells[17].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[18].Text = e.Row.Cells[18].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[19].Text = e.Row.Cells[19].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[20].Text = e.Row.Cells[20].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[21].Text = e.Row.Cells[21].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[22].Text = e.Row.Cells[22].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[23].Text = e.Row.Cells[23].Text.ToString().TrimEnd('0').TrimEnd('.');
            //e.Row.Cells[24].Text = e.Row.Cells[24].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[25].Text = e.Row.Cells[25].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[26].Text = e.Row.Cells[26].Text.ToString().TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[9].Text != null & e.Row.Cells[9].Text != "" & e.Row.Cells[9].Text != "&nbsp;")
                e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[12].Text != null & e.Row.Cells[12].Text != "" & e.Row.Cells[12].Text != "&nbsp;")
                e.Row.Cells[12].Text = Convert.ToDateTime(e.Row.Cells[12].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            if (txtLate.Text == "") txtLate.Text = "0";
            int i = 0;
            while (i < grdRecord.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)grdRecord.Rows[i].FindControl("chkapp");
                if (rbApp.Checked)
                {
                    cmd = new SqlCommand("update AppRecord set LateFee='" + Convert.ToDecimal(txtLate.Text) + "' where DNo='" + txtDiary.Text + "' and AppNo='" + grdRecord.Rows[i].Cells[3].Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                }
                i++;
            }
            Log.WriteLog(Request.QueryString["maikal"], "B007", txtDiary.Text.ToString(), "", "Late Fees Updated");
            Log.WriteLog("B007", Request.QueryString["maikal"], txtDiary.Text.ToString(),"", "Late Fees Updated");
            grd(); txtLate.Text = "";
            con.Close(); con.Dispose();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Late Fees Updated')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Invalid Format')", true);
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtDiary.Text != "") grd();
    }
}