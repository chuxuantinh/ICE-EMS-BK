<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="ExamBillView.aspx.cs" Inherits="Acc_ExamBillView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Examination Department
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblExamBillView" runat="server" Text="Examination Accounts[View]" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Accounts[View] </h1></div><br />
<br />
<table class="tbl" width="90%">
<tr><td align="right" colspan="1">Select Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="100px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></tr>
<tr><td align="right" >Session:&nbsp;&nbsp;</td><td><asp:Label ID="lblSessionHiddend" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Billing type&nbsp;<asp:DropDownList ID="ddlBillingType" runat="server" Width="200px" OnSelectedIndexChanged="ddlBilling_SelectedINdexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Value="Documents" Text="Paper & Documents Prining" /><asp:ListItem Value="PaperSetter" Text="Paper Setter Fees" /><asp:ListItem Value="ExamCenter" Text="Examination Center Fees" /><asp:ListItem Value="Invigilator" Text="Invigilator Fees" /><asp:ListItem Text="Other Fees/Charges" Value="Other" /></asp:DropDownList></td></tr>
</table>
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
</div><table style="color:White; font-weight:bold;"><tr><td>&nbsp;&nbsp;<asp:RadioButton ID="rbtnOne" Checked="true" GroupName="View" runat="server"  /></td><td><asp:RadioButton ID="rbtnAll" runat="server" GroupName="View" Text="View All" /></td><td><asp:Button ID="lbtnViewRollNo" OnClick="btnViewRollNo_Click" runat="server" Text="View " /></td></tr></table>
<div id="Div12" style="display:block;">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:400px">
<asp:GridView ID="GridView2" runat="server" AllowPaging="true" AutoGenerateColumns="true" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"  PageSize="25" OnRowDataBound="GridView2_OnRowDataBound"  ForeColor="Black" GridLines="Vertical" Width="100%" onpageindexchanging="GridView2_PageIndexChanging">
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
<br />
</div>
</asp:Content>

