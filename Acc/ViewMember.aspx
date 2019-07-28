<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="ViewMember.aspx.cs" Inherits="Acc_ViewMember" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">View Member Account
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblViewMember" runat="server" Text="View Member Account" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>View Member Account </h1></div><br />
<asp:Panel ID="panlSession" runat="server" ><table width="90%" class="tbl"><tr><td>Session:</td><td><asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_ONSelectediNdexCanged" Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;Year:<asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYear_OnTextChanged"></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"></asp:Label></td></tr><tr><td>IMID:</td><td><asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
Fee Type:
<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlType_SelectedIndexChanged">
<asp:ListItem>All</asp:ListItem>
<asp:ListItem>Subscription</asp:ListItem>
<asp:ListItem>Inspection</asp:ListItem>
<asp:ListItem>Membership</asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick"    />
</td></tr></table></asp:Panel>
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
<asp:GridView ID="GridAC" runat="server" AllowPaging="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"  PageSize="25" ForeColor="Black" GridLines="Vertical" Width="100%" onrowcommand="GridAC_RowCommand" onselectedindexchanged="GridAC_SelectedIndexChanged1" onrowdatabound="GridAC_RowDataBound" onpageindexchanging="GridAC_PageIndexChanging">
<RowStyle BackColor="#F7F7DE" />
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

