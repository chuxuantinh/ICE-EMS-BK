<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Student/StudentRptMaster.master" AutoEventWireup="true" CodeFile="StudentAccountRpt.aspx.cs" Inherits="Reports_Student_StudentAccountRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <asp:Content ID="Content3" ContentPlaceHolderID="title" Runat="Server">Student Form Report</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
 <div align="center">
 <h2>Student Account Details</h2>
 <asp:UpdatePanel ID="updpnl" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
  <table><tr><td colspan="3" align="center" class="style1">
        <asp:RadioButtonList ID="rblICE" runat="server" AutoPostBack="True" 
              RepeatDirection="Horizontal" Width="244px" 
            onselectedindexchanged="rblICE_SelectedIndexChanged" Height="19px">
            <asp:ListItem>ICE</asp:ListItem>
            <asp:ListItem>IMID</asp:ListItem>
        </asp:RadioButtonList>
    </td></tr>
    <tr><td colspan="3" align="center">Select
    <asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox" AutoPostBack="True" 
              onselectedindexchanged="ddlType_SelectedIndexChanged">
       
       
        <asp:ListItem>IMID</asp:ListItem>
        <asp:ListItem>Exam Status</asp:ListItem>
        <asp:ListItem Value="CFeesStatus">Composite Fees Status</asp:ListItem>
        <asp:ListItem Value="SID">Student Membership ID</asp:ListItem>
          <asp:ListItem Value="SName">Student Name</asp:ListItem>
    </asp:DropDownList>
   
      
          </td>
       </tr>
          <tr><td colspan="3"> &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblImid" runat="server" Text="IMID"></asp:Label>&nbsp;<asp:TextBox ID="txtIMID" CssClass="txtbox" width="95px" runat="server"></asp:TextBox>   
              &nbsp;&nbsp;&nbsp;<asp:Label ID="lblExamStatus" runat="server" Text="Status"></asp:Label>
    <asp:DropDownList ID="ddlExamStatus" CssClass="txtbox" runat="server" >
    </asp:DropDownList>
   
      
  
            
            
              <asp:Label ID="lblSID" runat="server" Text="Student Membership ID"></asp:Label>
              <asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
   
      
  
            
            
              </td></tr>
        
        
         <tr><td colspan="3" align="center">
                <asp:Label ID="lblSName" runat="server" Text="Student Name"></asp:Label>
                <asp:TextBox ID="txtSName" CssClass="txtbox" runat="server"></asp:TextBox> 
              
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" />
             </td></tr>
                
         
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

  <div style="overflow: scroll; width:100%">
  <CR:CrystalReportViewer ID="StudentAccount_Report" 
                  runat="server" Width="100%" 
        BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" ToolPanelView="None"
       HasCrystalLogo="False" HasToggleGroupTreeButton="false" 
        BorderStyle="None" 
            AutoDataBind="True" Height="1039px" 
        ReportSourceID="CrystalReportSource1" EnableTheming="True" oninit="StudentAccount_Report_Init"
         />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="StudentaccountCrt.rpt">
            </Report>
        </CR:CrystalReportSource>
  </div>
</asp:Content>

