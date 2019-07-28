using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public partial class Administrator_Course_CivilSyllabus : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con=new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            PnlSecA.Visible = false; PnlSecB.Visible = false;
            panelselectLevel.Visible = true;
            panelDiplomaSelect.Visible = false;
            panelDegreeSelect.Visible = false;
            PanelCheckBox.Visible = false;
            PnlSubjectEdit.Visible = false;
            panelShowExpEdit.Visible = false; btnUpdate.Visible = false; btnDelete.Visible = false;
            chkSubjectType.Text = "Extra Subject";
            if (Page.IsPostBack == false)
            {
                PnlPartI.Visible = false;
                PnlPartII.Visible = false; PnlSecA.Visible = false; PnlSecB.Visible = false;
                panelselectLevel.Visible = true;
                panelDiplomaSelect.Visible = false;
                panelDegreeSelect.Visible = false;
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
    protected void chkSubjecttype_CheckChaged(object sender, EventArgs e)
    {
        if (chkSubjectType.Checked == true)
        {
            chkSubjectType.Text = "Extra Subject";
        }
        else if (chkSubjectType.Checked == false)
        {
            chkSubjectType.Text = "Extra Subject";
        }
        btnUpdate.Visible = true; btnDelete.Visible = true;
        chkfn();
        PnlSubjectEdit.Visible = true;
        txtMaxMark.Focus();
    }
    protected void rbtnDiploma_CheckedChanged(object sender, EventArgs e)
    {
        panelDiplomaSelect.Visible = true;
        panelselectLevel.Visible = false;
        lblSelectedLevel.Text = "Diploma Level Syllabus";
    }
    protected void rbtnDegree_CheckedChanged(object sender, EventArgs e)
    {
        panelDegreeSelect.Visible = true;
        panelselectLevel.Visible = false;
        lblSelectedlevelDeg.Text = "Degree Level Syllabus";
    }
    protected void lbtnBackDegree_Click(object sender, EventArgs e)
    {
        panelDegreeSelect.Visible = false;
        panelselectLevel.Visible = true;
        lblSelectedlevelDeg.Text = "Degree Level Syllabus";
    }
    protected void lbtnBackDiploma_Click(object sender, EventArgs e)
    {
        panelDiplomaSelect.Visible = false;
        panelselectLevel.Visible = true;
        lblSelectedLevel.Text = "Diploma Level Syllabus";
    }
    protected void rbtnPart1_CheckedChanged(object sender, EventArgs e)
    {
        PnlPartI.Visible = true;
        PanelCheckBox.Visible = true;
        panelSelect.Visible = false;
        chkPart1.Checked = true;
    }
    protected void rbtnPart2_CheckedChanged(object sender, EventArgs e)
    {
        PnlPartII.Visible = true;
        PanelCheckBox.Visible = true; panelSelect.Visible = false;
        chkPartII.Checked = true;
    }
    protected void rbtnSecB_CheckedChanged(object sender, EventArgs e)
    {
        PnlSecB.Visible = true; PanelCheckBox.Visible = true;
        panelSelect.Visible = false;
        chkSecB.Checked = true;
    }
    protected void rbtnSecA_CheckedChanged(object sender, EventArgs e)
    {
        PnlSecA.Visible = true; PanelCheckBox.Visible = true;
        panelSelect.Visible = false;
        chkSecA.Checked = true;
    }
    protected void chkPart1_CheckedChanged(object sender, EventArgs e)
    {
        PnlPartI.Visible = true; PanelCheckBox.Visible = true;
        chkfn();
    }
    protected void chkPartII_CheckedChanged(object sender, EventArgs e)
    {
        PnlPartII.Visible = true; PanelCheckBox.Visible = true;
        chkfn();
    }
    protected void chkSecB_CheckedChanged(object sender, EventArgs e)
    {
        PnlSecB.Visible = true; PanelCheckBox.Visible = true;
        chkfn();
    }
    protected void chkSecA_CheckedChanged(object sender, EventArgs e)
    {
        PnlSecA.Visible = true; PanelCheckBox.Visible = true;
        chkfn();
    }
    public void chkfn()
    {
        PanelCheckBox.Visible = true;
        if (chkPart1.Checked == true)
            PnlPartI.Visible = true;
        if (chkPartII.Checked == true) PnlPartII.Visible = true;
        if (chkSecA.Checked == true) PnlSecA.Visible = true;
        if (chkSecB.Checked == true) PnlSecB.Visible = true;
        if (chkPart1.Checked == false) PnlPartI.Visible = false;
        if (chkPartII.Checked == false) PnlPartII.Visible = false;
        if (chkSecA.Checked == false) PnlSecA.Visible = false;
        if (chkSecB.Checked == false) PnlSecB.Visible = false;
        if (PnlSubjectEdit.Visible == true) PnlSubjectEdit.Visible = true;
        if (lblSecType.Text == "PartI" | lblSecType.Text == "SectionA")
        {
            chkSubjectType.Visible = false;
        }
        else
        {
            chkSubjectType.Visible = true;
        }
    }
    protected void lbntSelectAging_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url);
    }
    protected void lbtnPartIEdit_Click(object sender, EventArgs e)
    {
        PnlSubjectEdit.Visible = true;
        lblSubType.Text = "Part I";
        chkfn();
        lblSecType.Text = "PartI";
        getSubNo();
        chkSubjectType.Visible = false;
    }
    protected void lbtnPartIIEdit_Click(object sender, EventArgs e)
    {
        PnlSubjectEdit.Visible = true; lblSubType.Text = "Part II";
        chkfn();  lblSecType.Text = "PartII";
        chkSubjectType.Visible = true; getSubNo();
    }
    protected void lbtnSecAEdit_Click(object sender, EventArgs e)
    {
        PnlSubjectEdit.Visible = true;  lblSubType.Text = "Section A";
        chkfn();   lblSecType.Text = "SectionA";  chkSubjectType.Visible = false;
        getSubNo();
    }
    protected void lbtnSecBEdit_Click(object sender, EventArgs e)
    {
        PnlSubjectEdit.Visible = true;
        lblSubType.Text = "Section B";
        chkfn();
        lblSecType.Text = "SectionB";
        chkSubjectType.Visible = true;
        getSubNo();
    }
    protected void ibtnClose_Click(object sender, ImageClickEventArgs e)
    {
        chkfn();
        lblActiveId.Text = ""; txtSubID.Text = ""; txtSubName.Text = ""; chkSubjectType.Checked = false; txtMaxMark.Text = ""; txtMinMark.Text = ""; txtFirst.Text = "";
        PnlSubjectEdit.Visible = false;
    }
    protected void btnAddMoreExp_Click(object sender, EventArgs e)
    {
        chkfn();
        PnlSubjectEdit.Visible = true;
        btnUpdate.Visible = false;
        btnSave.Enabled = true;
        PanelInsertExperience.Visible = true;
        txtSubID.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        chkfn();
        PnlSubjectEdit.Visible = true;
        panelShowExpEdit.Visible = true;
        try
        {
            con.Close(); 
            con.Open();
            int i = Convert.ToInt32(lblSubNoEdit.Text) + 1;
            lblSubNoEdit.Text = i.ToString();
            SqlCommand cmd = new SqlCommand("insert into CivilSubMaster (SubID,SubName,MaxMark,MinMark,First,Lvl,Section,SubjectType,CourseID) values(@SubID,@SubName,@MaxMark,@MinMark,@First,@Lvl,@Section,@SubjectType,@CourseID)", con);
            cmd.Parameters.AddWithValue("@SubID", txtSubID.Text.ToString());
            cmd.Parameters.AddWithValue("@SubName", txtSubName.Text.ToString());
            cmd.Parameters.AddWithValue("@MaxMark", txtMaxMark.Text.ToString());
            cmd.Parameters.AddWithValue("@MinMark", txtMinMark.Text.ToString());
            cmd.Parameters.AddWithValue("@First", txtFirst.Text.ToString());
            cmd.Parameters.AddWithValue("@Lvl", lblSubNoEdit.Text.ToString());
            cmd.Parameters.AddWithValue("@Section", lblSecType.Text.ToString());
            if (chkSubjectType.Checked == false)
                cmd.Parameters.AddWithValue("@SubjectType", "Regular");
            else if (chkSubjectType.Checked == true)
            {
                cmd.Parameters.AddWithValue("@SubjectType", "Extra");
            }
            cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["id"].ToString());
            cmd.ExecuteNonQuery();
            panelShowExpEdit.Visible = true;
            lblSubID.Text = txtSubID.Text.ToString();
            lblSubName.Text = txtSubName.Text.ToString();
            lblMaxMark.Text = txtMaxMark.Text.ToString();
            lblMinMark.Text = txtMinMark.Text.ToString();
            lblFirst.Text = txtFirst.Text.ToString();
            lblSaveException.Text = "Saved";
        }
        catch (SqlException ex)
        {
            lblSaveException.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
            btnSave.Enabled = false;
            clear();
        }
    }
    public void clear()
    {
        txtSubID.Text = "";
        txtSubName.Text = ""; txtMaxMark.Text = "";
        txtMinMark.Text = ""; txtFirst.Text = "";
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            con.Close();  con.Open();
            SqlCommand cmd = new SqlCommand("select * from CivilSubMaster where SubName='" + ddlSubject.SelectedValue.ToString() + "' and CourseID='" + Request.QueryString["id"].ToString() + "'", con);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txtSubID.Text = rd[1].ToString();
                txtSubName.Text = rd[2].ToString();
                txtMaxMark.Text = rd[3].ToString();
                txtMinMark.Text = rd[4].ToString();
                txtFirst.Text = rd[5].ToString();
                lblSubNoEdit.Text = rd[6].ToString();
                string extra = rd[9].ToString();
                if (extra == "Extra") chkSubjectType.Checked = true;
                else if (extra == "Regular") chkSubjectType.Checked = false;
            }
            rd.Close();
            rd.Dispose();
            btnUpdate.Visible = true; btnDelete.Visible = true;
        }
        catch (SqlException ex)
        {
            lblSaveException.Text = ex.ToString();
        }
        finally
        {
            con.Close(); con.Dispose();
            chkfn();
            PnlSubjectEdit.Visible = true;
            txtSubID.Focus();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update CivilSubMaster set SubID=@SubID,SubName=@SubName,MaxMark=@MaxMark,MinMark=@MinMark,First=@First,Lvl=@Lvl,Section=@Section,Date=@Date,SubjectType=@SubjectType where SubName='" + ddlSubject.SelectedValue.ToString() + "' and CourseID='" + Request.QueryString["id"].ToString() + "'", con);
            cmd.Parameters.AddWithValue("@SubID", txtSubID.Text.ToString());
            cmd.Parameters.AddWithValue("@SubName", txtSubName.Text.ToString());
            cmd.Parameters.AddWithValue("@MaxMark", txtMaxMark.Text.ToString());
            cmd.Parameters.AddWithValue("@MinMark", txtMinMark.Text.ToString());
            cmd.Parameters.AddWithValue("@First", txtFirst.Text.ToString());
            cmd.Parameters.AddWithValue("@Lvl", lblSubNoEdit.Text.ToString());
            cmd.Parameters.AddWithValue("@Section", lblSecType.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"),dtinfo));
            if (chkSubjectType.Checked == false)
                cmd.Parameters.AddWithValue("@SubjectType", "Regular");
            else if (chkSubjectType.Checked == true)
            {
                cmd.Parameters.AddWithValue("@SubjectType", "Extra");
            }
            cmd.ExecuteNonQuery();
            lblSaveException.Text = txtSubID.Text.ToString() + " updated";
        }
        catch (SqlException ex)
        {
            lblSaveException.Text = ex.ToString();
        }
        finally
        {
            con.Close();  con.Dispose();
            chkfn(); btnSave.Visible = true;
            PnlSubjectEdit.Visible = true; PanelInsertExperience.Visible = true;
            PanelCheckBox.Visible = true;
            clear();
        }
    }
    public void getSubNo()
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from CivilSubMaster where Section='" + lblSecType.Text.ToString() + "' and CourseID='" + Request.QueryString["id"].ToString() + "'", con);
            SqlDataReader rd;
            rd = cmd1.ExecuteReader();
            int i = 0;
            if (rd.Read())
            {
                i = i + 1;
            }
            rd.Close();
            rd.Dispose();
            if (i == 0)
            {
                DateTime date = Convert.ToDateTime(DateTime.Now.ToShortDateString(), dtinfo);
                SqlCommand cmd = new SqlCommand("insert into CivilSubMaster (Lvl,Section,CourseID) values(@Lvl,@Section,@CourseID)", con);
                int lvl = 0;
                lblSubNoEdit.Text = lvl.ToString();
                cmd.Parameters.AddWithValue("@Lvl", lblSubNoEdit.Text);
                cmd.Parameters.AddWithValue("@Section", lblSecType.Text.ToString());
                cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["id"].ToString());
                cmd.ExecuteNonQuery();
            }
            SqlCommand cmd3 = new SqlCommand("select max(Lvl) from CivilSubMaster where Section='" + lblSecType.Text.ToString() + "' and CourseID='" + Request.QueryString["id"].ToString() + "'", con);
            SqlDataReader rd2 = cmd3.ExecuteReader();
            if (rd2.Read())
            {
                lblSubNoEdit.Text = rd2[0].ToString();
            }

            rd2.Close();
            rd2.Dispose();

            PnlSubjectEdit.Visible = true;
            PanelInsertExperience.Visible = true;
        }
        catch (SqlException ex)
        {
            lbltemp.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void txtSubID_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select SubID from CivilSubMaster where SubID='" + txtSubID.Text.ToString() + "' and CourseID='" + Request.QueryString["id"].ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        if (chk == txtSubID.Text.ToString())
        {
            lblActiveId.Text = "Subject ID already exists.";
            lblActiveId.ForeColor = System.Drawing.Color.Maroon;
            btnSave.Enabled = false;
            txtSubID.Focus();
            btnUpdate.Visible = true;
        }
        else
        {
            lblActiveId.Text = "Subject ID: " + txtSubID.Text + " is available.";
            lblActiveId.ForeColor = System.Drawing.Color.Green;
            btnSave.Enabled = true;
            txtSubName.Focus();
            btnSave.Visible = true;
        }
        con.Close(); con.Dispose();
        chkfn();
        PnlSubjectEdit.Visible = true; PanelInsertExperience.Visible = true;
        PanelCheckBox.Visible = true;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("delete CivilSubMaster where SubName='" + ddlSubject.SelectedValue.ToString() + "' and CourseID='" + Request.QueryString["id"].ToString() + "'", con);
        cmd.ExecuteNonQuery();
        lblSaveException.Text = ddlSubject.SelectedValue.ToString() + " Record Deleted";
        con.Close();
        con.Dispose();
        chkfn();
        PnlSubjectEdit.Visible = true; PanelInsertExperience.Visible = true;
        PanelCheckBox.Visible = true; btnDelete.Visible = true; btnUpdate.Visible = true;
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
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        chkfn();
    }
}
