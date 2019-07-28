using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Administrator_Default : System.Web.UI.Page
{
    #region Connection
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    string Qry;
    #endregion

    #region Function
    private void BindGridviewData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select ID,Name,FileName,FilePath from IMDocuments ORDER BY SN DESC", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        gvDetails.DataSource = ds;
        gvDetails.DataBind();
    }
    private void GridviewSearch()
    {
        con.Open();
        Qry = "select * from IMDocuments where ID='" + txtIMID.Text.ToString() + "'";
        SqlDataAdapter adp = new SqlDataAdapter(Qry, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }
    private void cleartext()
    {
        txtIMID.Text = String.Empty;
        txtIMName.Text = string.Empty;
    }
    private void getdata()
    {
        con.Close();  
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Member where ID='" + txtIMID.Text.ToString() + "'", con);
        SqlDataReader rd;
        rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            txtIMName.Text = rd["Name"].ToString();
        }
        con.Close();      
    }
    #endregion

    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridviewData();
            btnUpload.Visible = false;
            gvDetails.Visible = true;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select ID from Member where ID='" + txtIMID.Text.ToString() + "'", con);
        string strstatus = Convert.ToString(cmd.ExecuteScalar());
        if (strstatus == txtIMID.Text.ToString())
        {
            txtIMID.Text = txtIMID.Text.ToString();
            btnUpload.Visible = true;
            gvDetails.Visible = true;
        }
        else if (strstatus != txtIMID.Text.ToString())
        {
            txtIMID.Text = "Invalid IM ID.";
            btnUpload.Visible = false;
            gvDetails.Visible = true;
        }
        //else if (txtIMID.Text != String.Empty?)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter IM.........!!!');", true);
        //}
        getdata();
        GridviewSearch();
        gvDetails.Visible = true;
        con.Close();
        con.Dispose();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(Server.MapPath("~/XML/Files/" + filename));
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into IMDocuments (ID,Name,FileName,FilePath) values(@ID,@Name,@FileName,@FilePath)", con);
        cmd.Parameters.AddWithValue("@ID",txtIMID.Text);
        cmd.Parameters.AddWithValue("@Name",txtIMName.Text);
        cmd.Parameters.AddWithValue("@FileName", filename);
        cmd.Parameters.AddWithValue("@FilePath", "~/XML/Files/" + filename);
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data successfully Upload.........!!!');", true);
        BindGridviewData();
        cleartext();
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        string filePath = gvDetails.DataKeys[gvrow.RowIndex].Value.ToString();
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        BindGridviewData();
    }
    #endregion
}