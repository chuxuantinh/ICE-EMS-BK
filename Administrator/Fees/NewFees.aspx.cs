using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Administrator_Fees_NewFees : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                FeeMaster fm = new FeeMaster();
                lblNewid.Text = fm.rtnlvl(lblExamSeasonHidden.Text);
                ddlExamSeason.Focus();
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        FeeMaster fm = new FeeMaster();
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        lblNewid.Text = fm.rtnlvl(lblExamSeasonHidden.Text);
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeMaster fm = new FeeMaster();       
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        lblNewid.Text = fm.rtnlvl(lblExamSeasonHidden.Text);
        txtYearSeason.Focus();
    }
    protected void btnCreateNew_Onclick(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("select FeeLevel from FeeMaster where FeeLevel='" + lblNewid.Text.ToString() + "' and type='" + Request.QueryString["type"] + "'", con);
        string have = Convert.ToString(cmd.ExecuteScalar());
        if (have == lblNewid.Text.ToString())
        {
            lblExceptionID.Text = "Already Create For This Level, To make Change Update Fees Schedule.";
            btnCreateNew.Text = "Generate Schedule";
        }
        else
        {
            cmd = new SqlCommand("insert into FeeMaster(FeeType,FeeLevel,type) values(@FeeType,@FeeLevel,@type)", con);
            cmd.Parameters.AddWithValue("@FeeType", "Asso");
            cmd.Parameters.AddWithValue("@FeeLevel", lblNewid.Text.ToString());
            cmd.Parameters.AddWithValue("@type", Request.QueryString["type"]);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into FeeMaster(FeeType,FeeLevel,type) values(@FeeType,@FeeLevel,@type)", con);
            cmd.Parameters.AddWithValue("@FeeType", "Tech");
            cmd.Parameters.AddWithValue("@FeeLevel", lblNewid.Text.ToString());
            cmd.Parameters.AddWithValue("@type", Request.QueryString["type"]);
            cmd.ExecuteNonQuery();
            btnCreateNew.Text = "Successfully Generated";
            lblExceptionID.Text = "";
        }
        con.Close();
        con.Dispose();
    }
}
