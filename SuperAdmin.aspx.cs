using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Globalization;

public partial class SuperAdmin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    btnshow_Onclick();
                    SqlDataReader reader;
                    centAdmin.Visible = false;
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["dev"]);
                    centExam.Visible = false; centAdmission.Visible = false; centAccount.Visible = false; centInventory.Visible = false; centMembership.Visible = false; centProject.Visible = false; centReport.Visible = false;
                    ibtnAdmin.Visible = false;
                    lbtnAdmin.Visible = false;
                    ibtnExam.Visible = false; lbtnExam.Visible = false;
                    ibtnMembership.Visible = false;
                    ibtnAdmission.Visible = false; lbtnAdmission.Visible = false;
                    ibtnAccount.Visible = false; lbtnAccount.Visible = false;
                    ibtnInventory.Visible = false; lbtnInventory.Visible = false;
                    ibtnProject.Visible = false; lbtnProject.Visible = false;
                    ibtnReport.Visible = false; lbtnReport.Visible = false;
                    if (!IsPostBack)
                    {
                        SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Request.QueryString["dev"] + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                        con.Close();
                        con.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                           int lvl = Convert.ToInt32(reader[20].ToString());
                            if (lvl == 0)
                            {
                                lblWelcome.Text = "Administrator";
                                tdDebitNote.Visible = false;
                                updatepanelstip.Visible = true;
                            }
                            else if (lvl == 1)
                            {
                                lblWelcome.Text = "Admin";
                                updatepanelstip.Visible = false;
                              //  usermanage.Visible = false;
                                tdDebitNote.Visible = false;
                                ViewIDPanel.Visible = false;
                            }
                            else if (lvl == 2)
                            {
                                reader.Close();
                                con.Close();
                                con.Dispose();
                                Response.Redirect("Admin/SuperAdminManage.aspx");
                            }
                            if (Convert.ToString(Request.QueryString["ain"]) == "SuperAdmin")
                            {
                                tdDebitNote.Visible = true; ibtnAdmin.Visible = true; lbtnAdmin.Visible = true;         // Front Office.
                                centAdmin.Visible = true;
                            }
                            if (Convert.ToString(Request.QueryString["ain"]) == Convert.ToString(reader[5].ToString()))
                            {
                                if (Convert.ToString(Request.QueryString["ain"]) == "Admin" || Convert.ToString(reader[5].ToString()) == "Admin")
                                {
                                    ibtnAdmin.Visible = true;
                                    lbtnAdmin.Visible = true;         // Front Office.
                                    centAdmin.Visible = true;
                                }
                                if (Convert.ToString(Request.QueryString["em"]) == "E" || Convert.ToString(reader[6].ToString()) == "E")
                                {
                                    ibtnExam.Visible = true; lbtnExam.Visible = true; centExam.Visible = true;
                                }
                                if (Convert.ToString(Request.QueryString["eem"]) == "E2" || Convert.ToString(reader[7].ToString()) == "E2")
                                {
                                    ibtnMembership.Visible = true; centMembership.Visible = true;
                                }
                                if (Convert.ToString(Request.QueryString["adison"]) == "Ad" || Convert.ToString(reader[8].ToString()) == "Ad")
                                {
                                    ibtnAdmission.Visible = true; lbtnAdmission.Visible = true; centAdmission.Visible = true;
                                }
                                if (Convert.ToString(Request.QueryString["acouty"]) == "AC" || Convert.ToString(reader[9].ToString()) == "AC")
                                {
                                    ibtnAccount.Visible = true; lbtnAccount.Visible = true; centAccount.Visible = true;
                                }
                                if (Convert.ToString(Request.QueryString["i94en67"]) == "IN" || Convert.ToString(reader[10].ToString()) == "IN")
                                {
                                    ibtnInventory.Visible = true; lbtnInventory.Visible = true; centInventory.Visible = true;
                                }
                                if (Convert.ToString(Request.QueryString["Pro"]) == "Pro" || Convert.ToString(reader["Project"].ToString()) == "Pro")
                                {
                                    ibtnProject.Visible = true; lbtnProject.Visible = true; centProject.Visible = true;
                                }
                                if (Convert.ToString(Request.QueryString["rpt"]) == "rpt" || Convert.ToString(reader["Report"].ToString()) == "rpt")
                                {
                                    ibtnReport.Visible = true; lbtnReport.Visible = true; centReport.Visible = true;
                                }
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            centAdmin.Visible = false;
            centExam.Visible = false; centAdmission.Visible = false; centAccount.Visible = false; centInventory.Visible = false;
            ibtnAdmin.Visible = false;
            lbtnAdmin.Visible = false;
            ibtnExam.Visible = false; lbtnExam.Visible = false;
            ibtnAdmin.Visible = false; lbtnAdmin.Visible = false;
            ibtnAccount.Visible = false; lbtnAccount.Visible = false;
            ibtnInventory.Visible = false; lbtnInventory.Visible = false;
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void ibtnAdmin_Click(object sender, ImageClickEventArgs e)
    {
        
        Response.Redirect("Admin/SuperAdminManage.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&lvl=one&typ=FO");
    }
    protected void lbtnAdmin_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Admin/SuperAdminManage.aspx?maikal=" + Request.QueryString["dev"] + "&lnk=null&lvl=one&typ=FO");
    }
    protected void ibtnExam_Click(object sender, ImageClickEventArgs e)
    {
        
        Response.Redirect("Exam/ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void lbtnExam_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Exam/ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }

    protected void ibtnAdmission_Click(object sender, ImageClickEventArgs e)
    {
        
        Response.Redirect("Admission/AdmissionDefault.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ad");
    }

    protected void lbtnAdmission_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Admission/AdmissionDefault.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ad");
    }
    protected void ibtnAccount_Click(object sender, ImageClickEventArgs e)
    {
        
        Response.Redirect("AccountDepart.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ac");
    }

    protected void lbtnAccount_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("AccountDepart.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ac");
    }

    protected void ibtnInventory_Click(object sender, ImageClickEventArgs e)
    {
        
        Response.Redirect("Invent/Default.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");        
    }

    protected void lbtnInventory_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Invent/Default.aspx?maikal=" + Request.QueryString["maikal"] + "&lnk=null&typ=In");        
    }
    protected void lbtnProject_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Project/projectss.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro");
    }
    protected void ibtnProject_Click(object sender, ImageClickEventArgs e)
    {
        
        Response.Redirect("Project/projectss.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Pro");
    }
    protected void ibtnReport_Click(object sender, ImageClickEventArgs e)
    {
        
        Response.Redirect("Reports/ReportDefault.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=rpt");
    }
    protected void lbtnReport_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Reports/ReportDefault.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=rpt");
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
       ////Session.Remove("admin");
        Response.Redirect("Login.aspx");
    }
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        maikal mk = new maikal();
        int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
       
        if (lvl == 0)
        {
            Response.Redirect("Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
        }
        else if (lvl == 1)
        {
            Response.Redirect("User/Create.aspx?lnk=create&lvl=one");
        }
        else
        {  
        }
         }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Admin/Default.aspx");
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try{
        maikal mk = new maikal();
        int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
       
        if (lvl == 0)
        {
            Response.Redirect("Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
        }
        else if (lvl == 1)
        {
            Response.Redirect("User/Create.aspx?lnk=delete&lvl=one");
        }
        else
        {
        }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        maikal mk = new maikal();
        int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
        if (lvl == 0)
        {
            Response.Redirect("Admin/AdminCreate.aspx?lnk=update&lvl=zero&typ=Admin");
        }
        else if (lvl == 1)
        {
            Response.Redirect("User/Create.aspx?lnk=update&lvl=one");
        }
        else
        {
        }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void ibtnRegisterMem_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Administrator/Register.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&&lvl=zero");
    }
    protected void ibtnvewProfile_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Administrator/Membership.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=zero");
    }
   
    protected void ibtnMembership_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Administrator/Membership.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=zero");
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void ibtnMemberFeeMaster_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Administrator/MemberFeeMaster.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Fee&&lvl=zero&mst=member&sec=eef");
    }
    protected void ibtnAssociateFeeMaster_Click(object sender, ImageClickEventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select FeeLevel from FeeMaster where Status= 'Active' and FeeType='Asso'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("Administrator/Fees/AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Home&lvl="+id+"&sec=eef");
    }
    protected void ibtnTechFeeMaster_Click(object sender, ImageClickEventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select FeeLevel from FeeMaster where Status= 'Active' and FeeType='Tech'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("Administrator/Fees/TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Home&lvl=" + id + "&sec=eef");
    }
    protected void ibtnCreateNewFees_Click(object sender, ImageClickEventArgs e)
    {
       // Response.Redirect("Administrator/Fees/TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Home&lvl=" + id + "&sec=eef");
    }
    protected void CivilSubMaster_Click(object sender, ImageClickEventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select CourseID from CivilSubMaster where Status= 'Active'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("Administrator/Course/CivilSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Civil&lvl=zero&sec=ubs&id=" + id.ToString());
    }
    protected void ibtnArchiSubMaster_Click(object sender, ImageClickEventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select CourseID from ArchiSubMaster where Status= 'Active'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("Administrator/Course/ArchiSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Archi&lvl=zero&sec=ubs&id=" + id.ToString());
    }
    protected void ibtnDepartmentName_OnClick(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Admin/SystemName.aspx?page=department");
    }
    protected void ibtnCourierSerive_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Admin/SystemName.aspx?page=courier");
    }
    protected void ibtnBank_OnClick(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Admin/SystemName.aspx?page=bank");
    }
    protected void ibtnSessionDuration_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("Admin/SessionDuration.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=zero");
    }
    private void btnshow_Onclick()
    {
        DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime now = DateTime.Now;
        DateTime tdate = Convert.ToDateTime("20/05/2012", dtinfo);
        int i = DateTime.Compare(now, tdate);
        if (i == 0 || i == -1)
        {
        }
        else
        {
        }
    }
    protected void lbtnMembershipAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Administrator/ViewAppProfiles.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=zero");
    }
    protected void lbtnSetting_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admin/changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["dev"]);
    }
    protected void imgbtnProfile_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Profile/Letters.aspx?dev="+Request.QueryString["dev"]+"&lvl=zero&typ="+Request.QueryString["ain"]);
    }
    protected void imgbtnDebitNote_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Profile/DebitNoteReq.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void imgbtnStatus_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Admin/ViewStatus.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void imgbtnLookDiary_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Admin/TrackDiary.aspx?lnk=update&lvl=zero&typ=Admin");
    }
}
