<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="Student.aspx.cs" Inherits="Reports_Student_Student" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
     <asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >Student Profile Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
 <div align="center">
 <h2>Student Profile</h2>
 <asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table><tr><td  align="center">
        <asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="267px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem>ICE</asp:ListItem>
            <asp:ListItem>IMID</asp:ListItem>
            <asp:ListItem Value="SID">Student Membership</asp:ListItem>
        </asp:RadioButtonList>
    </td></tr>
      <asp:Panel ID="Panel1" runat="server">
     
    <tr><td align="center">Session
        <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="True">
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
              </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Year
        <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
       &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblImid" runat="server" Text="IMID"></asp:Label><asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
          </td>
        <td>
        
          </td></tr> </asp:Panel> 
          <tr ><td><asp:Label ID="lblSID" runat="server" Text="Student Membership ID"></asp:Label>
              <asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox></td></tr>
              <tr align="center"><td><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                onclick="btnView_Click" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Stuident_Profile_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
          oninit="Stuident_Profile_Report_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="StudentCrtrpt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

