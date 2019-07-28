<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="IMBuilding.aspx.cs" Inherits="Administrator_IMBuilding" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Building Info
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
        
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Building Infrastructure</h1></div><br />
 <div id="invisible" runat="server" style="height:500px;" ><center>Please Inter Student Enrolment No.in search box.</center></div>
 <div id="visisble" runat="server">
 

 <asp:UpdatePanel ID="updatepanel3" runat="server" ><ContentTemplate>
<center><asp:LinkButton ID="lblRegisterTitle" runat="server"  ForeColor="Green" Font-Bold="true" Text="Register" OnClick="lbtnRegistretitel_Click"></asp:LinkButton>&nbsp;&nbsp;<<&nbsp;&nbsp;<asp:LinkButton ID="lbtnIMHeadTitel" runat="server" ForeColor="Green" Font-Bold="true"  Text="IM Head" OnClick="lbbtnIMHeadTitel_Click"></asp:LinkButton>&nbsp;&nbsp;<<&nbsp;&nbsp;<asp:Label ID="lblBuildingTitle" runat="server"  ForeColor="Green" Font-Bold="true" Text="
 Building Details"></asp:Label><br /><asp:Label ID="lblTitleInfo" runat="server" ForeColor="Red"></asp:Label></center><br /><br />
</ContentTemplate></asp:UpdatePanel>

 <asp:UpdatePanel runat="server"><ContentTemplate>
 <table  class="tbl">
 <tr><td>Name of Center:</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox" Width="350px" Font-Bold="true" Font-Size="18px" Height="25px"></asp:TextBox><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td></tr>

<tr><td>Permanent Address:</td><td colspan="3"><asp:TextBox ID="txtPAddress" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtAddressHead2" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr></table><table class="tbl">
        <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td><td>State:<br /><asp:DropDownList ID="ddlstate" runat="server" 
        onselectedindexchanged="ddlstate_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlstate" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage=" Insert City Name">*</asp:RequiredFieldValidator></td><td>City:<br />
        <asp:DropDownList 
            ID="ddlcity" runat="server" 
            onselectedindexchanged="ddlcity_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcity" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert State Name">*</asp:RequiredFieldValidator></td><td>PinCode:<br /><asp:TextBox 
            ID="txtPPincode" runat="server" CssClass="txtbox" AutoPostBack="True" 
            ontextchanged="txtPPincode_TextChanged"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
            <tr><td></td>
    <td> 
        </td><td><asp:Label ID="lblcity" runat="server">Other City</asp:Label><br />
        <asp:TextBox ID="txtothercity" runat="server" CssClass="txtbox" 
            AutoPostBack="True" ontextchanged="txtothercity_TextChanged"></asp:TextBox></td>
    <td class="style1"></td></tr>

<%--
<tr><td>Correspondence Address:</td><td colspan="3"><asp:TextBox ID="txtCAddress" TextMode="MultiLine" Height="35px" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCCity" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCState" runat="server" CssClass="txtbox" ></asp:TextBox></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCPin" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtCPin" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxEender2" runat="server" TargetControlID="txtCPin" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>--%>


<tr><td>Phone:</td><td colspan="3"><asp:TextBox ID="txtPhonecode"  runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>

<tr><td>Fax:</td><td colspan="3"><asp:TextBox ID="txtFaxCode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFaxNo" Width="150px" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFaxCode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFaxNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>


<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator22" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>
     </table> </ContentTemplate></asp:UpdatePanel> <br />
 
     <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="abuild" runat="server" />
    
        <br />
      <div class="fromRegisterlbl"><h1>Existing Infrastructure</h1></div>
        <br />
        <table class="tbl"><tr><td>Name of Building:</td><td><asp:TextBox ID="txtBName" runat="server" CssClass="txtbox" Width="300px" Height="30px" Font-Bold="true"></asp:TextBox></td></tr>
        <tr><td>Type:</td><td><asp:DropDownList ID="ddlBType" runat="server" CssClass="txtbox" ><asp:ListItem Text="Owned" Value="Owned" /><asp:ListItem Text="Rented" Value="Rented" /><asp:ListItem Text="Leased" Value="Leased" /></asp:DropDownList></td></tr>
        <tr><td>Plinth Area:</td><td><asp:TextBox ID="txtBPArea" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;Sq.ft.</td><td>Covered Area:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCArea" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;Sq.ft.</td></tr>
        </table>
        
        
