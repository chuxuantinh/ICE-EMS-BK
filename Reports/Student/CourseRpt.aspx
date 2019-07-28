<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="CourseRpt.aspx.cs" Inherits="Reports_Student_CourseRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
     <asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >Course Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
 <div align="center">
 <h2>Student Admission Details</h2>
 <asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table><tr><td>Course&nbsp;
    <asp:DropDownList ID="ddlCourse" runat="server" Height="22px" CssClass="txtbox"
          Width="151px" >
           <asp:ListItem>ALL</asp:ListItem>
        <asp:ListItem>Civil</asp:ListItem>
        <asp:ListItem>Architecture</asp:ListItem>
    </asp:DropDownList>
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Part&nbsp;
     <asp:DropDownList ID="ddlPart" runat="server" Height="22px" Width="80px" CssClass="txtbox">
        <asp:ListItem Value="PartI">PartI</asp:ListItem>
        <asp:ListItem Value="PartII">PartII</asp:ListItem>
        <asp:ListItem Value="SectionA">SectionA</asp:ListItem>
       <asp:ListItem Value="SectionB">SectionB</asp:ListItem>
    </asp:DropDownList>
  
   
      &nbsp;&nbsp;</td>
    
        <td>
            &nbsp;</td></tr>
            <tr><td colspan="4"><br /></td></tr>
          <tr><td colspan="4" align="left">Session
        <asp:DropDownList ID="ddlSession" CssClass="txtbox" runat="server">
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
              </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Year
        <asp:TextBox ID="txtYear" runat="server" width="80px" CssClass="txtbox"></asp:TextBox>
        &nbsp;<asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" />
              </td> </tr>
         
    </table>
    </ContentTemplate></asp:UpdatePanel>

    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Course_Report" 
                  runat="server" Width="100%" ToolPanelView="None"
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
          oninit="Course_Report_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="CourseCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

