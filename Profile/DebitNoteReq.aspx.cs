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

public partial class Profile_DebitNoteReq : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0) ddlsession.SelectedValue = "Sum";
            else ddlsession.SelectedValue = "Win";
            txtSession.Text = DateTime.Now.Year.ToString();
            lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
            bindGrid();
        }
    }
    protected void btnSelectAll_Onclick(object sender, EventArgs e)
    {
        int i = 0;
        while (i < grdAppRecord.Rows.Count)
        {
            CheckBox rbApp = (CheckBox)grdAppRecord.Rows[i].FindControl("chkapp");
            rbApp.Checked = true;
            i++;
        }
        grdAppRecord.Focus();
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
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
    protected void gridDebitNote_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Select";
         //   e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "&nbsp;" | e.Row.Cells[1].Text == "")
                e.Row.Cells[1].Text = "";
            else e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[4].Text == "Requested")
                e.Row.ForeColor = System.Drawing.Color.Black;
            if (e.Row.Cells[4].Text == "Hold")
                e.Row.ForeColor = System.Drawing.Color.Red;
            if (e.Row.Cells[4].Text == "Processed")
                e.Row.ForeColor = System.Drawing.Color.Green;
            if (e.Row.Cells[4].Text == "Approved")
                e.Row.ForeColor = System.Drawing.Color.Maroon;
            e.Row.Cells[9].Text = e.Row.Cells[9].Text.ToString().TrimEnd('0').TrimEnd('.');
            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[10].Text = e.Row.Cells[10].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    protected void gridDebitNote_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        pnlAppr.Visible = true;
        lblDno.Text = gridDebitNote.SelectedRow.Cells[2].Text.ToString();
        BindGridApp(gridDebitNote.SelectedRow.Cells[2].Text.ToString());
        cmd = new SqlCommand("select Remarks from DebitNote where IMID='" + gridDebitNote.SelectedRow.Cells[3].Text.ToString() + "' and DiaryNo='" + gridDebitNote.SelectedRow.Cells[2].Text.ToString() + "'", con);
        txtRemarks.Text = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (gridDebitNote.SelectedRow.Cells[4 ].Text == "Requested" | gridDebitNote.SelectedRow.Cells[4].Text == "Hold")
        {
            con.Open();
            cmd = new SqlCommand();
            SqlTransaction sTR;
            sTR = con.BeginTransaction();
            cmd.Connection = con;
            cmd.Transaction = sTR;
            try
            {
                //cmd.CommandText = "update DebitNote set Status='Approved', Remarks='" + txtRemarks.Text.ToString() + "' where IMID='" + gridDebitNote.SelectedRow.Cells[3].Text.ToString() + "' and DiaryNo='" + gridDebitNote.SelectedRow.Cells[2].Text.ToString() + "'";
                //cmd.ExecuteNonQuery();
                int i = 0; int count = 0;
                while (i <= grdAppRecord.Rows.Count - 1)
                {
                     CheckBox rbApp = (CheckBox)grdAppRecord.Rows[i].FindControl("chkapp");
                     if (rbApp.Checked)
                     {
                         bool flg = true;
                         cmd.CommandText = "update AppRecord set FeeType='" + grdAppRecord.Rows[i].Cells[3].Text.ToString() + "', Status='no',SubDate='" + Convert.ToDateTime(DateTime.Now, dtinfo) + "' where AppNo='" + Convert.ToInt32(grdAppRecord.Rows[i].Cells[4].Text) + "' and Session='" + grdAppRecord.Rows[i].Cells[12].Text.ToString() + "'";
                         cmd.ExecuteNonQuery();
                         if (grdAppRecord.Rows[i].Cells[21].Text != "0")
                         {
                             updateIMBooks(grdAppRecord.Rows[i].Cells[6].Text.ToString(), grdAppRecord.Rows[i].Cells[7].Text.ToString(), grdAppRecord.Rows[i].Cells[3].Text.ToString(), flg, sTR, cmd);
                         }
                         if (grdAppRecord.Rows[i].Cells[22].Text != "0")  // Composite Fees
                         {
                             cmd.CommandText = "update CompositeFees set status='Submitted', Date='" + DateTime.Now + "' where sid='" + grdAppRecord.Rows[i].Cells[20].Text.ToString() + "' and sessionid in ( select MAX(sessionid) from CompositeFees where sid='" + grdAppRecord.Rows[i].Cells[20].Text.ToString() + "')";
                             cmd.ExecuteNonQuery();
                             if ((grdAppRecord.Rows[i].Cells[7].Text.ToString() == "PartII") || (grdAppRecord.Rows[i].Cells[7].Text == "SectionB"))
                             {
                                 Student st = new Student();
                                 if (st.AdmissionType(grdAppRecord.Rows[i].Cells[20].Text.ToString(), sTR, cmd) == "Direct")
                                     flg = false;
                             }
                             updateIMBooks(grdAppRecord.Rows[i].Cells[6].Text.ToString(), grdAppRecord.Rows[i].Cells[7].Text.ToString(), grdAppRecord.Rows[i].Cells[3].Text.ToString(), flg, sTR, cmd);
                         }
                         if (grdAppRecord.Rows[i].Cells[25].Text != "0")  // Exam Fees
                         {
                             if (grdAppRecord.Rows[i].Cells[7].Text.ToString() == "PartII")
                             {
                                 cmd.CommandText = "update ExamCurrent set CourseStatus='Approved' where SId='" + grdAppRecord.Rows[i].Cells[20].Text + "'";
                                 cmd.ExecuteNonQuery();
                             }
                             else
                             {
                                 cmd.CommandText = "update ExamCurrent set ExamStatus='Approved' where SId='" + grdAppRecord.Rows[i].Cells[20].Text + "'";
                                 cmd.ExecuteNonQuery();
                             }
                         }
                         if (grdAppRecord.Rows[i].Cells[24].Text != "0")  // ITI Fees
                         {
                             cmd.CommandText = "update ITIForm set Status='Approved' where AppNo='" + grdAppRecord.Rows[i].Cells[4].Text + "'";
                             cmd.ExecuteNonQuery();
                         }
                         //  cl.AmountSubmit(txtIMID.Text, grdAppRecord.Rows[i].Cells[8].Text.ToString(), Convert.ToDateTime(txtDate.Text, dtinfo), "Debit", (Convert.ToInt32(grdAppRecord.Rows[i].Cells[12].Text.ToString()) + Convert.ToInt32(grdAppRecord.Rows[i].Cells[13].Text.ToString())).ToString(), grdAppRecord.Rows[i].Cells[9].Text.ToString(), grdAppRecord.Rows[i].Cells[1].Text.ToString(), sTR, cmd);
                         count++;  
                     }
                     i++;
                     if (count == grdAppRecord.Rows.Count)
                     {
                         cmd.CommandText = "update DebitNote set Status='Approved', Remarks='" + txtRemarks.Text.ToString() + "' where IMID='" + gridDebitNote.SelectedRow.Cells[3].Text.ToString() + "' and DiaryNo='" + gridDebitNote.SelectedRow.Cells[2].Text.ToString() + "'";
                         cmd.ExecuteNonQuery();

                     }
                }
                sTR.Commit();
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Debit Note is Successfully Approved.!')", true);
                txtRemarks.Text = "";
                bindGrid(); pnlAppr.Visible = false;
            }
            catch (Exception ex)
            {
                sTR.Rollback();
              
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
           
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Debit Note is Already Approved.!')", true);
        }
    }
    private void updateIMBooks(string course, string part, string imid, bool flg, SqlTransaction sTR, SqlCommand cmd)
    {
        int cI = 0, cII = 0, cIIE = 0, cA = 0, cB = 0, aI = 0, aII = 0, aIIE = 0, aA = 0, aB = 0;
        if (flg == true)
        {
            if (course == "Civil")
            {
                if (part == "PartI")
                    cI = 1;
                else if (part == "PartII")
                    cII = 1;
                else if (part == "SectionA")
                    cA = 1;
                else if (part == "SectionB")
                    cB = 1;
            }
            else
            {
                if (part == "PartI")
                    aI = 1;
                else if (part == "PartII")
                    aII = 1;
                else if (part == "SectionA")
                    aA = 1;
                else if (part == "SectionB")
                    aB = 1;
            }
        }
        else if (flg == false)
        {
            if (course == "Civil")
            {
                if (part == "PartII")
                {
                    cIIE = 1;
                }
            }
            else
            {
                if (part == "PartII")
                    aIIE = 1;
            }
        }
        cmd.CommandText = "update IMBooks set CPartI=CPartI+'" + cI + "',CPartII=CPartII+'" + cII + "',CPartIIE=CPartIIE+'" + cIIE + "',CSectionA=CSectionA+'" + cA + "',CSectionB=CSectionB+'" + cB + "',APartI=APartI+'" + aI + "',APartII=APartII+'" + aII + "',APartIIE=APartIIE+'" + aIIE + "',ASectionA=ASectionA+'" + aA + "',ASectionB=ASectionB+'" + aB + "',CourseID='081' where IMID='" + imid + "'";
        cmd.ExecuteNonQuery();
    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
         int i = 0;
         con.Close(); con.Open();
         while (i <= grdAppRecord.Rows.Count - 1)
         {
             CheckBox rbApp = (CheckBox)grdAppRecord.Rows[i].FindControl("chkapp");
             if (rbApp.Checked)
             {
                 cmd = new SqlCommand("update AppREcord set Approvedby='Account' where Appno='" + grdAppRecord.Rows[i].Cells[4].Text.ToString() + "'", con);
                 cmd.ExecuteNonQuery();
             }
             i++;
         }
         bindGrid();
         pnlAppr.Visible = false;
         con.Close(); con.Dispose();
    }
    private void bindGrid()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance as LateFees,Amount from DebitNote where Status='Requested' and  DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='"+lblhiddenSession.Text+"') order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();
    }
    private void BindGridApp(string dno)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + dno + "' and ApprovedBy='DebitNote' and (Status='NotApproved' or Status='Hold')", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        grdAppRecord.DataSource = dt;
        grdAppRecord.DataBind();
    }
    protected void gridAppRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Select";
            //   e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text == "&nbsp;" | e.Row.Cells[10].Text == "")
                e.Row.Cells[10].Text = "";
            else e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");

            if (e.Row.Cells[13].Text == "&nbsp;" | e.Row.Cells[13].Text == "")
                e.Row.Cells[13].Text = "";
            else e.Row.Cells[13].Text = Convert.ToDateTime(e.Row.Cells[13].Text).ToString("dd/MM/yyyy");
        
            e.Row.Cells[17].Text = e.Row.Cells[17].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[18].Text = e.Row.Cells[18].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[19].Text = e.Row.Cells[19].Text.ToString().TrimEnd('0').TrimEnd('.');
         
            e.Row.Cells[21].Text = e.Row.Cells[21].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[22].Text = e.Row.Cells[22].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[23].Text = e.Row.Cells[23].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[24].Text = e.Row.Cells[24].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[25].Text = e.Row.Cells[25].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[27].Text = e.Row.Cells[27].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[28].Text = e.Row.Cells[28].Text.ToString().TrimEnd('0').TrimEnd('.');
            ////e.Row.Cells[1].Visible = false;
            //e.Row.Cells[10].Text = e.Row.Cells[10].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        bindGrid();
        txtSession.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        bindGrid();
      
    }
   
    protected void lnlRequest_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "') and Status='Requested' order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();
    }
    protected void lnlApproved_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "')  and Status='Approved'  order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();
    }
    protected void lnkProcess_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "')  and Status='Processed'  order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();

    }
    protected void imgHold_Click(object sender, ImageClickEventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ReqDate,DiaryNo,IMID,Status,Remarks,Admission,Exam,Others,Balance,Amount from DebitNote where DiaryNo in(select DiaryNo from DiaryEntry where ExamSession='" + lblhiddenSession.Text + "')  and Status='Hold'  order by SN Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridDebitNote.DataSource = dt;
        gridDebitNote.DataBind();
    }
}