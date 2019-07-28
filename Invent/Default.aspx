<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Invent_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">ICE(I): Inventory Section
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
   <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
   <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1> Inventory</h1></div>
<center> <img src="../images/invent.jpg" width="100%" height="250px" /></center><br /><br />
 </div>
 <br />
</asp:Content>

