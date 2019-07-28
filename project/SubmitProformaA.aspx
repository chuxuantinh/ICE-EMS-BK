<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="SubmitProformaA.aspx.cs" Inherits="project_SubmitProformaA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Proforma A Submission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Submit Proforma A" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnlcomp" runat="server">
<ContentTemplate>
<div class="fromRegisterlbl"><h1>Proforma A Submission</h1></div>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<center> <p>Project Status:&nbsp;<b>Selected</b> and Synopsis Status:<b> NotSubmitted</b></p><br />Membership NO.:&nbsp;<asp:TextBox ID="txtSID" Width="100px" runat="server" CssClass="txtbox" ontextchanged="txtSID_TextChanged" AutoPostBack="True" ForeColor="#993300"/>
<br />
<asp:Label ID="lblExceptionOK" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></center><br />
<hr />
<asp:Panel ID="pnlCompl" runat="server" Visible="false">
<center>
<table class="tbl">
<tr><td align="left">Name</td>
<td>&nbsp;:&nbsp;</td>
<td align="left"><asp:Label ID="lblStuName" runat="server" ForeColor="#993300" Font-Bold="True"/></td>
</tr>
<tr><td align="left">Course</td>
<td>:</td>
<td align="left"><asp:Label ID="lblCourse" runat="server" ForeColor="#993300" Font-Bold="True"/>
&nbsp;<asp:Label ID="lblPart" runat="server" ForeColor="#993300" Font-Bold="True"/></td>
</tr><tr><td align="left">Course Status&nbsp;&nbsp;&nbsp; </td>
<td>:</td>
<td align="left"><asp:Label ID="lblStatus" runat="server" ForeColor="#993300" Font-Bold="True" /></td>
</tr>
<tr><td align="left">Diary No</td><td>:</td>
<td align="left"><asp:TextBox ID="txtDiaryno" runat="server" CssClass="txtbox"/></td>
</tr><tr>
<td>Course Status</td><td>:</td><td><asp:Label ID="lblCourseStatus" runat="server"></asp:Label></td>
<%--<tr><td align="left">Synopsis Date</td><td>:</td>
<td align="left" colspan="10"><asp:TextBox ID="txtSynpDate" runat="server" CssClass="txtbox" />
<dev:CalendarExtender ID="txtSynpDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal1" PopupPosition="BottomRight" TargetControlID="txtSynpDate"/>
<img src="../images/cal.png" id="cal1" runat="server"  alt="Cal" />
</td></tr>--%>
<tr><td></td><td>&nbsp;</td>
<td style="text-decoration: blink; font-size: small; color: #993300; font-family: fantasy;"><b>
Select Institution:-</b></td>
</tr><tr><td align="left">Option1</td><td>:</td>
<td align="justify"><asp:DropDownList ID="ddlOpn1" runat="server" CssClass="txtbox" Width="280px" /></td>
</tr><tr>
<td align="left">Option2</td><td>:</td>
<td align="left"><asp:DropDownList ID="ddlOpn2" runat="server" CssClass="txtbox" Width="280px" /></td>
</tr><tr>
<td align="left">Option3</td><td>:</td>
<td align="left"><asp:DropDownList ID="ddlOpn3" runat="server" CssClass="txtbox" Width="280px" /></td>
</tr>
<tr><td align="left">Remarks</td><td>:</td>
<td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="46px" Width="280px"/></td>
</tr><tr>
<td colspan="2">&nbsp;</td>
<td align="center"><asp:Label ID="lblException" runat="server" Font-Bold="True" ForeColor="Red"/></td>
</tr><tr><td colspan="2">&nbsp;</td>
<td align="left"><asp:Button ID="btnSubmit" runat="server" CssClass="btnsmall" Text="Submit" onclick="btnSubmit_Click" />&nbsp;
<asp:Button ID="btnCancel" runat="server" CssClass="btnsmall" Text="Cancel" onclick="btnCancel_Click" /></td>
</tr></table>
<p>Update Project Status:&nbsp;<b>ProformaAApproved</b></p>
</center>
</asp:Panel>
<asp:Panel ID="pnlSpace" runat="server" Height="400px"/>
</div>
</ContentTemplate></asp:UpdatePanel>
<br />
</div><br />
</asp:Content>