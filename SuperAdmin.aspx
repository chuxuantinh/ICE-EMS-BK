<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SuperAdmin.aspx.cs" Inherits="SuperAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Institution of Civil Engineers (India)</title>
    <link rel="stylesheet" href="style.css" type="text/css" charset="utf-8" />	
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="page">
    <div id="content">
    <div id="welcome"><asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnLogout" runat="server" Text="Sign Out" onclick="lbtnLogout_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSettings" runat="server" Text="Settings" OnClick="lbtnSetting_Click"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:35px;">
        <b id="ViewIDPanel" runat="server" >&nbsp;&nbsp;&nbsp;</b>&nbsp;&nbsp;&nbsp;<asp:Label ID="lbltest" runat="server" ></asp:Label>
          <asp:ImageButton ID="refreshimage" runat="server" ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><img src="images/logo.gif" alt="ICE(I)" title="ICE (I)" width="50%" /></a><br />
    <div id="redline"></div>
    <div id="usermanage" runat="server"><table width="70%"><tr><td align="center"><asp:ImageButton ID="imgbtnCreate" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnCreate_Click" ImageUrl="~/images/createcolor.png"/><br />Create New</td><td align="center"><asp:ImageButton ID="imgbtnRecover" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnRecover_Click" ImageUrl="~/images/user_update.png"/><br />Recover Password</td><td align="center"><asp:ImageButton ID="imgbtnDelete" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnDelete_Click" ImageUrl="~/images/user_delete.png"/><br />Disactive</td><<td align="center"><asp:ImageButton ID="imgbtnManage" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnManage_Click" ImageUrl="~/images/userManage.jpg"/><br />Setting(s)</td><td align="center"><asp:ImageButton ID="imgbtnProfile" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Admin Letters" 
             ImageUrl="~/images/letter.jpg" onclick="imgbtnProfile_Click"/><br />Letter's</td><td align="center" id="tdDebitNote" runat="server"><asp:ImageButton ID="imgbtnDebitNote" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Debit Notes" 
             ImageUrl="~/images/debitnote.jpg" onclick="imgbtnDebitNote_Click"/><br />Debit Note</td><td align="center"><asp:ImageButton ID="imgbtnStatus" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Application Status" 
             ImageUrl="~/images/processicon.jpg" onclick="imgbtnStatus_Click"/><br />Process Status</td><td align="center"><asp:ImageButton ID="ImageButton1" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Application Status" 
             ImageUrl="~/images/courier-icon.gif" onclick="imgbtnLookDiary_Click"/><br />Diary Look</td>
             </tr></table>
    </div>
    <hr />
    <script>
        function toggle5(showHideDiv, switchImgTag) {   var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="images/plus.png">';
            }
            else {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="images/minus.png">';
            }
        }
    </script>
    <div id="togal">
    <div class="headerDivImg">
    <a id="imageDivLink" href="javascript:toggle5('contentDivImg', 'imageDivLink');"><img src="images/minus.png"></a>
