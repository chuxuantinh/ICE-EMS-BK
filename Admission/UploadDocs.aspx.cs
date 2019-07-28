using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class User_UploadDocs : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] == null | Session["sid"]=="")
        {
            Response.Redirect("AdmissionDepart.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
        }
            else {
                lblEnrolment.Text = Session["sid"].ToString();
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Docs where SID='" + Convert.ToString(Session["sid"].ToString()) + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblImgStatus.Text = reader[0].ToString();
                lblDocsStatus.Text = reader[21].ToString();
            }
            reader.Close();
            con.Close();
            if (lblImgStatus.Text == "")
            {
                lblImgTitle.Text = "Upload Profile Picture.";
                imgDefault.Visible = true;
            }
           else if (lblImgStatus.Text == "P")
            {
                lblImgTitle.Text = "Upload Signature.";
                imgDefault.Visible = false;
            }
          else if (lblImgStatus.Text == "PS")
            {
                lblImgTitle.Text = "Change Profile Image";
                imgDefault.Visible = false;
            }
        }
        if (!IsPostBack)
        {
            doc1.Visible = false; doc2.Visible = false; doc3.Visible = false; doc4.Visible = false; doc5.Visible = false; doc10.Visible = false; doc12.Visible = false;
            panelshow();
            docsStatus();
            ViewImg();
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
    protected void ibtnImgPnlShow_Click(object sender, EventArgs e)
    {
        panelImage.Visible = true;
        panelDocs.Visible = false;
        imgdocbar.Visible = false;
        imgimgbar.Visible = true;
        ViewImg();
    }
    protected void ibtnDocsPnlShow_Click(object sender, EventArgs e)
    {
        panelDocs.Visible = true;
        panelImage.Visible = false;
        imgimgbar.Visible = false;
        imgdocbar.Visible = true;
    }
    public void panelshow()
    {
        if (Request.QueryString["pnl"] == "img")
        {
            panelImage.Visible = true;
            panelDocs.Visible = false;
            imgimgbar.Visible = true;
            imgdocbar.Visible = false;
        }
        else if (Request.QueryString["pnl"] == "docs")
        {
            panelDocs.Visible = true;
            panelImage.Visible = false;
            imgimgbar.Visible = false;
            imgdocbar.Visible = true;
        }
    }
     protected void cmdSendNew2_Click(object sender, EventArgs e)
    {
        docsupload(Session["sid"].ToString());
    }
     public void docsupload(string sid)
     {
         string strFilename;
         string strFileEx;
         // Check to see if file was uploaded
         if (filMyFile.PostedFile != null)
         {
             // Get a reference to PostedFile object
             HttpPostedFile myFile = filMyFile.PostedFile;

             // Get size of uploaded file
             int nFileLen = myFile.ContentLength;

             // make sure the size of the file is > 0
             if (nFileLen > 0)
             {
                 // Allocate a buffer for reading of the file
                 byte[] myData = new byte[nFileLen];

                 // Read uploaded file from the Stream
                 myFile.InputStream.Read(myData, 0, nFileLen);
                 // HttpPostedFile myfilewrite = filMyFile.PostedFile;
                 //  myFile.InputStream.Write(myData, 0, nFileLen);

                 // Create a name for the file to store
                 string newname = sid + txtDocsNameOther.Text.Trim().ToString();
                 string stronlyName = Path.GetFileNameWithoutExtension(myFile.FileName);

                 strFilename = Path.GetFileName(newname + myFile.FileName);
                 strFileEx = Path.GetExtension(myFile.FileName);
                 if (strFileEx == ".pdf" | strFileEx == ".jpg")
                 {
                     lblInfo.Text = strFilename.ToString();
                     // Write data into a file
                     WriteToFileNew(Server.MapPath("uploads/" + strFilename), ref myData);
                 WriteToDBNew(txtDocsNameOther.Text.ToString(),strFilename,sid);  //Store data at database server.
                 docsStatus();
                 }
                 else
                 {
                     lblInfo.Text = "Wrong format" + strFileEx.ToString() + " Only .pdf or .jpg required.";
                 }
             }
         }
     }
    private void WriteToFileNew(string strPath, ref byte[] Buffer)
    {
        FileStream newFile = new FileStream(strPath, FileMode.Create);
        newFile.Write(Buffer, 0, Buffer.Length);
        newFile.Close();
    }
    private void WriteToDBNew(string name, string id, string sid)
    {
        string cmd = getcolum(name, id, sid);
        con.Close();
        con.Open();
        SqlCommand scmd = new SqlCommand(cmd, con);
        scmd.Parameters.AddWithValue("@ID", id.ToString());
        scmd.Parameters.AddWithValue("@Name", name.ToString());
        scmd.ExecuteNonQuery();
        con.Close();
    }
    private string getcolum(string name, string id, string sid)
    {
        maikal mk = new maikal();
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select DocsStatus from Docs where SID='" + Session["sid"].ToString() + "'", con);
        maikal.stri = Convert.ToInt32(cmd.ExecuteScalar());
        if (rbtn10.Checked == true)
        {
            string strcmd1 = "update Docs set D10ID=@ID,D10Name=@Name where SID='" + sid.ToString() + "'"; maikal.strUserName = strcmd1.ToString();
            maikal.stri = 2;
            updatestatus(sid, maikal.stri);
            return maikal.strUserName;
        }
        else if (rbtn12.Checked == true)
        {
            maikal.strUserName = "update Docs set D12ID=@ID,D12Name=@Name where SID='" + sid.ToString() + "'"; maikal.stri = 2;
            maikal.stri = 2;
            updatestatus(sid, maikal.stri);
            return maikal.strUserName;
        }
        else if (rbtnother.Checked == true)
        {
            if (maikal.stri == 2) { maikal.stri = 3; maikal.strUserName = "update Docs set D1ID=@ID,D1Name=@Name where SID='" + sid.ToString() + "'"; }
            else if (maikal.stri == 3) { maikal.stri = 4; maikal.strUserName = "update Docs set D2ID=@ID,D2Name=@Name where SID='" + sid.ToString() + "'"; }
            else if (maikal.stri == 4) { maikal.stri = 5; maikal.strUserName = "update Docs set D3ID=@ID,D3Name=@Name where SID='" + sid.ToString() + "'"; }
            else if (maikal.stri == 5) { maikal.stri = 6; maikal.strUserName = "update Docs set D4ID=@ID,D4Name=@Name where SID='" + sid.ToString() + "'"; }
            else if (maikal.stri == 6) { maikal.stri = 7; maikal.strUserName = "update Docs set D5ID=@ID,D5Name=@Name where SID='" + sid.ToString() + "'"; }
            else if (maikal.stri == 7) { maikal.stri = 7; }
            updatestatus(sid, maikal.stri);
            return maikal.strUserName;
        }
        return maikal.strUserName;
    }
    private void updatestatus(string status,int value)
    {
        con.Close();
        con.Open();
        SqlCommand cmd1 = new SqlCommand("update Docs set DocsStatus=@DocsStatus where SID='" + status.ToString() + "'", con);
        cmd1.Parameters.AddWithValue("@DocsStatus", value);
        cmd1.ExecuteNonQuery();
        con.Close();
    }
    private void docsStatus()
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Docs where SID='" + Convert.ToString(Session["sid"].ToString()) + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lbldoc1Name.Text = reader[12].ToString();
            lbldoc2Name.Text = reader[14].ToString();
            lbldoc3Name.Text = reader[16].ToString();
            lbldoc4Name.Text = reader[18].ToString();
            lbldoc5Name.Text = reader[20].ToString();
            lbllnk10.Text = reader[7].ToString(); lbllnk12.Text = reader[9].ToString(); lbllnk1.Text = reader[11].ToString(); lbllnk2.Text = reader[13].ToString();
            lbllnk3.Text = reader[15].ToString(); lbllnk4.Text = reader[17].ToString(); lbllnk5.Text = reader[19].ToString();
            lblImgStatus.Text = reader[0].ToString();
            lblDocsStatus.Text = reader[21].ToString();
        }
        reader.Close();
        con.Close();
        if (lblDocsStatus.Text == "1")
        { doc10.Visible = true; }
        else if (lblDocsStatus.Text == "2")
        {
            doc12.Visible = true; doc10.Visible = true;
        }
        else if (lblDocsStatus.Text == "3") { doc1.Visible = true; doc12.Visible = true; doc10.Visible = true; }
        else if (lblDocsStatus.Text == "4") { doc2.Visible = true; doc1.Visible = true; doc12.Visible = true; doc10.Visible = true; }
        else if (lblDocsStatus.Text == "5") { doc3.Visible = true; doc2.Visible = true; doc1.Visible = true; doc12.Visible = true; doc10.Visible = true; }
        else if (lblDocsStatus.Text == "6") { doc4.Visible = true; doc3.Visible = true; doc2.Visible = true; doc1.Visible = true; doc12.Visible = true; doc10.Visible = true; }
        else if (lblDocsStatus.Text == "7") { doc5.Visible = true; doc4.Visible = true; doc3.Visible = true; doc2.Visible = true; doc1.Visible = true; doc12.Visible = true; doc10.Visible = true; }
        else  { }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //Condition to check if the file uploaded or not
        if (fileuploadImage.HasFile)
        {
            //getting length of uploaded file
            int length = fileuploadImage.PostedFile.ContentLength;
            //create a byte array to store the binary image data
            byte[] imgbyte = new byte[length];
            //store the currently selected file in memeory
            HttpPostedFile img = fileuploadImage.PostedFile;
            //set the binary data
            img.InputStream.Read(imgbyte, 0, length);
            string imagename = fileuploadImage.PostedFile.FileName;
            string strex = Path.GetExtension(imagename);
            if (strex == ".jpg" | strex == ".gif" | strex == ".png")
            {
                //use the web.config to store the connection string
                if (lblImgStatus.Text == "" | lblImgStatus.Text=="PS")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Docs set ImgStatus=@ImgStatus, ImageName=@imagename, Image=@imagedata where SID='" + Convert.ToString(Session["sid"].ToString()) + "'", con);
                    // SqlCommand cmd = new SqlCommand("INSERT INTO Docs (ImageName,Image) VALUES (@imagename,@imagedata)", con);
                    cmd.Parameters.Add("@ImgStatus", SqlDbType.NVarChar, 50).Value = "P";
                    cmd.Parameters.Add("@imagename", SqlDbType.VarChar, 50).Value = imagename;
                    cmd.Parameters.Add("@imagedata", SqlDbType.Image).Value = imgbyte;
                    int count = cmd.ExecuteNonQuery();
                    con.Close();
                    lblImgException.Text = "Profile Image Saved.";
                    if (lblImgStatus.Text == "") lblImgTitle.Text = "upload Signature."; else lblImgTitle.Text = "Change Signature.";
                    ViewImg(); // show img form database;
                }
                else if (lblImgStatus.Text == "P")
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("update Docs set ImgStatus=@ImgStatus, SignName=@imagename, Sign=@imagedata where SID='" + Convert.ToString(Session["sid"].ToString()) + "'", con);
                    cmd1.Parameters.Add("@ImgStatus", SqlDbType.NVarChar, 50).Value = "PS";
                    cmd1.Parameters.Add("@imagename", SqlDbType.VarChar, 50).Value = imagename;
                    cmd1.Parameters.Add("@imagedata", SqlDbType.Image).Value = imgbyte;
                    int count = cmd1.ExecuteNonQuery();
                    con.Close();
                    lblImgException.Text = "Profile Image Saved.";
                    lblImgTitle.Text = "Change Profile Picture.";
                    ViewImg();
                }
                else
                {
                    lblImgTitle.Text = "Change Profile Image.";
                }
            }
            else
            {
                lblImgException.Text = "Wrong image format." + strex.ToString();
            }
        }
    }
    public void ViewImg()    
    {
        if (lblImgStatus.Text == "" | lblImgStatus.Text==null)
        {
            lblImgTitle.Text = "Upload Profile Picture.";
            imgDefault.Visible = true;
        }
        if (lblImgStatus.Text == "P")
        {
            lblImgTitle.Text = "Upload Signature.";
            imgDefault.Visible = false;
        }
        if (lblImgStatus.Text == "PS")
        {
            lblImgTitle.Text = "Change Profile Image";
            imgDefault.Visible = false;
        }

        SqlCommand command = new SqlCommand("SELECT ImageName,SID from [Docs] where SID='"+lblEnrolment.Text.ToString()+"'", con);
        SqlDataAdapter daimages = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        DataList1.DataSource = dt;
        DataList1.DataBind();
        SqlCommand cmd = new SqlCommand("SELECT SignName,SID from [Docs] where SID='" + lblEnrolment.Text.ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(command);
        DataTable dt2 = new DataTable();
        da.Fill(dt2);
        DataList2.DataSource = dt2;
        DataList2.DataBind();
        imgDefault.Visible = false;
        panelImage.Visible = true;
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
    protected void rbtn10_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtn10.Checked == true)
            pNewdoc.Visible = false;
        else pNewdoc.Visible = true;
        txtDocsNameOther.Text = "10";
    }
    protected void rbtn12_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtn12.Checked == true)
            pNewdoc.Visible = false;
        else pNewdoc.Visible = true;
        txtDocsNameOther.Text = "12";
    }
    protected void rbtnother_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {
            if (rbtnother.Checked == true)
                pNewdoc.Visible = true;
            else pNewdoc.Visible = true;
        }
    }
    public void ViewDoc(string str)
    {
        Response.Redirect("uploads/" + str);
    }
    protected void lbtn10View_Click(object sender, EventArgs e)
    {
        docsStatus();
        ViewDoc(lbllnk10.Text.ToString());
    }
    protected void lbtn10Download_Click(object sender, EventArgs e)
    {
        docsStatus();
        download(lbllnk10.Text.ToString());
    }
    protected void lbtndoc12View_Click(object sender, EventArgs e)
    {
        docsStatus();
        ViewDoc(lbllnk12.Text.ToString());
    }
    protected void lbtn12Download_Click(object sender, EventArgs e)
    {
        docsStatus();
       download(lbllnk12.Text.ToString());
    }
    protected void lbtndoc1View_Click(object sender, EventArgs e)
    {
        docsStatus();
        ViewDoc(lbllnk1.Text.ToString());
    }
    protected void lbtndoc1Download_Click(object sender, EventArgs e)
    {
        docsStatus();
        download(lbllnk1.Text.ToString());
    }
    protected void lbtndoc2View_Click(object sender, EventArgs e)
    {
        docsStatus();
        ViewDoc(lbllnk2.Text.ToString());
    }
    protected void lbtndoc2Download_Click(object sender, EventArgs e)
    {
        docsStatus();
        download(lbllnk2.Text.ToString());
    }
    protected void lbtndoc3View_Click(object sender, EventArgs e)
    {
        docsStatus();
        ViewDoc(lbllnk3.Text.ToString());
    }
    protected void lbtndoc3Download_Click(object sender, EventArgs e)
    {
        docsStatus();
        download(lbllnk3.Text.ToString());
    }
    protected void lbtndoc4View_Click(object sender, EventArgs e)
    {
        docsStatus();
        ViewDoc(lbllnk4.Text.ToString());
    }
    protected void lbtndoc4Download_Click(object sender, EventArgs e)
    {
        docsStatus();
        download(lbllnk4.Text.ToString());
    }
    protected void lbtndoc5View_Click(object sender, EventArgs e)
    {
        docsStatus();
        ViewDoc(lbllnk5.Text.ToString());
    }
    protected void lbtndoc5Download_Click(object sender, EventArgs e)
    {
        docsStatus();
        download(lbllnk5.Text.ToString());
    }
    protected void txtDocsNameOther_TextChanged(object sender, EventArgs e)
    {

    }
}