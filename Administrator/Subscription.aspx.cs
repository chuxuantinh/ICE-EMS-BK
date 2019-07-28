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
using System.Globalization;

public partial class Administrator_Subscription : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd; ClsAccount account = new ClsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(MyLogin.login[0]) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    panelProfile.Visible = false; panelChangeStatus.Visible = false; panelSubscribe.Visible = false;
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    ToBeExpireIM();
                    txtSubDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    if (Request.QueryString["id"] == "")
                    {
                    }
                    else
                    {
                        txtIMID.Text = Request.QueryString["id"];
                        showprofile(Request.QueryString["id"]);
                    }
                }
            }
            txtIMID.Focus();
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
    private DataSet fillgrid()
    {
        string qry = "";
        //if (ddlType.SelectedValue.ToString() == "All")
        //{
        //    if (ddlStatus.SelectedValue.ToString() == "Subscription")
        //    {
        //        qry = "select ID,Name,Designation,Address,State,Type,RegDate, RenewDate, ExpDate from Member where RenewDate<='" + DateTime.Now + "'";
        //    }
        //    if (ddlStatus.SelectedValue.ToString() == "Disactive")
        //    {
        //        qry = "select ID,Name,Address,State,Email,Type,RegDate,YearFrom from Member where Active='no'";
        //    }
        //    else if (ddlStatus.SelectedValue.ToString() == "Active")
        //    {
        //        qry = "select ID,Name,Address,State,Email,Type,RegDate,YearFrom from Member where Active='yes'";
        //    }
        //    else if (ddlStatus.SelectedValue.ToString() == "Registered")
        //    {
        //        qry = "select ID,Name,Address,State,Email,Type,RegDate,YearFrom from Member where Active='Registered' or Active='Register'";
        //    }
        //    else if (ddlStatus.SelectedValue.ToString() == "Blocked")
        //    {
        //        qry = "select ID,Name,Address,State,Email,Type,RegDate, ExpDate, YearFrom from Member where Active='Block'";
        //    }
        //    else if (ddlStatus.SelectedValue.ToString() == "Expired")
        //    {
        //        qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where ExpDate<='" + DateTime.Now + "'";
        //    }
        //}
        if (ddlType.SelectedValue.ToString() == "IM")
        {
            if (ddlStatus.SelectedValue.ToString() == "Subscription")
            {
                qry = "select ID,Name,Designation,Address,State,Type,RegDate, RenewDate, ExpDate from Member where RenewDate<='" + DateTime.Now + "' and Type='IM'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Designation,Address,State,Type,RegDate, RenewDate, ExpDate from Member where Active='no' and Type='IM'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Expired")
            {
                qry = "select ID,Name,Designation,Address,State,Type,RegDate, RenewDate, ExpDate from Member where ExpDate<='" + DateTime.Now + "' and Type='IM'";
            }
        }
        else if (ddlType.SelectedValue.ToString() == "Fellow")
        {
            if (ddlStatus.SelectedValue.ToString() == "Subscription")
            {
                qry = "select ID,Name,Designation,Address,State,Type,RegDate, RenewDate, ExpDate from Member where RenewDate<='" + DateTime.Now + "' and Type='Fellow'";
            }
            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where Active='no' and Type='Fellow'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Expired")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where ExpDate<='" + DateTime.Now + "' and Type='Fellow'";
            }
        }
        else if (ddlType.SelectedValue.ToString() == "Honorary")
        {
            if (ddlStatus.SelectedValue.ToString() == "Subscription")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where RenewDate<='" + DateTime.Now + "'  and Type='Honorary'";
            }
            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where Active='no' and Type='Honorary'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Expired")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where ExpDate<='" + DateTime.Now + "' and Type='Honorary'";
            }
        }
        else if (ddlType.SelectedValue.ToString() == "Member")
        {
            if (ddlStatus.SelectedValue.ToString() == "Subscription")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where RenewDate<='" + DateTime.Now + "' and Type='Member'";
            }
            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where Active='no' and Type='Member'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Expired")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where ExpDate<='" + DateTime.Now + "' and Type='Member'";
            }
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        return ds;
    }
    protected void btnViewGrid_Onclick(object sender, EventArgs e)
    {
      GridDuplicacy.DataSource = fillgrid();
        GridDuplicacy.DataBind();
       
        
    }
    protected void Grid_OnselectedIndexChanged(object sender, EventArgs e)
    {
        panelProfile.Visible = true;

        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?name=" + Request.QueryString["name"] + "&lnk=" + Request.QueryString["lnk"] + "&typ=" + Request.QueryString["typ"] + "&lvl=" + Request.QueryString["zero"] + "&id=" + GridDuplicacy.SelectedRow.Cells[1].Text.ToString());
    }
    private void showprofile(string id)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            con.Close(); con.Open();
            cmd = new SqlCommand("select Name from Member where ID='" + id.ToString() + "'", con);
            string chkexist = Convert.ToString(cmd.ExecuteScalar());
            if (chkexist == "")
            {
                //lblException.Text = "Invalid Membership No." + txtIMID.Text.ToString();
                txtIMID.Text = "";
                panelProfile.Visible = false; panelChangeStatus.Visible = false; panelSubscribe.Visible = false;
                txtIMID.Focus();
            }
            else
            {
                SqlDataReader reader;
                cmd = new SqlCommand("select max(TransID) from  MemberFee where ID='" + id.ToString() + "'", con);
                string tid = Convert.ToString(cmd.ExecuteScalar());
                if (tid == "")
                {
                    tid = "0";
                    lblBalance.Text = "0";
                    lblBalanceType.Text = "";
                }
                else
                {
                    cmd = new SqlCommand("select  TransType, Balance from  MemberFee where ID='" + id.ToString() + "' and TransID='" + Convert.ToInt32(tid.ToString()) + "'", con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lblBalanceType.Text = reader["TransType"].ToString();
                        lblBalance.Text = reader["Balance"].ToString().TrimEnd('0').TrimEnd('.');
                    }
                    reader.Close();
                }
                panelProfile.Visible = true; panelChangeStatus.Visible = true; panelSubscribe.Visible = true;
                cmd = new SqlCommand("select * from Member where ID='" + id.ToString() + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblName.Text = reader[3].ToString();
                    lblMemberTyep.Text = reader["Type"].ToString();
                    lblID.Text = reader[2].ToString();
                    lblAddress.Text = reader[7].ToString();
                    lblCity.Text = reader["Address2"].ToString() + ", " + reader[8].ToString();
                    lblEmail.Text = reader[14].ToString();
                    lblPhonne.Text = reader[10].ToString();
                    lblMobile.Text = reader[13].ToString();
                    string sts = reader[71].ToString();
                    if (sts.ToString() == "yes" | sts.ToString() == "Active")
                    {
                        lblStatus2.Text = "Active";
                        btnchangeStatus.Text = "Disactive";
                    }
                    else if (sts.ToString() == "Register" | sts.ToString() == "Registered")
                    {
                        lblStatus2.Text = "New Registration";
                        btnchangeStatus.Text = "Generate Membership";
                    }
                    else
                    {
                        lblStatus2.Text = "Disactive";
                        btnchangeStatus.Text = "Active";
                    }
                    lblEnrollDate.Text = Convert.ToDateTime(reader[72].ToString()).ToString("dd/MM/yyyy");
                    lblRenuwalDate.Text = Convert.ToDateTime(reader[73].ToString()).ToString("dd/MM/yyyy");
                    lblExpDate.Text = Convert.ToDateTime(reader[74].ToString()).ToString("dd/MM/yyyy");
                    lblfrom.Text = reader[75].ToString();
                    lblTo.Text = reader[76].ToString();
                }
                reader.Close();
                GridBalance.DataSource = GridBalanceData(id);
                GridBalance.DataBind();
                getFee();
                FeeMaster fm = new FeeMaster();
                lblSubscriptionFrom.Text = fm.nextSession(lblTo.Text);
                lblSubscriptionTo.Text = fm.nextSession(lblSubscriptionFrom.Text.ToString());
            }
        }
        catch (SqlException ex)
        {
            panelProfile.Visible = false; panelChangeStatus.Visible = false; panelSubscribe.Visible = false;
        }
        finally
        {
            con.Close();
        }
    }
    private DataTable GridBalanceData(string id)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select YearFrom, YearTo, Amt, TransType, Balance, FeeType, SubDate, SubType, AcountNo, DD, Bank from MemberFee where ID='" + id.ToString() + "' Order by TransID Desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        return dt;
    }
    protected void txtIMID_TextChaned(object sender, EventArgs e)
    {
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?name=" + Request.QueryString["name"] + "&lnk=" + Request.QueryString["lnk"] + "&typ=" + Request.QueryString["typ"] + "&lvl=" + Request.QueryString["zero"] + "&id=" + txtIMID.Text.ToString());
    }

    string strstatus;
    protected void btnChnageStatsu(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        if (lblStatus2.Text == "Disactive")
        {
            btnchangeStatus.Text = "Disactive"; strstatus = "yes";
            lblStatus2.Text = "Active";
        }
        else if (lblStatus2.Text == "Active")
        {
            btnchangeStatus.Text = "Active"; strstatus = "no";
            lblStatus2.Text = "Disactive";
        }
        if (lblStatus2.Text == "New Registration")
        {
            lblExceptionActive.Text = "Membership ID Not generated yet.";
        }
        else
        {
            cmd = new SqlCommand("update Member set Active='" + strstatus.ToString() + "' where ID='" + txtIMID.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("update IM set Active='" + strstatus.ToString() + "' where ID='" + txtIMID.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            lblExceptionActive.Text = "";
        }
    }
    protected void btnSubscribe_Onclick(object sender, EventArgs e)
    {
        dtinfo.DateSeparator = "/";
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        FeeMaster fm = new FeeMaster();
        string sto = fm.rtnlvl(lblSubscriptionFrom.Text);
        string sub = fm.rtnlvl(lblTo.Text);
        if (Convert.ToInt32(sub) <= Convert.ToInt32(sto))
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("update Member set RenewDate=@RenewDate,ExpDate=@ExpDate, YearFrom=@YearFrom, YearTo=@YearTo where ID='" + lblID.Text.ToString() + "'", con);
            DateTime sd = new DateTime();
            sd = Convert.ToDateTime(txtSubDate.Text.ToString(), dtinfo);
            sd = sd.AddMonths(12);
            cmd.Parameters.AddWithValue("@RenewDate", sd);
            sd = sd.AddMonths(6);
            cmd.Parameters.AddWithValue("@ExpDate", sd);
            cmd.Parameters.AddWithValue("@YearFrom", lblSubscriptionFrom.Text.ToString());
            cmd.Parameters.AddWithValue("@YearTo", lblSubscriptionTo.Text.ToString());
            cmd.ExecuteNonQuery();
            imtrans(txtIMID.Text.ToString(), lblSubFee.Text.ToString(), "Subscription Fees", "Subscription");
            amountupdate(txtIMID.Text.ToString(), Convert.ToInt32(lblSubFee.Text));
            ClsAccount amount1 = new ClsAccount();
            amount1.AmountSubmit(txtIMID.Text.ToString(), "", DateTime.Now, "Debit", lblSubFee.Text, lblSubscriptionFrom.Text.ToString(), "Subscription Fees");
            con.Close();
            con.Dispose();
        }
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString());
    }
    public void imtrans(string imid, string amt, string feetype, string stype)
    {
        int tid;
        int bl, fee, dif = 0;
        string ttype;
        SqlCommand cmd2 = new SqlCommand();
        try
        {
            cmd2 = new SqlCommand("select max(TransID) from  MemberFee where ID='" + imid.ToString() + "'", con);
            tid = Convert.ToInt32(cmd2.ExecuteScalar());
            if (tid == null)
            {
                bl = 0;
                tid = 0;
                ttype = "Credit";
            }
        }
        catch (InvalidCastException ex)
        {
            bl = 0;
            tid = 0;
            ttype = "Credit";
        }
        cmd2 = new SqlCommand("select TransType from MemberFee Where ID='" + imid.ToString() + "' and TransID='" + tid + "'", con);
        ttype = Convert.ToString(cmd2.ExecuteScalar());
        if (ttype == "" && tid == 0)
            ttype = "Credit";
        bl = Convert.ToInt32(lblBalance.Text);
        fee = Convert.ToInt32(amt);
        if (ttype == "Debit")
        {
            dif = fee + bl;
            ttype = "Debit";
        }
        else if (ttype == "Credit")
        {
            if (bl >= fee)
            {
                dif = bl - fee;
                ttype = "Credit";
            }
            else
            {
                dif = fee - bl;
                ttype = "Debit";
            }
        }
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        tid += 1;
        cmd2 = new SqlCommand("insert into MemberFee (MType, ID, Amt, FeeType, SubDate, SubType, AcountNo, DD, Bank, YearFrom, YearTo,TransType,Balance,TransID) values(@MType, @ID, @Amt, @FeeType, @SubDate, @SubType, @AcountNo, @DD, @Bank, @YearFrom, @YearTo, @TransType, @Balance,@TransID)", con);
        cmd2.Parameters.AddWithValue("@MType", lblMemberTyep.Text.ToString());
        cmd2.Parameters.AddWithValue("@ID", imid.ToString());
        cmd2.Parameters.AddWithValue("@Amt", amt.ToString());
        cmd2.Parameters.AddWithValue("@FeeType", feetype.ToString());
        cmd2.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(txtSubDate.Text.ToString(), dtinfo));
        cmd2.Parameters.AddWithValue("@SubType", stype.ToString());
        cmd2.Parameters.AddWithValue("@AcountNo", "N/A");
        cmd2.Parameters.AddWithValue("@DD", "N/A");
        cmd2.Parameters.AddWithValue("@Bank", "N/A");
        cmd2.Parameters.AddWithValue("@YearFrom", lblSubscriptionFrom.Text);
        cmd2.Parameters.AddWithValue("@YearTo", lblSubscriptionTo.Text);
        cmd2.Parameters.AddWithValue("@TransType", ttype.ToString());
        cmd2.Parameters.AddWithValue("@Balance", dif.ToString());
        cmd2.Parameters.AddWithValue("@TransID", tid);
        cmd2.ExecuteNonQuery();
        lblBalanceType.Text = ttype.ToString();
        lblBalance.Text = dif.ToString();
    }
    public void getFee()
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from MemberFeeMaster where MemberType='" + lblMemberTyep.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lblSubFee.Text = reader[3].ToString().TrimEnd('0');
            lblSubFee.Text = lblSubFee.Text.TrimEnd('.');
        }
        reader.Close();
        con.Close();
    }
    // To be Expire IM.
    private void ToBeExpireIM()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select ID,Name,Designation,Address,State,Email,RegDate, RenewDate, ExpDate from Member where RenewDate<='" + DateTime.Now + "'", con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridDuplicacy.DataSource = ds;
        GridDuplicacy.DataBind();
        //if (GridDuplicacy.Rows.Count > 0)
        //{
        //    for (int i = 0; i < GridDuplicacy.Rows.Count; i++)
        //    {
        //        GridDuplicacy.Rows[i].Cells[7].Text = Convert.ToDateTime(GridDuplicacy.Rows[i].Cells[7].Text).ToString("dd/MM/yyyy");
        //        GridDuplicacy.Rows[i].Cells[8].Text = Convert.ToDateTime(GridDuplicacy.Rows[i].Cells[8].Text).ToString("dd/MM/yyyy");
        //        GridDuplicacy.Rows[i].Cells[9].Text = Convert.ToDateTime(GridDuplicacy.Rows[i].Cells[9].Text).ToString("dd/MM/yyyy");
        //    }
        //}
    }
    public void amountupdate(string imidi, int amount)
    {
        try
        {
            int x, z, w;
            string y, v;
            string str = "select IMID,Total,Credit,GID,GTotal from IMAC where IMID='" + imidi + "'  ";
            cmd = new SqlCommand(str, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                x = Convert.ToInt32(rd["Total"]);
                y = (rd["IMID"]).ToString();
                z = Convert.ToInt32(rd["Credit"]);
                w = Convert.ToInt32(rd["GTotal"]);
                v = (rd["GID"]).ToString();

                if (x > amount)
                {
                    x = x - amount;
                }
                else
                {
                    amount = amount - x;
                    z = z + amount;
                }
                rd.Close(); rd.Dispose();
                string str1 = "update  IMAC set Total=@imactotal,Credit=@credit where IMID='" + imidi + "'";
                cmd = new SqlCommand(str1, con);
                cmd.Parameters.AddWithValue("@imactotal", x);
                cmd.Parameters.AddWithValue("@credit", z);
                cmd.ExecuteNonQuery();
                w = w - amount;
                string str2 = "update IMAC set GTotal=@gtotal where GID='" + v + "'";
                cmd = new SqlCommand(str2, con);
                cmd.Parameters.AddWithValue("@gtotal", w);
                cmd.ExecuteNonQuery();
            }
        }
        catch (IndexOutOfRangeException ex)
        {
        }
        catch (SqlException ex)
        {
        }
        finally
        {
        }
    }
   
    protected void GridBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void GridDuplicacy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToString("dd/MM/yyyy");
            e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToString("dd/MM/yyyy");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
    protected void GridBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        GridDuplicacy.AllowPaging = false;
        GridDuplicacy.DataSource = fillgrid();
        GridDuplicacy.DataBind();
        if (GridDuplicacy.Rows.Count > 0)
        {
            GridDuplicacy.Columns[0].Visible = false;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=Subscription.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridDuplicacy.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridDuplicacy.AllowPaging = false;
        GridDuplicacy.DataSource = fillgrid();
        GridDuplicacy.DataBind();
        if (GridDuplicacy.Rows.Count > 0)
        {
            GridDuplicacy.Columns[0].Visible = false;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=Subscription.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridDuplicacy.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridDuplicacy.AllowPaging = false;
        GridDuplicacy.DataSource = fillgrid();
        GridDuplicacy.DataBind();
        if (GridDuplicacy.Rows.Count > 0)
        {
            GridDuplicacy.Columns[0].Visible = false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=Subscription.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridDuplicacy.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
    }
}


