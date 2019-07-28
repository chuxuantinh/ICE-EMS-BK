<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="frontOffceHome.aspx.cs" Inherits="frontOffceHome" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Visitor Entry ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Front Office" CssClass="redirecttab" OnClick="lbtnRedirectAdmin_Click" Visible="false" 
             ></asp:LinkButton> </td><td><asp:Label ID="lblVisitorEntry" runat="server" Text="Visitor Entry" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2"><div id="header">
                           <div id="Div1" class="fromRegisterlbl" runat="server" ><h1>Visitor Entry</h1></div>
             <br /><table class="tbl">
           <tr><td>Full Name :</td><td><asp:TextBox ID="txtName" runat="server" 
                   CssClass="txtbox" Width="195px"></asp:TextBox>
           <asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic"  ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator>
           </td><td>
               &nbsp;</td></tr>

<tr><td>Phone:</td><td colspan="2"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                         runat="server" TargetControlID="txtPhonecode" FilterType="Numbers" />&nbsp;&nbsp;<asp:TextBox 
        ID="txtPhoneNo" runat="server" CssClass="txtbox" Width="130px"></asp:TextBox>
        <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                         runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers" />
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtPhoneNo" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Contact No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtPhoneNo" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td>
        </tr>
<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" 
        CssClass="txtbox" Width="195px"></asp:TextBox>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMobile" FilterType="Custom, Numbers"  ValidChars="+-=/*()."  >
               </dev:FilteredTextBoxExtender>
               <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td>
    <td>
        Email:&nbsp;&nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtEmail" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert EmailID" >*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Architecture"
            ErrorMessage="Insert Correct Email" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                     </td></tr>

<tr><td>Current Date:</td><td>
    <asp:TextBox ID="txtDate"  runat="server" 
        CssClass="txtbox" Width="165px"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate" Display="Dynamic"  ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
                    <dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" 
        PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                         ControlToValidate="txtDate" ErrorMessage="Invalid" 
                         ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                    &nbsp;<dev:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtDate" MaskType="Date" Mask="99/99/9999" runat="server">
   </dev:MaskedEditExtender>  
        </td>

    <td></td></tr>
                             
<tr><td>Purpose:</td><td>
    <asp:TextBox ID="Txtia" runat="server" Width="195px" CssClass="txtbox"></asp:TextBox></td></tr>
</table>

<table>
<tr><td class="style1">&nbsp;&nbsp;&nbsp; Comments:&nbsp;&nbsp;&nbsp; </td><td>
    <asp:TextBox ID="TxtComment" runat="server" Width="195px" TextMode="MultiLine" CssClass="txtbox" 
                                    Height="54px"></asp:TextBox></td></tr>
                                  
</table> <br /> 

<center>
    <asp:Label ID="lblexception" runat="server" Font-Bold="True" Font-Size="Medium" 
        ForeColor="Black" ></asp:Label>
<br />
<div>
    <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="Architecture" onclick="btnsave_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;<asp:Button
        ID="btncancel" runat="server" Text="Clear" onclick="btncancel_Click" CssClass="btnsmall" /></div></center>
<br />             <br />
<center>
             <asp:UpdatePanel ID="updatepanel" runat="server" ><ContentTemplate>
 
                            <br />
                            <script>
                                function toggleA1x(showHideDiv, switchImgTag) {
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
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div>
<div align="left" style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblGridTitle" runat="server"  ></asp:Label>
</div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px" 
            onscroll='javascript:setScroll(this, <% =scrollPos.ClientID %> );'>
            
<asp:GridView ID="GrvVisitorview" runat="server" PageSize="30"   
                                AllowPaging="True" 
                         onpageindexchanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_OnRowDataBound"
                            Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                    BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" 
                         onselectedindexchanged="GridView1_SelectedIndexChanged">
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                                                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                                            <FooterStyle BackColor="Tan" />
                                                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                                                HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                            <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                                            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                                            <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                                            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                                            </asp:GridView>
   </div>
   </div></div>
                    
  </ContentTemplate></asp:UpdatePanel>
  </center><br /><br />
            
             <br /><br />
 </div></div>
             <br /><br />
           </asp:Content>