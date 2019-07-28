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
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Acc_ACManage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"]) == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                rdLateFees.Checked = true;
                txtIMID.Focus();
                lblrbtnMessage.Text = "Manage late fees taken from ICE(I) account.";
                //DataTable dt = new DataTable();
                //dt= fillGrid();
                //GridIM.DataSource = dt;
                //GridIM.DataBind();
                btnManage.Visible = false;
                pnlACShow.Visible = false;
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
        finally
        {
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        con.Open();
        iminfo(txtIMID.Text.ToString());
        okk(txtIMID.Text.ToString());
        btnOK.Focus();
        con.Close(); con.Dispose();
    }
    private void okk(string strid)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" + strid.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        int i = 0;
        if (chk == strid.ToString())
        {
            i += 1;
            btnOK.Enabled = true;
        }
        else
        {
            txtIMID.Text = "Invalid ID";
            lblExceptionOK.Text = "Please Insert Valid IM ID.";
        }
        if (i == 1)
        {
            lblExceptionOK.Text = "";
            iminfo(strid);
            SqlCommand cd1 = new SqlCommand("select * from IMAC where IMId='" + strid + "'", con);
            SqlDataReader rd;
            con.Close(); con.Open();
            rd = cd1.ExecuteReader();
            while (rd.Read())
            {
                lblTAmt.Text = rd[3].ToString().TrimEnd('0').TrimEnd('.');
                lblGAmt.Text = rd[4].ToString().TrimEnd('0').TrimEnd('.');
                if (rdLateFees.Checked == true)
                {
                    lblAmtTaken.Text = rd[7].ToString().TrimEnd('0').TrimEnd('.');
                }
                else if (rdDues.Checked == true)
                {
                    lblAmtTaken.Text = rd[6].ToString().TrimEnd('0').TrimEnd('.');
                }
            }
            rd.Close();
        }
        if (lblAmtTaken.Text != "0")
        {
            btnManage.Visible = true;
            pnlACShow.Visible = true;
            lblExceptionOK.Text = "";
        }
        else
        {
            lblExceptionOK.Text = "IM Amount not found for manage.";
            btnManage.Visible = false;
            pnlACShow.Visible = false;
        }
        con.Close();
        con.Dispose();
    }
    private void iminfo(string code)
    {
        SqlCommand  cmd = new SqlCommand("select * from IM where ID='" + code + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblIMName.Text = reader[1].ToString();
            lblIMAddress.Text = reader[3].ToString();
            lblIMCity.Text = reader["Address2"].ToString() + ", " + reader[4].ToString() + " ,( " + reader[5].ToString() + " )";
            lblEnrolment.Text = code.ToString();
            lblGroupID.Text = reader["GID"].ToString();
        }
        reader.Close();
    }
    SqlDataAdapter ad;

    //private DataTable fillGrid()
    //{
    //    //DataTable dt = new DataTable();
    //    //string qry = "";
    //    //if (rdDues.Checked == true)
    //    //{
    //    //    lblrbtnMessage.Text = "Manage Dues amount from IM Account taken at the time of Application form Approval.";
    //    //    qry = "SELECT * FROM [IMAccount] WHERE Credit!=0 order by IMID ";
    //    //}
    //    //else
    //    //{
    //    //    lblrbtnMessage.Text = "Manage late fees taken from ICE(I) account.";
    //    //    qry = "SELECT * FROM [IMAccount]  order by IMID ";
    //    //}
    //    //ad = new SqlDataAdapter(qry, con);
    //    //ad.Fill(dt);
    //    //return dt;
    //}
    private int tamt, gamt, lamt, diff;
    private string late;
    private void manage(string code,string gcode)
    {
     tamt = Convert.ToInt32(lblTAmt.Text); gamt = Convert.ToInt32(lblGAmt.Text);
        lamt = Convert.ToInt32(lblAmtTaken.Text);
        if (lamt > 0)
        {
            if (lamt >= tamt)
            {
                tamt = 0;
            }
            else if (tamt > lamt)
            {
                tamt = tamt - lamt;
            }
            if (lamt >= gamt)
            {
               lamt = lamt - gamt;
                gamt = 0;
            }
            else if (lamt < gamt)
            {
                gamt = gamt - lamt;
                lamt = 0;
            }
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("update IMAC set Total='" + tamt + "', GTotal='" + gamt + "', LateFeeTaken='" + lamt + "' where IMID='" + code.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update IMAC set GTotal='" + gamt + "' where GID='" + gcode.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("select * from IMAC where IMID='ICE'", con);
            SqlDataReader read;
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                late = read["Late"].ToString().TrimEnd('0').TrimEnd('.');
            }
            read.Close();
            diff = Convert.ToInt32(late) + lamt;
            cmd = new SqlCommand("update IMAC set Late='" + diff + "' where IMID='ICE'", con);
            cmd.ExecuteNonQuery();
            lblFessStatus.Text = "Transaction Successful";
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Transaction Successful')", true);
        }
        else if (lamt <= 0)
        {
            btnManage.Visible = false;
            lblFessStatus.Text = "No Late Fees Present in Account";
        }
    }
    private void manageDues(string code, string gcode)
    {
        tamt = Convert.ToInt32(lblTAmt.Text); gamt = Convert.ToInt32(lblGAmt.Text);
        lamt = Convert.ToInt32(lblAmtTaken.Text);
        bool bl = false;
        if (lamt > 0)
        {
            if (gamt >= lamt)
            {
                if (gamt >= tamt)
                {
                    gamt = gamt - lamt;
                }
                else
                {
                    gamt = lamt - gamt;
                }
            }
            else
            {
                if (tamt >= lamt)
                {
                    gamt = gamt - lamt;
                }
                else
                {
                    gamt = gamt - tamt;
                }
            }
            if(tamt>=lamt)
            {
                tamt = tamt - lamt;
                lamt = 0;
            }
            else
            {
                lamt = lamt - tamt;
                tamt = 0;
            }
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("update IMAC set Total='" + tamt + "', GTotal='" + gamt + "', Credit='" + lamt + "' where IMID='" + code.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update IMAC set GTotal='" + gamt + "' where GID='" + gcode.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            lblFessStatus.Text = "Transaction Successful";
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Transaction Successful')", true);
        }
        else if (lamt <= 0)
        {
            btnManage.Visible = false;
            lblFessStatus.Text = "No Late Fees Present in Account";
        }
    }
    
    protected void btnMamageAC_OnClick(object sender, EventArgs e)
    {
      //  if (rdLateFees.Checked == true)
      //  {
      //      manage(lblEnrolment.Text.ToString(), lblGroupID.Text.ToString());
      //      DataTable dt = new DataTable();
      //          dt= fillGrid();
      //          GridIM.DataSource = dt;
      //          GridIM.DataBind();
      //  }
      //  else if (rdDues.Checked == true)
      //  {
      //      manageDues(lblEnrolment.Text.ToString(), lblGroupID.Text.ToString());
      //      DataTable dt = new DataTable();
      //          dt= fillGrid();
      //          GridIM.DataSource = dt;
      //          GridIM.DataBind();
      //  }
      ////  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Sucessfully Submitted')", true);
      //  con.Dispose();
    }
    protected void GridIM_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        GridViewRow rw;
        rw = GridIM.SelectedRow;
        txtIMID.Text = rw.Cells[1].Text.ToString();
        lblEnrolment.Text = rw.Cells[1].Text.ToString();
        iminfo(lblEnrolment.Text.ToString());
        lblTAmt.Text = rw.Cells[2].Text;
        lblGAmt.Text = rw.Cells[3].Text;
        lblGroupID.Text=rw.Cells[4].Text.ToString();
        if (rdLateFees.Checked == true)
        {
            lblAmtTaken.Text = rw.Cells[5].Text;
        }
        else if (rdDues.Checked == true)
        {
            lblAmtTaken.Text = rw.Cells[6].Text;
        }
        tamt = Convert.ToInt32(lblTAmt.Text);
        gamt = Convert.ToInt32(lblGAmt.Text);
        lamt = Convert.ToInt32(lblAmtTaken.Text);
        if (lamt > 0)
        {
            btnManage.Visible = true;
            pnlACShow.Visible = true;
            lblExceptionOK.Text = "";
        }
        else if (lamt <= 0)
        {
            btnManage.Visible = false;
            pnlACShow.Visible = false;
        }
        con.Close(); con.Dispose();
        btnManage.Focus();

    }
    protected void GridIM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridIM.PageIndex = e.NewPageIndex;
        //DataTable dt = new DataTable();
        //        dt= fillGrid();
        //        GridIM.DataSource = dt;
        //        GridIM.DataBind();
    }
    protected void rdLateFees_CheckedChanged(object sender, EventArgs e)
    {
        //rdLateFees.Focus();
        //DataTable dt = new DataTable();
        //        dt= fillGrid();
        //        GridIM.DataSource = dt;
        //        GridIM.DataBind();
        //lblFessStatus.Text = "";
    }
    protected void rdDues_CheckedChanged(object sender, EventArgs e)
    {
        //rdDues.Focus();
        //lblFessStatus.Text = "";
        //DataTable dt = new DataTable();
        //        dt= fillGrid();
        //        GridIM.DataSource = dt;
        //        GridIM.DataBind();
    }
    protected void GridIM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.ToString().TrimEnd('0').TrimEnd('.');
            GridIM.HeaderRow.Cells[6].Text = "Debit";
        }
    }
    protected void txtIMID_TextChanged(object sender, EventArgs e)
    {
        btnOK.Focus();
    }
}
