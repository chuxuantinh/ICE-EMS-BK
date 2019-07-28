using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;

public partial class project_ApproveProfB : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataAdapter adp;
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
                    pnlmain.Visible = false;
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum"; else ddlsession.SelectedValue = "Win";
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    ddlsession.Focus();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    bindgrid();
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
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (i == 0 | i == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (i == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        bindgrid();
        GridShow.Focus();
    }
    protected void GridShow_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlmain.Visible = true;
        con.Close(); con.Open();
        GridViewRow row = GridShow.SelectedRow;
        cmd = new SqlCommand("select * from Project where SID='" + row.Cells[1].Text + "' and EntryStatus='Running'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblGID.Text = rd["GroupID"].ToString();
            lblSynopsistitle.Text = rd["SynopsisTitle"].ToString();lblINstID.Text = rd["InstitutionID"].ToString();lblINStname.Text = rd["Institution"].ToString();
            lblgmate.Text = rd["Groupmate1"].ToString(); lblgmate1.Text = rd["GroupMate2"].ToString(); lblgmate2.Text = rd["GroupMate3"].ToString();
            txtSynRemarks.Text = rd["SynopsisRemarks"].ToString();
            lblprojecttitle.Text = lblSynopsistitle.Text.ToString();
        }
        rd.Close();
        if (lblgmate1.Text != "")
        {
            cmd = new SqlCommand("select * from Project where SID='" + lblgmate1.Text + "' and EntryStatus='Running'", con);
             rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                lblCourse1.Text = rd["Course"].ToString();
                lblPart1.Text = rd["Part"].ToString();
                lblSynopsisTitle1.Text = rd["SynopsisTitle"].ToString(); 
                lblInstID1.Text = rd["InstitutionID"].ToString(); 
                lblINstName1.Text = rd["Institution"].ToString();
                lblProjectTitle1.Text = rd["ProjectTitle"].ToString();
            }
            rd.Close();
        }
        if (lblgmate2.Text != "")
        {
            cmd = new SqlCommand("select * from Project where SID='" + lblgmate2.Text + "' and EntryStatus='Running'", con);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                lblCourse2.Text = rd["Course"].ToString();
                lblPart2.Text = rd["Part"].ToString();
                lblSynopsisTitle2.Text = rd["SynopsisTitle"].ToString(); 
                lblINstID2.Text = rd["InstitutionID"].ToString(); 
                lblINstName2.Text = rd["Institution"].ToString();
             lblprojectTitle2.Text = rd["ProjectTitle"].ToString();
            }
            rd.Close();
        }
        con.Close(); con.Dispose();
    }
    protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        bindgrid();
        GridShow.Focus();
    }
    private void bindgrid()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select SID,IMID,Session,ProjectTitle from Project where Status='ProformaBsubmitted' and (SynopsisStatus='Submitted' or SynopsisStatus='Pending')  and EntryStatus='Running' and Session='" + lblSessionHiddend.Text + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridShow.DataSource = dt;
        GridShow.DataBind();
    }
    protected void btnSIDView_Click(object sender, EventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select SID as Membership,IMID,Course,Part,Session from Project where Status='ProformaBsubmitted' and (SynopsisStatus='Submitted' or SynopsisStatus='Pending')  and EntryStatus='Running' and SID='" + txtSID.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridShow.DataSource = dt;
        GridShow.DataBind();
        GridShow.Focus();
    }
    private string chkgid(string id, string session)
    {
        string gid = "";
        cmd = new SqlCommand("select GroupID from Project where SId='" + id + "' and EntryStatus='Running'", con);
        gid = Convert.ToString(cmd.ExecuteScalar());
        return gid;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            cmd = new SqlCommand("update Project set Status=@Status,ProjectTitle=@ProjectTitle,Description=@Description, ProjectAppDate=@ProjectAppDate,SynopsisRemarks=@SynopsisRemarks,SynopsisStatus=@SynopsisStatus,LetterRemarks=@LetterRemarks where SID='" + lblgmate.Text + "' and EntryStatus='Running'", con);
            cmd.Parameters.AddWithValue("@Status", "ProformaBApproved");
            cmd.Parameters.AddWithValue("@ProjectTitle", lblprojecttitle.Text.ToString());
            cmd.Parameters.AddWithValue("@Description", txtProjectDescription.Text.ToString());
            cmd.Parameters.AddWithValue("@ProjectAppDate", Convert.ToDateTime(txtDate.Text, dtinfo));
            cmd.Parameters.AddWithValue("@SynopsisRemarks", txtSynRemarks.Text.ToString());
            cmd.Parameters.AddWithValue("@SynopsisStatus", ddlSysStatus.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@LetterRemarks",txtLetterRemarsk.Text.ToString());
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Approved.')", true);
            pnlmain.Visible = false;
            GridShow.SelectedRow.Visible = false;
            con.Close(); con.Dispose();
        }
        catch (NullReferenceException ex)
        {
            lblexcep.Text = ex.ToString();
        }
        catch(FormatException ex)
        {
            lblexcep.Text = ex.ToString();
        }
    }
    protected void lnKCheckButton_Click(object sender, EventArgs e)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ProjectTitle,SID,Session,GroupMate1,Groupmate2,Groupmate3 from Project where FREETEXT(ProjectTitle,'" + lblprojecttitle.Text + "')", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        grdChecktitle.DataSource = dt;
        grdChecktitle.DataBind();
        txtProjectDescription.Focus();
    }
}