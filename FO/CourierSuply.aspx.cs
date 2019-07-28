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

public partial class FO_CourierSuply : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
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
            else
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    pnlVisiblefls(); pnlToFrom.Visible = false; pnlPsubmit.Visible = false;
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    panelCourier.Visible = false; btnNewDepartment.Visible = false;
                    btnPSubmit.Visible = false;
                    tblEmpNameAcademic.Visible = false;
                    bindDiary();
                    ddlExamSeason.Focus();
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
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        try
        {
            maikal mk = new maikal();
            int lvl=mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]));
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
    private void bindDiary()
    {
        SqlDataAdapter addiary = new SqlDataAdapter("select Distinct DiaryNo,Diary,Status,CourierNo as RefNo,MembershipNo as Membership From DiaryEntry where Status='AccReceive' and ExamSession='" + lblHiddenSeason.Text.ToString() + "' order by Diary desc", con);
        DataSet ds = new DataSet();
        addiary.Fill(ds);
        if (ds.Tables[0].ToString() != "")
        {
            GridDiaryNo.DataSource = ds;
            GridDiaryNo.DataBind();
        }

        SqlDataAdapter countDispatch = new SqlDataAdapter("select DairyNo,Status,IMID from DairyCount where DairyNo IN (select Distinct DiaryNo From DiaryEntry where Status='CountDispatch' and ExamSession='" + lblHiddenSeason.Text.ToString() + "')  order by DairyNo desc ", con);
        DataSet ds1 = new DataSet();

         countDispatch.Fill(ds1);
        if (ds1.Tables[0].ToString() != "")
        {
            GrdCountDispatch.DataSource = ds1;
            GrdCountDispatch.DataBind();
        }
    }
    protected void refreshimage_Click(object sender, ImageClickEventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        bindDiary();      
        txtDiaryNo.Text = ""; txtDiaryNo.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        bindDiary();        
        txtDiaryNo.Text = ""; txtYearSeason.Focus();
    }
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        clear();
        lblExceptionDiary.Text = "";
        pnlSpace.Visible = true;
       // txtDiaryNo.Text = GridDiaryNo.SelectedRow.Cells[1].Text.ToString();
        con.Close(); con.Open();
        cmd = new SqlCommand("select Status from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
        string strdno = Convert.ToString(cmd.ExecuteScalar());
        if (strdno == "")
        {
            lblExceptionDiary.Text = "Diary Not Found.";
            ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
            txtDiaryNo.Focus();
        }
        else
        {
            if (strdno == "DiaryEntry")
            {
                lblExceptionDiary.Text = "Diary At Front Office.";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false; txtDiaryNo.Focus();
            }
            else if (strdno == "AccReceive")
            {
                lblExceptionDiary.Text = "Supply Diary For Entry.";
                ddlDeparmentNeme.Visible = true; lblDepartmneName.Visible = true; lblRcv.Visible = true; lblRemarks.Visible = true;
                panelIM.Visible = true; lblIMID.Visible = true; lblIMName.Visible = true; lblIMAddress.Visible = true; lblIMCity.Visible = true;
                pnlSdiary.Visible = true;
                ddlDeparmentNeme.Focus();
            }
            else if (strdno == "Supply")
            {
                lblExceptionDiary.Text = "Diary Already Supplied.";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            else if (strdno == "CountReceived")
            {
                lblExceptionDiary.Text = "Diary in Process.";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            else if (strdno == "CountDispatch")
            {
                lblExceptionDiary.Text = "First Receive Diary";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            showdiary();
        }
        btnNewDepartment.Visible = true;
        ddlDairyType.Focus();
        con.Close(); con.Dispose();
    }
    // Submit Other and letters
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear(); int i = 0;
        while (i < GridDiaryNo.Rows.Count)
        {
            if (GridDiaryNo.Rows[i].Cells[3].Text == "Supply")
            {
                GridDiaryNo.Rows[i].Cells[0].Enabled = false;
                GridDiaryNo.Rows[i].Cells[0].Text = "Supplied";
            }
            i++;
        }
        
         pnlSpace.Visible = true;
         lblExceptionDiary.Text = "";
        txtDiaryNo.Text = GridDiaryNo.SelectedRow.Cells[1].Text.ToString();
        con.Close(); con.Open();
        cmd = new SqlCommand("select Status from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
        string strdno = Convert.ToString(cmd.ExecuteScalar());
        if (strdno == "")
        {
            lblExceptionDiary.Text = "Diary Not Found.";
            ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
            txtDiaryNo.Focus();
        }
        else
        {
            if (strdno == "DiaryEntry")
            {
                lblExceptionDiary.Text = "Diary At Front Office.";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false; txtDiaryNo.Focus();
            }
            else if (strdno == "AccReceive")
            {
                lblExceptionDiary.Text = "Supply Diary For Entry.";
                ddlDeparmentNeme.Visible = true; lblDepartmneName.Visible = true; lblRcv.Visible = true; lblRemarks.Visible = true;
                panelIM.Visible = true; lblIMID.Visible = true; lblIMName.Visible = true; lblIMAddress.Visible = true; lblIMCity.Visible = true;
                pnlSdiary.Visible = true;
                ddlDeparmentNeme.Focus();
            }
            else if (strdno == "Supply")
            {
                lblExceptionDiary.Text = "Diary Already Supplied.";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            else if (strdno == "CountReceived")
            {
                lblExceptionDiary.Text = "Diary in Process.";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            else if (strdno == "CountDispatch")
            {
                lblExceptionDiary.Text = "First Receive Diary";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            showdiary();
        }
        btnNewDepartment.Visible = true;
        ddlDairyType.Focus();
        con.Close(); con.Dispose();
    }
    public string[] iminfos = new string[6];
    private static string diarytype = null;
    private void showdiary()
    {
         pnlDepartment.Visible = false;
        pnlVisiblefls();
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              diarytype = reader["MemberType"].ToString();
              lblRemark.Text = reader["Remark"].ToString();
              lblIMID.Text = reader["MembershipNo"].ToString();
              lblIMName.Text = reader["Name"].ToString();
              lblSubDate.Text = Convert.ToDateTime(reader["Date"].ToString()).ToString("dd/MM/yyyy");
              lblIMAddress.Text = reader["Address1"].ToString() + "\n" + reader["Address2"].ToString();
              lblIMCity.Text = reader["City"].ToString() + "\n Phone No:" + reader["Phone"].ToString();
              txtPEmpName.Text = diarytype; 
          }
          reader.Close();
          cmd = new SqlCommand("select TotalDDRcv,TotalNoForm from DairyCount where DairyNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'", con);
          reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              lblToForms.Text = reader["TotalNoForm"].ToString();
              lblToDD.Text = reader["TotalDDRcv"].ToString();
          }
          reader.Close();
          if (diarytype == "IM")
          {
              IMInfo infos = new IMInfo();
              iminfos = infos.info(lblIMID.Text.ToString());
              if (iminfos[0].ToString() != null | iminfos[0].ToString() != "null")
              {
                  lblIMName.Text = iminfos[0].ToString();
                  lblIMAddress.Text = iminfos[1].ToString() + "\n" + iminfos[2].ToString();
                  lblIMCity.Text = iminfos[3].ToString() + ", " + iminfos[4].ToString() + "-" + iminfos[5].ToString();
                  ddlDairyType.SelectedValue = "Select";
              }
          }
          else if (diarytype == "Other")
          {
              lblIMName.Text.ToString();
              lblIMID.Text = "";
              pnlToFrom.Visible = true;
              tblEmpNameAcademic.Visible = true; pnlDepartment.Visible = true;
              ddlDairyType.SelectedValue = "Others";
             
          }
          else if (diarytype == "Student")
          {
              cmd = new SqlCommand("select Name,PAddress,PAddress2,PCity,PState,Phone,Mobile from Student where SID='" + lblIMID.Text.ToString() + "'", con);
              reader = cmd.ExecuteReader();
              if (reader.Read())
              {
                  lblIMName.Text = reader["Name"].ToString();
                  lblIMAddress.Text = reader["PAddress"].ToString() + "\n" + reader["PAddress2"].ToString();
                  lblIMCity.Text = reader["PCity"].ToString() + ", " + reader["PState"].ToString() + " Contact: " + reader["Phone"].ToString() + ", " + reader["Mobile"].ToString();
              }
              reader.Close();
          }
      con.Close();
    }
    protected void ibtnNewDepartment_Onclick(object sender, EventArgs e)
    {
        panelCourier.Visible = true;
        txtNewCourier.Focus();
    }
    protected void btnCencelnew_Onclick(object sender, EventArgs e)
    {
        ddlProjectDepartment.DataBind();
        panelCourier.Visible = false;
        lblExceptionNewCourier.Text = ""; txtNewCourier.Text = "";
        txtEmpName.Focus();
    }
    protected void btnSAveNew_Onclick(object sender, EventArgs e)
    {
        if (txtNewCourier.Text == "")
        {
            lblExceptionNewCourier.Text = "Please Insert Department Name.";
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ServiceNameMaster(Name,City,Type) values(@Name,@City,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtNewCourier.Text.ToString());
            cmd.Parameters.AddWithValue("@City", txtNewCity.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", "Department");
            cmd.ExecuteNonQuery();
            lblExceptionNewCourier.Text = "Successfull Saved New Department Name";
            con.Close();
            ddlDeparmentNeme.DataBind();
            con.Dispose();
        }
        btnCencel.Focus();
    }
    protected void ddlDairyType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        pnlDepartment.Visible = false; 
        if (txtDiaryNo.Text == "")
        {
            lblExceptionDiary.Text = "Please Select Diary.";
            txtDiaryNo.Focus();
        }
        else
        {
            if (txtPEmpName.Text == "Other")
            {
                pnlToFrom.Visible = true; btnPSubmit.Visible = true;
                tblEmpNameAcademic.Visible = true; pnlDepartment.Visible = true;
            }
            else
            {
                pnlSpace.Visible = false;
                pnlVisiblefls();
                if (ddlDairyType.SelectedValue.ToString() == "Forms")
                {
                    pnlAcademic.Visible = true; btnPSubmit.Visible = true;
                    pnlProject.Visible = true; pnlOther.Visible = true; pnlPsubmit.Visible = true;
                    tblEmpNameAcademic.Visible = true; pnlAdvance.Visible = true;
                }
                else if (ddlDairyType.SelectedValue.ToString() == "Latters")
                {
                    pnlToFrom.Visible = true;
                    tblEmpNameAcademic.Visible = true; pnlDepartment.Visible = true;
                }
                else if (ddlDairyType.SelectedValue.ToString() == "Others")
                {
                }
                else if (ddlDairyType.SelectedValue.ToString() == "Select")
                {
                    pnlSpace.Visible = true;
                }
            }
            ddlDairyType.Focus();
        }
    }
    private void pnlVisiblefls()
    {
        pnlAcademic.Visible = false; pnlOther.Visible = false;
        pnlProject.Visible = false; pnlProject.Visible = false;
        pnlToFrom.Visible = false; tblEmpNameAcademic.Visible = false;
        pnlAdvance.Visible = false;
    }
    protected void btnPSubmit_Onclick(object sender, EventArgs e)
    {
        putZero();
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select Status from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "'", con);
                string str = cmd.ExecuteScalar().ToString();
                if (str == "AccReceive")
                {
                    cmd = new SqlCommand("update DiaryEntry set Status=@Status,OpenedDate=@OpenedDate,DiaryType=@DiaryType where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblHiddenSeason.Text.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@Status", "Open");
                    cmd.Parameters.AddWithValue("@OpenedDate", Convert.ToDateTime(txtDate.Text, dtinfo));
                    cmd.Parameters.AddWithValue("@DiaryType", ddlDairyType.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("update DairyCount set ADDRcv=@ADDRcv,ODDRcv=@ODDRcv,EnrollFormRcv=@EnrollFormRcv,ExamFormRcv=@ExamFormRcv,ITIRcv=@ITIRcv,CADRcv=@CADRcv,OtherFormRcv=@OtherFormRcv,ProvisionalRcv=@ProvisionalRcv,FinalPassRcv=@FinalPassRcv,ReCheckingRcv=@ReCheckingRcv,DuplicateDocsRcv=@DuplicateDocsRcv,Status=@Status,MemberRcv=@MemberRcv,BooksRcv=@BooksRcv,ProspectusRcv=@ProspectusRcv,TotalNoForm=@TotalNoForm,TotalDDRcv=@TotalDDRcv where DairyNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@ADDRcv", Convert.ToInt32(txtADDNo.Text));
                    cmd.Parameters.AddWithValue("@ODDrcv", Convert.ToInt32(txtODDNo.Text));
                    cmd.Parameters.AddWithValue("@EnrollFormRcv", Convert.ToInt32(txtEnroll.Text));
                    cmd.Parameters.AddWithValue("@ExamFormRcv", Convert.ToInt32(txtExam.Text));
                    cmd.Parameters.AddWithValue("@ITIRcv", Convert.ToInt32(txtITI.Text));
                    cmd.Parameters.AddWithValue("@CADRcv", Convert.ToInt32(txtCAD.Text));
                    cmd.Parameters.AddWithValue("@OtherFormRcv", Convert.ToInt32(txtOtherForm.Text));
                    cmd.Parameters.AddWithValue("@ProvisionalRcv", Convert.ToInt32(txtProvisional.Text));
                    cmd.Parameters.AddWithValue("@FinalPassRcv", Convert.ToInt32(txtFinalPass.Text));
                    cmd.Parameters.AddWithValue("@ReCheckingRcv", Convert.ToInt32(txtReChecking.Text));
                    cmd.Parameters.AddWithValue("@DuplicateDocsRcv", Convert.ToInt32(txtDuplicate.Text));
                    cmd.Parameters.AddWithValue("@Status", "Supply");
                    cmd.Parameters.AddWithValue("@MemberRcv", Convert.ToInt32(txtMembershipDD.Text));
                    cmd.Parameters.AddWithValue("@BooksRcv", Convert.ToInt32(txtBooksDD.Text));
                    cmd.Parameters.AddWithValue("@ProspectusRcv", Convert.ToInt32(txtPrespectusDD.Text));
                    cmd.Parameters.AddWithValue("@TotalDDRcv", Convert.ToInt32(txtADDNo.Text) + Convert.ToInt32(txtODDNo.Text) + Convert.ToInt32(txtPDD.Text) + Convert.ToInt32(txtMembershipDD.Text) + Convert.ToInt32(txtBooksDD.Text) + Convert.ToInt32(txtPrespectusDD.Text));
                    cmd.Parameters.AddWithValue("@TotalNoForm",Convert.ToInt32(txtProformaA.Text)+Convert.ToInt32(txtProformaB.Text)+ Convert.ToInt32(txtEnroll.Text) + Convert.ToInt32(txtITI.Text) + Convert.ToInt32(txtCAD.Text) + Convert.ToInt32(txtOtherForm.Text) + Convert.ToInt32(txtExam.Text) + Convert.ToInt32(txtProvisional.Text) + Convert.ToInt32(txtFinalPass.Text) + Convert.ToInt32(txtReChecking.Text) + Convert.ToInt32(txtDuplicate.Text));
                    cmd.ExecuteNonQuery();
                    btnPSubmit.Text = "Sumitted To: " + ddlDeparmentNeme.SelectedValue.ToString();
                    cmd = new SqlCommand("Update ProjectCount set EmpName=@EmpName, DDRcv=@DDRcv,ProformaARcv=@ProformaARcv,ProformaBRcv=@ProformaBRcv,Status=@Status where DairyNo='" + txtDiaryNo.Text.ToString() + "' and Session='" + lblHiddenSeason.Text.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@DDRcv", Convert.ToInt32(txtPDD.Text));
                    cmd.Parameters.AddWithValue("@EmpName", Convert.ToInt32(txtProformaC.Text));
                    cmd.Parameters.AddWithValue("@ProformaARcv", Convert.ToInt32(txtProformaA.Text));
                    cmd.Parameters.AddWithValue("@ProformaBRcv", Convert.ToInt32(txtProformaB.Text));
                    cmd.Parameters.AddWithValue("@Status", "Supply");
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary Submitted Successfully.')", true);
                }
                else ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary Already Submitted.')", true);
                con.Close();
                bindDiary();
                con.Dispose();
        //    }
        //    else
        //    {
        //        lblExceptionProject.Text = "Already Submitted, To Commit Changes go to edit Section.";
        //    }
        //}
        //else
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Insert Correct Count.')", true);
    }
    private void putZero()
    {
        if (txtADDNo.Text == "") txtADDNo.Text = "0";
        if (txtODDNo.Text == "") txtODDNo.Text = "0";
        if (txtPDD.Text == "") txtPDD.Text = "0";
        if (txtEnroll.Text == "") txtEnroll.Text = "0";
        if (txtExam.Text == "") txtExam.Text = "0";
        if (txtITI.Text == "") txtITI.Text = "0";
        if (txtCAD.Text == "") txtCAD.Text = "0";
        if (txtOtherForm.Text == "") txtOtherForm.Text = "0";
        if (txtProformaA.Text == "") txtProformaA.Text = "0";
        if (txtProformaC.Text == "") txtProformaC.Text = "0";
        if (txtProformaB.Text == "") txtProformaB.Text = "0";
        if (txtProvisional.Text == "") txtProvisional.Text = "0";
        if (txtReChecking.Text == "") txtReChecking.Text = "0";
        if (txtDuplicate.Text == "") txtDuplicate.Text = "0";
        if (txtFinalPass.Text == "") txtFinalPass.Text = "0";
        if (txtMembershipDD.Text == "") txtMembershipDD.Text = "0";
        if (txtBooksDD.Text == "") txtBooksDD.Text = "0";
        if (txtPrespectusDD.Text == "") txtPrespectusDD.Text = "0";
    }
    private void clear()
    {
       txtADDNo.Text ="";
       txtODDNo.Text = "";
       txtPDD.Text = "";
       txtEnroll.Text = "";
       txtExam.Text = "";
       txtITI.Text = "";
       txtCAD.Text = "";
       txtOtherForm.Text = "";
       txtProformaA.Text = "";
       txtProformaC.Text = "";
       txtProformaB.Text = "";
       txtProvisional.Text = "";
       txtReChecking.Text = "";
       txtDuplicate.Text = "";
       txtFinalPass.Text = "";
       txtMembershipDD.Text = "";
       txtBooksDD.Text ="";
       txtPrespectusDD.Text = "";
       txtLtrTo.Text = "";
       txtFrom.Text = ""; txtEmpName.Text = ""; txtEmpCode.Text = "";
    }
    protected void ddlDeparmentNeme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeparmentNeme.SelectedItem.Text == "ADMINISTRATION" && ddlDairyType.SelectedValue.ToString()=="Others")
        {
            pnlAdvance.Visible = true; btnPSubmit.Visible = true;
            txtMembershipDD.Focus();
        }
        else
        {
            pnlAdvance.Visible = false;
            txtEmpName.Focus();
        }
    }
    protected void btnReceive_Click(object sender, EventArgs e)
    {
        con.Open();
        int i = 0;
        while (i < GrdCountDispatch.Rows.Count)
        {
            CheckBox rbApp = (CheckBox)GrdCountDispatch.Rows[i].FindControl("chkapp");
            if (rbApp.Checked)
            {
                cmd = new SqlCommand("update DiaryEntry set Status='AccReceive' where DiaryNo='" + GrdCountDispatch.Rows[i].Cells[1].Text + "'", con);
                cmd.ExecuteNonQuery();
            }
            i++;
        }
        bindDiary();
        con.Close();
        con.Dispose();
    }
    protected void GridDiaryNo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
            if (e.Row.Cells[3].Text == "Open")
            {
                e.Row.Cells[0].Enabled = false;
                e.Row.Cells[0].Text = "Supplied";
            }
        }
    }
    protected void GrdCountDispatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (GrdCountDispatch.Rows.Count == 0)
        {
            btnReceive.Enabled = false;

        }
        else btnReceive.Enabled = true;
    }
}
