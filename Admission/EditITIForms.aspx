<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="EditITIForms.aspx.cs" Inherits="Admission_EditITIForms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Update ITI Application
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Edit ITI Forms" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Update ITI Application</h1></div>
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
    <center><asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>
   Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged1" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp; Status:
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" 
        Width="146px" AutoPostBack="True" 
        onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                    <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                    <asp:ListItem Value="ReadyForExam" Text="ReadyForExam"></asp:ListItem>
                    <asp:ListItem Value="RollNoGenerated" Text="RollNoGenerated" ></asp:ListItem>
                    <asp:ListItem Value="Qualified" Text="Qualified"></asp:ListItem>
                    <asp:ListItem Value="Disqualify" Text="Disqualify"></asp:ListItem>
                    <asp:ListItem Value="Cancel" Text="Cancel"></asp:ListItem>
                </asp:DropDownList>
            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" />
</center>
<div class="togalfees" style="width:100%">
 <div class="headerDivImgfees">
     &nbsp;&nbsp;&nbsp;&nbsp;<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"/></a>
</div><h1>ITI Forms:</h1>
    <br />
<div id="Div12" style="display:Block">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
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
<script type="text/javascript" language="javascript">
    function ConfirmApp() {
        if (confirm("Are you sure you want to Continue ?") == true)
            return true;
        else
            return false;
    }
    </script>
<center>
</center>
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:350px">
<asp:GridView ID="grviti" runat="server" BackColor="White"  BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" 
        Width="100%" 
        Height="80%" onrowdatabound="grviti_RowDataBound" DataKeyNames="SID" >
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="cbSelectAll" runat="server" OnClick="selectAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkStatus" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div>
</div>
<div><asp:Panel ID="pnlStudent" runat="server">
<h1>Update Forms:</h1>
<center><h1 style="color:Maroon">Edit ITI Form Details</h1></center>
&nbsp;
<center>
<table class="tbl">
        <tr>                     
            <td align="left">
               Edit Status:
            </td>
            <td colspan="2" align="left">
                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" Width="146px" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="5">
                <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btnsmall" onclick="btnSave_Click"  OnClientClick="return ConfirmApp();"/>
            </td>
            <td align="left" colspan="2">
                &nbsp;</td>
        </tr>
        <tr><td colspan="5" align="center">
            <asp:Label ID="lblStatus" runat="server" ForeColor="Green"></asp:Label>
            </td></tr>
</table></center></asp:Panel>
</div>
</div>
    </div>
</asp:Content>