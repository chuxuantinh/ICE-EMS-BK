<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="AutoCADSearch.aspx.cs" Inherits="Admission_AutoCADSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Search Auto CAD Application
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lbltitle" runat="server" Text="View M-CAD Application Forms" CssClass="redirecttabhome"></asp:Label></td></tr></table>
</div><div id="rightpanel2">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>View Auto CAD Details</h1></div>
<center>View By:&nbsp;<asp:DropDownList ID="ddlViewBy" runat="server" CssClass="txtbox"><asp:ListItem Value="RegistrationNo" Text="RegistrationNo"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Registration No:&nbsp;&nbsp;<asp:TextBox ID="txtRegistrationNo" runat="server" CssClass="txtbox"></asp:TextBox></center>
</ContentTemplate>
</asp:UpdatePanel>
</div>
<br />
</asp:Content>