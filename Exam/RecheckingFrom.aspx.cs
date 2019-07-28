using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Exam_RecheckingFrom : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            maikal mk = new maikal();
            int sn = mk.chksession();
            if (sn == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
            lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            bindPaperCode();
            ddlExamSeason.Focus();
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        bindPaperCode();
        ddlpaperCode.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void btnOK_OnClcick(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = fillgrid();
            GridView1.DataBind();
        }
        catch (SqlException ex)
        {
            lblExceptionOK.Text = ex.ToString();
        }
        finally
        {
            GridView1.Focus();
        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = fillgrid();
        GridView1.DataBind();

    }
    protected void btnSave_click(object seder, EventArgs e)
    {
        con.Close(); con.Open();
        DataTable dt = new DataTable();
        dt.Columns.Add("SubID",typeof(string));
        dt.Columns.Add("SID",typeof(string));
        dt.Columns.Add("Status",typeof(string));
        dt.Columns.Add("OldMarks",typeof(int));
        dt.Columns.Add("NewMarks",typeof(int));
        dt.Columns.Add("Result", typeof(string));
        foreach (GridViewRow rw in GridView1.Rows)
        {
            Label lb=(Label)rw.FindControl("lblSID");
            string sid=lb.Text;
            lb=(Label)rw.FindControl("lblOldMarks");
            string oldmarks=lb.Text;
            TextBox tx=(TextBox)rw.FindControl("txtNewmarks");
            string Newmarks=tx.Text;
            string status="";
            if(Convert.ToInt32(oldmarks)>=Convert.ToInt32(Newmarks))
                status="NoChange";
            else status="Change";
            string result;
            if ((ddlpaperCode.SelectedValue.ToString() == "TC 2.10" || ddlpaperCode.SelectedValue.ToString() == "TC 2.11" || ddlpaperCode.SelectedValue.ToString() == "TA 2.11" || ddlpaperCode.SelectedValue.ToString() == "TA 2.12") && (Convert.ToInt32(Newmarks) >= 40))
                result = "Pass";
            else if (Convert.ToInt32(Newmarks) >= 50)
                result = "Pass";
            else result = "Fail";
            dt.Rows.Add(ddlpaperCode.SelectedValue.ToString(), sid, status, oldmarks, Newmarks,result);
        }
        cmd = new SqlCommand("spReChecking",con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp = cmd.Parameters.AddWithValue("@ReCheckingtbl", dt);
        sp.SqlDbType = SqlDbType.Structured;
        cmd.Parameters.AddWithValue("@Session", ddlExamSeason.SelectedValue.ToString() + txtYearSeason.Text.ToString());
        SqlParameter sp2 = new SqlParameter("@Error", SqlDbType.VarChar,1000);
        sp2.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(sp2);
        cmd.ExecuteNonQuery();
        lblExceptionOK.Text = cmd.Parameters["@Error"].Value.ToString();
    }
    protected void btnUpdate_OldMarksClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("update Rechecking set Rechecking.OldMarks=SExamMarks.GetMarks from SExamMarks inner join Rechecking on SExamMarks.SID=Rechecking.SID and SExamMarks.ExamSeason='" + lblHiddenSeason.Text.ToString() + "' and Rechecking.Session='" + lblHiddenSeason.Text.ToString() + "' and Rechecking.SubID=SExamMarks.SubID", con);
        cmd.ExecuteNonQuery();
        con.Close(); con.Dispose();
    }
    private DataTable fillgrid()
    {
        cmd = new SqlCommand("select SExamMarks.RollNo,SExamMarks.SID,Rechecking.OldMarks,Rechecking.NewMarks from SExamMarks inner join Rechecking on SExamMarks.SID=Rechecking.SID and SExamMarks.ExamSeason='" + lblHiddenSeason.Text.ToString() + "' and Rechecking.Session='" + lblHiddenSeason.Text.ToString() + "' and ReChecking.SubID=SExamMarks.SubID and  Rechecking.SubID='" + ddlpaperCode.SelectedValue.ToString() + "' order by SExamMarks.RollNo", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
    }
    private void bindPaperCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select distinct(SubID) from ReChecking where Session='" + lblHiddenSeason.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        ddlpaperCode.DataSource = dt;
        ddlpaperCode.DataTextField = "SubID";
        ddlpaperCode.DataValueField = "SubID";
        ddlpaperCode.DataBind();
    }
}

//Report Query
//select AppRecord.CAD as SN,SExamMarks.RollNo, Rechecking.SID,AppRecord.Name,AppRecord.Course,AppRecord.Part,AppRecord.IMID,Rechecking.SubID,SExamMarks.GetMarks,SExamMarks.Center from AppRecord inner join Rechecking on AppRecord.AppNo=Rechecking.AppNo inner join SExamMarks on SExamMarks.SID=AppRecord.SID where Rechecking.Session='Win2012' and AppRecord.Session='Sum2013' and Rechecking.SubID=SExamMarks.SubID and SExamMarks.ExamSeason='Win2012' order by Rechecking.sid,CAD