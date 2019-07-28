<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="IMRegi.aspx.cs" Inherits="Administrator_IMRegi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="float:right; margin-right:50px; padding-bottom:0px;">Insert IM ID:&nbsp;&nbsp;&nbsp;<asp:TextBox 
        ID="txtEnrolment" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnViewEnroll" runat="server" Text="View Profile"  OnClick="btnView_Click" CssClass="btnsmall" /></div>
<br /> 
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Registration</h1></div><br />
 <asp:UpdatePanel ID="UpdatePanelgrop" runat="server" ><Triggers><asp:PostBackTrigger ControlID="lbtnIMHeadTitel" /></Triggers><ContentTemplate><div class="rightbox" style="width:30%; padding:0px;">
<asp:Panel runat="server" ID="panelforgroup">
<center><asp:RadioButton ID="rbtnAlone" runat="server" Text="Single"  GroupName="d"
        AutoPostBack="true" oncheckedchanged="rbtnAlone_CheckedChanged"  Checked="true"/>&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton 
        ID="rbtnGroup" runat="server" AutoPostBack="true" Text="InGroup" GroupName="d" 
        oncheckedchanged="rbtnGroup_CheckedChanged" /><br />
<table id="grouptable"  runat="server" ><tr><td>Reference IM Id:&nbsp;</td></tr><tr><td>
    <asp:TextBox ID="txtRefIM" runat="server" CssClass="txtbox" ></asp:TextBox></td></tr><tr><td><asp:LinkButton ID="lbtnViewGrup" runat="server" Text="View Group Id" OnClick="lbtnViewGrup_click"></asp:LinkButton></td></tr></table></center>
<center><asp:Label ID="lblGInfo" runat="server"></asp:Label></center><br />
</asp:Panel> </div>
 <center><asp:Label ID="lblRegisterTitle" runat="server"  ForeColor="Green" Font-Bold="true" Text="Register"></asp:Label>&nbsp;&nbsp;>>&nbsp;&nbsp;<asp:LinkButton ID="lbtnIMHeadTitel" runat="server" ForeColor="Gray" Font-Bold="true" OnClick="lbtnIMHeadTitle_Click" Text="IM Head"></asp:LinkButton>&nbsp;&nbsp;>>&nbsp;&nbsp;<asp:Label ID="lblBuildingTitle" runat="server"  ForeColor="Gray" Font-Bold="true" Text="
 Building Details"></asp:Label><br /><asp:Label ID="lblTitleInfo" runat="server" ForeColor="Red"></asp:Label></center><br /><br />
 <br />
 <table  class="tbl">
 <tr><td>Session:</td><td><asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox"><asp:ListItem Value="Sum" Text="Summer Examination"></asp:ListItem><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList></td><td>
     <asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="58px"></asp:TextBox></td></tr>
 <tr><td>Name of Institute:</td><td colspan="2"><asp:TextBox ID="txtName" 
         runat="server" CssClass="txtbox" Width="350px" Font-Bold="true" 
         Font-Size="18px" Height="25px"></asp:TextBox><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td><td></td></tr>
<tr><td>Permanent Address:</td><td colspan="3"><asp:TextBox ID="txtPAddress" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtAddress2" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td>
City:</td><td> 
        <asp:TextBox ID="ddlCity" runat="server" CssClass="txtbox" Width="155px"></asp:TextBox>
    </td>
    <td>&nbsp;</td></tr>
<tr><td>State:</td><td><asp:DropDownList ID="ddlState" runat="server" 
        CssClass="txtbox" Width="157px">
        </asp:DropDownList> <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlState" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert State Name">*</asp:RequiredFieldValidator></td>
        <td> PinCode:
  <asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td colspan="3"></td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td>
             <asp:CheckBox ID="chkSameAddress" runat="server" AutoPostBack="true" 
                 OnCheckedChanged="chkSameAddress_CheckChanged" Text="Both Address are Same" />
         </td>
         <td>
             &nbsp;</td>
     </tr>
