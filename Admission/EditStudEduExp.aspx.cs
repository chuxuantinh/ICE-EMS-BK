using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

public partial class Admission_EditStudEduExp : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd; SqlDataAdapter adp;
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
                    BindName(); BinXML();
                    txtSID.Focus(); BindGrid(); BindGridExp();
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Enter Membership no.!')", true);
            }
            else
            {
                con.Close(); con.Open();
                cmd = new SqlCommand("select EduType from StudentEdu where SID='" + txtSID.Text.ToString() + "' and EduType='" + ddlType.SelectedValue.ToString() + "'", con);
                string strChkEduType = Convert.ToString(cmd.ExecuteScalar());
                if (strChkEduType == "")
                {
                    cmd = new SqlCommand("insert into StudentEdu(SID,EduType,Board,Score,Year,OMarksheet,AMarksheet,OCertificate,ACertificate) values(@SID,@EduType,@Board,@Score,@Year,@OMarksheet,@AMarksheet,@OCertificate,@ACertificate)", con);
                    cmd.Parameters.AddWithValue("@SID", txtSID.Text.ToString());
                    cmd.Parameters.AddWithValue("@EduType", ddlType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Board", ddlBoard.SelectedValue.ToString()); ;
                    cmd.Parameters.AddWithValue("@Score", txtScore.Text.ToString());
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text.ToString());
                    if (chkOMark.Checked == true)
                        cmd.Parameters.AddWithValue("@OMarksheet", "Yes");
                    else
                        cmd.Parameters.AddWithValue("@OMarksheet", "No");
                    if (chkAtMark.Checked == true)
                        cmd.Parameters.AddWithValue("@AMarksheet", "Yes");
                    else
                        cmd.Parameters.AddWithValue("@AMarksheet", "No");
                    if (chkOrCert.Checked == true)
                        cmd.Parameters.AddWithValue("@OCertificate", "Yes");
                    else
                        cmd.Parameters.AddWithValue("@OCertificate", "No");
                    if (chkAtCert.Checked == true)
                        cmd.Parameters.AddWithValue("@ACertificate", "Yes");
                    else
                        cmd.Parameters.AddWithValue("@ACertificate", "No");
                    cmd.ExecuteNonQuery();
                    con.Close(); con.Open();
                    cmd = new SqlCommand("update Student set DocStatus=@DocStatus where SID='" + txtSID.Text.ToString() + "'",con);
                    if (chkDoc.Checked == true)
                        cmd.Parameters.AddWithValue("@DocStatus", "yes");
                    else
                        cmd.Parameters.AddWithValue("@DocStatus", "no");
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Inserted Successfully!')", true);
                    clear1(); BindGrid(); con.Close(); con.Dispose();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('This type of education details is already exists please try another one!')", true);
                    ddlType.Focus();
                }
            }
        }
        catch (NullReferenceException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please select Membership No')", true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("select EduType from StudentEdu where SID='" + GridEditEdu.SelectedRow.Cells[1].Text.ToString() + "' and Edutype='" + ddlType.SelectedValue.ToString() + "'", con);
            string strChkEduType = Convert.ToString(cmd.ExecuteScalar());
            if (strChkEduType != "")
            {
                cmd = new SqlCommand("update StudentEdu set EduType=@EduType,Board=@Board,Score=@Score,Year=@Year,OMarksheet=@OMarksheet,AMarksheet=@AMarksheet,OCertificate=@OCertificate,ACertificate=@ACertificate where SID='" + GridEditEdu.SelectedRow.Cells[1].Text.ToString() + "' and EduType='" + GridEditEdu.SelectedRow.Cells[2].Text.ToString() + "'", con);
                cmd.Parameters.AddWithValue("@EduType", ddlType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Board", ddlBoard.SelectedValue.ToString()); ;
                cmd.Parameters.AddWithValue("@Score", txtScore.Text.ToString());
                cmd.Parameters.AddWithValue("@Year", txtYear.Text.ToString());
                if (chkOMark.Checked == true)
                    cmd.Parameters.AddWithValue("@OMarksheet", "Yes");
                else
                    cmd.Parameters.AddWithValue("@OMarksheet", "No");
                if (chkAtMark.Checked == true)
                    cmd.Parameters.AddWithValue("@AMarksheet", "Yes");
                else
                    cmd.Parameters.AddWithValue("@AMarksheet", "No");
                if (chkOrCert.Checked == true)
                    cmd.Parameters.AddWithValue("@OCertificate", "Yes");
                else
                    cmd.Parameters.AddWithValue("@OCertificate", "No");
                if (chkAtCert.Checked == true)
                    cmd.Parameters.AddWithValue("@ACertificate", "Yes");
                else
                    cmd.Parameters.AddWithValue("@ACertificate", "No");
                cmd.ExecuteNonQuery();
                con.Close(); con.Open();
                cmd = new SqlCommand("update Student set DocStatus=@DocStatus where SID='" + GridEditEdu.SelectedRow.Cells[1].Text.ToString() + "'", con);
                if (chkDoc.Checked == true)
                    cmd.Parameters.AddWithValue("@DocStatus", "yes");
                else
                    cmd.Parameters.AddWithValue("@DocStatus", "no");
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Updated Successfully!')", true);
                BindGrid(); clear1(); GridEditEdu.Focus();
                con.Close(); con.Dispose();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('This type of education detail does not exists please choose correct one!')", true);
                ddlType.Focus();
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select Membership No')", true);
        }
        catch (NullReferenceException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select Membership No')", true);
        }
    }
    protected void txtSID_TextChanged(object sender, EventArgs e)
    {
        lblException.Text = "";
        con.Close(); con.Open();
        cmd = new SqlCommand("select SID from Student where SID='" + txtSID.Text.ToString() + "'", con);
        string strChk = Convert.ToString(cmd.ExecuteScalar());
        if (strChk == "") { lblException.Text = "Membership Not Found"; txtSID.Text = ""; lblName.Text = ""; lblCourse.Text = ""; lblPart.Text = ""; lblSession.Text = ""; txtSID.Focus(); }
        else 
        {
            BindName();
        }
    }
    private void BindGrid()
    {
        adp = new SqlDataAdapter("select * from StudentEdu where SID='" + txtSID.Text.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridEditEdu.DataSource = dt;
        GridEditEdu.DataBind();
    }
    protected void GridEditEdu_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from StudentEdu where SID='" + GridEditEdu.SelectedRow.Cells[1].Text.ToString() + "' and EduType='" + GridEditEdu.SelectedRow.Cells[2].Text.ToString() + "'", con); ;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lblEnrolment.Text = dr["SID"].ToString();
            ddlType.SelectedValue = dr["EduType"].ToString();
            ddlBoard.SelectedValue = dr["Board"].ToString();
            txtScore.Text = dr["Score"].ToString();
            txtYear.Text = dr["Year"].ToString();
            string strOMark = dr["OMarksheet"].ToString(); 
            if (strOMark == "Yes")
                chkOMark.Checked = true;
            else
                chkOMark.Checked = false;
            string strAMark = dr["AMarksheet"].ToString();
            if(strAMark=="Yes")
                chkAtMark.Checked = true;
            else
                chkAtMark.Checked = false;
            string strOCert = dr["OCertificate"].ToString();
            if(strOCert=="Yes")
                chkOrCert.Checked = true;
            else
                chkOrCert.Checked = false;
            string strACert = dr["ACertificate"].ToString();
            if(strACert=="Yes")
                chkAtCert.Checked = true;
            else
                chkAtCert.Checked = false;
        }
        dr.Close(); BindName(); con.Close();
    }
    private void BindName()
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from Student where SID='" + txtSID.Text.ToString() + "'",con); ;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lblName.Text = dr["Name"].ToString();
            lblSession.Text = dr["Session"].ToString();
            lblCourse.Text = dr["Course"].ToString();
            lblPart.Text = dr["Part"].ToString();
            string strDocStatus = dr["DocStatus"].ToString(); if (strDocStatus == "yes") chkDoc.Checked = true; else chkDoc.Checked = false;
            string strExpStatus = dr["ExpStatus"].ToString(); if (strExpStatus == "yes") chkExp.Checked = true; else chkExp.Checked = false;
        }
        dr.Close(); BindGrid(); BindGridExp(); BindRadio(); 
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
    protected void rdbtnEdu_CheckedChanged(object sender, EventArgs e)
    {
        BindName();
    }
    protected void rdbtnExp_CheckedChanged(object sender, EventArgs e)
    {
        BindName();
    }
    private void BindRadio()
    {
        if (rdbtnEdu.Checked == true)
        {
            pnlEdu.Visible = true; pnlSponsor.Visible = false; 
        }
        else if (rdbtnExp.Checked == true)
        {
            pnlEdu.Visible = false;  pnlSponsor.Visible = true; 
        }
        txtSID.Focus();
    }
    protected void btnAddExp_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Enter Membership no.!')", true);
            }
            else
            {
                con.Close(); con.Open();
                cmd = new SqlCommand("insert into StudentExp(SID,Type,Organization,Designation,Year,Location) values(@SID,@Type,@Organization,@Designation,@Year,@Location)", con);
                cmd.Parameters.AddWithValue("@SID", txtSID.Text.ToString());
                cmd.Parameters.AddWithValue("@Type", ddlSponExpType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Organization", txtOrg.Text.ToString());
                cmd.Parameters.AddWithValue("@Designation", txtDesig.Text.ToString());
                cmd.Parameters.AddWithValue("Year", txtYearExp.Text.ToString());
                cmd.Parameters.AddWithValue("@Location", txtLocation.Text.ToString());
                cmd.ExecuteNonQuery();
                con.Close(); con.Open();
                cmd = new SqlCommand("update Student set ExpStatus=@ExpStatus where SID='" + txtSID.Text.ToString() + "'", con);
                if (chkDoc.Checked == true)
                    cmd.Parameters.AddWithValue("@ExpStatus", "yes");
                else
                    cmd.Parameters.AddWithValue("@ExpStatus", "no");
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Saved SuccessFully!')", true);
                BindGridExp(); clear2();
                con.Close(); con.Dispose();
            }
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
    }
    protected void btnUpdateDetails_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("update StudentExp set SID=@SID,Type=@Type,Organization=@Organization,Designation=@Designation,Year=@Year,Location=@Location where SID='" + GridSponExp.SelectedRow.Cells[1].Text.ToString() + "' and Type='" + GridSponExp.SelectedRow.Cells[2].Text.ToString() + "'  and Organization='" + GridSponExp.SelectedRow.Cells[3].Text.ToString() + "' and Designation='" + GridSponExp.SelectedRow.Cells[4].Text.ToString() + "' and Location='" + GridSponExp.SelectedRow.Cells[6].Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("SID", txtSID.Text.ToString());
            cmd.Parameters.AddWithValue("Type", ddlSponExpType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("Organization", txtOrg.Text.ToString());
            cmd.Parameters.AddWithValue("Designation", txtDesig.Text.ToString());
            cmd.Parameters.AddWithValue("Year", txtYearExp.Text.ToString());
            cmd.Parameters.AddWithValue("Location", txtLocation.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close(); con.Open();
            cmd = new SqlCommand("update Student set ExpStatus=@ExpStatus where SID='" + GridSponExp.SelectedRow.Cells[1].Text.ToString() + "'", con);
            if (chkDoc.Checked == true)
                cmd.Parameters.AddWithValue("@ExpStatus", "yes");
            else
                cmd.Parameters.AddWithValue("@ExpStatus", "no");
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Updated SuccessFully!')", true);
            BindGridExp(); clear2(); GridSponExp.Focus();
            con.Close(); con.Dispose();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select Membership No')", true);
        }
        catch (NullReferenceException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
    }
    private void BindGridExp()
    {
        adp = new SqlDataAdapter("select * from StudentExp where SID='" + txtSID.Text.ToString() + "' and Type='" + ddlSponExpType.SelectedValue.ToString() + "'", con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridSponExp.DataSource = dt;
        GridSponExp.DataBind();
    }
    protected void GridSponExp_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("select * from StudentExp where SID='" + GridSponExp.SelectedRow.Cells[1].Text.ToString() + "' and Type='" + GridSponExp.SelectedRow.Cells[2].Text.ToString() + "'  and Organization='" + GridSponExp.SelectedRow.Cells[3].Text.ToString() + "' and Designation='" + GridSponExp.SelectedRow.Cells[4].Text.ToString() + "' and Location='" + GridSponExp.SelectedRow.Cells[6].Text.ToString() + "'", con); ;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lblEnrol.Text = dr["SID"].ToString();
            ddlSponExpType.SelectedValue = dr["Type"].ToString();
            txtOrg.Text = dr["Organization"].ToString();
            txtDesig.Text = dr["Designation"].ToString();
            txtYearExp.Text = dr["Year"].ToString();
            txtLocation.Text = dr["Location"].ToString();
        }
        dr.Close(); BindName(); con.Close();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("delete from StudentEdu where SID='" + GridEditEdu.SelectedRow.Cells[1].Text.ToString() + "' and EduType='" + GridEditEdu.SelectedRow.Cells[2].Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Deleted Successfully!')", true);
            clear1(); BindGrid(); GridEditEdu.Focus();
            con.Close(); con.Dispose();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select Membership No')", true);
        }
        catch (NullReferenceException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select Membership No')", true);
        }
    }
    private void clear1()
    {
        lblEnrolment.Text = ""; chkDoc.Checked = false; txtScore.Text = ""; txtYear.Text = ""; chkOMark.Checked = false; chkAtMark.Checked = false; chkAtCert.Checked = false; chkOrCert.Checked = false;
    }
    private void clear2()
    {
        lblEnrol.Text = ""; chkExp.Checked = false; txtOrg.Text = ""; txtYearExp.Text = ""; txtDesig.Text = ""; txtLocation.Text = "";
    }
    protected void btnDelExp_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("delete from StudentExp where SID='" + GridSponExp.SelectedRow.Cells[1].Text.ToString() + "' and Type='" + GridSponExp.SelectedRow.Cells[2].Text.ToString() + "'  and Organization='" + GridSponExp.SelectedRow.Cells[3].Text.ToString() + "' and Designation='" + GridSponExp.SelectedRow.Cells[4].Text.ToString() + "' and Location='" + GridSponExp.SelectedRow.Cells[6].Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Deleted SuccessFully!')", true);
            BindGridExp(); clear2(); GridSponExp.Focus();
            con.Close(); con.Dispose();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
        catch (NullReferenceException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + ex.ToString() + "')", true);
        }
    }
    protected void ddlSponExpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridExp();
    }
    private void BinXML()
    {
        XmlDocument xmlOrg = new XmlDocument();
        xmlOrg.Load(MapPath("~/Xml/EducationBoard.xml"));
        XmlNodeList lstOrg = xmlOrg.GetElementsByTagName("Board");
        foreach (XmlNode node in lstOrg)
        {
            ddlBoard.Items.Add(node.InnerText);
        }
    }
}