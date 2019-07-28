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


public partial class Administrator_IMInspection : System.Web.UI.Page
{
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd; ClsStateCity clsstatecity = new ClsStateCity(); FeeMaster fmcls = new FeeMaster();
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
                    ddlType.SelectedValue = "All"; ddlStatus.SelectedValue = "Registered";
                    clsstatecity.xmlstate(ddlState, "XMLState.xml");
                    clsstatecity.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
                    txtIMID.Text = Request.QueryString["id"].ToString();
                    if (Request.QueryString["id"] == "")
                    {
                        panelProfile.Visible = false; panelInspection.Visible = false; panelChangeStatus.Visible = false; panelSubscribe.Visible = false;
                    }
                    else
                    {
                        panelProfile.Visible = true;
                        showprofile(Request.QueryString["id"]);
                    }
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) { ddlsession.SelectedValue = "Sum"; ddlSessionFrom.SelectedValue = "Sum"; }
                    else { ddlsession.SelectedValue = "Win"; ddlSessionFrom.SelectedValue = "Win"; }
                    txtSession.Text = DateTime.Now.Year.ToString(); txtYearFrom.Text = DateTime.Now.Year.ToString();
                    lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
                    SqlDataAdapter ad = new SqlDataAdapter("select ID,Name,Address,State,Email,Type,RegDate,YearFrom from Member where Active='Registered' or Active='Register'", con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    GridDuplicacy.DataSource = ds;
                    GridDuplicacy.DataBind();
                    lblInspectionNo.Text = "0";
                    maikal devnagar = new maikal();
                    int lvl = devnagar.returnlevel(MyLogin.login[1].ToString(), MyLogin.login[0].ToString());
                    if (lvl == 2)
                    {
                        panelChangeStatus.Enabled = false; panelSubscribe.Enabled = false;
                    }
                    txtAddress.Focus();
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
    protected void txtdevYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    protected void ddldevExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSessionHiddend.Text = ddlsession.SelectedValue.ToString() + "" + txtSession.Text.ToString();
        txtSession.Focus();
    }
    private DataSet fillgrid()
    {
        string qry = "";
        if (ddlType.SelectedValue.ToString() == "All")
        {
            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Address,State,Email,Type,RegDate,YearFrom from Member where Active='no'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Active")
            {
                qry = "select ID,Name,Address,State,Email,Type,RegDate,YearFrom from Member where Active='yes'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Registered")
            {
                qry = "select ID,Name,Address,State,Email,Type,RegDate,YearFrom from Member where Active='Register'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Blocked")
            {
                qry = "select ID,Name,Address,State,Email,Type,RegDate, ExpDate,YearFrom from Member where Active='Block'";
            }
        }
        else if (ddlType.SelectedValue.ToString() == "IM")
        {

            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,PAddress,PCity,PState,Email,RegDate,HName,GID from IM where Active='no'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Active")
            {
                qry = "select ID,Name,PAddress,PCity,PState,Email,RegDate,HName,GID from IM where Active='yes'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Registered")
            {
                qry = "select ID,Name,PAddress,PCity,PState,Email,RegDate,HName,GID from IM where Active='Registered' or Active='Register'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Blocked")
            {
                qry = "select ID,Name,PAddress,PCity,PState,Email,RegDate,HName,GID from IM where Active='Block'";
            }
        }
        else if (ddlType.SelectedValue.ToString() == "Fellow")
        {

            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate,YearFrom from Member where Active='no' and Type='Fellow'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Active")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, YearFrom from Member where Active='yes' and Type='Fellow'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Registered")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate,  YearFrom from Member where Type='Fellow' and  Active='Register'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Blocked")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, ExpDate, YearFrom from Member where Active='Block' and Type='Fellow'";
            }
        }
        else if (ddlType.SelectedValue.ToString() == "Honorary")
        {

            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, YearFrom from Member where Active='no' and Type='Honorary'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Active")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, YearFrom from Member where Active='yes' and Type='Honorary'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Registered")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, YearFrom from Member where Type='Honorary' and  Active='Register'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Blocked")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, ExpDate, YearFrom from Member where Active='Block' and Type='Honorary'";
            }
        }
        else if (ddlType.SelectedValue.ToString() == "Member")
        {
            if (ddlStatus.SelectedValue.ToString() == "Disactive")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, YearFrom from Member where Active='no' and Type='Member'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Active")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, YearFrom from Member where Active='yes' and Type='Member'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Registered")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, YearFrom from Member where Type='Member' and Active='Register'";
            }
            else if (ddlStatus.SelectedValue.ToString() == "Blocked")
            {
                qry = "select ID,Name,Designation,Address,State,Email,RegDate, ExpDate, YearFrom from Member where Active='Block' and Type='Member'";
            }
        }
        SqlDataAdapter ad = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        return ds;
    }
    protected void btnViewGrid_Onclick(object sender, EventArgs e)
    {
        GridDuplicacy.DataSource =fillgrid();
        GridDuplicacy.DataBind();
    }
    protected void Grid_OnselectedIndexChanged(object sender, EventArgs e)
    {
        panelProfile.Visible = true;
        showprofile(GridDuplicacy.SelectedRow.Cells[1].Text.ToString());
        if (Request.QueryString["id"] == "")
        {
            Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString() + "" + GridDuplicacy.SelectedRow.Cells[1].Text.ToString());
        }
        else
        {
            if (Request.QueryString["id"] != GridDuplicacy.SelectedRow.Cells[1].Text.ToString())
            {
                Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?name=" + Request.QueryString["name"] + "&lnk=" + Request.QueryString["lnk"] + "&typ=" + Request.QueryString["typ"] + "&lvl=" + Request.QueryString["lvl"] + "&id=" + GridDuplicacy.SelectedRow.Cells[1].Text.ToString());
            }
        }
    }
    private void showprofile(string id)
    {
        try
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            SqlCommand cmd = new SqlCommand();
            con.Close(); con.Open();
            cmd = new SqlCommand("select Name from Member where ID='" + id.ToString() + "'", con);
            string chkexist = Convert.ToString(cmd.ExecuteScalar());
            if (chkexist == "")
            {
                //lblException.Text = "Invalid Membership No." + txtIMID.Text.ToString();
                panelProfile.Visible = false; panelInspection.Visible = false; panelChangeStatus.Visible = false; panelSubscribe.Visible = false;
                txtIMID.Focus();
            }
            else
            {
                cmd = new SqlCommand("select max(TransID) from  MemberFee where ID='" + id.ToString() + "'", con);
                string tid = Convert.ToString(cmd.ExecuteScalar());
                if (tid == "")
                {
                    tid = "0";
                    lblBalance.Text = "0";
                    lblBalanceType.Text = "Credit";
                }
                else
                {
                    cmd = new SqlCommand("select  TransType, Balance from  MemberFee where ID='" + id.ToString() + "' and TransID='" + Convert.ToInt32(tid.ToString()) + "'", con);
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        lblBalanceType.Text = rdr["TransType"].ToString();
                        lblBalance.Text = rdr["Balance"].ToString().TrimEnd('0').TrimEnd('.');
                    }
                    rdr.Close();
                }
                panelProfile.Visible = true; panelInspection.Visible = true; panelChangeStatus.Visible = true; panelSubscribe.Visible = true;
                cmd = new SqlCommand("select * from Member where ID='" + id.ToString() + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblName.Text = reader[3].ToString();
                    lblIMName.Text = lblName.Text.ToString();
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
                        sesionfrom.Visible = true;
                    }
                    else
                    {
                        lblStatus2.Text = "Disactive";
                        btnchangeStatus.Text = "Active";
                    }
                    lblEnrollDate.Text = Convert.ToDateTime(reader[72].ToString()).ToString("dd/MM/yyyy");
                    lblRenuwalDate.Text = Convert.ToDateTime(reader[73].ToString()).ToString("dd/MM/yyyy");
                    lblExpDate.Text = Convert.ToDateTime(reader[74].ToString()).ToString("dd/MM/yyyy");
                    lblyear.Text = reader[75].ToString();
                    lblYearto.Text = reader[76].ToString();
                }
                reader.Close();
                getFee();
                if (lblMemberTyep.Text != "IM")
                    panelInspection.Enabled = false;
                else panelInspection.Enabled = true;
                SqlDataAdapter ad = new SqlDataAdapter("select * from IMInspection where ID='" + id.ToString() + "' Order by SN Desc", con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                GridBalance.DataSource = ds;
                GridBalance.DataBind();
            }
        }
        catch (SqlException ex)
        {
            panelProfile.Visible = false; panelInspection.Visible = false; panelChangeStatus.Visible = false; panelSubscribe.Visible = false;
        }
        finally
        {
            con.Close();
        }
    }
    protected void txtIMID_TextChaned(object sender, EventArgs e)
    {
        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?name=" + Request.QueryString["name"] + "&lnk=" + Request.QueryString["lnk"] + "&typ=" + Request.QueryString["typ"] + "&lvl=" + Request.QueryString["lvl"] + "&id=" + txtIMID.Text.ToString());
        txtAddress.Focus();
    }
    protected void btnClean_Onclick(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        lblIMName.Text = "";
        txtAddress.Text = ""; txtAddress2.Text = "";
        txtPhone.Text = ""; txtPinCode.Text = ""; txtFeedback.Text = ""; txtInvertigetor.Text = ""; txtDesignation.Text = "";
        txtDate.Text = ""; chkBuilding.Checked = false; chkEdu.Checked = false;
        txtAddress.Focus();
    }
    protected void btnSave_ONclick(object sender, EventArgs e)
    {
        if (chkAC(Convert.ToInt32(lblInfectionFee.Text.ToString()), Convert.ToInt32(lblBalance.Text), lblBalanceType.Text))
        {
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            dtinfo.DateSeparator = "/";
            try
            {
                con.Close(); con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select max(SN) from IMInspection where ID='" + lblID.Text.ToString() + "'", con);
                string sn = Convert.ToString(cmd.ExecuteScalar());
                if (sn == "")
                    sn = "1";
                else
                {
                    sn = Convert.ToString(Convert.ToInt32(sn) + 1);
                }
                cmd = new SqlCommand("insert into IMInspection (SN,ID,Name,Investigator,Designation,Date,Address,Address2,City,State,PinCode,Phone,Status,Type,FeedBack,BuildingStatus,EduStatus) values(@SN,@ID,@Name,@Inverstigator,@Designation,@Date,@Address1,@Address2,@City,@State,@PinCode,@Phone,@Status,@Type,@FeedBack,@BuildingStatus,@EduStatus)", con);
                cmd.Parameters.AddWithValue("@SN", Convert.ToInt32(sn));
                cmd.Parameters.AddWithValue("@ID", lblID.Text.ToString());
                cmd.Parameters.AddWithValue("@Name", lblIMName.Text.ToString());
                cmd.Parameters.AddWithValue("@Inverstigator", txtInvertigetor.Text.ToString());
                cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.ToString());
                cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text.ToString(), dtinfo));
                cmd.Parameters.AddWithValue("@Address1", txtAddress.Text.ToString());
                cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.ToString());
                cmd.Parameters.AddWithValue("@City", ddlCity.Text.ToString());
                cmd.Parameters.AddWithValue("@State", ddlState.Text.ToString());
                cmd.Parameters.AddWithValue("@PinCode", txtPinCode.Text.ToString());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.ToString());
                cmd.Parameters.AddWithValue("@Status", ddlIMStatus.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Type", lblMemberTyep.Text.ToString());
                cmd.Parameters.AddWithValue("@FeedBack", txtFeedback.Text.ToString());
                if (chkBuilding.Checked == true)
                    cmd.Parameters.AddWithValue("@BuildingStatus", "Approved");
                else cmd.Parameters.AddWithValue("@BuildingStatus", "NotApproved");
                if (chkEdu.Checked == true)
                    cmd.Parameters.AddWithValue("@EduStatus", "Approved");
                else cmd.Parameters.AddWithValue("@EduStatus", "NotApprroved");
                cmd.ExecuteNonQuery();
                lblExceptionInspection.Text = "Inspection Report Submitted.";
                lblExceptionInspection.ForeColor = System.Drawing.Color.Green;
                sesionfrom.Visible = true;

                imtrans(txtIMID.Text.ToString(), lblInfectionFee.Text.ToString(), "Inspection Fees", "Inspection");
                amountupdate(txtIMID.Text.ToString(), Convert.ToInt32(lblInfectionFee.Text));
            }
            catch (SqlException ex)
            {
                lblExceptionInspection.Text = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
                Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?name=" + Request.QueryString["name"] + "&lnk=" + Request.QueryString["lnk"] + "&typ=" + Request.QueryString["typ"] + "&lvl=" + Request.QueryString["lvl"] + "&id=" + txtIMID.Text.ToString());
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Enough Amount Found.')", true);
        }
    }
    string strstatus;
    protected void btnChnageStatsu_OnClick(object sender, EventArgs e)
    {
        try
        {
            FeeMaster fmcls = new FeeMaster();
            dtinfo.DateSeparator = "/";
            dtinfo.ShortDatePattern = "dd/MM/yyyy";
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand();
            if (lblStatus2.Text == "Disactive")
            {
                btnchangeStatus.Text = "Active"; strstatus = "yes";
            }
            else if (lblStatus2.Text == "Active")
            {
                btnchangeStatus.Text = "Disactive"; strstatus = "no";
            }
            if (lblStatus2.Text == "New Registration")
            {
                cmd = new SqlCommand("select Status from IMInspection where ID='" + lblID.Text.ToString() + "'", con);
                string insp = Convert.ToString(cmd.ExecuteScalar());
                if (insp == "")
                {
                    lblExceptionActive.Text = "Inspection Report Not Submitted Yet.";
                }
                else if (insp == "NotApprove" || insp == "NotApproved")
                {
                    lblExceptionActive.Text = "Inspection Status is " + insp.ToString();
                }
                if (lblMemberTyep.Text != "IM")
                    insp = "Approve";
                if (insp == "Approve" || insp=="SubToApprove")
                {
                    if (chkAC(Convert.ToInt32(lblSubFee.Text) + Convert.ToInt32(lblEnrollFee.Text), Convert.ToInt32(lblBalance.Text), lblBalanceType.Text))
                    {
                        string gid = genid();
                        if (lblMemberTyep.Text == "IM")
                        {
                            SqlCommand cmdx = new SqlCommand("update IM set ID=@ID, Active=@Active where ID='" + lblID.Text.ToString() + "'", con);
                            cmdx.Parameters.AddWithValue("@ID", gid.ToString());
                            cmdx.Parameters.AddWithValue("@Active", "no");
                            cmdx.ExecuteNonQuery();

                            cmd = new SqlCommand("update IMAC set IMID=@IMID where IMID='" + lblID.Text.ToString() + "'", con);
                            cmd.Parameters.AddWithValue("@IMID", gid.ToString());
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("update IMInspection set ID=@ID where ID='" + lblID.Text.ToString() + "'", con);
                            cmd.Parameters.AddWithValue("@ID", gid.ToString());
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("update Account set IMID=@ID where IMID='" + lblID.Text.ToString() + "'", con);
                            cmd.Parameters.AddWithValue("@ID", gid.ToString());
                            cmd.ExecuteNonQuery();
                        }
                        cmd = new SqlCommand("update FeeAC set IMID=@ID where IMID='" + lblID.Text.ToString() + "'", con);
                        cmd.Parameters.AddWithValue("@ID", gid.ToString());
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("update Member set ID=@ID,Active=@Active,YearFrom=@YearFrom, YearTo=@YearTo where ID='" + lblID.Text.ToString() + "'", con);
                        cmd.Parameters.AddWithValue("@Active", "no");
                        cmd.Parameters.AddWithValue("@ID", gid.ToString());
                        cmd.Parameters.AddWithValue("@YearFrom", ddlSessionFrom.SelectedValue.ToString() + "" + txtYearFrom.Text.ToString());
                        cmd.Parameters.AddWithValue("@YearTo", fmcls.nextSession(ddlSessionFrom.SelectedValue.ToString() + "" + txtYearFrom.Text.ToString()));
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update MemberFee set ID=@ID where ID='" + lblID.Text.ToString() + "'", con);
                        cmd.Parameters.AddWithValue("@ID", gid.ToString());
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("Update IMBooks set IMID='" + gid + "' where IMID='" + lblID.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update IMStock set IMID='" + gid + "' where IMID='" + lblID.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update DiaryEntry set IMID='" + gid.ToString() + "', MembershipNo='" + gid.ToString() + "' where IMID='" + lblID.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update DairyCount set IMID='" + gid.ToString() + "' where IMID='" + lblID.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        btnchangeStatus.Text = "New ID: " + gid.ToString(); lblID.Text = gid.ToString();
                        lblStatus2.Text = "Disactive";
                        lblExceptionActive.Text = "Membership ID: " + gid.ToString(); txtIMID.Text = gid.ToString();
                        imtrans(txtIMID.Text.ToString(), lblEnrollFee.Text.ToString(), "Membership Fees", "Membership");
                        imtrans(txtIMID.Text.ToString(), lblSubFee.Text.ToString(), "Subscription Fees", "Subscription");
                        amountupdate(txtIMID.Text.ToString(), Convert.ToInt32(lblEnrollFee.Text));
                        amountupdate(txtIMID.Text.ToString(), Convert.ToInt32(lblSubFee.Text));
                        Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?name=" + Request.QueryString["name"] + "&lnk=" + Request.QueryString["lnk"] + "&typ=" + Request.QueryString["typ"] + "&lvl=" + Request.QueryString["lvl"] + "&id=" + txtIMID.Text.ToString());
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Enough Amount Found.')", true);
                    }
                }
            }
            else
            {
                cmd = new SqlCommand("update Member set Active='" + strstatus.ToString() + "' where ID='" + txtIMID.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                if (strstatus == "yes")
                {
                    btnchangeStatus.Text = "Disactive";
                    lblStatus2.Text = "Active";
                    lblExceptionActive.Text = "Membership  Activeted";
                }
                else
                {
                    btnchangeStatus.Text = "Active";
                    lblStatus2.Text = "Disactive";
                    lblExceptionActive.Text = "Membership  Disactiveted";
                }
            }
        }
        catch (Exception ex)
        {
            lblExceptionActive.Text = "Amount Not Found.";
        }
        finally
        {
            btnchangeStatus.Focus();
        }
    }
    public void imtrans(string imid, string amt, string feetype, string stype)
    {
        con.Close(); con.Open();
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
        cmd2.Parameters.AddWithValue("@SubDate", Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"), dtinfo));
        cmd2.Parameters.AddWithValue("@SubType", stype.ToString());
        cmd2.Parameters.AddWithValue("@AcountNo", "N/A");
        cmd2.Parameters.AddWithValue("@DD", "N/A");
        cmd2.Parameters.AddWithValue("@Bank", "N/A");
        cmd2.Parameters.AddWithValue("@YearFrom", ddlSessionFrom.SelectedValue.ToString() + "" + txtYearFrom.Text.ToString());
        cmd2.Parameters.AddWithValue("@YearTo", fmcls.nextSession(ddlSessionFrom.SelectedValue.ToString() + "" + txtYearFrom.Text.ToString()));
        cmd2.Parameters.AddWithValue("@TransType", ttype.ToString());
        cmd2.Parameters.AddWithValue("@Balance", dif.ToString());
        cmd2.Parameters.AddWithValue("@TransID", tid);
        cmd2.ExecuteNonQuery();
        lblBalanceType.Text = ttype.ToString();
        lblBalance.Text = dif.ToString();
        ClsAccount cl = new ClsAccount();
        cl.AmountSubmit(imid,"", DateTime.Now, "Debit", amt, lblSessionHiddend.Text.ToString(), feetype);
        con.Close();
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
            lblEnrollFee.Text = reader[2].ToString().TrimEnd('0');
            lblSubFee.Text = reader[3].ToString().TrimEnd('0');
            lblEnrollFee.Text = lblEnrollFee.Text.TrimEnd('.');
            lblSubFee.Text = lblSubFee.Text.TrimEnd('.');
            lblInfectionFee.Text = reader[4].ToString().TrimEnd('0');
            lblInfectionFee.Text = lblInfectionFee.Text.TrimEnd('.');
            lblEnrollFee.Text = (Convert.ToInt32(lblEnrollFee.Text) - Convert.ToInt32(lblInfectionFee.Text)).ToString();
        }
        reader.Close();
        con.Close();
    }
    public string genid()
    {
        SqlCommand cmdsn = new SqlCommand("select Max(ID) from Member where ID not like '%ICE%' and Type='" + lblMemberTyep.Text.ToString() + "'", con);
        con.Close();
        con.Open();
        string id = cmdsn.ExecuteScalar().ToString();
        if (id == "")
        {
            if (lblMemberTyep.Text == "Honorary")
                id = "10001";
            else if (lblMemberTyep.Text == "Member")
                id = "30001";
            else if (lblMemberTyep.Text == "Fellow")
                id = "20001";
            else if (lblMemberTyep.Text == "IM")
                id = "40001";
        }
        else
        {
            // char[] arr = new char[] { 'I', 'C', 'E', '0' }; // Trim these characters
            int ii = Convert.ToInt32(id) + 1;
            id = ii.ToString();
        }
        return id.ToString();
    }
    protected void btnSubscribe_Onclick(object sender, EventArgs e)
    {
        if (lblInspectionNo.Text == "0")
        {
            lblExceptionChngApprove.Text = "Please Select Inspection Record.";
        }
        else
        {
            con.Close(); con.Open();
            SqlCommand cmd = new SqlCommand("update IMInspection set Status=@Status where ID='" + txtIMID.Text.ToString() + "' and SN='" + lblInspectionNo.Text.ToString() + "'", con);
            cmd.Parameters.AddWithValue("@Status", ddlChangeStatus.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            lblExceptionChngApprove.Text = "Status Changed To: " + ddlChangeStatus.SelectedValue.ToString();
        }
    }
    protected void gridInspection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblInspectionNo.Text = GridBalance.SelectedRow.Cells[1].Text.ToString();
    }
    public void amountupdate(string imidi, int amount)
    {
        con.Close(); con.Open();
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
                if (x >= amount)
                {
                    x = x - amount;
                }
                else
                {
                    x = amount - x;
                    z = z + x;
                    x = 0;
                }
                con.Close();
                con.Open();
                string str1 = "update  IMAC set Total=@imactotal,Credit=@credit where IMID='" + imidi + "'";
                cmd = new SqlCommand(str1, con);
                cmd.Parameters.AddWithValue("@imactotal", x);
                cmd.Parameters.AddWithValue("@credit", z);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                w = w - amount;
                string str2 = "update IMAC set GTotal=@gtotal where GID='" + v + "'";
                cmd = new SqlCommand(str2, con);
                cmd.Parameters.AddWithValue("@gtotal", w);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            rd.Close(); rd.Dispose();
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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsstatecity.xmlCity(ddlCity, ddlState.SelectedItem.Text.ToString(), "XMLState.xml");
        ddlState.Focus();
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Focus();
    }
    protected void GridBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if(e.Row.RowType==DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible=false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[14].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[18].Text = e.Row.Cells[18].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[19].Text = e.Row.Cells[19].Text.ToString().TrimEnd('0').TrimEnd('.');
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).ToString("dd/MM/yyyy");
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        btnSave.Focus();
    }
    private bool chkAC(int fee, int amt,string type)
    {
        if (type == "Debit")
        {
            return false;
        }
        else
        {
            amt = amt - fee;
            if (amt < 0)
            {
                return false;
            }
            else return true;
        }
    }
    protected void GridDuplicacy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDuplicacy.PageIndex = e.NewPageIndex;
        GridDuplicacy.DataSource = fillgrid();
        GridDuplicacy.DataBind();
    }
    protected void GridDuplicacy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == null || e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;")
            {
            }
            else
            {
               e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).ToString("dd/MM/yyyy");
            }
        }
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
            "attachment;filename=Inspection.doc");
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
            "attachment;filename=Inspection.xls");
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
             "attachment;filename=Inspection.pdf");
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


