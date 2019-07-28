using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

public partial class Exam_BookletRange : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd; SqlDataAdapter adp;
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
                    ddlExamSeason.SelectedValue = "Sum";
                else { ddlExamSeason.SelectedValue = "Win"; }
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                BindGrid(); BindPCode(); ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
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
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString(); BindGrid();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString(); BindGrid(); txtYearSeason.Focus();
    }
    string str1; string str2; string str3;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblException.Text = "";
            if (txtStart.Text == "" | txtEnd.Text == "" | (txtStart.Text == "0" && txtEnd.Text == "0")) { lblException.Text = "Please enter booklet range!"; txtStart.Text = ""; txtEnd.Text = ""; txtStart.Focus(); }
            else if (Convert.ToInt32(txtStart.Text) <= Convert.ToInt32(txtEnd.Text))
            {
                con.Open();
                adp = new SqlDataAdapter("select StartRange, EndRange, SubID from BookletRange where  Session='" + lblExamSeasonHidden.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "'", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                int j = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i]; str1 = dr["StartRange"].ToString(); str2 = dr["EndRange"].ToString();
                    if ((Convert.ToInt32(txtStart.Text) < Convert.ToInt32(str1) && Convert.ToInt32(txtEnd.Text) < Convert.ToInt32(str1)) | (Convert.ToInt32(txtStart.Text) > Convert.ToInt32(str2) && Convert.ToInt32(txtEnd.Text) > Convert.ToInt32(str2)))
                    {
                        j++;
                    }
                }
                if (j == dt.Rows.Count)
                {
                    cmd = new SqlCommand("insert into BookletRange(Session,SubID,StartRange,EndRange,type,Qty) values(@Session,@SubID,@StartRange,@EndRange,@type,@Qty)", con);
                    cmd.Parameters.AddWithValue("@Session", lblExamSeasonHidden.Text.ToString());
                    cmd.Parameters.AddWithValue("@SubID", ddlPCode.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@StartRange", txtStart.Text);
                    cmd.Parameters.AddWithValue("@EndRange", txtEnd.Text);
                    cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Qty", (Convert.ToInt32(txtEnd.Text) - Convert.ToInt32(txtStart.Text) + 1));
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Submitted Successfully !')", true);
                    BindGrid(); ddlExamSeason.Focus();
                }
                else
                {
                    lblException.Text = "Booklet Range is out of Range Please enter correct Range!"; txtStart.Focus();
                }
            }
            else { lblException.Text = "Please enter correct range it can not be less than start range.!"; }
        }
        catch (SqlException ex) { lblException.Text = ex.ToString(); } con.Close(); 
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblException.Text = "";
            if (txtStart.Text == "" | txtEnd.Text == "") { lblException.Text = "Please enter booklet range!"; txtStart.Text = ""; txtEnd.Text = ""; txtStart.Focus(); }
            else if (Convert.ToInt32(txtStart.Text) <= Convert.ToInt32(txtEnd.Text))
            {
                con.Open(); lblException.Text = "";
                adp = new SqlDataAdapter("select StartRange, EndRange, SubID from BookletRange where SubID='" + ddlPCode.SelectedValue.ToString() + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "'", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                int j = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i]; str1 = dr["StartRange"].ToString(); str2 = dr["EndRange"].ToString();
                    if ((Convert.ToInt32(txtStart.Text) < Convert.ToInt32(str1) && Convert.ToInt32(txtEnd.Text) < Convert.ToInt32(str1)) | (Convert.ToInt32(txtStart.Text) > Convert.ToInt32(str2) && Convert.ToInt32(txtEnd.Text) > Convert.ToInt32(str2)))
                    {
                        j++;
                    }
                }
                if (j == dt.Rows.Count)
                {
                    cmd = new SqlCommand("update BookletRange set SubID=@SubID,StartRange=@StartRange,EndRange=@EndRange,Qty=@Qty where Session='" + lblExamSeasonHidden.Text.ToString() + "' and SN='" + GridRange.SelectedDataKey.Value.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("SubID", ddlPCode.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("StartRange", txtStart.Text);
                    cmd.Parameters.AddWithValue("EndRange", txtEnd.Text);
                    cmd.Parameters.AddWithValue("@Qty", (Convert.ToInt32(txtEnd.Text) - Convert.ToInt32(txtStart.Text) + 1));
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Updated Successfully !')", true);
                    BindGrid(); GridRange.Focus();
                }
                else
                {
                    lblException.Text = "Booklet Range is out of Range Please enter correct Range!"; txtStart.Focus();
                }
            }
            else { lblException.Text = "Please enter correct range it can not be less than start range.!"; }
        }
        catch (SqlException ex) { lblException.Text = ex.ToString(); } con.Close();
    }
    private void BindGrid()
    {
        adp = new SqlDataAdapter("select * from BookletRange where Session='" + lblExamSeasonHidden.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "'", con);
        DataTable dt = new DataTable(); adp.Fill(dt); GridRange.DataSource = dt; GridRange.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect(url.ToString());
    }
    protected void DeleteRecord(object sender, GridViewDeleteEventArgs e)
    {
        string SN = GridRange.DataKeys[e.RowIndex].Value.ToString();
        try
        {
            con.Open(); cmd = new SqlCommand("delete from BookletRange where SN='" + SN + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Record deleted successfully.!')", true); BindGrid();
        }
        catch (SqlException ee) { lblException.Text = ee.ToString(); }
        finally { con.Close(); con.Dispose(); }
    }
    protected void GridRange_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdd.Visible = false; btnCancel.Visible = true; btnUpdate.Visible = true;
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from BookletRange where SN='" + GridRange.SelectedDataKey.Value.ToString() + "'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            ddlPCode.SelectedItem.Text = rd["SubID"].ToString();
            txtStart.Text = rd["StartRange"].ToString();
            txtEnd.Text = rd["EndRange"].ToString();
            ddlType.SelectedValue = rd["Type"].ToString();
        }
        rd.Close(); con.Close(); con.Dispose(); ddlPCode.Focus();
    }
    string strCBind; string strABind;
    private void BindPCode()
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/XML/PaperCode.xml"));
            XmlElement el = doc.DocumentElement;
            XmlNodeList nlist = el.ChildNodes;
            foreach (XmlNode nd in nlist)
            {
                ddlPCode.Items.Add(nd.InnerText.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
        }
        //SqlDataAdapter adp1;
        //strCBind = "select SubID from CivilSubMaster where CourseID='081' order by SubID asc";
        //strABind = "select SubID from ArchiSubMaster where CourseID='081' order by SubID asc";
        //adp = new SqlDataAdapter(strCBind, con); adp1 = new SqlDataAdapter(strABind, con);
        //DataTable dt = new DataTable();
        //adp.Fill(dt); adp1.Fill(dt); ddlPCode.DataSource = dt; int i = dt.Rows.Count; ddlPCode.DataValueField = "SubID"; ddlPCode.DataBind();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}