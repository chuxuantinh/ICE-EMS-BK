using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Data.Sql;
using System.Configuration;
using System.Data.OleDb;

public partial class Admission_Autocad : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd;
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
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
            if (lvl == 0)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 1)
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());
            else if (lvl == 2)
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void btnUplode_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        txtNotUploaded.Text = "";
        string extension, FileName;
        string Path1;
        if (fileuploadExcel.HasFile)
        {
            extension = System.IO.Path.GetExtension(fileuploadExcel.PostedFile.FileName);
            if (extension == ".xls" |extension == ".xlsx")
            {
                FileName = fileuploadExcel.PostedFile.FileName;
                HttpPostedFile file = fileuploadExcel.PostedFile;
                int length = file.ContentLength;
                if (length > 0)
                {
                    string exten = Path.GetExtension(file.FileName);
                    string filename = "MCADData" + exten;
                    fileuploadExcel.SaveAs(Server.MapPath("~/Admission/MCAD/" + filename.ToString()));
                    Path1 = (Server.MapPath("~/Admission/MCAD/MCADData" + exten.ToString()));
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        string SourceConstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Path1 + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'";
                        OleDbConnection con1 = new OleDbConnection(SourceConstr);
                        con1.Open();
                        string query = "Select * from [Sheet1$]";
                        OleDbCommand Olcmd = new OleDbCommand(query, con1);
                        OleDbDataReader dr = Olcmd.ExecuteReader();
                        while (dr.Read())
                        {
                            string error = "";
                            con.Open();
                            string di = dr["SID"].ToString();
                            cmd = new SqlCommand("upload", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SID", SqlDbType.NVarChar).Value = dr["SID"].ToString();
                            cmd.Parameters.AddWithValue("@Value", SqlDbType.NVarChar).Value = rbtntype.SelectedValue;
                            cmd.Parameters.AddWithValue("@updateValue", SqlDbType.NVarChar).Value = dr["Value"];
                            cmd.Parameters.Add("@ERROR", SqlDbType.NVarChar, 50);
                            cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            error = (string)cmd.Parameters["@ERROR"].Value;
                            if (error != "")
                            { txtNotUploaded.Text += (string)cmd.Parameters["@ERROR"].Value + "<br/>"; } con.Close();
                        }
                        dr.Close(); con1.Close(); con.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Auto-Cad form(s) Data Updated Successfully')", true);
                    }
                    catch (Exception ex) { lblMessage.Text = ex.Message; }
                    finally { con.Dispose(); }
                }
            } else { lblMessage.ForeColor = System.Drawing.Color.Red; lblMessage.Text = "File Format should in Excel format"; }
        }
    }
    private void BindRadio(string sid,string value)
    {
        string qry = "";
        if (rbtntype.SelectedValue == "RegNo")
        {
            qry = "update MCAD set RegNo='" + value + "' where SID='" + sid + "' and CurrentStatus='Current'";
        }
        else if (rbtntype.SelectedValue == "Grade")
        {
            qry = "update MCAD set Grade='" + value + "' where RegNo='" + sid + "'";
        }
        else if (rbtntype.SelectedValue == "Status")
        {
            qry = "update MCAD set Status='" + value + "' where RegNo='" + sid + "'";
        }
    }
}