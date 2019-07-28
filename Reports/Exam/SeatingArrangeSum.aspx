<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Exam/Exam.master" AutoEventWireup="true" CodeFile="SeatingArrangeSum.aspx.cs" Inherits="Reports_Exam_SeatingArrangeSum" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Seating Arrangemeant Summary Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
    <h2> Seating Arrangement Summary</h2>
<div align="center">
<br />
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<asp:Panel ID="PanSession" runat="server">Exam Date:&nbsp;
<asp:TextBox Width="100px" ID="txtDate1" runat="server" CssClass="txtbox"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" 
        PopupPosition="BottomRight" runat="server" TargetControlID="txtDate1"></dev:CalendarExtender>
    <img src="http://localhost:50376/ICE08/images/cal.png" id="cal" 
    runat="server"  alt="Cal" />
Session:<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="AN">AN</asp:ListItem>
            <asp:ListItem Value="FN">FN</asp:ListItem>
              </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
      Center<asp:TextBox ID="txtCenterCode" runat="server"  CssClass="txtbox" Width="95px"></asp:TextBox>
      </asp:Panel>
      </ContentTemplate></asp:UpdatePanel>
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
              onclick="btnView_Click" />
                    <br />
                    </div>
<%--  <div style="width:100%">--%>
  <CR:CrystalReportViewer ID="SeatingArrange_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="SeatingArragSum.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>

