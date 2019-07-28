<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="PurchesRpt.aspx.cs" Inherits="Reports_Invent_PurchesRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Purchase Order Details Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">

<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>

  <table><tr><td align="center"><h2>Purchase Order Details</h2></td></tr><tr><td  align="center">
        <asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="290px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem>Supplier</asp:ListItem>
            <asp:ListItem Value="OrderType">Order Type</asp:ListItem>
            <asp:ListItem Value="OrderNo" >Order No</asp:ListItem>
        </asp:RadioButtonList>
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
    
     <tr><td align="center">  <asp:Label ID="lblName" runat="server" Text="Supplier"></asp:Label>&nbsp;<asp:DropDownList ID="ddlName" runat="server" CssClass="txtbox" AutoPostBack="True"> </asp:DropDownList>
            <asp:Label ID="lblOrderNo" runat="server" Text="Order No"></asp:Label>&nbsp;<asp:TextBox ID="txtOrderNo" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
          <asp:Label ID="lblType" runat="server" Text="Order Type"></asp:Label>&nbsp;<asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" CssClass="txtbox">
            <asp:ListItem >Books</asp:ListItem>
              <asp:ListItem >Prosepectus</asp:ListItem>
           
              </asp:DropDownList></td>
      </tr> 
              
                 <asp:Panel ID="panCity" runat="server">
     
    
        </asp:Panel>
        <tr align="center"><td><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                onclick="btnView_Click" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Purchase_Order_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
          oninit="Purchase_Order_Report_Init"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="PurchesCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>

</asp:Content>

