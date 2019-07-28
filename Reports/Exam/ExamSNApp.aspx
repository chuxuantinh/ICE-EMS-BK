<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Exam/Exam.master" AutoEventWireup="true" CodeFile="ExamSNApp.aspx.cs" Inherits="Reports_Exam_ExamSNApp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Exam Form Serial No
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="reporttitle"><b>Approved Exam Form Serial No: </b> Record of serial no of Exam form generated at account section. Enter Date of Exam Form Submission Date.</div>
    <div align="center">
  <br />
  <asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<table><tr><td>&nbsp;&nbsp; Session
        <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList>
        </td>
    <td>&nbsp;&nbsp; Year
        <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
         </td>
         </td></tr>

    </table>
    <center id="pnlDate" runat="server" >&nbsp;&nbsp;&nbsp;&nbsp;Form Submission Date:&nbsp;&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" CssClass="txtbox" Width="80"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDateFrom"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />
&nbsp;&nbsp;TO&nbsp;&nbsp; <asp:TextBox ID="txtDateto" runat="server" CssClass="txtbox" Width="80"></asp:TextBox><dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDateto">
</dev:CalendarExtender><img ID="cald" runat="server" alt="Cald" src="../../images/cal.png" /> 
<br /><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" onclick="btnView_Click" />
    </center></ContentTemplate>
    </asp:UpdatePanel>
    </div>
   <div style="overflow: scroll; width:100%">
   <cr:crystalreportviewer ID="ExamFormSerialNo" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" 
    ToolPanelView="None" HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
            <Report FileName="ExamSNApp.rpt">
            </Report>
        </cr:crystalreportsource>
    </div>
</asp:Content>



