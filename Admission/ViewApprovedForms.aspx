<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ViewApprovedForms.aspx.cs" Inherits="Admission_ViewApprovedForms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Approved Admission Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblGnMemshp" runat="server" Text="View Approved Admission From A/C" CssClass="redirecttabhome"></asp:Label></td></tr></table>
</div><div id="rightpanel2">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>New Admission Forms</h1></div>
<center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><br /><asp:Label ID="lblExceptionApp" runat="server"></asp:Label><br />
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>View By:&nbsp;&nbsp;<asp:DropDownList ID="ddlViewBy" runat="server" CssClass="txtbox" Width="200px" ><asp:ListItem Value="Approved" Text="Approved By A/C" /><asp:ListItem Value="NotApproved" Text="Not Approved By A/C"></asp:ListItem></asp:DropDownList></center>
<br />
</ContentTemplate></asp:UpdatePanel>
<center><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnView_Onclick" />&nbsp;&nbsp;&nbsp;</center>
<br />
<script>
    function toggleA1(showHideDiv, switchImgTag) {
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
<a id="A1" href="javascript:toggleA1('Div1', 'A1');"><img src="../images/minus.png" alt="Show"></a>
</div> <br /><br />
<div id="Div1" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid" style="width: 100%; overflow:scroll; height:200px" >
   <script type="text/javascript">
       function selectAll(invoker) {
           var inputElements = document.getElementsByTagName('input');
           for (var i = 0; i < inputElements.length; i++) {
               var myElement = inputElements[i];
               if (myElement.type === "checkbox") {
                   myElement.checked = invoker.checked;
               }
           }
       } 
</script>
<br />
<asp:GridView ID="GridApprove" runat="server" BackColor="#DEBA84" 
        AutoGenerateColumns="true" OnRowDataBound="GridApprove_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" 
        CellSpacing="5" Width="100%">
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>      </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView>
</div>
</div>
</div>

<br />
<br />
</div><br />
</asp:Content>