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
using System.Globalization;
using System.Data.SqlClient;

public partial class FO_ViewCourierDetail : System.Web.UI.Page
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
                    con.Open();
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    txtDiaryNo.Visible = false; ddlExamSeason.Visible = false; txtYearSeason.Visible = false;
                    lblGridTitle.Text = "Courier Details";
                    SqlDataAdapter ad = new SqlDataAdapter("select  DairyNo,Session,Department,DairyType,IMID,CurrentDate,Status from DairyCount where CurrentDate BETWEEN '" + DateTime.Now.AddDays(-7) + "' and '" + DateTime.Now + "'", con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    ddlType.Focus();
                    con.Close();
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtDiaryNo.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if(e.Row.Cells[6].Text!="&nbsp;")
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToString("dd/MM/yyyy");
        }
    }
    public void grid()
    {
        string qry = "";
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            if (ddlType.SelectedValue.ToString() == "Date")
            {
                lblGridTitle.Text = "List of Diaries, CurrentDate between " + dropp.Text.ToString() + " and " + drop2.Text.ToString();
                qry = "select DairyNo,Department,DairyType,IMID,Status,CurrentDate from DairyCount where CurrentDate BETWEEN '" + Convert.ToDateTime(dropp.Text, dtinfo) + "' and '" + Convert.ToDateTime(drop2.Text, dtinfo) + "' ";
            }
            else if (ddlType.SelectedValue.ToString() == "DiaryNo")
            {
                lblGridTitle.Text = "Diary No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select DairyNo,Department,DairyType,IMID,Status,CurrentDate from DairyCount where Session='" + lblHiddenSeason.Text.ToString() + "' and DairyNo='" + txtDiaryNo.Text.ToString() + "'";
            }
            else if (ddlType.SelectedValue.ToString() == "Reference")
            {
                lblGridTitle.Text = "Reference No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select DairyNo,Session,Department,DairyType,IMID,CurrentDate,Status from DairyCount where  Session='" + lblHiddenSeason.Text.ToString() + "' and CourierNo='" + txtDiaryNo.Text.ToString() + "'";
            }
            else if (ddlType.SelectedValue.ToString() == "IM")
            {
                lblGridTitle.Text = "IM Membership No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select DairyNo,Department,DairyType,IMID,Status,CurrentDate from DairyCount where Session='" + lblHiddenSeason.Text.ToString() + "' and IMID='" + txtDiaryNo.Text.ToString() + "'";
            }
            else if (ddlType.SelectedValue.ToString() == "Student")
            {
                lblGridTitle.Text = "Student Membership No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select DairyNo,Session,Department,DairyType,IMID,CurrentDate,Status from DairyCount where Session='" + lblHiddenSeason.Text.ToString() + "' and IMID='" + txtDiaryNo.Text.ToString() + "'";
            }
        }
        catch (SqlException ex)
        {
            lblGridTitle.Text = "Previous 30 Days Diaries.";
            qry = "select DairyNo,Session,Department,DairyType,IMID,CurrentDate,Status from DairyCount where CurrentDate BETWEEN '" + DateTime.Now.AddDays(-7) + "' and '" + DateTime.Now + "'";
        }
        catch (FormatException ex)
        {
            lblGridTitle.Text = "Previous 30 Days Diaries.";
            qry = "select DairyNo,Session,Department,DairyType,IMID,CurrentDate,Status from DairyCount where CurrentDate BETWEEN '" + DateTime.Now.AddDays(-7) + "' and '" + DateTime.Now + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
  
    protected void btnShow_Click(object sender, EventArgs e)
    {
        grid();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue.ToString() == "Student")
        {
            lblName.Text = "Membership No.";
            dates.Visible = false;
            txtDiaryNo.Visible = true;
            txtYearSeason.Visible = true;
            ddlExamSeason.Visible = true;
            txtDiaryNo.Text = "";
            ddlExamSeason.Focus();
        }
        else if (ddlType.SelectedValue.ToString() == "Date")
        {
            lblName.Text = "";
            dates.Visible = true;
            txtDiaryNo.Visible = false;
            txtYearSeason.Visible = false;
            ddlExamSeason.Visible = false;
            dropp.Focus();
        }
        else if (ddlType.SelectedValue.ToString() == "DiaryNo")
        {
            txtDiaryNo.Text = "";
            dates.Visible = false;
            txtDiaryNo.Visible = true;
            txtYearSeason.Visible = false;
            ddlExamSeason.Visible = false;
            lblName.Text = "Diary No.";
            txtDiaryNo.Focus();
        }
        else if (ddlType.SelectedValue.ToString() == "Reference")
        {
            txtDiaryNo.Text = "";
            lblName.Text = "Reference No.";
            dates.Visible = false;
            txtDiaryNo.Visible = true;
            txtYearSeason.Visible = false;
            ddlExamSeason.Visible = false;
            txtDiaryNo.Focus();
        }
        else if (ddlType.SelectedValue.ToString() == "IM")
        {
            txtDiaryNo.Text = "";
            lblName.Text = "IMID";
            dates.Visible = false;
            txtDiaryNo.Visible = true;
            txtYearSeason.Visible = true;
            ddlExamSeason.Visible = true;
            txtDiaryNo.Text = "";
            ddlExamSeason.Focus();
        }
    }
    private void displayTime()
    {
        lblDateHeader.Text=DateTime.Today.ToString("dd/MM/yyyy");
    }
    private void updateCount(string dairy)
    {
        SqlCommand cmd = new SqlCommand("select * from DairyCount where DairyNo='"+dairy.ToString()+"'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            displayTime();
            lblDiaryNo.Text = dr["DairyNo"].ToString();
            lblADDSub.Text = dr["ADDSub"].ToString();
            lblADDRcv.Text = dr["ADDRcv"].ToString();
            lblEnrollFormSub.Text = dr["EnrollFormSub"].ToString();
            lblEnrollFormRcv.Text = dr["EnrollFormRcv"].ToString();
            lblExamFormSub.Text = dr["ExamFormSub"].ToString();
            lblExamFormRcv.Text = dr["ExamFormRcv"].ToString();
            lblITISub.Text = dr["ITISub"].ToString();
            lblITIRcv.Text = dr["ITIRcv"].ToString();
            lblCADSub.Text = dr["CADSub"].ToString();
            lblCADRcv.Text = dr["CADRcv"].ToString();
            lblOtherFormSub.Text = dr["OtherFormSub"].ToString();
            lblOtherFormRcv.Text = dr["OtherFormRcv"].ToString();
            lblODDSub.Text = dr["ODDSub"].ToString();
            lblODDRcv.Text = dr["ODDrcv"].ToString();
            lblProvisionalSub.Text = dr["ProvisionalSub"].ToString();
            lblProvisionalRcv.Text = dr["ProvisionalRcv"].ToString();
            lblFinalPassSub.Text = dr["FinalPassSub"].ToString();
            lblFinalPassRcv.Text = dr["FinalPassRcv"].ToString();
            lblReCheckingSub.Text = dr["ReCheckingSub"].ToString();
            lblReCheckingRcv.Text = dr["ReCheckingRcv"].ToString();
            lblDuplicateDocsSub.Text = dr["DuplicateDocsSub"].ToString();
            lblDuplicateDocsRcv.Text = dr["DuplicateDocsRcv"].ToString();
            lblMemberSub.Text = dr["MemberSub"].ToString();
            lblMemberRcv.Text = dr["MemberRcv"].ToString();
            lblBooksSub.Text = dr["BooksSub"].ToString();
            lblBooksRcv.Text = dr["BooksRcv"].ToString();
            lblProspectusSub.Text = dr["ProspectusSub"].ToString();
            lblProspectusRcv.Text = dr["ProspectusRcv"].ToString();
        }
        dr.Close();
    }
    private void updateProjectCount(string diary)
    {
        SqlCommand cmd = new SqlCommand("select * from ProjectCount where DairyNo='" + diary + "'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lblDiaryNo.Text = dr["DairyNo"].ToString();
            lblDDSub.Text = dr["DDSub"].ToString();
            lblDDRcv.Text = dr["DDRcv"].ToString();
            lblProformaASub.Text = dr["ProformaASub"].ToString();
            lblProformaARcv.Text = dr["ProformaARcv"].ToString();
            lblProformaBSub.Text = dr["ProformaBSub"].ToString();
            lblProformaBRcv.Text = dr["ProformaBRcv"].ToString();
        }
        dr.Close();
        if (lblDDRcv.Text == "") lblDDRcv.Text = "0"; if (lblDDSub.Text == "") lblDDSub.Text = "0";
        if (lblProformaARcv.Text == "") lblProformaARcv.Text = "0"; if (lblProformaASub.Text == "") lblProformaASub.Text = "0";
        if (lblProformaBRcv.Text == "") lblProformaBRcv.Text = "0"; if (lblProformaBSub.Text == "") lblProformaBSub.Text = "0";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        GridViewRow row;
        row = GridView1.SelectedRow;
        updateCount(GridView1.SelectedRow.Cells[1].Text.ToString());
        updateProjectCount(GridView1.SelectedRow.Cells[1].Text.ToString());
        pnlAcademic.Visible = true;
        pnlOther.Visible = true;
        pnlProject.Visible = true;
        pnlBooks.Visible = true;
    }
    protected void lbtnViewDiaryDetails_Click1(object sender, EventArgs e)
    {
        Response.Redirect("ViewDiaryEntry.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=FO");
    }
}
