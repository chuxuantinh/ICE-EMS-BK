<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="D2DRpt.aspx.cs" Inherits="Reports_FO_D2DRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" Runat="Server">Diary Submitted to Department
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
<link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<center><h2>Diary Submitted to Department</h2><br /><asp:RadioButtonList ID="rbtnlstSelect" runat="server" 
        AutoPostBack="true" RepeatDirection="Horizontal" Width="70%"
        onselectedindexchanged="rbtnlstSelect_SelectedIndexChanged" ><asp:ListItem Text="ICE" Value="ICE"></asp:ListItem><asp:ListItem Value="IM" Text="IM" /><asp:ListItem Value="Dairy" Text="Dairy" /></asp:RadioButtonList></center>
<center id="pnlDairy" runat="server">Dairy No:&nbsp;<asp:TextBox ID="txtDNo" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;</center>
<center id="pnlDate" runat="server" ><asp:TextBox ID="txtIMID" runat="server" Width="100px" CssClass="txtbox" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp; Date:&nbsp;&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" CssClass="txtbox" Width="80"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDateFrom"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />
&nbsp;&nbsp;TO&nbsp;&nbsp; <asp:TextBox ID="txtDateto" runat="server" CssClass="txtbox" Width="80"></asp:TextBox><dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDateto">
</dev:CalendarExtender><img ID="cald" runat="server" alt="Cald" src="../../images/cal.png" /> </center>
<br />
<center><div ID="pnlViewbtn" style="margin-right:30px; height: 50px;">
<asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View" />
</div></center>
<br />
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><hr /><br /></ContentTemplate></asp:UpdatePanel>
 <CR:CrystalReportViewer ID="DiaryToDepartment" runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true"
        ToolPanelView="None" HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" DisplayToolbar="True"
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="D2DCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>

