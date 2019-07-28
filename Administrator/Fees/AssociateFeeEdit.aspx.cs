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

public partial class Administrator_Fees_AssociateFeeEdit : System.Web.UI.Page
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
            if (!IsPostBack)
            {

            }
            try
            {
                con.Close();
                con.Open();
                SqlDataReader rd;
                SqlCommand cmd = new SqlCommand("select * from FeeMaster where FeeType='Asso' and type='" + Request.QueryString["type"] + "'", con);
                rd = cmd.ExecuteReader();
                int i = 0;
                if (rd.Read())
                {
                    i = i + 1;
                }
                rd.Close();
                if (i == 0)
                {
                    SqlCommand cmdins = new SqlCommand("insert into FeeMaster(FeeType) Values('Asso') ", con);
                    cmdins.ExecuteNonQuery();
                }
                if (i == 1)
                {
                    if (!IsPostBack)
                    {
                        SqlDataReader reader;
                        SqlCommand cmdrd = new SqlCommand("select * from FeeMaster where FeeType='Asso' and FeeLevel='" + Request.QueryString["lvl"] + "' and type='"+Request.QueryString["type"]+"'", con);
                        reader = cmdrd.ExecuteReader();
                        while (reader.Read())
                        {
                            string str = reader[1].ToString();
                            lblMemFeesA.Text = str.TrimEnd('0');
                            lblComFeeA.Text = reader[2].ToString().TrimEnd('0');
                            lblMemFeesB.Text = reader[3].ToString().TrimEnd('0');
                            lblComFeeB.Text = reader[4].ToString().TrimEnd('0');
                            lblLatefee1.Text = reader[13].ToString().TrimEnd('0');
                            lblLatefee2.Text = reader[14].ToString().TrimEnd('0');
                            lblLatefee3.Text = reader[15].ToString().TrimEnd('0');
                            lblSumSchedule.Text = reader[16].ToString().TrimEnd('0');
                            lblWinSchedule.Text = reader[17].ToString().TrimEnd('0');
                            lblExemptionFee.Text = reader[18].ToString().TrimEnd('0');
                            lblAnnualSubscriptnFee.Text = reader[19].ToString().TrimEnd('0');
                            lblChangeExamCenter.Text = reader[20].ToString().TrimEnd('0');
                            lblReCheckingFee.Text = reader[21].ToString().TrimEnd('0');
                            lblProApproval.Text = reader[22].ToString().TrimEnd('0');
                            lblProEvaluation.Text = reader[23].ToString().TrimEnd('0');
                            lblProCertificate.Text = reader[24].ToString().TrimEnd('0');
                            lblCertificationFee.Text = reader[25].ToString().TrimEnd('0');
                            lblIDCard.Text = reader[26].ToString().TrimEnd('0');
                            lblAdminCatd.Text = reader[27].ToString().TrimEnd('0');
                            lblMarksStatment.Text = reader[28].ToString().TrimEnd('0');
                            lblMemCertificate.Text = reader[29].ToString().TrimEnd('0');
                            lblSecA.Text = reader[30].ToString().TrimEnd('0');
                            lblSecB.Text = reader[31].ToString().TrimEnd('0');
                            lblOldSecA.Text = reader[32].ToString().TrimEnd('0');
                            lblOldSecB.Text = reader[33].ToString().TrimEnd('0');
                            lblPostalCharge.Text = reader[34].ToString().TrimEnd('0');
                            txtITIFees.Text = reader["ITIFees"].ToString().TrimEnd('0');
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
            catch (SqlException ex)
            {
                lblexepinfo.Text = ex.ToString();
            }
            finally
            {
                con.Close();
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
    protected void btnSaveMembership_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update FeeMaster set FeeType=@FeeType,MemFee1=@MemFee1,ComFee1=@ComFee1,MemFee2=@MemFee2,ComFee2=@ComFee2,CompositeDuration=@CompositeDuration where FeeType='Asso' and FeeLevel='" + Request.QueryString["lvl"] + "' and type='"+Request.QueryString["type"]+"'", con);
                cmd.Parameters.AddWithValue("@FeeType", "Asso");
                cmd.Parameters.AddWithValue("@MemFee1", lblMemFeesA.Text.ToString()); cmd.Parameters.AddWithValue("@ComFee1", lblComFeeA.Text.ToString());
                cmd.Parameters.AddWithValue("@MemFee2", lblMemFeesB.Text.ToString()); cmd.Parameters.AddWithValue("@ComFee2", lblComFeeB.Text.ToString());
                cmd.Parameters.AddWithValue("@CompositeDuration", ddlCompositeDuration.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                lblsavemembership.Text = "Successfully saved !";
        }
        catch (SqlException ex)
        {
            lblsavemembership.Text = ex.ToString(); 
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void btnSaveExamSchedule_Click(object sender, EventArgs e)
    {
        if (txtSumWOL.Text == "" && txtWinWOL.Text == "" && txtSumWL1.Text == "" && txtWinWL1.Text == "" && txtSumWL2.Text == "" && txtWinWL2.Text == "" && txtSumWL3.Text == "" && txtWinWL3.Text == "")
        {
            lblMessage.Text = "Please Fill Details.!";
        }
        else
        {
            try
            {

                dtinfo.ShortDatePattern = "dd/MM/yyyy";
                dtinfo.DateSeparator = "/";
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("update FeeMaster set SumExamWOL=@SumExamWOL,WinExamWOL=@WinExamWOL,SumExamWL1=@SumExamWL1,SumExamWL2=@SumExamWL2,SumExamWL3=@SumExamWL3,WinExamWL1=@WinExamWL1,WinExamWL2=@WinExamWL2,WinExamWL3=@WinExamWL3,ExamLF1=@ExamLF1,ExamLF2=@ExamLF2,ExamLF3=@ExamLF3,SumExamSchedule=@SumExamSchedule,WinExamSchedule=@WinExamSchedule where FeeType='Asso' and FeeLevel='" + Request.QueryString["lvl"] + "' and type='"+Request.QueryString["type"]+"'", con);
                cmd.Parameters.AddWithValue("@SumExamWOL", Convert.ToDateTime(txtSumWOL.Text, dtinfo));
                cmd.Parameters.AddWithValue("@WinExamWOL", Convert.ToDateTime(txtWinWOL.Text, dtinfo));
                cmd.Parameters.AddWithValue("@SumExamWL1", Convert.ToDateTime(txtSumWL1.Text, dtinfo));
                cmd.Parameters.AddWithValue("@SumExamWL2", Convert.ToDateTime(txtSumWL2.Text, dtinfo));
                cmd.Parameters.AddWithValue("@SumExamWL3", Convert.ToDateTime(txtSumWL3.Text, dtinfo));
                cmd.Parameters.AddWithValue("@WinExamWL1", Convert.ToDateTime(txtWinWL1.Text, dtinfo));
                cmd.Parameters.AddWithValue("@WinExamWL2", Convert.ToDateTime(txtWinWL2.Text, dtinfo));
                cmd.Parameters.AddWithValue("@WinExamWL3", Convert.ToDateTime(txtWinWL3.Text, dtinfo));
                cmd.Parameters.AddWithValue("@ExamLF1", lblLatefee1.Text.ToString());
                cmd.Parameters.AddWithValue("@ExamLF2", lblLatefee2.Text.ToString());
                cmd.Parameters.AddWithValue("@ExamLF3", lblLatefee3.Text.ToString());
                cmd.Parameters.AddWithValue("@SumExamSchedule", lblSumSchedule.Text.ToString());
                cmd.Parameters.AddWithValue("@WinExamSchedule", lblWinSchedule.Text.ToString());
                cmd.ExecuteNonQuery();
                lblExamScheduleInfo.Text = "Successfully Saved!";
            }
            catch (SqlException ex)
            {
                lblExamScheduleInfo.Text = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
    protected void btnSaveExemption_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update FeeMaster set ExemptionFee=@ExemptionFee,ASubscription=@ASubscription,ExamCChange=@ExamCChange,ReChacking=@ReChacking where FeeType='Asso' and FeeLevel='" + Request.QueryString["lvl"] + "' and type='"+Request.QueryString["type"]+"'", con);
            cmd.Parameters.AddWithValue("@ExemptionFee", lblExemptionFee.Text.ToString());
            cmd.Parameters.AddWithValue("@ASubscription", lblAnnualSubscriptnFee.Text.ToString());
            cmd.Parameters.AddWithValue("@ExamCChange", lblChangeExamCenter.Text.ToString());
            cmd.Parameters.AddWithValue("@ReChacking", lblReCheckingFee.Text.ToString());
            cmd.ExecuteNonQuery();
            lblExemptionfeeInfo.Text = "Successfully Saved!";
        }
        catch (SqlException ex)
        {
            lblExemptionfeeInfo.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void btnProjects_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close();con.Open();
            SqlCommand cmd = new SqlCommand("update FeeMaster set ProApproval=@ProApproval,ProEvaluation=@ProEvaluation,PCertificate=@PCertificate,Certification=@Certification,IDCard=@IDCard,AdminCard=@AdminCard,MStatement=@MStatement,MCertificate=@MCertificate where FeeType='Asso' and FeeLevel='" + Request.QueryString["lvl"] + "' and type='"+Request.QueryString["type"]+"'", con);
            cmd.Parameters.AddWithValue("@ProApproval", lblProApproval.Text.ToString());
            cmd.Parameters.AddWithValue("@ProEvaluation", lblProEvaluation.Text.ToString());
            cmd.Parameters.AddWithValue("@PCertificate", lblProCertificate.Text.ToString());
            cmd.Parameters.AddWithValue("@Certification", lblCertificationFee.Text.ToString()); cmd.Parameters.AddWithValue("@IDCard", lblIDCard.Text.ToString()); cmd.Parameters.AddWithValue("@AdminCard", lblAdminCatd.Text.ToString()); cmd.Parameters.AddWithValue("@MStatement", lblMarksStatment.Text.ToString()); cmd.Parameters.AddWithValue("@MCertificate", lblMemCertificate.Text.ToString());
            cmd.ExecuteNonQuery();
            lblProjectInfo.Text = "Successfully Saved!.";
        }
        catch (SqlException ex)
        {
            lblProjectInfo.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void btnSaveExamFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update FeeMaster set ESecA=@ESecA,ESecB=@ESecB,OSecA=@OSecA,OSecB=@OSecB,Postal=@Postal,ITIFees=@ITIFees where FeeType='Asso' and FeeLevel='" + Request.QueryString["lvl"] + "' and type='"+Request.QueryString["type"]+"'", con);
            cmd.Parameters.AddWithValue("@ESecA", lblSecA.Text.ToString());
            cmd.Parameters.AddWithValue("@ESecB", lblSecB.Text.ToString());
            cmd.Parameters.AddWithValue("@OSecA", lblOldSecA.Text.ToString());
            cmd.Parameters.AddWithValue("@OSecB", lblOldSecB.Text.ToString());
            cmd.Parameters.AddWithValue("@Postal", lblPostalCharge.Text.ToString());
            cmd.Parameters.AddWithValue("@ITIFees", txtITIFees.Text.ToString());
            cmd.ExecuteNonQuery();
            lblSecInfo.Text = "Successfully Saved!";
        }
        catch (SqlException ex)
        {
            lblSecInfo.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void btnClear1_Click(object sender, EventArgs e)
    {
        lblMemFeesA.Text = ""; lblMemFeesB.Text = ""; lblComFeeA.Text = ""; lblComFeeB.Text = ""; lblsavemembership.Text = "";
    }
    protected void btnClear2_Click(object sender, EventArgs e)
    {
        txtSumWOL.Text = ""; txtWinWOL.Text = ""; txtSumWL1.Text = ""; txtWinWL1.Text = ""; txtSumWL2.Text = ""; txtWinWL2.Text = "";
        txtSumWL3.Text = ""; txtWinWL3.Text = ""; lblMessage.Text = "";
    }
    protected void btnClear3_Click(object sender, EventArgs e)
    {
        lblExemptionFee.Text = ""; lblAnnualSubscriptnFee.Text = ""; lblChangeExamCenter.Text = ""; lblReCheckingFee.Text = "";
        lblExemptionfeeInfo.Text = "";
    }
    protected void btnClear4_Click(object sender, EventArgs e)
    {
        lblProApproval.Text = ""; lblProEvaluation.Text = ""; lblProCertificate.Text = ""; lblCertificationFee.Text = ""; lblIDCard.Text = ""; lblAdminCatd.Text = ""; lblMarksStatment.Text = ""; lblMemCertificate.Text = "";
        lblProjectInfo.Text = "";
    }
    protected void btnClear5_Click(object sender, EventArgs e)
    {
        lblPostalCharge.Text = ""; lblSecA.Text = ""; lblSecB.Text = ""; lblOldSecA.Text = ""; lblOldSecB.Text = ""; txtITIFees.Text = ""; lblSecInfo.Text = "";
    }
}
