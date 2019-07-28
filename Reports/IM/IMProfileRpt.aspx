<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="IMProfileRpt.aspx.cs" Inherits="Reports_IM_IMProfileRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >Inspection Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="center">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table><tr align="center"><td><h2>Inspection Report</h2></td></tr><tr><td  align="center">
        <asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="267px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem>IMID</asp:ListItem>
            <asp:ListItem>Status</asp:ListItem>
            <asp:ListItem Value="Name">Name</asp:ListItem>
             <asp:ListItem>City</asp:ListItem>
        </asp:RadioButtonList>
    </td></tr>
      <asp:Panel ID="panStatus" runat="server">
     
    <tr><td align="center">Status
        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True">
            <asp:ListItem Value="Approve">Approve</asp:ListItem>
              <asp:ListItem Value="NotApprove">NotApprove</asp:ListItem>
            <asp:ListItem Value="Pending">Pending</asp:ListItem>
             <asp:ListItem Value="SubToApprove">SubToApprove</asp:ListItem>
             <asp:ListItem Value="ReFund">ReFund</asp:ListItem>
              </asp:DropDownList>
        </td></tr>
        </asp:Panel>
       
    
     <tr><td align="center">
            <asp:Label ID="lblImid" runat="server" Text="IMID"></asp:Label>&nbsp; <asp:TextBox ID="txtIMID" CssClass="txtbox" runat="server"></asp:TextBox>
            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
             &nbsp;<asp:TextBox ID="txtName" CssClass="txtbox" runat="server"></asp:TextBox>

     <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
        <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" CssClass="txtbox">          
              </asp:DropDownList>
            
        
       
          </td>
      </tr> 
       
              
                 
        <tr align="center"><td><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                onclick="btnView_Click" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="IMProfileReport" runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
          oninit="CrystalReportViewer1_Init"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="IMProfileCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>

</asp:Content>

