<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Exam/Exam.master" AutoEventWireup="true" CodeFile="ExportResult.aspx.cs" Inherits="Reports_Exam_ExportResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Export Result
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
   <div id="reporttitle"><b>Export Result: </b> Select Session and Couser-Part of Examination Result and Export it.</div>
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
       </tr>
          <tr> 
         <td>Course:&nbsp;&nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox"><asp:ListItem Value="All" Text="All" /><asp:ListItem Value="Civil" Text="Civil Engineering" ></asp:ListItem><asp:ListItem Value="Architechture" Text="Architecture" /></asp:DropDownList></td><td>&nbsp;&nbsp;&nbsp;Part:&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" ><asp:ListItem Value="PartI" Text="PartI" /><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Value="SectionA" Text="SectionA" /> <asp:ListItem Value="SectionB" Text="SectionB"></asp:ListItem></asp:DropDownList></td>
              </tr>
              <tr><td colspan="3" align="center">
                    <asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" onclick="btnView_Click" /></td></tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
   <div style="overflow: scroll; width:100%">
   <cr:crystalreportviewer ID="Exam_Result" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" 
    ToolPanelView="None" HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
            <Report FileName="ExportResultCrt.rpt">
            </Report>
        </cr:crystalreportsource>
    </div>
</asp:Content>

