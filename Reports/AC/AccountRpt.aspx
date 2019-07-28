<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="AccountRpt.aspx.cs" Inherits="Reports_AC_AccountRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <asp:Content ID="Content3" ContentPlaceHolderID="title" Runat="Server">Account Details Report
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">
<h2>Account Details Report</h2>
<asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /> </Triggers><ContentTemplate>
<center>
    <asp:RadioButtonList ID="rbtnlstSelect" runat="server" 
        AutoPostBack="true" RepeatDirection="Horizontal" Width="35%"
        onselectedindexchanged="rbtnlstSelect_SelectedIndexChanged" ><asp:ListItem Text="IMID" Value="IMID"></asp:ListItem><asp:ListItem Value="Diary" Text="Diary" />
    <asp:ListItem>Group ID</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    </center>
<center id="pnlDiary" runat="server">Diary No:&nbsp;<asp:TextBox ID="txtDiary" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>&nbsp;&nbsp;</center>
<center id="pnlIM" runat="server" >  Membership ID:<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
    &nbsp;&nbsp;Session:<asp:DropDownList ID="ddlsession" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddldevExamSeason_SelectedIndexChanged">
        <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
        <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;Year:<asp:TextBox ID="txtSession" runat="server" AutoPostBack="true" 
        CssClass="txtbox" OnTextChanged="txtdevYearSeason_TextChanged" Width="95px"></asp:TextBox>
    </center>
<div align="center"><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick"  /></div>
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label><hr /><br /></ContentTemplate></asp:UpdatePanel>
</div>
<div style="width:100%; overflow: scroll; margin-left:5px">
 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="AccountCrt.rpt"></Report>
        </CR:CrystalReportSource>
        </div>
</asp:Content>

