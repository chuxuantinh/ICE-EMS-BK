<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdministrator.master" AutoEventWireup="true" CodeFile="Create.aspx.cs" Inherits="Create" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
            <div id="rightborder">
<div id="header">
  <div id="paneldiv"><h1><asp:Label ID="lblHeadertitle" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label></h1>
      <br />
<asp:panel ID="panelUpdatePassword" runat="server" >
     <center><asp:Label ID="Lblmesg" runat="server" ></asp:Label></center>
     <asp:panel ID="panelAdmin" runat="server" >
     <table><tr><td>Log In Name:</td><td align="left"><asp:Label ID="lblAdminNameUp" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label></td></tr>
           <tr><td>Old Password:</td><td align="left"><asp:Label ID="lblPasswordUp" runat="server"></asp:Label></td></tr>
           <tr><td>Name:</td><td align="left"><asp:Label ID="lblName" runat="server" ></asp:Label></td></tr>
           <tr><td>Designation:</td><td align="left"><asp:Label ID="lblDesignation" runat="server" ></asp:Label></td></tr>
     </table>
     </asp:panel>
    <center><asp:Label ID="lblselectNote" ForeColor="Maroon" Font-Bold="true" runat="server"></asp:Label></center> 
     <table>  <tr><td>New Password:</td><td><asp:TextBox ID="txtPasswordUp"  TextMode="Password" runat="server" CssClass="txtbox" ></asp:TextBox>
         <asp:RequiredFieldValidator ID="regxmemfeeB" runat="server" 
             controltovalidate="txtPasswordUp" Display="Dynamic" 
             errormessage="Please Insert New Password." ValidationGroup="A2">*</asp:RequiredFieldValidator>
         </td><td><dev:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtPasswordUp" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" PreferredPasswordLength="10" BarIndicatorCssClass="strpoor;straverage;strunbreakable" BarBorderCssClass="bar"   StrengthStyles="strpoor;straverage;strunbreakable">
            </dev:PasswordStrength> </td></tr>
            <tr><td>Confirm Password:</td><td><asp:TextBox ID="txtConfirmPassUp" TextMode="Password" runat="server" CssClass="txtbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    controltovalidate="txtConfirmPassUp" Display="Dynamic" 
                    errormessage="Please Insert Confirmation Password" ValidationGroup="A2">*</asp:RequiredFieldValidator>
                <br /><asp:CompareValidator ID="conpareValidator1" runat="server" ControlToValidate="txtConfirmPassUp" ControlToCompare="txtPasswordUp" Display="Dynamic" ErrorMessage="Please Insert Same Password." Operator="Equal" ValidationGroup="A2"></asp:CompareValidator></td></tr>
            <tr ><td colspan="3" > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnsmall" OnClick="btnUpdate_Click" ValidationGroup="A2" /></td> </tr>
            </table>
          
            <center><asp:Label ID="lblselecttext" ForeColor="Maroon" Font-Bold="true" runat="server" ></asp:Label></center>
  </asp:panel>
<asp:Panel ID="panelCreate" runat="server" >
     <asp:UpdatePanel ID="padatePanelcheck" runat="server" ><ContentTemplate>
     <center><asp:Label ID="lblActiveId" runat="server" ></asp:Label></center>
        <table>
           <tr><td>New Admin Name:</td><td><asp:TextBox ID="txtUserName" AutoPostBack="true" 
                   runat="server" CssClass="txtbox" ontextchanged="txtUserName_TextChanged" 
                   Width="135px"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtUserName" Display="Dynamic" ValidationGroup="A1" errormessage="Insert Admin UserName" >*</asp:RequiredFieldValidator></td></tr>
           <tr><td>Password:</td><td><asp:TextBox ID="txtPassword" runat="server" 
                   TextMode="Password" CssClass="txtbox" Width="135px" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="txtPassword" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Password." >*</asp:RequiredFieldValidator></td><td><dev:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="txtPassword" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" PreferredPasswordLength="10" BarIndicatorCssClass="strpoor;straverage;strunbreakable" BarBorderCssClass="bar"   StrengthStyles="strpoor;straverage;strunbreakable">
                    </dev:PasswordStrength></td></tr>
                    <tr><td>Confirm Password:</td><td><asp:TextBox ID="txtConfirmPassword" 
                            TextMode="Password" runat="server" CssClass="txtbox" Width="135px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            controltovalidate="txtConfirmPassword" Display="Dynamic" 
                            errormessage="Please Insert Confirmation Password." ValidationGroup="A1">*</asp:RequiredFieldValidator>
                        <br /><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Display="Dynamic" ErrorMessage="Please Insert Same Password." Operator="Equal" ValidationGroup="A1"></asp:CompareValidator></td></tr></table>
      </ContentTemplate></asp:UpdatePanel>
     <table><tr><td>Name:</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txtName" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Admin Full Name" >*</asp:RequiredFieldValidator></td><td>Email:</td><td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ValidationGroup="A1" Display="Dynamic" ErrorMessage="Invalid Email Format">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="txtEmail" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Email." >*</asp:RequiredFieldValidator></td></tr>
       <tr><td>Designation:</td><td><asp:TextBox ID="txtDesignation" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" controltovalidate="txtDesignation" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Designation">*</asp:RequiredFieldValidator></td></tr>      
    </table>
