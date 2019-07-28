<%@ Page Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ViewExamSponcer.aspx.cs" Inherits="Exam_ViewExamSponcer" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exam Invigilator
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblPageName" runat="server" CssClass="redirecttabhome" 
            Text="View Exam Invigilator"></asp:Label>
    </td></tr></table></div>

<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Exam Invigilator</h1></div><br />
<table class="tbl"><tr>
    <td>Session:</td><td>
    <asp:DropDownList ID="dropsession" CssClass="txtbox" runat="server" 
        onselectedindexchanged="dropsession_SelectedIndexChanged">
        <asp:ListItem Value="Sum" Text="Summer Examination"></asp:ListItem>
        <asp:ListItem Value="Win" 
            Text="Winter Examination"></asp:ListItem>
    </asp:DropDownList>
</td><td>&nbsp;<asp:TextBox ID="txtsession" runat="server" 
            CssClass="txtbox"></asp:TextBox>
        <asp:Label ID="lblSeason" runat="server" Visible="False"></asp:Label>
    </td></tr>
</table>
<table class="tbl"><tr><td>Type: </td><td>
    &nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="droptype" runat="server" CssClass="txtbox" 
        AutoPostBack="True" onselectedindexchanged="droptype_SelectedIndexChanged">
        <asp:ListItem Value="Superintendent">Exam Superintendent</asp:ListItem><asp:ListItem Value="Invigilator">Invigilator</asp:ListItem></asp:DropDownList>
</td><td>Name of Exam Center:</td><td>
    <asp:DropDownList ID="txtExamCentrName" runat="server" CssClass="txtbox" 
        AutoPostBack="True"></asp:DropDownList>
</td><td>&nbsp;&nbsp;<asp:Button ID="btnsearch" runat="server" Text="OK" 
            onclick="btnsearch_Click" CssClass="btnsmall" /></td></tr></table><br /><br />
<hr /><asp:Panel ID="pan" runat="server" Visible="false"><br />
<table class="tbl"><tr><td>Full Name :</td><td><asp:Label ID="txtName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Designation.:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtDEsignation" runat="server"  Font-Bold="true" ></asp:Label></td></tr>
<tr><td></td></tr>
<tr><td>Center Code:</td><td><asp:Label ID="txtcenterCode" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
<tr><td></td></tr>
<tr><td>Permanent Address:</td><td colspan="3"><asp:Label ID="txtAddress1"  Height="20px" runat="server" Width="60%" Font-Bold="true"></asp:Label></td></tr>
<tr><td></td></tr>
<tr><td>City:</td><td><asp:Label ID="txtPCity" runat="server" Font-Bold="true"></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPState" runat="server" Font-Bold="true" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPPincode" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td></td></tr>
<tr><td>Phone:</td><td colspan="3"><asp:Label ID="txtPhoneNo" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td></td></tr>
<tr><td>Mobile:</td><td><asp:Label ID="txtMobile" runat="server" Font-Bold="true"></asp:Label></td><td colspan="2">
    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp; <asp:Label ID="txtEmail" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td></td></tr>
</table> <br /><br /> <hr /></asp:Panel>
<br />
<div style="height:350px;"></div>
</div>
</asp:Content>



