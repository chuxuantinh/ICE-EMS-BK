using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Xml;

public partial class Admission_AdditionalPaperStudent : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
            else
            {
                if (!IsPostBack)
                {

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
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select * from Student where SID='" + txtMembership.Text + "'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            tblView.Visible = true; lblerror.Text = "";
            lblEnrolment.Text = txtMembership.Text;
            lblNmae.Text = rd["Name"].ToString(); lblFatherName.Text = rd["FName"].ToString(); lblIMID.Text = rd["IMID"].ToString();
            rd.Close();
            cmd = new SqlCommand("select Course from ExamCurrent where SID='" + txtMembership.Text + "'", con);
            SqlDataReader rd1 = cmd.ExecuteReader();
            while (rd1.Read())
            {
                ddlCourse.SelectedItem.Text = rd1["Course"].ToString();
            }
            rd1.Close();
        }
        else
        {
            rd.Close(); tblView.Visible = false; lblerror.Text = "Membership NOt Found";
        }
        con.Close(); con.Dispose();
    }
    string[] subid;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select SID from PartIIStudent where SID='" + lblEnrolment.Text + "' and Course='" + ddlCourse.SelectedValue + "'", con);
        string sid = Convert.ToString(cmd.ExecuteScalar());
        if (sid == "")
        {
            lblerror.Text = "";
            string[] civil = { "TC 2.10", "TC 2.11" };
            string[] arch = { "TA 2.11", "TA 2.12" };
            if (ddlCourse.SelectedValue == "Civil")
            {
                subid = civil;
            }
            else if (ddlCourse.SelectedValue == "Architecture")
            {
                subid = arch;
            }
            for (int i = 0; i < subid.Length; i++)
            {
                cmd = new SqlCommand("insert into PartIIStudent (SID,Course,Part,SubID,Status,Remarks,Operator,Date) values(@SID,@Course,@Part,@SubID,@Status,@Remarks,@Operator,@Date)", con);
                cmd.Parameters.AddWithValue("@SID", lblEnrolment.Text);
                cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Part", "PartII");
                cmd.Parameters.AddWithValue("@SubID", subid[i]);
                cmd.Parameters.AddWithValue("@Status", "Fail");
                cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
                cmd.Parameters.AddWithValue("@Operator", Request.Cookies["MyLogin"]["UID"].ToString());
                cmd.Parameters.AddWithValue("Date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            tblView.Visible = false; lblerror.Text = "Successfully added"; txtremarks.Text = ""; txtMembership.Text = "";
        }
        else
        {
            lblerror.Text = "Record Already inserted for:" + lblEnrolment.Text;
        }
        con.Close();
    }
  
    protected void btnOK_click(object sender, EventArgs e)
    {
        GridAppTable.DataSource = bindgrid();
        GridAppTable.DataBind();
    }
  private DataTable  bindgrid()
    {
        SqlDataAdapter ad = new SqlDataAdapter();
        DataTable dt = new DataTable();
        dt.Clear();
        if (ddlViewBy.SelectedValue == "FailStudent")
        {
            ad = new SqlDataAdapter("select * from PartIIAdditionalCivil", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select * from PartIIAdditionalArch", con);
            ad.Fill(dt);
            
        }
        else
        {
            ad = new SqlDataAdapter("select * from PartIIAdditionalCiivilpassed", con);
            ad.Fill(dt);
            ad = new SqlDataAdapter("select * from PartIIAdditionalArchPassed", con);
            ad.Fill(dt);
            
        }
        return dt;
    }
  protected void btnUpdateFormResult_click(object sender, EventArgs e)
  {
      try
      {
          con.Close(); con.Open();
          cmd = new SqlCommand("update PartIIStudent set status='Pass' where sid in(select sid from SExamMarks where SubID='" + ddlSubID.SelectedValue.ToString() + "' and Status='Pass') and SubID='" + ddlSubID.SelectedValue.ToString() + "'", con);
          cmd.ExecuteNonQuery();
          lblExceptionUpdate.Text = ddlSubID.SelectedValue.ToString() + "successfully  Updated.";
      }
      catch (SqlException ex)
      {
          lblExceptionUpdate.Text = ex.ToString();

      }
      finally
      {
          con.Close(); con.Dispose();
      }
  }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridAppTable.AllowPaging = false;
        GridAppTable.DataSource = bindgrid();
        GridAppTable.DataBind();
        if (GridAppTable.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ApprovedMembership.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridAppTable.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}