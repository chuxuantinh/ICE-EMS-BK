<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="SuperAdminManage.aspx.cs" Inherits="Admin_SuperAdminManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Reception ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />

    <link href="AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:Label ID="lbtnNext1Redirect" runat="server" Text="Front Office" CssClass="redirecttab" 
             ></asp:Label> </td></tr></table></div>
             <div id="rightpanel2"><div id="header">
             <div class="fromRegisterlbl"><h1>Front Office</h1></div>
             <img src="../images/FrontOffice.gif" alt="Front Office" width="100%" /><hr />
            <div style="height:200px;"></div>
             </div></div>
</asp:Content>

