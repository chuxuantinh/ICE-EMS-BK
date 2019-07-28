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
public partial class Invent_IMOrderEntry : System.Web.UI.Page
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
                pnlGrid.Visible = false;
                pnlList.Visible = false;
                pnlProspectus.Visible = false;
                gen();
                datastructure();
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fillgrd();
                ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    string bl="";
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
    }
    private void gen()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(OrderNo) from IMOrder ", con);
        con.Close();
        con.Open();
        int i;
        string id = Convert.ToString(cmdsn.ExecuteScalar());
        if (id == "")
        {
            i = 1;
        }
        else
        {
            i = Convert.ToInt32(id);
            i = i + 1;
        }
        if (i <= 9)
        {
            id = "" + i;
        }
        else if (i <= 99)
        {
            id = "" + i;
        }
        lblOrderNo.Text = id.ToString();
        con.Close();
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
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtDate.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void ddlOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOType.SelectedValue == "Books")
        {
            pnlIm.Visible = true; pnlCourse.Visible = true;
            pnlProspectus.Visible = false;
            if (lblQuantity.Text == "") pnlList.Visible = false;
            else
                pnlList.Visible = true;
            ddlOType.Focus();
        }
        else if (ddlOType.SelectedValue == "Prospectus")
        {
            pnlIm.Visible = false; pnlCourse.Visible = false;
            pnlGrid.Visible = false;
            pnlList.Visible = false; pnlsupply.Visible = false;
            pnlProspectus.Visible = true;
            txtIMID.Focus();
        }
    }
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SubjectCode");
        dtDatas.Columns.Add("SubjectName");
        dtDatas.Columns.Add("Quantity"); 
        dtDatas.Columns.Add("Amount");
        dtDatas.Columns.Add("Course");
        dtDatas.Columns.Add("Part"); dtDatas.Columns.Add("Type");
        ViewState["dtDatas"] = dtDatas;
    }string except;
    int count;
     int cspartI, cspartII,cspartIIE, cssectionA, cssectionB, aspartI, aspartII,aspartIIE, assectionA, assectionB, SCpartI, SCpartII,SCpartIIE, SCSectionA, SCSectionB, SApartI, SApartII,SApartIIE, SASectionA, SASectionB;
 
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        GridViewRow row;
        row = grdIM.SelectedRow;
        pnlList.Visible = true;
        pnlGrid.Visible = false; btnOK.Enabled = false;
        ddlCourse.Enabled = false; ddlCourseId.Enabled = false; ddlPart.Enabled = false;
        cmd = new SqlCommand("select * from IMAC where IMID='" + row.Cells[1].Text + "'", con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            txtIMID.Text = reader["IMID"].ToString();
            lblBooksAmt.Text = reader["IMTotal"].ToString().TrimEnd('0').TrimEnd('.');
            lblProspectusAmt.Text = reader["Prospectus"].ToString().TrimEnd('0').TrimEnd('.');
            lblTotalAmt.Text = reader["Total"].ToString().TrimEnd('0').TrimEnd('.');
        }
        reader.Close(); reader.Dispose();
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
        GridView3.DataSource = dtDatas;
        GridView3.DataBind();
        dtDatas.Clear();

        if (Convert.ToInt32(row.Cells[2].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Civil' and Part='PartI' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                int tot = Convert.ToInt32(row.Cells[2].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[2].Text.ToString();
                drNewRow["Amount"] = tot.ToString();
                drNewRow["Course"] = "Civil"; drNewRow["Part"] = "PartI"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[3].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Civil' and Part='PartII' and SubjectType='Regular' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                int tot = Convert.ToInt32(row.Cells[3].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[3].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Civil"; drNewRow["Part"] = "PartII"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[4].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Civil' and Part='PartII' and SubjectType='Extra' and CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                int tot = Convert.ToInt32(row.Cells[4].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[4].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Civil"; drNewRow["Part"] = "PartII"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);
                ;
            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[5].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Civil' and Part='SectionA' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                int tot = Convert.ToInt32(row.Cells[5].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[5].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Civil"; drNewRow["Part"] = "SectionA"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[6].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Civil' and Part='SectionB' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                int tot = Convert.ToInt32(row.Cells[6].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[6].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Civil"; drNewRow["Part"] = "SectionB"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[7].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Architecture' and Part='PartI' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                int tot = Convert.ToInt32(row.Cells[7].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[7].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Architecture"; drNewRow["Part"] = "PartI"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[8].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Architecture' and Part='PartII' and SubjectType='Regular' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                int tot = Convert.ToInt32(row.Cells[8].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[8].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Architecture"; drNewRow["Part"] = "PartII"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[9].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Architecture' and Part='PartII' and SubjectType='Extra' and CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                int tot = Convert.ToInt32(row.Cells[9].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[9].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Architecture"; drNewRow["Part"] = "PartII"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[10].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Architecture' and Part='SectionA' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                int tot = Convert.ToInt32(row.Cells[10].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[10].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Architecture"; drNewRow["Part"] = "SectionA"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }
        if (Convert.ToInt32(row.Cells[11].Text) > 0)
        {
            cmd = new SqlCommand("select * from SubjectMaster where Course='Architecture' and Part='SectionB' and   CourseId='" + row.Cells[12].Text + "' ", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                int tot = Convert.ToInt32(row.Cells[11].Text) * Convert.ToInt32(read["Price"].ToString().TrimEnd('0').TrimEnd('.'));
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["SubjectCode"] = read["SubjectCode"].ToString();
                drNewRow["SubjectName"] = read["SubjectName"].ToString();
                drNewRow["Quantity"] = row.Cells[11].Text.ToString();
                drNewRow["Amount"] = tot.ToString(); drNewRow["Course"] = "Architecture"; drNewRow["Part"] = "SectionB"; drNewRow["Type"] = read["SubjectType"];
                dtDatas.Rows.Add(drNewRow);

            }
            read.Close();
        }


        con.Close(); con.Dispose();
        GridView3.DataSource = dtDatas;
        GridView3.DataBind();
        lblException.Text = "Stock Not Available for:" + except;
        pnlList.Focus();
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i = 0, totalamount = 0, totalqt = 0;
        while (i < GridView3.Rows.Count)
        {
            totalamount += Convert.ToInt32(GridView3.Rows[i].Cells[3].Text);
            totalqt += Convert.ToInt32(GridView3.Rows[i].Cells[2].Text);
            i++;
        }
        lblTotal.Text = totalamount.ToString();
        lblQuantity.Text = totalqt.ToString();
       
    }
    int stoke, newstoke, req, orderno, amt, required,refund,refundamt=0;
    int suply,suplyqt;
    protected void btnSend_Click(object sender, EventArgs e)
    {
        con.Open();
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (GridView2.Visible == false)
        {
            int i = 0;
            cmd = new SqlCommand("update IMBooks set CPartI='0',CPartII='0',CPartIIE='0',CSectionA='0',CSectionB='0',APartI='0',APartII='0',APartIIE='0',ASectionA='0',ASectionB='0' where IMID='" + txtIMID.Text+ "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("select * from IMOrder where IMID='" + txtIMID.Text + "' and Status='Generated' and OrderType='Auto'", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            { refundamt=Convert.ToInt32(rdr["Refund"].ToString().TrimEnd('0').TrimEnd('.'));
                orderno = Convert.ToInt32(rdr["OrderNo"].ToString());
                amt = Convert.ToInt32(rdr["Amount"].ToString().TrimEnd('0').TrimEnd('.'));
                required = Convert.ToInt32(rdr["RequiredQt"].ToString());
                rdr.Close(); rdr.Dispose();
                while (i < GridView3.Rows.Count)
                {
                    cmd = new SqlCommand("select Stock from IMStock where IMID='" + txtIMID.Text + "' and SubCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "'", con);
                    int stock = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd = new SqlCommand("select Price from SubjectMaster where SubjectCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "'", con);
                    int price = Convert.ToInt32(cmd.ExecuteScalar());
                    int stockold = stock;
                    stock = stock - Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString());
                    if (stock >= 0) {suply = Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString());refund=price*suply;}
                    else if (stock < 0) {suply = stockold;refund=0;}
                    cmd = new SqlCommand("update IMStock set Stock='" + stock + "' where IMID='" + txtIMID.Text + "' and SubCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
               
                    cmd = new SqlCommand("select * from IMOrderList where OrderNo='" + orderno + "' and SubjectCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "' and IMID='" + txtIMID.Text + "'", con);
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        req = Convert.ToInt32(read["Required"].ToString());

                        read.Close(); read.Dispose();
                        cmd = new SqlCommand("update IMOrderList set Required='" + (req + Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString())) + "' where SubjectCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "' and OrderNo='" + orderno + "' and IMID='" + txtIMID.Text + "'", con);
                        cmd.ExecuteNonQuery();
                        
                    }
                    else
                    {
                        read.Close();
                        cmd = new SqlCommand("insert into IMOrderList(OrderNo,SubjectCode,SubjectName,Required,Supply,CourseID,IMID,Course,Part) values(@OrderNo,@SubjectCode,@SubjectName,@Required,@Supply,@CourseID,@IMID,@Course,@Part)", con);
                        cmd.Parameters.AddWithValue("@SubjectCode", GridView3.Rows[i].Cells[0].Text.ToString());
                        cmd.Parameters.AddWithValue("@SubjectName", GridView3.Rows[i].Cells[1].Text.ToString());
                        cmd.Parameters.AddWithValue("@Required", Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString()));
                        cmd.Parameters.AddWithValue("@Supply", suply);
                        cmd.Parameters.AddWithValue("@Course", GridView3.Rows[i].Cells[4].Text.ToString());
                        cmd.Parameters.AddWithValue("@Part", GridView3.Rows[i].Cells[5].Text.ToString());
                        cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@CourseID", ddlCourseId.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                        cmd.ExecuteNonQuery();
                      

           
                    }
                    suplyqt += suply;
                    refundamt += refund;
                    i++;
                }
                cmd = new SqlCommand("update IMOrder set Amount='" + (amt + Convert.ToInt32(lblTotal.Text)) + "',RequiredQt='" + (required + Convert.ToInt32(lblQuantity.Text)) + "',SupplyQt='"+suplyqt+"',Refund='"+refundamt+"' where OrderNo='" + orderno + "' and IMID='" + txtIMID.Text + "'", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                rdr.Close();
                while (i < GridView3.Rows.Count)
                {
                   
                    cmd = new SqlCommand("select Stock from IMStock where IMID='" + txtIMID.Text + "' and SubCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "'", con);
                    int stock = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd = new SqlCommand("select Price from SubjectMaster where SubjectCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "'", con);
                    int price = Convert.ToInt32(cmd.ExecuteScalar());
                    int stockold = stock;
                    stock = stock - Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString());
                    if (stock >= 0) { suply = Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString()); refund = price * suply; }
                    else if (stock < 0) {suply = stockold;refund=0;}
                    cmd = new SqlCommand("update IMStock set Stock='" + stock + "' where IMID='" + txtIMID.Text + "' and SubCode='" + GridView3.Rows[i].Cells[0].Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
               
                    cmd = new SqlCommand("insert into IMOrderList(OrderNo,SubjectCode,SubjectName,Required,Supply,CourseID,IMID,Course,Part) values(@OrderNo,@SubjectCode,@SubjectName,@Required,@Supply,@CourseID,@IMID,@Course,@Part)", con);
                    cmd.Parameters.AddWithValue("@SubjectCode", GridView3.Rows[i].Cells[0].Text.ToString());
                    cmd.Parameters.AddWithValue("@SubjectName", GridView3.Rows[i].Cells[1].Text.ToString());
                    cmd.Parameters.AddWithValue("@Required", Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString()));
                    cmd.Parameters.AddWithValue("@Supply", suply);
                    cmd.Parameters.AddWithValue("@Course", GridView3.Rows[i].Cells[4].Text.ToString());
                    cmd.Parameters.AddWithValue("@Part", GridView3.Rows[i].Cells[5].Text.ToString());
                    cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@CourseID", ddlCourseId.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                    cmd.ExecuteNonQuery();
                    suplyqt += suply;
                     refundamt+=refund;

                    i++;
                }


                cmd = new SqlCommand("insert into IMOrder(Session,IMID,Type,OrderNo,OrderDate,RequiredQt,Amount,Status,OrderType,SupplyQt,Refund)values(@Session,@IMID,@Type,@OrderNo,@OrderDate,@RequiredQt,@Amount,@Status,@OrderType,@SupplyQt,@Refund)", con);
                cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
                cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                cmd.Parameters.AddWithValue("@Type", ddlOType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
                cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(txtDate.Text.ToString(), dtinfo));
                cmd.Parameters.AddWithValue("@RequiredQt", Convert.ToInt32(lblQuantity.Text));
                cmd.Parameters.AddWithValue("@SupplyQt", suplyqt);
                cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(lblTotal.Text));
                cmd.Parameters.AddWithValue("@Status", "Generated");
                cmd.Parameters.AddWithValue("@OrderType", "Auto");
                cmd.Parameters.AddWithValue("@Refund", refundamt);
                cmd.ExecuteNonQuery();
            }
           
        }
        else
        {
            int i = 0;
                    
                    while (i < GridView3.Rows.Count)
                    {
                        cmd = new SqlCommand("insert into IMOrderList(OrderNo,SubjectCode,SubjectName,Required,CourseID,IMID,Supply,Course,Part) values(@OrderNo,@SubjectCode,@SubjectName,@Required,@CourseID,@IMID,@Supply,@Course,@Part)", con);
                        cmd.Parameters.AddWithValue("@SubjectCode", GridView3.Rows[i].Cells[0].Text.ToString());
                        cmd.Parameters.AddWithValue("@SubjectName", GridView3.Rows[i].Cells[1].Text.ToString());
                        cmd.Parameters.AddWithValue("@Required", GridView3.Rows[i].Cells[2].Text.ToString());
                        cmd.Parameters.AddWithValue("@Supply", 0);
                        cmd.Parameters.AddWithValue("@Course", GridView3.Rows[i].Cells[4].Text.ToString());
                        cmd.Parameters.AddWithValue("@Part", GridView3.Rows[i].Cells[5].Text.ToString());
                        cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@CourseID", ddlCourseId.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                        cmd.ExecuteNonQuery();
                        //newstoke = stoke - Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString());
                        //cmd = new SqlCommand("update SubjectMaster set Stoke='" + newstoke + "' where SubjectCode='" + GridView3.Rows[i].Cells[0].Text + "' and CourseID='" + ddlCourseId.SelectedValue + "'", con);
                        //cmd.ExecuteNonQuery();
                        //cmd=new SqlCommand("select Stock from IMStock where IMID='" + txtIMID.Text + "' and SubCode='" + GridView3.Rows[i].Cells[0].Text+"'",con);
                        //int stock=Convert.ToInt32(cmd.ExecuteScalar());
                        //cmd = new SqlCommand("update IMStock set Stock='"+(stock-Convert.ToInt32(GridView3.Rows[i].Cells[2].Text.ToString()))+"' where IMID='" + txtIMID.Text + "' and SubCode='" + GridView3.Rows[i].Cells[0].Text + "'", con);
                        //cmd.ExecuteNonQuery();
                        i++;
                    }
                    cmd = new SqlCommand("insert into IMOrder(Session,IMID,Type,OrderNo,OrderDate,RequiredQt,Amount,Status,OrderType,SupplyQt)values(@Session,@IMID,@Type,@OrderNo,@OrderDate,@RequiredQt,@Amount,@Status,@OrderType,@SupplyQt)", con);
                    cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
                    cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                    cmd.Parameters.AddWithValue("@Type", ddlOType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(txtDate.Text.ToString(), dtinfo));
                    cmd.Parameters.AddWithValue("@RequiredQt", lblQuantity.Text.ToString());
                    cmd.Parameters.AddWithValue("@SupplyQt", 0);
                    cmd.Parameters.AddWithValue("@Amount", lblTotal.Text.ToString());
                    cmd.Parameters.AddWithValue("@Status", "Generated");

                    cmd.Parameters.AddWithValue("@OrderType", "NonAuto");


                    cmd.ExecuteNonQuery();
                }
         
        
        con.Close();
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
        dtDatas.Clear(); lblQuantity.Text = ""; lblTotal.Text = "";
        pnlList.Visible = false;
        GridView2.DataBind();
        grdIM.DataBind();
        txtIMID.Text = "";
        ddlCourse.Enabled = true;
        ddlCourseId.Enabled = true;
        ddlPart.Enabled = true;
        btnOK.Enabled = true;
        gen();
        fillgrd();
        ddlExamSeason.Focus();
        con.Dispose();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlList.Visible = false; txtIMID.Text = "";
        ddlCourse.Enabled = true;
        ddlCourseId.Enabled = true;
        ddlPart.Enabled = true;
        btnOK.Enabled = true;
        lblBooksAmt.Text = "";
        lblTotalAmt.Text = ""; lblProspectusAmt.Text = "";
        GridView2.DataBind();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        bl = "true";
        con.Open();
        pnlsupply.Visible = false;
        lblIM.Text = "";
        cmd = new SqlCommand("select * from IMAC where IMID='" + txtIMID.Text + "'", con);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            
            lblBooksAmt.Text = reader["IMTotal"].ToString().TrimEnd('0').TrimEnd('.');
            lblProspectusAmt.Text = reader["Prospectus"].ToString().TrimEnd('0').TrimEnd('.');
            lblTotalAmt.Text = reader["Total"].ToString().TrimEnd('0').TrimEnd('.');
            if (ddlOType.SelectedValue.ToString() == "Books")
            {
                pnlGrid.Visible = true;
                pnlGrid.Focus();
            }
            else if (ddlOType.SelectedValue.ToString() == "Prospectus")
            {
                pnlGrid.Visible = false;
            }
        }
        else
        {
            txtIMID.Text = "";
            lblIM.Text = "Invalid IMID";
            txtIMID.Focus();
        }
       
        reader.Close(); reader.Dispose();
        con.Close(); con.Dispose();
    }
    int totalQt, totalAmt;
    protected void btnAddList_Click(object sender, EventArgs e)
    {
        if (lblQuantity.Text == "")
        {
            totalQt = 0;
        }
        else
            totalQt = Convert.ToInt32(lblQuantity.Text);
        pnlList.Visible = true;
        if (lblTotal.Text == "")
        {
            totalAmt = 0;
        }
        else totalAmt = Convert.ToInt32(lblTotal.Text);
        bool IsExists = false;
        pnlList.Visible = true;
        int i = 0, j = 0, K = 0;
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
        GridViewRow row;
        row = GridView2.SelectedRow; GridView3.DataSource = dtDatas;
        GridView3.DataBind();
        if (dtDatas.Rows.Count == 0)
        {
            lblTotal.Text = "";
            lblQuantity.Text = "";
            while (i < GridView2.Rows.Count)
            {
                TextBox txtQuan = (TextBox)GridView2.Rows[i].FindControl("txtQuantity");
                if (txtQuan.Text != "")
                {
                    int quan = Convert.ToInt32(txtQuan.Text);
                  
                        totalAmt = (quan * Convert.ToInt32(GridView2.Rows[i].Cells[2].Text));
                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["SubjectCode"] = GridView2.Rows[i].Cells[0].Text;
                        drNewRow["SubjectName"] = GridView2.Rows[i].Cells[1].Text;
                        drNewRow["Quantity"] = quan.ToString();
                        drNewRow["Amount"] = totalAmt.ToString();
                        drNewRow["Course"] = ddlCourse.SelectedValue.ToString();
                        drNewRow["Part"] = ddlPart.SelectedValue.ToString(); drNewRow["Type"] = GridView2.Rows[i].Cells[5].Text;
                        dtDatas.Rows.Add(drNewRow);
                    
                }
                i++;
            }
        }
        else
        {
            int k;
            for (k = 0; k < GridView2.Rows.Count; k++)
            {
                TextBox txtQuan = (TextBox)GridView2.Rows[k].FindControl("txtQuantity");
                if (txtQuan.Text != "")
                {
                    IsExists = false;
                    int quan = Convert.ToInt32(txtQuan.Text);
                    for (j = 0; j < dtDatas.Rows.Count; j++)
                    {
                        DataRow dr = dtDatas.Rows[j];
                        if (GridView2.Rows[k].Cells[0].Text == dr["SubjectCode"].ToString())
                        {
                           
                                int quan2 = Convert.ToInt32(dr["Quantity"].ToString()) + Convert.ToInt32(txtQuan.Text);
                                totalAmt = (quan2 * Convert.ToInt32(GridView2.Rows[k].Cells[2].Text));
                                dr["Quantity"] = quan2.ToString();
                                dr["Amount"] = totalAmt.ToString();
                                IsExists = true;
                          
                        }
                    }
                    if (!IsExists)
                    {
                        
                            totalAmt = (quan * Convert.ToInt32(GridView2.Rows[k].Cells[2].Text));
                            DataRow drNewRow = dtDatas.NewRow();
                            drNewRow["SubjectCode"] = GridView2.Rows[k].Cells[0].Text;
                            drNewRow["SubjectName"] = GridView2.Rows[k].Cells[1].Text;
                            drNewRow["Quantity"] = quan.ToString();
                            drNewRow["Amount"] = totalAmt.ToString();
                            drNewRow["Course"] = ddlCourse.SelectedValue.ToString();
                            drNewRow["Part"] = ddlPart.SelectedValue.ToString(); drNewRow["Type"] = GridView2.Rows[i].Cells[5].Text;
                            dtDatas.Rows.Add(drNewRow);
                       
                    }
                }
            }
        }
        GridView3.DataSource = dtDatas;
        GridView3.DataBind();

        
       
        pnlList.Focus();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].Visible = false;
        }
    }
    int amtPros;
    string res; int stockPros;
    protected void btnPros_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/"; if (txtIMID.Text != "")
    {
        if (txtPros.Text == "")
        {
            lblExceptPros.Text = "Please Insert Quantity of Prospectus.";
        }
        else
        {
            lblExceptPros.Text = "";
            con.Open();
            cmd = new SqlCommand("select * from SubjectMaster where Course='Prospectus'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string amt = reader["Price"].ToString().TrimEnd('0').TrimEnd('.');
                amtPros = Convert.ToInt32(amt) * Convert.ToInt32(txtPros.Text);
                stockPros = Convert.ToInt32(reader["Stoke"].ToString());
            }
            reader.Close(); reader.Dispose();
          
                
                    cmd = new SqlCommand("insert into IMOrder(Session,IMID,Type,OrderNo,OrderDate,RequiredQt,Amount,Status,SupplyQt)values(@Session,@IMID,@Type,@OrderNo,@OrderDate,@RequiredQt,@Amount,@Status,@SupplyQt)", con);
                    cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
                    cmd.Parameters.AddWithValue("@IMID", txtIMID.Text.ToString());
                    cmd.Parameters.AddWithValue("@Type", ddlOType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(txtDate.Text,dtinfo));
                    cmd.Parameters.AddWithValue("@RequiredQt", txtPros.Text.ToString());
                    cmd.Parameters.AddWithValue("@SupplyQt", 0);
                    cmd.Parameters.AddWithValue("@Amount", amtPros.ToString());
                    cmd.Parameters.AddWithValue("@Status", "Ordered");
                    cmd.ExecuteNonQuery();
                    lblExceptPros.Text = "successfully added";
              
            con.Close();
            gen();
            con.Dispose();
            txtPros.Text = ""; txtIMID.Text = "";
        }
    }
    else lblIM.Text = "Enter IMID";
    }
    protected void txtPros_TextChanged(object sender, EventArgs e)
    {
        lblExceptPros.Text = "";
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void fillgrd()
    {
        string str = "";
        str = "SELECT DISTINCT IMID, CPartI, CPartII,CPartIIE, CSectionA, CSectionB, APartI, APartII,APartIIE, ASectionA, ASectionB, CourseID FROM IMBooks WHERE (CPartI>0) OR (CPartII>0) OR (CPartIIE>0) OR (CSectionA>0) OR (CSectionB>0) OR (APartI>0) OR (APartII>0) OR (APartIIE>0) OR (ASectionA>0) OR (ASectionB>0)";
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataSet dt = new DataSet();
        adp.Fill(dt);
        grdIM.DataSource = dt;
        grdIM.DataBind();
    }
   
}
