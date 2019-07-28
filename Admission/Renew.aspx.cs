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

public partial class User_Renew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ddlExamSeason.Focus();
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                PnlStudInfo.Visible = false;
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
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
    }
    private string chkim(string imid)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + imid.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        int i = 0;
        if (chk == imid.ToString())
        {
            i += 1;
            return "";
        }
        else
        {
            return "Please Insert Valid IM ID.";
        }
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
        string qry = "";
        qry = "select FormType,Amount,Type,Enrolment,AppNo,IMID,SerialNo,Name from RecoverApp where Status='" + ddlStatus.SelectedValue.ToString() + "' and FormType='Admission'";
        SqlDataAdapter ad = new SqlDataAdapter();
        ad = new SqlDataAdapter(qry, con);
        DataTable at = new DataTable();
        ad.Fill(at);
        GridToBeApprove.DataSource = at;
        GridToBeApprove.DataBind();
    }
    protected void btnToBeApprove_OnClick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        for (int i = 0; i < GridToBeApprove.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridToBeApprove.Rows[i].FindControl("chkapp");
            if (chk.Checked)
            {
                cmd = new SqlCommand("update Student set Status=@Status where SN='" + Convert.ToInt32(GridToBeApprove.Rows[i].Cells[2].Text.ToString()) + "'", con);
                cmd.Parameters.AddWithValue("@Status", "ToBeApprove");
                cmd.ExecuteNonQuery();
            }
        }
        con.Close();
    }
    protected void GridAppTable_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPart_OnselectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridToBeApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ddlStatus.SelectedValue == "Approved")
        {
            GridToBeApprove.Columns[1].Visible = true;
        }
    }
    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridToBeApprove.DataSource = null;
        GridToBeApprove.DataBind();
        btnViewa.Focus();
    }
    private string genid()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(SID) from Student", con);
        con.Close();
        con.Open();
        string id = cmdsn.ExecuteScalar().ToString();
        if (id == "")
        {
            id = "5001";
        }

        int ii = Convert.ToInt32(id) + 1;
        id = ii.ToString();
        return id.ToString();
    }
    private void chkExamForm(string sid, string nid)
    {
        con.Close(); con.Open();
        SqlCommand dmc = new SqlCommand();
        dmc = new SqlCommand("select SID from ExamForm where SID='" + sid.ToString() + "'", con);
        string chk = Convert.ToString(dmc.ExecuteScalar());
        if (chk == sid)
        {
            dmc = new SqlCommand("update ExamForm set SID=@SID where SID='" + sid.ToString() + "'", con);
            dmc.Parameters.AddWithValue("@SID", nid.ToString());
            dmc.ExecuteNonQuery();
            dmc = new SqlCommand("update ExamForms set SID=@SID where SID='" + sid.ToString() + "'", con);
            dmc.Parameters.AddWithValue("@SID", nid.ToString());
            dmc.ExecuteNonQuery();
            dmc = new SqlCommand("update ITIForm set SID=@SID where AppNo='" + sid.ToString() + "'", con);
            dmc.Parameters.AddWithValue("@SID", nid.ToString());
            dmc.ExecuteNonQuery();
        }
    }
    protected void GridToBeApprove_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedItem.Text == "Approved")
        {
            btnApprove.Visible = false;
        }
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd/MM/yyyy";
        dtfi.DateSeparator = "/";
         GridViewRow row;
        row = GridToBeApprove.SelectedRow;
        lblEnrol.Text = row.Cells[5].Text.ToString();
        PnlStudInfo.Visible = true;
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from RecoverApp where Enrolment='"+lblEnrol.Text.ToString()+"'", con);
         SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblName.Text = reader[12].ToString();
                lblCourse.Text = reader[10].ToString();
                lblDob.Text = reader[14].ToString();
                lblPart.Text = reader[11].ToString();
                lblStatus.Text = reader[15].ToString();
                Remarks.Text = reader[16].ToString();
                lblStream.Text = reader[9].ToString();
            }
            reader.Close();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("update RecoverApp set Status='Approved'", con);
        SqlCommand cmd1 = new SqlCommand("update AppRecord set Part='"+lblPart.Text.ToString()+"',Stream='"+lblStream.Text.ToString()+"',Course='"+lblCourse.Text.ToString()+"'", con);
        cmd.ExecuteNonQuery();
        cmd1.ExecuteNonQuery();
        lblMessage.Visible = true;
        lblMessage.Text = "Successfully Approved";
        PnlStudInfo.Visible = false;
        con.Close();
    }
}