<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="SynopsisApprovalRpt.aspx.cs" Inherits="Reports_Project_SynopsisApprovalRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Detail Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>

<div align="center">
<h2> Synopsis Approval Report</h2>
<br />

<center>
<asp:RadioButtonList ID="rbtnlstSelect" runat="server" 
        AutoPostBack="true" RepeatDirection="Horizontal" Width="50%"
        onselectedindexchanged="rbtnlstSelect_SelectedIndexChanged" ><asp:ListItem Text="Date" Value="Date" Selected="True"></asp:ListItem><asp:ListItem Value="Session" Text="Session" /></asp:RadioButtonList></center><br />
        <asp:Panel ID="PanSession" runat="server">
Session:<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      Year:<asp:TextBox ID="txtSession" runat="server"  CssClass="txtbox" Width="95px"></asp:TextBox>
     
   &nbsp;&nbsp;
    Status:<asp:DropDownList ID="ddlStatus" 
          CssClass="txtbox" runat="server" Width="150px">
            <asp:ListItem>ProformaBSubmitted</asp:ListItem>
            <asp:ListItem>ProformaASubmittd</asp:ListItem>
             </asp:DropDownList>
      
      </asp:Panel>
       <asp:Panel ID="panDate" runat="server">
     <div id="dates" runat="server" style="text-align:center;">
Date:<asp:TextBox Width="100px" ID="txtDate1" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate1" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate1"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />           
   &nbsp;TO  <asp:TextBox Width="100px" ID="txtDate2" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtDate2" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="cald" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate2"></dev:CalendarExtender><img src="../../images/cal.png" id="cald" runat="server"  alt="Cald" />
</div>
    </asp:Panel>
         &nbsp;&nbsp;
          <asp:Panel ID="PanStatus" runat="server">Entry Status:<asp:DropDownList ID="ddlEntryStatus" 
          CssClass="txtbox" runat="server" Width="150px">
            <asp:ListItem>ALL</asp:ListItem>
            <asp:ListItem>Running</asp:ListItem>
              <asp:ListItem>OldProject</asp:ListItem></asp:DropDownList>
              </asp:Panel>         
       <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
              onclick="btnView_Click" /> </div>
     
        
                  
  <div style="width:100%">
  <CR:CrystalReportViewer ID="Project_Synopsis_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="SynopsisApprovalCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
        </div>
</asp:Content>
