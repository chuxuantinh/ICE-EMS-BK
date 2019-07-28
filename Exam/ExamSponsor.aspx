<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamSponsor.aspx.cs" Inherits="Exam_ExamSponsor" %>
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
<asp:Label ID="lblPageName" runat="server" CssClass="redirecttabhome" Text="Exam Invigilator"></asp:Label>
</td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Exam Invigilator</h1></div><br />
<table class="tbl"></table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table class="tbl"><tr><td>Session:</td><td>
<asp:DropDownList ID="dropsession" CssClass="txtbox" runat="server" onselectedindexchanged="dropsession_SelectedIndexChanged" Width="160px"><asp:ListItem Value="Sum" Text="Summer Examination"></asp:ListItem><asp:ListItem Value="Win" Text="Winter Examination"></asp:ListItem>
</asp:DropDownList>
</td><td>Year:&nbsp;<asp:TextBox ID="txtsession" runat="server" CssClass="txtbox" Width="55px"></asp:TextBox>
<asp:Label ID="lblSeason" runat="server" Visible="False"></asp:Label>
</td></tr>
<tr><td>Full Name :</td><td>
<asp:TextBox ID="txtName" runat="server" CssClass="txtbox" Width="157px"></asp:TextBox><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td><td colspan="2">
&nbsp;&nbsp;&nbsp;Designation.:&nbsp;<asp:TextBox ID="txtDEsignation" runat="server" CssClass="txtbox" ></asp:TextBox></td></tr>
<tr><td>Type: </td><td>
<asp:DropDownList ID="droptype" runat="server" Width="160px" CssClass="txtbox"><asp:ListItem Value="Superintendent">Exam Superintendent</asp:ListItem><asp:ListItem Value="Invigilator">Invigilator</asp:ListItem></asp:DropDownList>
</td></tr>
<tr><td>Exam Center:</td><td>
<asp:DropDownList ID="ddlExamCenterName" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamCenter_OnSelectedIndexChanged"  DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
</asp:DropDownList>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT [Name] FROM [ExamCenter] WHERE ([Season] = @Season2)">
<SelectParameters>
<asp:ControlParameter ControlID="lblSeason" Name="Season2" PropertyName="Text" Type="String" />
</SelectParameters>
</asp:SqlDataSource>
</td><td>&nbsp;&nbsp;&nbsp;Center Code:<asp:Label ID="txtcenterCode" runat="server"  Font-Bold="true"></asp:Label></td></tr>
<tr><td>Permanent Address:</td><td colspan="3"><asp:TextBox ID="txtAddress1"  Height="20px" runat="server" CssClass="txtbox" Width="66%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtAddress2"  Height="20px" runat="server" CssClass="txtbox" Width="66%"></asp:TextBox></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlCity" runat="server" CssClass="txtbox"></asp:DropDownList></td><td>
State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="txtbox"></asp:DropDownList></td><td>
Pin:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Phone:</td><td colspan="3"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox" Width="167px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>
<tr><td></td><td></td><td>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblcode" runat="server" Text="" Font-Bold="true"></asp:Label><td>
<asp:Label ID="lblsession" runat="server" Text="" Visible="false"></asp:Label></td></td></tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
<center><asp:Label ID="lblException" runat="server" ForeColor="Maroon"></asp:Label>
<br /><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnsmall" ValidationGroup="Architecture"  onclick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btbClear" runat="server" Text="Clear" CssClass="btnsmall" onclick="btbClear_Click" /></center><br /><br />
<div style="height:240px;"></div>
</div>
</asp:Content>