<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reports_AC_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accounts Reporting</title>
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
        <link rel="stylesheet" href="../../style.css" type="text/css" charset="utf-8" />
        <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div id="page">
    <div id="content">
    <div id="welcome"><asp:ImageButton ID="btnNoredird" runat="server" ImageUrl="~/images/invisible.gif"  AlternateText="." TabIndex="1" /><asp:ImageButton ID="ibtnHomeIcon" TabIndex="20" runat="server" ImageUrl="~/images/home.png" ToolTip="Home" AlternateText="Home" Height="20px" Width="20px" OnClick="ibtnHOme_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSettings" runat="server" Text="Settings"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:40px;">
         <asp:Label ID="lbltest" runat="server" ></asp:Label>  
            <asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/logo.gif" AlternateText="ICE(I)"  width="50%" /></a><br />
    <div id="redline"></div>
    <div id="panelHeader" runat="server" >
                          <div id="usermanage" runat="server"  >
    <table width="40%"><tr align="center"><td align="center"><asp:ImageButton ID="imgbtnCreate" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            ImageUrl="~/images/createcolor.png"/><br />Create New</td><td>
            <asp:ImageButton ID="imgbtnRecover" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            ImageUrl="~/images/user_update.png" onclick="imgbtnRecover_Click"/><br />Recover Password</td><td><asp:ImageButton ID="imgbtnDelete" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
             ImageUrl="~/images/user_delete.png"/><br />Disactive</td><td>
            <asp:ImageButton ID="imgbtnManage" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
             ImageUrl="~/images/Report.png" onclick="imgbtnManage_Click"/><br />All Reports</td></tr></table>
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
</div><h1>Account</h1>
<div id="contentDivImg98" style="display: block;"> 
  <br />
   <div class="leftlist">
  <ul><li><asp:LinkButton ID="lbtnDDDate" runat="server" Text=" DD Report" CssClass="leftlink" OnClick="lbtnDDDateAcc_OnClick">DD Report Via Date/Diary</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnDDEntry" runat="server" Text="Account DD Entry" CssClass="leftlink" OnClick="lbtnDDAcc_OnClick">Account DD Entry Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAcc" runat="server" Text="Account Transection" CssClass="leftlink" OnClick="lbtnAcc_OnClick">Account Transection Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnMembership" runat="server" Text="Membership Account Report" CssClass="leftlink" OnClick="lbtnMSAcc_OnClick">Membership AC Report</asp:LinkButton> </li>       
       <li><asp:LinkButton ID="lbtnAppApprove" runat="server" Text="Application Approve Report" CssClass="leftlink" OnClick="lbtnAppApproveAcc_OnClick">Application Approve Report</asp:LinkButton> </li>       
       <li><asp:LinkButton ID="lbtnConsolidateRpt" runat="server" Text="Consolidated Amount Report" CssClass="leftlink" OnClick="lbtnConsolidateRptAcc_OnClick">Consolidated Amount Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAllExport" runat="server" Text="All Account Export" CssClass="leftlink" OnClick="lbtnAllExportAcc_OnClick">All Account Export</asp:LinkButton> </li>       
       <li><asp:LinkButton ID="lbtnExamBill" runat="server" CssClass="leftlink" OnClick="lbtnExamBill_Click" Enabled="false">Exam Bill Report</asp:LinkButton></li>
        <li><asp:LinkButton ID="lbtnApplicationStatus" runat="server" CssClass="leftlink" OnClick="lbtnApplicationStatus_Click" Enabled="True">Application Status</asp:LinkButton></li>
         <li><asp:LinkButton ID="lbtnApplicationStatusSum" runat="server" CssClass="leftlink" OnClick="lbtnApplicationStatusSum_Click" Enabled="True">Application Status Sum</asp:LinkButton></li>
         <li><asp:LinkButton ID="lbtnApplicationStatusCourse" runat="server" CssClass="leftlink" OnClick="lbtnApplicationStatusCourse_Click" Enabled="True">Application Status Course</asp:LinkButton></li>
         <li><asp:LinkButton ID="lbtnFormType" runat="server" Text="Form Type" 
                 CssClass="leftlink" onclick="lbtnFormType_Click" >Form Type Report</asp:LinkButton> </li>
                 <li><asp:LinkButton ID="lblDebitNot" runat="server" Text="Debit Note" 
                 CssClass="leftlink" onclick="lblDebitNot_Click" >Debit Note Report</asp:LinkButton> </li>
   </ul>
    </div></div>
    </div>
    </asp:Panel>
</div>
</div>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server"  Text="Home" CssClass="redirecttab" 
        onclick="lblHomeRedirect_Click"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="All Reports" 
            CssClass="redirecttab" onclick="lbtnNext1Redirect_Click" 
             ></asp:LinkButton> </td><td><asp:Label ID="lblEnquiryHome" runat="server" Text="Account Reports" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1>Account Reports</h1></div>
<img src="../../images/reportdefault.png" alt="Reports" width="100%" height="250px" /><hr />
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

    <div>
    
    </div>
   
    </form>
</body>
</html>
