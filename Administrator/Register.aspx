<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Administrator_Register" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Register New Member
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updatepanel" runat="server" ><Triggers><asp:PostBackTrigger  ControlID="btnRegisterIM" /><asp:PostBackTrigger ControlID="btnRegister" /><asp:PostBackTrigger ControlID="btnViewProfile" /> </Triggers><ContentTemplate>
<br />
<br /><div id="register"><center><h3>REGISTER NEW MEMBER</h3><br />
<table class="tbl"><tr><td>Select Session:</td><td>
    <asp:DropDownList ID="ddlExamSeason" runat="server" 
        OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" 
        CssClass="txtbox" 
        ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label></td></tr>
  </table>
<input id="scrollPos4" runat="server" type="hidden" value="0" />
                 <div id="div2">Enter Diary No
<asp:TextBox ID="txtdiary" runat="server" CssClass="txtbox"></asp:TextBox><asp:Button ID="btndiary" 
                         runat="server" Text="Ok" CssClass="btnsmall" onclick="btndiary_Click" />
</div>
<asp:Label ID="lblExceptionDiary" runat="server"></asp:Label></center>
<br />
<asp:Panel ID="pnldetails" runat="server" Visible="false">  <center>
 <asp:RadioButton ID="rbtnHFellow" runat="server" GroupName="Dev" ForeColor="Blue"
            Text="Honoraray Fellow" oncheckedchanged="rbtnHFellow_CheckedChanged" AutoPostBack="true" />
        <asp:RadioButton ID="rbtnFellow" runat="server" Text="Fellow" GroupName="Dev" AutoPostBack="true"
            oncheckedchanged="rbtnFellow_CheckedChanged" ForeColor="Blue" />
        <asp:RadioButton GroupName="Dev" runat="server" ID="rbtMember" Text="Member" AutoPostBack="true" ForeColor="Blue"
            oncheckedchanged="rbtMember_CheckedChanged" /><asp:RadioButton ID="rbtnIM" AutoPostBack="true" ForeColor="Blue"
            runat="server" Text="Institutional Member" GroupName="Dev" 
            oncheckedchanged="rbtnIM_CheckedChanged" /><br />
            <br /><asp:Label ID="lblException" runat="server" ></asp:Label></center>
           <center> <asp:ValidationSummary runat="server" ID="ValidationSummary" ValidationGroup="D" DisplayMode="BulletList" /></center>
            <asp:Panel ID="panelNewMem" runat="server" >
          <center> <asp:Label  ID="lblstep1" runat="server" Text="1. Register" ForeColor="Black" ></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ForeColor="Gray" ID="lblStep2" runat="server" Text="2. Update Profile"></asp:Label><br /><br />
          <table><tr><td><asp:Label ID="lblName" runat="server" ></asp:Label></td><td><asp:TextBox ID="txtNaem" runat="server" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="regexName" controltovalidate="txtNaem" Display="Dynamic" ValidationGroup="D" errormessage="Please enter your name!" >*</asp:RequiredFieldValidator></td></tr>
   <tr><td><asp:Label ID="lblDesignation" runat="server" ></asp:Label></td><td><asp:TextBox ID="txtDesignation" runat="server" ></asp:TextBox>
   <asp:RequiredFieldValidator runat="server" id="regexdesignation" Display="Dynamic" controltovalidate="txtDesignation" ValidationGroup="D" errormessage="Please enter your Designaiton!">*</asp:RequiredFieldValidator></td></tr>
   <tr><td><asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td><td><asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox>
   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ValidationGroup="D" Display="Dynamic" ErrorMessage="Invalid Email Format">*</asp:RegularExpressionValidator>
   <asp:RequiredFieldValidator runat="server" id="regexRegEmiail" Display="Dynamic" controltovalidate="txtEmail" ValidationGroup="D" errormessage="Please enter your Email!">*</asp:RequiredFieldValidator>
</td></tr>
   </table> 
   <br /><div id="Stripview" runat="server"  style="border:1px solid green; background-color:#C1F7B2; padding:10px; width:200px;">Account is successfully created<br />To Active Membership fill up Profile Infotmation. &nbsp;&nbsp;&nbsp;&nbsp;<b><asp:Label ID="lblRegID" runat="server" Visible="false" ></asp:Label></b></div><br /><br />
   <asp:Button ID="btnRegister" runat="server" Text="Register" 
           onclick="btnRegister_Click" ValidationGroup="D" CssClass="btnsmall" /><asp:Button ID="btnViewProfile" runat="server" 
                  onclick="btnViewProfile_Click" CssClass="btnsmall"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRegisterIM" runat="server" CssClass="btnsmall" Text="Register New IM" OnClick="btnRegNewIM_Click"/> 
              &nbsp;</center><br />
            </asp:Panel>
            </asp:Panel>
</div>
</ContentTemplate> </asp:UpdatePanel>
</asp:Content>

