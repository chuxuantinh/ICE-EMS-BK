using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public partial class Exam_ViewExamCenter : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            btnAdd.Visible = false;
            txtYearSeason.Text = DateTime.Now.Year.ToString();
             string yr = DateTime.Now.Year.ToString();
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0)
            {
                ddlExamSeason.SelectedValue = "Sum";
            }
            else
            {
                ddlExamSeason.SelectedValue = "Win";
            }
            lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            txtYearSeason.Text = DateTime.Now.Year.ToString();
            lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
            ddlExamSeason.Focus();
            PanView.Visible = false;
            panView5.Visible = false;
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
        {
            Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnSessionOK_OnClick(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        GridView1.DataBind();
        btnAdd.Visible = false;
    }
    private void centerinfo(string code, string session)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        SqlCommand cmd = new SqlCommand("select * from  ExamCenter where ID='" + code + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblCenterCode.Text = reader["ID"].ToString();
            lblCenteNaem.Text = reader["Name"].ToString();
            lblCenterAddress.Text = reader["Address"].ToString();
            lblCenterAddress2.Text = reader["Address2"].ToString();
            lblCenterCity.Text = reader["City"].ToString();
            lblCenterState.Text = reader["State"].ToString();
            lblPinCode.Text = reader["Pin"].ToString();
            lblExceptionCode.Text = "";
            reader.Close();
            SqlCommand cmd1 = new SqlCommand("select * from ExamCenter where ID='" + lblCenterCode.Text + "'", con);
            SqlDataReader read = cmd1.ExecuteReader();
            while (read.Read())
            {
                sea = read["Season"].ToString();
            }
            read.Close();
            read.Dispose(); string yr = DateTime.Now.Year.ToString();
            maikal dev = new maikal();
            int se = dev.chksession();
            if (se == 0)
            {
                season = "Sum" + yr;
            }
            else { season = "Win" + yr; }
              if (sea == season) btnAdd.Visible = false;
            else if (sea != season) btnAdd.Visible = true;
        }
        else
        {
            lblExceptionCode.Text = "Invalid Exam Center Code";
            pnlAcInf.Visible = false; panView5.Visible = false; PanView.Visible = false;
        }
        reader.Close();
        cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + txtExamCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        {
            lblCapacity.Text = sum.ToString();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridView1.SelectedRow;
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        lblCenterCode.Text = gr.Cells[1].Text.ToString();
        con.Close();
        con.Open(); SqlCommand cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + txtExamCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        else
        {
            lblCapacity.Text = sum.ToString();
        }
        centerinfo(lblCenterCode.Text.ToString(), lblSeasonHidden.Text.ToString());
        PanView.Visible = true;
        panView5.Visible = true;
        acInf(GridView1.SelectedRow.Cells[1].Text.ToString());
        bindInvg(GridView1.SelectedRow.Cells[1].Text.ToString());
        pnlAcInf.Visible = true;
        con.Close(); con.Dispose();
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow rw;
        rw = GridView3.SelectedRow;
    }
    string season,sea;
    protected void btnOkCenterCode_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        if (txtExamCode.Text == "") txtExamCode.Text = lblCenterCode.Text;
        centerinfo(txtExamCode.Text.ToString(), lblSeasonHidden.Text.ToString());
        PanView.Visible = true;
        panView5.Visible = true;
        acInf(txtExamCode.Text.ToString());
        bindInvg(txtExamCode.Text.ToString());
        pnlAcInf.Visible = true;
        centerinfo(txtExamCode.Text.ToString(), lblSeasonHidden.Text.ToString());
        con.Close(); con.Dispose();
        txtExamCode.Focus();
    }
    private void acInf(string stid)
    {
        cmd = new SqlCommand("select * from ExamCenter where ID='" + stid + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        SqlDataReader dr1 = cmd.ExecuteReader();
        while (dr1.Read())
        {
            lblACNo.Text = dr1["ACNo"].ToString();
            lblIFSCCode.Text = dr1["IFSCCode"].ToString();
            lblBankName.Text = dr1["Bank"].ToString();
            lblbAdd.Text = dr1["BankAddress"].ToString();
            lblDDInFvr.Text = dr1["DDInFavour"].ToString();
            lblPaybleAt.Text = dr1["PayableAt"].ToString();
            lblAcTtl.Text = dr1["ACTitle"].ToString();
            lblcourier.Text = dr1["Courier"].ToString();
        }
        dr1.Close();
    }
    private void bindInvg(string stidIn)
    {
        SqlDataAdapter adp = new SqlDataAdapter("select * from Invigilator where Session='" + lblSeasonHidden.Text.ToString() + "' and CenterCode='" + stidIn + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        gridInvig.DataSource = dt;
        gridInvig.DataBind();
    }
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select1")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string datakey = GridView1.DataKeys[row.RowIndex].Value.ToString();
            Response.Redirect("ExamSponsor.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=" + datakey + "&session=" + lblSeasonHidden.Text);
        }
    }
    private string idcenter()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(ID) from ExamCenter where Season='" + season + "'", con);
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
    string strid;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        con.Open(); 
        string yr = DateTime.Now.Year.ToString();
        maikal dev = new maikal();
        int se = dev.chksession();
        if (se == 0)
        {
            season = "Sum" + yr;
        }
        else { season = "Win" + yr; }
        strid = idcenter();
        SqlCommand cmd, cmd1; cmd1 = new SqlCommand("select * from ExamCenter where Name='" + lblCenteNaem.Text + "' and Season='" + season + "' and City='" + lblCenterCity.Text + "'", con);
        SqlDataReader read1 = cmd1.ExecuteReader();
        if (read1.Read())
        {
            lblExceptionCode.Text = "Already Exist";
            read1.Close();
        }
        else
        {
            read1.Close();
            cmd = new SqlCommand("select * from ExamCenter where ID='" + lblCenterCode.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd = new SqlCommand("insert into ExamCenter(ID,Name,BName,Type,Address,Address2,City,State,Pin,Phone,Fax,Mobile,Email,ToSeat,Season,RollNo,Date) values(@ID,@Name,@BName,@Type,@Address,@Address2,@City,@State,@Pin,@Phone,@Fax,@Mobile,@Email,@ToSeat,@Season,@RollNo,@Date)", con);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(strid.ToString()));
                cmd.Parameters.AddWithValue("@Name", reader["Name"].ToString());
                cmd.Parameters.AddWithValue("@BName", reader["BName"].ToString());
                cmd.Parameters.AddWithValue("@Type", reader["Type"].ToString());
                cmd.Parameters.AddWithValue("@Address", reader["Address"].ToString());
                cmd.Parameters.AddWithValue("@Address2", reader["Address2"].ToString());
                cmd.Parameters.AddWithValue("@City", reader["City"].ToString());
                cmd.Parameters.AddWithValue("@State", reader["State"].ToString());
                cmd.Parameters.AddWithValue("@Pin", reader["Pin"].ToString());
                cmd.Parameters.AddWithValue("@Phone", reader["Phone"].ToString());
                cmd.Parameters.AddWithValue("@Fax", reader["Fax"].ToString());
                cmd.Parameters.AddWithValue("@Mobile", reader["Mobile"].ToString());
                cmd.Parameters.AddWithValue("@Email", reader["Email"].ToString());
                cmd.Parameters.AddWithValue("@ToSeat", reader["ToSeat"].ToString());
                cmd.Parameters.AddWithValue("@Season", season);
                cmd.Parameters.AddWithValue("@RollNo", reader["RollNo"].ToString());
                cmd.Parameters.AddWithValue("@Date", reader["Date"].ToString());
            }
            reader.Close();
            read1.Dispose();
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update Rooms set ID='" + strid + "',Season='" + season + "'", con);
            cmd.ExecuteNonQuery();
        }
        con.Close();
        con.Dispose();
        GridView1.DataBind();
    }
    protected void gridInvig_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            if (e.Row.Cells[15].Text == "" | e.Row.Cells[15].Text == "&nbsp;")
            {
            }
            else
            {
                e.Row.Cells[15].Text = Convert.ToDateTime(e.Row.Cells[15].Text).ToString("dd/MM/yyyy");
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
}
