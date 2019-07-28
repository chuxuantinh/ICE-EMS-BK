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

public partial class project_SubmitProfB : System.Web.UI.Page
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
                    pnlmain.Enabled = false;
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum"; else ddlsession.SelectedValue = "Win";
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    datastructure(); BindSynStatus();
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
    protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        btnView.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        btnView.Focus();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        bindgrid(); BindNewGridTitle(); clear();
    }
    protected void GridShow_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridShow.SelectedRow;
        txtgmate1.Text = row.Cells[1].Text; txtSID.Text = row.Cells[1].Text; BindNewGridTitle();
        viewsid(txtgmate1.Text.ToString());
    }
    private void viewsid(string sid)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select StudentName,Course,Part,SynopsisStatus,SynopsisTitle,ApprovalFees,SynopsisDate,ProjectAppDate,InstitutionID,Institution,DiaryB,ProjectNo,Session,SynopsisRemarks,Remark,GroupID,GroupMate2,GroupMate3,LetterRemarks,ProjectTitle,LetterIssueDate from Project where  SID='" + sid.ToString() + "' and EntryStatus='Running'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblName.Text = rd["StudentName"].ToString();
            lblCourse.Text = rd["Course"].ToString();
            lblpart.Text = rd["Part"].ToString(); lblSynopsisTtl.Text = rd["SynopsisTitle"].ToString();
            lblProformaBFees.Text = rd["ApprovalFees"].ToString().TrimEnd('0').TrimEnd('.');
            lblSynopsisStatus.Text = rd["SynopsisStatus"].ToString();
            if (rd["LetterIssueDate"].ToString() == "" | rd["LetterIssueDate"].ToString() == null) { txtLetterIssueDate.Text = ""; }
            else
            txtLetterIssueDate.Text =Convert.ToDateTime( rd["LetterIssueDate"].ToString()).ToString("dd/MM/yyyy");
            try
            {
                ddlSynopsisStatus.SelectedValue = lblSynopsisStatus.Text.ToString();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ddlSynopsisStatus.Items.Add(lblSynopsisStatus.Text);
                ddlSynopsisStatus.SelectedValue = lblSynopsisStatus.Text;
            }
            if (rd["ProjectAppDate"].ToString() == "" | rd["ProjectAppDate"].ToString() == null) { txtProAppDate.Text = ""; }
            else
                txtProAppDate.Text = Convert.ToDateTime(rd["ProjectAppDate"]).ToString("dd/MM/yyyy");
            if (rd["synopsisDate"].ToString() == "" | rd["synopsisDate"].ToString() == null) { txtDate.Text = ""; }
            else
                txtDate.Text = Convert.ToDateTime(rd["SynopsisDate"]).ToString("dd/MM/yyyy");
            lblInstID.Text = rd["InstitutionID"].ToString(); lblINstName.Text = rd["Institution"].ToString();
            txtDNo.Text = rd["DiaryB"].ToString();
            txtProjectNO.Text = rd["ProjectNo"].ToString();
            txtSynopsisRemarks.Text = rd["SynopsisRemarks"].ToString();
            txtRemarks.Text = rd["Remark"].ToString();
            lblSessionHiddend.Text = rd["Session"].ToString();
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblGID.Text = rd["GroupID"].ToString();
            txtgmate2.Text = rd["GroupMate2"].ToString();
            txtgmate3.Text = rd["GroupMate3"].ToString();
            txtLetterRemarsk.Text = rd["LetterRemarks"].ToString();
            lblprojecttitle.Text = rd["ProjectTitle"].ToString();
            txtNewSynDate.Text = txtDate.Text;
            pnlmain.Enabled = true;
            lblException.Text = "";
            txtDNo.Focus();
        }
        else { lblException.Text = "Project Record Not Found."; txtSID.Focus(); }
        rd.Close(); rd.Dispose(); gmate2(); gmate3(); gmate1check(); gmate2check(); gmate3check();
        if (lblGID.Text.ToString() == "" | lblGID.Text == "0") { lblGID.Text = gengid().ToString(); }
        con.Close(); BindSynStatus(); con.Dispose();
    }
    private void bindgrid()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select SID,IMID,Session,SynopsisStatus from Project where Status='" + ddlProfBStatus.SelectedValue.ToString() + "' and EntryStatus='Running' and Session='" + lblSessionHiddend.Text.ToString() + "'", con);
        DataTable dt = new DataTable(); adp.Fill(dt); GridShow.DataSource = dt; GridShow.DataBind(); clear();
    }
    protected void btnSIDView_Click(object sender, EventArgs e)
    {
        txtgmate1.Text = txtSID.Text.ToString(); errorgmate2.Text = ""; tgmate21.Text = ""; tgmate22.Text = ""; tgmate23.Text = ""; tgmate31.Text = ""; tgmate32.Text = ""; tgmate33.Text = "";
        BindNewGridTitle(); viewsid(txtSID.Text.ToString());
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (checkgroupMate() == true)
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            SqlTransaction trans;
            trans = con.BeginTransaction("RangeTransaction");
            cmd = con.CreateCommand();
            cmd.Transaction = trans;
            try
            {
                cmd.CommandText = "update Project set SynopsisDate=@SynopsisDate,ProjectAppDate=@ProjectAppDate,Status=@Status,SynopsisStatus=@SynopsisStatus,SynopsisTitle=@SynopsisTitle,Description=@Description,GroupMate1=@GroupMate1,GroupMate2=@GroupMate2,GroupMate3=@GroupMate3,DiaryB=@DiaryB,GroupID=@GroupID,ProjectTitle=@ProjectTitle,LetterRemarks=@LetterRemarks,SynopsisRemarks=@SynopsisRemarks,Remark=@Remark,ProjectNo=@ProjectNo,LetterIssueDate=@LetterIssueDate where SID='" + txtgmate1.Text + "' and EntryStatus='Running'";
                if (ddlSynopsisStatus.SelectedValue == "Approved")
                {
                    if (txtProAppDate.Text == "") cmd.Parameters.AddWithValue("ProjectAppDate", DBNull.Value); else cmd.Parameters.AddWithValue("ProjectAppDate", Convert.ToDateTime(txtProAppDate.Text, dtinfo));
                    if (txtDate.Text == "") cmd.Parameters.AddWithValue("SynopsisDate", DBNull.Value); else cmd.Parameters.AddWithValue("SynopsisDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@Status", "ProformaBApproved");
                }
                else
                {
                    if (txtDate.Text == "") cmd.Parameters.AddWithValue("SynopsisDate", DBNull.Value); else cmd.Parameters.AddWithValue("SynopsisDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    if (txtProAppDate.Text == "") cmd.Parameters.AddWithValue("ProjectAppDate", DBNull.Value); else cmd.Parameters.AddWithValue("ProjectAppDate", Convert.ToDateTime(txtProAppDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@Status", "ProformaBSubmitted");
                }
                cmd.Parameters.AddWithValue("@LetterIssueDate", Convert.ToDateTime(txtLetterIssueDate.Text, dtinfo));
                cmd.Parameters.AddWithValue("@SynopsisStatus", ddlSynopsisStatus.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SynopsisTitle", lblSynopsisTtl.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.ToString());
                cmd.Parameters.AddWithValue("@GroupMate1", txtgmate1.Text.ToString());
                cmd.Parameters.AddWithValue("@GroupMate2", txtgmate2.Text.ToString());
                cmd.Parameters.AddWithValue("@GroupMate3", txtgmate3.Text.ToString());
                cmd.Parameters.AddWithValue("@DiaryB", txtDNo.Text.ToString());
                cmd.Parameters.AddWithValue("@GroupID", lblGID.Text.ToString());
                cmd.Parameters.AddWithValue("@ProjectTitle", lblprojecttitle.Text.ToString());
                cmd.Parameters.AddWithValue("@LetterRemarks", txtLetterRemarsk.Text.ToString());
                cmd.Parameters.AddWithValue("@SynopsisRemarks", txtSynopsisRemarks.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text.ToString());
                cmd.Parameters.AddWithValue("@ProjectNo", txtProjectNO.Text.ToString());
                cmd.ExecuteNonQuery();
                if (tgmate21.Text != "")
                {
                    cmd.CommandText = "update Project set GroupID='" + lblGID.Text + "', GroupMate1='" + tgmate21.Text + "',GroupMate2='" + tgmate22.Text + "',GroupMate3='" + tgmate23.Text + "',SynopsisTitle=@SynopsisTitle2,Description= @Description2 where SID='" + tgmate21.Text + "' and EntryStatus='Running'";
                    cmd.Parameters.AddWithValue("@SynopsisTitle2", lblSynopsisTtl.Text);
                    cmd.Parameters.AddWithValue("@Description2", txtDescription.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
                if (tgmate31.Text != "")
                {
                    cmd.CommandText = "update Project set GroupID='" + lblGID.Text + "', GroupMate1='" + tgmate31.Text + "',GroupMate2='" + tgmate32.Text + "',GroupMate3='" + tgmate33.Text + "',SynopsisTitle=@SynopsisTitle3,Description=@Description3 where SID='" + tgmate31.Text + "' and EntryStatus='Running'";
                    cmd.Parameters.AddWithValue("@SynopsisTitle3", lblSynopsisTtl.Text);
                    cmd.Parameters.AddWithValue("@Description3", txtDescription.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
                trans.Commit(); bindgrid(); con.Close(); con.Dispose(); clear(); pnlmain.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Submitted')", true);
            }
            catch (SqlException ex) { trans.Rollback(); }
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('GroupMate Not Valide')", true);

    }
    private string chkgid(string id, string session)
    {
        string gid = "";
        cmd = new SqlCommand("select GroupID from Project where SId='" + id + "' and EntryStatus='Running'", con);
        gid = Convert.ToString(cmd.ExecuteScalar());
        return gid;
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
    private int gengid()
    {
        cmd = new SqlCommand("select max(GroupID)+1 from Project", con);
        int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        if (count == null)
            count = 1;
        return count;
    }
    //Select groupmates
    private void gmate2()
    {
        cmd = new SqlCommand("select * from Project where SID='" + txtgmate2.Text + "' and EntryStatus='Running'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            tgmate22.Text = rd["GroupMate2"].ToString(); tgmate23.Text = rd["GroupMate3"].ToString();
        }
        rd.Close(); tgmate21.Text = txtgmate2.Text;
        //tgmate32.Text = txtgmate2.Text;
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
            errorgmate2.Text = errorgmate2.Text + " GroupMate2 not valid";
        }
        else { errorgmate2.Text = ""; btnsave.Enabled = true; }
    }
    private bool checkgroupMate()
    {
        bool flag = true;
        if (txtgmate1.Text != tgmate22.Text || txtgmate1.Text != tgmate33.Text)
        {
            lblException.Text = "Group Mate 1 Not Valide.";
            flag = false;
        }
        else if (txtgmate2.Text != tgmate21.Text || txtgmate2.Text != tgmate32.Text)
        {
            lblException.Text = "Group Mate 2 Not Valide.";
            flag = false;
        }
        else if(txtgmate3.Text!=tgmate23.Text|| txtgmate3.Text!=tgmate31.Text)
        {
            lblException.Text = "Group Mate 3 Not Valide.";
            flag = false;
        }
        return flag;
    }
    private void gmate1check()
    {
        if (txtgmate1.Text != tgmate22.Text || txtgmate1.Text != tgmate33.Text || txtgmate1.Text == "")
        {
            errorgmate2.Text = errorgmate2.Text + " GroupMate1 not valid";
        }
        else { errorgmate2.Text = ""; btnsave.Enabled = true; }
    }
    private void gmate3check()
    {
        if (txtgmate3.Text != tgmate31.Text || txtgmate3.Text != tgmate23.Text || txtgmate3.Text == "")
        {
            errorgmate2.Text = errorgmate2.Text + " GroupMate3 not valid";
        }
        else { errorgmate2.Text = ""; btnsave.Enabled = true; }
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
    private void clear()
    {
        txtDNo.Text = ""; lblGID.Text = ""; txtgmate1.Text = ""; txtgmate2.Text = ""; errorgmate2.Text = ""; txtgmate3.Text = ""; lblSynopsisTtl.Text = ""; txtDescription.Text = ""; txtDate.Text = ""; tgmate21.Text = ""; tgmate22.Text = ""; tgmate23.Text = ""; tgmate31.Text = ""; tgmate32.Text = ""; tgmate33.Text = "";
    }
    protected void lnKCheckButton_Click(object sender, EventArgs e)
    {
        if (lblprojecttitle.Text == "") { lblprojecttitle.Focus(); }
        else
        {
            SqlDataAdapter adp = new SqlDataAdapter("select ProjectTitle,SID,Session,GroupMate1,Groupmate2,Groupmate3 from Project where FREETEXT(ProjectTitle,'" + lblprojecttitle.Text + "')", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            grdChecktitle.DataSource = dt;
            grdChecktitle.DataBind();
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblprojecttitle.Text = lblSynopsisTtl.Text.ToString();
    }
    protected void ddlSynopsisStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSynStatus();
    }
    private void BindSynStatus()
    {
        if (ddlSynopsisStatus.SelectedValue == "Approved") { lblSyn.Text = "Approve Date"; txtDate.Visible = false; cal1.Visible = false; txtProAppDate.Visible = true; cal2.Visible = true; }
        else { lblSyn.Text = "Synopsis Date"; txtDate.Visible = true; cal1.Visible = true; txtProAppDate.Visible = false; cal2.Visible = false; }
    }
    private void BindNewGridTitle()
    {
        adp = new SqlDataAdapter("select ProjectSynopsis.SID,ProjectSynopsis.ProjectNo,ProjectSynopsis.SynopsisTitle from Project,ProjectSynopsis where ProjectSynopsis.SID='" + txtgmate1.Text + "' and Project.Status='" + ddlProfBStatus.SelectedValue.ToString() + "' and Project.EntryStatus='Running' and Project.Session='" + lblSessionHiddend.Text.ToString() + "' and Project.SID=ProjectSynopsis.SID order by ProjectSynopsis.SN Desc", con);
        DataTable dt = new DataTable(); adp.Fill(dt); GridNewTitle.DataSource = dt; GridNewTitle.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Close(); con.Open();
        SqlTransaction trans;
        trans = con.BeginTransaction("RangeTrans");
        cmd = con.CreateCommand();
        cmd.Transaction = trans;
        try
        {
            cmd.CommandText = "update Project set SynopsisDate=@SynopsisDate,SynopsisTitle=@SynopsisTitle,Description=@Description where SID='" + txtgmate1.Text + "' and EntryStatus='Running'";
            if (txtNewSynDate.Text == "") cmd.Parameters.AddWithValue("@SynopsisDate", DBNull.Value); else cmd.Parameters.AddWithValue("@SynopsisDate", Convert.ToDateTime(txtNewSynDate.Text, dtinfo));
            cmd.Parameters.AddWithValue("@SynopsisTitle", txtNewTitle.Text.ToString());
            cmd.Parameters.AddWithValue("@Description", txtNewDescrip.Text.ToString());
            cmd.ExecuteNonQuery();
            cmd.CommandText = "insert into ProjectSynopsis(SID,ProjectNo,SynopsisTitle,Date) values(@SID,@ProjectNo,@SynopsisTittle,@Date)";
            cmd.Parameters.AddWithValue("@SID", txtgmate1.Text);
            cmd.Parameters.AddWithValue("@ProjectNo", txtProjectNO.Text.ToString());
            cmd.Parameters.AddWithValue("@SynopsisTittle", txtNewTitle.Text.ToString());
            if (txtNewSynDate.Text == "") cmd.Parameters.AddWithValue("@Date", DBNull.Value); else cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtNewSynDate.Text, dtinfo));
            cmd.ExecuteNonQuery();
            trans.Commit(); BindNewGridTitle(); con.Close(); con.Dispose();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('New Project Title Added Successfully !')", true); txtNewTitle.Text = ""; txtNewDescrip.Text = "";
        }
        catch (SqlException ex) { trans.Rollback(); }
    }
    protected void lbtnNewProjectNo_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        string session = lblSessionHiddend.Text.ToString().Substring(0, 1) + lblSessionHiddend.Text.ToString().Substring(5, 2);
        cmd = new SqlCommand("select max(ProjectNo) from Project where Session='" + lblSessionHiddend.Text.ToString() + "' and (ProjectNo like '%S%' or ProjectNo like '%W%') ", con);
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
        txtProjectNO.Text = ncount.ToString();
        txtProjectNO.Focus();
    }
}