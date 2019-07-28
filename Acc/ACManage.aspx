<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="ACManage.aspx.cs" Inherits="Acc_ACManage" %>

<script runat="server">
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Account LateFees/Dues ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblACManage" runat="server" Text="Manage IM Account" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
 <div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Manage IM Account </h1></div><br />
<center><asp:Label ID="lblrbtnMessage" runat="server" ForeColor="Gray"></asp:Label></center>
<table class="tbl" width="90%"><tr><td align="center">
<asp:RadioButton ID="rdLateFees" runat="server" Text="Manage Late Fees" oncheckedchanged="rdLateFees_CheckedChanged" GroupName="a" AutoPostBack="True" /></td><td align="center">
<asp:RadioButton ID="rdDues" runat="server" Text="Manage IM Dues" oncheckedchanged="rdDues_CheckedChanged"  GroupName="a" AutoPostBack="True"/></td></tr>
<tr><td align="right">Insert IMID:&nbsp;&nbsp;</td><td align="left">
    <asp:TextBox ID="txtIMID" Width="100px" runat="server" CssClass="txtbox" 
        ontextchanged="txtIMID_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" CssClass="btnsmall" Text=" OK " OnClick="btnOK_Click"  /></td><td>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr></table>
<asp:Panel ID="pnlACShow" runat="server" >
<table class="tbl" width="90%"><tr><td colspan="3">
<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
<tr><td>Amount of IM:&nbsp;Rs.</td><td><asp:Label ID="lblTAmt" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td><td>Address:&nbsp;&nbsp;<asp:Label ID="lblIMAddress" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Group Amount:&nbsp;&nbsp;Rs.</td><td><asp:Label ID="lblGAmt" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label> </td><td><asp:Label ID="lblIMCity" runat="server" ></asp:Label></td></tr>
<tr><td>Amt. Taken From ICE(I)</td><td><asp:Label ID="lblAmtTaken" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td><td>Group ID:&nbsp;<asp:Label ID="lblGroupID" runat="server" ForeColor="MediumBlue"></asp:Label></td></tr>
</table></asp:Panel><center><asp:Label ID="lblExceptionOK" runat="server" ></asp:Label></center><asp:Label Visible="false" ID="lblSessionHiddend" runat="server"  Font-Bold="true"></asp:Label>
<br />

<center><asp:Label ID="lblFessStatus" runat="server"></asp:Label><br /><asp:Button ID="btnManage" runat="server" CssClass="btnsmall" Text="Manage Account Amount"  OnClick="btnMamageAC_OnClick"  /></center>
<script>
      function toggleA1x(showHideDiv, switchImgTag) {
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
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Select IM Account</h1>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px;">
<center><asp:GridView ID="GridIM" runat="server" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" 
                         BorderWidth="1px" CellPadding="8" CellSpacing="8" 
                         GridLines="Horizontal" HorizontalAlign="Center" Width="100%" 
                         AllowPaging="True" onpageindexchanging="GridIM_PageIndexChanging" 
                         onselectedindexchanged="GridIM_SelectedIndexChanged" 
        onrowdatabound="GridIM_RowDataBound">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <EmptyDataTemplate><center> IM Account Record Not found for Manage !!!</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" Position="Bottom" FirstPageText="First" NextPageText="Next" LastPageText="Last"  /><PagerStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Bottom" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView></center>
                     
   </div>
   </div></div>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>

