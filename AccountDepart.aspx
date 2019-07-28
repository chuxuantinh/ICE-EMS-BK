<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="AccountDepart.aspx.cs" Inherits="AccountDepart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Institution of Civil Engineers (India)</title>
    <link rel="stylesheet" href="style.css" type="text/css" charset="utf-8" />	
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="page">
    <div id="content">
    <div id="welcome"><asp:ImageButton ID="btnNoredird" runat="server" ImageUrl="~/images/invisible.gif"  AlternateText="." TabIndex="1" /><asp:ImageButton ID="ImageButton4" TabIndex="20" runat="server" ImageUrl="~/images/home.png" ToolTip="Home" AlternateText="Home" OnClick="ibtnHome_Click" Height="20px" Width="20px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" onclick="lbtnLogout_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSettings" runat="server" Text="Settings"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:40px;">
           <asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><img src="images/logo.gif" alt="ICE(I)" title="ICE (I)" width="50%" /></a><br />
    <div id="redline"></div>
    <div id="usermanage" runat="server"  ><br />
    <table width="60%"><tr><td><asp:ImageButton ID="imgbtnCreate" runat="server" 
            CssClass="imgbtncreate"  AlternateText="New User" 
            onclick="imgbtnCreate_Click" ImageUrl="~/images/createcolor.png"/><br />New User</td><td><asp:ImageButton ID="ImageButton1" runat="server" 
            CssClass="imgbtncreate"  AlternateText="New User" 
            onclick="imgbtnRecover_Click" ImageUrl="~/images/user_update.png"/><br />Recover</td><td><asp:ImageButton ID="ImageButton2" runat="server" 
            CssClass="imgbtncreate"  AlternateText="New User" 
            onclick="imgbtnDelete_Click" ImageUrl="~/images/user_delete.png"/><br />Disactive</td><td><asp:ImageButton ID="ImageButton3" runat="server" 
            CssClass="imgbtncreate"  AlternateText="New User" 
            onclick="imgbtnReport_Click" ImageUrl="~/images/report.png"/><br />Report's</td>
            <td><asp:Label ID="lblApproval" CssClass="countsts" runat="server" ></asp:Label><br /><br /> Approval</td>
           <td><asp:Label ID="lblHold" CssClass="countsts"  runat="server" ></asp:Label><br /><br /> Hold</td>
             <td><asp:Label ID="lblDebitNote"  CssClass="countsts" runat="server"></asp:Label> <br /><br />DebitNote</td>
             <td><asp:Label ID="lblDDEntry" CssClass="countsts"  runat="server" ></asp:Label> <br /><br /> DDEntry</td>
             <td><asp:Label ID="lblReApproval" CssClass="countsts"  runat="server"></asp:Label> <br /> <br />Re-Approval</td>
            </tr>
            </table>
    </div>
    <hr />
    <asp:Panel ID="panelAccount" runat="server" >
    <center><table width="100%"><tr><td align="center"><asp:ImageButton ID="ibtnExaminationFEE" ImageUrl="~/images/mainacc.png" runat="server" AlternateText="Main Account" OnClick="ibtnExaminationFee_Click" /><br />
        <asp:LinkButton ID="lbtnMainIMAcc" runat="server" CssClass="txt2" 
            onclick="lbtnMainIMAcc_Click">Main IM Account</asp:LinkButton></td><td align="center"><asp:ImageButton ID="ibtnmembershipFEE" runat="server" ImageUrl="~/images/latefee1.jpg" AlternateText="View Accounts Form" OnClick="ibtnMemberFee_Click"/><br />
        <asp:LinkButton ID="lbtnLateFee" runat="server"  CssClass="txt2" onclick="lbtnLateFee_Click" 
               >Donate Late Fees</asp:LinkButton></td>
    <td align="center"><asp:ImageButton ID="ibtnExamBill" runat="server"  ImageUrl="~/images/exambill.jpg" AlternateText="Examination Billing" OnClick="btnExamBill_Click" /><br />
        <asp:LinkButton ID="lbtnExamBill" runat="server"  CssClass="txt2" onclick="lbtnExamBill_Click" 
           >Examination Billing</asp:LinkButton></td></tr>  <tr><td><br /></td></tr>
            <tr><td align="center"><asp:ImageButton ID="ibtnMembershipAC"  runat="server" 
                    AlternateText="Membership Account" ImageUrl="~/images/membershipacc.jpg" 
                    onclick="ibtnMembershipAC_Click"/><br />
        <asp:LinkButton ID="lbtnMembershipAcc" runat="server"  CssClass="txt2" onclick="lbtnMembershipAcc_Click" 
                >Membership Account</asp:LinkButton></td>
    
     <td align="center"><asp:ImageButton ID="ibtnExamAC" runat="server" ImageUrl="~/images/application_add1.png" AlternateText="Add Application Forms" OnClick="btnAddAppForm_Click" /><br />
        <asp:LinkButton ID="lbtnAddApps" runat="server"  CssClass="txt2" onclick="lbtnAddApps_Click" 
            >Add Application</asp:LinkButton></td><td align="center">
                    <asp:ImageButton ID="ibtnAppApprove" runat="server" 
                        AlternateText="Application Forms Approval" 
                        ImageUrl="~/images/application_accept1.png" onclick="ibtnAppApprove_Click" /><br />
        <asp:LinkButton ID="lbtnApproveApps" runat="server"  CssClass="txt2" onclick="lbtnApproveApps_Click" 
                >Approve Application</asp:LinkButton></td></tr><tr><td><br /></td></tr>
    </table></center><!-- Exam Fee==Account and membership Fee== Admission Form -->
    </asp:Panel><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label><br /><br />
    
     <!-- end togel -->
   
    </div><br />
    </div>
    <!-- footer -->
    <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Help & Support</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    </form>
</body>
</html>

