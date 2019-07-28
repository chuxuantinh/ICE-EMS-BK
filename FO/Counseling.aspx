<%@ Page Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="Counseling.aspx.cs" Inherits="FO_Counseling" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Counselling ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td>
        <td><asp:Label ID="lblCounselling" runat="server" Text="Counselling" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1><div style="float:right; margin-right:30px;">Counselling No.:&nbsp;<asp:Label ID="lblCID" runat="server" ></asp:Label></div>Counselling Department</h1></div>
           <br /><center>Session:&nbsp;&nbsp;&nbsp;
              <asp:DropDownList ID="txtsession" CssClass="txtbox" runat="server">
              <asp:ListItem Value="Win" Text="Winter Examination"></asp:ListItem><asp:ListItem Value="Sum" Text="Summer Examination"></asp:ListItem></asp:DropDownList> <asp:TextBox ID="txtsessionyear"  Width="100px"  runat="server" CssClass="txtbox" ></asp:TextBox></center>
           <table class="tbl" width="95%">
           <tr><td>Name of Counselor:&nbsp;<asp:TextBox ID="txtcounselor" Width="150px" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtcounselor" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Counselor Name">*</asp:RequiredFieldValidator></td></tr></table>
<hr /><br />
<table class="tbl">
<tr>
<td>Student Name :</td>
<td>
  <asp:TextBox ID="txtname" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtname" Display="Dynamic"  ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td>             
</tr>
<tr>
<td>Course :</td>
<td>
               <asp:DropDownList ID="ddlCourse" Width="59%"  AutoPostBack="true" CssClass="txtbox"
                   runat="server" onselectedindexchanged="DdlCourse_SelectedIndexChanged">
               <asp:ListItem Value="Civil" Text="Civil Engineering"></asp:ListItem>
               <asp:ListItem Value="Architecture" Text="Architecture Engineering"></asp:ListItem>
               <asp:ListItem Value="AutoCAD" Text="AutoCAD "></asp:ListItem>
               </asp:DropDownList>
               </td>
<td>
                   <asp:Label ID="lblPart" runat="server" Text="Part:"></asp:Label>
                   &nbsp;</td>
<td><asp:DropDownList ID="ddlPart"  runat="server" CssClass="txtbox"
                       onselectedindexchanged="ddlPart_SelectedIndexChanged" Width="85%">
               <asp:ListItem Value="PartI" Text="PartI"></asp:ListItem>
               <asp:ListItem Value="PartII" Text="PartII"></asp:ListItem>
               <asp:ListItem Value="SectionA" Text="SectionA"></asp:ListItem>
               <asp:ListItem Value="SectionB" Text="SectionB"></asp:ListItem>
               </asp:DropDownList>
                   </td>
</tr>

<tr>
<td>Contact No. :</td>
<td>
               <asp:TextBox ID="txtcontact" runat="server" CssClass="txtbox"></asp:TextBox>
               <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                         runat="server" TargetControlID="txtcontact" FilterType="Numbers" />
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="txtcontact" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Contact No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtcontact" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td>
<td>Mobile:</td>
<td><asp:TextBox ID="txtmobile" runat="server" CssClass="txtbox"></asp:TextBox>
                   <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                         runat="server" TargetControlID="txtmobile" FilterType="Numbers" />
                          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td>
</tr>
<tr>
<td>Email :</td>
<td>
               <asp:TextBox ID="txtemail" runat="server" CssClass="txtbox"></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Correct Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                     </td>
<td>Date:&nbsp;</td>
<td><asp:TextBox ID="txtdate" runat="server" CssClass="txtbox"></asp:TextBox>
                                     <dev:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtDate" MaskType="Date" Mask="99/99/9999" runat="server">
   </dev:MaskedEditExtender>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtdate"></dev:CalendarExtender><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td>
</tr>
<tr>
<td>Address :</td>
<td colspan="3">
    <asp:TextBox ID="txtaddress" 
       runat="server" CssClass="txtbox" Width="250px"></asp:TextBox></td>
</tr>
<tr>
<td></td>
<td colspan="3">
    <asp:TextBox ID="txtaddress2" 
       runat="server" CssClass="txtbox" Width="250px"></asp:TextBox></td>
</tr>
<tr>
<td>State:</td>
<td>
     <asp:DropDownList ID="ddlState" CssClass="txtbox" Width="59%"  runat="server" 
         onselectedindexchanged="ddlState_SelectedIndexChanged" 
         AutoPostBack="true" >
     </asp:DropDownList>
    </td>
<td>City:&nbsp;&nbsp;</td>
<td> 
    <asp:DropDownList ID="ddlCity" CssClass="txtbox"  
         Width="85%" runat="server" >
     </asp:DropDownList>
    </td>
</tr>
<tr>
<td>PinCode:</td>
<td><asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                         runat="server" TargetControlID="txtPPincode" FilterType="Numbers" /><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td>
<td></td>
<td></td>
</tr>
<tr>
<td>Follow Up Details :</td>
<td>
          <asp:TextBox ID="txtdetail" runat="server" TextMode="MultiLine" Width="250" Height="50" CssClass="txtbox"></asp:TextBox></td>
<td></td>
<td></td>
</tr>
<tr>
<td>Current Status :</td>
<td>
             <asp:DropDownList  Font-Size="12px" Font-Bold="true" ID="ddlResponse" CssClass="txtbox"
                 runat="server" Width="59%"  AutoPostBack="true" 
                 OnSelectedIndexChanged="ddlResponse_SeelctedIndexChanged"><asp:ListItem Value="Positive" Text="Positive" /><asp:ListItem Value="Negative" Text="Negative" /><asp:ListItem Value="Normal" Text="Normal" /></asp:DropDownList></td>
<td>Next Followup:</td>
<td><asp:TextBox ID="txtNextDate" runat="server" CssClass="txtbox" Width="100px" ></asp:TextBox>
               <dev:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtNextDate" MaskType="Date" Mask="99/99/9999" runat="server">
   </dev:MaskedEditExtender>
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="calimg" PopupPosition="BottomRight" runat="server" TargetControlID="txtNextDate"></dev:CalendarExtender><img src="../images/cal.png" id="calimg" runat="server"  alt="Cal" /> 
             </td>
</tr>

</table>
           <br />
           <center>
               <asp:Label ID="lblexception" runat="server"  ForeColor="Red" Font-Bold="True" 
                   Font-Size="Medium"></asp:Label><br />
               <asp:Button ID="btnsave" runat="server" CssClass="btnsmall" Text="Save"  ValidationGroup="Architecture" onclick="btnsave_Click" />&nbsp;&nbsp;
               <asp:Button ID="btncancel"
                   runat="server" Text="Clear" CssClass="btnsmall" onclick="btncancel_Click" />
               </center><br />
                        </div>
             </div>
      </asp:Content>
