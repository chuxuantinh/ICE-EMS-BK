<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ManageFees.aspx.cs" Inherits="project_ManageFees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Fees of AICTE Approved Institution
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td><asp:Label ID="lblNext" runat="server" Text="Manage Fees" CssClass="redirecttabhome"/></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IM Code:&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>
    Manage Fees of AICTE Approved Institutions</h1></div><br />
<center>Select Institution:&nbsp;&nbsp;<asp:DropDownList ID="dropname" runat="server" CssClass="txtbox" Width="290px"  Font-Bold="true" ForeColor="Maroon" onselectedindexchanged="dropname_SelectedIndexChanged" AutoPostBack="true"/>&nbsp;&nbsp;<asp:Button ID="btnshow" runat="server" Text="ok" onclick="btnshow_Click" CssClass="btnsmall"/><br /><br />
</center>
<asp:Panel ID="Panel1" runat="server" Height="420px" Visible="False"><center>
<table class="tbl"><tr><th colspan="2">Training Fees:</th></tr>
<tr><td align="center">Civil Part II<br /><asp:TextBox ID="txtTCPartII" runat="server" CssClass="txtbox" Font-Bold="true" /></td>
    <td align="center">Civil Section B<br /><asp:TextBox ID="txtTCSectionB" runat="server" CssClass="txtbox" Font-Bold="true" /></td></tr>
<tr><td align="center">Architecture Part II<br /><asp:TextBox ID="txtTArchiPartII" runat="server" CssClass="txtbox" Font-Bold="true" /></td>
    <td align="center">Architecture Section B<br /><asp:TextBox ID="txtTArchiSectionB" runat="server" CssClass="txtbox" Font-Bold="true" /></td></tr>
<tr><td colspan="2">&nbsp;</td></tr><tr><th colspan="2">Guidence Fees:</th></tr>
<tr><td align="center">Civil Part II<br /><asp:TextBox ID="txtGCPartII" runat="server" CssClass="txtbox" Font-Bold="true" /></td>
    <td align="center">Civil Section B<br /><asp:TextBox ID="txtGCSectionB" runat="server" CssClass="txtbox" Font-Bold="true" /></td></tr>
<tr><td align="center">Architecture Part II<br /><asp:TextBox ID="txtGArchiPartII" runat="server" CssClass="txtbox" Font-Bold="true" /></td>
    <td align="center">Architecture Section B<br /><asp:TextBox ID="txtGArchiSectionB" runat="server" CssClass="txtbox" Font-Bold="true" /></td></tr>
</table><br />
<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnsmall" onclick="btnSubmit_Click" />
</center></asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="420px" Visible="False"/>
<br /><br /><br />
</div><br />
</asp:Content>