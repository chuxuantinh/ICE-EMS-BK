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
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Xml;

public partial class Admission_ApproveAdmission : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
    SqlCommand cmd;
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
                Image1.Visible = false;
                btnApprove.Enabled = false;
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                else ddlExamSeason.SelectedValue = "Win";
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                lblMembrshipNo.Text = selectid();
                pnlCourse.Visible = true; pnlIM.Visible = false; ddlPart.Visible = false;
                ddlExamSeason.Focus();
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
  protected override void OnPreRender(EventArgs e)
{
base.OnPreRender(e);

string strDisAbleBackButton;
strDisAbleBackButton = "<script language='javascript'>\n";
strDisAbleBackButton += "window.history.forward(1);\n";
strDisAbleBackButton += "\n</script>";

ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "clientScript", strDisAbleBackButton);

} 
    protected void Page_Unload(object sender, EventArgs e)
    {
        con.Dispose();
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
        catch (NullReferenceException Exception)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmissionDefault.aspx?name=" + Request.QueryString["name"] + "&lnk=null&typ=Ad");
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        ddlViewBy.Focus();
    }
    protected void txtIMID_TextChanged(object sender, EventArgs e)
    {
     lblExceptionOK.Text=  chkim(txtIMID.Text.ToString());
     con.Dispose();
    }
    private string chkim(string imid)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("select ID from IM where ID='" +imid.ToString() + "'", con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        int i = 0;
        if (chk == imid.ToString())
        {
            i += 1;
            return "";
        }
        else
        {
            return  "Please Insert Valid IM ID.";
        }
    }
    protected void btnView_Onclick(object sender, EventArgs e)
    {
        string qry = "", qry2 = "";
        if (ddlViewBy.SelectedValue.ToString() == "Course")
        {
            if(ddlCourse.SelectedValue.ToString() == "All")
            {
                if(ddlStatus.SelectedValue.ToString()=="Active")
                    qry = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and Status='" + ddlStatus.SelectedValue.ToString() + "' and IMID='" + txtIMID.Text + "' and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
                else
                    qry = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Status='" + ddlStatus.SelectedValue.ToString() + "' and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
                qry2 = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Status='ToBeApprove' and Session='" + lblSeasonHidden.Text + "'  order by CONVERT(int, AppId)";
            }
            else
            {
                if(ddlStatus.SelectedValue.ToString()=="Active")
                    qry = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and Status='" + ddlStatus.SelectedValue.ToString() + "' and IMID='" + txtIMID.Text + "' and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
               else
                    qry = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and Status='" + ddlStatus.SelectedValue.ToString() + "' and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
                qry2 = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and Status='ToBeApprove'  and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
            }
        }
        else if (ddlViewBy.SelectedValue.ToString() == "IMID")
        {
            qry = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Course,Part,Session,Status from Student where IMID='" + txtIMID.Text.ToString() + "' and Status='" + ddlStatus.SelectedValue.ToString() + "'  and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
            qry2 = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Course,Part,Session,Status from Student where IMID='" + txtIMID.Text.ToString() + "' and Status='ToBeApprove'  and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
        }
        SqlDataAdapter ad = new SqlDataAdapter();
        ad = new SqlDataAdapter(qry, con);
        DataTable at = new DataTable();
        ad.Fill(at);
        GridToBeApprove.DataSource = at;
        GridToBeApprove.DataBind();
        ad = new SqlDataAdapter(qry2, con);
        at.Clear();
        ad.Fill(at);
        GridAppTable.DataSource = at;
        GridAppTable.DataBind();
        if (GridAppTable.Rows.Count > 0)
        {
            btnApprove.Enabled = true;
        }
        else btnApprove.Enabled = false;
        btnViewa.Focus();
    }
    private DataSet GetDataSource()
    {
        string qry2 = "";
        if (ddlViewBy.SelectedValue.ToString() == "Course")
        {
            if (ddlCourse.SelectedValue.ToString()=="All")
            {
                qry2 = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Status='ToBeApprove' and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
            }
            else
            {
                qry2 = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Session,Status from Student where Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and Status='ToBeApprove'  and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
            }
        }
        else if (ddlViewBy.SelectedValue.ToString() == "IMID")
        {
            qry2 = "select SN,IMID,SID,Name,FName,Course,Part,DOB,AppId,Course,Part,Session,Status from Student where IMID='" + txtIMID.Text.ToString() + "' and Status='ToBeApprove'  and Session='" + lblSeasonHidden.Text + "' order by CONVERT(int, AppId)";
        }
        SqlDataAdapter ad = new SqlDataAdapter();
        DataSet at = new DataSet();
        ad = new SqlDataAdapter(qry2, con);
        at.Clear();
        ad.Fill(at);
        return at;
    }
    protected void btnToBeApprove_OnClick(object sender, EventArgs e)
    {
        btnApprove.Enabled = true;
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        for (int i = 0; i < GridToBeApprove.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridToBeApprove.Rows[i].FindControl("chkapp");
            if(chk.Checked)
            {
            cmd = new SqlCommand("update Student set Status=@Status where SN='" + Convert.ToInt32(GridToBeApprove.Rows[i].Cells[2].Text.ToString()) + "'", con);
            cmd.Parameters.AddWithValue("@Status", "ToBeApprove");
            cmd.ExecuteNonQuery();
            }
        }
        GridAppTable.DataSource = GetDataSource();
        GridAppTable.DataBind();
        con.Close();
        con.Dispose();
        btnToBeApprove.Focus();
    }
    protected void ddlCourse_selectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCourse.SelectedValue.ToString() == "All") ddlPart.Visible = false; else ddlPart.Visible = true;
        ddlCourse.Focus();
    }
    protected void GridToBeApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text == "" | e.Row.Cells[9].Text == "&nbsp;")
            {

            }
            else
            {
                e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToString("dd/MM/yyyy");
            }
        }
        if (ddlStatus.SelectedValue == "Active")
        {
            GridToBeApprove.Columns[1].Visible = false;
            pnlApp.Visible = false;
        }
        else
        {
            pnlApp.Visible = true; GridToBeApprove.Columns[1].Visible = true;
        }
    }
    protected void GridAppTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dtinfo.ShortDatePattern = "dd/MM/yyyy";
        dtinfo.DateSeparator = "/";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            if (((CheckBox)e.Row.FindControl("chkapp")).Checked == true)
            {
               
            }
            if (e.Row.Cells[9].Text == "" | e.Row.Cells[9].Text == "&nbsp;")
            {
            }
            else
            {
               // e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToString("dd/MM/yyyy");
            }
        } if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
    protected void ddlViewBy_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        txtIMID.Text = "";
        if (ddlViewBy.SelectedValue.ToString() == "Course")
        {
            pnlCourse.Visible = true; pnlIM.Visible = false;
        }
        else if (ddlViewBy.SelectedValue.ToString() == "IMID")
        {
            pnlIM.Visible = true; pnlCourse.Visible = false;
        }
        ddlStatus.Focus();
    }
    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue.ToString() == "NotApprove")
        {
            if (ddlViewBy.SelectedValue.ToString() == "Course")
            {
                pnlCourse.Visible = true; pnlIM.Visible = false;
            }
            btnApprove.Visible = true; btnHold.Visible = true; btnToBeApprove.Visible = true;
        }
        else if (ddlStatus.SelectedValue.ToString() == "Active")
        {
            btnHold.Visible = false; btnApprove.Visible = false; btnToBeApprove.Visible = false; pnlIM.Visible = true;
        }
        else if (ddlStatus.SelectedValue.ToString() == "Pending")
        {
            if (ddlViewBy.SelectedValue.ToString() == "Course")
            {
                pnlCourse.Visible = true; pnlIM.Visible = false;
            }
            btnApprove.Visible = false; btnHold.Visible = false; btnToBeApprove.Visible = true;
        }
        GridToBeApprove.DataSource = null;
        GridToBeApprove.DataBind();
        ddlStatus.Focus();
    }
    protected void btnApprove_Onclick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        //SqlTransaction sTR;
        //sTR = con.BeginTransaction();
        //cmd.Connection = con;
        //cmd.Transaction = sTR;
        lblMembrshipNo.Text = selectid();
        try
        {
            Image1.Visible = true;
            for (int i = 0; i <= GridAppTable.Rows.Count - 1; i++)
            {
                string sid = genid();
                cmd = new SqlCommand("update Student set SID='" + sid.ToString() + "',EnrollDate='" + DateTime.Now + "',Status='Active' where SN='" + Convert.ToInt32(GridAppTable.Rows[i].Cells[2].Text.ToString()) + "'", con);
                cmd.ExecuteNonQuery();
                cmd =new SqlCommand("update Docs set SID='"+sid+"' where SID='" + Convert.ToInt32(GridAppTable.Rows[i].Cells[2].Text.ToString()) + "'",con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update AppRecord set Enrolment='" + sid.ToString() + "' where Enrolment='" + GridAppTable.Rows[i].Cells[4].Text.ToString() + "' and Session='" + lblSeasonHidden.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                chkExamForm(GridAppTable.Rows[i].Cells[4].Text.ToString(), sid.ToString());
                GridAppTable.Rows[i].Cells[4].Text = sid.ToString();
                GridAppTable.Rows[i].Cells[12].Text = "Approved";
                if (GridAppTable.Rows[i].Cells[8].Text == "PartII")
                {
                    string sub1 = "", sub2 = "";
                    if (GridAppTable.Rows[i].Cells[7].Text.ToString() == "Civil")
                    { sub1 = "TC 2.10"; sub2 = "TC 2.11"; }
                    else { sub1 = "TA 2.11"; sub2 = "TA 2.12"; }
                    partIIStudent(sid, GridAppTable.Rows[i].Cells[7].Text.ToString(), GridAppTable.Rows[i].Cells[8].Text.ToString(),sub1);
                    partIIStudent(sid, GridAppTable.Rows[i].Cells[7].Text.ToString(), GridAppTable.Rows[i].Cells[8].Text.ToString(), sub2);
                }
            }
            //sTR.Commit();
            lblMessage.Text = "Membership Generated Successfully.!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Membership Generated Successfully.!')", true);
            lblMembrshipNo.Text = selectid();
            btnApprove.Enabled = false;
            Image1.Visible = false;
        }
        catch (Exception ex)
        {
           // sTR.Rollback();
            rollbackID();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }

    private void partIIStudent(string sid, string course, string part, string subid)
    {
        cmd = new SqlCommand("insert into PartIIStudent(SID,Course,Part,SubID) Values(@SID,@Course,@Part,@SubID)", con);
        cmd.Parameters.AddWithValue("@SID", sid.ToString());
        cmd.Parameters.AddWithValue("@Course", course);
        cmd.Parameters.AddWithValue("@Part", part);
        cmd.Parameters.AddWithValue("@SubID", subid.ToString());
        cmd.ExecuteNonQuery();
    }
    protected void btnHold_Onclick(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
        for (int i = 0; i < GridToBeApprove.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridToBeApprove.Rows[i].FindControl("chkapp");
            if(chk.Checked)
            {
            cmd = new SqlCommand("update Student set Status=@Status where SN='" + Convert.ToInt32(GridToBeApprove.Rows[i].Cells[2].Text.ToString()) + "'", con);
            cmd.Parameters.AddWithValue("@Status","Pending");
            cmd.ExecuteNonQuery();
            }
        }
        con.Close();
        con.Dispose();
        btnHold.Focus();
    }
    private string genid()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/SID.xml"));
        XmlNodeList lstVideos = xmlDoc.GetElementsByTagName("ID");
        XmlNode nd = lstVideos.Item(0);
        int it = Convert.ToInt32(nd.InnerText); it = it + 1;
        nd.InnerText = (it).ToString();
        xmlDoc.Save(MapPath("~/Xml/SID.xml"));
        return it.ToString();
    }
    private string selectid()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/SID.xml"));
        XmlNodeList lstVideos = xmlDoc.GetElementsByTagName("ID");
        XmlNode nd = lstVideos.Item(0);
        int it = Convert.ToInt32(nd.InnerText);
        return it.ToString();
    }
    private void rollbackID()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/SID.xml"));
        XmlNodeList lstVideos = xmlDoc.GetElementsByTagName("ID");
        XmlNode nd = lstVideos.Item(0);
        nd.InnerText = lblMembrshipNo.Text.ToString();
        xmlDoc.Save(MapPath("~/Xml/SID.xml"));
    }
    private void chkExamForm(string sid, string nid)
    {
      SqlCommand cmd =new SqlCommand("select SID from ExamForms where SID='" + sid.ToString() + "' and ExamSeason='"+lblSeasonHidden.Text.ToString()+"'",con);
        string chk = Convert.ToString(cmd.ExecuteScalar());
        if (chk == sid)
        {
            cmd = new SqlCommand("update ExamForm set SID='" + nid + "' where SID='" + sid.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            cmd=new SqlCommand("update ExamForms set SID='" + nid + "' where SID='" + sid.ToString() + "'",con);
            cmd.ExecuteNonQuery();
        }
        cmd = new SqlCommand("update ITIForm set SID='" + nid + "' where SID='" + sid.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        cmd =new SqlCommand("update ExamCurrent set SId='" + nid + "' where SId='" + sid.ToString() + "'",con);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("update CompositeFees set SID='" + nid + "' where SID='" + sid.ToString() + "'", con);
        cmd.ExecuteNonQuery();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        GridAppTable.AllowPaging = false;
        GridAppTable.DataSource = grid();
        GridAppTable.DataBind();
        if (GridAppTable.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ApprovedMembership.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridAppTable.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridAppTable.AllowPaging = false;
        GridAppTable.DataSource = grid();
        GridAppTable.DataBind();
        if (GridAppTable.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ApprovedMembership.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridAppTable.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridAppTable.AllowPaging = false;
        GridAppTable.DataSource = grid();
        GridAppTable.DataBind();
        if (GridAppTable.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=ApprovedMembership.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridAppTable.RenderControl(hw);
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
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SN");
        dtDatas.Columns.Add("IMID");
        dtDatas.Columns.Add("SID");
         dtDatas.Columns.Add("Name");
        dtDatas.Columns.Add("FName");
        dtDatas.Columns.Add("Course");
         dtDatas.Columns.Add("Part");
        dtDatas.Columns.Add("DOB");
        dtDatas.Columns.Add("AppId");
        dtDatas.Columns.Add("Session");
        dtDatas.Columns.Add("Status");
        ViewState["dtDatas"] = dtDatas;
    }
    private DataTable grid()
    {
        datastructure();
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
            if (GridAppTable.Rows.Count > 0)
            {
                int i = 0;
                while (i < GridAppTable.Rows.Count)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["SN"] = GridAppTable.Rows[i].Cells[2].Text;
                    drNewRow["IMID"] = GridAppTable.Rows[i].Cells[3].Text;
                    drNewRow["SID"] = GridAppTable.Rows[i].Cells[4].Text;
                    drNewRow["Name"] = GridAppTable.Rows[i].Cells[5].Text;
                    drNewRow["FName"] = GridAppTable.Rows[i].Cells[6].Text;
                    drNewRow["Course"] = GridAppTable.Rows[i].Cells[7].Text;
                    drNewRow["Part"] = GridAppTable.Rows[i].Cells[8].Text;
                    drNewRow["DOB"] = GridAppTable.Rows[i].Cells[9].Text;
                    drNewRow["AppId"] = GridAppTable.Rows[i].Cells[10].Text;
                    drNewRow["Session"] = GridAppTable.Rows[i].Cells[11].Text;
                    drNewRow["Status"] = GridAppTable.Rows[i].Cells[12].Text;
                    dtDatas.Rows.Add(drNewRow);
                    i++;
                }
            }
        return dtDatas;
        }
    protected void btnSendback_Click(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("update Student set Status='NotApprove' where Status= 'ToBeApprove'", con);          
                cmd.ExecuteNonQuery();                  
        con.Close();
        con.Dispose();
    }
}
