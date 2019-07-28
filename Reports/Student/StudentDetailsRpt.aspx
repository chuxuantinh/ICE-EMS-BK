<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="StudentDetailsRpt.aspx.cs" Inherits="Reports_Student_StudentDetailsRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >Student Detail Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
 <div align="center">
 <h2>Students Detail</h2>
<div><asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="267px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="Date">Enroll Date</asp:ListItem>
            <asp:ListItem>SID</asp:ListItem>          
            <asp:ListItem>IMID</asp:ListItem>           
        </asp:RadioButtonList><asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
          <asp:DropDownList ID="ddlStatus" runat="server" 
              CssClass="txtbox">
              <asp:ListItem Value="Active">Active</asp:ListItem>
              <asp:ListItem Value="NotApprove">NotApprove</asp:ListItem>
              <asp:ListItem Value="ToBeApprove">ToBeApprove</asp:ListItem>
              <asp:ListItem Value="Pending">Pending</asp:ListItem>
          </asp:DropDownList></div>
        
 <asp:UpdatePanel ID="updpnl" runat="server"><ContentTemplate>
  <table>
      <asp:Panel ID="panSession" runat="server">
     
    <tr><td align="center" colspan="2"><asp:Label ID="lblSID" runat="server" Text="SID:From "></asp:Label>
        
        <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
       
            <asp:Label ID="lblImid" runat="server" Text="IMID"></asp:Label>&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox></td>
        <td>
        
          </td></tr> </asp:Panel> 
           <asp:Panel ID="panDate" runat="server">
          <tr ><td><asp:Label ID="lblDate" runat="server">Date:</asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" CssClass="txtbox" 
                  Width="80"></asp:TextBox>
              <dev:CalendarExtender ID="devdage" runat="server" Format="dd/MM/yyyy" 
                  PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtDateFrom">
              </dev:CalendarExtender>
              <img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />
              &nbsp;&nbsp;TO&nbsp;&nbsp;
              <asp:TextBox ID="txtDateto" runat="server" CssClass="txtbox" Width="80"></asp:TextBox>
              <dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                  PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDateto">
              </dev:CalendarExtender>
              <img ID="cald" runat="server" alt="Cald" src="../../images/cal.png" />
              </td></tr>
              </asp:Panel>
               </table>
              </ContentTemplate>
    </asp:UpdatePanel>
             
        
   
    <center><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                onclick="btnView_Click" /></center>
    
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Student_Details_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
          oninit="Student_Details_Report_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="StudentDetailsCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
  </asp:Content>