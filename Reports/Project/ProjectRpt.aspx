<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="ProjectRpt.aspx.cs" Inherits="Reports_Project_ProjectRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Submission Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>

<div align="center">
<h2> Project Submission Report</h2>
<br />
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<asp:Panel ID="PanSession" runat="server">
Session:<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      Year:<asp:TextBox ID="txtSession" runat="server"  CssClass="txtbox" Width="95px"></asp:TextBox>
      </asp:Panel>

  <table>
  <tr align="center"><td>&nbsp;&nbsp;&nbsp;&nbsp; Select:&nbsp;
      <asp:DropDownList ID="ddlProject" 
          CssClass="txtbox" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlProject_SelectedIndexChanged" Width="150px">
            <asp:ListItem>Status</asp:ListItem>
            <asp:ListItem>Student ID</asp:ListItem>
            <asp:ListItem>Group ID</asp:ListItem>
        </asp:DropDownList></td><td ><asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="175px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem Selected="True">ICE</asp:ListItem>
            <asp:ListItem>IMID</asp:ListItem>
        </asp:RadioButtonList></td></tr>
        </table><table>
        <tr><td  align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddlOrder" runat="server" CssClass="txtbox"
            onselectedindexchanged="ddlOrder_SelectedIndexChanged">
            <asp:ListItem Value="NotApproved">Not Approved</asp:ListItem>
            <asp:ListItem Value="CopiesSubmitted">Copies Submitted</asp:ListItem>
            <asp:ListItem>Evaluated</asp:ListItem>
        </asp:DropDownList>
            <asp:Label ID="lblMembershipID" runat="server" Text="Student ID:"></asp:Label>
            <asp:TextBox ID="txtMembershipID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
    </td>
    <asp:Panel ID="panIMID" runat="server" >
        <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IMID:&nbsp;<asp:TextBox ID="txtIMID" CssClass="txtbox" runat="server" Width="95px"></asp:TextBox> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        </td>
        </asp:Panel>
       <td align="center"> <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
         <asp:TextBox ID="txtDuration" runat="server" Width="95px"></asp:TextBox>
           &nbsp;</td></tr>
    </table>
      </ContentTemplate></asp:UpdatePanel>
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
              onclick="btnView_Click" />
                    </div>
                   
                
  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Project_Submission_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
          oninit="Project_Submission_Report_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ProjectCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
 
</asp:Content>

