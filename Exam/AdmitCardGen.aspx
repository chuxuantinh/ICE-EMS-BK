<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="AdmitCardGen.aspx.cs" Inherits="Exam_AdmitCardGen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Generate Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
  <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Admit Cards" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;"><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Admit Card</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox></td></tr></table></center><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label>
<br />
<center><h2>Exam Form Status</h2></center>
<br /><asp:Label ID="lblExceptionOK" runat="server" ></asp:Label>
    <br /><center>Select Date Schedule:&nbsp;&nbsp;<asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox" ><asp:ListItem Value="Home" Text="Home" ></asp:ListItem><asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSaveForAdmitCard" runat="server" Text="Approve Admit Card" OnClick="btnSave_Onclick" CssClass="btnsmall" /></center><br />
<table style="width:80%; margin-left:10%;"><tr><td><h3>Total Exam Form Submitted:</h3></td><td><h3><asp:Label ID="lblExamFormSub" runat="server"></asp:Label></h3></td></tr>
<tr><td><h3>Total Exam Form Approved:</h3></td><td><h3><asp:Label ID="lblExamFormApproved" runat="server"></asp:Label></h3></td></tr>
<tr><td><h3>Total Exam Form Filled:</h3></td><td><h3><asp:Label ID="lblExamFormFilled" runat="server"></asp:Label></h3></td></tr>
<tr><td><h3>Total Exam Form RollNo Generated:</h3></td><td><h3><asp:Label ID="lblExamFormRollNo" runat="server"></asp:Label></h3></td></tr>
<tr><td><h3>Total Exam Form Admit Card Generated:</h3></td><td><h3><asp:Label ID="lblExamFormAdmitCard" runat="server"></asp:Label></h3></td></tr>
</table>
<br />
<asp:UpdatePanel ID="updatepanel1" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btnRefresh" /></Triggers><ContentTemplate>
<div class="fromRegisterlbl"><h1>Change Examination Schedule</h1></div><br />
<center><p>In case of Exam Schedule change, re-generate admit card.</p></center>
<table width="90%;" style="margin-left:5%;"><tr><td>Course:</td><td><asp:DropDownList ID="ddlCourse" runat="server" 
        Width="150px" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"><asp:ListItem Value="Architecture" Text="Architectural Engineering"></asp:ListItem><asp:ListItem Value="Civil" Text="Civil Engineering" /></asp:DropDownList></td>
        <td>Section/Part:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" AutoPostBack="true" Width="80px" >
    <asp:ListItem Text="Part I" Value="PartI" /><asp:ListItem Value="PartII" 
        Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" />
    <asp:ListItem Value="SectionB" Text="Section B" ></asp:ListItem></asp:DropDownList></td></tr>
    <tr><td>Subject Name:</td><td><asp:DropDownList ID="ddlSubID" runat="server" CssClass="txtbox" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlSubID_OnSelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:Label ID="lblSubName" runat="server"></asp:Label></td>
    </tr>
    </table>
    <center><asp:Label ID="lblExceptionChange" runat="server" ></asp:Label><br /><asp:Button ID="btnChage" runat="server" CssClass="btnsmall" Text="Change" OnClick="btnChange_Click" OnClientClick='return confirm("Are you sure you want to reset exam schedule ?");' />
    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" CssClass="btnsmall" OnClick="btnRefresh_Click" Text="Refresh" /></center>
    </ContentTemplate></asp:UpdatePanel><br /><br />
</div>
</asp:Content>

