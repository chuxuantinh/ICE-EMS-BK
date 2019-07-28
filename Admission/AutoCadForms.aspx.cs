using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Admission_AutoCadForms : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd;
    SqlDataAdapter adp;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    ClsAutoCAD AutoCAD = new ClsAutoCAD();
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
                BindGrid(); bindbatch();
                txtDAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnReg.Enabled = false;
                btnSubmit.Enabled = false;
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            maikal m = new maikal();
            int lvl = m.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        adp = new SqlDataAdapter("select AppNo, Enrolment as Membership,IMID,Course,Part,Session,Amount,FormType,CAD from AppRecord where  Status!='NotApproved' and Status!='Hold' and Status!='Filled' and CAD='" + txtAppNo.Text.ToString() + "' and (FormType='MCADRegistration' or FormType='MCADLateFee') order by CAD", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridAutoCad.DataSource = dt;
        GridAutoCad.DataBind();
    }
    private void BindGrid()
    {
        adp = new SqlDataAdapter("select AppNo, Enrolment as Membership,IMID,Course,Part,Session,Amount,FormType,CAD from AppRecord where Status!='NotApproved' and Status!='Hold' and Status!='Filled' and (FormType='MCADRegistration' or FormType='MCADLateFee') order by CAD", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridAutoCad.DataSource = dt;
        GridAutoCad.DataBind();
    }
    protected void GridAutoCad_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlspc.Visible = false;
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Close(); con.Open();
        SqlDataReader dr;
        string strSelect = GridAutoCad.SelectedRow.Cells[8].Text.ToString();
        cmd = new SqlCommand("select Batch_ID,Status,RegDate,RegNo from MCAD where SID='" + GridAutoCad.SelectedRow.Cells[2].Text.ToString() + "' and CurrentStatus='Current'", con);
        dr = cmd.ExecuteReader();
        bool flag = false;
        if (dr.Read())
        {
            lblStBatch.Text = dr["Batch_ID"].ToString();
            lblStatus.Text = dr["Status"].ToString();
            lblRegistrationDAte.Text = dr["RegDate"].ToString();
            if (lblRegistrationDAte.Text != "")
            {
                lblRegistrationDAte.Text = Convert.ToDateTime(dr["RegDate"].ToString()).ToString("dd/MM/yyyy");
            }
            lblRegNo.Text = dr["RegNo"].ToString();
            flag = true;
        }
        dr.Close();
        if (flag == false)
        {
            lblStBatch.Text = AutoCAD.currentBatch(con).ToString();
            lblMonthDif.Text = "0";
            lblRegNo.Text = RegTemp();
            lblRegistrationDAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblStatus.Text = "Current";
            btnSubmit.Enabled = false;
            btnReg.Enabled = true;
        }
        else
        {
            DateTime dt = Convert.ToDateTime(lblRegistrationDAte.Text, dtinfo);
            DateDifference dtd = new DateDifference(Convert.ToDateTime(txtDAte.Text, dtinfo), dt);
            decimal dif = Convert.ToDecimal(dtd.Months + "." + dtd.Days);
            lblMonthDif.Text = dtd.Months.ToString() + " Months " + dtd.Days + " Days.";
            if (dif > 6)
            {
                lblException.Text = "Registration Period  (6th Months) Expired";
            }
            if (strSelect == "MCADLateFee")
            {
                lblFeeType.Text = "LateFee";
                btnReg.Enabled = false; btnSubmit.Enabled = true;
            }
            else
            {
                //if (lblStatus.Text.ToString() == "LateFee")
                //{
                    //check Registration Date <=6
                    if (dif <= 6)
                    {
                        lblFeeType.Text = "Re-Registered";
                        btnReg.Enabled = false; btnSubmit.Enabled = true;
                    }
                    else
                    {
                        lblFeeType.Text = "Registered";
                        btnReg.Enabled = true; btnSubmit.Enabled = false;
                        lblException.Text = "Registration Period (6th Months) Expired";
                    }
                //}
                //else
                //{
                //    lblFeeType.Text = "Re-Registered";
                //    btnReg.Enabled = true; btnSubmit.Enabled = true;
                //}
            }
        }
        cmd = new SqlCommand("select Name,Mobile,Email,DOB from Student where SID='" + GridAutoCad.SelectedRow.Cells[2].Text.ToString() + "'", con);
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            txtName.Text = dr["Name"].ToString();
            txtMobile.Text = dr["Mobile"].ToString();
            txtEmail.Text = dr["Email"].ToString();
            txtDOB.Text =Convert.ToDateTime(dr["DOB"].ToString()).ToString("dd/MM/yyyy");
        }
        dr.Close(); con.Close(); pnlAuto.Visible = true; con.Dispose();
    } 
    protected void btnReg_Click(object sender, EventArgs e)
    {
        try
        {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Close(); con.Open();
            cmd = new SqlCommand("update MCAD set CurrentStatus='Old' where SID='" + GridAutoCad.SelectedRow.Cells[2].Text + "'", con);
            cmd.ExecuteNonQuery(); con.Close(); con.Open();
            cmd = new SqlCommand("insert into MCAD(SID,Name,Mobile,Email,Status,CurrentStatus,RegNo,Course,Part,Batch_ID) values(@SID,@Name,@Mobile,@Email,@Status,@CurrentStatus,@RegNo,@Course,@Part,@Batch_ID)", con);
            cmd.Parameters.AddWithValue("@SID", GridAutoCad.SelectedRow.Cells[2].Text);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@Status", "Registered");
            cmd.Parameters.AddWithValue("@CurrentStatus", "Current");
            cmd.Parameters.AddWithValue("@RegNo", lblRegNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Course", GridAutoCad.SelectedRow.Cells[4].Text.ToString());
            cmd.Parameters.AddWithValue("@Part", GridAutoCad.SelectedRow.Cells[5].Text.ToString());
            cmd.Parameters.AddWithValue("@Batch_ID",ddlBatchNo.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            feeinsert(GridAutoCad.SelectedRow.Cells[2].Text.ToString(), lblFeeType.Text, GridAutoCad.SelectedRow.Cells[7].Text, lblRegNo.Text.ToString(), con);
            cmd = new SqlCommand("update AppRecord set Status=@Status where Appno='"+GridAutoCad.SelectedRow.Cells[1].Text.ToString()+"'", con);
            cmd.Parameters.AddWithValue("Status", "Filled");
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + lblRegNo.Text.ToString() + " M-Cad Registration Successfully Registererd.')", true);
            con.Close(); btnReg.Enabled = false;
          }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Dispose();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            feeinsert(GridAutoCad.SelectedRow.Cells[2].Text.ToString(), lblFeeType.Text, GridAutoCad.SelectedRow.Cells[7].Text, lblRegNo.Text.ToString(), con);
            cmd = new SqlCommand("update AppRecord set Status=@Status where Appno='" + GridAutoCad.SelectedRow.Cells[1].Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("Status", "Filled");
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Submitted')", true);
            con.Close(); btnSubmit.Enabled = false; 
        }
        catch (SqlException ex)
        {
            lblException.Text = ex.ToString();
        }
        finally
        {
            con.Dispose();
        }
    }
    protected void GridAutoCad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    public string RegTemp()
    {
        cmd = new SqlCommand("select count(RegNo) from MCAD", con);
        return Convert.ToString(cmd.ExecuteScalar().ToString());
    }
    public void feeinsert(string sid, string feetype, string amount, string regno,SqlConnection con)
    {
        cmd = new SqlCommand("insert into MCADFee(SID,FeeType,Date,Amount,RegNo,MCADNo) values(@SID,@FeeType,@Date,@Amount,@RegNo,@MCADNo)", con);
        cmd.Parameters.AddWithValue("@SID", sid);
        cmd.Parameters.AddWithValue("@FeeType", feetype);
        cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
        cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount));
        cmd.Parameters.AddWithValue("@RegNo", regno);
        cmd.Parameters.AddWithValue("@MCADNo", GridAutoCad.SelectedRow.Cells[9].Text.ToString());
        cmd.ExecuteNonQuery();
    }

    private void bindbatch()
    {
        adp = new SqlDataAdapter("select Batch_ID from MCADBatch order by batch_ID desc", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlBatchNo.DataSource = dt;
        ddlBatchNo.DataTextField = "Batch_ID";
        ddlBatchNo.DataValueField = "Batch_ID";
        ddlBatchNo.DataBind();
    }
}