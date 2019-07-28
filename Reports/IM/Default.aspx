<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reports_IM_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>IM Report</title>

    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
        <link rel="stylesheet" href="../../style.css" type="text/css" charset="utf-8" />
        <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">

      <div id="page">
    <div id="content">
    <div id="welcome"><asp:ImageButton ID="btnNoredird" runat="server" ImageUrl="~/images/invisible.gif"  AlternateText="." TabIndex="1" /><asp:ImageButton ID="ibtnHomeIcon" TabIndex="20" runat="server" ImageUrl="~/images/home.png" ToolTip="Home" AlternateText="Home" Height="20px" Width="20px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSettings" runat="server" Text="Settings"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:40px;">
         <asp:Label ID="lbltest" runat="server" ></asp:Label>  
            <asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/logo.gif" AlternateText="ICE(I)"  width="50%" /></a><br />
    <div id="redline"></div>
    <div id="panelHeader" runat="server" >
                          <div id="usermanage" runat="server"  >
    <%--<table width="40%"><tr align="center"><td align="center"><asp:ImageButton ID="imgbtnCreate" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            ImageUrl="~/images/createcolor.png"/><br />Create New</td><td>
            <asp:ImageButton ID="imgbtnRecover" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            ImageUrl="~/images/user_update.png" onclick="imgbtnRecover_Click"/><br />Recover Password</td><td><asp:ImageButton ID="imgbtnDelete" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
             ImageUrl="~/images/user_delete.png"/><br />Disactive</td><td>
            <asp:ImageButton ID="imgbtnManage" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
             ImageUrl="~/images/Report.png" onclick="imgbtnManage_Click"/><br />All Reports</td></tr></table>--%>
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
</div><h1>Membership</h1>
<div id="contentDivImg98" style="display: block;"> 
  <br />
   <div class="leftlist">
  <ul>
      
       <li><asp:LinkButton ID="lbtnIM" runat="server" Text="IM Profile" CssClass="leftlink" OnClick="lbtnIM_OnClick">Member's Profile Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnSubscription" runat="server" CssClass="leftlink" OnClick="lbtnSubscription_Click">Subscription Reports</asp:LinkButton></li>
     <%--  <li><asp:LinkButton ID="lkbtnStudentAccount" runat="server" CssClass="leftlink" OnClick="lkbtnStudentAccount_click">Student Account Report</asp:LinkButton></li>--%>
      <%-- <li><asp:LinkButton ID="lkbtnStatus" runat="server" CssClass="leftlink" OnClick="lbtnDiaryStatusRpt_Click" >Diary Status Reports</asp:LinkButton></li>
       <li><asp:LinkButton ID="lkbtnMemberType" runat="server" CssClass="leftlink" OnClick="lbtnMemberType_Click">Diary Entry Via Member Type</asp:LinkButton></li>--%>
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
             ></asp:LinkButton> </td><td><asp:Label ID="lblEnquiryHome" runat="server" Text="Membership Reports" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1>Membership Reports</h1></div>
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
