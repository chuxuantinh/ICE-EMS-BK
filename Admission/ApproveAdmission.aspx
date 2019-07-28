<%@ Page Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ApproveAdmission.aspx.cs" Inherits="Admission_ApproveAdmission" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Generate Membership
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblGnMemshp" runat="server" Text="Generate Membership" CssClass="redirecttabhome"></asp:Label></td></tr></table>
</div><div id="rightpanel2">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Generate Membership</h1></div>
<center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><br /><asp:Label ID="lblExceptionApp" runat="server"></asp:Label><br />
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>View By:&nbsp;&nbsp;<asp:DropDownList ID="ddlViewBy" runat="server" CssClass="txtbox" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlViewBy_OnSelectedIndexChanged"><asp:ListItem Value="Course" Text="Course/Part" /><asp:ListItem Value="IMID" Text="[IM]:Institutional Member"></asp:ListItem></asp:DropDownList>
Status:&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="150px" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged"><asp:ListItem Value="NotApprove" Text="Not Approve" /><asp:ListItem Value="Active" Text="Approved" /><asp:ListItem Value="Pending" Text="Pending" /></asp:DropDownList></center>
<br />
<center>
<table><tr><asp:Panel ID="pnlCourse" runat="server" Width="400px"><td>Course:<br /><asp:DropDownList ID="ddlCourse" Width="150px" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_selectedIndexChanged"><asp:ListItem Value="All" Text="All Course"></asp:ListItem><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
</td><td><br /><asp:DropDownList  Width="150px" ID="ddlPart"  runat="server" CssClass="txtbox"><asp:ListItem Text="--Select Part/Section--" Value=""></asp:ListItem><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList>
</td></asp:Panel><td><asp:Panel ID="pnlIM" runat="server" Width="200px"><center>
IMID:&nbsp;<br /> <asp:TextBox ID="txtIMID" Width="100px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtIMID_TextChanged"></asp:TextBox><br />
<asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true" ></asp:Label></center></asp:Panel></td></tr></table>
<br /></center></ContentTemplate></asp:UpdatePanel>
<center><asp:Button ID="btnViewa" runat="server" CssClass="btnsmall" Text="View" OnClick="btnView_Onclick" />&nbsp;&nbsp;&nbsp;</center>
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
<asp:GridView ID="GridToBeApprove" runat="server" BackColor="#DEBA84" OnRowDataBound="GridToBeApprove_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" 
        CellSpacing="5" Width="100%" >
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns><asp:ButtonField ButtonType="Link" CommandName="Select" Text="Approve" Visible="false" />
        <asp:TemplateField ><HeaderTemplate><asp:CheckBox ID="cbSelectAll" runat="server" OnClick="selectAll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
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
<center><asp:Button ID="btnSendback" runat="server" CssClass="btnsmall" Text="Send Back" 
        onclick="btnSendback_Click" />&nbsp;&nbsp;  <asp:Button ID="btnHold" runat="server" CssClass="btnsmall" Text="Hold" OnClick="btnHold_Onclick" />
&nbsp;&nbsp;<asp:Button ID="btnToBeApprove" runat="server" CssClass="btnsmall" Text="Send for Approval" OnClick="btnToBeApprove_OnClick" />
<br /><asp:Label ID="lblExceptionAppTable" runat="server"></asp:Label></center>
    
<asp:Panel ID="pnlApp" runat="server">
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees"> <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>&nbsp;Membership No:&nbsp;<asp:Label ID="lblMembrshipNo" runat="server" ></asp:Label></h1>
<div id="Div12" style="display:block;">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:200px" >
<script type="text/javascript">
    function selectAlll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0 ; i < inputElements.length ; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    } 
</script>
<asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true" OnRowDataBound="GridAppTable_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5"
        CellSpacing="5" Width="100%">
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns><asp:ButtonField ButtonType="Link" CommandName="Select" Text="Approve" Visible="false" />
        <asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
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

<center><asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label></center>

<center><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading.png" 
        Height="51px" Width="265px" /><br /><asp:Button ID="btnApprove" CssClass="btnsmall" runat="server" Text="Generate Membership" OnClick="btnApprove_Onclick" /><br /><br /></center>
</asp:Panel>
<br />
<br />
</div>
<br />
</asp:Content>