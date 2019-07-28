<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ViewExamForm.aspx.cs" Inherits="Exam_ViewExamForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Examination Form
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
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTempEnrol" runat="server" ></asp:Label><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>View Examination Form</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox></td></tr></table></center>
<br />
<center>
<table style="width:95%;  text-align:center;">
<tr><td>Diary Submitted</td><td>Submitted</td><td>Approved</td><td>Filled</td><td>On Hold</td><td>RollNo Generated</td><td>Admit Card</td></tr>
<tr>
<td><h3><asp:Label ID="lblToExamDiary" runat="server" ></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormSub" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormApproved" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormFilled" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamHold" runat="server" ></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormRollNo" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormAdmitCard" runat="server"></asp:Label></h3></td></tr>
</table><asp:Label ID="lblSN" runat="server" Visible="false"></asp:Label>
<br />Serial No./Membership No.:&nbsp;&nbsp;<asp:TextBox ID="txticesn" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtSerialNo_TextChanged" CssClass="txtbox"></asp:TextBox><asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" Visible="false"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnViewMembership" runat="server" OnClick="btnViewMembershipNo_Onclick" Text="View Subject(s)" /><br /> <asp:Label ID="lblExceptionOK" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
<asp:Label ID="lblException" runat="server" ></asp:Label><br /><asp:Label ID="lblException2" runat="server" ></asp:Label></center>
<center runat="server" id="cnProfile" visible="false">
<h3 class="hl3"><asp:Label ID="lblName" runat="server"></asp:Label></h3>
<asp:Label ID="lblCourse" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server" Font-Bold="true"></asp:Label><br />
<asp:Label ID="lblIMID" runat="server" ForeColor="Blue" ></asp:Label><br />
<br />
<asp:Button ID="btnSubmit" runat="server"  CssClass="bigbutton" /><br /></center>
<center><asp:Label ID="lblMsg" runat="server" ForeColor="Maroon"></asp:Label></center>
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
   <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
    </div><div style="padding:1px;"><h1><asp:Label ID="lblExamSeasonHidden" runat="server"></asp:Label> &nbsp;&nbsp;IMID:<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;<asp:RadioButton 
            ID="rbtnGenerated" runat="server" AutoPostBack="True" GroupName="a" 
            oncheckedchanged="rbtnGenerated_CheckedChanged" Text="Submitted" />
        <asp:RadioButton ID="rbtnNotGenerated" runat="server" AutoPostBack="True" 
            GroupName="a" oncheckedchanged="rbtnNotGenerated_CheckedChanged" 
            Text="Not Submitted" />
        </h1></div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<asp:GridView ID="GridExamForms" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="1" CellSpacing="1" PageSize="50" OnSelectedIndexChanged="GridExamForms_OnSelectedIndexChangd"
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%" 
                         onrowdatabound="GridExamForms_RowDataBound">
        <Columns>
        <asp:CommandField ShowSelectButton="true" />
        </Columns>
        <EmptyDataTemplate><center> Duplicate Record Not found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
   </div>
   </div></div>
</div>
</asp:Content>

