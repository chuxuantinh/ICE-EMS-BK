<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="Viewstatus.aspx.cs" Inherits="Admission_Viewstatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Status 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="View Status" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>VIEW STATUS & CHANGE STATUS </h1></div>
<br /><br />
<center> <br />
<asp:Label ID="lblId" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
<br /><asp:Label ID="lblName" runat="server" Text="" Font-Bold="True" ForeColor="Black"></asp:Label>
<br />
<asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="True" ForeColor="Black"></asp:Label><br />
<asp:Button ID="btnStatus" runat="server" Text="Change Status" onclick="btnStatus_Click" CssClass="btnsmall" /><br />
<br /><asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
<hr /><br />
</center>
<br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>
</asp:Content>

