<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="importExamDAta.aspx.cs" Inherits="Exam_importExamDAta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exam Center</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Exam Center Rooms" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Import Exam Data</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSessionOK" runat="server" CssClass="btnsmall" OnClick="btnSessionOK_OnClick" Text="View" /></td></tr></table></center>
<asp:Label ID="lblSeasonHidden" runat="server"></asp:Label>
    <br />
    <center>Center Code:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCenterCoe" runat="server" CssClass="txtbox"></asp:TextBox>
    <br /><br /><asp:FileUpload ID="FileUpload1" runat="server" Width="200px"/>&nbsp;&nbsp;<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    &nbsp;&nbsp;&nbsp; <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" /><br />
    <asp:Label ID="lblMessage" runat="server" ></asp:Label>
    </center>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Width="100%" 
        GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <hr />
    <b>Instruction:</b>
   <ul><li>Excel Sheet Name=Sheet1 and  Format should like this.<br />
   Course(Text)	Part(Text)	RollNo(Text)	SID(Text)	Center(Text)	SubID(Text)	Name(Text)	Fname(Text)	IMID(Text)	Date(Date)	Shift(Text)	SubName(Text)</li>
<li>While uploading new excel sheet data confirm that previous Seating Plan is completed.</li>
<li>First Upload Excel Sheet data in <b>upSeating table</b></li>
<li>Second insert Center Code in Text box and update Data from upseating to Exam Form for Seating Plan</li>
</ul>
 <div id="invisible" runat="server" style="height:300px;" ></div>
</div>
</asp:Content>