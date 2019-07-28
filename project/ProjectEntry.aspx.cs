using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;

public partial class project_ProjectEntry : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd; SqlDataAdapter adp;
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
                    bindFinalOpn(); txtSid.Focus();
                }
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    #region Option Bind
    private void bindFinalOpn()
    {
        adp = new SqlDataAdapter("select Name from InstitutionReg order by Name", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlOpn1.DataSource = dt; ddlOpn1.DataValueField = "Name"; ddlOpn1.DataTextField = "Name"; ddlOpn1.DataBind();
        ddlOpn2.DataSource = dt; ddlOpn2.DataValueField = "Name"; ddlOpn2.DataTextField = "Name"; ddlOpn2.DataBind();
        ddlOpn3.DataSource = dt; ddlOpn3.DataValueField = "Name"; ddlOpn3.DataTextField = "Name"; ddlOpn3.DataBind();
        ddlOpn4.DataSource = dt; ddlOpn4.DataValueField = "Name"; ddlOpn4.DataTextField = "Name"; ddlOpn4.DataBind();
    }
    #endregion
    #region SID Text Changed
    protected void txtSID_OnTextChanted(object sender, EventArgs e)
    {
        adp = new SqlDataAdapter("select SN,SID,StudentName,Session,Status,SynopsisStatus,EntryStatus from Project where SID='" + txtSid.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridEval.DataSource = dt;
        GridEval.DataBind(); GridEval.Focus();
    }
    #endregion
    #region Update Project Entry
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            if (txtSid.Text == "" | lblIMID.Text == "" | txtInstID.Text == "" | lblStuName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please enter Details!')", true);
            }
            else
            {
                con.Open();
                cmd = new SqlCommand("select ID from InstitutionReg where Name='" + ddlOpn4.SelectedValue.ToString() + "'", con);
                string Insid = Convert.ToString(cmd.ExecuteScalar());
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Project set SID=@SID,Course=@Course,Part=@Part,InstitutionID=@InstitutionID,Institution=@Institution,GroupID=@GroupID,GroupMate1=@GroupMate1,GroupMate2=@GroupMate2,GroupMate3=@GroupMate3,OptionID=@OptionID,Option1=@Option1,Option2=@Option2,Option3=@Option3,Status=@Status,SynopsisStatus=@SynopsisStatus,ProjectNo=@ProjectNo,SynopsisTitle=@SynopsisTitle,ProjectTitle=@ProjectTitle,Description=@Description,Duration=@Duration,SynopsisRemarks=@SynopsisRemarks,Remark=@Remark,NoOfCopies=@NoOfCopies,DispatchNo=@DispatchNo,ApprovalFees=@ApprovalFees,EvalutionFees=@EvalutionFees,TrainingFees=@TrainingFees,GuidanceFees=@GuidanceFees,SynopsisDate=@SynopsisDate,ProjectDate=@ProjectDate,ProjectAppDate=@ProjectAppDate,LetterIssueDate=@LetterIssueDate,CopySubmitDate=@CopySubmitDate,SendDate=@SendDate,GradeDate=@GradeDate,EvalutionDate=@EvalutionDate,Grade=@Grade,DiaryA=@DiaryA,DiaryB=@DiaryB,DiaryC=@DiaryC,CourseStatus=@CourseStatus,LetterRemarks=@LetterRemarks where SN='" + Convert.ToInt32(GridEval.SelectedRow.Cells[1].Text) + "' and SID='" + GridEval.SelectedRow.Cells[2].Text + "'", con);
                cmd.Parameters.AddWithValue("@SID", txtSid.Text.ToString());
                cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString()); cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@InstitutionID", txtInstID.Text.ToString()); cmd.Parameters.AddWithValue("@Institution", ddlOpn4.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@GroupID", txtGP.Text.ToString()); cmd.Parameters.AddWithValue("@GroupMate1", txtGm1.Text.ToString()); cmd.Parameters.AddWithValue("@GroupMate2", txtGm2.Text.ToString()); cmd.Parameters.AddWithValue("@GroupMate3", txtGm3.Text.ToString());
                cmd.Parameters.AddWithValue("@OptionID", Insid.ToString()); cmd.Parameters.AddWithValue("@Option1", ddlOpn1.SelectedValue.ToString()); cmd.Parameters.AddWithValue("@Option2", ddlOpn2.SelectedValue.ToString()); cmd.Parameters.AddWithValue("@Option3", ddlOpn3.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SynopsisStatus", ddlSynStatus.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ProjectNo", txtProNo.Text.ToString());
                cmd.Parameters.AddWithValue("@SynopsisTitle", txtSynTtl.Text.ToString());
                cmd.Parameters.AddWithValue("@ProjectTitle", txtProTtl.Text.ToString());
                cmd.Parameters.AddWithValue("@Description", txtDes.Text.ToString());
                cmd.Parameters.AddWithValue("@Duration", txtDuration.Text.ToString());
                cmd.Parameters.AddWithValue("@SynopsisRemarks", txtSynRemark.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());
                cmd.Parameters.AddWithValue("@NoOfCopies", ddlCopies.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@DispatchNo", txtDNo.Text.ToString());
                if (txtAppFees.Text == "") cmd.Parameters.AddWithValue("@ApprovalFees", "0"); else cmd.Parameters.AddWithValue("@ApprovalFees", txtAppFees.Text.ToString());
                if (txtEvalFees.Text == "") cmd.Parameters.AddWithValue("@EvalutionFees", "0"); else cmd.Parameters.AddWithValue("@EvalutionFees", txtEvalFees.Text.ToString());
                if (txtTraFees.Text == "") cmd.Parameters.AddWithValue("@TrainingFees", "0"); else cmd.Parameters.AddWithValue("@TrainingFees", txtTraFees.Text.ToString());
                if (txtGuidFees.Text == "") cmd.Parameters.AddWithValue("@GuidanceFees", "0"); else cmd.Parameters.AddWithValue("@GuidanceFees", txtGuidFees.Text.ToString());
                if (txtSynDate.Text == "") cmd.Parameters.AddWithValue("@SynopsisDate", DBNull.Value); else cmd.Parameters.AddWithValue("@SynopsisDate", Convert.ToDateTime(txtSynDate.Text.ToString(), dtinfo));
                if (txtProDate.Text == "") cmd.Parameters.AddWithValue("@ProjectDate", DBNull.Value); else cmd.Parameters.AddWithValue("@ProjectDate", Convert.ToDateTime(txtProDate.Text.ToString(), dtinfo));
                if (txtProAppDate.Text == "") cmd.Parameters.AddWithValue("@ProjectAppDate", DBNull.Value); else cmd.Parameters.AddWithValue("@ProjectAppDate", Convert.ToDateTime(txtProAppDate.Text.ToString(), dtinfo));
                if (txtLetIsDate.Text == "") cmd.Parameters.AddWithValue("@LetterIssueDate", DBNull.Value); else cmd.Parameters.AddWithValue("@LetterIssueDate", Convert.ToDateTime(txtLetIsDate.Text.ToString(), dtinfo));
                if (txtCpySubDate.Text == "") cmd.Parameters.AddWithValue("@CopySubmitDate", DBNull.Value); else cmd.Parameters.AddWithValue("@CopySubmitDate", Convert.ToDateTime(txtCpySubDate.Text.ToString(), dtinfo));
                if (txtSenDate.Text == "") cmd.Parameters.AddWithValue("@SendDate", DBNull.Value); else cmd.Parameters.AddWithValue("@SendDate", Convert.ToDateTime(txtSenDate.Text.ToString(), dtinfo));
                if (txtGDate.Text == "") cmd.Parameters.AddWithValue("@GradeDate", DBNull.Value); else cmd.Parameters.AddWithValue("@GradeDate", Convert.ToDateTime(txtGDate.Text.ToString(), dtinfo));
                if (txtEvalDate.Text == "") cmd.Parameters.AddWithValue("@EvalutionDate", DBNull.Value); else cmd.Parameters.AddWithValue("@EvalutionDate", Convert.ToDateTime(txtEvalDate.Text.ToString(), dtinfo));
                cmd.Parameters.AddWithValue("@Grade", ddlGrade.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@DiaryA", txtDA.Text.ToString()); cmd.Parameters.AddWithValue("@DiaryB", txtDB.Text.ToString()); cmd.Parameters.AddWithValue("@DiaryC", txtDC.Text.ToString());
                cmd.Parameters.AddWithValue("@CourseStatus", ddlCourseStatus.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@LetterRemarks", txtLetRemark.Text.ToString());
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Updated Successfully!')", true);
                lblIMID.Text = ""; txtSid.Text = ""; lblStuName.Text = ""; txtInstID.Text = ""; txtGP.Text = ""; txtGm1.Text = ""; txtGm2.Text = ""; txtGm3.Text = ""; txtProNo.Text = ""; txtSynTtl.Text = ""; txtProTtl.Text = ""; txtDes.Text = ""; txtDuration.Text = ""; txtDNo.Text = ""; txtSynRemark.Text = ""; txtRemark.Text = ""; txtLetRemark.Text = ""; txtAppFees.Text = ""; txtAppFees.Text = ""; txtTraFees.Text = ""; txtGuidFees.Text = ""; txtDA.Text = ""; txtDB.Text = ""; txtDC.Text = ""; txtEvalFees.Text = ""; txtSynDate.Text = ""; txtProDate.Text = ""; txtProAppDate.Text = ""; txtLetIsDate.Text = ""; txtCpySubDate.Text = ""; txtSenDate.Text = ""; txtGDate.Text = ""; txtEvalDate.Text = ""; txtSid.Focus();
            }
        }
        catch (NullReferenceException ex) { lblException.Text = ex.ToString(); }
        catch (FormatException ex) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please enter Correct Date!')", true); }
        catch (OutOfMemoryException ex) { lblException.Text = ex.ToString(); }
        con.Close(); con.Dispose();  GridEval.Focus();
    }
#endregion
    #region GridView Selected Index Change
    protected void GridEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from Project where SN='"+Convert.ToInt32(GridEval.SelectedRow.Cells[1].Text)+"' and SID='" +GridEval.SelectedRow.Cells[2].Text + "'", con);
        SqlDataReader dr; dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            txtSid.Text = GridEval.SelectedRow.Cells[2].Text.ToString();
            lblIMID.Text = dr["IMID"].ToString(); lblStuName.Text = dr["StudentName"].ToString();
            ddlCourse.SelectedValue = dr["Course"].ToString(); ddlPart.SelectedValue = dr["Part"].ToString();
            txtInstID.Text = dr["InstitutionID"].ToString();
            ddlOpn1.SelectedItem.Text = dr["Option1"].ToString(); ddlOpn2.SelectedItem.Text = dr["Option2"].ToString(); ddlOpn3.SelectedItem.Text = dr["Option3"].ToString();
            ddlOpn4.SelectedItem.Text = dr["Institution"].ToString();
            if (dr["SynopsisDate"] == DBNull.Value) txtSynDate.Text = ""; else txtSynDate.Text = Convert.ToDateTime(dr["SynopsisDate"]).ToString("dd/MM/yyyy");
            txtDA.Text = dr["DiaryA"].ToString(); txtDB.Text = dr["DiaryB"].ToString(); txtDC.Text = dr["DiaryC"].ToString();
            txtGP.Text = dr["GroupID"].ToString(); txtGm1.Text = dr["GroupMate1"].ToString(); txtGm2.Text = dr["GroupMate2"].ToString(); txtGm3.Text = dr["GroupMate3"].ToString();
            ddlCourseStatus.SelectedValue = dr["CourseStatus"].ToString();
            txtSynRemark.Text = dr["SynopsisRemarks"].ToString();
            txtProNo.Text = dr["ProjectNo"].ToString();
            txtDuration.Text = dr["Duration"].ToString();
            txtProTtl.Text = dr["ProjectTitle"].ToString();
            txtDes.Text = dr["Description"].ToString();
            txtRemark.Text = dr["Remark"].ToString();
            if (dr["ProjectDate"] == DBNull.Value) txtProDate.Text = ""; else txtProDate.Text = Convert.ToDateTime(dr["ProjectDate"]).ToString("dd/MM/yyyy");
            if (dr["ProjectAppDate"] == DBNull.Value) txtProAppDate.Text = ""; else txtProAppDate.Text = Convert.ToDateTime(dr["ProjectAppDate"]).ToString("dd/MM/yyyy");
            txtLetRemark.Text = dr["LetterRemarks"].ToString();
            if (dr["LetterIssueDate"] == DBNull.Value) txtLetIsDate.Text = ""; else txtLetIsDate.Text = Convert.ToDateTime(dr["LetterIssueDate"]).ToString("dd/MM/yyyy");
            txtAppFees.Text=dr["Approvalfees"].ToString().TrimEnd('0').TrimEnd('.');
            txtEvalFees.Text = dr["EvalutionFees"].ToString().TrimEnd('0').TrimEnd('.');
            txtTraFees.Text = dr["Trainingfees"].ToString().TrimEnd('0').TrimEnd('.');
            txtGuidFees.Text = dr["GuidanceFees"].ToString().TrimEnd('0').TrimEnd('.');
            ddlCopies.SelectedValue = dr["NoOfCopies"].ToString();
            txtDNo.Text = dr["DispatchNo"].ToString();
            if (dr["SendDate"] == DBNull.Value) txtSenDate.Text = ""; else txtSenDate.Text = Convert.ToDateTime(dr["SendDate"]).ToString("dd/MM/yyyy");
            if (dr["CopySubmitDate"] == DBNull.Value) txtCpySubDate.Text = ""; else txtCpySubDate.Text = Convert.ToDateTime(dr["CopySubmitDate"]).ToString("dd/MM/yyyy");
            if (dr["GradeDate"] == DBNull.Value) txtGDate.Text = ""; else txtGDate.Text = Convert.ToDateTime(dr["GradeDate"]).ToString("dd/MM/yyyy");
            ddlGrade.SelectedValue = dr["Grade"].ToString();
            if (dr["EvalutionDate"] == DBNull.Value) txtEvalDate.Text = ""; else txtEvalDate.Text = Convert.ToDateTime(dr["EvalutionDate"]).ToString("dd/MM/yyyy");
            ddlStatus.SelectedValue = dr["Status"].ToString();
            ddlSynStatus.SelectedValue = dr["SynopsisStatus"].ToString();
            txtSynTtl.Text = dr["SynopsisTitle"].ToString();
        }
        dr.Close(); dr.Dispose(); con.Close(); con.Dispose(); ddlCourse.Focus();
    }
         #endregion
}