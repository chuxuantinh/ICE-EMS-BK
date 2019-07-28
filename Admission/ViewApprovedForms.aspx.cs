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
using System.Xml;

public partial class Admission_ViewApprovedForms : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
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
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
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
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException Exception)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmissionDefault.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        ddlViewBy.Focus();
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
        string qry = "";
        if (ddlViewBy.SelectedValue.ToString() == "Approved")
        {
            qry = "select IMID,Enrolment as Membership,Name,FName,Course,Part,DOB,DNo as DiaryNo,FormType,UnderAge,SubDate from AppRecord where Status!='NotApproved' and Status!='Hold' and FormType like '%NewAdmission%' and Enrolment not in(select sid from Student where Session='" + lblSeasonHidden.Text.ToString() + "') and Session='" + lblSeasonHidden.Text.ToString() + "' order by Enrolment";
        }
        else
        {
            qry = "select IMID,Enrolment as Membership,Name,FName,Course,Part,DOB,DNo as DiaryNo,FormType,UnderAge,SubDate from AppRecord where (Status='NotApproved' or Status='Hold') and FormType like '%NewAdmission%' and Session='" + lblSeasonHidden.Text.ToString() + "' order by Enrolment";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        GridApprove.DataSource = dt;
        GridApprove.DataBind();
    }
    protected void GridApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");
            }
        }
        catch (FormatException ex)
        {
        }
    }
}