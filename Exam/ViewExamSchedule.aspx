<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ViewExamSchedule.aspx.cs" Inherits="Exam_ViewExamSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Exam Schedule
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
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Schedule:- &nbsp;&nbsp;&nbsp; Type:&nbsp;<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" ><asp:ListItem Value="Home" Text="Home" ></asp:ListItem><asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Level:<asp:DropDownList ID="ddlSyllabus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSyllabus_OnslelectdIndexChanged" ></asp:DropDownList></h1></div><br />
<asp:UpdatePanel ID="updatePanel1" runat="server" ><ContentTemplate>
<table class="tbl" width="100%"><tr><td>Session:</td><td>
    <asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" 
        Width="150px" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtyear" runat="server" CssClass="txtbox" Width="60px" AutoCompleteType="FirstName" AutoPostBack="true" OnTextChanged="txtyear_TextChanged"></asp:TextBox><asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Invalid Year" ValueToCompare="9999" ControlToValidate="txtyear" Operator="LessThanEqual" Type="Double" ValidationGroup="A2">*</asp:CompareValidator></td>
<td><asp:Label ID="lblStreamName" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblStreamHidden" runat="server" Visible="false"></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:DropDownList ID="ddlCourse" runat="server" Width="150px" CssClass="txtbox" AutoPostBack="true"  OnTextChanged="ddlCourse_OnTextChanged" ><asp:ListItem Value="Architecture" Text="Architectural Engineering"></asp:ListItem><asp:ListItem Value="Civil" Text="Civil Engineering" /></asp:DropDownList></td><td>Section/Part:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" AutoPostBack="true" Width="80px" >
    <asp:ListItem Text="Part I" Value="PartI" /><asp:ListItem Value="PartII" 
        Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" />
    <asp:ListItem Value="SectionB" Text="Section B" ></asp:ListItem></asp:DropDownList></td></tr>
</table><asp:Label ID="lblSeason" runat="server" Visible="false"></asp:Label><br />
<p>&nbsp;&nbsp;<asp:Button ID="btnShow" runat="server" OnClick="btmShow_Grid" Text="Show"/></p><br />
  <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="Black" 
        DataKeyNames="SCode" 
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
          </Columns>
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
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>