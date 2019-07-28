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

public partial class Exam_SeatingArrangement : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    private static List<int> idx;
    private static DataSet ds = new DataSet();
    private static DataTable dt1 = new DataTable();
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
                   
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    txtExamCode.Focus();
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
    protected void ddlExamSeason_SelectedIndexChanged2(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        grdRoomCapacity.DataBind();
        txtYearSeason.Focus();
    }
    protected void ddlShift_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btnSort.Visible=true;
            txtExamCode.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        grdRoomCapacity.DataBind();
    }
    protected void GridExamCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridExamCenter.SelectedRow;
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        txtExamCode.Text = gr.Cells[1].Text.ToString();
        lblCity.Text = gr.Cells[3].Text.ToString();
        con.Open();
        roomcapacity();
        con.Close();
        txtExamCode.Focus();
    }
    protected void GridView2_PageIndexChangeing(object sender, GridViewPageEventArgs e)
    {
        GridExamCenter.PageIndex = e.NewPageIndex;
        GridExamCenter.DataBind();
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
            roomcapacity();
            con.Close();
            con.Dispose();
            grdRoomCapacity.Focus();
            //ddlExaminationdate.Focus();
        }
        catch (FormatException ex)
        {
            lblExceptionCode.Text = "Invalid Exam Center Code";
        }
    }
    private void roomcapacity()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select RoomNo,RoomName,Capacity,Columns from Rooms where ID='" + txtExamCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        grdRoomCapacity.DataSource = dt;
        grdRoomCapacity.DataBind();
    }
    protected void ddlExamDate_SelectedIndexChanged(object sneder, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        btnSort.Visible = true;
        ddlShift.Focus();
    }
    protected void ddlExaminationdate_DataBound(object sender, EventArgs e)
    {
        //if(IsPostBack)
        //ddlExaminationdate.Focus();
    }
    protected void GridExamSub_OnRowDateBound(object sender, GridViewRowEventArgs e)
    {
    }
    private void datastructure(DataTable dt1,DataSet ds)
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SubID");
        dtDatas.Columns.Add("No");
        lblToApp.Text = "0";
        for (int a = 0; a < dt1.Rows.Count; a++)
        {
            DataRow dr = dtDatas.NewRow();
            dr["SubID"] = dt1.Rows[a][0].ToString();
            dr["No"] = ds.Tables[a].Rows.Count.ToString();
            dtDatas.Rows.Add(dr);
            lblToApp.Text = (Convert.ToInt32(lblToApp.Text) + Convert.ToInt32(ds.Tables[a].Rows.Count)).ToString(); ;
        }
        grdexam.DataSource = dtDatas;
        grdexam.DataBind();
    }
    protected void btnSelectExamFrom_OnClick(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
       
        dt1.Clear();
            //SqlDataAdapter adp = new SqlDataAdapter("select subid,count(subid) from ExamForm inner join ExamForms on ExamForms.SN=ExamForm.SN where ExamForm.Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue, dtinfo) + "' and ExamForm.Shift='" + ddlShift.SelectedValue.ToString() + "' and ExamForms.ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and ExamForms.CenterCode='" + txtExamCode.Text.ToString() + "' and ExamForms.RollNo!='N/A' and ExamForms.RollNo not in(select RollNo from SeatingArrange where Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue, dtinfo) + "' and CenterCode='" + txtExamCode.Text + "' and Shift='" + ddlShift.Text + "') group by ExamForm.subid", con);
      //  SqlDataAdapter adp = new SqlDataAdapter("select subid,count(subid) as Count from ExamForm inner join ExamForms on ExamForms.SN=ExamForm.SN where ExamForm.Date='" + ddlExaminationdate.SelectedValue + "' and ExamForm.Shift='" + ddlShift.SelectedValue.ToString() + "' and ExamForms.ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and ExamForms.CenterCode='" + txtExamCode.Text.ToString() + "' and ExamForms.RollNo!='N/A' and ExamForms.RollNo not in(select RollNo from SeatingArrange where Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue, dtinfo) + "' and CenterCode='" + txtExamCode.Text + "' and Shift='" + ddlShift.Text + "') group by ExamForm.subid", con);
        SqlDataAdapter adp = new SqlDataAdapter("select subid,count(subid) as Count from ExamForm inner join ExamForms on ExamForms.SN=ExamForm.SN where ExamForm.Date='" + ddlExaminationdate.SelectedValue + "' and ExamForm.Shift='" + ddlShift.SelectedValue.ToString() + "' and ExamForms.ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and ExamForms.CenterCode='" + txtExamCode.Text.ToString() + "' and ExamForms.RollNo!='N/A'  group by ExamForm.subid order by Count(SubID) desc", con);
            adp.Fill(dt1);
            ViewState["dt1"] = dt1;
            if (dt1.Rows.Count > 0)
            {
                ds = new DataSet();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                  //SqlDataAdapter add = new SqlDataAdapter("select ExamForms.RollNo,ExamForm.SubID from ExamForm inner join ExamForms on ExamForms.SN=ExamForm.SN where ExamForm.subid='" + dt1.Rows[i][0].ToString() + "'  and ExamForms.ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and ExamForms.CenterCode='" + txtExamCode.Text.ToString() + "' and ExamForms.RollNo!='N/A' and ExamForms.RollNo not in(select RollNo from SeatingArrange where Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue, dtinfo) + "' and CenterCode='" + txtExamCode.Text + "' and Shift='" + ddlShift.Text + "') order by ExamForms.RollNo", con);
                    SqlDataAdapter add = new SqlDataAdapter("select ExamForms.RollNo,ExamForm.SubID from ExamForm inner join ExamForms on ExamForms.SN=ExamForm.SN where ExamForm.subid='" + dt1.Rows[i][0].ToString() + "'  and ExamForms.ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and ExamForms.CenterCode='" + txtExamCode.Text.ToString() + "' and ExamForms.RollNo!='N/A' and ExamForms.RollNo not in(select RollNo from SeatingArrange where Date='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue, dtinfo) + "' and CenterCode='" + txtExamCode.Text + "' and Shift='" + ddlShift.Text + "') order by ExamForms.RollNo", con);
                    DataTable dsdt = new DataTable();
                    add.Fill(ds.Tables.Add(i.ToString()));
                }
                if (idx != null)
                {
                    if (idx.Count > dt1.Rows.Count)
                        for (int i = 0; i < idx.Count - dt1.Rows.Count; i++)
                            ds.Tables.Add((dt1.Rows.Count + i + 1).ToString());
                }
                ViewState["ds"] = ds;
            }
            datastructure(dt1, ds);
            btnPrint.Visible = false;
            btnAddExamForm.Visible = false;
            if (btnSort.Visible == true)
            {
                btnSort.Visible = true;
                btnSort.Focus();
            }
            else
            {
                btnArrange.Visible = true;
                btnArrange.Focus();
            }
            pnlArrange.Visible = true;
            con.Dispose();
    } 
    int rowvalue, tblvalue, capacity, value, capacity2, j;
    protected void btnArrange_Click(object sender, EventArgs e)
    {
       
        grdArrange.DataBind(); lblerror.Text = "";

        if (lblroomno.Text != "" && checkseating() == false)
        {
            DataTable dt = new DataTable();
            for (int j = 1; j <= Convert.ToInt32(lblcolumn.Text); j++)   //Add Column
            {
                dt.Columns.Add(j.ToString());
            }
            for (int j = 1; j <= Convert.ToInt32(lblrow.Text); j++)  //Add Row
            {
                dt.Rows.Add();
            }
            DataRow dr = dt.NewRow();
            ViewState["dt"] = dt;
            capacity = Convert.ToInt32(lblrow.Text) * Convert.ToInt32(lblcolumn.Text);
            capacity2 = capacity;
            // Blank Arranged Column
            if (dt.Rows[1][1].ToString() == "")
            {
                tblvalue = 0; rowvalue = 1;
            }
            //  DataSet ds = (DataSet)ViewState["ds"];  // Exam Forms 
            // DataTable dt1 = (DataTable)ViewState["dt1"];  // subject Name and Count
            Arrange(ds);
            Arrange(ds);
            datastructure(dt1, ds);
            //lblToApp.Text = (Convert.ToInt32(lblToCP1.Text) + Convert.ToInt32(lblToCP2.Text) + Convert.ToInt32(lblToCS1.Text) + Convert.ToInt32(lblToCS2.Text) + Convert.ToInt32(lblToAP1.Text) + Convert.ToInt32(lblToAP2.Text) + Convert.ToInt32(lblToAS1.Text) + Convert.ToInt32(lblToAS2.Text)).ToString();
            btnSave.Enabled = true;  lblDate.Text = Convert.ToDateTime(ddlExaminationdate.SelectedValue).ToString("dd/MM/yyyy"); lblExamCenter.Text = lblCity.Text; lblpcentercode.Text = txtExamCode.Text;
            RoomNo();
            lblshift.Text = ddlShift.Text; lblheadsession.Text = lblSeasonHidden.Text;
            divArrange.Visible = true;
            btnArrange.Visible = false;
            btnSave.Visible = true; btnSave.Focus();
        }
        else
        {
            lblerror.Text = "Select Exam Form First";
            btnAddExamForm.Visible = true; btnAddExamForm.Focus();
        }
    }
    private void RoomNo()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select RoomName from Rooms where RoomNo='" + lblroomno.Text + "' and ID='" + txtExamCode.Text + "' and Season='" + lblSeasonHidden.Text + "'", con);
        string roomname = Convert.ToString(cmd.ExecuteScalar());
        lblRoomNeme.Text = roomname;
        con.Close();
    }
    protected void btnSort_Click(object sender, EventArgs e)
    {
        List<int> array = new List<int>();
       // DataSet ds =(DataSet)ViewState["ds"];
        for (int j = 0; j < ds.Tables.Count; j++)
        {
            array.Add(Convert.ToInt32(ds.Tables[j].Rows.Count));
            // add no of subject code count to array.
        }
        var sorted = array.Select((x, i) => new KeyValuePair<int, int>(x, i)).OrderByDescending(x => x.Key).ToList();
       idx = sorted.Select(x => x.Value).ToList();
       btnSort.Visible = false;
       btnArrange.Visible = true;
       btnArrange.Focus();
    }
     private void Arrange(DataSet ds)
    { 
                DataTable dt = (DataTable)ViewState["dt"];  // Row and Column table
                    DataTable dtcp1;
                    for (int k = 0; k < idx.Count; k++)
                    {
                        dtcp1 = ds.Tables[idx[k]];   // Subject wise datatable.

                        if (dtcp1.Rows.Count > 0 && capacity2 > 0)
                        {
                            for (int r = 0; r < capacity; r++)   //Add RollNo to Table
                            {
                                if (r < dtcp1.Rows.Count)
                                {
                                    if (tblvalue >= dt.Rows.Count)
                                    {
                                        j++; if (dt.Rows.Count % 2 == 0) { if (tblvalue == dt.Rows.Count) tblvalue = 1; else tblvalue = 0; }
                                        else { if (tblvalue == dt.Rows.Count) tblvalue = 0; else tblvalue = 1; }
                                    }
                                    if (j < Convert.ToInt32(lblcolumn.Text))
                                    {
                                        if ((tblvalue * j) <= capacity)
                                        {
                                            dt.Rows[tblvalue][j] = dtcp1.Rows[r][0].ToString() + " / " + dtcp1.Rows[r][1].ToString();
                                         //   ds.Tables[idx[k]].Rows[tblvalue].Delete();
                                            dt1.Rows[idx[k]]["Count"] = (Convert.ToInt32(dt1.Rows[idx[k]]["Count"]) - 1).ToString();
                                            tblvalue += rowvalue + 1; capacity2--; value = r;
                                        }
                                        else break;
                                    }
                                }
                            }
                            for (int z = value; z >= 0; z--) dtcp1.Rows.RemoveAt(z);
                            grdArrange.DataSource = dt;
                            grdArrange.DataBind();
                            check();
                        }
                    }
}
    private void check()
    {
     
        if (grdArrange.Rows.Count > 0)
        {
            if (grdArrange.Rows[0].Cells[0].Text == "&nbsp;")
            {
                rowvalue = 1; tblvalue = 0; j = 0;
            }
            else if (grdArrange.Rows[1].Cells[0].Text == "&nbsp;")
            {
                rowvalue = 1; tblvalue = 1; j = 0;
            }
            else
            {
                int row;

                for (int col = 0; col < Convert.ToInt32(lblcolumn.Text); col++)
                {
                    for (row = 0; row < grdArrange.Rows.Count; row++)
                    {
                        if (grdArrange.Rows[row].Cells[col].Text == "&nbsp;")
                        {
                            j = col; tblvalue = row;
                            break;
                        }
                    }
                    if (row < grdArrange.Rows.Count)
                    {
                        if (grdArrange.Rows[row].Cells[col].Text == "&nbsp;")
                        {
                            j = col; tblvalue = row;
                            break;
                        }
                    }
                }
            }
            
        }
    }
    private void datarollno()
    { 
        DataTable dtcp1 = new DataTable();
        dtcp1.Columns.Add("RollNo");
        dtcp1.Columns.Add("Code");
        ViewState["dtcp1"] = dtcp1;
   
        DataTable dtcp2 = new DataTable();
        dtcp2.Columns.Add("RollNo");
        dtcp2.Columns.Add("Code");
        ViewState["dtcp2"] = dtcp2;
   
        DataTable dtcs1 = new DataTable();
        dtcs1.Columns.Add("RollNo");
        dtcs1.Columns.Add("Code");
        ViewState["dtcs1"] = dtcs1;
   
        DataTable dtcs2 = new DataTable();
        dtcs2.Columns.Add("RollNo");
        dtcs2.Columns.Add("Code");
        ViewState["dtcs2"] = dtcs2;
   
        DataTable dtap1 = new DataTable();
        dtap1.Columns.Add("RollNo");
        dtap1.Columns.Add("Code");
        ViewState["dtap1"] = dtap1;
    
        DataTable dtap2 = new DataTable();
        dtap2.Columns.Add("RollNo");
        dtap2.Columns.Add("Code");
        ViewState["dtap2"] = dtap2;
   
        DataTable dtas1 = new DataTable();
        dtas1.Columns.Add("RollNo");
        dtas1.Columns.Add("Code");
        ViewState["dtas1"] = dtas1;
    
        DataTable dtas2 = new DataTable();
        dtas2.Columns.Add("RollNo");
        dtas2.Columns.Add("Code");
        ViewState["dtas2"] = dtas2;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Open();
        for (int i = 0; i < grdArrange.Rows.Count; i++)
        {
            for (int j = 0; j < Convert.ToInt32(lblcolumn.Text); j++)
            { string rollno,subcode;
            char[] MyChar = { '0','1','2','3','4','5','6','7','8','9','0',' ','/'};
            char[] MyChar2 = {' ', '/' };
            if (grdArrange.Rows[i].Cells[j].Text == "&nbsp;") { rollno = ""; subcode = ""; }
            else { rollno = grdArrange.Rows[i].Cells[j].Text.Substring(0, 7).TrimEnd(MyChar2).ToString(); subcode = grdArrange.Rows[i].Cells[j].Text.TrimStart(MyChar).ToString(); 
                SqlCommand cmd = new SqlCommand("insert into seatingarrange (CenterCode,RoomNo,RollNo,SubCode,Row,Date,Shift,Session,ColumnNo,RoomName) values('" + txtExamCode.Text + "','" + lblroomno.Text + "','" +rollno  + "','" + subcode + "','" + i + "','" + Convert.ToDateTime(ddlExaminationdate.SelectedValue,dtinfo) + "','" + ddlShift.SelectedValue + "','" + lblSeasonHidden.Text + "','" + j + "','"+lblRoomNeme.Text+"')", con);
                cmd.ExecuteNonQuery();
                 }
            }
        } con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Saved')", true);
         btnSave.Visible = false;
        con.Dispose();
        btnPrint.Visible = true;
        btnPrint.Focus();
    }
    protected void grdRoomCapacity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        gr = grdRoomCapacity.SelectedRow;
        lblcapacity.Text = gr.Cells[3].Text.ToString();
        lblroomno.Text = gr.Cells[1].Text.ToString();
        lblcolumn.Text = gr.Cells[4].Text.ToString();
        lblrow.Text = (Convert.ToInt32(lblcapacity.Text) / Convert.ToInt32(lblcolumn.Text)).ToString();
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select distinct(RoomNo) from SeatingArrange where Date='" +Convert.ToDateTime( ddlExaminationdate.SelectedValue,dtinfo) + "' and CenterCode='" + txtExamCode.Text + "' and Shift='" + ddlShift.Text + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        bool bl = false;
        while (reader.Read())
        {
            if (reader["RoomNo"].ToString() == grdRoomCapacity.SelectedRow.Cells[1].Text.ToString())
            {
                bl = true;
                break;
            }
        }
        if(bl==true)
        {
            lblExceptionCode.Text = "Already Arranged.";
            btnAddExamForm.Visible = false;
            grdRoomCapacity.Focus();
        }
        else 
        {
            lblExceptionCode.Text = "";
            btnAddExamForm.Visible = true;
            btnAddExamForm.Focus();
        }
        con.Close(); con.Dispose();
    }
    private bool checkseating()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select CenterCode from seatingarrange where CenterCode='" + txtExamCode.Text + "' and date='" + ddlExaminationdate.SelectedValue + "' and RoomNo='"+lblroomno.Text+"' and Shift='"+ddlShift.SelectedValue.ToString()+"'", con);
        string chk1 = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        if (chk1 == "") return false;
        else return true;
    }
    private void data()
    {
        DataTable dtmain = new DataTable();
        dtmain.Columns.Add("RollNo");
        dtmain.Columns.Add("Course");
        dtmain.Columns.Add("Part");
        dtmain.Columns.Add("Code");
        ViewState["dtmain"] = dtmain;
    }
}