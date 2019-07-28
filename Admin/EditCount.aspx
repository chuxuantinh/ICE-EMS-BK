<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EditCount.aspx.cs" Inherits="Admin_EditCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Edit Count
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblCity" runat="server" Text="Manage City" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"></h1><h1>
    Manage Edit Count</h1></div><br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<asp:Panel Height="400px" runat="server" ID="Panel1">
<center><table class="tbl" width="100%">
<tr><td align="right">Process Type:</td>
<td align="left">
    <asp:DropDownList  ID="ddlEdit" runat="server" 
        AutoPostBack="True" 
        CssClass="txtbox"  Width="160px" 
        onselectedindexchanged="ddlEdit_SelectedIndexChanged"></asp:DropDownList><br /></td>
</tr>
<tr><td align="right">
    <asp:Label ID="lblCounter0" runat="server" Text="Counter:"></asp:Label>
    </td>
<td align="left">
    <asp:Label ID="lblCount" runat="server" Text="Label"></asp:Label>
    <br /></td>
</tr>
<tr><td align="right">
    <asp:Label ID="Label9" runat="server" Text="Total:"></asp:Label>
    </td>
<td align="left">
    <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>
    <br /></td>
</tr>
<caption>
<br />
    <tr>
        <td align="right">
            Enter Counter Value</td>
        <td align="left">
            <asp:TextBox ID="txtCount" runat="server" CssClass="txtbox"></asp:TextBox>
        </td>
    </tr>
<tr><td></td>
<td align="left">
    <asp:Button ID="btnAdd" runat="server" CssClass="btnsmall"  
        Text="Add" onclick="btnAdd_Click" OnClientClick='return confirm("Are you sure you want to update?");'  />&nbsp;
<asp:Button ID="btnReset" runat="server" CssClass="btnsmall" Text="Reset" 
        onclick="btnReset_Click" OnClientClick='return confirm("Are you sure you want to reset values?");' />&nbsp;
</td>
</tr>
<caption>
<br />
<tr><td></td>
<td align="left"><asp:Label ID="Label5" runat="server" ForeColor="Maroon"></asp:Label></td>
<td>&nbsp;</td>
</tr>
</caption>
</caption>
</table></center>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>

