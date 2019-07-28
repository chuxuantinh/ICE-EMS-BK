using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;

public partial class Acc_LateFeeAC : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con=new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataAdapter ad;
    IMInfo iminfo;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                fillGridLatFee();
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
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 1)
            {
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
            else if (lvl == 2)
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    private void fillGridLatFee()
    {
        //ad = new SqlDataAdapter("select  FeeAc.IMID,FeeAC.Amt,FeeAC.SubDate,FeeAC.DDNO,FeeAC.Bank,FeeAC.Session,FeeAC.DiaryNo from FeeAC left join IMAC on IMAC.IMID=FeeAC.IMID where IMAC.Late>0 and IMAC.IMID!='ICE' and FeeAC.Amt<=IMAC.Late and FeeAC.Amt<=IMAC.Total", con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //GridIM.DataSource = ds;
        //GridIM.DataBind();
    }
    protected void GridIM_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        pnlDonor.Visible = true;
        pnlshow.Visible = true;
        pnlBene.Visible = false;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        iminfo = new IMInfo();
        string[] strInfo = iminfo.info(GridIM.SelectedRow.Cells[1].Text.ToString());
        lblDIMID.Text = GridIM.SelectedRow.Cells[1].Text.ToString();
        lblDName.Text = strInfo[0].ToString();
        lblDAddress1.Text = strInfo[1].ToString();
        lblDAddress2.Text = strInfo[2].ToString();
        lblDCity.Text = strInfo[3].ToString() + ", " + strInfo[4].ToString() + "-" + strInfo[5].ToString();

        String[] strimac=iminfo.imac(GridIM.SelectedRow.Cells[1].Text.ToString());
        lblDLateFees.Text=strimac[0].ToString().TrimEnd('0').TrimEnd('.');
        lblDAmount.Text = strimac[1].ToString().TrimEnd('0').TrimEnd('.');
        lblDGAmount.Text = strimac[2].ToString().TrimEnd('0').TrimEnd('.');

        lblSubDate.Text = GridIM.SelectedRow.Cells[3].Text.ToString();
        lblDDNo.Text = GridIM.SelectedRow.Cells[4].Text.ToString();
        lblBankNo.Text = GridIM.SelectedRow.Cells[5].Text.ToString();
        lblAmount.Text = GridIM.SelectedRow.Cells[2].Text.ToString().TrimEnd('0').TrimEnd('.');
        lblDiaryNo.Text = GridIM.SelectedRow.Cells[7].Text.ToString();
        lblSession.Text = GridIM.SelectedRow.Cells[6].Text.ToString();
    }
    protected void txtBIM_ONTextChanged(object sender, EventArgs e)
    {
        pnlBene.Visible = true;
        iminfo = new IMInfo();
        if(iminfo.isIMHave(txtBIM.Text.ToString()))
        {
        string[] strinfo = iminfo.info(txtBIM.Text.ToString());
        lblBName.Text = strinfo[0].ToString();
        lblBAddress.Text = strinfo[1].ToString();
        lblBAddress2.Text = strinfo[2].ToString();
        lblBcity.Text = strinfo[3].ToString() + ", " + strinfo[4].ToString() + "-" + strinfo[5].ToString();

        string[] strimac = iminfo.imac(txtBIM.Text.ToString());
        lblBLate.Text = strimac[0].ToString();
        lblBTotal.Text = strimac[1].ToString();
        lblBGAmount.Text = strimac[2].ToString();
        }
        else txtBIM.Text="Invalid IMID.";
    }
    protected void ibtnTransfer_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void ibtnTransfer_Click1(object sender, ImageClickEventArgs e)
    {
        if (txtBIM.Text == "" || txtBIM.Text == "Invalid IMID.")
        {

        }
        else
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("SPLateFeesX",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SPLateFeesX";
            cmd.CommandTimeout = 200;
            cmd.Parameters.Clear();
            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            cmd.Parameters.Add("@DIMID", lblDIMID.Text.ToString());
            cmd.Parameters.Add("@DTotal", lblDAmount.Text.ToString());
            cmd.Parameters.Add("@DGTotal", lblDGAmount.Text.ToString());
            cmd.Parameters.Add("@DLate", lblDLateFees.Text.ToString());

            cmd.Parameters.Add("@BIMID", txtBIM.Text.ToString());
            cmd.Parameters.Add("@BTotal", lblBTotal.Text.ToString());
            cmd.Parameters.Add("@BGTotal", lblBGAmount.Text.ToString());
            cmd.Parameters.Add("@BLate", lblBLate.Text.ToString());

            cmd.Parameters.Add("@DDNO", lblDDNo.Text.ToString());
            cmd.Parameters.Add("@Bank", lblBankNo.Text.ToString());
            cmd.Parameters.Add("@Date", lblSubDate.Text.ToString());
            cmd.Parameters.Add("@Amount", lblAmount.Text.ToString());
            cmd.Parameters.Add("@DiaryNo",lblDiaryNo.Text.ToString());
            cmd.Parameters.Add("@DNDate", System.DateTime.Now.ToString());
            cmd.Parameters.Add("@Session", lblSession.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            lblmsg.Text = "Transferred Successfully";
        }
    }
}