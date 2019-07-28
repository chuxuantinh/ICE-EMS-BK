using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;

public partial class Exam_ExamCenter : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    ClsECenterCity ecity = new ClsECenterCity();
    ClsStateCity clstate = new ClsStateCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
        {
            Response.Redirect("../Login.aspx");
            invisible.Visible = true; visisble.Visible = false;
        }
        if (Request.QueryString["id"].ToString() == "")
        {
            panelRoom.Visible = false;
            invisible.Visible = true;
        }
        else
        {
            lblEnrolment.Text = Request.QueryString["id"].ToString();
            panelRoom.Visible = true;
            invisible.Visible = false;
        }
        if (!IsPostBack)
        {
           // clstate.xmlstate(ddlState, "XMLState.xml");
            ecity.getItems(ddlCity);
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0) ddlExamSeason.SelectedValue = "Sum";
            else ddlExamSeason.SelectedValue = "Win";
            lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            visisble.Visible = true; invisible.Visible = true;
            room();
            txtExamID.Text = ecity.getCenterCode(ddlCity.SelectedValue.ToString());
            ddlExamSeason.Focus();
        }
           maikal mk = new maikal();
           int i = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
           if (i == 0 | i == 1)
               invisible.Visible = true;
           else if (i == 2)
           {
               invisible.Visible = false;
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
    protected void room()
    {
        txtRoomNo.Visible = true;
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        try
        {
            con.Close();
            con.Open();
            cmd = new SqlCommand("select Max(RoomNo) from Rooms where ID='" + lblEnrolment.Text.ToString() + "' and Season='" + lblHiddenSeason.Text.ToString() + "'", con);
            string rno = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            if (rno == "")
            {
                rno = "1";
            }
            else
            {
                int i = Convert.ToInt32(rno);
                i = i + 1;
                rno = i.ToString();
            }
            txtRoomNo.Text = rno.ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtName.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender,EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void ddlCity_SelectedInexChanged(object sender, EventArgs e)
    {
        txtExamID.Text = ecity.getCenterCode(ddlCity.SelectedValue.ToString());
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
             lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            con.Open();
            string stn = "insert into ExamCenter(ID,Name,Address,Address2,City,State,Pin,Phone,Fax,Mobile,Email,ToSeat,Season,ACNo,IFSCCode,Bank,BankAddress,DDInFavour,PayableAt,ACTitle,Courier)values(@ID,@Name,@Address,@Address2,@City,@State,@Pin,@Phone,@Fax,@Mobile,@Email,@ToSeat,@Season,@ACNo,@IFSCCode,@Bank,@BankAddress,@DDInFavour,@PayableAt,@ACTitle,@Courier)";
            cmd = new SqlCommand(stn, con);
            
            cmd.Parameters.AddWithValue("@ID", txtExamID.Text.ToString());
            cmd.Parameters.AddWithValue("@Name",txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Address", txtPAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.ToString());
            cmd.Parameters.AddWithValue("@City",ddlCity.SelectedItem. Text.ToString());
            cmd.Parameters.AddWithValue("@State", txtAddress3.Text.ToString());
            cmd.Parameters.AddWithValue("@Pin", txtPPincode.Text.ToString());
            cmd.Parameters.AddWithValue("Phone", txtPhonecode.Text.ToString() + "-" + txtPhoneNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFaxCode.Text.ToString() + "-" + txtFaxNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("ToSeat", 0);
            cmd.Parameters.AddWithValue("@Season", ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString());
            cmd.Parameters.AddWithValue("@ACNo",txtAcNo.Text.ToString());
            cmd.Parameters.AddWithValue("@IFSCCode",txtIFSCCode.Text.ToString());
            cmd.Parameters.AddWithValue("@Bank",txtBankname.Text.ToString());
            cmd.Parameters.AddWithValue("@BankAddress",txtBAdd.Text.ToString());
            cmd.Parameters.AddWithValue("@DDInFavour",txtDDInFvr.Text.ToString());
            cmd.Parameters.AddWithValue("@PayableAt", txtPblAt.Text.ToString());
            cmd.Parameters.AddWithValue("@ACTitle",txtAcTtl.Text.ToString());
            cmd.Parameters.AddWithValue("@Courier",txtCor.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            lblException.Text = "New Examination Center Code " + txtExamID.Text.ToString();
            btnSave.Enabled = false;
            Response.Redirect("ExamCenter.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=" + txtExamID.Text.ToString());
            txtRoomCapacity.Focus();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    private string idcenter()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(ID) from ExamCenter where Season='"+ddlExamSeason.SelectedValue.ToString()+""+txtYearSeason.Text.ToString()+"'", con);
        con.Close();
        con.Open();
        int i;
        string id = Convert.ToString(cmdsn.ExecuteScalar());
        if (id == "")
        {
            i = 1;
        }
        else
        {
            i = Convert.ToInt32(id);
            i = i + 1;
        }
        if (i <= 9)
        {
            id = "" + i;
        }
        else if (i <= 99)
        {
            id = "" + i;
        }
        return id;
    }
    protected void btnCencel_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        invisible.Visible = false;        
        Response.Redirect(url.ToString());
    }
    protected void btnRoom_click(object seder, EventArgs e)
    {
        try
        {
            con.Close();
            if (txtRoomCapacity.Text != "")
            {
                con.Open();
                cmd = new SqlCommand("insert into Rooms(Season,ID,RoomNo,Capacity,Columns) values(@Season,@ID,@RoomNo,@Capacity,@Columns)", con);
                cmd.Parameters.AddWithValue("@Season", ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString());
                cmd.Parameters.AddWithValue("@ID", lblEnrolment.Text.ToString().TrimStart('0'));
                cmd.Parameters.AddWithValue("@RoomNo", Convert.ToInt32(txtRoomNo.Text.ToString()));
                cmd.Parameters.AddWithValue("@Capacity", Convert.ToInt32(txtRoomCapacity.Text.ToString()));
                cmd.Parameters.AddWithValue("@Columns", ddlRoomColumn.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                int total, ntotal;
                cmd = new SqlCommand("select ToSeat from ExamCenter where ID='" + lblEnrolment.Text + "' and Season='" + ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString() + "' ", con);
                total = Convert.ToInt32(cmd.ExecuteScalar());
                ntotal = total + Convert.ToInt32(txtRoomCapacity.Text);
                cmd = new SqlCommand("update ExamCenter set ToSeat=@ToSeat where ID='" + lblEnrolment.Text + "' and Season='" + ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString() + "' ", con);
                cmd.Parameters.AddWithValue("@ToSeat", ntotal);
                cmd.ExecuteNonQuery();
                lblExceptionRoom.Text = "Room No.: " + txtRoomNo.Text.ToString() + "  of Capacity: " + txtRoomCapacity.Text.ToString() + " Added.";
                gvroomshow.DataBind();
                room();
            }
        }
        catch (SqlException ex)
        {
            lblExceptionRoom.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        txtRoomCapacity.Text = "";
        txtRoomCapacity.Focus();
        invisible.Visible = false;
    }
}