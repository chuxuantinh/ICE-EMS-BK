using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Globalization;
using System.Xml;

public partial class Administrator_ManageGroups : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlIM.Visible = false; pnlGID.Visible = false; pnlNewIM.Visible = false;
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select GID from IM where ID='" + txtIMID.Text.ToString() + "'", con);
        string gid = Convert.ToString(cmd.ExecuteScalar());
        if (gid == "")
        {
            lblException.Text = "Invalid IMID!!!"; pnlIM.Visible = false;
        }
        else
        {
            lblGID.Text = gid; pnlIM.Visible = true; lblException.Text = "";
            fill();
        }
        con.Close();
    }
    private void fill()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select ID from IM where GID='" + lblGID.Text.ToString() + "'", con);
        DataSet dt = new DataSet();
        adp.Fill(dt);
        ddlIM.DataSource = dt;
        ddlIM.DataTextField = "ID";
        ddlIM.DataValueField = "ID";
        ddlIM.DataBind();
    }
    private void genid(string gid)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/GID.xml"));
        XmlNodeList lstVideos = xmlDoc.GetElementsByTagName("ID");
        XmlNode nd = lstVideos.Item(0);
        int it = Convert.ToInt32(nd.InnerText); it = it + 1;
        nd.InnerText = (it).ToString();
        xmlDoc.Save(MapPath("~/Xml/GID.xml"));
    }
    private string selectid()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(MapPath("~/Xml/GID.xml"));
        XmlNodeList lstVideos = xmlDoc.GetElementsByTagName("ID");
        XmlNode nd = lstVideos.Item(0);
        int it = Convert.ToInt32(nd.InnerText); it = it + 1;
        nd.InnerText = (it).ToString();
        string id = "";
        if (it <= 9)
        {
            id = "GID00" + it.ToString();
        }
        else if (it > 9 && it <= 99)
        {
            id = "GID0" + it.ToString();
        }
        else if (it > 99)
        {
            id = "GID" + it.ToString();
        }
        return id;
    }
    protected void lbtnGID_Click(object sender, EventArgs e)
    {
        pnlGID.Visible = true; pnlNewIM.Visible = false;
        lblGroup.Text = selectid();
    }
    protected void lbtnNewIM_Click(object sender, EventArgs e)
    {
        pnlNewIM.Visible = true; pnlGID.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update IM set GID='" + lblGroup.Text.ToString() + "' where ID='" + txtIMID.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("update IMAccount set GID='" + lblGroup.Text.ToString() + "' where IMID='" + txtIMID.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        genid(lblGroup.Text.ToString());
        con.Close(); con.Dispose();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update IM set GID='" + lblGID.Text.ToString() + "' where ID='" + txtIMI.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("update IMAccount set GID='" + lblGID.Text.ToString() + "' where IMID='" + txtIMI.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        fill();
       con.Close(); con.Dispose();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update IM set GID='" + selectid() + "' where ID='" + txtIMI.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("update IMAccount set GID='" + selectid() + "' where IMID='" + txtIMI.Text.ToString() + "'", con);
        cmd.ExecuteNonQuery();
        genid(lblGroup.Text.ToString()); fill();
        con.Close(); con.Dispose();

    }
}