<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SystemName.aspx.cs" Inherits="Admin_SystemName" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">System Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblHomelink" runat="server" CssClass="redirecttabhome"></asp:Label>     </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Manage System</h1></div>
<asp:UpdatePanel ID="updpnl" runat="server"><ContentTemplate>
<asp:Panel Height="400px" runat="server" ID="pnlSpace"><center><table class="tbl">
<tr><td><asp:Label ID="lblName" runat="server" Text="[Label]" Font-Bold="true"></asp:Label>:</td><td>
<asp:DropDownList  ID="ddlType" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlType_SelectedIndexChanged" CssClass="txtbox" 
        Width="145px">
</asp:DropDownList><br /></td></tr>
<tr><td><asp:Label ID="lblName2" runat="server" Font-Bold="true"></asp:Label>:&nbsp;&nbsp;</td>
<td><asp:TextBox ID="txtDept" runat="server" CssClass="txtbox"></asp:TextBox><br /></td>
</tr>
<caption>
<br />
<tr><td></td>
<td><asp:Button ID="btnAdd" runat="server" CssClass="btnsmall" onclick="btnAdd_Click" Text="Add" />&nbsp;
<asp:Button ID="btnEdit" runat="server" CssClass="btnsmall" onclick="btnEdit_Click" Text="Edit" />&nbsp;
<asp:Button ID="btnDelete" runat="server" CssClass="btnsmall" onclick="btnDelete_Click" Text="Delete" />&nbsp;
</td>
</tr>
<caption>
<br />
<tr><td></td>
<td><asp:Label ID="lblException" runat="server" ForeColor="Maroon"></asp:Label></td>
</tr>
</caption>
</caption>
</table></center>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>

