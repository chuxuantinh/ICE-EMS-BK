<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdministrator.master" AutoEventWireup="true" CodeFile="AdminCreate.aspx.cs" Inherits="Admin_AdminCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Admin Account
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="AdminStyle.css" type="text/css" charset="utf-8" />	
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:Label ID="lblNext" runat="server" Text="Create Module Admin" CssClass="redirecttabhome"></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<asp:Panel ID="Panel1" runat="server">
<div id="header">
<asp:Panel ID="panelCreate" runat="server" CssClass="panel" >
<div id="paneldiv"><h1>Create New Admin</h1><br />
 <asp:UpdatePanel ID="updatepanel3" runat="server" > <ContentTemplate>
<center><asp:Label ID="lblActiveId" runat="server" ></asp:Label></center>
<table>
<tr><td>New Admin Name:</td><td><asp:TextBox ID="txtUserName" 
        AutoPostBack="true" runat="server" CssClass="txtbox" 
        ontextchanged="txtUserName_TextChanged" Width="135px"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtUserName" Display="Dynamic" ValidationGroup="A1" errormessage="Insert Admin UserName" >*</asp:RequiredFieldValidator></td></tr>
       <tr><td>Password:</td><td><asp:TextBox ID="txtPassword" runat="server" 
               TextMode="Password" CssClass="txtbox" Width="135px" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
               controltovalidate="txtPassword" Display="Dynamic" 
               errormessage="Please Insert Password." ValidationGroup="A1">*</asp:RequiredFieldValidator>
           </td><td><dev:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="txtPassword" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" PreferredPasswordLength="10" BarIndicatorCssClass="strpoor;straverage;strunbreakable" BarBorderCssClass="bar"   StrengthStyles="strpoor;straverage;strunbreakable">
        </dev:PasswordStrength></td></tr><tr><td>Confirm Password:</td><td><asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" 
            CssClass="txtbox" Width="135px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            controltovalidate="txtConfirmPassword" Display="Dynamic" 
            errormessage="Please Insert Confirmation Password." ValidationGroup="A1">*</asp:RequiredFieldValidator>
            <br /><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Display="Dynamic" ErrorMessage="Insert Same  Password." Operator="Equal" ValidationGroup="A1"></asp:CompareValidator>
       </td></tr>
</table>
<table>       <tr><td>Name:</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txtName" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Admin Full Name" >*</asp:RequiredFieldValidator></td><td>Email:</td><td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ValidationGroup="A1" Display="Dynamic" ErrorMessage="Invalid Email Format">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="txtEmail" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Email." >*</asp:RequiredFieldValidator></td></tr>
       <tr><td>Designation:</td><td><asp:TextBox ID="txtDesignation" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" controltovalidate="txtDesignation" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Designation">*</asp:RequiredFieldValidator></td><td><asp:TextBox ID="txtFacultyId" runat="server" Visible="false" CssClass="txtbox"></asp:TextBox></td></tr>      
</table>
<table>
       <tr align="left"><td><asp:CheckBox ID="chkmembership" runat="server" Text="Membership" /></td><td><asp:CheckBox ID="chkFOffice" runat="server" Text="Front Office" /></td><td><asp:CheckBox ID="chkAdmission" runat="server" Text="Admission" /></td><td><asp:CheckBox ID="chkAcc" runat="server" Text="Accounts" />
           &nbsp;</td></tr>
     <tr align="left"><td><asp:CheckBox ID="chkExam" runat="server" Text="Examination" /></td><td><asp:CheckBox ID="chkInventory" runat="server" Text="Inventory" />
         &nbsp;&nbsp; </td><td><asp:CheckBox ID="chkProject" runat="server" Text="Project" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td><td><asp:CheckBox ID="chkReports" runat="server" Text="Report(s)" /></td></tr>
</table><br />
<center><asp:Label ID="lblException" runat="server" ForeColor="Brown" ></asp:Label></center>
<br /><asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ValidationGroup="A1" CssClass="expbox" /></ContentTemplate></asp:UpdatePanel>
<center><asp:Button ID="btnCreate" runat="server" Text="Save" CssClass="btnsmall" OnClick="btnCreate_Click" ValidationGroup="A1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" Text="Clear" OnClick="btnClearCreate_Click" CssClass="btnsmall" runat="server" /></center><br /><br />
  </div></asp:Panel>
