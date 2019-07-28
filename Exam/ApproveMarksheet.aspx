<%@ Page Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ApproveMarksheet.aspx.cs" Inherits="Exam_ApproveMarksheet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Student Promotion [Change Stream and Part]</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Student Promotion" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updatePanel1" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTempEnrol" runat="server" ></asp:Label><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Promote Student for Next Session</h1></div>
<table class="tbl" width="95%">
<tr><td align="right" colspan="2">Select Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="100px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></tr>
<tr><td align="right">Insert IM ID:&nbsp;&nbsp;</td><td align="left"><asp:TextBox ID="txtIMID" Width="100px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtIMID_TextChanged"></asp:TextBox></td><td>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
<tr><td  align="right"></td><td></td><td>Address:&nbsp;&nbsp;<asp:Label ID="lblIMAddress" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td align="right"></td><td>Group ID:&nbsp;<asp:Label ID="lblGroupID" runat="server" ForeColor="MediumBlue"></asp:Label></td><td><asp:Label ID="lblIMCity" runat="server" ></asp:Label></td></tr>
</table>
<hr /><asp:Label ID="lblHiddendStream" runat="server" Visible="false"></asp:Label><asp:Label ID="lblStreamDDL" runat="server" Visible="false"></asp:Label>
<center><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true" ></asp:Label><br />
Course:&nbsp;<asp:DropDownList ID="ddlCourse" Width="150px" runat="server" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
Part/Section:&nbsp;<asp:DropDownList  Width="150px" ID="ddlPart" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_OnselectedIndexChanged"  runat="server" CssClass="txtbox"><asp:ListItem Text="--Select--" Value=""></asp:ListItem><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList>
<br />
&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" OnClick="btnView_Onclick" Text="View" />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnApprove" runat="server" Text="Promote for Next Session" OnClick="btnApprove_Onclick" />
</center>
<br /><asp:GridView ID="GridSID" runat="server"  Visible="false" AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" AllowPaging="false"
        GridLines="Horizontal" >
    </asp:GridView>
    <asp:DropDownList ID="ddlSyllabus" runat="server"  Visible="false"></asp:DropDownList>
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
</div><div style="padding:1px;"><h1>&nbsp;<asp:Label ID="lblSessionHiddend" runat="server" Font-Bold="true"></asp:Label>,&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server"></asp:Label>&nbsp;Passed Student for Promotion&nbsp;</h1></div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:400px" 
            >
<br /><asp:GridView ID="GridExamForms" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" PageSize="50" 
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%">
        <Columns>
       
        </Columns>
      
        <EmptyDataTemplate><center> Exam Form Not found !</center></EmptyDataTemplate>
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

