<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ResultPromote.aspx.cs" Inherits="Exam_ResultPromote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Promote Result
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Result Promote" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;"><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Result Promotion</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;&nbsp;Session ID:&nbsp;<asp:Label ID="lblExamSeasonHidden" runat="server" ></asp:Label></td></tr></table>
<br />
Course:&nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" ><asp:ListItem Value="Civil" Text="Civil" ></asp:ListItem><asp:ListItem Value="Architecture" Text="ARchitecture" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Part:&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox"><asp:ListItem Value="PartI" Text="PartI" /><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Text="SectionA" Value="SectionA" /> <asp:ListItem Value="SectionB" Text="SectionB" /></asp:DropDownList></center>
<center>
<asp:Button ID="btnUdateAdmisisoon" CssClass="btnsmall" Visible="false" runat="server" Text="Update Admission Status" OnClick="btnUpdate_Admission_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUpdateAccount" runat="server" CssClass="btnsmall" OnClick="btnUpdateAccount_Click" Text="Update Account (Final Pass)" ToolTip="Update Exam Current Data from SFinalPass Student" />
<asp:Button ID="btnPromoteResult" runat="server" CssClass="btnsmall" Text="Promote" Enabled="false" OnClick="btnPromoteResult_Click" /></center>
<br /><br />
<ul>
<li>To Update Admission status(Regular/Direct) with Session of Examination.</li>
<li>To Update Account Select Session with perticular Course and Part.</li>
</ul><br />
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;"><asp:Label ID="Label1" runat="server" ></asp:Label></h1><h1>Composite Fees Update</h1></div>
<br />
<center><asp:Button ID="btnCompositeUpdate" CssClass="btnsmall" runat="server" Text="Update Composite Fees" OnClick="btnUpdateCompositeFees_Click" /></center>
<br />
<center><asp:Label ID="lblCompositeException" runat="server" ></asp:Label></center>
<h3>Instruction:</h3>
<ul>
<li>To Generate Composite Fees List first select Exam Session (SessionID)</li>
<li>Click Update Compoiste Button to update Composite Fees list.</li>
</ul>
<br /><br />
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;"><asp:Label ID="Label2" runat="server" ></asp:Label></h1><h1>Update Promotted Student</h1></div>

Step-1: First Update Additional Pass Status <b>Pass</b>&nbsp; To Update click Here>>&nbsp;&nbsp;<asp:LinkButton ID="lbtnAdditionalPass" runat="server" Text="Additional Paper Pass" OnClick="lbtnAdditionalPaperPass_click"></asp:LinkButton><br />
Step-2: Exam Session. <br />
<center>
Student who passed all regulr subject's in PartII and remain only TC 2.10,TC 2.11,TA 2.11,TA 2.12.<br />
&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPromotted" runat="server" Text="Get List" OnClick="btnpromote_Click" />&nbsp;&nbsp;<asp:Button ID="btnPromoteStudent" runat="server" Text="Promote Student" OnClick="btnPromote_Student_click" />
Course:&nbsp;&nbsp;<asp:DropDownList ID="ddlCoursePrmote" runat="server" CssClass="txtbox" ><asp:ListItem Value="Civil" Text="Civil" /><asp:ListItem Value="Architecture" Text="Architecture" /></asp:DropDownList>
&nbsp;&nbsp;No of Subject Passed:<asp:DropDownList ID="ddlNoofSubject" Enabled="false" runat="server"><asp:ListItem Value="2" Text="0 or 1 Passed" /><asp:ListItem Value="" Text="All Fail" /><asp:ListItem Value="1" Text="1" /></asp:DropDownList></center>
<br />
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
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
   &nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;
    <a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1><asp:Label ID="lblGridTitle" runat="server" ></asp:Label>Total Forms:<asp:Label 
            ID="lblTotalForms" runat="server"></asp:Label>
    </h1>
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:450px">
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
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
   </div>
</div>
</div>
<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
<br />
</div>
</asp:Content>