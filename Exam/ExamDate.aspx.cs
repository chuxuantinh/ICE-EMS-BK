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

public partial class Exam_ExamDate : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                ddlCourse.SelectedValue = "Civil";
                ddlPart.SelectedValue = "PartI";
                lblSeasonHidden.Text = DropDownList1.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                btnNext.Visible = false;
                showcourse(); panelView.Visible = false; lblSearchLabel.Text = "Enter Roll No"; rbtnRollNo.Checked = true;
                lbtnGoTo.Text = "Go To Rechecking Panel >>>"; panelREchecking.Visible = false; panelInnerRechecking.Visible = false;
                DropDownList1.Focus();
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
    protected void GridExamCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        centerCapacity();
    }
    private void centerCapacity()
    {
        GridViewRow gr;
        gr = GridView2.SelectedRow;
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        lblCenterCode.Text = gr.Cells[1].Text.ToString();
        lblCapacity.Text = gr.Cells[6].Text.ToString();
        con.Close();
        con.Open(); SqlCommand cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" +lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        con.Close(); con.Dispose();
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        {
            lblCapacity.Text = sum.ToString();
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
    protected void btnSAVE_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            string time = atime(txtEnrolment.Text.ToString(), ddlsubcode.SelectedValue.ToString(),  txtIMID.Text.ToString(), ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString());
            chkduplicate();
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("insert into SExamMarks(SID,SubID,SubName,GetMarks,Status,ExamSeason,ATime,IMID,Stream,Course,Part,RollNo,Center) values(@SID,@SubID,@SubName,@GetMarks,@Status,@ExamSeason,@ATime,@IMID,@Stream,@Course,@Part,@RollNo,@Center)", con);
            cmd.Parameters.AddWithValue("@SID", txtEnrolment.Text.ToString());
            cmd.Parameters.AddWithValue("@SubID", ddlsubcode.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SubName", lblSubNamess.Text.ToString());
            cmd.Parameters.AddWithValue("@GetMarks", txtObtainMarks.Text.ToString());
            cmd.Parameters.AddWithValue("@Status", lblStatus.Text.ToString());
            cmd.Parameters.AddWithValue("@ExamSeason", lblSeasonHidden.Text.ToString());
            cmd.Parameters.AddWithValue("@ATime", time.ToString());
            cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
            cmd.Parameters.AddWithValue("@Stream", lblStreamCode.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", ddlCourse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("RollNo", lblRollNoSelected.Text.ToString());
            cmd.Parameters.AddWithValue("@Center", lblCenter.Text.ToString());
            cmd.ExecuteNonQuery();
            lblException.Text = "Roll NO.:"+lblRollNoSelected.Text.ToString()+" Marks Saved.";
            txtObtainMarks.Text = "";
            btnSave.Enabled = false;
            btnNext.Visible = true;
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    string atme;
    private string atime(string enrol, string subcode, string imid, string course, string part)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select max(ATime) from SExamMarks where SID='" + enrol.ToString() + "' and SubID='" + subcode.ToString() + "' and Status='Fail'  and IMID='" + imid.ToString() + "' and Course='" + course.ToString() + "' and Part='" + part.ToString() + "'", con);
        string tm = Convert.ToString(cmd.ExecuteScalar());
        if (tm == "")
        {
            tm = "0";
        }
        int im = Convert.ToInt32(tm) + 1;
        con.Close();
        return atme = im.ToString();
    }

    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = DropDownList1.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();

    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = DropDownList1.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        btnNext.Visible = false;

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());

    }
   
    string stcmss;
    protected void ddlSubCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
        if (ddlCourse.SelectedValue.ToString()== "Civil")
        {
            stcmss = "select * from CivilSubMaster where SubID='" + ddlsubcode.SelectedValue.ToString() +"'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            stcmss = "select * from ArchiSubMaster where SubID='" + ddlsubcode.SelectedValue.ToString() + "'";
        }
        SqlCommand cms = new SqlCommand(stcmss, con);

        SqlDataReader rd;
        rd = cms.ExecuteReader();
        while (rd.Read())
        {
            lblSubNamess.Text = rd["SubName"].ToString();
            lblMinMarsk.Text = rd["MinMark"].ToString();
            lblToMarks.Text = rd["MaxMark"].ToString();
            lblFirstMarks.Text = rd["First"].ToString();

        }
        rd.Close();
        rd.Dispose();
        con.Close();
        con.Dispose();
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        showcourse();
        if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
        {
            lblStreamCode.Text = "Tech";
            lblStreamName.Text = "Technician Engineering";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
        {
            lblStreamName.Text = "Associate Engineering";
            lblStreamCode.Text = "Asso";
        }
    }
    protected void ddlCourse_SeelctedIndexchanged(object sender, EventArgs e)
    {
        showcourse();
    }
    string cmd;
    private void showcourse()
    {
        try
        {
            if (ddlPart.SelectedValue.ToString() == "PartI" | ddlPart.SelectedValue.ToString() == "PartII")
            {
                lblStreamCode.Text = "Tech";
                lblStreamName.Text = "Technician Engineering";
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionA" | ddlPart.SelectedValue.ToString() == "SectionB")
            {
                lblStreamName.Text = "Associate Engineering";
                lblStreamCode.Text = "Asso";
            }
            if (ddlCourse.SelectedValue.ToString() == "Civil")
            {
                cmd = "select * from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "'";
            }
            else if (ddlCourse.SelectedValue.ToString() == "Architecture")
            {
                cmd = "select * from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "'";
            }
            SqlDataAdapter ad = new SqlDataAdapter(cmd, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            ddlsubcode.DataSource = dt;
            ddlsubcode.DataValueField = "SubID";
            ddlsubcode.DataTextField = "SubID";
            ddlsubcode.DataBind();

        }
        catch (SqlException ex)
        {

        }
        finally
        {
        }
    }
    protected void txtOMarks_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = DropDownList1.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        if (lblMinMarsk.Text == "") { lblException.Text = "Please select Subject Code"; lblException.ForeColor = System.Drawing.Color.Red;
        lblException.Font.Bold = true;
        }
        else
        {
            lblException.Text = "";
            if (Convert.ToInt32(lblMinMarsk.Text) > Convert.ToInt32(txtObtainMarks.Text))
            {
                lblStatus.Text = "Fail";
            }
            else if (Convert.ToInt32(lblMinMarsk.Text) <= Convert.ToInt32(txtObtainMarks.Text))
            {
                lblStatus.Text = "Pass";
            }
        }

    }
    protected void btnCenterCode_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        lblSeasonHidden.Text = DropDownList1.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        SqlCommand cmd = new SqlCommand("select * from  ExamCenter where ID='" + Convert.ToInt32(txtExamCode.Text) + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if(reader.Read())
        {
                    lblCenterCode.Text = reader["ID"].ToString();
                    lblCenteNaem.Text = reader["Name"].ToString();
                    lblCenter.Text = reader["City"].ToString();
                    lblExceptionCode.Text = "";
        }
        else
        {
                    lblExceptionCode.Text = "Invalid Exam Center Code";
        }
        reader.Close();
        reader.Dispose();
        SqlCommand cmde = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmde.ExecuteScalar());
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        {
            lblCapacity.Text = sum.ToString();
        }           
       
        con.Close();
        con.Dispose();
    }
    protected void btnShowEnrolment_Onclick(object sender, EventArgs e)
    {
        try
        {
            lblSeasonHidden.Text = DropDownList1.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
          SqlDataAdapter ad=new SqlDataAdapter("select SID,SubID,SubName,MaxMarks,MinMarks,Status,First,IMID,SubType,RollNo from ExamForm where SubID='" + ddlsubcode.SelectedValue.ToString() + "' and ExamSession='" + lblSeasonHidden.Text.ToString() + "' and Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and CenterCode='" + lblCenterCode.Text.ToString() + "'", con);
          DataTable dt = new DataTable();
          ad.Fill(dt);
          GridView1.DataSource = dt;
          GridView1.DataBind();

        }
        catch (SqlException ex)
        {

        }
        finally
        {
        }
    }
    protected void GridExamForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = DropDownList1.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        GridViewRow gr;
        gr = GridView1.SelectedRow;
        txtEnrolment.Text = gr.Cells[1].Text.ToString();
        txtIMID.Text = gr.Cells[8].Text.ToString();
        lblRollNoSelected.Text = gr.Cells[10].Text.ToString();
        lblExStatus.Text = gr.Cells[6].Text.ToString();
        chkduplicate();
        chkUFM();
        con.Dispose();
    }
    private string chkduplicate()
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select SubID from SExamMarks where SID='" + txtEnrolment.Text.ToString() + "' and  SubID='" + ddlsubcode.SelectedValue.ToString() + "' and ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and IMID='" + txtIMID.Text.ToString() + "' and Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and RollNo='" + lblRollNoSelected.Text.ToString() + "'", con);
        string value = Convert.ToString(cmd.ExecuteScalar());
        if (value == "")
        {
            value = "NO";
        }
        else if (value == ddlsubcode.SelectedValue.ToString())
        {
            value = "YES";
        }
        con.Close();
        return value;

    }
    protected void GridView2_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }
    private void chkUFM()
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("select RollNo from ExamUFM where SID='" + txtEnrolment.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and SubID='" + ddlsubcode.SelectedValue.ToString() + "' and CenterCode='" + lblCenterCode.Text.ToString() + "'", con);
            string strRollNo = Convert.ToString(cmd.ExecuteScalar());
            if (strRollNo.ToString() == "")
            {
                panelView.Visible = false;
                txtObtainMarks.Text = "";
                txtObtainMarks.Enabled = true;
                lblStatus.Text = "";
            }
            else
            {
                panelView.Visible = true;
                SqlCommand cmd1=new SqlCommand("select * from ExamUFM where SID='"+ txtEnrolment.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "' and SubID='" + ddlsubcode.SelectedValue.ToString() + "' and CenterCode='" + lblCenterCode.Text.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    lblExamDate.Text = reader["ExamDate"].ToString();
                    lblShift.Text = reader["Shift"].ToString();
                    string status = reader["Status"].ToString();
                    if (status.ToString().ToLower() == "unfair")
                    {
                        btnunfair.Visible = true;
                        btnfair.Visible = false;
                        txtObtainMarks.Enabled = false;
                        lblStatus.Text = "UFM";
                        txtObtainMarks.Text = "0";
                    }
                    else if (status.ToString().ToLower() == "fair")
                    {
                        btnfair.Visible = true;
                        btnunfair.Visible = false;
                        txtObtainMarks.Text = "";
                        txtObtainMarks.Enabled = true;
                        lblStatus.Text = "";

                    }
                }
                reader.Close();
                reader.Dispose();
            }
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

    // Part Tow Marks Feeding.................//
    protected void btnOK_Click(object sender, EventArgs e)
    {
        GridRechecking.DataSource = gridsouce();
        GridRechecking.DataBind();
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void GridUFM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridRechecking.PageIndex = e.NewPageIndex;
        GridRechecking.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;
        rw = GridRechecking.SelectedRow;
        panelInnerRechecking.Visible = true;
        lblMembershipNoRechecking.Text = rw.Cells[1].Text.ToString();
        lblSubjectNoRechecking.Text = rw.Cells[2].Text.ToString();
        lblSubNameRechecking.Text = rw.Cells[3].Text.ToString();
        lblOldMarksRechecking.Text = rw.Cells[4].Text.ToString();
    }
    string qury;
     private DataSet gridsouce()
    {

        if (rbtnRollNo.Checked == true)
        {
            qury = "select SID,SubID,SubName,OldMarks,NewMarks,Remarks,Status from Rechecking where RollNo='" + txtSearchBox.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
        }
        else if (rbtnCenterCode.Checked == true)
        {
          //  qury = "select RollNo,SubID,SubName,OldMarks,NewMarks,Remarks,Status from Rechecking where RollNo=(select RollNo from ExamFroms where CenterCode='"+txtSearchBox.Text.ToString()+"') and Session='" + lblSeasonHidden.Text.ToString() + "'";
        }
        else if (rbtnIMID.Checked == true)
        {
            qury = "select RollNo,SID,SubID,SubName,OldMarks,NewMarks,Remarks,Status from Rechecking where IMID='" + txtSearchBox.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
        }
        else if (rbtnAll.Checked == true)
        {
            qury = "select SID,SubID,SubName,OldMarks,NewMarks,Remarks,Status from Rechecking where Session='" + lblSeasonHidden.Text.ToString() + "'";
        }
        else
        {
            qury = "select SID,SubID,SubName,OldMarks,NewMarks,Remarks,Status from Rechecking where Session='" + lblSeasonHidden.Text.ToString() + "'";
        }
        if (txtSearchBox.Text == "")
        {
            qury = "select SID,SubID,SubName,OldMarks,NewMarks,Remarks,Status from Rechecking where Session='" + lblSeasonHidden.Text.ToString() + "'";
        }

        SqlDataAdapter ad = new SqlDataAdapter(qury, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        return ds;
    }

     protected void rbtnRollNo_CheckedChanged(object sender, EventArgs e)
     {
         lblSearchLabel.Text = "Enter Roll No"; txtSearchBox.Enabled = true;
     }
     protected void rbtnCenterCode_CheckedChanged(object sender, EventArgs e)
     {
         lblSearchLabel.Text = "Examination Center Code:"; txtSearchBox.Enabled = true;
     }
     protected void rbtnIMID_CheckedChanged(object sender, EventArgs e)
     {
         lblSearchLabel.Text = "Enter IMID:"; txtSearchBox.Enabled = true;
     }
     protected void rbtnAll_CheckedChanged(object sender, EventArgs e)
     {
         lblSearchLabel.Text = "";
         txtSearchBox.Enabled = false;
     }
     protected void rbtnMembershipNo_CheckedChanged(object sender, EventArgs e)
     {
         lblSearchLabel.Text = "Membership No:";
         txtSearchBox.Enabled = true;
     }

     protected void lbtnGOTO_Onclick(object sender, EventArgs e)
     {
         if (panelREchecking.Visible == true)
         {
             panelMarks.Visible = true;
             panelREchecking.Visible = false;
             lbtnGoTo.Text = "Go To Rechecking Panel >>>";
         }
         else if (panelMarks.Visible == true)
         {
             panelMarks.Visible = false;
             panelREchecking.Visible = true;
             lbtnGoTo.Text = "Go To Marks Panel >>>";
         }
     }
     protected void btnCencel_ONClickRechecking(object sender, EventArgs e)
     {
         panelInnerRechecking.Visible = false;
     }
     protected void btnSave_OnClick(object sender, EventArgs e)
     {
         try
         {
             con.Close(); con.Open();
             SqlCommand cmd = new SqlCommand("update SExamMarks set GetMarks=@Marks where ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and SID='" + lblMembershipNoRechecking.Text.ToString() + "' and SubID='" + lblSubjectNoRechecking.Text.ToString() + "'", con);
             cmd.Parameters.AddWithValue("@Marks", txtNewMarks.Text.ToString());
             cmd.ExecuteNonQuery();
             lblExceptionRechecking.Text = "Marks Updated Successfully !";
         }
         catch (SqlException ex)
         {
             lblExceptionRechecking.Text = ex.ToString();
         }
         finally
         {
             con.Close();
             con.Dispose();
         }
     }
}
