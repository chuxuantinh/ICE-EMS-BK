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
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Acc_PendingFormApprove : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            lblFormType.Text = ddlFormtype.SelectedValue.ToString();
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlsession.SelectedValue = "Sum";
                else ddlsession.SelectedValue = "Win";
                txtSession.Text = DateTime.Now.Year.ToString();
                lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
        finally
        {
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
        string query = "";
        if (ddlFormtype.SelectedValue.ToString() == "All")
        {
            query = "select FormType,Amount,Type,Enrolment as Membership,IMID,SerialNo,Course,Part,Remark,SN from RecoverApp where Status='"+ddlSearchType.SelectedValue.ToString()+"' and Session='" + lblSessionHiddend.Text.ToString() + "' order by IMID";
        }
        else
        {
            query = "select FormType,Amount,Type,Enrolment as Membership,IMID,SerialNo,Course,Part,Remark,SN from RecoverApp where Status='" + ddlSearchType.SelectedValue.ToString() + "' and Session='" + lblSessionHiddend.Text.ToString() + "' and FormType='" + ddlFormtype.SelectedValue.ToString() + "' order by IMID";
        }
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridAppForm.DataSource = ds;
        GridAppForm.DataBind();
        lblSearchlabel.Text = ddlFormtype.SelectedValue.ToString() + " Form(s)";
        lblExceptionOK.Text = "";
        btnView.Focus();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            for (int i = 0; i < GridAppForm.Rows.Count; i++)
            {
                CheckBox rbApp = (CheckBox)GridAppForm.Rows[i].FindControl("chkapp");
                if (rbApp.Checked == true)
                {
                    approveapps(i);
                    GridAppForm.Rows[i].Visible = false;
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Amount Managed')", true);
            con.Close();
            btnApprove.Enabled = false;
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Error ! ')", true);
        }
        finally
        {
            con.Dispose();
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }

    #region methods
    private void approveapps(int index)
    {
        cmd = new SqlCommand();
        GridViewRow rw = GridAppForm.Rows[index];
       double amt = Convert.ToDouble(rw.Cells[2].Text.ToString());
        if (amt > 0)
        {
            if (rw.Cells[2].Text.ToString() == "Project")
            {
                cmd = new SqlCommand("update IMAC set Total=Total+'" + amt + "', Project=Project+'" + amt + "' where IMID='" + rw.Cells[5].Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("update IMAC set Total=Total+'" + amt + "' where IMID='" + rw.Cells[5].Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
            }
        }
        cmd = new SqlCommand("update RecoverApp set Status='Approved' where SN='" + rw.Cells[10].Text+ "'", con);
        cmd.ExecuteNonQuery();
    }
    #endregion
}