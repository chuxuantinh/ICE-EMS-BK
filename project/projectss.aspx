<%@ Page Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="projectss.aspx.cs" Inherits="project_projectss" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
Project
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server"  Text="Home" CssClass="redirecttab" onclick="lblHomeRedirect_Click"></asp:LinkButton></td><td>
<asp:Label ID="lbtnNext1Redirect" runat="server" Text="Project" CssClass="redirecttab" ></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Project Department</h1></div>
<asp:Image ID="Image1" runat="server" ImageUrl="~/images/ProjectBanner.jpg" Width="100%" Height="250px"/><hr />
<asp:Panel ID="pnlSpc" runat="server" Height="260px" />
</div>
<br />
</asp:Content>

