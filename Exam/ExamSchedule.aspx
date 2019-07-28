<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamSchedule.aspx.cs" Inherits="Exam_ExamSchedule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Schedule
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Schedule: &nbsp;&nbsp;&nbsp; Type:&nbsp;<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_OnselectedIndexChanged" ><asp:ListItem Value="Home" Text="Home" ></asp:ListItem><asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Level:<asp:DropDownList ID="ddlSyllabus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSyllabus_OnslelectdIndexChanged" ></asp:DropDownList></h1></div><br />
<asp:UpdatePanel ID="updatePanel1" runat="server" ><ContentTemplate>
<table class="tbl" width="100%"><tr><td>Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" 
        Width="150px" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtyear" runat="server" CssClass="txtbox" Width="60px" AutoCompleteType="FirstName" AutoPostBack="true" OnTextChanged="txtyear_TextChanged"></asp:TextBox><asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Invalid Year" ValueToCompare="9999" ControlToValidate="txtyear" Operator="LessThanEqual" Type="Double" ValidationGroup="A2">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtyear" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
<td><asp:Label ID="lblStreamName" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblStreamHidden" runat="server" Visible="false"></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:DropDownList ID="ddlCourse" runat="server" 
        Width="150px" CssClass="txtbox" AutoPostBack="true"  
        OnTextChanged="ddlCourse_OnTextChanged" ><asp:ListItem Value="Architecture" Text="Architectural Engineering"></asp:ListItem><asp:ListItem Value="Civil" Text="Civil Engineering" /></asp:DropDownList></td><td>Section/Part:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" AutoPostBack="true" Width="80px" >
    <asp:ListItem Text="Part I" Value="PartI" /><asp:ListItem Value="PartII" 
        Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" />
    <asp:ListItem Value="SectionB" Text="Section B" ></asp:ListItem></asp:DropDownList></td></tr>
</table><asp:Label ID="lblSeason" runat="server" Visible="false"></asp:Label>
<p style="float:right; margin-right:30px;"><asp:Button ID="btnDeleteRow" runat="server" OnClick="btnDeleteRow_Click" CssClass="btnsmall" Text="Delete Selected" /></p>
<center><asp:Label ID="lblRowDeleted" runat="server" ></asp:Label></center>
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="Black" 
        DataKeyNames="SCode" OnRowDeleted="GridView3_RowDeleted" OnRowDataBound="GridView3_OnRowDataBound"
        GridLines="Vertical" PageSize="15" Width="100%" 
        onselectedindexchanged="GridView3_SelectedIndexChanged">
        <RowStyle BackColor="#F7F7DE" />
        <EmptyDataRowStyle BackColor="#6B696B" Font-Bold="true" ForeColor="White" />
        <EmptyDataTemplate><center>Schedule Record not found.</center></EmptyDataTemplate>
        <Columns>
         <asp:BoundField DataField="SCode" HeaderText="Subject Code" SortExpression="SCode" />
         <asp:BoundField DataField="SName" HeaderText="Subject Name" SortExpression="SName" />
         <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
         <asp:BoundField DataField="Shift" HeaderText="Shift" SortExpression="Shift" />
        <%-- <asp:BoundField DataField="PScode" HeaderText="Setter Code" SortExpression="PScode" />
         <asp:BoundField DataField="PSName" HeaderText="Name" SortExpression="PSName" />--%>
           <asp:CommandField ShowSelectButton="True" SelectText="Delete" /></Columns>
        <FooterStyle BackColor="#CCCC99" /><PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT [SCode], [SName], [Date], [Shift] FROM [ExamDate] WHERE ([Season] = @Season)  AND ([Course] = @Course) AND ([Part] = @Part) AND ([Type] = @Type) AND ([CourseID] = @CourseID) ORDER BY [DATE]"
        DeleteCommand="Delete from ExamDate where ([SCode]=@SCode) AND ([CourseID]=@CourseID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblSeason" Name="Season" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="ddlCourse" Name="Course" 
                PropertyName="SelectedValue" Type="String" />     
            <asp:ControlParameter ControlID="ddlPart" Name="Part" 
                PropertyName="SelectedValue" Type="String" />
                  <asp:ControlParameter ControlID="ddlType" Name="Type" 
                PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ddlSyllabus" Name="CourseID"
                 PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <DeleteParameters>
        <asp:ControlParameter ControlID="ddlSyllabus" Name="CourseID" PropertyName="SelectedValue" Type="String" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <br />
