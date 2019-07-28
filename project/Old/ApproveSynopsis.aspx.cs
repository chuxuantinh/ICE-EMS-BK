using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Data;

public partial class project_ApproveSynopsis : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum"; else ddlsession.SelectedValue = "Win";
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    lblcdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlsession.Focus();
                }
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
    private void filldescriptn()
    {
        if (lblprojecttitle.Text == "")
        {
            lblexception.Text = "Membership ID Invalid Please Enter Correct Membership ID. !";
            lblexception.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            con.Close(); con.Open();
            SqlCommand cmd1 = new SqlCommand("select Description from ProjectName where ProjectTitle='" + lblprojecttitle.Text.ToString() + "'", con);
            lbldescription.Text = cmd1.ExecuteScalar().ToString();
            lblexception.Text = "";
            con.Close();
        }
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    public void readid(string id)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select SynopsisTitle,GroupID,GroupMate1,GroupMate2,GroupMate3,IMID,Stream,Course,Part,SynopsisDate,Status from Project where SID='" + id.ToString() + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            if (dr["Status"].ToString() == "NotApproved")
            {
                lblshow.Text = "";
                pnlresult.Visible = true;
                lblprojecttitle.Text = dr["SynopsisTitle"].ToString();
                lblgroupid.Text = dr["GroupID"].ToString();
                lblgroupmate1.Text = dr["GroupMate1"].ToString();
                lblgroupmate2.Text = dr["GroupMate2"].ToString();
                lblgroupmate3.Text = dr["GroupMate3"].ToString();
                lblIMID.Text = dr["IMID"].ToString();
                lblstream.Text = dr["Stream"].ToString();
                lblcourse.Text = dr["Course"].ToString();
                lblpart.Text = dr["Part"].ToString();
                lbldate.Text = Convert.ToDateTime(dr["SynopsisDate"].ToString()).ToString("dd/MM/yyyy");
                lblstatus.Text = dr["Status"].ToString();
                filldescriptn();
            }
            else
            {
                lblshow.Text = "Status:" + dr["Status"].ToString();
                pnlresult.Visible = false;
            }
        }
        dr.Close();
        con.Close();
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            dtfi.DateSeparator = "/";
            con.Close();
            con.Open();
            SqlCommand cmd2 = new SqlCommand("update AppRecord set Status='Filled' where AppNo='"+lblAppNo.Text+"' ", con);
            cmd2.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("update project set status=@Status,SynopsisDate=@SynopsisDate,ProjectTitle=@ProjectTitle where GroupID='" + lblgroupid.Text.ToString() + "'", con);
            SqlCommand cmd1 = new SqlCommand("update ProjectName set ProjectTitle=@ProjectTitle where Description='" + lbldescription.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Status", "Approved");
            cmd.Parameters.AddWithValue("@SynopsisDate", Convert.ToDateTime(lblcdate.Text.ToString(), dtfi));
            cmd.Parameters.AddWithValue("@ProjectTitle", lblprojecttitle.Text.ToString());
            cmd1.Parameters.AddWithValue("@ProjectTitle", lblprojecttitle.Text.ToString());
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            lblexeption.Text = "Project Approved";
            btnSave.Enabled = false;
        }
        catch (Exception ex)
        {
            lblexeption.Text = ex.ToString();
        }
    }
    protected void rgroupmate1_CheckedChanged(object sender, EventArgs e)
    {
        rfetchdata(lblgroupmate1.Text.ToString());
    }
    protected void rgroupmate2_CheckedChanged(object sender, EventArgs e)
    {
        rfetchdata(lblgroupmate2.Text.ToString());
    }
    protected void rgroupmate3_CheckedChanged(object sender, EventArgs e)
    {
        rfetchdata(lblgroupmate3.Text.ToString());
    }
    private void rfetchdata(string id)
    {
        if (id.ToString() == "")
        {
            lblexeption.Text = "Please Select Membership No.";
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select StudentName from Project where SID='" + id.ToString() + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblstuname.Text = dr["StudentName"].ToString();
                readid(id.ToString());
            }
            else
                lblshow.Text = "Submit Synopsis first";
            dr.Close();
            con.Close();
            
        }
    }
    protected void btbClear_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string myString = lblprojecttitle.Text.ToString();
        char[] separator = new char[] { ' ' };
        string[] str = myString.Split(separator);
        string str1 = null;
        con.Open();
        if (str.Length == 0)
        {
            str1 = str[0];
        }
        else
        {
            for (int i = 0; i < str.Length; i++)
            {
                str1 = str[i] + "%'or ProjectTitle like '" + str1;
            }
        }
        SqlCommand cmd = new SqlCommand("select ProjectTitle,Description from ProjectName where ProjectTitle like '%" + str1 + " %'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        Panel1.Visible = true;
        GridView1.DataSource = reader;
        GridView1.DataBind();
        con.Close();
        con.Dispose();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {

    }
    string sn;
    protected void btnshow_Click(object sender, EventArgs e)
    {
        con.Open();
        if(pnlresult.Visible ==true)
        {
            pnlSpace.Visible = false;
        }
        SqlCommand cmd;
        string Prid = txtsearch.Text + "Approval";
        cmd = new SqlCommand("select * from AppRecord where FormType='"+Prid+"' and Status='no' ", con);
        SqlDataReader read = cmd.ExecuteReader();
        if (read.Read())
        {
            sn = read["Enrolment"].ToString();
            lblAppNo.Text = read["AppNo"].ToString();
            lblMem.Text = sn;
            rfetchdata(sn);
            
        }
        else
        {

            lblshow.Text = "Invalid Serial No:";
        }
        read.Close(); read.Dispose();
       
        con.Close(); con.Dispose();
    }
}