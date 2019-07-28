<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="ExamBilling.aspx.cs" Inherits="Acc_ExamBilling" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Examination Billing
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblExamBilling" runat="server" Text="Examination Billing" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Serial No.:&nbsp;<asp:Label ID="lblSN" runat="server" ForeColor="BlueViolet" Font-Bold="true"></asp:Label></b></h1><h1>Examination Billing </h1></div><br />
<asp:UpdatePanel ID="updatePanel2" runat="server" ><ContentTemplate>
<center>Exam Session:&nbsp;<asp:DropDownList ID="ddlSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlseasion_SelectedINdexChanged"><asp:ListItem Value="Win" Text="Winter Examination" /><asp:ListItem Value="Sum" Text="Summer Examination" /></asp:DropDownList> &nbsp;Year:&nbsp;<asp:TextBox ID="txtyear" runat="server" CssClass="txtbox" Width="60px" AutoCompleteType="FirstName" AutoPostBack="true" OnTextChanged="txtyear_TextChanged"></asp:TextBox><asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Invalid Year" ValueToCompare="9999" ControlToValidate="txtyear" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtyear" FilterType="Numbers"></dev:FilteredTextBoxExtender>
Billing Type:&nbsp;<asp:DropDownList ID="ddlBillingType" runat="server" Width="200px" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlBillingType_OnSelectedIndexChanged"><asp:ListItem Value="Select" Text="---Select Billing Type-------"></asp:ListItem><asp:ListItem Value="PaperSetter" Text="Paper Setter Fees" /><asp:ListItem Value="ExamCenter" Text="Examination Center Fees" /><asp:ListItem Value="Invigilator" Text="Invigilator Fees" /><asp:ListItem Value="Documents" Text="Paper & Documents Prining" /><asp:ListItem Text="Other Fees/Charges" Value="Other" /></asp:DropDownList><br /></center><br />
<hr /><br /><asp:Label ID="lblSeason" runat="server" Visible="false"></asp:Label>
<asp:Panel ID="pnlProfile" runat="server" >
<table class="tbl"><tr><td>Select Name:</td><td><asp:DropDownList ID="ddlName" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlName_OnSelectedIndexChanged" Width="200px"></asp:DropDownList></td></tr></table>
<table class="tbl" runat="server" id="tblProfile">
<tr><td>Code:</td><td><asp:Label ID="lblCode" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Address:</td><td><asp:Label ID="lblAddress1" runat="server" ></asp:Label></td></tr>
<tr><td></td><td><asp:Label ID="lblAddress2" runat="server" ></asp:Label><asp:Label ID="lblCity" runat="server" ></asp:Label>,&nbsp;<asp:Label ID="lblState" runat="server" ></asp:Label>-<asp:Label ID="lblPinCode" runat="server" ></asp:Label></td></tr>
<tr><td>Contact No.:</td><td><asp:Label ID="lblConatctNo" runat="server" ></asp:Label></td></tr>
<tr><td>Email ID:</td><td><asp:Label ID="lblEmail" runat="server" ></asp:Label></td></tr>
</table>
</asp:Panel>
<table class="tbl" runat="server" id="tblOther"><tr><td>Name</td><td><asp:TextBox ID="txtPPName" runat="server" CssClass="txtbox" Width="250px"></asp:TextBox></td><td>Code:&nbsp;<asp:TextBox ID="txtcode" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
<tr><td> Address:</td><td colspan="3"><asp:TextBox ID="txtPAddress"   runat="server" CssClass="txtbox" Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td><asp:TextBox ID="txtPPAddress2" runat="server" CssClass="txtbox" Width="250px"></asp:TextBox></td></tr>
<tr><td></td><td>City:<br /><asp:TextBox ID="txtPCity" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtPCity" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage=" Insert City Name">*</asp:RequiredFieldValidator></td><td>State:<br /><asp:TextBox ID="txtPState" runat="server" CssClass="txtbox" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtPState" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert State Name">*</asp:RequiredFieldValidator></td><td>PinCode:<br /><asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Contact No.:</td><td><asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td><td>Email:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>
</table><br />
<asp:Panel ID="panelSubmitAmt" runat="server" CssClass="panelCenter">
<center><asp:Label ID="lblTitleInfo" runat="server" Font-Bold="true" ForeColor="Gray" ></asp:Label></center><br />
<table>
<tr><td>Amount Type:</td><td><asp:DropDownList ID="ddlAmtType" runat="server" CssClass="txtbox"  AutoPostBack="true" onselectedindexchanged="ddlAmtType_SelectedIndexChanged" ><asp:ListItem Value="DD" Text="Demand Draft"></asp:ListItem><asp:ListItem Value="Cash" Text="Cash"></asp:ListItem><asp:ListItem Text="Chaque" Value="CC"></asp:ListItem></asp:DropDownList></td></tr>
<tr><td><asp:Label ID="lblDDNNO" runat="server" ></asp:Label></td><td><asp:TextBox ID="txtDDNO" CssClass="txtbox" runat="server" ></asp:TextBox></td></tr><tr><td><asp:Label ID="lblAccountNo" runat="server" ></asp:Label></td><td><asp:TextBox ID="txtACNO" CssClass="txtbox" runat="server"></asp:TextBox></td></tr>
<tr><td>Bank:</td><td><asp:TextBox CssClass="txtbox" ID="txtBank" runat="server" ></asp:TextBox></td></tr>
<tr><td>Date:</td><td><asp:TextBox ID="txtDOB" runat="server" CssClass="txtbox" 
        ontextchanged="txtDOB_TextChanged"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDOB" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date of Submission, Othrwise default present date." >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDOB"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td></tr>