<asp:Panel ID="panelMembership" runat="server" >
               <table><tr><td><asp:CheckBox ID="chkRegisterMember" runat="server" Text="Register Member" /></td>
                          <td><asp:CheckBox ID="chkRenewalReg" runat="server" Text="Renewal Registration" /></td>
               </tr></table>
</asp:Panel>
<asp:Panel ID="panelFrontOffice" runat="server" >
              <table><tr>
                <td><asp:CheckBox ID="chkFOffice" runat="server" Text="Visitors and Counselling" /></td>
                <td><asp:CheckBox ID="chkEnquiry" runat="server" Text="Diary Entry" /></td></tr>
               <tr><td><asp:CheckBox ID="chkCourier" runat="server" Text="Courier Dispetch" /></td>
               <td><asp:CheckBox ID="chkD2D" runat="server" Text="Dairy To Department" /></td></tr>
          </table>
</asp:Panel>
<asp:Panel ID="panelAdmission" runat="server" ><table><tr>
            <td><asp:CheckBox ID="chkAdmission" runat="server" Text="Admission" /></td>
            <td><asp:CheckBox ID="chkAdmissionApprove" runat="server" Text="Approve Admisison" /></td>
            </tr></table></asp:Panel>
<asp:Panel ID="panelFees" runat="server" >
       <table><tr align="left">
       <td><asp:CheckBox ID="chkMainACFees" runat="server" Text="Main Account" /></td>
       <td><asp:CheckBox ID="chkLateFees" runat="server" Text="Beneficiary A/C" /></td>
       <td><asp:CheckBox ID="chkAddApp" runat="server" Text="Application Froms Entry" /></td>
       <td><asp:CheckBox ID="chkAppApprove" runat="server" Text="Approve Application Forms" />
       <asp:CheckBox ID="chkMembershipAC" runat="server" Text="Membership A/C" Visible="false" /></td></tr>
       <tr><td><asp:CheckBox ID="chkExamBill" runat="server" Text="Examination A/C" /></td>
       </tr>  </table><!-- Exam Fee==Account and membership Fee== Admission Form -->
