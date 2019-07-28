<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="AdditionalPaperStudent.aspx.cs" Inherits="Admission_AdditionalPaperStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Additional Paper Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
<style>
     
 .tblView{width:50%;margin:auto;}
 .tblView tr{}
 .tblView th{background:#808080;color:#fff;text-align:center;}
 .tblView td{padding:3px;width:auto;}
 </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" />
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Admission" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">                    
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"><asp:Label ID="lblEnrolment" runat="server" /></h1><h1>Additional Paper Management</h1></div>
 <center>Membership No:<asp:TextBox ID="txtMembership" runat="server" CssClass="txtbox"></asp:TextBox>
    <asp:Button ID="Button1"  CssClass="btnsmall" runat="server" Text="OK" 
        onclick="btnOk_Click" /><br /><asp:Label ID="lblerror" runat="server" ForeColor="Maroon"></asp:Label></center>
<table id="tblView" class="tblView" runat="server" visible="false"><tr><td><b>Name</b></td><td><asp:Label ID="lblNmae" runat="server"></asp:Label></td></tr>
<tr><td><b>Father's Name</b></td><td><asp:Label ID="lblFatherName" runat="server"></asp:Label></td></tr>
<tr><td><b>IMID</b></td><td><asp:Label ID="lblIMID" runat="server"></asp:Label></td></tr><tr><td><b>Course:</b></td><td><asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox">
    <asp:ListItem>Civil</asp:ListItem>
    <asp:ListItem>Architecture</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;</td></tr>
<tr><td><b>Remarks:</b></td><td><asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" CssClass="txtbox"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="*" ForeColor="Maroon" ControlToValidate="txtremarks" ValidationGroup="abc"></asp:RequiredFieldValidator>
    </td></tr><tr><td colspan="3" align="center"><asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btnsmall" 
            onclick="btnAdd_Click" ValidationGroup="abc" /></td></tr>
       </table><br /><br />
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
<div class="headerDivImgfees"> &nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;
<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>&nbsp;View By:&nbsp;<asp:DropDownList ID="ddlViewBy" runat="server" CssClass="txtbox"><asp:ListItem Value="FailStudent" Text="Additional Paper Student"></asp:ListItem><asp:ListItem Value="PassStudent"  Text="Passed Student" /> </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_click" /></h1>
<div id="Div12" style="display:block;">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:200px" >
<script type="text/javascript">
    function selectAlll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    } 
</script>
<asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5"
        CellSpacing="5" Width="100%">
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>
        
        </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView>
</div>
</div>
</div>
<br /><br />
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"></h1><h1>Update Data From Result</h1></div>
<center>
Here Updating <b>Pass / Fail</b> status of Additional Paper from Result.<br />
Select Subject:&nbsp;<asp:DropDownList ID="ddlSubID" runat="server" CssClass="txtbox" >
<asp:ListItem Value="TC 2.10" Text="TC 2.10" />
<asp:ListItem Value="TC 2.11" Text="TC 2.11" />
<asp:ListItem Value="TA 2.11" Text="TA 2.11" />
<asp:ListItem Value="TA 2.12" Text="TA 2.12" />
</asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnUpdateResult" runat="server" OnClick="btnUpdateFormResult_click" Text="Update" />
<br /><asp:Label ID="lblExceptionUpdate" runat="server"></asp:Label><br /></center>
</div>
</asp:Content>