<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="VisitorsRpt.aspx.cs" Inherits="Reports_FO_VisitorsRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Front Office Visitors: Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>

 <asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>

<center id="pnlDate" runat="server" ><h2>Front Office Visitors: Report</h2><br />Date:&nbsp;&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" CssClass="txtbox" Width="80"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDateFrom"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />
&nbsp;&nbsp;TO&nbsp;&nbsp; <asp:TextBox ID="txtDateto" runat="server" CssClass="txtbox" Width="80"></asp:TextBox><dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDateto">
                                            </dev:CalendarExtender><img ID="cald" runat="server" alt="Cald" src="../../images/cal.png" />
  <br /><br /> <asp:Label ID="lblExceptioN" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label></center>
  <div><center><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick" /></center></div>
  </ContentTemplate></asp:UpdatePanel><hr /><br />   
  
 <CR:CrystalReportViewer ID="VisitorReport" runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true" DisplayToolbar="true"  
        DisplayStatusbar="true" EnableTheming="false"
        ToolPanelView="None"  
        BorderStyle="None"
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" oninit="CrystalReportViewer1_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="VisitorsCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>


