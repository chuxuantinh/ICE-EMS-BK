<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="ProjectApproved.aspx.cs" Inherits="Reports_Project_ProjectApproved" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Copies for and on behalf of ICE(I) Ludhiana
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
 
 <h2> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Project Copies for and on behalf of ICE(I) Ludhiana </h2>
<div align="center">
<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
 <asp:Label ID="lblSessoin" runat="server"></asp:Label>
<div id="dates" runat="server" style="text-align:center;">Sesssion:<asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlSEssion_SelectedIndexChanged"
                   CssClass="txtbox"  
                   Width="150px" >
                   <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                   <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
               </asp:DropDownList>&nbsp; Year:<asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true"  CssClass="txtbox" OnTextChanged="txtYear_TextChanged"   Width="100px"></asp:TextBox>  
   <br />
   <asp:DropDownList ID="ddlProjectCopies" runat="server" CssClass="txtbox" AutoPostBack="true"
        onselectedindexchanged="ddlProjectCopies_SelectedIndexChanged"><asp:ListItem Text="CopySubmitted" Value="CopySubmitted" AutoPostBack="ture" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" /><asp:ListItem Text="CopyDispatched" Value="CopyDispatched" /></asp:DropDownList>
  &nbsp;&nbsp;
 <span id="date" runat="server"> Send   Date:&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="txtbox"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender><img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" />           
</span>
    </div>
      </ContentTemplate></asp:UpdatePanel>
        <br />
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
              onclick="btnView_Click" />
                    </div>
  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="ProjectApproved_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
           />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ProjectApproved.rpt">
            </Report>
        </CR:CrystalReportSource>
        </div>
       </asp:Content>
