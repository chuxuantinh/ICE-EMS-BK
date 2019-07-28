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

public partial class Exam_ApproveMarksheet : System.Web.UI.Page
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
                datastructure();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                ddlsession.Focus();
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
            Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtIMID.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        con.Close(); con.Open();
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
        SqlDataAdapter ad = new SqlDataAdapter("select distinct ef.SID, st.Name, st.IMID from ExamForm ef, Student st where ef.MarkStatus='Approved' and ef.ExamSession='"+lblSessionHiddend.Text.ToString()+"' and ef.Course='"+ddlCourse.SelectedValue.ToString()+"' and ef.Part='"+ddlPart.SelectedValue.ToString()+"' and ef.SID=st.SID", con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridSID.DataSource = ds;
        GridSID.DataBind();
        for (int i = 0; i < GridSID.Rows.Count; i++)
        {
            cmd = new SqlCommand("select CourseID from ExamCurrent where SID='" + GridSID.Rows[i].Cells[0].Text.ToString() + "'", con);
            string cid = Convert.ToString(cmd.ExecuteScalar());
            fillddl(cid,GridSID.Rows[i].Cells[0].Text.ToString());
            int k=0;
            for(int jj=0; jj<ddlSyllabus.Items.Count;jj++)
            {
                cmd = new SqlCommand("select SID from SExamMarks where SID='" + GridSID.Rows[i].Cells[0].Text.ToString() + "' and SubID='" + ddlSyllabus.Items[jj].Value.ToString() + "' and Status='Pass' and MarkStatus='Submitted'", con);
                string id = Convert.ToString(cmd.ExecuteScalar());

                if (id != "")
                {
                    k = k + 1;
                }
                if (ddlPart.SelectedValue.ToString() == "SectionB")
                {
                    if (k == 10) k = ddlSyllabus.Items.Count;
                }
            }
            if (k >= ddlSyllabus.Items.Count)
            {
               // cmd = new SqlCommand("update SExamMarks set MarkStatus='Approved' where SID='" + GridSID.Rows[i].Cells[0].Text.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "'", con);
               // cmd.ExecuteNonQuery();
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SID"] = GridSID.Rows[i].Cells[0].Text.ToString();
                drNewRow["Name"] = GridSID.Rows[i].Cells[1].Text.ToString();
                drNewRow["Stream"] = lblHiddendStream.Text.ToString();
                drNewRow["Course"] = ddlCourse.SelectedValue.ToString();
                drNewRow["Part"] = ddlPart.SelectedValue.ToString();
                drNewRow["IMID"] = GridSID.Rows[i].Cells[2].Text.ToString();
                dtDatas.Rows.Add(drNewRow);
               GridExamForms.DataSource = dtDatas;
               GridExamForms.DataBind();
            }
        }
        con.Close();
        con.Dispose();
        btnView.Focus();
    }
    private void fillddl(string cid,string sid)
    {
        string qry = "";
        Student st = new Student();
        string[] std = new string[5];
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            if(ddlPart.SelectedValue.ToString()=="PartI")
            qry = "select SubID from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "' and SubjectType='Regular'";
            else if (ddlPart.SelectedValue.ToString() == "PartII")
            {
                std = st.status(sid);
                if(std[0].ToString()=="Regular")
                    qry = "select SubID from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "' and SubjectType='Regular'";
                else
                    qry = "select SubID from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "'";
            }
            else if(ddlPart.SelectedValue.ToString()=="SectionA")
                qry = "select SubID from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "'";
            else if(ddlPart.SelectedValue.ToString()=="SectionB")
                qry = "select SubID from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "'";
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            if (ddlPart.SelectedValue.ToString() == "PartI")
                qry = "select SubID from CivilSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "' and SubjectType='Regular'";
            else if (ddlPart.SelectedValue.ToString() == "PartII")
            {
                std = st.status(sid);
                if (std[0].ToString() == "Regular")
                    qry = "select SubID from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "' and SubjectType='Regular'";
                else
                    qry = "select SubID from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "'";
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionA")
                qry = "select SubID from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "'";
            else if (ddlPart.SelectedValue.ToString() == "SectionB")
                qry = "select SubID from ArchiSubMaster where Section='" + ddlPart.SelectedValue.ToString() + "' and CourseID='" + cid.ToString() + "'";
        }
        SqlDataAdapter add = new SqlDataAdapter(qry,con);
        DataSet ds1 = new DataSet();
        add.Fill(ds1);
        ddlSyllabus.DataSource = ds1;
        ddlSyllabus.DataTextField = "SubID";
        ddlSyllabus.DataValueField = "SubID";
        ddlSyllabus.DataBind();
    }
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SID");
        dtDatas.Columns.Add("Name");
        dtDatas.Columns.Add("Stream");
        dtDatas.Columns.Add("Course");
        dtDatas.Columns.Add("Part");
        dtDatas.Columns.Add("IMID");
        ViewState["dtDatas"] = dtDatas;
    }
    protected void btnApprove_Onclick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        for (int i = 0; i < GridExamForms.Rows.Count; i++)
        {
         cmd = new SqlCommand("update SExamMarks set MarkStatus='Approved' where SID='" +GridExamForms.Rows[i].Cells[0].Text.ToString() + "' and Part='" +GridExamForms.Rows[i].Cells[4].Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update ExamCurrent set Stream=@Stream,Part=@Part where SID='" + GridExamForms.Rows[i].Cells[0].Text.ToString() + "'", con);
            string old = "", nw = "";
            if (GridExamForms.Rows[i].Cells[4].Text.ToString() == "PartI")
            {
                old = "PartI"; nw = "PartII";
                cmd.Parameters.AddWithValue("@Stream","Tech");
                cmd.Parameters.AddWithValue("@Part", "PartII");
            }
            else if (GridExamForms.Rows[i].Cells[4].Text.ToString() == "PartII")
            {
                old = "PartII"; nw = "SectionA";
                cmd.Parameters.AddWithValue("@Stream", "Asso");
                cmd.Parameters.AddWithValue("@Part", "SectionA");
            }
            else if (GridExamForms.Rows[i].Cells[4].Text.ToString() == "SectionA")
            {
                old = "SectionA"; nw = "SectionB";
                cmd.Parameters.AddWithValue("@Stream", "Asso");
                cmd.Parameters.AddWithValue("@Part", "SectionB");
            }
            else if (GridExamForms.Rows[i].Cells[4].Text.ToString() == "SectionB")
            {
                old = "SectionB"; nw = "SectionB";
                cmd.Parameters.AddWithValue("@Stream", "Asso");
                cmd.Parameters.AddWithValue("@Part", "SectionB");
            }
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into Promotion (SID,Name,ChangeIn,OldValue,NewValue,Date,Session,Remark) values (@SID,@Name,@ChangeIn,@OldValue,@NewValue,@Date,@Session,@Remark)", con);
            cmd.Parameters.AddWithValue("@SID", GridExamForms.Rows[i].Cells[0].Text.ToString());
            cmd.Parameters.AddWithValue("@Name", GridExamForms.Rows[i].Cells[1].Text.ToString());
            cmd.Parameters.AddWithValue("@ChangeIn", "Course");
            cmd.Parameters.AddWithValue("@OldValue", old.ToString());
            cmd.Parameters.AddWithValue("@NewValue", nw.ToString());
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")));
            cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", "Passed Student Promotion");
            cmd.ExecuteNonQuery();
        }
        con.Close();
        con.Dispose();
        btnView.Focus();
    }
    protected void txtIMID_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + txtIMID.Text.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        int i = 0;
        if (chk == txtIMID.Text.ToString())
        {
            i += 1;
        }
        else
        {
            txtIMID.Text = "Invalid ID";
            lblExceptionOK.Text = "Please Insert Valid IM ID.";
        }
        if (i == 1)
        {
            lblExceptionOK.Text = "";
            cmd = new SqlCommand("select * from IM where ID='" + txtIMID.Text + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                lblIMAddress.Text = reader[3].ToString();
                lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                lblEnrolment.Text = txtIMID.Text;
                lblGroupID.Text = reader["GID"].ToString();
            }
            reader.Close();
            reader.Dispose();
        }
        con.Close();
        con.Dispose();
        btnView.Focus();
    }
    protected void ddlPart_OnselectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPart.SelectedValue.ToString() == "PartI")
        {
            lblPart.Text = "Part I";
            lblHiddendStream.Text = "Tech";
            lblStreamDDL.Text = "Technical Examination.";
        }
        else if (ddlPart.SelectedValue.ToString() == "PartII")
        {
            lblHiddendStream.Text = "Tech";
            lblStreamDDL.Text = "Technical Examination.";
            lblPart.Text = "Part II";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionA")
        {
            lblHiddendStream.Text = "Asso";
            lblStreamDDL.Text = "Associate Examination";
            lblPart.Text = "Section A";
        }
        else if (ddlPart.SelectedValue.ToString() == "SectionB")
        {
            lblHiddendStream.Text = "Asso";
            lblStreamDDL.Text = "Associate Examination";
            lblPart.Text = "Section B";
        }
        ddlPart.Focus();
    }
}
