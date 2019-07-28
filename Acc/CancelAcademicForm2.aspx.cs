using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class Acc_CancelAcademicForm2 : System.Web.UI.Page
{
    #region var;
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    #endregion;

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
        finally
        {
        }
    }
    protected void btnView_click(object sender, EventArgs e)
    {
        string qry="";
        if (ddlStatus.SelectedValue.ToString() == "NotApproved" | ddlStatus.SelectedValue.ToString() == "Hold")
        {
            qry = "select Enrolment,Name,FName,DOB,FormType,FeeType,Amount,DNo,SubDate,AppNo,IMID,Course,Part,Session from AppRecord where FormType='" + ddlAppType.SelectedValue.ToString() + "' and Status='" + ddlStatus.SelectedValue.ToString() + "' and DNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else
        {
            qry = "select Enrolment,Name,FName,DOB,FormType,FeeType,Amount,DNo,SubDate,AppNo,IMID,Course,Part,Session from AppRecord where FormType='" + ddlAppType.SelectedValue.ToString() + "' and Status!='NotApproved' and Status!='Hold' and DNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        GridAppTable.DataSource = dt;
        GridAppTable.DataBind();
        btnSubmit.Enabled = true;
    }
    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = "DiaryNo";
            e.Row.Cells[1].Text = "Membership";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.ToString().TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[4].Text != null & e.Row.Cells[4].Text != "" & e.Row.Cells[4].Text != "&nbsp;")
                e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[9].Text != null & e.Row.Cells[9].Text != "" & e.Row.Cells[9].Text != "&nbsp;")
                e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        for (int i = 0; i < GridAppTable.Rows.Count; i++)
        {
            CheckBox rbApp = (CheckBox)GridAppTable.Rows[i].FindControl("chkapp");
            if (rbApp.Checked == true)
            {
                recoverApp(i);
                updateDiaryCount(GridAppTable.Rows[i].Cells[5].Text.ToString());
            }
        }
        con.Close(); con.Dispose();
        btnSubmit.Enabled = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }

    #region
    public void recoverApp(int index)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        cmd = new SqlCommand("delete AppRecord where Appno='" + GridAppTable.Rows[index].Cells[10].Text + "'", con);
        cmd.ExecuteNonQuery();
        if (ddlAppType.SelectedValue == "FinalPass")
        {
            // Update SFinalPass.Certificate=NotIssued
            cmd = new SqlCommand("Update SFinalPass set Certificate='NotIssued' where SID='" + GridAppTable.Rows[index].Cells[1].Text.ToString() + "' and Course='" + GridAppTable.Rows[index].Cells[12].Text.ToString() + "' and Part='" + GridAppTable.Rows[index].Cells[13].Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
        }
        if (ddlAppType.SelectedValue == "Rechecking")
        {
            // Delete ReChecking Record Submitted.
        }
        if (ddlStatus.SelectedValue.ToString() != "NotApproved" && ddlStatus.SelectedValue.ToString() != "Hold")
        {
            cmd = new SqlCommand("Insert into RecoverApp(FormType,APPNo,Amount,Type,Enrolment,IMID,SerialNo,Session,Course,Part,Name,FName,DOB,Status,Remark,Date) values(@FormType,@APPNo,@Amount,@Type,@Enrolment,@IMID,@SerialNo,@Session,@Course,@Part,@Name,@FName,@DOB,@Status,@Remark,@Date)", con);
            cmd.Parameters.AddWithValue("@FormType",ddlAppType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@AppNo",GridAppTable.Rows[index].Cells[10].Text.ToString());
            cmd.Parameters.AddWithValue("@Amount", GridAppTable.Rows[index].Cells[7].Text.ToString());
            cmd.Parameters.AddWithValue("@Type", "Credit");
            cmd.Parameters.AddWithValue("@Enrolment", GridAppTable.Rows[index].Cells[1].Text.ToString());
            cmd.Parameters.AddWithValue("@IMID", GridAppTable.Rows[index].Cells[11].Text.ToString());
            cmd.Parameters.AddWithValue("@SerialNo", GridAppTable.Rows[index].Cells[10].Text.ToString());
            cmd.Parameters.AddWithValue("@Session", GridAppTable.Rows[index].Cells[14].Text.ToString());
            cmd.Parameters.AddWithValue("@Course", GridAppTable.Rows[index].Cells[12].Text.ToString());
            cmd.Parameters.AddWithValue("@Part", GridAppTable.Rows[index].Cells[13].Text.ToString());
            cmd.Parameters.AddWithValue("@Name", GridAppTable.Rows[index].Cells[2].Text.ToString());
            cmd.Parameters.AddWithValue("@FName", GridAppTable.Rows[index].Cells[3].Text.ToString());
            cmd.Parameters.AddWithValue("@DOB",Convert.ToDateTime(GridAppTable.Rows[index].Cells[4].Text.ToString(),dtinfo));
            cmd.Parameters.AddWithValue("@Status", "NotApproved");
            cmd.Parameters.AddWithValue("@Remark",txtRemarks.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();
            lblException.Text="Form's Successfully submitted for Cancelation.";
            lblException.Attributes.Add("class","success");
        }
    }

    // Decrease Diary Count
    private void updateDiaryCount(string apps)
    {
        string qry = "";
        if (ddlAppType.SelectedValue.ToString() == "ExamCenter")  // Other Count
        {
            qry = "update DairyCount set OtherFormSub=OtherFormSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "Rechecking") //RechecckingRcv/Sub
        {
            qry = "update DairyCount set  ReCheckingSub=ReCheckingSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "FinalPass")   //FinalPassRcv/Sub
        {
            qry = "update DairyCount set FinalPassSub=FinalPassSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "ProvisionalCertificate") //ProvisionalRcv/Sub
        {
            qry = "update DairyCount set ProspectusSub=ProspectusSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "IDCard") // DuplicateDocsRcv/Sub
        {
            qry = "update DairyCount set DuplicateDocsSub=DuplicateDocsSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "AdmitCard")  //DuplicateDocsRcv/Sub
        {
            qry = "update DairyCount set DuplicateDocsSub=DuplicateDocsSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "MarksStatement") //DuplicateDocsRcv/Sub
        {
            qry = "update DairyCount set DuplicateDocsSub=DuplicateDocsSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "MembershipCertificate") // Sutdent Memebrship Duplicate
        {
            qry = "update DairyCount set DuplicateDocsSub=DuplicateDocsSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        else if (ddlAppType.SelectedValue.ToString() == "MCADRegistration" | ddlAppType.SelectedValue.ToString() == "MCADLateFee")
        {
            qry = "update DairyCount set CADSub=CADSub-1 where DairyNo='" + txtDiaryNo.Text.ToString() + "'";
        }
        cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
    }
    #endregion
}