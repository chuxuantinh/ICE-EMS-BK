<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/FO/FORptMaster.master" AutoEventWireup="true" CodeFile="FormOnHold.aspx.cs" Inherits="Reports_FO_FormOnHold" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Forms On Hold
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div align="center">
    <p class="">No of Application Forms and DD count Received and Submitted.</p>
  <br />
  <asp:UpdatePanel ID="updatePanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnView" /></Triggers><ContentTemplate>
<table><tr><td>&nbsp;&nbsp; Session
        <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox">
            <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
            <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
              </asp:DropDownList>
        </td>
    <td>&nbsp;&nbsp; Year
        <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>
         </td>
       </tr>
          <tr> <asp:Panel ID="panDepartmentName" runat="server"><td colspan="4" align="center">Department
              <asp:DropDownList ID="ddlDepartmentName" runat="server" CssClass="txtbox" AutoPostBack="True" >
    </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
              </asp:Panel>
              </tr>
              <tr><td colspan="2" align="center"> Select:<asp:DropDownList ID="ddltype" 
                      runat="server" CssClass="txtbox" 
                      onselectedindexchanged="ddltype_SelectedIndexChanged">
                  <asp:ListItem Value="Forms">Forms</asp:ListItem>
                  <asp:ListItem Value="DD">DD</asp:ListItem>
                  </asp:DropDownList>
                  Type
                  <asp:DropDownList ID="ddlselect" runat="server" CssClass="txtbox" AutoPostBack="True" onselectedindexchanged="ddltype_SelectedIndexChanged">
                      <asp:ListItem Value="IMID">IMID</asp:ListItem>
                      <asp:ListItem Value="DairyNo">Diary No</asp:ListItem>
                  </asp:DropDownList>
                  <asp:Label ID="lblImid" runat="server" Text="IMID"> </asp:Label>
                  <asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="95px"></asp:TextBox>  <asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" onclick="btnView_Click" /></td></tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
   <div style="overflow: scroll; width:100%">
   <cr:crystalreportviewer ID="FormOnHold" runat="server" Width="100%" BestFitPage="True" DisplayPage="true"  DisplayStatusbar="true" 
    ToolPanelView="None" HasCrystalLogo="False" HasToggleGroupTreeButton="false" BorderStyle="None" AutoDataBind="True" Height="1039px" ReportSourceID="CrystalReportSource1" EnableTheming="True"  />
        <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
            <Report FileName="FormOnHoldCrt.rpt">
            </Report>
        </cr:crystalreportsource>
        </div>
        </asp:Content>