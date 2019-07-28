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

public partial class FO_ViewDiaryEntry : System.Web.UI.Page
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
                    maikal dev = new maikal();
                    maikal mk = new maikal();
                    int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    txtDiaryNo.Visible = false; ddlExamSeason.Visible = false; txtYearSeason.Visible = false;
                    lblGridTitle.Text = "This Week Diary";
                    SqlDataAdapter ad = new SqlDataAdapter("select  DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where Date BETWEEN '" + DateTime.Now.AddDays(-7) + "' and '" + DateTime.Now + "'", con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    ddlType.Focus();
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
        txtYearSeason.Focus();
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
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
                lblGridTitle.Text = "List of Diaries, Date between " + dropp.Text.ToString() + " and " + drop2.Text.ToString();
                if(ddlTypes.SelectedValue=="All")
                    qry = "select  DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where Convert(DateTime,Date,103) BETWEEN Convert(DateTime,'" + dropp.Text + "',103) and Convert(DateTime,'" + drop2.Text + "',103)+1 ";
                else 
                    qry = "select  DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where membertype='"+ddlTypes.SelectedValue.ToString()+"' and  Convert(DateTime,Date,103) BETWEEN Convert(DateTime,'" + dropp.Text + "',103) and Convert(DateTime,'" + drop2.Text + "',103)+1 ";
            }
            else if (ddlType.SelectedValue.ToString() == "DiaryNo")
            {
                lblGridTitle.Text = "Diary No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "'";
            }
            else if (ddlType.SelectedValue.ToString() == "Reference")
            {
                lblGridTitle.Text = "Reference No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select ExamSession,DiaryNo,DiaryType,Date,Remark,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where  CourierNo='" + txtDiaryNo.Text.ToString() + "'";
            }
            else if (ddlType.SelectedValue.ToString() == "IM")
            {
                lblGridTitle.Text = "IM Membership No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where ExamSession='" + lblHiddenSeason.Text.ToString() + "' and IMID='" + txtDiaryNo.Text.ToString() + "'";
            }
            else if (ddlType.SelectedValue.ToString() == "Student")
            {
                lblGridTitle.Text = "Student Membership No: " + txtDiaryNo.Text.ToString() + " and Session: " + lblHiddenSeason.Text.ToString();
                qry = "select DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where ExamSession='" + lblHiddenSeason.Text.ToString() + "' and IMID='" + txtDiaryNo.Text.ToString() + "' and MemberType='Student'";
            }
            else if (ddlType.SelectedValue.ToString() == "Consignmentno")
            {
                lblGridTitle.Text = "Consignment No." + txtDiaryNo.Text.ToString();
                qry = "select DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where ConsignmentNo='" + txtDiaryNo.Text.ToString() + "'";
            }
        }
        catch (SqlException ex)
        {
            lblGridTitle.Text = "Previous 30 Days Diaries.";
            qry = "select  DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where Date BETWEEN '" + DateTime.Now.AddDays(-30) + "' and '" + DateTime.Now + "'";
        }
        catch (FormatException ex)
        {
            lblGridTitle.Text = "Previous 30 Days Diaries.";
            qry = "select  DiaryNo,DiaryType,Remark,Date,MembershipNo,CourierService,CourierNo,Status,ConsignmentNo,SubmittedTo from DiaryEntry where Date BETWEEN '" + DateTime.Now.AddDays(-30) + "' and '" + DateTime.Now + "'";
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
        btnShow.Focus();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue.ToString() == "Student")
        {
            lblName.Text = "Membership No.";
            dates.Visible = false; txtDiaryNo.Visible = true; txtYearSeason.Visible = true; ddlExamSeason.Visible = true;
        }
        else if (ddlType.SelectedValue.ToString() == "Date")
        {
            lblName.Text = "";
            dates.Visible = true; txtDiaryNo.Visible = false; txtYearSeason.Visible = false; ddlExamSeason.Visible = false;
        }
        else if (ddlType.SelectedValue.ToString() == "DiaryNo")
        {
            dates.Visible = false; txtDiaryNo.Visible = true; txtYearSeason.Visible = false; ddlExamSeason.Visible = false;
            lblName.Text = "Diary No.";
        }
        else if (ddlType.SelectedValue.ToString() == "Reference")
        {
            lblName.Text = "Reference No.";
            dates.Visible = false; txtDiaryNo.Visible = true; txtYearSeason.Visible = false; ddlExamSeason.Visible = false;
        }
        else if (ddlType.SelectedValue.ToString() == "IM")
        {
            lblName.Text = "IMID";
            dates.Visible = false; txtDiaryNo.Visible = true; txtYearSeason.Visible = true; ddlExamSeason.Visible = true;
        }
        else if (ddlType.SelectedValue == "Consignmentno")
        {
            dates.Visible = false; txtDiaryNo.Visible = true; txtYearSeason.Visible = false; ddlExamSeason.Visible = false;
            lblName.Text = "Consignment No.";
        }
    }
}
