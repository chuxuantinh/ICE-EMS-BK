<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamFormImprovement.aspx.cs" Inherits="Exam_ExamFormImprovement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exam Form Improvement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    input:focus,textarea:focus,select:focus {
    outline:1px solid red;  
}
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Exam Form" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div><div id="rightpanel2">
<asp:UpdatePanel ID="updatePanel1" runat="server" >
    <Triggers>
    </Triggers>
<ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Form&nbsp;&nbsp;&nbsp;Exam Schedule:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSchedule" runat="server" CssClass="txtbox"><asp:ListItem Value="Home" Text="Home" /><asp:ListItem Value="Overseas" Text="Overseas" /></asp:DropDownList></h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;<asp:Label ID="lblSessionID" runat="server"></asp:Label></td></tr></table></center>
<br /><center>Application  No or Membership:&nbsp;&nbsp;<asp:TextBox ID="txticesn" runat="server" Width="100px" CssClass="txtbox"></asp:TextBox>&nbsp; &nbsp;&nbsp;<b>
<br />
Course:&nbsp;<asp:DropDownList ID="ddlCoure" runat="server" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil" /><asp:ListItem Value="Architecture" Text="Architecture"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox"><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Value="SectionB" Text="SectionB" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" CssClass="btnsmall" OnClick="btnOK_TextChanged" Text="OK" />
<asp:Label ID="lblExceptionOK" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
<asp:Label ID="lblCourseID" runat="server" Visible="false" /><asp:Label ID="txtSerialNo" runat="server" 
/>&nbsp;<asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label><asp:Label ID="lblCourseStatus" runat="server" Visible="false"></asp:Label><asp:Label ID="lblexamstatus" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblException" runat="server"></asp:Label><br /><asp:Label ID="lblException2" runat="server" ></asp:Label></center>
<asp:Panel ID="panelVisible" runat="server">
<table class="tbl">
<tr><td>Name:</td><td>
    <asp:Label ID="lblName" runat="server" Font-Bold="True" 
        ForeColor="Maroon" Font-Size="Small"></asp:Label></td></tr>
<tr><td>s/o,d/o,w/o:</td><td><asp:Label ID="lblFName" runat="server" ></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:Label ID="lblStream" runat="server" ></asp:Label></td><td>
    <b>&nbsp;<asp:Label ID="lblCourse" runat="server"></asp:Label>
    &nbsp;&nbsp;<asp:Label ID="lblPartName" runat="server"></asp:Label>
    </b></td></tr>
<tr><td>Institutional Member:</td><td colspan="2">[<asp:Label ID="lblIMID" runat="server" Font-Bold="true" ></asp:Label>]</td></tr>
<tr><td>ExamCenter City-1:</td><td>
<asp:DropDownList ID="ddlExamCity" runat="server"  CssClass="txtbox"></asp:DropDownList>
</td><td>ExamCenter City-2:&nbsp;&nbsp;
<asp:DropDownList ID="ddlExamCity2" runat="server" CssClass="txtbox" ></asp:DropDownList>
</td></tr>
</table>
<asp:Panel ID="panelCivilGrid" runat="server" >
<asp:GridView ID="GridCivil" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" HorizontalAlign="Center" 
        GridLines="Vertical" Width="100%">
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
            <asp:BoundField DataField="SubID" HeaderText="Subject Code" SortExpression="SubID" />
            <asp:TemplateField>
                    <HeaderTemplate>
                     <asp:Label ID="lblAppSub" runat="server" Text="Select Subjects" ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                     <asp:CheckBox ID="chkAppSubject" runat="server"  />
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <EmptyDataTemplate>
          <center>  No Subject found !!!</center>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView><br />
</asp:Panel>
<center>
 <table class="tbl">
<tr>
<td><asp:TextBox ID="txtRemarks" runat="server" CssClass="txtbox" Height="24px" TextMode="MultiLine" Width="147px"></asp:TextBox></td>
<td>&nbsp;&nbsp; <asp:Button ID="btnSaveForm" runat="server" Text="Save Form" CssClass="btnsmall" onclick="btnSaveForm_Click" OnClientClick="return Enterpwd();"/></td>
</tr>
</table></center>
</asp:Panel><asp:Panel ID="panelInVisible" runat="server" Height="500px"></asp:Panel>
    </b>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>