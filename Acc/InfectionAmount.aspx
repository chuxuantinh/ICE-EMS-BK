<%@ Page Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="InfectionAmount.aspx.cs" Inherits="Acc_InfectionAmount" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Inspection Amount
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblInfectionAmount" runat="server" Text="IM Membership Inspection" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>IM Membership Inspection </h1></div><br />
<center>View: &nbsp;<asp:DropDownList runat="server" ID="ddViewBy" CssClass="txtbox" AutoPostBack="true" onselectedindexchanged="ddlViewBy_SelectedIndexChanged" ><asp:ListItem Value="All" Text="All" /><asp:ListItem Value="IMID" Text="IMID" /></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
Status:&nbsp;<asp:DropDownList ID="ddlIMStatus" runat="server" Width="150px" CssClass="txtbox"><asp:ListItem Value="NotApprove" Text="Not Approve" /><asp:ListItem Value="SubToApprove" Text="Subject to Approve" /><asp:ListItem Value="Approve" Text="Approve" /><asp:ListItem Value="Pending" Text="Pending" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btnsmall" /></center>
<asp:Panel ID="pnlAmount" runat="server" >
<fieldset><legend>Fee Structure</legend>
<table width="80%"><tr><td>Account Balance:(Rs)&nbsp;<asp:Label ID="lblToAmt" runat="server" Font-Bold="true"></asp:Label></td><td>Diary Amount:(Rs)&nbsp;<asp:Label ID="lblDiaryAmt" runat="server" Font-Bold="true" ></asp:Label></td></tr></table>
</fieldset><br />
<center>Return Amount:&nbsp;<asp:TextBox Width="100px"  ID="txtRefund" AutoPostBack="true"
        CssClass="txtbox" runat="server" ontextchanged="txtRefund_TextChanged1" ></asp:TextBox><br />
<asp:Label ID="lblException" runat="server" Font-Bold="true"></asp:Label><br /><br />
<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnsmall" OnClick="btnSAve_Onclick"/>&nbsp;&nbsp;
</center>
<br /></asp:Panel>
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
        var ele = document.getElementById(showHideDiv);
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
</div><table style="color:White; font-weight:bold;"><tr><td>&nbsp;&nbsp;<asp:DropDownList ID="ddlViewType" runat="server" Width="150px" Visible="false" ><asp:ListItem Value="NotApprove" Text="Not Approve" /></asp:DropDownList></td><td>
        &nbsp;</td></tr></table>
<div id="Div12" style="display:block;"><br /><br />
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:350px" >
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true"  Width="100%"
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="GridView2_OnRowDataBound"
        CellPadding="3" ForeColor="Black"  OnSelectedIndexChanged="GridView2_OnSelectedIndexChanged"
        GridLines="Vertical">
        <FooterStyle BackColor="#CCCCCC" />
        <Columns>
        <asp:CommandField ShowSelectButton="true" SelectText="Select" />
        </Columns>
        <EmptyDataTemplate ><center><br /><br /><b>Inspection Record not found for adjustment.</b></center></EmptyDataTemplate>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
   
   </div>
</div>
</div>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" 
           Width="100%" onrowdatabound="GridView1_RowDataBound">
            <RowStyle BackColor="#F7F7DE" />
           <EmptyDataTemplate><center>  Record Not Found !</center></EmptyDataTemplate>
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
</div><br />
</asp:Content>

