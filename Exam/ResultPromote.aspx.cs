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
using System.Globalization;

public partial class Exam_ResultPromote : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd; SessionDuration sd;
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
               sd  = new SessionDuration();
               lblExamSeasonHidden.Text=   sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();
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
        sd = new SessionDuration();
        lblExamSeasonHidden.Text = sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();
        btnPromoteResult.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        sd = new SessionDuration();
        lblExamSeasonHidden.Text = sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();
        txtYearSeason.Focus();
    }
   
    protected void btnPromoteResult_Click(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        cmd = new SqlCommand("spPromoteResult", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 200;
        cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Sessionid",Convert.ToInt32(lblExamSeasonHidden.Text));
       // cmd.Parameters.Add("@ReturnMassage", SqlDbType.NVarChar,1024);
       // cmd.Parameters["@ReturnMassage"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('')", true);
        con.Close();
        con.Dispose();
    }
    protected void btnUpdateAccount_Click(object sender, EventArgs e)
    {
        /*
         * Note: This updation only executed with current Session of Examination.
         *  1. Examcurrent.ExamStatus='NotSubmitted'
         *  2. CompositeStatus:
         * (
         *  if(PartI -->PartII) NotSubmitted
         *  else if(SectionA --->SectionB) NotSubmitted
         *  else
         *    No Change.
         *    
         *  3. EnrollStatus
         *      Submitted
         *      NotSubmitted
         *          if(PartII---->SectionA) NotSubmitted
         *          else Submitted
         *  4.CourseStatus
         *      Passed
         *      NotPassed
         *      Submitted
         *      Promotted
         *          if(partI) Passed
         *          else if(SectionA) Passed
         *          else if(SectionB) Passed
         *          else if(PartII)
         *          {
         *              if(Promotted) Promotted
         *              else Passed
         *          }
         */
        con.Close(); con.Open();
        if (ddlPart.SelectedValue == "PartI")
            UpdatePartI();
        else if (ddlPart.SelectedValue == "PartII")
            UpdatePartII();
        else if (ddlPart.SelectedValue == "SectionA")
            UpdateSectionA();
        else if (ddlPart.SelectedValue == "SectionB")
            UpdateSectionB();
        con.Close(); con.Dispose();

        /*
         * if(PartI--->PartII)
         *  Composite Apply
         *  if(SectionA--->SectionB)
         *      Composite Apply
         *  if(PartII Passed)
         *  {
         *  
         * 
         *  }
         */
    }
    private void UpdatePartI()
    {
        cmd = new SqlCommand("Update ExamCurrent set CourseStatus='Passed' where sid in(select sid from SFinalPass where SessionID='" + lblExamSeasonHidden.Text.ToString() + "' and Part='PartI' and ExamCurrent.Part=SFinalPass.Part)", con);
        cmd.ExecuteNonQuery();
    }
    private void UpdateSectionA()
    {
        cmd = new SqlCommand("Update ExamCurrent set CourseStatus='Passed' where sid in(select sid from SFinalPass where SessionID='" + lblExamSeasonHidden.Text.ToString() + "' and Part='SectionA' and ExamCurrent.Part=SFinalPass.Part)", con);
        cmd.ExecuteNonQuery();
    }
    private void UpdateSectionB()
    {
        cmd = new SqlCommand("Update ExamCurrent set  CourseStatus='Passed' where sid in(select sid from SFinalPass where SessionID='" + lblExamSeasonHidden.Text.ToString() + "' and Part='SectionB' and ExamCurrent.Part=SFinalPass.Part)", con);
        cmd.ExecuteNonQuery();
    }
    private void UpdatePartII()
    {
        cmd = new SqlCommand("Update ExamCurrent set  CourseStatus='Passed' where sid in(select sid from SFinalPass where SessionID='" + lblExamSeasonHidden.Text.ToString() + "' and Part='PartII' and ExamCurrent.Part=SFinalPass.Part)", con);
        cmd.ExecuteNonQuery();
    }
    protected void btnUpdate_Admission_Click(object sender, EventArgs e)
    {
        //Update Admission.AdmissionStatus='Direct' which student get Exemption in PartI or SectionA.
        //con.Close(); con.Open();
        //cmd = new SqlCommand("update Student set AdmissionStatus='Direct' where SID in( select distinct(SID) from SExamMarks where SessionID='" + lblExamSeasonHidden.Text.ToString() + "' and ExmpID='4' or (Part='PartI' or Part='SectionA'))", con);
        //cmd.ExecuteNonQuery();
        //con.Close(); con.Dispose();
    }
    protected void lbtnAdditionalPaperPass_click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("update SExamMarks set Status='Pass' where (SubID='TC 2.10' or SubID='TC 2.11' or SubID='TA 2.11' or SubID='TA 2.12')and GetMarks>=40 and GetMarks!='UFM' and GetMarks!='A' and GetMarks!='EXMP'", con);
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    protected void btnUpdateCompositeFees_Click(object sender, EventArgs e)
    {
        try
        {
        con.Close(); con.Open();
        cmd = new SqlCommand("spUpdateComposite", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SessionID", Convert.ToInt32(lblExamSeasonHidden.Text));
        cmd.Parameters.Add("@Error", SqlDbType.VarChar, 1000);
        cmd.Parameters["@Error"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        lblCompositeException.Text = cmd.Parameters["@Error"].Value.ToString();
        }
        catch (SqlException ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    protected void btnpromote_Click(object sender, EventArgs e)
    {
        try
        {
        con.Close(); con.Open();
        cmd = new SqlCommand("spPromotted", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SessionID",lblExamSeasonHidden.Text);
        cmd.Parameters.AddWithValue("@Course",ddlCoursePrmote.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@NoSub", ddlNoofSubject.SelectedValue.ToString());

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridView1.DataSource = dt;
        ViewState["data"] = dt;
        GridView1.DataBind();
        }
        catch (SqlException ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    protected void btnPromote_Student_click(object sender, EventArgs e)
    {
        // update ExamCurrent.CourseStaus=Promotted from Passed.
        try
        {
        con.Close(); con.Open();
        DataTable dt=(DataTable)ViewState["data"];
        for(int i=0;i<dt.Rows.Count;i++)
        {
           cmd=new SqlCommand("update ExamCurrent set CourseStatus='Promotted' where CourseStatus='Passed' and SID='"+dt.Rows[i]["sid"]+"'",con);
           cmd.ExecuteNonQuery();
        }
        }
        catch (SqlException ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.AllowPaging = false;
        GridView1.DataSource = (DataTable)ViewState["data"];
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=AdditionalPaperNotPassed.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}