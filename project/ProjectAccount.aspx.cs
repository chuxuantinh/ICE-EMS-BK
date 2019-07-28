using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Collections;

public partial class project_ProjectAccount : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    IMInfo iminfo;
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
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlSession.SelectedValue = "Sum"; else ddlSession.SelectedValue = "Win";
                    txtYear.Text = DateTime.Now.Year.ToString();
                    lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                    ddlSession.Focus();
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
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtSID.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHidden.Text = ddlSession.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            bool flg = false;
            cmd = new SqlCommand("select IMID,SID,StudentName,Course,Part,InstitutionID,Institution,Status,SynopsisStatus,ApprovalFees,EvalutionFees from Project where SID='" + txtSID.Text.ToString() + "' and Session='" + lblSessionHidden.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblIMID.Text = reader["IMID"].ToString();
                lblName.Text = reader["StudentName"].ToString();
                lblCourse.Text = reader["Course"].ToString();
                lblPart.Text = reader["Part"].ToString();
                lblStatus.Text = reader["Status"].ToString();
                lblInsID.Text = reader["InstitutionID"].ToString();
                lblInsName.Text = reader["Institution"].ToString();
                lblApprovalFees.Text = reader["ApprovalFees"].ToString().TrimEnd('0').TrimEnd('.');
                lblEvaluationFees.Text = reader["EvalutionFees"].ToString().TrimEnd('0').TrimEnd('.');
                lblException.Text = "";
                flg = true;
            }
            else
            {
                lblException.Text = "Student Membership Not Found.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Student Membership Not Found.')", true);
                flg = false;
            }
            reader.Close();
            if (flg == true)
            {
                cmd = new SqlCommand("select * from IMAC where IMID='" + lblIMID.Text + "'", con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblToAmount.Text = reader["Total"].ToString().TrimEnd('0').TrimEnd('.');
                    lblDuesAmount.Text = reader["Credit"].ToString().TrimEnd('0').TrimEnd('.');
                    lblBooksAmt.Text = reader["IMTotal"].ToString().TrimEnd('0').TrimEnd('.');
                    lblProspectus.Text = reader["Prospectus"].ToString().TrimEnd('0').TrimEnd('.');
                    lblProjectAmt.Text = reader["Project"].ToString().TrimEnd('0').TrimEnd('.');
                }
                reader.Close();
                reader.Dispose();
                iminfo = new IMInfo();
                lblFeeType.Text = iminfo.imFeeMaster(lblIMID.Text.ToString());
            }
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Error!!!, Student Membership Not Found.')", true);
        }
        finally
        {
            con.Close();
            con.Dispose();
            ddlProforma.Focus();
        }
    }
    protected void ddlProforma_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] fee = Feemaster();
        if (ddlProforma.SelectedValue.ToString() == "ProformaB")
        {
            lblAmount.Text = fee[0].ToString();
            if (lblApprovalFees.Text == "0" | lblApprovalFees.Text == "")
            {
                lblException.Text = ""; btnManage.Enabled = true;
            }
            else
            {
                btnManage.Enabled = false;
                lblException.Text = "Project approval Fees Already Submitted.";
            }
        }
        else if (ddlProforma.SelectedValue.ToString() == "ProformaC")
        {
            lblAmount.Text = fee[1].ToString();
            if (lblEvaluationFees.Text == "0" | lblEvaluationFees.Text == "")
            {
                lblException.Text = ""; btnManage.Enabled = true;
            }
            else
            {
                btnManage.Enabled = false;
                lblException.Text = "Project Evaluation Fees Already Submitted.";
            }
        }
        else
        {
            btnManage.Enabled = false;
        }
        ddlProforma.Focus();
    }
    protected void btnManage_click(object sender, EventArgs e)
    {
        string qry="";
        if (Convert.ToInt32(lblAmount.Text) > Convert.ToInt32(lblProjectAmt.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Enough Amount Found.')", true);
        }
        else if (ddlProforma.SelectedValue.ToString() == "ProformaB" && lblApprovalFees.Text != "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Approval Fees Aleardy Submitted.')", true);
        }
        else if (ddlProforma.SelectedValue.ToString() == "ProformaC" && lblEvaluationFees.Text != "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Project Evaluation Fees Aleardy Submitted.')", true);
        }
        else 
        {
            if(ddlProforma.SelectedValue.ToString()=="ProformaB")
            {
                qry="update Project set ApprovalFees='"+lblAmount.Text+"' where Session='"+lblSessionHidden.Text.ToString()+"' and SID='"+txtSID.Text.ToString()+"'";
                lblApprovalFees.Text = lblAmount.Text;
                lblProjectAmt.Text = (Convert.ToInt32(lblProjectAmt.Text) - Convert.ToInt32(lblAmount.Text)).ToString();
            }
            else if(ddlProforma.SelectedValue.ToString()=="ProformaC")
            {
                qry = "update Project set EvalutionFees='" + lblAmount.Text + "' where Session='" + lblSessionHidden.Text.ToString() + "' and SID='" + txtSID.Text.ToString() + "'";
                lblEvaluationFees.Text = lblAmount.Text;
                lblProjectAmt.Text = (Convert.ToInt32(lblProjectAmt.Text) - Convert.ToInt32(lblAmount.Text)).ToString();
            }
            con.Close(); con.Open();
            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Update IMAC set Total=Total-@total,Project=Project-@amount where IMID='" + lblIMID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@total", Convert.ToInt32(lblAmount.Text));
            cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(lblAmount.Text));
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (ddlProforma.SelectedValue.ToString() == "ProformaB" && lblApprovalFees.Text == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Approval Fees Not Found.')", true);
        }
        else if (ddlProforma.SelectedValue.ToString() == "ProformaC" && lblEvaluationFees.Text == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Project Evaluation Fees Not Found.')", true);
        }
        else 
        {
        string qry = "";
            if (ddlProforma.SelectedValue.ToString() == "ProformaB")
            {
                qry = "update Project set ApprovalFees=0 where Session='" + lblSessionHidden.Text.ToString() + "' and SID='" + txtSID.Text.ToString() + "'";
                lblProjectAmt.Text = (Convert.ToInt32(lblProjectAmt.Text) + Convert.ToInt32(lblApprovalFees.Text)).ToString();
                lblApprovalFees.Text = "0";
            }
            else if (ddlProforma.SelectedValue.ToString() == "ProformaC")
            {
                qry = "update Project set EvalutionFees=0 where Session='" + lblSessionHidden.Text.ToString() + "' and SID='" + txtSID.Text.ToString() + "'";
                lblProjectAmt.Text = (Convert.ToInt32(lblProjectAmt.Text) + Convert.ToInt32(lblEvaluationFees.Text)).ToString();
                lblEvaluationFees.Text = "0";
            }
            con.Close(); con.Open();
            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Update IMAC set Total=Total+@total,Project=Project+@amount where IMID='" + lblIMID.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@total", Convert.ToInt32(lblAmount.Text));
            cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(lblAmount.Text));
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
        }
    }

    #region Methods
    private string[] Feemaster()
    {
        string stream = "";
        if (lblPart.Text == "PartII") stream = "Tech";
        else if (lblPart.Text == "SectionB") stream = "Asso";
        string[] profee = new string[2];
        con.Close(); con.Open();
        cmd = new SqlCommand("select ProApproval,ProEvaluation from FeeMaster where FeeType='" + stream + "' and Type='" + lblFeeType.Text + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            profee[0] = reader["proApproval"].ToString().TrimEnd('0').TrimEnd('.');
            profee[1] = reader["ProEvaluation"].ToString().TrimEnd('0').TrimEnd('.'); ;
        }
        else
        {
            profee = null;
        }
        reader.Close();
        reader.Dispose();
        con.Close(); con.Dispose();
        return profee;
    }
    #endregion
}