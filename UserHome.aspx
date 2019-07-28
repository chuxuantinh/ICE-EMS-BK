<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserHome.aspx.cs" Inherits="UserHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ICE(I): Home</title>
  <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link rel="stylesheet" href="style.css" type="text/css" charset="utf-8" />	
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="page">
    <div id="content">
    <div id="welcome"><asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" onclick="lbtnLogout_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnSettings" runat="server" Text="Settings" onclick="lbtnSettings_Click"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:40px;"><br />
        &nbsp;&nbsp;&nbsp;<asp:Label ID="lbltest" runat="server" ></asp:Label>  <asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><img src="images/logo.gif" alt="ICE(I)" title="ICE (I)" /></a><br />
    <div id="redline"></div>
    <asp:Label ID="lbltext" runat="server" ></asp:Label>
    <asp:Panel ID="panelAdmission" runat="server" >
    <br /><center><table width="100%"><tr><td align="center">
    <asp:ImageButton ID="lbtnAdmission" runat="server" Text="Admission Admin" OnClick="lbtnAdmissionAdmin_Click" ImageUrl="~/images/btnAdmission.png"></asp:ImageButton></td>
    <td align="center">
    <asp:ImageButton ID="lbtnAdmissionApprove" runat="server" Text="Admission Approve" OnClick="lbtnAdmissionApprove_Click" ImageUrl="~/images/btnAdmissionregister.png"></asp:ImageButton></td></tr></table></center>
    </asp:Panel>
    <asp:Panel ID="panelAccount" runat="server" >
    <center><table width="100%"><tr>
    <td align="center"><asp:ImageButton ID="ibtnMainAC" OnClick="ibtnMainAC_Click" ImageUrl="~/images/btnAccount.png" runat="server" AlternateText="Main Account" /></td>
    <td align="center"><asp:ImageButton ID="ibtnLateFees" runat="server" ImageUrl="~/images/btnAdmissionForm.png" AlternateText="Late Fees" OnClick="ibtnLateFees_Click"/></td></tr>
    <tr><td align="center"><asp:ImageButton ID="ibtnAddApps" runat="server" ImageUrl="~/images/btnExamAC.png" AlternateText="Add Application Forms" OnClick="ibtnAddApps_Click" /></td>
       <td align="center"><asp:ImageButton ID="ibtnAppApprove" runat="server" AlternateText="Approve Application Forms" ImageUrl="~/images/btnAppApprove.png"  OnClick="ibtnAppApprove_Click"  /></td></tr>
       <tr><td align="center"><asp:ImageButton ID="ibtnExamBill" runat="server" ImageUrl="~/images/btnExamBill.png" AlternateText="Examination Billing" OnClick="btnExamBill_Click" /></td>
    <td align="center"><asp:ImageButton ID="ibtnMembershipAC" runat="server" AlternateText="Membership Account" ImageUrl="~/images/btnMemberAC.png" OnClick="ibtnmembershipAC_Click" /></td></tr>
    </table></center><!-- Exam Fee==Account and membership Fee== Admission Form -->
    </asp:Panel>

    <asp:Panel ID="panelExamination" runat="server" >
    <center><table width="100%"><tr><td align="center"><asp:ImageButton ID="ibtnExamSchedule" ImageUrl="~/images/btnExamSchedule.png" runat="server" AlternateText="Main Account" OnClick="ibtnExamSchedule_Click" /></td><td align="center"><asp:ImageButton ID="ibtnExamCEnter" runat="server" ImageUrl="~/images/btnExamCEnter.png" AlternateText="Exam Center" OnClick="ibtnExamCenter_Click"/></td></tr>
    </table>
    <table width="100%"><tr><td align="center"><asp:ImageButton ID="ibtnExamFrom" runat="server" AlternateText="Exam Form" OnClick="ibtnExamForm_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnRollNO" runat="server" AlternateText="Roll No." OnClick="ibtnRollNo_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnAdmitCard" runat="server" AlternateText="Admit Card" OnClick="ibtnAdmitCard_Onclick" /></td></tr>
    <tr><td align="center"><asp:ImageButton ID="ibtnMarkFeed" runat="server" AlternateText="MarkFeet" OnClick="ibtnMarkFeed_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnExamPaper" runat="server" AlternateText="Exam Paper" OnClick="ibtnExamPaper_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnPaperSetter" runat="server" AlternateText="Paper Setter" OnClick="ibtnPaperSEtter_Onclick" /></td></tr>
    <tr><td align="center"><asp:ImageButton ID="ibtnSeating" runat="server" AlternateText="Seating Arrangement" OnClick="ibtnSeating_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnMarkSheet" runat="server" AlternateText="MarkSheet" OnClick="ibtnMarksheet_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnCertificate" runat="server" AlternateText="Certificates" OnClick="ibtnCertificate_Onclick" /></td></tr>
    <tr><td align="center"><asp:ImageButton ID="ibtnUFM" runat="server" AlternateText="UFM" OnClick="ibtnUFM_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnRecheking" runat="server" AlternateText="Re-Checking Form" OnClick="ibtnRechecking_Onclick" /></td><td align="center"><asp:ImageButton ID="ibtnMarking" runat="server" AlternateText="Exam Marking" OnClick="ibtnMarking_Onclick" /></td></tr>
    <tr><td align="center"><asp:ImageButton ID="ImageButton12" runat="server" AlternateText="Exam Form" Visible="false" /></td><td align="center"><asp:ImageButton ID="ImageButton13" runat="server" AlternateText="Exam Form"  Visible="false"/></td></tr>
    </table>
    </center>
    </asp:Panel>
    <asp:Panel ID="panelInventory" runat="server">
    <center><table width="100%"><tr><td align="center"><asp:ImageButton ID="ibtnStock" runat="server" AlternateText="Stock Manager" OnClick="ibtnStock_OnClick" ImageUrl="~/images/btnstockmanager.png" /></td><td><asp:ImageButton ID="ibtnIMSupplier" runat="server" AlternateText="IM Supplier" OnClick="ibtnIMSupplier_OnClick" ImageUrl="~/images/btnImsupplier.fw.png" /></td></tr></table></center>
    </asp:Panel>
    <asp:Panel ID="panelProject" runat="server" >
    <center><table width="100%"><tr><td align="center"><asp:ImageButton ID="ibtnProSynopsis" runat="server" AlternateText="Synopsis" OnClick="ibtnProSynopsis_OnClick" ImageUrl="~/images/btnsynopsis.fw.png" /></td><td><asp:ImageButton ID="ibtnProApprove" runat="server" AlternateText="Project Approve" OnClick="ibtnProApprove_Onclick" ImageUrl="~/images/btnprojectapprove.png" /></td></tr>
    <tr><td align="center"><asp:ImageButton ID="ibtnProSubmit" runat="server" AlternateText="Project Submit" OnClick="ibtnProSubmit_OnClick" ImageUrl="~/images/btnProjectSubmit.png" /></td><td><asp:ImageButton ID="ibtnProEvaluate" runat="server" AlternateText="Project Evaluation" OnClick="ibtnProEvaluate_OnClick" ImageUrl="~/images/btnprjctevaluation.png" /></td></tr>
    </table></center>
    </asp:Panel>
    
    <asp:Panel ID="panelMembership" runat="server" >
     <center><table width="100%"><tr><td align="center"><asp:ImageButton ID="ibtnMemberRegister" runat="server" ImageUrl="~/images/btnMemberRegister.png" AlternateText="Member Registration" OnClick="ibtnMemberRegistration_Click" /></td><td align="center"><asp:ImageButton ID="ibtnMemReNewal" ImageUrl="~/images/btnMemberRenewal.png" runat="server" AlternateText="Profile Renewal" OnClick="ibtnMemRenewal_Click" /></td></tr></table></center>
    </asp:Panel>
    
    
    <asp:Panel ID="panelFrontOffice" runat="server" >
    
    <center><table width="100%"><tr><td align="center"><asp:ImageButton ID="ibtnFrontOffice" runat="server" ImageUrl="~/images/btnFrontOffice.png" AlternateText="Visitors and Counselling" OnClick="ibtnFrontOffice_Click" /></td><td align="center"><asp:ImageButton ID="ibtnEnquiry" ImageUrl="~/images/btnDairy.png" runat="server" AlternateText="Dairy Entry" OnClick="ibtnEnquiry_Click" /></td></tr>
    <tr><td align="center"><b><asp:ImageButton ID="ibtnCourier" ImageUrl="~/images/btnCourier.png" runat="server"  AlternateText="Courier Dispetch" OnClick="ibtnCourier_Click" /></b></td><td align="center"><b><asp:ImageButton ID="ibtnD2D" ImageUrl="~/images/btnDairyToDep.png" runat="server"  AlternateText="DairyToDepartment" OnClick="ibtnD2D_Click" /></b></td></tr></table><br /></center>
    </asp:Panel>
     <!-- end togel -->
   <br /><br /><br />
    </div><br />
    </div>
    <!-- footer -->
   
    <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Help & Support</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    </form>
</body>
</html>
