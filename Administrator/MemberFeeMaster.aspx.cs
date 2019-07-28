using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class MemberFeeMaster : System.Web.UI.Page
{
   
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
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
    protected void lbtnNavHome_Click(object sender, EventArgs e)
    {
        //        lbtnNavHome.Text = "Home";

    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {
        try
        {

            maikal mk = new maikal();
           int lvl = mk.returnlevel(Server.HtmlEncode(Request.Cookies["MyLogin"]["UID"]).ToString(), Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]).ToString());
           con.Close(); con.Dispose(); if (lvl == 0)
            {
                
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());


            }
            else if (lvl == 1)
            {
                
                Response.Redirect("../SuperAdmin.aspx?" + Request.Cookies["redic"].Value.ToString());


            }
            else if (lvl == 2)
            {
                // Response.Redirect("Admin/SuperAdminManage.aspx");
                
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
        
    }
    protected void rbtnHonoraryFellow_CheckedChanged(object sender, EventArgs e)
    {
        lblMemebertype.Text = "_FellowMembership"; lblMType.Text = "Honorary";
        check();
    }
    protected void rbtnFellow_CheckedChanged(object sender, EventArgs e)
    {
        lblMemebertype.Text = "Membership"; lblMType.Text = "Fellow";
        check();
    }
    protected void rbtnMember_CheckedChanged(object sender, EventArgs e)
    {
        lblMemebertype.Text = "Membership"; lblMType.Text = "Member";
        check();
    }
    protected void rbtnIM_CheckedChanged(object sender, EventArgs e)
    {
        lblMemebertype.Text = "Membership"; lblMType.Text = "IM";
        check();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close();
            con.Open(); int i = 0;
            SqlCommand cmd1 = new SqlCommand("select * from MemberFeeMaster where MemberType='" + lblMType.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd1.ExecuteReader();
            if (reader.Read()) i = i + 1; else i = i + 0;
            while (reader.Read())
            {
                if (reader[0].ToString() == "") i = i + 0;
                else i = i + 1;
                if (reader[1].ToString() == "") i += 0;
                else i += i;
                lblOldEnrollFee.Text = reader[2].ToString();
                lblOldSubFee.Text = reader[3].ToString();
                if (lblOldSubFee.Text == "") { i += 0; lblOldSubFee.Text = "record not found."; } else { i += i; }
            }
            reader.Close();
            if (i==0)
            {
                SqlCommand cmd = new SqlCommand("insert into MemberFeeMaster (MemberType,EnrollFee,SubFee) values(@MemberType,@EnrollFee,@SubFee)", con);
                cmd.Parameters.AddWithValue("@MemberType", lblMType.Text.ToString());
                cmd.Parameters.AddWithValue("@EnrollFee", txtEnrollFee.Text);
                cmd.Parameters.AddWithValue("@SubFee", txtSubFee.Text);
                cmd.ExecuteNonQuery();
                lbltitleInfo.Text = "New Fee Record Inserted.";
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("update MemberFeeMaster set EnrollFee=@EnrollFee,SubFee=@SubFee where MemberType='"+lblMType.Text.ToString()+"' ", con);
              //  cmd2.Parameters.AddWithValue("@MemberType", lblMType.Text.ToString());
                cmd2.Parameters.AddWithValue("@EnrollFee", txtEnrollFee.Text);
                cmd2.Parameters.AddWithValue("@SubFee", txtSubFee.Text);
                cmd2.ExecuteNonQuery();
                lbltitleInfo.Text = lblMType.Text.ToString() + " is Updated.";
            }


        }
        catch (SqlException ex)
        {
            lbltitleInfo.Text = ex.ToString();
        }
        finally
        {
            con.Close();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
       // lbltest.Text = url.ToString();
        Response.Redirect(url.ToString());

    }
    public void check()
    {
        try
        {

            con.Close();
            con.Open();
            lblOldEnrollFee.Text = "Record not found";
            lblOldSubFee.Text = "Record not found";
            SqlCommand cmd1 = new SqlCommand("select * from MemberFeeMaster where MemberType='" + lblMType.Text.ToString() + "'", con);
            SqlDataReader reader;
            reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                String str = reader[1].ToString();
                lblOldEnrollFee.Text = reader[2].ToString();
                lblOldSubFee.Text = reader[3].ToString();
                if (str == lblMType.Text.ToString()) {  lblOldSubFee.Text = reader[3].ToString(); }
                else if(str!=lblMType.Text.ToString()) {  lblOldSubFee.Text = "Record Not Fpound"; }
                if (str == lblMType.Text.ToString()) { lblOldEnrollFee.Text = reader[2].ToString(); }
                else if(str!=lblMType.Text.ToString()) {  lblOldEnrollFee.Text = "Record Not Found."; }
              
               // if (lblOldSubFee.Text == "") { i += 0; lblOldSubFee.Text = "Record not found."; } else { i += i; }
            }
            //if (reader.Read()) { i += 0; } else { lblOldSubFee.Text = "record not found"; }
            reader.Close();
            con.Close();
        }
        catch (SqlException ex)
        {
            lbltitleInfo.Text = ex.ToString();
        }
    }
    

}