<br />
<table><tr><td>Subject Code:</td><td><asp:Label ID="lblSCode" runat="server" ></asp:Label></td><td>Subject Name:&nbsp;&nbsp;<asp:Label ID="lblSName" runat="server" ></asp:Label></td></tr>
<tr><td>Examination Date:</td><td><asp:TextBox ID="txtDate" runat="server" 
        CssClass="txtbox" AutoPostBack="True" ontextchanged="txtDate_TextChanged"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Form Receiving Date" >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td><td>Time:&nbsp;&nbsp;<asp:DropDownList ID="ddlHr" runat="server" Width="50px" CssClass="txtbox"></asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="ddlMin" runat="server" Width="50px" CssClass="txtbox"></asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="ddlmeridian" runat="server" Width="50px" CssClass="txtbox"><asp:ListItem Value="AM" Text="AM" Selected="True" /><asp:ListItem Value="PM" Text="PM"></asp:ListItem></asp:DropDownList>
    </td></tr>
    <tr><td>Duration:&nbsp;</td><td><asp:DropDownList ID="ddlDuHr" runat="server" Width="50px" CssClass="txtbox"></asp:DropDownList>&nbsp;Hr.&nbsp;<asp:DropDownList ID="ddlDuMin" runat="server" Width="50px" CssClass="txtbox"></asp:DropDownList>&nbsp;Min.</td><td>Shift:&nbsp;&nbsp;<asp:DropDownList ID="ddlShift" runat="server" ><asp:ListItem Value="FN" Text="FN" /><asp:ListItem Value="AN" Text="AN" /></asp:DropDownList></td></tr>
    <tr><td></td><td><asp:TextBox ID="txtPSetterCode" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtPSetterCode_Techchanged"></asp:TextBox></td><td><asp:TextBox ID="txtPSetterName" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
</table><br /><center><b><asp:Label ID="lblException" runat="server" ></asp:Label></b></center><br /><center>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
            CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
            ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCencel_click" 
            CssClass="btnsmall" /></center><br /><br />
<asp:Panel ID="panelAsso" runat="server" >
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
        CellPadding="2" DataSourceID="SqlDataSource1" ForeColor="Black" 
        GridLines="None" Width="100%" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="SubID" HeaderText="Subject Code" SortExpression="SubID" />
            <asp:BoundField DataField="SubName" HeaderText="Subject Name" 
                SortExpression="SubName" />
            <asp:BoundField DataField="MaxMark" HeaderText="Max. Marks" 
                SortExpression="MaxMark" />
            <asp:BoundField DataField="MinMark" HeaderText="Min. Passing Marks" 
                SortExpression="MinMark" />
            <asp:BoundField DataField="First" HeaderText="First Div. Marks" SortExpression="First" />
            <asp:BoundField DataField="SubjectType" HeaderText="Type" SortExpression="SuhjectType" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
            HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT DISTINCT [SubID], [SubName], [MaxMark], [MinMark], [First], [SubjectType] FROM [ArchiSubMaster] WHERE (([Section] = @Section) AND ([CourseID]=@CourseID)) ORDER BY [SubID]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlPart" Name="Section" 
                PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ddlSyllabus" Name="CourseID" PropertyName="SelectedValue" Type="String" />
                
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Panel>
<asp:Panel ID="panelTech" runat="server" >
    <asp:GridView ID="GridView2" runat="server" 
 AllowPaging="True" 
             AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"  PageSize="30"
             BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
             DataSourceID="SqlDataSource2" ForeColor="Black" GridLines="Vertical" 
             Width="100%"  onpageindexchanging="GridView2_PageIndexChanging"
        onselectedindexchanged="GridView2_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="SubID" HeaderText="Subect Code" SortExpression="SubID" />
            <asp:BoundField DataField="SubName" HeaderText="Subject Name" 
                SortExpression="SubName" />
            <asp:BoundField DataField="MaxMark" HeaderText="Max. Mark" 
                SortExpression="MaxMark" />
            <asp:BoundField DataField="MinMark" HeaderText="Min. Passing Marks" 
                SortExpression="MinMark" />
            <asp:BoundField DataField="First" HeaderText="First Div. Marks" SortExpression="First" />
            <asp:BoundField DataField="SubjectType" HeaderText="Type" SortExpression="SuhjectType" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
        HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT DISTINCT [SubID], [SubName], [MaxMark], [MinMark], [First],[SubjectType] FROM [CivilSubMaster] WHERE ([Section] = @Section) AND ([CourseID]=@CourseID) ORDER BY [SubID], [SubjectType]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlPart" Name="Section" 
                PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ddlSyllabus" Name="CourseID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Panel></ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>

