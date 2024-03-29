﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="UpdateExamFees.aspx.cs" Inherits="Acc_UpdateExamFees" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Update Exam Fees
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblAppApprove" runat="server" Text="Update Late Fees" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Update Late Fees  </h1></div>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"  Font-Bold="true"></asp:Label>
<center>Session:<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" 
        AutoPostBack="True" onselectedindexchanged="ddlSession_SelectedIndexChanged" ><asp:ListItem Value="Win">Winter Examination</asp:ListItem><asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
    </asp:DropDownList>&nbsp;<asp:TextBox ID="txtYear" runat="server" AutoPostBack="true"
        CssClass="txtbox" ontextchanged="txtYear_TextChanged"></asp:TextBox><br />
    Part:&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_OnSelectedIndexChanged"><asp:ListItem Value="PartI" Text="PartI" /><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Value="SectionA" Text="SectionA" /><asp:ListItem Value="SectionB" Text="SectionB" /></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;IMID:&nbsp;&nbsp;<asp:DropDownList ID="ddlIMID" runat="server" CssClass="txtbox"><asp:ListItem Value="40189" Text="40189"></asp:ListItem></asp:DropDownList> 
    <asp:Button ID="btnok" runat="server" CssClass="btnsmall"  Text="Ok" onclick="btnok_Click"/><br />
    <asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true"></asp:Label></center>
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
<div class="headerDivImgfees" style="color:White;">&nbsp;<a id="A1w" href="javascript:toggleA1w('Div12', 'A1w');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Total Forms:<asp:Label ID="lblTotal" runat="server"></asp:Label><br /></h1>

<div id="Div12" style="display:block;">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:300px" >
<script type="text/javascript">
    function selectAlll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    } 

</script>
<asp:GridView ID="grdRecord" runat="server" BackColor="#DEBA84" 
        AutoGenerateColumns="true" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5"
        CellSpacing="5" Width="100%" onrowdatabound="grdRecord_RowDataBound">
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>
        <asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
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
<br />
<center>Exam Amount:&nbsp;&nbsp;<asp:TextBox ID="txtExamFees" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;Late Fees:&nbsp;&nbsp;<asp:TextBox ID="txtLateFees" runat="server" CssClass="txtbox" Text="0" Width="50px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btnsmall" onclick="btnUpdate_Click"/></center>
</div><br />
</asp:Content>
