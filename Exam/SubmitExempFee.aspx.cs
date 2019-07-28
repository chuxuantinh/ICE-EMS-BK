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
using System.IO;
using System.Globalization;

public partial class Exam_SubmitExempFee : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    ClsECenterCity ecity = new ClsECenterCity();
    SessionDuration sd;
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                sd = new SessionDuration();
                lblExamSeasonHidden.Text = sd.SessionToSessionID(lblExamSeasonHidden.Text).ToString();
                bindGrid();
                panelInVisible.Visible = true;
                panelVisible.Visible = false;
                ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
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
        sd = new SessionDuration();
        lblExamSeasonHidden.Text = sd.SessionToSessionID(lblExamSeasonHidden.Text).ToString();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        sd = new SessionDuration();
        lblExamSeasonHidden.Text = sd.SessionToSessionID(lblExamSeasonHidden.Text).ToString();
        txtYearSeason.Focus();
    }
    private void bindGrid()
    {
        string strBind = "select IMID,Enrolment,AppNo,Course,Part,Name,FName,DOB,DNo,FormType,Amount,Exempted from AppRecord where Status!='Hold' and Status !='NotApproved' and Exempted!=0 ";
        SqlDataAdapter adp = new SqlDataAdapter(strBind, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridSubmitExmpFee.DataSource = dt;
        gridSubmitExmpFee.DataBind();
    }
    protected void gridSubmitExmpFee_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid2Bind();
        panelVisible.Visible = true;
        GridCivilArch.Focus();
    }
    private void Grid2Bind()
    {
        try
        {
            if (gridSubmitExmpFee.SelectedRow.Cells[4].Text == "Civil")
            {
                SqlDataAdapter adp = new SqlDataAdapter("select SubID,SubName,SubjectType from CivilSubMaster where SubID Not in(select SubID from SExamMarks where Part='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "' and SID='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text + "' and Status='Pass') and (([Section] ='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "') and ([CourseID]=081)) ORDER BY [SubID]", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                GridCivilArch.DataSource = dt;
                GridCivilArch.DataBind();
            }
            else
            {
                SqlDataAdapter adp = new SqlDataAdapter("select SubID,SubName,SubjectType from ArchiSubMaster where SubID Not in(select SubID from SExamMarks where Part='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "' and SID='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text + "' and Status='Pass') and (([Section] ='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "') and ([CourseID]=081)) ORDER BY [SubID]", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                GridCivilArch.DataSource = dt;
                GridCivilArch.DataBind();
            }
        }
        catch (SqlException ex)
        {
        }
        finally
        {
        }
    }
    //protected void chkAppSubjectC_CheckChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int count = 0;
    //        GridViewRow rw = ((CheckBox)sender).NamingContainer as GridViewRow;
    //        CheckBox cb = (CheckBox)rw.FindControl("chkAppSubjectA");
    //        for (int j = 0; j <= GridCivilArch.Rows.Count - 1; j++)
    //        {
    //            CheckBox cb1 = (CheckBox)GridCivilArch.Rows[j].FindControl("chkAppSubjectA");
    //            if (cb1.Checked == true)
    //                count++;
    //        }
    //        int subno = Convert.ToInt32(gridSubmitExmpFee.SelectedRow.Cells[12].Text.ToString()) / 500;
    //        if (count > subno)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('You can not Checked more  than " + subno + " subject.!')", true);
    //            cb.Checked = false;
    //        }
    //        cb.Focus();
    //    }
    //    catch (FormatException ex)
    //    {
    //    }
    //}
    protected void gridSubmitExmpFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[8].Text == "" | e.Row.Cells[7].Text == "&nbsp;")
            {
            }
            else
            {
                e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToString("dd/MM/yyyy");
            }
            e.Row.Cells[11].Text = e.Row.Cells[11].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[12].Text = e.Row.Cells[12].Text.TrimEnd('0').TrimEnd('.');
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Select";
            e.Row.Cells[9].Text = "Diary No.";
            e.Row.Cells[2].Text = "Membership";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            //cmd = new SqlCommand("select MembershipNo from ExemptionForm where MembershipNo='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text + "' and Session='" +lblExamSeasonHidden.Text.ToString() + "'",con);
            //string strMembershipNo = Convert.ToString(cmd.ExecuteScalar());
            //if (strMembershipNo == "")
            //{
            cmd = new SqlCommand();
            SqlTransaction sTR ;
            sTR=con.BeginTransaction();
            cmd.Transaction = sTR;
            cmd.Connection = con;
            try
            {
                for (int j = 0; j <= GridCivilArch.Rows.Count - 1; j++)
                {
                    CheckBox cb1 = (CheckBox)GridCivilArch.Rows[j].FindControl("chkAppSubjectA");
                    if (cb1.Checked == true && txtRemarks.Text != "")
                    {
                        cmd.CommandText = "insert into SExamMarks(SID,SubID,Status,GetMarks,ExamSeason,Course,Part,Center,RollNo,SessionID,ExmpID) Values('" + gridSubmitExmpFee.SelectedRow.Cells[2].Text + "','" + GridCivilArch.Rows[j].Cells[0].Text.ToString() + "','Pass','50','" + ddlExamSeason.SelectedValue + txtYearSeason.Text.ToString() + "','" + gridSubmitExmpFee.SelectedRow.Cells[4].Text + "','" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "','','','" + lblExamSeasonHidden.Text.ToString() + "','4')";
                        cmd.ExecuteNonQuery();
                        //cmd = new SqlCommand("insert into ExemptionForm(IMID,MembershipNo,SubID,SubName,Session,Remarks) values (@IMID,@MembershipNo,@SubID,@SubName,@Session,@Remarks)", con);
                        //cmd.Parameters.AddWithValue("@IMID", gridSubmitExmpFee.SelectedRow.Cells[1].Text);
                        //cmd.Parameters.AddWithValue("@MembershipNo", gridSubmitExmpFee.SelectedRow.Cells[2].Text);
                        //cmd.Parameters.AddWithValue("@SubID", GridCivilArch.Rows[j].Cells[0].Text.ToString());
                        //cmd.Parameters.AddWithValue("@SubName", GridCivilArch.Rows[j].Cells[1].Text.ToString());
                        //cmd.Parameters.AddWithValue("@Session", lblExamSeasonHidden.Text.ToString());
                        //cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                        //cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        CheckBox cb2 = (CheckBox)GridCivilArch.Rows[j].FindControl("chkAppSubjectA");
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Fill Details.!')", true);
                        cb2.Checked = false;
                        //txtRemarks.Text = "";
                    }
                }
                // SFinal Pass Promotion Query
                if (gridSubmitExmpFee.SelectedRow.Cells[5].Text == "PartI")
                {
                    cmd.CommandText = "insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + gridSubmitExmpFee.SelectedRow.Cells[4].Text + "' and Part='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "' and (Status='Pass' or Status='NotPass')  group by SID,Course,Part having count(Status)=6 and SID='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text.ToString() + "' and avg(cast(GetMarks as decimal))>='50'";
                }
                if (gridSubmitExmpFee.SelectedRow.Cells[5].Text == "PartII" && gridSubmitExmpFee.SelectedRow.Cells[4].Text == "Civil")
                {
                    cmd.CommandText = "insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + gridSubmitExmpFee.SelectedRow.Cells[4].Text + "' and Part='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "' and (Status='Pass' or Status='NotPass')  and SubID!='TC 2.10' and SubID!='TC 2.11'  group by SID,Course,Part having count(Status)=9 and SID='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text.ToString() + "' and avg(cast(GetMarks as decimal))>='50'";
                }
                if (gridSubmitExmpFee.SelectedRow.Cells[5].Text == "PartII" && gridSubmitExmpFee.SelectedRow.Cells[4].Text == "Architecture")
                {
                    cmd.CommandText = "insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + gridSubmitExmpFee.SelectedRow.Cells[4].Text + "' and Part='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "' and ( Status='Pass' or Status='NotPass') and SubID!='TA 2.11' and SubID!='TA 2.12'  group by SID,Course,Part having count(Status)=10 and SID='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text.ToString() + "' and avg(cast(GetMarks as decimal))>='50'";
                }
                if (gridSubmitExmpFee.SelectedRow.Cells[5].Text == "SectionB")
                    cmd.CommandText = "insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + gridSubmitExmpFee.SelectedRow.Cells[4].Text + "' and Part='" + gridSubmitExmpFee.SelectedRow.Cells[5].Text + "' and (Status='Pass' or Status='NotPass')  group by SID,Course,Part having count(Status)=10 and SID='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text.ToString() + "' and avg(cast(GetMarks as decimal))>='50'";
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Successfully Submitted.!')", true);
                sTR.Commit();
                con.Close();
            }
            catch (SqlException ex)
            {
                sTR.Rollback();
            }
            finally
            {
                con.Dispose();
            }
            //txtRemarks.Text = "";
            ddlExamSeason.Focus();
        }
        catch (NullReferenceException ex)
        {
        }
    }
    protected void btnPromote_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            if (gridSubmitExmpFee.SelectedRow.Cells[5].Text.ToString() == "PartI")
            {
                cmd = new SqlCommand("update ExamCurrent set Part='PartII', CompositeStatus='NotSubmitted' where SId='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text + "'", con);
            }
            else if (gridSubmitExmpFee.SelectedRow.Cells[5].Text.ToString() == "PartII")
            {
                cmd = new SqlCommand("update ExamCurrent set Stream='Asso', Part='SectionA', EnrollStatus='NotSubmitted' where SId='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text + "'", con);
            }
            else if (gridSubmitExmpFee.SelectedRow.Cells[5].Text.ToString() == "SectionA")
            {
                cmd = new SqlCommand("update ExamCurrent set Part='SectionB', CompositeStatus='NotSubmitted' where SId='" + gridSubmitExmpFee.SelectedRow.Cells[2].Text + "'", con);
            }
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Student Promoted.!')", true);
        }
        catch (FormatException ex)
        {
        }
    }
    protected void txtMembership_TextChanged(object sender, EventArgs e)
    {
        string strBind = "select IMID,Enrolment,AppNo,Course,Part,Name,FName,DOB,DNo,FormType,Amount,Exempted from AppRecord where Status like 'no%' and Status !='NotApproved' and Exempted!=0 and Enrolment='"+txtMembership.Text+"' ";
        SqlDataAdapter adp = new SqlDataAdapter(strBind, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridSubmitExmpFee.DataSource = dt;
        gridSubmitExmpFee.DataBind();
    }
}