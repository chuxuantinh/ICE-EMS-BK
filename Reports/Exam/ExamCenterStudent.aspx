<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Exam/Exam.master" AutoEventWireup="true" CodeFile="ExamCenterStudent.aspx.cs" Inherits="Reports_Exam_ExamCenterStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Exam Center Student Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
    <h2> Student Count of Exam Center</h2>
<div align="center">

<br />
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<asp:Panel ID="PanSession" runat="server">
Session:<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
      Year:<asp:TextBox ID="txtSession" runat="server"  CssClass="txtbox" Width="95px"></asp:TextBox>
      </asp:Panel>
      </ContentTemplate></asp:UpdatePanel>
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
              onclick="btnView_Click" />
                    <br />
                    </div>
<%--  <div style="width:100%">--%>
  <CR:CrystalReportViewer ID="ExamCenter_Student_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ExamCenterStudentCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
        </asp:Content>