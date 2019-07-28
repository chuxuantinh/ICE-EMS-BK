<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="EditAdmission.aspx.cs" Inherits="Admission_EditAdmission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="contenttitle" runat="server" ContentPlaceHolderID="contenttitle">Edit Admission</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" />
<div style="float:right; margin-right:30px;">Membership No.:&nbsp;&nbsp;&nbsp;<asp:TextBox Width="100px" ID="txtEnrolment" runat="server" CssClass="txtbox" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnViewEnroll" runat="server"  CssClass="btnsmall" Text="View Profile"  OnClick="btnView_Click" /></div>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Edit Admission" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:20px;"><asp:Label ID="lblEnrolment" runat="server" /></h1><h1>Update Personal Profile</h1></div><br />
<center><asp:Label ID="lblStrm" Text="Stream:" runat="server" Visible="false" /><asp:Label ID="lblStream" runat="server" ForeColor="BlueViolet" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCor" runat="server" Visible="false" Text="Course:" /><asp:Label ID="lblCourse" runat="server"  ForeColor="BlueViolet" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server"  ForeColor="BlueViolet" Font-Bold="true" /></center>
<asp:ValidationSummary ID="VSummary1" CssClass="expbox" runat="server" DisplayMode="BulletList" ValidationGroup="Architecture" ForeColor="Red" />
<center><asp:Label ID="lblmessage" runat="server" ForeColor="Red" /></center>
<table class="tbl" width="95%"><tr><td>Name:</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox" /><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td>
    <td align="right"><asp:Label ID="lblApp" runat="server" Visible="false" Text="Application Form No.:" /></td><td><asp:Label ID="txtAppNo" runat="server" ForeColor="BlueViolet" /></td></tr>
<tr><td><asp:DropDownList ID="ddlPrefix" runat="server" CssClass="txtbox" >
    <asp:ListItem>s/o</asp:ListItem>
    <asp:ListItem>w/o</asp:ListItem>
    <asp:ListItem>d/o</asp:ListItem>
    </asp:DropDownList>
    </td><td><asp:TextBox ID="txtFather" runat="server" CssClass="txtbox" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFather" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Father Name">*</asp:RequiredFieldValidator></td>
    <td align="right">Mother's Name:</td><td><asp:TextBox ID="txtMother" runat="server" CssClass="txtbox" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMother" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate's Mother Name">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Permanent Address:</td><td colspan="3"><asp:TextBox ID="txtPAddress" runat="server" CssClass="txtbox" Width="60%" MaxLength="45" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtPAddress2" runat="server" CssClass="txtbox" Width="60%" MaxLength="45" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPAddress2" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address 2">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtPCity" runat="server" CssClass="txtbox" Width="60%" MaxLength="45" /></td></tr>
<tr><td>State:</td><td><asp:DropDownList ID="ddlState" runat="server" CssClass="txtbox"  Width="151px" /></td>
<tr><td>Local Address:</td><td colspan="3">
<asp:TextBox ID="CAddress" runat="server" Width="60%" CssClass="txtbox" MaxLength="45" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="CAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3">
<asp:TextBox ID="txtCAddress2" runat="server" Width="60%" CssClass="txtbox" MaxLength="45" /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCAddress2" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address 2">*</asp:RequiredFieldValidator>
</td></tr>
<tr><td>&nbsp;</td><td colspan="3"><asp:TextBox ID="txtCCity" runat="server" CssClass="txtbox" Width="60%" MaxLength="45" /></td></tr>
<tr><td>State:</td><td><asp:DropDownList ID="ddlState2" runat="server" CssClass="txtbox" Width="151px" /></td>
<tr><td>Phone:</td><td><asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtPhoneNo" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
<td align="right">Gender:</td><td><asp:DropDownList ID="ddlgender" runat="server" CssClass="txtbox"><asp:ListItem>Male</asp:ListItem><asp:ListItem>Female</asp:ListItem>
</asp:DropDownList>
</td></tr>
<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox" /><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers" /></td>
<td align="right">Email:</td><td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox" /></td></tr>
<tr><td>Date of Birth:</td><td><asp:TextBox ID="txtDOB" runat="server" CssClass="txtbox" /><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDOB" /><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDOB" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date of Birth" >*</asp:RequiredFieldValidator>&nbsp; <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td>
<td align="right">Age:</td><td><asp:TextBox ID="txtAge" runat="server" CssClass="txtbox" Width="40px" /><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtAge" FilterType="Numbers" /></td></tr>
<tr><td></td><td colspan="2"><asp:RegularExpressionValidator ID="RegExpreValid" runat="server" ControlToValidate="txtDOB" ErrorMessage="Enter Date in correct Format.!" ValidationExpression="[0-3][0-9]/[0-1][0-9]/[1-2][0-9][0-9][0-9]" /></td></tr>
<tr><td>Nationality:</td><td><asp:DropDownList ID="ddlNationality" runat="server"  Width="152px" CssClass="txtbox"></asp:DropDownList><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="ddlNationality" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Select Nationality." >*</asp:RequiredFieldValidator></td>
<td align="right">Category:</td><td><asp:DropDownList ID="ddlCategory" runat="server" CssClass="txtbox" Width="150px"><asp:ListItem Value="General" Text="General"></asp:ListItem><asp:ListItem Value="OBC" Text="OBC" /><asp:ListItem Value="SC" Text="SC" /><asp:ListItem Value="ST" Text="ST" /><asp:ListItem Value="PH" Text="PH"></asp:ListItem><asp:ListItem Value="Other" Text="Others"></asp:ListItem></asp:DropDownList><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="ddlCategory" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Select Category." >*</asp:RequiredFieldValidator></td></tr>
<tr><td>Remarks :</td><td>
<asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="38px" /></td>
<td align="right">Exmp Remarks :</td><td>
<asp:TextBox ID="txtExmpRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="37px" /></td></tr>
<tr><td>Admission Status:</td><td><asp:DropDownList ID="ddlAdmissionStatus" runat="server" CssClass="txtbox"><asp:ListItem Value="select" Text="Direct" /><asp:ListItem Value="Direct" Text="Direct" /><asp:ListItem Value="Regular" Text="Regular" /></asp:DropDownList></td></tr>
<tr><td colspan="2"><asp:CheckBox ID="chkExp" Text=" Experience" runat="server" Font-Bold="true" /></td>
<td colspan="2"><asp:CheckBox ID="chkDoc" runat="server" Text="Original Documents" Font-Bold="true" /></td></tr>
</table>
<br />
<center><asp:Label ID="lblProfileExceptioN" runat="server" ForeColor="Green" />
<br /><br />
<asp:Button ID="btnSaveProfile" runat="server" Text="Update" ValidationGroup="Architecture" CssClass="btnsmall" OnClick="btnUPdateProfile_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCleaer" runat="server" Text="Refresh" OnClick="btnClear_Click" CssClass="btnsmall" /></center>
<br />
</div>
</asp:Content>