<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="CompositeFees.aspx.cs" Inherits="Acc_CompositeFees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Composite Fees
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab" /></td>
<td><asp:Label ID="lblACManage" runat="server" Text="Composite Fees" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
 <div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" /></h1><h1>Composite Fees </h1></div><br />
<center>
    <table>
<tr><td>Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; 
    <asp:TextBox ID="txtYearSeason" AutoPostBack="true" 
        OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" 
        Width="60px" />&nbsp;&nbsp;SessionID:&nbsp;<asp:Label ID="lblSessionID" runat="server"></asp:Label></td></tr></table><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false" />

<br />Membership No :&nbsp;<asp:TextBox runat="server" ID="txtSID" CssClass="txtbox" Font-Bold="true" Width="100px" />&nbsp;<asp:Button ID="btnOk" runat="server" CssClass="btnsmall" Text="OK" onclick="btnOk_Click" />
<br /><asp:Label ID="lblException" Font-Bold="true" ForeColor="Brown" runat="server" /><br />
<table class="tbl">
<tr><td>Amount:</td><td><asp:TextBox ID="txtAmount" CssClass="txtbox" runat="server" Width="100px" /></td><td>Type:</td><td><asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox"><asp:ListItem Text="Direct" Value="Direct" /><asp:ListItem Text="Regular" Value="Regular" /></asp:DropDownList></td> <td>Status:</td><td><asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox"><asp:ListItem Text="Submitted" Value="Submitted" /><asp:ListItem Text="NotSubmitted" Value="NotSubmitted" /><asp:ListItem Text="Process" Value="Process" /></asp:DropDownList></td></tr></table>
<br /><asp:Button ID="btnSubmit" runat="server" Text="Submit Fees" CssClass="btnsmall" onclick="btnSubmit_Click" />
    <br />
    <br /></center>
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
    <script type="text/javascript" language="javascript">
        function ConfirmApp() {
            if (confirm("Are you sure you want to Delete?") == true)
                return true;
            else
                return false;
        }
</script>
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Composite Fees</h1>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:350px;">
<center><asp:GridView ID="GridFees" runat="server" BackColor="White" 
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        HorizontalAlign="Center" Width="100%" AllowPaging="True" 
        OnRowCommand="GridFees_SelectedIndexChanged" 
        onrowdatabound="GridFees_RowDataBound">
        <Columns>
             <asp:TemplateField>
             <ItemTemplate><asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Del" Text="Delete" OnClientClick="return ConfirmApp();" /> </ItemTemplate>
             </asp:TemplateField>      
        </Columns>
        <EmptyDataTemplate><center> Record Not found !!!</center></EmptyDataTemplate>
        <RowStyle BackColor="White" ForeColor="#003399" HorizontalAlign="Center" />
        <PagerSettings PreviousPageText="Previous" Position="Bottom" 
            FirstPageText="First" NextPageText="Next" LastPageText="Last"  />
        <PagerStyle HorizontalAlign="Left" VerticalAlign="Bottom" BackColor="#99CCCC" 
            ForeColor="#003399" />
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView></center>
   </div>
   </div></div>
   <asp:Panel runat="server" Height="150px" />
</div>
<br />
</asp:Content>