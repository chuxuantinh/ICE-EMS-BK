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

public partial class project_ViewProject : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    ClsEdit clsts = new ClsEdit();
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
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
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    ddlSysStatus.Visible = false; pnldetails.Visible = false; pnlspace.Visible = true; pnltxt.Visible = true; pnlStatus.Visible = false;
                    countall(); 
                    txtSid.Focus();
                }
            }
        }
        catch
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
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        countall(); bind();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        countall();bind();
    }
    private void countall()
    {
        lblProToStudent.Text = clsts.ProTotalStudent(lblSeasonHidden.Text.ToString());
        lblProformaASub.Text = clsts.ProformaASubmitted(lblSeasonHidden.Text.ToString());
        lblProformaAApp.Text = clsts.ProformaAApproved(lblSeasonHidden.Text.ToString());
        lblProformaBSub.Text = clsts.ProformaBSubmitted(lblSeasonHidden.Text.ToString());
        lblProformaBApp.Text = clsts.ProformaBApproved(lblSeasonHidden.Text.ToString());
        lblCopyPending.Text = clsts.ProCopyPending(lblSeasonHidden.Text.ToString());
        lblCopySubmitted.Text = clsts.ProCopySubmitted(lblSeasonHidden.Text.ToString());
        lblPorformaC.Text = clsts.ProformaC(lblSeasonHidden.Text.ToString());
        lblProResubmit.Text = clsts.ProResubmit(lblSeasonHidden.Text.ToString());
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }
    private void bind()
    {
        string qry = "";
        if (ddlSearch.SelectedValue == "Status")
        {
                qry = "select SN, SID,StudentName,IMID,Course,Part,Session,EntryStatus from Project where Status='" + ddlStatus.SelectedValue.ToString() + "' and Session='" + lblSeasonHidden.Text + "' order by SID";
        }
        else if (ddlSearch.SelectedValue == "IMID")
        {
            qry = "select SN, SID,StudentName,IMID,Course,Part,Session,EntryStatus from Project where IMID='" + txtID.Text + "' and Session='" + lblSeasonHidden.Text + "' order by SID";
        }
        else if (ddlSearch.SelectedValue == "Enrolment")
        {
            qry = "select SN,SID,StudentName,IMID,Course,Part,Session,EntryStatus from Project where SID='" + txtID.Text + "'";
        }
        else if (ddlSearch.SelectedValue == "InstitutionID")
        {
            qry = "select SN,SID,StudentName,IMID,Course,Part,Session,EntryStatus from Project where InstitutionID='" + txtID.Text + "' and Session='" + lblSeasonHidden.Text + "' order by SID";
        }
        SqlDataAdapter adp = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridToBeApprove.DataSource = dt;
        GridToBeApprove.Columns[9].Visible = false;
        GridToBeApprove.DataBind();
    }
    protected void ddlSysStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void GridToBeApprove_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridToBeApprove.Columns[9].Visible = true;
        pnldetails.Visible = true; pnlspace.Visible = false; 
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from Project where SN='" + GridToBeApprove.SelectedDataKey.Value.ToString() + "'",con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            txtStatus.Text = rd["Status"].ToString(); txtSynopsisStatus.Text = rd["SynopsisStatus"].ToString();
            txtSid.Text = rd["SID"].ToString();
            lblIMID.Text = rd["IMID"].ToString();
            ddlCourse.Text = rd["Course"].ToString();
            ddlPart.Text = rd["Part"].ToString();
            lblStuName.Text = rd["StudentName"].ToString();
            txtInstID.Text = rd["InstitutionID"].ToString(); ddlOpn4.Text = rd["Institution"].ToString(); txtGP.Text = rd["GroupID"].ToString();
            txtGm1.Text = rd["GroupMate1"].ToString(); txtGm2.Text = rd["GroupMate2"].ToString(); txtGm3.Text = rd["GroupMate3"].ToString();
            ddlOpn1.Text = rd["Option1"].ToString(); ddlOpn2.Text = rd["Option2"].ToString(); ddlOpn3.Text = rd["Option3"].ToString();
            txtSynTtl.Text = rd["SynopsisTitle"].ToString(); txtSynRemark.Text = rd["SynopsisRemarks"].ToString(); ddlCourseStatus.Text = rd["CourseStatus"].ToString();
            txtDB.Text = rd["DiaryB"].ToString(); txtProNo.Text = rd["ProjectNo"].ToString(); txtDuration.Text = rd["Duration"].ToString(); txtProTtl.Text = rd["ProjectTitle"].ToString();
            txtDes.Text = rd["Description"].ToString(); txtRemark.Text = rd["Remark"].ToString();
            if (rd["ProjectDate"] == DBNull.Value) txtProDate.Text = "N/A"; else txtProDate.Text = Convert.ToDateTime(rd["ProjectDate"]).ToString("dd/MM/yyyy"); if (rd["ProjectAppDate"] == DBNull.Value) txtProAppDate.Text = "N/A"; else txtProAppDate.Text = Convert.ToDateTime(rd["ProjectAppDate"]).ToString("dd/MM/yyyy");
            txtLetRemark.Text = rd["LetterRemarks"].ToString(); if (rd["LetterIssueDate"] == DBNull.Value) txtLetIsDate.Text = "N/A"; else txtLetIsDate.Text = Convert.ToDateTime(rd["LetterIssueDate"]).ToString("dd/MM/yyyy"); txtAppFees.Text = rd["ApprovalFees"].ToString().TrimEnd('0').TrimEnd('.'); txtEvalFees.Text = rd["EvalutionFees"].ToString().TrimEnd('0').TrimEnd('.'); txtTraFees.Text = rd["TrainingFees"].ToString().TrimEnd('0').TrimEnd('.'); txtGuidFees.Text = rd["GuidanceFees"].ToString().TrimEnd('0').TrimEnd('.');
            ddlCopies.Text = rd["NoOfCopies"].ToString(); txtDNo.Text = rd["DispatchNo"].ToString(); if (rd["SendDate"] == DBNull.Value) txtSenDate.Text = "N/A"; else txtSenDate.Text = Convert.ToDateTime(rd["SendDate"]).ToString("dd/MM/yyyy"); if (rd["GradeDate"] == DBNull.Value) txtGDate.Text = "N/A"; else txtGDate.Text = Convert.ToDateTime(rd["GradeDate"]).ToString("dd/MM/yyyy"); if (rd["CopySubmitDate"] == DBNull.Value) txtCpySubDate.Text = "N/A"; else txtCpySubDate.Text = Convert.ToDateTime(rd["CopySubmitDate"]).ToString("dd/MM/yyyy");
            ddlGrade.Text = rd["Grade"].ToString(); if (rd["EvalutionDate"] == DBNull.Value) txtEvalDate.Text = "N/A"; else txtEvalDate.Text = Convert.ToDateTime(rd["EvalutionDate"]).ToString("dd/MM/yyyy"); txtDC.Text = rd["DiaryC"].ToString();
        }
        rd.Close();
        con.Close(); con.Dispose();
        txtSid.Focus();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtID.Text = "";
        if (ddlSearch.SelectedValue == "Status")
        {
            pnltxt.Visible = false; pnlStatus.Visible = true;             pnlStatus.Focus();
        }
        else
        {
            pnltxt.Visible = true; pnlStatus.Visible = false; pnltxt.Focus();
        }
    }
    protected void txtID_TextChanged(object sender, EventArgs e)
    {
        bind(); GridToBeApprove.Focus();
    }
    protected void DeleteRecord(object sender, GridViewDeleteEventArgs e)
    {
        string SN = GridToBeApprove.DataKeys[e.RowIndex].Value.ToString();
        try
        {
            con.Open();
            cmd = new SqlCommand("delete from Project where SN='" + SN + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Record deleted successfully.!')", true);
            bind();
        }
        catch (SqlException ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ee.ToString() + "')", true);
        }
        finally
        {
           con.Close(); con.Dispose();
        }
    }
   
}