</div><br /><br /><br />
<div id="contentDivImg" style="display: block;"> 
    <div id="admintable"><table width="100%"><tr>
                                   <td id="MembershipTDId" runat="server"><center class="adminbox" id="centMembership" runat="server" > <asp:ImageButton ID="ibtnMembership" runat="server" ImageUrl="images/administrator-icon.png" CssClass="superimg" AlternateText="Membership Profile" onclick="ibtnMembership_Click"  /><br />
                                       <asp:LinkButton ID="lbtnMembershipAdmin" runat="server" Text="Membership" CssClass="txt1" onclick="lbtnMembershipAdmin_Click"> </asp:LinkButton></center></td>
                                   <td><center class="adminbox" id="centAdmin" runat="server" ><asp:ImageButton ID="ibtnAdmin" runat="server" ImageUrl="images/reception.png"  CssClass="superimg" AlternateText="Super Admin" onclick="ibtnAdmin_Click"  /><br /><asp:LinkButton ID="lbtnAdmin" runat="server" OnClick="lbtnAdmin_Click" Text="Front Office" CssClass="txt1"> </asp:LinkButton></center></td>
                                   <td><center class="adminbox" id="centAdmission" runat="server" ><asp:ImageButton ID="ibtnAdmission" runat="server" ImageUrl="images/Admission.jpg" AlternateText="Super Admin" OnClick="ibtnAdmission_Click"  CssClass="superimg" /><br /><asp:LinkButton ID="lbtnAdmission" runat="server" OnClick="lbtnAdmission_Click" Text="Student" CssClass="txt1"> </asp:LinkButton></center></td>
                                   <td><center class="adminbox" id="centAccount" runat="server"><asp:ImageButton ID="ibtnAccount" runat="server" ImageUrl="images/AccountDepartment.jpg" AlternateText="Super Admin" OnClick="ibtnAccount_Click"  CssClass="superimg" /><br /><asp:LinkButton ID="lbtnAccount" runat="server" OnClick="lbtnAccount_Click" Text="Accounts" CssClass="txt1"> </asp:LinkButton></center></td>
                                   </tr><tr>
                                   <td><center class="adminbox" id="centExam" runat="server" ><asp:ImageButton ID="ibtnExam" runat="server" ImageUrl="images/examination.jpg" AlternateText="Examination Department" OnClick="ibtnExam_Click" CssClass="superimg" /><br /><asp:LinkButton ID="lbtnExam" runat="server" OnClick="lbtnExam_Click" Text="Examination" CssClass="txt1"> </asp:LinkButton></center></td>
                                   <td><center class="adminbox" id="centInventory" runat="server" ><asp:ImageButton ID="ibtnInventory" runat="server" ImageUrl="images/inventory_icon.jpg" AlternateText="Inventory Accounts" OnClick="ibtnInventory_Click"  CssClass="superimg" /><br /><asp:LinkButton ID="lbtnInventory" runat="server" OnClick="lbtnInventory_Click" Text="Inventory" CssClass="txt1"> </asp:LinkButton></center></td>
                                   <td><center class="adminbox" id="centProject" runat="server" ><asp:ImageButton ID="ibtnProject" runat="server" ImageUrl="images/ProjectIcon.png" AlternateText="Project Department" OnClick="ibtnProject_Click" CssClass="superimg" /><br /><asp:LinkButton ID="lbtnProject" runat="server" OnClick="lbtnProject_Click" Text="Project" CssClass="txt1"> </asp:LinkButton></center></td>
                                   <td><center class="adminbox" id="centReport" runat="server" ><asp:ImageButton ID="ibtnReport" runat="server" ImageUrl="images/report.png" AlternateText="Report Section" OnClick="ibtnReport_Click"  CssClass="superimg" /><br /><asp:LinkButton ID="lbtnReport" runat="server" OnClick="lbtnReport_Click" Text="Reports" CssClass="txt1"> </asp:LinkButton></center></td>
                                   </tr></table></div>
    </div>
   </div> <!-- end togel -->
   <asp:UpdatePanel ID="updatepanelstip" runat="server">
   <Triggers><asp:PostBackTrigger ControlID="ibtnRegisterMem" />
   <asp:PostBackTrigger ControlID="ibtnvewProfile" />
   <asp:PostBackTrigger ControlID="ibtnAssociateFeeMaster" />
   <asp:PostBackTrigger ControlID="ibtnTechFeeMaster" />
   <asp:PostBackTrigger ControlID="ibtnMemberFeeMaster" />
   <asp:PostBackTrigger ControlID="ibtnArchiSubMaster" />
   <asp:PostBackTrigger ControlID="CivilSubMaster" />
   <asp:PostBackTrigger ControlID="ibtnSessionDuration" />
   <asp:PostBackTrigger ControlID="ibtnDepartmentName" />
   <asp:PostBackTrigger ControlID="ibtnCourierService" />
   <asp:PostBackTrigger ControlID="ibtnBankName" />
</Triggers>
   <ContentTemplate>
    <script>
        function toggle(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="images/plus.png">';
            }
            else {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="images/minus.png">';
            }
        }
        function setting(id) {
            var dv = document.getElementById('feemaster');
            dv.style.display = "none";
            dv = document.getElementById('membership');
            dv.style.display = "none";
            dv = document.getElementById('sessionduration');
            dv.style.display = "none";
            dv = document.getElementById('coursemaster');
            dv.style.display = "none";
            dv = document.getElementById('settingname');
            dv.style.display = "none";
            dv = document.getElementById(id);
            dv.style.display = "block";
       }
    </script>
   <div class="togal">
    <div class="headerDivImg">
    <a id="imageDivLink1" href="javascript:toggle('contentDivImg1', 'imageDivLink1');"><img src="images/minus.png" alt="Show"></a>
