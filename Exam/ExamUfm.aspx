<%@ Page Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamUfm.aspx.cs" Inherits="Exam_ExamUfm" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Unfair Means Case
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="rightpanel2">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div class="fromRegisterlbl"><h1>Unfair Means Case</h1></div>
<br />
<table  class="tbl">
 <tr><td>Examination Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
 <tr><td></td><td colspan="1"><asp:RadioButton ID="rbtnRollNo" runat="server" Text="Roll No" GroupName="A" />&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnSID" runat="server" Text="Membership ID" GroupName="A" /><br /><asp:TextBox ID="txtRollNo" runat="server" CssClass="txtbox" Width="150px" Font-Bold="true"></asp:TextBox></td><td><asp:Button ID="btnOK" runat="server" Text=" OK " OnClick="btnOK_OnClcick" CssClass="btnsmall" /></td></tr>
 </table><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label><center><asp:Label ID="lblExceptionOK" runat="server" ></asp:Label></center>
<asp:Panel ID="pnlSubject" runat="server" Visible="False">
<table class="tbl" width="80%"><tr><td>Subject Code:&nbsp;&nbsp;<asp:DropDownList ID="ddlSubID" runat="server" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubID_SelectedInxdexChanted"></asp:DropDownList></td><td>Subject Name:&nbsp;<asp:Label ID="lblSubName" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>ExamDate:&nbsp;&nbsp;<asp:Label ID="lblExamDate" runat="server" Font-Bold="true"></asp:Label></td><td>Shift:&nbsp;&nbsp;<asp:Label ID="lblShift" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Membership No:&nbsp;<asp:Label ID="lblSID" runat="server" Font-Bold="true"></asp:Label></td><td>Stream:&nbsp;&nbsp;<asp:Label ID="lblStream" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Course:&nbsp;&nbsp;<asp:Label ID="lblCourse" runat="server" Font-Bold="true"></asp:Label></td><td>Part:&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Center Code:&nbsp;<asp:Label ID="lblCenterCode" runat="server" Font-Bold="true"></asp:Label></td><td>Examination Center:&nbsp;<asp:Label ID="lblCenterName" runat="server" Font-Bold="true"></asp:Label></td></tr>
</table>
<center>Details<br /><asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" Height="50px" Width="60%" Font-Bold="true"></asp:TextBox><br />
<asp:Label ID="lblExceptionSubmit" runat="server" ></asp:Label>
<br /><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Onclick" CssClass="btnsmall"/><br />
</center>
</asp:Panel>

<hr /><%--<div style="height:300px;">--%>
<%--</div>--%>
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
<div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px" >
            
<asp:GridView ID="grvExamUfm" runat="server" PageSize="30" AllowPaging="True" onpageindexchanging="grvExamUfm_PageIndexChanging" Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" HeaderStyle-HorizontalAlign="Center">
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
</asp:GridView>
   </div>
</div>
    </div>
</asp:Content>

