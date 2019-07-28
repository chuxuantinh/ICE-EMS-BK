<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="AicteProjectRpt.aspx.cs" Inherits="Reports_Project_AicteProjectRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Institutes Regisrtation Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
 
 <h2> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AICTE Letter </h2>
   
<div align="center">

<asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>

<div id="dates" runat="server" style="text-align:center;">Sesssion:<asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" 
                   CssClass="txtbox"  
                   Width="150px" >
                   <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                   <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
               </asp:DropDownList>&nbsp; Year:<asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true"  CssClass="txtbox"  Width="100px"></asp:TextBox>  
    
    &nbsp;&nbsp;AICTE ID:<asp:TextBox ID="txtINSId" runat="server" AutoPostBack="true" 
        CssClass="txtbox" Width="100px"></asp:TextBox>
    </div>

      </ContentTemplate></asp:UpdatePanel>
        <br />
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
              onclick="btnView_Click" />
                    </div>
                   
                
  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="AICTE_Letter_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" 
           />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="AicteLetterCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
        </div>
</asp:Content>

