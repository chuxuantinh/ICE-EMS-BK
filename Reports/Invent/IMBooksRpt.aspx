<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="IMBooksRpt.aspx.cs" Inherits="Reports_Invent_IMBooksRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Books Detail Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">
<h2>Required IM Course Details</h2>
<table class="tbl">
<tr><td>
<asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="267px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem>ALL</asp:ListItem>
            <asp:ListItem>IMID</asp:ListItem>
        </asp:RadioButtonList></td></tr>
         <asp:Panel ID="panIMID" runat="server">
    <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IMID:<asp:TextBox ID="txtIMID" runat="server" Width="95px"></asp:TextBox>       
       </td></tr>
        </asp:Panel>       
        </table>
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  
     
    
   <center>
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                onclick="btnView_Click" />
                </center>
        
  
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
<CR:CrystalReportViewer ID="IMBooks_Details_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="IMBooksCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>

