<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ChangeCourse.aspx.cs" Inherits="Admission_ChangeCourse" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Change Student Course
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Delete Admission" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;<asp:ImageButton AlternateText="Results" runat="server" ID="iResults" ImageUrl="~/images/Results.jpg" Visible="false" /></h1><h1>
    Delete Admission(Student&#39;s Status should be DisActive)</h1></div><br />
    <asp:Panel runat="server" ID="pnlOld" runat="server" Visible="false">
<center>Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged1" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged" ></asp:TextBox><asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
<br /><asp:RadioButton ID="rbtnNewAdmission" runat="server" Text="New Admission" AutoPostBack="true"  GroupName="dev" oncheckedchanged="rbtnNewAdmission_CheckedChanged"/>&nbsp;&nbsp;&nbsp;
<asp:RadioButton   ID="rbtnOldAdmission" runat="server" GroupName="dev" AutoPostBack="true" Text="Old Admission" oncheckedchanged="rbtnOldAdmission_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
<asp:RadioButton ID="rbtnNewAdmissiontoOld" GroupName="dev" runat="server" Text="New Admission To Old Admission" AutoPostBack="true" OnCheckedChanged="rbtnNewAdmissionToOld_CheckChanged" />
<br />
<asp:RadioButton ID="rbtnMembership" runat="server" Text="Membership No" GroupName="nagar" />&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnSerailNo" runat="server" Text="Serial No" GroupName="nagar" />
&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtSID_TextChanged"  Width="80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOldSID" runat="server" CssClass="txtbox" Width="80px" AutoPostBack="true" OnTextChanged="txtOldSID_TextChanged"></asp:TextBox>&nbsp;&nbsp;<asp:ImageButton ImageUrl="../images/Message_Information.png" AlternateText="Info" ID="ibtnOldSIDInfo"  runat="server"/>
<dev:PopupControlExtender ID="PopupControlExtender1" runat="server" Position="Center" OffsetX="-350" OffsetY="0" PopupControlID="pnlResult" TargetControlID="iResults" ></dev:PopupControlExtender>
<asp:Panel ID="pnlResult" runat="server" CssClass="pnlpopup2" Width="400px">
Results<br />
PartI:&nbsp;<asp:Label ID="lblPartICount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
PartII:&nbsp;<asp:Label ID="lblPartIICount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
SectionA:&nbsp;<asp:Label ID="lblSectionACount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
SectionB:&nbsp;<asp:Label ID="lblSectionBCount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:GridView ID="GridResult" runat="server" AutoGenerateColumns="true" OnRowDataBound="GridResult_RowDataBound"></asp:GridView><br />
</asp:Panel>
<dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-350" OffsetY="0" PopupControlID="pnlOldSIDInfo" TargetControlID="ibtnOldSIDInfo" ></dev:PopupControlExtender>
<asp:Panel ID="pnlOldSIDInfo" runat="server" Width="350px" CssClass="pnlpopup" >
<center>Student Info</center>
<table class="tbl"><tr><td>Name:</td><td><asp:Label ID="lblOldName" runat="server" ForeColor="White" ></asp:Label></td></tr>
<tr><td>Father Name:</td><td><asp:Label ID="lblOldFatherName" runat="server"  ForeColor="White"></asp:Label></td></tr>
<tr><td>DOB:</td><td><asp:Label ID="lblOldDOB" runat="server" ForeColor="White" ></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:Label ID="lblOldCourse" runat="server"  ForeColor="White"></asp:Label></td></tr>
<tr><td>Part:</td><td><asp:Label ID="lblOldPart" runat="server"  ForeColor="White"></asp:Label></td></tr>
<tr><td>ExamStatus:</td><td><asp:Label ID="lblOldExamStatus" runat="server"  ForeColor="White"></asp:Label></td></tr>
<tr><td>Comp Status:</td><td><asp:Label ID="lblOldCompositeStatus" runat="server"  ForeColor="White"></asp:Label></td></tr>
<tr><td>PartII ExamForm:</td><td><asp:Label ID="lblOldCourseStatus" runat="server"  ForeColor="White"></asp:Label></td></tr>
</table>
</asp:Panel>
<table class="tbl"><tr><td>Student Name:</td><td><asp:Label ID="lblName" runat="server"  ForeColor="Maroon" Font-Size="15px" Font-Bold="true"></asp:Label></td><td>Father's Name:&nbsp;<asp:Label ID="lblFatherName" runat="server"  Font-Bold="true"  ForeColor="Maroon" Font-Size="15px"></asp:Label></td></tr></table>
<table class="tbl" width="100%"><tr align="center"><td>Application Status:</td><td>Admission Status</td><td>ExamForm</td><td>ITI Form</td></tr>
<tr align="center"><td align="center"><asp:Label ID="lblApplication" runat="server" Font-Bold="true"></asp:Label></td><td><asp:Label ID="lblAdmissionStatus" runat="server" Font-Bold="true"></asp:Label></td><td>[<asp:Label ID="lblExamCount" runat="server" Font-Bold="true" Text="0" ForeColor="Maroon"></asp:Label>]&nbsp;<asp:Label ID="lblExamSerialNo" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblExamFormStatus" runat="server" Font-Bold="true"></asp:Label></td><td><asp:Label ID="lblITISerialNo" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblITIFormStatus" runat="server" Font-Bold="true"></asp:Label></td></tr>
</table>
<asp:Panel ID="pnlExamForm" runat="server" Visible="false">
<center><asp:CheckBox ID="chkDeleteExamForm" Visible="false" runat="server" Text="Delete Exam Form" /></center>
    <asp:GridView ID="GridExam" runat="server" BackColor="White" Width="100%"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#330099" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <SortedAscendingCellStyle BackColor="#FEFCEB" />
        <SortedAscendingHeaderStyle BackColor="#AF0101" />
        <SortedDescendingCellStyle BackColor="#F6F0C0" />
        <SortedDescendingHeaderStyle BackColor="#7E0000" />
    </asp:GridView>
</asp:Panel><hr />
<table width="100%" class="tbl" runat="server" id="tblApps"><tr align="center"><td>Admission Course</td><td>Current Course</td></tr>
<tr align="center"><td><asp:Label ID="lblCourseAdmisison" runat="server" Font-Bold="true" ForeColor="BurlyWood"></asp:Label>&nbsp;&nbsp;<asp:Label runat="server" ID="lblPartAdmission" Font-Bold="true" ForeColor="BurlyWood"></asp:Label></td>
<td><asp:Label ID="lblCourseCurrent" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;<asp:Label runat="server" ID="lblPartCurrent" ForeColor="Maroon" Font-Bold="true"></asp:Label></td>
</tr>
<tr align="center"><td colspan="2">New Course:&nbsp;&nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" ><asp:ListItem Value="Civil" Text="Civil Engineering"></asp:ListItem><asp:ListItem Value="Architecture" Text="Architecture Engineering"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblHiddenStream" runat="server" Visible="false"></asp:Label><asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" ><asp:ListItem Value="PartI" Text="PartI" /><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Value="SectionA" Text="SectionA" /><asp:ListItem Value="SectionB" Text="SectionB" /></asp:DropDownList></td>
<td>PartII ExamForm:&nbsp;<asp:DropDownList ID="ddlCourseStatus" runat="server" CssClass="txtbox"><asp:ListItem Value="NotPassed" Text="Not Passed" /><asp:ListItem Value="Passed" Text="Passed" /><asp:ListItem Value="Promotted" Text="Promotted" /><asp:ListItem Value="Submitted" Text="Part II Submitted" /><asp:ListItem Value="NotSubmitted" Text="NotSubmitted"></asp:ListItem><asp:ListItem Value="Filled" Text="Filled"></asp:ListItem></asp:DropDownList></td></tr>
<tr align="center"><td colspan="2">Amount:&nbsp;<asp:Label ID="lblAmount" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Size="15px"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblExamAmount" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Size="15px"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblAmountType" runat="server" ></asp:Label></td></tr>
</table>
<table class="tbl">
<tr><td>Course Remarks :</td>
<td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="38px"></asp:TextBox></td></tr>
<tr><td>Additional Paper:</td><td><asp:DropDownList ID="ddlAdditionalPaper" runat="server" CssClass="txtbox">
<asp:ListItem Value="" Text="Select" />
<asp:ListItem Value="YES" Text="YES" />
<asp:ListItem Value="NO" Text="NO" />
</asp:DropDownList></td><td><asp:Label ID="lblAdditionalPaper" runat="server"></asp:Label></td></tr>
</table>
<asp:RequiredFieldValidator ID="ReqRemarks" runat="server" ControlToValidate="txtRemarks" Display="Dynamic" Text="Please Insert Remarks" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Insert Remarks." ValidationGroup="vali"></asp:RequiredFieldValidator>
<br />
<asp:Label ID="lblException" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
<asp:Button ID="btnUpdate" runat="server" ValidationGroup="Vali" CssClass="btnsmall" Text="Update" OnClick="btnUpdate_Click"  OnClientClick="return confirm('Are you sure, Update Course ?');"/>
  </center></asp:Panel>
<br /><center>
Delete Admission Form whose Membership no Not Generated.<br />
<b>Memebership No (For Admission Rollback):</b>&nbsp;<asp:TextBox runat="server" ID="txtStudentSID" CssClass="txtbox" Font-Bold="true" Width="100px"/>&nbsp;<asp:Button ID="btnOk" runat="server" CssClass="btnsmall" Text="OK" onclick="btnOk_Click" /><br />
<br /><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Brown" /><br />
<asp:GridView ID="GridAdmissionRollback" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="false" OnRowDeleting="DeleteRecord" DataKeyNames="SID" >
             <Columns>
             <asp:TemplateField HeaderText="SN">
            <ItemTemplate><%# Eval("SN") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="SID">
            <ItemTemplate><%# Eval("SID")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
            <ItemTemplate><%# Eval("Name") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Course">
            <ItemTemplate><%# Eval("Course") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Part">
            <ItemTemplate><%# Eval("Part") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Session">
            <ItemTemplate><%# Eval("Session") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
            <ItemTemplate><%# Eval("Status") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete?">
            <ItemTemplate>
            <span onclick="return confirm('Are you sure, Do you want to do Admission Form Rollback?')">
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/images/delete.png" />
            </span>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" HorizontalAlign="Center" />
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#330099" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" HorizontalAlign="Center" />
        <SortedAscendingCellStyle BackColor="#FEFCEB" />
        <SortedAscendingHeaderStyle BackColor="#AF0101" />
        <SortedDescendingCellStyle BackColor="#F6F0C0" />
        <SortedDescendingHeaderStyle BackColor="#7E0000" />
</asp:GridView>
</center>
</ContentTemplate></asp:UpdatePanel>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br /><br />
</div>
</asp:Content>