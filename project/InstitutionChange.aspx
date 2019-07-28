<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="InstitutionChange.aspx.cs" Inherits="project_InstitutionChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Change Profile of AICTE Approved Institution</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td><asp:Label ID="lblNext" runat="server" Text="Change Profile" CssClass="redirecttabhome"/></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IM Code:&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Change Profile of AICTE Approved Institutions</h1></div><br />
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false" /><asp:Label ID="lblSession" runat="server" Visible="false" />
<center>Select Institution:&nbsp;&nbsp;<asp:DropDownList ID="dropname" runat="server" CssClass="txtbox" Width="290px"  Font-Bold="true" ForeColor="Maroon" onselectedindexchanged="dropname_SelectedIndexChanged" AutoPostBack="true"/>&nbsp;&nbsp;<asp:Button ID="btnshow" runat="server" Text="ok" onclick="btnshow_Click" CssClass="btnsmall"/><br /><br />
<asp:Panel ID="Panel1" runat="server" Visible="False"><table class="tbl"><tr><td align="left">Institution</td><td align="left">:</td>
<td colspan="5" align="left"><asp:TextBox ID="txtName" runat="server" CssClass="txtbox" Width="358px"/><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Please Insert Institution Name" ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td></tr>
<tr><td align="left">Address</td><td align="left">:</td><td colspan="5" align="left">
<asp:TextBox ID="txtAdd1" runat="server" CssClass="txtbox" Font-Bold="true" Width="358px" /></td></tr>
<tr><td align="left" colspan="2">&nbsp;</td><td align="left" colspan="5">
<asp:TextBox ID="txtAdd2" runat="server" CssClass="txtbox" Font-Bold="true" Width="358px" /></td></tr>
<tr><td colspan="2">&nbsp;</td><td colspan="3" align="left">State:<br /><asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" CssClass="txtbox" onselectedindexchanged="ddlState_SelectedIndexChanged" Width="162px"/></td><td align="left">City:<br />
<asp:DropDownList ID="ddlCity" runat="server" CssClass="txtbox" Width="160px"/><br /></td><td align="left">PinCode:<br /><asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox" />
<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtPPincode"/>
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPPincode" ErrorMessage="PIN CODE limit exit." Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture" ValueToCompare="999999">*</asp:CompareValidator></td></tr>
<tr><td align="left">Phone</td><td align="left">:</td><td colspan="5" align="left"><asp:TextBox ID="txtPhoneNo"  runat="server" CssClass="txtbox" Width="160px"/>
<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Custom" ValidChars="0123456789-" /></td></tr>
<tr><td align="left">Mobile</td><td align="left">:</td>
<td colspan="3" align="left"><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox" Width="160px"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" controltovalidate="txtMobile" Display="Dynamic" errormessage="Please Insert Mobile No." ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile No. can not be greater than 12 No." Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture" ValueToCompare="999999999999">*</asp:CompareValidator>
<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers" /></td>
<td colspan="2" align="left">Email:&nbsp;<asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox" Width="205px" /><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Architecture">*</asp:RegularExpressionValidator></td></tr>
<tr><td align="left">Reg. Date</td><td align="left">:</td>
<td><asp:TextBox ID="txtreg" runat="server" CssClass="txtbox" Width="160px"/><asp:CalendarExtender ID="calid" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtreg"/></td>
<td><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td>
<td><asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" controltovalidate="txtreg" Display="Dynamic" errormessage="Insert Registration Date" ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td>
<td align="left" colspan="2">HOD: &nbsp;<asp:TextBox ID="txtHOD" runat="server" CssClass="txtbox" Width="205px" /></td></tr>
<tr><td align="left">Admin Person</td><td align="left">:</td><td align="left" colspan="3"><asp:TextBox ID="txtPerson" runat="server" CssClass="txtbox" Width="160px" Font-Bold="true" /></td>
    <td align="left" colspan="2">Designation:&nbsp;<asp:TextBox ID="txtDesig" runat="server" CssClass="txtbox" Width="170px" Font-Bold="true" /></td></tr></table>
<div style="width:70%; margin:10px; margin-top:0px;">
<fieldset><legend><font style="color:#B21235; font-size:13px; font-family:Verdana;">Session:-</font></legend>
<table class="tbl"><tr><td align="center" colspan="5"><b>-Start Session-</b></td><td align="center"><b>
    -Expire Session-</b></td></tr>
<tr><td align="center" colspan="5"><asp:DropDownList ID="ddlsession" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" ForeColor="Brown" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" Width="180px">
<asp:ListItem Text="Summer Examination" Value="Sum" /><asp:ListItem Text="Winter Examination" Value="Win" /></asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="txtdevYearSeason_TextChanged" Width="50px" /></td>
<td align="center"><asp:DropDownList ID="ddlsession3" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon" Width="180px" OnTextChanged="ddlsession3_SelectedIndexChanged"><asp:ListItem Text="Summer Examination" Value="Sum" /><asp:ListItem Text="Winter Examination" Value="Win" /></asp:DropDownList>&nbsp;Year: <asp:TextBox ID="txtSession3" runat="server" AutoPostBack="true" CssClass="txtbox" Width="50px" ontextchanged="txtSession3_TextChanged" /></td></tr></table></fieldset></div><br />
<b>=> Check the courses for Approved Institutions</b><br />
<asp:CheckBox ID="chkCPartII" runat="server" Text="Civil Part II" /><asp:CheckBox ID="chkCSectionB" runat="server" Text="Civil Section B" /><asp:CheckBox ID="chkAPartII" runat="server" Text="Architecture Part II" /><asp:CheckBox ID="chkASectionB" runat="server" Text="Architecture Section B" />
<br /><center><asp:ValidationSummary ID="validatesummmary" ValidationGroup="Architecture"  runat="server" ShowMessageBox="false" CssClass="expbox" ShowSummary="true" /><asp:Label ID="lblExceptionSave" runat="server" ForeColor="Red"></asp:Label><br /><br />
<asp:Button ValidationGroup="Architecture"  ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" CssClass="btnsmall"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</center><br /><br /></asp:Panel></center>
<asp:Panel ID="Panel2" runat="server" Height="420px" Visible="False"/>
<br /><br /><br />
</div><br />
</asp:Content>