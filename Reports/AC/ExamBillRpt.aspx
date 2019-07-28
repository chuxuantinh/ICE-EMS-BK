<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="ExamBillRpt.aspx.cs" Inherits="Reports_AC_ExamBillRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Exam Bill Report
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">
<h2>Exam Bill Report</h2>

<asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /> </Triggers><ContentTemplate>
<table><tr><td colspan="3" align="center">
<center>
    <asp:RadioButtonList ID="rbtnlstSelect" runat="server" 
        AutoPostBack="true" RepeatDirection="Horizontal" Width="47%"
        onselectedindexchanged="rbtnlstSelect_SelectedIndexChanged" >
        <asp:ListItem Text="Session" Value="Session"></asp:ListItem>
        <asp:ListItem Value="Bill Type" Text="Bill Type" />
    </asp:RadioButtonList>
    <br />
    </center>
    </td></tr>
<tr><td>

<center id="pnlSession" runat="server">&nbsp;Session:&nbsp;&nbsp;
    <asp:DropDownList ID="ddlsession" runat="server" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" Width="70px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></center>
  <br />
    
 
   
   
    
 
   
 
    <center ID="pnlBillType" runat="server">
        Bill Type: &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlBillingType" runat="server" 
            AutoPostBack="true" CssClass="txtbox" Width="200px">
            <asp:ListItem Text="Paper Setter Fees" Value="PaperSetter" />
            <asp:ListItem Text="Examination Center Fees" Value="ExamCenter" />
            <asp:ListItem Text="Invigilator Fees" Value="Invigilator" />
            <asp:ListItem Text="Paper &amp; Documents Prining" Value="Documents" />
            <asp:ListItem Text="Other Fees/Charges" Value="Other" />
        </asp:DropDownList>
        &nbsp;
    </center>
    
 
   
 

    
 
   
 
   </td></tr>
 <tr><td colspan="3">
<div align="center"><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick"  /></div></td></tr></table>
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label><hr /><br /></ContentTemplate></asp:UpdatePanel>
</div>
<div style="width:100%; overflow: scroll; margin-left:5px">
 <CR:CrystalReportViewer ID="ExamBill_Report" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" oninit="ExamBill_Report_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ExamBillCrt.rpt"></Report>
        </CR:CrystalReportSource>
        </div>
</asp:Content>




