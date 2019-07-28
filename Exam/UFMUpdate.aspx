<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="UFMUpdate.aspx.cs" Inherits="Exam_UFMUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">UFM Manager
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    

    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>

<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Unfair Means Update</h1></div>

<br />
<table  class="tbl">
 <tr><td>Examination Session:</td><td><asp:DropDownList ID="ddlExamSeason" 
         runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" 
         AutoPostBack="true" 
          ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
 <tr><td></td><td colspan="1"><asp:RadioButton ID="rbtnRollNo" runat="server" Text="Roll No" GroupName="A" />&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnSID" runat="server" Text="Membership ID" GroupName="A" /><br /><asp:TextBox ID="txtRollNo" runat="server" CssClass="txtbox" Width="150px" Font-Bold="true"></asp:TextBox></td><td>
     <asp:Button ID="btnOK" runat="server" Text=" OK " OnClick="btnOK_OnClcick" 
         CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" 
         runat="server" Text=" View All" OnClick="btnViewAll_OnClcick" 
         CssClass="btnsmall" /></td></tr>
 </table><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label><center><asp:Label ID="lblExceptionOK" runat="server" ></asp:Label></center>
 <hr />
 <asp:Panel ID="panelView" runat="server" CssClass="confirmationBox">
   <br />
   <table class="tbl" width="90%"><tr><td>Subject Code:&nbsp;&nbsp;<asp:Label ID="lblSubID" runat="server" Font-Bold="true"></asp:Label></td><td>Subject Name:&nbsp;<asp:Label ID="lblSubName" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>ExamDate:&nbsp;&nbsp;<asp:Label ID="lblExamDate" runat="server" Font-Bold="true"></asp:Label></td><td>Shift:&nbsp;&nbsp;<asp:Label ID="lblShift" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Membership No:&nbsp;<asp:Label ID="lblSID" runat="server" Font-Bold="true"></asp:Label></td><td>Roll No:&nbsp;&nbsp;<asp:Label ID="lblRollNo" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Course:&nbsp;&nbsp;<asp:Label ID="lblCourse" runat="server" Font-Bold="true"></asp:Label></td><td>Part:&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Center Code:&nbsp;<asp:Label ID="lblCenterCode" runat="server" Font-Bold="true"></asp:Label></td><td>Examination Center:&nbsp;<asp:Label ID="lblCenterName" runat="server" Font-Bold="true"></asp:Label></td></tr><tr><td>Status:&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label></td></tr><tr>
       <td colspan="2">Change Status:&nbsp;&nbsp;<br /><asp:ImageButton ID="btnunfair" ImageUrl="~/images/unfair.png" runat="server"  OnClick="btnunfair_OnClick" Width="30%" />
           <asp:ImageButton ID="btnfair" runat="server" ImageUrl="~/images/fair.png" 
               OnClick="btnfair_OnClick" Width="30%" />
       </td></tr>
</table>
<br /><center></center>

<br />
</asp:Panel>
<br />
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
<div class="togalfees" style="width:99%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">UFM Case List</div>
<div id="Div1x" style="display:block;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:200px">
<asp:GridView ID="GridUFM" runat="server" AutoGenerateColumns="true" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" OnRowDataBound="GridView1_OnRowDataBound" 
        GridLines="Vertical" onselectedindexchanged="GridView1_SelectedIndexChanged"  PageSize="50" 
        Width="100%" onpageindexchanging="GridUFM_PageIndexChanging">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <EmptyDataTemplate><center>No UFM Case Found !!!</center></EmptyDataTemplate>
     <PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" Position="Bottom" FirstPageText="First" NextPageText="Next" LastPageText="Last"  /><PagerStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Bottom" /> 
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
   </div>
   </div></div>
</div></asp:Content>