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

public partial class Acc_EditMainAC : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            string IMID = Request.QueryString["acid"];
            string DNo = Request.QueryString["DNo"];
            if (!IsPostBack)
            {
                ClsEdit clEdit = new ClsEdit();
                string[] stredit = clEdit.EditCount("DDEntry");
               lblEditCount.Text = stredit[0].ToString();
                if (stredit[1] == "False") pnlEdit.Enabled = false;
                else pnlEdit.Enabled = true;
                btnSubmitAmt.Enabled = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                if (IMID != null || DNo != null)
                {
                    txtIDIM.Text = IMID;
                    txtDiaryNo.Text = DNo;
                    View();
                }
                else
                {
                    txtIDIM.Text = "";
                    txtDiaryNo.Text = "";
                }
                lblDDNNO.Text = "DD No:";
                lblAccountNo.Text = "Diary No:";
                pnlIMInfo.Visible = false;
                txtDDNO.Visible = true; txtDiaryNo.Visible = true;
                totalamt.Visible = false; panelCourier.Visible = false; 
                ddlsession.Focus();
                LoadDropdownList();
            }
        }
        catch (NullReferenceException ex)
        {
            txtIDIM.Enabled = false;
            Response.Redirect("../Login.aspx");
        }
        finally
        {
        }
    }
    private void LoadDropdownList()
    {
        try
        {
            DataSet dsHeader = new DataSet();
            ddlAmtForMs.Items.Add("-- Select --");
            dsHeader.ReadXml(Server.MapPath("~/XML/AmountHeader.xml"));
            ddlAmtForMs.DataSource = dsHeader;
            ddlAmtForMs.DataTextField = "Aname";
            ddlAmtForMs.DataValueField = "Aname";
            ddlAmtForMs.DataBind();
            //ddlAmountHeader.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlAmtForMs.DataSource = dsHeader;
        }
        catch (Exception ex)
        {
            throw ex;
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
            int lvl = mk.returnlevel(Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"])), Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])));
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
    protected void txtAmt_textChanged(object sender, EventArgs e)
    {
        totalamt.Visible = true;
        if (txtAmt.Text == "") txtAmt.Text = "0";
        lblTAmt.Text = txtAmt.Text.ToString();
        lblTAmt.Visible = true;
        btnSubmitAmt.Focus();
    }
    protected void txtIDIM_TextChanged(object sender, EventArgs e)
    {
        txtIMID();
        con.Dispose();
        GridDiaryNo.Focus();
    }
    private void txtIMID()
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + txtIDIM.Text.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        int i = 0;
        if (chk == txtIDIM.Text.ToString())
        {
            i += 1;
        }
        else
        {
            txtIDIM.Text = "Invalid ID"; lblIMAddress.Text = ""; lblIMName.Text = "";
            lblIMCity.Text = "Please Insert Valid IM ID."; pnlIMInfo.Visible = false;
            txtIDIM.Focus();
        }
        if (i == 1)
        {
            cmd = new SqlCommand("select * from IM where ID='" + txtIDIM.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblIMName.Text = reader[1].ToString();
                lblIMAddress.Text = reader[3].ToString();
                lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
                lblEnrolment.Text = reader["ID"].ToString();
                lblGroupID.Text = reader["GID"].ToString();
                pnlIMInfo.Visible = true;
            }
            reader.Close();
            txtDiaryNo.Text = ""; lblexceptionDirary.Text = "";
            SqlDataAdapter addiary = new SqlDataAdapter("select Distinct DiaryNo From DiaryEntry where IMID='" + txtIDIM.Text.ToString() + "' and ExamSession='" + lblhiddenSession.Text.ToString() + "' and Status='Open' order by DiaryNo desc", con);
            DataSet ds = new DataSet();
            addiary.Fill(ds);
            if (ds.Tables[0].ToString() != "")
            {
                GridDiaryNo.DataSource = ds;
                GridDiaryNo.DataBind();
            }
            txtDiaryNo.Focus();
        }
        con.Close();
    }
    protected void ddlAmtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanBank.Visible = true;

        if (ddlAmtType.SelectedValue == "DD")
        {
            lblDDNNO.Text = "DD No:";
            lblAccountNo.Text = "Diary No:";
            txtDDNO.Visible = true; txtDiaryNo.Visible = true; txtDDNO.Focus();
        }
        else if (ddlAmtType.SelectedValue == "Cash")
        {
            lblDDNNO.Text = ""; txtDDNO.Text = "";
            txtDDNO.Visible = false; txtDDNO.Text = "Cash";
            ddlBank.Focus();
            PanBank.Visible = false;
        }
        else if (ddlAmtType.SelectedValue == "CC")
        {
            lblDDNNO.Text = "Chaque No."; txtDDNO.Visible = true; txtDDNO.Text = "";
            lblAccountNo.Text = "Diary No:"; txtDiaryNo.Visible = true; txtDDNO.Focus();
        }
    }
    public int tice, gice, icelate; 
    public int tamt, amt, gamt, dues;
    string type;
    protected void btnSubmitAmt_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        SqlTransaction sTR;
        sTR = con.BeginTransaction();
        cmd.Connection = con;
        int dif = 0;
        bool flag=false;
        cmd.Transaction = sTR;
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            ClsEdit clEdit = new ClsEdit();
            clEdit.CountUp("DDEntry");          
            //cmd.CommandText = "select * from IMAC where IMID='"+txtIDIM.Text+"'";
            //SqlDataReader read = cmd.ExecuteReader();
            //while (read.Read())
            //{
            //    tamt = Convert.ToInt32(read["Total"].ToString().TrimEnd('0').TrimEnd('.'));
            //    gamt = Convert.ToInt32(read["GTotal"].ToString().TrimEnd('0').TrimEnd('.'));
            //    dues = Convert.ToInt32(read["Credit"].ToString().TrimEnd('0').TrimEnd('.'));  
            //}
            //read.Close();
            int oamt= Convert.ToInt32(GridAppTable.SelectedRow.Cells[2].Text);
            int namt = Convert.ToInt32(txtAmt.Text);
            if (namt >= oamt)
            {
                dif = namt - oamt;
                lblDiaryAmount.Text = (Convert.ToInt32(lblDiaryAmount.Text) + dif).ToString();
                flag=true;
            }
            else
            {   
                dif = oamt - namt;
                lblDiaryAmount.Text = (Convert.ToInt32(lblDiaryAmount.Text) - dif).ToString();
                flag=false;
            }
            cmd.CommandText = "select SN from Account where Session='" + ddlsession.SelectedValue + txtSession.Text + "' and IMID='" + txtIDIM.Text + "' and DiaryNo='" + GridAppTable.SelectedRow.Cells[1].Text.ToString() + "' and Details='" + GridAppTable.SelectedRow.Cells[3].Text.ToString()+":" + GridAppTable.SelectedRow.Cells[5].Text.ToString() + "'";
            int Sn = Convert.ToInt32(cmd.ExecuteScalar());
            ClsAccount c2 =new ClsAccount();
            c2.AmountUpdate(Sn,txtAmt.Text , cmd, ddlBank.SelectedItem.Text ,txtDDNO.Text);           
            //ClsAccount cl = new ClsAccount();
            //cl.AmountSubmit(txtIDIM.Text.ToString(), txtDiaryNo.Text.ToString(), Convert.ToDateTime(txtDOB.Text, dtinfo), type, amt.ToString(), lblhiddenSession.Text.ToString(), "DD Edit",sTR,cmd);
            cmd.CommandText = "Update DiaryEntry set Amount='" + lblDiaryAmount.Text + "' where DiaryNo='" + txtDiaryNo.Text.ToString() + "'";
            cmd.ExecuteNonQuery();
                cmd.CommandText = "update FeeAC set IMID=@IMID,Amt=@Amt,AmtType=@AmtType,AmtFor=@AmtFor,SubDate=@SubDate,DDDate=@DDDate,DDNO=@DDNO,Bank=@Bank,Narration=@Narration,Session=@Session,DiaryNo=@DiaryNo where DDNO='" + GridAppTable.SelectedRow.Cells[3].Text.ToString() + "' and IMID='"+txtIDIM.Text+"' and DiaryNo='" + txtDiaryNo.Text + "'";
                cmd.Parameters.AddWithValue("@IMID", lblEnrolment.Text.ToString());
                cmd.Parameters.AddWithValue("@Amt",namt.ToString());
                cmd.Parameters.AddWithValue("@AmtType", ddlAmtType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@AmtFor", ddlAmtForMs.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SubDate",Convert.ToDateTime(txtDate.Text,dtinfo));
                cmd.Parameters.AddWithValue("@DDDate", Convert.ToDateTime(txtDOB.Text, dtinfo));
                cmd.Parameters.AddWithValue("@DDNO", txtDDNO.Text.ToString());
                cmd.Parameters.AddWithValue("@Bank", ddlBank.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Narration", txtNarration.Text.ToString());       
                cmd.Parameters.AddWithValue("@Session", lblhiddenSession.Text.ToString());
                cmd.Parameters.AddWithValue("@DiaryNo", txtDiaryNo.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Update IMAccount set Amount=Amount-'" + oamt + "' where IMID='" + lblEnrolment.Text + "' and Fees='" + GridAppTable.SelectedRow.Cells[8].Text.ToString() + "'";
                cmd.ExecuteNonQuery();
                   cmd.CommandText = "Update IMAccount set Amount=Amount+'" + namt + "' where IMID='" + lblEnrolment.Text + "' and Fees='" +ddlAmtForMs.SelectedValue.ToString() + "'";
                   cmd.ExecuteNonQuery();
                //cmd.CommandText = "update IMAC set Total='" + tamt.ToString() + "',Gtotal='" + gamt.ToString() + "',Credit='" + dues.ToString() + "' where IMID='" + txtIDIM.Text + "'";
                //cmd.ExecuteNonQuery();
                sTR.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Sucessfully Submitted')", true);
                lblException.Text = lblTAmt.Text.ToString() + " Rs. is saved in IM: " + lblEnrolment.Text.ToString();
                string[] stredit = clEdit.EditCount("DDEntry");
                lblEditCount.Text = stredit[0].ToString();
                if (stredit[1] == "False") pnlEdit.Enabled = false;
                else pnlEdit.Enabled = true;
        }
        catch (SqlException ex)
        {
            sTR.Rollback();
            lblException.Text = ex.ToString();
        }
        catch (Exception ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            View();
            con.Close();
            con.Dispose();
            txtDDNO.Text = "";
            txtNarration.Text = "";
            txtAmt.Text = "";
        }
        btnSubmitAmt.Enabled = false;
        lblEdit.Visible = true;
    }
    protected void txtDiaryNo_TextChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd8 = new SqlCommand("select IMID from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "' and ExamSession='" + lblhiddenSession.Text.ToString() + "'", con);
        string damount = Convert.ToString(cmd8.ExecuteScalar());
        if (damount.ToString() == "")
        {
            lblexceptionDirary.Text = "Invalid Diary No: " + txtDiaryNo.Text.ToString();
            btnSubmitAmt.Enabled = false; txtDiaryNo.Text = "";
            txtDiaryNo.Focus();
        }
        else
        {
            if (txtIDIM.Text == damount.ToString())
            {
                lblException.Text = "";
                lblexceptionDirary.Text = "";
                ddlAmtType.Focus();
            }
            else
            {
                lblexceptionDirary.Text = "Invalid Diary No:" + txtDiaryNo.Text.ToString() + "  for IMID: " + txtIDIM.Text.ToString();
                txtDiaryNo.Text = "";
                txtDiaryNo.Focus();
            }
        }
        con.Close();
        con.Dispose();
        btnViewDiray.Focus();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        Response.Redirect(url.ToString());
    }
    private int[] chkdob(DateTime now, DateTime dt)
    {
        int[] dif = new int[3];
        int mo, dy;
        if (dt.Year == now.Year & now.Month == dt.Month & dt.Day > now.Day)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else if (dt.Year == now.Year & dt.Month > now.Month)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else if (dt.Year > now.Year)
        {
            dif[0] = 0;
            dif[1] = 0;
            dif[2] = 100;
        }
        else
        {
            int yr = now.Year - dt.Year;
            if (now.Month < dt.Month || now.Month == dt.Month && now.Day < dt.Day)
            {
                --yr;
            }
            dif[0] = yr;
            if (now.Month < dt.Month)
            {
                mo = (12 - dt.Month) + now.Month;
                if (now.Day < dt.Day)
                    --mo;
            }
            else
            {
                mo = now.Month - dt.Month;
                if (now.Month == dt.Month & now.Day < dt.Day)
                {
                    --mo;
                }
            }
            dif[1] = mo;
            if (now.Day < dt.Day)
            {
                if (now.Month == 1)
                {
                    int ddy = DateTime.DaysInMonth(now.Year, now.Month);
                    dy = (ddy - dt.Day) + now.Day;
                }
                else
                {
                    int ddy = DateTime.DaysInMonth(now.Year, now.Month - 1);
                    dy = (ddy - dt.Day) + now.Day;
                }
            }
            else
            {
                dy = now.Day - dt.Day;
            }
            dif[2] = dy;
        }
        return dif;
    }
    
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtDiaryNo.Text = ""; txtSession.Focus();
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = "";
        lblhiddenSession.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        GridDiaryNo.Focus();
    }
    protected void ibtnNewCourier_Onclick(object sender, EventArgs e)
    {
        panelCourier.Visible = true;
        txtNewCourier.Focus();
    }
    protected void btnSAveNew_Onclick(object sender, EventArgs e)
    {
        if (txtNewCourier.Text == "")
        {
            lblExceptionNewCourier.Text = "Please Insert Bank Name.";
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ServiceNameMaster(Name,City,Type) values(@Name,@City,@Type)", con);
            cmd.Parameters.AddWithValue("@Name", txtNewCourier.Text.ToString());
            cmd.Parameters.AddWithValue("@City", txtNewCity.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", "Bank");
            cmd.ExecuteNonQuery();
            lblExceptionNewCourier.Text = "Successfull Saved New Bank Name";
            con.Close();
            con.Dispose();
        }
        btnCencel.Focus();
    }
    protected void btnCencelnew_Onclick(object sender, EventArgs e)
    {
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }
    protected void GridDiaryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDiaryNo.Text = GridDiaryNo.SelectedRow.Cells[1].Text.ToString();
        txtDiaryNo.Focus();
    }
    protected void btnViewDiary_OnClick(object sendr, EventArgs e)
    {
        View();
        getDiaryAmount();
        if (GridAppTable.Rows.Count >0)
        {
            GridAppTable.Focus();
        }
        else           
        ddlAmtType.Focus();
    }
    private void getDiaryAmount()
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select Amount from DiaryEntry where DiaryNo='" + txtDiaryNo.Text.ToString() + "'", con);
        lblDiaryAmount.Text = Convert.ToString(cmd.ExecuteScalar());
        lblDiaryAmount.Text = lblDiaryAmount.Text.ToString().TrimEnd('0').TrimEnd('.');
        con.Close();
    }
    private void View()
    {
        SqlDataAdapter adp;
        if (txtDDSearch.Text == "")
            adp = new SqlDataAdapter("select DiaryNo,Amt,DDNO,SubDate,Bank,Narration,DDDate,AmtFor from FeeAC where Session='" + lblhiddenSession.Text.ToString() + "' and DiaryNo='" + txtDiaryNo.Text.ToString() + "'  order by SubDate Desc", con);
        else
            adp = new SqlDataAdapter("select DiaryNo,Amt,DDNO,SubDate,Bank,Narration,DDDate,AmtFor from FeeAC where Session='" + lblhiddenSession.Text.ToString() + "' and DiaryNo='" + txtDiaryNo.Text.ToString() + "' and DDNO='"+txtDDSearch.Text.ToString()+"'  order by SubDate Desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridAppTable.DataSource = dt;
        GridAppTable.DataBind();
    }
    protected void GridAppTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmt.Text=GridAppTable.SelectedRow.Cells[2].Text.ToString();
        txtDDNO.Text=GridAppTable.SelectedRow.Cells[3].Text.ToString();
        txtDate.Text=GridAppTable.SelectedRow.Cells[4].Text.ToString();
        ddlBank.SelectedItem.Text = GridAppTable.SelectedRow.Cells[5].Text.ToString();
        txtNarration.Text=GridAppTable.SelectedRow.Cells[6].Text.ToString();
        txtDOB.Text = GridAppTable.SelectedRow.Cells[7].Text.ToString();
        try
        {
            ddlAmtForMs.SelectedValue = GridAppTable.SelectedRow.Cells[8].Text.ToString();
        }
        catch (Exception ex)
        {
            ddlAmtForMs.SelectedItem.Text = GridAppTable.SelectedRow.Cells[8].Text.ToString();
        }
        lblEdit.Visible = false;
        btnSubmitAmt.Enabled = true;
        ddlAmtType.Focus();
    }
    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text == "&nbsp;" || e.Row.Cells[6].Text=="")
            {
                e.Row.Cells[6].Text = "";
            }
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.TrimEnd('0').TrimEnd('.');
            if (e.Row.Cells[4].Text == "&nbsp;" )
            {
                e.Row.Cells[4].Text = "";
            }
            else e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MM/yyyy");
            if(e.Row.Cells[7].Text == "&nbsp;")
            {
                e.Row.Cells[7].Text = "";
            }
            else
            {
                e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).ToString("dd/MM/yyyy");
            }
        }
    }
    protected void txtDate_TechChanged(object sender, EventArgs e)
    {
        try
        {
            DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            int[] diff = new int[3];
            DateTime dt = Convert.ToDateTime(txtDOB.Text, dtinfo);
            DateTime now = Convert.ToDateTime(txtDate.Text, dtinfo);
            diff = chkdob(now, dt);
            if (diff[0] == 0 & diff[1] == 0 & diff[2] == 100)
            {
                lblExcedate.Text = "DD Date should be earlier than Current Date.";
                btnSubmitAmt.Enabled = false;
            }
            else
            {
                double dev = now.Subtract(dt).TotalDays;
                if (dev > 80)
                {
                    btnSubmitAmt.Enabled = false;
                    lblExceptiondAte.Text = "Total No of Days: " + dev.ToString();
                }
                else
                {
                    btnSubmitAmt.Enabled = true;
                    lblExceptiondAte.Text = "Total No of Days: " + dev.ToString();
                }
            }
            if (ddlAmtType.SelectedValue.ToString() != "DD")
            {
                btnSubmitAmt.Enabled = true;
            }

        }
        catch (FormatException ex)
        {
            lblExcedate.Text = "Invalid Submission Date Format.";
        }
        txtNarration.Focus();
    }
}
