using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

public partial class Exam_MarksStatement : System.Web.UI.Page
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
            else
            {
                if (!IsPostBack)
                {
                    ddlExamSeason.Focus();
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    maikal mk = new maikal();
                    int sn = mk.chksession();
                    if (sn == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
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
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtRollNo.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void btnOK_OnClcick(object sender, EventArgs e)
    {
        fillGrid();       
    }
    private void fillGrid()
    {
        string qry = "";
        if (rbtnRollNo.Checked == true)
        {
            qry = "select RollNo,Course,Part,TotalMaxMarks as MaximumMarks,TotalObMarks as ObtainedMarks,Aggregate,Result,Session from Marksheet where RollNo='" + txtRollNo.Text + "' and Session='" + lblHiddenSeason.Text + "'";
        }
        else if (rbtnSID.Checked == true)
        {
            qry = "select RollNo,Course,Part,TotalMaxMarks as MaximumMarks,TotalObMarks as ObtainedMarks,Aggregate,Result,Session from Marksheet where SID='" + txtRollNo.Text + "'";
        }
        else if (rbtnIMID.Checked == true)
        {
            qry = "select Marksheet.RollNo,Marksheet.Course,Marksheet.Part,Marksheet.TotalMaxMarks as MaximumMarks,Marksheet.TotalObMarks as ObtainedMarks,Marksheet.Aggregate,Marksheet.Result,Marksheet.Session  from Marksheet, ExamCurrent where Marksheet.SID=ExamCurrent.SID and ExamCurrent.IMID='" + txtRollNo.Text.ToString() + "' and Marksheet.Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        gridMarks.DataSource = dt;
        gridMarks.DataBind();
        gridMarks.Focus();
    }
    protected void gridMarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row;
        row = gridMarks.SelectedRow;
        Label1.Text = row.Cells[1].Text;
        SqlDataAdapter ad = new SqlDataAdapter("select SubID,SubName,GetMarks as ObtainedMarks,Center as ExamCenter from SExamMarks where RollNo='" + row.Cells[1].Text + "' and Course='" + row.Cells[2].Text + "' and Part='" + row.Cells[3].Text + "' and ExamSeason='" +row.Cells[8].Text+ "'", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridMarksheet.DataSource = dt;
        GridMarksheet.DataBind();
        GridMarksheet.Focus();
    }
protected void  gridMarks_PageIndexChanging(object sender, GridViewPageEventArgs e)
{
    gridMarks.PageIndex = e.NewPageIndex;
    fillGrid();
    gridMarks.DataBind();
}
}