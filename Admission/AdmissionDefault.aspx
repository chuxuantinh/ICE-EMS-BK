<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="AdmissionDefault.aspx.cs" Inherits="Admission_AdmissionDefault" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Admission Department: ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div style="float:right; margin-right:50px;">Student Membership No.:&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" Width="100px" runat="server" CssClass="txtbox" />&nbsp;<asp:Button ID="btnViewEnroll" runat="server" Text="View Profile"  OnClick="btnView_Click" CssClass="btnsmall" /></div> 
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Admission" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Admissions&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" /></h1>
</div>
<img src="../images/admissionss.jpg"  height="250px" width="100%" alt="Admission"/><hr />
<asp:Panel ID="pnlSpc" runat="server" Height="360px" />
</div>
</asp:Content>