using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Data.OleDb;


public partial class Exam_UploadMarks : System.Web.UI.Page
{
    SessionDuration sd;
    #region Private Member Variables
    private static string UPLOADFOLDER = "Result";
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    XmlDocument doc;
    #endregion

    #region Web Methods
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                showimg();
                try
                {
                    SqlDataReader reader;
                    con.Open();
                    lbtnUserName.Text = Convert.ToString(Request.QueryString["dev"]);
                    SqlCommand cmd = new SqlCommand("select * from Login where LogName='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])) + "' and Password='" + Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbtnUserName.Text = Convert.ToString(reader[1].ToString());
                        int lvl = Convert.ToInt32(reader[20].ToString());
                        if (lvl == 0)
                        {
                            lblWelcome.Text = "Administrator";
                        }
                        else if (lvl == 1)
                        {
                            lblWelcome.Text = "Admin";
                            panelUpdate.Visible = false;
                        }
                        else if (lvl == 2)
                        {
                            panelExamForm.Visible = false; panelSeatingArrangement.Visible = false; panelDegree.Visible = false; panelPaperSetter.Visible = false;
                            panelExamPaper.Visible = false; panelMarksFeed.Visible = false; PanelAdmitCard.Visible = false; PanelRollNo.Visible = false; panelExamSchedule.Visible = false;
                            panelCertificate.Visible = false; panelExamCenter.Visible = false; panelMarking.Visible = false; panelUFM.Visible = false; panelRechecking.Visible = false;
                            panelUpdate.Visible = false;
                            lblWelcome.Text = "User ID";
                            usermanage.Visible = false;
                            if (reader["ExamForm"].ToString() == "ExamForm") panelExamForm.Visible = true;
                            if (reader["Seating"].ToString() == "Seating") panelSeatingArrangement.Visible = true;
                            if (reader["Marksheet"].ToString() == "Marksheet") panelDegree.Visible = true;
                            if (reader["PaperSetter"].ToString() == "PaperSetter") panelPaperSetter.Visible = true;
                            if (reader["ExamPaper"].ToString() == "ExamPaper") panelExamPaper.Visible = true;
                            if (reader["MarksFeed"].ToString() == "MarksFeed") panelMarksFeed.Visible = true;
                            if (reader["AdmitCard"].ToString() == "AdmitCard") PanelAdmitCard.Visible = true;
                            if (reader["RollNO"].ToString() == "RollNO") PanelRollNo.Visible = true;
                            if (reader["ExamAdmin1"].ToString() == "ExamSchedule") panelExamSchedule.Visible = true;
                            if (reader["Certi"].ToString() == "Certi") panelCertificate.Visible = true;
                            if (reader["ExamCenter"].ToString() == "ECenter") panelExamCenter.Visible = true;
                            if (reader["Marking"].ToString() == "Marking") panelMarking.Visible = true;
                            if (reader["UFM"].ToString() == "UFM") panelUFM.Visible = true;
                            if (reader["Rechecking"].ToString() == "Rechecking") panelRechecking.Visible = true;
                        }
                    }
                    reader.Close();
                    reader.Dispose();

                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0)
                    {
                        ddlSession.SelectedValue = "Sum";
                    }
                    else { ddlSession.SelectedValue = "Win"; }
                    txtYear.Text = DateTime.Now.Year.ToString();
                    sd = new SessionDuration();
                    SeasonId.Text = sd.SessionToSessionID(ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString()).ToString();
                    ddlSession.Focus();
                    this.Session["UploadDetail"] = new UploadDetail { IsReady = false };
                    LoadUploadedFiles(ref gvNewFiles);
                    con.Close(); con.Dispose();

                }
                catch (SqlException ex)
                {
                    lblWelcome.Text = "Error";
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static object GetUploadStatus()
    {
        //Get the length of the file on disk and divide that by the length of the stream
        UploadDetail info = (UploadDetail)HttpContext.Current.Session["UploadDetail"];
        if (info != null && info.IsReady)
        {
            int soFar = info.UploadedLength;
            int total = info.ContentLength;
            int percentComplete = (int)Math.Ceiling((double)soFar / (double)total * 100);
            string message = "Uploading...";
            string fileName = string.Format("{0}", info.FileName);
            string downloadBytes = string.Format("{0} of {1} Bytes", soFar, total);
            return new
            {
                percentComplete = percentComplete,
                message = message,
                fileName = fileName,
                downloadBytes = downloadBytes
            };
        }
        //Not ready yet
        return null;
    }
    #endregion

    #region Web Callbacks
    protected void gvNewFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "eventMouseOver(this)");
            e.Row.Attributes.Add("onmouseout", "eventMouseOut(this)");
        }
    }
    protected void gvNewFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strFolder;
        string filePath;
        switch (e.CommandName)
        {
            case "deleteFile":
                DeleteFile(e.CommandArgument.ToString());
                LoadUploadedFiles(ref gvNewFiles);
                break;
            case "downloadFile":
                strFolder = "Uploads";
                filePath = Path.Combine(strFolder, e.CommandArgument.ToString());
                DownloadFile(filePath);
                break;
            case "uploadFile":
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int id = row.RowIndex;
                string var = ((LinkButton)row.FindControl("lbtnFiles")).Text.ToString();
                uploadFile(var);
                break;
        }
    }
    protected void hdRefereshGrid_ValueChanged(object sender, EventArgs e)
    {
        LoadUploadedFiles(ref gvNewFiles);
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        // Session.Remove("admin");
        Response.Redirect("../Login.aspx");
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void imgbtnCreate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            //imgbtnCreate.ImageUrl = "~/images/createtrans.png";
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=create&lvl=zero&typ=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=create&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnManage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/Exam/Default.aspx?name=" + Request.QueryString["dev"] + "&lnk=rpt&lvl=zero&typ=" + Request.QueryString["typ"]);
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=delete&lvl=zero&typ=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=delete&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void imgbtnRecover_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0 & (Request.QueryString["lnk"].ToString() != "null"))
                Response.Redirect("../Admin/AdminCreate.aspx?lnk=update&lvl=zerotyp=Admin");
            else if (lvl == 1 | (Request.QueryString["lnk"].ToString() == "null"))
                Response.Redirect("../User/Create.aspx?lnk=update&lvl=one&typ=" + Request.QueryString["typ"].ToString() + "");
            else
            {
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    public void showimg()
    {
        if (Request.QueryString["lnk"].ToString() == "create")
        {
            imgbtnCreate.ImageUrl = "~/images/createcolor.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "update")
        {
            imgbtnRecover.ImageUrl = "~/images/user_update.png";
        }
        else if (Request.QueryString["lnk"].ToString() == "delete")
        {
            imgbtnDelete.ImageUrl = "~/images/user_delete.png";
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void lbtnExamFrom_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamForm.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnExaminationDate_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamSchedule.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnRollNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("GenerateRollNo.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewRollNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("GenViewRollNo.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnChangeCenter_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ChangeCenter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAdmitCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCard.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAdmitCardGen_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCardGen.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAdmitCardAppli_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmitCardAppli.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnMarksFeed_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeedMarks.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnMarksUPload_Click(object sender, EventArgs e)
    {
        Response.Redirect("UploadMarks.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnRecheckingMarks_Click(object sender, EventArgs e)
    {
        Response.Redirect("RecheckingUpdate.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnpaperSetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamPaperSetter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnpaperUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamPaperUpload.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnCenterRegi_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamCenter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void lbtnCenterAdmin_Click(object sender, EventArgs e)
    {
        string str;

        if (DateTime.Now.Month <= 6)
            str = "Sum" + DateTime.Now.Year.ToString();
        else
            str = "Win" + DateTime.Now.Year.ToString();
        Response.Redirect("ExamSponsor.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=&session=" + str);
    }
    protected void lbtnSeating_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamSeating.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnProvisional_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProvisionalCerti.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewProvisional_Click(object sender, EventArgs e)
    {

    }
    protected void lbtnViewCenterAdminProfile_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ViewExamSponcer.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewExamCenter_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ViewExamCenter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewExamFrom_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ViewExamForm.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewExamSchedule_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewExamSchedule.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewMarksFeed_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewMarksDetails.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewpaperSetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewPaperSetter.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnviewSeating_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewSeating.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewRechecking_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("RecheckingView.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnSubmitRechecking_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("RecheckingFrom.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnUFMdeetails_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("UFMView.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnUFMManage_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("UFMUpdate.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnUFMSubmit_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ExamUFM.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnEditExamFrom_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EditExamForm.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnApproveMarksEntry_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveMarks.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    //protected void lbtnViewMarksheetDetails_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ViewMarksDetails.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    //}
    protected void lbtnApproveMarksheets_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveMarksheet.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnApproveFinalMarksheet_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("ApproveFinalMarksheet.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnViewMarkDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("MarksStatement.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnMarksAppli_Click(object sender, EventArgs e)
    {
        Response.Redirect("MarksStatementAppli.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnAddRooms_Onclick(object sender, EventArgs e)
    {
        Response.Redirect("AddRooms.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void lbtnOldPapers_Click(object sender, EventArgs e)
    {
        Response.Redirect("OldPapers.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex");
    }
    protected void btnDelete_click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("delete SExamMarks where ExamSeason='" + ddlSession.SelectedValue.ToString() + txtYear.Text.ToString() + "' and Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert( Successfully deleted. )", true);
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert( Opps !!! Error !!! )", true);
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    protected void btnPromoteStudent_click(object sender, EventArgs e)
    {
        con.Open();
        CivilArchPartIUpdate();
        CivilArchPartIPass();
        CivilArchSectionBUpdate();
        CivilArchSectionBPass();
        CivilPartIIUpdate();
        CivilPartIIPass();
        ArchPartIIUpdate();
        ArchPartIIPass();
        con.Close(); 
        //con.Close(); con.Open();
        //cmd = new SqlCommand("spPromoteResult");
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Course", SqlDbType.NVarChar).Value = ddlCourseDelete.SelectedValue.ToString();
        //cmd.Parameters.AddWithValue("@Part", SqlDbType.NVarChar).Value = ddlPartDelete.SelectedValue.ToString();
        //cmd.Parameters.AddWithValue("@Session", SqlDbType.NVarChar).Value = ddlSession.SelectedValue.ToString() + txtYear.Text.ToString();
        ////   SqlParameter pn = new SqlParameter("@SID");
        //cmd.Connection = con;
        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);
        //GridExamForms.DataSource = dt;
        //GridExamForms.DataBind();
        //for (int i = 0; i <= dt.Rows.Count;i++ )
        //{
        //}
        //con.Close();
    }
    #endregion
    #region Support Methods
    public void LoadUploadedFiles(ref GridView gv)
    {
        DataTable dtFiles = GetFilesInDirectory(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
        gv.DataSource = dtFiles;
        gv.DataBind();
        if (dtFiles != null && dtFiles.Rows.Count > 0)
        {
            double totalSize = Convert.ToDouble(dtFiles.Compute("SUM(Size)", ""));
            if (totalSize > 0) lblTotalSize.Text = CalculateFileSize(totalSize);
        }
    }
    public DataTable GetFilesInDirectory(string sourcePath)
    {
        System.Data.DataTable dtFiles = new System.Data.DataTable();
        if ((Directory.Exists(sourcePath)))
        {
            dtFiles.Columns.Add(new System.Data.DataColumn("Name"));
            dtFiles.Columns.Add(new System.Data.DataColumn("Size"));
            dtFiles.Columns["Size"].DataType = typeof(double);
            dtFiles.Columns.Add(new System.Data.DataColumn("ConvertedSize"));
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            foreach (FileInfo files in dir.GetFiles("*.*"))
            {
                System.Data.DataRow drFile = dtFiles.NewRow();
                drFile["Name"] = files.Name;
                drFile["Size"] = files.Length;
                drFile["ConvertedSize"] = CalculateFileSize(files.Length);
                dtFiles.Rows.Add(drFile);
            }
        }
        return dtFiles;
    }
    public void DownloadFile(string filePath)
    {
        if (File.Exists(Server.MapPath(filePath)))
        {
            string strFileName = Path.GetFileName(filePath).Replace(" ", "%20");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
            Response.Clear();
            Response.WriteFile(Server.MapPath(filePath));
            Response.End();
        }
    }

    public void uploadFile(string filePath)
    {
        string extension;
        string Path1;
        extension = System.IO.Path.GetExtension(filePath);
        if (extension == ".XLS" | extension == ".xls" | extension == ".xlsx" | extension == ".XLSX")
        {
            Path1 = (Server.MapPath("~/Exam/Result/" + filePath));
            try
            {
                lblMessage.Text = "";
                DataTable dtExcel = new DataTable();
                string SourceConstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Path1 + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'";
                OleDbConnection con1 = new OleDbConnection(SourceConstr);
                con1.Open();
                string query = "Select * from [Sheet1$]";
                OleDbCommand Olcmd = new OleDbCommand(query, con1);
                OleDbDataReader dr = Olcmd.ExecuteReader();
                con.Open();
                while (dr.Read())
                {
                    cmd = new SqlCommand("uploadResult", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SID", SqlDbType.NVarChar).Value = dr["SID"].ToString();
                    cmd.Parameters.AddWithValue("@SubID", SqlDbType.NVarChar).Value = dr["SubID"].ToString();

                    cmd.Parameters.AddWithValue("@GetMarks", SqlDbType.NVarChar).Value = dr["GetMarks"].ToString();
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = dr["Status"].ToString();
                    cmd.Parameters.AddWithValue("@ExamSeason", SqlDbType.NVarChar).Value = dr["ExamSeason"].ToString();

                    cmd.Parameters.AddWithValue("@Course", SqlDbType.NVarChar).Value = dr["Course"].ToString();
                    cmd.Parameters.AddWithValue("@Part", SqlDbType.NVarChar).Value = dr["Part"].ToString();
                    cmd.Parameters.AddWithValue("@RollNo", SqlDbType.NVarChar).Value = dr["RollNo"].ToString();
                    cmd.Parameters.AddWithValue("@Center", SqlDbType.NVarChar).Value = dr["Center"].ToString();
                    cmd.Parameters.AddWithValue("@SessionID", SqlDbType.Int).Value = dr["SessionID"].ToString();
                    cmd.Parameters.AddWithValue("@ExmpId", SqlDbType.Int).Value = dr["ExmpId"].ToString();
                    cmd.ExecuteNonQuery();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Updated Successfully')", true);
                dr.Close(); con1.Close(); con.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message + ", Wrong Format";
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert(File Format should in Excel format)", true);
        }
    }

    public void update()
    {
    }
    public string DeleteFile(string FileName)
    {
        string strMessage = "";
        try
        {
            string strPath = Path.Combine(UPLOADFOLDER, FileName);
            if (File.Exists(Server.MapPath(strPath)) == true)
            {
                File.Delete(Server.MapPath(strPath));
                strMessage = "File Deleted";
            }
            else
                strMessage = "File Not Found";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message;
        }
        return strMessage;
    }


    private void CivilUpdate()
    {


        if ((ddlCourseDelete.SelectedValue.ToString() == "Civil" || ddlCourseDelete.SelectedValue == "Architecture") && (ddlCourseDelete.SelectedValue.ToString() == "PartI" || ddlCourseDelete.SelectedValue == "SectionA"))
        {
            SqlCommand cmd = new SqlCommand("insert into Link(Sid,Seasion,Count,Avv) select sid, max(sessionid) ,count(Status),avg(cast GetMarks as decimal)from SExamMarks where Status='Pass' or Status='NotPass' and Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlCourseDelete.SelectedValue.ToString() + "' group by SID having count(Status)=6 and avg(cast(GetMarks as decimal))>=50 and max(sessionid)=10", con);
            con.Open();

            cmd.ExecuteNonQuery();

        }

    }



    //private void UCivilUpdate()
    //{


    //    if ((ddlCourseDelete.SelectedValue.ToString() == "Civil" || ddlCourseDelete.SelectedValue == "Architecture") && (ddlCourseDelete.SelectedValue.ToString() == "PartI" || ddlCourseDelete.SelectedValue == "SectionA"))
    //    {
    //        SqlCommand cmd = new SqlCommand("update SexamMarks set status='Pass' from 
    //        con.Open();

    //        cmd.ExecuteNonQuery();

    //    }

    //}


    public string CalculateFileSize(double FileInBytes)
    {
        string strSize = "00";
        if (FileInBytes < 1024)
            strSize = FileInBytes + " B";//Byte
        else if (FileInBytes > 1024 & FileInBytes < 1048576)
            strSize = Math.Round((FileInBytes / 1024), 2) + " KB";//Kilobyte
        else if (FileInBytes > 1048576 & FileInBytes < 107341824)
            strSize = Math.Round((FileInBytes / 1024) / 1024, 2) + " MB";//Megabyte
        else if (FileInBytes > 107341824 & FileInBytes < 1099511627776)
            strSize = Math.Round(((FileInBytes / 1024) / 1024) / 1024, 2) + " GB";//Gigabyte
        else
            strSize = Math.Round((((FileInBytes / 1024) / 1024) / 1024) / 1024, 2) + " TB";//Terabyte
        return strSize;

    }
    #endregion
    protected void gvNewFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        sd = new SessionDuration();
        SeasonId.Text = sd.SessionToSessionID(ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString()).ToString();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        sd = new SessionDuration();
        SeasonId.Text = sd.SessionToSessionID(ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString()).ToString();
        txtYear.Focus();
    }
    private void CivilArchPartIUpdate()
    {
        if ((ddlCourseDelete.SelectedValue.ToString() == "Civil" || ddlCourseDelete.SelectedValue == "Architecture") && (ddlPartDelete.SelectedValue.ToString() == "PartI" || ddlPartDelete.SelectedValue == "SectionA"))
        {
            SqlCommand cmd = new SqlCommand("delete from SFinalPass where Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and SessionId='" + SeasonId.Text + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and Status='Pass' or Status='NotPass'  group by SID,Course,Part having count(Status)=6 and max(sessionid)='" + SeasonId.Text + "' and avg(cast(GetMarks as decimal))>='50'", con);
            cmd.ExecuteNonQuery();
        }
    }
    private void CivilArchPartIPass()
    {
     if ((ddlCourseDelete.SelectedValue.ToString() == "Civil" || ddlCourseDelete.SelectedValue == "Architecture") && (ddlPartDelete.SelectedValue.ToString() == "PartI" || ddlPartDelete.SelectedValue == "SectionA"))
        {

            SqlCommand cmd = new SqlCommand("update SExamMarks set status='Pass' from SExamMarks,SFinalPass where SExamMarks.Status='NotPass' and SExamMarks.Sid=SFinalPass.Sid and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery();   
            cmd = new SqlCommand("update SExamMarks set status='Fail' from SExamMarks where SExamMarks.Status='NotPass'  and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Result Updated')", true);            
        }
    }
    private void CivilArchSectionBUpdate()
    {
        if ((ddlCourseDelete.SelectedValue.ToString() == "Civil" || ddlCourseDelete.SelectedValue == "Architecture") && (ddlPartDelete.SelectedValue == "SectionB"))
        {
            SqlCommand cmd = new SqlCommand("delete from SFinalPass where Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and SessionId='" + SeasonId.Text + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and Status='Pass' or Status='NotPass'  group by SID,Course,Part having count(Status)=10 and max(sessionid)='" + SeasonId.Text + "' and avg(cast(GetMarks as decimal))>='50'", con);
            cmd.ExecuteNonQuery();    
        }
    }
    private void CivilArchSectionBPass()
    {
        if ((ddlCourseDelete.SelectedValue.ToString() == "Civil" || ddlCourseDelete.SelectedValue == "Architecture") && (ddlPartDelete.SelectedValue == "SectionB"))
        {
            SqlCommand cmd = new SqlCommand("update SExamMarks set status='Pass' from SExamMarks,SFinalPass where SExamMarks.Status='NotPass' and SExamMarks.Sid=SFinalPass.Sid and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update SExamMarks set status='Fail' from SExamMarks where SExamMarks.Status='NotPass'  and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Result Updated')", true);
        }
    }
    private void CivilPartIIUpdate()
    {
        if ((ddlCourseDelete.SelectedValue.ToString() == "Civil") && (ddlPartDelete.SelectedValue == "PartII"))
        {
            SqlCommand cmd = new SqlCommand("delete from SFinalPass where Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and SessionId='" + SeasonId.Text + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and (Status='Pass' or Status='NotPass') and SubID!='TC 2.10' and SubID!='TC 2.11'  group by SID,Course,Part having count(Status)=9 and max(sessionid)='" + SeasonId.Text + "' and avg(cast(GetMarks as decimal))>='50'", con);
            cmd.ExecuteNonQuery();
        }
    }
    private void CivilPartIIPass()
    {
    if ((ddlCourseDelete.SelectedValue.ToString() == "Civil" ) && (ddlPartDelete.SelectedValue == "PartII"))
        {
            SqlCommand cmd = new SqlCommand("update SExamMarks set status='Pass' from SExamMarks,SFinalPass where SExamMarks.Status='NotPass' and SExamMarks.Sid=SFinalPass.Sid and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update SExamMarks set status='Fail' from SExamMarks where SExamMarks.Status='NotPass'  and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Result Updated')", true);     
        }
    }

    private void ArchPartIIUpdate()
    {
        if ((ddlCourseDelete.SelectedValue.ToString() == "Architecture") && (ddlPartDelete.SelectedValue == "PartII"))
        {
            SqlCommand cmd = new SqlCommand("delete from SFinalPass where Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and SessionId='" + SeasonId.Text + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into SFinalPass(Sid,SessionId,Course,Part,Average,PaperPass) select sid, max(sessionid) , Course,Part,avg(cast(getmarks as decimal)) ,count(Status) from SExamMarks where  Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and Part='" + ddlPartDelete.SelectedValue.ToString() + "' and (Status='Pass' or Status='NotPass')  and SubID!='TA 2.11' and SubID!='TA 2.12'  group by SID,Course,Part having count(Status)=10 and max(sessionid)='" + SeasonId.Text + "' and avg(cast(GetMarks as decimal))>='50'", con);
            cmd.ExecuteNonQuery();
        }
    }
    private void ArchPartIIPass()
    {
        if ((ddlCourseDelete.SelectedValue.ToString() == "Architecture") && (ddlPartDelete.SelectedValue == "PartII"))
        {
           SqlCommand cmd = new SqlCommand("update SExamMarks set status='Pass' from SExamMarks,SFinalPass where SExamMarks.Status='NotPass' and SExamMarks.Sid=SFinalPass.Sid and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
           cmd.ExecuteNonQuery(); 
           cmd = new SqlCommand("update SExamMarks set status='Fail' from SExamMarks where SExamMarks.Status='NotPass'  and SExamMarks.Course='" + ddlCourseDelete.SelectedValue.ToString() + "' and SExamMarks.Part='" + ddlPartDelete.SelectedValue.ToString() + "'", con);
           cmd.ExecuteNonQuery();
           this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Result Updated')", true);
        }
    }
    }