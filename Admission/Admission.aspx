<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="Admission.aspx.cs" Inherits="Admission_Admission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">New Student Admission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    function limitText(limitField, limitNum) {
        if (limitField.value.length > limitNum) {
            limitField.value = limitField.value.substring(0, limitNum);
        }
    }
</script>
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" />
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Admission" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">                    
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"><asp:Label ID="lblEnrolment" runat="server" /></h1><h1>New Admission</h1></div><br />
<asp:ValidationSummary ID="VSummary1" CssClass="expbox" runat="server" DisplayMode="BulletList" ValidationGroup="Architecture" ForeColor="Red" /><center>
<asp:Label ID="lblAppNameLabel" runat="server" Text="Serial No." />&nbsp;&nbsp;<asp:TextBox ID="txticesn" runat="server" CssClass="txtbox" Width="100px" /> &nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged1" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" Text=" OK " OnClick="btnOK_clicck" CssClass="btnsmall" /><br /><asp:Label ID="lblExceptionApp" runat="server" ForeColor="Red" /><asp:Label ID="lblLavelApp" runat="server" Visible="false" />
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false" /><asp:Label ID="lblFeeLevel" runat="server" /></center><hr />
<asp:Panel ID="pnlAdmission" runat="server" >
<asp:UpdatePanel ID="updatepanleIM" runat="server" ><ContentTemplate>
 <script>
     function toggleA3x(showHideDiv, switchImgTag) {
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
    </script><div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA3x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Send For Re-Approval</h1>
<div id="Div1x" style="display:none;">
  <input id="Hidden1" runat="server" type="hidden" value="0" /> <div id="div2" style="width: 90%; height:150px;">
<center><br />Membership Fees:&nbsp;<asp:Label ID="lblEnrollFee" runat="server" Font-Bold="true" /></center>
 <asp:Label ID="lblstream" runat="server" Visible="false" /><asp:Label ID="lblcourse" runat="server" Visible="false" /><asp:Label ID="lblPart" runat="server" Visible="false" />
 <center><asp:Panel ID="PanelApptoApp" runat="server" Width="100%">
<br />
<center><asp:Label ID="lblExceptionEli" runat="server" Font-Bold="true" /></center>Select New Course:<asp:DropDownList CssClass="txtbox" ID="ddlCourse" runat="server">
<asp:ListItem Value="Civil">Civil Engineering</asp:ListItem>
<asp:ListItem Value="Architecture">Architecture Engineering</asp:ListItem>
</asp:DropDownList>
Part:<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox">
<asp:ListItem Value="PartI">PartI</asp:ListItem>
<asp:ListItem Value="PartII">PartII</asp:ListItem>
<asp:ListItem Value="SectionA">SectionA</asp:ListItem>
<asp:ListItem Value="SectionB">SectionB</asp:ListItem>
</asp:DropDownList>
<br /><br />Comments:<asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="txtbox" />
<asp:Button ID="btnSendForApprove" runat="server" Text="Send" OnClick="btnSendForApproval_Onclick" CssClass="btnsmall"/>
 <asp:Panel ID="panelIM" runat="server" Visible="false" >
<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="15px" />
<asp:Label ID="lblIMAddress" runat="server" />
<asp:Label ID="lblIMCity" runat="server" />
<asp:Label ID="lblStstusActive" runat="server" /><b>
</asp:Panel>
</asp:Panel></center></div></div></div>
<asp:Panel ID="pnlIMIDapp" runat="server" Visible="false"><center>
<table runat="server" id="tblApp" class="tbl"><tr><td>Stream:</td><td><asp:Label ID="lblStreamApp" runat="server" Font-Bold="true" /></td><td>Course:&nbsp;&nbsp;&nbsp;</td><td><asp:Label ID="lblCourseApp" runat="server" Font-Bold="true" /></td></tr>
<tr><td>Part/Section:</td><td><asp:Label ID="lblPartApp" runat="server" Font-Bold="true" /></td><td>IM ID:&nbsp;&nbsp;</td><td><asp:Label ID="lblIMIDAPP" runat="server" Font-Bold="true" /></td></tr>
</table></center>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<asp:UpdatePanel runat="server"><ContentTemplate>
<table class="tbl" width="95%"><tr><td>Name:</td><td colspan="2">
<asp:TextBox ID="txtName" runat="server" CssClass="txtbox" /><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td>
</tr>
<tr><td><asp:DropDownList ID="ddlPrifix" runat="server" CssClass="txtbox" Width="120px" ><asp:ListItem Value="Father" Text="Father Name" /><asp:ListItem Value="Husband" Text="Husband Name" /></asp:DropDownList></td>
<td><asp:TextBox ID="txtFather" runat="server" CssClass="txtbox" /><asp:RequiredFieldValidator ID="RFieldVal1" runat="server" ControlToValidate="txtFather" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Father Name">*</asp:RequiredFieldValidator></td>
<td>Mother&#39;s Name:<asp:RequiredFieldValidator ID="RFieldVal2" runat="server" ControlToValidate="txtMother" Display="Dynamic" ErrorMessage="Please Insert Candidate's Mother Name" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
&nbsp;<asp:TextBox ID="txtMother" runat="server" Width="200px" CssClass="txtbox" /></td></tr>
<tr><td>Gender:&nbsp;&nbsp;</td><td><asp:DropDownList ID="ddlGender" runat="server" CssClass="txtbox" Width="92px"><asp:ListItem Value="Male" Text="Male"/><asp:ListItem Value="Female" Text="Female" /><asp:ListItem Value="Other" Text="Other" /></asp:DropDownList></td></tr>
<tr><td colspan="3"><b>Permanent Address</b></td></tr>
<tr><td>Address 1:</td><td colspan="2"><asp:TextBox ID="txtPAddress" MaxLength="45" runat="server" CssClass="txtbox"  Width="200px"/><asp:RequiredFieldValidator ID="RFieldVal3" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Address 2</td><td colspan="2"><asp:TextBox ID="txtPaddress2" runat="server" MaxLength="45" CssClass="txtbox" Width="200px" /><asp:RequiredFieldValidator ID="RFieldValc" runat="server" ControlToValidate="txtPAddress2" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address 2">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Address 3:</td><td colspan="2"><asp:TextBox ID="txtPCity" runat="server" CssClass="txtbox"  Width="200px"/></td></tr>
<tr><td>State:</td><td><asp:DropDownList ID="ddlState" runat="server" CssClass="txtbox"  Width="151px" /></td></tr>
<tr><td></td><td colspan="2"><asp:CheckBox ID="chkSameAddress" runat="server" Text="Both Address Are Same" AutoPostBack="true" OnCheckedChanged="chkSameAddress_CheckChanged" /></td></tr>
<tr><td colspan="3"><b>Correspondence Address:</b></td></tr>
<tr><td>Address 1:</td><td colspan="2"><asp:TextBox ID="CAddress" runat="server" CssClass="txtbox" Width="200px" />
<asp:RequiredFieldValidator ID="RFieldVal6" runat="server" ControlToValidate="CAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Address 2:</td><td colspan="2"><asp:TextBox ID="CAddress2" runat="server" CssClass="txtbox" Width="200px" /><asp:RequiredFieldValidator ID="RFieldVala" runat="server" ControlToValidate="CAddress2" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address 2">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Address 3:</td><td colspan="2"><asp:TextBox ID="txtCCity" runat="server" CssClass="txtbox"  Width="200px"/></td></tr>
<tr><td>State:</td><td><asp:DropDownList ID="ddlState2" runat="server" CssClass="txtbox" />
</td></tr><tr><td>Phone:</td><td><asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtPhoneNo" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers" /></td><td>Mobile:&nbsp;<asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox" /><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers" /></td></tr>
<tr><td>Email:</td><td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox" /></td></tr>
<tr><td></td><td colspan="2"><asp:Label ID="lblExceptDob" runat="server" Font-Bold="true" ForeColor="Red" /></td></tr>
<tr><td>Date of Birth:</td><td><asp:TextBox ID="txtDOB" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="txtDOB_OntextChanged" /> &nbsp;&nbsp;<img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
<dev:CalendarExtender ID="devdage" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtDOB" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" controltovalidate="txtDOB" Display="Dynamic" errormessage="Insert Date of Birth" ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td>
<td>&nbsp;Age:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAge" runat="server" CssClass="txtbox" Width="40px" />
<dev:FilteredTextBoxExtender ID="FiltxtExttxtAge" runat="server" FilterType="Numbers" TargetControlID="txtAge" /></td></tr>
<tr><td>Nationality:</td><td><asp:DropDownList ID="ddlNationality" runat="server"  Width="151px" CssClass="txtbox" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" controltovalidate="ddlNationality" Display="Dynamic" errormessage="Please Select Nationality." ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td>
<td>&nbsp;Category: <asp:DropDownList ID="ddlCategory" runat="server" CssClass="txtbox" Width="85px" ><asp:ListItem Value="General" Text="General"></asp:ListItem><asp:ListItem Value="OBC" Text="OBC" /><asp:ListItem Value="SC" Text="SC" /><asp:ListItem Value="ST" Text="ST" /><asp:ListItem Value="PH" Text="PH"></asp:ListItem><asp:ListItem Value="Other" Text="Others"></asp:ListItem> </asp:DropDownList><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="ddlCategory" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Select Category." >*</asp:RequiredFieldValidator></td></tr>
<tr>
<td>Admission Status:</td><td><asp:DropDownList ID="ddlAdmissionStatus" runat="server" CssClass="txtbox"><asp:ListItem Value="Regular" Text="Regular" /><asp:ListItem Value="Direct" Text="Direct" /></asp:DropDownList></td>
<td>Enrollment Date:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" runat="server" CssClass="txtbox" /><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender3" PopupButtonID="Img3" PopupPosition="BottomRight" runat="server" TargetControlID="txtEnrolment" />&nbsp;<img src="../images/cal.png" id="Img3" runat="server"  alt="Cal" /><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" controltovalidate="txtEnrolment" Display="Dynamic" errormessage="Insert Date of Birth" ValidationGroup="Architecture">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegExpreValid" runat="server" ControlToValidate="txtEnrolment" ErrorMessage="Enter Date in correct Format.!" ValidationExpression="[0-3][0-9]/[0-1][0-9]/[1-2][0-9][0-9][0-9]" /></td></tr>
<tr><td colspan="2"><asp:CheckBox ID="chkExp" Text="Experience" runat="server" Font-Bold="true" /></td>
<td><asp:CheckBox ID="chkDoc" runat="server" Text="Original Document" Font-Bold="true" /></td></tr>
</table>
<br /></ContentTemplate></asp:UpdatePanel>
<asp:UpdatePanel ID="updatepanel" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl">
<h1>Remarks:</h1>
</div>
<br />
<table class="tbl">
<tr><td>Remarks :</td>
<td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="38px" /></td>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Exmp Remarks :</td>
<td><asp:TextBox ID="txtExmpRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="37px" /></td></tr>
</table>
</ContentTemplate></asp:UpdatePanel>
<br /><br /><br /><asp:Label ID="lblException" runat="server" ForeColor="Brown" /><br />
<asp:Panel ID="panelMemberIDIS" runat="server" CssClass="generatedid">
<center><h1>Successfully Submitted<br /></h1></center><br />
</asp:Panel><br />
<center>
<asp:Button ID="btnSAVE" runat="server" Text="Save" onclick="btnSAVE_Click"  ValidationGroup="Architecture" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btnsmall" onclick="btnclear_Click" /></center>
<br />
</asp:Panel>
<asp:Panel ID="panleSpace" runat="server" Height="550px" />
</div>
</asp:Content>