using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Xml;

public partial class Exam_Exam2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"].ToString());
    SqlCommand cmd; SqlDataAdapter adp;
    DateTimeFormatInfo dtinfo = new DateTimeFormatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                    ddlExamSeason.SelectedValue = "Sum";
                else { ddlExamSeason.SelectedValue = "Win"; }
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                 ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
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
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
        }
        catch (NullReferenceException ex) { Response.Redirect("../Login.aspx"); }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void txtCenter_TextChanged(object sender, EventArgs e)
    {
        lblException.Text = ""; Label1.Text = ""; con.Close(); con.Open();
        lblException.Text = "";
        cmd = new SqlCommand("select Name from ExamCenter where ID='" + txtCenter.Text.ToString() + "' and Season='" + lblExamSeasonHidden.Text.ToString() + "'", con);
        lblCenterName.Text = Convert.ToString(cmd.ExecuteScalar());
        if (lblCenterName.Text == "")
        {
            lblException.Text = "Center code not Found, it may be wrong please try another one!"; txtCenter.Text = ""; txtCenter.Focus();
        }
        con.Close(); con.Dispose();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        txtCenter.Text = ""; lblCenterName.Text = ""; lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtCenter.Focus();
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCenter.Text = ""; lblCenterName.Text = ""; lblExamSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtCenter.Focus();
    }
    string str1; string str2; string str3;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        try
        {
            if (txtCenter.Text == "" | txtStart.Text == "" | txtEnd.Text == "") { Label1.Text = "You can not left text blank!"; }
            else
            {
                Label1.Text = ""; con.Open();

                // cmd = new SqlCommand("select StartRange, EndRange, SubID from BookletRange where SubID='" + lblPaperCode.Text.ToString() + "' and Session='" + lblExamSeasonHidden.Text.ToString() + "'", con);
                //  SqlDataReader dr; dr = cmd.ExecuteReader();
                bool flag = false;
                if (gridRange.Rows.Count > 0)
                {
                    for (int i = 0; i <= gridRange.Rows.Count - 1; i++)
                    {
                        GridViewRow rw = gridRange.Rows[i];
                        if ( Convert.ToInt32(txtStart.Text)>=Convert.ToInt32(rw.Cells[0].Text)  && Convert.ToInt32(txtEnd.Text)<=Convert.ToInt32(rw.Cells[1].Text) )
                        {
                            if (Convert.ToInt32(rw.Cells[2].Text) >= Convert.ToInt32(txtBooklet.Text))
                            {
                                lblSt1.Text = rw.Cells[0].Text; lblSt2.Text = rw.Cells[1].Text; lblRef.Text = rw.Cells[3].Text;
                                flag = true; break;
                            }
                            else Label1.Text = "Booklet Out Of Range !";
                        }
                        else { Label1.Text = "Not Valid !"; }
                    }
                    for (int j = 0; j <  GridPRange.Rows.Count; j++)
                    {
                        Label lbl1 = (Label)GridPRange.Rows[j].FindControl("lblStartRange");
                        Label lbl2 = (Label)GridPRange.Rows[j].FindControl("lblEndRange");
                        GridViewRow rw1 = GridPRange.Rows[j];
                        if (Convert.ToInt32(lbl1.Text) >= Convert.ToInt32(txtStart.Text) && Convert.ToInt32(lbl2.Text) <= Convert.ToInt32(txtEnd.Text))
                        {
                            flag = false; break;
                        }
                    }
                }
                else
                {
                    Label1.Text = "Booklet Series Not Provided.";
                }
                if (flag == true && GridBookletPRange.SelectedRow.Cells.Count>0)
                {
                    if((Convert.ToInt32(GridBookletPRange.SelectedRow.Cells[3].Text))>(Convert.ToInt32(GridBookletPRange.SelectedRow.Cells[4].Text)+Convert.ToInt32(txtBooklet.Text)))
                    {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Provide Required Booklet')", true);
                    }
                    else 
                    {
                    con.Close(); con.Open();
                    cmd = new SqlCommand("insert into PaperRange(SN,StartRange,EndRange,Qty,Ref) values(@SN,@StartRange,@EndRange,@Qty,@Ref)", con);
                    cmd.Parameters.AddWithValue("SN", GridBookletPRange.SelectedRow.Cells[1].Text.ToString());
                    cmd.Parameters.AddWithValue("StartRange", txtStart.Text);
                    cmd.Parameters.AddWithValue("EndRange", txtEnd.Text);
                    cmd.Parameters.AddWithValue("@Qty", txtBooklet.Text);
                    cmd.Parameters.AddWithValue("Ref", lblRef.Text);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("update BookletPRange Set Supply=Supply+'" + txtBooklet.Text.ToString() + "' where Session='"+lblExamSeasonHidden.Text.ToString()+"' and Centercode='"+txtCenter.Text.ToString()+"' and Type='"+ddlType.SelectedValue.ToString()+"' and SubID='"+lblPaperCode.Text.ToString()+"'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("update BookletRange set qty=qty-'" + txtBooklet.Text + "' where StartRange='" + lblSt1.Text + "' and EndRange='" + lblSt2.Text + "' and SubID='" + GridBookletPRange.SelectedRow.Cells[2].Text.ToString() + "' and Session='" + lblExamSeasonHidden.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    bindRangGrid();
                    BindGrid();
                    bindPapercode();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Submitted Successfully !')", true);
                    }
                }
            }
        }
        catch (SqlException ex) { Label1.Text = ex.ToString(); }
        finally { con.Close(); con.Dispose(); }
    }
    protected void btnDelte_click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = con.CreateCommand();
        SqlTransaction Updtrans;
        Updtrans = con.BeginTransaction("UpdateTrans");
        cmd.Connection = con;
        cmd.Transaction = Updtrans;
        try
        {
            //cmd.CommandText = "update BookletPRange set SubID=@SubID,Session=@Session,CenterCode=@CenterCode,Type=@Type where SN='" + GridPRange.SelectedDataKey.Value.ToString() + "'";
            //cmd.Parameters.AddWithValue("SubID", lblPaperCode.Text.ToString());
            //cmd.Parameters.AddWithValue("Session", lblExamSeasonHidden.Text.ToString());
            //cmd.Parameters.AddWithValue("CenterCode", txtCenter.Text.ToString());
            //cmd.Parameters.AddWithValue("Type", ddlType.SelectedValue.ToString());
            //cmd.ExecuteNonQuery();
            cmd.CommandText = "delete PaperRange where No='" + lblNo.Text + "'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "update BookletPRange set Supply=Supply-'" + Convert.ToInt32(txtBooklet.Text) + "' where CenterCode='" + txtCenter.Text + "' and Session='" + lblExamSeasonHidden.Text + "' and Type='" + ddlType.SelectedValue.ToString() + "' and SubID='" + lblPaperCode.Text + "'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "update BookletRange set Qty=Qty+'" + Convert.ToInt32(lblPQty.Text) + "' where SN='" + Convert.ToInt32(lblSN.Text) + "'";
            cmd.ExecuteNonQuery();
            Updtrans.Commit();
            bindRangGrid();
            bindPapercode();
            BindGrid(); lblPQty.Text = ""; lblSN.Text = ""; txtBooklet.Text = ""; txtStart.Text = ""; txtEnd.Text = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert(' Successfully Deleted !')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Updated !')", true);
            try { Updtrans.Rollback(); }
            catch (Exception ex1) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Not Updated !')", true); }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      //  string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect(url.ToString());
        txtCenter.Enabled = true;
        ddlType.Enabled = true;
        btnDelete.Visible = false;
        btnAdd.Visible = true;
    }
    protected void GridBookletPRange_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblPaperCode.Text = GridBookletPRange.SelectedRow.Cells[2].Text.ToString();
        lblReqQty.Text = GridBookletPRange.SelectedRow.Cells[3].Text.ToString();
        lblSupplyQty.Text = GridBookletPRange.SelectedRow.Cells[4].Text.ToString();
        txtBooklet.Text = GridBookletPRange.SelectedRow.Cells[5].Text.ToString().TrimStart('-');
        
        txtStart.Text = "";
        btnAdd.Visible = true;
        btnDelete.Visible = false;
        txtEnd.Text = "";
        bindRangGrid();
        BindGrid();
        txtStart.Focus();
    }
    protected void GridPRange_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        btnAdd.Visible = false; btnCancel.Visible = true;
        btnDelete.Visible = true;
        Label lb = new Label();
        lb =(Label) GridPRange.SelectedRow.FindControl("lblCenterCode");
        txtCenter.Text = lb.Text;

        lb = (Label)GridPRange.SelectedRow.FindControl("lblSubID");
        lblPaperCode.Text = lb.Text;


        lb = (Label)GridPRange.SelectedRow.FindControl("lblStartRange");
        txtStart.Text= lb.Text;

        lb = (Label)GridPRange.SelectedRow.FindControl("lblEndRange");
        txtEnd.Text = lb.Text;

        lb = (Label)GridPRange.SelectedRow.FindControl("lblQty");
        txtBooklet.Text = lb.Text; lblPQty.Text = lb.Text;

        lb = (Label)GridPRange.SelectedRow.FindControl("lblSN");
        lblNo.Text = lb.Text;
       
        lb = (Label)GridPRange.SelectedRow.FindControl("lblPRef");
        lblSN.Text = lb.Text;
        txtCenter.Enabled = false;
        ddlType.Enabled = false;
        //con.Close(); con.Open();
        //cmd = new SqlCommand("select BookletPRange.SN,BookletPRange.SubID,BookletPRange.CenterCode,PaperRange.StartRange,PaperRange.EndRange,BookletPRange.Session,BookletPRange.Type from BookletPRange PaperRange where BookletPRange.SN='" + GridPRange.SelectedDataKey.Value.ToString() + "'", con);
        //SqlDataReader rd = cmd.ExecuteReader();
        //if (rd.Read())
        //{
        //    txtCenter.Text = rd["CenterCode"].ToString();
        //    lblPaperCode.Text = rd["SubID"].ToString();
        //    txtStart.Text = rd["StartRange"].ToString();
        //    txtEnd.Text = rd["EndRange"].ToString();
        //}
        //rd.Close(); rd.Dispose(); con.Close(); con.Dispose(); txtCenter.Focus();
    }
    private void BindGrid()
    {
        adp = new SqlDataAdapter("select PaperRange.No,BookletPRange.SubID,BookletPRange.CenterCode,PaperRange.StartRange,PaperRange.EndRange,PaperRange.Qty,PaperRange.Ref from BookletPRange inner join PaperRange on BookletPRange.SN=PaperRange.SN  where BookletPRange.Session='" + lblExamSeasonHidden.Text.ToString() + "' and Type='"+ddlType.SelectedValue.ToString()+"' and SubID = '"+lblPaperCode.Text.ToString()+"' and CenterCode='"+txtCenter.Text.ToString()+"' ", con);
        DataTable dt = new DataTable(); adp.Fill(dt); GridPRange.DataSource = dt; GridPRange.DataBind();
    }
    private void bindRangGrid()
    {
        adp = new SqlDataAdapter("select StartRange,EndRange,Qty,SN from BookletRange where Type='" + ddlType.SelectedValue.ToString() + "'  and Session='" + ddlExamSeason.SelectedValue.ToString() + txtYearSeason.Text.ToString() + "'", con);
        DataTable dt = new DataTable(); adp.Fill(dt);
        gridRange.DataSource = dt;
        gridRange.DataBind();
    }
    protected void gridRangeRowBound_Click(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
    private void bindPapercode()
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        con.Close(); con.Open();
        cmd = new SqlCommand("select CenterCode from BookletPRange where Session='" + lblExamSeasonHidden.Text.ToString() + "' and CenterCode='" + txtCenter.Text.ToString() + "'", con);
        string strCenterCode = Convert.ToString(cmd.ExecuteScalar());
        if (strCenterCode == "")
        {
            lblException.Text = "Please enter Center Code !"; 
        }
        else
        {
            adp = new SqlDataAdapter("select SN,SubID,Required,Supply,(Supply-Required) as Balance from BookletPRange where Session='" + lblExamSeasonHidden.Text.ToString() + "' and Type='" + ddlType.SelectedValue.ToString() + "' and Centercode='" + txtCenter.Text + "' and EDate='" + Convert.ToDateTime(ddlExaminationdate.SelectedValue) + "' and Shift='" + ddlShift.SelectedValue.ToString() + "'", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            GridBookletPRange.DataSource = dt;
            GridBookletPRange.DataBind();
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        updatePaperCode();
        bindPapercode();
        btnDelete.Visible = false;
        btnAdd.Visible = true;
        con.Close(); con.Dispose();
    }
    protected void txtEndRange_TextChanged(object sender, EventArgs e)
    {
        if (txtStart.Text == "") txtStart.Text = "0";
        if (txtEnd.Text == "") txtEnd.Text = "0";
        txtBooklet.Text = (Convert.ToInt32(txtEnd.Text) - Convert.ToInt32(txtStart.Text) + 1).ToString(); txtBooklet.Focus();
    }
    protected void txtStartRange_TextChanged(object sender, EventArgs e)
    {
        if (txtStart.Text == "") txtStart.Text = "0";
        if (txtEnd.Text == "") txtEnd.Text = "0";
        txtBooklet.Text = (Convert.ToInt32(txtEnd.Text) - Convert.ToInt32(txtStart.Text) + 1).ToString(); txtEnd.Focus();
    }
    private void updatePaperCode()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SubID");
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("~/XML/PaperCode.xml"));
        XmlElement el = doc.DocumentElement;
        XmlNodeList nlist = el.ChildNodes;
        foreach (XmlNode nd in nlist)
        {
            dt.Rows.Add(nd.InnerText.ToString());
        }
        cmd = new SqlCommand("spBookletUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ssn", lblExamSeasonHidden.Text.ToString());
        SqlParameter sp = cmd.Parameters.AddWithValue("@Papercodetbl", dt);
        cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@CenterCode", txtCenter.Text);
        cmd.ExecuteNonQuery();
    }
    protected void btnUpdate_clic(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        cmd = new SqlCommand("spBookletUpdateRequiredQty", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ssn", lblExamSeasonHidden.Text.ToString());
        cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@CenterCode", txtCenter.Text);
        cmd.ExecuteNonQuery();
        con.Close(); con.Dispose();
    }
}