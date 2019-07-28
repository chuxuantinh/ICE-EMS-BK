using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Data;
using System.Data.SqlClient;


public partial class Acc_ApproveRechecking : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    Student st = new Student();
    ClsExamForm p2 = new ClsExamForm();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                {
                    DropDownList1.SelectedValue = "Sum";
                    ddlsession.SelectedValue = "Win";
                }
                else
                {
                    ddlsession.SelectedValue = "Sum";
                    DropDownList1.SelectedValue = "Win";
                }
                TextBox1.Text = DateTime.Now.Year.ToString();
                txtSession.Text = (DateTime.Now.Year-1).ToString();
                lblSessionHiddend.Text = DropDownList1.SelectedValue + "" + TextBox1.Text;
                PnlMembership.Visible = false; btnSubmit.Enabled = false;
                pansession.Visible = false;
                lblSessionHiddend.Text = "";
                lblMsg.Text = "";
                btnSubmit.Enabled = false;
                con.Close(); con.Dispose();
                ddlsession.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
    
        lblExceptionOK.Text = "";
        lblMsg.Text = ""; lblMsg.CssClass = "";     
       con.Close(); con.Open();
       lblCADSerialNo.Text = serialno("ReChecking".ToString());
      
       cmd = new SqlCommand("select Examcurrent.IMID,ExamCurrent.Course,ExamCurrent.Stream,ExamCurrent.Part,Student.FeeLevel,Student.Name,Student.FName,Student.FeeLevel,Student.DOB from ExamCurrent inner join Student on ExamCurrent.SID=Student.SID where ExamCurrent.SID='" + txtMem.Text.ToString() +"'", con);
       SqlDataReader reader;
       reader = cmd.ExecuteReader();
       if (reader.Read())
       {
           if (lblIMID.Text == reader["IMID"].ToString())
           {
               pnlMain.Visible = true;
               PnlMembership.Visible = true; pnlSpace.Visible = false;
               lblName.Text = reader["Name"].ToString(); lblFName.Text = reader["FName"].ToString(); lblDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("dd/MM/yyyy");
               lblCourse.Text = reader["Course"].ToString(); lblPart.Text = reader["Part"].ToString(); lblStream.Text = reader["Stream"].ToString();
               lblLvl.Text = reader["FeeLevel"].ToString();
               reader.Close();         
               PnlMembership.Visible = true;
               GridToBeApprove.Visible = true;
              
               SqlDataAdapter ad = new SqlDataAdapter("select SID,SubID From SExamMarks where ExamSeason='" + ddlsession.SelectedValue + txtSession.Text + "' and SID='" + txtMem.Text + "' and Course='" + lblCourse.Text + "' and Part='" + lblPart.Text + "'", con);
               DataTable dt = new DataTable();
               ad.Fill(dt);
               GridToBeApprove.DataSource = dt;
               GridToBeApprove.DataBind();
               if (GridToBeApprove.Rows.Count > 0)
               {
                   btnSubmit.Enabled = true;
                   btnSubmit.Focus();
               }
               else
                   btnSubmit.Enabled = false;
           }
           else{
               lblExceptionOK.Text = "Invalid IMID for Membership " + txtMem.Text;
               lblExceptionOK.ForeColor = System.Drawing.Color.Red;
               btnSubmit.Enabled = false; 
           }
       }
       else { pnlMain.Visible = false; PnlMembership.Visible = false; lblExceptionOK.Text = "Invalid Membership"; lblExceptionOK.ForeColor = System.Drawing.Color.Red; pnlSpace.Visible = true; btnSubmit.Enabled = false; btnView.Focus(); }
       reader.Close(); reader.Dispose();
       con.Close();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        cmd = new SqlCommand();
        SqlTransaction sTR;
        sTR = con.BeginTransaction();
        cmd.Connection = con;
        cmd.Transaction = sTR;
        lblappno.Text = (apno(cmd,sTR).ToString()).ToString();
        try
        {
            int cout = 0; int flag = 0;
            if ((Convert.ToInt32(lblReCheckingRcv.Text) > Convert.ToInt32(lblRecheckingSub.Text)))
            {
                cmd.Parameters.Add("@SID",SqlDbType.NVarChar);
                cmd.Parameters.Add("@SubID", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Session", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                cmd.Parameters.Add("@AppNo", SqlDbType.NVarChar);
                for (int i = 0; i < GridToBeApprove.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridToBeApprove.Rows[i].FindControl("chkapp");
                    if (chk.Checked)
                    {
                        string SubID = GridToBeApprove.Rows[i].Cells[3].Text;
                        string SID = GridToBeApprove.Rows[i].Cells[2].Text;
                        cmd.Parameters["@SID"].Value = SID.ToString();
                        cmd.Parameters["@SubID"].Value = SubID.ToString();
                        cmd.Parameters["@Session"].Value = ddlsession.SelectedValue + txtSession.Text.ToString();
                        cmd.Parameters["@Remarks"].Value = "NoChange";
                        cmd.Parameters["@AppNo"].Value = lblappno.Text.ToString();
                        cmd.CommandText = "select count(SID) from Rechecking where SID=@SID and Session=@Session and SubID=@SubID";
                        // cmd.CommandText = "select count(SID) from Rechecking where SID=@SID and Session='" + ddlsession.SelectedValue + txtSession.Text + "' and SubID='" + SubID + "'";
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                        {
                            cout = cout + 1;
                            cmd.CommandText = "insert into Rechecking(SID,SubID,Session,AppNo,Status) values(@SID,@SubID,@Session,@AppNo,@Remarks)";
                            cmd.ExecuteNonQuery();
                            flag = 1;
                        }
                        else
                        {
                            lblMsg.Text += GridToBeApprove.Rows[i].Cells[3].Text + " Already Submitted.";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            btnSubmit.Enabled = false;
                        }
                    }
                }
                if (flag == 1)
                {
                    lblAmount.Text = getcurrentfees(sTR, cmd);
                    lblAmount.Text = (lblAmount.Text).Trim('0').Trim('.');
                    int Totalamount = Convert.ToInt32(lblAmount.Text) * cout;
                    lblAmount.Text = Totalamount.ToString();
                    cmd.CommandText = "insert into AppRecord(IMID,AppNo,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,Enrolment,AdmissionFees,Lavel,CompositeFees,AnnualSubFees,ITIFees,ExamFees,CADFees,underage,DupForm,SID,Exam,ITI,CAD,Project) values(@IMID,@AppNoo,@Stream,@Course,@Part,@Name,@FName,@DOB,@DNo,@Sessionn,@SubDate,@Status,@FormType,@FeeType,@Amount,@LateFee,@Exempted,@Enrolment,@AdmissionFees,@Lavel,@CompositeFees,@AnnualSubFees,@ITIFees,@ExamFees,@CADFees,@UnderAge,@DupForm,@SIDd,@Exam,@ITI,@CAD,@Project)";
                    cmd.Parameters.AddWithValue("@IMID", lblIMID.Text.ToString());
                    cmd.Parameters.AddWithValue("@AppNoo", lblappno.Text);
                    cmd.Parameters.AddWithValue("@Stream", lblStream.Text.ToString());
                    cmd.Parameters.AddWithValue("@Course", lblCourse.Text.ToString());
                    cmd.Parameters.AddWithValue("@Part", lblPart.Text.ToString());
                    cmd.Parameters.AddWithValue("@Name", lblName.Text.ToString());
                    cmd.Parameters.AddWithValue("@FName", lblFName.Text.ToString());
                    cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(lblDOB.Text.ToString(), dtinfo));
                    cmd.Parameters.AddWithValue("@DNo", txtDiaryNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Sessionn", lblSessionHiddend.Text.ToString());
                    cmd.Parameters.AddWithValue("@SubDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", "NotApproved");
                    cmd.Parameters.AddWithValue("@FormType", "ReChecking");
                    cmd.Parameters.AddWithValue("@FeeType", "ReChecking");
                    cmd.Parameters.AddWithValue("@Amount", lblAmount.Text);
                    cmd.Parameters.AddWithValue("@LateFee", "0");
                    cmd.Parameters.AddWithValue("@Exempted", "0");
                    cmd.Parameters.AddWithValue("@Enrolment", txtMem.Text.ToString());
                    cmd.Parameters.AddWithValue("@AdmissionFees", "0");
                    cmd.Parameters.AddWithValue("@Lavel", "YES");
                    cmd.Parameters.AddWithValue("@CompositeFees", "0");
                    cmd.Parameters.AddWithValue("@AnnualSubFees", "0");
                    cmd.Parameters.AddWithValue("@ITIFees", "0");
                    cmd.Parameters.AddWithValue("@ExamFees", "0");
                    cmd.Parameters.AddWithValue("@CADFees", "0");
                    cmd.Parameters.AddWithValue("@UnderAge", "NO");
                    cmd.Parameters.AddWithValue("@DupForm", "");
                    cmd.Parameters.AddWithValue("@CAD", lblCADSerialNo.Text);
                    cmd.Parameters.AddWithValue("@Exam", "");
                    cmd.Parameters.AddWithValue("@ITI", "");
                    cmd.Parameters.AddWithValue("@Project", "");
                    cmd.Parameters.AddWithValue("@SIDd", txtMem.Text.ToString());
                    cmd.ExecuteNonQuery();
                    updateCount(lblSessionHiddend.Text, txtDiaryNo.Text, sTR, cmd);
                    SqlDataAdapter ad = new SqlDataAdapter("select AppNo,CAD,Enrolment,Name,FName,DOB,FormType,FeeType,Amount,DNo,SubDate from AppRecord where  DNo='" + txtDiaryNo.Text.ToString() + "' order by AppNo DESC", con);
                    ad.SelectCommand.Transaction = sTR;
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    GridAppTable.DataSource = dt;
                    GridAppTable.DataBind();
                    lblMsg.CssClass = "success"; lblMsg.Text = "Successfully Submitted";
                    lblFormType.Text = "";
                    lblAmount.Text = "0";
                    btnSubmit.Enabled = false;
                    updateserialno("ReChecking", lblSessionHiddend.Text.ToString(),sTR,cmd);
                }
            }
            else
            {
                lblMsg.Text = "All Forms Already Submitted";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            sTR.Commit();
        }
        catch (SqlException ex)
        {
            sTR.Rollback();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    private int apno(SqlCommand cmd,SqlTransaction sTR)
    {
        cmd.CommandText = "select Max(AppNo) from AppRecord";
        string appno = Convert.ToString(cmd.ExecuteScalar());
        return Convert.ToInt32(appno) + 1;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = "";
        txtSession.Focus();
    }
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        PnlMembership.Visible = false;
        GridToBeApprove.Visible = false;
        lblIMID.Text = ""; txtDiaryRcvDate.Text = "";     
        lblExceptionOK.Text = "";
        lblMsg.Text = "";
        con.Close(); con.Open();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        lblSessionHiddend.Text = DropDownList1.SelectedValue + "" + TextBox1.Text;
        cmd = new SqlCommand("select IMID,Date from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + DropDownList1.SelectedValue + "" + TextBox1.Text+ "' and Status='Open'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            lblIMID.Text = rd["IMID"].ToString();
            txtDiaryRcvDate.Text = Convert.ToDateTime(rd["Date"]).ToString("dd/MM/yyyy");          
           lblDate.Text = Convert.ToDateTime(rd["Date"]).ToString("MM/dd/yyyy");
           showcount(DropDownList1.SelectedValue+TextBox1.Text, txtDiaryNo.Text.ToString());
            btnView.Focus();
            pansession.Visible = true;         
        
        }
        else
        {
            lblExceptionOK.Text = "Invalid Diary No. for " + lblIMName.Text.ToString();
            lblExceptionOK.ForeColor = System.Drawing.Color.Red;
            lblExceptionOK.Font.Bold = true;
            PnlMembership.Visible = false;           
            txtDiaryNo.Focus();
            pansession.Visible = false;
            //  btnView.Enabled = false;
        }
        rd.Close(); rd.Dispose();
        con.Close(); con.Dispose();
    }

    private void showcount(string session, string dairy)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from DairyCount where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        lblMsg.Text = "";
        while (reader.Read())
        {
            lblReCheckingRcv.Text = reader["ReCheckingRcv"].ToString(); lblRecheckingSub.Text = reader["ReCheckingSub"].ToString();
           
        }
        reader.Close();
        con.Close();
        con.Dispose();
    }

    private void updateCount(string session, string dairy,SqlTransaction sTR,SqlCommand cmd)
    {
        lblRecheckingSub.Text = (Convert.ToInt32(lblRecheckingSub.Text) + 1).ToString();
        cmd.CommandText = "update DairyCount set ReCheckingSub=@RecheckingSub where Session='" + session.ToString() + "' and DairyNo='" + dairy.ToString() + "'";
        cmd.Parameters.AddWithValue("@RecheckingSub", Convert.ToInt32(lblRecheckingSub.Text));
        cmd.ExecuteNonQuery();
    }

    string parts;
    private string getcurrentfees(SqlTransaction sTR,SqlCommand cmd)
    {
        if(lblPart.Text=="PartI"  || lblPart.Text=="PartII")
        {
            parts = "Tech";
        }
        if (lblPart.Text == "SectionA" || lblPart.Text == "SectionB")
        {
            parts = "Asso";
        }
        cmd.CommandText = "select Rechacking from FeeMaster where FeeType='"+parts+"' and FeeLevel='"+lblLvl.Text+"'and Type='" + ddlFeeMaster.SelectedValue.ToString() + "'";
        string level = Convert.ToString(cmd.ExecuteScalar());
        return level;
    }

    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Text = "DiaryNo";
            e.Row.Cells[3].Text = "Membership";
            e.Row.Cells[1].Text = "SNo";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.ToString().TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[5].Text != null & e.Row.Cells[5].Text != "" & e.Row.Cells[3].Text != "&nbsp;")
                e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).ToString("dd/MM/yyyy");
            if (e.Row.Cells[10].Text != null & e.Row.Cells[10].Text != "" & e.Row.Cells[10].Text != "&nbsp;")
                e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).ToString("dd/MM/yyyy");
        }
    }

    private string serialno(string type)
    {
        cmd = new SqlCommand("select SerialNo from FeeList where FeeName='" + type.ToString() + "' and Status='NO' and Session='" + lblSessionHiddend.Text.ToString() + "'", con);
        string rtnsn = Convert.ToString(cmd.ExecuteScalar());
        if (rtnsn == "")
        {
            cmd = new SqlCommand("insert into FeeList (FeeName,Status,Session) values(@FeeName,@Status,@Session)", con);
            cmd.Parameters.AddWithValue("@FeeName", type.ToString());
            cmd.Parameters.AddWithValue("@Status", "NO");
            cmd.Parameters.AddWithValue("@Session", lblSessionHiddend.Text.ToString());
            cmd.ExecuteNonQuery();
            rtnsn = "0";
        }
        string fw = type.Substring(0, 1);
        int intsn = Convert.ToInt32(rtnsn) + 1;
        rtnsn = intsn.ToString();
        return fw.ToString() + "" + rtnsn.ToString();
    }

    private void updateserialno(string type, string session,SqlTransaction sTR,SqlCommand cmd)
    {
        lblMsg.Text = "";
        try
        {
            cmd.CommandText = "update Feelist set SerialNo=SerialNo+1 where FeeName='" + type.ToString() + "' and Session='" + session.ToString() + "'";
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            lblMsg.Text += type.ToString() + " SerialNo. Not Updated.";
        }
    }
   
}