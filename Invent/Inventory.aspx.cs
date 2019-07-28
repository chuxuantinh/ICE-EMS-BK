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
using System.Data.SqlClient;
using System.Data;

public partial class Inventory : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
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
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
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
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("select ID from IM where ID='" + txtIMID.Text.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        int i = 0;
        if (chk == txtIMID.Text.ToString())
        {
            i += 1;
        }
        else
        {
            txtIMID.Text = "Invalid ID";
            lblException.Text = "Please Insert Valid IM ID.";
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
                lblGroupID.Text = "[" + reader["GID"].ToString() + "]";
            }
            reader.Close();
            btnChange.Focus();
        }
        con.Close();
    }
    protected void txtSID_OnTextChanged(object sendr, EventArgs e)
    {
        chkStd(txtSID.Text.ToString());
    }
    protected void txtStudent_OnctextChaged(object sender, EventArgs e)
    {
        chkStd(txtStudent.Text.ToString());
    }
    private static string[] getstr;
    private void chkStd(string sid)
    {
        Student st = new Student();
        // lblFeesType.Text = ""; lblFormType.Text = "";
        getstr = st.status(sid.ToString());
        if (getstr[0].ToString() == "Exception")
        {
            lblException.Text = "Enrolment ID Not Found" + getstr[0].ToString() + "" + getstr[1].ToString();

        }
        if (getstr[0].ToString() == "No")
        {
            lblException.Text = "Enrolment ID Not Found" + getstr[0].ToString() + "" + getstr[1].ToString();


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
                // Student st = new Student();
                //  getstr = st.status(txtEnrolID.Text.ToString());

                con.Close(); con.Open();
                SqlCommand cmdstd = new SqlCommand();
                cmdstd = new SqlCommand("select * from Student where SID='" + sid.ToString() + "'", con);
                SqlDataReader rdr;
                rdr = cmdstd.ExecuteReader();
                while (rdr.Read())
                {
                    //txtName.Text = rdr["Name"].ToString();
                    lblStudentName.Text = rdr["Name"].ToString();
                    lblOldIMID.Text = rdr["IMID"].ToString();

                }
                rdr.Close();
                cmdstd = new SqlCommand("select Name form IM where ID='" + lblOldIMID.Text.ToString() + "'", con);
                lblOldIMIDName.Text = Convert.ToString(cmdstd.ExecuteScalar());
            }
        }
    }
    protected void btnChange_Onclick(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            con.Close(); con.Open();

            cmd = new SqlCommand("update AppRecord set IMID=@IMID where Enrolment='" + txtSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update ExamCurrent set IMID=@IMID where SId='" + txtSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update ExamForm set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
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

            cmd = new SqlCommand("update Rechecking set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update RecoverApp set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update SExamMarks set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update Student set IMID=@IMID where SID='" + txtSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
            cmd.ExecuteNonQuery();

            lblException.Text = "Student [" + txtSID.Text + "] IM Changed.";

            cmd = new SqlCommand("insert into Promotion (SID,Name,ChangeIn,OldValue,NewValue,Date,Session,Remark) values (@SID,@Name,@ChangeIn,@OldValue,@NewValue,@Date,@Session,@Remark)", con);
            cmd.Parameters.AddWithValue("@SID", txtSID.Text.ToString());
            cmd.Parameters.AddWithValue("@Name", lblStudentName.Text.ToString());
            cmd.Parameters.AddWithValue("@ChangeIn", "IMID");
            cmd.Parameters.AddWithValue("@OldValue", lblOldIMID.Text.ToString());
            cmd.Parameters.AddWithValue("@NewValue", txtIMID.Text.ToString());
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")));
            cmd.Parameters.AddWithValue("@Session", lblSeasonHidden.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue.ToString() == "All")
        {
            txtStudent.Visible = false;
        }
        else if (ddlType.SelectedValue.ToString() == "Student")
        {
            txtStudent.Visible = true;
        }
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
    }

}
