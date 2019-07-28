<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="MarksStatement.aspx.cs" Inherits="Exam_MarksStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Marks Statement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Marks Statement" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div class="fromRegisterlbl"><h1>Marks Statement</h1></div>
<br />
<table  class="tbl">
 <tr><td>Examination Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
 <tr><td></td><td colspan="1"><asp:RadioButton ID="rbtnRollNo" runat="server" Text="Roll No" GroupName="A" />&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnSID" runat="server" Text="Membership" GroupName="A" />&nbsp;<asp:RadioButton ID="rbtnIMID" runat="server" GroupName="A" Text="IMID" /><br /><asp:TextBox ID="txtRollNo" runat="server" CssClass="txtbox" Width="150px" Font-Bold="true"></asp:TextBox></td><td><asp:Button ID="btnOK" runat="server" Text=" OK " OnClick="btnOK_OnClcick" CssClass="btnsmall" /></td></tr>
 </table><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label><center><asp:Label ID="lblExceptionOK" runat="server" ></asp:Label></center>
<asp:Panel ID="pnlSubject" runat="server" Visible="False">
</asp:Panel>
<hr />
<script>
    function toggleA1x(showHideDiv, switchImgTag) {
        var ele = document.getElementById(showHideDiv);
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
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblGridTitle" runat="server" ></asp:Label>
<br />
</div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px" >
            
<asp:GridView ID="gridMarks" runat="server" PageSize="30" AllowPaging="True"
        Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
        BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" 
        HeaderStyle-HorizontalAlign="Center" OnPageIndexChanging="gridMarks_PageIndexChanging"
        onselectedindexchanged="gridMarks_SelectedIndexChanged">
                    <Columns>
                      <%-- <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lbtnView" Text="View Marksheet" runat="server" CommandName="cmdView" CommandArgument='<%#Eval("RollNo") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField>--%>
                        <asp:CommandField SelectText="View Marksheet" ShowSelectButton="True" />
                    </Columns>
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    <RowStyle HorizontalAlign="Center" />
</asp:GridView>
   
   </div>
</div>
    </div>

    <script>
        function toggleA1y(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
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
 <a id="A2x" href="javascript:toggleA1y('Div2x', 'A2x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">Roll No:
<asp:Label ID="Label1" runat="server" ></asp:Label>
<br />
</div>
<div id="Div2x" style="display:block;">
  <input id="Hidden1" runat="server" type="hidden" value="0" />
<div id="div2" style="width: 100%; overflow:scroll; height:400px" >
            
<asp:GridView ID="GridMarksheet" runat="server" PageSize="30" AllowPaging="True" 
        Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
        BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" 
        HeaderStyle-HorizontalAlign="Center" 
        ><Columns></Columns>
                   
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    <RowStyle HorizontalAlign="Center" />
</asp:GridView>
   
   </div>
</div>
    </div>
    </div>
</asp:Content>

