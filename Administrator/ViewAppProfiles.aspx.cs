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
using System.Xml;
using iTextSharp.text.html;

using iTextSharp.text.html.simpleparser;
public partial class Administrator_ViewAppProfiles : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
  
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
                    ddlstatebind();
                    lblid.Visible = true;
                    txtid.Visible = true;
                    rbtnactive.Visible = false;
                    rbtnblock.Visible = false;
                    Lblstate.Visible = false;
                    ddlState.Visible = false;
                    lblname.Visible = false;
                    txtname.Visible = false;
                    ddlSearch.SelectedValue = "IMID";
                    maikal mk = new maikal();
                    int lvl = mk.returnlevel(Convert.ToString(MyLogin.login[1]), Convert.ToString(MyLogin.login[0]));
                    if (lvl == 0)
                    { }
                    else if (lvl == 1)
                    {
                    }
                    else if (lvl == 2)
                    {

                    }
                    ddlSearch.Focus();
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
    protected void GridDuplicate_OnRowDateBound(object sender, GridViewRowEventArgs e)
    {

    }

    public DataTable function()
    {
        string str="";
        
        if (ddlSearch.SelectedItem.Text == "Status")
        {
            if (rbtnactive.Checked == true)
            {
                str = "Select ID,Name,State,Email,Mobile,Active from Member where Active='Register'";

            }
            else if (rbtnblock.Checked == true)
            {
               str = "Select ID,Name,State,Email,Mobile,Active from Member where Active='nos'";
            }
        }
        else if (ddlSearch.SelectedItem.Text == "Name")
        {
           str = "Select ID,Name,State,Email,Mobile,Active from Member where Name like '%" + txtname.Text + "%'";
            txtname.Focus();
        }
        else if (ddlSearch.SelectedItem.Text == "State")
        {

            str = "Select ID,Name,State,Email,Mobile,Active from Member where State='" + ddlState.SelectedValue.ToString() + "'";
            ddlState.Focus();
        }
        else if (ddlSearch.SelectedItem.Text == "IMID")
        {
             str = "Select ID,Name,State,Email,Mobile,Active from Member where ID='" + txtid.Text + "'";
            txtid.Focus();
        }
        else if (ddlSearch.SelectedItem.Text == "All Members")
        {
          str = "Select ID,Name,State,Email,Mobile,Active from Member";
            Button1.Focus();
        }
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
       }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridDuplicacy.DataSource = function();
        GridDuplicacy.DataBind();
        
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearch.SelectedItem.Text == "Status")
        {
            rbtnactive.Visible = true;
            rbtnblock.Visible = true;
            lblid.Visible = false;
            txtid.Visible = false;
            Lblstate.Visible = false;
            ddlState.Visible = false;
            lblname.Visible = false;
            txtname.Visible = false;
           

        }
        else if (ddlSearch.SelectedItem.Text == "Name")
        {
            lblname.Visible = true;
            txtname.Visible = true;
            rbtnactive.Visible = false;
            rbtnblock.Visible = false;
            lblid.Visible = false;
            txtid.Visible = false;
            Lblstate.Visible = false;
            ddlState.Visible = false;
           
        }
        else if (ddlSearch.SelectedItem.Text == "State")
        {
            Lblstate.Visible = true;
            ddlState.Visible = true;
            rbtnactive.Visible = false;
            rbtnblock.Visible = false;
            lblid.Visible = false;
            txtid.Visible = false;
            lblname.Visible = false;
            txtname.Visible = false;
            
        }
        else if (ddlSearch.SelectedItem.Text == "IMID")
        {
            lblid.Visible = true;
            txtid.Visible = true;
            rbtnactive.Visible = false;
            rbtnblock.Visible = false;
            Lblstate.Visible = false;
            ddlState.Visible = false;
            lblname.Visible = false;
            txtname.Visible = false;
           
        }
        else if (ddlSearch.SelectedItem.Text == "All Members")
        {
            lblid.Visible = false;
            txtid.Visible = false;
            rbtnactive.Visible = false;
            rbtnblock.Visible = false;
            Lblstate.Visible = false;
            ddlState.Visible = false;
            lblname.Visible = false;
            txtname.Visible = false;
        }
       
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        Button1.Focus();
    }
   
    protected void GridDuplicacy_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "select1")
        {
            string i = check(e.CommandArgument.ToString());
            if (i == "yes")
            {
                Response.Redirect("../Administrator/FellowmemberRegistration.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + e.CommandArgument.ToString() + "");
            }
            else if (i == "no")
            {
                Response.Redirect("../Administrator/ImRegiUpdate.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + e.CommandArgument.ToString() + "");
            }

        }
        else if (e.CommandName == "select2")
        {
            string i = check(e.CommandArgument.ToString());
            if (i == "yes")
            {
                Response.Redirect("../Administrator/FellowmemberRegistration.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + e.CommandArgument.ToString() + "");
            }
            else if (i == "no")
            {
                Response.Redirect("../Administrator/IMProfile.aspx?name=" + Request.QueryString["dev"] + "&lnk=null&typ=Ms&lvl=" + Request.QueryString["lvl"] + "&id=" + e.CommandArgument.ToString() + "");
            }
        }

    }
    string x;
    public string check(string strg)
    {
        con.Open();
        string str = "Select Type from Member where ID='" + strg + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader rd = cmd.ExecuteReader();
        int name = rd.GetOrdinal("Type");
        while (rd.Read())
        {
            string aname = rd.GetValue(name).ToString();
            if (aname != "IM")
            {
                x = "yes";
            }
            else
            {
                x = "no";
            }
        }
        rd.Close(); rd.Dispose(); con.Close();
        con.Dispose();
        return x;
       

    }

  
    /*Reports*/
   
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button1.Focus();
    }
    public void ddlstatebind()
    {
        SqlCommand cmd = new SqlCommand("Select distinct State from Member", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlState.DataTextField = ds.Tables[0].Columns["State"].ToString();
        ddlState.DataSource = ds.Tables[0];
        ddlState.DataBind();
    }
    protected void rbtnactive_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void rbtnactive_CheckedChanged1(object sender, EventArgs e)
    {
        Button1.Focus();
    }


    protected void GridDuplicacy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
        GridDuplicacy.PageIndex = e.NewPageIndex;
        GridDuplicacy.DataSource = function();
        GridDuplicacy.DataBind();
       
        
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        Button1.Focus();
    }
    protected void txtid_TextChanged(object sender, EventArgs e)
    {
        Button1.Focus();
    }
    protected void GridDuplicacy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ibtnExportDocAppTableDoc_click(object sender, ImageClickEventArgs e)
    {
        GridDuplicacy.AllowPaging = false;
        GridDuplicacy.DataSource = function();
        GridDuplicacy.DataBind();
        if (GridDuplicacy.Rows.Count > 0)
        {
            GridDuplicacy.Columns[7].Visible = false;
            GridDuplicacy.Columns[8].Visible = false;
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
        GridDuplicacy.DataSource = function();
        GridDuplicacy.DataBind();
        if (GridDuplicacy.Rows.Count > 0)
        {
            GridDuplicacy.Columns[7].Visible = false;
            GridDuplicacy.Columns[8].Visible = false;
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
        GridDuplicacy.DataSource = function();
        GridDuplicacy.DataBind();
        if (GridDuplicacy.Rows.Count > 0)
        {
            GridDuplicacy.Columns[7].Visible = false;
            GridDuplicacy.Columns[8].Visible = false;
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
