<%@ Page Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="ImRegiUpdate.aspx.cs" Inherits="Administrator_ImRegiUpdate" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="float:right; margin-right:50px;">Insert IM ID:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="btnViewEnroll" runat="server" Text="View Profile" 
        onclick="btnViewEnroll_Click"  CssClass="btnsmall"  /></div>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            ></asp:LinkButton> </td></tr></table></div><br /> 
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Registration</h1></div><br /><center><asp:Label ID="lblTitleInfo" runat="server" ForeColor="Red"></asp:Label></center>
 <div class="rightbox1">
<asp:Panel  runat="server" ID="panelforgroup">
<asp:Label ID="lblmesg" runat="server"  Font-Size="14px" ForeColor="Maroon"> </asp:Label>
<asp:Label ID="lblgroupid" runat="server" Text="Group ID"></asp:Label>
<center><asp:Label ID="lblGInfo" runat="server"></asp:Label></center><br />
</asp:Panel> </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
 <table  class="tbl">
 <tr><td>Name of Institute:</td><td colspan="3"><asp:TextBox ID="txtName" runat="server" CssClass="txtbox" Width="290px" Font-Bold="true" Font-Size="16px" Height="25px"></asp:TextBox><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td></tr>
 
 
<tr><td>Permanent Address:</td><td colspan="3"><asp:TextBox ID="txtPAddress" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtAddress2" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td></td><td>State:<br /><asp:DropDownList 
            ID="ddlState" runat="server" 
            onselectedindexchanged="ddlState_SelectedIndexChanged" 
        AutoPostBack="true" CssClass="txtbox">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlState" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert State Name">*</asp:RequiredFieldValidator>



    </td>
    <td>City:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlcity" 
        runat="server" onselectedindexchanged="ddlcity_SelectedIndexChanged" 
            AutoPostBack="true" CssClass="txtbox">
    </asp:DropDownList>   
    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddlcity" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage=" Insert City Name">*</asp:RequiredFieldValidator>
        </td>
<td>Pin Code:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPPincode" runat="server" 
        CssClass="txtbox" ontextchanged="txtPPincode_TextChanged" 
        AutoPostBack="True"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td></td>
    <td> 
        </td><td><asp:Label ID="lblcity" runat="server">Other City</asp:Label><br />
        <asp:TextBox ID="txtothercity" runat="server" CssClass="txtbox" 
            AutoPostBack="True" ontextchanged="txtothercity_TextChanged"></asp:TextBox></td>
    <td class="style1"></td></tr>
<tr><td></td><td><asp:CheckBox ID="chkSameAddress" runat="server" 
        Text="Both Address Are Same"   AutoPostBack="true"
         /></td></tr>
<tr><td>Correspondence Address:</td><td colspan="3"><asp:TextBox ID="txtCAddress" TextMode="MultiLine" Height="35px" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td></td>
<td>State:<br /><asp:DropDownList 
            ID="ddlstate1" runat="server" 
            onselectedindexchanged="ddlstate1_SelectedIndexChanged" 
        AutoPostBack="true" CssClass="txtbox">
        </asp:DropDownList>


    </td><td>City:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlcity1" runat="server" 
        onselectedindexchanged="ddlcity1_SelectedIndexChanged" AutoPostBack="true" 
            CssClass="txtbox">
    </asp:DropDownList>
    </td><td>Pin Code:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCPin" runat="server" 
            CssClass="txtbox" AutoPostBack="True" ontextchanged="txtCPin_TextChanged"></asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtCPin" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxEender2" runat="server" TargetControlID="txtCPin" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>

    <tr><td></td>
    <td> 
        </td><td><asp:Label ID="lblOtherCITy" runat="server">Other City</asp:Label><br />
        <asp:TextBox ID="txtOther" runat="server" CssClass="txtbox" 
            AutoPostBack="True" ontextchanged="txtOther_TextChanged" ></asp:TextBox></td>
    <td class="style1"></td></tr>
