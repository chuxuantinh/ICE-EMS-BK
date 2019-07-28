<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="ITILetters.aspx.cs" Inherits="Reports_Student_ITILetters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
     <asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >ITI Examination Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
 <div align="center">
 <h2>ITI Examination</h2>
 <asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table>
     
          <tr ><td colspan="2" align="center">Exam Date: 
              <asp:DropDownList ID="ddlCoursrID"  dataTextFormatString="{0:dd MMM yyyy}" runat="server" CssClass="txtbox">
              </asp:DropDownList>
              &nbsp;</td></tr>
                      <tr><td colspan="2">Time&nbsp; From: <asp:TextBox ID="txtfrom" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>&nbsp;<asp:DropDownList ID="ddlFrom" runat="server" CssClass="txtbox">
                          <asp:ListItem>AM</asp:ListItem>
                          <asp:ListItem>PM</asp:ListItem>
              </asp:DropDownList>&nbsp;To <asp:TextBox ID="txtTo" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>&nbsp;<asp:DropDownList ID="ddlTo" runat="server" CssClass="txtbox">
              <asp:ListItem>AM</asp:ListItem>
                          <asp:ListItem>PM</asp:ListItem>
              </asp:DropDownList></td><td><asp:Button ID="btnView" runat="server" CssClass="btnsmall" 
                      onclick="btnView_Click" Text="View" /></td></tr>
              
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="ITI_Letters_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ITILettersCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>


