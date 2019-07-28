<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="ProjectStatusRpt.aspx.cs" Inherits="Reports_Project_ProjectStatusRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Status Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div align="center">
<h2>Project Submission Report</h2>
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table>
  <tr><td><asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="267px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged">
            <asp:ListItem Selected="True">ICE</asp:ListItem>
            <asp:ListItem>IMID</asp:ListItem>
           
           
        </asp:RadioButtonList></td></tr><tr><td  align="center">
        <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox"
            onselectedindexchanged="ddlCourse_SelectedIndexChanged" Width="115px">
            <asp:ListItem Value="Civil">Civil</asp:ListItem>
            <asp:ListItem Value="Architecture">Architecture</asp:ListItem>
        
        </asp:DropDownList>
          <asp:DropDownList ID="ddlProject" runat="server" CssClass="txtbox"
              onselectedindexchanged="ddlProject_SelectedIndexChanged">
              <asp:ListItem>PartII</asp:ListItem>
              <asp:ListItem>SectionB</asp:ListItem>
              <asp:ListItem>PartII+SectionB</asp:ListItem>
          </asp:DropDownList>
        <br />
    </td></tr>
     

        <asp:Panel ID="panIMID" runat="server">
    <tr><td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IMID:<asp:TextBox ID="txtIMID" runat="server" Width="95px"></asp:TextBox> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        
        </td></tr>
        
        </asp:Panel>
       
    
     
              
                
        <tr align="center"><td><asp:Button ID="btnView" runat="server" Text="View" 
                CssClass="btnsmall" onclick="btnView_Click" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Project_Status_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ProjectStatusCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

