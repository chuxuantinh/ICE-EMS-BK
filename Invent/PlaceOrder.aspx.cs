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

public partial class Invent_PlaceOrder : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    InvenItem Iitem;
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
                pnlAddItem.Visible = false;
                pnlList.Visible = false;
                pnlProspectus.Visible = false;
                Iitem = new InvenItem();
                Iitem.getItems(ddlOType);
                txtItemPrice.Text = Iitem.getPurchesPrice(ddlOType.SelectedItem.Text);
                gen();
                datastructure();
                pnlSupplier.Visible = false;
                txtYear.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";// lblFromName.Text = "Membership No:";
                lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlSupplier.Focus();
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
    private void gen()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(OrderNo) from Purches ", con);
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
        txtDate.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblHiddenSeason.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYear.Text.ToString();
        txtYear.Focus();
    }
    protected void lbtnAddItems_Click(object sender, EventArgs e)
    {
        pnlAddItem.Visible = true;
    }
    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        Iitem = new InvenItem();
        Iitem.addItem(txtItemName.Text.ToString(), txtPurchesPrice.Text.ToString(), txtSellPrice.Text.ToString());
        ddlOType.Items.Clear();
        Iitem.getItems(ddlOType);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        pnlAddItem.Visible = false;
    }
    int supplierid, totalQt, totalAmt;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtName.Text != "")
        {
            con.Open();
            pnlSupplier.Visible = true;
            cmd = new SqlCommand("insert into Supplier(Name,Address,Address2,City,State,PinCode,Phone,Fax,Email,Mobile) values(@Name,@Address,@Address2,@City,@State,@PinCode,@Phone,@Fax,@Email,@Mobile)", con);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@Address", txtAddress1.Text.ToString());
            cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.ToString());
            cmd.Parameters.AddWithValue("@City", ddlCity.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@State", ddlState.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@PinCode", txtPinCode.Text.ToString());
            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFax.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close(); con.Dispose();
            ddlSupplier.DataBind();
            ddlSupplier.SelectedValue = txtName.Text;
            ddlSupplier.Focus();
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = ""; txtPinCode.Text = ""; txtPhone.Text = ""; txtMobile.Text = ""; txtFax.Text = ""; txtEmail.Text = ""; pnlSupplier.Visible = false;
             }
             else{
            txtAddress1.Focus();
            }
    }
    protected void lbtnSupplier_Click(object sender, EventArgs e)
    {
        pnlSupplier.Visible = true;
        ClsStateCity obStateCity = new ClsStateCity();
        obStateCity.xmlstate(ddlState, "XMLState.xml");
        obStateCity.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
        pnlSupplier.Focus();
    }
    protected void btnShowDetial_Click(object sender, EventArgs e)
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
            int i = 0, j = 0;
            DataTable dtDatas = (DataTable)ViewState["dtDatas"];

            GridViewRow row;
            row = GridView1.SelectedRow; GridView2.DataSource = dtDatas;
            GridView2.DataBind();
            if (dtDatas.Rows.Count == 0)
            {
                lblTotal.Text = "";
                lblQuantity.Text = "";
                while (i < GridView1.Rows.Count)
                {
                    TextBox txtQuan = (TextBox)GridView1.Rows[i].FindControl("txtQuantity");
                    if (txtQuan.Text != "")
                    {
                        int quan = Convert.ToInt32(txtQuan.Text);
                        quan = Convert.ToInt32(txtQuan.Text);
                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["SubjectCode"] = GridView1.Rows[i].Cells[0].Text;
                        drNewRow["SubjectName"] = GridView1.Rows[i].Cells[1].Text;
                        drNewRow["Quantity"] = quan.ToString();
                        dtDatas.Rows.Add(drNewRow);
                        totalQt += quan;
                        totalAmt += (quan * Convert.ToInt32(GridView1.Rows[i].Cells[2].Text));
                    }
                    i++;
                }
            }
            else
            {
                int k;
                for (k = 0; k < GridView1.Rows.Count; k++)
                {
                    TextBox txtQuan = (TextBox)GridView1.Rows[k].FindControl("txtQuantity");
                    if (txtQuan.Text != "")
                    {
                        IsExists = false;
                        int quan = Convert.ToInt32(txtQuan.Text);

                        for (j = 0; j < dtDatas.Rows.Count; j++)
                        {
                            DataRow dr = dtDatas.Rows[j];
                            if (GridView1.Rows[k].Cells[0].Text == dr["SubjectCode"].ToString())
                            {

                                int quan2 = Convert.ToInt32(dr["Quantity"].ToString()) + Convert.ToInt32(txtQuan.Text);
                                dr["Quantity"] = quan2.ToString();

                                totalQt += quan;
                                totalAmt += (quan * Convert.ToInt32(GridView1.Rows[k].Cells[2].Text));
                                IsExists = true;
                            }
                        }
                        if (!IsExists)
                        {
                            DataRow drNewRow = dtDatas.NewRow();
                            drNewRow["SubjectCode"] = GridView1.Rows[k].Cells[0].Text;
                            drNewRow["SubjectName"] = GridView1.Rows[k].Cells[1].Text;
                            drNewRow["Quantity"] = quan.ToString();
                            dtDatas.Rows.Add(drNewRow);

                            totalQt += quan;
                            totalAmt += (quan * Convert.ToInt32(GridView1.Rows[k].Cells[2].Text));
                        }
                    }
                }
            }
            GridView2.DataSource = dtDatas;
            GridView2.DataBind();
            lblQuantity.Text = totalQt.ToString();
            lblTotal.Text = totalAmt.ToString();
            pnlList.Focus();
        }
    
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SubjectCode");
        dtDatas.Columns.Add("SubjectName");
        dtDatas.Columns.Add("Quantity");
        ViewState["dtDatas"] = dtDatas;
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            cmd = new SqlCommand("select * from Supplier where Name='" + ddlSupplier.SelectedValue + "'", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                supplierid = Convert.ToInt32(read["ID"].ToString());
            }
            read.Close();
            int i = 0;
            while (i < GridView2.Rows.Count)
            {
                cmd = new SqlCommand("insert into PurchesList(OrderNo,SubjectCode,SubjectName,RequiredQt,CourseID) values(@OrderNo,@SubjectCode,@SubjectName,@RequiredQt,@CourseID)", con);
                cmd.Parameters.AddWithValue("@SubjectCode", GridView2.Rows[i].Cells[0].Text.ToString());
                cmd.Parameters.AddWithValue("@SubjectName", GridView2.Rows[i].Cells[1].Text.ToString());
                cmd.Parameters.AddWithValue("@RequiredQt", GridView2.Rows[i].Cells[2].Text.ToString());
                cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
                cmd.Parameters.AddWithValue("@CourseID", ddlCourseId.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                i++;
            }
            cmd = new SqlCommand("insert into Purches(Session,SID,Supplier,OrderType,OrderNo,OrderDate,RequiredQt,Amount,Status)values(@Session,@SID,@Supplier,@OrderType,@OrderNo,@OrderDate,@RequiredQt,@Amount,@Status)", con);
            cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
            cmd.Parameters.AddWithValue("@SID", supplierid.ToString());
            cmd.Parameters.AddWithValue("@Supplier", ddlSupplier.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@OrderType", ddlOType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
            cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(txtDate.Text.ToString(), dtinfo));
            cmd.Parameters.AddWithValue("@RequiredQt", lblQuantity.Text.ToString());
            cmd.Parameters.AddWithValue("@Amount", lblTotal.Text.ToString());
            cmd.Parameters.AddWithValue("@Status", "Ordered");
            cmd.ExecuteNonQuery();

            con.Close();
            DataTable dtDatas = (DataTable)ViewState["dtDatas"];
            dtDatas.Clear(); lblQuantity.Text = ""; lblTotal.Text = "";
            pnlList.Visible = false;
            GridView1.DataBind();
            gen();
            ddlSupplier.Focus();
            con.Dispose();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successful sent')", true);
        }
        catch (SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Resubmit data')", true);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlList.Visible = false;
        int i = 0;
        while (i < GridView1.Rows.Count)
        {
            TextBox txtQuan = (TextBox)GridView1.Rows[i].FindControl("txtQuantity");
            txtQuan.Text = "";
            i++;
        }
    }
    protected void ddlCourseId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourseId.Focus();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.Focus();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsStateCity obStateCity = new ClsStateCity();
        obStateCity.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
        ddlState.Focus();
    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblException.Visible = false;
        lbtnSupplier.Focus();
    }
    protected void ddlOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Iitem = new InvenItem();
        txtItemPrice.Text = Iitem.getPurchesPrice(ddlOType.SelectedItem.Text);
        if (ddlOType.SelectedValue == "Books")
        {
            pnlGrid.Visible = true;
            pnlProspectus.Visible = false;
            if (lblQuantity.Text == "") pnlList.Visible = false;
            else
            pnlList.Visible = true;
            ddlOType.Focus();
        }
        else
        {
            pnlGrid.Visible = false;
            pnlList.Visible = false;
            pnlProspectus.Visible = true;
            pnlProspectus.Focus();
        }
    }
    int amtPros;
    protected void btnPros_Click(object sender, EventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (txtPros.Text == "")
        {
            lblExceptPros.Text = "Please Insert Quantity of Prospectus.";
        }
        else
        {
            lblExceptPros.Text = "";
            con.Open();
            cmd = new SqlCommand("select ID from Supplier where Name='" + ddlSupplier.SelectedValue + "'", con);
            supplierid = Convert.ToInt32(cmd.ExecuteScalar());
            amtPros = Convert.ToInt32(txtItemPrice.Text) * Convert.ToInt32(txtPros.Text);
            cmd = new SqlCommand("insert into Purches(Session,SID,Supplier,OrderType,OrderNo,OrderDate,RequiredQt,Amount,Status)values(@Session,@SID,@Supplier,@OrderType,@OrderNo,@OrderDate,@RequiredQt,@Amount,@Status)", con);
            cmd.Parameters.AddWithValue("@Session", lblHiddenSeason.Text.ToString());
            cmd.Parameters.AddWithValue("@SID", supplierid.ToString());
            cmd.Parameters.AddWithValue("@Supplier", ddlSupplier.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@OrderType", ddlOType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@OrderNo", lblOrderNo.Text.ToString());
            cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(txtDate.Text.ToString(), dtinfo));
            cmd.Parameters.AddWithValue("@RequiredQt", txtPros.Text.ToString());
            cmd.Parameters.AddWithValue("@Amount", amtPros.ToString());
            cmd.Parameters.AddWithValue("@Status", "Ordered");
            cmd.ExecuteNonQuery();
            con.Close();
            gen();
            con.Dispose();
            txtPros.Text = "";
            lblExceptPros.Text = "successfully added.!";
            ddlSupplier.Focus();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Successful Added')", true);
        }
    }
    protected void txtPros_TextChanged(object sender, EventArgs e)
    {
        lblExceptPros.Text = "";
    }
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPart.Focus();
    }
}