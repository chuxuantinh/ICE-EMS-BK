<%@ Page Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ApproveMarks.aspx.cs" Inherits="Exam_ApproveMarks" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Approve Provisional Marksheet</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Approve Marks" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updatePanel1" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTempEnrol" runat="server" ></asp:Label><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Approve Examination Result for Publish</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox></td></tr></table></center>
<asp:Label ID="lblExamSeasonHidden" Visible="false" runat="server" ></asp:Label>
<br />
<center><asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Onclick" CssClass="btnsmall" />
<br /><br />
<h2>Examination Marks Statement</h2>
</center>
<br />
<table width="80%" style="margin-left:10%;"><tr><td>Total No of Exam Form Submitted.</td><td><asp:Label ID="lblToExamForm" runat="server" ></asp:Label></td></tr>
<tr><td>Exam Marks Not Submitted</td><td><asp:Label ID="lblMarkNotSubmitted" runat="server" ></asp:Label></td></tr>
<tr><td>Exam Marks Submitted</td><td><asp:Label ID="lblMarksSubmitted" runat="server" ></asp:Label></td></tr>
<tr><td>Exam Marks Approved</td><td><asp:Label ID="lblMarksApproved" runat="server" ></asp:Label></td></tr>
</table>
<br />
</ContentTemplate></asp:UpdatePanel>
<center>Course:&nbsp;&nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" ><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architecture Engineering" /></asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox"><asp:ListItem Value="PartI" Text="PartI" /><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Value="SectionA" Text="SectionA" /><asp:ListItem Value="SectionB" Text="SectionB" /></asp:DropDownList>
&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPro" runat="server" Text="Promote" CssClass="btnsmall" OnClick="btnPro_Click" /><br />

<b>Total Promoted Student: &nbsp;&nbsp;<asp:Label ID="lblToStudent" runat="server" ></asp:Label></b></center>
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

</div>
</asp:Content>

