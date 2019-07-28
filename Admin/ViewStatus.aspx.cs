using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_ViewStatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    ClsEdit clsts = new ClsEdit();
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
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                dispaly();
            }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
  
    private void dispaly(){
        lblApp.Text = clsts.AppAppCount(lblExamSeasonHidden.Text.ToString());
        lblAddApp.Text = clsts.AppAddCount(lblExamSeasonHidden.Text.ToString()); 
                lblToExamDiary.Text = clsts.ExamToDiary(lblExamSeasonHidden.Text.ToString());
                lblExam.Text = lblToExamDiary.Text.ToString();
                lblExamFormSub.Text = clsts.ExamFormSubmitted(lblExamSeasonHidden.Text.ToString());
                lblExamFormApproved.Text = clsts.ExamFormApproved(lblExamSeasonHidden.Text.ToString());
                lblExamFormFilled.Text = clsts.ExamFormFilled(lblExamSeasonHidden.Text.ToString());
                lblExamHold.Text=clsts.ExamFormHold(lblExamSeasonHidden.Text.ToString());
                lblExamFormRollNo.Text = clsts.ExamFormRollNO(lblExamSeasonHidden.Text.ToString());
                lblExamFormAdmitCard.Text = clsts.ExamFormAdmitCard(lblExamSeasonHidden.Text.ToString());

                lblAddDiarySub.Text = clsts.AddToDiary(lblExamSeasonHidden.Text.ToString()); lblAdd.Text = lblAddDiarySub.Text.ToString();
                lblAddSub.Text = clsts.AddFormSubmitted(lblExamSeasonHidden.Text.ToString());
                lblAddApproved.Text = clsts.AddFormApproved(lblExamSeasonHidden.Text.ToString());
                lblAddFilled.Text = clsts.AddFormFilled(lblExamSeasonHidden.Text.ToString());
                lblAddActive.Text = clsts.AddActive(lblExamSeasonHidden.Text.ToString());
                lblAddDisactive.Text = clsts.AddDisActive(lblExamSeasonHidden.Text.ToString());

                lblDiaryEntry.Text = clsts.DiaryEntry(lblExamSeasonHidden.Text.ToString());
                lblCountReceived.Text = clsts.CountRcv(lblExamSeasonHidden.Text.ToString());
                lblCountDispatch.Text = clsts.CountDispatch(lblExamSeasonHidden.Text.ToString());
                lblAccReceive.Text = clsts.AccReceive(lblExamSeasonHidden.Text.ToString());
                lblOpen.Text = clsts.AccSupply(lblExamSeasonHidden.Text.ToString());

                //lblAmt.Text = clsts.ASFAmount(lblExamSeasonHidden.Text.ToString()); 
                lblASFSub.Text = clsts.ASFSubmitted(lblExamSeasonHidden.Text.ToString()); lblASFApp.Text = clsts.ASFApproved(lblExamSeasonHidden.Text.ToString()); lblASF.Text = lblASFApp.Text;
               // lblCompAmt.Text = clsts.CompAmount(lblExamSeasonHidden.Text.ToString());
                lblCompSub.Text = clsts.CompSubmitted(lblExamSeasonHidden.Text.ToString()); lblCompApp.Text = clsts.CompApproved(lblExamSeasonHidden.Text.ToString()); lblComp.Text = lblCompApp.Text;



                lblProTotalStudent.Text = clsts.ProTotalStudent(lblExamSeasonHidden.Text.ToString()); lblProToStudent.Text = lblProTotalStudent.Text.ToString();
                lblProformaASub.Text = clsts.ProformaASubmitted(lblExamSeasonHidden.Text.ToString());
                lblProformaAApp.Text = clsts.ProformaAApproved(lblExamSeasonHidden.Text.ToString());
                lblProformaBSub.Text = clsts.ProformaBSubmitted(lblExamSeasonHidden.Text.ToString());
                lblProformaBApp.Text = clsts.ProformaBApproved(lblExamSeasonHidden.Text.ToString());
                lblCopyPending.Text = clsts.ProCopyPending(lblExamSeasonHidden.Text.ToString());
                lblCopySubmitted.Text = clsts.ProCopySubmitted(lblExamSeasonHidden.Text.ToString());
                lblPorformaC.Text = clsts.ProformaC(lblExamSeasonHidden.Text.ToString());
                lblProResubmit.Text = clsts.ProResubmit(lblExamSeasonHidden.Text.ToString());
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
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }

    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        dispaly();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
        dispaly();

    }
}