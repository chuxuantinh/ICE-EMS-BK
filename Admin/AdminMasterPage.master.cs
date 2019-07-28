using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Admin_AdminMasterPage : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                SqlDataReader reader;
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                    int lvl = Convert.ToInt32(reader[20].ToString());
                    if (lvl == 0)
                    {
                        lblWelcome.Text = "Administrator";
                        if (reader["Admin"] == "SuperAdmin")
                            tdDebitNote.Visible = true;
                        else tdDebitNote.Visible = false;
                    }
                    else if (lvl == 1)
                        lblWelcome.Text = "Admin";
                    else if (lvl == 2)
                        lblWelcome.Text = "User ID";
                }
                reader.Close(); reader.Dispose(); con.Close(); con.Dispose();
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
    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("AdminCreate.aspx?lnk=create&lvl=zero");
            else if (lvl == 1)
                Response.Redirect("../User/Create.aspx?lnk=create&lvl=one");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports//Default.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=");
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("AdminCreate.aspx?lnk=delete&lvl=zero");
            else if (lvl == 1)
                Response.Redirect("../User/Create.aspx?lnk=delete&lvl=one");
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
            if (lvl == 0)
                Response.Redirect("AdminCreate.aspx?lnk=update&lvl=zero");
            else if (lvl == 1)
                Response.Redirect("../User/Create.aspx?lnk=update&lvl=one");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
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
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void lbtnDepartmentName_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SystemName.aspx?page=Department");
    }
    protected void lbtnCourierService_OnClcick(object sender, EventArgs e)
    {
        Response.Redirect("SystemName.aspx?page=Courier");
    }
    protected void lbtnBankName_OnClick(object sendere, EventArgs e)
    {
        Response.Redirect("SystemName.aspx?page=Bank");
    }
    protected void lbtnSessionDuration_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SessionDuration.aspx");
    }
    protected void lbtnCity_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("City.aspx");
    }
    protected void lbtnMembership_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/MemberFeeMaster.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Fee&&lvl=zero&mst=member&sec=eef");
    }
    protected void lbtnTechnicianFees_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select FeeLevel from FeeMaster where Status= 'Active' and FeeType='Tech'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("../Administrator/Fees/TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&lvl=" + id + "&sec=eef");
    }
    protected void lbtnAssociateFees_Onclick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select FeeLevel from FeeMaster where Status= 'Active' and FeeType='Asso'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("../Administrator/Fees/AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&lvl=" + id + "&sec=eef");
    }
    protected void lbtnArchiCourse_Onclick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select CourseID from ArchiSubMaster where Status= 'Active'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("../Administrator/Course/ArchiSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Archi&lvl=zero&sec=ubs&id=" + id.ToString());
    }
    protected void lbtnCivilCourse_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select CourseID from CivilSubMaster where Status= 'Active'", con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        Response.Redirect("../Administrator/Course/CivilSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Civil&lvl=zero&sec=ubs&id=" + id.ToString());
    }
    protected void lbtnSubjectPrice_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SubjectPrices.aspx");
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditCount.aspx");
    }
    protected void lnkbtnSystemProcess_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewStatus.aspx");
    }
    protected void imgbtnProfile_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Profile/Letters.aspx?dev=" + Request.QueryString["dev"] + "&lvl=zero&typ=" + Request.QueryString["ain"]);
    }
    protected void imgbtnDebitNote_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Profile/DebitNoteReq.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void imgbtnStatus_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ViewStatus.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void imgbtnLookDiary_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("TrackDiary.aspx?lnk=update&lvl=zero&typ=Admin");
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("changePassword.aspx?lnk=update&lvl=zero&typ=Admin&name=" + Request.QueryString["name"]);
    }
    protected void lbtnAddFeesHeader_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/Fees/CreateFeesHead.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Overseas&lvl=" + Request.QueryString["lvl"]);
    }
    protected void lbtnAutoCAD_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AmountHeader.aspx?lnk=update&lvl=zero&typ=Admin");
    }

    protected void SetLimit_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AmountHeader.aspx?lnk=update&lvl=zero&typ=Admin");
    }
}
