<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="InstituteReRpt.aspx.cs" Inherits="Reports_Project_InstituteReRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Institutes Regisrtation Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
<div align="center">
<h2> Institutes Registration Report</h2>
<br />
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table>
  <tr align="center"><td>&nbsp;&nbsp;&nbsp;&nbsp; Select:&nbsp;
     
       <td><asp:RadioButtonList ID="rblICE" runat="server" 
              RepeatDirection="Horizontal" Width="175px" >
            <asp:ListItem Selected="True">Diploma</asp:ListItem>
            <asp:ListItem>Degree</asp:ListItem>
        </asp:RadioButtonList></td></tr>
        </table><table>
    </table>
      </ContentTemplate></asp:UpdatePanel>
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
              onclick="btnView_Click" />
                    </div>
  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Institutes_Regisrtation_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="InstituteRe.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>