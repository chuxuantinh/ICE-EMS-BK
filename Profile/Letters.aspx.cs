using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Globalization;

public partial class Profile_Letters : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            con.Open();
            cmd = new SqlCommand("select Designation from Login where LogName='" + Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString() + "' and Password='" + Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString() + "'", con);
            string desig = cmd.ExecuteScalar().ToString();
            lblName.Text = Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString();
            lblDesignation.Text = desig.ToString();
            devdage.Enabled = false; pnlMain.Visible = false; lblTitle.Text = "Unread Letters"; pnlSelect.Visible = false;
            bindUnread();
            con.Close();
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]));
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
    protected void grdLetters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "&nbsp;"  | e.Row.Cells[3].Text == "" )
            {
                e.Row.Cells[3].Text = "";
            }
            else e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
            if ( e.Row.Cells[4].Text == "&nbsp;" | e.Row.Cells[4].Text == "")
            {
                e.Row.Cells[4].Text = "";
            }
           
            else 
            e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MM/yyyy");
        }
        lblTotal.Text = grdLetters.Rows.Count.ToString();
    }
    private void bindUnread()
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlDataAdapter adp = new SqlDataAdapter(" SELECT [LetterFrom], [Subject],[ReceiveDate], [DispatchDate], [DiaryNo] FROM [DiaryLetter] WHERE Designation= '" + lblDesignation.Text + "' and (Status='NotOpen') order by SN Desc", con);
        DataSet ds = new DataSet();
        adp.Fill(ds); 
        grdLetters.DataSource = ds;
        grdLetters.DataBind();
    }
    protected void grdLetters_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSelect.Visible = true;
       
    }
    protected void btnDispatch_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update DiaryLetter set Details='" + txtRemarks.Text + "' , Status='ReadyToDispatch',DispatchDate='"+DateTime.Now+"' where DiaryNo='" + grdLetters.SelectedRow.Cells[5].Text + "'", con);
        cmd.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Dispatched')", true);
        con.Close();
        con.Dispose();
    }
  
    protected void ddlSearch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSearch.SelectedValue.ToString() == "Date")
        {
            devdage.Enabled = true;
            
        }
        else
        {
            devdage.Enabled = false;
        }
        txtSearch.Text = "";
    }
    protected void btnOk_Click1(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        string str = "";
        if (ddlSearch.SelectedValue.ToString() == "Name")
        {
            str = " SELECT [LetterFrom], [Subject],[ReceiveDate], [DispatchDate], [DiaryNo], [Details],[Status] FROM [DiaryLetter] WHERE Designation= '" + lblDesignation.Text + "' and (LetterFrom like '%" + txtSearch.Text.ToString() + "%') order by SN Desc";
        }
        else if (ddlSearch.SelectedValue.ToString() == "Date")
        {
            str = " SELECT [LetterFrom], [Subject],[ReceiveDate], [DispatchDate], [DiaryNo], [Details],[Status] FROM [DiaryLetter] WHERE Designation= '" + lblDesignation.Text + "' and (ReceiveDate='" + Convert.ToDateTime(txtSearch.Text).ToString("dd/MM/yyyy") + "') order by SN Desc";
        }
        else if (ddlSearch.SelectedValue.ToString() == "DiaryNo")
        {
            str = " SELECT [LetterFrom], [Subject],[ReceiveDate], [DispatchDate], [DiaryNo], [Details],[Status] FROM [DiaryLetter] WHERE Designation= '" + lblDesignation.Text + "' and (DiaryNo='" + txtSearch.Text.ToString() + "') order by SN Desc";
        }
        else if (ddlSearch.SelectedValue.ToString() == "IMID")
        {
            str = " SELECT [LetterFrom], [Subject],[ReceiveDate], [DispatchDate], [DiaryNo], [Details],[Status] FROM [DiaryLetter] WHERE Designation= '" + lblDesignation.Text + "' and DiaryNo in (select DiaryNo from DiaryEntry where IMID='" + txtSearch.Text.ToString() + "') order by SN Desc";
        }
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        grdLetters.DataSource = ds;
        grdLetters.DataBind();
    }
    protected void lnkUnread_Click(object sender, ImageClickEventArgs e)
    {
        pnlMain.Visible = false;
        lblTitle.Text = "Unread Letters";
        SqlDataAdapter adp = new SqlDataAdapter(" SELECT [LetterFrom], [Subject],[ReceiveDate], [DispatchDate], [DiaryNo] FROM [DiaryLetter] WHERE Designation= '" + lblDesignation.Text + "' and (Status='NotOpen') order by SN Desc", con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        grdLetters.DataSource = ds;
        grdLetters.DataBind();
    }
    protected void lnkOpen_Click(object sender, ImageClickEventArgs e)
    {
        pnlMain.Visible = false;
        lblTitle.Text = "Opened Letters";

        SqlDataAdapter adp = new SqlDataAdapter(" SELECT [LetterFrom], [Subject],[ReceiveDate], [DispatchDate], [DiaryNo], [Details] FROM [DiaryLetter] WHERE Designation= '" + lblDesignation.Text + "' and (Status='Open') order by SN Desc", con);
        DataSet ds = new DataSet();
        adp.Fill(ds); 
        grdLetters.DataSource = ds;
        grdLetters.DataBind();
    }
    protected void lnkSearch_Click(object sender, ImageClickEventArgs e)
    {
        pnlMain.Visible = true; lblTitle.Text = "Search Letters"; grdLetters.DataBind();
    }
    protected void btnSupply_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select DairyNo from DairyCount where DairyNo='" + grdLetters.SelectedRow.Cells[3].Text + "'", con);
        string dno = Convert.ToString(cmd.ExecuteScalar());
        if (dno == "")
        {
            cmd = new SqlCommand("select IMID from DiaryEntry where DiaryNo='" + grdLetters.SelectedRow.Cells[3].Text + "'", con);
            string im = Convert.ToString(cmd.ExecuteScalar());
            cmd = new SqlCommand("insert into DairyCount(DairyNo,Session,Department,EmpName,EmpCode,CurrentDate,DairyType,IMID,LatterTo,LatterFrom,Status) Values(@DiaryNo,@Session,@Department,@EmpName,@EmpCode,@CurrentDate,@DairyType,@IMID,@LatterTo,@LatterFrom,@Status)", con);
            cmd.Parameters.AddWithValue("@DiaryNo", grdLetters.SelectedRow.Cells[3].Text);
            cmd.Parameters.AddWithValue("@Session", grdLetters.SelectedRow.Cells[4].Text);
            cmd.Parameters.AddWithValue("@Department", "Account");
            cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@DairyType", "Letters");
            cmd.Parameters.AddWithValue("@IMID", im);
            cmd.Parameters.AddWithValue("@LatterTo",lblDesignation.Text.ToString());
            cmd.Parameters.AddWithValue("@LatterFrom", grdLetters.SelectedRow.Cells[5].Text);
             cmd.Parameters.AddWithValue("@Status", "Open");
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update DiaryEntry set Status='CountDispatch' where DiaryNo='" + grdLetters.SelectedRow.Cells[3].Text + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Supplied to Account')", true);
        }
        con.Close(); con.Dispose();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update DiaryLetter set Details='" + txtRemarks.Text + "' , Status='Open' where  DiaryNo='" + grdLetters.SelectedRow.Cells[5].Text + "'", con);
        cmd.ExecuteNonQuery(); bindUnread(); txtRemarks.Text = "";
        con.Close();
        con.Dispose(); 
    }
}