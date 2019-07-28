<%@ Page Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="InstitutionRegi.aspx.cs" Inherits="project_InstitutionRegi" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Institution Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"/></td><td>
<asp:Label ID="lblNext" runat="server" Text="Institute Registration" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnl" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>Registration of AICTE Approved Institution</h1></div><br />
<asp:Panel ID="panelRight" runat="server" ><asp:Label ID="lblSessionHiddend" runat="server" Visible="false"/><asp:Label ID="lblSession" runat="server" Visible="false"/>
<center>
<table class="tbl"><tr><td align="left">Institution</td><td>:</td>
<td align="left" colspan="4"><asp:TextBox ID="txtName" runat="server" CssClass="txtbox" Width="290px" ForeColor="#333300" Font-Bold="true"/><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Please Insert Institution Name" ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td>
<th align="left">&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="IM code :" Visible="false" />&nbsp;<asp:Label ID="lblcode" runat="server" ForeColor="Maroon" /></th></tr>
<tr><td align="left">Address</td><td>:</td>
<td colspan="5" align="left"><asp:TextBox ID="txtPAddress" runat="server" CssClass="txtbox" Width="290px"/><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td colspan="2"></td><td colspan="5" align="left"><asp:TextBox ID="txtAddressHead2" runat="server" CssClass="txtbox" Width="290px"/></td></tr>
<tr><td colspan="2"></td>
<td colspan="3" align="left">State:<br /><asp:DropDownList ID="ddlState" runat="server" CssClass="txtbox" Width="160px"/></td>
<td>City:<br /> 
<asp:TextBox ID="txtCity" runat="server" CssClass="txtbox"/>&nbsp;&nbsp;&nbsp;</td>
<td>PinCode:<br /> <asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td align="left">Phone</td><td>:</td>
<td colspan="5" align="left"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"/>-<asp:TextBox ID="txtPhoneNo" Width="103px" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td align="left">Mobile</td><td>:</td>
<td colspan="3" align="left"><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox" Width="160px"/><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
<td colspan="2" align="left">Email:&nbsp;<asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox" Width="150px"/><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>
<tr><td align="left">Reg. Date</td><td>:</td>
<td><asp:TextBox ID="txtreg" runat="server" CssClass="txtbox" Width="160px"/>
<dev:CalendarExtender ID="devdage" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtreg"></dev:CalendarExtender></td>
<td><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td>
<td><asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" controltovalidate="txtreg" Display="Dynamic" errormessage="Insert Registration Date" ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td>
</tr></table>
<div style="width:70%; margin:10px; margin-top:0px;">
<fieldset><legend><font style="color:#B21235; font-size:13px; font-family:Verdana;">Session:-</font></legend>
<table class="tbl"><tr><td align="center" colspan="5"><b>-Start Session-</b></td><td align="center"><b>-Expire Session-</b></td></tr>
<tr><td align="center" colspan="5"><asp:DropDownList ID="ddlsession" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" Width="180px">
<asp:ListItem Text="Summer Examination" Value="Sum" /><asp:ListItem Text="Winter Examination" Value="Win" /></asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="txtdevYearSeason_TextChanged" Width="50px" /></td>
<td align="center"><asp:DropDownList ID="ddlsession3" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon" Width="180px" OnTextChanged="ddlsession3_SelectedIndexChanged"><asp:ListItem Text="Summer Examination" Value="Sum" /><asp:ListItem Text="Winter Examination" Value="Win" /></asp:DropDownList>&nbsp;Year: <asp:TextBox ID="txtSession3" runat="server" AutoPostBack="true" CssClass="txtbox" Width="50px" ontextchanged="txtSession3_TextChanged" /></td></tr></table></fieldset>
</div><br />
<b>=> Check the courses for Approved Institutions</b><br />
<asp:CheckBox ID="chkCPartII" runat="server" Text="Civil Part II" /><asp:CheckBox ID="chkCSectionB" runat="server" Text="Civil Section B" /><asp:CheckBox ID="chkAPartII" runat="server" Text="Architecture Part II" /><asp:CheckBox ID="chkASectionB" runat="server" Text="Architecture Section B" />
</center><br /><center><asp:ValidationSummary ID="validatesummmary" ValidationGroup="Architecture"  runat="server" ShowMessageBox="false" CssClass="expbox" ShowSummary="true" /><asp:Label ID="lblExceptionSave" runat="server" ForeColor="Red"></asp:Label><br /><br /><asp:Button ValidationGroup="Architecture"  ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</center><br />
</asp:Panel><asp:Panel ID="pnlspc" runat="server" Height="150px"/>
</ContentTemplate></asp:UpdatePanel>
</div><br />
</asp:Content>