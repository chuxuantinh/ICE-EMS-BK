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
using System.Drawing;
using System.Drawing.Imaging;

public partial class Admission_UploadmultipleImage : System.Web.UI.Page{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {

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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (filMyFile.HasFile)
        {
            //getting length of uploaded file
            int length = filMyFile.PostedFile.ContentLength;
            //create a byte array to store the binary image data
            byte[] imgbyte = new byte[length];
            //store the currently selected file in memeory
            HttpPostedFile img = filMyFile.PostedFile;
            //set the binary data
            img.InputStream.Read(imgbyte, 0, length);
            string imagename = filMyFile.PostedFile.FileName;
            string strex = Path.GetExtension(imagename);
            if (strex == ".tif")
            { 
                filMyFile.SaveAs(Server.MapPath("~/PICS/" + txtSID.Text + strex));
                txtSID.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Uploaded')", true);

            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Image Not in .tif Format')", true);

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblExcept.Text = ""; lblImgException.Text = "";
        if (Convert.ToInt32(txtMem1.Text) <= Convert.ToInt32(txtMem1.Text))
        { 
            for (int i = Convert.ToInt32(txtMem1.Text); i <= Convert.ToInt32(txtMem2.Text); i++)
            {
                try
                {
                    string Path1 = (Server.MapPath("~/PICS/" + i.ToString() + ".tif" + ""));
                    TiffImageConverter.ConvertTiffToJpeg(Path1);
                    if (File.Exists(Path1)) File.Delete(Path1);
                     string Path2 = (Server.MapPath("~/PICS/" + i.ToString() +""+ ".jpg" + ""));
                    byte[] imgbyte = File.ReadAllBytes(@Path2);
                    string strex = Path.GetExtension(Path2);//For retrive file for export into Database

                    if (strex == ".jpg" | strex == ".tif" | strex == ".tiff")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update Docs set ImgStatus=@ImgStatus, ImageName=@imagename, Image=@imagedata where SID='" + i + "'", con);
                        cmd.Parameters.Add("@ImgStatus", SqlDbType.NVarChar, 50).Value = "P";
                        cmd.Parameters.Add("@imagename", SqlDbType.VarChar, 50).Value = i.ToString() + strex;
                        cmd.Parameters.Add("@imagedata", SqlDbType.Image).Value = imgbyte;
                        int count = cmd.ExecuteNonQuery();
                        con.Close();
                        lblImgException.Text = "Profile Image Saved.";
                    }
                }
                catch (SqlException ex)
                {
                    lblImgException.Text = "Invalid";
                }
                catch (FileNotFoundException ex)
                {
                    lblExcept.Text += "Image Not Found for" + i.ToString()+"<br>";
                }


            }
        }
        else lblImgException.Text = "Pls Insert Correct Range";
       
    }

    
}