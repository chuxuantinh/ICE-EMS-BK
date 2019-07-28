using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Globalization;

public partial class Invent_IMOrderSupply : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
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
                btnSupply.Enabled = false;
                pnlBooks.Visible = false; pnlProspectus.Visible = false;
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                ddlExamSeason.Focus();
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).ToString("dd/MM/yyyy") ;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSupply.Enabled = true;
        GridViewRow rw;
        rw = GridView1.SelectedRow; if (rw.Cells[2].Text == "Books")
        {
            lblOrderNo.Text = rw.Cells[3].Text;
            lblIMID.Text = rw.Cells[1].Text;
            pnlBooks.Visible = true; pnlProspectus.Visible = false;
        }
        else 
        {
            lblPros.Text = rw.Cells[3].Text;
            lblIMID.Text = rw.Cells[1].Text;
            lblReqPros.Text=rw.Cells[6].Text;
            pnlBooks.Visible = false; pnlProspectus.Visible = true;
        }
    }
    int req, totalQt,stoke,sup;
    string except,ordertype;
    bool flag = false;
    protected void btnSupply_Click(object sender, EventArgs e)
    {
       
        
            lblException.Text = "";
            con.Open();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            if (btnSupply.Enabled==true)
            {
            int i = 0;
            cmd = new SqlCommand("select * from IMOrder where OrderNo='" + lblOrderNo.Text + "'", con);
            SqlDataReader read;
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                ordertype = read["OrderType"].ToString();
                req = Convert.ToInt32(read["RequiredQt"].ToString());
                if (read["SupplyQt"].ToString() == "")
                    totalQt = 0;
                else
                    totalQt = Convert.ToInt32(read["SupplyQt"].ToString());
            }
            read.Close(); read.Dispose();

            while (i < grdShow.Rows.Count)
            {
                TextBox txtQuan = (TextBox)grdShow.Rows[i].FindControl("txtQuantity");
                Label lblQuan = (Label)grdShow.Rows[i].FindControl("lblQuantity");
                if (txtQuan.Text == "")
                {
                    txtQuan.Text = "0";
                }
                if (txtQuan.Text != "")
                {
                    if (Convert.ToInt32(grdShow.Rows[i].Cells[4].Text) == Convert.ToInt32(grdShow.Rows[i].Cells[5].Text))
                    {
                    }
                    else if (Convert.ToInt32(grdShow.Rows[i].Cells[4].Text) >= Convert.ToInt32(txtQuan.Text) + Convert.ToInt32(grdShow.Rows[i].Cells[5].Text))
                    {
                        cmd = new SqlCommand("select * from SubjectMaster where CourseID='" + grdShow.Rows[i].Cells[7].Text + "' and SubjectCode='" + grdShow.Rows[i].Cells[2].Text + "'", con);
                        SqlDataReader reader;
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader["Stoke"].ToString() == "")
                                stoke = 0;
                            else
                            {
                                stoke = Convert.ToInt32(reader["Stoke"].ToString());

                            }
                        }
                        reader.Close(); reader.Dispose();
                        sup = Convert.ToInt32(grdShow.Rows[i].Cells[5].Text.ToString());
                        int totalSupply = Convert.ToInt32(txtQuan.Text) + sup;
                        //if (stoke >= Convert.ToInt32(txtQuan.Text))
                        //{
                        cmd = new SqlCommand("update IMOrderList set Supply='" + totalSupply + "' where OrderNo='" + lblOrderNo.Text + "' and SubjectCode='" + grdShow.Rows[i].Cells[2].Text + "' and CourseId='" + grdShow.Rows[i].Cells[7].Text + "'", con);
                        cmd.ExecuteNonQuery();
                        stoke = stoke - Convert.ToInt32(txtQuan.Text);
                        cmd = new SqlCommand("update SubjectMaster set Stoke='" + stoke + "' where SubjectCode='" + grdShow.Rows[i].Cells[2].Text + "' and CourseId='" + grdShow.Rows[i].Cells[7].Text + "'", con);
                        cmd.ExecuteNonQuery(); totalQt += Convert.ToInt32(txtQuan.Text);
                        //}
                        //else
                        //{
                        //    except += grdShow.Rows[i].Cells[2].Text + ",";
                        //}
                    }
                    else
                    {
                        lblException.Text = "Supply Quantity is greater than Required!!";

                    }
                }
                if (ordertype == "NonAuto")
                {
                    cmd = new SqlCommand("select Stock from IMStock where IMID='" + lblIMID.Text + "' and SubCode='" + grdShow.Rows[i].Cells[2].Text + "'", con);
                    int stock = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("update IMStock set Stock='" + (stock + Convert.ToInt32(txtQuan.Text)) + "' where IMID='" + lblIMID.Text + "' and SubCode='" + grdShow.Rows[i].Cells[2].Text + "'", con);
                    cmd.ExecuteNonQuery();

                }
                else if (ordertype == "Auto")
                {
                    cmd = new SqlCommand("select Stock from IMStock where IMID='" + lblIMID.Text + "' and SubCode='" + grdShow.Rows[i].Cells[2].Text + "'", con);
                    int stock = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("update IMStock set Stock='" + (stock + Convert.ToInt32(txtQuan.Text)) + "' where IMID='" + lblIMID.Text + "' and SubCode='" + grdShow.Rows[i].Cells[2].Text + "'", con);
                    cmd.ExecuteNonQuery();
                }
                i++;
            }

            if (except == null)
            {
                if (req == totalQt)
                {
                    if (ordertype == "NonAuto")
                    {
                        cmd = new SqlCommand("update IMOrder set SupplyQt='" + totalQt + "',Status='Supplied',DeliverDate='" + DateTime.Now + "' where OrderNo='" + lblOrderNo.Text + "' and IMID='" + lblIMID.Text + "' ", con);
                    }
                    else if (ordertype == "Auto")
                    {
                        cmd = new SqlCommand("update IMOrder set SupplyQt='" + totalQt + "',Status='Generated',DeliverDate='" + DateTime.Now + "' where OrderNo='" + lblOrderNo.Text + "' and IMID='" + lblIMID.Text + "' ", con);
                    }
                }
                else
                {
                    cmd = new SqlCommand("update IMOrder set SupplyQt='" + totalQt + "',Status='Generated',DeliverDate='" + DateTime.Now + "' where OrderNo='" + lblOrderNo.Text + "' and IMID='" + lblIMID.Text + "' ", con);
                }
                cmd.ExecuteNonQuery();
            }
            else lblException.Text = "";// "Stock Not Available For:" + except;
            GridView1.DataBind();
            grdShow.DataBind();
            GridView1.Focus();
            con.Close(); con.Dispose();
           
        }
            flag = true;
            btnSupply.Enabled = false;
           
    }
    protected void grdShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtQuan = (TextBox)e.Row.FindControl("txtQuantity");
            Label lblQuan = (Label)e.Row.FindControl("lblQuantity");
            if (e.Row.Cells[5].Text == "&nbsp;")
                e.Row.Cells[5].Text = "0";
            if (Convert.ToInt32(e.Row.Cells[4].Text) == Convert.ToInt32(e.Row.Cells[5].Text))
            {
                txtQuan.Visible = false;
                lblQuan.Visible = true;
                lblQuan.Text = e.Row.Cells[5].Text.ToString();
            }
        }
                
    }
    protected void btnPros_Click(object sender, EventArgs e)
    {
        //con.Open();
        //cmd=new SqlCommand("select * from IMOrder where OrderType='Prospectus' and OrderNo='"+lblPros.Text+"' and Session='"+lblHiddenSeason.Text+"'",con);
        //SqlDataReader read = cmd.ExecuteReader();

        //con.Close();
        //con.Dispose();
    }
    protected void grdShow_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}