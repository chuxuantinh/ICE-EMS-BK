<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SubjectPrices.aspx.cs" Inherits="Admin_SubjectPrices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"> Manage Subject Prices
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:Label ID="lblHomelink" runat="server" CssClass="redirecttabhome" Text="Subject Prices Management"></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnlSubjectPrices" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Subject Master</h1></div>
<asp:Panel Height="400px" runat="server" ID="pnlSpace"><center><br />
<table class="tbl" cellspacing="1"><tr><td class="style1">
<asp:Label ID="Label1" runat="server" Text="Course Level" Font-Bold="true"></asp:Label>
    :</td><td class="style1">
</td>
<td colspan="4" class="style1">
<asp:DropDownList ID="ddlCourseLevel" runat="server" onselectedindexchanged="ddlCourseLevel_SelectedIndexChanged" CssClass="txtbox">
</asp:DropDownList>
</td>
</tr>
<tr><td class="style2"><asp:Label ID="Label2" runat="server" Text="Course" Font-Bold="true"></asp:Label>
    :</td>
<td class="style2"></td>
<td class="style2"><asp:DropDownList ID="ddlcourse" runat="server" AutoPostBack="True" onselectedindexchanged="ddlcourse_SelectedIndexChanged" CssClass="txtbox">
<asp:ListItem Value="Civil ">Civil Engineering</asp:ListItem>
<asp:ListItem Value="Architecture ">Architecture Engineering</asp:ListItem>
</asp:DropDownList>
</td>
<td class="style2"></td>
<td class="style2"><asp:Label ID="Label3" runat="server" Text="Part/Section" Font-Bold="true"></asp:Label>
    :</td>
<td class="style2"><asp:DropDownList ID="ddlPart" runat="server" AutoPostBack="True" onselectedindexchanged="ddlPart_SelectedIndexChanged" CssClass="txtbox">
<asp:ListItem>PartI</asp:ListItem>
<asp:ListItem>PartII</asp:ListItem>
<asp:ListItem>SectionA</asp:ListItem>
<asp:ListItem>SectionB</asp:ListItem>
</asp:DropDownList><br />
</td>
</tr>
<tr><td class="style2"><asp:Label ID="Label4" runat="server" Text="Subject Code" Font-Bold="true"></asp:Label>
    :</td>
<td class="style2"></td>
<td class="style2"><asp:DropDownList ID="ddlSubCode" runat="server" onselectedindexchanged="ddlSubCode_SelectedIndexChanged" AutoPostBack="True" CssClass="txtbox">
</asp:DropDownList>
</td>
<td class="style2"></td>
<td class="style2"><asp:Label Text="Subject Name:" runat="server" Visible="false" ID="lblSubName" Font-Bold="true" /><br /></td>
<td class="style2"><asp:Label ID="lblSubjectName" runat="server"></asp:Label></td>
</tr>
<tr><td class="style3"><asp:Label ID="Label6" runat="server" Text="Price" Font-Bold="true"></asp:Label>:</td>
<td class="style3"></td>
<td colspan="4" class="style3"><asp:TextBox ID="txtPrice" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br /></td>
</tr>
<tr><td class="style3">&nbsp;</td>
<td class="style3">&nbsp;</td>
<td class="style3" colspan="4" align="left"><asp:Label ID="lblexception" runat="server" ForeColor="Maroon"></asp:Label></td>
</tr><tr><td><br />&nbsp;</td>
<td colspan="5" align="left"><asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" Text="Update" CssClass="btnsmall"/>&nbsp;&nbsp;
<asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear"  CssClass="btnsmall"/>
</td></tr></table></center>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>