</div><table class="stripmenu" width="90%"><tr><td>
               <a id="lbtMembership" href="javascript:setting('membership')"  class="stripbtn" >Membership</a></td><td>
               <a id="lbnfeemaster" href="javascript:setting('feemaster')"  class="stripbtn" >Fees Master</a></td><td>
               <a id="ltncoursemaster" href="javascript:setting('coursemaster')"  class="stripbtn" >Course Master</a></td><td >
               <a id="A2" href="javascript:setting('settingname')"  class="stripbtn" >Settings</a></td><td> 
               <a id="A3" href="javascript:setting('sessionduration')"  class="stripbtn" >Session Duration</a></td>
                   </tr></table>
<div id="contentDivImg1" style="display: block;"> 
    <!-- Membership panel -->
    <div id="membership" style="display:block;">
     <table class="stripcontent"><tr><td><asp:ImageButton ID="ibtnRegisterMem" 
            runat="server" ImageUrl="~/images/member.jpg" 
            onclick="ibtnRegisterMem_Click" /><br />Register New Member</td><td>
            <asp:ImageButton ID="ibtnvewProfile" runat="server" 
                ImageUrl="~/images/profile.jpg" onclick="ibtnvewProfile_Click" /><br />View Profile</td><td>
            </tr></table>
       </div>
    <div id="feemaster" style="display:none;">
             <table class="stripcontent"><tr><td>
            <asp:ImageButton ID="ibtnAssociateFeeMaster" runat="server" 
                ImageUrl="~/images/coin.jpg" onclick="ibtnAssociateFeeMaster_Click" 
                Height="50px" Width="48px"  /><br />Associate Fees</td><td>
            <asp:ImageButton ID="ibtnTechFeeMaster" runat="server" 
                ImageUrl="~/images/coin.jpg" onclick="ibtnTechFeeMaster_Click"  /><br />Technician Fees</td><td>
            <asp:ImageButton ID="ibtnMemberFeeMaster" runat="server" 
                ImageUrl="~/images/coin.jpg" onclick="ibtnMemberFeeMaster_Click" /><br />Membership Fees</td>
                <td><asp:ImageButton ID="ibtnCreateNewFees" runat="server" 
                ImageUrl="~/images/coin.jpg" onclick="ibtnCreateNewFees_Click" 
                Height="50px" Width="48px"  /><br />Create New Fees</td></tr></table>
    </div>
    <!-- Fees master panel -->
    <!-- Course panel --->
    <div id="coursemaster" style="display:none;">
    <table  class="stripcontent"><tr><td>
            <asp:ImageButton ID="ibtnArchiSubMaster" runat="server" 
                ImageUrl="~/images/books.jpg" onclick="ibtnArchiSubMaster_Click"  /><br />Architectural Engineering</td><td>
            <asp:ImageButton ID="CivilSubMaster" runat="server" 
                ImageUrl="~/images/books.jpg" onclick="CivilSubMaster_Click"  /><br />Civil Engineering</td></tr></table>
    </div>
    <div  id="settingname"  style="display:none;" >
   <table class="stripcontent"><tr>
   <td><asp:ImageButton ID="ibtnDepartmentName" runat="server" ImageUrl="~/images/document-management-icon.gif" OnClick="ibtnDepartmentName_OnClick"/><br />Department</td>
   <td><asp:ImageButton ID="ibtnCourierService" runat="server" ImageUrl="~/images/CourierService.jpg" OnClick="ibtnCourierSerive_Click" /><br />Courier Service</td>
   <td><asp:ImageButton ID="ibtnBankName" runat="server" ImageUrl="~/images/Bank.jpg"  OnClick="ibtnBank_OnClick"/><br />Banks</td>
   </tr></table>
   </div>
   <div  id="sessionduration" style="display:none;">
   <table class="stripcontent"><tr>
   <td><asp:ImageButton ID="ibtnSessionDuration" OnClick="ibtnSessionDuration_Onclick" runat="server" ImageUrl="~/images/session.jpg" /><br />Session Duration</td></tr></table>
   </div>
    </div>
   </div>
 </ContentTemplate></asp:UpdatePanel>
    </div><br />
    </div>
    <!-- footer -->
    <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="SiteMap.aspx" title="SiteMap">SiteMap</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    </form>
</body>
</html>
