using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Acc_AdmissionForm : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            con.Close();
            con.Open();
            SqlCommand cmdlate=new SqlCommand("select * from IMAC where IMID='ICE'",con);
            SqlDataReader rdlate;
            rdlate=cmdlate.ExecuteReader();
            while (rdlate.Read())
            {
                lblTLate.Text = rdlate["Late"].ToString().TrimEnd('0');
                lblTLate.Text = lblTLate.Text.TrimEnd('.');
            }
            rdlate.Close();
            con.Close();
            con.Dispose();
            if (!IsPostBack)
            {
                btnApprove.Visible = false; btnPay.Visible = false;
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
          finally
          {
          }
    }
    protected void txtIDIM_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + txtIDIM.Text.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        int i = 0;
        if (chk == txtIDIM.Text.ToString())
        {
            i += 1;
        }
        else
        {
            txtIDIM.Text = "Invalid ID"; lblIMAddress.Text = ""; lblIMName.Text = "";
            lblIMCity.Text = "Please Insert Valid IM ID.";
        }
        if (i == 1)
        {
            cmd = new SqlCommand("select * from IM where ID='" + txtIDIM.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                lblIMAddress.Text = reader[7].ToString();
                lblIMCity.Text = "City: " + reader[8].ToString() + " State: " + reader[9].ToString();
                lblEnrolment.Text = txtIDIM.Text.ToString();
                lblGroupID.Text = reader[54].ToString();
               
            }
            reader.Close();
            SqlCommand cd1 = new SqlCommand("select * from IMAC where IMId='" + txtIDIM.Text.ToString() + "'", con);
            reader = cd1.ExecuteReader();
            while (reader.Read())
            {
                lblTAmt.Text = reader["Total"].ToString().TrimEnd('0');
                lblTAmt.Text = lblTAmt.Text.TrimEnd('.');
                lblGAmt.Text = reader["GTotal"].ToString().TrimEnd('0');
                lblGAmt.Text = lblGAmt.Text.TrimEnd('.');
               
            }
            reader.Close();
        }
        con.Close();
        con.Dispose();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_Load(object sender, EventArgs e)
    {
       
        int i = GridView1.Rows.Count;
        lbltotalRow.Text = i.ToString();
    }
    protected void lbtnViewGroup_Click(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from IMCradit where IMID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblTFromNo.Text = reader[2].ToString();
            txtFromNo.Text = lblTFromNo.Text.ToString();
            lblReqAmt.Text = reader[3].ToString().TrimEnd('0');
            lblReqAmt.Text = lblReqAmt.Text.TrimEnd('.');
        }

        reader.Close();
        con.Close();
        con.Dispose();
    }
    public int T, L, R, Df, Mr, Tkn, Pmt, Udr, Ltkn;
    protected void chkLateFEE_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLateFEE.Checked == true)
        {
            T = Convert.ToInt32(lblGAmt.Text);
            L = Convert.ToInt32(lblTLate.Text);
            R = Convert.ToInt32(lblReqAmt.Text);
            if (R <= T)
            {
                lblMessage1.Text = "Late Fee Not Required";
                lblLTaken.Text = "0";
                lblLRem.Text = lblTLate.Text.ToString();

                lblTotalAmtPay.Text = lblReqAmt.Text.ToString();
                btnApprove.Visible = true;
               
            }
            else
            {
                Df = R - T;
                if (Df > L)
                {
                    Mr = Df - L;
                    lblLTaken.Text = L.ToString();
                    L = L - (Df - Mr);
                    Ltkn = Df - Mr;
                    
                    lblLRem.Text = "0";
                    Pmt = T + Ltkn;
                    lblMessage1.Text = "Late fee is less, More Required: " + Mr.ToString() + " Rs. for all form approval, and Now Total Amt: We have "+Pmt.ToString();
                    lblTotalAmtPay.Text = Pmt.ToString();
                    btnApprove.Visible = true;
                }
                else if (Df <= L)
                {
                    Tkn = L - Df;
                    L = L - Df;
                    lblLTaken.Text = Df.ToString();
                    lblLRem.Text = L.ToString();
                    Pmt = T + Df;
                    Udr = Df;
                    lblMessage1.Text = "update late fee as: " + L.ToString() + " Rs. ";
                    btnApprove.Visible = true;
                    lblTotalAmtPay.Text = Pmt.ToString();
                }
            }

        }
        else if (chkLateFEE.Checked == false)
        {
            T = Convert.ToInt32(lblGAmt.Text);
            L = 0;
            R = Convert.ToInt32(lblReqAmt.Text);
            lblLTaken.Text = L.ToString();
            lblLRem.Text = lblTLate.Text.ToString();
            if (R <= T)
            {
                lblMessage1.Text = "Amount enough 4 Admission form.";
                lblTotalAmtPay.Text = lblReqAmt.Text.ToString();
                lblToBeApprovadNo.Text = lblTFromNo.Text.ToString();
                lblToBeAmount.Text = lblReqAmt.Text.ToString();
                btnApprove.Visible = true;
            }
            else
            {
                Df = R - T;
                if (Df > L)
                {
                    Mr = Df - L;

                    L = L - (Df - Mr);
                    Ltkn = Df - Mr;
                    Pmt = T + Ltkn;
                    lblMessage1.Text = "Late fee is less, More Required: " + Mr.ToString() + " Rs. for all form approval, and Now Total Amt: We have " + Pmt.ToString();
                    lblTotalAmtPay.Text = T.ToString();
                    
                    btnApprove.Visible = true;
                }
                else if (Df <= L)
                {
                    Tkn = L - Df;
                    L = L - Df;

                    Pmt = T + Df;
                    Udr = Df;
                    lblMessage1.Text = "update late fee as: " + L.ToString() + " Rs. ";
                    btnApprove.Visible = true;
                    lblTotalAmtPay.Text = Pmt.ToString();
                }
            }
        }
    }
    public int To, Re, Fr, count;
    public float Div;
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        btnPay.Visible = true;
        try
        {
            To = Convert.ToInt32(lblTotalAmtPay.Text);
            Fr = Convert.ToInt32(lblTFromNo.Text);
            Re = Convert.ToInt32(lblReqAmt.Text);
            if (Re <= To)
            {
                lblMessage1.Text = "Approve All Form " + To.ToString() + " From No." + Fr.ToString() + " Requir " + Re.ToString();
                lblToBeAmount.Text = Re.ToString();
                lblToBeApprovadNo.Text = Fr.ToString();
            }
            else if (Re > To)
            {
                count = 0;
                Div = Re / Fr;
                for (int i = 1; i <= Fr+1; i++)
                {
                    Re = Re - (int)Div;
                    if (Re <= To)
                    {
                        count = count + i;
                        goto outer;
                        //break;
                    }
                    // continue;
                    // Re = Re - (int)Div;
                }
            outer:
                if (count > Fr)
                {
                    lblToBeApprovadNo.Text = "0";
                    lblToBeAmount.Text = "0";
                }
                else
                {
                    lblToBeApprovadNo.Text = (Fr - count).ToString();
                    lblToBeAmount.Text = ((Fr - count) * Div).ToString();
                }
            }
                int ldf = Convert.ToInt32(lblTotalAmtPay.Text) - Convert.ToInt32(lblToBeAmount.Text);
                lblLTaken.Text = (Convert.ToInt32(lblLTaken.Text) - ldf).ToString();
                lblLRem.Text = (Convert.ToInt32(lblLRem.Text) + ldf).ToString();
        }
        catch (Exception ex)
        {
            lblMessage1.Text = ex.ToString();
        }
    }
    protected void btnPay_click(object sender, EventArgs e)
    {
        updateadmission(Convert.ToInt32(lblTAmt.Text), Convert.ToInt32(lblGAmt.Text), Convert.ToInt32(lblLTaken.Text), Convert.ToInt32(lblTotalAmtPay.Text), Convert.ToInt32(lblToBeApprovadNo.Text), lblEnrolment.Text.ToString(), lblGroupID.Text.ToString());
    }
  public static int[] ad;
    private void updateadmission(int IM,int GTo,int Ltkn, int Pay,int Fr,string IMID,string GId)
    { int var,Pay2;
    try
    {
        con.Close();
        con.Open();
        if (Pay >= IM)
        {
            var = Pay - IM;
            cmd= new SqlCommand("update IMAC set Total=0 where IMID='" + IMID.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            lblPayMessage.Text = "Update IM=0; when pay-IM=" + var.ToString();
        }
        else if (IM > Pay)
        {
            var = IM - Pay;
            IM = IM - Pay;
            cmd = new SqlCommand("update IMAC set Total='" + IM + "' where IMID='" + IMID.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            lblPayMessage.Text = "Update IM when IM-Pay=" + IM.ToString();
        }
        if (GTo <= Pay)
        {
            Pay2 = Pay - GTo;
            cmd = new SqlCommand("update IMAC set GTotal=0 where GID='" + GId.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            lblPayMessage.Text = lblPayMessage.Text.ToString() + " and Gto=0";
        }
        else if (GTo > Pay)
        {
            GTo = GTo - Pay;
            cmd = new SqlCommand("update IMAC set GTotal='" + GTo + "' where GID='" + GId.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            lblPayMessage.Text = lblPayMessage.Text.ToString() + " and GTo=GTo-Pay;";
        }
        int lt = Convert.ToInt32(lblTLate.Text) - Ltkn;
        SqlCommand cmlate = new SqlCommand("update IMAC set Late='" + lt + "' where IMID='ICE'", con);
        SqlCommand cmd1 = new SqlCommand("select * from IMCradit where IMID='" + IMID.ToString() + "'", con);
        SqlDataReader rdcradti; ad = new int[4];
        rdcradti = cmd1.ExecuteReader();
        while (rdcradti.Read())
        {
            ad[0] = Convert.ToInt32(rdcradti[2]);  // adno
            ad[1] = Convert.ToInt32(rdcradti[3]);  // adAmt
            ad[2] = Convert.ToInt32(rdcradti[4]);  //total
            ad[3] = Convert.ToInt32(rdcradti[5]); // Gtotal;
           // ad[4] = Convert.ToInt32(rdcradti[6]); //GId;
        }
        rdcradti.Close();
        cmlate.ExecuteNonQuery();
        cmd = new SqlCommand("update IMCradit set AdNo='" + (ad[0] - Fr) + "',AdAmt='" + (ad[1] - Ltkn) + "',Total='" + (ad[2] - Ltkn) + "' where IMID='" + IMID.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("update IMCradit set GTotal='" + (ad[1] - Pay) + "' where GID='" + GId + "'", con);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("update top(" + Fr + ") Student set Status='yes' where IMID='" + IMID.ToString() + "' and Status='no'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    catch (SqlException ex)
    {
        lblPayMessage.Text = ex.ToString();
    }
    catch (Exception ex)
    {
        lblPayMessage.Text = ex.ToString();
    }
    finally
    {
    }
    }
}
