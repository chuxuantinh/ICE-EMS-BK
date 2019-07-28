using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Administrator_Fees_AssociateFeesShow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../../Login.aspx");
            }
            try
            {
                
                con.Close();
                con.Open();
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand("select * from FeeMaster where FeeType='Asso' and FeeLevel='" + Request.QueryString["lvl"] + "' and type='" + Request.QueryString["type"] + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblMemFeesA.Text = reader[1].ToString().TrimEnd('0').TrimEnd('.');
                    lblComFeeA.Text = reader[2].ToString().TrimEnd('0').TrimEnd('.');
                    lblMemFeesB.Text = reader[3].ToString().TrimEnd('0').TrimEnd('.');
                    lblComFeeB.Text = reader[4].ToString().TrimEnd('0').TrimEnd('.');
                    lblDateSumWOFee.Text = reader[5].ToString();
                    lblDateWinWOFee.Text = reader[6].ToString();
                    lblDateSumWLFee1.Text = reader[7].ToString();
                    lblDateSumWLFee2.Text = reader[8].ToString();
                    lblDateSumWLFee3.Text = reader[9].ToString();
                    lblDateWinWLFee1.Text = reader[10].ToString();
                    lblDateWinWLFee2.Text = reader[11].ToString();
                    lblDateWinWLFee3.Text = reader[12].ToString();
                    lblLatefee1.Text = reader[13].ToString().TrimEnd('0').TrimEnd('.');
                    lblLatefee2.Text = reader[14].ToString().TrimEnd('0').TrimEnd('.');
                    lblLatefee3.Text = reader[15].ToString().TrimEnd('0').TrimEnd('.');
                    lblSumSchedule.Text = reader[16].ToString();
                    lblWinSchedule.Text = reader[17].ToString();
                    lblExemptionFee.Text = reader[18].ToString().TrimEnd('0').TrimEnd('.');
                    lblAnnualSubscriptnFee.Text = reader[19].ToString().TrimEnd('0').TrimEnd('.');
                    lblChangeExamCenter.Text = reader[20].ToString().TrimEnd('0').TrimEnd('.');
                    lblReCheckingFee.Text = reader[21].ToString().TrimEnd('0').TrimEnd('.');
                    lblProApproval.Text = reader[22].ToString().TrimEnd('0').TrimEnd('.');
                    lblProEvaluation.Text = reader[23].ToString().TrimEnd('0').TrimEnd('.');
                    lblProCertificate.Text = reader[24].ToString().TrimEnd('0').TrimEnd('.');
                    lblCertificationFee.Text = reader[25].ToString().TrimEnd('0').TrimEnd('.');
                    lblIDCard.Text = reader[26].ToString().TrimEnd('0').TrimEnd('.');
                    lblAdminCard.Text = reader[27].ToString().TrimEnd('0').TrimEnd('.');
                    lblMStatement.Text = reader[28].ToString().TrimEnd('0').TrimEnd('.');
                    lblMCeritficate.Text = reader[29].ToString().TrimEnd('0').TrimEnd('.');
                    lblSecA.Text = reader[30].ToString().TrimEnd('0').TrimEnd('.');
                    lblSecB.Text = reader[31].ToString().TrimEnd('0').TrimEnd('.');
                    lblOldSecA.Text = reader[32].ToString().TrimEnd('0').TrimEnd('.');
                    lblOldSecB.Text = reader[33].ToString().TrimEnd('0').TrimEnd('.');
                    lblPostalCharge.Text = reader[34].ToString().TrimEnd('0').TrimEnd('.');
                    lblCompositeDuration.Text = reader["CompositeDuration"].ToString();
                    lblITIFees.Text = reader["ITIFees"].ToString().TrimEnd('0').TrimEnd('.');
                    exeption1.Visible = false;
                }
                reader.Close();
                reader.Dispose();
            }
            catch (SqlException ex)
            {
                lblExeptionInfo1.Text = ex.ToString();
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
}
