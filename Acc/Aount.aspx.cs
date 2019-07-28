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


public partial class Acc_Aount : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataReader reader;
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win"; 
                txtSession.Text = DateTime.Now.Year.ToString();
                lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                lblDDNNO.Text = "DD No:";
                lblAccountNo.Text = "Diary No:";
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                pnlIMInfo.Visible = false;
                txtDDNO.Visible = true; txtACNO.Visible = true;
                lblCurrancyName.Visible = false;
                txtCurrancyValue.Visible = false;
                totalamt.Visible = false; panelCourier.Visible = false; panelAmtFor.Visible = false;
                fillDiary();
                ddlsession.Focus();
                LoadDropdownList();
            }
        }
        catch (NullReferenceException ex)
        {
            txtIDIM.Enabled = false;
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    private void LoadDropdownList()
    {
        try
        {
            DataSet dsHeader = new DataSet();
            ddlAmountHeader.Items.Add("-- Select --");
            dsHeader.ReadXml(Server.MapPath("~/XML/AmountHeader.xml"));
            ddlAmountHeader.DataSource = dsHeader;
            ddlAmountHeader.DataTextField = "Aname";
            ddlAmountHeader.DataBind();
            //ddlAmountHeader.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlAmountHeader.DataSource = dsHeader;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    private void fillDiary()
    {
        SqlDataAdapter addiary = new SqlDataAdapter("select DairyCount.DairyNo,DiaryEntry.IMID,DiaryEntry.MemberType from DairyCount left outer join DiaryEntry on  DairyCount.DairyNo=DiaryEntry.DiaryNo where DiaryEntry.ExamSession='" + lblhiddenSession.Text.ToString() + "' and DiaryEntry.Status='Open' and (DairyCount.TotalDDRcv > (DairyCount.TotalDDSub+DairyCount.MemberRcv)) order by DairyCount.DairyNo desc", con);
        DataSet ds = new DataSet();
        addiary.Fill(ds);
        if (ds.Tables[0].ToString() != "")
        {
            GridDiaryNo.DataSource = ds;
            GridDiaryNo.DataBind();
        }
    }
    //protected void total(string IMID,SqlTransaction sTR,SqlCommand cmd)
    //{
    //    cmd.CommandText = "select * from IMAC where IMID='"+IMID+"'";
    //    reader = cmd.ExecuteReader();
    //    while(reader.Read())
    //    {
    //        lblTotalAmount.Text = reader["Total"].ToString().TrimEnd('0').TrimEnd('.');  //total
    //        lblBooksAmount.Text = reader["IMTotal"].ToString().TrimEnd('0').TrimEnd('.'); // Books
    //        lblProspectus.Text = reader["Prospectus"].ToString().TrimEnd('0').TrimEnd('.'); // Prospectus
    //    }
    //    reader.Close();
    //}
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
        finally
        {
        }
    }
    protected void txtAmt_textChanged(object sender, EventArgs e)
    {
        totalamt.Visible = true;
        if (txtAmt.Text == "") txtAmt.Text = "1"; if (txtCurrancyValue.Text == "") txtCurrancyValue.Text = "1";
        lblTAmt.Text = txtAmt.Text.ToString();
        lblTAmt.Visible = true;
        double amt = Convert.ToDouble(txtAmt.Text);
        double camt = Convert.ToDouble(txtCurrancyValue.Text);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
        ddlCurrancy.Focus();
    }

    protected void txtIDIM_TextChanged(object sender, EventArgs e)
    {
        lblException.Text = "";
        con.Close(); con.Open();
        cmd = new SqlCommand();
        SqlTransaction sTR;
        sTR = con.BeginTransaction();
        cmd.Connection = con;
        cmd.Transaction = sTR;
        try
        {
            int i = 0;
                cmd.CommandText = "select * from IM where ID='" + txtIDIM.Text.ToString() + "'";
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblIMName.Text = reader[1].ToString();
                    lblIMAddress.Text = reader[3].ToString();
                    lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                    lblEnrolment.Text = reader["ID"].ToString();
                    lblGroupID.Text = reader["GID"].ToString();
                    btnSubmitAmt.Enabled = true;
                    pnlIMInfo.Visible = true;
                    i += 1;
                }
                else
                {
                    txtIDIM.Text = "Invalid ID"; lblIMAddress.Text = ""; lblIMName.Text = "";
                    lblIMCity.Text = "Please Insert Valid IM ID."; pnlIMInfo.Visible = false;
                    txtIDIM.Focus();
                }
                reader.Close();
                if (i == 1)
                {
                   // total(txtIDIM.Text, sTR, cmd);
                    txtACNO.Text = ""; lblexceptionDirary.Text = "";
                    cmd.CommandText = "select DairyCount.DairyNo,DairyCount.IMID,DiaryEntry.MemberType from DairyCount left outer join DiaryEntry on  DairyCount.DairyNo=DiaryEntry.DiaryNo where DairyCount.TotalDDRcv > (DairyCount.TotalDDSub+DairyCount.MemberRcv) and DiaryCount.Session='" + lblhiddenSession.Text + "' and DiaryEntry.IMID='" + txtIDIM.Text + "' order by DairyCount.DairyNo desc";
                    //cmd.CommandText = "select DiaryNo,MembershipNo,MemberType From DiaryEntry where IMID='" + txtIDIM.Text.ToString() + "' and ExamSession='" + lblhiddenSession.Text.ToString() + "' and Status='Open' order by DiaryNo desc";
                    reader = cmd.ExecuteReader();
                    GridDiaryNo.DataSource = reader;
                    GridDiaryNo.DataBind();
                    reader.Close();
                    reader.Dispose();
                    sTR.Commit();
                }
            txtACNO.Focus();
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
    protected void ddlAmtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanBank.Visible = true;
        lblException.Text = "";
        if (ddlAmtType.SelectedValue == "DD")
        {
            lblDDNNO.Text = "DD No:";
            lblAccountNo.Text = "Diary No:";
            txtDDNO.Visible = true; txtACNO.Visible = true; txtDDNO.Focus();
        }
        else if (ddlAmtType.SelectedValue == "Cash")
        {
            lblDDNNO.Text = ""; txtDDNO.Text = "";
            txtDDNO.Visible = false; txtDDNO.Text = "Cash";
            ddlBank.Focus();
            PanBank.Visible = false;
        }
        else if (ddlAmtType.SelectedValue == "CC")
        {
            lblDDNNO.Text = "Cheque No."; txtDDNO.Visible = true; txtDDNO.Text = "";
            lblAccountNo.Text = "Diary No:"; txtACNO.Visible = true; txtDDNO.Focus();
        }
        ddlAmtType.Focus();
    }
    protected void txtCurrancyValue_TextChanged(object sender, EventArgs e)
    {
        if (txtAmt.Text == "") txtAmt.Text = "1"; if (txtCurrancyValue.Text == "") txtCurrancyValue.Text = "1";
        lblTAmt.Visible = true;
        double amt = Convert.ToDouble(txtAmt.Text);
        double camt = Convert.ToDouble(txtCurrancyValue.Text);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
        txtCurrancyValue.Focus();
    }
    private void calculate(string amtt,string camtt)
    {
         double amt = Convert.ToDouble(amtt);
        double camt = Convert.ToDouble(camtt);
        double namt = amt * camt;
        lblTAmt.Text = namt.ToString();
    }
    private static int[] ad;
    private int tice, gice, icelate;
    protected void btnSubmitAmt_Click(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand();
            SqlTransaction sTR;
            sTR = con.BeginTransaction();
            cmd.Connection = con;
            cmd.Transaction = sTR;
            cmd.CommandText = "Select DDNO from FeeAC where DDNO='" + txtDDNO.Text.ToString() + "' and Bank='" + ddlBank.SelectedValue.ToString() + "' and DDDate='"+ Convert.ToDateTime(txtDOB.Text, dtinfo)+"'";
            string strddno = Convert.ToString(cmd.ExecuteScalar());
            if (strddno == "")
            {
                if (Convert.ToInt32(lblTDDRcv.Text) > Convert.ToInt32(lblTDDSub.Text))
                {
                    if ((ddlAmountHeader.SelectedValue.ToString() == "Academic" || ddlAmountHeader.SelectedValue.ToString()=="AutoCAD")  && (Convert.ToInt32(lblADDRcv.Text) == Convert.ToInt32(lblADDSub.Text)))
                    {
                        lblException.Text="All DD Submitted";
                        ddlAmountHeader.Focus();
                    }
                    else if(ddlAmountHeader.SelectedValue.ToString()=="Others" && (Convert.ToInt32(lblODDRcv.Text)==Convert.ToInt32(lblODDSub.Text))){
                        lblException.Text="All DD Submitted";
                        ddlAmountHeader.Focus();
                    }
                      else if(ddlAmountHeader.SelectedValue.ToString()=="Project" && (Convert.ToInt32(lblProRcv.Text)==Convert.ToInt32(lblProSub.Text))){
                        lblException.Text="All DD Submitted";
                        ddlAmountHeader.Focus();
                    }
                    else if (ddlAmountHeader.SelectedValue.ToString() == "Books" && (Convert.ToInt32(lblBookRcv.Text) == Convert.ToInt32(lblBookSub.Text)))
                    {
                        lblException.Text = "All DD Submitted";
                        ddlAmountHeader.Focus();
                    }
                    else if (ddlAmountHeader.SelectedValue.ToString() == "Prospectus" && (Convert.ToInt32(lblProspectusRcv.Text) == Convert.ToInt32(lblProspectusSub.Text)))
                    {
                        lblException.Text = "All DD Submitted";
                        ddlAmountHeader.Focus();
                    }
                    else
                    {
                        try
                        {
                            cmd.CommandText="insert into FeeAC(IMID,Amt,AmtType,AmtFor,SubDate,DDNO,Bank,Narration,CurrancyName,CurrancyValue,Session,DiaryNo,DDDate) values(@IMID,@Amt,@AmtType,@AmtFor,@SubDate,@DDNO,@Bank,@Narration,@CurrancyName,@CurrancyValue,@Session,@DiaryNo,@DDDate)";
                            cmd.Parameters.AddWithValue("@IMID", lblEnrolment.Text.ToString());
                            cmd.Parameters.AddWithValue("@Amt", lblTAmt.Text);
                            cmd.Parameters.AddWithValue("@AmtType", ddlAmtType.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@AmtFor", ddlAmountHeader.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SubDate",DateTime.Now);
                            cmd.Parameters.AddWithValue("@DDNO", txtDDNO.Text.ToString());
                            cmd.Parameters.AddWithValue("@Bank", ddlBank.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@Narration", txtNarration.Text.ToString());
                            cmd.Parameters.AddWithValue("@CurrancyName", ddlCurrancy.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@CurrancyValue", txtCurrancyValue.Text);
                            cmd.Parameters.AddWithValue("@Session", lblhiddenSession.Text.ToString());
                            cmd.Parameters.AddWithValue("@DiaryNo", txtACNO.Text.ToString());
                            cmd.Parameters.AddWithValue("@DDDate", Convert.ToDateTime(txtDOB.Text, dtinfo));
                            cmd.ExecuteNonQuery();
                            //cmd.CommandText ="select * from IMAC where IMID='" + lblEnrolment.Text.ToString() + "'";
                            //SqlDataReader reader;
                            ad = new int[5];
                            //reader = cmd.ExecuteReader();
                            //while (reader.Read())
                            //{
                            //    ad[0] = Convert.ToInt32(reader["Late"]);  //late
                            //    ad[1] = Convert.ToInt32(reader["Total"]);  //total
                            //   // ad[2] = Convert.ToInt32(reader["GTotal"]);  // Gtotal;
                            //    ad[2] = Convert.ToInt32(reader["IMTotal"]); // Books
                            //    ad[3] = Convert.ToInt32(reader["Prospectus"]); // Prospectus
                            //}
                            //reader.Close();
                            ad[1] =  Convert.ToInt32(lblTAmt.Text);
                            if (ddlAmountHeader.SelectedValue.ToString() == "Late Fees")
                                ad[0] = Convert.ToInt32(lblTAmt.Text);
                            else ad[0] = 0;
                            if (ddlAmountHeader.SelectedValue.ToString() == "Books")
                                ad[2] = Convert.ToInt32(lblTAmt.Text);
                            else ad[2] = 0;
                            if (ddlAmountHeader.SelectedValue.ToString() == "Prospectus")
                                ad[3] = Convert.ToInt32(lblTAmt.Text);
                            else ad[3] = 0;
                            if (ddlAmountHeader.SelectedValue.ToString() == "Project")
                                ad[4] = Convert.ToInt32(lblTAmt.Text.ToString());
                            else ad[4] = 0;
                            cmd.CommandText = "Update IMAccount set Amount=Amount+'" + Convert.ToInt32(lblTAmt.Text) + "' where Fees='" + ddlAmountHeader.SelectedValue.ToString() + "' and IMID='" + lblEnrolment.Text.ToString() + "'";
                            cmd.ExecuteNonQuery();
                            //cmd.CommandText = "update IMAC set  Late=Late+'" + ad[0] + "', Total=Total+'" + ad[1] + "', IMTotal=IMTotal+'" + ad[2] + "', Prospectus=Prospectus+'" + ad[3] + "' , Project=Project+'" + ad[3] + "' where IMID='" + lblEnrolment.Text.ToString() + "'";
                            //cmd.ExecuteNonQuery();
                            //icelate = Convert.ToInt32(lblTAmt.Text);
                            //if (ddlAmountHeader.SelectedValue.ToString() != "Late Fees")
                            //{
                            //    icelate = 0;
                            //}
                            //cmd.CommandText = "update IMAC set Late=Late+'" + icelate + "', Total=Total+'" + Convert.ToInt32(lblTAmt.Text) + "' where IMID='ICE'";
                            //cmd.ExecuteNonQuery();
                            lblDiaryAmount.Text = (Convert.ToInt32(lblDiaryAmount.Text) + Convert.ToInt32(txtAmt.Text)).ToString();
                            ClsAccount ac = new ClsAccount();
                            ac.AmountSubmit(lblEnrolment.Text, txtACNO.Text, DateTime.Now, "Credit", lblTAmt.Text, lblhiddenSession.Text,txtDDNO.Text + ":" +ddlBank.SelectedItem.Text, sTR, cmd);
                            updateCount(lblhiddenSession.Text.ToString(), txtACNO.Text.ToString(),sTR,cmd);
                            Log.WriteLog(Request.QueryString["maikal"], "B002", txtIDIM.Text.ToString() + ":" + txtACNO.Text.ToString(), ddlBank.SelectedValue.ToString(), txtDDNO.Text.ToString(), lblTAmt.Text.ToString(), "Add DD to Im Account");
                            Log.WriteLog("B002", Request.QueryString["maikal"], txtIDIM.Text.ToString() + ":" + txtACNO.Text.ToString(), ddlBank.SelectedValue.ToString(), txtDDNO.Text.ToString(), lblTAmt.Text.ToString(), "Add DD to Im Account");
                            txtDDNO.Text = ""; txtNarration.Text = ""; txtAmt.Text = "";
                            lblTAmt.Text = "0";
                            lblExceptionCount.Text = "";
                            lblException.Text = "";
                          //  total(txtIDIM.Text,sTR,cmd);
                            sTR.Commit();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Sucessfully Submitted')", true);
                            txtACNO.Focus();
                        }
                        catch (Exception ex)
                        {
                            sTR.Rollback();
                        }
                    }
                }
                else
                {
                    lblExceptionCount.Text = "All DD Submitted.";
                    btnClear.Focus();
                }
            }
            else
            {
                lblException.Text = "Already Submitted This DD No.: " + txtDDNO.Text.ToString()+" In "+ddlBank.SelectedValue.ToString();
                txtDDNO.Focus();
            }
        }
        catch (SqlException ex)
        {
            lblException.Text =ex.ToString();
        }
        catch (Exception ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select IMID from DiaryEntry where DiaryNo='" + txtACNO.Text.ToString() + "' and ExamSession='"+lblhiddenSession.Text.ToString()+"'", con);
        string damount = Convert.ToString(cmd.ExecuteScalar());
        if (damount.ToString() == "")
        {
            lblexceptionDirary.Text = "Invalid Diary No: "+txtACNO.Text.ToString();
            btnSubmitAmt.Enabled = false; txtACNO.Text = "";
            txtACNO.Focus();
        }
        else
        {
            if (txtIDIM.Text == damount.ToString())
            {
                  cmd =new SqlCommand("select Sum(Amt) from FeeAC where DiaryNo='" + txtACNO.Text.ToString() + "'",con);
                 lblDiaryAmount.Text = Convert.ToString(cmd.ExecuteScalar());
                 if (lblDiaryAmount.Text == "") lblDiaryAmount.Text = "0.00";
                 lblDiaryAmount.Text = lblDiaryAmount.Text.ToString().TrimEnd('0').TrimEnd('.');
                lblException.Text = "";
                btnSubmitAmt.Enabled = true;
                lblexceptionDirary.Text = "";
                ddlAmtType.Focus();
                countDD(lblhiddenSession.Text.ToString(), txtACNO.Text.ToString());
            }
            else
            {
                lblexceptionDirary.Text = "Invalid Diary No:" + txtACNO.Text.ToString() + "  for IMID: " + txtIDIM.Text.ToString();
                txtACNO.Text = "";
                txtACNO.Focus();
            }
        }
        con.Dispose();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtDOB.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDDNO.Text = ""; txtNarration.Text = ""; txtAmt.Text = "0";
        ddlCurrancy.SelectedIndex = 0;
        lblCurrancyText.Visible = false;
        lblCurrancyName.Visible = false;
        txtCurrancyValue.Visible = false;
        txtCurrancyValue.Text = "1";
        calculate(txtAmt.Text, txtCurrancyValue.Text);
        lblException.Text = "";
        GridDiaryNo.Focus();
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
            ddlCurrancy.Focus();
        }
        else if (ddlCurrancy.SelectedValue == "DL")
        {
            lblCurrancyName.Text = " One Dolar Equal To";
            lblCurrancyText.Text = "Presenet Currency Value:";
            lblCurrancyText.Visible = true;
            lblCurrancyName.Visible = true;
            txtCurrancyValue.Visible = true;
            txtCurrancyValue.Focus();
        }
        else if (ddlCurrancy.SelectedValue == "OT")
        {
            lblCurrancyName.Text = " One Unit Equal To";
            lblCurrancyText.Text = "Presenet Currency Value:";
            lblCurrancyText.Visible = true;
            lblCurrancyName.Visible = true;
            txtCurrancyValue.Visible = true;
            txtCurrancyValue.Focus();
        }
        else
        {
        }
        ddlCurrancy.Focus();
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
                btnSubmitAmt.Enabled = false;
            }
            else
            {
                double dev = now.Subtract(dt).TotalDays;
                if (dev > 80)
                {
                    btnSubmitAmt.Enabled = false;
                    lblExceptiondAte.Text = "Total No of Days: " + dev.ToString();
                }
                else
                {
                    btnSubmitAmt.Enabled = true;
                    lblExceptiondAte.Text = "Total No of Days: " + dev.ToString();
                }
            }
            if (ddlAmtType.SelectedValue.ToString() != "DD")
            {
                btnSubmitAmt.Enabled = true;
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
          DateTime dt =Convert.ToDateTime(txtDOB.Text,dtinfo);
          DateTime now=Convert.ToDateTime(txtDate.Text,dtinfo);
          diff = chkdob(now, dt);
          if (diff[0] == 0 & diff[1] == 0 & diff[2] == 100)
          {
              lblExceptiondAte.Text = "DD Date is earlier than Current Date.";
              btnSubmitAmt.Enabled = false;
          }
          else
          {
              //if (diff[0] > 0)
              //{
              //    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
              //    btnSubmitAmt.Enabled = true;
              //}
              //else if (diff[1] > 2)
              //{
              //    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
              //    btnSubmitAmt.Enabled = true;
              //}
              //else if (diff[1] == 2 & diff[2] > 25)
              //{
              //    lblExceptiondAte.Text = diff[0].ToString() + " Yr. " + diff[1].ToString() + " Mo. " + diff[2].ToString() + "Day Old DD.";
              //    btnSubmitAmt.Enabled = true;
              //}
        double dev = now.Subtract(dt).TotalDays;
        if (dev > 80)
        {
            btnSubmitAmt.Enabled = false;
            lblExceptiondAte.Text = "Total No of Days: " + dev.ToString();
        }
        else
        {
            btnSubmitAmt.Enabled = true;
            lblExceptiondAte.Text = "Total No of Days: " + dev.ToString();
        }
          }
          if (ddlAmtType.SelectedValue.ToString() != "DD")
          {
              btnSubmitAmt.Enabled = true;
          }           
           
        }
        catch (FormatException ex)
        {
            lblExceptiondAte.Text = "Invalid Submission Date Format.";
        }
        txtNarration.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        fillDiary();
        txtACNO.Text = ""; txtSession.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
       
        txtACNO.Text = "";
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        fillDiary();
        txtIDIM.Focus();
    }
    protected void ibtnNewCourier_Onclick(object sender, EventArgs e)
    {
        panelCourier.Visible = true;
        txtNewCourier.Focus();

    }
    protected void btnSAveNew_Onclick(object sender, EventArgs e)
    {
        if (txtNewCourier.Text == "")
        {
            lblExceptionNewCourier.Text = "Please Insert Bank Name.";
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ServiceNameMaster(Name,City,Type) values(@Name,@City,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtNewCourier.Text.ToString());
            cmd.Parameters.AddWithValue("@City", txtNewCity.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", "Bank");

            cmd.ExecuteNonQuery();
            lblExceptionNewCourier.Text = "Successfully Saved New Bank Name";
            ddlBank.DataBind();
            con.Close();
            con.Dispose();
        }

        btnCencel.Focus();
    }
    protected void btnCencelnew_Onclick(object sender, EventArgs e)
    {
        panelCourier.Visible = false;
        ddlBank.Focus();
    }
    protected void ibtnNewAmtType_Onclick(object sender, EventArgs e)
    {
        panelAmtFor.Visible = true;
        txtNewAmtType.Focus();
    }
    protected void btnCloseAmtFor_Onclick(object sender, EventArgs e)
    {
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }
    protected void btnSAveAmtType_Onclick(object sender, EventArgs e)
    {
        if (txtNewAmtType.Text == "")
        {
            lbleXceptionAmtType.Text = "Please Insert Amount Type.";
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ServiceNameMaster(Name,City,Type) values(@Name,@City,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtNewAmtType.Text.ToString());
            cmd.Parameters.AddWithValue("@City", "N/A");
            cmd.Parameters.AddWithValue("@Type", "Amount");

            cmd.ExecuteNonQuery();
            lbleXceptionAmtType.Text = "Successfull Saved New Amount Type";
            con.Close();
            con.Dispose();
            btnCencelamtType.Focus();
        }
    }
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtACNO.Text = GridDiaryNo.SelectedRow.Cells[1].Text.ToString();
        con.Close(); con.Open();
        cmd = new SqlCommand();
        SqlTransaction sTR;
        sTR = con.BeginTransaction();
        cmd.Connection = con;
        cmd.Transaction = sTR;
        try
        {
            cmd.CommandText = "select Sum(Amt) From FeeAC where DiaryNo='" + txtACNO.Text.ToString() + "'";
            lblDiaryAmount.Text = Convert.ToString(cmd.ExecuteScalar());
            if (lblDiaryAmount.Text == "") lblDiaryAmount.Text = "0.00";
            lblDiaryAmount.Text = lblDiaryAmount.Text.ToString().TrimEnd('0').TrimEnd('.');
                txtIDIM.Text = GridDiaryNo.SelectedRow.Cells[2].Text.ToString();
            cmd.CommandText = "select * from IM where ID='" + txtIDIM.Text.ToString() + "'";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                lblIMAddress.Text = reader[3].ToString();
                lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                lblGroupID.Text = reader["GID"].ToString();
                btnSubmitAmt.Enabled = true;
                pnlIMInfo.Visible = true;
            }
            reader.Close();
          //  total(txtIDIM.Text, sTR, cmd);
            reader.Dispose();
            sTR.Commit();
            con.Close();
        }
        catch (Exception ex)
        {
            sTR.Rollback();
        }
        lblexceptionDirary.Text = "";
        lblEnrolment.Text = txtIDIM.Text;
        countDD(lblhiddenSession.Text.ToString(), txtACNO.Text.ToString());
        ddlAmtType.Focus();
        con.Dispose();
    }
    protected void GridDiaryNo_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "Diary No";
            e.Row.Cells[2].Text = "Membership";
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
   private void countDD(string session,string dairy)
   {
     con.Close();con.Open();
     cmd=new SqlCommand("select * from DairyCount where Session='"+session.ToString()+"' and DairyNo='"+dairy.ToString()+"'",con);
     SqlDataReader reader;
       reader=cmd.ExecuteReader();
       while(reader.Read())
       {
           lblADDRcv.Text=reader["ADDRcv"].ToString();
           lblADDSub.Text=reader["ADDSub"].ToString();
           lblODDRcv.Text=reader["ODDRcv"].ToString();
           lblODDSub.Text=reader["ODDSub"].ToString();
           lblBookRcv.Text = reader["BooksRcv"].ToString();
           lblBookSub.Text = reader["BooksSub"].ToString();
           lblProspectusRcv.Text = reader["ProspectusRcv"].ToString();
           lblProspectusSub.Text = reader["ProspectusSub"].ToString();
           lblTDDRcv.Text = reader["TotalDDRcv"].ToString();
           lblTDDSub.Text = reader["TotalDDSub"].ToString();
       }
       reader.Close();
       cmd = new SqlCommand("select * from ProjectCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
       reader = cmd.ExecuteReader();
       while (reader.Read())
       {
           lblProRcv.Text = reader["DDRcv"].ToString();
           lblProSub.Text = reader["DDSub"].ToString();
       }
       reader.Close();
       reader.Dispose();
       if (lblProRcv.Text == "") lblProRcv.Text = "0"; if (lblProSub.Text == "") lblProSub.Text = "0";
       lblTDDRcv.Text = Convert.ToString(Convert.ToInt32(lblADDRcv.Text) + Convert.ToInt32(lblODDRcv.Text) + Convert.ToInt32(lblProRcv.Text) + Convert.ToInt32(lblBookRcv.Text) + Convert.ToInt32(lblProspectusRcv.Text));
       lblTDDSub.Text = Convert.ToString(Convert.ToInt32(lblADDSub.Text) + Convert.ToInt32(lblODDSub.Text) + Convert.ToInt32(lblProSub.Text) + Convert.ToInt32(lblBookSub.Text) + Convert.ToInt32(lblProspectusSub.Text));
       con.Close();
       if (Convert.ToInt32(lblTDDRcv.Text) > Convert.ToInt32(lblTDDSub.Text))
       {
           lblExceptionCount.Text = "";
       }
       else
       {
           lblExceptionCount.Text = "All DD Submitted.";
       }
       con.Close();
      // con.Dispose();
    }
   private void updateCount(string session, string dairy,SqlTransaction sTR,SqlCommand cmd)
   {
       if (Convert.ToInt32(lblTDDRcv.Text) > Convert.ToInt32(lblTDDSub.Text))
       {
           int addsub=Convert.ToInt32(lblADDSub.Text);
           int oddsub=Convert.ToInt32(lblODDSub.Text);
           int prosub=Convert.ToInt32(lblProSub.Text);
           int booksub = Convert.ToInt32(lblBookSub.Text);
           int prospectussub = Convert.ToInt32(lblProspectusSub.Text);
           int subdd = Convert.ToInt32(lblTDDSub.Text);
           
           if (ddlAmountHeader.SelectedValue.ToString() == "Academic" || ddlAmountHeader.SelectedValue.ToString()=="AutoCAD")
           {
               if (Convert.ToInt32(lblADDRcv.Text) > Convert.ToInt32(lblADDSub.Text))
               {
                   addsub += 1;subdd += 1;
                   lblADDSub.Text = addsub.ToString();
               }
               else
                   lblExceptionCount.Text = "All DD Submitted";
           }
           if (ddlAmountHeader.SelectedValue.ToString() == "Others")
           {
               if (Convert.ToInt32(lblODDRcv.Text) > Convert.ToInt32(lblODDSub.Text))
               {
                   oddsub += 1;subdd += 1;
                   lblODDSub.Text = oddsub.ToString();
               }
               else
                   lblExceptionCount.Text = "All DD Submitted";
           }
            if (ddlAmountHeader.SelectedValue.ToString() == "Project")
            {
                if (Convert.ToInt32(lblProRcv.Text) > Convert.ToInt32(lblProSub.Text))
                {
                    prosub += 1;subdd += 1;
                    lblProSub.Text = prosub.ToString();
                }
                else
                    lblExceptionCount.Text = "All DD Submitted";
           }
            if (ddlAmountHeader.SelectedValue.ToString() == "Books")
            {
                if (Convert.ToInt32(lblBookRcv.Text) > Convert.ToInt32(lblBookSub.Text))
                {
                   booksub += 1; subdd += 1;
                   lblBookSub.Text = booksub.ToString();
                }
                else
                    lblExceptionCount.Text = "All DD Submitted";
            }
           
            if (ddlAmountHeader.SelectedValue.ToString() == "Prospectus")
            {
                if (Convert.ToInt32(lblProspectusRcv.Text) > Convert.ToInt32(lblProspectusSub.Text))
                {
                   prospectussub += 1; subdd += 1;
                   lblProspectusSub.Text =prospectussub.ToString();
                }
                else
                    lblExceptionCount.Text = "All DD Submitted";
            }
           lblTDDSub.Text = subdd.ToString();
           cmd.CommandText ="update DairyCount set ADDSub=@ADDSub, ODDSub=@ODDSub,BooksSub=@BooksSub,ProspectusSub=@ProspectusSub,TotalDDSub=@TotalSub where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'";
           cmd.Parameters.AddWithValue("@ADDSub",addsub);
           cmd.Parameters.AddWithValue("@ODDSub",oddsub);
           cmd.Parameters.AddWithValue("@BooksSub", booksub);
           cmd.Parameters.AddWithValue("@ProspectusSub", prospectussub);
           cmd.Parameters.AddWithValue("@TotalSub", subdd);
           cmd.ExecuteNonQuery();
           cmd.CommandText ="update ProjectCount set DDSub=@DDSub where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'";
           cmd.Parameters.AddWithValue("@DDSub", prosub);
           cmd.ExecuteNonQuery();
       }
       else
       {
           lblExceptionCount.Text = "All DD Submitted.";
       }
   }
   //protected void ddlAmtForMs_SelectedIndexChanged(object sender, EventArgs e)
   //{
   //    lblException.Text = "";
   //    ddlAmtForMs.Focus();
   //}
}