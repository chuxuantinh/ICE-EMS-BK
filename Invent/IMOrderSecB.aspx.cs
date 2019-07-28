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

public partial class Invent_IMOrderSecB : System.Web.UI.Page
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
            }
            if (!IsPostBack)
            {
                pnlView.Visible = false;
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lblHomeRedirect_Click(object sender, EventArgs e)
    {

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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlView.Visible = true;
        GridViewRow row;
        row = GridView1.SelectedRow;
        SqlDataAdapter adp = new SqlDataAdapter("select * from Subjectmaster where Course='" + row.Cells[3].Text + "' and Part='SectionB' and SubjectType='Extra'", con);
        DataSet dt = new DataSet();
        adp.Fill(dt);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        SqlDataAdapter adp1 = new SqlDataAdapter("select * from Subjectmaster where Course='" + row.Cells[3].Text + "' and Part='SectionB' and SubjectType='Regular'", con);
        DataSet dt1 = new DataSet();
        adp1.Fill(dt1);
        GridView3.DataSource = dt1;
        GridView3.DataBind();

    }
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SubjectCode");
        dtDatas.Columns.Add("SubjectName");
        dtDatas.Columns.Add("Course");
        dtDatas.Columns.Add("Part");
        dtDatas.Columns.Add("Price");
        ViewState["dtDatas"] = dtDatas;
    }
    int refundamt, orderno, amt, required, suply, refund, req, suplyqt;
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con.Open();
        int i = 0; int count = 0;
        datastructure();
         GridViewRow row;
        row = GridView1.SelectedRow;
        GridView4.DataSource = null;
        GridView4.DataBind();
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
     dtDatas.Clear();
        while (i < GridView2.Rows.Count)
        {
            CheckBox rbApp = (CheckBox)GridView2.Rows[i].FindControl("chkapp");
           
            if (rbApp.Checked)
            {
                        count++;
                       
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = GridView2.Rows[i].Cells[4].Text.ToString();
                drNewRow["SubjectName"] = GridView2.Rows[i].Cells[5].Text.ToString();
                drNewRow["Course"] = GridView2.Rows[i].Cells[2].Text.ToString();
                drNewRow["Part"] = GridView2.Rows[i].Cells[3].Text.ToString();
                drNewRow["Price"] = GridView2.Rows[i].Cells[6].Text.ToString();
                dtDatas.Rows.Add(drNewRow);
            }
            else
            {
            }
            i++;
        }
        if (count ==5)
        {
            int j = 0;
            while (j < GridView3.Rows.Count)
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = GridView3.Rows[j].Cells[3].Text.ToString();
                drNewRow["SubjectName"] = GridView3.Rows[j].Cells[4].Text.ToString();
                drNewRow["Course"] = GridView3.Rows[j].Cells[1].Text.ToString();
                drNewRow["Part"] = GridView3.Rows[j].Cells[2].Text.ToString();
                drNewRow["Price"] = GridView3.Rows[j].Cells[5].Text.ToString();
                dtDatas.Rows.Add(drNewRow);
                j++;
            }
            ViewState["dtDatas"] = dtDatas;
            GridView4.DataSource = dtDatas;
            GridView4.DataBind();
            int ii = 0, totalamount = 0, totalqt = 0;
            while (ii < GridView4.Rows.Count)
            {
                if (GridView4.Rows[ii].Cells[4].Text == "") GridView4.Rows[ii].Cells[4].Text = "0";
                totalamount += Convert.ToInt32(GridView4.Rows[ii].Cells[4].Text);
                totalqt++;
                ii++;
            }
            lblTotal.Text = totalamount.ToString();
            lblQuantity.Text = totalqt.ToString();
            int k = 0;

            cmd = new SqlCommand("select * from IMOrder where IMID='" + row.Cells[2].Text + "' and Status='Generated' and OrderType='Auto'", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                refundamt = Convert.ToInt32(rdr["Refund"].ToString().TrimEnd('0').TrimEnd('.'));
                orderno = Convert.ToInt32(rdr["OrderNo"].ToString());
                amt = Convert.ToInt32(rdr["Amount"].ToString().TrimEnd('0').TrimEnd('.'));
                required = Convert.ToInt32(rdr["RequiredQt"].ToString());
                rdr.Close(); rdr.Dispose();
                while (k < GridView4.Rows.Count)
                {
                    cmd = new SqlCommand("select Stock from IMStock where IMID='" + row.Cells[2].Text + "' and SubCode='" + GridView4.Rows[k].Cells[0].Text.ToString() + "'", con);
                    int stock = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd = new SqlCommand("select Price from SubjectMaster where SubjectCode='" + GridView4.Rows[k].Cells[0].Text.ToString() + "'", con);
                    int price = Convert.ToInt32(cmd.ExecuteScalar());
                    int stockold = stock;
                    stock = stock - 1;
                    if (stock >= 0) { suply = 1; refund = price * suply; }
                    else if (stock < 0) { suply = stockold; refund = 0; }
                    cmd = new SqlCommand("update IMStock set Stock='" + stock + "' where IMID='" + row.Cells[2].Text + "' and SubCode='" + GridView4.Rows[k].Cells[0].Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("select * from IMOrderList where OrderNo='" + orderno + "' and SubjectCode='" + GridView4.Rows[k].Cells[0].Text.ToString() + "' and IMID='" + row.Cells[2].Text + "'", con);
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        req = Convert.ToInt32(read["Required"].ToString());

                        read.Close(); read.Dispose();
                        cmd = new SqlCommand("update IMOrderList set Required='" + (req + 1) + "' where SubjectCode='" + GridView4.Rows[k].Cells[0].Text.ToString() + "' and OrderNo='" + orderno + "' and IMID='" + row.Cells[2].Text + "'", con);
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        read.Close();
                        cmd = new SqlCommand("insert into IMOrderList(OrderNo,SubjectCode,SubjectName,Required,Supply,CourseID,IMID,Course,Part) values(@OrderNo,@SubjectCode,@SubjectName,@Required,@Supply,@CourseID,@IMID,@Course,@Part)", con);
                        cmd.Parameters.AddWithValue("@SubjectCode", GridView4.Rows[k].Cells[0].Text.ToString());
                        cmd.Parameters.AddWithValue("@SubjectName", GridView4.Rows[k].Cells[1].Text.ToString());
                        cmd.Parameters.AddWithValue("@Required", '1');
                        cmd.Parameters.AddWithValue("@Supply", suply);
                        cmd.Parameters.AddWithValue("@Course", GridView4.Rows[k].Cells[2].Text.ToString());
                        cmd.Parameters.AddWithValue("@Part", GridView4.Rows[k].Cells[3].Text.ToString());
                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                        cmd.Parameters.AddWithValue("@CourseID", 081);
                        cmd.Parameters.AddWithValue("@IMID", row.Cells[2].Text);
                        cmd.ExecuteNonQuery();
                    }
                    suplyqt += suply;
                    refundamt += refund;
                    k++;
                }
                cmd = new SqlCommand("update IMOrder set Amount='" + (amt + Convert.ToInt32(lblTotal.Text)) + "',RequiredQt='" + (required + Convert.ToInt32(lblQuantity.Text)) + "',SupplyQt='" + suplyqt + "',Refund='" + refundamt + "' where OrderNo='" + orderno + "' and IMID='" + row.Cells[2].Text + "'", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                rdr.Close();
                int a = 0;
                cmd = new SqlCommand("select max(OrderNo) from IMOrder ", con);
                int or = Convert.ToInt32(cmd.ExecuteScalar());
                or = or + 1;
                while (a < GridView4.Rows.Count)
                {

                    cmd = new SqlCommand("select Stock from IMStock where IMID='" + row.Cells[2].Text + "' and SubCode='" + GridView4.Rows[a].Cells[0].Text.ToString() + "'", con);
                    int stock = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd = new SqlCommand("select Price from SubjectMaster where SubjectCode='" + GridView4.Rows[a].Cells[0].Text.ToString() + "'", con);
                    int price = Convert.ToInt32(cmd.ExecuteScalar());
                    int stockold = stock;
                    stock = stock - 1;
                    if (stock >= 0) { suply = 1; refund = price * suply; }
                    else if (stock < 0) { suply = stockold; refund = 0; }
                    cmd = new SqlCommand("update IMStock set Stock='" + stock + "' where IMID='" + row.Cells[2].Text + "' and SubCode='" + GridView4.Rows[a].Cells[0].Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into IMOrderList(OrderNo,SubjectCode,SubjectName,Required,Supply,CourseID,IMID,Course,Part) values(@OrderNo,@SubjectCode,@SubjectName,@Required,@Supply,@CourseID,@IMID,@Course,@Part)", con);
                    cmd.Parameters.AddWithValue("@SubjectCode", GridView4.Rows[a].Cells[0].Text.ToString());
                    cmd.Parameters.AddWithValue("@SubjectName", GridView4.Rows[a].Cells[1].Text.ToString());
                    cmd.Parameters.AddWithValue("@Required", '1');
                    cmd.Parameters.AddWithValue("@Supply", suply);
                    cmd.Parameters.AddWithValue("@Course", GridView4.Rows[a].Cells[2].Text.ToString());
                    cmd.Parameters.AddWithValue("@Part", GridView4.Rows[a].Cells[3].Text.ToString());
                    cmd.Parameters.AddWithValue("@OrderNo", or);
                    cmd.Parameters.AddWithValue("@CourseID", 081);
                    cmd.Parameters.AddWithValue("@IMID", row.Cells[2].Text);
                    cmd.ExecuteNonQuery();
                    suplyqt += suply;
                    refundamt += refund;

                    a++;
                }


                cmd = new SqlCommand("insert into IMOrder(Session,IMID,Type,OrderNo,OrderDate,RequiredQt,Amount,Status,OrderType,SupplyQt,Refund)values(@Session,@IMID,@Type,@OrderNo,@OrderDate,@RequiredQt,@Amount,@Status,@OrderType,@SupplyQt,@Refund)", con);
                cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
                cmd.Parameters.AddWithValue("@IMID", row.Cells[2].Text);
                cmd.Parameters.AddWithValue("@Type", "Books");
                cmd.Parameters.AddWithValue("@OrderNo", or);
                cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(DateTime.Now, dtinfo));
                cmd.Parameters.AddWithValue("@RequiredQt",lblQuantity.Text);
                cmd.Parameters.AddWithValue("@SupplyQt", suplyqt);
                cmd.Parameters.AddWithValue("@Amount", '0');
                cmd.Parameters.AddWithValue("@Status", "Generated");
                cmd.Parameters.AddWithValue("@OrderType", "Auto");
                cmd.Parameters.AddWithValue("@Refund", refundamt);
                cmd.ExecuteNonQuery();
            }
            cmd = new SqlCommand("update IMOrderSecB set Status='Submitted' where SID='" + row.Cells[1].Text + "' and Session='"+lblHiddenSeason.Text+"'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update IMBooks set CSectionB=CSectionB-1 where IMID='" + row.Cells[2].Text + "'", con);
            cmd.ExecuteNonQuery();
            GridView1.DataBind();
            pnlView.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successfully Submitted.')", true);
            
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Extra Subject should be equal to 5.')", true);
        }
        con.Close();
        con.Dispose();
    }


    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
        
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
        
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[5].Text = e.Row.Cells[5].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
}