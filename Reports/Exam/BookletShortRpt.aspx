<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Exam/Exam.master" AutoEventWireup="true" CodeFile="BookletShortRpt.aspx.cs" Inherits="Reports_Exam_BookletShortRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content4" ContentPlaceHolderID="title" Runat="Server">Booklet Range Exam Center
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
<div id="reporttitle"><b> Booklet Range ICE(I)</b> </div>
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
  Center Code:&nbsp;<asp:TextBox ID="txtExamCenter" runat="server" width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;  Exam Type:&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlType" ><asp:ListItem Value="Home" Text="Home"></asp:ListItem><asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem></asp:DropDownList> 
    &nbsp;&nbsp;&nbsp;   <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" /><br />
                    </div>
<%--  <div style="width:100%">--%>
  <CR:CrystalReportViewer ID="Booklet_Range_Short" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="BookletRangeShortCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>