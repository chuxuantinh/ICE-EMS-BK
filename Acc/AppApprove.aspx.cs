using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Acc_AppApprove : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    private DataRow dr2;
    SqlCommand cmd;
    clsIMAC IMAC = new clsIMAC();
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                btnAddToApprovalTAble.Enabled = false;
                datastructure();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                int lvl = dev.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
                if (lvl == 0)
                    chkUseLimit.Enabled = false;
                else chkUseLimit.Enabled = true;
                btnApprove.Enabled = false; acclass.Visible = false;
                btnPay.Visible = false;
                btnSendDebitNote.Visible = false;
                lblToAmtFo.Text = "0"; lblToLateFo.Text = "0"; panelApproveNow.Visible = false;
                ddlsession.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
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
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("IMID");
        dtDatas.Columns.Add("AppNo");
        dtDatas.Columns.Add("Stream");
        dtDatas.Columns.Add("Course");
        dtDatas.Columns.Add("Part");
        dtDatas.Columns.Add("Name");
        dtDatas.Columns.Add("FatherName");
        dtDatas.Columns.Add("DOB");
        dtDatas.Columns.Add("DiaryNo");
        dtDatas.Columns.Add("Session");
        dtDatas.Columns.Add("Date");
        dtDatas.Columns.Add("FormType");
        dtDatas.Columns.Add("Amount");
        dtDatas.Columns.Add("LateFee");
        dtDatas.Columns.Add("Enrolment");
        dtDatas.Columns.Add("AdmissionFees");
        dtDatas.Columns.Add("CompositeFees");
        dtDatas.Columns.Add("AnnualSubFees");
        dtDatas.Columns.Add("ITIFees");
        dtDatas.Columns.Add("ExamFees");
        dtDatas.Columns.Add("EXMPFees");
        dtDatas.Columns.Add("CADFees");
        ViewState["dtDatas"] = dtDatas;
    }
    protected void txtDiaryNo_TectChantd(object sender, EventArgs e)
    {
        lblDiaryAmount.Text = "";
        getDiaryAmount();
        showcount(lblSessionHiddend.Text.ToString(), txtDiaryNo.Text.ToString());
        con.Close(); con.Dispose();
    }
    private void getDiaryAmount()  // select to amount of diary.
    {
        if (txtIMID.Text == "")
        {
            lblExceptionOK.Text = "Please Insert IMID"; txtIMID.Focus();
        }
        else
        {           
            cmd = new SqlCommand("select SN from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "'", con);
            con.Close(); con.Open();
            string damount1 = Convert.ToString(cmd.ExecuteScalar());
            if (damount1.ToString() == "")
            {                
                lblExceptionOK.Text = "Invalid Diary No.";
                lblExceptionOK.ForeColor = System.Drawing.Color.Red;
                txtDiaryNo.Focus();
            }
            else
            {
                cmd = new SqlCommand("select sum(Amt) from FeeAC where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and AmtFor!='Membership'", con);
                con.Close(); con.Open();
                string damount = Convert.ToString(cmd.ExecuteScalar());
                con.Close();
                if (damount == "")
                    lblDiaryAmount.Text = "0";
                else
                {
                    lblDiaryAmount.Text = damount.ToString();
                    lblDiaryAmount.Text = lblDiaryAmount.Text.TrimEnd('0').TrimEnd('.');
                }
                lblExceptionOK.Text = "";
                btnApproveNow.Focus();
            }
        }
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString(); txtIMID.Text = ""; txtDiaryNo.Text = "";
        txtIMID.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIMID.Text = ""; txtDiaryNo.Text = "";
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtIMID.Focus();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        lblTAmt.Text = "";
        Gid();       
        SqlDataAdapter ad = new SqlDataAdapter("select Distinct DNo From AppRecord where Status='NotApproved' and Session='" + lblSessionHiddend.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridDiaryNo.DataSource = dt;
        GridDiaryNo.DataBind();
        con.Close(); con.Open();
        cmd = new SqlCommand();
        SqlTransaction sTR;
        sTR = con.BeginTransaction();
        cmd.Transaction = sTR;
        cmd.Connection = con;
        try
        {
            okk(txtIMID.Text.ToString(), sTR, cmd);
            sTR.Commit();
        }
        catch (Exception ex)
        {
            sTR.Rollback();
        }
        getDebitNote(con);
        txtDiaryNo.Focus();
        con.Close(); 
        con.Dispose();
    }
    private void okk(string strid, SqlTransaction sTR, SqlCommand cmd)  // Fill Diary and Select IMAC amount.
    {
        cmd.CommandText = "select ID from IM where ID='" + strid.ToString() + "'";
        string chk = Convert.ToString(cmd.ExecuteScalar());
        bool i = false;
        if (chk == strid.ToString())
        {
            i = true;
            btnOK.Enabled = true;
            acclass.Visible = true;
        }
        else
        {
            txtIMID.Text = "Invalid ID";
            lblExceptionOK.Text = "Please Insert Valid IM ID.";
            txtIMID.Focus();
        }
        if (i)
        {
            lblExceptionOK.Text = ""; btnAddToApprovalTAble.Enabled = true;
            cmd.CommandText = "select * from IM where ID='" + strid.ToString() + "'";
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
            }
            reader.Close();
            imamount(sTR, cmd, reader, strid);
            ddlGid.DataSource = IMAC.GroupMate(txtIMID.Text);
            ddlGid.DataBind();
            ddlGid.SelectedValue = txtIMID.Text;
        }
    }
    protected void ddlApstype_OnSelectedIndexChanged(object sender, EventArgs e)
    {
            con.Close(); con.Open();
            cmd = new SqlCommand();
            SqlTransaction sTR;
            sTR = con.BeginTransaction();
            cmd.Transaction = sTR;
            cmd.Connection = con;
            SqlDataReader reader=null;
            try
            {
                imamount(sTR, cmd, reader, txtIMID.Text);
                sTR.Commit();
            }
            catch (SqlException ex)
            {
                sTR.Rollback();
            }
            finally
            {
                con.Close(); con.Dispose();
            }
    }
    private void imamount(SqlTransaction sTR, SqlCommand cmd, SqlDataReader reader, string strid)
    {
            lblTAmt.Text = IMAC.totalamount(strid, sTR, cmd);
            //lblProspectusAmount.Text = IMAC.specificAmount(strid, "Prospectus", sTR, cmd);
            lblPAmt.Text = IMAC.specificAmount(strid, ddlAppstype.SelectedValue.ToString(), sTR, cmd);
    }
    private void getDebitNote(SqlConnection con)
    {
        lblApprovallimit.Text = IMAC.DebitNoteLimit(txtIMID.Text,con);
        lblApprovalRange.Text = IMAC.DebitNoteRange(txtIMID.Text,con);
        lblAvailable.Text = (Convert.ToInt32(lblApprovallimit.Text) - Convert.ToInt32(lblApprovalRange.Text)).ToString();
    }
    private int T, L, R, Df, Mr, Tkn, Pmt, Udr, Ltkn;
    private int To, Re, Fr;
    public float Div;
    protected void chkAddAmount_OnChackedChanged(object sender, EventArgs e)
    {
        int req=Convert.ToInt32(lblReqAmt.Text);
        int pamt=Convert.ToInt32(lblPAmt.Text);
        int available=Convert.ToInt32(lblAvailable.Text);
        if (chkUseLimit.Checked == true)
        {
            if (Convert.ToInt32(lblReqAmt.Text) > Convert.ToInt32(lblPAmt.Text))
            {
                if (Convert.ToInt32(lblAvailable.Text) >= req - pamt)
                {
                    lblDuesAmt.Text = (req - pamt).ToString();
                }
                else
                {
                    lblExceptionAC.Text = "Enough Amount not Available.";
                    chkUseLimit.Checked = false;
                }
            }
        }
        else
            lblDuesAmt.Text = "0";
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int imamt = Convert.ToInt32(lblIMAmount.Text);
            int reqamt = Convert.ToInt32(lblReqAmt.Text);
            int payamt = Convert.ToInt32(lblPAmt.Text);
            int duesamt = Convert.ToInt32(lblDuesAmt.Text);
            if (imamt >= reqamt)  // Amount is enough.
            {
                btnSendDebitNote.Visible = false;
                btnPay.Visible = true;
            }
            else
            {
                // Debit Limit Used.
                if (chkUseLimit.Checked == true)
                {
                    // Check Amount.
                    if (imamt+duesamt >= reqamt)  // Enough
                    {
                        btnSendDebitNote.Visible = false;
                        btnPay.Visible = true;
                    }
                    else
                    {
                        btnPay.Visible = false;
                        btnSendDebitNote.Visible = false;
                        // Can't Pay and Send Debit note.
                    }
                }
                else
                {
                    btnSendDebitNote.Visible = true;
                    btnPay.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage1.Text = ex.ToString();
        }
        finally
        {
            btnApprove.Enabled = false;
        }
    }                            
    protected void btnPay_click(object sender, EventArgs e)
    {
        lblEnrolment.Text = txtIMID.Text.ToString();
        Log.WriteLog(Request.QueryString["maikal"], "B005", txtIMID.Text.ToString() + ":" + txtDiaryNo.Text.ToString(), lblEnrolment.Text.ToString(), "Application Approved");
        Log.WriteLog("B005", Request.QueryString["maikal"], txtIMID.Text.ToString() + ":" + txtDiaryNo.Text.ToString(), lblEnrolment.Text.ToString(), "Application Approved");
        updateadmission(Convert.ToInt32(lblIMAmount.Text), Convert.ToInt32(lblReqAmt.Text), Convert.ToInt32(lblDuesAmt.Text), ddlGid.SelectedValue);
        btnApprove.Enabled = false; btnPay.Visible = false;
        con.Close(); con.Dispose();
    }

    // insert into Debitnote
    // update AppRecord.Approvedby=DebitNote
    protected void btnSendDebitNote_Click(object sender, EventArgs e)
    {
            con.Close(); con.Open();
            cmd = new SqlCommand("insert into DebitNote(IMID,DiaryNo,ReqDate,Status,Admission,Exam,Others,Balance,Amount) values(@IMID,@DiaryNo,@ReqDate,@Status,@Admission,@Exam,@Other,@Balance,@Amount)", con);
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text);
            cmd.Parameters.AddWithValue("@Balance", Convert.ToInt32(lblToLateFo2.Text));
            cmd.Parameters.AddWithValue("@Amount", lblReqAmt.Text);
            cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text);
            cmd.Parameters.AddWithValue("@ReqDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", "Requested");
            cmd.Parameters.AddWithValue("@Admission", ("[" + lblAddForms.Text + "]" + "," + lblAddTotal.Text));
            cmd.Parameters.AddWithValue("@Exam", ("[" + lblExamForms.Text + "]" + "," + lblExamTotal.Text));
            cmd.Parameters.AddWithValue("@Other", lblotherTotal.Text);
            cmd.ExecuteNonQuery();
            int i=0;
            while (i <= GridApprove.Rows.Count - 1)
            {
                cmd = new SqlCommand("Update AppRecord set Approvedby='DebitNote' where AppNo='" + Convert.ToInt32(GridApprove.Rows[i].Cells[1].Text) + "'", con);
                cmd.ExecuteNonQuery();
                i++;
            }
            Log.WriteLog(Request.QueryString["maikal"], "B006", txtIMID.Text.ToString() + ":" + txtDiaryNo.Text.ToString(), lblEnrolment.Text.ToString(), "Debit Note Send");
            Log.WriteLog("B006", Request.QueryString["maikal"], txtIMID.Text.ToString() + ":" + txtDiaryNo.Text.ToString(), lblEnrolment.Text.ToString(), "Debit Note Send");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Request Send')", true);
        con.Close(); con.Dispose();
    }
    ClsAccount cl = new ClsAccount();
    public void updateadmission(int imamt, int req, int dues, string feetype)
        {
            int var;
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Close();
            con.Open();
            cmd = new SqlCommand();
            SqlTransaction sTR;
            sTR = con.BeginTransaction();
            cmd.Connection = con;
            cmd.Transaction = sTR;
            try
            {
                SqlDataReader reader = null;
                IMAC.DebitAmount(txtIMID.Text, imamt, ddlAppstype.SelectedValue.ToString(), sTR, cmd);
                cmd.CommandText = "update IMAC set Range=Range+'" + Convert.ToInt32(dues) + "' where IMID='" + txtIMID.Text.ToString() + "'";
                cmd.ExecuteNonQuery();
               
                int amout =Convert.ToInt32(lblPAmt.Text)- Convert.ToInt32(lblIMAmount.Text);
                cmd.CommandText="update IMAccount set Amount='"+ amout+"' where IMID='"+txtIMID.Text+"' and Fees='"+ddlAppstype.SelectedValue.ToString()+"'";
                cmd.ExecuteNonQuery();
                //cmd.CommandText = "update IMAC set Total='" + total + "', Credit=Credit+'" + Fr + "',LateFeeTaken=LateFeeTaken+'" + Ltkn + "' where IMID='" + IMID.ToString() + "'";
                //cmd.ExecuteNonQuery();
                //if (lblDebitNote.Text != "")
                //{
                //    cmd.CommandText = "update DebitNote set Status='Processed' where DiaryNo='" + txtDiaryNo.Text + "' and IMID='" + txtIMID.Text + "' and SN in(select MAX(sn) from DebitNote where  DiaryNo='"+txtDiaryNo.Text+"')";
                //    cmd.ExecuteNonQuery();
                //}
              //  if (Ltkn != 0) cl.AmountSubmit(IMID, txtDiaryNo.Text.ToString(), Convert.ToDateTime(txtDate.Text, dtinfo), "Debit", Ltkn.ToString(), lblSessionHiddend.Text.ToString(), "LateFees", sTR, cmd);
                int i = 0;
                while (i <= GridApprove.Rows.Count - 1)
                {
                    bool flg = true;
                    cmd.CommandText = "update AppRecord set FeeType='" + ddlGid.SelectedValue + "', Status='no',SubDate='" + Convert.ToDateTime(txtDate.Text, dtinfo) + "' where AppNo='" + Convert.ToInt32(GridApprove.Rows[i].Cells[1].Text) + "' and Session='" + GridApprove.Rows[i].Cells[9].Text.ToString() + "'";
                    cmd.ExecuteNonQuery();
                    if (GridApprove.Rows[i].Cells[15].Text != "0")
                    {
                        updateIMBooks(GridApprove.Rows[i].Cells[3].Text.ToString(), GridApprove.Rows[i].Cells[4].Text.ToString(), GridApprove.Rows[i].Cells[0].Text.ToString(), flg, sTR, cmd);
                    }
                    if (GridApprove.Rows[i].Cells[16].Text != "0")  // Composite Fees
                    {
                        cmd.CommandText = "update CompositeFees set status='Submitted', Date='" + DateTime.Now + "' where sid='" + lblEnrolment.Text.ToString() + "' and sessionid in ( select MAX(sessionid) from CompositeFees where sid='" + lblEnrolment.Text.ToString() + "')";
                        cmd.ExecuteNonQuery();
                        if ((GridApprove.Rows[i].Cells[4].Text.ToString() == "PartII") || (GridApprove.Rows[i].Cells[4].Text == "SectionB"))
                        {
                            Student st = new Student();
                            if (st.AdmissionType(GridApprove.Rows[i].Cells[14].Text.ToString(), sTR, cmd) == "Direct")
                                flg = false;
                        }
                        updateIMBooks(GridApprove.Rows[i].Cells[3].Text.ToString(), GridApprove.Rows[i].Cells[4].Text.ToString(), GridApprove.Rows[i].Cells[0].Text.ToString(), flg, sTR, cmd);
                    }
                    if (GridApprove.Rows[i].Cells[19].Text != "0")  // Exam Fees
                    {
                        if (GridApprove.Rows[i].Cells[4].Text.ToString() == "PartII")
                        {
                            cmd.CommandText = "update ExamCurrent set CourseStatus='Approved' where SId='" + GridApprove.Rows[i].Cells[14].Text + "'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "update ExamCurrent set ExamStatus='Approved' where SId='" + GridApprove.Rows[i].Cells[14].Text + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    if (GridApprove.Rows[i].Cells[18].Text != "0")  // ITI Fees
                    {
                        cmd.CommandText = "update ITIForm set Status='Approved' where AppNo='" + GridApprove.Rows[i].Cells[1].Text + "'";
                        cmd.ExecuteNonQuery();
                    }
                  //  cl.AmountSubmit(txtIMID.Text, GridApprove.Rows[i].Cells[8].Text.ToString(), Convert.ToDateTime(txtDate.Text, dtinfo), "Debit", (Convert.ToInt32(GridApprove.Rows[i].Cells[12].Text.ToString()) + Convert.ToInt32(GridApprove.Rows[i].Cells[13].Text.ToString())).ToString(), GridApprove.Rows[i].Cells[9].Text.ToString(), GridApprove.Rows[i].Cells[1].Text.ToString(), sTR, cmd);
                    i++;
                }
                imamount(sTR, cmd, reader, txtIMID.Text.ToString());
                sTR.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Application Forms Approved.')", true);
                lblPayMessage.Text = "Application Forms Approved";
            }
            catch (Exception ex)
            {
                sTR.Rollback();
                lblPayMessage.Text = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
    }
    protected void btnSelectAll_Onclick(object sender, EventArgs e)
    {
        int i = 0;
        while (i < GridAppTable.Rows.Count)
        {
            CheckBox rbApp = (CheckBox)GridAppTable.Rows[i].FindControl("chkapp");
            rbApp.Checked = true;
            i++;
        }
        GridAppTable.Focus();
    }
    
    protected void btnSaveRecordTable_click(object sender, EventArgs e) // View Records Button
    {
        string qry = "";
            if(ddlAppstype.SelectedValue=="Project")
                qry = "select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='NotApproved' and Approvedby='Account'  and (FormType='ProformaB' or FormType='ProformaC')  ORDER BY AppNo";
         else if(ddlAppstype.SelectedValue=="AutoCAD")
                qry = "select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='NotApproved' and Approvedby='Account'  and (FormType='MCADLateFee' or FormType='MCADRegistration') ORDER BY AppNo";
         else if(ddlAppstype.SelectedValue=="Academic")
                qry = "select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='NotApproved' and Approvedby='Account'  and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0)  ORDER BY AppNo";
        else
                qry = "select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='NotApproved' and Approvedby='Account' ORDER BY AppNo";
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GridAppTable.DataSource = dt;
        GridAppTable.DataBind();
        btnApprove.Enabled = true;
        btnSelectAll.Focus();
    }
    decimal grdtotal = 0, latetotal = 0, addtotal = 0, examtotal = 0, otherttl = 0; int adno = 0, tono = 0;
    int addform, examform, otherform;
    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[11].Text = "DiaryNo";
            e.Row.Cells[18].Text = "LateFee";
            e.Row.Cells[19].Text = "ExmpFee";
            e.Row.Cells[20].Text = "Membership";
            e.Row.Cells[23].Text = "ASF";
          //  e.Row.Cells[17].Visible = false;//Amount
            e.Row.Cells[5].Visible = false; // Stream
            e.Row.Cells[27].Visible = false;//CAD
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
          //  e.Row.Cells[17].Visible = false;//Amount
            e.Row.Cells[5].Visible = false; //Stream
            e.Row.Cells[27].Visible = false;//CAD
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
            decimal rowtatal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            decimal latefee = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LateFee"));
            string strexamtotal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FormType"));
            if (strexamtotal == "Admission")
                adno = adno + 1;
            tono = tono + 1;
            grdtotal = grdtotal + rowtatal;
            latetotal = latetotal + latefee;
            if (e.Row.Cells[10].Text != null & e.Row.Cells[10].Text != "" & e.Row.Cells[10].Text != "&nbsp;")
                e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[13].Text != null & e.Row.Cells[13].Text != "" & e.Row.Cells[13].Text != "&nbsp;")
                e.Row.Cells[13].Text = Convert.ToDateTime(e.Row.Cells[13].Text).ToString("dd/MM/yyyy");
        }
        lblToAmtFo.Text = grdtotal.ToString();
        lblAdmiSnFO.Text = adno.ToString();
        lblExamSnFo.Text = (tono - adno).ToString();
        lblToLateFo.Text = latetotal.ToString();
    }
    decimal grdtotal2 = 0, latetotal2 = 0; int adno2 = 0, tono2 = 0, eto2 = 0;
    protected void GridApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = "Total";
            e.Row.Cells[14].Text = "Membership";
            e.Row.Cells[16].Text = "Comp Fee";
            e.Row.Cells[17].Text = "ASF";
            e.Row.Cells[20].Text = "EXMP";
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void btnAddToApprovalTableFinal_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            lblExceptinApprovedTable.Text = "";
            panelApproveNow.Visible = true;
            GridApprove.DataSource = null;
            GridApprove.DataBind();
            DataTable dtDatas = (DataTable)ViewState["dtDatas"];
            dtDatas.Clear();
            while (i < GridAppTable.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GridAppTable.Rows[i].FindControl("chkapp");
                if (rbApp.Checked)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["IMID"] = GridAppTable.Rows[i].Cells[3].Text;
                    drNewRow["AppNo"] = GridAppTable.Rows[i].Cells[4].Text;
                    drNewRow["Stream"] = GridAppTable.Rows[i].Cells[5].Text;
                    drNewRow["Course"] = GridAppTable.Rows[i].Cells[6].Text;
                    drNewRow["Part"] = GridAppTable.Rows[i].Cells[7].Text;
                    drNewRow["Name"] = GridAppTable.Rows[i].Cells[8].Text;
                    drNewRow["FatherName"] = GridAppTable.Rows[i].Cells[9].Text;
                    drNewRow["DOB"] = Convert.ToDateTime(GridAppTable.Rows[i].Cells[10].Text, dtinfo);
                    drNewRow["DiaryNo"] = GridAppTable.Rows[i].Cells[11].Text;
                    drNewRow["Session"] = GridAppTable.Rows[i].Cells[12].Text;
                    drNewRow["Date"] = Convert.ToDateTime(GridAppTable.Rows[i].Cells[13].Text, dtinfo);
                    drNewRow["FormType"] = GridAppTable.Rows[i].Cells[15].Text;
                    if (GridAppTable.Rows[i].Cells[15].Text.ToString().Contains("NewAdmission")) addform = addform + 1;
                    else if (GridAppTable.Rows[i].Cells[15].Text.ToString().Contains("Exam")) examform = examform + 1;
                    drNewRow["Amount"] = GridAppTable.Rows[i].Cells[17].Text;
                    drNewRow["LateFee"] = GridAppTable.Rows[i].Cells[18].Text;
                    grdtotal2 = grdtotal2 + Convert.ToDecimal(GridAppTable.Rows[i].Cells[17].Text);
                    latetotal2 = latetotal2 + Convert.ToDecimal(GridAppTable.Rows[i].Cells[18].Text);
                    drNewRow["Enrolment"] = GridAppTable.Rows[i].Cells[20].Text;
                    drNewRow["AdmissionFees"] = GridAppTable.Rows[i].Cells[21].Text;
                    addtotal = addtotal + Convert.ToInt32(GridAppTable.Rows[i].Cells[21].Text);
                    drNewRow["CompositeFees"] = GridAppTable.Rows[i].Cells[22].Text;
                    drNewRow["AnnualSubFees"] = GridAppTable.Rows[i].Cells[23].Text;
                    drNewRow["ITIFees"] = GridAppTable.Rows[i].Cells[24].Text;
                    drNewRow["ExamFees"] = GridAppTable.Rows[i].Cells[25].Text;
                    examtotal = examtotal + Convert.ToInt32(GridAppTable.Rows[i].Cells[25].Text);
                    drNewRow["EXMPFees"] = GridAppTable.Rows[i].Cells[19].Text;
                    drNewRow["CADFees"] = GridAppTable.Rows[i].Cells[27].Text;
                    dtDatas.Rows.Add(drNewRow);
                    tono2++;
                }
                i++;
            }
            ViewState["dtapprove"] = dtDatas;
            GridApprove.DataSource = dtDatas;
            GridApprove.DataBind();
            lblToAmtFo2.Text = grdtotal2.ToString();
            lblAdmiSnFO2.Text = adno2.ToString();
            lblExamSnFo2.Text = eto2.ToString();
            lblToLateFo2.Text = latetotal2.ToString();
            lbltoFNo.Text = tono2.ToString();
            lblAddTotal.Text = addtotal.ToString();
            lblExamTotal.Text = examtotal.ToString();
            otherttl = (grdtotal2 - addtotal - examtotal) + latetotal2;
            lblotherTotal.Text = otherttl.ToString();
            lblAddForms.Text = addform.ToString();
            lblExamForms.Text = examform.ToString();
            lblReqAmt.Text = (Convert.ToDecimal(lblToAmtFo2.Text) + Convert.ToDecimal(lblToLateFo2.Text)).ToString();
            if (Convert.ToInt32(lblReqAmt.Text) > Convert.ToInt32(lblPAmt.Text))
            {
                chkUseLimit.Enabled = true;
                lblIMAmount.Text = lblPAmt.Text;
            }
            else
            {
                chkUseLimit.Enabled = false; chkUseLimit.Checked = false;
                lblIMAmount.Text = (Convert.ToInt32(lblReqAmt.Text)).ToString();
            }
            btnAddToApprovalTAble.Focus();
        }
        catch (FormatException ex)
        {
            lblExceptinApprovedTable.Text = "Invalid Date Format.";
            panelApproveNow.Visible = false;
        }
        catch (Exception ex)
        {
            lblExceptinApprovedTable.Text = "Invalid Value.";
            panelApproveNow.Visible = false;
        }
        finally
        {
            btnApprove.Enabled = true;
            btnSendDebitNote.Visible = false;
            btnPay.Visible = false;
            lblDuesAmt.Text = "0";
        }
    }
   
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ibtnExportPDFAppTable_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition",
         "attachment;filename=ApplicationAdded.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridApprove.AllowPaging = false;
        GridApprove.DataSource = (DataTable)ViewState["dtapprove"];
        GridApprove.DataBind();
        GridApprove.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    protected void ibtnExportExcelAppTable_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
        "attachment;filename=ApplicationFormAdded.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridApprove.AllowPaging = false;
        GridApprove.DataSource = (DataTable)ViewState["dtapprove"];
        GridApprove.DataBind();
        GridApprove.RenderControl(hw);
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void ibtnExportDocAppTable_click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
        "attachment;filename=ApplicationFormSerialNumber.doc");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-word ";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridApprove.AllowPaging = false;
        GridApprove.DataSource = (DataTable)ViewState["dtapprove"];
        GridApprove.DataBind();
        GridApprove.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = GridDiaryNo.SelectedRow.Cells[1].Text.ToString();
        getDiaryAmount();
        showcount(lblSessionHiddend.Text.ToString(), txtDiaryNo.Text.ToString());
    }
    private void showcount(string session, string dairy)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from DairyCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblADDRcv.Text = reader["ADDRcv"].ToString();
            lblADDSub.Text = reader["ADDSub"].ToString();
            lblODDRcv.Text = reader["ODDRcv"].ToString();
            lblODDSub.Text = reader["ODDSub"].ToString();
            lblAdmissionRcv.Text = reader["EnrollFormRcv"].ToString();
            lblAdmissionSub.Text = reader["EnrollFormSub"].ToString();
            lblExamRcv.Text = reader["ExamFormRcv"].ToString();
            lblExamSub.Text = reader["ExamFormSub"].ToString();
            lblITIRcv.Text = reader["ITIRcv"].ToString(); lblITISub.Text = reader["ITISub"].ToString();
            lblOthersFormRcv.Text = reader["OtherFormRcv"].ToString(); lblOthersFormSub.Text = reader["OtherFormSub"].ToString();
            lblProvisionalRcv.Text = reader["ProvisionalRcv"].ToString(); lblProvisionalSub.Text = reader["ProvisionalSub"].ToString();
            lblFinalPassRcv.Text = reader["FinalPassRcv"].ToString(); lblFinalPassSub.Text = reader["FinalPassSub"].ToString();
            lblReCheckingRcv.Text = reader["ReCheckingRcv"].ToString(); lblReCheckingSub.Text = reader["ReCheckingSub"].ToString();
            lblDuplicateRcv.Text = reader["DuplicateDocsRcv"].ToString(); lblDuplicateSub.Text = reader["DuplicateDocsSub"].ToString();
            lblMembershipRcv.Text = reader["MemberRcv"].ToString(); lblMembershipSub.Text = reader["MemberSub"].ToString();
            lblBooksRcv.Text = reader["BooksRcv"].ToString(); lblBooksSub.Text = reader["BooksSub"].ToString();
            lblProsRcv.Text = reader["ProspectusRcv"].ToString(); lblProsSub.Text = reader["ProspectusSub"].ToString();
        }
        reader.Close();
        cmd = new SqlCommand("select * from ProjectCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        SqlDataReader read;
        read = cmd.ExecuteReader();
        while (read.Read())
        {
            lblProjectRcv.Text = read["DDRcv"].ToString(); lblProjectSub.Text = read["DDSub"].ToString();
            lblProformaCRcv.Text = read["ProformaARcv"].ToString(); lblProformaCSub.Text = read["ProformaASub"].ToString();
            lblProformaBRcv.Text = read["ProformaBRcv"].ToString(); lblProformaBSub.Text = read["ProformaBSub"].ToString();
        }
        read.Close();
        con.Close();
        con.Dispose();
    }
    private void updateIMBooks(string course, string part, string imid, bool flg, SqlTransaction sTR, SqlCommand cmd)
    {
        int cI = 0, cII = 0, cIIE = 0, cA = 0, cB = 0, aI = 0, aII = 0, aIIE = 0, aA = 0, aB = 0;
        if (flg == true)
        {
            if (course == "Civil")
            {
                if (part == "PartI")
                    cI =  1;
                else if (part == "PartII")
                    cII =  1;
                else if (part == "SectionA")
                    cA =  1;
                else if (part == "SectionB")
                    cB =  1;
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
                    aIIE =  1;
            }
        }
        cmd.CommandText = "update IMBooks set CPartI=CPartI+'" + cI + "',CPartII=CPartII+'" + cII + "',CPartIIE=CPartIIE+'" + cIIE + "',CSectionA=CSectionA+'" + cA + "',CSectionB=CSectionB+'" + cB + "',APartI=APartI+'" + aI + "',APartII=APartII+'" + aII + "',APartIIE=APartIIE+'" + aIIE + "',ASectionA=ASectionA+'" + aA + "',ASectionB=ASectionB+'" + aB + "',CourseID='081' where IMID='" + imid + "'";
        cmd.ExecuteNonQuery();
    }
    protected void btnViewHold_Click(object sender, EventArgs e)
    {
        SqlDataAdapter adp;
        if (ddlAppstype.SelectedValue == "Project")
        {
            if (txtIMID.Text == "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (FormType='ProformaB' or FormType='ProformaC')  ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text == "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (FormType='ProformaB' or FormType='ProformaC')  ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account'  and (FormType='ProformaB' or FormType='ProformaC') ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = true;
            }
            else
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (FormType='ProformaB' or FormType='ProformaC')  ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
        }
        else if (ddlAppstype.SelectedValue == "AutoCAD")
        {
            if (txtIMID.Text == "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (FormType='MCADLateFee' or FormType='MCADRegistration') ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text == "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (FormType='MCADLateFee' or FormType='MCADRegistration')  ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account'  and (FormType='MCADLateFee' or FormType='MCADRegistration') ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = true;
            }
            else
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (FormType='MCADLateFee' or FormType='MCADRegistration')  ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
        }
        else if (ddlAppstype.SelectedValue == "Adacemic")
        {
            if (txtIMID.Text == "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0) ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text == "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0)  ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0) ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = true;
            }
            else
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0) ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
        }
        else 
        {
            if (txtIMID.Text == "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0) ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text == "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0)  ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
            else if (txtIMID.Text != "" && txtDiaryNo.Text != "")
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where IMID='" + txtIMID.Text.ToString() + "' and DNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0) ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = true;
            }
            else
            {
                adp = new SqlDataAdapter("select Lavel,IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,UnderAge,CADFees,DupForm from AppRecord where Session='" + lblSessionHiddend.Text.ToString() + "' and Status='Hold' and ApprovedBy='Account' and (Exempted!=0 or AdmissionFees!=0 or CompositeFees!=0 or AnnualSubFees!=0 or ITIFees!=0 or ExamFees!=0 or DupForm!=0) ORDER BY Lavel", con);
                btnAddToApprovalTAble.Enabled = false;
            }
        }
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridAppTable.DataSource = dt;
        GridAppTable.DataBind();
        btnApprove.Enabled = true;
        btnSelectAll.Focus();
    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            con.Close(); con.Open();
            while (i < GridAppTable.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GridAppTable.Rows[i].FindControl("chkapp");
                if (rbApp.Checked)
                {
                    cmd = new SqlCommand("update AppRecord set Status='Hold' where AppNo='" + Convert.ToInt32(GridAppTable.Rows[i].Cells[4].Text.ToString()) + "'", con);
                    cmd.ExecuteNonQuery();
                }
                i++;
            }
            con.Close(); con.Dispose();
            if (i > 0)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Selected Forms on Hold')", true);
        }
        catch (SqlException ex)
        {
        }
    }
    protected void Gid()
    {
        ddlGid.Items.Clear();
        con.Close(); con.Open();    
        DataTable dt = new DataTable();
        dt = IMAC.GroupMate(txtIMID.Text);
        ddlGid.DataSource = dt;       
        ddlGid.DataTextField = "IMID";
        ddlGid.DataValueField = "IMID";
        ddlGid.DataBind();    
        ddlGid.SelectedValue = txtIMID.Text;      
        con.Close();
    }
    protected void ddlGid_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataReader reader = null;
        string strid = ddlGid.SelectedItem.Text;
        con.Close();
        con.Open();
        cmd = new SqlCommand();
        SqlTransaction sTR;
        sTR = con.BeginTransaction();
        cmd.Transaction = sTR;
        cmd.Connection = con;
        try
        {
            imamount(sTR, cmd, reader, strid);
            sTR.Commit();
        }
        catch (Exception ex)
        {
            sTR.Rollback();
        }
        getDebitNote(con);
        con.Close(); con.Dispose();              
    }
}