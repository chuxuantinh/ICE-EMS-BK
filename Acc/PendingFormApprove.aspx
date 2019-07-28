<%@ Page Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="PendingFormApprove.aspx.cs" Inherits="Acc_PendingFormApprove" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Application Form Approval ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblPendingFormApprove" runat="server" Text="Re-Approve" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Re-Approve &nbsp;<asp:Label ID="lblFormType" runat="server" ></asp:Label>&nbsp;&nbsp;[Accounts Section] </h1></div><br />
<table class="tbl" width="95%">
<tr><td align="right">Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp; <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="100px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></tr>
</table>
<asp:Label ID="lblSessionHiddend" runat="server" Font-Bold="true" Visible="false"></asp:Label>
<center><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true"></asp:Label></center><hr />
<center>Form Type:&nbsp;&nbsp; <asp:DropDownList ID="ddlFormtype" runat="server" CssClass="txtbox">
<asp:ListItem Value="All" Text="All" />
<asp:ListItem Value="Admission" Text="New Admission" />
<asp:ListItem Value="ReAdmission" Text="Old Admission" />
<asp:ListItem Value="Exam" Text="Exam" />
<asp:ListItem Value="ITI" Text="ITI" />
<asp:ListItem Value="CAD" Text="CAD"></asp:ListItem>
<asp:ListItem Value="Composite" Text="Composite" />
<asp:ListItem Value="ASF" Text="ASF" />
<asp:ListItem Value="Exemption" Text="Exemption" />

<asp:ListItem Value="Project" Text="Project" />

<asp:ListItem Value="ExamCenter" Text="Change of Exam Center" />
<asp:ListItem Value="Rechecking" Text="Re-Checking Subjects" />
<asp:ListItem Value="FinalPass" Text="Final Marksheet" />
<asp:ListItem Value="ProvisionalCertificate" Text="Provisional Certificate" ></asp:ListItem>
    <asp:ListItem Value="IDCard" Text="ID Card"></asp:ListItem>
    <asp:ListItem Value="AdmitCard" Text="Admit Card"></asp:ListItem>
    <asp:ListItem Value="MarksStatement" Text="Marks Statement"></asp:ListItem>
    <asp:ListItem Value="MembershipCertificate" Text="Membership Certificate"></asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlSearchType" runat="server" CssClass="txtbox"><asp:ListItem Value="NotApproved" Text="Not Approved" /><asp:ListItem Value="Approved" Text="Approved Forms" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Onclick" CssClass="btnsmall" />
</center>
<script>
      function toggleA1x(showHideDiv, switchImgTag) {
          var ele = document.getElementById(showHideDiv);
          var imageEle = document.getElementById(switchImgTag);
          var imageEle = document.getElementById(switchImgTag);
          if (ele.style.display == "block")
           {
              ele.style.display = "none";
              imageEle.innerHTML = '<img src="../images/plus.png">';
          }
          else 
          {
              ele.style.display = "block";
              imageEle.innerHTML = '<img src="../images/minus.png">';
          }
      }
</script>
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px;"><h1><asp:Label ID="lblSearchlabel" runat="server" ></asp:Label></h1></div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 99%; overflow:scroll; height:200px">
<asp:GridView ID="GridAppForm" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3"  CellSpacing="2" Width="99%" HorizontalAlign="Center">
<EmptyDataTemplate><center> Record(s) Not Found !!!</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
<Columns>
        <asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
</Columns>
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White"/>
</asp:GridView>
</div></div></div>
<center>
<asp:Button ID="btnApprove" runat="server" Text="Approve" onclick="btnApprove_Click" CssClass="btnsmall"/>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick"  CssClass="btnsmall"/></center>
<br /><br />
<br /><br />
<br /><br />
<br /><br />
</ContentTemplate></asp:UpdatePanel>
</div><br />
</asp:Content>