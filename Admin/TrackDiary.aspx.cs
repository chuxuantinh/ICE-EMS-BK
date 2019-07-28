using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Globalization;

public partial class Admin_Track_Diary : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlData.Visible = false; pnlSpace.Visible = true; pnlletters.Visible = false;
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
    protected void btnTrack_Click(object sender, EventArgs e)
    {
        lblStatus.Text = ""; pnlletters.Visible = false;
        con.Open();
        cmd = new SqlCommand("select * from DiaryEntry where DiaryNo='" + txtDiary.Text.ToString() + "'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            pnlData.Visible = true; pnlSpace.Visible = false;
            lblConsignment.Text = rd["ConsignmentNo"].ToString();
            lbltype.Text = rd["DiaryType"].ToString();
            lblName.Text = rd["Name"].ToString(); lblMembership.Text = rd["MembershipNo"].ToString();
            lblMemberType.Text = rd["MemberType"].ToString(); lblSubmittedTo.Text = rd["SubmittedTo"].ToString();
            lblCourierNo.Text = rd["CourierNo"].ToString();
            lblCourierService.Text = rd["CourierService"].ToString();
            lblDispatch.Text = Convert.ToDateTime(rd["DispatchDate"]).ToString("dd/MM/yyyy"); lblOpenDate.Text = Convert.ToDateTime(rd["OpenedDate"]).ToString("dd/MM/yyyy");
            if (rd["Status"].ToString() == "DiaryEntry") lblStatus.Text = "Diary Entry,Not yet Opened";
            else if (rd["Status"].ToString() == "CountReceive") lblStatus.Text = "Diary Received for Count ,Not Yet Dispatched";
            else if (rd["Status"].ToString() == "CountDispatch") lblStatus.Text = "Diary Dispatched for Supply to Account,Not yet Received";
            else if (rd["Status"].ToString() == "AccReceive") lblStatus.Text = "Diary Received for Supply,Not yet Dispatched to Account";
            else if (rd["Status"].ToString() == "Open") lblStatus.Text = "Diary Supplied to Account";
            else if (rd["Status"].ToString() == "ProReceive") lblStatus.Text = "Diary Received for Project ,not yet Submitted";
            else if (rd["Status"].ToString() == "ProSubmit") lblStatus.Text = "Diary Project Count Submitted,not supplied to Account";
            
        }
        else
        {
            lblStatus.Text = "Diary Not Found"; pnlData.Visible = false; pnlSpace.Visible = true;
        }
        rd.Close();
        showdata();
        if (lbltype.Text.ToString() == "Latters")
        {
            pnlletters.Visible = true;
            cmd = new SqlCommand("select * from DiaryLetter where DiaryNo='" + txtDiary.Text + "'", con);
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lblLetterFrom.Text = rd["LetterFrom"].ToString(); lblLetterTo.Text = rd["Designation"].ToString();
                ReceiveDate.Text = Convert.ToDateTime(rd["ReceiveDate"]).ToString("dd/MM/yyyy"); lblSubject.Text = rd["Subject"].ToString();
            }
            rd.Close();
        }
        con.Close();
        con.Dispose();
    }
      private void showdata()
    {
        lblDF.Text = txtDiary.Text;
                        cmd = new SqlCommand("select * from DairyCount where DairyNo='" + txtDiary.Text + "' ", con);
                       SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            lblADDRcv.Text = reader["ADDRcv"].ToString();
                            lblADDSub.Text = reader["ADDSub"].ToString();
                            lblODDRcv.Text = reader["ODDRcv"].ToString();
                            lblODDSub.Text = reader["ODDSub"].ToString();
                            lblAdmissionRcv.Text = reader["EnrollFormRcv"].ToString();
                            lblAdmissionSub.Text = reader["EnrollFormSub"].ToString();
                            lblExamRcv.Text = reader["ExamFormRcv"].ToString();
                            lblExamSub.Text = reader["ExamFormSub"].ToString();
                            lblITIRcv.Text = reader["ITIRcv"].ToString(); lblITISub.Text = reader["ITISub"].ToString();
                            lblOthersFormRcv.Text = reader["OtherFormRcv"].ToString(); lblOthersFormSub.Text = reader["OtherFormSub"].ToString();
                            lblProvisionalRcv.Text = reader["ProvisionalRcv"].ToString(); lblProvisionalSub.Text = reader["ProvisionalSub"].ToString();
                            lblFinalPassRcv.Text = reader["FinalPassRcv"].ToString(); lblFinalPassSub.Text = reader["FinalPassSub"].ToString();
                            lblReCheckingRcv.Text = reader["ReCheckingRcv"].ToString(); lblReCheckingSub.Text = reader["ReCheckingSub"].ToString();
                            lblDuplicateRcv.Text = reader["DuplicateDocsRcv"].ToString(); lblDuplicateSub.Text = reader["DuplicateDocsSub"].ToString();
                            lblMembershipRcv.Text = reader["MemberRcv"].ToString(); lblMembershipSub.Text = reader["MemberSub"].ToString();
                            lblBooksRcv.Text = reader["BooksRcv"].ToString(); lblBooksSub.Text = reader["BooksSub"].ToString();
                            lblProsRcv.Text = reader["ProspectusRcv"].ToString(); lblProsSub.Text = reader["ProspectusSub"].ToString();
                        }
                        reader.Close();
                        cmd = new SqlCommand("select * from ProjectCount where DairyNo='" + txtDiary.Text.ToString() + "'", con);
                        SqlDataReader read;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            lblProjectRcv.Text = read["DDRcv"].ToString(); lblProjectSub.Text = read["DDSub"].ToString();
                            lblProformaCRcv.Text = read["ProformaARcv"].ToString(); lblProformaCSub.Text = read["ProformaASub"].ToString();
                            lblProformaBRcv.Text = read["ProformaBRcv"].ToString(); lblProformaBSub.Text = read["ProformaBSub"].ToString();
                        }
                        read.Close();
                        lblDNo.Text = txtDiary.Text;
                        SqlDataAdapter adp = new SqlDataAdapter("select IMID,AppNo,Enrolment,Stream,Course,Part,Name,FName,DOB,DNo,Session,SubDate,Status,FormType,FeeType,Amount,LateFee,Exempted,AdmissionFees,CompositeFees,AnnualSubFees,ITIFees,ExamFees,CADFees,DupForm,UnderAge from AppRecord where DNo='" + txtDiary.Text + "' order by DNo,Enrolment", con);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        grdForms.DataSource = dt;
                        grdForms.DataBind();
                       
                    }





      int notapp, hold = 0;
      protected void grdForms_RowDataBound(object sender, GridViewRowEventArgs e)
      {
          int app = grdForms.Rows.Count;
         
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
              if (e.Row.Cells[12].Text == "NotApproved")
              {
                  e.Row.ForeColor = System.Drawing.Color.Red;
                  notapp++;
              }
              if (e.Row.Cells[12].Text == "Hold")
              {
                  e.Row.ForeColor = System.Drawing.Color.Blue;
                  hold++;
              }
              e.Row.Cells[15].Text = e.Row.Cells[15].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[16].Text = e.Row.Cells[16].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[17].Text = e.Row.Cells[17].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[18].Text = e.Row.Cells[18].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[19].Text = e.Row.Cells[19].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[20].Text = e.Row.Cells[20].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[21].Text = e.Row.Cells[21].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[22].Text = e.Row.Cells[22].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[23].Text = e.Row.Cells[23].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[24].Text = e.Row.Cells[24].Text.TrimEnd('0').TrimEnd('.');
              e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToString("dd/MM/yyyy");
              e.Row.Cells[11].Text = Convert.ToDateTime(e.Row.Cells[11].Text).ToString("dd/MM/yyyy");
              lblApp.Text = (app - (notapp + hold)+1).ToString();
              lblNotApp.Text = notapp.ToString(); lblHold.Text = hold.ToString();
          }
      }
}