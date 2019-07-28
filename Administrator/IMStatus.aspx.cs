using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Administrator_IMStatus : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Conn"]);
 
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

                lblEnrolment.Text = Request.QueryString["id"].ToString();

                viewprofile(lblEnrolment.Text.ToString());

            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {

            maikal mk = new maikal();
           int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
                // Response.Redirect("Admin/SuperAdminManage.aspx");
                
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }

    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + txtEnrolment.Text.ToString() + "'", con);
        string strstatus = Convert.ToString(cmd.ExecuteScalar());
        if (strstatus == txtEnrolment.Text.ToString())
        {
            lblEnrolment.Text = txtEnrolment.Text.ToString();
            Response.Redirect("IMRegi.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + lblEnrolment.Text.ToString());
        }
        else if (strstatus != txtEnrolment.Text.ToString())
        {
            txtEnrolment.Text = "Invalid IM ID.";
        }
        con.Close(); con.Dispose();
    }


    public void viewprofile(string id)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        try
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Member where ID='" + id.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //lblName.Text = reader[3].ToString() + "\0\0 " + reader[4].ToString();
                //lblFName.Text = reader[5].ToString();
                //lblDesignation.Text = reader[6].ToString();
                //lblAddress.Text = reader[7].ToString();
                //lblState.Text = reader[8].ToString();
                //lblPincode.Text = reader[9].ToString();
                //lblTelNo.Text = reader[10].ToString();
                //lblOffice.Text = reader[11].ToString();
                //lblResidential.Text = reader[12].ToString();
                //lblMobile.Text = reader[13].ToString();
                //lblEmail.Text = reader[14].ToString();
                //lblDateBirth.Text = Convert.ToString(reader[15].ToString());
                //lblAge.Text = reader[16].ToString();
                //lblHeigestQualification.Text = reader[17].ToString();
                //lblDesipliceBE.Text = reader[19].ToString();
                //lblDesipliineMTech.Text = reader[20].ToString();
                //lblDesiplinePhd.Text = reader[21].ToString();
                //lbldesipleineohter.Text = reader[22].ToString();
                //lblCollegeBE.Text = reader[23].ToString();
                //lblCollegeMTech.Text = reader[24].ToString();
                //lblCollegePhd.Text = reader[25].ToString();
                //lblCollegeohter.Text = reader[26].ToString();
                //lblPercentageBE.Text = reader[27].ToString();
                //lblPercentageMTech.Text = reader[28].ToString();
                //lblPercentagePhd.Text = reader[29].ToString();
                //lblpercentageohter.Text = reader[30].ToString();
                //lblUniversityBE.Text = reader[31].ToString();
                //lbluniversityMTech.Text = reader[32].ToString();
                //lblUniversityPhd.Text = reader[33].ToString();
                //lbluniversigtyohter.Text = reader[34].ToString();
                //lblYearBE.Text = reader[35].ToString();
                //lblyearMTech.Text = reader[36].ToString();
                //lblyearphd.Text = reader[37].ToString();
                //lblyearother.Text = reader[38].ToString();
                //lblPost1.Text = reader[40].ToString(); lblPost2.Text = reader[41].ToString(); lblPost3.Text = reader[42].ToString(); lblPost4.Text = reader[43].ToString(); lblPost5.Text = reader[44].ToString();
                //lblOrg1.Text = reader[45].ToString(); lblOrg2.Text = reader[46].ToString(); lblOrg3.Text = reader[47].ToString(); lblOrg4.Text = reader[48].ToString(); lblOrg5.Text = reader[49].ToString();
                //lblAdd1.Text = reader[50].ToString(); lblAdd2.Text = reader[51].ToString(); lblAdd3.Text = reader[52].ToString(); lblAdd4.Text = reader[53].ToString(); lblAdd5.Text = reader[54].ToString();
                //lblf1.Text = reader[55].ToString(); lblf2.Text = reader[56].ToString(); lblf3.Text = reader[57].ToString(); lblf4.Text = reader[58].ToString(); lblf5.Text = reader[59].ToString();
                //lblt1.Text = reader[60].ToString(); lblt2.Text = reader[61].ToString(); lblt3.Text = reader[62].ToString(); lblt4.Text = reader[63].ToString(); lblt5.Text = reader[64].ToString();
                //  lbtnExpStatus.Text = reader[77].ToString();
                if (reader[71].ToString() == "yes") { lblStatus.Text = "Active"; lblStatus2.Text = "Active"; } else if (reader[71].ToString() == "no") { lblStatus.Text = "Disactive"; lblStatus2.Text = "Disactive"; }
                lblEnrollDate.Text = Convert.ToDateTime(reader[72].ToString(),dtinfo).ToString("dd/MM/yyyy");
                lblRenuwalDate.Text = Convert.ToDateTime(reader[73].ToString(),dtinfo).ToString("dd/MM/yyyy");
                lblExpDate.Text = Convert.ToDateTime(reader[74].ToString(),dtinfo).ToString("dd/MM/yyyy");
                lblyear.Text = Convert.ToDateTime(reader[75].ToString(),dtinfo).ToString("dd/MM/yyyy") + " TO " + Convert.ToDateTime(reader[76].ToString(),dtinfo).ToString("dd/MM/yyyy");
                // lblExpCase.Text = reader[78].ToString();

            }
            reader.Close(); reader.Dispose();

        }
        catch (SqlException ex)
        {
            // lblExceptionViewProfiel.Text = ex.ToString();
        }
        catch (FormatException ex)
        {
        }
        finally
        {
            con.Close(); con.Dispose();
            if (lblStatus.Text == "Disactive")
            {
                btnchangeStatus.Text = "Active";
            }
            else if (lblStatus.Text == "Active")
            {
                btnchangeStatus.Text = "Disactive";
            }
        }

    }
    string strstatus;
    protected void btnChnageStatsu(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        if (lblStatus.Text == "Disactive")
        {
            btnchangeStatus.Text = "Active"; strstatus = "yes";
        }
        else if (lblStatus.Text == "Active")
        {
            btnchangeStatus.Text = "Disactive"; strstatus = "no";
        }
        SqlCommand cmd = new SqlCommand("update Member set Active='" + strstatus.ToString() + "' where ID='" + lblEnrolment.Text + "'", con);
        cmd.ExecuteNonQuery();
        con.Close(); con.Dispose();
        string url;
        url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
       

    }
}