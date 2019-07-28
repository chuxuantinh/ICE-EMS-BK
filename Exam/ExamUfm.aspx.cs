using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

public partial class Exam_ExamUfm : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                ddlExamSeason.Focus();
                grvExamUfmfill();
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                maikal mk = new maikal();
                int sn = mk.chksession();
                if (sn == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                rbtnRollNo.Checked = true;
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtRollNo.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    string qury;
    protected void btnOK_OnClcick(object sender, EventArgs e)
    {
        if (rbtnRollNo.Checked == true)
        {
            qury = "select * from ExamForm where RollNo='" + txtRollNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'";
        }
        else if (rbtnSID.Checked == true)
        {
            qury = "select * from ExamForm where SID='" + txtRollNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'";
        }
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand(qury, con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())           
            {
                lblExamDate.Text = Convert.ToDateTime(reader["Date"].ToString()).ToString("dd/MM/yyyy");
                lblShift.Text = reader["Shift"].ToString();
                lblCenterCode.Text = reader["CenterCode"].ToString();
                lblCenterName.Text = reader["CenterName"].ToString() + ", " + reader["City"].ToString();
                lblCourse.Text = reader["Course"].ToString(); lblPart.Text = reader["Part"].ToString();
                lblSID.Text = reader["SID"].ToString();
                if (lblPart.Text == "PartI" | lblPart.Text == "PartII")
                    lblStream.Text = "Technician Engineering";
                else if (lblPart.Text == "SectionA" | lblPart.Text == "SectionB")
                    lblStream.Text = "Associate Engineering";
            }
            reader.Close();
            reader.Dispose();
            SqlDataAdapter ad = new SqlDataAdapter(qury, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            ddlSubID.DataSource = ds;
            ddlSubID.DataTextField = "SubID";
            ddlSubID.DataValueField = "SubID";
            ddlSubID.DataBind();
            con.Close();
           
            subname(ddlSubID.SelectedValue.ToString(), lblPart.Text.ToString());
            pnlSubject.Visible = true;
            if (lblSubName.Text == "")
            {
                lblExceptionOK.Text = "Invaid Code."; btnSubmit.Enabled = false;
                lblExceptionOK.ForeColor = System.Drawing.Color.Red;
                lblExceptionOK.Font.Bold = true;
                lblCenterCode.Text = ""; lblCenterName.Text = ""; lblSID.Text = ""; lblExamDate.Text = ""; lblShift.Text = "";
                lblCourse.Text = ""; lblStream.Text = "";
                pnlSubject.Visible = false;
            }
            else
            {
                lblExceptionOK.Text = "";
            }
        }
        catch (SqlException ex)
        {
            lblExceptionOK.Text = ex.ToString();
        }
    }
    protected void btnSubmit_Onclick(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("insert into ExamUFM(RollNo,SID,Session,Course,Part,SubID,SubName,ExamDate,Shift,CenterCode,CenterName,Details,Status) Values(@RollNo,@SID,@Session,@Course,@Part,@SubID,@SubName,@ExamDate,@Shift,@CenterCode,@CenterName,@Details,@Status)", con);
            cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text.ToString());
            cmd.Parameters.AddWithValue("@SID", lblSID.Text.ToString());
            cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", lblCourse.Text.ToString());
            cmd.Parameters.AddWithValue("@Part", lblPart.Text.ToString());
            cmd.Parameters.AddWithValue("@SubID", ddlSubID.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SubName", lblSubName.Text.ToString());
            cmd.Parameters.AddWithValue("@ExamDate", lblExamDate.Text.ToString());
            cmd.Parameters.AddWithValue("@Shift", lblShift.Text.ToString());
            cmd.Parameters.AddWithValue("@CenterCode", lblCenterCode.Text.ToString());
            cmd.Parameters.AddWithValue("@CenterName", lblCenterName.Text.ToString());
            cmd.Parameters.AddWithValue("@Details", txtDetails.Text.ToString());
            cmd.Parameters.AddWithValue("@Status", "Unfair");
            cmd.ExecuteNonQuery();
            lblExceptionSubmit.Text = "UFM Case Submitted Successfully.";
            btnSubmit.Enabled = false;
            grvExamUfmfill();
        }
        catch (SqlException ex)
        {
            lblExceptionSubmit.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void ddlSubID_SelectedInxdexChanted(object sender, EventArgs e)
    {
        subname(ddlSubID.SelectedValue.ToString(),lblPart.Text.ToString());
    }
    string quri;
    private void subname(string subid, string part)
    {
        try
        {
            if (lblPart.Text != "")
            {
                btnSubmit.Enabled = true;
                con.Close(); con.Open();
                if (lblPart.Text == "PartI" | lblPart.Text == "PartII")
                  quri = "select SubName from CivilSubMaster where SubID='" + subid.ToString() + "'";
                else if (lblPart.Text == "SectionA" | lblPart.Text == "SectionB")
                    quri = "select SubName from ArchiSubMaster where SubID='" + subid.ToString() + "'";
                SqlCommand cmd = new SqlCommand(quri, con);
                string subname = Convert.ToString(cmd.ExecuteScalar());
                lblSubName.Text = subname.ToString();
            }
        }
        catch (SqlException ex)
        {
            lblExceptionSubmit.Text = ex.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    protected void grvExamUfm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExamUfmfill();
        grvExamUfm.PageIndex = e.NewPageIndex;
        grvExamUfm.DataBind();
    }
    private void grvExamUfmfill()
    {
        string strupdate = "select RollNo,Course,Part,SubID,SubName,ExamDate,Shift,CenterCode,CenterName,Details,Status from ExamUFM where Session='" +lblHiddenSeason.Text + "' and SubID='"+ddlSubID.SelectedValue+"'";
        SqlDataAdapter adp = new SqlDataAdapter(strupdate, con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        grvExamUfm.DataSource = ds;
        grvExamUfm.DataBind();
    }
   
}
