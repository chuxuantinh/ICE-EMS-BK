<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="ApplicationStatusSum.aspx.cs" Inherits="Reports_AC_ApplicationStatusSum" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<script runat="server">

    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Application Status Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<h2>&nbsp;&nbsp;&nbsp;  Total number of Exam Form and Admission Form </h2>
<br />
<div id="dates" runat="server" style="text-align:center;">Sesssion:&nbsp;
               <asp:DropDownList ID="ddlExamSeason" runat="server" 
                   CssClass="txtbox" 
                   Width="180px" >
                   <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                   <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
               </asp:DropDownList><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
          &nbsp;Year:<asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true" 
                   CssClass="txtbox" Width="80px"></asp:TextBox>
    &nbsp;
    Application Status:
    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox"><asp:ListItem Value="Approved" Text="Approved" /><asp:ListItem Value="NotApproved" Text="Not Approved" /><asp:ListItem Value="Hold" Text="Hold" />
    </asp:DropDownList>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br />
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><hr />
 </div>
 
 <CR:CrystalReportViewer ID="ApplicationStatusSum" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%"  BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" BorderStyle="None"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ApplicationStatusSumCrt.rpt"></Report>
        </CR:CrystalReportSource>
          </asp:Content>


