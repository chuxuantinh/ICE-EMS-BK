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

public partial class Exam_ExamSchedule : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try{
        if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                btnSave.Enabled = false;
                txtPSetterCode.Visible = false;
                txtPSetterName.Visible = false;
                gettime(); lblStreamHidden.Text = "Tech"; lblStreamName.Text = "Technician Engineering";
                txtyear.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
              
                btnDeleteRow.Visible = false;
                string qry = "";
                ddlCourse.SelectedValue = "Civil";
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            panelAsso.Visible = false;
            panelTech.Visible = true;
            qry = "select Distinct CourseID from CivilSubMaster";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            panelAsso.Visible = true;
            panelTech.Visible = false;
            qry = "select Distinct CourseID from ArchiSubMaster";
        
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddlSyllabus.DataSource = ds;
        ddlSyllabus.DataTextField = "CourseID";
        ddlSyllabus.DataValueField = "CourseID";
        ddlSyllabus.DataBind();
        FeeMaster fm = new FeeMaster();
        ddlSyllabus.SelectedValue = fm.currentCourse(ddlCourse.SelectedValue.ToString());
                ddlType.Focus();
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
    protected bool DateStatus()
    {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            for (int i = 0; i <= GridView3.Rows.Count - 1; i++)
            {
                if (GridView3.Rows[i].Cells[0].Text.ToString() != lblSCode.Text)
                {
                    if (Convert.ToDateTime(GridView3.Rows[i].Cells[2].Text, dtinfo) == Convert.ToDateTime(txtDate.Text, dtinfo))
                    {
                        if (GridView3.Rows[i].Cells[3].Text.ToString() == ddlShift.SelectedItem.Text.ToString())
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try{
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
    protected void ddlType_OnselectedIndexChanged(object sender, EventArgs e)
    {
        ddlSyllabus.Focus();
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void txtyear_TextChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
        ddlCourse.Focus();
    }
    protected void ddlSyllabus_OnslelectdIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
        GridView2.DataBind();
        GridView3.DataBind();
        ddlCourse.Focus();
    }
    protected void ddlCourse_OnTextChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            qry = "select Distinct CourseID from CivilSubMaster";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            qry = "select Distinct CourseID from ArchiSubMaster";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddlSyllabus.DataSource = ds;
        ddlSyllabus.DataTextField = "CourseID";
        ddlSyllabus.DataValueField = "CourseID";
        ddlSyllabus.DataBind();
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            panelAsso.Visible = false;
            panelTech.Visible = true;
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            panelAsso.Visible = true;
            panelTech.Visible = false;
        }
        ddlPart.Focus();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString()=="PartII")
        {
            lblStreamName.Text = "Technician Engineering";
            lblStreamHidden.Text = "Tech";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
        {
            lblStreamHidden.Text = "Asso";
            lblStreamName.Text = "Associate Engineering";
        }
        ddlPart.Focus();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row;
        row = GridView1.SelectedRow;
        lblSCode.Text = row.Cells[1].Text;
        lblSName.Text = row.Cells[2].Text;
        btnSave.Enabled = true;
        lblRowDeleted.Text = "";
        txtDate.Focus();
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblException.Text = "";
        GridViewRow row;
        row = GridView2.SelectedRow;
        lblSCode.Text = row.Cells[1].Text;
        lblSName.Text = row.Cells[2].Text;
        btnSave.Enabled = true;
        lblRowDeleted.Text = "";
        txtDate.Focus();
    }
    private void gettime()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(MapPath("~/Exam/Time.xml"));
        DataView dv = ds.Tables["Min"].DefaultView;
        dv.Sort = "ID";
        ddlMin.DataTextField = "Value";
        ddlMin.DataValueField = "Value";
        ddlMin.DataSource = dv;
        ddlMin.DataBind();
        ddlDuMin.DataTextField = "Value";
        ddlDuMin.DataValueField = "Value";
        ddlDuMin.DataSource = dv;
        ddlDuMin.DataBind();
        ddlMin.SelectedValue = "00";
        ddlDuMin.SelectedValue = "00";
        DataSet ds2 = new DataSet();
        ds2.ReadXml(MapPath("~/Exam/HTime.xml"));
        DataView dv2 = ds2.Tables["hr"].DefaultView;
        dv2.Sort = "ID";
        ddlHr.DataTextField = "Value";
        ddlHr.DataValueField = "Value";
        ddlHr.DataSource = dv2;
        ddlHr.DataBind();
        ddlHr.SelectedValue = "10";
        ddlDuHr.DataTextField = "Value";
        ddlDuHr.DataValueField = "Value";
        ddlDuHr.DataSource = dv2;
        ddlDuHr.DataBind();
        ddlDuHr.SelectedValue = "03";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != "")
            {
                dtinfo.DateSeparator = "/";
                dtinfo.ShortDatePattern = "dd/MM/yyyy";
                if (DateStatus())
                {
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Schedule Already Exist')", true);
                    btnSave.Focus();
                }
                    con.Close(); con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ExamDate(SCode,SName,Season,Stream,Course,Part,Date,Time,PSCode,PSName,Shift,Duration,Type,CourseID) values(@SCode,@SName,@Season,@Stream,@Course,@Part,@Date,@Time,@PSCode,@PSName,@Shift,@Duration,@Type,@CourseID)", con);
                    cmd.Parameters.AddWithValue("@SCode", lblSCode.Text.ToString());
                    cmd.Parameters.AddWithValue("@SName", lblSName.Text.ToString());
                    cmd.Parameters.AddWithValue("@Season", ddlExamSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString());
                    cmd.Parameters.AddWithValue("@Stream", lblStreamHidden.Text.ToString());
                    cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@Time", ddlHr.SelectedValue.ToString() + ":" + ddlMin.SelectedValue.ToString() + " " + ddlmeridian.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@PSCode", txtPSetterCode.Text.ToString());
                    cmd.Parameters.AddWithValue("@PSName", txtPSetterName.Text.ToString());
                    cmd.Parameters.AddWithValue("@Shift", ddlShift.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Duration", ddlDuHr.SelectedValue.ToString() + " Hr. " + ddlDuMin.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CourseID", ddlSyllabus.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    lblException.Text = "[" + lblSCode.Text.ToString() + " ]" + lblSName.Text.ToString() + " Schedule Saved";
                    btnSave.Enabled = false;
               
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Insert Valid Date')", true);
            }
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
            catch (FormatException ex)
        {
            lblException.Text = "Invalid Date";
        }

        finally
        {
            con.Close();
            con.Dispose();
            GridView3.DataBind();
            btnCancel.Focus();
        }
    }
    protected void btnCencel_click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        GridView3.DeleteRow(GridView3.SelectedIndex);
        GridView3.Focus();
    }
    protected void GridView3_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void GridView3_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception == null)
        {
            lblRowDeleted.Text = "Record Deleted Successfully.";
            btnDeleteRow.Visible = false;
        }
        else
        {
            lblRowDeleted.Text = "Please select the Record for delete.";
            e.ExceptionHandled = true;
        }
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;
        rw = GridView3.SelectedRow;
        lblRowDeleted.Text = "Delete Subject Code: " + GridView3.SelectedRow.Cells[0].Text + " Subject Name: " + GridView3.SelectedRow.Cells[1].Text;
        btnDeleteRow.Visible = true;
        GridView3.Focus();
    }
    protected void txtPSetterCode_Techchanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select Name from PaperSetter where Code='" + txtPSetterCode.Text.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        con.Close(); con.Dispose();
        if (chk != "")
        {
            txtPSetterName.Text = chk.ToString();
        }
        else if (chk == "")
        {
            txtPSetterCode.Text = "Invalid PaperSetter Code";
            txtPSetterName.Text = "";
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtyear.Text.ToString();
        txtyear.Focus();
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
         lblException.Text = "";
    }
}

   

