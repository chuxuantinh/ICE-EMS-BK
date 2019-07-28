using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class project_UpdateProject : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
                    txtapprovaldate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum"; else ddlsession.SelectedValue = "Win";
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        okk(txtIMID.Text.ToString());
        txtDiaryNo.Focus();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        txtimcode.Text = "";
        lblIm.Visible = false;
        txtstuname.Text = "";
        lblStuName.Visible = false;
        txtstream.Text = "";
        lblCourses.Visible = false;
        txtcourse.Text ="";
        lblpart.Text = "";
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    private void okk(string strid)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + strid.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        if (chk != "")
        {
            int i = 0;
            if (chk == strid.ToString())
            {
                i += 1;
                btnOK.Enabled = true;
            }
            else
            {
                txtIMID.Text = "Invalid ID";
                lblExceptionOK.Text = "Please Insert Valid IM ID.";
            }
            if (i == 1)
            {
                lblExceptionOK.Text = "";
                cmd = new SqlCommand("select * from IM where ID='" + strid + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    lblIMName.Text = reader[1].ToString();
                    lblIMAddress.Text = reader[3].ToString();
                    lblAdd.Visible = true;
                    lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                    lblEnrolment.Text = strid.ToString();
                    Label3.Text = reader["GID"].ToString();
                    lblGrp.Visible = true;
                }
                reader.Close();
                txtDiaryNo.Text = ""; txtDOB.Text = "";
            }
        }
        else
        {
            cmd = new SqlCommand("select MembershipNo from DiaryEntry where IMID='" + strid.ToString() + "'", con);
            string chk1 = Convert.ToString(cmd.ExecuteScalar());
            int i = 0;
            if (chk1 == strid.ToString())
            {
                i += 1;
                btnOK.Enabled = true;
            }
            else
            {
                txtIMID.Text = "Invalid ID";
                lblExceptionOK.Text = "Please Insert Valid IM ID.";
            }
            if (i == 1)
            {
                lblExceptionOK.Text = "";
                cmd = new SqlCommand("select * from Student where SID='" + strid + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    lblIMName.Text = reader["Name"].ToString();
                    //lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                    lblEnrolment.Text = strid.ToString();
                }
                reader.Close();
                txtDiaryNo.Text = ""; txtDOB.Text = "";
            }
        }
        con.Close();
        con.Dispose();
    }
    protected void txtDiaryNo_TextChaged(object sender, EventArgs e)
    {
        if (pnlMember.Visible == true)
        {
            pnlSpace.Visible = false;
        }
        con.Close(); con.Open();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        SqlCommand cmd8 = new SqlCommand("select IMID from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "'", con);
        string damount = Convert.ToString(cmd8.ExecuteScalar());
        if (damount.ToString() == "")
        {
            lblExceptionOK.Text = "Invalid Diary No";
            lblExceptionOK.ForeColor = System.Drawing.Color.Red;
            lblExceptionOK.Font.Bold = true;
            txtDiaryNo.Focus();
        }
        else
        {
            if (txtIMID.Text == damount.ToString())
            {
                lblExceptionOK.Text = damount.ToString();
                SqlCommand cmd = new SqlCommand("select Date from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "'", con);
                string dat = Convert.ToString(cmd.ExecuteScalar());
                txtDOB.Text = Convert.ToDateTime(dat.ToString()).ToString("dd/MM/yyyy");
                lblRcv.Visible = true;
                pnlMember.Visible = true;
                txtmemshipno.Focus();
            }
            else
            {
                lblExceptionOK.Text = "Invalid Diary No. for " + lblIMName.Text.ToString();
                lblExceptionOK.ForeColor = System.Drawing.Color.Red;
                lblExceptionOK.Font.Bold = true;
                txtDiaryNo.Focus();
            }
        }
        con.Close();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID,Name,Address,City,State,Pincode,Contact,Mobile,Email from InstitutionReg where Session='" + lblSessionHiddend.Text.ToString() + "'AND Name='" + ddlInstitute.SelectedItem.Text.ToString() + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            lblid.Text = dr["ID"].ToString();
            lblname.Text = dr["Name"].ToString();
            lbladdress.Text = dr["Address"].ToString();
            lblcity.Text = dr["City"].ToString();
            lblstate.Text = dr["State"].ToString();
            lblpincode.Text = dr["Pincode"].ToString();
            lblcontact.Text = dr["Contact"].ToString();
            lblmobile.Text = dr["Mobile"].ToString();
            lblemail.Text = dr["Email"].ToString();
        }
        else
        {
            lblid.Text = ""; lblname.Text = ""; lbladdress.Text = ""; lblcity.Text = ""; lblstate.Text = ""; lblpincode.Text = "";
            lblcontact.Text = ""; lblmobile.Text = "";
            lblemail.Text = "";
        } dr.Close();
        con.Close(); Panin.Visible = true; fillptitle(); 
        pnlProjTitle.Visible = true;
        pnlshow.Visible = false;
            Txttitle.Focus();
    }
    public string gidd()
    {
        con.Close();
        con.Open();
        SqlCommand cmdsn = new SqlCommand("select Max(GroupID) from Project where Session='" + lblSessionHiddend.Text.ToString() + "'", con);
        int i = 0;
        string id = Convert.ToString(cmdsn.ExecuteScalar().ToString());
        if (id == "")
        {
            i = 1;
        }
        else
        {
            i = Convert.ToInt32(id.ToString());
            i = i + 1;
        }
        if (i <= 9)
        {
            id = "000" + i;
        }
        else if (i <= 99)
        {
            id = "00" + i;
        }
        else if (i <= 999)
        {
            id = "0" + i;
        }
        else
        {
            id = Convert.ToString(i + 1);
        }
        id = id.ToString();
        return id;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlCommand cmd;
        con.Close();
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd/MM/yyyy";
        dtfi.DateSeparator = "/";
        con.Open();
        if (txtapprovaldate.Text == " ")
        {
            lblexeption.Text = "Insert Date";
        }
        if (txtgmate1.Text != "")
        {
            SqlCommand cmd2 = new SqlCommand("update AppRecord set Status='Filled' where AppNo='" + lblAppNo.Text + "' ", con);
            cmd2.ExecuteNonQuery();
            cmd = new SqlCommand("insert into Project(IMID,SID,GroupID,StudentName,Stream,Course,Part,InstitutionID,Institution,GroupMate1,GroupMate2,GroupMate3,SynopsisTitle,ProjectTitle,ApprovalFee,EvaluationFee,SynopsisDate,DiaryNo,Session,Status,Duration,Remarks)values(@IMID,@SID,@GroupID,@StudentName,@Stream,@Course,@Part,@InstitutionID,@Institution,@GroupMate1,@GroupMate2,@GroupMate3,@SynopsisTitle,@ProjectTitle,@ApprovalFee,@EvaluationFee,@SynopsisDate,@DiaryNo,@Session,@Status,@Duration,@Remarks)", con);
            SqlCommand cmd1 = new SqlCommand("insert into ProjectName(ProjectID,ProjectTitle,Stream,Section,Description) values(@ProjectID,@ProjectTitle,@Stream,@Section,@Description)", con);
            cmd.Parameters.AddWithValue("@IMID", txtimcode.Text.ToString());
            cmd.Parameters.AddWithValue("@SID", lblMem.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupID", lblGroupID.Text.ToString());
            cmd.Parameters.AddWithValue("@StudentName", txtstuname.Text.ToString());
            cmd.Parameters.AddWithValue("@Stream", txtstream.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", txtcourse.Text.ToString());
            cmd.Parameters.AddWithValue("@Part", lblpart.Text.ToString());
            cmd.Parameters.AddWithValue("@InstitutionID", lblid.Text.ToString());
            cmd.Parameters.AddWithValue("@Institution", ddlInstitute.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate1", txtgmate2.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate2", txtgmate3.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate3", txtgmate1.Text.ToString());
            cmd.Parameters.AddWithValue("@SynopsisTitle", Txttitle.Text.ToString());
            cmd.Parameters.AddWithValue("@ProjectTitle", Txttitle.Text.ToString());
            cmd.Parameters.AddWithValue("@ApprovalFee", "");
            cmd.Parameters.AddWithValue("@EvaluationFee", "");
            cmd.Parameters.AddWithValue("@SynopsisDate", Convert.ToDateTime(txtapprovaldate.Text, dtfi));
            cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text);
            cmd.Parameters.AddWithValue("@Status", "Approved");
            cmd.Parameters.AddWithValue("@Duration", lblDuration.Text.ToString());
            cmd.Parameters.AddWithValue("@Remarks", lblRemarks.Text.ToString());
            cmd.ExecuteNonQuery();
            cmd1.Parameters.AddWithValue("@ProjectID", "");
            cmd1.Parameters.AddWithValue("@ProjectTitle", Txttitle.Text.ToString());
            cmd1.Parameters.AddWithValue("@Stream", txtstream.Text.ToString());
            cmd1.Parameters.AddWithValue("@Section", txtcourse.Text.ToString());
            cmd1.Parameters.AddWithValue("@Description", TxtDescription.Text.ToString());
            cmd1.ExecuteNonQuery();
        }
        if (txtgmate2.Text != "")
        {
            if (txtgmate3.Text != "")
            {
                cmd = new SqlCommand("update Project set GroupMate3='"+txtgmate1.Text+"' where SID='"+txtgmate2.Text+"' and ProjectTitle='"+Txttitle.Text+"' ", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Project set GroupMate3='" + txtgmate1.Text + "' where SID='" + txtgmate3.Text + "'  and ProjectTitle='" + Txttitle.Text + "'", con);
                cmd.ExecuteNonQuery();
            }
            else if (txtgmate3.Text == "")
            {
                cmd = new SqlCommand("update Project set GroupMate2='" + txtgmate1.Text + "' where SID='" + txtgmate2.Text + "'  and ProjectTitle='" + Txttitle.Text + "' ", con);
                cmd.ExecuteNonQuery();
            }
        }
        con.Close();
        lblexeption.Text = "Information will be Saved";
        btnSave.Enabled = false;
    }
    public void imcod()
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select IMID from Student where SID='" + txtmemshipno.Text.ToString() + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtimcode.Text = dr["IMID"].ToString();
        }
        con.Close();
    }
    
    protected void btnmember_Click(object sender, EventArgs e)
    {  
        SqlCommand cmd;
        con.Open();
        string Prid = txtmemshipno.Text + "Approval";
        cmd = new SqlCommand("select * from AppRecord where FormType='"+Prid+"' and Status='no' ", con);
        SqlDataReader read = cmd.ExecuteReader();
        if (read.Read())
        {
            lblAppNo.Text = read["AppNo"].ToString();
            lblmemship.Visible = true;
           lblMem.Text= read["Enrolment"].ToString();
            read.Close();

            cmd = new SqlCommand("select IMID,Name,Stream,Course,Part from Student where SID='" + lblMem.Text + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                txtimcode.Text = dr["IMID"].ToString();
                lblIm.Visible = true;
                txtstuname.Text = dr["Name"].ToString();
                lblStuName.Visible = true;
                txtstream.Text = dr["Stream"].ToString();
                lblCourses.Visible = true;
                txtcourse.Text = dr["Course"].ToString();
                lblpart.Text = dr["Part"].ToString();
                SelectInst();
            }
        }
        else lblEx.Text = "Invalid Serial No:";
        con.Close(); insname();
       
        ddlInstitute.Focus();
    }
    protected void ibtnGenDiary_ONClick(object sender, EventArgs e) // create GroupNo.
    {
       // txtGID.Text = gidd();
        //txtGID.Focus();
    }
    protected void ddlProjectTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldescriptn();
    }
    string instype;
    private void fillptitle()
    {
        if (txtstream.Text == "" & txtcourse.Text == "")
        {
            lblExcepitnProject.Text = "Please Select Student Membership ID";
            lblExcepitnProject.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
        }
    }
    private void filldescriptn()
    {
        if (txtstream.Text == "" & txtcourse.Text == "")
        {
            lblExcepitnProject.Text = "Please Select Student Membership ID";
            lblExcepitnProject.ForeColor = System.Drawing.Color.Red;
           // txtIMID.Focus();
        }
        else
        {
                con.Close(); con.Open();
                SqlCommand cmd1 = new SqlCommand("select Description from ProjectName where ProjectTitle='" + Txttitle.Text.ToString() + "'", con);
                lblExcepitnProject.Text = "";
                con.Close();
        }
        if (Txttitle.Text == "")
            {
                lblExcepitnProject.Text = "Please Insert Project Title and Description Details.";
                lblExcepitnProject.ForeColor = System.Drawing.Color.Red;
                Txttitle.Focus();
            }
    }
    private void insname()
      {
    con.Close(); con.Open();
    if (txtstream.Text == "Tech") { instype = "Diploma"; lblinstype.Text = instype; } else if (txtstream.Text == "Asso") { instype = "Degree"; lblinstype.Text = instype; }

      SqlCommand cmd = new SqlCommand("Select * from InstitutionReg where Type='"+lblinstype.Text.ToString()+"'", con);
      SqlDataReader reader;
      reader = cmd.ExecuteReader();
      ddlInstitute.DataSource = reader;
      ddlInstitute.DataValueField = "Name";
      ddlInstitute.DataTextField = "Name";
      ddlInstitute.DataBind();
      lblExcepitnProject.Text = "";
      reader.Close();
      con.Close();
      ddlInstitute.Focus();

}
    protected void lbtnNewProjectTitle_ONclick(object sender, EventArgs e)
    {

    }
    protected void btnSaveTitle_Onclick(object sender, EventArgs e)
    {
        if (txtstream.Text == "" & txtcourse.Text == "")
        {

        }
        else
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("insert into ProjectName(ProjectID,ProjectTitle,Stream,Section,Description) values(@ProjectID,@ProjectTitle,@Stream,@Section,@Description)", con);
            cmd.Parameters.AddWithValue("@ProjectID", "");
            cmd.Parameters.AddWithValue("@Stream", txtstream.Text.ToString());
            cmd.Parameters.AddWithValue("@Section", txtcourse.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
        } 
    }
    protected void btnClearTitle_Onclick(object sender, EventArgs e)
    {
        //txtDescriptionTitle.Text = ""; txtNewProject.Text = "";
        //txtNewProject.Focus();
    }
    protected void ibtnCloseTitle_Onclick(object sender, EventArgs e)
    {
       // panelProjectTitle.Visible = false;
       //ddlProjectTitle.Enabled = true;
    }
    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string myString = Txttitle.Text.ToString();
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
        SqlCommand cmd = new SqlCommand("select DISTINCT ProjectTitle,GroupID,GroupMate1,GroupMate2,GroupMate3 from Project where Status='Approved' and ProjectTitle like '%" + str1 + " %'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        Pnlgrid.Visible = true;
        GridView1.DataSource = reader;
        GridView1.DataBind();
        con.Close();
    }
    protected void txtgmate1_TextChanged(object sender, EventArgs e)
    {
        Projectcls p = new Projectcls();
        string s = p.CheckSID(txtgmate1.Text.ToString());
        if (s == "true")
        {
            lblexeption.Text = "Already Exists";
            btnSave.Enabled = false;
        }
        else if (s == "false")
        {
            btnSave.Enabled = true;
        }
    }
    protected void txtgmate2_TextChanged(object sender, EventArgs e)
    {
        Projectcls p = new Projectcls();
        string s = p.CheckSID(txtgmate2.Text.ToString());
        if (s == "true")
        {
            lblexeption.Text = "Already Exists";
            btnSave.Enabled = false;
           
        }
        else if (s == "false")
        {
            btnSave.Enabled = true;
        }
    }
    protected void txtgmate3_TextChanged(object sender, EventArgs e)
    {
        Projectcls p = new Projectcls();
        string s = p.CheckSID(txtgmate3.Text.ToString());
        if (s == "true")
        {
            lblexeption.Text = "Already Exists";
            btnSave.Enabled = false;
           
        }
        else if (s == "false")
        {
            btnSave.Enabled = true;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlshow.Visible = true;
        GridViewRow row;
        row = GridView1.SelectedRow;
        Txttitle.Text = row.Cells[1].Text;
        lblGroupID.Text = row.Cells[2].Text;
        txtgmate1.Text = lblMem.Text;
        txtgmate2.Text = row.Cells[3].Text;
        txtgmate3.Text = row.Cells[4].Text;
        SqlCommand cmd = new SqlCommand("select * from Project where Groupmate1='" + row.Cells[3].Text + "'", con);
        SqlDataReader reader;
        con.Open();
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblDuration.Text = reader["Duration"].ToString();
            lblRemarks.Text = reader["Remarks"].ToString();
        }
        reader.Close();
        con.Close();
    }
    private void SelectInst()
    {
        if (txtimcode.Text == "")
        {
            lblExceptionOK.Text = "";
        }
        else
        {
            pnlSelectInst.Visible = true;
        }
    }
   
}