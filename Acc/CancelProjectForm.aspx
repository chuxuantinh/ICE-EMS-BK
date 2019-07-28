<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="CancelProjectForm.aspx.cs" Inherits="Acc_CancelProjectForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Cancel Project Form
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblAount" runat="server" Text="Cancel Project Form" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>
    Cancel Project Form:</h1></div>
<center>Apps Type:&nbsp;<asp:DropDownList ID="ddlAppType" runat="server" CssClass="txtbox" >
<asp:ListItem >ProformaB</asp:ListItem>
<asp:ListItem >ProformaC</asp:ListItem>
    </asp:DropDownList>
&nbsp;&nbsp;
Status:&nbsp;<asp:DropDownList runat="server" ID="ddlStatus" CssClass="txtbox">
<asp:ListItem Value="NotApproved" Text="NotApproved" />
<asp:ListItem Value="Hold" Text="Hold" />
<asp:ListItem Value="Approved" Text="Approved" />
</asp:DropDownList>
&nbsp;&nbsp;&nbsp;Diary No.&nbsp;&nbsp;<asp:TextBox ID="txtDiaryNo" runat="server" CssClass="txtbox"></asp:TextBox> &nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" OnClick="btnView_click" />
</center>
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
<div class="headerDivImgfees" style="color:White;">&nbsp;
<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>&nbsp;</h1>
<div id="Div12" style="display:block;">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:200px" >
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
<asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" 
        AutoGenerateColumns="true" OnRowDataBound="GridAppTable_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5"
        CellSpacing="5" Width="100%">
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
<center>Remarks:&nbsp;&nbsp;<asp:TextBox ID="txtRemarks" runat="server" CssClass="txtbox" TextMode="MultiLine" Height="30px"></asp:TextBox><br /><asp:Label ID="lblException" runat="server"></asp:Label><br />
<asp:Button ID="btnSubmit" runat="server" CssClass="btnsmall" OnClientClick="return confirm('Confirm Submit Form ?');"  Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnsmall" OnClick="btnCancel_Click" />
</center>
<br /><br /><br />
<br /><br /><br />
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>

