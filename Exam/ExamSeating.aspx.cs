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
public partial class Exam_ExamSeating : System.Web.UI.Page
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
            else
            {
                if (!IsPostBack)
                {
                    btnSelectForRollNo.Visible = false;
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    datastructure(); datastructure2();
                    lblToApp.Text = "0"; lblToCP1.Text = "0"; lblToCP2.Text = "0"; lblToCS1.Text = "0"; lblToCS2.Text = "0";
                    lblToAP1.Text = "0"; lblToAP2.Text = "0"; lblToAS1.Text = "0"; lblToAS2.Text = "0";
                    lblToExamForms.Text = "0"; lblToPart1.Text = "0"; lbltoPartII.Text = "0"; lblToSectionA.Text = "0"; lblToSectinB.Text = "0";
                    lblToPPPartI.Text = "0"; lblToPPartII.Text = "0"; lblToSSectionA.Text = "0"; lblToSSectinB.Text = "0";
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    lbltef.Text = "0";
                    lbltp1.Text = "0";
                    lbltpII.Text = "0";
                    lbltsA.Text = "0";
                    lbltsB.Text = "0";
                    lbltpp1.Text = "0";
                    lbltpp2.Text = "0";
                    lbltssA.Text = "0";
                    lbltssB.Text = "0";
                    ddlExamSeason.Focus();
                    //  lblExamDAte.Text = Convert.ToDateTime(ddlExaminationdate.SelectedValue.ToString()).ToString("dd/MM/yyyy");
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtExamCode.Focus();
    }
    protected void ddlExamDate_SelectedIndexChanged(object sneder, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        lblExamDAte.Text = Convert.ToDateTime(ddlExaminationdate.SelectedValue.ToString()).ToString("dd/MM/yyy");
        ddlShift.Focus();
    }
    protected void GridExamCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridView2.SelectedRow;
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        lblCenterCode.Text = gr.Cells[1].Text.ToString();
        lblCity.Text = gr.Cells[3].Text.ToString();
        con.Close();
        con.Open(); SqlCommand cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        con.Close(); con.Dispose();
        if (sum == "")
            lblCapacity.Text = "0";
        else 
            lblCapacity.Text = sum.ToString();
        txtExamCode.Focus();
    }
    protected void GridExamSub_OnRowDateBound(object sender, GridViewRowEventArgs e)
    {
    }
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SID");
        dtDatas.Columns.Add("SubID");
        dtDatas.Columns.Add("SubName");
        dtDatas.Columns.Add("Status");
        dtDatas.Columns.Add("ExamSession");
        dtDatas.Columns.Add("IMID");
        dtDatas.Columns.Add("Course");
        dtDatas.Columns.Add("Part");
        dtDatas.Columns.Add("CenterCode");
        dtDatas.Columns.Add("SubType");
        dtDatas.Columns.Add("City");
        dtDatas.Columns.Add("RollNo");
        ViewState["dtDatas"] = dtDatas;
    }
    protected void btnSelectExamFrom_OnClick(object sender, EventArgs e)
    {
        lblToApp.Text = toapp.ToString();
        lblToCP1.Text = cp1.ToString();
        lblToCP2.Text = cp2.ToString();
        lblToCS1.Text = cs1.ToString();
        lblToCS2.Text = cs2.ToString();
        lblToAP1.Text = ap1.ToString();
        lblToAP2.Text = ap2.ToString();
        lblToAS1.Text = as1.ToString();
        lblToAS2.Text = as2.ToString();
        clearGrid();
        lblToExamForms.Text = "0"; lblToPart1.Text = "0"; lbltoPartII.Text = "0"; lblToSectionA.Text = "0"; lblToSectinB.Text = "0";
        lblToPPPartI.Text = "0"; lblToPPartII.Text = "0"; lblToSSectionA.Text = "0"; lblToSSectinB.Text = "0";
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
        dtDatas.Clear();
        GridExamSub.DataSource = null;
        GridExamSub.DataBind();
        GridViewRow rw;
        rw = GridView1.SelectedRow;
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("select ExamForms.SID,ExamForm.SubID,ExamForm.SubName,Examforms.Status,ExamForms.ExamSeason,Examforms.IMID,ExamForms.Course,examforms.Part,ExamForms.CenterCode,ExamForm.SubType,Examforms.City,ExamForms.RollNo from ExamForm inner join ExamForms on ExamForms.SN=ExamForm.SN where ExamForm.SubID='" + GridView1.Rows[i].Cells[0].Text.ToString() + "' and ExamForms.ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and ExamForms.CenterCode='" + txtExamCode.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SID"] = reader["SID"].ToString();
                drNewRow["SubID"] = reader["SubID"].ToString();
                drNewRow["SubName"] = reader["SubName"].ToString();
                drNewRow["Status"] = reader["Status"].ToString();
                drNewRow["ExamSession"] = reader["ExamSeason"].ToString();
                drNewRow["IMID"] = reader["IMID"].ToString();
                drNewRow["Course"] = reader["Course"].ToString();
                drNewRow["Part"] = reader["Part"].ToString();
                drNewRow["CenterCode"] = reader["CenterCode"].ToString();
                drNewRow["SubType"] = reader["SubType"].ToString();
                drNewRow["City"] = reader["City"].ToString();
                drNewRow["RollNo"] = reader["RollNo"].ToString();
                dtDatas.Rows.Add(drNewRow);
                GridExamSub.DataSource = dtDatas;
                GridExamSub.DataBind();
            }
            reader.Close();
            reader.Dispose();
            con.Close();
        }
        selectno();
        con.Dispose();
        txtNOOfForms.Focus();
    }
    decimal toapp = 0, cp1 = 0, cp2 = 0, cs1 = 0, cs2 = 0, ap1 = 0, ap2 = 0, as1 = 0, as2 = 0;
    private void selectno()
    {
        int i = 0, j;
        toapp = Convert.ToDecimal(lblToApp.Text);
        cp1 = Convert.ToDecimal(lblToCP1.Text);
        cp2 = Convert.ToDecimal(lblToCP2.Text);
        cs1 = Convert.ToDecimal(lblToCS1.Text);
        cs2 = Convert.ToDecimal(lblToCS2.Text);
        ap1 = Convert.ToDecimal(lblToAP1.Text);
        ap2 = Convert.ToDecimal(lblToAP2.Text);
        as1 = Convert.ToDecimal(lblToAS1.Text);
        as2 = Convert.ToDecimal(lblToAS2.Text);
        for (j = 0; j <= GridExamSub.Rows.Count-1; j++)
        {
            if (GridExamSub.Rows[j].Cells[7].Text == "Civil")
                    {
                        if (GridExamSub.Rows[j].Cells[8].Text == "PartI")
                        {
                            cp1 = cp1 + 1;
                            toapp = toapp + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "PartII")
                        {
                            cp2 = cp2 + 1; toapp = toapp + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionA")
                        {
                            cs1 = cs1 + 1; toapp = toapp + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionB")
                        {
                            cs2 = cs2 + 1; toapp = toapp + 1;
                        }
                    }
            else if (GridExamSub.Rows[j].Cells[7].Text == "Architecture")
                    {
                        if (GridExamSub.Rows[j].Cells[8].Text == "PartI")
                        {
                            ap1 = ap1 + 1;
                            toapp = toapp + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "PartII")
                        {
                            ap2 = ap2 + 1; toapp = toapp + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionA")
                        {
                            as1 = as1 + 1; toapp = toapp + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionB")
                        {
                            as2 = as2 + 1; toapp = toapp + 1;
                        }
                    }
        }
        lblToApp.Text = toapp.ToString();
        lblToCP1.Text = cp1.ToString();
        lblToCP2.Text = cp2.ToString();
        lblToCS1.Text = cs1.ToString();
        lblToCS2.Text = cs2.ToString();
        lblToAP1.Text = ap1.ToString();
        lblToAP2.Text = ap2.ToString();
        lblToAS1.Text = as1.ToString();
        lblToAS2.Text = as2.ToString();
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;
        rw = GridView3.SelectedRow;
        lblRoomNo.Text = rw.Cells[2].Text.ToString();
        lblRoomCapacity.Text = rw.Cells[3].Text.ToString();
        lblRoomColumn.Text = rw.Cells[4].Text.ToString();
        btnSelectForRollNo.Visible = true;
        ddlPart.Focus();
    }
    decimal ts=0, tofo = 0, pI = 0, pII = 0, SI = 0, SII = 0, ppI = 0, ppII = 0, SSI = 0, SSII = 0, select = 0;
    protected void btnSelectForRollNo_Click(object sender, EventArgs e)
    {
        select = Convert.ToDecimal(txtNOOfForms.Text);
        tofo = Convert.ToDecimal(lblToApp.Text);
        pI = Convert.ToDecimal(lblToCP1.Text);
        pII = Convert.ToDecimal(lblToCP2.Text);
        SI = Convert.ToDecimal(lblToCS1.Text);
        SII = Convert.ToDecimal(lblToCS2.Text);
        ppI = Convert.ToDecimal(lblToAP1.Text);
        ppII = Convert.ToDecimal(lblToAP2.Text);
        SSI = Convert.ToDecimal(lblToAS1.Text);
        SSII = Convert.ToDecimal(lblToAS2.Text);
        toseat = Convert.ToDecimal(lblToExamForms.Text);
        ts = Convert.ToDecimal(lbltef.Text);
        cpseat1 = Convert.ToDecimal(lblToPart1.Text);
        cpseat2 = Convert.ToDecimal(lbltoPartII.Text);
        csseat1 = Convert.ToDecimal(lblToSectionA.Text);
        csseat2 = Convert.ToDecimal(lblToSectinB.Text);
        apseat1 = Convert.ToDecimal(lblToPPPartI.Text);
        apseat2 = Convert.ToDecimal(lblToPPartII.Text);
        asseat1 = Convert.ToDecimal(lblToSSectionA.Text);
        asseat2 = Convert.ToDecimal(lblToSSectinB.Text);
        if (Convert.ToDecimal(lblRoomCapacity.Text) >= (ts + select))
        {
            if (ddlCourse.SelectedValue.ToString() == "Civil")
            {
                if (ddlPart.SelectedValue.ToString() == "PartI")
                {
                    if (pI >= select & cpseat1 + select <= pI)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()); lblExceptionSelect.Text = "";
                    }
                    else if (pI < select)
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + pI.ToString();
                    }
                    else if (cpseat1 + select > pI)
                    {
                        lblExceptionSelect.Text = "Selected Part I forms are more then available forms";
                    }
                }
                if (ddlPart.SelectedValue.ToString() == "PartII")
                {
                    if (pII >= select & cpseat2 + select <= pII)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()); lblExceptionSelect.Text = "";
                    }
                    else if (pII < select)
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + pII.ToString();
                    }
                    else if (cpseat2 + select > pII)
                    {
                        lblExceptionSelect.Text = "Selected Part II forms are more then available forms";
                    }
                }
                if (ddlPart.SelectedValue.ToString() == "SectionA")
                {
                    if (SI >= select & csseat1 + select <= SI)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()); lblExceptionSelect.Text = "";
                    }
                    else if (csseat1 + select > SI)
                    {
                        lblExceptionSelect.Text = "Selected Section A forms are more then available forms";
                    }
                    else
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + SI.ToString();
                    }
                }
                if (ddlPart.SelectedValue.ToString() == "SectionB")
                {
                    if (SII >= select & csseat2 + select <= SII)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()); lblExceptionSelect.Text = "";
                    }
                    else if (csseat2 + select > SII)
                    {
                        lblExceptionSelect.Text = "Selected Section B forms are more then available forms";
                    }
                    else
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + SII.ToString();
                    }
                }
            }
            else if (ddlCourse.SelectedValue.ToString() == "Architecture")
            {
                if (ddlPart.SelectedValue.ToString() == "PartI")
                {
                    if (ppI >= select & apseat1 + select <= ppI)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()); lblExceptionSelect.Text = "";
                    }
                    else if (apseat1 + select > ppI)
                    {
                        lblExceptionSelect.Text = "Selected Part I forms are more then available forms";
                    }
                    else
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + ppI.ToString();
                    }
                }
                if (ddlPart.SelectedValue.ToString() == "PartII")
                {
                    if (ppII >= select & apseat2 + select <= ppII)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()); lblExceptionSelect.Text = "";
                    }
                    else if (apseat2 + select > ppII)
                    {
                        lblExceptionSelect.Text = "Selected Part II forms are more then available forms";
                    }
                    else
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + ppII.ToString();
                    }
                }
                if (ddlPart.SelectedValue.ToString() == "SectionA")
                {
                    if (SSI >= select & asseat1 + select <= SSI)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString()); lblExceptionSelect.Text = "";
                    }
                    else if (asseat1 + select > SSI)
                    {
                        lblExceptionSelect.Text = "Selected Section A forms are more then available forms";
                    }
                    else
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + SSI.ToString();
                    }
                }
                if (ddlPart.SelectedValue.ToString() == "SectionB")
                {
                    if (SSII >= select & asseat2 + select <= SSII)
                    {
                        selectno(ddlCourse.SelectedValue.ToString(), ddlPart.SelectedValue.ToString());
                        lblExceptionSelect.Text = "";
                    }
                    else if (asseat2 + select > SSI)
                    {
                        lblExceptionSelect.Text = "Selected Section B forms are more then available forms";
                    }
                    else
                    {
                        lblExceptionSelect.Text = "Please Select No. of Forms under " + SSII.ToString();
                    }
                }
            }
        }
        else
        {
            lblExceptionSelect.Text = "Please Select Under Room Capacity";
        }
    }
   public decimal toseat = 0, cpseat1 = 0, cpseat2 = 0, csseat1 = 0, csseat2 = 0, apseat1 = 0, apseat2 = 0, asseat1 = 0, asseat2 = 0;
   public decimal tos = 0, cps1 = 0, cps2 = 0, css1 = 0, css2 = 0, aps1 = 0, aps2 = 0, ass1 = 0, ass2 = 0;
    public void selectno(string course, string part)
    {
        int i = 0, j;
        toseat = Convert.ToDecimal(lblToExamForms.Text);
        cpseat1 = Convert.ToDecimal(lblToPart1.Text);
        cpseat2 = Convert.ToDecimal(lbltoPartII.Text);
        csseat1 = Convert.ToDecimal(lblToSectionA.Text);
        csseat2 = Convert.ToDecimal(lblToSectinB.Text);
        apseat1 = Convert.ToDecimal(lblToPPPartI.Text);
        apseat2 = Convert.ToDecimal(lblToPPartII.Text);
        asseat1 = Convert.ToDecimal(lblToSSectionA.Text);
        asseat2 = Convert.ToDecimal(lblToSSectinB.Text);

        tos = Convert.ToDecimal(lbltef.Text);
        cps1 = Convert.ToDecimal(lbltp1.Text);
        cps2 = Convert.ToDecimal(lbltpII.Text);
        css1 = Convert.ToDecimal(lbltsA.Text);
        css2 = Convert.ToDecimal(lbltsB.Text);
        aps1 = Convert.ToDecimal(lbltpp1.Text);
        aps2 = Convert.ToDecimal(lbltpp2.Text);
        ass1 = Convert.ToDecimal(lbltssA.Text);
        ass2 = Convert.ToDecimal(lbltssB.Text);
        for (j = 0; j <= GridExamSub.Rows.Count; j++)
        {
            if (i < select)
            {
                if (GridExamSub.Rows[j].Cells[7].Text == course & GridExamSub.Rows[j].Cells[8].Text == part)
                {

                    if (GridExamSub.Rows[j].Cells[7].Text == "Civil")
                    {
                        if (GridExamSub.Rows[j].Cells[8].Text == "PartI")
                        {
                            cpseat1 = cpseat1 + 1; cps1 = cps1 + 1;
                            toseat = toseat + 1; tos = tos + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "PartII")
                        {
                            cpseat2 = cpseat2 + 1; toseat = toseat + 1;
                            cps2 = cps2 + 1; tos = tos + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionA")
                        {
                            csseat1 = csseat1 + 1; toseat = toseat + 1;
                            css1 = css1 + 1; tos = tos + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionB")
                        {
                            csseat2 = csseat2 + 1; toseat = toseat + 1;
                            css2 = css2 + 1; tos = tos + 1;
                        }
                    }
                    else if (GridExamSub.Rows[j].Cells[7].Text == "Architecture")
                    {
                        if (GridExamSub.Rows[j].Cells[8].Text == "PartI")
                        {
                            apseat1 = apseat1 + 1; aps1 = aps1 + 1;
                            toseat = toseat + 1; tos = tos + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "PartII")
                        {
                            apseat2 = apseat2 + 1; toseat = toseat + 1;
                            aps2 = aps2 + 1; tos = tos + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionA")
                        {
                            asseat1 = asseat1 + 1; toseat = toseat + 1;
                            ass1 = ass1 + 1; tos = tos + 1;
                        }
                        else if (GridExamSub.Rows[j].Cells[8].Text == "SectionB")
                        {
                            asseat2 = asseat2 + 1; toseat = toseat + 1;
                            ass2 = ass2 + 1; tos = tos + 1;
                        }
                    }
                    DataTable dtdas = (DataTable)ViewState["dtbal"];
                    DataRow drNewRw = dtdas.NewRow();
                    drNewRw["SID"] = GridExamSub.Rows[j].Cells[0].Text;
                    drNewRw["RollNo"] = GridExamSub.Rows[j].Cells[13].Text;
                    drNewRw["SubCode"] = GridExamSub.Rows[j].Cells[1].Text;
                    drNewRw["SubName"] = GridExamSub.Rows[j].Cells[2].Text;
                    drNewRw["ExamSeason"] = GridExamSub.Rows[j].Cells[4].Text;
                    drNewRw["Course"] = GridExamSub.Rows[j].Cells[7].Text;
                    drNewRw["Part"] = GridExamSub.Rows[j].Cells[8].Text;
                    drNewRw["CenterCode"] = GridExamSub.Rows[j].Cells[9].Text;
                    drNewRw["CenterName"] = GridExamSub.Rows[j].Cells[10].Text;
                    drNewRw["City"] = GridExamSub.Rows[j].Cells[12].Text;
                    drNewRw["Date"] = ddlExaminationdate.SelectedValue.ToString();
                    drNewRw["Shift"] = ddlShift.SelectedValue.ToString();
                    drNewRw["RoomNo"] = lblRoomNo.Text.ToString();
                    dtdas.Rows.Add(drNewRw);
                    GridSeating.DataSource = dtdas;
                    GridSeating.DataBind();
                    i++;
                    GridExamSub.Rows[j].Dispose();
                    GridExamSub.Rows[j].Cells[8].Text = "selected";
                }
            }
        }
        lblToExamForms.Text = toseat.ToString();
        lblToPart1.Text = cpseat1.ToString();
        lbltoPartII.Text= cpseat2.ToString();
        lblToSectionA.Text = csseat1.ToString();
        lblToSectinB.Text = csseat2.ToString();
        lblToPPPartI.Text = apseat1.ToString();
        lblToPPartII.Text = apseat2.ToString();
        lblToSSectionA.Text = asseat1.ToString();
         lblToSSectinB.Text = asseat2.ToString();
        lbltef.Text = tos.ToString();
        lbltp1.Text = cps1.ToString();
        lbltpII.Text = cps2.ToString();
        lbltsA.Text = css1.ToString();
        lbltsB.Text = css2.ToString();
        lbltpp1.Text = aps1.ToString();
        lbltpp2.Text = aps2.ToString();
        lbltssA.Text = ass1.ToString();
        lbltssB.Text = ass2.ToString();
    }
    private void datastructure2()
    {
        DataTable dtbal = new DataTable();
        dtbal.Columns.Add("SID");
        dtbal.Columns.Add("RollNo");
        dtbal.Columns.Add("SubCode");
        dtbal.Columns.Add("SubName");
        dtbal.Columns.Add("ExamSeason");
        dtbal.Columns.Add("Course");
        dtbal.Columns.Add("Part");
        dtbal.Columns.Add("CenterCode");
        dtbal.Columns.Add("CenterName");
        dtbal.Columns.Add("City");
        dtbal.Columns.Add("Date");
        dtbal.Columns.Add("Shift");
        dtbal.Columns.Add("RoomNo");
        ViewState["dtbal"] = dtbal;
    }
    int tofm, rcp, col, row, rwm;
    string query, part, part2;
    protected void btnSaveRoom_clcik(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            tofm = Convert.ToInt32(lblToExamForms.Text);
            rcp = Convert.ToInt32(lblRoomCapacity.Text);
            col = Convert.ToInt32(lblRoomColumn.Text);
            if (tofm > 0)
            {
                row = rcp / col;
                int tof = row * col;
                if (tof != rcp)
                {
                    row = row + 1;
                }
                string sn = "";
                cmd = new SqlCommand("select SN from Seating where SN='1' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
                sn = Convert.ToString(cmd.ExecuteScalar());
                if (sn == "")
                {
                    for (int i = 1; i <= row; i++)
                    {
                        cmd = new SqlCommand("insert into Seating(SN,CenterCode,CenterName,City,Date,Shift,RoomNo,Session) Values(@SN,@CenterCode,@CenterName,@City,@Date,@Shift,@RoomNo,@Session)", con);
                        cmd.Parameters.AddWithValue("@SN", i.ToString());
                        cmd.Parameters.AddWithValue("@CenterCode", lblCenterCode.Text.ToString());
                        cmd.Parameters.AddWithValue("@CenterName", lblCenteNaem.Text.ToString());
                        cmd.Parameters.AddWithValue("@City", lblCity.Text.ToString());
                        cmd.Parameters.AddWithValue("@Date", ddlExaminationdate.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@Shift", ddlShift.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@RoomNo", lblRoomNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@Session", lblSeasonHidden.Text.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
                query = "update Seating set Column1='" + GridSeating.Rows[0].Cells[1].Text.ToString() + "' where SN='1' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                part = GridSeating.Rows[0].Cells[6].Text.ToString();
                GridSeating.Rows[0].Cells[6].Text = "Selected";
            }
            int rw = 2;
            int cl = 1;
            rwm = 2;
            //for(int cl=1;cl<=col;cl++)
            //{
            //for (int rw = 1; rw <= row; rw++)
            //{
            //for (int gr = 1; gr <= GridSeating.Rows.Count - 1; gr++)
            //{
            do
            {
                for (int gv = 1; gv <= GridSeating.Rows.Count - 1; gv++)
                {
                    if (gv == 1) part2 = "0";
                    if ((GridSeating.Rows[gv].Cells[6].Text == "PartI" | GridSeating.Rows[gv].Cells[6].Text == "PartII" | GridSeating.Rows[gv].Cells[6].Text == "SectionA" | GridSeating.Rows[gv].Cells[6].Text == "SectionB") & GridSeating.Rows[gv].Cells[6].Text != part)
                    {
                        //if (GridSeating.Rows[gv].Cells[6].Text != GridSeating.Rows[gv+1].Cells[6].Text)
                        //{
                        //Update Seating.
                        if (cl == 1)
                        {
                            query = "update Seating set Column1='" + GridSeating.Rows[gv].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        }
                        else if (cl == 2)
                        {
                            query = "update Seating set Column2='" + GridSeating.Rows[gv].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        }
                        else if (cl == 3)
                        {
                            query = "update Seating set Column3='" + GridSeating.Rows[gv].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        }
                        else if (cl == 4)
                        {
                            query = "update Seating set Column4='" + GridSeating.Rows[gv].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        }
                        else if (cl == 5)
                        {
                            query = "update Seating set Column5='" + GridSeating.Rows[gv].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "'and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        }
                        else if (cl == 6)
                        {
                            query = "update Seating set Column6='" + GridSeating.Rows[gv].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                        }
                        else
                        {
                        }
                        SqlCommand cmupdte = new SqlCommand(query, con);
                        cmupdte.ExecuteNonQuery(); part2 = "1"; part = GridSeating.Rows[gv].Cells[6].Text.ToString();
                        GridSeating.Rows[gv].Cells[6].Text = "Selected";
                        // GridSeating.DataBind();

                        rw++;
                        rwm++;
                        break;
                    }
                    else
                    {
                        if (gv == GridSeating.Rows.Count - 1)
                        {
                            if (part2.ToString() == "0")
                            {
                                do
                                {
                                    for (int gv1 = 1; gv1 <= GridSeating.Rows.Count - 1; gv1++)
                                    {

                                        if ((GridSeating.Rows[gv1].Cells[6].Text == "PartI" | GridSeating.Rows[gv1].Cells[6].Text == "PartII" | GridSeating.Rows[gv1].Cells[6].Text == "SectionA" | GridSeating.Rows[gv1].Cells[6].Text == "SectionB") & GridSeating.Rows[gv1].Cells[6].Text == part)
                                        {
                                            //if (GridSeating.Rows[gv].Cells[6].Text != GridSeating.Rows[gv+1].Cells[6].Text)
                                            //{
                                            //Update Seating.
                                            if (cl == 1)
                                            {
                                                query = "update Seating set Column1='" + GridSeating.Rows[gv1].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                                            }
                                            else if (cl == 2)
                                            {
                                                query = "update Seating set Column2='" + GridSeating.Rows[gv1].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                                            }
                                            else if (cl == 3)
                                            {
                                                query = "update Seating set Column3='" + GridSeating.Rows[gv1].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                                            }
                                            else if (cl == 4)
                                            {
                                                query = "update Seating set Column4='" + GridSeating.Rows[gv1].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                                            }
                                            else if (cl == 5)
                                            {
                                                query = "update Seating set Column5='" + GridSeating.Rows[gv1].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "'and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                                            }
                                            else if (cl == 6)
                                            {
                                                query = "update Seating set Column6='" + GridSeating.Rows[gv].Cells[1].Text.ToString() + "' where SN='" + rw.ToString() + "' and  CenterCode='" + lblCenterCode.Text.ToString() + "' and Date='" + ddlExaminationdate.SelectedValue.ToString() + "' and Shift='" + ddlShift.SelectedValue.ToString() + "' and RoomNo='" + lblRoomNo.Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'";
                                            }
                                            else
                                            {
                                            }
                                            SqlCommand cmupdt = new SqlCommand(query, con);
                                            cmupdt.ExecuteNonQuery(); part = GridSeating.Rows[gv1].Cells[6].Text.ToString();
                                            GridSeating.Rows[gv1].Cells[6].Text = "Selected";
                                            rw++;
                                            rwm++;
                                            break;
                                        }
                                    }
                                    if (rw - 1 == row)
                                    {
                                        if (cl <= 5)
                                        {
                                            cl = cl + 1;
                                            rw = 1;
                                        }
                                    }
                                }
                                while (rwm <= GridSeating.Rows.Count);
                            }
                        }
                    }
                }
                if (rw - 1 == row)
                {
                    if (cl <= 5)
                    {
                        cl = cl + 1;
                        rw = 1;
                    }
                }
            }
            while (rwm <= GridSeating.Rows.Count);
            //}
            //}
            // }
            lblExceptionSave.ForeColor = System.Drawing.Color.Green;
            lblExceptionSave.Text = "Seating Arrangement Saved";
        }
        catch (SqlException ex)
        {
            lblExceptionSave.Text = ex.ToString();
            lblExceptionSave.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void GridSeating_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridSeating.PageIndex = e.NewPageIndex;
    }
    protected void btnCenterCode_OnClick(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            SqlCommand cmd = new SqlCommand("select * from  ExamCenter where ID='" + Convert.ToInt32(txtExamCode.Text) + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                lblCenterCode.Text = reader["ID"].ToString();
                lblCenteNaem.Text = reader["Name"].ToString();
                lblCity.Text = reader["City"].ToString();
                lblExceptionCode.Text = "";
            }
            else
            {
                lblExceptionCode.Text = "Invalid Exam Center Code";
            }
            reader.Close();
            reader.Dispose();
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
            ddlExaminationdate.Focus();
        }
        catch (FormatException ex)
        {
            lblExceptionCode.Text = "Invalid Exam Center Code";
        }
    
    }
    protected void GridView2_PageIndexChangeing(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }
    protected void btnDisposeGrid_OnClick(object sender, EventArgs e)
    {
        clearGrid();
    }
    private void clearGrid()
    {
        DataTable dtdas = (DataTable)ViewState["dtbal"];
        dtdas.Clear();
        GridSeating.DataSource = null;
        GridSeating.DataBind();
        lbltef.Text = "0";
        lbltp1.Text = "0";
        lbltpII.Text = "0";
        lbltsA.Text = "0";
        lbltsB.Text = "0";
        lbltpp1.Text = "0";
        lbltpp2.Text = "0";
        lbltssA.Text = "0";
        lbltssB.Text = "0";
    }
    protected void ddlExaminationdate_DataBound(object sender, EventArgs e)
    {
        //if(IsPostBack)
        //ddlExaminationdate.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged2(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
}
