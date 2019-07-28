<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="SubscriptionRpt.aspx.cs" Inherits="Reports_IM_SubscriptionRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
     <asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >Subscription Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="center">
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
 <h2>IM Subscription Report</h2>
  <table><tr><td  align="center">
  <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
      </td></tr>
     
     
    <tr><td align="center">Subscription:
        <asp:DropDownList ID="ddlSubscription" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Subscription Dues">Subscription Dues</asp:ListItem>
              <asp:ListItem Value="Subscription Expired">Subscription Expired</asp:ListItem>
              <asp:ListItem Value="Subscription Required">Subscription Required</asp:ListItem>
              </asp:DropDownList>
        </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date:&nbsp;&nbsp;<asp:TextBox 
                ID="txtDateFrom" runat="server" CssClass="txtbox" Width="80" 
                ontextchanged="txtDateFrom_TextChanged"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDateFrom"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />
            &nbsp;&nbsp;&nbsp;&nbsp; </center></td></tr>
     
       
    
    
    
        <tr align="center"><td colspan="2"><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                onclick="btnView_Click" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Subscription_Details_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="SubscriptionCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

