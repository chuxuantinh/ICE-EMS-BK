<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="InventoryReport.aspx.cs" Inherits="Acc_InventoryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Inventory Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblAdmissionForm" runat="server" Text="Institutional Member Account" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2"><asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" >
<Triggers><asp:PostBackTrigger ControlID="ibtnExportDoc" /><asp:PostBackTrigger ControlID="ibtnExportExcel" /><asp:PostBackTrigger ControlID="ibtnExportPDF" /></Triggers>
<ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member[IM] Account </h1></div><br />
<asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label>
<table width="80%" class="tbl"><tr><td>
<table class="tbl"><tr align="center"><td>Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" Width="70px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></tr>
<tr align="center"><td>Date:</td><td>
<asp:TextBox Width="100px" ID="txtDate1" runat="server" CssClass="txtbox"></asp:TextBox> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate1"></dev:CalendarExtender><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />           
   &nbsp;TO  <asp:TextBox Width="100px" ID="txtDate2" runat="server" CssClass="txtbox"></asp:TextBox> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="cald" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate2"></dev:CalendarExtender><img src="../images/cal.png" id="cald" runat="server"  alt="Cald" />
</td>
 </tr></table></td><td align="center">
 <asp:Button ID="btnView" runat="server" CssClass="btnsmall" OnClick="btnVeiw_OnClick" Text="View" />
 </td></tr></table>
 <center><asp:Label ID="lblException" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></center>
 <script>
     function toggleBG(showHideDiv, switchImgTag) {
         var ele = document.getElementById(showHideDiv);
         var imageEle = document.getElementById(switchImgTag);
         var imageEle = document.getElementById(switchImgTag);
         if (ele.style.display == "block") {
             ele.style.display = "none";
             imageEle.innerHTML = '<img src="../images/plus.png">';
         }
         else {
             ele.style.display = "block";
             imageEle.innerHTML = '<img src="../images/minus.png">';
         }
     }
</script>
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
<asp:ImageButton ID="ibtnExportDoc" runat="server" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTable_click" Height="25px" Width="25px" />&nbsp;&nbsp;<asp:ImageButton ID="ibtnExportExcel" runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTable_Click"  Height="25px" Width="25px"/>&nbsp;&nbsp;<asp:ImageButton ID="ibtnExportPDF" runat="server"  AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTable_Click"  Height="25px" Width="25px"/>
<a id="A1G" href="javascript:toggleBG('Div1G', 'A1G');"><%--<img src="../images/minus.png" alt="Show">--%></a>
</div><br /><br />
<div id="Div1G" style="display:block;">
<input id="scrollPos3" runat="server" type="hidden" value="0" />
<div id="divdatagrid3" style="width: 100%; overflow:scroll; height:450px" >
<asp:GridView ID="GridBooks" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true"  AllowPaging="false"  BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="5" Width="100%">
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>
        </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>