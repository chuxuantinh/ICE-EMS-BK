<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ChangeFeeLevel.aspx.cs" Inherits="Admission_Chenge_FeeLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Change Fee Level
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Change Fee Level" CssClass="redirecttabhome"></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Change Fee Level </h1></div><br />
<center> Enter Membership No:<asp:TextBox ID="txtMembership" runat="server" CssClass="txtbox"></asp:TextBox>
    <asp:Button ID="btnOk" runat="server" Text="OK" onclick="btnOk_Click"  CssClass="btnsmall"/><asp:Label ID="lblFee" runat="server"></asp:Label><br />
    <br />
Fee Level:<asp:DropDownList ID="ddlFeeLevel" runat="server" 
        DataSourceID="SqlDataSource1" DataTextField="FeeLevel" 
        DataValueField="FeeLevel" CssClass="txtbox"></asp:DropDownList><br />
        <br /><asp:Button ID="btnChange" runat="server" Text="Change"  
        CssClass="btnsmall" onclick="btnChange_Click"/>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT DISTINCT [FeeLevel] FROM [FeeMaster]">
    </asp:SqlDataSource>
    </center>
</div>
<div id="space" runat="server" style="height:500px;"></div>

</asp:Content>

