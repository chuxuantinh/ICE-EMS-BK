using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Exam_UFMUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ddlExamSeason.Focus();
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                panelView.Visible = false;
                rbtnRollNo.Checked = true;
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
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try{
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
      protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtRollNo.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    string qury;
    protected void btnOK_OnClcick(object sender, EventArgs e)
    {
    try
    {
        GridUFM.DataSource = gridsouce();
        GridUFM.DataBind();
        panelView.Visible = true;
        con.Close(); con.Open();
        if (rbtnRollNo.Checked == true)
        {
            qury = "select * from ExamUFM where RollNo='" + txtRollNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        else if (rbtnSID.Checked == true)
        {
            qury = "select * from ExamUFM where SID='" + txtRollNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        else
        {
            qury = "select  * from ExamUFM where  Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        SqlCommand cmd1 = new SqlCommand(qury, con);
        SqlDataReader reader1;
        reader1 = cmd1.ExecuteReader();
        while (reader1.Read())
        {
            lblExamDate.Text = reader1["ExamDate"].ToString();
            lblShift.Text = reader1["Shift"].ToString();
            lblCenterCode.Text = reader1["CenterCode"].ToString();
            lblCenterName.Text = reader1["CenterName"].ToString();
            lblRollNo.Text = reader1["RollNo"].ToString();
            lblCourse.Text = reader1["Course"].ToString(); lblPart.Text = reader1["Part"].ToString();
            lblSID.Text = reader1["SID"].ToString();
            lblSubID.Text = reader1["SubID"].ToString(); lblSubName.Text = reader1["SubName"].ToString();
            lblStatus.Text = reader1["Status"].ToString();
           
        }
        reader1.Close();
        reader1.Dispose();
    }
    catch(SqlException ex)
    {
        lblExceptionOK.Text = ex.ToString();
    }
    finally
    {
        con.Close();
        con.Dispose();
    }
    }
    private DataSet gridsouce()
    {
        if (rbtnRollNo.Checked == true)
        {
            qury = "select RollNo,SID,SubID,SubName,ExamDate,Shift,Status from ExamUFM where RollNo='" + txtRollNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        else if (rbtnSID.Checked == true)
        {
            qury = "select  RollNo,SID,SubID,SubName,ExamDate,Shift,Status from ExamUFM where SID='" + txtRollNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        else
        {
            qury = "select  RollNo,SID,SubID,SubName,ExamDate,Shift,Status from ExamUFM where  Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        if(txtRollNo.Text=="")
        {
            qury = "select  RollNo,SID,SubID,SubName,ExamDate,Shift,Status from ExamUFM where  Session='" + lblHiddenSeason.Text.ToString() + "'";
        }
        panelView.Visible = true;
        SqlDataAdapter ad = new SqlDataAdapter(qury, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        return ds;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;
        rw = GridUFM.SelectedRow;
        panelView.Visible = true;
        lblSubID.Text = rw.Cells[3].Text.ToString();
        lblSubName.Text = rw.Cells[4].Text.ToString();
        lblExamDate.Text = rw.Cells[5].Text.ToString();
        lblShift.Text = rw.Cells[6].Text.ToString();
        lblSID.Text = rw.Cells[2].Text.ToString();
        lblRollNo.Text = rw.Cells[1].Text.ToString();
        string strStatus = rw.Cells[7].Text.ToString();
        if (strStatus.ToString().ToLower() == "unfair")
        {
            btnunfair.Visible = false; btnfair.Visible = true;
        }
        else if (strStatus.ToString().ToLower() == "fair")
        {
            btnunfair.Visible = true; btnfair.Visible = false;
        }
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select * from ExamUFM where RollNo='" + lblRollNo.Text.ToString() + "' and SubID='" + lblSubID.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblCourse.Text = reader["Course"].ToString();
            lblPart.Text = reader["Part"].ToString();
            lblCenterCode.Text = reader["CenterCode"].ToString();
            lblCenterName.Text = reader["CenterName"].ToString();
        }
        reader.Close();
        reader.Dispose();
        con.Close();
        con.Dispose();
    }
    protected void btnunfair_OnClick(object sender, EventArgs e)
    {
        lblStatus.Text = "unfair";
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update ExamUFM set Status='unfair' where RollNo='" + lblRollNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "' and SubID='" + lblSubID.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            GridUFM.DataSource = gridsouce();
            GridUFM.DataBind();
        }
        catch (SqlException ex)
        {
            lblExceptionOK.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
       
    }
    protected void btnViewAll_OnClcick(object sender, EventArgs e)
    {
        panelView.Visible = false;
         qury = "select RollNo,SID,SubID,SubName,ExamDate,Shift,Status from ExamUFM where  Session='" + lblHiddenSeason.Text.ToString() + "'";
         SqlDataAdapter ad = new SqlDataAdapter(qury, con);
         DataSet ds = new DataSet();
         ad.Fill(ds);
         GridUFM.DataSource = ds;
         GridUFM.DataBind();
    }
    protected void btnfair_OnClick(object sender, EventArgs e)
    {
        lblStatus.Text = "fair";
       
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update ExamUFM set Status='fair' where RollNo='" + lblRollNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "' and SubID='" + lblSubID.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            GridUFM.DataSource = gridsouce();
            GridUFM.DataBind();
        }
        catch (SqlException ex)
        {
            lblExceptionOK.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
       
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void GridUFM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridUFM.PageIndex = e.NewPageIndex;
        GridUFM.DataBind();
    }
}