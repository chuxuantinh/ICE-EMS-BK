using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class Admission_DeleteAdmission_ : System.Web.UI.Page
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
                    lblTotalAmount.Visible = false;
                    txtYear.Text = DateTime.Now.Year.ToString();
                    maikal mk = new maikal();
                    int sn = mk.chksession();
                    if (sn == 0) ddlSession.SelectedValue = "Sum"; else ddlSession.SelectedValue = "Win";
                    lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected DataTable disp()
    {
        string strqry = "select Enrolment,AppNo,Amount,LateFee,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,Exempted,DupForm, Course ,Part,Session,IMID,Status,Name,FName,FormType from AppRecord where Enrolment='" + txtEnter.Text + "' and Session='" + ddlSession.SelectedValue + txtYear.Text + "'";
        adp = new SqlDataAdapter(strqry, con);
        DataTable dt = new DataTable();            
        adp.Fill(dt);
        grviti.DataSource = dt;
        grviti.DataBind();
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            if (dr[18].ToString().Contains("ReAdmission") == true)
            {
                rbtnOldAdmission.Checked = true;
                rbtnNewAdmission.Enabled = false;
            }
            else
            {
                rbtnNewAdmission.Checked = true;
                rbtnOldAdmission.Enabled = false;
            }
        }
        return dt;
    }
    SqlDataReader dtr;
    SqlCommand cmd;
    protected void Exam()
    {
        con.Close();
        con.Open();
        cmd = new SqlCommand("select Course ,Part from ExamCurrent where Sid='" + txtEnter.Text + "' and Session='" + ddlSession.SelectedValue + txtYear.Text + "'",con);
        dtr = cmd.ExecuteReader();
    if (dtr.Read())
    {
        ddlCourse.SelectedValue = dtr["Course"].ToString();      
    }
    if (rbtnOldAdmission.Checked == true)
    {
        ddlCourse.Visible = true;
        ddlpart.Visible = true;
    }
    cmd.Dispose();
    con.Close();         
    }
    protected void ITIForm()
    {
        if (Convert.ToInt32(grviti.Rows[0].Cells[7].Text.TrimEnd('0').TrimEnd('.')) > 0)
        {
            cmd = new SqlCommand("Delete from ITIForm where SID='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "'", con);
            cmd.ExecuteNonQuery();
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        rbtnOldAdmission.Enabled = true;
        rbtnNewAdmission.Enabled = true;
        lblTotalAmount.Visible = true;
        lblAmount.Text = "0";
        disp();
        Exam();
        btnDelete.Enabled = true;
    }
    protected void RecoverAppRecord()
    {
        string str = grviti.Rows[0].Cells[15].Text;
        if (str != "NotApproved" && str != "Hold")
        {
            cmd = new SqlCommand("Insert into RecoverApp(FormType,APPNo,Amount,Type,Enrolment,IMID,SerialNo,Session,Course,Part,Status,Remark,Date) values(@FormType,@APPNo,@Amount,@Type,@Enrolment,@IMID,@SerialNo,@Session,@Course,@Part,@Status,@Remark,@Date)", con);
            cmd.Parameters.AddWithValue("@FormType", "Admission");
            cmd.Parameters.AddWithValue("@AppNo", grviti.Rows[0].Cells[1].Text);
            cmd.Parameters.AddWithValue("@Amount", lblAmount.Text);
            cmd.Parameters.AddWithValue("@Type", "Credit");
            cmd.Parameters.AddWithValue("@Enrolment", grviti.Rows[0].Cells[0].Text);
            cmd.Parameters.AddWithValue("@IMID", grviti.Rows[0].Cells[14].Text);
            cmd.Parameters.AddWithValue("@SerialNo", grviti.Rows[0].Cells[0].Text);
            cmd.Parameters.AddWithValue("@Session", grviti.Rows[0].Cells[13].Text);
            cmd.Parameters.AddWithValue("@Course", grviti.Rows[0].Cells[11].Text);
            cmd.Parameters.AddWithValue("@Part", grviti.Rows[0].Cells[12].Text);
            cmd.Parameters.AddWithValue("@Status", "NotApproved");
            cmd.Parameters.AddWithValue("@Remark", "Delete");
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        con.Open();
        if (grviti.Rows.Count != 0)
        {
            SqlCommand cmd;
            if (rbtnNewAdmission.Checked == true)
            {
                cmd = new SqlCommand("Delete from Docs where Docs.SID in (select SN from Student where SID='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "')", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Delete  from Student where SID='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Delete  from Examcurrent where SID='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Delete from AppRecord where Enrolment='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "'", con);
                cmd.ExecuteNonQuery();
                ITIForm();
                RecoverAppRecord();
                lblAmount.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Cancelled')", true);                          
            }
            else
            {
                cmd = new SqlCommand("Delete from AppRecord where Enrolment='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "' and AdmissionFees>0", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Delete from AppRecord where Enrolment='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "' and ExamFees>0 and Part='SectionA'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Examcurrent set ExamStatus='NotSubmitted', Course='"+ddlCourse.SelectedValue+"', Part='"+ddlpart.SelectedValue+"' where SID='" + grviti.Rows[0].Cells[0].Text + "' and session='" + ddlSession.SelectedValue + txtYear.Text + "'", con);
                cmd.ExecuteNonQuery();
                ITIForm();
                RecoverAppRecord();
                lblAmount.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Cancelled')", true);                          
            }
        }
        con.Close();
        con.Dispose();
        btnDelete.Enabled = false;
    }
    int Amount = 0;
    protected void grviti_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            Amount =Amount+Convert.ToInt32(e.Row.Cells[2].Text.TrimEnd('0').TrimEnd('.')) + Convert.ToInt32(e.Row.Cells[3].Text.TrimEnd('0').TrimEnd('.'));
        lblAmount.Text = Amount.ToString();
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
}