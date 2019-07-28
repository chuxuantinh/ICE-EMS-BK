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

public partial class Admission_ChangeIMID : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ddlExamSeason.Focus();
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            }
            txtStudent.Visible = false;
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtSID.Focus();
    }
    protected void txtIMID_OnTextChanged(object sender, EventArgs e)
    {
        if (txtSID.Text == "" && txtIMID.Text == "")
        {
            pnlChange.Visible = false;
        }
        else if (txtSID.Text != "" && txtIMID.Text == "")
        {
            lblIMName.Visible = false; lblGroupID.Visible = false; chkNOC.Visible = false;
        }
        else if (txtSID.Text != "" && txtIMID.Text != "")
        {
            lblIMName.Visible = true; lblGroupID.Visible = true; chkNOC.Visible = true;
        }
        else if (txtSID.Text == "" && txtIMID.Text != "")
        {
            chkNOC.Visible = true;
        }
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd =  new SqlCommand("select ID from IM where ID='" + txtIMID.Text.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        int i = 0;
        if (chk == txtIMID.Text.ToString())
        {
            i += 1;
        }
        else
        {
            lblException.Text = "Please Insert Valid IM ID.";
            txtIMID.Text = ""; lblIMName.Visible = false; lblGroupID.Visible = false; chkNOC.Visible = false;
            txtIMID.Focus();
        }
        if (i == 1)
        {
            lblException.Text = "";
            cmd = new SqlCommand("select * from IM where ID='" + txtIMID.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                lblGroupID.Text = "New IMID : [" + reader["GID"].ToString() + "]";
                pnlChange.Visible = true;
            }
            reader.Close();
            txtRemark.Focus();
        }
        con.Close();
    }
    protected void txtSID_OnTextChanged(object sendr, EventArgs e)
    {
        if (txtSID.Text == "" && txtIMID.Text=="")
        {
            pnlChange.Visible = false;
            txtSID.Focus();
        }
        else if (txtSID.Text == "" && txtIMID.Text != "")
        {
            lblStudentName.Visible = false; lblOldIMID.Visible = false; lblOldIMIDName.Visible = false;
        }
        else if (txtSID.Text != "" && txtIMID.Text != "")
        {
            lblStudentName.Visible = true; lblOldIMID.Visible = true; lblOldIMIDName.Visible = true;
            lblException.Text = "";
        }
        chkStd(txtSID.Text.ToString());
        if (lblException.Text == "")
        {
            txtIMID.Focus();
        }
    }
    protected void txtStudent_OnctextChaged(object sender, EventArgs e)
    {
        chkStd(txtStudent.Text.ToString());
    }
    private static string[] getstr;
    private void chkStd(string sid)
    {
        Student st = new Student(); 
        getstr = st.status(sid.ToString());
        if (getstr[0].ToString() == "Exception")
        {
            lblException.Text = "Enrolment ID Not Found" + getstr[0].ToString() + "" + getstr[1].ToString();
            txtSID.Text = "";
            lblStudentName.Visible = false;
            lblOldIMID.Visible = false;
            lblOldIMIDName.Visible = false; pnlChange.Visible = false;
            txtSID.Focus();
        }
        if (getstr[0].ToString() == "No")
        {
            lblException.Text = "Enrolment ID Not Found" + getstr[0].ToString() + "" + getstr[1].ToString();
            lblStudentName.Visible = false;
            lblOldIMID.Visible = false;
            lblOldIMIDName.Visible = false; pnlChange.Visible = false;
            txtSID.Text = "";
            txtSID.Focus();
        }
        if (getstr[0].ToString() != "No" & getstr[0].ToString() != "Exception")
        {
            if (sid == "")
            {
                lblExceptionApp.Text = "Please Enter Enrolment No.";
                lblExceptionApp.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                con.Close(); con.Open();
                SqlCommand cmdstd = new SqlCommand();
                cmdstd = new SqlCommand("select * from Student where SID='" + sid.ToString() + "'", con);
                SqlDataReader rdr;
                rdr = cmdstd.ExecuteReader();
                while (rdr.Read())
                {
                    lblStudentName.Text = rdr["Name"].ToString();
                    lblOldIMID.Text = rdr["IMID"].ToString();
                    pnlChange.Visible = true;
                }
                rdr.Close();
                cmdstd = new SqlCommand("select Name from IM where ID='" + lblOldIMID.Text.ToString() + "'", con);
                lblOldIMIDName.Text = Convert.ToString(cmdstd.ExecuteScalar());
                con.Close();
            }
        }
    }
    protected void btnChange_Onclick(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            con.Close(); con.Open();
            cmd=new SqlCommand("select * from Student where SID='" + txtSID.Text.ToString() + "' and Status='Active'",con);
            SqlDataReader rw;
            bool flag = false;
            rw = cmd.ExecuteReader();
            while (rw.Read())
            {
                flag = true;
            }
            rw.Close();
            if (flag == true)
            {
              //  cmd = new SqlCommand("update AppRecord set IMID=@IMID where Enrolment='" + txtSID.Text.ToString() + "'", con);
              //  cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
              //  cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update ExamCurrent set IMID=@IMID where SId='" + txtSID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update ExamForms set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update ITIForm set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Project set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("update Rechecking set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
                //cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                //cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update RecoverApp set IMID=@IMID where Enrolment='" + txtSID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("update SExamMarks set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
                //cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                //cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Student set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.ExecuteNonQuery();
                lblException.Text = "Student [" + txtSID.Text + "] IM Changed.";
                cmd = new SqlCommand("insert into Promotion (SID,Name,ChangeIn,OldValue,NewValue,Date,Session,Remark,IMID,NOCStatus) values (@SID,@Name,@ChangeIn,@OldValue,@NewValue,@Date,@Session,@Remark,@IMID,@NOCStatus)", con);
                cmd.Parameters.AddWithValue("@SID", txtSID.Text.ToString());
                cmd.Parameters.AddWithValue("@Name", lblStudentName.Text.ToString());
                cmd.Parameters.AddWithValue("@ChangeIn", "IMID");
                cmd.Parameters.AddWithValue("@OldValue", lblOldIMID.Text.ToString());
                cmd.Parameters.AddWithValue("@NewValue", txtIMID.Text.ToString());
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Session", lblSeasonHidden.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());
                cmd.Parameters.AddWithValue("@IMID", lblOldIMID.Text.ToString());
                if (chkNOC.Checked == true)
                    cmd.Parameters.AddWithValue("@NOCStatus", "YES");
                else
                    cmd.Parameters.AddWithValue("@NOCStatus", "NO");
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ddlType.Focus();
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue.ToString() == "All")
        {
            txtStudent.Visible = false;
            ddlViewType.Focus();
        }
        else if (ddlType.SelectedValue.ToString() == "Student")
        {
            txtStudent.Visible = true;
            txtStudent.Focus();
        }
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
        string qry="select * from Promotion where ChangeIn='"+ddlViewType.SelectedValue.ToString()+"' ORDER By Date DESC";
        SqlDataAdapter adp = new SqlDataAdapter(qry,con);
        DataTable at = new DataTable();
        adp.Fill(at);
        GridChange.DataSource = at;
        GridChange.DataBind();
    }
    protected void GridChange_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}