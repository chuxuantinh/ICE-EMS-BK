<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Exam/Exam.master" AutoEventWireup="true" CodeFile="ToBeFillExamFormRpt.aspx.cs" Inherits="Reports_Exam_ToBeFillExamFormRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">ExamForm Approved: To Be Fill
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<div id="reporttitle"><b>Exam Form Details: </b> Details of Exam Form which approved from Account section and not submitted at exam section.</div>
<div id="dates" runat="server" style="text-align:center;">Sesssion:&nbsp;
               <asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" 
                   CssClass="txtbox" OnTextChanged="ddlExamSeason_SelectedIndexChanged" 
                   Width="200px" >
                   <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                   <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
               </asp:DropDownList><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
          &nbsp;Year:&nbsp;&nbsp;
               <asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true"  CssClass="txtbox"  Width="100px"></asp:TextBox>  
    
    &nbsp;&nbsp;
    <asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View" />
                 </div>
</ContentTemplate></asp:UpdatePanel>
<CR:CrystalReportViewer ID="ExamFormForEntry" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%"  BestFitPage="True" DisplayPage="true"  
        DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" BorderStyle="None" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ToBeFillExamFormCrt.rpt"></Report>
        </CR:CrystalReportSource>
</asp:Content>