<br /><br />
<div class="fromRegisterlbl"><h1> Building Facilities Available</h1></div>
    
       
        <br />
        <table style="width:100%;">
      <tr style="text-align:center;"><td style="height: 23px; width: 76px;">Particulars</td><td style="height: 23px; width: 165px;">No. of Rooms
                </td>
                <td style="height: 23px; width: 170px;">
                    Seating Capacity</td>
                <td style="width: 163px; height: 23px">
                    Total Area (Sq.ft.)</td>
            </tr>
            <tr>
                <td style="height: 23px; width: 76px;">
                    Class Rooms</td>
                <td style="height: 23px; width: 165px;">
                    <asp:TextBox ID="txtBNRoom1" runat="server" ValidationGroup="abuild" BackColor="LavenderBlush" ></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ValidationGroup="abuild" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBNRoom1"
                        ErrorMessage="Enter Only Numbers" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" Display="Dynamic">*</asp:RegularExpressionValidator>
                </td>
                <td style="height: 23px; width: 170px;"><asp:TextBox ID="txtBSCapacity1" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="abuild" runat="server" ControlToValidate="txtBSCapacity1"
                        Display="Dynamic" ErrorMessage="Enter Only Numbers" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="width: 163px; height: 23px"><asp:TextBox ID="txtBTotal1" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBTotal1"
                        Display="Dynamic" ErrorMessage="Enter Only Numbers" ValidationGroup="abuild" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Labs</td>
                <td style="height: 23px; width: 165px;"><asp:TextBox ID="txtBNRoom2" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="abuild" runat="server" ControlToValidate="txtBNRoom2"
                        ErrorMessage="Enter Only Numbers" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="height: 23px; width: 170px;"><asp:TextBox ID="txtBSCapacity2" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="abuild" runat="server" ControlToValidate="txtBSCapacity2"
                        Display="Dynamic" ErrorMessage="Enter Only Numbers" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="width: 163px; height: 23px"><asp:TextBox ID="txtBTotal2" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="abuild" runat="server" ControlToValidate="txtBTotal2"  ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"
                        Display="Dynamic" ErrorMessage="Enter Only Numbers">*</asp:RegularExpressionValidator>
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Computer Lab</td>
                <td style="height: 23px; width: 165px;"><asp:TextBox ID="txtBNRoom3" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="abuild" runat="server" ControlToValidate="txtBNRoom3"
                        Display="Dynamic" ErrorMessage="Enter Only Numbers" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="height: 23px; width: 170px;"><asp:TextBox ID="txtBSCapacity3" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="abuild" runat="server" ControlToValidate="txtBSCapacity3"
                        Display="Dynamic" ErrorMessage="Enter Only Numbers"  ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="width: 163px; height: 23px"><asp:TextBox ID="txtBTotal3" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="abuild" runat="server" ControlToValidate="txtBTotal3"
                        Display="Dynamic" ErrorMessage="Enter Only Numbers"  ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Library</td>
                <td style="height: 23px; width: 165px;"><asp:TextBox ID="txtBNRoom4" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBNRoom4" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="height: 23px; width: 170px;"><asp:TextBox ID="txtBSCapacity4" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBSCapacity4" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="width: 163px; height: 23px"><asp:TextBox ID="txtBTotal4" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBTotal4" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Reception</td>
                <td style="height: 23px; width: 165px;"><asp:TextBox ID="txtBNRoom5" runat="server" ValidationGroup="abuild" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBNRoom5" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="height: 23px; width: 170px;"><asp:TextBox ID="txtBSCapacity5" runat="server" ValidationGroup="abuild" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBSCapacity5" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="width: 163px; height: 23px"><asp:TextBox ID="txtBTotal5" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBTotal5" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Admin Block</td>
                <td style="height: 23px; width: 165px;"><asp:TextBox ID="txtBNRoom6" runat="server" ValidationGroup="abuild" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBNRoom6" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="height: 23px; width: 170px;"><asp:TextBox ID="txtBSCapacity6" runat="server" ValidationGroup="abuild" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBSCapacity6" Display="Dynamic" ErrorMessage="Enter Only Number"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="width: 163px; height: 23px"><asp:TextBox ID="txtBTotal6" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" ValidationGroup="abuild" runat="server"
                        ControlToValidate="txtBTotal6" Display="Dynamic" ErrorMessage="Enter Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Any Other</td>
                <td style="height: 23px; width: 165px;"><asp:TextBox ID="txtBNRoom7" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                        ControlToValidate="txtBNRoom7" Display="Dynamic" ErrorMessage="Fill Only Numbers"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>&nbsp;
                </td>
                <td style="height: 23px; width: 170px;"><asp:TextBox ID="txtBSCapacity7" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                        ControlToValidate="txtBSCapacity7" Display="Dynamic" ErrorMessage="Fill Only Number"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
                <td style="width: 163px; height: 23px"><asp:TextBox ID="txtBTotal7" ValidationGroup="abuild" runat="server" BackColor="LavenderBlush"   ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                        ControlToValidate="txtBTotal7" Display="Dynamic" ErrorMessage="Fill Only Number"
                        ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)">*</asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <br /><br /><center><asp:Label ID="lblException" runat="server" ></asp:Label><br />
        <asp:Button ID="btnSave" runat="server" OnClick="Button1_Click" CssClass="btnsmall" ValidationGroup="abuild" Text="Submit" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCalcel" runat="server" CssClass="btnsmall" Text="Cancel"  OnClick="btnCencel_Click"/></center><br /><br />
 </div>
 </div>
</asp:Content>

