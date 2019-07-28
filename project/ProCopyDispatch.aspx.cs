using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;

public partial class project_ProCopyDispatch : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlCommand cmd;
    SqlDataAdapter adp;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    txtcDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
                string strqry = "";
                if (ddlViewBy.SelectedValue.ToString() == "All")
                {
                    strqry = "select SID,StudentName,Course,Part,ProjectNo,Grade,NoOfCopies,ApprovalFees,EvalutionFees,ProjectTitle from Project where EntryStatus='Running' and SynopsisStatus='Approved' and Status='CopySubmitted' and Grade!='N/A' and Grade!='ABSENT' and ApprovalFees!=0 and EvalutionFees!=0 and NoofCopies!=0  and GroupID not in(select Distinct(GroupID) from Project where NoofCopies=0) order by GroupID,SID";
                }
                else if (ddlViewBy.SelectedValue.ToString() == "SID")
                {
                    strqry = "select SID,StudentName,Course,Part,ProjectNo,Grade,,NoOfCopies,ApprovalFees,EvalutionFees,ProjectTitle from Project where SID='" + txtView.Text.ToString() + "' and EntryStatus='Running' and  SynopsisStatus='Approved' and Status='CopySubmitted' and Grade!='N/A' and Grade!='ABSENT' and ApprovalFees!=0 and EvalutionFees!=0 and NoofCopies!=0  and GroupID not in(select Distinct(GroupID) from Project where NoofCopies=0) order by GroupID,SID";
                }
                else if (ddlViewBy.SelectedValue.ToString() == "ProjectNo")
                {
                    strqry = "select SID,StudentName,Course,Part,ProjectNo,Grade,,NoOfCopies,ApprovalFees,EvalutionFees ,ProjectTitle from Project where ProjectNo='" + txtView.Text.ToString() + "' and EntryStatus='Running' and  SynopsisStatus='Approved' and Status='CopySubmitted' and Grade!='N/A' and Grade!='ABSENT' and ApprovalFees!=0 and EvalutionFees!=0 and NoofCopies!=0  and GroupID not in(select Distinct(GroupID) from Project where NoofCopies=0) order by GroupID,SID";
                }
                adp = new SqlDataAdapter(strqry, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                GridDispatch.DataSource = dt;
                GridDispatch.DataBind();
                GridDispatch.Focus();
                if (GridDispatch.Rows.Count > 0)
                    GridDispatch.Focus();
                else ddlViewBy.Focus();
        }
        catch (NullReferenceException ex)
        {
            lblException.Text = ex.ToString();
        }
        catch (SqlException ex)
        {
         lblException.Text = ex.ToString(); 
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            for (int i = 0; i <= GridDispatch.Rows.Count - 1; i++)
            {
                CheckBox chk = (CheckBox)GridDispatch.Rows[i].FindControl("chkapp");
                if (chk.Checked == true)
                {
                    cmd = new SqlCommand("Update Project Set Status=@Status,SendDate=@SendDate where SID='" + GridDispatch.Rows[i].Cells[1].Text + "' and EntryStatus='Running'", con);
                    cmd.Parameters.AddWithValue("@Status", "CopyDispatched");
                    cmd.Parameters.AddWithValue("@SendDate", Convert.ToDateTime(txtcDate.Text, dtinfo));
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Project Successfully Updated.')", true);            
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
}