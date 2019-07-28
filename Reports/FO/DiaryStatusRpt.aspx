<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="DiaryStatusRpt.aspx.cs" Inherits="Reports_FO_DiaryStatusRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Diary Status Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">
 <asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<table><tr><td colspan="4" align="center"><h2>Diary Status</h2></td></tr><tr><td>Status
    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" Width="95px"
        onselectedindexchanged="ddlStatus_SelectedIndexChanged" >
        <asp:ListItem>Blocked</asp:ListItem>
        <asp:ListItem>Open</asp:ListItem>
        <asp:ListItem>NotOpen</asp:ListItem>
    </asp:DropDownList>
    
    </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session
        <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="True">
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
              </asp:DropDownList>
        </td>
    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Year
        <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox></td>
        <td>
        <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" CssClass="btnsmall"
                style="height: 26px" />
          </td></tr>
           </table>
           </ContentTemplate>
           </asp:UpdatePanel>
           </div>
          <br />
<div style="overflow: scroll; width:100%"><cr:crystalreportviewer ID="DiaryStatus" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
            <Report FileName="DiaryStatusCrt.rpt">
            </Report>
        </cr:crystalreportsource>
</div>
</asp:Content>

