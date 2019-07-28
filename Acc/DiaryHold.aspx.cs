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

public partial class Acc_DiaryHold : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    btnSave.Enabled = false;
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
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
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from DairyCount where DairyNo='" + txtDiaryNo.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblADDRcv.Text = reader["ADDRcv"].ToString();
            lblADDSub.Text = reader["ADDSub"].ToString();
            lblODDRcv.Text = reader["ODDRcv"].ToString();
            lblODDSub.Text = reader["ODDSub"].ToString();
            lblAdmissionRcv.Text = reader["EnrollFormRcv"].ToString();
            lblAdmissionSub.Text = reader["EnrollFormSub"].ToString();
            lblExamRcv.Text = reader["ExamFormRcv"].ToString();
            lblExamSub.Text = reader["ExamFormSub"].ToString();
            lblITIRcv.Text = reader["ITIRcv"].ToString(); lblITISub.Text = reader["ITISub"].ToString();
            lblCADRcv.Text = reader["CADRcv"].ToString(); lblCADSub.Text = reader["CADSub"].ToString();
            lblOthersFormRcv.Text = reader["OtherFormRcv"].ToString(); lblOthersFormSub.Text = reader["OtherFormSub"].ToString();
            lblProvisionalRcv.Text = reader["ProvisionalRcv"].ToString(); lblProvisionalSub.Text = reader["ProvisionalSub"].ToString();
            lblFinalPassRcv.Text = reader["FinalPassRcv"].ToString(); lblFinalPassSub.Text = reader["FinalPassSub"].ToString();
            lblReCheckingRcv.Text = reader["ReCheckingRcv"].ToString(); lblReCheckingSub.Text = reader["ReCheckingSub"].ToString();
            lblDuplicateRcv.Text = reader["DuplicateDocsRcv"].ToString(); lblDuplicateSub.Text = reader["DuplicateDocsSub"].ToString();
            lblMembershipRcv.Text = reader["MemberRcv"].ToString(); lblMembershipSub.Text = reader["MemberSub"].ToString();
            lblBooksRcv.Text = reader["BooksRcv"].ToString(); lblBooksSub.Text = reader["BooksSub"].ToString();
            lblProsRcv.Text = reader["ProspectusRcv"].ToString(); lblProsSub.Text = reader["ProspectusSub"].ToString();
            txtRemarks.Text = reader["LatterTo"].ToString();
            btnSave.Enabled = true;
        }
        else
        {
            clear();
            btnSave.Enabled = false;
        }
        reader.Close();
        cmd = new SqlCommand("select * from ProjectCount where DairyNo='" + txtDiaryNo.Text.ToString() + "'", con);
        SqlDataReader read;
        read = cmd.ExecuteReader();
        while (read.Read())
        {
            lblProjectRcv.Text = read["DDRcv"].ToString(); lblProjectSub.Text = read["DDSub"].ToString();
            lblProformaCRcv.Text = read["ProformaARcv"].ToString(); lblProformaCSub.Text = read["ProformaASub"].ToString();
            lblProformaBRcv.Text = read["ProformaBRcv"].ToString(); lblProformaBSub.Text = read["ProformaBSub"].ToString();
        }
        read.Close();
        con.Close();
        con.Dispose();
    }
    private void clear()
    {
        lblADDRcv.Text = ""; lblADDSub.Text = "";
        lblODDRcv.Text = ""; lblODDSub.Text = "";
        lblAdmissionRcv.Text = ""; lblAdmissionSub.Text = ""; lblExamRcv.Text = ""; lblExamSub.Text = "";
        lblITIRcv.Text = ""; lblITISub.Text = ""; lblCADRcv.Text = ""; lblCADSub.Text = "";
        lblOthersFormRcv.Text = ""; lblOthersFormSub.Text = ""; lblProvisionalRcv.Text = ""; lblProvisionalSub.Text = "";
        lblFinalPassRcv.Text = ""; lblFinalPassSub.Text = "";
        lblReCheckingRcv.Text = ""; lblReCheckingSub.Text = ""; lblDuplicateRcv.Text = ""; lblDuplicateSub.Text = "";
        lblMembershipRcv.Text = ""; lblMembershipSub.Text = ""; lblBooksRcv.Text = ""; lblBooksSub.Text = "";
        lblProjectRcv.Text = ""; lblProjectSub.Text = ""; lblProformaBRcv.Text = ""; lblProformaBSub.Text = "";
        lblProformaCRcv.Text = ""; lblProformaCSub.Text = ""; lblProsRcv.Text = ""; lblProsSub.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("update DairyCount  set LatterTo=@latterTo where DairyNo='"+txtDiaryNo.Text.ToString()+"'", con);
            cmd.Parameters.AddWithValue("@latterTo", txtRemarks.Text.ToString());
            con.Close(); con.Open();
            cmd.ExecuteNonQuery();
            lblExceptionOK.Text = "Successfully Remarks Submitted.";
        }
        catch (SqlException ex)
        {
            lblExceptionOK.Text = "Invalid Data";
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
}