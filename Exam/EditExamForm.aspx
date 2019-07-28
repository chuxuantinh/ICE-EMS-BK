<%@ Page Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="EditExamForm.aspx.cs" Inherits="Exam_EditExamForm"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Edit Examination Form
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab" onclick="lbtnNext1Redirect_Click" /></td><td><asp:Label ID="lblPageName" runat="server" Text="Edit Exam Form" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updatePanel1" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTempEnrol" runat="server" ></asp:Label><asp:Label ID="lblEnrolment" runat="server" /></h1><h1>Approve for Re-Submission Examination Form</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" CssClass="txtbox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" /></td></tr></table></center>
<br /><center>Serial No./Membership No.:&nbsp;&nbsp;<asp:TextBox ID="txticesn" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtSerialNo_TextChanged" CssClass="txtbox" /><br /><asp:Label ID="lblExceptionOK" runat="server" ForeColor="Red" Font-Bold="true" />
<asp:Label ID="lblException" runat="server" /><br /><asp:Label ID="lblException2" runat="server" /></center><br />
<center>
<asp:Panel ID="pnlDetails" runat="server">
<h3 class="hl3"><asp:Label ID="lblName" runat="server" /></h3>
<asp:Label ID="lblCourse" runat="server" Font-Bold="true" />&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server" Font-Bold="true" /><br />
<asp:Label ID="lblIMID" runat="server" ForeColor="Blue" /><br /><br />
</asp:Panel><br />
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
<a id="A1" href="javascript:toggleA1x('Div1xx', 'A1xx');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:1px;"><h1>- Exam Forms Details -</h1></div>
<div id="Div11" style="display:block;">
<input id="Hidden1" runat="server" type="hidden" value="0" />
<div id="div2" style="overflow:scroll; height:200px" >
<asp:GridView ID="GridExam" runat="server" BackColor="White" BorderColor="#E7E7FF" 
        BorderStyle="None" BorderWidth="1px" OnRowDeleting="DeleteRecord" 
        AutoGenerateColumns="False"  DataKeyNames="SN" CellPadding="8" CellSpacing="8" 
        PageSize="20" GridLines="Horizontal" HorizontalAlign="Center" Width="100%" 
        onselectedindexchanged="GridExam_SelectedIndexChanged">
<Columns>
<asp:CommandField ShowSelectButton="true" HeaderText="View Subject" />
<asp:TemplateField HeaderText="SN" Visible="false"><ItemTemplate><%# Eval("SN") %></ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="SID"><ItemTemplate><%# Eval("SID")%></ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="IMID"><ItemTemplate><%# Eval("IMID") %></ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="Course"><ItemTemplate><%# Eval("Course") %></ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="Part"><ItemTemplate><%# Eval("Part") %></ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="City"><ItemTemplate><%# Eval("City") %></ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="Delete?"><ItemTemplate>
<span onclick="return confirm('Are you sure you want to Re-Submit the form ?')">
<asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/images/delete.png" />
</span></ItemTemplate></asp:TemplateField>
</Columns>
<EmptyDataTemplate><center> Record Not found !</center></EmptyDataTemplate>
<RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
<FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
<PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
<HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
<AlternatingRowStyle BackColor="#F7F7F7" />
</asp:GridView>
</div></div></div></center>
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
</script><asp:Label ID="lblSN" runat="server" Visible="false" />
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:1px;"><h1>&nbsp;&nbsp;Exam Forms of Session:&nbsp;<asp:Label ID="lblExamSeasonHidden" runat="server"></asp:Label> &nbsp;&nbsp;&nbsp;</h1></div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="overflow:scroll; height:300px" >
<asp:GridView ID="GridExamForms" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true" CellPadding="8" CellSpacing="8" PageSize="50" GridLines="Horizontal" HorizontalAlign="Center" Width="100%">
<EmptyDataTemplate><center> Duplicate Record Not found !</center></EmptyDataTemplate>
<RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
<FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
<PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
<HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" />
<AlternatingRowStyle BackColor="#F7F7F7" />
</asp:GridView></div></div></div>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>