<tr><td>Correspondence Address:</td><td colspan="3"><asp:TextBox ID="txtCAddress" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td>City:</td><td colspan="3"><asp:TextBox ID="txtCCity" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr><tr>
<td>State:</td><td><asp:TextBox ID="txtCState" runat="server" CssClass="txtbox" 
         Width="157px" ></asp:TextBox></td><td>PinCode: <asp:TextBox ID="txtCPin" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtCPin" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxEender2" runat="server" TargetControlID="txtCPin" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Phone:</td><td colspan="3"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhoneNo" Width="150px" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Fax:</td><td colspan="3"><asp:TextBox ID="txtFaxCode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFaxNo" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFaxCode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFaxNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" 
        CssClass="txtbox" Width="155px"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td><td colspan="2">
    Email:&nbsp;&nbsp; &nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>
    <tr><td>Letter Issue Date:</td><td><asp:TextBox ID="txtDate"  runat="server" 
            CssClass="txtbox" Width="155px"></asp:TextBox></td></tr>
     </table>  <br /></ContentTemplate></asp:UpdatePanel>
     <div class="fromRegisterlbl" runat="server" ><h1>Location of Center:</h1></div>
   <br />
    <table class="tbl"><tr><td>
        &nbsp;<asp:RadioButton GroupName="A1" ID="chkRArea" Text="Remote Area" runat="server"  /></td> <td >
        &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;<asp:RadioButton GroupName="A1" Text="Easily Accessible" ID="chkEAccessible" runat="server" /></td></tr>
  <tr><td>
      &nbsp;<asp:RadioButton GroupName="B" Text="Residential Area" ID="chkResidential" runat="server" /></td> <td >
      &nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;<asp:RadioButton GroupName="B" Text="Commercial Area" ID="chkCommercial" runat="server" /></td></tr>
      
      <tr><td>
      &nbsp;<asp:RadioButton GroupName="Civil" Text="Within the City" ID="chkWCity" runat="server" /></td> <td>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:RadioButton GroupName="Civil" Text="Outskirts of the City" ID="chkOCity" runat="server" /></td></tr></table><br /><table class="tbl">
<tr><td>Distance from Railway Stn.:</td><td><asp:TextBox ID="txtDRStn" runat="server" Width="106px" CssClass="txtbox" ></asp:TextBox>Kms</td> <td>Name of the city</td><td><asp:TextBox ID="txtNCity" runat="server" CssClass="txtbox" Width="119px" ></asp:TextBox>
    </td></tr><tr><td>Distance from bus stop:</td><td ><asp:TextBox ID="txtBStop" runat="server" Width="106px" CssClass="txtbox"  ></asp:TextBox>Kms</td> <td >Name of the area</td><td ><asp:TextBox ID="txtNArea" runat="server" CssClass="txtbox" Width="119px"  ></asp:TextBox>
    </td></tr>
  <tr><td >Year of Establishment:</td><td><asp:TextBox ID="txtYEstablishment" runat="server" Width="106px" CssClass="txtbox" ></asp:TextBox>
      </td></tr> <tr><td style="width: 151px">Acedemic Status of Institution:</td><td> <asp:TextBox ID="txtASInstitution" runat="server" Width="107px"  CssClass="txtbox" ></asp:TextBox>
              </td></tr><tr><td>Type of Institution govt.:</td><td >
      <asp:DropDownList ID="txttypeig" runat="server" Width="140px" CssClass="txtbox" >
       
          <asp:ListItem>Central</asp:ListItem>
          <asp:ListItem>State</asp:ListItem>
          <asp:ListItem>U.T. Trust</asp:ListItem>
          <asp:ListItem>Society</asp:ListItem>
          <asp:ListItem>Private</asp:ListItem>
          <asp:ListItem></asp:ListItem>
      </asp:DropDownList>
              </td></tr>
      <tr><td>Courses being conducted presently<br />are recognized by:</td><td><asp:TextBox ID="txtCRecognizedby" runat="server" CssClass="txtbox" ></asp:TextBox>
          </td></tr><tr><td style="height: 40px; width: 160px;">No. of Students at present:</td><td>
          <asp:TextBox ID="txtNSPresent" runat="server" CssClass="txtbox" ></asp:TextBox>
          </td></tr>
          <tr><td>Account Approval Limit:</td><td><asp:TextBox ID="txtAcApprovalLimit" runat="server" CssClass="txtbox"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtAcApprovalLimit" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Approval Limit." >*</asp:RequiredFieldValidator>
          <br />Enter minimum amount that accountant can give approval of application form.</td></tr>
   </table><asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="Architecture" CssClass="expbox" />
<br /><center>        <asp:Label ID="lblException" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label> <br /><asp:Button ID="btnSave" runat="server" Text="Register"  ValidationGroup="Architecture" OnClick="btnSave_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel"
                  runat="server" CssClass="btnsmall" Text="Next" OnClick="btnCancel_Click" /></center>
        <br /><br />
       <b>Note:&nbsp;</b>Make sure that IM Name is unique.
 </div>  
</asp:Content>

