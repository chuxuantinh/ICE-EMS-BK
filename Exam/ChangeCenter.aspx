<%@ Page Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ChangeCenter.aspx.cs" Inherits="Exam_ChangeCenter" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Roll No. Generation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanage4r4" runat="server" ></asp:ScriptManager>
    <div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Change Exam Center" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Center Change & Generate New Roll No. </h1></div><br />
<center>Serial No:&nbsp;&nbsp;<asp:TextBox ID="txtSN" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtSN_OnTextChanged" CssClass="txtbox" ></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp; Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" Width="100px"></asp:TextBox><br />
<asp:Label ID="lblExceptionSN" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
</center><asp:Panel runat="server" ID="panelProfile">
<center><h3 class="hl3"><asp:Label ID="lbSName" runat="server" Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="lblTitle" runat="server" ></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblFName" runat="server" Font-Bold="true"></asp:Label></h3>
IMID:&nbsp;<asp:Label ID="lblIMID" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Membership No.:&nbsp;<asp:Label ID="lblSID" runat="server" Font-Bold="true"></asp:Label><br />
Course:&nbsp;:<asp:Label ID="lblStream" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblCourse" runat="server" ></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server" ></asp:Label><br /><br />
</center></asp:Panel>
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>
    <br /> <script>
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
 <%--  <asp:ImageButton ID="ibtnExportDoc" runat="server" Visible="false" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTable_click" />&nbsp;&nbsp;<asp:ImageButton ID="ibtnExportExcel" Visible="false" runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTable_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ibtnExportPDF" runat="server" Visible="false" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTable_Click" />--%>
    <a id="Nagar" href="javascript:toggleA1w('Dev', 'Nagar');"><img src="../images/plus.png" alt="Show"></a>
</div><table style="color:White; font-size:18px; font-family:Times New Roman;"><tr><td>Student's Application for Examination Center Change:</td><td></td></tr></table>
<div id="Dev" style="display:none;">
 <input id="scrollPos3" runat="server" type="hidden" value="0" />
<div id="divdatagrid3" style="width: 99%; overflow:scroll; height:200px" >
    <asp:GridView ID="GridChangeApps" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" OnSelectedIndexChanged="GridChangedApps_OnselectedIndexChanged"
        CellSpacing="5" Width="100%">
        <Columns>
        <asp:CommandField ShowSelectButton="true" SelectText="Select" />
        <%--<asp:TemplateField HeaderText="Amount"><ItemTemplate></ItemTemplate><FooterTemplate></FooterTemplate></asp:TemplateField>--%>
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
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Select Examination Center:</div>
<div id="Div1x" style="display:none;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:200px"  >
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
    
    <table class="tbl" style="float:right; margin-right:10px; width:50%;"><tr><td>Exam Center Code:&nbsp;&nbsp;<asp:Label ID="lblCenterCode" runat="server" Font-Bold="true" ForeColor="Black" ></asp:Label></td></tr><tr><td>&nbsp;&nbsp;<asp:Label ID="lblCenteNaem" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblCenterAddress" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblCenterAddress2" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>,</td></tr><tr><td>&nbsp;<asp:Label ID="lblCenterCity" runat="server" Font-Bold="true"></asp:Label>, &nbsp;(<asp:Label ID="lblCenterState" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label> &nbsp;)-<asp:Label ID="lblPinCode" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td></tr>
<tr><td>Total Capacity:&nbsp;<asp:Label ID="lblCapacity" runat="server" ></asp:Label></td></tr>
</table>
<table class="tbl" width="35%"><tr><td>
Enter Exam Center Code:&nbsp;<br /><asp:TextBox ID="txtExamCode" runat="server" CssClass="txtbox" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtExamCode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td><asp:Button ID="btnOKCenterCode" runat="server" OnClick="btnCenterCode_OnClick" Text="OK" /></td></tr>
</table><center><asp:Label ID="lblExceptionCode" runat="server" ></asp:Label></center><br /><br /><br /><br /><br /><hr />

 
<center><asp:Button ID="btnGenerateRollNo" runat="server" Text="Generate Roll No." OnClick="btnGenerateRollNo_Click" /></center>
<br /><center>List of Generated RollNo</center><br />

   
 <input id="scrollPos4" runat="server" type="hidden" value="0" />
<div id="divdatagrid4" style="width: 99%; overflow:scroll; height:200px" 
            onscroll='javascript:setScroll(this, <% =scrollPos4.ClientID %> );'>
   
    <asp:GridView ID="GridView3" runat="server" AllowPaging="false"
        AutoGenerateColumns="true" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%">
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
           
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
   
   </div>
</div>
</asp:Content>