<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamPaperUpload.aspx.cs" Inherits="Exam_ExamPaperUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">    function fixform() {
        if (opener.document.getElementById("aspnetForm").target != "_blank") return;
        opener.document.getElementById("aspnetForm").target = "";
        opener.document.getElementById("aspnetForm").action = opener.location.href;
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>

<div id="rightpanel2" onload="fixform()">

<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ID No.-<asp:TextBox ID="txtID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUpdateShow" runat="server" Text="Show" OnClick="btnShowID_Click"></asp:LinkButton></h1><h1>Examination Paper Setter:</h1></div>
<center><asp:Label ID="lblException" runat="server" ></asp:Label></center><br />
<asp:Panel ID="panelHidden" runat="server" Height="600px"><center>Insert Papser Setter ID in above textbox to upload paper detials.</center></asp:Panel> 
<asp:Panel ID="panelView" runat="server" ><asp:UpdatePanel ID="updatePanelq" runat="server" ><ContentTemplate>
<br /><table class="tbl"><tr><td>Name:&nbsp; </td><td><asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label></td><td>ID No.:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCode" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Designation:</td><td><asp:Label ID="lblDesignation" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Education Qualification:</td><td><asp:Label ID="lblEducatioN" runat="server" Font-Bold="true" ></asp:Label></td><td>Experience:&nbsp;&nbsp;<asp:Label ID="lblExperience" runat="server" Font-Bold="true"></asp:Label></td></tr>
</table>

<table class="tbl"><tr><td>Select Examination Season:</td><td>
    <asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlExamSeason_SelectedIndexChanged"  ><asp:ListItem Value="Win" Text="Winter Exam"></asp:ListItem><asp:ListItem Value="Sum" Text="Summer Exam"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtSeason_TexhChanged"></asp:TextBox></td></tr>
<tr><td>Syllabus Level:</td><td><asp:DropDownList ID="ddlSyllabus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSyllabus_SelectedIndexChanged" CssClass="txtbox"></asp:DropDownList></td><td><asp:Label ID="lblStreamName" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblStreamCode" runat="server" Visible="false"></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SeelctedIndexchanged" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList></td><td>Part/Section:&nbsp;</td>
<td><asp:DropDownList ID="ddlPart" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" runat="server" CssClass="txtbox"><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList></td></tr>
<tr><td>Subject Code:</td><td><asp:DropDownList ID="ddlsubcode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCode_SelectedIndexChanged" CssClass="txtbox"></asp:DropDownList></td><td>Subject Name:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSubName" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>

</table><br />
<asp:Label ID="lblSeason" runat="server" ></asp:Label>
<br />
<center><asp:Label ID="lblSavedInfo" runat="server" ForeColor="Maroon" ></asp:Label><br /><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;</center>
</ContentTemplate></asp:UpdatePanel>
<div class="fromRegisterlbl"><h1>Upload Paper:</h1></div>
<br />
<center>
<asp:Label ID="lbllnk10" runat="server" ></asp:Label>
<asp:Label ID="lblDocstitle" runat="server" ></asp:Label><br />
<asp:FileUpload ID="filMyFile" runat="server" Width="200px" BorderColor="AliceBlue"  BorderStyle="Groove" BorderWidth="3px" /><br />
 <asp:Label ID="lblInfo" runat="server" ></asp:Label><br /><br />
 <asp:Button ID="Button1" runat="server" Text="Save" OnClick="cmdSendNew2_Click" />
 
 <br /><asp:Label ID="lblDownlloadInfo" runat="server" ></asp:Label><br /></center><center>
 <asp:Panel ID="doc10" runat="server">
 <asp:Label ID="lbl10Name" Width="250px" runat="server" Text="Paper Sample" CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtn10view" runat="server" Text="View" OnClick="lbtn10View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtn10Download" runat="server" Text="Download" OnClick="lbtn10Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel></center>
 <div style="height:300px;" ></div>
 </asp:Panel>
</div>
</asp:Content>

