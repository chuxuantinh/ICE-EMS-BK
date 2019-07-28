<%@ Page Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true"
    CodeFile="ApproveFinalMarksheet.aspx.cs" Inherits="Exam_ApproveFinalMarksheet"
    Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" runat="Server">
    Student Promotion [Change Stream and Part]</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 148px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server">
    </asp:ScriptManager>
    <div id="redirect" runat="server">
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click"
                        Text="Home" CssClass="redirecttab">
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
                        onclick="lbtnNext1Redirect_Click">
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="lblPageName" runat="server" Text="Marksheet Approve" CssClass="redirecttabhome">
                    </asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTempEnrol" runat="server" ></asp:Label><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>View Final Pass Student</h1></div>
<center><table><tr>
    <td style="font-family: Verdana; font-size: small; font-weight: bold; color: #800080" 
        class="style1">Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td>
    <td style="font-family: Verdana; font-size: small; font-weight: bold; font-style: normal; color: #800080">Year:&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;&nbsp;Session ID:&nbsp;<asp:Label ID="lblExamSeasonHidden" runat="server" ></asp:Label></td></tr></table>
<br />
</center>
<asp:Label ID="lblHiddendStream" runat="server" Visible="false"></asp:Label><asp:Label ID="lblStreamDDL" runat="server" Visible="false"></asp:Label>
<center><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true" ></asp:Label><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Course:&nbsp;<asp:DropDownList ID="ddlCourse" Width="150px" runat="server" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
Part/Section:&nbsp;<asp:DropDownList  Width="150px" ID="ddlPart" runat="server" CssClass="txtbox"><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList>
   &nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkViewResult" runat="server" 
        AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" 
        Text="View Final Pass" />
<br />
&nbsp;&nbsp;&nbsp;&nbsp; <br /><br />
</center>
        &nbsp;&nbsp;&nbsp;
<br />
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
    <asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportDocAppTableDoc_click" />
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:1px;"><h1>&nbsp;<asp:Label ID="lblSessionHiddend" runat="server" Font-Bold="true"></asp:Label>&nbsp;Total 
        Record found For <asp:Label ID="lblPart" runat="server"></asp:Label>&nbsp;Final 
        Pass:&nbsp;&nbsp; <asp:Label ID="Label1" runat="server" ></asp:Label>
        </h1></div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:400px">
<br /><asp:GridView ID="GridExamForms" runat="server"  
        BackColor="White" BorderColor="#E7E7FF"  BorderStyle="Dotted" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" PageSize="40"  HorizontalAlign="Left" Width="100%">
        <Columns>
        </Columns>
        <EmptyDataTemplate><center> Exam Form Not found Please Update Result.. !</center></EmptyDataTemplate>
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