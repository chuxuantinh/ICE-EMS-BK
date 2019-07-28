<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="RecheckingFrom.aspx.cs" Inherits="Exam_RecheckingFrom" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Rechecking Marks Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" 
        onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" 
        meta:resourcekey="lblHomeRedirectResource1"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" 
            meta:resourcekey="lbtnNext1RedirectResource1" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
        ID="lblEnrolment" runat="server" meta:resourcekey="lblEnrolmentResource1" ></asp:Label></h1><h1>Rechecking Form</h1></div>
 <table  class="tbl">
 <tr><td>Examination Session:</td><td>
     <asp:DropDownList ID="ddlExamSeason" 
         runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" 
         AutoPostBack="True" 
          ><asp:ListItem Text="Summer Examination" Value="Sum" 
             meta:resourcekey="ListItemResource1"></asp:ListItem>
         <asp:ListItem Text="Winter Examination" Value="Win" 
             meta:resourcekey="ListItemResource2"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; 
         <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" 
             AutoPostBack="True" OnTextChanged="txtYearSeason_TextChanged" 
             meta:resourcekey="txtYearSeasonResource1"></asp:TextBox></td><td><asp:Button ID="btnUpdateOldMarks" runat="server" Text="Update Old Marks" OnClick="btnUpdate_OldMarksClick" CssClass="btnsmall" OnClientClick="return confirm('Confirm Update Old Marks of ReChecking Forms ?')" /></td></tr>
 </table><asp:Label ID="lblHiddenSeason" runat="server" Visible="False" 
        meta:resourcekey="lblHiddenSeasonResource1"></asp:Label><center>
      Paper Code:&nbsp;<asp:DropDownList ID="ddlpaperCode" runat="server" CssClass="txtbox" Width="150px"></asp:DropDownList> &nbsp;&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" Text=" OK " OnClick="btnOK_OnClcick" 
         CssClass="btnsmall" meta:resourcekey="btnOKResource1" /><br /> <asp:Label ID="lblExceptionOK" runat="server" 
            meta:resourcekey="lblExceptionOKResource1" ></asp:Label></center>
    <br />
    <asp:GridView ID="GridView1" runat="server" BackColor="LightGoldenrodYellow"  AutoGenerateColumns="false"
        BorderColor="Tan" BorderWidth="1px" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" CellPadding="2" ForeColor="Black"  Width="100%"
        GridLines="None" PageSize="100" AllowPaging="true" onpageindexchanging="Grid1_PageIndexChanging">
        <Columns>
        <asp:TemplateField HeaderText="RollNo"><ItemTemplate><asp:Label ID="lblRollNo" Text='<%# Eval("RollNo") %>' runat="server"></asp:Label></ItemTemplate></asp:TemplateField>
        <asp:TemplateField HeaderText="MembershipNo"><ItemTemplate><asp:Label ID="lblSID" Text='<%# Eval("SID") %>' runat="server"></asp:Label></ItemTemplate></asp:TemplateField>
        <asp:TemplateField HeaderText="Old Marks"><ItemTemplate><asp:Label ID="lblOldMarks" Text='<%# Eval("OldMarks") %>' runat="server"></asp:Label></ItemTemplate></asp:TemplateField>
        <asp:TemplateField HeaderText="New Marks"><ItemTemplate><asp:TextBox ID="txtNewmarks" Text='<%# Eval("NewMarks") %>' runat="server"></asp:TextBox></ItemTemplate></asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
            HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    </asp:GridView>
    <br />
    <br />
    <center><asp:Button ID="btnSave" runat="server" CssClass="btnsmall" Text="Save" OnClientClick="return confirm('Confirm Submit Subjects ?')" OnClick="btnSave_click" /></center>
<hr />
<h2>Instruction:</h2>
<ol>
<li>After Submitting New ReChecking marks please update Old Marks from Result to ReChecking.</li>
<li>Status: <b>NoChange</b> if New Marks is less or equal to Old Marks.</li>
<li>Status: <b>Change</b> if New Marks greater then old marks.</li>
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
<li></li>
</ol>
<br />

</div></asp:Content>