</div>
<center></center><br />
</asp:Panel>
<asp:Panel ID="panelupdate" runat="server" CssClass="panel">
 <div class="paneldivupdate"><h1>Recover Admin Password</h1>
<center>
<asp:Panel ID="panelAdmin" runat="server">
<table><tr><td>Log In Name:</td><td><asp:Label ID="lblAdminNameUp" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label></td></tr>
       <tr><td>Old Password:</td><td><asp:Label ID="lblPasswordUp" runat="server"></asp:Label></td></tr>
 </table>
 </asp:Panel>
  </center> 
 <center><asp:Label ID="lblselectNote" runat="server" Font-Bold="true"></asp:Label></center>
 <br />
 <table>  <tr><td>New Password:</td><td><asp:TextBox ID="txtPasswordUp"  TextMode="Password" runat="server" CssClass="txtbox" ></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="reqpass" runat="server" ControlToValidate="txtPasswordUp" Display="Dynamic" ValidationGroup="up" ErrorMessage="Insert Password" SetFocusOnError="true">*</asp:RequiredFieldValidator></td><td>Confirm Password:</td><td><asp:TextBox ID="txtConfirmPassUp" TextMode="Password" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="reqPassConfig" runat="server" Display="Dynamic" ValidationGroup="up" ControlToValidate="txtConfirmPassUp" ErrorMessage="Insert Confirm Password" SetFocusOnError="true"></asp:RequiredFieldValidator>
<br /> <asp:CompareValidator runat="server" Operator="Equal" ControlToCompare="txtPasswordUp" ControlToValidate="txtConfirmPassUp" ErrorMessage="Confirm Same Password" ValidationGroup="up" Display="Dynamic" SetFocusOnError="true">*</asp:CompareValidator></td></tr>
    </table>
       <br /><asp:ValidationSummary ID="validation" runat="server" DisplayMode="BulletList" ValidationGroup="up" CssClass="expbox" />
      <center><asp:Label ID="lblmesg" runat="server"> </asp:Label></center>
       <center><asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="up" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClearUp" Text="Clear" OnClick="btnClearUp_Click" runat="server" CssClass="btnsmall" /> </center>
       <br /> 
<center><asp:Label ID="lblselecttext" runat="server" Font-Bold="true"></asp:Label></center>
<br />
 </div>
</asp:Panel>
<asp:Panel ID="paneldelete" runat="server" CssClass="panel">
<div class="paneldivupdate">
<h1>Delete Account</h1><asp:Label ID="Label1" runat="server" ></asp:Label><br /><center><asp:Label ID="lblDeletetitle" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label><br />
<asp:Panel ID="panelAdminDelete" runat="server" >
<table ><tr><td align="right">Log In Name:&nbsp;&nbsp;&nbsp;&nbsp;</td><td align="left"><asp:Label ID="lblAdminDelete" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label></td></tr>
       <tr><td align="right">Email Id:&nbsp;&nbsp;&nbsp;&nbsp;</td><td align="left"><asp:Label ID="lblEmailDelete" runat="server"></asp:Label></td></tr>
 </table></asp:Panel>
 <br /><asp:Button ID="btndelete" runat="server"
            Text="Delete Account forever" OnClientClick="return confirm('Are you sure delete account forever ?');" onclick="btndelete_Click" /></center><br />
  </div>  </asp:Panel>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Width="100%" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White"  PageSize="25"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_OnRowDataBound" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <HeaderStyle HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="SN" HeaderText="S No" InsertVisible="False" Visible="false" 
                SortExpression="SN" />
            <asp:BoundField DataField="LogName" HeaderText=" User Name" 
                SortExpression="LogName" />
            <asp:BoundField DataField="Password" HeaderText="Password" Visible="false" 
                SortExpression="Password" />
            <asp:BoundField DataField="LDate" HeaderText="Register Date" SortExpression="LDate"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Designation" HeaderText="Designation" 
                SortExpression="Designation" />
            <asp:BoundField DataField="EmpId" HeaderText="Faculty ID" Visible="false" SortExpression="EmpId" />
            <asp:BoundField DataField="Lavel" HeaderText="Lavel" SortExpression="Lavel" Visible="false" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT [SN], [LogName], [Password], [LDate], [Email], [Name], [Designation], [EmpId], [Lavel] FROM [Login] WHERE ([Lavel] = @Lavel) order by LogName">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="Lavel" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <!-- content end -->
    </div><br />
    <!-- end right panel ---->
    </div>
</asp:Content>

