<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/AC/AccountReportMaster.master" AutoEventWireup="true" CodeFile="FormTypeRpt.aspx.cs" Inherits="Reports_AC_FormTypeRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Form Type
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
  <h2> &nbsp;&nbsp;&nbsp;Form Type</h2>
    <div align="center">
  <br />
  <asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
 

<table><tr><td> Select App Date:
<asp:TextBox Width="100px" ID="txtDate1" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate1" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate1"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />           
   &nbsp; &nbsp;TO  <asp:TextBox Width="100px" ID="txtDate2" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtDate2" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="cald" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate2"></dev:CalendarExtender><img src="../../images/cal.png" id="cald" runat="server"  alt="Cald" />
</td>
 </tr>
 <tr><td>
        Select FormType: <asp:DropDownList ID="ddlSelect" runat="server" CssClass="txtbox" Width="130px">
        </asp:DropDownList>
        &nbsp;&nbsp;Status:<asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" Width="110px">
            <asp:ListItem Value="Approved">Approved</asp:ListItem>
            <asp:ListItem Value="Hold">Hold</asp:ListItem>
            <asp:ListItem Value="NotApproved">Not Approved</asp:ListItem>
        </asp:DropDownList>
        </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <center id="pnlDate" runat="server" > 
&nbsp; 
&nbsp;&nbsp;
<br /><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" onclick="btnView_Click" />
    </center>
    
    </div>
   
   <cr:crystalreportviewer ID="FormType" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" 
    ToolPanelView="None" HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
            <Report FileName="FormTypeCrt.Rpt">
            </Report>
        </cr:crystalreportsource>
  
   
</asp:Content>

