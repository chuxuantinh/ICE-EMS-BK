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
public partial class project_AllocateProject : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlDataReader dr = null;
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
                    datastructure();
                    ddlsession.Focus(); BindGridPrCpySub();
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
        if (txtProjectTitle.Text == "")
        {
            txtProjectTitle.Text = "Project Title Not Found!";
        }
        else
        {
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
        BindGridPrCpySub();
        GridPrCpySub.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        BindGridPrCpySub();
        GridPrCpySub.Focus();
    }
    public void readid(string id,SqlDataReader dr)
    {
        cmd = new SqlCommand("select * from Project where SID='" + id.ToString() + "'  and EntryStatus='Running'", con);
        dr = cmd.ExecuteReader();
        if(dr.Read())
        {
                Pnlresult.Visible = true;
                txtProjectTitle.Text = dr["ProjectTitle"].ToString();
                txtDiary.Text = dr["DiaryC"].ToString();
                lblProjectNo.Text = dr["ProjectNo"].ToString();
                lblStatus.Text = dr["Status"].ToString();
                txtgmate2.Text = dr["GroupMate2"].ToString();
                txtgmate3.Text = dr["GroupMate3"].ToString();
                txtRemarks.Text = dr["Remark"].ToString();
                txtSynopsisRemarks.Text = dr["SynopsisRemarks"].ToString();
                lbldescription.Text = dr["Description"].ToString();
                lblGID.Text = dr["GroupID"].ToString();
                lblSessionHiddend.Text = dr["Session"].ToString();
        }
        dr.Close(); dr.Dispose();
        gmate2(); gmate3();
        gmate1check();
        gmate2check();
        gmate3check();
        if (lblGID.Text.ToString() == "" | lblGID.Text == "0")
        {
            lblGID.Text = gengid().ToString();
        }
    }
    private int gengid()
    {
        cmd = new SqlCommand("select max(GroupID)+1 from Project", con);
        int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        if (count == null)
            count = 1;
        return count;
    }
    protected void ddlProjectStatus_OnSelectedINdexChanged(Object sender, EventArgs e)
    {
        BindGridPrCpySub();
    }
    private void BindGridPrCpySub()
    {
        adp = new SqlDataAdapter("select SID,StudentName,IMID,Course,Part,Grade,ApprovalFees,EvalutionFees,NoofCopies,ProjectTitle from Project where Status='" + ddlProjectStatus.SelectedValue.ToString() + "' and EntryStatus='Running' order by GroupID,SID", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridPrCpySub.DataSource = dt;
        GridPrCpySub.DataBind();
    }
    protected void btnSIDView_Click(object sender, EventArgs e)
    {
        adp = new SqlDataAdapter("select SID,StudentName,IMID,Course,Part,Grade,ApprovalFees,EvalutionFees,NoofCopies,ProjectTitle from Project where EntryStatus='Running' and SID='" + txtSID.Text.ToString() + "' ", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridPrCpySub.DataSource = dt;
        GridPrCpySub.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (checkgroupMate() == true)
        {
            try
            {
                dtinfo.ShortDatePattern = "dd/MM/yyyy";
                dtinfo.DateSeparator = "/";
                if (ddlCopy.Text == "" | txtcDate.Text == "" | txtDiary.Text == "")
                {
                    lblexeption.Text = "Please enter Details!";
                }
                else
                {
                    con.Close(); con.Open();
                    cmd = new SqlCommand("update project set status=@Status,CopySubmitDate=@CopySubmitDate,NoOfCopies=@NoOfCopies,DiaryC=@DiaryC,Remark=@Remark,SynopsisRemarks=@SynopsisRemarks,ProjectNo=@ProjectNo,GroupMate1=@GroupMate1,GroupMate2=@GroupMate2,GroupMate3=@GroupMate3,GroupID=@GroupId,ProjectTitle=@ProjectTitle where SID='" + lblSID.Text.ToString() + "' and EntryStatus='Running'", con);
                    cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CopySubmitDate", Convert.ToDateTime(txtcDate.Text.ToString(), dtinfo));
                    cmd.Parameters.AddWithValue("@NoOfCopies", ddlCopy.Text.ToString());
                    cmd.Parameters.AddWithValue("@DiaryC", txtDiary.Text.ToString());
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text.ToString());
                    cmd.Parameters.AddWithValue("@SynopsisRemarks", txtSynopsisRemarks.Text.ToString());
                    cmd.Parameters.AddWithValue("@ProjectNo", lblProjectNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@GroupMate1", txtgmate1.Text.ToString());
                    cmd.Parameters.AddWithValue("@GroupMate2", txtgmate2.Text.ToString());
                    cmd.Parameters.AddWithValue("@GroupMate3", txtgmate3.Text.ToString());
                    cmd.Parameters.AddWithValue("@GroupID", lblGID.Text.ToString());
                    cmd.Parameters.AddWithValue("@ProjectTitle", txtProjectTitle.Text.ToString());
                    cmd.ExecuteNonQuery();
                    if (ddlStatus.SelectedValue == "CopySubmitted")
                        GridPrCpySub.SelectedRow.Visible = false;
                    lblStatus.Text = ddlStatus.SelectedValue.ToString();
                    if (tgmate21.Text != "")
                    {
                        cmd = new SqlCommand("update Project set GroupID='" + lblGID.Text + "', GroupMate1='" + tgmate21.Text + "',GroupMate2='" + tgmate22.Text + "',GroupMate3='" + tgmate23.Text + "' where SID='" + tgmate21.Text + "' and EntryStatus='Running'", con);
                        cmd.ExecuteNonQuery();
                    }
                    if (tgmate31.Text != "")
                    {
                        cmd = new SqlCommand("update Project set GroupID='" + lblGID.Text + "', GroupMate1='" + tgmate31.Text + "',GroupMate2='" + tgmate32.Text + "',GroupMate3='" + tgmate33.Text + "' where SID='" + tgmate31.Text + "' and EntryStatus='Running'", con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close(); con.Dispose();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Project Copies Submitted for ID : '" + lblSID.Text.ToString() + "'')", true);
                    lblexeption.Text = ""; btnSave.Enabled = false;
                }
            }
            catch (FormatException ex)
            {
                lblexeption.Text = "Invalid Date Format!";
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('GroupMate Not Valide.')", true);
    }
    protected void btbClear_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void GridPrCpySub_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        lblSID.Text=GridPrCpySub.SelectedRow.Cells[1].Text.ToString();
        SqlDataReader reader = null;
        txtgmate1.Text = lblSID.Text.ToString();
        readid(lblSID.Text, reader);
        filldescriptn(); pnlComp.Visible = true;
        con.Close(); con.Dispose();
    }
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("GroupId");
        dtDatas.Columns.Add("GroupMate1");
        dtDatas.Columns.Add("GroupMate2");
        dtDatas.Columns.Add("GroupMate3");
        ViewState["dtDatas"] = dtDatas;
    }
    private void gmate2()
    {
        cmd = new SqlCommand("select * from Project where SID='" + txtgmate2.Text + "' and EntryStatus='Running'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            tgmate22.Text = rd["GroupMate2"].ToString(); tgmate23.Text = rd["GroupMate3"].ToString();
        }
        rd.Close(); tgmate21.Text = txtgmate2.Text;
        if (tgmate22.Text == "" || tgmate23.Text == "") { tgmate22.Text = txtgmate1.Text; tgmate23.Text = txtgmate3.Text; }
        if (tgmate33.Text == "") tgmate33.Text = txtgmate1.Text;
    }
    private void gmate3()
    {
        cmd = new SqlCommand("select * from Project where SID='" + txtgmate3.Text + "' and EntryStatus='Running' ", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            tgmate32.Text = rd["GroupMate2"].ToString(); tgmate33.Text = rd["GroupMate3"].ToString();
        }
        rd.Close(); tgmate31.Text = txtgmate3.Text;
        if (tgmate32.Text == "" || tgmate33.Text == "") { tgmate32.Text = txtgmate1.Text; tgmate33.Text = txtgmate1.Text; }
        if (tgmate23.Text == "") tgmate23.Text = txtgmate3.Text;
        if (tgmate33.Text == "") tgmate33.Text = txtgmate1.Text;
    }
    private void gmate2check()
    {
        if (txtgmate2.Text != tgmate21.Text || txtgmate2.Text != tgmate32.Text || txtgmate2.Text == "")
        {
            errorgmate2.Text =errorgmate2.Text+ " GroupMate2 not valid";
        }
        else { errorgmate2.Text = ""; btnSave.Enabled = true; }
    }
    private void gmate1check()
    {
        if (txtgmate1.Text != tgmate22.Text || txtgmate1.Text != tgmate33.Text || txtgmate1.Text == "")
        {
            errorgmate2.Text =errorgmate2.Text+  " GroupMate1 not valid";
        }
        else { errorgmate2.Text = ""; btnSave.Enabled = true; }
    }
    private void gmate3check()
    {
        if (txtgmate3.Text != tgmate31.Text || txtgmate3.Text != tgmate23.Text || txtgmate3.Text=="")
        {
            errorgmate2.Text = errorgmate2.Text+ " GroupMate3 not valid"; 
        }
        else { errorgmate2.Text = ""; btnSave.Enabled = true; }
    }
    protected void txtgmate2_TextChanged(object sender, EventArgs e)
    {
        errorgmate2.Text = "";
        con.Open(); gmate2(); gmate1check(); gmate2check(); gmate3check(); con.Close(); txtgmate3.Focus();
    }
    protected void txtgmate3_TextChanged(object sender, EventArgs e)
    {
        errorgmate2.Text = "";
        con.Open(); gmate2(); gmate3(); gmate1check(); gmate2check(); gmate3check(); con.Close();
    }
    private bool checkgroupMate()
    {
        bool flag = true;
        if (txtgmate1.Text != tgmate22.Text || txtgmate1.Text != tgmate33.Text)
        {
           lblexception.Text = "Group Mate 1 Not Valide.";
            flag = false;
        }
        else if (txtgmate2.Text != tgmate21.Text || txtgmate2.Text != tgmate32.Text)
        {
            lblexception.Text = "Group Mate 2 Not Valide.";
            flag = false;
        }
        else if (txtgmate3.Text != tgmate23.Text || txtgmate3.Text != tgmate31.Text)
        {
            lblexception.Text = "Group Mate 3 Not Valide.";
            flag = false;
        }
        return flag;
    }
    protected void lbtnNewProjectNo_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        string session = lblSessionHiddend.Text.ToString().Substring(0, 1) + lblSessionHiddend.Text.ToString().Substring(5, 2);
        cmd = new SqlCommand("select max(ProjectNo) from Project where Session='" + lblSessionHiddend.Text.ToString() + "' and (ProjectNo like '%S%' or ProjectNo like '%W%')", con);
        string count = Convert.ToString(cmd.ExecuteScalar().ToString());
        string ncount = "";
        if (count == "" | count == null)
            ncount = session + "0001";
        else
        {
            count = (Convert.ToInt32(count.Substring(3, count.Length - 3)) + 1).ToString();
            count = count.PadLeft(4, '0');
            ncount = session + count;
        }
        con.Close(); con.Dispose();
        lblProjectNo.Text = ncount.ToString();
        lblProjectNo.Focus();
    }
}