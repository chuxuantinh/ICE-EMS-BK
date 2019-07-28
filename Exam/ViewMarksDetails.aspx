<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ViewMarksDetails.aspx.cs" Inherits="Exam_ViewMarksDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Marks Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updatePanel1" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTempEnrol" runat="server" ></asp:Label><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>View  Examination Marks Details</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox></td></tr></table>
Insert IM ID:&nbsp;&nbsp;<asp:TextBox ID="txtIMID" Width="100px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtIMID_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label><br />
<asp:Label ID="lblExceptionOK" runat="server" ></asp:Label><br /><hr />
<asp:Label ID="lblFulldName" runat="server" Font-Bold="true"></asp:Label><br />
<asp:Label ID="lblFullCourse" runat="server" ></asp:Label><br />
</center>
<br />
<center>Marks:&nbsp;<asp:DropDownList ID="ddlmarks" runat="server" Width="200px" CssClass="txtbox"><asp:ListItem Value="Approved" Text="Approved" /><asp:ListItem Value="Submitted" Text="Submitted for Approval" /><asp:ListItem Value="NotSubmitted" Text="Not Submitted yet" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlViewType" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddltype_OnselectedIndexChanged" ><asp:ListItem Value="All" Text="View All" /><asp:ListItem Value="IM" Text="IM" /><asp:ListItem Value="Student" Text="Student"></asp:ListItem></asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtStudent" runat="server" Width="100px" CssClass="txtbox" ></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" OnClick="btnView_Onclick" Text="View" CssClass="btnsmall" />
</center>
<br />
<script>
      function toggleA1x(showHideDiv, switchImgTag) {
          var ele = document.getElementById(showHideDiv);
          var imageEle = document.getElementById(switchImgTag);
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
</div><div style="padding:1px;"><h1>&nbsp;&nbsp;Exam Forms of Session:&nbsp;<asp:Label ID="lblExamSeasonHidden"  runat="server"></asp:Label> &nbsp;&nbsp;&nbsp;<asp:Button ID="btnApprove" runat="server" Text="View Result" OnClick="btnViewMarks_Onclick" /></h1></div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width:99%; overflow:scroll; height:400px" >
<asp:GridView ID="GridExamForms" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" PageSize="50" OnSelectedIndexChanged="GridExamForms_OnSelectedIndexChangd"
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%" 
                         onrowdatabound="GridExamForms_RowDataBound">
        <Columns>
        <asp:CommandField ShowSelectButton="true" />
        </Columns>
        <EmptyDataTemplate><center> Marks Record Not found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
   </div>
   </div></div>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>

