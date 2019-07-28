<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="MainACRpt.aspx.cs" Inherits="Reports_AC_MainACRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Account:DD Entry Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<center><asp:RadioButtonList ID="rbtnlstSelect" runat="server" 
        AutoPostBack="true" RepeatDirection="Horizontal" Width="50%"
        onselectedindexchanged="rbtnlstSelect_SelectedIndexChanged" ><asp:ListItem Text="IMID" Value="IMID"></asp:ListItem><asp:ListItem Value="Diary" Text="Diary" /></asp:RadioButtonList></center>
<asp:UpdatePanel ID="updatePanel1" runat="server" >
<Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers>
<ContentTemplate>
<center id="pnlDiary" runat="server">Diary No:&nbsp;<asp:TextBox ID="txtDiary" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;</center>
<center id="pnlIM" runat="server">IMID: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;Session:&nbsp;&nbsp;<td><asp:DropDownList ID="ddlsession" runat="server" CssClass="txtbox" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" Width="70px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></center><center>
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label>
<asp:DropDownList ID="ddlAmtForMs" runat="server" CssClass="txtbox"
            DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Name" 
            Width="150px" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Amount') ORDER BY Name">
        </asp:SqlDataSource>
<br /><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick" Font-Size="15px" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPrint" runat="server" CssClass="btnsmall" Text="Print" OnClick="btnPrint_click" /><hr /></center><br /></ContentTemplate></asp:UpdatePanel>
 <CR:CrystalReportViewer ID="MainAc_Report" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%"   HasRefreshButton="True"
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasToggleGroupTreeButton="false" 
        BorderStyle="None" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="MainACCrt.rpt"></Report>
        </CR:CrystalReportSource>
</asp:Content>

