<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="CounsellingRpt.aspx.cs" Inherits="Reports_FO_CounsellingRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Front Office:Courier Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
<link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="pnlViewbtn" style="float:right; margin-right:30px;"></div>
<asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<center><h2>Front Office:Counselling Reports</h2><br /><br />
    <asp:RadioButtonList ID="rbtnlstSelect" runat="server" AutoPostBack="true" 
        RepeatDirection="Horizontal" Width="62%"  
        onselectedindexchanged="rbtnlstSelect_SelectedIndexChanged" ><asp:ListItem Text="Status" Value="Status"></asp:ListItem><asp:ListItem Value="Response" Text="Response" /><asp:ListItem Value="Student" Text="Student Name" /></asp:RadioButtonList></center>
<center id="pnlCourier" runat="server">Name of Student:&nbsp;<asp:TextBox ID="txtSName" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;</center>
<center id="pnlDate" runat="server" >Select:&nbsp; <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" Width="80px"><asp:ListItem Value="Running" Text="Running" /><asp:ListItem Value="Converted" Text="Converted" /><asp:ListItem Value="NotConverted" Text="Not Converted" /></asp:DropDownList>            <asp:DropDownList ID="ddlResponse" runat="server" Width="90px" AutoPostBack="true" OnSelectedIndexChanged="ddlResponse_SeelctedIndexChanged" CssClass="txtbox" ><asp:ListItem Value="Hot" Text="Hot" /><asp:ListItem Value="Cold" Text="Cold" /><asp:ListItem Value="Warm" Text="Warm" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp; Date:&nbsp;&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" CssClass="txtbox" Width="80"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDateFrom"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />
&nbsp;&nbsp;TO&nbsp;&nbsp; <asp:TextBox ID="txtDateto" runat="server" CssClass="txtbox" Width="80"></asp:TextBox><dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDateto">
</dev:CalendarExtender><img ID="cald" runat="server" alt="Cald" src="../../images/cal.png" /> </center>
<br />
<center>
<asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View"  />
</center>
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><hr /><br /></ContentTemplate></asp:UpdatePanel>
<CR:CrystalReportViewer ID="CounsellingReport" runat="server"  DisplayPage="true" ToolPanelView="None" DisplayToolbar="True" DisplayStatusbar="true" AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" Width="901px" />
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
<Report FileName="CounsellingCrt.rpt"></Report>
</CR:CrystalReportSource>
</asp:Content>

