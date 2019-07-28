<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ITIApplication.aspx.cs" Inherits="Admission_ITIApplication" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">ITI Application
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="ITI Form Application" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers><asp:PostBackTrigger ControlID="btnOk" /></Triggers>
<ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>ITI Application</h1></div>
<asp:Panel ID="panlSession" runat="server" >
<center><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_ONSelectediNdexCanged" Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;Year: <asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYear_OnTextChanged"></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"/></div></center><br />
<center><div>Select Criteria:&nbsp;<asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="150px" CssClass="txtbox">
        <asp:ListItem>Date</asp:ListItem>
        <asp:ListItem>IMID</asp:ListItem>
        <asp:ListItem Value="FormNo">Serial No</asp:ListItem>
        <asp:ListItem>All</asp:ListItem>
    </asp:DropDownList>&nbsp;<asp:TextBox ID="txtEnter" runat="server" CssClass="txtbox"></asp:TextBox>
    </div></center><br />
<center><asp:Panel ID="pnlDate" runat="server" Width="38%">
<table width="100%">
<tr>
<td><asp:TextBox ID="txtDateFrom" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"  Format="dd/MM/yyyy" PopupButtonID="cal0" PopupPosition="BottomRight" TargetControlID="txtDateFrom"></dev:CalendarExtender></td>
<td><img src="../images/cal.png" id="cal0" runat="server"  alt="Cal" /></td>
<td>TO</td>
<td><asp:TextBox ID="txtDateto" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender ID="txtDateto_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDateto"></dev:CalendarExtender></td>
<td><img ID="cald" runat="server" alt="Cald" src="../images/cal.png" /></td>
</tr>
</table></asp:Panel></center>
<br />
<center><asp:Label ID="lblMessageExc" runat="server" ForeColor="Red"></asp:Label></center>
<br />
<center>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOk" runat="server" CssClass="btnsmall" onclick="btnOk_Click" Text="View" /></center>
<br />
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
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
    
   <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
   
    <a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"/></a>
</div><h1><asp:Label ID="lblGridTitle" runat="server" ></asp:Label>Total Forms:<asp:Label ID="lblTotalForms" runat="server"></asp:Label>
    </h1>
    <br />
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:450px">
<asp:GridView ID="GridView" runat="server" AllowPaging="True" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  PageSize="25" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onrowdatabound="GridView_RowDataBound" >
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div>
</div>
</div>
<asp:Panel ID="pnlspc" runat="server" Height="48px"></asp:Panel>
</div>
</asp:Content>

