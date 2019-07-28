<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="FeeStatusRpt.aspx.cs" Inherits="Reports_AC_FeeStatusRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Application Form Status Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<h2>&nbsp;&nbsp;&nbsp; Application Form Status Report</h2>
<div id="dates" runat="server" style="text-align:center;">Sesssion:&nbsp;
               <asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" 
                   CssClass="txtbox" OnTextChanged="ddlExamSeason_SelectedIndexChanged" 
                   Width="200px" 
        onselectedindexchanged="ddlExamSeason_SelectedIndexChanged">
                   <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                   <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
               </asp:DropDownList><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
          &nbsp;Year:&nbsp;&nbsp;
               <asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true" 
                   CssClass="txtbox"  Width="100px"></asp:TextBox>
                 
    <br />
    Fee Type:
   <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="true" CssClass="txtbox" Width="250px" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged">

<asp:ListItem Value="Admission" Text="Admission Application Forms " />
<asp:ListItem Value="Exam" Text="Examination Application Forms" />
<asp:ListItem Value="Membership" Text="Membership Application Forms" Enabled="False" />
<asp:ListItem Value="ITI" Text="ITI Application Forms" />
<asp:ListItem Value="Composite" Text="Student Composite Fees" />
<asp:ListItem Value="Subscription" Text="Student Annual Subscription" />
<asp:ListItem Value="ExamCenter" Text="Change of Exam Center & Re-Checking" />
<asp:ListItem Value="FinalPass" Text="Final Marksheet" />
<asp:ListItem Value="Certificate" Text="Provisional Certificate" />
<asp:ListItem Value="Duplicate" Text="Duplicate Documents " />
<asp:ListItem Value="OldSet" Text="Old Question Papers" />
<asp:ListItem Value="Project" Text="Project" />
<asp:ListItem Value="CAD">Auto-CAD</asp:ListItem>
<asp:ListItem Value="ReAdmission" Text="New Membership" />
<asp:ListItem  Value="Other" Text="Other Charges...." />
</asp:DropDownList>
    <br />
    <br />
     Select:
    <asp:DropDownList ID="ddlSelect" Width="95px" runat="server" CssClass="txtbox" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
        AutoPostBack="True">
        <asp:ListItem Value="All" Text="All" />
        <asp:ListItem Value="IMID" 
            Text="IMID" /><asp:ListItem Value="SID" Text="SID" />
    </asp:DropDownList>
      <br /><br />
    <asp:Label ID="lblIMID" runat="server">Membership ID</asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox>
    <br />

    <asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View" />
<br />
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><hr />

</div>
</ContentTemplate></asp:UpdatePanel>
 <CR:CrystalReportViewer ID="FeeStatusForm" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%"  BestFitPage="True" DisplayPage="true"  
        DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" BorderStyle="None" 
        oninit="FeeStatusForm_Init" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="FeeStatusCrt.rpt"></Report>
        </CR:CrystalReportSource>
</asp:Content>