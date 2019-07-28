<%@ Page Title="" Language="C#" MasterPageFile="Account.master" AutoEventWireup="true" CodeFile="ExamCurrent.aspx.cs" Inherits="Exam_ExamCurrent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Edit Exam Current
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
 <style>
 .tblView{width:80%;margin:auto;}
 .tblView tr{}
 .tblView th{background:#808080;color:#fff;text-align:center;}
 .tblView td{padding:3px; }
 </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab" onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Membership Details" CssClass="redirecttabhome"></asp:Label></td></tr></table>
</div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1>
<h1>Membership Details</h1></div><br /><div id="visible" runat="server"><center>Membership No:
<asp:TextBox ID="txtMembership" runat="server" CssClass="txtbox"></asp:TextBox><asp:Button ID="btnOk" 
        runat="server" Text="OK" CssClass="btnsmall" onclick="btnOk_Click" /><br />
        <asp:Label ID="lblerror" runat="server" ForeColor="Maroon"></asp:Label></center>
        <br /><div id="viewdetails" runat="server" visible="false">
<table id="tblView" class="tblView">
<tr><td>Student Name</td><td><asp:Label ID="lblName" runat="server"></asp:Label></td><td>IMID</td><td><asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
<tr><td>
    Course</td><td><asp:DropDownList ID="ddlCourse" runat="server"  CssClass="txtbox">
        <asp:ListItem>Civil</asp:ListItem>
        <asp:ListItem>Architecture</asp:ListItem>
        </asp:DropDownList></td><td>Part</td><td><asp:DropDownList ID="ddlPart" 
        runat="server"  CssClass="txtbox" AutoPostBack="True" 
        onselectedindexchanged="ddlPart_SelectedIndexChanged">
    <asp:ListItem Value="PartI">Part I</asp:ListItem>
    <asp:ListItem Value="PartII">Part II</asp:ListItem>
    <asp:ListItem Value="SectionA">Section A</asp:ListItem>
    <asp:ListItem Value="SectionB">Section B</asp:ListItem>
    </asp:DropDownList></td></tr>
<tr><td>Stream</td><td><asp:Label ID="lblStream" runat="server"></asp:Label></td><td>
    CourseID</td><td><asp:TextBox ID="txtCousreID" runat="server"  CssClass="txtbox"></asp:TextBox></td></tr>
<tr><td>Exam Status</td><td><asp:DropDownList ID="ddlExamStatus" runat="server"  
        CssClass="txtbox" DataSourceID="SqlDataSource1" DataTextField="ExamStatus" 
        DataValueField="ExamStatus">
   
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT DISTINCT [ExamStatus] FROM [ExamCurrent]">
    </asp:SqlDataSource>
    </td><td>
    PartII Exam Status</td><td><asp:DropDownList ID="ddlCourseStatus" 
            runat="server"  CssClass="txtbox" DataSourceID="SqlDataSource4" 
            DataTextField="CourseStatus" DataValueField="CourseStatus"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [CourseStatus] FROM [ExamCurrent]">
        </asp:SqlDataSource>
    </td></tr>
<tr><td>Session</td><td colspan="3"><asp:DropDownList ID="ddlSession" runat="server"  CssClass="txtbox">
    <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
    <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
    </asp:DropDownList>
  <asp:TextBox ID="txtYear" runat="server"  CssClass="txtbox" Width="80px"></asp:TextBox>
    <asp:Button ID="btnEdit" Text="Update" runat="server" CssClass="btnsmall" 
        onclick="btnEdit_Click" /></td></tr>
</table>
<h3 style="text-align:center;">Result</h3>

<table class="tblView"><tr><th  colspan="2"><asp:Label ID="lblCourse" runat="server"></asp:Label></th></tr><tr><th>PartI</th><th>PartII</th></tr>
<tr><td><asp:GridView ID="grdPartI" runat="server" HorizontalAlign="Center" 
        Width="100%" onrowdatabound="grdPartI_RowDataBound">
<EmptyDataTemplate>--No Records Found--</EmptyDataTemplate>
<EmptyDataRowStyle HorizontalAlign="Center" /></asp:GridView></td>
<td><asp:GridView ID="grdPartII" runat="server" HorizontalAlign="Center" 
        Width="100%" onrowdatabound="grdPartII_RowDataBound">
<EmptyDataTemplate>--No Records Found--</EmptyDataTemplate><EmptyDataRowStyle HorizontalAlign="Center" /></asp:GridView></td>
</tr>
<tr><th>SectionA</th><th>SectionB</th></tr>
<tr><td><asp:GridView ID="grdSectionA" runat="server" HorizontalAlign="Center" 
        Width="100%" onrowdatabound="grdSectionA_RowDataBound">
<EmptyDataTemplate>--No Records Found--</EmptyDataTemplate>
<EmptyDataRowStyle HorizontalAlign="Center" /></asp:GridView></td>
<td><asp:GridView ID="grdSectionB" runat="server" HorizontalAlign="Center" 
        Width="100%" onrowdatabound="grdSectionB_RowDataBound">
<EmptyDataTemplate>--No Records Found--</EmptyDataTemplate>
<EmptyDataRowStyle HorizontalAlign="Center" /></asp:GridView></td></tr>
<tr><th colspan="2" style="Background:black; color:#fff;">Final Result</th></tr>
<tr><td colspan="2"><asp:GridView ID="grdsFinal" runat="server" Width="100%"></asp:GridView></td></tr>

</table>
</div>

</div><asp:Label ID="lblSessionDuration" runat="server" Visible="false"></asp:Label><asp:Label ID="lblstreamhidden" runat="server" Visible="false"></asp:Label><div id="invisible" runat="server" style="height:300px;" ></div>
 </div>

</asp:Content>

