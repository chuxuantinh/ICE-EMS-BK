<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamFormDeletion.aspx.cs" Inherits="Exam_ExamFormDeletion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Delete Examination Form
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
input:focus,textarea:focus,select:focus {
    outline:1px solid red;
}
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Exam Form" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1><blink>In Maintanance Mode</blink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSchedule" runat="server" CssClass="txtbox"><asp:ListItem Value="Home" Text="Home" /><asp:ListItem Value="Overseas" Text="Overseas" /></asp:DropDownList></h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox></td></tr></table></center>
<br /><center>Application  No or Membership:&nbsp;&nbsp;<asp:TextBox ID="txticesn" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtSerialNo_TextChanged" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<b>
<br /> &nbsp;<asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label><br /></center>
<asp:Panel ID="pnlMain" runat="server"><div style="overflow:scroll">
<asp:GridView ID="grdForms" runat="server" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" HorizontalAlign="Center" 
        GridLines="Vertical" Width="100%"  
        onselectedindexchanged="grdForms_SelectedIndexChanged" 
        onrowdatabound="grdForms_RowDataBound">
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
         <asp:CommandField HeaderText="Delete" ShowHeader="True" 
                ShowSelectButton="True" SelectText="Delete" />
      </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <EmptyDataTemplate>
          <center>  No Record found !!!</center>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView></div>
    <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label><asp:Label ID="lblAmount" runat="server" Visible="false"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
</asp:Panel>
<asp:Panel ID="panelInVisible" runat="server" Height="500px"></asp:Panel>
    </div>
</asp:Content>

