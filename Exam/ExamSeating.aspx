<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamSeating.aspx.cs" Inherits="Exam_ExamSeating" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Seating Arrangement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
    <br />
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Seating Arrangement</h1></div>
<center>Select Examination Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" 
        runat="server"  
        AutoPostBack="true" CssClass="txtbox" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged2" 
          ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></center>
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label><br />
<script>
    function toggleA1y(showHideDiv, switchImgTag) {
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
 <a id="A1y" href="javascript:toggleA1y('Div1y', 'A1y');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Select Examination Center:</div>
<div id="Div1y" style="display:none;"><br />
  <input id="scrollPos2" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid2" style="width: 99%; overflow:scroll; height:200px" 
            onscroll='javascript:setScroll(this, <% =scrollPos2.ClientID %> );'>
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"  PageSize="30"
        CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="Black" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChangeing"
        GridLines="Vertical" onselectedindexchanged="GridExamCenter_SelectedIndexChanged" 
        Width="100%">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
         <PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" Position="Bottom" FirstPageText="First" NextPageText="Next" LastPageText="Last"  /><PagerStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Bottom" /> 
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
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
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
 <table class="tbl" style="float:right; margin-right:10px; width:50%;"><tr><td>Exam Center Code:&nbsp;&nbsp;<asp:Label ID="lblCenterCode" runat="server" ></asp:Label></td></tr><tr><td>Exam Center Name:&nbsp;&nbsp;<asp:Label ID="lblCenteNaem" runat="server" ></asp:Label>,&nbsp;<br /><asp:Label ID="lblCity" runat="server" ></asp:Label></td></tr>
<tr><td>Total Capacity:&nbsp;<asp:Label ID="lblCapacity" runat="server" ></asp:Label></td></tr>
</table>
<table class="tbl" width="35%"><tr><td>
Enter Exam Center Code:&nbsp;<br /><asp:TextBox ID="txtExamCode" runat="server" CssClass="txtbox" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtExamCode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td><asp:Button ID="btnOKCenterCode" runat="server" OnClick="btnCenterCode_OnClick" Text="OK" CssClass="btnsmall"/></td></tr>
</table><center><asp:Label ID="lblExceptionCode" runat="server" ></asp:Label></center><hr />


    <br />
   <table width="95%" class="tbl"><tr><td>Schedule Type:&nbsp;&nbsp;<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" ><asp:ListItem Value="Home" Text="Home" ></asp:ListItem><asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem></asp:DropDownList></td><td>Select Date:&nbsp;&nbsp;
   <asp:DropDownList ID="ddlExaminationdate" runat="server" CssClass="txtbox" 
           AutoPostBack="True" Width="120px"   dataTextFormatString="{0:dd/MM/yyyy}"
            DataSourceID="SqlDataSource1" DataTextField="Date" DataValueField="Date" 
           OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged" 
           ondatabound="ddlExaminationdate_DataBound" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;Shift:&nbsp;<asp:DropDownList ID="ddlShift" runat="server" ><asp:ListItem Value="FN" Text="FN" /><asp:ListItem Value="AN" Text="AN" /></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [Date] FROM [ExamDate] WHERE ([Season] = @Season) AND ([Type] = @Type) ORDER BY [Date]">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                    PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="ddlType" Name="Type" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource></td></tr></table> 
  
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
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Examination Subjectes on Date:&nbsp;<asp:Label ID="lblExamDAte" runat="server" Font-Bold="true"></asp:Label></div>
<div id="Div1x" style="display:none;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 97%; overflow:scroll; height:200px" 
            >
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="Black"  OnRowDataBound="GridExamSub_OnRowDateBound"
        GridLines="Vertical"
        Width="100%">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:BoundField DataField="SCode" HeaderText="SCode" SortExpression="SCode" />
            <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
            <asp:BoundField DataField="Stream" HeaderText="Stream" 
                SortExpression="Stream" />
            <asp:BoundField DataField="Course" HeaderText="Course" 
                SortExpression="Course" />
            <asp:BoundField DataField="Part" HeaderText="Part" SortExpression="Part" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Shift" HeaderText="Shift" SortExpression="Shift" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        
        SelectCommand="SELECT [SCode], [SName], [Stream], [Course], [Part], [Date], [Shift] FROM [ExamDate] WHERE (([Date] = @Date) AND ([Season] = @Season) AND ([Shift] = @Shift)) ORDER BY [Date]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlExaminationdate" Name="Date" 
                PropertyName="SelectedValue" Type="DateTime" />
            <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                PropertyName="Text" Type="String" />
                 <asp:ControlParameter ControlID="ddlShift" Name="Shift" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
   </div>
   </div></div>
   
    <script>
        function togglez(showHideDiv, switchImgTag) {
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
    </script><center><asp:Label ID="lblExceptionExamFormTable" runat="server"></asp:Label></center>
   <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
    
  <%-- <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />--%>
   
    <a id="Az" href="javascript:togglez('Divz', 'Az');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;"><asp:Button ID="btnAddExamForm" runat="server" Text="Select Exam Form" OnClick="btnSelectExamFrom_OnClick" CssClass="btnsmall" />&nbsp;&nbsp;Examination Forms via Examination Date</div>
<div id="Divz" style="display:block;">
 
   
 <input id="scrollPos3" runat="server" type="hidden" value="0" />
<div id="divdatagrid3" style="width: 100%; overflow:scroll; height:200px" 
          >
   
    <asp:GridView ID="GridExamSub" runat="server" AllowPaging="true"
        AutoGenerateColumns="true" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4"  PageSize="550"
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
</div>
    <br /><center>Total Examination Forms:&nbsp;&nbsp;<asp:Label ID="lblToApp" runat="server" ></asp:Label></center>
   <table class="tbl" width="95%"><tr><td style="color:Maroon; font-size:15px; font-family:Times New Roman;">Civil Engineering</td></tr>
   <tr><td>Part I:&nbsp;&nbsp;<asp:Label ID="lblToCP1" runat="server" ></asp:Label></td><td>Part II:&nbsp;&nbsp;<asp:Label ID="lblToCP2" runat="server" ></asp:Label></td><td>Section A:&nbsp;&nbsp;<asp:Label ID="lblToCS1" runat="server" ></asp:Label></td><td>Section B:&nbsp;&nbsp;<asp:Label ID="lblToCS2" runat="server" ></asp:Label></td></tr>
   <tr><td></td></tr>
   <tr><td style="color:Maroon; font-size:15px; font-family:Times New Roman;">Architectural Engineering</td></tr>
   <tr><td>Part I:&nbsp;&nbsp;<asp:Label ID="lblToAP1" runat="server" ></asp:Label></td><td>Part II:&nbsp;&nbsp;<asp:Label ID="lblToAP2" runat="server" ></asp:Label></td><td>Section A:&nbsp;&nbsp;<asp:Label ID="lblToAS1" runat="server" ></asp:Label></td><td>Section B:&nbsp;&nbsp;<asp:Label ID="lblToAS2" runat="server" ></asp:Label></td></tr>
   </table>
<hr />
<asp:Panel ID="panel" runat="server"  Width="100%">
<div style="float:right; margin-right:5px; width:50%;">
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" Visible="true"
        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSource4" 
        onselectedindexchanged="GridView3_SelectedIndexChanged" Width="100%">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="RoomNo" HeaderText="RoomNo" 
                SortExpression="RoomNo" />
            <asp:BoundField DataField="Capacity" HeaderText="Capacity" 
                SortExpression="Capacity" />
            <asp:BoundField DataField="Columns" HeaderText="Columns" 
                SortExpression="Columns" />
        </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT [ID], [RoomNo], [Capacity], [Columns] FROM [Rooms] WHERE ([ID] = @ID) ORDER BY [RoomNo]">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblCenterCode" Name="ID" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

</div>
<div style="width:45%;">
<center>Room No:&nbsp;&nbsp;<asp:Label ID="lblRoomNo" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label><br />
Capacity:&nbsp;&nbsp;<asp:Label ID="lblRoomCapacity" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;&nbsp;Column(s)&nbsp;<asp:Label ID="lblRoomColumn" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label>
</center>
<table class="tbl" width="98%"><tr><td>Course:&nbsp;<br />
    &nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList></td><td>Part/Section:&nbsp;<br />
    &nbsp;<asp:DropDownList ID="ddlPart"  runat="server" CssClass="txtbox"><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList></td></tr><tr><td>No. of Forms:&nbsp;&nbsp;<asp:TextBox ID="txtNOOfForms" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox></td><td><asp:Button ID="btnSelectForRollNo" runat="server" Text="Select for Seating Plan." OnClick="btnSelectForRollNo_Click" CssClass="btnsmall"/></td></tr></table><br />
<asp:Label ID="lblExceptionSelect" runat="server" ForeColor="Red" ></asp:Label><br />

</div></asp:Panel>
   <br /><center>Total Examination Forms:&nbsp;&nbsp;<asp:Label ID="lbltef" runat="server" ></asp:Label>/<asp:Label ID="lblToExamForms" runat="server" ></asp:Label></center>
   <table class="tbl" width="95%"><tr><td style="color:Maroon; font-size:15px; font-family:Times New Roman;">Civil Engineering</td></tr>
   <tr><td>Part I:&nbsp;&nbsp;<asp:Label ID="lbltp1" runat="server" ></asp:Label>/<asp:Label ID="lblToPart1" runat="server" ></asp:Label></td><td>Part II:&nbsp;&nbsp;<asp:Label ID="lbltpII" runat="server"></asp:Label>/<asp:Label ID="lbltoPartII" runat="server" ></asp:Label></td><td>Section A:&nbsp;&nbsp;<asp:Label ID="lbltsA" runat="server" ></asp:Label>/<asp:Label ID="lblToSectionA" runat="server" ></asp:Label></td><td>Section B:&nbsp;&nbsp;<asp:Label ID="lbltsB" runat="server" ></asp:Label>/<asp:Label ID="lblToSectinB" runat="server" ></asp:Label></td></tr>
   <tr><td></td></tr>
   <tr><td style="color:Maroon; font-size:15px; font-family:Times New Roman;">Architectural Engineering</td></tr>
   <tr><td>Part I:&nbsp;&nbsp;<asp:Label runat="server" ID="lbltpp1" ></asp:Label>/<asp:Label ID="lblToPPPartI" runat="server" ></asp:Label></td><td>Part II:&nbsp;&nbsp;<asp:Label ID="lbltpp2" runat="server" ></asp:Label>/<asp:Label ID="lblToPPartII" runat="server" ></asp:Label></td><td>Section A:&nbsp;&nbsp;<asp:Label runat="server" ID="lbltssA"></asp:Label>/<asp:Label ID="lblToSSectionA" runat="server" ></asp:Label></td><td>Section B:&nbsp;&nbsp;<asp:Label ID="lbltssB" runat="server" ></asp:Label>/<asp:Label ID="lblToSSectinB" runat="server" ></asp:Label></td></tr>
   </table><hr /><br />
 <script>
     function toggleD(showHideDiv, switchImgTag) {
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
    </script><center><asp:Label ID="lblExceptionSeating" runat="server"></asp:Label></center>
   <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
    
  <%-- <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />--%>
   
    <a id="AD" href="javascript:toggleD('DivD', 'AD');"><asp:Button ID="btnDisposeGrid" runat="server" OnClick="btnDisposeGrid_OnClick" Text="Clear Selection" />&nbsp;&nbsp;<img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">&nbsp;&nbsp;Seating Arrangement </div>
<div id="DivD" style="display:block;">
 
   
 <input id="scrollPos4" runat="server" type="hidden" value="0" />
<div id="divdatagrid4" style="width: 100%; overflow:scroll; height:200px" 
            onscroll='javascript:setScroll(this, <% =scrollPos4.ClientID %> );'>
   
    <asp:GridView ID="GridSeating" runat="server" BackColor="White" PageSize="50" AllowPaging="true"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        ForeColor="Black" GridLines="Vertical" Width="100%" 
        onpageindexchanging="GridSeating_PageIndexChanging1">
        <Columns>
        </Columns>
        <RowStyle BackColor="#F7F7DE" />
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </div></div></div>
   
    <center><asp:Label runat="server" ID="lblExceptionSave"></asp:Label><br /><asp:Button ID="btnSaveRoom" runat="server" Text="Save" OnClick="btnSaveRoom_clcik" CssClass="btnsmall" /><br /><br /></center>
    </div>
</asp:Content>

