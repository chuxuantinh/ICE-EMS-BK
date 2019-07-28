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

public partial class project_UpdProStatus : System.Web.UI.Page
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
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum"; else ddlsession.SelectedValue = "Win";
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    ddlsession.Focus();
                    GridBind();
                    btnOldProject.Enabled = false;
                    btnRunning.Enabled = false;
                    pnlSID.Visible = false;
                }
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
    protected void ddlViewBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSID.Visible = false; pnlStatus.Visible = false;
        if (ddlViewBy.SelectedValue == "Status")
        {
            pnlStatus.Visible = true;
            ddlStatus.Focus();
        }
        else if (ddlViewBy.SelectedValue == "Membership")
        {
            pnlSID.Visible = true;
            txtSIDStatus.Focus();
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GridBind();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEditStatus.Items.Clear(); ddlSynosisStatus.Items.Clear();
        if (ddlStatus.SelectedValue == "Selected")
        {
        }
        else if (ddlStatus.SelectedValue == "ProformaASubmitted")
        {
            ddlEditStatus.Items.Add("Selected"); ddlSynosisStatus.Items.Add("NotSubmitted");
        }
        else if (ddlStatus.SelectedValue == "ProformaAApproved")
        {
            ddlEditStatus.Items.Add("ProformaASubmitted"); ddlSynosisStatus.Items.Add("Submitted"); ddlSynosisStatus.Items.Add("Approved"); ddlSynosisStatus.Items.Add("Rejected");
        }
        else if (ddlStatus.SelectedValue == "ProformaBSubmitted")
        {
            ddlEditStatus.Items.Add("ProformaAApproved"); ddlSynosisStatus.Items.Add("Submitted"); ddlSynosisStatus.Items.Add("Approved"); ddlSynosisStatus.Items.Add("Rejected");
        }
        else if (ddlStatus.SelectedValue == "ProformaBApproved")
        {
            ddlEditStatus.Items.Add("ProformaBSubmitted"); ddlSynosisStatus.Items.Add("Approved");
        }
        else if (ddlStatus.SelectedValue == "CopySubmitted")
        {
            ddlEditStatus.Items.Add("ProformaBApproved"); ddlSynosisStatus.Items.Add("Approved");
        }
        else if (ddlStatus.SelectedValue == "CopyPending")
        {
            ddlEditStatus.Items.Add("CopySubmitted"); ddlSynosisStatus.Items.Add("Approved");
        }
        else if (ddlStatus.SelectedValue == "CopyDispatched")
        {
            ddlEditStatus.Items.Add("CopyPending"); ddlEditStatus.Items.Add("CopySubmitted"); ddlSynosisStatus.Items.Add("Approved");
        }
        else if (ddlStatus.SelectedValue == "Approved")
        {
            ddlEditStatus.Items.Add("CopyDispatched"); ddlSynosisStatus.Items.Add("Approved");
        }
        else if (ddlStatus.SelectedValue == "Rejected")
        {
            ddlEditStatus.Items.Add("Selected"); ddlSynosisStatus.Items.Add("NotSubmitted");
            ddlEditStatus.Items.Add("ProformaASubmitted");
            ddlEditStatus.Items.Add("ProformaAApproved"); ddlSynosisStatus.Items.Add("Submitted");
            ddlEditStatus.Items.Add("ProformaBSubmitted"); ddlSynosisStatus.Items.Add("Approved");
            ddlEditStatus.Items.Add("ProformaBApproved"); ddlSynosisStatus.Items.Add("ReSubmit");
            ddlEditStatus.Items.Add("CopySubmitted"); ddlSynosisStatus.Items.Add("Rejected");
            ddlEditStatus.Items.Add("CopyPending");
            ddlEditStatus.Items.Add("CopyDispatched");
            ddlEditStatus.Items.Add("Approved");
        }
        ddlEditStatus.Items.Add("Rejected");
    }
    protected void btnSIDView_Click(object sender, EventArgs e)
    {
        adp = new SqlDataAdapter("select SN, SID,StudentName,IMID,Course,Part,Session,DiaryB,DiaryC,Status,SynopsisStatus,NoOfCopies,Grade,EntryStatus from Project where SID='" + txtSIDStatus.Text.ToString() + "' order by SID", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridupdProStatus.DataSource = dt;
        GridupdProStatus.DataBind();
        ddlEditStatus.Items.Clear(); ddlSynosisStatus.Items.Clear();
        ddlEditStatus.Items.Add("Selected"); ddlSynosisStatus.Items.Add("NotSubmitted");
        ddlEditStatus.Items.Add("ProformaASubmitted");
        ddlEditStatus.Items.Add("ProformaAApproved"); ddlSynosisStatus.Items.Add("Submitted");
        ddlEditStatus.Items.Add("ProformaBSubmitted"); ddlSynosisStatus.Items.Add("Approved");
        ddlEditStatus.Items.Add("ProformaBApproved"); ddlSynosisStatus.Items.Add("ReSubmit");
        ddlEditStatus.Items.Add("CopySubmitted"); ddlSynosisStatus.Items.Add("Rejected");
        ddlEditStatus.Items.Add("CopyPending");
        ddlEditStatus.Items.Add("CopyDispatched");
        ddlEditStatus.Items.Add("Approved");
        ddlEditStatus.Items.Add("Rejected");
        btnView.Focus();
    }
    private void GridBind()
    {
        adp = new SqlDataAdapter("select SID,StudentName,IMID,Course,Part,Session,DiaryB,DiaryC,Status,SynopsisStatus, NoOfCopies,Grade,EntryStatus from Project where Status='" + ddlStatus.SelectedValue + "' and Session='" + lblSessionHiddend.Text.ToString() + "' order by SID", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            GridupdProStatus.DataSource = dt;
            GridupdProStatus.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strqry;
        try
        {
            int i = 0;
            con.Close(); con.Open();
            while (i < GridupdProStatus.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GridupdProStatus.Rows[i].FindControl("chkStatus");
                if (rbApp.Checked)
                {
                    strqry = "update Project set Status='" + ddlEditStatus.SelectedValue.ToString() + "', SynopsisStatus='" + ddlSynosisStatus.SelectedValue.ToString() + "' where SN='" + GridupdProStatus.Rows[i].Cells[1].Text + "'  and Session='" + GridupdProStatus.Rows[i].Cells[7].Text + "'";
                    cmd = new SqlCommand(strqry, con);
                    cmd.ExecuteNonQuery();
                }
                i++;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Project Status Updated Successfully.')", true);
            con.Close();
          //  GridBind();
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
        catch (NullReferenceException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
        catch (OutOfMemoryException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Dispose();
        }
    }
    private void bindProject()
    {
        adp = new SqlDataAdapter("select SN,SID,StudentName,Session,Status,SynopsisStatus,EntryStatus from Project where SID='" + txtSid.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridEval.DataSource = dt;
        GridEval.DataBind();
    }
    protected void txtSID_OnTextChanted(object sender, EventArgs e)
    {
        bindProject();
    }
    protected void GridEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRunning.Enabled = true;
        btnOldProject.Enabled = true;
    }
    protected void btnRunning_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("update Project Set EntryStatus='OldProject' where SID='" + txtSid.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();

        cmd = new SqlCommand("update Project set EntryStatus='Running' where SID='" + txtSid.Text.ToString() + "' and SN='" +Convert.ToInt32(GridEval.SelectedRow.Cells[1].Text.ToString()) + "'", con);
        cmd.ExecuteNonQuery();
        bindProject(); 
        con.Close(); con.Dispose();
    }
    protected void btnOldProject_click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("update Project Set EntryStatus='OldProject' where SID='" + txtSid.Text.ToString() + "' and SN='" +Convert.ToInt32(GridEval.SelectedRow.Cells[1].Text.ToString()) + "'", con);
        cmd.ExecuteNonQuery();
        bindProject();
        con.Close(); con.Dispose();
    }
}