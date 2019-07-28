<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="LateFeeAC.aspx.cs" Inherits="Acc_LateFeeAC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Late Fees Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblLateFeeAC" runat="server" Text="Late Fees Account" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Late Fees Account </h1></div>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label><table><tr><td>
        &nbsp;</td><td>&nbsp;</td></tr>
</table>
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
</div><div style="padding:1px;"><h1>Available IM for Late Fees Donation</h1></div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 100%; overflow:scroll; height:auto">
<asp:GridView ID="GridIM" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true" Height="200px"
        CellPadding="8" CellSpacing="8" OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%">
        <Columns><asp:CommandField ShowSelectButton="true" SelectText="Select" /></Columns>
        <EmptyDataTemplate><center> Late Fees Record Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
   </div>
   </div></div>
   <br />
   <div class="rightpanel"><center><h3> Beneficiary IM</h3></center>
   <br />
   <center>Select IM:&nbsp;&nbsp;<asp:TextBox ID="txtBIM" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtBIM_ONTextChanged"></asp:TextBox></center>
   <asp:Label ID="lblBName" runat="server" Font-Bold="true"></asp:Label><br />
   <asp:Label ID="lblBAddress" runat="server" ></asp:Label><br />
   <asp:Label ID="lblBAddress2" runat="server" ></asp:Label><br />
   <asp:Label ID="lblBcity" runat="server" ></asp:Label><br /><hr />
   <br /><asp:Panel ID="pnlBene" runat=server Visible="false">
   <table><tr><td>Total Amount:</td><td><asp:Label ID="lblBTotal" runat="server" Font-Bold="true"></asp:Label></td></tr>
   <tr><td>Group Amount:</td><td><asp:Label ID="lblBGAmount" runat="server" ></asp:Label> </td></tr>
   <tr><td>Late Fees:</td><td><asp:Label ID="lblBLate" runat="server" ></asp:Label> </td></tr>
   </table></asp:Panel>

   <br />
   </div>
   <div class="leftpanel">
   <div class="underleftpanel"><center><h3>Donor IM</h3></center>
    <br />
   <center> &nbsp;&nbsp;<asp:Label ID="lblDIMID" runat="server" Font-Bold="true"></asp:Label></center>
   <asp:Label ID="lblDName" runat="server" Font-Bold="true"></asp:Label><br />
   <asp:Label ID="lblDAddress1" runat="server" ></asp:Label><br />
   <asp:Label ID="lblDAddress2" runat="server" ></asp:Label><br />
   <asp:Label ID="lblDCity" runat="server" ></asp:Label><br /><hr />
   <br /><asp:Panel ID="pnlDonor" runat=server Visible="false">
   <table ><tr><td>Total Amount:</td><td><asp:Label ID="lblDAmount" runat="server" Font-Bold="true"></asp:Label></td></tr>
   <tr><td>Group Amount:</td><td><asp:Label ID="lblDGAmount" runat="server" ></asp:Label> </td></tr>
   <tr><td>Late Fees:</td><td><asp:Label ID="lblDLateFees" runat="server"  ></asp:Label> </td></tr>
   </table></asp:Panel>

   <br />
   </div>
   </div><br />
    <asp:Label ID="lblDiaryNo" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblSession" runat="server" visible="false"></asp:Label><asp:Label
        ID="Subdate" runat="server" Visible="false"></asp:Label>
  <asp:Panel ID="pnlshow" runat=server Visible="false"> <center>
      <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label><br /><table><tr><td> <table><tr><td>DD No.</td><td><asp:Label ID="lblDDNo" runat="server" ></asp:Label></td></tr>
  <tr><td> Bank No.:</td><td><asp:Label ID="lblBankNo" runat="server" ></asp:Label></td></tr>
 <tr><td>  Date:</td><td><asp:Label ID="lblSubDate" runat="server" ></asp:Label></td></tr>
   <tr><td>Amount:</td><td><asp:Label ID="lblAmount" runat="server" ></asp:Label></td></tr></table>
   </td><td><asp:ImageButton ID="ibtnTransfer" runat="server" 
               ImageUrl="~/images/forward.png" onclick="ibtnTransfer_Click1" /></td></tr></table></center></asp:Panel>
  
</ContentTemplate></asp:UpdatePanel>
<br /><br />
</div><br />
</asp:Content>

