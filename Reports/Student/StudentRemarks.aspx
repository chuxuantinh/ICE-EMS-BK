<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="StudentRemarks.aspx.cs" Inherits="Reports_Student_StudentRemarks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
     <asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >Membership Generated Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
 <div align="center">
 <h2>Students Remark</h2>
 <asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table>
     
          <tr ><td colspan="2" align="center">Status: 
              <asp:DropDownList ID="ddlMembershipGtd" runat="server" CssClass="txtbox">
                  <asp:ListItem Value="Remarks">Remarks</asp:ListItem>
                  <asp:ListItem Value="ExmpRemarks">Exmp Remarks</asp:ListItem>
              </asp:DropDownList>
             </td></tr>
              
        
    </table>
    <table>
      <tr><td align="center">Session
        <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="True">
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
              </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Year
        <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
          &nbsp;</td>
       <td>&nbsp;<asp:Button ID="btnView" runat="server" Text="View" 
                      CssClass="btnsmall" onclick="btnView_Click" Height="25px" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Stu_Remarks_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="StuMembershipGtd.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

