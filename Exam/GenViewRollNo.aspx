<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="GenViewRollNo.aspx.cs" Inherits="Exam_GenViewRollNo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Roll No. List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:Label ID="lblPageName" runat="server" CssClass="redirecttabhome" Text="View Roll No." ></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Roll No. Generation</h1></div><br />
<br /><center>Select Examination Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="100px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></center>
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>
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
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Exam Center Code:<asp:TextBox ID="txtExamCenter" runat="server" CssClass="txtbox"></asp:TextBox>
        <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="btnsmall" 
            onclick="btnOk_Click" /></div>
<div id="Div1x" style="display:none;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:200px" >
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" 
        GridLines="Vertical" onselectedindexchanged="GridView1_SelectedIndexChanged" 
        Width="100%">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="ToSeat" HeaderText="ToSeat" 
                SortExpression="ToSeat" />
            <asp:BoundField DataField="RollNo" HeaderText="RollNo" 
                SortExpression="RollNo" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT [ID], [Name], [City], [State], [Email], [ToSeat], [RollNo] FROM [ExamCenter] WHERE ([Season] = @Season) ORDER BY [ID]">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
   </div>
   </div></div>

<br />
<table class="tbl"><tr><td>Exam Center Code:&nbsp;</td><td><asp:Label ID="lblCenterCode" runat="server" ></asp:Label></td><td>Exam Center Name:&nbsp;&nbsp;<asp:Label ID="lblCenteNaem" runat="server" ></asp:Label></td></tr>
<tr><td>Total Capacity:&nbsp;</td><td><asp:Label ID="lblCapacity" runat="server" ></asp:Label></td></tr>
</table>
<br /><hr />
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
    </script><center><asp:Label ID="lblExceptionAppTable" runat="server"></asp:Label></center>
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
    
   <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
   
    <a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><table style="color:White; font-weight:bold;"><tr><td>
        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlCourse_SelectedIndexChanged">
            <asp:ListItem Value="Civil">Civil Engineering</asp:ListItem>
            <asp:ListItem Value="Architecture">Architecture Engineering</asp:ListItem>
        </asp:DropDownList></td><td>
            <asp:DropDownList ID="ddlPart" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlPart_SelectedIndexChanged">
               
            <asp:ListItem>PartI</asp:ListItem>
            <asp:ListItem>PartII</asp:ListItem>
            <asp:ListItem>SectionA</asp:ListItem>
            <asp:ListItem>SectionB</asp:ListItem>
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList></td><td>Total students:<asp:Label ID="lblCount" runat="server"></asp:Label> </td></tr></table>
<div id="Div12" style="display:block;">
 
   
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:400px" 
            onscroll='javascript:setScroll(this, <% =scrollPos2.ClientID %> );'>
   
    <asp:GridView ID="GridView2" runat="server"
        AutoGenerateColumns="true" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4"  PageSize="25"
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onpageindexchanging="GridView2_PageIndexChanging" 
        onrowdatabound="GridView2_RowDataBound">
        <RowStyle BackColor="#F7F7DE"  HorizontalAlign="Center"/>
        <Columns>
           
        </Columns>
        <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
   
   
   </div>
</div>
</div>
<div style="height:150px;"></div>
</div>
</asp:Content>

