using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Administrator_Course_CreateNewSyllabus : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            if (Request.QueryString["typ"].ToString() == "create")
            {
                panelManage.Visible = false;
                panelCreate.Visible = true;
            }
            else if (Request.QueryString["typ"].ToString() == "manage")
            {
                panelCreate.Visible = false;
                panelManage.Visible = true;
            }

            if (Page.IsPostBack == false)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                lbntSelectAging.Visible = false; PnlSubjectEdit.Visible = false;
                PanelCheckBox.Visible = false;// panelManage.Visible = true;
                FeeMaster fm = new FeeMaster();
                lblNewid.Text = fm.rtnlvl(lblSessionHiddend.Text);
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    private void bindddlsys()
    {
        if (rbtnCivil.Checked==true)
        {
            SqlDataAdapter add = new SqlDataAdapter("select distinct CourseID from CivilSubMaster order by CourseID", con);
            DataSet ds1 = new DataSet();
            add.Fill(ds1);
            ddlsylbsmamage.DataSource = ds1;
            ddlsylbsmamage.DataTextField = "CourseID";
            ddlsylbsmamage.DataValueField = "CourseID";
            ddlsylbsmamage.DataBind();
        }
        else if (rbtnArchi.Checked == true)
        {
            SqlDataAdapter addd = new SqlDataAdapter("select distinct CourseID from ArchiSubMaster order by CourseID", con);
            DataSet ds1d = new DataSet();
            addd.Fill(ds1d);
            ddlsylbsmamage.DataSource = ds1d;
            ddlsylbsmamage.DataTextField = "CourseID";
            ddlsylbsmamage.DataValueField = "CourseID";
            ddlsylbsmamage.DataBind();
        }
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        FeeMaster fm = new FeeMaster();
        lblNewid.Text = fm.rtnlvl(lblSessionHiddend.Text);
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        FeeMaster fm = new FeeMaster();
        lblNewid.Text = fm.rtnlvl(lblSessionHiddend.Text);
        txtSession.Focus();
    }
    protected void lbtnCivill_Click(object sender, EventArgs e)
    {
        Response.Redirect("CivilSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Civil&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnArchitectural_Click(object sender, EventArgs e)
    {
        Response.Redirect("ArchiSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=Archi&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnCteateNewSyllabus_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateNewSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=create&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbtnManageSyllabus_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateNewSyllabus.aspx?dev=" + Request.QueryString["dev"] + "&typ=manage&lvl=zero&sec=ubs&id=" + Request.QueryString["id"].ToString());
    }
    protected void lbntSelectAging_Click(object sender, EventArgs e)
    {
        panelSelect.Visible = true;
        PanelCheckBox.Visible = false; PnlSubjectEdit.Visible = false; panelManage.Visible = false;
        lblLevelInfoTitle.Text = ""; rbtnArchi.Checked = false; rbtnCivil.Checked = false;
    }
    protected void rbtnArchi_CheckedChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["typ"].ToString() == "manage")
        {
            panelSelect.Visible = false; lblExceptionID.Text = ""; panelManage.Visible = true;
            PanelCheckBox.Visible = true;  lbntSelectAging.Visible = true;
            lblLevelInfoTitle.Text = "Architectural Engineering Syllabus"; bindddlsys();
        }
        if (Request.QueryString["typ"].ToString() == "create")
        {
            panelSelect.Visible = false; lblExceptionID.Text = "";
            PanelCheckBox.Visible = true; PnlSubjectEdit.Visible = true; lbntSelectAging.Visible = true;
            lblLevelInfoTitle.Text = "Architectural Engineering Syllabus"; //bindddlsys();
        }
    }
    protected void rbtnCivil_CheckedChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["typ"].ToString() == "manage")
        {
            panelSelect.Visible = false; panelManage.Visible = true; lbntSelectAging.Visible = true;
            PanelCheckBox.Visible = true; lblExceptionID.Text = "";
            lblLevelInfoTitle.Text = "Civil Engineering Syllabus"; bindddlsys();
        }
        else if (Request.QueryString["typ"].ToString() == "create")
        {
            panelSelect.Visible = false; PnlSubjectEdit.Visible = true; lbntSelectAging.Visible = true;
            PanelCheckBox.Visible = true; lblExceptionID.Text = "";
            lblLevelInfoTitle.Text = "Civil Engineering Syllabus";
        }
    }
    protected void ibtnClose_Click(object sender, ImageClickEventArgs e)
    {
        panelSelect.Visible = true; lblExceptionID.Text = "";
        PanelCheckBox.Visible = false; PnlSubjectEdit.Visible = false;
        lblLevelInfoTitle.Text = ""; rbtnArchi.Checked = false; rbtnCivil.Checked = false;
    }
    protected void btnCreateNew_Onclick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        string query = "";
        if (rbtnCivil.Checked == true)
            query = "select CourseID from CivilSubMaster where CourseID='" + lblNewid.Text.ToString() + "'";
        else if (rbtnArchi.Checked == true)
            query = "select CourseID from ArchiSubMaster where CourseID='" + lblNewid.Text.ToString() + "'";
        cmd = new SqlCommand(query, con);
        string have = Convert.ToString(cmd.ExecuteScalar());
        if (have == lblNewid.Text.ToString())
        {
            lblExceptionID.Text = "Already Created Syllabus for selected Session, To make Change Update Syllabus.";
            btnCreateNew.Text = "Generate New Syllabus";
        }
        else
        {
            string qry = "";
            if (rbtnCivil.Checked == true)
                qry = "insert into CivilSubMaster (SubID,SubName,Lvl,Section,SubjectType,CourseID) values(@SubID,@SubName,@Lvl,@Section,@SubjectType,@CourseID)";
            else if (rbtnArchi.Checked == true)
                qry = "insert into ArchiSubMaster (SubID,SubName,Lvl,Section,SubjectType,CourseID) values(@SubID,@SubName,@Lvl,@Section,@SubjectType,@CourseID)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@SubID", "NA");
            cmd.Parameters.AddWithValue("@SubName", "NA");
            cmd.Parameters.AddWithValue("@Lvl", 1);
            cmd.Parameters.AddWithValue("@Section", "NA");
            cmd.Parameters.AddWithValue("@SubjectType", "NA");
            cmd.Parameters.AddWithValue("@CourseID", lblNewid.Text.ToString());
            cmd.ExecuteNonQuery();
            btnCreateNew.Text = "Successfully Generated";
            lblExceptionID.Text = "";
        }
        con.Close();
        con.Dispose();
    }
    protected void ddlsylabmanage_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close();  con.Open();
        string qry = "";
        if (rbtnCivil.Checked == true)
            qry = "select Status from CivilSubMaster where CourseID='" + ddlsylbsmamage.SelectedValue.ToString() + "'";
        else if (rbtnArchi.Checked == true)
            qry = "select Status from ArchiSubMaster where CourseID='" + ddlsylbsmamage.SelectedValue.ToString() + "'";
       SqlCommand cmd = new SqlCommand(qry, con);
       string stats = Convert.ToString(cmd.ExecuteScalar());
       lblActivetext.Text = "This Syllabus is " + stats.ToString() + ", To Make Change Click Big Button.";
       btnActive.Text = "Active";
       con.Close();
       con.Dispose();
    }
    protected void btnActive_Onclick(object sender, EventArgs e)
    {
        con.Close();  con.Open();
        SqlCommand cmd = new SqlCommand();
        string query="", qry="";
        if (rbtnCivil.Checked == true)
        {
            qry = "update CivilSubMaster set Status='DisActive'";
            query = "update CivilSubMaster set Status='Active' where CourseID='" + ddlsylbsmamage.SelectedValue.ToString() + "'";
        }
        else if (rbtnArchi.Checked == true)
        {
            qry = "update ArchiSubMaster set Status='DisActive'";
            query = "update ArchiSubMaster set Status='Active' where CourseID='" + ddlsylbsmamage.SelectedValue.ToString() + "'";
        }
        cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand(query, con);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        lblActivetext.Text = "Level: " + ddlsylbsmamage.SelectedValue.ToString() + " Activeted for Current Session.";
    }
}
