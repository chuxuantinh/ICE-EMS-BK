using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Exam_ViewPaperSetter : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum"; else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                rbnPapersetter.SelectedIndexChanged += new EventHandler(rbnPapersetter_SelectedIndexChanged);
                pnlCourse.Visible = false;
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    public void grid()
    {
        SqlCommand cmd = new SqlCommand("select Code,Name,SCode,Course,Session  from PaperSetter", con); 
        DataSet ds = new DataSet();
        SqlDataAdapter dap = new SqlDataAdapter(cmd);
        dap.Fill(ds, "PaperSetter");
        GridpaperView.DataSource = ds;
        GridpaperView.DataBind();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        switch (rbnPapersetter.SelectedItem.Value)
        {
            case "Code":
                SqlCommand cmd = new SqlCommand("select Code,Name,SCode,Course,Session from PaperSetter where Code='" + txtSearch.Text.ToString() + "'", con);
                 DataSet ds = new DataSet();
                 con.Open();
                 SqlDataAdapter dap = new SqlDataAdapter(cmd);
                 dap.Fill(ds, "PaperSetter");
                 GridpaperView.DataSource = ds;
                 GridpaperView.DataBind();
                 break;

            case "Name":
                 SqlCommand cmd1 = new SqlCommand("select Code,Name,SCode,Course,Session from PaperSetter where  Name='" + txtSearch.Text.ToString() + "'", con);
                 DataSet ds1 = new DataSet();
                 con.Open();
                 SqlDataAdapter dap1 = new SqlDataAdapter(cmd1);
                 dap1.Fill(ds1, "PaperSetter");
                 GridpaperView.DataSource = ds1;
                 GridpaperView.DataBind();
                 break;
        }
    }
    protected void rbnPapersetter_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        if (rbnPapersetter.SelectedItem.Value == "Code")
        {
            pnlView.Visible = true;
            lblPaperCode.Visible = true;
            lblCourseShow.Visible = false;
            lblNameShow.Visible = false;
            txtSearch.Visible = true;
            btnView.Visible = true;
            pnlCourse.Visible = false;
            txtSearch.Focus();
        }
        if (rbnPapersetter.SelectedItem.Value == "Course")
        {
            pnlView.Visible = true;
            lblPaperCode.Visible = false;
            lblCourseShow.Visible = false;
            lblNameShow.Visible = false;
            txtSearch.Visible = false;
            btnView.Visible = false;
            pnlCourse.Visible = true;
        }
        if (rbnPapersetter.SelectedItem.Value == "Name")
        {
            pnlView.Visible = true;
            lblPaperCode.Visible = false;
            lblCourseShow.Visible = false;
            lblNameShow.Visible = true;
            txtSearch.Visible = true;
            btnView.Visible = true;
            pnlCourse.Visible = false;
            txtSearch.Focus();
        }  
    }
    protected void GridpaperView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridpaperView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (GridpaperView.Rows.Count > 0)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select Code,Name,SCode,Course,Session from PaperSetter", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            GridpaperView.DataSource = ds;
            GridpaperView.DataBind();
            pnlselect.Visible = true;
        }
    }
   
    protected void GridpaperView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridpaperView.Rows.Count > 0)
        {
            pnlselect.Visible = true;
            lblPapersetterCode.Text = GridpaperView.SelectedRow.Cells[1].Text.ToString();
            lblName.Text = GridpaperView.SelectedRow.Cells[2].Text.ToString();
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from PaperSetter where Code='" + lblPapersetterCode.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
            lblPapersetterCode.Text = reader["Code"].ToString();
            lblDesignation.Text = reader["Designation"].ToString();
            lblName.Text = reader["Name"].ToString();
            lblDOB.Text = reader["DOB"].ToString();
            lblAge.Text = reader["Age"].ToString();
            lblEducation.Text = reader["Education"].ToString();
            lblExperience.Text = reader["Experience"].ToString();
            lblPAddress.Text = reader["PAddress"].ToString();
            lblPCity.Text = reader["PCity"].ToString();
            lblPState.Text = reader["PState"].ToString();
            lblPPin.Text = reader["PPin"].ToString();
            lblCAddress.Text = reader["CAddress"].ToString();
            lblCCity.Text = reader["CCity"].ToString();
            lblCState.Text = reader["CState"].ToString();
            lblCPin.Text = reader["CPin"].ToString();
            lblMobile.Text = reader["Mobile"].ToString();
            lblPhone.Text = reader["Phone"].ToString();
            lblFax.Text = reader["Fax"].ToString();
            lblEmail.Text = reader["Email"].ToString();
            lblSession.Text = reader["Session"].ToString();
            lblCourse.Text = reader["Course"].ToString();
            lblSubjectCode.Text = reader["SCode"].ToString();
            lblSubjectName.Text = reader["SName"].ToString();
            }
            reader.Close();
            reader.Dispose();
            con.Close();
            con.Dispose();
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            pnlGrid.Visible = true;
            SqlCommand cmd4 = new SqlCommand("select Code,Name,SCode,Course,Session  from PaperSetter where Course ='" + ddlCourse.SelectedValue.ToString() + "'", con);
            DataSet ds4 = new DataSet();
            SqlDataAdapter dap4 = new SqlDataAdapter(cmd4);
            dap4.Fill(ds4, "PaperSetter");
            GridpaperView.DataSource = ds4;
            GridpaperView.DataBind();
        }
        else if (ddlCourse.SelectedValue.ToString() == "Architecture")
        {
            pnlGrid.Visible = true;
            SqlCommand cmd4 = new SqlCommand("select Code,Name,SCode,Course,Session  from PaperSetter where Course ='" + ddlCourse.SelectedValue.ToString() + "'", con);
            DataSet ds4 = new DataSet();
            SqlDataAdapter dap4 = new SqlDataAdapter(cmd4);
            dap4.Fill(ds4, "PaperSetter");
            GridpaperView.DataSource = ds4;
            GridpaperView.DataBind();
        }
    }
}
