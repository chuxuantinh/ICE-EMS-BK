<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="IMOrder.aspx.cs" Inherits="Reports_Invent_IMOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Order Details Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">

<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<h2>IM Order Details</h2>
  <table><tr><td  align="center">
        <asp:Label ID="lblOrder" runat="server" Text="Order Type"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddlOrder" runat="server"  AutoPostBack="True" CssClass="txtbox"
            onselectedindexchanged="ddlOrder_SelectedIndexChanged">
            <asp:ListItem>Session</asp:ListItem>
            <asp:ListItem>IMID &amp; Session</asp:ListItem>
            <asp:ListItem>IMID &amp; Type &amp; Session</asp:ListItem>
            <asp:ListItem>Order No</asp:ListItem>
        </asp:DropDownList>
        <br />
    </td></tr>
     
         <asp:Panel ID="panSession" runat="server">
     
    
     
       <tr><td>Session
        <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      Year
      
        <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
           <br />
       </td>
       </tr>
       </asp:Panel>
        <asp:Panel ID="panIMID" runat="server">
    <tr><td align="center">
         <asp:Label ID="lblType" runat="server" Text="Type"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" Width="150px">
         <asp:ListItem>Books</asp:ListItem>
         <asp:ListItem>Prospectus</asp:ListItem>
     </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     IMID&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
        </td></tr>
        </asp:Panel>
    
     <tr><td>
            <asp:Label ID="lblOrderNo" runat="server" Text="Order No"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOrderNo" runat="server" CssClass="txtbox"></asp:TextBox>
          &nbsp;</td>
      </tr> 
              
                 <asp:Panel ID="panCity" runat="server">
     
    
        </asp:Panel>
        <tr align="center"><td><asp:Button ID="btnView" runat="server" Text="View" 
                CssClass="btnsmall" onclick="btnView_Click" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="IMOrder_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="IMOrderCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>

</asp:Content>

