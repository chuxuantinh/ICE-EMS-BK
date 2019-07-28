<%@ Page Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="InstitutionView.aspx.cs" Inherits="project_AllocatedProject" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">AICTE Institution
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblNext" runat="server" Text="View Profile" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Head of Institution</h1></div><br />
<table class="tbl"><tr><td>Select Institute Type:</td><td colspan="2">
<asp:DropDownList ID="droptype" runat="server" CssClass="txtbox" AutoPostBack="True" onselectedindexchanged="droptype_SelectedIndexChanged" Font-Bold="true" ForeColor="Maroon">
<asp:ListItem Value="CivilSectionB" Text="Degree-Civil"/>
<asp:ListItem Value="CivilPartII" Text="Diploma-Civil"/>
<asp:ListItem Value="ArchiSectionB" Text="Degree-Architecture"/>
<asp:ListItem Value="ArchiPartII" Text="Diploma-Architecture"/>
</asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
</td></tr>
</table>
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
<asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
<a id="A12" href="javascript:toggleA1w('Div122', 'A122');"><img src="../images/minus.png" alt="Show"/></a>
</div><h1><asp:Label ID="lblCount" runat="server" ></asp:Label></h1>
<div id="Div12" style="display:block;">
<input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:450px"><br />
<asp:GridView ID="grdDetails" runat="server"  BackColor="#DEBA84" AutoGenerateColumns="true" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="5" Width="100%" onrowdatabound="grdDetails_RowDataBound">
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView></div></div></div>
</div>
</asp:Content>