<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="SubMasterRpt.aspx.cs" Inherits="Reports_Invent_SubMasterRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Course Stock Details Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">

<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>



  <table><tr><td align="center" colspan="3">
      <h2>Course Stock Details</h2>
      </td></tr>
      <tr><td></td></tr><tr><td  align="center">Course
        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox">
               <asp:ListItem Value="Civil" Text="Civil Engineering"></asp:ListItem>
               <asp:ListItem Value="Architecture" Text="Architecture Engineering"></asp:ListItem>
               <asp:ListItem Value="AutoCAD" Text="AutoCAD "></asp:ListItem>
             
    </asp:DropDownList>
    &nbsp;&nbsp;
    </td><td  align="center">Part
        <asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox">
      
               <asp:ListItem Value="PartI" Text="PartI"></asp:ListItem>
               <asp:ListItem Value="PartII" Text="PartII"></asp:ListItem>
               <asp:ListItem Value="SectionA" Text="SectionA"></asp:ListItem>
               <asp:ListItem Value="SectionB" Text="SectionB"></asp:ListItem>
             
      
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;
    </td><td  align="center">Course ID
        <asp:DropDownList ID="ddlCoursrID" runat="server" CssClass="txtbox">
    </asp:DropDownList>
    </td></tr>
     
 <tr align="center"><td colspan="3" align="center"><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                onclick="btnView_Click" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
<CR:CrystalReportViewer ID="Course_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
        oninit="Course_Report_Init"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="SubjectMasterCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>

