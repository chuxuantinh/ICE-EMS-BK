<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="DDDateRpt.aspx.cs" Inherits="Reports_AC_DDDateRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">DD Report Via Date/Diary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updatepanel" runat="server" >
<Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers>
<ContentTemplate>
<center><h2>DD/Cheque Detail of Institutional Members</h2><asp:RadioButton ID="rbtnDate" runat="server" Text="Date" Visible="false" AutoPostBack="true" GroupName="A" OnCheckedChanged="rbtnDate_CheckChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnDiaryNo" runat="server" Text="Diary No" AutoPostBack="true" GroupName="A" OnCheckedChanged="rbtnDiary_CheckChnaged" Visible="false" /></center>
<div id="dates" runat="server" style="text-align:center;">
<asp:TextBox Width="100px" ID="txtDate1" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate1" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate1"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />           
   &nbsp;TO  <asp:TextBox Width="100px" ID="txtDate2" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtDate2" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="cald" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate2"></dev:CalendarExtender><img src="../../images/cal.png" id="cald" runat="server"  alt="Cald" />
 <br /><asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View" />
</div></center>
<br />
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><hr />
 </ContentTemplate></asp:UpdatePanel>
 <div style="width:100%; overflow: scroll; margin-left:5px">
 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true"
        ToolPanelView="None" HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" DisplayToolbar="True"
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
    oninit="CrystalReportViewer1_Init" ShowAllPageIds="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ACDDDate.rpt">
            </Report>
        </CR:CrystalReportSource>
        </div>
</asp:Content>

