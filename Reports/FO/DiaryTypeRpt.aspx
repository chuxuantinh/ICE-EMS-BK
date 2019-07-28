<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="DiaryTypeRpt.aspx.cs" Inherits="Reports_FO_DiaryTypeRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Diary Type Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div align="center">
    <h2>Diary Details</h2>
    <asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>

  <table><tr><td colspan="4" align="center">
        <asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="267px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem Value="Date">Date</asp:ListItem>
            <asp:ListItem Value="Diary">Diary No</asp:ListItem>
        </asp:RadioButtonList>
    </td></tr>
    </table>
    <center id="pnlDate" runat="server" > <asp:TextBox ID="txtDate" runat="server" CssClass="txtbox" Width="80"></asp:TextBox><dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDate">
</dev:CalendarExtender>&nbsp;&nbsp;&nbsp;&nbsp;<img ID="cald" runat="server" alt="Cald" src="../../images/cal.png" /> &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" />
    &nbsp; </center>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="DiaryTypeReport" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
                  oninit="CrystalReportViewer1_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="DiaryTypeCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

