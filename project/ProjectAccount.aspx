<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ProjectAccount.aspx.cs" Inherits="project_ProjectAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Project Proforma
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Manage Project Account" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><asp:Label ID="lblSessionHidden" Visible="false" runat="server" />
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFeeType" runat="server" ></asp:Label></h1><h1>Manage Project Account </h1></div>
<center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddldevExamSeason_SelectedIndexChanged"><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="50px" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged" ></asp:TextBox>
Membership No:&nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" ></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" CssClass="btnsmall" />
</center><br />
<!-- Right Box of Account --->
<div class="rightbox">
<center><b>IM&nbsp;&nbsp;<asp:Label ID="lblIMID" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp; Account</b></center>
<table>
<tr><td>Total Amount:</td><td><asp:Label ID="lblToAmount" runat="server" ></asp:Label></td></tr>
<tr><td>Dues Amount:</td><td><asp:Label ID="lblDuesAmount" runat="server" ></asp:Label></td></tr>
<tr><td>Books Amount:</td><td><asp:Label ID="lblBooksAmt" runat="server" ></asp:Label></td></tr>
<tr><td>Prospectus Amount:</td><td><asp:Label ID="lblProspectus" runat="server" ></asp:Label></td></tr>
<tr><td><b>Project Amount:</b></td><td><b><asp:Label ID="lblProjectAmt" runat="server" ></asp:Label></b></td></tr>
</table>
</div> <!--End Right Box of Account---->
<!-- Left Box of Account --->
<div class="imbox">
<center><b>Student Info</b></center>
<table>
<tr align="left"><td><b>Student Name:</b></td><td><asp:Label ID="lblName" Font-Bold="true" ForeColor="Maroon" runat="server" ></asp:Label></td></tr>
<tr align="left"><td>Course:</td><td><asp:Label ID="lblCourse" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server" ></asp:Label></td></tr>
<tr align="left"><td>AICTE Institute:</td><td>[<asp:Label ID="lblInsID" Font-Bold="true" runat="server" ></asp:Label>]&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInsName" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr align="left"><td><b>Status:</b></td><td><b><asp:Label ID="lblStatus" runat="server" ></asp:Label></b></td></tr>
<tr align="left"><td>Approval Fees:&nbsp;<asp:Label ID="lblApprovalFees" runat="server"></asp:Label></td><td>Evaluation Fees:&nbsp;<asp:Label ID="lblEvaluationFees" runat="server"></asp:Label></td></tr>
</table>
</div> <!--End Left Box of Account---->
<center>Select Proforma&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlProforma" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlProforma_SelectedIndexChanged"><asp:ListItem Value="" Text="Select Proforma"></asp:ListItem><asp:ListItem Value="ProformaB" Text="ProformaB" /><asp:ListItem Value="ProformaC" Text="ProformaC"></asp:ListItem></asp:DropDownList>
<br /><b><br />
Amount:&nbsp;&nbsp;<asp:Label ID="lblAmount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></b>
<br />
<asp:Label ID="lblException" runat="server"></asp:Label><br /><br />
<asp:Button ID="btnManage" runat="server" Text="Add" OnClick="btnManage_click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" CssClass="btnsmall" Text="Remove" OnClick="btnRemove_Click" /><br />
<br />
</center>
<hr />
<div class="documentation">
<titletag>Add Amount</titletag>
<p>Add Amount: Submit Project <b>Proforma[B/C]</b> amount and substract amount from IM Account of Project. If Project Amount Not found as required, Amount can't adjusted.</p>
<titletag>Remove Amount</titletag>
<p>To Remove Amount process is reverse of <b>Add Amount</b> process. While selecting Membership if <b>Proforma[B/C]</b> amount is removed then specific Project amount again add to IM Project Amount and Project status will be not submitted project approval or evaluation fees.</p>
</div>
</ContentTemplate></asp:UpdatePanel>
</div><br /><br /><br /><br /><br /><br /><br />
</asp:Content>