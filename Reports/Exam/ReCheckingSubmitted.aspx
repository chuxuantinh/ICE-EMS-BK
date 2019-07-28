<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Exam/Exam.master" AutoEventWireup="true" CodeFile="ReCheckingSubmitted.aspx.cs" Inherits="Reports_Exam_ReChecking" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">ReChecking Report
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
<div id="reporttitle"><b>Re-Checking Form Submitted</b> </div>
<div align="center">
<br />
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<asp:Panel ID="PanSession" runat="server">
Current Session:<asp:DropDownList ID="ddlCurrentSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
      Year:<asp:TextBox ID="txtCurrentSession" runat="server"  CssClass="txtbox" Width="95px"></asp:TextBox>
Examination Session:<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
      Year:<asp:TextBox ID="txtSession" runat="server"  CssClass="txtbox" Width="95px"></asp:TextBox>
      <br />
     <asp:RadioButton ID="rbtnOnlyApproved" runat="server" GroupName="dev" Text="Only Approved Forms" Checked="true" />&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnAllForm" runat="server" Text="All Forms" GroupName="dev" />
      </asp:Panel>
      </ContentTemplate></asp:UpdatePanel>
    &nbsp;&nbsp;&nbsp;   <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" /><br />
                    </div>
<%--  <div style="width:100%">--%>
  <CR:CrystalReportViewer ID="ReChecking_Form_Submitted" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ReCheckingSubmittedCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>





