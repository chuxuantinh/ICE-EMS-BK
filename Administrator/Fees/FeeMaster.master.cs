using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Administrator_Fees_FeeMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    bindfeelevel(); ddlFeeLevel.SelectedValue = Request.QueryString["lvl"];
                    ddlFeeType.SelectedValue = Request.QueryString["typ"];
                    ddlSullabus.SelectedValue = Request.QueryString["id"];
                    ddltype.SelectedValue = Request.QueryString["type"];
                }
                try
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
                        }
                        else if (lvl == 1)
                        {
                            lblWelcome.Text = "Admin";
                        }
                        else if (lvl == 2)
                        {
                            lblWelcome.Text = "User ID";
                        }
                    }
                    if (Request.QueryString["sec"] == "ubs")
                    {
                        panelHeaderSyllabus.Visible = true;
                        adminTableHeader.Visible = false;
                    }
                    else if (Request.QueryString["sec"] == "eef")
                    {
                        panelHeaderSyllabus.Visible = false;
                        adminTableHeader.Visible = true;
                    }
                    reader.Close();
                    reader.Dispose();
                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = ex.ToString();
                }
                finally
                {
                    if (Request.QueryString["typ"] == "Asso")
                    {
                        panelHeaderSyllabus.Visible = false;
                    }
                    else if (Request.QueryString["typ"] == "Tech")
                    {
                        panelHeaderSyllabus.Visible = false;
                    }
                    con.Close();
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
    private void bindfeelevel()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT DISTINCT [FeeLevel] FROM [FeeMaster]  where type='"+ddltype.SelectedValue.ToString()+"'  ORDER BY [FeeLevel] DESC", con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddlFeeLevel.DataSource = ds;
        ddlFeeLevel.DataTextField = "FeeLevel";
        ddlFeeLevel.DataValueField = "FeeLevel";
        ddlFeeLevel.DataBind();

        if (Request.QueryString["typ"].ToString() == "Civil")
        {
            SqlDataAdapter add = new SqlDataAdapter("select distinct CourseID from CivilSubMaster order by CourseID", con);
            DataSet ds1 = new DataSet();
            add.Fill(ds1);
            ddlSullabus.DataSource = ds1;
            ddlSullabus.DataTextField = "CourseID";
            ddlSullabus.DataValueField = "CourseID";
            ddlSullabus.DataBind();
        }
        else if (Request.QueryString["typ"].ToString() == "Archi")
        {
            SqlDataAdapter addd = new SqlDataAdapter("select distinct CourseID from ArchiSubMaster order by CourseID", con);
            DataSet ds1d = new DataSet();
            addd.Fill(ds1d);
            ddlSullabus.DataSource = ds1d;
            ddlSullabus.DataTextField = "CourseID";
            ddlSullabus.DataValueField = "CourseID";
            ddlSullabus.DataBind();
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
            {
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                Response.Redirect("../../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());
    }
    protected void ibtnNewFees_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void ibtnShowFees_Click(object sender, ImageClickEventArgs e)
    {
        if (ddltype.SelectedValue.ToString() == "Home")
        {
            if (Request.QueryString["typ"] == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
            else if (Request.QueryString["typ"] == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
        }
        else if (ddltype.SelectedValue.ToString() == "Overseas")
        {
            if (Request.QueryString["typ"] == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
            else if (Request.QueryString["typ"] == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
        }
    }
    protected void ibtnEditFees_Click(object sender, ImageClickEventArgs e)
    {
        if (ddltype.SelectedValue.ToString() == "Home")
        {
            if (Request.QueryString["typ"] == "Asso")
            {
                Response.Redirect("AssociateFeeEdit.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
            else if (Request.QueryString["typ"] == "Tech")
            {
                Response.Redirect("TechnicianFeeEdit.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
        }
        else if (ddltype.SelectedValue.ToString() == "Overseas")
        {
            if (Request.QueryString["typ"] == "Asso")
            {
                Response.Redirect("AssociateFeeEdit.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
            else if (Request.QueryString["typ"] == "Tech")
            {
                Response.Redirect("TechnicianFeeEdit.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
        }
    }
    protected void ddlSyllabusLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue.ToString() == "Home")
        {
            if (Request.QueryString["typ"].ToString() == "Civil")
            {
                Response.Redirect("CivilSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Civil&type=Home&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
            else if (Request.QueryString["typ"].ToString() == "Archi")
            {
                Response.Redirect("ArchiSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Archi&type=Home&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
            else if (Request.QueryString["typ"].ToString() == "create")
            {
                Response.Redirect("CreateNewSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=create&type=Home&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
            else if (Request.QueryString["typ"].ToString() == "manage")
            {
                Response.Redirect("CreateNewSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=manage&type=Home&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
        }
        else if (ddltype.SelectedValue.ToString() == "Overseas")
        {
            if (Request.QueryString["typ"].ToString() == "Civil")
            {
                Response.Redirect("CivilSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Civil&type=Overseas&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
            else if (Request.QueryString["typ"].ToString() == "Archi")
            {
                Response.Redirect("ArchiSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Archi&type=Overseas&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
            else if (Request.QueryString["typ"].ToString() == "create")
            {
                Response.Redirect("CreateNewSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=create&type=Overseas&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
            else if (Request.QueryString["typ"].ToString() == "manage")
            {
                Response.Redirect("CreateNewSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=manage&type=Overseas&lvl=zero&sec=ubs&id=" + ddlSullabus.SelectedValue.ToString());
            }
        }
    }
    protected void ddlFeeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lvl = Request.QueryString["lvl"].ToString();
        if (lvl == "zero")
        {
        }
        if (ddltype.SelectedValue.ToString() == "Home")
        {
            if (ddlFeeType.SelectedValue.ToString() == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Home&lvl=" + ddlFeeLevel.SelectedValue.ToString());
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Home&lvl=" + ddlFeeLevel.SelectedValue.ToString());
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Ms")
            {
            }
        }
        else if (ddltype.SelectedValue.ToString() == "Overseas")
        {
            if (ddlFeeType.SelectedValue.ToString() == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Overseas&lvl=" + ddlFeeLevel.SelectedValue.ToString());
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Overseas&lvl=" + ddlFeeLevel.SelectedValue.ToString());
            }
        }
    }
    protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue.ToString() == "Home")
        {
            if (ddlFeeType.SelectedValue.ToString() == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Ms")
            {
                Response.Redirect("../MemberFeeMaster.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Fee&&lvl=" + Request.QueryString["lvl"] + "&mst=member&sec=eef");
            }
        }
        else if (ddltype.SelectedValue.ToString() == "Overseas")
        {
            if (ddlFeeType.SelectedValue.ToString() == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Ms")
            {
                Response.Redirect("../MemberFeeMaster.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Fee&&lvl=" + Request.QueryString["lvl"] + "&mst=member&sec=eef");
            }
        }
    }
    protected void ArchiEngineering_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Course/ArchiSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Archi&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void CivilEngineerig_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Course/CivilSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Civil&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void ibtnNewSchedule_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("NewFees.aspx?dev=" + Request.QueryString["dev"] + "&typ="+Request.QueryString["typ"]+"&type="+ddltype.SelectedValue.ToString()+"&lvl="+Request.QueryString["lvl"]);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Course/CivilSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Civil&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Course/ArchiSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Archi&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue.ToString() == "Home")
        {
            if (ddlFeeType.SelectedValue.ToString() == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Home&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Ms")
            {
                Response.Redirect("../MemberFeeMaster.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Fee&&lvl=" + Request.QueryString["lvl"] + "&mst=member&sec=eef");
            }
        }
        else if (ddltype.SelectedValue.ToString() == "Overseas")
        {
            if (ddlFeeType.SelectedValue.ToString() == "Asso")
            {
                Response.Redirect("AssociateFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Asso&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Tech")
            {
                Response.Redirect("TechnicianFeesShow.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Overseas&lvl=" + Request.QueryString["lvl"]);
            }
            else if (ddlFeeType.SelectedValue.ToString() == "Ms")
            {
                Response.Redirect("../MemberFeeMaster.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Fee&&lvl=" + Request.QueryString["lvl"] + "&mst=member&sec=eef");
            }
        }
    }
    protected void lbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Admin/changePassword.aspx?lnk=update&lvl=" + Request.QueryString["lvl"] + "&typ=Admin&name=" + Request.QueryString["dev"]);
    }
    protected void lnkCreateFeesHeader_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateFeesHead.aspx?dev=" + Request.QueryString["dev"] + "&typ=Tech&type=Overseas&lvl=" + Request.QueryString["lvl"]);
    }
}
