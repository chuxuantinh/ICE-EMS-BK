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

public partial class Exam_ApproveMarks : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
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
            if (!IsPostBack)
            {
                ClsEdit cledit = new ClsEdit();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                lblToExamForm.Text = cledit.ExamFormFilled(lblExamSeasonHidden.Text.ToString());
                lblMarkNotSubmitted.Text = cledit.ExamMarksNotSubmitted(lblExamSeasonHidden.Text.ToString());
                lblMarksSubmitted.Text = cledit.ExamMarksSubmitted(lblExamSeasonHidden.Text.ToString());
                ddlExamSeason.Focus();
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    //protected void btnView_Onclick(object sender, EventArgs e)
    //{
    //    string query = "";
    //    if (ddlViewType.SelectedValue.ToString() == "NotSubmitted")
    //    {
    //        query = "select * from ExamForms efs where efs.SID in(select ef.SID from ExamForm ef where ef.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and ef.MarkStatus='NotSubmitted' and  ef.SID not in (select sm.SID from SExamMarks sm where sm.SubID=ef.SubID  and sm.SID=ef.SID and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "')) and efs.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'";
    //        btnApprove.Visible = false;
    //    }
    //    else if (ddlViewType.SelectedValue.ToString() == "Submitted")
    //    {
    //        query="select * from ExamForms efs where efs.SID  in(select ef.SID from ExamForm ef where ef.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and ef.MarkStatus='Submitted' ) and efs.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'";
    //        btnApprove.Visible = true;
    //    }
    //    else if (ddlViewType.SelectedValue.ToString() == "Approved")
    //    {
    //        query = "select * from ExamForms efs where efs.SID in(select ef.SID from ExamForm ef where ef.ExamSession='" + lblExamSeasonHidden.Text.ToString() + "' and ef.MarkStatus='Approved' and  ef.SID  in (select sm.SID from SExamMarks sm where sm.SubID=ef.SubID and sm.SID=ef.SID and ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "')) and efs.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'";
    //        btnApprove.Visible = false;
    //    }
    //    SqlDataAdapter ad=new SqlDataAdapter(query,con);
    //    DataSet ds=new DataSet();
    //    ad.Fill(ds);
    //    GridExamForms.DataSource=ds;
    //    GridExamForms.DataBind();
    //}
    protected void btnApprove_Onclick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update ExamForms set Status='MarkSubmitted' where ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Approved')", true);
        con.Close();
        con.Dispose();
    }
    protected void btnPro_Click(object sender, EventArgs e)
    {
        ClsExamForm clexam = new ClsExamForm();
        DataTable dt = new DataTable();
        dt=clexam.ResultPro(lblExamSeasonHidden.Text.ToString(), ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString());
        lblToStudent.Text = dt.Rows.Count.ToString();
    }
}