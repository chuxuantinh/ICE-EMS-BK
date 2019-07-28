<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="ITIResult.aspx.cs" Inherits="Reports_Student_ITIResult" %>

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
 <h2>ITI Result</h2>
 <asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table>
     
          <tr ><td>Exam Date: 
              <asp:DropDownList ID="ddlCoursrID"  dataTextFormatString="{0:dd MMM yyyy}" runat="server" CssClass="txtbox">
              </asp:DropDownList>
              &nbsp;</td><td>
              <asp:Button ID="btnView" runat="server" Text="View" 
                      CssClass="btnsmall" onclick="btnView_Click"  /></td></tr>
                      <tr><td colspan="2">&nbsp;</td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="ITI_Result_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ITIResult.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>


