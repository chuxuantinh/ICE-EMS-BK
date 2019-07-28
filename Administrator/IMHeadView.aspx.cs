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
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
public partial class Administrator_IMHeadView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            lblEnrolment.Text = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                getdata();
                ViewImg();
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
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
          if (i == 0 | i == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2)
            {
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
    private void getdata()
    {
        if (Request.QueryString["id"].ToString() == "")
        {

        }
        else
        {
            con.Close(); con.Open();

            SqlCommand cmd = new SqlCommand("select * from IM where ID='" + lblEnrolment.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtName.Text = reader["HName"].ToString();
                txtDEsignation.Text = reader["HDesignation"].ToString();
                txtPAddress.Text = reader["HAddress"].ToString();
                txtPCity.Text = reader["HCity"].ToString();
                txtPhonecode.Text = reader["HPhone"].ToString();
                txtFaxCode.Text = reader["HFax"].ToString();
                txtMobile.Text = reader["HMobile"].ToString();
                txtEmail.Text = reader["HEmail"].ToString();
                txtDOB.Text = reader["HDOB"].ToString();
                txtAge.Text = reader["HAge"].ToString();
                txtEducationQ.Text = reader["HEdu"].ToString();
                txtExperience.Text = reader["HExp"].ToString();


            }
            con.Close();

        }

    }
    public void ViewImg()
    {
       

        SqlCommand command = new SqlCommand("SELECT HImgName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter daimages = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        DataList1.DataSource = dt;
        DataList1.DataBind();


        SqlCommand cmdhsign = new SqlCommand("SELECT HSignName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter dasign = new SqlDataAdapter(cmdhsign);
        DataTable dtsign = new DataTable();
        dasign.Fill(dtsign);
        dlsign.DataSource = dtsign;
        dlsign.DataBind();

        SqlCommand cmd2 = new SqlCommand("SELECT AImgName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt22 = new DataTable();
        da2.Fill(dt22);
        DataList3.DataSource = dt22;
        DataList3.DataBind();


        SqlCommand cmd3 = new SqlCommand("SELECT ASignName,ID from [IM] where ID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        DataTable dt23 = new DataTable();
        da3.Fill(dt23);
        DataList4.DataSource = dt23;
        DataList4.DataBind();


        imgDefault.Visible = false;
        panelImage.Visible = true;
        Image2.Visible = false;

    }
}
