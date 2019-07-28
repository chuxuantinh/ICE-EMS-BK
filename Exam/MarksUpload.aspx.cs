using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;
using System.Data;


public partial class Exam_MarksUpload : System.Web.UI.Page
 { 
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                {
                    ddlExamSeason.SelectedValue = "Sum";
                }
                else { ddlExamSeason.SelectedValue = "Win"; }
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                updatexml();
                this.Session["UploadDetail"] = new UploadDetail { IsReady = false };
                LoadUploadedFiles(ref gvNewFiles);
                ddlExamSeason.Focus();
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
        switch (e.CommandName)
        {
            case "deleteFile":
                DeleteFile(e.CommandArgument.ToString());
                LoadUploadedFiles(ref gvNewFiles);
                break;
            case "downloadFile":
                string strFolder = "Uploads";
                string filePath = Path.Combine(strFolder, e.CommandArgument.ToString());
                DownloadFile(filePath);
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        updatexml();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        updatexml();
        txtYearSeason.Focus();
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
    private void updatexml()
    {
        string flag = "1";
        doc = new XmlDocument();
        doc.Load(Server.MapPath("../xml/Marksheet.xml"));
        XmlNodeList node = doc.SelectNodes("/Session/dayta/Name");
        for (int i = node.Count - 1; i >= 0; i--)
        {
            string tt = node[i].InnerText;

            if (tt == lblExamSeasonHidden.Text)
            {
                flag = "0";
            }
        }
        if (flag == "1")
        {
            XmlNode nodeElement = doc.CreateNode(XmlNodeType.Element, "dayta", null);
            XmlNode nodetitle = doc.CreateElement("Name");
            nodetitle.InnerText = lblExamSeasonHidden.Text;
            XmlNode nodeCourse1 = doc.CreateElement("Section");
            XmlAttribute attr = doc.CreateAttribute("Course");
            attr.Value = "CPartI";
            nodeCourse1.Attributes.Append(attr);
            XmlAttribute attr1 = doc.CreateAttribute("Status");
            attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            XmlAttribute attr2 = doc.CreateAttribute("record");
            attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);

            nodeCourse1 = doc.CreateElement("Section");
            attr = doc.CreateAttribute("Course");
            attr.Value = "CPartII";
            nodeCourse1.Attributes.Append(attr);
            attr1 = doc.CreateAttribute("Status"); attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            attr2 = doc.CreateAttribute("record"); attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);

            nodeCourse1 = doc.CreateElement("Section");
            attr = doc.CreateAttribute("Course");
            attr.Value = "CSectionA";
            nodeCourse1.Attributes.Append(attr);
            attr1 = doc.CreateAttribute("Status"); attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            attr2 = doc.CreateAttribute("record"); attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);

            nodeCourse1 = doc.CreateElement("Section");
            attr = doc.CreateAttribute("Course");
            attr.Value = "CSectionB";
            nodeCourse1.Attributes.Append(attr);
            attr1 = doc.CreateAttribute("Status"); attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            attr2 = doc.CreateAttribute("record"); attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);

            nodeCourse1 = doc.CreateElement("Section");
            attr = doc.CreateAttribute("Course");
            attr.Value = "APartI";
            nodeCourse1.Attributes.Append(attr);
            attr1 = doc.CreateAttribute("Status"); attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            attr2 = doc.CreateAttribute("record"); attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);

            nodeCourse1 = doc.CreateElement("Section");
            attr = doc.CreateAttribute("Course");
            attr.Value = "APartII";
            nodeCourse1.Attributes.Append(attr);
            attr1 = doc.CreateAttribute("Status"); attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            attr2 = doc.CreateAttribute("record"); attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);

            nodeCourse1 = doc.CreateElement("Section");
            attr = doc.CreateAttribute("Course");
            attr.Value = "ASectionA";
            nodeCourse1.Attributes.Append(attr);
            attr1 = doc.CreateAttribute("Status"); attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            attr2 = doc.CreateAttribute("record"); attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);

            nodeCourse1 = doc.CreateElement("Section");
            attr = doc.CreateAttribute("Course");
            attr.Value = "ASectionB";
            nodeCourse1.Attributes.Append(attr);
            attr1 = doc.CreateAttribute("Status"); attr1.Value = "Not Upload";
            nodeCourse1.Attributes.Append(attr1);
            attr2 = doc.CreateAttribute("record"); attr2.Value = "";
            nodeCourse1.Attributes.Append(attr2);
            nodeElement.AppendChild(nodetitle);
            nodeElement.AppendChild(nodeCourse1);
            doc.DocumentElement.AppendChild(nodeElement);
            doc.Save(Server.MapPath("../xml/Marksheet.xml"));
        }
    }
 
    #endregion
}

  ////  public string getExtension1(string FileName)  //call funtion for Doc
  //  {
  //      string sMime1 = "application/octet-stream";
  //      string sExtension = System.IO.Path.GetExtension(FileName);
  //      if (!string.IsNullOrEmpty(sExtension))
  //      {
  //          sExtension = sExtension.Replace(".", "");
  //          sExtension = sExtension.ToLower();
  //          if (sExtension == "xls" || sExtension == "xlsx")
  //              {
  //                  sMime1 = "True";
  //              }
  //      }
  //       return sMime1;
  //  }
  //  private void UploadDocuments()
  //  {
  //      if (fileUpload1.HasFile) //start first if
  //      {
  //         if (fileUpload1.PostedFile.ContentLength < 10240 * 1024) //start second if sizae 20kb
  //              {
  //                  string filename = System.IO.Path.GetFileName(fileUpload1.PostedFile.FileName);
  //                  string extension = System.IO.Path.GetExtension(filename);
  //                  string successflag = getExtension1(filename);
  //                  ViewState["Extension"] = extension;
  //                  if (successflag == "True")
  //                  {
  //                      string strfile = ddlCourse.SelectedValue + ddlPart.SelectedValue;
  //                          ViewState["filename"] = strfile + extension;
  //                          UploadDocumentsType();
  //                  }
  //                  else
  //                  {
  //                      lblError.Text = "File Format incorrect !";
  //                  }
  //              } //second if end
  //              else
  //              {
  //                  lblError.Text = "Documents size Exceed Should less then 20KB";
  //              }
  //          }
  //          else
  //          {
  //              lblError.Text = "Already Exist";
  //          }//firs if end
  //  }