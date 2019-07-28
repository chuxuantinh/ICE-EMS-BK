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

public partial class Acc_MembershipAC : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    public string status; string ddno;
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    clsIMAC imac;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnlid.Visible = false;
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                panelSubmitAmt.Visible = false;
                panelSubmitamount.Visible = false;
                panelReNew.Visible = true;
                panelspace.Visible = true;
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSubscribe.Enabled = false;
                txtAmt.Text = "";
                txtIMID.Focus();
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void ibtnRenewal_click(object sender, EventArgs e)
    {
        panelReNew.Visible = true;
    }
    protected void ibtnNewIDApproval_Click(object sender, EventArgs e)
    {
        panelReNew.Visible = false;
    }
    protected void lbtnViewAC_Click(object sender, EventArgs e)
    {
        pnlid.Visible = true;
        fillgr();
        if (GridDiaryNo.Rows.Count > 0)
        {
            try
            {
                dtinfo.ShortDatePattern = "dd/MM/yyyy";
                dtinfo.DateSeparator = "/";
                con.Close(); con.Open();
                cmd = new SqlCommand("select max(TransID) from  MemberFee where ID='" + txtId.Text.ToString() + "'", con);
                string tid = Convert.ToString(cmd.ExecuteScalar());
                if (tid == "") tid = "0";
                cmd = new SqlCommand("select  TransType, Balance from  MemberFee where ID='" + txtId.Text.ToString() + "' and TransID='" + Convert.ToInt32(tid.ToString()) + "'", con);
                SqlDataReader rdr;
                string ttype = "";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ttype = rdr["TransType"].ToString();
                    if (ttype == "Debit")
                    {
                        lblBalanceType.Text = "Debit";
                    }
                    else lblBalanceType.Text = "Credit";
                    lblBalance.Text = rdr["Balance"].ToString().TrimEnd('0').TrimEnd('.');
                }
                rdr.Close();
                cmd = new SqlCommand("select * from Member where ID='" + txtId.Text.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblName.Text = reader[1].ToString();
                    lblMemberTyep.Text = reader["Type"].ToString();
                    lblID.Text = reader[2].ToString();
                    lblAddress.Text = reader[7].ToString();
                    lblCity.Text = reader[8].ToString();
                    lblEmail.Text = reader[14].ToString();
                    lblPhonne.Text = reader[10].ToString();
                    lblMobile.Text = reader[13].ToString();
                    lblMemberType.Text = lblMemberTyep.Text.ToString();
                    lblSubscriptionDate.Text = Convert.ToDateTime(reader["RenewDate"].ToString()).ToString("dd/MM/yyyy");
                    lblSubFrom.Text = reader["YearFrom"].ToString();
                    lblSubTo.Text = reader["YearTo"].ToString();
                    lblRegistrationdAte.Text = Convert.ToDateTime(reader["RegDate"].ToString()).ToString("dd/MM/yyyy");
                    if (reader[71].ToString() == "yes" | reader[71].ToString() == "Active")
                    {
                        lblStatus.Text = "Active";
                    }
                    else lblStatus.Text = "DisActive";
                    panelspace.Visible = false;
                    ddlAmtType.Focus();

                }
                reader.Close();
                SqlDataAdapter ad = new SqlDataAdapter("select SubDate as Date,DD as DDNo,Bank,YearFrom as Session,Amt as Amount,TransType,Balance from MemberFee where ID='" + txtId.Text.ToString() + "' ORDER BY TransID DESC", con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (lblID.Text == "")
                    lblException.Text = "Invalid ID No.";
                else
                    panelSubmitAmt.Visible = true;
            }
            catch (SqlException ex)
            {
                lblException.Text = ex.ToString();
            }
            finally
            {
                con.Close();
                btnSubscribe.Visible = true;
                panelSubmitamount.Visible = true;
                getMasterFee();
                lblDDNNO.Text = "DD No:";
                lblAccountNo.Text = "Account No:";
                txtDDNO.Visible = true; txtACNO.Visible = true;
                lblTAmt.Text = "0";
                con.Dispose();
            }
        }
        else lblMessage.Text = "Diary No Found.";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row; panelspace.Visible = false;
        row = GridView1.SelectedRow;
        lblTitleInfo.Text = "Membership Id: " + row.Cells[6].Text;
        panelSubmitamount.Visible = true;
        btnSubscribe.Visible = false;
        Session["FeeID"] = row.Cells[6].Text;
        lblMemberType.Text = row.Cells[5].Text;
        getMasterFee();
        lblDDNNO.Text = "DD No:";
        lblAccountNo.Text = "Account No:";
        txtDDNO.Visible = true; txtACNO.Visible = true;
        lblTAmt.Text = "1";
    }
    protected void btnSubscribe_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            con.Close(); con.Open();
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            if (lblmemRcv.Text == "") Label1.Text = "Please Select Diary.";
            else
            {
                if (Convert.ToInt32(lblmemRcv.Text) > Convert.ToInt32(lblmemSub.Text))
                {
                        Label1.Text = "";
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2 = new SqlCommand("select max(TransID) from  MemberFee where ID='" + txtId.Text.ToString() + "'", con);
                        string tid = Convert.ToString(cmd2.ExecuteScalar());
                        if (tid == "") tid = "0";
                        cmd2 = new SqlCommand("select  TransType, Balance from  MemberFee where ID='" + txtId.Text.ToString() + "' and TransID='" + Convert.ToInt32(tid.ToString()) + "'", con);
                        SqlDataReader reader;
                        string ttype = "", typ = "";
                        reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            ttype = reader["TransType"].ToString();
                            lblBalance.Text = reader["Balance"].ToString().TrimEnd('0').TrimEnd('.');
                        }
                        reader.Close();
                        int bl, amt;
                        if (lblBalance.Text == "") bl = 0;
                        else
                            bl = Convert.ToInt32(lblBalance.Text);
                        amt = Convert.ToInt32(lblTAmt.Text);
                        if (ttype == "Debit" && bl > amt)
                        {
                            typ = "Debit";
                            lblBalance.Text = (bl - amt).ToString();
                        }
                        else if (ttype == "Debit" && bl <= amt)
                        {
                            typ = "Credit";
                            lblBalance.Text = (amt - bl).ToString();
                        }
                        else if (ttype == "Credit")
                        {
                            typ = "Credit";
                            lblBalance.Text = (amt + bl).ToString();
                        }
                        else if (ttype == "")
                            typ = "Credit";
                        cmd2 = new SqlCommand("insert into MemberFee (MType, ID, Amt, FeeType, SubDate, SubType, AcountNo, DD, Bank, YearFrom, YearTo,TransType,Balance,TransID) values(@MType, @ID, @Amt, @FeeType, @SubDate, @SubType, @AcountNo, @DD, @Bank, @YearFrom, @YearTo, @TransType, @Balance,@TransID)", con);
                        cmd2.Parameters.AddWithValue("@MType", lblMemberType.Text.ToString());
                        cmd2.Parameters.AddWithValue("@ID", txtId.Text.ToString());
                        cmd2.Parameters.AddWithValue("@Amt", lblTAmt.Text.ToString());
                        cmd2.Parameters.AddWithValue("@FeeType", txtNarration.Text.ToString());
                        cmd2.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                        cmd2.Parameters.AddWithValue("@SubType", ddlAmtType.SelectedValue.ToString());
                        cmd2.Parameters.AddWithValue("@AcountNo", txtACNO.Text.ToString());
                        cmd2.Parameters.AddWithValue("@DD", txtDDNO.Text.ToString());
                        cmd2.Parameters.AddWithValue("@Bank", ddlBank.SelectedValue.ToString());
                        cmd2.Parameters.AddWithValue("@YearFrom", lblSessionHiddend.Text.ToString());
                        cmd2.Parameters.AddWithValue("@YearTo", "");
                        cmd2.Parameters.AddWithValue("@TransType", typ.ToString());
                        cmd2.Parameters.AddWithValue("@Balance", lblBalance.Text.ToString());
                        cmd2.Parameters.AddWithValue("@TransID", tid + 1);
                        cmd2.ExecuteNonQuery();
                        ClsAccount cl = new ClsAccount();
                        //Account
                        cl.AmountSubmit(txtId.Text.ToString(), GridDiaryNo.SelectedRow.Cells[1].Text.ToString(), Convert.ToDateTime(txtDate.Text, dtinfo), "Credit", lblTAmt.Text.ToString(), lblSessionHiddend.Text.ToString(), txtDDNO.Text.ToString() + ":" + ddlBank.SelectedValue.ToString());
                        //IMAC
                        imac = new clsIMAC();
                        // Submit Amount in IMAccount 
                        imac.submitAmount(txtId.Text,Convert.ToInt32(txtAmt.Text), ddlAccountType.SelectedValue.ToString(), con);
                        // amountupdate(txtId.Text.ToString(), Convert.ToInt32(lblTAmt.Text));
                        //FeeAC
                        cmd = new SqlCommand("insert into FeeAC(IMID,Amt,AmtType,AmtFor,SubDate,DDNO,Bank,Narration,CurrancyName,CurrancyValue,Session,DiaryNo,DDDate) values(@IMID,@Amt,@AmtType,@AmtFor,@SubDate,@DDNO,@Bank,@Narration,@CurrancyName,@CurrancyValue,@Session,@DiaryNo,@DDDate)", con);
                        cmd.Parameters.AddWithValue("@IMID", txtId.Text.ToString());
                        cmd.Parameters.AddWithValue("@Amt", txtAmt.Text);
                        cmd.Parameters.AddWithValue("@AmtType", ddlAmtType.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@AmtFor", "Membership");
                        cmd.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                        cmd.Parameters.AddWithValue("@DDNO", txtDDNO.Text.ToString());
                        cmd.Parameters.AddWithValue("@Bank", ddlBank.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@Narration", txtNarration.Text.ToString());
                        cmd.Parameters.AddWithValue("@CurrancyName", ddlCurrancy.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@CurrancyValue", txtCurrancyValue.Text);
                        cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text.ToString());
                        cmd.Parameters.AddWithValue("@DiaryNo", GridDiaryNo.SelectedRow.Cells[1].Text.ToString());
                        cmd.Parameters.AddWithValue("@DDDate", Convert.ToDateTime(txtDOB.Text, dtinfo));
                        cmd.ExecuteNonQuery();
                        updateCount(lblSessionHiddend.Text, extra.Text);
                        countDD(lblSessionHiddend.Text, extra.Text);
                        Log.WriteLog(Request.QueryString["maikal"], "B003", lblID.Text.ToString() + ":" + GridDiaryNo.SelectedRow.Cells[1].Text.ToString(), ddlBank.SelectedValue.ToString(), txtDDNO.Text.ToString(), lblTAmt.Text.ToString(), "Membership DD to Im Account");
                        Log.WriteLog("B003", Request.QueryString["maikal"], lblID.Text.ToString() + ":" + GridDiaryNo.SelectedRow.Cells[1].Text.ToString(), ddlBank.SelectedValue.ToString(), txtDDNO.Text.ToString(), lblTAmt.Text.ToString(), "Membership DD to Im Account");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Amount Successfully Submitted.')", true);
                        flag = true; lblTAmt.Text = "0";
                        txtAmt.Text = ""; txtDDNO.Text = ""; txtDate.Text = ""; txtNarration.Text = ""; txtACNO.Text = ""; txtCurrancyValue.Text = "1"; ddlCurrancy.SelectedIndex = 0;
                }
                else
                {
                    Label1.Text = "All DD submitted";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('All Membership DD Submitted.')", true);
                }
            }
            SqlDataAdapter ad = new SqlDataAdapter("select SubDate as Date,DD as DDNo,Bank,YearFrom as Session,Amt as Amount,TransType,Balance from MemberFee where ID='" + txtId.Text.ToString() + "' ORDER BY TransID DESC", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            btnSubscribe.Enabled = false;
            con.Close();
            con.Dispose();
        }
        catch (FormatException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Enter Details in Correct Format.')", true);
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Incorrect Data.')", true);

        }
        finally
        {
            btnSubscribe.Enabled = false;
            txtAmt.Text = "";
        }
    }
    protected void ddlAmtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAmtType.SelectedValue == "DD")
        {
            lblDDNNO.Text = "DD No:";
            lblAccountNo.Text = "Account No:";
            txtDDNO.Visible = true; txtACNO.Visible = true;
        }
        else if (ddlAmtType.SelectedValue == "Cash")
        {
            lblDDNNO.Text = ""; lblAccountNo.Text = ""; txtACNO.Text = ""; txtDDNO.Text = "";
            txtDDNO.Visible = false; txtACNO.Visible = false;
        }
        else if (ddlAmtType.SelectedValue == "CC")
        {
            lblDDNNO.Text = ""; txtDDNO.Visible = false; txtDDNO.Text = "";
            lblAccountNo.Text = "Account No:"; txtACNO.Visible = true;
        }
        else if (ddlAmtType.SelectedValue == "Online")
        {
            lblDDNNO.Text = "Transection No:"; txtDDNO.Visible = true;
            lblAccountNo.Text = "Account No:"; txtACNO.Visible = true;
        }
        ddlAmtType.Focus();
    }
    public string imgenid()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(SN) from Member", con);
        con.Close();
        con.Open();
        string id;
        int i = Convert.ToInt32(cmdsn.ExecuteScalar().ToString());
        i = i + 1;
        if (i <= 9)
            id = "000" + i;
        else if (i <= 99)
            id = "00" + i;
        else if (i <= 999)
            id = "0" + i;
        else
            id = Convert.ToString(i + 1);
        id = "ICE" + id.ToString() + "I";
        return id;
    }   
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtAmt.Text = "";
        txtNarration.Text = "";
        txtACNO.Text = "";
        txtDDNO.Text = "";
        txtCurrancyValue.Text = "1";
        ddlCurrancy.SelectedValue = "RS";
    }
    public void getMasterFee()
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from MemberFeeMaster where MemberType='" + lblMemberType.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblEnrollFee.Text = reader[2].ToString().TrimEnd('0');
            lblSubFee.Text = reader[3].ToString().TrimEnd('0');
            lblEnrollFee.Text = lblEnrollFee.Text.TrimEnd('.');
            lblSubFee.Text = lblSubFee.Text.TrimEnd('.');
        }
        else
        {
            panelSubmitAmt.Visible = false;
            panelSubmitamount.Visible = false;
            panelReNew.Visible = false;
            panelspace.Visible = true;
        }
        reader.Close();
        con.Close();
    }
    decimal stsamt = 0;
    protected void txtAmt_TextChanged(object sender, EventArgs e)
    {
        if (txtAmt.Text == "") txtAmt.Text = "1"; if (txtCurrancyValue.Text == "") txtCurrancyValue.Text = "1";
        lblTAmt.Text = txtAmt.Text.ToString();
        lblTAmt.Visible = true;
        double amt = Convert.ToDouble(txtAmt.Text);
        double camt = Convert.ToDouble(txtCurrancyValue.Text);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
        string[] desai = chkamt(lblEnrollFee.Text,lblTAmt.Text);       
        txtAmt.Focus();
    }
    protected void txtCurrancyValue_TextChanged(object sender, EventArgs e)
    {
        if (txtAmt.Text == "") txtAmt.Text = "1"; if (txtCurrancyValue.Text == "") txtCurrancyValue.Text = "1";
        lblTAmt.Visible = true;
        double amt = Convert.ToDouble(txtAmt.Text);
        double camt = Convert.ToDouble(txtCurrancyValue.Text);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
        string[] desai = chkamt(lblEnrollFee.Text,lblTAmt.Text);
        txtCurrancyValue.Focus();
    }
    private void calculate(string amtt, string camtt)
    {
        double amt = Convert.ToDouble(amtt);
        double camt = Convert.ToDouble(camtt);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
    }
    protected void ddlCurrancy_Changed(object sender, EventArgs e)
    {
        if (ddlCurrancy.SelectedValue == "RS")
        {
            lblCurrancyText.Visible = false;
            lblCurrancyName.Visible = false;
            txtCurrancyValue.Visible = false;
            txtCurrancyValue.Text = "1";
            calculate(txtAmt.Text, txtCurrancyValue.Text);
        }
        else if (ddlCurrancy.SelectedValue == "DL")
        {
            lblCurrancyName.Text = " One Dolar Equal To";
            lblCurrancyText.Text = " Currency Value:";
            lblCurrancyText.Visible = true;
            lblCurrancyName.Visible = true;
            txtCurrancyValue.Visible = true;
        }
        else if (ddlCurrancy.SelectedValue == "OT")
        {
            lblCurrancyName.Text = " One Unit Equal To";
            lblCurrancyText.Text = " Currency Value:";
            lblCurrancyText.Visible = true;
            lblCurrancyName.Visible = true;
            txtCurrancyValue.Visible = true;
        }
        else
        {
        }
        txtCurrancyValue.Focus();
    }
    private string[] chkamt(string enrolfee,string tamt)
    {
        int amt1, amt2, dif;
        string[] tejal = new string[4];
        amt1 = Convert.ToInt32(enrolfee);
        amt2 = Convert.ToInt32(tamt);
        tejal[0] = amt1.ToString();
        tejal[1] = amt2.ToString();
        if (amt2 < amt1)
        {
            dif = amt1 - amt2;
            tejal[2]= "Debit"; tejal[3] = dif.ToString();
        }
        else if (amt2 > amt1)
        {
            dif = amt2 - amt1;
            tejal[2] = "Credit"; tejal[3] = dif.ToString();
        }
        else if (amt2 == amt1)
        {
            dif = amt1 - amt2; stsamt = dif;
            tejal[2] = "Equal"; tejal[3] = dif.ToString();
        }
        return tejal;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.TrimEnd('0').TrimEnd('.');
            e.Row.Cells[0].Text = Convert.ToDateTime(e.Row.Cells[0].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void btnViewAC_Click(object sender, EventArgs e)
    {
        pnlid.Visible = true;
        txtId.Text = txtIMID.Text.ToString();
         fillgr();
         if (GridDiaryNo.Rows.Count > 0)
         {
             try
             {
                 txtId.Text = txtIMID.Text;
                 dtinfo.ShortDatePattern = "dd/MM/yyyy";
                 dtinfo.DateSeparator = "/";
                 con.Close(); con.Open();
                 cmd = new SqlCommand("select max(TransID) from  MemberFee where ID='" + txtIMID.Text.ToString() + "'", con);
                 string tid = Convert.ToString(cmd.ExecuteScalar());
                 if (tid == "") tid = "0";
                 cmd = new SqlCommand("select TransType, Balance from  MemberFee where ID='" + txtIMID.Text.ToString() + "' and TransID='" + Convert.ToInt32(tid.ToString()) + "'", con);
                 SqlDataReader rdr;
                 string ttype = "";
                 rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     ttype = rdr["TransType"].ToString();
                     if (ttype == "Debit")
                     {
                         lblBalanceType.Text = "Debit";
                     }
                     else lblBalanceType.Text = "Credit";
                     lblBalance.Text = rdr["Balance"].ToString().TrimEnd('0').TrimEnd('.');
                 }
                 rdr.Close();
                 cmd = new SqlCommand("select * from Member where ID='" + txtIMID.Text.ToString() + "'", con);
                 SqlDataReader reader;
                 reader = cmd.ExecuteReader();
                 while (reader.Read())
                 {
                     lblName.Text = reader[3].ToString();
                     lblMemberTyep.Text = reader["Type"].ToString();
                     lblID.Text = reader[2].ToString();
                     lblAddress.Text = reader[7].ToString();
                     lblCity.Text = reader[8].ToString();
                     lblEmail.Text = reader[14].ToString();
                     lblPhonne.Text = reader[10].ToString();
                     lblMobile.Text = reader[13].ToString();
                     lblMemberType.Text = lblMemberTyep.Text.ToString();
                     lblSubscriptionDate.Text = Convert.ToDateTime(reader["RenewDate"].ToString()).ToString("dd/MM/yyyy");
                     lblSubFrom.Text = reader["YearFrom"].ToString();
                     lblSubTo.Text = reader["YearTo"].ToString();
                     lblRegistrationdAte.Text = Convert.ToDateTime(reader["RegDate"].ToString()).ToString("dd/MM/yyyy");
                     if (reader[71].ToString() == "yes" | reader[71].ToString() == "Active")
                         lblStatus.Text = "Active";
                     else lblStatus.Text = "DisActive";
                     panelspace.Visible = false;
                     ddlAmtType.Focus();
                 }
                 reader.Close();
                 SqlDataAdapter ad = new SqlDataAdapter("select SubDate as Date,DD as DDNo,Bank,YearFrom as Session,Amt as Amount,TransType,Balance from MemberFee where ID='" + txtIMID.Text.ToString() + "' ORDER BY TransID DESC", con);
                 DataTable dt = new DataTable();
                 ad.Fill(dt);
                 GridView1.DataSource = dt;
                 GridView1.DataBind();
                 if (lblID.Text == "")
                     lblException.Text = "Invalid ID No.";
                 else
                     panelSubmitAmt.Visible = true;
             }
             catch (SqlException ex)
             {
                 lblException.Text = ex.ToString();
             }
             finally
             {
                 con.Close();
                 btnSubscribe.Visible = true;
                 panelSubmitamount.Visible = true;
                 getMasterFee();
                 lblDDNNO.Text = "DD No:";
                 lblAccountNo.Text = "Account No:";
                 txtDDNO.Visible = true; txtACNO.Visible = true;
                 lblTAmt.Text = "0";
                 con.Dispose();
             }
         }
         else
             lblMessage.Text = "Diary Not Found.";
    }
    protected void txtDateSub_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            int[] diff = new int[3];
            DateTime dt = Convert.ToDateTime(txtDOB.Text, dtinfo);
            DateTime now = Convert.ToDateTime(txtDate.Text, dtinfo);
            diff = chkdob(now, dt);
            if (diff[0] == 0 & diff[1] == 0 & diff[2] == 100)
            {
                lblExceptiondAte.Text = "DD Date is earlier than Current Date.";
                btnSubscribe.Enabled = false;
            }
            else
            {
                if (diff[0] > 0)
                {
                    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
                    btnSubscribe.Enabled = false;
                }
                else if (diff[1] > 2)
                {
                    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
                    btnSubscribe.Enabled = false;
                }
                else if (diff[1] == 2 & diff[2] > 25)
                {
                    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
                    btnSubscribe.Enabled = false;
                }
                else
                {
                    btnSubscribe.Enabled = true;
                    lblExceptiondAte.Text = "";
                }
            }
            if (ddlAmtType.SelectedValue.ToString() != "DD")
            {
                btnSubscribe.Enabled = true;
            }
        }
        catch (FormatException ex)
        {
            lblExceptiondAte.Text = "Invalid Submission Date Format.";
        }
        txtNarration.Focus();
    }
    protected void txtDate_TechChanged(object sender, EventArgs e)
    {
        try
        {
            DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            int[] diff = new int[3];
            DateTime dt = Convert.ToDateTime(txtDOB.Text, dtinfo);
            DateTime now = Convert.ToDateTime(txtDate.Text, dtinfo);
            diff = chkdob(now, dt);
            if (diff[0] == 0 & diff[1] == 0 & diff[2] == 100)
            {
                lblExceptiondAte.Text = "DD Date is earlier than Current Date.";
                btnSubscribe.Enabled = false;
            }
            else
            {
                if (diff[0] > 0)
                {
                    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
                    btnSubscribe.Enabled = false;
                }
                else if (diff[1] > 2)
                {
                    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
                    btnSubscribe.Enabled = false;
                }
                else if (diff[1] == 2 & diff[2] > 25)
                {
                    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
                    btnSubscribe.Enabled = false;
                }
                else
                {
                    btnSubscribe.Enabled = true;
                    lblExceptiondAte.Text = "";
                }
            }
            if (ddlAmtType.SelectedValue.ToString() != "DD")
            {
                btnSubscribe.Enabled = true;
            }
        }
        catch (FormatException ex)
        {
            lblExceptiondAte.Text = "Invalid Submission Date Format.";
        }
        txtNarration.Focus();
    }
    private int[] chkdob(DateTime now, DateTime dt)
    {
        int[] dif = new int[3];
        int mo, dy;
        if (dt.Year == now.Year & now.Month == dt.Month & dt.Day > now.Day)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else if (dt.Year == now.Year & dt.Month > now.Month)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else if (dt.Year > now.Year)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else
        {
            int yr = now.Year - dt.Year;
            if (now.Month < dt.Month || now.Month == dt.Month && now.Day < dt.Day)
            {
                --yr;
            }
            dif[0] = yr;
            if (now.Month < dt.Month)
            {
                mo = (12 - dt.Month) + now.Month;
                if (now.Day < dt.Day)
                    --mo;
            }
            else
            {
                mo = now.Month - dt.Month;
                if (now.Month == dt.Month & now.Day < dt.Day)
                {
                    --mo;
                }
            }
            dif[1] = mo;
            if (now.Day < dt.Day)
            {
                if (now.Month == 1)
                {
                    int ddy = DateTime.DaysInMonth(now.Year, now.Month);
                    dy = (ddy - dt.Day) + now.Day;
                }
                else
                {
                    int ddy = DateTime.DaysInMonth(now.Year, now.Month - 1);
                    dy = (ddy - dt.Day) + now.Day;
                }
            }
            else
            {
                dy = now.Day - dt.Day;
            }
            dif[2] = dy;
        }
        return dif;
    }
    //public void amountupdate(string imidi, int amount)
    //{
    //    try
    //    {
    //            string str1 = "update  IMAC set Total=Total+'"+amount+"' where IMID='" + imidi + "'";
    //            cmd = new SqlCommand(str1, con);
    //            cmd.ExecuteNonQuery();
    //    }
    //    catch (IndexOutOfRangeException ex)
    //    {
    //        Response.Write(ex.ToString());
    //    }
    //    catch (SqlException ex)
    //    {
    //        Response.Write(ex.ToString());
    //    }
    //    finally
    //    {
    //    }
    //}
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubscribe.Enabled = true;
        ddno = GridDiaryNo.SelectedRow.Cells[1].Text.ToString();
        con.Close(); con.Open();
        cmd = new SqlCommand("select Status from DiaryEntry where DiaryNo='" + ddno + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "'", con);
        string strdno = Convert.ToString(cmd.ExecuteScalar());
        extra.Text=ddno;
        con.Close();
        countDD(lblSessionHiddend.Text.ToString(), ddno);
        ddlAmtType.Focus();
        con.Dispose();
    }
    private void countDD(string session, string dairy)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from DairyCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblBookRcv.Text = reader["BooksRcv"].ToString();
            lblBookSub.Text = reader["BooksSub"].ToString();
            lblProspectusRcv.Text = reader["ProspectusRcv"].ToString();
            lblProspectusSub.Text = reader["ProspectusSub"].ToString();
            lblmemRcv.Text = reader["MemberRcv"].ToString();
            lblmemSub.Text = reader["MemberSub"].ToString();
            lblTDDRcv.Text = reader["TotalDDRcv"].ToString();
            lblTDDSub.Text = reader["TotalDDSub"].ToString();
        }
        reader.Close();
        con.Close();
        con.Close();
    }
    protected void GridDiaryNo_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.Cells[1].Text = "Diary No";
    }
    private void fillgr()
    {
        SqlDataAdapter addiary = new SqlDataAdapter("select DairyNo from DairyCount where MemberRcv>0 and MemberRcv>MemberSub and IMID='" + txtId.Text + "'", con);
        DataSet ds = new DataSet();
        addiary.Fill(ds);
        if (ds.Tables[0].ToString() != "")
        {
            GridDiaryNo.DataSource = ds;
            GridDiaryNo.DataBind();
        }
    }
    private void updateCount(string session, string dairy)
    {
            SqlCommand cmd4;
            cmd4 = new SqlCommand("update DairyCount set MemberSub=@MemberSub,TotalDDSub=@TotalSub where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
            cmd4.Parameters.AddWithValue("@MemberSub",Convert.ToInt32(lblmemSub.Text)+1);
            cmd4.Parameters.AddWithValue("@TotalSub",Convert.ToInt32(lblTDDSub.Text)+1);
            cmd4.ExecuteNonQuery();
    }
}