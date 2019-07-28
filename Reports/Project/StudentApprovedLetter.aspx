<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="StudentApprovedLetter.aspx.cs" Inherits="Reports_Project_StudentApprovedLetter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Approved Letter to Students
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">
<h2>Project Approved Letter to Students</h2>
<br />
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
     <div ID="dates" runat="server" style="text-align:center;">
         <asp:TextBox ID="txtDate1" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
         <dev:CalendarExtender ID="devdage" runat="server" Format="dd/MM/yyyy" 
             PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtDate1">
         </dev:CalendarExtender>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
             controltovalidate="txtDate1" Display="Dynamic" errormessage="Insert Date " 
             ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
         <img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />
         &nbsp;TO
         <asp:TextBox ID="txtDate2" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
         <dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
             PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDate2">
         </dev:CalendarExtender>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
             controltovalidate="txtDate2" Display="Dynamic" errormessage="Insert Date " 
             ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
         <img src="../../images/cal.png" id="cald" runat="server"  alt="Cald" />
         <br />
&nbsp; <asp:Button ID="btnView" runat="server" Text="View" 
                CssClass="btnsmall" onclick="btnView_Click" />
 </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
<CR:CrystalReportViewer ID="StuApprovedLetter_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="StudentApprovedLetterCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>