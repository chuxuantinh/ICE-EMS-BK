<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="BookletRange.aspx.cs" Inherits="Exam_BookletRange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Booklet Range
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" />
<div id="redirect" runat="server">	
<table>
<tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab" onclick="lbtnNext1Redirect_Click" /></td><td><asp:Label ID="lblPageName" runat="server" Text="Booklet Range" CssClass="redirecttabhome" /></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Booklet Range</h1></div>
<center><table>
<tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="100px" /></td></tr></table></center><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false" />
<br />
<center>
<table class="tbl"><asp:Label ID="lblQty" runat="server" Visible="false" />
<tr><td>Paper Code:</td><td><asp:DropDownList ID="ddlPCode" runat="server" CssClass="txtbox" /></td><td>Type:</td><td>
<asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox" onselectedindexchanged="ddlType_SelectedIndexChanged" AutoPostBack="true"><asp:ListItem Value="Home"/><asp:ListItem Value="Overseas"/></asp:DropDownList></td></tr>
<tr><td>Start Range:</td><td><asp:TextBox ID="txtStart" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FStart" runat="server" TargetControlID="txtStart" FilterType="Numbers" /></td><td>End Range:</td><td><asp:TextBox ID="txtEnd" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FEnd" runat="server" TargetControlID="txtEnd" FilterType="Numbers" /></td></tr>
</table><br />
<asp:Button ID="btnAdd" runat="server" CssClass="btnsmall" Text="Add Booklet Range" onclick="btnAdd_Click" />&nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnsmall" onclick="btnUpdate_Click" Visible="false" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btnsmall" Text="Cancel" onclick="btnCancel_Click" Visible="false" />
<br /><br />
<asp:Label ID="lblException" runat="server" ForeColor="Brown" />
</center>
<script>
         function toggleA1x(showHideDiv, switchImgTag) {
             var ele = document.getElementById(showHideDiv);
             var imageEle = document.getElementById(switchImgTag);
             if (ele.style.display == "block") {
                 ele.style.display = "none";
                 imageEle.innerHTML = '<img src="../images/plus.png">';
             }
             else {
                 ele.style.display = "block";
                 imageEle.innerHTML = '<img src="../images/minus.png">';
             }
         }
</script>
<div class="togalfees" style="width:100%"><div class="headerDivImgfees">
<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1');"><img src="../images/minus.png" alt="Show"/></a></div>
<div style="padding:5px; color:White; font-size:15px;"><b>Booklet Range</b></div><br />
<div id="Div1" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div  id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px">
 <asp:GridView ID="GridRange" runat="server" BackColor="White"
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        Width="100%" OnRowDeleting="DeleteRecord" 
        AutoGenerateColumns="False"  DataKeyNames="SN" 
        onselectedindexchanged="GridRange_SelectedIndexChanged" >
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Edit"/>
             <asp:TemplateField HeaderText="SN" Visible="false">
            <ItemTemplate><%# Eval("SN") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Paper Code">
            <ItemTemplate><%# Eval("SubID")%></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Start Range">
            <ItemTemplate><%# Eval("StartRange") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="End Range">
            <ItemTemplate><%# Eval("EndRange") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Session">
            <ItemTemplate><%# Eval("Session") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Type">
            <ItemTemplate><%# Eval("Type") %></ItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Delete?">
            <ItemTemplate>
            <span onclick="return confirm('Are you sure you want to delete the record from your database?')">
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/images/delete.png" />
            </span>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="White" ForeColor="#003399" HorizontalAlign="Center" />
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <PagerStyle ForeColor="#003399" HorizontalAlign="Left" BackColor="#99CCCC" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="White" Font-Size="Small" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
</asp:GridView>
</div></div></div>
</div>
</asp:Content>