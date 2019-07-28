<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="StuExpRpt.aspx.cs" Inherits="Reports_Student_StuExpRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
     <asp:Content ID="Content0" ContentPlaceHolderID="title" runat="server" >Student Experience/Doc Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
     <h2>&nbsp;&nbsp;Student Experience/Doc Status</h2>
 <div align="center">
 <asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table>    
          <tr >                      
      <td align="center">&nbsp; Session
        <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
              </asp:DropDownList>&nbsp;&nbsp;Year
        <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
          &nbsp;</td>
      </tr>
      <tr>
              <td colspan="2" align="center">Type:<asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox" >
                  <asp:ListItem Value="ExpStatus">Experience</asp:ListItem>
                  <asp:ListItem Value="DocStatus">Document</asp:ListItem> </asp:DropDownList> &nbsp; Status: 
              <asp:DropDownList ID="ddlMembershipGtd" runat="server" CssClass="txtbox">
                  <asp:ListItem Value="yes">With Experience/Doc</asp:ListItem>
                  <asp:ListItem Value="no">Without Experience/Doc</asp:ListItem>
              </asp:DropDownList> &nbsp; 
            Select:<asp:DropDownList ID="ddlselect" runat="server" CssClass="txtbox" 
                              AutoPostBack="True" onselectedindexchanged="ddlselect_SelectedIndexChanged">
            <asp:ListItem Value="ALL">ALL</asp:ListItem>
            <asp:ListItem Value="IMID">IMID</asp:ListItem>
              </asp:DropDownList>&nbsp;&nbsp;
                          <asp:Label ID="lblIMID" runat="server" Text="IMID"></asp:Label><asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox></td> <td>&nbsp;<asp:Button ID="btnView" runat="server" Text="View" 
                      CssClass="btnsmall" onclick="btnView_Click" Height="25px" /></td></tr>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="Stu_Exp_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="StuExperience.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

