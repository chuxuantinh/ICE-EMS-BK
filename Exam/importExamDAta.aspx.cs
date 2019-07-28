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

public partial class Exam_importExamDAta : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                SessionDuration sd = new SessionDuration();
                if (se == 0)
                {
                    ddlExamSeason.SelectedValue = "Sum";
                }
                else { ddlExamSeason.SelectedValue = "Win"; }// lblFromName.Text = "Membership No:";
                lblSeasonHidden.Text = sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblSeasonHidden.Text = sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();

                ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
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
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void btnSessionOK_OnClick(object sender, EventArgs e)
    {
     SessionDuration   sd = new SessionDuration();
     lblSeasonHidden.Text = sd.SessionToSessionID(ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString()).ToString();

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string extension;
        string Path1;
        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(Server.MapPath("~/Exam/Result/" + filename));
        extension = System.IO.Path.GetExtension(filename);
        if (extension == ".XLS" | extension == ".xls" | extension == ".xlsx" | extension == ".XLSX")
        {
            Path1 = (Server.MapPath("~/Exam/Result/" + filename));
            try
            {
                DataTable dtExcel = new DataTable();
                string SourceConstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Path1 + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'";
                OleDbConnection con1 = new OleDbConnection(SourceConstr);
                con1.Open();
                string query = "Select * from [Sheet1$]";
                OleDbCommand Olcmd = new OleDbCommand(query, con1);
                OleDbDataReader dr = Olcmd.ExecuteReader();
                con.Open();
                cmd = new SqlCommand("delete upseating", con); cmd.ExecuteNonQuery();
                while (dr.Read())
                {
                    lblMessage.Text = "";
                    cmd = new SqlCommand("insert into upSeating(SID,RollNo,Center,SubID,SubName,Course,Part,Name,FName,IMID,Date,Shift) values(@SID,@RollNo,@Center,@SubID,@SubName,@Course,@Part,@Name,@FName,@IMID,@Date,@Shift)", con);

                    cmd.Parameters.AddWithValue("@SID", SqlDbType.NVarChar).Value = dr["SID"].ToString();
                    cmd.Parameters.AddWithValue("@RollNo", SqlDbType.NVarChar).Value = dr["RollNo"].ToString();

                    cmd.Parameters.AddWithValue("@Center", SqlDbType.NVarChar).Value = dr["Center"].ToString();
                    cmd.Parameters.AddWithValue("@SubID", SqlDbType.NVarChar).Value = dr["SubID"].ToString();
                    cmd.Parameters.AddWithValue("@SubName", SqlDbType.NVarChar).Value = dr["SubName"].ToString();

                    cmd.Parameters.AddWithValue("@Course", SqlDbType.NVarChar).Value = dr["Course"].ToString();
                    cmd.Parameters.AddWithValue("@Part", SqlDbType.NVarChar).Value = dr["Part"].ToString();
                    cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = dr["Name"].ToString();
                    cmd.Parameters.AddWithValue("@FName", SqlDbType.NVarChar).Value = dr["FName"].ToString();
                    cmd.Parameters.AddWithValue("@IMID", SqlDbType.NVarChar).Value = dr["IMID"].ToString();
                    cmd.Parameters.AddWithValue("@Date", SqlDbType.Int).Value = dr["Date"].ToString();
                    cmd.Parameters.AddWithValue("@Shift", SqlDbType.Int).Value = dr["Shift"].ToString();
                    cmd.ExecuteNonQuery();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Updated Successfully')", true);
                SqlDataAdapter ad = new SqlDataAdapter("select * from UpSeating", con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lblMessage.Text = lblMessage.Text + "Total Records " + dt.Rows.Count.ToString();
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("spStoredProcedure", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Centercode", txtCenterCoe.Text);
            cmd.ExecuteNonQuery();
            lblMessage.Text = "Success Fully Updated.";
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert(Error!)", true);
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
}