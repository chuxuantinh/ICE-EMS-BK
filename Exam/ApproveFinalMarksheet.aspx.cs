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
using System.Xml;
using iTextSharp.text;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Exam_ApproveFinalMarksheet : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
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
        else
        {
            if (!IsPostBack)
            {
               // datastructure();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                    ddlExamSeason.SelectedValue = "Sum";
                else { ddlExamSeason.SelectedValue = "Win"; }
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                sd = new SessionDuration();
                lblExamSeasonHidden.Text = sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();
                ddlExamSeason.Focus();
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
    //private void datastructure()
    //{
    //    DataTable dtDatas = new DataTable();
    //    dtDatas.Columns.Add("SID");
    //    dtDatas.Columns.Add("Aggregate");
    //    ViewState["dtDatas"] = dtDatas;
    //}
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
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        sd = new SessionDuration();
        lblExamSeasonHidden.Text = sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();
        txtYearSeason.Focus();
    }
    protected void btnVeiw2_Click(object sender, EventArgs e)
    {
        //if ((ddlCourse.SelectedValue.ToString() == "Civil" || ddlCourse.SelectedValue == "Architecture") && (ddlPart.SelectedValue.ToString() == "PartI" || ddlPart.SelectedValue == "SectionA"))
        //    PartISectionA();
        ////else if (ddlCourse.SelectedValue == "Civil" && ddlPart.SelectedValue == "PartII")
        //// //   PartII();
        ////else if (ddlCourse.SelectedValue == "Civil" && ddlPart.SelectedValue == "SectionB")
        ////   // SectionB();
        ////else if (ddlCourse.SelectedValue == "Architecture" && ddlPart.SelectedValue == "PartII")
        ////    PartII();
        ////else if (ddlCourse.SelectedValue == "Architecture" && ddlPart.SelectedValue == "SectionB")
        ////  //  SectionB();
    }
    //private void PartISectionA()
    //{
    //      DataClassesDataContext dc = new DataClassesDataContext();
    //      var result = from ef in dc.Results
    //                 where ef.RCourse==ddlCourse.SelectedValue.ToString() && ef.RPart==ddlPart.SelectedValue.ToString() && ef.ECourse == ddlCourse.SelectedValue.ToString() && ef.Part == ddlPart.SelectedValue.ToString() && ef.ExamSeason == lblSessionHiddend.Text.ToString()
    //                 group ef by ef.SID into g
    //                 select new { GID = g.Key, Result = g,Count=g.Count() };
    //    DataTable dtDatas = (DataTable)ViewState["dtDatas"];
    //    foreach (var sid in result)
    //    {
    //        // var passcount = sid.Result.Select(ee=>new {Count=ee.SubID.Count(),sts=ee.Status}).Where(st => st.sts=="Pass");
    //        int passcount = sid.Result.Where(st => st.Status == "Pass").Count();
    //        if (passcount == 6)// All Subject Passed
    //        {
    //            //int obmarks = 0;
    //            //// get average of subjects.
    //            //foreach (var rs in sid.Result.Where(n => n.Status == "Pass"))
    //            //{
    //            //    if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Pass")
    //            //        obmarks = obmarks + 50;
    //            //    else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Fail")
    //            //        obmarks = obmarks + 0;
    //            //    else if (rs.GetMarks == "A")
    //            //        obmarks = obmarks + 0;
    //            //    else if (rs.GetMarks.ToUpper() == "EXMP")
    //            //        obmarks = obmarks + 50;
    //            //    else
    //            //        obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //            //}
    //            //float agg = 0F;
    //            //agg = (float)obmarks / (float)passcount;

    //            // Add in Gridview Record.
    //            DataRow drNewRow = dtDatas.NewRow();
    //            drNewRow["SID"] = sid.GID.ToString();
    //            drNewRow["Aggregate"] = "Passed";
    //            dtDatas.Rows.Add(drNewRow);

    //        }
    //        else if (passcount == 5)  // Check Final Pass
    //        {
    //            // Get Student Fail Subject which appeared in Current Session Exam.
    //            //var failsub = (from f in sid.Result where f.RSession == ddlsession.SelectedValue.ToString() + txtSession.Text.ToString() && f.Status == "Fail" select new { Coun = f.SubID }).ToList();
    //            //if (failsub.Count() == 0)
    //            //{
    //            //    //string id = failsub[0].Coun.ToString();
    //            //}
    //            //else
    //            //{
    //            //    string id = failsub[0].Coun.ToString();
    //            //}
    //            int obmarks = 0; bool bl = false;
    //            foreach (var rs in sid.Result.Where(ed => (ed.Status == "Pass" || ed.RSession == ddlsession.SelectedValue.ToString() + txtSession.Text.ToString())))
    //            {
    //                if (rs.Status == "Pass" && rs.GetMarks.ToUpper()!="EXMP" && rs.GetMarks.ToUpper()!="UFM")
    //                    obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //                else if (rs.GetMarks == "UFM" && rs.Status == "Pass")
    //                    obmarks = obmarks + 50;
    //                else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Fail")
    //                    obmarks = obmarks + 0;
    //                else if (rs.GetMarks.ToUpper() == "A")
    //                    obmarks = obmarks + 0;
    //                else if (rs.GetMarks.ToUpper() == "EXMP")
    //                    obmarks = obmarks + 50;
    //                if (rs.Status == "Fail" && Convert.ToInt32(rs.GetMarks) >= 40)
    //                {
    //                    bl = true;
    //                    obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //                }
    //            }
    //            if (bl == true)
    //            {
    //                float agg = 0F;
    //                agg = (float)obmarks / (float)6;
    //                if (agg >= 50F)
    //                {
    //                    // Add in GridView Record.
    //                    DataRow drNewRow = dtDatas.NewRow();
    //                    drNewRow["SID"] = sid.GID.ToString();
    //                    drNewRow["Aggregate"] = agg.ToString();
    //                    dtDatas.Rows.Add(drNewRow);
    //                }
    //            }
    //        }

    //        //GridExamForms.DataSource = sid.Result;
    //        //GridExamForms.DataBind();
    //    }
    //    ViewState["dtDatas"] = dtDatas;
    //    GridExamForms.DataSource = dtDatas;
    //    GridExamForms.DataBind();
    //}
    private void PartISectionA()
    {
    

     








            
        
        //ViewState["dtDatas"] = dtDatas;
        //GridExamForms.DataSource = dtDatas;
        //GridExamForms.DataBind()
    }
    protected void  ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //private void PartII()
    //{
    //    DataClassesDataContext dc = new DataClassesDataContext();
    //    var result = from ef in dc.Results
    //                     where ef.RCourse == ddlCourse.SelectedValue.ToString() && ef.RPart == ddlPart.SelectedValue.ToString() && ef.ECourse == ddlCourse.SelectedValue.ToString() && ef.Part == ddlPart.SelectedValue.ToString() && ef.ExamSeason == lblSessionHiddend.Text.ToString()
    //                     && ef.SubID!= "TA 2.11" && ef.SubID!= "TA 2.12" && ef.SubID!="TC 2.10" && ef.SubID!="TC 2.11"
    //                     group ef by ef.SID into g
    //                     select new { GID = g.Key, Result = g, Count = g.Count() };
    //        var dict = getcourse("081", "PartII");
    //    //081 =Course ID
    //    int coursecount=9;
    //    if (ddlCourse.SelectedValue == "Civil")
    //        coursecount = 9;
    //    else coursecount = 10;
    //    // Get Course table according to CourseID.
    //    DataTable dtDatas = (DataTable)ViewState["dtDatas"];
    //    foreach (var sid in result)
    //    {
    //        // var passcount = sid.Result.Select(ee=>new {Count=ee.SubID.Count(),sts=ee.Status}).Where(st => st.sts=="Pass");
    //        int passcount = sid.Result.Where(st => st.Status == "Pass").Count();
    //        // int v = (from s in sid.Result
    //        //         join c in dict on s.SubID.TrimEnd(' ') equals c.subid
    //        //         where s.Status == "Pass"
    //        //         select s).Count();
    //        if (passcount >= coursecount)// All Subject Passed
    //        {
    //            //int obmarks = 0;
    //            //// get average of subjects.
    //            //foreach (var rs in sid.Result.Where(n => n.Status == "Pass"))
    //            //{
    //            //    if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Pass")
    //            //        obmarks = obmarks + 50;
    //            //    else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Fail")
    //            //        obmarks = obmarks + 0;
    //            //    else if (rs.GetMarks.ToUpper() == "A")
    //            //        obmarks = obmarks + 0;
    //            //    else if (rs.GetMarks.ToUpper() == "EXMP")
    //            //        obmarks = obmarks + 50;
    //            //    else
    //            //        obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //            //}
    //            //float agg = 0F;
    //            //agg = (float)obmarks / (float)(passcount);
    //            // Add in Gridview Record.
    //            DataRow drNewRow = dtDatas.NewRow();
    //            drNewRow["SID"] = sid.GID.ToString();
    //            drNewRow["Aggregate"] = "Passed";
    //            dtDatas.Rows.Add(drNewRow);
    //        }
    //        else if (passcount == coursecount-1)  // Check Final Pass
    //        {
    //            // Get Student Fail Subject which appeared in Current Session Exam.
    //            //var failsub = (from f in sid.Result where f.RSession == ddlsession.SelectedValue.ToString() + txtSession.Text.ToString() && f.Status == "Fail" select new { Coun = f.SubID }).ToList();
    //            //if (failsub.Count() == 0)
    //            //{
    //            //    //string id = failsub[0].Coun.ToString();
    //            //}
    //            //else
    //            //{
    //            //    string id = failsub[0].Coun.ToString();
    //            //}
    //            int obmarks = 0; bool bl = false;
    //            foreach (var rs in sid.Result.Where(ed => (ed.Status == "Pass" || ed.RSession == ddlsession.SelectedValue.ToString() + txtSession.Text.ToString())))
    //            {
    //                if (rs.Status == "Pass" && rs.GetMarks.ToUpper() != "EXMP" && rs.GetMarks.ToUpper() != "UFM")
    //                    obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //                else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Pass")
    //                    obmarks = obmarks + 50;
    //                else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Fail")
    //                    obmarks = obmarks + 0;
    //                else if (rs.GetMarks.ToUpper() == "A")
    //                    obmarks = obmarks + 0;
    //                else if (rs.GetMarks.ToUpper() == "EXMP")
    //                    obmarks = obmarks + 50;
    //                if (rs.Status == "Fail" && Convert.ToInt32(rs.GetMarks) >= 40)
    //                {
    //                    bl = true;
    //                    obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //                }
    //            }
    //            if (bl == true)
    //            {
    //                float agg = 0F;
    //                agg = (float)obmarks / (float)(coursecount);
    //                if (agg >= 50F)
    //                {
    //                    // Add in GridView Record.
    //                    DataRow drNewRow = dtDatas.NewRow();
    //                    drNewRow["SID"] = sid.GID.ToString();
    //                    drNewRow["Aggregate"] = agg.ToString();
    //                    dtDatas.Rows.Add(drNewRow);
    //                }
    //            }
    //        }
    //        //GridExamForms.DataSource = sid.Result;
    //        //GridExamForms.DataBind();
    //    }
    //    ViewState["dtDatas"] = dtDatas;
    //    GridExamForms.DataSource = dtDatas;
    //    GridExamForms.DataBind();
    //}
    //private void SectionB()
    //{
    //    DataClassesDataContext dc = new DataClassesDataContext();
    //    var result = from ef in dc.Results
    //                 where ef.RCourse == ddlCourse.SelectedValue.ToString() && ef.RPart == ddlPart.SelectedValue.ToString() && ef.ECourse == ddlCourse.SelectedValue.ToString() && ef.Part == ddlPart.SelectedValue.ToString() && ef.ExamSeason == lblSessionHiddend.Text.ToString()
    //                 group ef by ef.SID into g
    //                 select new { GID = g.Key, Result = g, Count = g.Count() };
    //    var dict = getcourse("081","SectionB");
    //    // Get Course table according to CourseID.
    //    DataTable dtDatas = (DataTable)ViewState["dtDatas"];
    //    foreach (var sid in result)
    //    {
    //        // var passcount = sid.Result.Select(ee=>new {Count=ee.SubID.Count(),sts=ee.Status}).Where(st => st.sts=="Pass");
    //        int passcount = sid.Result.Where(st => st.Status == "Pass").Count();
    //        int v=(from s in sid.Result join c in dict  on s.SubID.TrimEnd(' ') equals c.subid
    //              where  s.Status=="Pass"
    //                 select s).Count();
    //        if (passcount == 10 && v==5 )// All Subject Passed
    //        {
    //           // int obmarks = 0;
    //            //// get average of subjects.
    //            //foreach (var rs in sid.Result.Where(n => n.Status == "Pass"))
    //            //{
    //            //    if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Pass")
    //            //        obmarks = obmarks + 50;
    //            //    else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Fail")
    //            //        obmarks = obmarks + 0;
    //            //    else if (rs.GetMarks.ToUpper() == "A")
    //            //        obmarks = obmarks + 0;
    //            //    else if (rs.GetMarks.ToUpper() == "EXMP")
    //            //        obmarks = obmarks + 50;
    //            //    else
    //            //        obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //            //}
    //            //float agg = 0F;
    //            //agg = (float)obmarks  / (float) passcount;
    //            // Add in Gridview Record.
    //            DataRow drNewRow = dtDatas.NewRow();
    //            drNewRow["SID"] = sid.GID.ToString();
    //            drNewRow["Aggregate"] ="Passed";
    //            dtDatas.Rows.Add(drNewRow);
    //        }
    //        else if (passcount == 9 && v>=4)  // Check Final Pass
    //        {
    //            int obmarks = 0; bool bl = false;
    //            foreach (var rs in sid.Result.Where(ed => (ed.Status == "Pass" || ed.RSession == ddlsession.SelectedValue.ToString() + txtSession.Text.ToString())))
    //            {
    //                if (rs.Status == "Pass" && rs.GetMarks.ToUpper() != "EXMP" && rs.GetMarks.ToUpper() != "UFM")
    //                    obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //                else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Pass")
    //                    obmarks = obmarks + 50;
    //                else if (rs.GetMarks.ToUpper() == "UFM" && rs.Status == "Fail")
    //                    obmarks = obmarks + 0;
    //                else if (rs.GetMarks.ToUpper() == "A")
    //                    obmarks = obmarks + 0;
    //                else if (rs.GetMarks.ToUpper() == "EXMP")
    //                    obmarks = obmarks + 50;

    //                if (rs.Status == "Fail" && Convert.ToInt32(rs.GetMarks) >= 40)
    //                {
    //                    bl = true;
    //                    obmarks = obmarks + Convert.ToInt32(rs.GetMarks.ToString());
    //                }
    //            }
    //            if (bl == true)
    //            {
    //                float agg = 0F;
    //                agg = (float)obmarks / (float)10;
    //                if (agg >= 50F)
    //                {
    //                    // Add in GridView Record.
    //                    DataRow drNewRow = dtDatas.NewRow();
    //                    drNewRow["SID"] = sid.GID.ToString();
    //                    drNewRow["Aggregate"] = agg.ToString();
    //                    dtDatas.Rows.Add(drNewRow);
    //                }
    //            }
    //        }
    //    }
    //    ViewState["dtDatas"] = dtDatas;
    //    GridExamForms.DataSource = dtDatas;
    //    GridExamForms.DataBind();
    //}
    private IEnumerable<Couse> getcourse(string courseid,string course)
    {
        DataClassesDataContext dc=new DataClassesDataContext();
        if (ddlCourse.SelectedValue == "Civil")
            return (from c in dc.CivilSubMasters where c.CourseID == courseid && c.Section==course && c.SubjectType=="Regular" select new Couse { subid = c.SubID, type = c.SubjectType ,section=c.Section});
        else return from c in dc.ArchiSubMasters where c.CourseID == courseid && c.Section==course && c.SubjectType == "Regular" select new Couse { subid = c.SubID, type = c.SubjectType, section = c.Section };
    }
    public class Couse
    {
        public string subid{get;set;}
        public string type{get;set;}
        public string section { get; set;}
    }
    protected void btnPromoteStudent_Click(object sender, EventArgs e)
    {
        // UpdateExamCurrent Table 
        //cmd = new SqlCommand("update ExamCurrent set EnrollStatus='NotSubmitted' where SId='" + GridExamForms.Rows[i].Cells[0].Text + "'", con);
        //cmd.ExecuteNonQuery();
        int i = 0;
        while (i <= GridExamForms.Rows.Count - 1)
        {
            if (ddlPart.SelectedValue != "PartII")
            {

            }
        }
    }
    protected void ddlPart_OnselectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPart.SelectedValue.ToString() == "PartI")
        {
            lblPart.Text = "Part I";
            lblHiddendStream.Text = "Tech";
            lblStreamDDL.Text = "Technical Examination.";
        }
        else if (ddlPart.SelectedValue.ToString() == "PartII")
        {
            lblHiddendStream.Text = "Tech";
            lblStreamDDL.Text = "Technical Examination.";
            lblPart.Text = "Part II";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA")
        {
            lblHiddendStream.Text = "Asso";
            lblStreamDDL.Text = "Associate Examination.";
            lblPart.Text = "Section A";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionB")
        {
            lblHiddendStream.Text = "Asso";
            lblStreamDDL.Text = "Associate Examination.";
            lblPart.Text = "Section B";
        }
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        if (GridExamForms.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=FinalPassStudent.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridExamForms.AllowPaging = false;
            GridExamForms.DataSource = (DataTable)ViewState["dtDatas"];
            GridExamForms.DataBind();

            GridExamForms.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkViewResult.Checked)
        {
            if ((ddlCourse.SelectedValue.ToString() == "Civil") && (ddlPart.SelectedValue.ToString() == "PartII"))
            {
                SqlDataAdapter adp = new SqlDataAdapter("select sid ,Course ,part  from SFinalPass where  Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and SessionId='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                ViewState["dtDatas"] = dt;
                GridExamForms.DataSource = dt;
                GridExamForms.DataBind();
                Label1.Text = GridExamForms.Rows.Count.ToString();
            }
            else if ((ddlCourse.SelectedValue.ToString() == "Civil" || ddlCourse.SelectedValue == "Architecture") && (ddlPart.SelectedValue.ToString() == "PartI" || ddlPart.SelectedValue == "SectionA"))
            {
                SqlDataAdapter adp = new SqlDataAdapter("select sid ,Course ,part   from SFinalPass where  Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "'and SessionId='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                ViewState["dtDatas"] = dt;
                GridExamForms.DataSource = dt;
                GridExamForms.DataBind();
                Label1.Text = GridExamForms.Rows.Count.ToString();
            }
            else if ((ddlCourse.SelectedValue.ToString() == "Civil" || ddlCourse.SelectedValue == "Architecture") && (ddlPart.SelectedValue == "SectionB"))
            {
                SqlDataAdapter adp = new SqlDataAdapter("select sid ,Course ,part  from SFinalPass where  Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "'and SessionId='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                ViewState["dtDatas"] = dt;
                GridExamForms.DataSource = dt;
                GridExamForms.DataBind();
                Label1.Text = GridExamForms.Rows.Count.ToString();
            }
            else if ((ddlCourse.SelectedValue == "Architecture") && (ddlPart.SelectedValue == "PartII"))
            {
                SqlDataAdapter adp = new SqlDataAdapter("select sid ,Course ,part from SFinalPass where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "'and SessionId='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                ViewState["dtDatas"] = dt;

                GridExamForms.DataSource = dt;
                GridExamForms.DataBind();
                Label1.Text = GridExamForms.Rows.Count.ToString();



            }

        }

        else
        {
            GridExamForms.DataSource = null;
            GridExamForms.DataBind();


        }


    }
   
}