<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Fees/FeeMaster.master" AutoEventWireup="true" CodeFile="CreateFeesHead.aspx.cs" Inherits="Administrator_Fees_CreateFeesHead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Create Fees Header
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
<link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<script>
      function toggle(showHideDiv, switchImgTag) {
          var ele = document.getElementById(showHideDiv);
          var imageEle = document.getElementById(switchImgTag);
          if (ele.style.display == "block") {
              ele.style.display = "none";
              imageEle.innerHTML = '<img src="../../images/plus.png">';
          }
          else {
              ele.style.display = "block";
              imageEle.innerHTML = '<img src="../../images/minus.png">';
          }
      }
</script>
<div class="togalfees">
<div class="headerDivImgfees">
<a id="imageDivLink1" href="javascript:toggle('contentDivImg1', 'imageDivLink1');"><img src="../../images/minus.png" alt="Show"/></a>
</div><h1>Create Fees Header &nbsp;<asp:DropDownList ID="ddlType" CssClass="txtbox" runat="server" Width="120px" ForeColor="Brown" Font-Bold="true"><asp:ListItem>Home</asp:ListItem><asp:ListItem>Overseas</asp:ListItem></asp:DropDownList>
</h1>
<div id="contentDivImg" style="display: block;">
<asp:UpdatePanel ID="updCreateFee" runat="server" ><ContentTemplate>
<div ><br />
<center><table><tr><td>Fees Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win" /></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="70px" /></td></tr></table>
<asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label><br />
<asp:Label ID="lblExceptionID" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
<div>Fees Name:&nbsp;<asp:TextBox ID="txtFeesName" runat="server" CssClass="txtbox" MaxLength="20" Width="285px" Font-Bold="true" ForeColor="Brown" />
<dev:FilteredTextBoxExtender ID="filterchk" runat="server" FilterType="UppercaseLetters" TargetControlID="txtFeesName"/>
&nbsp;&nbsp;Amount:&nbsp;<asp:TextBox ID="txtAmount" runat="server" CssClass="txtbox" Width="90PX" />
<dev:FilteredTextBoxExtender ID="fltAmount" runat="server" TargetControlID="txtAmount" FilterType="Numbers"/>
&nbsp;&nbsp;<asp:DropDownList ID="ddlFees" runat="server" CssClass="txtbox" Width="290px" onselectedindexchanged="ddlFees_SelectedIndexChanged" AutoPostBack="true" /><br /><br />
<asp:Button ID="btnCreateNew" CssClass="bigbutton" runat="server" Text="Add Fees Header" onclick="btnCreateNew_Click" /><br /><br />
<script type="text/javascript" language="javascript">
    function ConfirmApp() {
        if (confirm("Are you sure you want to Update this Fees?") == true)
            return true;
        else
            return false;
    }
</script>
<script type="text/javascript" language="javascript">
    function ConfirmAppDel() {
        if (confirm("Are you sure you want to Delete this Fees?") == true)
            return true;
        else
            return false;
    }
</script>
<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnsmall" onclick="btnupdate_Click" OnClientClick="return ConfirmApp();" />&nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnsmall" onclick="btnDelete_Click" OnClientClick="return ConfirmAppDel();" />
</div><br />
</center>
</div>
<script>
    function toggleBG(showHideDiv, switchImgTag) {
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
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>&nbsp;&nbsp;View Fees Header</b><br />
<br /><div id="Div1x" style="display: block;"><div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px;">
<asp:GridView ID="GridFeesHeader" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true"  AllowPaging="false"  BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="5" Width="100%" onrowdatabound="GridFeesHeader_RowDataBound">
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
</ContentTemplate></asp:UpdatePanel>
</div></div>
</asp:Content>