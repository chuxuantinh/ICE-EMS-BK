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

public partial class project_DiaryReceive : System.Web.UI.Page
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
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
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
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
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
        SqlDataAdapter addiary,addiary1;
        // B7
        addiary = new SqlDataAdapter("select DiaryEntry.DiaryNo,DiaryEntry.Diary,DiaryEntry.Status,DiaryEntry.CourierNo as RefNo,DiaryEntry.IMID as Membership from DiaryEntry where SubmittedTo='Project'and Status='ProReceive' and DiaryNo in (select DairyNo from DairyCount where Department='Project') and ExamSession='" + lblHiddenSeason.Text + "'", con);
        addiary1 = new SqlDataAdapter("select DiaryEntry.DiaryNo,DiaryEntry.Diary,DiaryEntry.Status,DiaryEntry.CourierNo as RefNo,DiaryEntry.IMID as Membership from DiaryEntry where SubmittedTo='Project'and Status='CountDispatch' and DiaryNo in (select DairyNo from DairyCount where Department='Account') and ExamSession='" + lblHiddenSeason.Text + "'", con);
        DataTable ds = new DataTable();
        addiary.Fill(ds);
        addiary1.Fill(ds);
        GridDiaryNo.DataSource = ds; 
        GridDiaryNo.DataBind();
        //B6
        addiary = new SqlDataAdapter("select DairyNo,Status,IMID from DairyCount where DairyNo IN (select Distinct DiaryNo From DiaryEntry where Status='CountDispatch' and ExamSession='" + lblHiddenSeason.Text.ToString() + "') and Department='Project' order by DairyNo desc", con);
        DataTable dt = new DataTable();
        addiary.Fill(dt);
        GrdCountDispatch.DataSource = dt; 
        GrdCountDispatch.DataBind();
        //B8
        addiary = new SqlDataAdapter("select Status,DairyNo,IMID,DDRcv,DDSub,ProformaARcv,ProformaASub,ProformaBRcv,ProformaBSub,CurrentDate from Projectcount where Session='"+lblHiddenSeason.Text+"' and dairyNo in(select DiaryNo from DiaryEntry where SubmittedTo='Project') order by DairyNo desc", con);
        DataTable dt1 = new DataTable();
        addiary.Fill(dt1);
        GridProject.DataSource = dt1; 
        GridProject.DataBind();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        bindDiary(); txtDiaryNo.Text = ""; txtDiaryNo.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        bindDiary();  txtDiaryNo.Text = ""; txtYearSeason.Focus();
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
                cmd = new SqlCommand("update DiaryEntry set Status='ProReceive' where DiaryNo='" + GrdCountDispatch.Rows[i].Cells[1].Text + "'", con);
                cmd.ExecuteNonQuery();
            }
            i++;
        }
        bindDiary(); con.Close(); con.Dispose();
    }
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;rw=GridDiaryNo.SelectedRow;
        txtDiaryNo.Text = rw.Cells[1].Text;
        txtIMID.Text = rw.Cells[5].Text.ToString();
        lblRcv.Visible = true;
    }
    protected void GridDiaryNo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
            if (e.Row.Cells[3].Text == "CountDispatch") { e.Row.Cells[0].Text = "Supplied"; e.Row.Cells[0].ForeColor = System.Drawing.Color.Red; }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select IMID,DDRcv,EmpName,ProformaARcv,ProformaBRcv from ProjectCount where DairyNo='" + txtDiaryNo.Text + "'", con);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read())
        {
            txtIMID.Text = rd["IMID"].ToString();
            txtPDD.Text = rd["DDRcv"].ToString();
            txtProformaC.Text = rd["EmpName"].ToString();
            txtProformaA.Text = rd["ProformaARcv"].ToString();
            txtProformaB.Text = rd["ProformaBRcv"].ToString();
        }
        rd.Close(); con.Close(); con.Dispose();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblExcepiton.Text = "";
        if (txtPDD.Text == "") txtPDD.Text = "0"; if (txtProformaA.Text == "") txtProformaA.Text = "0";if (txtProformaB.Text == "") txtProformaB.Text = "0";
        con.Close(); con.Open();
        if (txtDiaryNo.Text != "")
        {
                cmd = new SqlCommand("update DairyCount set Status='Open',TotalDDRcv='" + txtPDD.Text + "',TotalNoForm='" + (Convert.ToInt32(txtProformaA.Text) + Convert.ToInt32(txtProformaB.Text) + Convert.ToInt32(txtProformaC.Text)) + "' where DairyNo='" + txtDiaryNo.Text + "'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update ProjectCount set EmpName='" + txtProformaC.Text + "', DDRcv='" + txtPDD.Text + "',ProformaARcv='" + txtProformaA.Text + "',ProformaBRcv='" + txtProformaB.Text + "' where DairyNo='" + txtDiaryNo.Text + "'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update DiaryEntry set Status='ProSubmit' where DiaryNo='" + txtDiaryNo.Text + "'", con);
                cmd.ExecuteNonQuery();
                bindDiary();
                lblExcepiton.Text = "Project Count Submitted";
            }
        
        con.Close(); con.Dispose(); btnSupply.Focus();
    }
    protected void btnSupply_Click(object sender, EventArgs e)
    {
        lblExcepiton.Text = "";
        if (txtPDD.Text == "") txtPDD.Text = "0"; if (txtProformaC.Text == "") txtProformaC.Text = "0"; if (txtProformaA.Text == "") txtProformaA.Text = "0"; if (txtProformaB.Text == "") txtProformaB.Text = "0";
        con.Close(); con.Open();
        if (txtDiaryNo.Text != "")
        {
            if (txtIMID.Text == "N/A" || txtIMID.Text == "" || txtIMID.Text == "N/A")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please submit IMID No.')", true);
            }
            else
            {
                cmd = new SqlCommand("update DairyCount set IMId='"+txtIMID.Text.ToString()+"', Status='Open',Department='Account',TotalDDRcv='" + txtPDD.Text + "',TotalNoForm='" + (Convert.ToInt32(txtProformaA.Text) + Convert.ToInt32(txtProformaB.Text) + Convert.ToInt32(txtProformaC.Text)) + "' where DairyNo='" + txtDiaryNo.Text + "'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update ProjectCount set IMId='" + txtIMID.Text.ToString() + "', EmpName='" + txtProformaC.Text + "', DDRcv='" + txtPDD.Text + "',ProformaARcv='" + txtProformaA.Text + "',ProformaBRcv='" + txtProformaB.Text + "' where DairyNo='" + txtDiaryNo.Text + "'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update DiaryEntry set IMId='" + txtIMID.Text.ToString() + "', Status='Open' where DiaryNo='" + txtDiaryNo.Text + "'", con);
                cmd.ExecuteNonQuery();
                bindDiary();
                clear();
                lblExcepiton.Text = "Supplied to Account";
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Diary Not found.')", true);
            lblExcepiton.Text = "Diary Not Found!";
        }
        con.Close(); con.Dispose();
    }
    private void clear()
    {
        txtPDD.Text = ""; txtProformaA.Text = ""; txtProformaB.Text = ""; txtProformaC.Text = "0"; txtDiaryNo.Text = ""; lblSubDate.Text = ""; lblRcv.Text = ""; 
    }
    protected void GridProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToString("dd/MM/yyyy");
        }
        lblDiary.Text = GridProject.Rows.Count.ToString();
    }
}