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
using Microsoft.VisualBasic;


public partial class Exam_ExamFormDeletion : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    string strstatus;
    ClsECenterCity ecity = new ClsECenterCity();
    ClsExamForm clExamForm = new ClsExamForm();
    SessionDuration fm = new SessionDuration();
    SqlCommand cmd;
    string[] subjects = new string[2];
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
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    panelInVisible.Visible = true;
                    txticesn.Focus();
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
        Response.Redirect("AdmissionDefault.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        txticesn.Text = ""; lblEnrolment.Text = "";
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txticesn.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txticesn.Text = "";
        lblEnrolment.Text = "";
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtSerialNo_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select Enrolment,AppNo, Course ,Part,Session,IMID,Status,Name,FName,FormType,Amount,LateFee,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,Exempted,DupForm from AppRecord where Enrolment='" + txticesn.Text + "' and Session='" + lblExamSeasonHidden.Text + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        grdForms.DataSource = dt;
        grdForms.DataBind();
    }
    protected void grdForms_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        if (grdForms.Rows.Count != 0)
        {
            if (grdForms.Rows[0].Cells[10].Text.Contains("Admission"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Delete Admission Form first')", true);
            }
            else {
                if (grdForms.Rows[0].Cells[7].Text.ToString() == "NotApproved" && grdForms.Rows[0].Cells[8].Text.ToString() == "Hold")
                {
                    cmd = new SqlCommand("Update ExamCurrent set ExamStatus='NotSubmitted' where SID='" + grdForms.Rows[0].Cells[1].Text + "' and session='" + lblExamSeasonHidden.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Delete from AppRecord where Enrolment='" + grdForms.Rows[0].Cells[1].Text + "' and session='" + lblExamSeasonHidden.Text + "' and FormType like'%Exam%'", con);
                    cmd.ExecuteNonQuery();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Re-Submit EXamForm first')", true);
                RecoverAppRecord();
            }

        }
        con.Close();
        con.Dispose();
    }
    protected void RecoverAppRecord()
    {
        string str = grdForms.Rows[0].Cells[7].Text;
        if (str != "NotApproved" && str != "Hold")
        {
            cmd = new SqlCommand("Insert into RecoverApp(FormType,APPNo,Amount,Type,Enrolment,IMID,SerialNo,Session,Course,Part,Status,Remark,Date) values(@FormType,@APPNo,@Amount,@Type,@Enrolment,@IMID,@SerialNo,@Session,@Course,@Part,@Status,@Remark,@Date)", con);
            cmd.Parameters.AddWithValue("@FormType", "Exam");
            cmd.Parameters.AddWithValue("@AppNo", grdForms.Rows[0].Cells[1].Text);
            cmd.Parameters.AddWithValue("@Amount", lblAmount.Text);
            cmd.Parameters.AddWithValue("@Type", "Credit");
            cmd.Parameters.AddWithValue("@Enrolment", grdForms.Rows[0].Cells[1].Text);
            cmd.Parameters.AddWithValue("@IMID", grdForms.Rows[0].Cells[6].Text);
            cmd.Parameters.AddWithValue("@SerialNo", grdForms.Rows[0].Cells[1].Text);
            cmd.Parameters.AddWithValue("@Session", grdForms.Rows[0].Cells[5].Text);
            cmd.Parameters.AddWithValue("@Course", grdForms.Rows[0].Cells[3].Text);
            cmd.Parameters.AddWithValue("@Part", grdForms.Rows[0].Cells[3].Text);
            cmd.Parameters.AddWithValue("@Status", "NotApproved");
            cmd.Parameters.AddWithValue("@Remark", "Delete");
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
    }
    int Amount = 0;
    protected void grdForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount = Amount + Convert.ToInt32(e.Row.Cells[11].Text.TrimEnd('0').TrimEnd('.')) + Convert.ToInt32(e.Row.Cells[12].Text.TrimEnd('0').TrimEnd('.'));
            e.Row.Cells[11].Text = e.Row.Cells[11].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[12].Text = e.Row.Cells[12].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[13].Text = e.Row.Cells[13].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[14].Text = e.Row.Cells[14].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[15].Text = e.Row.Cells[15].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[16].Text = e.Row.Cells[16].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[17].Text = e.Row.Cells[17].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[18].Text = e.Row.Cells[18].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[19].Text = e.Row.Cells[19].Text.TrimEnd('0').TrimEnd('.');
        }
        lblAmount.Text = Amount.ToString();
    }
}