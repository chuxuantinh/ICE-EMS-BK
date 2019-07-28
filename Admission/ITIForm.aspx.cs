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
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Globalization;
public partial class Admission_ITIForm : System.Web.UI.Page
{
    SqlDataAdapter adp;
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
                    txtYear.Text = DateTime.Now.Year.ToString();
                    maikal mk = new maikal();
                    int sn = mk.chksession();
                    if (sn == 0) ddlSessionSelect.SelectedValue = "Sum"; else ddlSessionSelect.SelectedValue = "Win";
                    lblSessionHidden.Text = ddlSessionSelect.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                    ddlSessionSelect.Focus();
                    grviti.DataSource = disp();
                    grviti.DataBind();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void ddlSession_ONSelectediNdexCanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSessionSelect.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        grviti.DataSource = disp(); grviti.DataBind();
        txtYear.Focus();
    }
    protected void txtYear_OnTextChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSessionSelect.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        grviti.DataSource = disp(); grviti.DataBind();

    }
    protected DataTable disp()
    {
        string strqry = "select SNO, Name,FName,SID,IMID,AppNo,Stream,Course,Part,DOB,Session,SubDate, Amount,Status from ITIForm where Status='Approved' and Session='" + lblSessionHidden.Text + "' order by convert(int, AppNo)";
        adp = new SqlDataAdapter(strqry, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    protected void grviti_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grviti.PageIndex = e.NewPageIndex;
        grviti.DataSource = disp();
        grviti.DataBind();
    }
    protected void clear()
    {
        txtIMID.Text = "";
        txtFName.Text = "";
        txtApplicationNo.Text = "";
        txtAmount.Text = "";
        txtIMID.Text = "";
        txtName.Text = "";
        txtSID.Text = "";
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
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
    protected void grviti_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblITIStatus.Text = "";
        lblStatus.Text = "";
        pnlStudent.Visible = true;
        panApprove.Visible = true;
        if (grviti.SelectedRow.Cells[8].Text == "Civil")
        {
            ddlCourse.SelectedValue = "Civil";
        }
        else
            ddlCourse.SelectedValue = "Architecture";
        txtSubDate.Text = grviti.SelectedRow.Cells[12].Text;
        if (grviti.SelectedRow.Cells[9].Text == "PartI")
        {
            ddlPart.SelectedValue = "PartI";
        }
        if (grviti.SelectedRow.Cells[9].Text == "PartII")
        {
            ddlPart.SelectedValue = "PartII";
        }
        if (grviti.SelectedRow.Cells[9].Text == "SectionA")
        {
            ddlPart.SelectedValue = "SectionA";
        }
        if (grviti.SelectedRow.Cells[9].Text == "SectionB")
        {
            ddlPart.SelectedValue = "SectionB";
        }
        txtAmount.Text = grviti.SelectedRow.Cells[13].Text;
        txtDOB.Text = grviti.SelectedRow.Cells[10].Text;
        txtFName.Text = grviti.SelectedRow.Cells[3].Text;
        txtName.Text = grviti.SelectedRow.Cells[2].Text;
        txtSID.Text = grviti.SelectedRow.Cells[4].Text;
        txtIMID.Text = grviti.SelectedRow.Cells[5].Text;
        if (grviti.SelectedRow.Cells[11].Text.Substring(0, 3) == "Sum")
            ddlSession.SelectedValue = "Sum";
        else ddlSession.SelectedValue = "Win";
        txtYearUpdate.Text = grviti.SelectedRow.Cells[11].Text.Substring(3, 4);
        if (grviti.SelectedRow.Cells[7].Text == "Tech")
            ddlStream.SelectedValue = "Tech";
        else
            ddlStream.SelectedValue = "Asso";
        txtApplicationNo.Text = grviti.SelectedRow.Cells[6].Text;
        txtStuID.Text = grviti.SelectedRow.Cells[4].Text;
    }
    protected void grviti_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[12].Text = Convert.ToDateTime(e.Row.Cells[12].Text).ToString("dd/MM/yyyy");
        }
    }
    SqlCommand cmd;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = "";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Close();
            con.Open();
            cmd = new SqlCommand("select SID from ITIForm where SID='" + txtSID.Text.ToString() + "'", con);
            string chk = Convert.ToString(cmd.ExecuteScalar());
            if (chk != txtSID.Text || txtSID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Updated')", true);
            }
            else
            {
                cmd = new SqlCommand("update ITIForm set Name=@Name,FName=@FName,SID=@SID,IMID=@IMID,AppNo=@AppNo,Stream=@Stream,Course=@Course,Part=@Part,DOB=@DOB,Session=@Session,SubDate=@SubDate,Amount=@Amount where SID='" + txtSID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
                cmd.Parameters.AddWithValue("@FName", txtFName.Text.ToString());
                cmd.Parameters.AddWithValue("@SID", txtSID.Text.ToString());
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.Parameters.AddWithValue("@Stream", ddlStream.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@AppNo", txtApplicationNo.Text.ToString());
                cmd.Parameters.AddWithValue("@Session", ddlSession.SelectedValue.ToString() + txtYearUpdate.Text.ToString());
                cmd.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(txtSubDate.Text.ToString(), dtinfo));
                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.ToString());
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(txtDOB.Text.ToString(), dtinfo));
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Updated')", true);
                grviti.DataSource = disp();
                grviti.DataBind();
            }
            clear();
        }
        catch (SqlException ex)
        {
            lblStatus.Text = ex.ToString();
            clear();
        }
        catch (System.FormatException ex)
        {
            lblStatus.Text = "Wrong Format";
            clear();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            lblITIStatus.Text = "";
            cmd = new SqlCommand("select SID from ITIForm where SID='" + txtStuID.Text.ToString() + "'", con);
            string chk = Convert.ToString(cmd.ExecuteScalar());
            if (chk != txtStuID.Text || txtStuID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Updated')", true);
            }
            else
            {
                cmd = new SqlCommand("update ITIForm set Status=@Status where SID='" + txtStuID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@Status", ddlITIStatus.SelectedItem.Text);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Updated')", true);
            }
            grviti.DataSource = disp();
            grviti.DataBind();
        }
        catch (SqlException ex)
        {
            lblITIStatus.Text = ex.ToString();
        }
        catch (System.FormatException ex)
        {
            lblITIStatus.Text = "Wrong Format";
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
}