<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="AllExport.aspx.cs" Inherits="Reports_AC_AllExport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Account Export
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<h2>&nbsp;&nbsp;&nbsp;&nbsp;Account Details Report</h2>
<br />
<div id="dates" runat="server" style="text-align:center;">
<asp:TextBox Width="100px" ID="txtDate1" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate1" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate1"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />           
   &nbsp;TO  <asp:TextBox Width="100px" ID="txtDate2" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtDate2" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="cald" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate2"></dev:CalendarExtender><img src="../../images/cal.png" id="cald" runat="server"  alt="Cald" />
</div>
<br />
<center>Select Type:&nbsp;<asp:DropDownList ID="ddlType" CssClass="txtbox" 
        runat="server" 
        onselectedindexchanged="ddlType_SelectedIndexChanged" ><asp:ListItem Value="Apps" Text="Application Forms" /><asp:ListItem Value="DD" Text="All DD" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnView_Click" Text="View"  ValidationGroup="Architecture"/>
        <asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label></center>
<div style="width:100%; overflow: scroll; margin-left:5px">
<CR:CrystalReportViewer ID="ApplicationForms" runat="server" 
           AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%"  BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" BorderStyle="None"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="AllAppsCrt.rpt"></Report>
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="AccountDD" runat="server" 
           AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource2" 
             Width="100%"  BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" BorderStyle="None"/>
        <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
            <Report FileName="AllDDCrt.rpt"></Report>
        </CR:CrystalReportSource>
        </div>
      
</asp:Content>

