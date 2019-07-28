<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamPaperSetter.aspx.cs" Inherits="Exam_ExamPaperSetter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exam Paper Setter</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>

<div id="rightpanel2">

<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ID No:-<asp:TextBox ID="txtID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;
<asp:Button ID="Button1" runat="server" CssClass="btnsmall"  Text="Show" OnClick="btnShowID_Click" /></h1><h1>
        PaperSetter Profile:</h1></div>

 
<center><asp:Label ID="lblException" runat="server" Font-Bold="true"></asp:Label></center>


<table class="tbl">
<tr><td>Exam Session:<td><asp:DropDownList ID="ddlExamSeason" CssClass="txtbox"  runat="server"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>
    Year:<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px">2012</asp:TextBox></td></tr>
<tr><td>Full Name :</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td><td colspan="2">Designation:&nbsp;
<asp:TextBox ID="txtDEsignation" runat="server" CssClass="txtbox" ></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDEsignation" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td></tr>


<tr><td>Permanent Address:</td><td colspan="3">
<asp:TextBox ID="txtPAddress" TextMode="MultiLine" Height="35px" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td>
 <td>State:<br />
     <asp:DropDownList ID="ddlPState" CssClass="txtbox" Width="150px"  
         runat="server" onselectedindexchanged="ddlPState_SelectedIndexChanged" 
         AutoPostBack="true">
     </asp:DropDownList>
 </td>
 <td>City:<br />
     <asp:DropDownList ID="ddlPCity" CssClass="txtbox"  Width="100px" 
         runat="server" >
     </asp:DropDownList>
 </td>
 
 
    <td>Pin:&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>


<tr><td>Correspondence Address:</td><td colspan="3">
<asp:TextBox ID="txtCAddress" TextMode="MultiLine" Height="35px" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr>

<tr><td></td>
 <td>State:<br />
     <asp:DropDownList ID="ddlCState" CssClass="txtbox" Width="150px"  
         runat="server" onselectedindexchanged="ddlCState_SelectedIndexChanged" 
         AutoPostBack="true">
     </asp:DropDownList>
 </td>
 <td>City:<br />
     <asp:DropDownList ID="ddlCCity" CssClass="txtbox"  Width="100px"  runat="server" >
     </asp:DropDownList>
 </td>
 
 
    <td>Pin:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCPin" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtCPin" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxEender2" runat="server" TargetControlID="txtCPin" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>

<tr><td>Phone:</td><td colspan="3"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>

<tr><td>Fax:</td><td colspan="3"><asp:TextBox ID="txtFaxCode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFaxNo" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFaxCode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFaxNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>


<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>

<tr><td>Date of Birth:</td><td>
<asp:TextBox ID="txtDOB" runat="server" CssClass="txtbox" 
        ontextchanged="txtDOB_TextChanged" AutoPostBack="True"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDOB" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date of Birth" >*</asp:RequiredFieldValidator>

   <dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDOB"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td>
<td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Age:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
        ID="txtAge" runat="server"></asp:Label>

</td></tr>
<tr>
<td colspan="3">Education Qualification&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox 
        ID="txtEducationQ" runat="server" CssClass="txtbox" Width="239px" Height="40px" 
        TextMode="MultiLine"></asp:TextBox></td>
</tr>
<tr>
<td colspan="3">Professional Experience&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtExperience" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
    &nbsp;Years</td>
</tr>
</table>

<br /><br /><center><asp:Label ID="lblExcepiton" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></center>

<br /><center><asp:Button ID="btnSave"  CssClass="btnsmall" runat="server" Text="Save" ValidationGroup="Architecture" OnClick="btnSave_click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUpdate" CssClass="btnsmall" runat="server" Text="Update" OnClick="btnUpdate_click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btbClear"  CssClass="btnsmall" runat="server" Text="Clear" OnClick="btnClear_Click" /></center><br /><br />

<div style="height:300px" ><br /></div>
</div>
</asp:Content>

