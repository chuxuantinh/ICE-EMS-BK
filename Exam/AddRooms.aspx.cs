using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Exam_AddRooms : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Conn"]);
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
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                maikal dev = new maikal();
                int se = dev.chksession();
                if (se == 0)
                {
                    ddlExamSeason.SelectedValue = "Sum";
                }
                else { ddlExamSeason.SelectedValue = "Win"; }// lblFromName.Text = "Membership No:";
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                txtYearSeason.Text = DateTime.Now.Year.ToString();
                lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
                btnDelete.Visible = false;
                ddlExamSeason.Focus();
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
            {
                Response.Redirect("../UserHome.aspx?" + Request.Cookies["redic"].Value.ToString());
            }
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
    protected void btnSessionOK_OnClick(object sender, EventArgs e)
    {
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        GridView1.DataBind();
    }
    int total, ntotal;
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr;
        gr = GridView1.SelectedRow;
        lblEnrolment.Text = gr.Cells[1].Text.ToString();
        lblCenteCode.Text = gr.Cells[1].Text.ToString();
        room();
        txtRoomCapacity.Focus();
    }
    protected void room()
    {
        txtRoomNo.Visible = true;
        lblSeasonHidden.Text = ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString();
        try
        {
            con.Close();
            con.Open();
            cmd = new SqlCommand("select Max(RoomNo) from Rooms where ID='" + lblEnrolment.Text + "' and Season='" + lblSeasonHidden.Text.ToString() + "'", con);
            string rno = Convert.ToString(cmd.ExecuteScalar());
            con.Close(); con.Dispose();
            if (rno == "")
            {
                rno = "1";
            }
            else
            {
                int i = Convert.ToInt32(rno);
                i = i + 1;
                rno = i.ToString();
            }
            txtRoomNo.Text = rno.ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void btnRoom_click(object sender, EventArgs e)
    {
        try
        {
            con.Close();
            if (txtRoomCapacity.Text != "")
            {
                con.Open();
                cmd = new SqlCommand("select RoomName from Rooms where ID='" + lblEnrolment.Text + "' and RoomName='" + txtRoomName.Text + "'", con);
                string rname = Convert.ToString(cmd.ExecuteScalar());
                if (rname == txtRoomName.Text)
                {
                    lblExceptionRoom.Text = "Room Name already Exist.";
                    lblExceptionRoom.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    cmd = new SqlCommand("update ExamCenter set ToSeat=ToSeat+'" + Convert.ToInt32(txtRoomCapacity.Text) + "' where ID='" + lblEnrolment.Text + "' and Season='" + lblSeasonHidden.Text + "' ", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into Rooms(Season,ID,RoomNo,Capacity,Columns,RoomName) values(@Season,@ID,@RoomNo,@Capacity,@Columns,@RoomName)", con);
                    cmd.Parameters.AddWithValue("@Season", ddlExamSeason.SelectedValue.ToString() + "" + txtYearSeason.Text.ToString());
                    cmd.Parameters.AddWithValue("@ID", lblEnrolment.Text.ToString().TrimStart('0'));
                    cmd.Parameters.AddWithValue("@RoomNo", Convert.ToInt32(txtRoomNo.Text.ToString()));
                    cmd.Parameters.AddWithValue("@Capacity", Convert.ToInt32(txtRoomCapacity.Text.ToString()));
                    cmd.Parameters.AddWithValue("@Columns", ddlRoomColumn.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@RoomName", txtRoomName.Text);
                    cmd.ExecuteNonQuery();
                    lblExceptionRoom.Text = "Room No.: " + txtRoomNo.Text.ToString() + "  of Capacity: " + txtRoomCapacity.Text.ToString() + " Added.";
                    gvroomshow.DataBind();
                    room();
                }
            }
            GridView1.DataBind();
        }
        catch (SqlException ex)
        {
            lblExceptionRoom.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        txtRoomCapacity.Text = "";
        txtRoomCapacity.Focus();
        invisible.Visible = false;
    }
    protected void btnCAncel_Click(object sender, EventArgs e)
    {
        btnRoom.Visible = true; btnDelete.Visible = false;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            con.Close(); con.Open();
            cmd = new SqlCommand("delete Rooms where Season='" + lblSeasonHidden.Text.ToString() + "' and ID='" + lblCenteCode.Text.ToString() + "' and RoomNo='" + txtRoomNo.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            gvroomshow.DataBind();
            lblExceptionRoom.Text = "Successfully Deleted.";
            room();
        }
        catch (SqlException ex)
        {
            lblExceptionRoom.Text = "Error";
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    protected void gvroomshow_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRoomNo.Text = gvroomshow.SelectedRow.Cells[3].Text.ToString();
        txtRoomName.Text = gvroomshow.SelectedRow.Cells[4].Text.ToString();
        txtRoomCapacity.Text = gvroomshow.SelectedRow.Cells[5].Text.ToString();
        ddlRoomColumn.SelectedItem.Text = gvroomshow.SelectedRow.Cells[6].Text.ToString();
        btnDelete.Visible = true; btnRoom.Visible = false;
    }
}