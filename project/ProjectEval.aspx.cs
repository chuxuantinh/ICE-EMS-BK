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
public partial class project_ProjectEval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                    txtcDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlsession.Focus(); BindGridEval();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    private void filldescriptn()
    {
        if (lblprojecttitle.Text == "")
        {
            lblprojecttitle.Text = "Project Title Not Found!";
        }
        else
        {
            cmd = new SqlCommand("select Description from Project where ProjectTitle='" + lblprojecttitle.Text.ToString() + "' and EntryStatus='Running'", con);
            lbldescription.Text = cmd.ExecuteScalar().ToString();
            lblexception.Text = "";
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
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    public void readid(string id)
    {
        cmd = new SqlCommand("select * from Project where SID='" + id.ToString() + "' and EntryStatus='Running'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            if (dr["Status"].ToString() == "CopyDispatched")
            {
                Pnlresult.Visible = true;
                lblprojecttitle.Text = dr["ProjectTitle"].ToString();
                lblgroupmate1.Text = dr["GroupMate1"].ToString();
                lblgroupmate2.Text = dr["GroupMate2"].ToString();
                lblgroupmate3.Text = dr["GroupMate3"].ToString();
                lblIMID.Text = dr["IMID"].ToString();
                lblcourse.Text = dr["Course"].ToString();
                lblpart.Text = dr["Part"].ToString();
                if (lblpart.Text == "PartI" | lblpart.Text == "PartII")
                    lblstream.Text = "Technician";
                else
                    lblstream.Text = "Associate";
                string strCheckDate = dr["SynopsisDate"].ToString();
                if (strCheckDate == "") lbldate.Text = "";
                else lbldate.Text = Convert.ToDateTime(dr["SynopsisDate"].ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                lblexception.Text = "InValid";
            }
        }
        dr.Close(); dr.Dispose();
    }
    private void BindGridEval()
    {
        adp = new SqlDataAdapter("select SID,StudentName,IMID,Course,Part,InstitutionID,Institution,CourseStatus from Project where Status='CopyDispatched' and Session='" + lblSessionHiddend.Text.ToString() + "' and EntryStatus='Running'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridEval.DataSource = dt;
        GridEval.DataBind();
    }
    protected void rgroupmate1_CheckedChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        rfetchdata(lblgroupmate1.Text.ToString());
        con.Close(); con.Dispose();
    }
    protected void rgroupmate2_CheckedChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        rfetchdata(lblgroupmate2.Text.ToString());
        con.Close(); con.Dispose();
    }
    protected void rgroupmate3_CheckedChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        rfetchdata(lblgroupmate3.Text.ToString());
        con.Close(); con.Dispose();
    }
    private void rfetchdata(string id)
    {
        if (id.ToString() == "")
        {
            lblexeption.Text = "Please Select Membership No."; rgroupmate1.Checked = false; rgroupmate2.Checked = false; rgroupmate3.Checked = false;
        }
        else
        {
            cmd = new SqlCommand("select StudentName from Project where SID='" + id.ToString() + "' and EntryStatus='Running'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblstuname.Text = dr["StudentName"].ToString();
                lblgroupmateid.Text = id.ToString();
            }
            dr.Close(); dr.Dispose();
            readid(id.ToString());
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            if (txtcDate.Text == "")
            {
                lblexeption.Text = "Please enter Details!";
            }
            else
            {
                con.Close(); con.Open();
                cmd = new SqlCommand("update project set Status=@Status, EvalutionDate=@EvalutionDate where SID='" + lblgroupmateid.Text.ToString() + "' and EntryStatus='Running'", con);
                cmd.Parameters.AddWithValue("@Status", "Approved");
                cmd.Parameters.AddWithValue("@EvalutionDate", Convert.ToDateTime(txtcDate.Text.ToString(), dtinfo));
                cmd.ExecuteNonQuery();
                con.Close(); con.Dispose();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Project Evaluated Successfully.')", true);
                lblexeption.Text = ""; Pnlresult.Visible = false; BindGridEval();
            }
        }
        catch (FormatException ex)
        {
            lblexeption.Text = "Invalid Date Format!";
        }
    }
    protected void GridEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        rfetchdata(GridEval.SelectedRow.Cells[1].Text.ToString());
        lblgroupmateid.Visible = false;
        filldescriptn(); pnlComp.Visible = true; rgroupmate1.Focus();
        con.Close(); con.Dispose();
    }
    protected void txtSid_TextChanged(object sender, EventArgs e)
    {
        if (txtSid.Text == "")
        {
            lblexception.Text = "Please insert Membership No";
            txtSid.Focus();
        }
        else
        {
            adp = new SqlDataAdapter("select SID,StudentName,IMID,Course,Part,InstitutionID,Institution,CourseStatus from Project where Status='CopyDispatched' and SID='"+txtSid.Text.ToString()+"' and EntryStatus='Running'", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            GridEval.DataSource = dt;
            GridEval.DataBind();
        }
    }
    protected void btnRej_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            if (txtcDate.Text == "")
            {
                lblexeption.Text = "Please enter Details!";
            }
            else
            {
                con.Close(); con.Open();
                cmd = new SqlCommand("update project set Status=@Status, EvalutionDate=@EvalutionDate where SID='" + lblgroupmateid.Text.ToString() + "' and EntryStatus='Running'", con);
                cmd.Parameters.AddWithValue("@Status", "Rejected");
                cmd.Parameters.AddWithValue("@EvalutionDate", Convert.ToDateTime(txtcDate.Text.ToString(), dtinfo));
                cmd.ExecuteNonQuery();
                con.Close(); con.Dispose();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Project Rejected Successfully.')", true);
                lblexeption.Text = ""; Pnlresult.Visible = false; BindGridEval();
            }
        }
        catch (FormatException ex)
        {
            lblexeption.Text = "Invalid Date Format!";
        }
    }
}