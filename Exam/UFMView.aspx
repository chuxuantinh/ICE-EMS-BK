<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="UFMView.aspx.cs" Inherits="Exam_UFMView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">UFM Overview
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>UFM Overview</h1></div>
<br />
<asp:UpdatePanel ID="updpnlufm" runat="server"><ContentTemplate>
<table  class="tbl">
<tr><td class="style1">&nbsp;&nbsp; Examination Session:</td><td class="style1"><asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td>
    <td class="style1">Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
</table>
<table  class="tbl">
 <tr><td>&nbsp;&nbsp; Search: </td><td><asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" CssClass="txtbox" onselectedindexchanged="ddlSearch_SelectedIndexChanged">
     <asp:ListItem Text="Roll No"></asp:ListItem>
     <asp:ListItem Text="Center Code"></asp:ListItem>
     <asp:ListItem Text="IMID"></asp:ListItem>
     <asp:ListItem Text="Membership ID"></asp:ListItem>
     <asp:ListItem Text="Course"></asp:ListItem>
     <asp:ListItem Text="Subject"></asp:ListItem>
     </asp:DropDownList></td><td>
<asp:Panel ID="pnlSearch" runat="server" Visible="false">
<table class="tbl">
     <tr>
     <td><asp:TextBox ID="txtRollNo" runat="server" Visible="false" Width="98px" CssClass="txtbox"/></td>
     <td><asp:TextBox ID="txtCenterCode" runat="server" Visible="false" Width="98px" CssClass="txtbox"/></td>
     <td><asp:TextBox ID="txtIMID" runat="server" Visible="false" Width="98px" CssClass="txtbox"/></td>
     <td><asp:TextBox ID="txtMID" runat="server" Visible="false" Width="98px" CssClass="txtbox"/></td>
     </tr>
</table>
</asp:Panel>
     </td></tr>
 </table>
<asp:Panel ID="pnlCourse" runat="server" Visible="false">
<table class="tbl">
<tr><td>&nbsp;&nbsp; Course:</td>
<td><asp:DropDownList ID="ddlCourse" runat="server" Width="150px" CssClass="txtbox" 
        AutoPostBack="true" onselectedindexchanged="ddlCourse_SelectedIndexChanged">
    <asp:ListItem Value="Architecture" Text="Architectural Engineering"/>
    <asp:ListItem Value="Civil" Text="Civil Engineering"/>
</asp:DropDownList>
</td>
<td>Section/Part:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" AutoPostBack="true" Width="80px" onselectedindexchanged="ddlPart_SelectedIndexChanged1" >
    <asp:ListItem Text="Part I" Value="PartI" />
    <asp:ListItem Value="PartII" Text="Part II" />
    <asp:ListItem Value="SectionA" Text="Section A" />
    <asp:ListItem Value="SectionB" Text="Section B" ></asp:ListItem></asp:DropDownList>
</td>
    <td><asp:Label ID="lblStreamName" runat="server" Font-Bold="true"></asp:Label></td>
    <td><asp:Label ID="lblStreamHidden" runat="server" Visible="false"></asp:Label></td>
</tr>
</table>
</asp:Panel>
<table class="tbl">
<tr>
<td>
<asp:Panel ID="pnlSubj" runat="server" Visible="false" Width="250px">
<table class="tbl">
<tr><td>Subject:</td><td><asp:DropDownList ID="ddlSub" runat="server" Width="150px" CssClass="txtbox" AutoPostBack="true"  ></asp:DropDownList></td>
</table></asp:Panel>
</td>
<td>
<asp:Panel ID="pnlLvl" runat="server" Width="250px" Visible="false">
<table class="tb1">
<tr><td>Syllabus Level:</td>
<td><asp:DropDownList ID="ddlSyllabus" runat="server" CssClass="txtbox" 
        onselectedindexchanged="ddlSyllabus_SelectedIndexChanged"></asp:DropDownList>
</td></tr>
</table>
</asp:Panel>
</td>
</tr>
</table>


<asp:Panel ID="pnlBtn" runat="server" Visible="false">
<center><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
        onclick="btnView_Click" /></center>
</asp:Panel>
 <asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label><center><asp:Label ID="lblExceptionOK" runat="server" ></asp:Label></center>
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
    <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblGridTitle" runat="server" ></asp:Label>
<br />
</div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 100%; overflow:scroll; height:300px" >
<asp:GridView ID="grvUfm" runat="server" PageSize="30" AllowPaging="True" Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" HeaderStyle-HorizontalAlign="Center">
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <RowStyle HorizontalAlign="Center" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
</asp:GridView>
</div>
</ContentTemplate></asp:UpdatePanel>
<asp:Panel ID="pnlSpace" runat="server" Height="300px"></asp:Panel>
</div>
</asp:Content>