</asp:Panel>
<asp:Panel ID="panelExamInsert" runat="server" ><table class="tbl" width="100%"><tr align="left">
        <td align="left"><asp:CheckBox ID="chkExamCenter" runat="server" Text="Exam Center" /></td>
        <td align="left"><asp:CheckBox ID="chkExamSchedule" runat="server" Text="Exax Schedule"/></td>
        <td align="left"><asp:CheckBox ID="chkMarkFeed" runat="server" Text="Marks Feed" /></td>
        <td align="left"><asp:CheckBox ID="chkExamForm" runat="server" Text="Exam Forms"/></td></tr>
        <tr align="left"><td align="left"><asp:CheckBox ID="chkAdminCard" runat="server" Text="Admit Card" /></td>
        <td align="left"><asp:CheckBox ID="chkExamPaper" runat="server" Text="Exam Paper Manager" /></td>
        <td align="left"><asp:CheckBox ID="chkPaperSetter" runat="server" Text="Paper Setter Profile" /></td>
        <td align="left"><asp:CheckBox ID="chkRollNO" runat="server" Text="Roll No. Generation" /></td></tr>
       <tr align="left"><td align="left"><asp:CheckBox ID="chkCertificate" runat="server" Text="Certificates" /></td>
       <td  align="left"><asp:CheckBox ID="chkMarking" runat="server" Text="Examination Marking" /></td>
       <td align="left"><asp:CheckBox ID="chkMarksheet" runat="server" Text="Marksheet" /></td>
       <td align="left"><asp:CheckBox ID="chkSeating" runat="server" Text="Seating Arrangement" /></td></tr>
       <tr align="left"><td align="left"><asp:CheckBox ID="chkUFM" runat="server" Text="UFM" />
       </td><td  align="left"><asp:CheckBox ID="chkRechecking" runat="server" Text="Re-Checking Form"  /></td>
       <td align="left"><asp:CheckBox ID="CheckBox4" runat="server" Text="Marksheet"  Visible="false"/></td>
       <td align="left"><asp:CheckBox ID="CheckBox5" runat="server" Text="Seating Arrangement"  Visible="false"/></td></tr>
   </table>
</asp:Panel>
<asp:Panel ID="panelInventory" runat="server" >
       <table><tr><td><asp:CheckBox ID="chkStockInout" runat="server" Text="Stock Manager" /></td>
       <td><asp:CheckBox ID="chkSuplier" runat="server" Text="Supplier Manager" /></td>
       <td><asp:CheckBox ID="chkPurchesOrder" runat="server" Text="Purches" Visible="false" /></td>
       </tr></table>
</asp:Panel>
<asp:Panel ID="panelProject" runat="server" >
<table><tr align="left"><td align="left"><asp:CheckBox ID="chkSynopsis" runat="server" Text="Synopsis" /></td>
<td align="left"><asp:CheckBox ID="chkProApprove" runat="server" Text="Project Allocation" /></td></tr>
<tr align="left"><td align=left><asp:CheckBox ID="chkCopySubmit" runat="server" Text="Project Submission" /></td>
<td><asp:CheckBox ID="chkProEvaluation" runat="server" Text="Evaluation" /></td></tr>
</table>
</asp:Panel>
<br /><asp:ValidationSummary ID="validation" runat="server" DisplayMode="BulletList" ValidationGroup="A2" CssClass="expbox" />
     <center>
         <asp:Label ID="lblException" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
     </center>
<br /><asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ValidationGroup="A1" CssClass="expbox" />
<center><asp:Button ID="btnCreate" runat="server" CssClass="btnsmall" Text="Save" OnClick="btnCreate_Click" ValidationGroup="A1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" Text="Clear" CssClass="btnsmall" OnClick="btnClearCreate_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</center><br /><br />
</asp:Panel>
</div>
<asp:Panel ID="paneldelete" runat="server">
<div class="paneldivupdate">
<center><asp:Label ID="lblDeletetitle" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label><br />
<table><tr align="left"><td>Log In Name:&nbsp;&nbsp;&nbsp;&nbsp;</td>
    <td align="left"><asp:Label ID="lblAdminDelete" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label></td></tr>
       <tr><td>Email Id:&nbsp;&nbsp;&nbsp;&nbsp;</td><td align="left"><asp:Label ID="lblEmailDelete" runat="server"></asp:Label></td></tr>
 </table><br /><asp:Button ID="btndelete" runat="server" CssClass="btnsmall"
            Text="Delete Account forever" OnClientClick="return confirm('Are you sure delete account forever ?');" onclick="btndelete_Click" /></center><br />
  </div>  </asp:Panel>
</div><br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Width="100%" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <RowStyle BackColor="#F7F7DE" />
        <HeaderStyle HorizontalAlign="Center" />
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
                    SelectCommand="SELECT SN, LogName, Password, LDate, Email, Name, Designation, EmpId, Lavel FROM Login WHERE (Lavel = @Lavel) AND (Type = @Type) order by LogName">
        <SelectParameters>
            <asp:Parameter DefaultValue="2" Name="Lavel" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="Type" QueryStringField="typ" />
        </SelectParameters>
    </asp:SqlDataSource>
    </div>
</div>
   <br />
</asp:Content>

