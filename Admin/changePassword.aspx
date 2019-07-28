<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdministrator.master" AutoEventWireup="true" CodeFile="changePassword.aspx.cs" Inherits="Admin_changePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Change Your Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="rightpanel2">
<asp:Panel ID="panelupdate" runat="server" CssClass="panel">
<div class="paneldivupdate"><h1>Change Your Password</h1>
<center><asp:Label ID="lblselectNote" runat="server" Font-Bold="true"></asp:Label></center>
<br />
<center>
<table><tr><td >Old Password:</td><td><asp:TextBox ID="txtChangePassword" TextMode="Password" runat="server" CssClass="txtbox" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPasswordUp" Display="Dynamic" ValidationGroup="up" ErrorMessage="Please Enter old Password!" SetFocusOnError="true">*</asp:RequiredFieldValidator></td></tr> 
<tr><td>New Password:</td><td><asp:TextBox ID="txtPasswordUp"  TextMode="Password" runat="server" CssClass="txtbox" />&nbsp;<asp:RequiredFieldValidator ID="reqpass" runat="server" ControlToValidate="txtPasswordUp" Display="Dynamic" ValidationGroup="up" ErrorMessage="Please Enter new Password!" SetFocusOnError="true">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Confirm Password:</td><td><asp:TextBox ID="txtConfirmPassUp" TextMode="Password" runat="server" CssClass="txtbox"/>&nbsp;<asp:RequiredFieldValidator ID="reqPassConfig" runat="server" Display="Dynamic" ValidationGroup="up" ControlToValidate="txtConfirmPassUp" ErrorMessage="Please Confirm Password!" SetFocusOnError="true">*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPasswordUp" ControlToValidate="txtConfirmPassUp" Display="Dynamic" ErrorMessage="Please Enter Same Password!" Operator="Equal" SetFocusOnError="true" ValidationGroup="up">*</asp:CompareValidator>
<br /> </td></tr>
</table>
</center>
<br /><asp:ValidationSummary ID="validation" runat="server" DisplayMode="BulletList" ValidationGroup="up" CssClass="expbox" />
<script type="text/javascript" language="javascript">
    function ConfirmApp() {
        if (confirm("Are you sure you want to Change your Password?") == true)
            return true;
        else
            return false;
    }
</script>
<center><asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="up" CssClass="btnsmall" OnClientClick="return ConfirmApp();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClearUp" Text="Clear" OnClick="btnClearUp_Click" runat="server" CssClass="btnsmall" /></center>
<br /> 
<center><asp:Label ID="lblselecttext" runat="server" Font-Bold="true"></asp:Label></center>
<br />
 </div>
</asp:Panel>
</div>
</asp:Content>

