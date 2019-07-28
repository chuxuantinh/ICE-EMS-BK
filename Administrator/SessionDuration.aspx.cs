using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Administrator_SessionDuration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Server.HtmlEncode(Request.Cookies["MyLogin"]["PWD"])) == "")
            {
                Response.Redirect("../Login.aspx");

            }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../Login.aspx");
        }
    }
   
    protected void btnSave_Onclic(object sender, EventArgs e)
    {
        if (ddlStream.SelectedValue.ToString() == "Asso")
        {
            if (ddlPart.SelectedValue.ToString() == "PartI")
            {
            }
            else if (ddlPart.SelectedValue.ToString() == "PartII")
            {
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionA")
            {
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionB")
            {

            }
        }
        if (ddlStream.SelectedValue.ToString() == "Tech")
        {
            if (ddlPart.SelectedValue.ToString() == "PartI")
            {
            }
            else if (ddlPart.SelectedValue.ToString() == "PartII")
            {
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionA")
            {
            }
            else if (ddlPart.SelectedValue.ToString() == "SectionB")
            {

            }
        }
    }
    private void updateduration(string stream, string part, string value)
    {
        XDocument xdoc = XDocument.Parse("../Exam/SessionDuration.xml");
        var element = xdoc.Elements("part1").Single();
        element.Value = "222";
        xdoc.Save("../Exam/SessionDuration.xml");
          }
}
