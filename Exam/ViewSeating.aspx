<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ViewSeating.aspx.cs" Inherits="Exam_ViewSeating" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Seating Plan
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    

    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../jquery-1.3.1.min.js" > </script> 
      <script language="javascript">
          function PrintElem(elem) {
              Popup($(elem).html());
          }

          function Popup(data) {
              var mywindow = window.open('', 'my div', 'height=400,width=600');
              mywindow.document.write('<html><head><title>Seating Plan</title>');
              /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
              mywindow.document.write('</head><body >');
              mywindow.document.write(data);
              mywindow.document.write('</body></html>');

              mywindow.print();
              mywindow.close();

              return true;
          }
</script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>View Seating Arrangement</h1></div>
<center>Select Examination Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></center>
<br />
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
  <table><tr><td>
  <table class="tbl">
   <tr><td>Enter Exam Center Code:</td>
<td><asp:TextBox ID="txtExamCode" runat="server" CssClass="txtbox" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtExamCode" FilterType="Numbers"></dev:FilteredTextBoxExtender>
<asp:Button ID="btnOKCenterCode" runat="server" OnClick="btnCenterCode_OnClick" Text="OK" CssClass="btnsmall" /></td></tr>
<tr><td>Exam Center Name:</td><td><asp:Label ID="lblCenteNaem" runat="server" ></asp:Label></td></tr>
<tr><td>Total Capacity:</td><td><asp:Label ID="lblCapacity" runat="server" ></asp:Label></td></tr>
</table>
<table class="tbl"><tr><td>Schedule Type:&nbsp;&nbsp;<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" ><asp:ListItem Value="Home" Text="Home" ></asp:ListItem><asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem></asp:DropDownList></td><td>Select Date:&nbsp;&nbsp;<asp:DropDownList 
            ID="ddlExaminationdate" runat="server" CssClass="txtbox" 
           AutoPostBack="True" Width="120px"   dataTextFormatString="{0:dd/MM/yyyy}"
            DataSourceID="SqlDataSource1" DataTextField="Date" DataValueField="Date" OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;Shift:&nbsp;<asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_OnSelectedIndexChanged" ><asp:ListItem Value="FN" Text="FN" /><asp:ListItem Value="AN" Text="AN" /></asp:DropDownList>
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
        <div style="width:45%;">
<center>Room No:&nbsp;&nbsp;<asp:Label ID="lblRoomNo" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label><br />
Capacity:&nbsp;&nbsp;<asp:Label ID="lblRoomCapacity" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;&nbsp;Column(s)&nbsp;<asp:Label ID="lblRoomColumn" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label>
</center>
<br />
<asp:Label ID="lblExceptionSelect" runat="server" ForeColor="Red" ></asp:Label><br />
<br /><br />
</div>
  </td>
  <td>
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
                  <asp:BoundField DataField="RoomName" HeaderText="RoomName" 
                SortExpression="RoomName" />
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
       SelectCommand="SELECT [ID], [RoomNo],[RoomName], [Capacity], [Columns] FROM [Rooms] WHERE ([ID] = @ID) and (Season=@Season) ORDER BY [RoomNo]">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblCenterCode" Name="ID" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
  
  </td>
  </tr>
  
  </table>
  
    
<center><asp:Label ID="lblExceptionCode" runat="server" ></asp:Label></center><hr />

     <asp:Panel ID="panel" runat="server"  Width="100%">
    

    
</asp:Panel> <center> <input type="button" value="Print" onClick="PrintElem('#mydiv')" visible="false"  id="btnPrint" runat="server"/>
         <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btnsmall"  Visible="false" OnClick="Delete_Click"/></center>

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
      <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
   
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Seating Arrangement on Date:&nbsp;<asp:Label ID="lblExamDAte" runat="server" Font-Bold="true"></asp:Label></div>
<div id="Div1x" style="display:block;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%;height:400px">
                 <div id="mydiv">
         <center><h1 style="color:Black;">The Institution of Civil Engineers (India)</h1></center>

                  <h3><center><u>Seating Plan <asp:Label ID="lblSeasonHidden" runat="server" Font-Bold="true" ></asp:Label> Examination</u></center></h3>
         <table width="100%"  style="font-weight:bold;"><tr><td>Exam Center:-<asp:Label ID="lblCity" runat="server" ></asp:Label></td><td>CenterCode:-<asp:Label ID="lblCenterCode" runat="server" ></asp:Label></td><td>Room:-<asp:Label ID="lblRoomName" runat="server"></asp:Label></td>
         <td>Date:-<asp:Label ID="lblDate" runat="server"></asp:Label></td><td>Shift:-<asp:Label ID="lblshift" runat="server"></asp:Label></td></tr></table>
<asp:GridView ID="GridView1" runat="server" 
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3"  OnRowDataBound="GridExamSub_OnRowDateBound" OnPageIndexChanging="GridView1_PageIndexChanging"
        Width="100%">
        <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
        <EmptyDataTemplate><center>Seating Record Not Found !!!</center></EmptyDataTemplate>
        <Columns>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView></div>
   </div>
   </div></div>       
           <asp:Label ID="lblcolumn" runat="server" Visible="false"></asp:Label>
           <asp:Label ID="lblrow" runat="server" Visible="false"></asp:Label>
</div></asp:Content>