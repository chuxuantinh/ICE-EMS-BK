<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="AutocadUpload.aspx.cs" Inherits="Admission_Autocad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Uplodad Auto-Cad Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" />
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" CssClass="redirecttab" onclick="lblHomeRedirect_Click" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Upload Auto-Cad Forms" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Upload Auto Cad Forms</h1></div><center>
<asp:RadioButtonList ID="rbtntype" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Font-Bold="true">
<asp:ListItem Value="RegNo" Selected="True">Registration No</asp:ListItem>
<asp:ListItem>Grade</asp:ListItem>
<asp:ListItem Value="Status">Status</asp:ListItem>
</asp:RadioButtonList><br />
<asp:FileUpload ID="fileuploadExcel" runat="server" Width="200px" BorderColor="AliceBlue"  BorderStyle="Groove" BorderWidth="3px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUplode" runat="server" Text="Upload" onclick="btnUplode_Click" CssClass="btnsmall"  />
<br /><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Brown" /><br /><br />
<div  id="Not" style="width: 35%; overflow:scroll; height:200px;"><asp:Label ID="txtNotUploaded" runat="server" ForeColor="Brown"/></div><br />
<h2>* Instructions For uploading Data</h2>
</center><br />
<img src="../images/InstrF.png" alt="Instruction" />


    </div>
</asp:Content>