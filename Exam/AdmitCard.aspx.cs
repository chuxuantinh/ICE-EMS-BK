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

public partial class Exam_AdmitCard : System.Web.UI.Page
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                    ddlExamSeason.SelectedValue = "Sum";
                else { ddlExamSeason.SelectedValue = "Win"; }
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ddlExamSeason.Focus();
                DataShow.Visible = false;
                TxtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
               // TxtDate.Text = Convert.ToDateTime(DateTime.Now, CultureInfo.GetCultureInfo("en-US")).ToString("dd/MM/yyyy HH/MM/SS");
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
    protected void Glot_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select  max(RSN) from examforms where ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'  and RSN!=0", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        int a = 1;
        if (sum.Length > 0 && sum != null)
        {
            LotNo.Text = sum.ToString();
            LotNo.Text = (Convert.ToInt32(LotNo.Text) + a).ToString();
        }
        else
        {
            LotNo.Text  = "1";
        }
        con.Close(); con.Open();
    }
    protected void Showdata_Click(object sender, EventArgs e)
    {
        DataShow.Visible = true;
        DataShow.DataBind();
        TotalAdmitCard.Text = DataShow.Rows.Count.ToString();
    }
    protected void UpdateList_Click(object sender, EventArgs e)
    {
        if (LotNo.Text.Length == 0)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Generate Lot No At First')", true);
        }
        else
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update ExamForms set RSN=@RSN,PrintDate=@Date,Status=@Status where ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "' and Status='RollNoGenerated'", con);
            cmd.Parameters.AddWithValue("@RSN", LotNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", "AdmitCardPrinted");
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update Sucees Ready To AdmitCard Print')", true);
        }
    }
    protected void Padmit_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        DataTable dt = new DataTable();
        dt = null;
        SqlCommand cmd = new SqlCommand("spAdmitCard",con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp = cmd.Parameters.AddWithValue("@sid",dt);
        cmd.Parameters.AddWithValue("@RSN", ddlRSN.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Session", lblExamSeasonHidden.Text);
        cmd.Parameters.AddWithValue("@Type", "Original"); // used for Admit Card Type Original/Duplicate
        //SqlCommand cmd = new SqlCommand("insert into AdmitCard(sid,rollNo,Name,Father,IMNo,SubId,Date,Session,Subcode,ExamCenter,Lotno,ImageName,address1,address2,address3,Course,part)select examforms.SID,examforms.RollNo,Student.Name,student.FName,examforms.IMID,ExamForm.SubID,examform.Date,examform.Shift,examform.SubName,examforms.City,examforms.RSN,docs.Image,ExamCenter.Address,ExamCenter.Address2,ExamCenter.City,examforms.Course,examforms.part from ExamForm,ExamForms,Docs,Student,ExamCenter where examform.SN=examforms.SN and ExamForm.SID=docs.SID and examform.SID=student.sid  and examforms.City=ExamCenter.City and examforms.RSN='"+ddlRSN.SelectedValue.ToString()+"' and examforms.Status='AdmitCardPrinted'  and examforms.ExamSeason='" + lblExamSeasonHidden.Text.ToString() + "'", con);
       cmd.CommandTimeout = 60*60;
        cmd.ExecuteNonQuery();
        con.Close(); con.Dispose();
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update Sucees Ready To AdmitCard Print')", true);
    }
}