using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class FO_DiaryCount : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
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
                if (!IsPostBack)
                {
                    panDiary.Visible = false;
                    panelLetter.Visible = false;
                   
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    ddlDiaryType.Items[1].Enabled = false; 
                    bindDiary();
                    ddlExamSeason.Focus();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    private void bindDiary()
    {
        SqlDataAdapter addiary = new SqlDataAdapter("select Distinct DiaryNo,Diary,Status,CourierNo as RefNo,MembershipNo as Mem_No From DiaryEntry where (Status='CountReceived' OR Status='CountDispatch') and ExamSession='" + lblHiddenSeason.Text + "' order by Diary desc", con);
        DataSet ds = new DataSet();
        addiary.Fill(ds);
            GridDiaryNo.DataSource = ds;  //B3
            GridDiaryNo.DataBind();
          
        SqlDataAdapter countdiary = new SqlDataAdapter("select Distinct DiaryNo,Diary,CourierNo as RefNo,MembershipNo as Mem_No From DiaryEntry where Status='DiaryEntry' and ExamSession='" +lblHiddenSeason.Text + "' order by Diary desc", con);
        DataSet ds1 = new DataSet();
        countdiary.Fill(ds1);
            GrdCountReceived.DataSource = ds1;  //B2
            GrdCountReceived.DataBind();
      
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
   {
       dtinfo.DateSeparator = "/";
       dtinfo.ShortDatePattern = "dd/MM/yyyy";
       Projectcls pcl = new Projectcls();
       string sts = pcl.CountStatusDairy(txtDiaryNo.Text.ToString(),ddlExamSeason.SelectedValue.ToString()+ txtYearSeason.Text);
       if (sts == "NotSubmitted" && txtDiaryNo.Text!="" && ddlDiaryType.SelectedValue.ToString()!="select")
       {
           con.Close(); con.Open();
           SqlCommand cmd = new SqlCommand();
           cmd = new SqlCommand("update DiaryEntry set Status=@Status,DiaryType=@DiaryType,SubmittedTo=@SubmittedTo,OpenedDate=@OpenedDate where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
           cmd.Parameters.AddWithValue("@Status", "CountDispatch");
           cmd.Parameters.AddWithValue("@DiaryType", ddlDiaryType.SelectedValue.ToString());
           cmd.Parameters.AddWithValue("@SubmittedTo",ddlSupplyTo.SelectedValue.ToString());
           cmd.Parameters.AddWithValue("@OpenedDate", DateTime.Now);
           cmd.ExecuteNonQuery();
           cmd = new SqlCommand("insert into DairyCount(DairyNo,Session,Department,CurrentDate,DairyType,IMID,LatterTo,LatterFrom,Status,TotalDDRcv,TotalNoForm) Values(@DiaryNo,@Session,@Department,@CurrentDate,@DairyType,@IMID,@LatterTo,@LatterFrom,@Status,@TotalDDRcv,@TotalNoForm)", con);
           cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text.ToString());
           cmd.Parameters.AddWithValue("@Session", ddlExamSeason.SelectedValue.ToString() + txtYearSeason.Text);
           cmd.Parameters.AddWithValue("@Department",ddlSupplyTo.SelectedValue.ToString());
           cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);
           cmd.Parameters.AddWithValue("@DairyType", ddlDiaryType.SelectedValue.ToString());
           cmd.Parameters.AddWithValue("@IMID", lblIMID.Text);
           cmd.Parameters.AddWithValue("@LatterTo", "N/A");
           cmd.Parameters.AddWithValue("@LatterFrom","N/A");
           if(txtDD.Text=="" |txtForms.Text=="")
           cmd.Parameters.AddWithValue("@Status", "NotOpen");
           else cmd.Parameters.AddWithValue("@Status", "Open");
           cmd.Parameters.AddWithValue("@TotalDDRcv", txtDD.Text);
           cmd.Parameters.AddWithValue("@TotalNoForm", txtForms.Text);
           cmd.ExecuteNonQuery();
           cmd = new SqlCommand("Insert into ProjectCount(DairyNo,Session,IMID,CurrentDate,Status) Values(@DairyNo,@Session,@IMID,@CurrentDate,@Status)", con);
           cmd.Parameters.AddWithValue("@DairyNo", txtDiaryNo.Text.ToString());
           cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
           cmd.Parameters.AddWithValue("@IMID", lblIMID.Text.ToString());
           cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);
           cmd.Parameters.AddWithValue("@Status", "Submitted");
           cmd.ExecuteNonQuery();
           con.Close();
           bindDiary();
           con.Dispose();
           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Submitted')", true);
           txtDD.Text = ""; txtForms.Text = ""; txtDiaryNo.Text = ""; lblIMID.Text = "";
       }
       else
       {
           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please insert Correct value')", true);
       }
       GridDiaryNo.Focus();
   }
    protected void ibtnHome_Click(object sender, EventArgs e)
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
    string strdno;SqlDataReader reader;
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        lblExceptionDiary.Text = "";
        btnSubmit.Visible = false;
        btnLetterSubmit.Visible = true;
        diaryclick();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        bindDiary();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        bindDiary();
    }
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = 0;
        while (i < GridDiaryNo.Rows.Count)
        {
            if (GridDiaryNo.Rows[i].Cells[3].Text == "CountDispatch")
            {
                GridDiaryNo.Rows[i].Cells[0].Enabled = false;
                GridDiaryNo.Rows[i].Cells[0].Text = "Submitted";
            }
            i++;
        }
        txtDiaryNo.Text = GridDiaryNo.SelectedRow.Cells[1].Text.ToString();
        btnSubmit.Visible = false;
        btnLetterSubmit.Visible = false;
        diaryclick();
    }
    private void diaryclick()
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select IMID, Status from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text + "'", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                strdno = reader["Status"].ToString();
                lblIMID.Text = reader["IMID"].ToString();
                lblIm.Visible = true;
            }
            reader.Close();
            reader.Dispose();
            if (strdno == "")
            {
                lblExceptionDiary.Text = "Diary Not Found.";
                txtDiaryNo.Focus();
            }
            else
            {
                if (strdno == "DiaryEntry")
                {
                    lblExceptionDiary.Text = "Receive Diary First.";
                    txtDiaryNo.Focus();
                    btnSubmit.Visible = false;
                    btnLetterSubmit.Visible = false;
                }
                else if (strdno == "CountDispatch")
                {
                    lblExceptionDiary.Text = "Diary Already Dispatched.";
                    txtDiaryNo.Focus(); btnSubmit.Visible = false;
                }
                else if (strdno == "CountReceived")
                {
                    lblExceptionDiary.Text = "Supply Diary For Entry.";
                    btnSubmit.Visible = true;
                    btnLetterSubmit.Visible = true;
                }
                else if (strdno == "AccReceive")
                {
                    lblExceptionDiary.Text = "Diary Processing in Account.";
                    txtDiaryNo.Focus();
                    btnSubmit.Visible = false;
                }
                else if (strdno == "Supply")
                {
                    lblExceptionDiary.Text = "Diary Supplied";
                    txtDiaryNo.Focus();
                    btnSubmit.Visible = false;
                }
            }
            con.Close();
        }
        catch (SqlException ex)
        {
        }
        finally
        {
            con.Dispose();
        }
    }
    protected void clear()
    {
        txtLetterFrom.Text = "";
        txtSubject.Text = "";
    }
    protected void btnLetterSubmit_Onclick(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            if (txtDiaryNo.Text != "" && txtLetterFrom.Text!="" && txtSubject.Text!="" | (ddlDiaryType.SelectedValue.ToString()=="select"))
            {
                if (ddlSupplyTo.SelectedValue == "Select")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Select Department.')", true);
                }
                else
                {
                    cmd = new SqlCommand("update DiaryEntry set Status=@Status,DiaryType=@DiaryType,OpenedDate=@OpenedDate,SubmittedTo=@SubmittedTo where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@Status", "CountDispatch");
                    cmd.Parameters.AddWithValue("@DiaryType", ddlDiaryType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@OpenedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SubmittedTo", ddlSupplyTo.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into DiaryLetter(ReceiveDate,DispatchDate,Subject,Status,Session,Designation,DispatchNo,Details,LetterFrom,DiaryNo) Values(@ReceiveDate,@DispatchDate,@Subject,@Status,@Session,@Designation,@DispatchNo,@Details,@LetterFrom,@DiaryNo)", con);
                    cmd.Parameters.AddWithValue("@ReceiveDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@DispatchDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Subject", txtSubject.Text.ToString());
                    cmd.Parameters.AddWithValue("@Status", "NotOpen");
                    cmd.Parameters.AddWithValue("@Session", ddlExamSeason.SelectedValue.ToString() + txtYearSeason.Text);
                    cmd.Parameters.AddWithValue("@Designation", ddlSupplyTo.SelectedValue);
                    cmd.Parameters.AddWithValue("@Details", "");
                    cmd.Parameters.AddWithValue("@LetterFrom", txtLetterFrom.Text.ToString());
                    cmd.Parameters.AddWithValue("@DispatchNo", "");
                    cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text);
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Submitted.')", true);
                    txtLetterFrom.Text = ""; txtSubject.Text = "";
                    bindLetters();
                    bindDiary();
                }
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Enter Details.')", true);
        }
        catch (SqlException ex)
        {
        }
        con.Close();
        clear();
    }
    protected void ddlDiaryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        panDiary.Visible = false;
        panelLetter.Visible = false;
        if (ddlDiaryType.SelectedValue == "Forms")
        {
            panDiary.Visible = true; pnlcr.Visible = true; 
        }
        if (ddlDiaryType.SelectedValue == "Latters")
        {
            panelLetter.Visible = true; pnlcr.Visible = false;
        }
        if (ddlDiaryType.SelectedItem.Text== "BOTH")
        {
            panDiary.Visible = true;
            panelLetter.Visible = true; pnlcr.Visible = true;
        }
        bindDiary();
        txtDate.Focus();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtDiaryNoUp.Text == "" )
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert(' DiaryNo is mandatory...')", true);
            txtDiaryNoUp.Text = ""; txtFormsUp.Text = ""; txtDDUp.Text = "";
            txtDiaryNoUp.Focus();
        }
        else
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select DiaryNo from DiaryEntry where DiaryNo='" + txtDiaryNoUp.Text.ToString() + "'", con);
            string strd = Convert.ToString(cmd.ExecuteScalar());
            if (strd == "")
            {
                txtDiaryNoUp.Text = ""; txtFormsUp.Text = ""; txtDDUp.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary No. Not Found, Try another one.')", true);
            }
            else
            {
                if (txtFormsUp.Text == "") txtFormsUp.Text = "0"; if (txtDDUp.Text == "") txtDDUp.Text = "0";
                cmd = new SqlCommand("select DairyNo from DairyCount where DairyNo='" + txtDiaryNoUp.Text.ToString() + "'", con);
                string strDiary = Convert.ToString(cmd.ExecuteScalar());
                if (strDiary == "")
                {
                    cmd = new SqlCommand("insert into DairyCount(DairyNo,Session,Department,CurrentDate,DairyType,IMID,LatterTo,LatterFrom,Status,TotalDDRcv,TotalNoForm) Values('" + txtDiaryNoUp.Text + "','" + lblHiddenSeason.Text + "','" + ddlSubmitUpdate.SelectedValue.ToString() + "','" + DateTime.Now + "','Forms','" + lblIMIDUp.Text + "','N/A','N/A','Open','" + txtDDUp.Text + "','" + txtFormsUp.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Insert into ProjectCount(DairyNo,Session,IMID,CurrentDate,Status) Values('" + txtDiaryNoUp.Text + "','" + lblHiddenSeason.Text + "','" + lblIMIDUp.Text + "','" + DateTime.Now + "','Submitted')", con);
                    cmd.ExecuteNonQuery();
                    txtDiaryNoUp.Focus();
                }
                else
                {
                    cmd = new SqlCommand("update DairyCount set TotalDDRcv=@TotalDDRcv,TotalNoForm=@TotalNoForm,Department=@Department,IMID=@IMID where DairyNo='" + txtDiaryNoUp.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@TotalDDRcv", txtDDUp.Text);
                    cmd.Parameters.AddWithValue("@TotalNoForm", txtFormsUp.Text);
                    cmd.Parameters.AddWithValue("@Department", ddlSubmitUpdate.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IMID", lblIMIDUp.Text);
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("Update DiaryEntry Set SubmittedTo='" + ddlSubmitUpdate.SelectedValue.ToString() + "', Status='CountDispatch' where DiaryNo='" + txtDiaryNoUp.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Successfully Updated.!')", true);
                txtDiaryNoUp.Text = ""; txtFormsUp.Text = ""; txtDDUp.Text = "";
                txtDiaryNoUp.Focus();
            }
            con.Close(); con.Dispose();
        }
    }
    string strDiary;
    protected void txtDiaryNoUp_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDiaryNoUp.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Enter Diary No.!')", true);
            }
            else
            {
                con.Close(); con.Open();
                cmd = new SqlCommand("select DiaryNo,SubmittedTo,IMID from DiaryEntry where DiaryNo='" + txtDiaryNoUp.Text.ToString() + "'", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    strDiary = rd[0].ToString();
                    if (rd[1].ToString() == "&nbsp;" | rd[1].ToString()=="") ddlSubmitUpdate.SelectedValue = "N/A";
                    else ddlSubmitUpdate.SelectedValue = rd[1].ToString();
                    lblIMIDUp.Text = rd[2].ToString();
                }
                rd.Close(); rd.Dispose();
                if (strDiary == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary No. Not Found, Try another one.')", true);
                }
                else
                {
                    bindLetters();
                    cmd = new SqlCommand("select TotalDDRcv,TotalNoForm,IMID from DairyCount where DairyNo='" + txtDiaryNoUp.Text.ToString() + "'", con);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtDDUp.Text = dr[0].ToString();
                        txtFormsUp.Text = dr[1].ToString();
                    }
                    dr.Close();
                }
                con.Close(); con.Dispose();
            }
        }
        catch (FormatException ex)
        {
        }
    }
    private void bindLetters()
    {
        string strBind = "select DiaryNo,ReceiveDate,Subject,Designation,Status from DiaryLetter where DiaryNo='" + txtDiaryNoUp.Text.ToString() + "'";
        SqlDataAdapter adp = new SqlDataAdapter(strBind, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridLetFrom.DataSource = dt;
        GridLetFrom.DataBind();
    }
    protected void GridLetFrom_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "" | e.Row.Cells[1].Text == "&nbsp;")
            {
            }
            else
            {
                e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("dd/MM/yyyy");
            }
        }
    }
    protected void btnReceive_Click(object sender, EventArgs e)
    {
        con.Open();
        int i=0;
         while (i < GrdCountReceived.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GrdCountReceived.Rows[i].FindControl("chkapp");
                if (rbApp.Checked)
                {
                     cmd=new SqlCommand("update DiaryEntry set Status='CountReceived',OpenedDate='"+DateTime.Now+"' where DiaryNo='"+GrdCountReceived.Rows[i].Cells[1].Text+"'",con);
                     cmd.ExecuteNonQuery();
                }
             i++;
         }
         bindDiary();
        con.Close();
        con.Dispose();
      
    }
    protected void GridDiaryNo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
            if (e.Row.Cells[3].Text == "CountDispatch")
            {
                e.Row.Cells[0].Enabled = false; e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].Text = "Submitted";
            }
        }
    }
    protected void GrdCountReceived_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (GrdCountReceived.Rows.Count == 0)
            btnReceive.Enabled = false;
        else btnReceive.Enabled = true;
    }
    protected void ddlSupplyTo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSupplyTo.SelectedValue == "Account" | ddlSupplyTo.SelectedValue=="Project")
           ddlDiaryType.Items[1].Enabled = false;  
        else ddlDiaryType.Items[1].Enabled = true;
        bindDiary();
    }
    protected void ddlSubmitUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDiary();
    }
}