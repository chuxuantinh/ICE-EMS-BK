<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="City.aspx.cs" Inherits="Admin_Default2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage City
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
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"></h1><h1>Manage City of States</h1></div><br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<asp:Panel Height="400px" runat="server" ID="Panel1">
<center><table class="tbl" width="100%">
<tr><td align="right"><asp:Label ID="Label1" runat="server" Text="State" Font-Bold="true"></asp:Label>:</td>
<td align="left"><asp:DropDownList  ID="ddlState" runat="server" 
        AutoPostBack="True" onselectedindexchanged="ddlState_SelectedIndexChanged" 
        CssClass="txtbox"  Width="160px"></asp:DropDownList><br /></td>
</tr>
<tr><td align="right"><asp:Label ID="Label4" runat="server" Text="City" Font-Bold="true"></asp:Label>:</td>
<td align="left"><asp:DropDownList  ID="ddlcity" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlcity_SelectedIndexChanged" CssClass="txtbox" 
        Width="160px"></asp:DropDownList><br /></td>
</tr>
<tr><td align="right"><asp:Label ID="Label2" runat="server" Font-Bold="true" Text="City Name"></asp:Label>
    :</td>
<td align="left"><asp:TextBox ID="Txtcity" runat="server" CssClass="txtbox" 
        Width="155px"></asp:TextBox><br /></td>
</tr>
<caption>
<br />
<tr><td></td>
<td align="left"><asp:Button ID="Button1" runat="server" CssClass="btnsmall" onclick="btnAddd_Click" Text="Add" />&nbsp;
<asp:Button ID="Button2" runat="server" CssClass="btnsmall" onclick="btnEditt_Click" Text="Edit" />&nbsp;
<asp:Button ID="Button3" runat="server" CssClass="btnsmall" onclick="btnDeletee_Click" Text="Delete" />&nbsp;
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

