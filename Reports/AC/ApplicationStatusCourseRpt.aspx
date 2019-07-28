<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="ApplicationStatusCourseRpt.aspx.cs" Inherits="Reports_AC_ApplicationStatusCourseRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<script runat="server">

    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Application Status Course Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>

<h2>&nbsp;&nbsp;&nbsp; Application Status Course Report (Total number of forms of Each Course)</h2>
<br />
<center>
<div id="dates" runat="server" style="text-align:center;">Sesssion:&nbsp;
               <asp:DropDownList ID="ddlExamSeason" runat="server" 
                   CssClass="txtbox" 
                   Width="150px" >
                   <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                   <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
               </asp:DropDownList>&nbsp;&nbsp;Year:
               <asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true" 
                   CssClass="txtbox" Width="80px"></asp:TextBox>
    &nbsp;&nbsp;Forms:&nbsp;<asp:DropDownList ID="ddlAppType" runat="server" CssClass="txtbox">
                        <asp:ListItem Text="Examination Application Forms" Value="Exam" />
                       <asp:ListItem Text="New Admission Application Forms " Value="NewAdmission" />
                        <asp:ListItem Text="Promotted Admission Form(SectionA)" Value="ReAdmission" />
                        <asp:ListItem Text="ITI Application Forms" Value="ITI" />
                        <asp:ListItem Text="Composite Fees" Value="Composite" />
                        <asp:ListItem Text="Annual Subscription" Value="Subscription" />
                        <asp:ListItem Text="Exam Center Change" Value="ChangeCenter" />
                        <asp:ListItem Text="Re-Checking" Value="Rechecking" />
                        <asp:ListItem Text="Final Marksheet" Value="FinalPass" />
                        <asp:ListItem Text="Provisional Certificate" Value="ProvisionalCertificate" />
                        <asp:ListItem Text="Membership Certificate" Value="MembershipCertificate"></asp:ListItem>
                        <asp:ListItem Text="Marks Statement" Value="MarksStatement"></asp:ListItem>
                        <asp:ListItem Text="Project Proforma B" Value="ProformaB" />
                        <asp:ListItem Text="Project Proforma C" Value="ProformaC" />
                        <asp:ListItem Text="MCADLateFee" Value="MCADLateFee" />
                        <asp:ListItem Text ="MCADRegistration" Value="MCADRegistration"></asp:ListItem>
                        <asp:ListItem Text="NOC" Value="NOC" />
                        <asp:ListItem Text="DATEOFADMMCERTFEE" Value="DATEOFADMMCERTFEE" />
                        <asp:ListItem Text="ID Card" Value="IDCard"></asp:ListItem>
                        <asp:ListItem Text="Admit Card" Value="AdmitCard"></asp:ListItem>
                        <asp:ListItem Text="Old Question Papers" Value="OldSet" />
                        <asp:ListItem Text="Other Charges...." Value="Other" />
    </asp:DropDownList>
    Status:
    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox"><asp:ListItem Value="All" Text="All" /><asp:ListItem Value="Approved" Text="Approved" /><asp:ListItem Value="NotApproved" Text="Not Approved" /><asp:ListItem Value="Hold" Text="Hold" />
    </asp:DropDownList>
    <br /><asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View" />
<br />
<asp:Label ID="lblExceptioN" runat="server" Font-Bold="true"></asp:Label><hr />
 </div>
 </center>
 
 <CR:CrystalReportViewer ID="ApplicationStatusCourse" runat="server" 
            AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" 
             Width="100%"  BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" BorderStyle="None"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ApplicationStatusSumCrt.rpt"></Report>
        </CR:CrystalReportSource>
</asp:Content>

