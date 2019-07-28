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
using System.Xml;
using iTextSharp.text.html.simpleparser;

public partial class Administrator_UpdateDates : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
    ClsStateCity statecity = new ClsStateCity(); DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Convert.ToString(MyLogin.login[0]) == "")
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"].ToString() == "")
                {
                    txtEnrolment.Focus();
                }
                else
                {
                    lblEnrolment.Text = Request.QueryString["id"].ToString();
                    fetchdata();
                }
            }
        }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Open();
            string stn = "update Member set RegDate=@RegDate,RenewDate=@RenewDate,ExpDate=@ExpDate,YearFrom=@YearFrom,YearTo=@YearTo where ID='" + lblEnrolment.Text + "'";
            SqlCommand cmd = new SqlCommand(stn, con);
            cmd.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(txtregi.Text, dtinfo));
            cmd.Parameters.AddWithValue("@RenewDate", Convert.ToDateTime(Txtrenew.Text, dtinfo));
            cmd.Parameters.AddWithValue("@ExpDate", Convert.ToDateTime(Txtexpiry.Text, dtinfo));
            cmd.Parameters.AddWithValue("@YearFrom", ddlSession.SelectedValue.ToString() + txtYear.Text.ToString());
            cmd.Parameters.AddWithValue("@YearTo", ddlSession.SelectedValue.ToString() + (Convert.ToInt32(txtYear.Text) + 1).ToString());
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            Lblmesg.Text = "Details updated";
    }
    private void fetchdata()
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select RegDate,RenewDate,ExpDate,YearFrom from Member where ID ='" + lblEnrolment.Text + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtregi.Text = Convert.ToDateTime(dr["RegDate"].ToString()).ToString("dd/MM/yyyy");
                Txtrenew.Text = Convert.ToDateTime(dr["RenewDate"].ToString()).ToString("dd/MM/yyyy");
                Txtexpiry.Text = Convert.ToDateTime(dr["ExpDate"].ToString()).ToString("dd/MM/yyyy");
                string yearfrom = dr["YearFrom"].ToString();
                string aa = yearfrom;
                yearfrom = yearfrom.Substring(0, 3);
                if (yearfrom == "Win")
                {
                    ddlSession.SelectedValue = "Winter Examination";
                }
                else
                {
                    ddlSession.SelectedValue = "Summer Examination";
                }
               aa = aa.Substring(3, 4);
               txtYear.Text = aa;
            }
        }
        catch (IndexOutOfRangeException ex)
        {
        }
        catch(NullReferenceException ex)
        {
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    protected void btnViewEnroll_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Response.Redirect("../Administrator/UpdateDates.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + txtEnrolment.Text.ToString() + "");
        }
        else
        {
            txtEnrolment.Text = "Invalid Id";
        }
        dr.Close();
        con.Close(); con.Dispose();
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {

    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(MyLogin.login[1].ToString(), MyLogin.login[0].ToString());
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
}