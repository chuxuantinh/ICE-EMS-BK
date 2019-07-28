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
using System.Data.OleDb;
using System.Data.Linq;
public partial class Exam_GenerateRollNo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    DataContext conLinq = new DataContext(System.Configuration.ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    ClsECenterCity ecity = new ClsECenterCity();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ecity.getItems(ddlExamCity);
                    datastructure();
                    maikal dev = new maikal();
                    int se = dev.chksession();
                    if (se == 0) ddlExamSeason.SelectedValue = "Sum";
                    else ddlExamSeason.SelectedValue = "Win";
                    txtYearSeason.Text = DateTime.Now.Year.ToString();
                    lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                    lblToPart1.Text = "0";
                    lbltoPartII.Text = "0"; lblToSectionA.Text = "0"; lblToSectinB.Text = "0";
                    lblToPPPartI.Text = "0"; lblToPPartII.Text = "0"; lblToSSectionA.Text = "0"; lblToSSectinB.Text = "0";
                    ddlExamCity.Focus();
                    //readexcel();
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
    protected void lbtnNext1Redirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamDefault.aspx?dev=" + Request.QueryString["dev"] + "&lnk=null&typ=Ex&id=");
    }
    protected void ddlExamSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtYearSeason.Focus();
    }
    protected void txtYearSeason_TextChanged(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        txtExamCode.Focus();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridView1.SelectedRow;
        lblCenteNaem.Text = gr.Cells[2].Text.ToString();
        lblCenterCode.Text = gr.Cells[1].Text.ToString();
        txtExamCode.Text = gr.Cells[1].Text.ToString();
        con.Close();
        con.Open(); 
        SqlCommand cmd = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmd.ExecuteScalar());
        if (sum == "")
            lblCapacity.Text = "0";
       else 
            lblCapacity.Text = sum.ToString();
        centerinfo(lblCenterCode.Text.ToString(), lblSeasonHidden.Text.ToString());
        //capacity(lblCenterCode.Text, lblSeasonHidden.Text);
        con.Close(); con.Dispose();
        txtExamCode.Focus();
    }
    decimal total=0, p1=0, p2=0, s1=0, s2=0, pp1=0, pp2=0, ss1=0, ss2=0;
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Course = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Course"));
            string Part = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Part"));
            if (Course == "Civil")
            {
                total = total + 1;
                if (Part == "PartI") p1 = p1 + 1;
                if (Part == "PartII") p2 = p2 + 1;
                if (Part == "SectionA") s1 = s1 + 1;
                if (Part == "SectionB") s2 = s2 + 1;
            }
            if (Course == "Architecture")
            {
                total = total + 1;
                if (Part == "PartI") pp1 = pp1 + 1;
                if (Part == "PartII") pp2 = pp2 + 1;
                if (Part == "SectionA") ss1 = ss1 + 1;
                if (Part == "SectionB") ss2 = ss2 + 1;
            }
            lblToExamForms.Text = total.ToString();
            lblToPart1.Text = p1.ToString();
            lbltoPartII.Text = p2.ToString();
            lblToSectionA.Text = s1.ToString();
            lblToSectinB.Text = s2.ToString();
            lblToPPPartI.Text = pp1.ToString();
            lblToPPartII.Text = pp2.ToString();
            lblToSSectionA.Text = ss1.ToString();
            lblToSSectinB.Text = ss2.ToString();
        }
        ddlCourse.Focus();
    }
    decimal cp = 0, tofo = 0, pI = 0, pII = 0, SI = 0, SII = 0, ppI = 0, ppII = 0, SSI = 0, SSII = 0, select = 0, toselected;
    bool bl;
    protected void btnSelectForRollNo_Click(object sender, EventArgs e)
    {
        
       
    }

   
    private void datastructure()
    {
        DataTable dtDatas = new DataTable();
        dtDatas.Columns.Add("SID");
        dtDatas.Columns.Add("Status");
        dtDatas.Columns.Add("ExamSeason");
        dtDatas.Columns.Add("IMID");
        dtDatas.Columns.Add("Course");
        dtDatas.Columns.Add("Part");
        dtDatas.Columns.Add("CenterCode");
        dtDatas.Columns.Add("CenterName");
        dtDatas.Columns.Add("RollNo");
        dtDatas.Columns.Add("City");
        dtDatas.Columns.Add("City2");
        ViewState["dtDatas"] = dtDatas;
    } 
    protected void btnGenerateRollNo_Click(object sender, EventArgs e)
    {
        int a = Convert.ToInt32(StartRol.Text.ToString());
        int count = a;
        con.Close(); con.Open();
        for (int i = 0; i <= GridAppTable.Rows.Count - 1; i++)
        {
            SqlCommand cmd = new SqlCommand("update ExamForms set Status=@Status,RollNo=@RollNo where SID='" + GridAppTable.Rows[i].Cells[0].Text.ToString() + "' and Status='" + GridAppTable.Rows[i].Cells[1].Text.ToString() + "' and ExamSeason='" + GridAppTable.Rows[i].Cells[2].Text.ToString() + "' and IMID='" + GridAppTable.Rows[i].Cells[3].Text.ToString() + "'  and Course='" + GridAppTable.Rows[i].Cells[4].Text.ToString() + "' and Part='" + GridAppTable.Rows[i].Cells[5].Text.ToString() + "'and RollNo='N/A' and status='Submitted'", con);
            cmd.Parameters.AddWithValue("@RollNo",count);
            cmd.Parameters.AddWithValue("@Status", "RollNoGenerated");
            cmd.ExecuteNonQuery();
            count++;
        }
        DataTable dtDatas = (DataTable)ViewState["dtDatas"];
        dtDatas.Clear();
        GridAppTable.DataSource = dtDatas;
        GridAppTable.DataBind();
        GridView3.DataSource = GetDataSource();
        GridView3.DataBind();
        GridView2.DataBind();
        con.Close();
        con.Dispose();
        ddlPart.Focus();
    }
    string cmqry;
    private DataSet GetDataSource()
    {
        cmqry = "select SID,ExamSeason,IMID,CenterCode,Course,Part,RollNo,City from ExamForms where City='" + ddlExamCity.Text.ToString() + "' and Status='RollNoGenerated' and ExamSeason='" + lblSeasonHidden.Text.ToString() + "' order by rollno";
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        GridView3.DataSource = GetDataSource();
        GridView3.DataBind();
        GridView3.Focus();
    }
    private void centerinfo(string code, string session)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        SqlCommand cmd = new SqlCommand("select * from  ExamCenter where city='" + ddlExamCity.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            lblCenterCode.Text = reader["ID"].ToString();
            txtExamCode.Text = lblCenterCode.Text.ToString();
            lblCenteNaem.Text = reader["Name"].ToString();
            lblCenterAddress.Text = reader["Address"].ToString();
            lblCenterAddress2.Text = reader["Address2"].ToString();
            lblCenterCity.Text = reader["City"].ToString();
            lblCenterState.Text = reader["State"].ToString();
            lblPinCode.Text = reader["Pin"].ToString();
            lblExceptionCode.Text = "";
        }
        else
        {
            lblExceptionCode.Text = "Invalid Exam Center Code";
        }
        reader.Close();
        reader.Dispose();
        SqlCommand cmdw = new SqlCommand("select Sum(Capacity) from Rooms where ID='" + lblCenterCode.Text.ToString() + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
        string sum = Convert.ToString(cmdw.ExecuteScalar());
        if (sum == "")
        {
            lblCapacity.Text = "0";
        }
        {
            lblCapacity.Text = sum.ToString();
        }
    }
    protected void ddlExamCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Close(); con.Open();
        centerinfo(txtExamCode.Text.ToString(), lblSeasonHidden.Text.ToString());
        con.Close(); con.Dispose();
        ddlExamCity.Focus();    
    }
     public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        GridView3.AllowPaging = false;
        GridView3.DataSource = GetDataSource();
        GridView3.DataBind();
        if (GridView3.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ApprovedAdmitCardApplication.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView3.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }
    protected void ibtnExportExcelAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridView3.AllowPaging = false;
        GridView3.DataSource = GetDataSource();
        GridView3.DataBind();
        if (GridView3.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=ApprovedAdmitCardApplication.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView3.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ibtnExportPDFAppTableDoc_Click(object sender, ImageClickEventArgs e)
    {
        GridView3.AllowPaging = false;
        GridView3.DataSource = GetDataSource();
        GridView3.DataBind();
        if (GridView3.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=ApprovedAdmitCardApplication.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView3.RenderControl(hw);
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
    protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            if (ddlPart.SelectedValue.ToString() == "PartI")
                txtNOOfForms.Text = lblToPart1.Text;
            else if (ddlPart.SelectedValue.ToString() == "PartII")
                txtNOOfForms.Text = lbltoPartII.Text;
            else if (ddlPart.SelectedValue.ToString() == "SectionA")
                txtNOOfForms.Text = lblToSectionA.Text;
            else if (ddlPart.SelectedValue.ToString() == "SectionB")
                txtNOOfForms.Text = lblToSectinB.Text;
        }
        else
        {
            if (ddlPart.SelectedValue.ToString() == "PartI")
                txtNOOfForms.Text = lblToPPPartI.Text;
            else if (ddlPart.SelectedValue.ToString() == "PartII")
                txtNOOfForms.Text = lblToPPartII.Text;
            else if (ddlPart.SelectedValue.ToString() == "SectionA")
                txtNOOfForms.Text = lblToSSectionA.Text;
            else if (ddlPart.SelectedValue.ToString() == "SectionB")
                txtNOOfForms.Text = lblToSSectinB.Text;
        }
        

        GridAppTable.DataSource=GetDataSource1();
        GridAppTable.DataBind();
        TotalRecord.Text = GridAppTable.Rows.Count.ToString();
        onstartrollno();
        txtNOOfForms.Focus();
    }
   


    private DataSet GetDataSource1()
    {
        cmqry = "select SID,Status,ExamSeason,IMID,Course,Part,CenterCode,RollNo,City,City2 from ExamForms where City='" + ddlExamCity.Text.ToString() + "'  and ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and RollNo='N/A' and Status='Submitted'";
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }


    private DataSet GetDataSource2()
    {
        cmqry = "select SID,Status,ExamSeason,IMID,Course,Part,CenterCode,RollNo,City,City2 from ExamForms where  ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and CenterCode='" + checkno.Text.ToString() + "' or Status='" + checkno.Text.ToString() + "' order by rollno desc";
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }


    private DataSet GetDataSource3()
    {
        cmqry = "select distinct CenterCode,City from ExamForms where  ExamSeason='" + lblSeasonHidden.Text.ToString() + "'  and rollno='N/A' order by CenterCode";
        SqlDataAdapter ad = new SqlDataAdapter(cmqry, con);
        DataSet dt = new DataSet();
        ad.Fill(dt);
        return dt;
    }



    public void startrollno()
    {

        if (ddlCourse.SelectedValue.ToString() == "Civil")
        {
            if (ddlPart.SelectedValue.ToString() == "PartI")
            {
                StartRol.Text = lblCenterCode.Text.ToString() + "" + "1001";
                EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();

            }
                else if (ddlPart.SelectedValue.ToString() == "PartII")
                {
                    StartRol.Text = lblCenterCode.Text.ToString() + "" + "2001";
               EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();
                
                }
                else if (ddlPart.SelectedValue.ToString() == "SectionA")
                {
                    StartRol.Text = lblCenterCode.Text.ToString() + "" +  "3001";
                     EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();

                }
                else if (ddlPart.SelectedValue.ToString() == "SectionB")
                {
                   StartRol.Text = lblCenterCode.Text.ToString() + "" +  "4001";
                    EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();
                }
            }
            else if (ddlCourse.SelectedValue.ToString() == "Architecture")
            {
                if (ddlPart.SelectedValue.ToString() == "PartI")
                {
                    StartRol.Text = lblCenterCode.Text.ToString() + "" + "5001";
                     EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();
                }
                else if( ddlPart.SelectedValue.ToString() == "PartII")
                {
                    StartRol.Text = lblCenterCode.Text.ToString() + "" +  "6001";
                     EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();
                }
                else if (ddlPart.SelectedValue.ToString() == "SectionA")
                {
                     StartRol.Text = lblCenterCode.Text.ToString() + "" +  "7001";
                     EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();

                }
                else if (ddlPart.SelectedValue.ToString() == "SectionB")
                {
                    StartRol.Text = lblCenterCode.Text.ToString() + "" +  "8001";
                    EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text)-1).ToString();

                }
            }
            
        }



    public void onstartrollno()
    {

        con.Close(); con.Open();
     SqlCommand cmd = new SqlCommand("select  max(rollno) from examforms where City='" + ddlExamCity.Text.ToString() + "'  and ExamSeason='" + lblSeasonHidden.Text.ToString() + "' and Course='" + ddlCourse.SelectedValue.ToString() + "' and Part='" + ddlPart.SelectedValue.ToString() + "' and RollNo!='N/A'",con);
     string sum = Convert.ToString(cmd.ExecuteScalar());
     
  if(sum.Length>0 && sum!=null)
        {
            int a=1;
            StartRol.Text = sum.ToString();
            StartRol.Text = (Convert.ToInt32(StartRol.Text) + a).ToString();
            EndRollno.Text = (Convert.ToInt32(StartRol.Text) + Convert.ToInt32(TotalRecord.Text) - 1).ToString();
        }

        else
        {
        startrollno();
        }
        
        con.Close(); con.Dispose();
    }



    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        GridView3.DataSource = GetDataSource2();
        GridView3.DataBind();

    }
   
    protected void AdmitCard_Click(object sender, ImageClickEventArgs e)
    {
        GridView3.DataSource = GetDataSource3();
        GridView3.DataBind();
    }
    protected void txtNOOfForms_TextChanged(object sender, EventArgs e)
    {
        btnGenerateRollNo.Focus();
    }
    protected void Saveimp_Click(object sender, ImageClickEventArgs e)
    {
        string extension, fileName, fileName1;
        extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
        if (extension == ".xls" || extension == ".xlsx")                                       
        {
            fileName = FileUpload1.PostedFile.FileName;
            fileName1 = fileName.Replace(fileName, "ChangeStatus");
            FileUpload1.SaveAs(Server.MapPath("~/XML/uploads/") + fileName1 + ".xls");
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtExcel = readexcel();
            con.Close(); con.Open();
            for (int i = 0; i < dtExcel.Rows.Count; i++)
            {
                cmd = new SqlCommand("update ExamForms set Status='" + holdcmb.SelectedValue.ToString() + "' Where SID='" + dtExcel.Rows[i][0].ToString() + "' and ExamSeason='" + lblSeasonHidden.Text.ToString() + "'", con);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            
        }
        finally
        {
            con.Close(); con.Dispose();
    
        }
        lblAdmit.Text = "List of Hold Membership No";
    
    }
    

     private DataTable readexcel()
    {
         string Path = (Server.MapPath ("~/XML/uploads/ChangeStatus.xls"));           
           
         try
            {
                DataTable dtExcel = new DataTable();
                string SourceConstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Path + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'";
                OleDbConnection con1 = new OleDbConnection(SourceConstr);
                string query = "Select * from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, con1);
                data.Fill(dtExcel);
                GridView3.DataSource=dtExcel;
                GridView3.DataBind();
                return dtExcel;
            }
            catch (Exception ex)
            {
                //lblmessage.Text = ex.Message;
                return null;
            }
            finally
            {
                conLinq.Dispose();
            }
    }
}
