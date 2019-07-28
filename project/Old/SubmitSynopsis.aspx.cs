using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class project_SubmitSynopsis : System.Web.UI.Page
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
                    if (pnlComp.Visible == false)
                    {
                        pnlSpace.Visible = true;
                    }
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlsession.SelectedValue = "Sum"; else ddlsession.SelectedValue = "Win";
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    ddlsession.Focus();
                    txtapprovaldate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
        if (txtIMID.Text == "")
        {
            lblExceptionOK.Text = "Please insert IMID";
            txtIMID.Focus();
        }
        else
        {
            okk(txtIMID.Text.ToString());
            txtDiaryNo.Focus();
        }
    }
    private void okk(string strid)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + strid.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
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
            while (reader.Read())
            {
                lblExcepitnProject.Text = "";
                lblIMName.Text = reader[1].ToString();
                lblIMAddress.Text = reader[3].ToString();
                lblAdd.Visible = true;
                lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                lblEnrolment.Text = strid.ToString();
                lblGroupID.Text = reader["GID"].ToString();
                lblGp.Visible = true;
            }
            reader.Close();
            txtDiaryNo.Text = ""; txtDate.Text = "";
        }
        con.Close();
        con.Dispose();
    }
    protected void txtDiaryNo_TextChaged(object sender, EventArgs e)
        {
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
                lblExceptionOK.Visible = false;
                SqlCommand cmd = new SqlCommand("select Date from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblSessionHiddend.Text.ToString() + "'", con);
                string dat = Convert.ToString(cmd.ExecuteScalar());
                txtDate.Text = Convert.ToDateTime(dat.ToString()).ToString("dd/MM/yyyy");
                lblRcv.Visible = true;
                txtmemshipno.Focus();
                pnlComp.Visible = true;
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
        con.Dispose();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        txtmemshipno.Focus();
        txtmemshipno.Text = ""; txtimcode.Visible = false;
        lblIMId.Visible = false;
        txtstuname.Text = "";
        lblStuName.Visible =false;
        txtstream.Text = "";
        txtcourse.Text = "";
        lblCourses.Visible = false;
        lblpart.Text = "";
        txtgmate1.Text = ""; Panin.Visible = false;
        pnlSelectInst.Visible = false; Txttitle.Text = ""; TxtDescription.Text = ""; txtsynopsisremark.Text = ""; txtGID.Text = ""; txtgmate2.Text = ""; txtgmate3.Text = "";
        
        //string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        //Response.Redirect(url.ToString());
        //Response.Redirect("IMBuilding.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + Request.QueryString["id"].ToString());
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (Panin.Visible == true)
        {
            pnlSpace.Visible = false;
        }
        else
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
        con.Close(); con.Dispose(); Panin.Visible = true;
        Txttitle.Focus();
    }
    public string gidd()
    {
        con.Close();
        con.Open();
        SqlCommand cmdsn = new SqlCommand("select Max(GroupID) from Project where Session='" + lblSessionHiddend.Text.ToString() + "'", con);
        int i = 0;
        string id = Convert.ToString(cmdsn.ExecuteScalar().ToString());
        con.Close();
        con.Dispose();
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
        con.Close();
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd/MM/yyyy";
        dtfi.DateSeparator = "/";
        con.Open();
        if (txtgmate1.Text != "")
        {
            SqlCommand cmd = new SqlCommand("insert into Project(IMID,SID,GroupID,StudentName,Stream,Course,Part,InstitutionID,Institution,GroupMate1,GroupMate2,GroupMate3,SynopsisTitle,ApprovalFee,EvaluationFee,SynopsisDate,DiaryNo,Session,Status,Duration,Remarks)values(@IMID,@SID,@GroupID,@StudentName,@Stream,@Course,@Part,@InstitutionID,@Institution,@GroupMate1,@GroupMate2,@GroupMate3,@SynopsisTitle,@ApprovalFee,@EvaluationFee,@SynopsisDate,@DiaryNo,@Session,@Status,@Duration,@Remarks)", con);
            SqlCommand cmd1 = new SqlCommand("insert into ProjectName(ProjectID,ProjectTitle,Stream,Section,Description) values(@ProjectID,@ProjectTitle,@Stream,@Section,@Description)", con);
            cmd.Parameters.AddWithValue("@IMID", txtimcode.Text.ToString());
            cmd.Parameters.AddWithValue("@SID", txtmemshipno.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupID", txtGID.Text.ToString());
            cmd.Parameters.AddWithValue("@StudentName", txtstuname.Text.ToString());
            cmd.Parameters.AddWithValue("@Stream", txtstream.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", txtcourse.Text.ToString());
            cmd.Parameters.AddWithValue("@Part", lblpart.Text.ToString());
            cmd.Parameters.AddWithValue("@InstitutionID", lblid.Text.ToString());
            cmd.Parameters.AddWithValue("@Institution", ddlInstitute.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate1", txtgmate1.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate2", txtgmate2.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate3", txtgmate3.Text.ToString());
            cmd.Parameters.AddWithValue("@SynopsisTitle", Txttitle.Text.ToString());
            cmd.Parameters.AddWithValue("@ApprovalFee", "");
            cmd.Parameters.AddWithValue("@EvaluationFee", "");
            cmd.Parameters.AddWithValue("@SynopsisDate", Convert.ToDateTime(txtapprovaldate.Text.ToString(), dtfi));
            cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text);
            cmd.Parameters.AddWithValue("@Status", "NotApproved");
            cmd.Parameters.AddWithValue("@Duration", txtDurtaion.Text.ToString() + " Months");
            cmd.Parameters.AddWithValue("@Remarks", txtsynopsisremark.Text.ToString());
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
            SqlCommand cmd = new SqlCommand("select IMID,Name,Stream,Course,Part from Student where SID='" + txtgmate3.Text.ToString() + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                txtimcode.Text = dr["IMID"].ToString();
                txtstuname.Text = dr["Name"].ToString();
                txtstream.Text = dr["Stream"].ToString();
                txtcourse.Text = dr["Course"].ToString();
                lblpart.Text = dr["Part"].ToString();
                txtgmate1.Text = txtmemshipno.Text.ToString();
            }
            dr.Close();
            cmd = new SqlCommand("insert into Project(IMID,SID,GroupID,StudentName,Stream,Course,Part,InstitutionID,Institution,GroupMate1,GroupMate2,GroupMate3,SynopsisTitle,ApprovalFee,EvaluationFee,SynopsisDate,DiaryNo,Session,Status,Duration,Remarks)values(@IMID,@SID,@GroupID,@StudentName,@Stream,@Course,@Part,@InstitutionID,@Institution,@GroupMate1,@GroupMate2,@GroupMate3,@SynopsisTitle,@ApprovalFee,@EvaluationFee,@SynopsisDate,@DiaryNo,@Session,@Status,@Duration,@Remarks)", con);
            cmd.Parameters.AddWithValue("@IMID", txtimcode.Text.ToString());
            cmd.Parameters.AddWithValue("@SID", txtgmate2.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupID", txtGID.Text.ToString());
            cmd.Parameters.AddWithValue("@StudentName", txtstuname.Text.ToString());
            cmd.Parameters.AddWithValue("@Stream", txtstream.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", txtcourse.Text.ToString());
            cmd.Parameters.AddWithValue("@Part", lblpart.Text.ToString());
            cmd.Parameters.AddWithValue("@InstitutionID", lblid.Text.ToString());
            cmd.Parameters.AddWithValue("@Institution", ddlInstitute.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate1", txtgmate1.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate2", txtgmate2.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate3", txtgmate3.Text.ToString());
            cmd.Parameters.AddWithValue("@SynopsisTitle", Txttitle.Text.ToString());
            cmd.Parameters.AddWithValue("@ApprovalFee", "");
            cmd.Parameters.AddWithValue("@EvaluationFee", "");
            cmd.Parameters.AddWithValue("@SynopsisDate", Convert.ToDateTime(txtapprovaldate.Text.ToString(), dtfi));
            cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text);
            cmd.Parameters.AddWithValue("@Status", "NotApproved");
            cmd.Parameters.AddWithValue("@Duration", txtDurtaion.Text.ToString() + " Months");
            cmd.Parameters.AddWithValue("@Remarks", txtsynopsisremark.Text.ToString());
            cmd.ExecuteNonQuery();
        }
        if (txtgmate3.Text != "")
        {
            SqlCommand cmd = new SqlCommand("select IMID,Name,Stream,Course,Part from Student where SID='" + txtgmate2.Text.ToString() + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                txtimcode.Text = dr["IMID"].ToString();
                txtstuname.Text = dr["Name"].ToString();
                txtstream.Text = dr["Stream"].ToString();
                txtcourse.Text = dr["Course"].ToString();
                lblpart.Text = dr["Part"].ToString();
                txtgmate1.Text = txtmemshipno.Text.ToString();
            }
            dr.Close();
            cmd = new SqlCommand("insert into Project(IMID,SID,GroupID,StudentName,Stream,Course,Part,InstitutionID,Institution,GroupMate1,GroupMate2,GroupMate3,SynopsisTitle,ApprovalFee,EvaluationFee,SynopsisDate,DiaryNo,Session,Status,Duration,Remarks)values(@IMID,@SID,@GroupID,@StudentName,@Stream,@Course,@Part,@InstitutionID,@Institution,@GroupMate1,@GroupMate2,@GroupMate3,@SynopsisTitle,@ApprovalFee,@EvaluationFee,@SynopsisDate,@DiaryNo,@Session,@Status,@Duration,@Remarks)", con);
            cmd.Parameters.AddWithValue("@IMID", txtimcode.Text.ToString());
            cmd.Parameters.AddWithValue("@SID", txtgmate3.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupID", txtGID.Text.ToString());
            cmd.Parameters.AddWithValue("@StudentName", txtstuname.Text.ToString());
            cmd.Parameters.AddWithValue("@Stream", txtstream.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", txtcourse.Text.ToString());
            cmd.Parameters.AddWithValue("@Part", lblpart.Text.ToString());
            cmd.Parameters.AddWithValue("@InstitutionID", lblid.Text.ToString());
            cmd.Parameters.AddWithValue("@Institution", ddlInstitute.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate1", txtgmate1.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate2", txtgmate2.Text.ToString());
            cmd.Parameters.AddWithValue("@GroupMate3", txtgmate3.Text.ToString());
            cmd.Parameters.AddWithValue("@SynopsisTitle", Txttitle.Text.ToString());
            cmd.Parameters.AddWithValue("@ApprovalFee", "");
            cmd.Parameters.AddWithValue("@EvaluationFee", "");
            cmd.Parameters.AddWithValue("@SynopsisDate", Convert.ToDateTime(txtapprovaldate.Text.ToString(), dtfi));
            cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text);
            cmd.Parameters.AddWithValue("@Status", "NotApproved");
            cmd.Parameters.AddWithValue("@Duration", txtDurtaion.Text.ToString() + " Months");
            cmd.Parameters.AddWithValue("@Remarks", txtsynopsisremark.Text.ToString());
            cmd.ExecuteNonQuery();
        }
        con.Close();
        con.Dispose();
        lblexeption.Text = "Information Saved";
        txtmemshipno.Text = "";
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
        dr.Close();
        con.Close();
        con.Dispose();
    }
    protected void btnmember_Click(object sender, EventArgs e)
            {
        if (txtmemshipno.Text == "")
        {
            lblExcepitnProject.Text = "Please Insert Membership No";
            txtmemshipno.Focus();
        }
        else
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("select IMID,Name,Stream,Course,Part from Student where SID='" + txtmemshipno.Text + "' and IMID='" + txtIMID.Text.ToString() + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                txtimcode.Text = dr["IMID"].ToString();
                lblIMId.Visible = true;
                txtstuname.Text = dr["Name"].ToString();
                lblStuName.Visible = true;
                txtstream.Text = dr["Stream"].ToString();
                txtcourse.Text = dr["Course"].ToString();
                lblCourses.Visible = true;
                lblpart.Text = dr["Part"].ToString();
                txtgmate1.Text = txtmemshipno.Text.ToString();
                pnlSelectInst.Visible = true;
            }
            dr.Close();
            insname();
            fillptitle();
            con.Close();
            con.Dispose();
            ddlInstitute.Focus();
        }
    }
    protected void ibtnGenDiary_ONClick(object sender, EventArgs e) // create GroupNo.
    {
        txtGID.Text = gidd();
        txtGID.Focus();
    }
    protected void ddlProjectTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldescriptn();
    }
    string instype;
    private void fillptitle()
    {
        if (txtIMID.Text == "" && txtmemshipno.Text == "")
        {
            lblExceptionOK.Text = "";
            lblExcepitnProject.Text = "Please Enter IMID";
        }
        if (txtIMID.Text == "" && txtmemshipno.Text != "")
        {
            lblExcepitnProject.Text = "Please Enter IMID";
        }
        if (txtIMID.Text != "" && txtmemshipno.Text == "")
        {
            lblExcepitnProject.Text = "Please Enter Membership ID";
            txtimcode.Visible = false;
            lblIMId.Visible = false;
            txtstuname.Visible=false;
            txtstream.Visible=false;
            txtcourse.Visible=false;
            lblpart.Visible=false;
            txtgmate1.Visible = false;
        }
        if (txtstream.Text == "" && txtcourse.Text == "" && txtIMID.Text !="")
        {
            lblExcepitnProject.Text = "Please Enter Valid Membership ID";
            lblExcepitnProject.ForeColor = System.Drawing.Color.Red;
            txtIMID.Focus();
        }
        else
        {
            //con.Close(); con.Open();
            //SqlCommand cmd = new SqlCommand("select * from ProjectName", con);
            //SqlDataReader reader;
            //reader = cmd.ExecuteReader();
            //ddlProjectTitle.DataSource = reader;
            //ddlProjectTitle.DataValueField = "ProjectTitle";
            //ddlProjectTitle.DataTextField = "ProjectTitle";
            //ddlProjectTitle.DataBind();
            //lblExcepitnProject.Text = "";
            //reader.Close();
            //con.Close();
        }
    }
    private void filldescriptn()
    {
        if (txtstream.Text == "" & txtcourse.Text == "")
        {
            lblExcepitnProject.ForeColor = System.Drawing.Color.Red;
            txtIMID.Focus();
        }
        else
        {
            if (Txttitle.Text == "")
            {
                lblExcepitnProject.Text = "Please Insert Project Title and Description Details.";
                lblExcepitnProject.ForeColor = System.Drawing.Color.Red;
                Txttitle.Focus();
            }
            else
            {
                con.Close(); con.Open();
                SqlCommand cmd1 = new SqlCommand("select Description from ProjectName where ProjectTitle='" + Txttitle.Text.ToString() + "'", con);
                lblExcepitnProject.Text = "";
                con.Close();
                con.Dispose();
                txtDurtaion.Focus();
            }
        }
    }
    private void insname()
    {
        con.Close(); con.Open();
        if (txtstream.Text == "Tech") { instype = "Diploma"; lblinstype.Text = instype; } else if (txtstream.Text == "Asso") { instype = "Degree"; lblinstype.Text = instype; }
        SqlCommand cmd = new SqlCommand("Select * from InstitutionReg where Type='" + lblinstype.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        ddlInstitute.DataSource = reader;
        ddlInstitute.DataValueField = "Name";
        ddlInstitute.DataTextField = "Name";
        ddlInstitute.DataBind();
        lblExcepitnProject.Text = "";
        reader.Close();
        con.Close();
        con.Dispose();
        ddlInstitute.Focus();
    }
    protected void lbtnNewProjectTitle_ONclick(object sender, EventArgs e)
    {
       // panelProjectTitle.Visible = true;
       // ddlProjectTitle.Enabled = false;
    }
    protected void btnSaveTitle_Onclick(object sender, EventArgs e)
    {
        if (txtstream.Text == "" & txtcourse.Text == "")
        {
            //lblexceptionTitle.Text = "Please select Student Membership No.";
            //lblexceptionTitle.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            //if (txtNewProject.Text == "" & txtDescriptionTitle.Text == "")
            //{
            //   // lblexceptionTitle.Text = "Please Submit Project Title Details.";
            //   // lblexceptionTitle.ForeColor = System.Drawing.Color.Red;
            //}
           // else
            {
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProjectName(ProjectID,ProjectTitle,Stream,Section,Description) values(@ProjectID,@ProjectTitle,@Stream,@Section,@Description)", con);
                cmd.Parameters.AddWithValue("@ProjectID", "");
                cmd.Parameters.AddWithValue("@Stream", txtstream.Text.ToString());
                cmd.Parameters.AddWithValue("@Section", txtcourse.Text.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
            }
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
        //ddlProjectTitle.Focus();
    }
    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
       
        string myString =Txttitle.Text.ToString();
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
             for (int i = 0; i <str.Length; i++)
             {
                     str1 = str[i] +"%'or ProjectTitle like '"+str1; 
                 }                
             }
             SqlCommand cmd = new SqlCommand("select ProjectTitle,Description from ProjectName where ProjectTitle like '%"+ str1 +" %'", con);
             SqlDataReader reader;
             reader = cmd.ExecuteReader();
             Pnlgrid.Visible = true;
             GridView1.DataSource = reader;
             GridView1.DataBind();
             con.Close(); con.Dispose();
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
    protected void Txttitle_TextChanged(object sender, EventArgs e)
    {
        chkTitle();
        TxtDescription.Focus();
    }
    private void chkTitle()
    {
        if (lblid.Text == "" && Txttitle.Text == "")
        {
            lblTitle.Text = "";
        }
        if (lblid.Text != "" && Txttitle.Text == "")
        {
            lblTitle.Text = "Please Enter Project Title !";
        }
        else
        {
            lblTitle.Text = "";
        }

    }
    protected void TxtDescription_TextChanged(object sender, EventArgs e)
    {
        LinkButton1.Focus();
    }
    protected void txtIMID_TextChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Focus();
    }
}


