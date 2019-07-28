using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class Exam_ExamPaperUpload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    private string strsn;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                panelHidden.Visible = true;
                panelView.Visible = false;
                lblStreamCode.Text = "Tech";
                lblStreamCode.Text = "Tech";
                lblStreamName.Text = "Technician Engineering";
                txtYear.Text = DateTime.Now.Year.ToString();
                lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
               string qyry = "";
               if (ddlCourse.SelectedValue.ToString() == "Civil")
               {
                   qyry = "select distinct CourseID from CivilSubMaster";
               }
               else if (ddlCourse.SelectedValue.ToString() == "Architecture")
               {
                   qyry = "select distinct CourseID from ArchiSubMaster";
               }
               SqlDataAdapter ad = new SqlDataAdapter(qyry, con);
               DataSet ds = new DataSet();
               ad.Fill(ds);
               ddlSyllabus.DataSource = ds;
               ddlSyllabus.DataTextField = "CourseID";
               ddlSyllabus.DataValueField = "CourseID";
               ddlSyllabus.DataBind();
               FeeMaster fm = new FeeMaster();
               ddlSyllabus.SelectedValue = fm.currentCourse(ddlCourse.SelectedValue.ToString());
               showcourse();
            }
            // visisble.Visible = true; invisible.Visible = false;
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
    { try
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
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void txtSeason_TexhChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
    }
    protected void btnShowID_Click(object sender, EventArgs e)
    {
        shwo();
    }
    private void shwo()
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select * from PaperSetter where Code='" + txtID.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader[1].ToString() != txtID.Text.ToString())
            {
                lblException.Text = "Invalid ID No.";
            }
            else
            {
                lblCode.Text = reader[1].ToString();
                lblName.Text = reader[2].ToString();
                lblDesignation.Text = reader[3].ToString();
                lblEducatioN.Text = reader[18].ToString();
                lblExperience.Text = reader[19].ToString();
                //txtSubCode.Text = reader[20].ToString();
                txtSubName.Text = reader[21].ToString();
                if (reader[23].ToString() == "Civil") ddlCourse.SelectedValue = "Civil";
                else if (reader[23].ToString() == "Archi") ddlCourse.SelectedValue = "Archi";
                lbllnk10.Text = reader[24].ToString();
                panelHidden.Visible = false;
                panelView.Visible = true;
            }
        }
        con.Close();
        con.Dispose();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update PaperSetter set SCode=@SCode,SName=@SName,Session=@Season,Course=@Course where Code='" + lblCode.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@SCode", ddlsubcode.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SName", txtSubName.Text.ToString());
            cmd.Parameters.AddWithValue("@Season", lblSeason.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            lblSavedInfo.Text = "Paper Name and Code Saved";
        }
        catch (SqlException ex)
        {
            lblSavedInfo.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void cmdSendNew2_Click(object sender, EventArgs e)
    {
        docsupload(lblCode.Text.ToString());
    }
    public void docsupload(string sid)
    {
        string strFilename;
        string strFileEx;
        if (filMyFile.PostedFile != null)
        {
            HttpPostedFile myFile = filMyFile.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (nFileLen > 0)
            {
                byte[] myData = new byte[nFileLen];
                myFile.InputStream.Read(myData, 0, nFileLen);
                string newname = sid.ToString();
                string stronlyName = Path.GetFileNameWithoutExtension(myFile.FileName);
                strFilename = Path.GetFileName(newname + myFile.FileName);
                strFileEx = Path.GetExtension(myFile.FileName);
                if (strFileEx == ".pdf" | strFileEx == ".jpg")
                {
                    lblInfo.Text = strFilename.ToString();
                    WriteToFileNew(Server.MapPath("uploads/" + strFilename), ref myData);
                    WriteToDBNew(strFilename, sid);  //Store data at database server.
                }
                else
                {
                    lblInfo.Text = "Wrong format" + strFileEx.ToString() + " Only .pdf or .jpg required.";
                }
            }
        }
    }
    protected void ddlSyllabus_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            cmd = "select * from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            cmd = "select * from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        SqlCommand cmdd = new SqlCommand(cmd, con);
        SqlDataReader reader;
        reader = cmdd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(reader);
        ddlsubcode.DataSource = dt;
        ddlsubcode.DataValueField = "SubID";
        ddlsubcode.DataTextField = "SubID";
        ddlsubcode.DataBind();
       // showcourse();
        con.Close();
        con.Dispose();
    }
    string stcmss;
    protected void ddlSubCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            stcmss = "select * from CivilSubMaster where SubID='" + ddlsubcode.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            stcmss = "select * from ArchiSubMaster where SubID='" + ddlsubcode.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
        }
        SqlCommand cms = new SqlCommand(stcmss, con);

        SqlDataReader rd;
        rd = cms.ExecuteReader();
        while (rd.Read())
        {
          txtSubName.Text = rd["SubName"].ToString();
        }
        rd.Close();
        con.Close();
        con.Dispose();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
        {
            lblStreamCode.Text = "Tech";
            lblStreamName.Text = "Technician Engineering";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
        {
            lblStreamName.Text = "Associate Engineering";
            lblStreamCode.Text = "Asso";
        }
        showcourse();
    }
    protected void ddlCourse_SeelctedIndexchanged(object sender, EventArgs e)
    {
        string qyry = "";
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            qyry = "select distinct CourseID from CivilSubMaster";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            qyry = "select distinct CourseID from ArchiSubMaster";
        }
        SqlDataAdapter ad = new SqlDataAdapter(qyry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddlSyllabus.DataSource = ds;
        ddlSyllabus.DataTextField = "CourseID";
        ddlSyllabus.DataValueField = "CourseID";
        ddlSyllabus.DataBind();
        showcourse();
    }
    string cmd;
    private void showcourse()
    {
        try
        {
            if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
            {
                lblStreamCode.Text = "Tech";
                lblStreamName.Text = "Technician Engineering";
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
            {
                lblStreamName.Text = "Associate Engineering";
                lblStreamCode.Text = "Asso";
            }
            con.Close(); con.Open();

            if (ddlCourse.SelectedValue.ToString() == "Civil")
            {
                cmd = "select * from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
            }
            else if (ddlCourse.SelectedValue.ToString() == "Architecture")
            {
                cmd = "select * from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + ddlSyllabus.SelectedValue.ToString() + "'";
            }
            SqlDataAdapter ad = new SqlDataAdapter(cmd, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            ddlsubcode.DataSource = dt;
            ddlsubcode.DataValueField = "SubID";
            ddlsubcode.DataTextField = "SubID";
            ddlsubcode.DataBind();
        }
        catch (SqlException ex)
        {
        }
        finally
        {
        }
    }
    private void WriteToFileNew(string strPath, ref byte[] Buffer)
    {
        FileStream newFile = new FileStream(strPath, FileMode.Create);
        newFile.Write(Buffer, 0, Buffer.Length);
        newFile.Close();
    }
    private void WriteToDBNew(string id, string sid)
    {
        con.Close();
        con.Open();
        SqlCommand scmd = new SqlCommand("update PaperSetter set PDocs=@PDocs where Code='"+sid.ToString()+"'", con);
        scmd.Parameters.AddWithValue("@PDocs", id.ToString());
        scmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    protected void lbtn10View_Click(object sender, EventArgs e)
    {
        shwo();
        ViewDoc(lbllnk10.Text.ToString());
    }
    protected void lbtn10Download_Click(object sender, EventArgs e)
    {
        shwo();
        download(lbllnk10.Text.ToString());
    }
    public void ViewDoc(string str)
    {
        Response.Redirect("uploads/" + str);
    }
    public void download(string fname)
    {
        if (Page.IsPostBack)
        {
            if (fname == "")
            {
                lblDownlloadInfo.Visible = true;
                lblDownlloadInfo.ForeColor = System.Drawing.Color.Red;
                lblDownlloadInfo.Text = "Docs is not Available now. ";
            }
            else
            {
                Response.ContentType = "application/ms-word/pdf/jpg";
                Response.AddHeader("content-disposition", "attachment; filename=" + fname.ToString() + "");
                FileStream sourceFile = new FileStream(@Server.MapPath(@"uploads/" + fname.ToString()), FileMode.Open);
                long FileSize;
                FileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)FileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                Response.BinaryWrite(getContent);
                lblDownlloadInfo.Visible = true;
                lblDownlloadInfo.ForeColor = System.Drawing.Color.Green;
                lblDownlloadInfo.Text = fname.ToString() + "  downloaded. ";
            }
        }
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
    }
}
