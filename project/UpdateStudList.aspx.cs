using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.IO;
using System.Data.OleDb;

public partial class project_UpdateStudList : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    SqlDataAdapter adp;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) { ddlsession.SelectedValue = "Sum"; ddlSeason.SelectedValue = "Sum"; } else { ddlSeason.SelectedValue = "Win"; ddlsession.SelectedValue = "Win"; }
                    txtSession.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    txtyr.Text = txtSession.Text; txtyer.Text = txtSession.Text; rbtnPartII.Checked = true;
                    ddlSeason.Focus();
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        bindGrid();
        lblStat.Text = "FinalPass";
    }
     string qry, qry1 = "";
    private void bindGrid()
     {
         if (rbtnPartII.Checked)
         {
                      if(ddlCourse.SelectedValue=="Civil")
                     {
                         qry = "select distinct SID from  SExamMarks where  Part='PartII' and Course='Civil' and Status='Pass'  and SID in (select SID from Student where Part!='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status)>=9) ";
                         qry1 = "select distinct SID from SExamMarks where  Part='PartII' and Course='Civil' and Status='Pass' and SID in (select SID from Student where Part='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status)>=11)  ";
             
                     }
                     else if(ddlCourse.SelectedValue=="Architecture")
                     {
                         qry = "select distinct SID from SExamMarks where  Part='PartII' and Course='Architecture' and Status='Pass' and SID in (select SID from Student where Part!='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status)>=10)  ";
                         qry1 = "select  distinct SID from SExamMarks where Part='PartII' and Course='Architecture' and Status='Pass'  and SID in (select SID from Student where Part='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status)>=12) ";
                     }
         }
         else if(rbtnSectionB.Checked)
         {
             qry = "select distinct SID from SExamMarks where  Part='SectionB' and Course='" + ddlCourse.SelectedValue + "' and Status='Pass' and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "')  group by SID having (Count(Status)>=10)";
             qry1 = "select distinct SID from SExamMarks where Part='SectionB' and Course='" + ddlCourse.SelectedValue + "' and Status='Pass' and  SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status)>=10)";

         }
        SqlDataAdapter adp = new SqlDataAdapter(qry, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(qry1, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        adp1.Fill(dt);
        GridShow.DataSource = dt;
        GridShow.DataBind();
    }
    private void bindGridPromoted()
    {
        if (rbtnPartII.Checked)
        {
            if (ddlCourse.SelectedValue == "Civil")
            {
                qry = "select distinct SID from  SExamMarks where  Part='PartII' and Course='Civil' and Status='Pass'  and SID in (select SID from Student where Part!='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status) between 7 and 8) ";
                qry1 = "select distinct SID from SExamMarks where  Part='PartII' and Course='Civil' and Status='Pass' and SID in (select SID from Student where Part='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status) between 9 and 10)  ";
            }
            else if (ddlCourse.SelectedValue == "Architecture")
            {
                qry = "select distinct SID from SExamMarks where  Part='PartII' and Course='Architecture' and Status='Pass' and SID in (select SID from Student where Part!='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status) between 8 and 9)  ";
                qry1 = "select  distinct SID from SExamMarks where Part='PartII' and Course='Architecture' and Status='Pass'  and SID in (select SID from Student where Part='PartII') and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status) between 10 and 11) ";
            }
        }
        else if (rbtnSectionB.Checked)
        {
            qry = "select distinct SID from SExamMarks where  Part='SectionB' and Course='" + ddlCourse.SelectedValue + "' and Status='Pass' and SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "')  group by SID having (Count(Status) between 8 and 9)";
            qry1 = "select distinct SID from SExamMarks where Part='SectionB' and Course='" + ddlCourse.SelectedValue + "' and Status='Pass' and  SID not in (select SID from Project) and SID in (select distinct(SID) from SExamMarks where ExamSeason='" + lblSessionHiddend.Text + "') group by SID having (Count(Status) between 8 and 9)";
        }
        SqlDataAdapter adp = new SqlDataAdapter(qry, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(qry1, con);
        DataTable dt = new DataTable();
        adp.Fill(dt); adp1.Fill(dt);
        GridShow.DataSource = dt;
        GridShow.DataBind();
    }
    protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void GridShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        lblCount.Text = GridShow.Rows.Count.ToString(); lblList.Text = "Students Final pass List:";
    }
    protected void btnPromotted_Click(object sender, EventArgs e)
    {
        bindGridPromoted(); lblStat.Text = "Promotted"; lblList.Text = "promotted Student List:";
    }
    protected void btnUpdateProject_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        int i=0;
        SqlDataReader rd = null;
        try
        {
            while (i < GridShow.Rows.Count)
            {
                CheckBox rbApp = (CheckBox)GridShow.Rows[i].FindControl("chkapp");
                if (rbApp.Checked == true)
                {
                    cmd = new SqlCommand("select SName,IMID,Course,Part from ExamCurrent where SID='" + GridShow.Rows[i].Cells[1].Text + "'", con);
                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        lblsName.Text = rd["SName"].ToString();
                        lblIM.Text = rd["IMID"].ToString();
                        lblCouse.Text = rd["Course"].ToString();
                        lblParts.Text = rd["Part"].ToString();
                    }
                    rd.Close();
                    cmd = new SqlCommand("insert into Project(SID,StudentName,IMID,Course,Part,Status,Session,CourseStatus,SynopsisStatus) values(@SID,@Name,@IMID,@Course,@Part,@Status,@Session,@CourseStatus,@SynopsisStatus)", con);
                    cmd.Parameters.AddWithValue("@SID", GridShow.Rows[i].Cells[1].Text);
                    cmd.Parameters.AddWithValue("@Name", lblsName.Text);
                    cmd.Parameters.AddWithValue("@IMID", lblIM.Text);
                    cmd.Parameters.AddWithValue("@Course", lblCouse.Text);
                    cmd.Parameters.AddWithValue("@Part", lblParts.Text);
                    cmd.Parameters.AddWithValue("@Status", "Selected");
                    cmd.Parameters.AddWithValue("@Session", ddlsesion.SelectedValue.ToString()+txtyer.Text);
                    cmd.Parameters.AddWithValue("@CourseStatus", lblStat.Text);
                    cmd.Parameters.AddWithValue("@SynopsisStatus", "NotSubmitted");
                    cmd.ExecuteNonQuery();
                }
                i++;
            }
            rd.Dispose();
            GridShow.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Updated')", true);
        }
        catch (NullReferenceException ex)
        {
        }

    con.Close(); con.Dispose();
    }
    protected void txtID_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        //cmd = new SqlCommand("select SID from Project where SID='" + txtID.Text + "' and Session='" + (ddlSeason.SelectedValue.ToString() + txtyr.Text )+ "'", con);
        //string sid = Convert.ToString(cmd.ExecuteScalar());
        //if(sid!="")
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('This membership already added')", true);
        //else if (sid == "")
        //{
        cmd = new SqlCommand("select SID from ExamCurrent where SID='" + txtID.Text + "'", con);
        string strSid = Convert.ToString(cmd.ExecuteScalar());
        if (strSid == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('["+txtID.Text+"] Not Found Please try another one!')", true);
            txtID.Text = ""; lblName.Text = ""; lblIMID.Text = ""; lblCourse.Text = ""; lblpart.Text = ""; txtID.Focus();
        }
        else
        {
            cmd = new SqlCommand("select SName,IMID,Course,Part from ExamCurrent where SID='" + txtID.Text + "'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                lblName.Text = rd["SName"].ToString();
                lblIMID.Text = rd["IMID"].ToString();
                lblCourse.Text = rd["Course"].ToString();
                lblpart.Text = rd["Part"].ToString();
            }
            rd.Close(); rd.Dispose();
        }
        //}
        con.Close(); con.Dispose(); ddlSeason.Focus();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        if (txtID.Text != "")
        {
            cmd = new SqlCommand("update Project Set EntryStatus='OldProject' where SID='" + txtID.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into Project(SID,StudentName,IMID,Course,Part,Status,Session,CourseStatus,SynopsisStatus,EntryStatus) values(@SID,@Name,@IMID,@Course,@Part,@Status,@Session,@CourseStatus,@SynopsisStatus,@EntryStatus)", con);
            cmd.Parameters.AddWithValue("@SID", txtID.Text);
            cmd.Parameters.AddWithValue("@Name", lblName.Text);
            cmd.Parameters.AddWithValue("@IMID", lblIMID.Text);
            cmd.Parameters.AddWithValue("@Course", ddlCrse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Part", ddlPart.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Status", "Selected");
            cmd.Parameters.AddWithValue("@Session", ddlSeason.SelectedValue.ToString() + txtyr.Text);
            cmd.Parameters.AddWithValue("@CourseStatus", ddlStatus.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SynopsisStatus", "NotSubmitted");
            cmd.Parameters.AddWithValue("@EntryStatus", "Running");
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('[" + txtID.Text + "] Successfully added!')", true);
            txtID.Text = ""; lblName.Text = ""; lblIMID.Text = ""; lblCourse.Text = ""; lblpart.Text = ""; txtID.Focus();
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Check Membership')", true);
        con.Close(); con.Dispose();
    }

    protected void lbtnUpload_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (fileupload.HasFile)
            {
                int length = fileupload.PostedFile.ContentLength;

                byte[] imagefile = new byte[length];
                // Store the current file in memory.
                HttpPostedFile htpfile = fileupload.PostedFile;
                // Read the stream of file.
                htpfile.InputStream.Read(imagefile, 0, length);
                // Get file name.
                string filename = fileupload.PostedFile.FileName.ToString();
                // Get Extension of file.
                string exen = Path.GetExtension(filename);

                if (exen == ".xls" || exen == ".xlsx")
                {
                    filename = "ProjectDate" + exen;
                    // Save file in Project folder.
                    fileupload.SaveAs(Server.MapPath("~/Project/" + filename));
                    string path1 = (Server.MapPath("~/Project/" + filename));

                    string source = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path1 + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'";

                    OleDbConnection olcon = new OleDbConnection(source);
                    olcon.Open();

                    string query = "select * from [Sheet1$]";
                    OleDbCommand olcmd = new OleDbCommand(query, olcon);
                    OleDbDataReader dr = olcmd.ExecuteReader();
                    con.Close(); con.Open();
                    while (dr.Read())
                    {
                        string error = "";
                        cmd = new SqlCommand("update Project Set EntryStatus='OldProject' where SID='" + dr["SID"].ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("insert into Project(SID,StudentName,IMID,Course,Part,Session,CourseStatus) values(@SID,@Name,@IMID,@Course,@Part,@Session,@CourseStatus)", con);
                        cmd.Parameters.AddWithValue("@SID", dr["SID"]);
                        cmd.Parameters.AddWithValue("@Name", dr["Name"]);
                        cmd.Parameters.AddWithValue("@IMID", dr["IMID"]);
                        cmd.Parameters.AddWithValue("@Course", dr["Course"]);
                        cmd.Parameters.AddWithValue("@Part", dr["Part"]);
                        cmd.Parameters.AddWithValue("@Session", dr["Session"]);
                        cmd.Parameters.AddWithValue("@CourseStatus", dr["CourseStatus"]);
                        cmd.ExecuteNonQuery();
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Uploaded.')", true);
                }
            }
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('"+ex.ToString()+"')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
}