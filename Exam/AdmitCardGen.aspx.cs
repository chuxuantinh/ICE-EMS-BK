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

public partial class Exam_AdmitCardGen : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                {
                    ddlExamSeason.SelectedValue = "Sum";
                }
                else { ddlExamSeason.SelectedValue = "Win"; }
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ClsEdit cledit = new ClsEdit();
                lblExamFormSub.Text=cledit.ExamFormSubmitted(lblExamSeasonHidden.Text.ToString());
                lblExamFormApproved.Text=cledit.ExamFormApproved(lblExamSeasonHidden.Text.ToString());
                lblExamFormFilled.Text = cledit.ExamFormFilled(lblExamSeasonHidden.Text.ToString());
                lblExamFormRollNo.Text = cledit.ExamFormRollNO(lblExamSeasonHidden.Text.ToString());
                lblExamFormAdmitCard.Text = cledit.ExamFormAdmitCard(lblExamSeasonHidden.Text.ToString());
                con.Close(); con.Open();
                cmd=new SqlCommand("SELECT  count(distinct(SID)) FROM [ExamForms] WHERE ([ExamSeason] = '" + lblExamSeasonHidden.Text + "') AND ([Status] = 'RollNoGenerated') ", con);
                string count = Convert.ToString(cmd.ExecuteScalar());
                btnSaveForAdmitCard.Text = "Approve Admit Card: " + count.ToString();
                fillddlSubID();
                con.Close(); con.Dispose();
                ddlExamSeason.Focus();
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
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
     string date, shifit, time, query;
    int j;
    protected void btnSave_Onclick(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            con.Close(); con.Open();
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter ad = new SqlDataAdapter("SELECT ExamForms.SID, ExamForm.SubID, ExamForm.SubName, ExamForm.MarkStatus, ExamForms.RollNo FROM ExamForm, ExamForms where ExamForm.SN=ExamForms.SN and ExamForms.ExamSeason = '" + lblExamSeasonHidden.Text +"' AND ExamForms.Status = 'RollNoGenerated'  ORDER BY ExamForm.SID", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            DataRow rw;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                rw = dt.Rows[i];
                cmd = new SqlCommand("select Date,Time,Shift from ExamDate where SCode='" + rw[1].ToString() + "' and Season='" + lblExamSeasonHidden.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    date = Convert.ToDateTime(reader[0]).ToShortDateString();
                    time = reader[1].ToString();
                    shifit = reader[2].ToString();
                } reader.Close();
                SqlCommand cmd2 = new SqlCommand("update ExamForm set ExamForm.Date=@Date,ExamForm.Time=@Time,ExamForm.Shift=@Shift From ExamForm, ExamForms where ExamForm.SN=ExamForms.SN and ExamForms.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ExamForms.SID='" + rw[0].ToString() + "' and ExamForm.SubID='" + rw[1].ToString() + "'  and ExamForms.RollNo='" + rw[4].ToString() + "'", con);
                    cmd2.Parameters.AddWithValue("@Date", Convert.ToDateTime(date.ToString()));
                    cmd2.Parameters.AddWithValue("@Time", time.ToString());
                    cmd2.Parameters.AddWithValue("@Shift", shifit.ToString());
                    cmd2.ExecuteNonQuery();
                    cmd2 = new SqlCommand("update ExamForms set ExamForms.Status='AdmitCardGenerated'  where  ExamForms.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and ExamForms.SID='" + rw[0].ToString() + "' and ExamForms.RollNo='" + rw[4].ToString() + "'", con);
                    cmd2.ExecuteNonQuery();
            }
          Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
        }
        catch (Exception ex)
        {
            lblExceptionOK.Text = ex.ToString();
        }
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlSubID();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlSubID();
    }
    protected void ddlSubID_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            qry = "select SubName from CivilSubMaster where SubID='"+ddlSubID.SelectedValue.ToString()+"'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            qry = "select SubName from ArchiSubMaster where SubID='" + ddlSubID.SelectedValue.ToString() + "'";
        }
        con.Close(); con.Open();
        cmd = new SqlCommand(qry, con);
        lblSubName.Text = cmd.ExecuteScalar().ToString();
        con.Close(); con.Dispose();
    }
    private void fillddlSubID()
    {
        string qry = "";
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            qry = "select SubID from CivilSubMaster where Section='"+ddlPart.SelectedValue.ToString()+"'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            qry = "select SubID from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddlSubID.DataSource = ds;
        ddlSubID.DataTextField = "SubID";
        ddlSubID.DataValueField = "SubID";
        ddlSubID.DataBind();
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("update ExamForm set RollStatus='Submitted' where SID in (select SID from ExamForm where SubID='" + ddlSubID.SelectedValue.ToString() + "' and ExamSession='" + lblExamSeasonHidden.Text + "') and ExamSession='" + lblExamSeasonHidden.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Exam Schedule Changed')", true);
        }
        catch (Exception ex)
        {
            lblExceptionChange.Text = "Please select course.";
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }
}