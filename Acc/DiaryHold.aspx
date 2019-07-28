<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="DiaryHold.aspx.cs" Inherits="Acc_DiaryHold" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Diary On Hold
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblDiaryHold" runat="server" Text="Diaries on Hold" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Submit Diary Hold Remarks </h1></div><br />
<br />
<center>Enter Diary No.:&nbsp;&nbsp;<asp:TextBox ID="txtDiaryNo" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtDiaryNo_TextChanged" ></asp:TextBox>&nbsp;&nbsp;<asp:Image ID="ibtnViewDairy" ImageUrl="~/images/dairycount.gif"  runat="server" AlternateText="Dairy" />&nbsp;
<br /><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true"></asp:Label><br /><dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-350" OffsetY="25" PopupControlID="pnlDairyCount" TargetControlID="ibtnViewDairy" ></dev:PopupControlExtender>
Enter Remarks:&nbsp;&nbsp;<asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Width="250px" Height="50px"></asp:TextBox>
<br /><br /><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnsmall" OnClick="btnSave_Click" /></center>
<asp:Panel ID="pnlDairyCount" runat="server" Width="350px" CssClass="pnlpopup" >
<div class="redsubtitle"><center>Application Form Count</center></div>
<table width="100%"><tr><td>Applications</td><td>Total Received</td><td>Total Submitted</td></tr>
<tr><td>Academic DD</td><td><asp:Label ID="lblADDRcv" ForeColor="White" runat="server" ></asp:Label></td><td><asp:Label ForeColor="White" ID="lblADDSub" runat="server" ></asp:Label></td></tr>
<tr><td>Others DD</td><td><asp:Label ID="lblODDRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblODDSub"  ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Admission </td><td><asp:Label ID="lblAdmissionRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblAdmissionSub" ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Examination</td><td><asp:Label ID="lblExamRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblExamSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>ITI </td><td><asp:Label ID="lblITIRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblITISub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Auto-CAD</td><td><asp:Label ID="lblCADRcv" runat="server" ForeColor="White"></asp:Label></td><td><asp:Label ID="lblCADSub" runat="server" ForeColor="White"></asp:Label></td></tr>
<tr><td>Others Form</td><td><asp:Label ID="lblOthersFormRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblOthersFormSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Provisional</td><td><asp:Label ID="lblProvisionalRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProvisionalSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Final Pass</td><td><asp:Label ID="lblFinalPassRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblFinalPassSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Re-Checking</td><td><asp:Label ID="lblReCheckingRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblReCheckingSub" runat="server"  ForeColor="White" ></asp:Label></td></tr>
<tr><td>Duplicate Docs</td><td><asp:Label ID="lblDuplicateRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblDuplicateSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project DD</td><td><asp:Label ID="lblProjectRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProjectSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaC</td><td><asp:Label ID="lblProformaCRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaCSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaB</td><td><asp:Label ID="lblProformaBRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaBSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Membership DD</td><td><asp:Label ID="lblMembershipRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblMembershipSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Books DD</td><td><asp:Label ID="lblBooksRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblBooksSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Prospectus DD</td><td><asp:Label ID="lblProsRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProsSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
</table>
</asp:Panel>
<hr />
<br /><br /><br /><br /><br />
<br /><br /><br /><br /><br />
<br /><br /><br /><br /><br />
<br /><br /><br /><br /><br />
<br /><br /><br /><br /><br />
<br /><br /><br /><br /><br />
<br /><br /><br /><br /><br />
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>