<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ICE(India):Login</title>
   <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link rel="stylesheet" href="style.css" type="text/css" charset="utf-8" />
    <script src="jquery.tools.min.js" type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" href="tooltip-generic.css"/> 	
</head>
<body>
    <form id="form1" runat="server"><br /><br /><br />
    <asp:ScriptManager ID="ScriptaManager1" runat="server"></asp:ScriptManager>
   <div id="dev"><br />
<div id="maikal">
	<a href="#" title="ICE(I)"><img src="images/logo2.gif" alt="ICE(I)" title="ICE (I)" /></a><br />
	<img src="images/Fram.png" title="Institute of Civil Engineering" alt="ICE" /><br /><br />
	<div id="loginpanel">
	<fieldset><legend><h2 style="color:#B21235; font-size:18px; font-family:Verdana;">Login</h2></legend>
	<center> <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="65px" ShowMessageBox="true" 
	  Width="200px"   ValidationGroup="Architecture"/></center><asp:UpdatePanel ID="updatepanel1" runat="server" ><ContentTemplate>
               <asp:Label ID="lblException1" runat="server"></asp:Label>
	<br /><table width="400px"><tr><td><asp:Label ID="lblType" runat="server" Text="Login Type" Visible="false"></asp:Label></td><td>
	</td></tr>
	                           <tr><td><asp:Label ID="lblName" runat="server" Text="Login Name"></asp:Label></td><td>
                                   <asp:TextBox ID="txtName" Width="150px" runat="server" TabIndex="1" ToolTip="Type User ID" Height="18px"></asp:TextBox>
	                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="Insert Login Name" TabIndex="2"  ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td></tr>
	                           <tr><td><asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label></td><td>
                                   <asp:TextBox ID="txtPassword"  Width="150px" runat="server" TextMode="Password"  ToolTip="Password more then 6 char. long" 
                                       OnTextChanged="txtPassword_Changed" TabIndex="1" Height="18px"  ></asp:TextBox>
	                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtPassword" ErrorMessage="Insert Password !" TabIndex="3"  ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td></tr>
	</table>   <asp:Label ID="lblpass" runat="server" Visible="false"></asp:Label><asp:Label ID="lblNm" runat="server" Visible="false"></asp:Label>
	<div style="font-size:10px; margin-left:200px;"><asp:HyperLink ID="hlnkforget" runat="server" Text="Forget Password !"></asp:HyperLink></div>
	</ContentTemplate></asp:UpdatePanel><div  style="margin-left:120px; margin-top:10px;">
        <asp:Button ID="btnLogin" CssClass="btnsmall" runat="server" Text="Login" ValidationGroup="Architecture"
            TabIndex="3" onclick="btnLogin_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencel" runat="server" Text="Cancel" TabIndex="4" CssClass="btnsmall" 
            onclick="btnCencel_Click" /></div><div  style="margin-left:60px;">
        </div>
	</fieldset>
	<br /><br />
    <center><b>Note: User and Application Form LOG updated, Please maintain UID and Password on own level.</b></center><br />
	  </div>
</div><br />
</div> <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Help & Support</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    </form><%--
<script>
    $("#form1 :input").tooltip({
        position: "center right",
        offset: [-2, 10],
        effect: "fade",
        opacity: 0.7
    });
</script>--%>
</body>
</html>
