<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="ViewAccount.aspx.cs" Inherits="Acc_ViewAccount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Account Transaction
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblViewAccount" runat="server" Text="Account Transaction" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Account Transaction</h1></div><br />
<div id="pnlViewbtn" style="float:right; margin-right:30px;"><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick" Height="50px" Width="100px" Font-Size="20px" /></div>
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<center><table width="60%"><tr><td><asp:RadioButton ID="rbtnICE" runat="server" Text="ICE(I)" GroupName="dev" OnCheckedChanged="rbtnICE_OnCheckedChanged" AutoPostBack="true" /></td><td><asp:RadioButton ID="rbtnIM" runat="server" Text="IMID" GroupName="dev" OnCheckedChanged="rbtnIMD_CheckedChanged" AutoPostBack="true" /></td><td><asp:RadioButton ID="rbtnDiary" runat="server" Text="Diary No." Visible="false" GroupName="dev" OnCheckedChanged="rbtnDiary_OnCheckedChanged" AutoPostBack="true" /></td></tr></table></center>
<asp:Panel ID="panlSession" runat="server" ><center>Session:&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_ONSelectediNdexCanged" Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYear_OnTextChanged"></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;IMID:&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="100px"  AutoPostBack="true" OnTextChanged="txtIMID_TextChanged"></asp:TextBox></center></asp:Panel>
<asp:Panel ID="pnlDate" runat="server" ><center>Date:&nbsp;&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" CssClass="txtbox" Width="80"></asp:TextBox>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDateFrom"></dev:CalendarExtender><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
&nbsp;&nbsp;TO&nbsp;&nbsp; <asp:TextBox ID="txtDateto" runat="server" CssClass="txtbox" Width="80"></asp:TextBox>&nbsp;<dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
PopupButtonID="cald" PopupPosition="BottomRight" TargetControlID="txtDateto">
</dev:CalendarExtender><img ID="cald" runat="server" alt="Cald" src="../images/cal.png" /></Center></asp:Panel>
<asp:Panel ID="pnlDiary" runat="server" ><center>Diary No: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDiary" runat="server" Width="200px" CssClass="txtbox"></asp:TextBox></center></asp:Panel>
<asp:Panel ID="pnlAC" runat="server" >
<fieldset><legend>&nbsp;Account Status</legend><table class="tbl"><tr><td>IMID:</td><td><asp:Label ID="lblIMID" runat="server"></asp:Label></td><td>Late Fees Dues:</td><td><asp:Label ID="lbllateFees" runat="server" ></asp:Label></td></tr>
<tr><td>Total Amount:</td><td><asp:Label ID="lbltotal" runat="server" ></asp:Label></td><td>Dues Amount:</td><td><asp:Label ID="lblDues" runat="server" ></asp:Label></td></tr>
<tr><td>Group Amount:</td><td><asp:Label ID="lblGtotal" runat="server" ></asp:Label></td></tr>
<tr><td>Books Amount:</td><td><asp:Label ID="lblBooksAmount" runat="server" ></asp:Label></td><td>Prospectus:</td><td><asp:Label ID="lblProspectus" runat="server" ></asp:Label></td></tr>
</table>
</fieldset>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<br />
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
<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1><asp:Label ID="lblGridTitle" runat="server" ></asp:Label></h1>
<div id="Div12" style="display:block;">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:450px">
<asp:GridView ID="GridAC" runat="server" AllowPaging="True" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  PageSize="25" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onrowcommand="GridAC_RowCommand" 
        onselectedindexchanged="GridAC_SelectedIndexChanged1" 
        onrowdatabound="GridAC_RowDataBound" onpageindexchanging="GridAC_PageIndexChanging">
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
</div><br />
</asp:Content>