<tr><td>Narration Box:&nbsp;</td><td><asp:TextBox ID="txtNarration" CssClass="txtbox" runat="server" TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox></td></tr>
<tr><td>Amount:</td><td><asp:TextBox ID="txtAmt" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtAmt_textChanged" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAmt" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Currency:&nbsp;</td><td><asp:DropDownList ID="ddlCurrancy" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrancy_Changed" ><asp:ListItem Value="RS" Selected="True" Text="Rupees"></asp:ListItem><asp:ListItem Value="DL" Text="Dolar"></asp:ListItem><asp:ListItem Value="OT" Text="Other"></asp:ListItem></asp:DropDownList></td></tr>
<tr><td><asp:Label ID="lblCurrancyText" runat="server" ></asp:Label>&nbsp;</td><td><asp:TextBox ID="txtCurrancyValue" runat="server" AutoPostBack="true" OnTextChanged="txtCurrancyValue_TextChanged" CssClass="txtbox" Width="50px"></asp:TextBox><asp:Label ID="lblCurrancyName" runat="server" ></asp:Label>&nbsp;RS.<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCurrancyValue" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
</table>
<div id="totalamt" runat="server" >Total Amount:&nbsp;&nbsp;<asp:Label ID="lblTAmt" runat="server" ></asp:Label>&nbsp;Rs.</div>
<asp:ValidationSummary ID="validasum" runat="server" CssClass="expbox" DisplayMode="BulletList" ForeColor="Red" ValidationGroup="Architecture" />
<center><asp:Label ID="lblException" Font-Bold="True" runat="server" 
        ForeColor="Maroon" ></asp:Label></center>
<br /><center><asp:Button ID="btnSubmitAmt" runat="server" Text="Submit" CssClass="btnsmall"  ValidationGroup="Architecture"  onclick="btnSubmitAmt_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" CssClass="btnsmall" runat="server" Text="Clear" onclick="btnClear_Click" /></center>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<br />
<asp:Panel ID="pnlspace" runat="server" Height="400px"></asp:Panel>
</div><br /><br /><br /><br /><br /><br />
</asp:Content>

