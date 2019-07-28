<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamCenter.aspx.cs" Inherits="Exam_ExamCenter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Center
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server"/>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab" onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Exam Center" CssClass="redirecttabhome"></asp:Label></td></tr></table>
</div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1>
<h1>Examination Center Registration</h1></div><br />
<div id="visisble" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
 <table  class="tbl">
 <tr><td>Examination Session:</td><td><asp:DropDownList ID="ddlExamSeason" 
         runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" 
         AutoPostBack="true"  CssClass="txtbox" Width="150px" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>
 &nbsp;Year:&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="txtYearSeason_TextChanged" Width="80px"></asp:TextBox></td></tr>
<tr><td>Ceter City:</td><td><asp:DropDownList ID="ddlCity" runat="server"  CssClass="txtbox" Width="150px"  AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedInexChanged"
  Height="20px" >
    </asp:DropDownList></td><td>ExamCenterID &nbsp;<asp:Label ID="txtExamID" runat="server" Enable="false"
         CssClass="txtbox" ForeColor="Maroon" Width="80px"></asp:Label></td></tr>
 <tr><td>Name of Center:</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox" Width="350px" Font-Bold="true" Font-Size="18px" Height="25px"></asp:TextBox><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="abuild" ErrorMessage="Please Insert Center Name">*</asp:RequiredFieldValidator></td> </tr>
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
 <asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
   <ContentTemplate><table class="tbl">
<tr><td>Permanent Address:&nbsp;&nbsp; </td><td colspan="3"><asp:TextBox ID="txtPAddress" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="abuild" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtAddress2" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td></td><td><asp:TextBox ID="txtAddress3" runat="server" CssClass="txtbox" width="60%"></asp:TextBox>
    </td></tr>
    <tr><td>PinCode:</td><td><asp:TextBox ID="txtPPincode" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="abuild">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Phone:</td><td colspan="3"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox"  Width="100px"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Fax:</td><td colspan="3"><asp:TextBox ID="txtFaxCode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFaxNo" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFaxCode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFaxNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>Mobile:</td><td colspan="2"><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender>
</td></tr><tr><td >Email:</td><td colspan="2"><asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator22" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>
     </table> 
       </ContentTemplate>
         </asp:UpdatePanel>    
      <br />
     <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="abuild" runat="server" />
        <br />
      <div class="fromRegisterlbl"><h1>Account Information:</h1></div>
        <br />
<table class="tbl">
<tr><td>A/C No:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
<td style="margin-left: 120px"><asp:TextBox ID="txtAcNo" runat="server" CssClass="txtbox"></asp:TextBox></td>
<td>IFSC Code:</td>
<td><asp:TextBox ID="txtIFSCCode" runat="server" CssClass="txtbox"></asp:TextBox></td>
</tr>
<tr><td>Bank Name:</td>
<td style="margin-left: 80px"><asp:TextBox ID="txtBankname" runat="server" CssClass="txtbox"></asp:TextBox></td>
<td>Address:</td>
<td><asp:TextBox ID="txtBAdd" runat="server" CssClass="txtbox"></asp:TextBox></td>
</tr>
<tr><td>DD In Favour:</td>
<td><asp:TextBox ID="txtDDInFvr" runat="server" CssClass="txtbox"></asp:TextBox></td>
<td>Payable At:</td>
<td><asp:TextBox ID="txtPblAt" runat="server" CssClass="txtbox"></asp:TextBox></td>
</tr>
<tr><td>A/C Title:</td>
<td><asp:TextBox ID="txtAcTtl" runat="server" CssClass="txtbox"></asp:TextBox></td>
<td>Courier Service:</td>
<td><asp:TextBox ID="txtCor" runat="server" CssClass="txtbox"></asp:TextBox></td>
</tr>
</table>
        
           <br /><br /><center><asp:Label ID="lblException" runat="server" ></asp:Label>
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="abuild" Text="Register" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnsCalcel" runat="server" CssClass="btnsmall" Text="Cancel"  OnClick="btnCencel_Click"/></center>
<br /><br />
<div class="fromRegisterlbl"><h1> Building Facilities Available</h1></div>
        <br />
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
   <ContentTemplate>
        <asp:Panel ID="panelRoom" runat="server" >
        <center>
        <table><tr><td>Room No.:</td><td><asp:Label  ID="txtRoomNo" runat="server"></asp:Label></td></tr>
        <tr><td>Seating Capacity:</td><td><asp:TextBox ID="txtRoomCapacity" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Enter only Numbers." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="ADD">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtRoomCapacity" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr><tr><td>No. of Studnet Column(s):</td><td><asp:DropDownList ID="ddlRoomColumn" runat="server" CssClass="txtbox"><asp:ListItem Value="2" Text="2"></asp:ListItem><asp:ListItem Value="3" Text="3" /><asp:ListItem Value="4" Text="4" /><asp:ListItem Value="5" Text="5" /></asp:DropDownList>
          &nbsp;&nbsp;  <asp:Button ID="btnRoom" runat="server" CssClass="btnsmall" OnClick="btnRoom_click" 
                Text="Add" ValidationGroup="ADD" />
            </td></tr>
        </table>
    <asp:Label ID="lblExceptionRoom" runat="server" ></asp:Label>
        </center>
        <br /><center></center>
            <asp:GridView ID="gvroomshow" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" 
                Width="100%">
                <RowStyle BackColor="#F7F7DE" />
                <Columns>
                    <asp:BoundField DataField="Season" HeaderText="Season" 
                        SortExpression="Season" />
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="RoomNo" HeaderText="RoomNo" 
                        SortExpression="RoomNo" />
                    <asp:BoundField DataField="Capacity" HeaderText="Capacity" 
                        SortExpression="Capacity" />
                    <asp:BoundField DataField="Columns" HeaderText="Columns" 
                        SortExpression="Columns" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                SelectCommand="SELECT [Season], [ID], [RoomNo], [Capacity], [Columns] FROM [Rooms] WHERE (([Season] = @Season) AND ([ID] = @ID)) ORDER BY [SN] DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblHiddenSeason" Name="Season" 
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblEnrolment" Name="ID" PropertyName="Text" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br /><br />
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
 </div>
 <div id="invisible" runat="server" style="height:300px;" ></div>
 </div>
</asp:Content>

