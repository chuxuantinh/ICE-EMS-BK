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

public partial class FO_EditcourierSupply : System.Web.UI.Page
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
                    ClsEdit clEdit = new ClsEdit();
                    string[] stredit = clEdit.EditCount("DiarySupply");
                    lblCount.Text = stredit[0].ToString();
                    if (stredit[1] == "False") pnlMain.Enabled = false;
                    else pnlMain.Enabled = true;
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    pnlVisiblefls(); pnlToFrom.Visible = false; pnlPsubmit.Visible = false;
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    panelCourier.Visible = false; btnNewDepartment.Visible = false;
                   
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
            int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]));
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
        SqlDataAdapter addiary = new SqlDataAdapter("select Distinct DiaryNo,Diary From DiaryEntry where Status='Open' and ExamSession='" + lblHiddenSeason.Text.ToString() + "' order by Diary desc", con);
        DataSet ds = new DataSet();
        addiary.Fill(ds);
        if (ds.Tables[0].ToString() != "")
        {
            GridDiaryNo.DataSource = ds;
            GridDiaryNo.DataBind();
        }
        foreach (GridViewRow rw in GridDiaryNo.Rows)
        {
            rw.Cells[2].Visible = false;
            GridDiaryNo.HeaderRow.Cells[2].Visible = false;
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
        try
        {
            clear();
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
                if (strdno == "NotOpen")
                {
                    lblExceptionDiary.Text = "Diary Not Submitted For Entry.";
                    ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false; txtDiaryNo.Focus();
                }
                else if (strdno == "Open")
                {
                    lblExceptionDiary.Text = "Edit Dairy Count."; ddlDeparmentNeme.Focus(); btnNewDepartment.Visible = true;
                    ddlDeparmentNeme.Visible = true; lblDepartmneName.Visible = true;
                    ddlDeparmentNeme.Focus();
                }
                else if (strdno == "Edit" || strdno=="Process")
                {
                    lblExceptionDiary.Text = "Diary Processing in Entry.";
                    ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                    txtDiaryNo.Focus();
                }
                showdiary();
                showdata();
            }
            con.Close();
            con.Dispose();
        }
        catch (SqlException ex)
        {
        }
    }
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        pnlSpace.Visible = true;
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
            if (strdno == "NotOpen")
            {
                lblExceptionDiary.Text = "Diary Not  Submitted For Entry.";
                 ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false; txtDiaryNo.Focus();
            }
            else if (strdno == "Open")
            {
                lblExceptionDiary.Text = "Edit Dairy Count.";
                ddlDeparmentNeme.Visible = true; lblDepartmneName.Visible = true; lblRcv.Visible = true; lblRemarks.Visible = true;
                panelIM.Visible = true; lblIMID.Visible = true; lblIMName.Visible = true; lblIMAddress.Visible = true; lblIMCity.Visible = true;
                //pnlSdiary.Visible = true;
                ddlDeparmentNeme.Focus();
            }
            else if (strdno == "Edit" || strdno=="Process")
            {
                lblExceptionDiary.Text = "Diary Processing in Entry.";
              ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            else if (strdno == "Blocked")
            {
                lblExceptionDiary.Text = "This  Diary  Blocked";
                ddlDeparmentNeme.Visible = false; lblDepartmneName.Visible = false;
                txtDiaryNo.Focus();
            }
            showdiary();
            showdata();
        }
        btnNewDepartment.Visible = true;
        con.Close();
        con.Dispose();
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
        if (diarytype == "IM")
        {
            IMInfo infos = new IMInfo();
            iminfos = infos.info(lblIMID.Text.ToString());
            if (iminfos[0].ToString() != null | iminfos[0].ToString() != "null")
            {
                lblIMName.Text = iminfos[0].ToString();
                lblIMAddress.Text = iminfos[1].ToString() + "\n" + iminfos[2].ToString();
                lblIMCity.Text = iminfos[3].ToString() + ", " + iminfos[4].ToString() + "-" + iminfos[5].ToString();
            }
        }
        else if (diarytype == "Other")
        {
            lblIMName.Text.ToString();
            lblIMID.Text = "";
            pnlToFrom.Visible = true; 
            tblEmpNameAcademic.Visible = true; pnlDepartment.Visible = true;
         }
        else if (diarytype == "Student")
        {
            cmd = new SqlCommand("select Name,PAddress,PAddress2,PCity,PState,Phone,Mobile from Student where SID='" + lblIMID.Text.ToString() + "'", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblIMName.Text = reader["Name"].ToString();
                lblIMAddress.Text = reader["PAddress"].ToString() + "\n" + reader["PAddress2"].ToString();
                lblIMCity.Text = reader["PCity"].ToString() + ", " + reader["PState"].ToString() + " Contact: " + reader["Mobile"].ToString();
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
            cmd = new SqlCommand("insert into ServiceNameMaster(Name,lblFormType.Text) values(@Name,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtNewCourier.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", "Department");
            cmd.ExecuteNonQuery();
            lblExceptionNewCourier.Text = "Successfull Saved New Department Name";
            con.Close();
            ddlDeparmentNeme.DataBind();
            con.Dispose();
        }
        btnCencel.Focus();
    }
    private void pnlVisiblefls()
    {
        pnlAcademic.Visible = false; pnlOther.Visible = false;
        pnlProject.Visible = false; pnlProject.Visible = false;
        pnlToFrom.Visible = false; tblEmpNameAcademic.Visible = false;
        pnlAdvance.Visible = false;
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
        if (txtProformaC.Text == "") txtProformaC.Text = "0";
        if (txtProformaA.Text == "") txtProformaA.Text = "0";
        if (txtProformaB.Text == "") txtProformaB.Text = "0";
        if (txtProvisional.Text == "") txtProvisional.Text = "0";
        if (txtReChecking.Text == "") txtReChecking.Text = "0";
        if (txtDuplicate.Text == "") txtDuplicate.Text = "0";
        if (txtFinalPass.Text == "") txtFinalPass.Text = "0";
        if (txtMembershipDD.Text == "") txtMembershipDD.Text = "0";
        if (txtBooksDD.Text == "") txtBooksDD.Text = "0";
        if (txtPrespectusDD.Text == "") txtPrespectusDD.Text = "0";
        if (lblADDNo.Text == "") lblADDNo.Text = "0";
        if (lblODDNo.Text == "") lblODDNo.Text = "0";
        if (lblProj.Text == "") lblProj.Text = "0";
        if (lblEnroll.Text == "") lblEnroll.Text = "0";
        if (lblExam.Text == "") lblExam.Text = "0";
        if (lblITI.Text == "") lblITI.Text = "0";
        if (lblCAD.Text == "") lblCAD.Text = "0";
        if (lblOtherForm.Text == "") lblOtherForm.Text = "0";
        if (lblProC.Text == "") lblProC.Text = "0";
        if (lblProB.Text == "") lblProB.Text = "0";
        if (lblProC.Text == "") lblProC.Text = "0";
        if (lblProvisional.Text == "") lblProvisional.Text = "0";
        if (lblRechecking.Text == "") lblRechecking.Text = "0";
        if (lblDuplicate.Text == "") lblDuplicate.Text = "0";
        if (lblFinalPass.Text == "") lblFinalPass.Text = "0";
        if (lblMembership.Text == "") lblMembership.Text = "0";
        if (lblBooks.Text == "") lblBooks.Text = "0";
        if (lblProspectus.Text == "") lblProspectus.Text = "0";
    }
    private void clear()
    {
        txtADDNo.Text = "";
        txtODDNo.Text = "";
        txtPDD.Text = "";
        txtEnroll.Text = "";
        txtExam.Text = "";
        txtITI.Text = "";
        txtCAD.Text = "";
        txtOtherForm.Text = "";
        txtProformaA.Text = "";
        txtProformaB.Text = "";
        txtProformaC.Text = "";
        txtProvisional.Text = "";
        txtReChecking.Text = "";
        txtDuplicate.Text = "";
        txtFinalPass.Text = "";
        txtMembershipDD.Text = "";
        txtBooksDD.Text = "";
        txtPrespectusDD.Text = "";
        txtLtrTo.Text = "";
        txtFrom.Text = ""; txtEmpName.Text = ""; txtEmpCode.Text = "";
    }
    protected void ddlDeparmentNeme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeparmentNeme.SelectedItem.Text == "ADMINISTRATION")
        {
            pnlAdvance.Visible = true;
            txtMembershipDD.Focus();
        }
        else
        {
            pnlAdvance.Visible = false;
            txtEmpName.Focus();
        }
    }
    private void showdata()
    {
        try
        {
            con.Open();
            cmd = new SqlCommand("select * from DairyCount where DairyNo='" + txtDiaryNo.Text + "' and Session='" + lblHiddenSeason.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblFormType.Text = reader["DairyType"].ToString();
            }
            reader.Close();
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
                    pnlToFrom.Visible = true;
                    tblEmpNameAcademic.Visible = true; pnlDepartment.Visible = true;
                    cmd = new SqlCommand("select * from DairyCount where DairyNo='" + txtDiaryNo.Text + "' and Session='" + lblHiddenSeason.Text + "'", con);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ddlDeparmentNeme.Text = reader["Department"].ToString();
                        if (ddlDeparmentNeme.Text == "ADMINISTRATION")
                        {
                            pnlAdvance.Visible = true;
                            txtMembershipDD.Text = reader["MemberRcv"].ToString(); txtBooksDD.Text = reader["BooksRcv"].ToString(); txtDuplicate.Text = reader["DuplicateDocsRcv"].ToString();
                            txtPrespectusDD.Text = reader["ProspectusRcv"].ToString(); txtLtrTo.Text = reader["LatterTo"].ToString(); txtFrom.Text = reader["LatterFrom"].ToString(); txtEmpName.Text = reader["EmpName"].ToString();
                        }
                        else
                        {
                            pnlAdvance.Visible = false;
                            txtLtrTo.Text = reader["LatterTo"].ToString(); txtFrom.Text = reader["LatterFrom"].ToString(); txtEmpName.Text = reader["EmpName"].ToString();
                        }
                    }
                    reader.Close();
                }
                else
                {
                    pnlSpace.Visible = false;
                    pnlVisiblefls();
                    if (lblFormType.Text == "Forms")
                    {
                        pnlAcademic.Visible = true;
                        pnlProject.Visible = true; pnlOther.Visible = true; pnlPsubmit.Visible = true;
                        tblEmpNameAcademic.Visible = true; pnlAdvance.Visible = true;
                        cmd = new SqlCommand("select * from DairyCount where DairyNo='" + txtDiaryNo.Text + "' and Session='" + lblHiddenSeason.Text + "'", con);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                                txtADDNo.Text = reader["ADDRcv"].ToString();
                                txtODDNo.Text = reader["ODDrcv"].ToString();
                                txtEnroll.Text = reader["EnrollFormRcv"].ToString();
                                txtExam.Text = reader["ExamFormRcv"].ToString(); txtITI.Text = reader["ITIRcv"].ToString();
                                txtCAD.Text = reader["CADRcv"].ToString(); txtOtherForm.Text = reader["OtherFormRcv"].ToString();
                                txtProvisional.Text = reader["ProvisionalRcv"].ToString(); txtFinalPass.Text = reader["FinalPassRcv"].ToString();
                                txtReChecking.Text = reader["ReCheckingRcv"].ToString(); txtDuplicate.Text = reader["DuplicateDocsRcv"].ToString();
                                txtMembershipDD.Text = reader["MemberRcv"].ToString(); txtBooksDD.Text = reader["BooksRcv"].ToString(); txtDuplicate.Text = reader["DuplicateDocsRcv"].ToString();
                                txtPrespectusDD.Text = reader["ProspectusRcv"].ToString();
                                lblADDNo.Text = reader["ADDSub"].ToString();
                                lblODDNo.Text = reader["ODDSub"].ToString();
                                lblEnroll.Text = reader["EnrollFormSub"].ToString();
                                lblExam.Text = reader["ExamFormSub"].ToString(); lblITI.Text = reader["ITISub"].ToString();
                                lblCAD.Text = reader["CADSub"].ToString(); lblOtherForm.Text = reader["OtherFormSub"].ToString();
                                lblProvisional.Text = reader["ProvisionalSub"].ToString(); lblFinalPass.Text = reader["FinalPassSub"].ToString();
                                lblRechecking.Text = reader["ReCheckingSub"].ToString(); lblDuplicate.Text = reader["DuplicateDocsSub"].ToString();
                                lblMembership.Text = reader["MemberSub"].ToString(); lblBooks.Text = reader["BooksSub"].ToString(); lblDuplicate.Text = reader["DuplicateDocsSub"].ToString();
                                lblProspectus.Text = reader["ProspectusSub"].ToString();
                            }
                            reader.Close();
                            cmd = new SqlCommand("select * from ProjectCount where DairyNo='" + txtDiaryNo.Text + "' and Session='" + lblHiddenSeason.Text + "'", con);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                // A==C and C==A
                                txtPDD.Text = reader["DDRcv"].ToString(); lblProj.Text = reader["DDSub"].ToString();
                                txtProformaC.Text = reader["EmpName"].ToString(); lblProA.Text = reader["EmpCode"].ToString();
                                txtProformaA.Text = reader["ProformaARcv"].ToString(); lblProC.Text = reader["ProformaASub"].ToString();
                                txtProformaB.Text = reader["ProformaBRcv"].ToString(); lblProB.Text = reader["ProformaBSub"].ToString();
                            }
                                reader.Close();
                           }
                        else if (lblFormType.Text == "Latters")
                        {
                            pnlToFrom.Visible = true; 
                            tblEmpNameAcademic.Visible = true; pnlDepartment.Visible = true;
                            pnlAdvance.Visible = false;
                            cmd = new SqlCommand("select * from DairyCount where DairyNo='" + txtDiaryNo.Text + "' and Session='" + lblHiddenSeason.Text + "' and DairyType='Latters'", con);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                            ddlDeparmentNeme.Text=reader["Department"].ToString();
                            txtLtrTo.Text = reader["LatterTo"].ToString(); txtFrom.Text = reader["LatterFrom"].ToString(); txtEmpName.Text = reader["EmpName"].ToString();
                            }
                            reader.Close();reader.Dispose();
                            }
                        else if (lblFormType.Text == "Others")
                        {
                        }
                    }
                }
            putZero();
            con.Close(); con.Dispose();
            }
        catch (Exception ex)
        {
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            putZero();
                    ClsEdit clEdit =new ClsEdit();
            if (lblFormType.Text == "Forms" || lblFormType.Text=="Others")
            {
                if ((Convert.ToInt32(txtADDNo.Text) >= Convert.ToInt32(lblADDNo.Text)) && (Convert.ToInt32(txtEnroll.Text) >= Convert.ToInt32(lblEnroll.Text)) && (Convert.ToInt32(txtExam.Text) >= Convert.ToInt32(lblExam.Text)) && (Convert.ToInt32(txtITI.Text) >= Convert.ToInt32(lblITI.Text)) && (Convert.ToInt32(txtCAD.Text) >= Convert.ToInt32(lblCAD.Text)) && (Convert.ToInt32(txtOtherForm.Text) >= Convert.ToInt32(lblOtherForm.Text)) && (Convert.ToInt32(txtODDNo.Text) >= Convert.ToInt32(lblODDNo.Text)) && (Convert.ToInt32(txtProvisional.Text) >= Convert.ToInt32(lblProvisional.Text)) && (Convert.ToInt32(txtDuplicate.Text) >= Convert.ToInt32(lblDuplicate.Text)) && (Convert.ToInt32(txtReChecking.Text) >= Convert.ToInt32(lblRechecking.Text)) && (Convert.ToInt32(txtFinalPass.Text) >= Convert.ToInt32(lblFinalPass.Text)) && (Convert.ToInt32(txtMembershipDD.Text) >= Convert.ToInt32(lblMembership.Text)) && (Convert.ToInt32(txtBooksDD.Text) >= Convert.ToInt32(lblBooks.Text)) && (Convert.ToInt32(txtPrespectusDD.Text) >= Convert.ToInt32(lblProspectus.Text)) && (Convert.ToInt32(txtPDD.Text) >= Convert.ToInt32(lblProj.Text)) && (Convert.ToInt32(txtProformaA.Text) >= Convert.ToInt32(lblProC.Text)) && (Convert.ToInt32(txtProformaB.Text) >= Convert.ToInt32(lblProB.Text)) && (Convert.ToInt32(txtProformaC.Text) >= Convert.ToInt32(lblProA.Text)))
                {
                    clEdit.CountUp("DiarySupply");
                    cmd = new SqlCommand("update DairyCount set ADDRcv=@ADDRcv,ODDRcv=@ODDRcv,EnrollFormRcv=@EnrollFormRcv,CADRcv=@CADRcv,ExamFormRcv=@ExamFormRcv,ITIRcv=@ITIRcv,OtherFormRcv=@OtherFormRcv,ProvisionalRcv=@ProvisionalRcv,FinalPassRcv=@FinalPassRcv,ReCheckingRcv=@ReCheckingRcv,DuplicateDocsRcv=@DuplicateDocsRcv,MemberRcv=@MemberRcv,BooksRcv=@BooksRcv,ProspectusRcv=@ProspectusRcv,TotalDDRcv=@TotalDDRcv,TotalNoForm=@TotalNoForm,ADDSub=@ADDSub,ODDSub=@ODDSub,EnrollFormSub=@EnrollFormSub,CADSub=@CADSub,ExamFormSub=@ExamFormSub,ITISub=@ITISub,OtherFormSub=@OtherFormSub,ProvisionalSub=@ProvisionalSub,FinalPassSub=@FinalPassSub,ReCheckingSub=@ReCheckingSub,DuplicateDocsSub=@DuplicateDocsSub,MemberSub=@MemberSub,BooksSub=@BooksSub,ProspectusSub=@ProspectusSub,TotalDDSub=@TotalDDSub where Session='" + lblHiddenSeason.Text + "' and DairyNo='" + txtDiaryNo.Text + "'", con);
                    cmd.Parameters.AddWithValue("@ODDRcv", Convert.ToInt32(txtODDNo.Text));
                    cmd.Parameters.AddWithValue("@ADDRcv", Convert.ToInt32(txtADDNo.Text));
                    cmd.Parameters.AddWithValue("@EnrollFormRcv", Convert.ToInt32(txtEnroll.Text));
                    cmd.Parameters.AddWithValue("@ExamFormRCv", Convert.ToInt32(txtExam.Text));
                    cmd.Parameters.AddWithValue("@CADRcv", Convert.ToInt32(txtCAD.Text));
                    cmd.Parameters.AddWithValue("@ITIRcv", Convert.ToInt32(txtITI.Text));
                    cmd.Parameters.AddWithValue("@OtherFormRcv", Convert.ToInt32(txtOtherForm.Text));
                    cmd.Parameters.AddWithValue("@ProvisionalRcv", Convert.ToInt32(txtProvisional.Text));
                    cmd.Parameters.AddWithValue("@FinalPassRcv", Convert.ToInt32(txtFinalPass.Text));
                    cmd.Parameters.AddWithValue("@ReCheckingRcv", Convert.ToInt32(txtReChecking.Text));
                    cmd.Parameters.AddWithValue("@DuplicateDocsRcv", Convert.ToInt32(txtDuplicate.Text));
                    cmd.Parameters.AddWithValue("@MemberRcv", Convert.ToInt32(txtMembershipDD.Text));
                    cmd.Parameters.AddWithValue("@ProspectusRcv", Convert.ToInt32(txtPrespectusDD.Text));
                    cmd.Parameters.AddWithValue("@BooksRcv", Convert.ToInt32(txtBooksDD.Text));

                    cmd.Parameters.AddWithValue("@TotalDDRcv", (Convert.ToInt32(txtADDNo.Text) + Convert.ToInt32(txtODDNo.Text) + Convert.ToInt32(txtPDD.Text) + Convert.ToInt32(txtMembershipDD.Text) + Convert.ToInt32(txtBooksDD.Text) + Convert.ToInt32(txtPrespectusDD.Text)));
                    cmd.Parameters.AddWithValue("@TotalNoForm", Convert.ToInt32(txtProformaA.Text) + Convert.ToInt32(txtProformaC.Text) + Convert.ToInt32(txtProformaB.Text) + Convert.ToInt32(txtEnroll.Text) + Convert.ToInt32(txtITI.Text) + Convert.ToInt32(txtCAD.Text) + Convert.ToInt32(txtOtherForm.Text) + Convert.ToInt32(txtExam.Text) + Convert.ToInt32(txtProvisional.Text) + Convert.ToInt32(txtFinalPass.Text) + Convert.ToInt32(txtReChecking.Text) + Convert.ToInt32(txtDuplicate.Text));
                    
                    cmd.Parameters.AddWithValue("@ODDSub", Convert.ToInt32(lblODDNo.Text));
                    cmd.Parameters.AddWithValue("@ADDSub", Convert.ToInt32(lblADDNo.Text));
                    cmd.Parameters.AddWithValue("@EnrollFormSub", Convert.ToInt32(lblEnroll.Text));
                    cmd.Parameters.AddWithValue("@ExamFormSub", Convert.ToInt32(lblExam.Text));
                    cmd.Parameters.AddWithValue("@CADSub", Convert.ToInt32(lblCAD.Text));
                    cmd.Parameters.AddWithValue("@ITISub", Convert.ToInt32(lblITI.Text));
                    cmd.Parameters.AddWithValue("@OtherFormSub", Convert.ToInt32(lblOtherForm.Text));
                    cmd.Parameters.AddWithValue("@ProvisionalSub", Convert.ToInt32(lblProvisional.Text));
                    cmd.Parameters.AddWithValue("@FinalPassSub", Convert.ToInt32(lblFinalPass.Text));
                    cmd.Parameters.AddWithValue("@ReCheckingSub", Convert.ToInt32(lblRechecking.Text));
                    cmd.Parameters.AddWithValue("@DuplicateDocsSub", Convert.ToInt32(lblDuplicate.Text));
                    cmd.Parameters.AddWithValue("@MemberSub", Convert.ToInt32(lblMembership.Text));
                    cmd.Parameters.AddWithValue("@ProspectusSub", Convert.ToInt32(lblProspectus.Text));
                    cmd.Parameters.AddWithValue("@BooksSub", Convert.ToInt32(lblBooks.Text));
                    cmd.Parameters.AddWithValue("@TotalDDSub", (Convert.ToInt32(lblADDNo.Text) + Convert.ToInt32(lblODDNo.Text) + Convert.ToInt32(lblProspectus.Text) + Convert.ToInt32(lblMembership.Text) + Convert.ToInt32(lblBooks.Text) + Convert.ToInt32(lblProspectus.Text)));
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("update ProjectCount set EmpName=@EmpName,EmpCode=@EmpCode,DDRcv=@DDRcv,DDSub=@DDSub,ProformaARCv=@ProformaARcv,ProformaASub=@ProformaASub,ProformaBRcv=@ProformaBRcv,ProformaBSub=@ProformaBSub where  Session='" + lblHiddenSeason.Text + "' and DairyNo='" + txtDiaryNo.Text + "'", con);
                    cmd.Parameters.AddWithValue("@DDRcv", Convert.ToInt32(txtPDD.Text));
                    cmd.Parameters.AddWithValue("@DDSub", Convert.ToInt32(lblProj.Text));
                    cmd.Parameters.AddWithValue("@EmpName", Convert.ToInt32(txtProformaC.Text));
                    cmd.Parameters.AddWithValue("@EmpCode", Convert.ToInt32(lblProA.Text));
                    cmd.Parameters.AddWithValue("@ProformaARcv", Convert.ToInt32(txtProformaA.Text));
                    cmd.Parameters.AddWithValue("@ProformaASub", Convert.ToInt32(lblProC.Text));
                    cmd.Parameters.AddWithValue("@ProformaBRcv", Convert.ToInt32(txtProformaB.Text));
                    cmd.Parameters.AddWithValue("@ProformaBSub", Convert.ToInt32(lblProB.Text));
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary Successfully Updated')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Update Lessthen Submitted Count')", true);
                }
            }
            else
            {
                cmd = new SqlCommand("update DairyCount set LatterTo='"+txtLtrTo.Text+"',LatterFrom='"+txtFrom.Text+"',Department='"+ddlDeparmentNeme.SelectedValue+"',EmpName='"+txtEmpName.Text+"' where Session='" + lblHiddenSeason.Text + "' and DairyNo='" + txtDiaryNo.Text + "'", con);
                cmd.ExecuteNonQuery();
                if (lblFormType.Text == "Others" && ddlDeparmentNeme.Text == "ADMINISTRATION")
                {
                    if ((Convert.ToInt32(txtMembershipDD.Text) >= Convert.ToInt32(lblMembership.Text) && Convert.ToInt32(txtBooksDD.Text) >= Convert.ToInt32(lblBooks.Text) && Convert.ToInt32(txtPrespectusDD.Text) >= Convert.ToInt32(lblProspectus.Text)))
                    {
                        clEdit.CountUp("DiarySupply");
                        int total= (Convert.ToInt32(txtMembershipDD.Text) + Convert.ToInt32(txtBooksDD.Text) + Convert.ToInt32(txtPrespectusDD.Text));
                        cmd = new SqlCommand("update DairyCount set MemberRcv=@MemberRcv,BooksRcv=@BooksRcv,ProspectusRcv=@ProspectusRcv,TotalDDRcv='"+total+"' where Session='" + lblHiddenSeason.Text + "' and DairyNo='" + txtDiaryNo.Text + "'", con);
                        cmd.Parameters.AddWithValue("@MemberRcv", Convert.ToInt32(txtMembershipDD.Text));
                        cmd.Parameters.AddWithValue("@ProspectusRcv", Convert.ToInt32(txtPrespectusDD.Text));
                        cmd.Parameters.AddWithValue("@BooksRcv", Convert.ToInt32(txtBooksDD.Text));
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary Successfully Updated')", true);
                    }
                }
            }
            string[] stredit = clEdit.EditCount("DiarySupply");
            lblCount.Text = stredit[0].ToString();
            if (stredit[1] == "False") pnlMain.Enabled = false;
            else pnlMain.Enabled = true;
            con.Close(); con.Dispose();
        }
        catch (SqlException ex)
        {
        }
    }
}
