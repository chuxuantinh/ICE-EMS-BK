<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reports_Project_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Project Department Reports</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
        <link rel="stylesheet" href="../../style.css" type="text/css" charset="utf-8" />
        <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
    <div id="content">
    <div id="welcome"><asp:ImageButton ID="btnNoredird" runat="server" ImageUrl="~/images/invisible.gif"  AlternateText="." TabIndex="1" /><asp:ImageButton ID="ibtnHomeIcon" TabIndex="20" runat="server" ImageUrl="~/images/home.png" ToolTip="Home" AlternateText="Home" OnClick="ibtnHome_Click" Height="20px" Width="20px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" onclick="lbtnLogout_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSettings" runat="server" Text="Settings"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:40px;">
         <asp:Label ID="lbltest" runat="server" ></asp:Label>  <asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/logo.gif" AlternateText="ICE(I)"  width="50%" /></a><br />
    <div id="redline"></div>
    <div id="panelHeader" runat="server" >
                          <div id="usermanage" runat="server"  >
   <%-- <table width="40%"><tr align="center"><td align="center"><asp:ImageButton ID="imgbtnCreate" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnCreate_Click" ImageUrl="~/images/createcolor.png"/><br />Create New</td><td><asp:ImageButton ID="imgbtnRecover" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnRecover_Click" ImageUrl="~/images/user_update.png"/><br />Recover Password</td><td><asp:ImageButton ID="imgbtnDelete" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnDelete_Click" ImageUrl="~/images/user_delete.png"/><br />Disactive</td><td><asp:ImageButton ID="imgbtnManage" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnManage_Click" ImageUrl="~/images/Report.png"/><br />All Reports</td></tr></table>--%>
    </div>        
    </div>
      <hr />
   <div id="leftpanel2">
<div id="leftteg" >
<asp:Panel ID="pnlFO" runat="server" >
   <script>
       function toggle98(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../../images/minus.png">';
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="imageDivLink98" href="javascript:toggle98('contentDivImg98', 'imageDivLink98');"><img src="../../images/minus.png" alt="Show"></a>
</div><h1>Project</h1>
<div id="contentDivImg98" style="display: block;"> 
  <br />
   <div class="leftlist">
  <ul>

  <li><asp:LinkButton ID="lbtnProject" runat="server" Text="Project Report" CssClass="leftlink" OnClick="lbtnProject_OnClick"></asp:LinkButton></li>
 <li><asp:LinkButton ID="lbtnProjectStatus" runat="server" Text="Project Status Report" CssClass="leftlink" OnClick="lbtnProjectStatus_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnInstituteRe" runat="server" Text="Institution Registration" CssClass="leftlink" OnClick="lbtnInstituteRe_OnClick"></asp:LinkButton></li>
  <li>Letter
  <ul>
 <li><asp:LinkButton ID="lbtnIMLetter" runat="server" 
          Text="IM Letter Report" CssClass="leftlink" onclick="lbtnIMLetter_Click" 
           ></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnAicteLetter" runat="server" 
          Text="AICTE Letter Report" CssClass="leftlink" onclick="lbtnAicteLetter_Click" 
           ></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnStudentLetter" runat="server" 
          Text="Student Letter Report" CssClass="leftlink" 
          onclick="lbtnStudentLetter_Click" ></asp:LinkButton>
          <ul><li><asp:LinkButton ID="lbtnStuRejectedLetter" runat="server" 
          Text="Project Rejected Letter" CssClass="leftlink" onclick="lbtnStuRejectedLetter_Click" 
           ></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnStuApproved" runat="server" 
          Text="Project Approval Letter" CssClass="leftlink" onclick="lbtnStuApproved_Click"
           ></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnStuAppRemarks" runat="server" 
          Text="Project Approval with Remarks Letter" CssClass="leftlink" onclick="lbtnStuAppRemarks_Click"
           ></asp:LinkButton></li>
           </ul>
           </li>
           </ul>
           </li>
  <li><asp:LinkButton ID="lbtnProjectDataEntry" runat="server" Text="Project Export" CssClass="leftlink" OnClick="lbtnProjectDataEntry_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnProjectApproveRpt" runat="server" Text="Project Approved From Account" CssClass="leftlink" OnClick="lbtnProjectApprovedAc_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnCopysubmit" runat="server" Text="Project Copy Submit Export" CssClass="leftlink" OnClick="lbtnCopysubmit_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnSynopsisApprovalReport" runat="server" Text="Synopsis Approval Report" CssClass="leftlink" OnClick="lbtnSynopsisApproveRpt_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnProjectApprove" runat="server" Text="Project Sent to Evaluation" CssClass="leftlink" OnClick="lbtnApproveApproveRpt_OnClick"></asp:LinkButton></li>  
   </ul>
    </div></div>
    </div>
    </asp:Panel>
</div>
</div>
        
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lbtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="All Reports" CssClass="redirecttab" OnClick="lbtnRedirectAdmin_Click" 
             ></asp:LinkButton> </td><td><asp:Label ID="lblEnquiryHome" runat="server" Text="Project Reports" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1>Project Reports</h1></div>
<img src="../../images/reportdefault.png" alt="Reports" height="250px" /><hr />
<br />
    </div>
             </div>
   <br /><br />
    </div><br />
    </div>
    <!-- footer -->
    <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Help & Support</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    </form>
</body>
</html>
