<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="MemberFees.aspx.cs" Inherits="Reports_AC_MemberFees" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
 Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Member's Fee Detail Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">
<h2>Member's Fee Detail Report</h2>
<asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /> </Triggers><ContentTemplate>
<br />

  <div style="margin-left:95px;">
    <asp:RadioButtonList ID="rbtnlstSelect" runat="server" 
        AutoPostBack="true" RepeatDirection="Horizontal" Width="35%"
        onselectedindexchanged="rbtnlstSelect_SelectedIndexChanged" ><asp:ListItem Text="IMID" Value="IMID" Selected="True"></asp:ListItem>
    <asp:ListItem>Group ID</asp:ListItem>
    </asp:RadioButtonList>
    </div>
   
  

   
    <br />
<center id="pnlIM" runat="server" > <asp:Label ID="lblIMID" Text=" Membership ID:" runat="server"></asp:Label><asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
    &nbsp;&nbsp;</center>
    
    <br />
<div align="center"><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick"  /></div>
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label><hr /><br /></ContentTemplate></asp:UpdatePanel>
</div>

 <CR:CrystalReportViewer ID="MemberFees_Report" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None"  />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="AccountCrt.rpt"></Report>
        </CR:CrystalReportSource>


</asp:Content>

