<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdministrator.master" AutoEventWireup="true" CodeFile="MemberFeeMaster.aspx.cs" Inherits="MemberFeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Member Fee Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirect"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2" >
<div class="fromRegisterlbl"><h1 >Update Memeber Fees:-</h1></div>
<asp:UpdatePanel ID="updatepnale1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnSave" /><asp:PostBackTrigger ControlID="btnCancel" /></Triggers><ContentTemplate>
<asp:Panel runat="server"  ID="panelMemberFeeMaster">
<br /><center style="width:100%;"><asp:RadioButton ID="rbtnHonoraryFellow" runat="server" 
            Text="Honorary Fellow" GroupName="dev" AutoPostBack="true" 
            oncheckedchanged="rbtnHonoraryFellow_CheckedChanged" />
        <asp:RadioButton ID="rbtnFellow" runat="server" Text="Fellow" GroupName="dev"  AutoPostBack="true"
            oncheckedchanged="rbtnFellow_CheckedChanged" />
        <asp:RadioButton ID="rbtnMember" runat="server" Text="Member" GroupName="dev"  AutoPostBack="true"
            oncheckedchanged="rbtnMember_CheckedChanged" /><asp:RadioButton ID="rbtnIM" AutoPostBack="true"
            runat="server" Text="Institutional Member" GroupName="dev" 
            oncheckedchanged="rbtnIM_CheckedChanged" /> </center><br />
           <center> <asp:Label ID="lbltitleInfo" runat="server" Font-Bold="true" ></asp:Label></center>
<fieldset style="width:50%; margin-left:25%;"><legend><b><img src="../images/leftlink.jpg" alt=":-" />&nbsp;&nbsp;<asp:Label ID="lblMType" runat="server" ></asp:Label><asp:Label ID="lblMemebertype" runat="server" ></asp:Label>&nbsp;</b></legend>
<table style="padding:20px;" width="100%"><tr><td>Old Enrollment Fee:</td><td><asp:Label ID="lblOldEnrollFee" runat="server" ></asp:Label></td></tr>
<tr><td>Old Subcription Fee:</td><td><asp:Label ID="lblOldSubFee" runat="server"></asp:Label></td></tr>
<tr><td></td></tr>
<tr><td>New Enrollment Fee:</td><td><asp:TextBox ID="txtEnrollFee" runat="server" ></asp:TextBox></td></tr>
<tr><td>New Subcription Fee:</td><td><asp:TextBox ID="txtSubFee" runat="server" ></asp:TextBox></td></tr>
</table><br />
<center><asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="dev" 
        onclick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" /></center>
<br />
</fieldset>
<br /><br />

</asp:Panel></ContentTemplate></asp:UpdatePanel>

</div> <!-- end right panel ---->

</asp:Content>

