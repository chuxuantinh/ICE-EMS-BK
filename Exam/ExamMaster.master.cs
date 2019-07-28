using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Exam_ExamMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
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
                    showimg();
                    try
                    {
                        SqlDataReader reader;
                        con.Open();
                        lbtnUserName.Text = Convert.ToString(Request.QueryString["dev"]);
                        SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                            int lvl = Convert.ToInt32(reader[20].ToString());
                            if (lvl == 0)
                                lblWelcome.Text = "Administrator";
                            else if (lvl == 1)
                            {
                                lblWelcome.Text = "Admin";
                                panelUpdate.Visible = false;
                            }
                            else if (lvl == 2)
                            {
                                panelExamForm.Visible = false; panelSeatingArrangement.Visible = false; panelDegree.Visible = false; panelPaperSetter.Visible = false;
                                panelExamPaper.Visible = false; panelMarksFeed.Visible = false; PanelAdmitCard.Visible = false; PanelRollNo.Visible = false; panelExamSchedule.Visible = false;
                                panelCertificate.Visible = false; panelExamCenter.Visible = false; panelMarking.Visible = false; panelUFM.Visible = false; panelRechecking.Visible = false;
                                panelUpdate.Visible = false;
                                lblWelcome.Text = "User ID";
                                usermanage.Visible = false;
                                if (reader["ExamForm"].ToString() == "ExamForm") panelExamForm.Visible = true;
                                if (reader["Seating"].ToString() == "Seating") panelSeatingArrangement.Visible = true;
                                if (reader["Marksheet"].ToString() == "Marksheet") panelDegree.Visible = true;
                                if (reader["PaperSetter"].ToString() == "PaperSetter") panelPaperSetter.Visible = true;
                                if (reader["ExamPaper"].ToString() == "ExamPaper") panelExamPaper.Visible = true;
                                if (reader["MarksFeed"].ToString() == "MarksFeed") panelMarksFeed.Visible = true;
                                if (reader["AdmitCard"].ToString() == "AdmitCard") PanelAdmitCard.Visible = true;
                                if (reader["RollNO"].ToString() == "RollNO") PanelRollNo.Visible = true;
                                if (reader["ExamAdmin1"].ToString() == "ExamSchedule") panelExamSchedule.Visible = true;
                                if (reader["Certi"].ToString() == "Certi") panelCertificate.Visible = true;
                                if (reader["ExamCenter"].ToString() == "ECenter") panelExamCenter.Visible = true;
                                if (reader["Marking"].ToString() == "Marking") panelMarking.Visible = true;
                                if (reader["UFM"].ToString() == "UFM") panelUFM.Visible = true;
                                if (reader["Rechecking"].ToString() == "Rechecking") panelRechecking.Visible = true;
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        con.Close(); con.Dispose();
                    }
                    catch (SqlException ex)
                    {
                        lblWelcome.Text = ex.ToString();
                    }
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    }

    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {  try
        { 
        maikal mk = new maikal();
        int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
        if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            Response.Redirect("../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
        else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/Exam/Default.aspx?name=" + Request.QueryString["dev"] + "&lnk=rpt&lvl=zero&typ="+Request.QueryString["typ"]);
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    public void showimg()
    {
        if (Request.QueryString["lnk"].ToString() == "create")
        {
            imgbtnCreate.ImageUrl = "~/images/createcolor.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "update")
        {
            imgbtnRecover.ImageUrl = "~/images/user_update.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "delete")
        {
            imgbtnDelete.ImageUrl = "~/images/user_delete.png";
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void lbtnExamFrom_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamForm.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnImportExamDAta_Click(object sender, EventArgs e)
    {
        Response.Redirect("importExamDAta.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnExaminationDate_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamSchedule.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnRollNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("GenerateRollNo.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewRollNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("GenViewRollNo.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnChangeCenter_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ChangeCenter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnChangeCity_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ChangeExamCity.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAdmitCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCard.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAdmitCardGen_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCardGen.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAdmitCardAppli_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCardAppli.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnMarksFeed_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeedMarks.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnMarksUPload_Click(object sender, EventArgs e)
    {
        Response.Redirect("UploadMarks.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnRecheckingMarks_Click(object sender, EventArgs e)
    {
        Response.Redirect("RecheckingUpdate.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnpaperSetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamPaperSetter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnpaperUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamPaperUpload.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnCenterRegi_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamCenter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void lbtnCenterAdmin_Click(object sender, EventArgs e)
    {
        string str;
        if (DateTime.Now.Month <= 6)
            str = "Sum" + DateTime.Now.Year.ToString();
        else
            str = "Win" + DateTime.Now.Year.ToString();           
        Response.Redirect("ExamSponsor.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=&session="+str);
    }
    protected void lbtnSeating_Click(object sender, EventArgs e)
    {
      //  Response.Redirect("ExamSeating.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
        Response.Redirect("SeatingArrangement.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnProvisional_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProvisionalCerti.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewProvisional_Click(object sender, EventArgs e)
    {
    }
    protected void lbtnViewCenterAdminProfile_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ViewExamSponcer.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnManageECity_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ManageECity.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewExamCenter_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ViewExamCenter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    
    protected void lbtnViewExamSchedule_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewExamSchedule.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewMarksFeed_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewMarksDetails.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewpaperSetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewPaperSetter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnviewSeating_Click(object sender,EventArgs e)
    {
        Response.Redirect("ViewSeating.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewRechecking_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("RecheckingView.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnRecheckingEntry_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("RecheckingFrom.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnUFMdeetails_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("UFMView.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnUFMManage_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("UFMUpdate.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnUFMSubmit_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ExamUFM.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnEditExamFrom_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EditExamForm.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnApproveMarksEntry_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveMarks.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnApproveMarksheets_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveMarksheet.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnApproveFinalMarksheet_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ApproveFinalMarksheet.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewMarkDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("MarksStatement.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnMarksAppli_Click(object sender, EventArgs e)
    {
        Response.Redirect("MarksStatementAppli.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAddRooms_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("AddRooms.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnOldPapers_Click(object sender, EventArgs e)
    {
        Response.Redirect("OldPapers.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnExempForm_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubmitExempFee.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnExamHold_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("HoldExamForm.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    
    protected void lbtnDeleteExamForm_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExamFormDeletion.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["dev"]);
    }
    protected void lbtnBookletRange_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookletRange.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnPRange_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookletPRange.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnPromoteResult_Click(object sender, EventArgs e)
    {
        Response.Redirect("ResultPromote.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAdmitCardDuplicate_Click(object sender, EventArgs e)
    {
        Response.Redirect("DuplicateAdmitCard.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
}