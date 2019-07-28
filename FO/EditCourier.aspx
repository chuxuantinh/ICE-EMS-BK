<%@ Page Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="EditCourier.aspx.cs" Inherits="FO_EditCourier" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Update Consignment No
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"/></td><td>
</td><td><asp:Label ID="lblEditCourier" runat="server" Text="Edit Diary Details" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2"><div id="header">
<div class="fromRegisterlbl"><h1>Edit Diary Details:</h1></div>
<asp:UpdatePanel ID="updatePanel" runat="server" ><ContentTemplate>
<br /><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"/>
<center>
<table class="tbl"><tr><td>Select Session:</td><td><asp:DropDownList Width="200px" CssClass="txtbox" ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"/></td></tr>
<tr><td><asp:Label ID="lblFromName"  runat="server" ></asp:Label></td><td><asp:TextBox ID="txtSName" AutoPostBack="true" OnTextChanged="txtSName_TExtChnaged" runat="server" CssClass="txtbox" Width="200px" Font-Bold="true"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Name ">*</asp:RequiredFieldValidator>
</td></tr></table></center>
<center><asp:Label ID="lblExceptiontbl" ForeColor="Red" runat="server"/></center>
<asp:Panel ID="panelhide" runat="server" Height="400px">
<center><asp:Label ID="lblExcepitonDiary" runat="server" Font-Bold="True" Font-Size="Medium"/></center>
</asp:Panel>
<asp:Panel ID="panelshow" runat="server" >
<center><table id="tbllabel" class="tbl" runat="server"><tr><td><asp:Label ID="lblName" runat="server" ></asp:Label></td></tr></table></center>
<table class="tbl">
<tr>
<td>Receive From:&nbsp;&nbsp;&nbsp;&nbsp; </td>
<td><asp:DropDownList ID="ddlRecivefrom" runat="server" AutoPostBack="true" CssClass="txtbox" OnSelectedIndexChanged="ddlRecive_SelectedIndexChanged" Width="200px">
<asp:ListItem Selected="True" Text="IM" Value="IM"/>
<asp:ListItem Text="Student" Value="Student"/>
<asp:ListItem Text="Other" Value="Other"/>
</asp:DropDownList>
</td></tr>
<tr><td><asp:Label ID="lblFromName1" runat="server"/></td>
<td colspan="2"><asp:TextBox ID="txtSName1" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" Width="197px" ontextchanged="txtSName1_TextChanged"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSName" Display="Dynamic" ErrorMessage="Click to Create New Diary" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
<asp:TextBox ID="txtStName" runat="server" CssClass="txtbox" Font-Bold="true" Visible="false" Width="200px"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStName" Display="Dynamic" ErrorMessage="Click to Create New Diary" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
</td></tr>
<asp:Panel ID="pnlIMIDUp" runat="server" Visible="false">
<tr>
<td>IMID :</td>
<td colspan="2"><asp:TextBox ID="txtMEmUP" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" ontextchanged="txtSName1_TextChanged" Width="197px" /></td>
</tr>
</asp:Panel>
<tr>
<td colspan="3"><asp:Panel ID="otherpnl" runat="server">
<table class="tbl">
<tr>
<td align="left">Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;</td>
<td><asp:TextBox ID="txtAddress1" runat="server" CssClass="txtbox" Width="198px"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Insert Permanent Address" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
</td></tr>
<tr>
<td align="left">City:</td>
<td><asp:TextBox ID="txtCity" runat="server" CssClass="txtbox"/></td>
<tr>
<td>State:</td>
<td><asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" CssClass="txtbox" onselectedindexchanged="ddlState_SelectedIndexChanged1" Width="198px" /></td>
<td>&nbsp;&nbsp;PinCode:&nbsp; </td>
<td><asp:TextBox ID="txtPincode" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPincode" FilterType="Numbers" />
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPincode" ErrorMessage="PIN CODE limit exit." Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture" ValueToCompare="999999">*</asp:CompareValidator>
</td></tr></tr>
<tr><td>Phone:</td>
<td colspan="3"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"/>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers" />
&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox" Width="138px"/>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers" />
</td></tr></table></asp:Panel></td></tr>
<tr><td>Date :</td>
<td><asp:TextBox ID="txtDate" runat="server" CssClass="txtbox"/>
<dev:CalendarExtender ID="devdage" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtDate"/>
<img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" controltovalidate="txtDate" Display="Dynamic" errormessage="Insert Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
</td></tr>
<tr><td> &nbsp;</td>
<td><asp:LinkButton ID="btnNewCourierService" runat="server" Font-Bold="true" ForeColor="Red" OnClick="ibtnNewCourier_Onclick" Text="New Courier Service"/>
<br />
<asp:DropDownList ID="ddlCourierService" runat="server" CssClass="txtbox" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" Width="200px">
</asp:DropDownList>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Courier') ORDER BY Name">
</asp:SqlDataSource>
&nbsp;</td>
<td>Reference No:&nbsp;<asp:Label ID="lblRefNO" runat="server" Font-Bold="true"></asp:Label>
</td></tr></table>
<asp:Panel ID="panelCourier" runat="server" CssClass="expbox">
<center>Name of Courier Serivce:&nbsp;<asp:TextBox ID="txtNewCourier" runat="server" CssClass="txtbox" Width="200px" Font-Bold="true"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox Visible="false" ID="txtNewCity" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox><br /><asp:Label ID="lblExceptionNewCourier" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br /><br /><asp:Button ID="btnSaveNewCourier" runat="server" Text="Save" OnClick="btnSAveNew_Onclick" />    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencel"  runat="server" Text="Cencel" OnClick="btnCencelnew_Onclick" /></center>
</asp:Panel>
<table class="tbl"><tr><td>Consignment No:</td>
<td><asp:TextBox ID="txtConsignment" runat="server" CssClass="txtbox"/>
</td><td>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Weight:</td>
<td><asp:TextBox ID="txtwtkg" runat="server" CssClass="txtbox" Width="51px"/>Kg 
    <asp:TextBox ID="txtwtgm" runat="server" CssClass="txtbox" Width="42px"/>gms</td></tr>
<tr><td>Diary Type:</td><td><asp:TextBox ID="txtDiraryType" runat="server" CssClass="txtbox" Width="200px"/>
</td></tr>
<tr><td>Remark:</td><td><asp:TextBox ID="txtRemoark"  TextMode="MultiLine" Height="40px" runat="server" CssClass="txtbox" Width="200px"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRemoark" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Name ">*</asp:RequiredFieldValidator>
</td></tr>
<tr><td>Current Status:</td><td><asp:Label ID="lblStatus" runat="server" ></asp:Label></td></tr>

<asp:Panel runat="server" ID="panelStatus"> <tr><td>Change Status:</td><td><asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox"><asp:ListItem Value="Open" Text="Open" /><asp:ListItem Value="NotOpen" Text="Not Open" /><asp:ListItem Value="Block" Text="Blocked"/></asp:DropDownList></td></tr></asp:Panel>
</table><br />
<center><br /><asp:Button ID="btnSaveDiary" runat="server" Text="Update" ValidationGroup="Architecture" OnClick="btnSAveDiray_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBlock" runat="server" OnClientClick="return confirm('Are you certain you want to block this diary ?');" OnClick="btnBlock_Onclick"  CssClass="btnsmall"/></center><br />
<asp:Label ID="lblDiaryyHiddend" runat="server" Visible="false"/>
<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /></asp:Panel>
</ContentTemplate></asp:UpdatePanel>          
</div></div>
<br /><br />
</asp:Content>