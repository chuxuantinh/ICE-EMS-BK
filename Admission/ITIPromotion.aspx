<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ITIPromotion.aspx.cs" Inherits="Admission_ITIPromotion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">ITI Promotion
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="ITI Application" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"><asp:Label ID="lblEnrolment" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;</h1><h1>ITI Application</h1></div><br />

<center>Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged1" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged" ></asp:TextBox><asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
<table class="tbl"><tr><td>Membership No:</td><td><asp:TextBox ID="txtMembership" 
        runat="server" CssClass="txtbox" ontextchanged="txtMembership_TextChanged" 
        AutoPostBack="True"></asp:TextBox></td></tr></table><asp:Panel ID="pnlInfo" runat="server"><table class="tbl"
<tr><td>Name:</td><td><asp:Label ID="lblName" runat="server" ForeColor="Maroon"></asp:Label></td><td>Father Name:</td><td><asp:Label ID="lblFatherName" runat="server"  ForeColor="Maroon"></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:Label ID="lblCourse" runat="server" ForeColor="Maroon"></asp:Label><asp:Label ID="lblPart" runat="server" ForeColor="Maroon"></asp:Label></td><td>ITI Form Status:</td><td><asp:Label ID="lblStatus" runat="server" ForeColor="Maroon"></asp:Label></td></tr>

<tr><td>Current Course:</td><td><asp:DropDownList ID="ddlCourse" runat="server"  
        CssClass="txtbox" AutoPostBack="True" Enabled="False">
    <asp:ListItem Value="Civil ">Civil Engineering</asp:ListItem>
    <asp:ListItem Value="Architecture">Architecture Engineering</asp:ListItem>
    </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox">
        <asp:ListItem>PartII</asp:ListItem>
    </asp:DropDownList></td></tr>
    <tr><td>&nbsp;</td><td><asp:Button ID="btnPromote" CssClass="btnsmall" 
            Text="Promote" runat="server" onclick="btnPromote_Click" /></td></tr>
  </asp:Panel>  </table></center>
</ContentTemplate></asp:UpdatePanel>
<div style="height:300px;"></div>
</div>
</asp:Content>

