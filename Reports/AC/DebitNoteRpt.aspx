<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="DebitNoteRpt.aspx.cs" Inherits="Reports_AC_DebitNoteRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <h2>Debit Note Report</h2>
    <center>Sesssion:&nbsp;
               <asp:DropDownList ID="ddlExamSeason" runat="server" 
                   CssClass="txtbox" 
                   Width="200px" >
                   <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                   <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
               </asp:DropDownList><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
          &nbsp;Year:&nbsp;&nbsp;
               <asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true" 
                   CssClass="txtbox" Width="100px"></asp:TextBox><br />

    IMID:
    <asp:TextBox ID="txtSearch" runat="server"  CssClass="txtbox">
    </asp:TextBox> Status:<asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Processed">Processed</asp:ListItem>
            <asp:ListItem Value="Requested">Requested</asp:ListItem>
            <asp:ListItem Value="Approved">Approved</asp:ListItem>
              <asp:ListItem Value="Hold">Hold</asp:ListItem>
              </asp:DropDownList>
         <asp:Button ID="btnOk" runat="server" Text="Search" CssClass="btnsmall" 
             onclick="btnOk_Click" /></center>
    <div>
    <br />
    <div width:100%">
   <cr:crystalreportviewer ID="DebitNoteRpt" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" 
    ToolPanelView="None" HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
            <Report FileName="DebitNoteCrt.rpt">
            </Report>
        </cr:crystalreportsource>
        </div>
</asp:Content>

