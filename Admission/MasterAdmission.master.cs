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
public partial class MasterAdmission : System.Web.UI.MasterPage
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                showimg();
                try
                {
                    SqlDataReader reader;
                    con.Close(); con.Open();
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["name"]);
                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                        int lvl = Convert.ToInt32(reader[20].ToString());
                        if (lvl == 0)
                        {
                            lblWelcome.Text = "Administrator";
                        }
                        else if (lvl == 1)
                        {
                            lblWelcome.Text = "Admin";
                        }
                        else if (lvl == 2)
                        {
                            lblWelcome.Text = "User ID";
                            usermanage.Visible = false;
                            panelAdminManage.Visible = false;
                            lbtnNewAdmission.Visible = false; lbtnRenewal.Visible = false; lblUpdoadDoc.Visible = false;
                            lbtnUploadImages.Visible = false; lbtnIMIDChange.Visible = false; lbtnGenerateID.Visible = false;
                            lbtnPromoteAdmission.Visible = false;
                            if (Request.QueryString["typ"] == "Ad")
                            {
                                if (reader["Admission"].ToString() == "Ad")
                                {
                                    lbtnNewAdmission.Visible = true; lblUpdoadDoc.Visible = true;
                                    lbtnUploadImages.Visible = true;
                                }
                                if (reader["AdmissionApprove"].ToString() == "AdmissionApprove")
                                {
                                    lbtnGenerateID.Visible = true; lbtnIMIDChange.Visible = true;
                                    lbtnRenewal.Visible = true; lbtnPromoteAdmission.Visible = true;
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
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
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            {
                Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/Student/Default.aspx?maikal="+Request.QueryString["name"]+"&lnk=rpt&lvl=zero&typ=AD");
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            {
                Response.Redirect("../User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
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
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            {
                Response.Redirect("../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
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
    protected void lbtnNewAdmission_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/Admission.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnRenewalAdmisoin_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ViewApprovedForms.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/EditAdmission.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnUploaddocs_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/UploadDocs.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad&pnl=docs");
    }
    protected void lbtnUploadImages_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/UploadDocs.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad&pnl=img");
    }
    protected void lbtnViewProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/AdmissionDepart.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnViewStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ViewStatus.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad&pnl=docs");
    }
    protected void lbtnChangeIMID_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ChangeIMID.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnEnrolmetID_Click(object sender, EventArgs e)  // Generate enrolment Id......................................
    {
        Response.Redirect("../Admission/ApproveAdmission.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnChangeCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ChangeCourse.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnCancelAdmission_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/DeleteAdmission.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void lbtnCreateAdmin_Click(object sender, EventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
            {
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
            }
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
            {
                Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            }
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    
    protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Admission/SearchStudent.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnPromoteAdmission_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/PromoteAdmission.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnITIForms_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ITIForm.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnITIRollNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ITIRollNo.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnITI_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ITIApplication.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnEditITIForms_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/EditITIForms.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnUploadMultiple_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/UploadmultipleImage.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad&pnl=img");
    }
    protected void lbtnITIPromotion_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ITIPromotion.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["name"]);
    }
    protected void ibtnStudentCredential_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Admission/EditStudEduExp.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnExpEdu_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/EditStudEduExp.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnAutocad_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/AutoCAdForms.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void btnMCADBatch_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/MCADBatch.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void btnMCADUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/AutoCADUpload.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void btnViewAutoCAD_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ViewAutoCadForms.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnAdditionalPaper_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/AdditionalPaperStudent.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
    protected void lbtnChangeFeeLevel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admission/ChangeFeeLevel.aspx?name=nv1&lnk=null&typ=" + Request.QueryString["typ"].ToString() + "");
    }
}