<tr><td>Phone:</td><td colspan="3"><asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>

<tr><td>Fax:</td><td colspan="3"><asp:TextBox ID="txtFaxNo" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFaxNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>


<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>
     </table>  </ContentTemplate>
    </asp:UpdatePanel><br />
     <div id="Div1" class="fromRegisterlbl" runat="server" ><h1>Location of Center:</h1></div>
   <br />
    <table class="tbl"><tr><td>
        Remote area:&nbsp;</td><td><asp:RadioButton ID="chkRArea" runat="server" /></td> <td >
        Easily Accessible:&nbsp;</td><td><asp:RadioButton ID="chkEAccessible" runat="server" /></td></tr>
  <tr><td>
      Residential Area:&nbsp;</td><td><asp:RadioButton ID="chkResidential" runat="server" /></td> <td >
      Commercial Area&nbsp;</td><td><asp:RadioButton ID="chkCommercial" runat="server" /></td></tr>
      
      <tr><td>
     Within the City &nbsp;</td><td><asp:RadioButton ID="chkWCity" runat="server" /></td> <td>
      Outskirts of the City&nbsp;</td><td><asp:RadioButton ID="chkOCity" runat="server" /></td></tr></table><br /><table class="tbl">
<tr><td>Distance from Railway Stn.</td><td><asp:TextBox ID="txtDRStn" runat="server" Width="106px" CssClass="txtbox" ></asp:TextBox>Kms</td> <td>Name of the city</td><td><asp:TextBox ID="txtNCity" runat="server" CssClass="txtbox" Width="119px" ></asp:TextBox>
    </td></tr><tr><td>Distance from bus stop</td><td ><asp:TextBox ID="txtBStop" runat="server" Width="106px" CssClass="txtbox"  ></asp:TextBox>Kms</td> <td >Name of the area</td><td ><asp:TextBox ID="txtNArea" runat="server" CssClass="txtbox" Width="119px"  ></asp:TextBox>
    </td></tr>
  <tr><td >Year of Establishment</td><td><asp:TextBox ID="txtYEstablishment" runat="server" Width="106px" CssClass="txtbox" ></asp:TextBox>
      </td></tr> <tr><td style="width: 160px">Acedemic Status of Institution</td><td> <asp:TextBox ID="txtASInstitution" runat="server" Width="107px"  CssClass="txtbox" ></asp:TextBox>
              </td></tr><tr><td>Type of Institution govt.</td><td >
      <asp:DropDownList ID="txttypeig" runat="server" Width="154px" CssClass="txtbox" >
       
          <asp:ListItem>Central</asp:ListItem>
          <asp:ListItem>State</asp:ListItem>
          <asp:ListItem>U.T. Trust</asp:ListItem>
          <asp:ListItem>Society</asp:ListItem>
          <asp:ListItem>Private</asp:ListItem>
          <asp:ListItem></asp:ListItem>
      </asp:DropDownList>
              </td></tr>
      <tr><td>Courses being conducted presently<br />are recognized by </td><td><asp:TextBox ID="txtCRecognizedby" runat="server" CssClass="txtbox" ></asp:TextBox>
          </td></tr><tr><td style="height: 40px; width: 160px;">No. of Students at present</td><td>
          <asp:TextBox ID="txtNSPresent" runat="server" CssClass="txtbox" ></asp:TextBox>
          </td></tr>
   </table><asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="Architecture" CssClass="expbox" />
<br /><center>        <br />  <asp:Button ID="btnSave" runat="server" Text="Update"  
           OnClick="btnSave_Click" style="height:26px;" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel"
                  runat="server" Text="Cancel" CssClass="btnsmall" /><asp:Label ID="lblerror" runat="server" Text="" ForeColor="Maroon"></asp:Label></center>
       
        
        <br /><br />
        
    
 </div>  
 <br /> 
</asp:Content>

