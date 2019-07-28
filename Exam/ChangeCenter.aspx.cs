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

public partial class Exam_ChangeCenter : System.Web.UI.Page
{
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
            if (!IsPostBack)
            {
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                    ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win"; 
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                ddlExamSeason.Focus();
                fillapps();
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
    private void fillapps()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select Name,FName,Enrolment,IMID,Stream,Course,Part,DOB,DNo,SubDate,Amount,LateFee,AppNo from AppRecord where FormType='ChangeCenter' and Status='no'", con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridChangeApps.DataSource = ds;
        GridChangeApps.DataBind();
        for (int i = 0; i < GridChangeApps.Rows.Count; i++)
        {
            GridChangeApps.Rows[i].Cells[8].Text = Convert.ToDateTime(GridChangeApps.Rows[i].Cells[8].Text).ToString("dd/MM/yyyy");
            GridChangeApps.Rows[i].Cells[10].Text = Convert.ToDateTime(GridChangeApps.Rows[i].Cells[10].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void txtSN_OnTextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("select Status from AppRecord where AppNo='" + txtSN.Text.ToString() + "' and FormType='ChangeCenter' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sts = Convert.ToString(cmd.ExecuteScalar());
        if (sts == "")
        {
            lblExceptionSN.Text = "Application No Not Found.";
        }
        else if (sts == "NotApproved")
        {
            lblExceptionSN.Text = "Application Form Currentlt Not Approved.";
        }
        else if (sts == "no")
        {
            lblExceptionSN.Text = "";
            cmd = new SqlCommand("select * from AppRecord where AppNo='" + txtSN.Text.ToString() + "' and FormType='ChangeCenter' and Session='" + lblSeasonHidden.Text.ToString() + "'  and  Status='no'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lbSName.Text = reader["Name"].ToString();
                lblFName.Text = reader["FName"].ToString();
                lblEnrolment.Text = reader["Enrolment"].ToString();
                lblIMID.Text = reader["IMID"].ToString();
                lblStream.Text = reader["Stream"].ToString();
                lblCourse.Text = reader["Course"].ToString();
                lblPart.Text = reader["Part"].ToString();
            }
            reader.Close();
            reader.Dispose();
        }
        con.Close();
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridView1.SelectedRow;
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        lblCenterCode.Text = gr.Cells[1].Text.ToString();
        con.Close();
        con.Open(); SqlCommand cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        con.Dispose();
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        {
            lblCapacity.Text = sum.ToString();
        }
    }
    protected void btnGenerateRollNo_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select max(RSN) from ExamForms where Course='" + lblCourse.Text.ToString() + "' and Part='" +lblPart.Text.ToString() + "' and ExamSeason='" +lblSeasonHidden.Text.ToString() + "'", con);
        string rono = Convert.ToString(cmd.ExecuteScalar().ToString());
        if (rono.ToString() == "")
        {
           if (lblCourse.Text == "Civil")
                {
                    if (lblPart.Text == "PartI")
                    {
                        rono =  "1001";
                    }
                    else if (lblPart.Text == "PartII")
                    {
                        rono =  "2001";
                    }
                    else if (lblPart.Text== "SectionA")
                    {
                        rono ="3001";
                    }
                    else if (lblPart.Text == "SectionB")
                    {
                        rono = "4001";
                    }
                }
                else if (lblCourse.Text == "Architecture")
                {
                    if (lblPart.Text == "PartI")
                    {
                        rono = "5001";
                    }
                    else if (lblPart.Text == "PartII")
                    {
                        rono = "6001";
                    }
                    else if (lblPart.Text == "SectionA")
                    {
                        rono =  "7001";
                    }
                    else if (lblPart.Text == "SectionB")
                    {
                        rono = "8001";
                    }
                }
                rono = lblCenterCode.Text.ToString() + "" + rono.ToString();
            }
            else
            {
                int rn = Convert.ToInt32(rono);
                rn = rn + 1;
                rono = rn.ToString();
            }
            cmd=new SqlCommand("update ExamForms set CenterCode=@CenterCode,CenterName=@CenterName,RollNo=@RollNo, RSN=@RSN where SID='" +lblEnrolment.Text.ToString() + "' and ExamSeason='" +lblSeasonHidden.Text.ToString() + "' and IMID='" +lblIMID.Text.ToString()+ "' and SID='" +lblSID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@CenterCode", lblCenterCode.Text.ToString());
            cmd.Parameters.AddWithValue("@CenterName", lblCenteNaem.Text.ToString());
            cmd.Parameters.AddWithValue("@RollNo", rono.ToString());
            cmd.Parameters.AddWithValue("@RSN", rono.ToString());
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("update ExamForm set CenterCode=@CenterCode,CenterName=@CenterName,RollNo=@RollNo,RollStatus=@RollStatus,CenterAddress=@CenterAddress,PinCode=@PinCode where SID='" +lblSID.Text.ToString()+ "' and ExamSession='" +lblSeasonHidden.Text.ToString() + "' and IMID='" +lblIMID.Text.ToString().ToString() + "'", con);
            cmd2.Parameters.AddWithValue("@CenterCode", lblCenterCode.Text.ToString());
            cmd2.Parameters.AddWithValue("@CenterName", lblCenteNaem.Text.ToString());
            cmd2.Parameters.AddWithValue("@RollNo", rono.ToString());
            cmd2.Parameters.AddWithValue("@RollStatus", "Submitted");
            cmd2.Parameters.AddWithValue("@CenterAddress", lblCenterAddress.Text.ToString() + "," + lblCenterAddress2.Text.ToString());
            cmd2.Parameters.AddWithValue("@PinCode", lblPinCode.Text.ToString());
            cmd2.ExecuteNonQuery();
        cmd=new SqlCommand("Insert into Promotion (SID, Name, ChangeIn, OldValue, NewValue, Date, Session,Remark) values(@SID,@Name,@ChangeIn,@OldValue,@NewValue,@Date,@Session,@Remark)",con);
        cmd.Parameters.AddWithValue("@SID",lblEnrolment.Text.ToString());
        cmd.Parameters.AddWithValue("@Name",lbSName.Text.ToString());
        cmd.Parameters.AddWithValue("@ChangeIn","ChangeCenter");
        cmd.Parameters.AddWithValue("@OldValue","");
        cmd.Parameters.AddWithValue("@NewValue","");
        cmd.Parameters.AddWithValue("@Date","");
        cmd.Parameters.AddWithValue("@Remark","");
        cmd.ExecuteNonQuery();
        GridView3.DataSource = GetDataSource();
        GridView3.DataBind();
        con.Close();
        con.Dispose();
    }
    string cmqry;
    private DataSet GetDataSource()
    {
        //if (rbtnViewFromExamCenter.Checked == true)
        //{
        cmqry = "select SID,ExamSeason,IMID,CenterCode,CenterName,RollNo,City from ExamForms where CenterCode='" + lblCenterCode.Text.ToString() + "' and RollNo!='N/A' and ExamSeason='" + lblSeasonHidden.Text.ToString() + "'";
        //}
        //else if (rbntViewAll.Checked == true)
        //{
        //    cmqry = "select SID,ExamSeason,IMID,CenterCode,CenterName,RollNo,City from ExamForms where ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and RollNo!='N/A' order by RollNo";
        //}
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);

        //SqlCommand cmd = new SqlCommand(cmqry, con);
        //SqlDataReader reader;
        //reader = cmd.ExecuteReader();
        return dt;
    }
    protected void btnCenterCode_OnClick(object sender, EventArgs e)
    {
        centerinfo(txtExamCode.Text.ToString(), lblSeasonHidden.Text.ToString());
    }
    private void centerinfo(string code, string session)
    {
        con.Close(); con.Open();
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        SqlCommand cmd = new SqlCommand("select * from  ExamCenter where ID='" + Convert.ToInt32(code.ToString()) + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblCenterCode.Text = reader["ID"].ToString();
            lblCenteNaem.Text = reader["Name"].ToString();
            lblCenterAddress.Text = reader["Address"].ToString();
            lblCenterAddress2.Text = reader["Address2"].ToString();
            lblCenterCity.Text = reader["City"].ToString();
            lblCenterState.Text = reader["State"].ToString();
            lblPinCode.Text = reader["Pin"].ToString();
            lblExceptionCode.Text = "";
        }
        else
        {
            lblExceptionCode.Text = "Invalid Exam Center Code";
        }
        reader.Close();
        SqlCommand cmdw = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmdw.ExecuteScalar());
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
    protected void GridChangedApps_OnselectedIndexChanged(object sender, EventArgs e)
    {
        lbSName.Text = GridChangeApps.SelectedRow.Cells[1].Text.ToString();
        lblFName.Text = GridChangeApps.SelectedRow.Cells[2].Text.ToString();
        lblIMID.Text = GridChangeApps.SelectedRow.Cells[4].Text.ToString();
        lblSID.Text = GridChangeApps.SelectedRow.Cells[3].Text.ToString();
        lblStream.Text = GridChangeApps.SelectedRow.Cells[5].Text.ToString();
        lblCourse.Text = GridChangeApps.SelectedRow.Cells[6].Text.ToString();
        lblPart.Text = GridChangeApps.SelectedRow.Cells[7].Text.ToString();
    }
}
