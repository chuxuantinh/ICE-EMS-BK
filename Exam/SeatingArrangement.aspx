<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="SeatingArrangement.aspx.cs" Inherits="Exam_SeatingArrangement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Seating Arrangement
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

<table class="tbl" width="80%"><tr><td>Schedule Type:<asp:DropDownList ID="ddlType"  CssClass="txtbox" runat="server" AutoPostBack="true" ><asp:ListItem Value="Home" Text="Home" ></asp:ListItem><asp:ListItem Value="Overseas" Text="Overseas"></asp:ListItem></asp:DropDownList></td>
   <td>Select Date:<asp:DropDownList ID="ddlExaminationdate" runat="server" CssClass="txtbox"  AutoPostBack="True" Width="120px"   dataTextFormatString="{0:dd/MM/yyyy}" DataSourceID="SqlDataSource1" DataTextField="Date" DataValueField="Date"  OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged"  ondatabound="ddlExaminationdate_DataBound" ></asp:DropDownList></td><td>
   Shift:<asp:DropDownList ID="ddlShift" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_OnSelectedIndexChanged" ><asp:ListItem Value="FN" Text="FN" /><asp:ListItem Value="AN" Text="AN" /></asp:DropDownList>
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
    </script>
    <center><asp:Label ID="lblExceptionExamFormTable" runat="server"></asp:Label></center>
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
<asp:GridView ID="GridExamCenter" runat="server" AutoGenerateColumns="False" 
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
 <asp:Label ID="lblExceptionCode" runat="server" ForeColor="Maroon" ></asp:Label>
 <table class="tbl"><tr><td>
 <table class="tbl">
 <tr><td>
Enter Exam Center Code:</td><td><asp:TextBox ID="txtExamCode" runat="server" CssClass="txtbox" Font-Bold="true" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtExamCode" FilterType="Numbers"></dev:FilteredTextBoxExtender><asp:Button ID="btnOKCenterCode" runat="server" OnClick="btnCenterCode_OnClick" Text="OK" CssClass="btnsmall"/></td></tr>
 <tr><td>Exam Center Name:</td><td><asp:Label ID="lblCenteNaem" runat="server" Font-Bold="true" ></asp:Label>,&nbsp;<br /><asp:Label ID="lblCity" runat="server" Font-Bold="true" ></asp:Label ></td></tr>
</table></td><td><asp:GridView ID="grdRoomCapacity" runat="server" 
                onselectedindexchanged="grdRoomCapacity_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
              <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            </asp:GridView></td><td>
            Total Examination Forms:&nbsp;&nbsp;<asp:Label ID="lblToApp" runat="server" ></asp:Label>
<asp:GridView ID="grdexam" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FFF1D4" />
    <SortedAscendingHeaderStyle BackColor="#B95C30" />
    <SortedDescendingCellStyle BackColor="#F1E5CE" />
    <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView>
            
            </td></tr>
</table><br />
<center>
<asp:Button ID="btnAddExamForm" runat="server" Text="Select Exam Form" OnClick="btnSelectExamFrom_OnClick" CssClass="btnsmall" />&nbsp;&nbsp;
<asp:Button ID="btnSort" runat="server" Text="Sort" OnClick="btnSort_Click" CssClass="btnsmall" />
</center>
   <br />
   <asp:Panel ID="pnlArrange" runat="server" Visible="false" >
<center>
   <br />
  <asp:Button ID="btnArrange" Text="Arrange" runat="server" CssClass="btnsmall" 
           onclick="btnArrange_Click"/><asp:Button ID="btnSave" Text="Save" 
           runat="server" CssClass="btnsmall" onclick="btnSave_Click" Enabled="false"/></center>
           <asp:Label ID="lblcolumn" runat="server" Visible="false"></asp:Label>
           <asp:Label ID="lblcapacity" runat="server" Visible="false"></asp:Label>
           <asp:Label ID="lblroomno" runat="server" Visible="false"></asp:Label>
           <asp:Label ID="lblrow" runat="server" Visible="false"></asp:Label>
          
         <center> <asp:Label ID="lblerror" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label><br /> 
         <input type="button" value="Print" onClick="PrintElem('#mydiv')" visible="false" id="btnPrint" runat="server"/>
         <div id="divArrange" runat="server" visible="false">
         <div id="mydiv">
         <center><h1>The Institution of Civil Engineers (India)</h1></center>
         <h3><center><u>Seating Plan <asp:Label ID="lblheadsession" runat="server"></asp:Label> Examination</u></center></h3>
         <table width="100%"  style="font-weight:bold;"><tr><td>Exam Center:-<asp:Label ID="lblExamCenter" runat="server"></asp:Label></td><td>CenterCode:-<asp:Label ID="lblpcentercode" runat="server"></asp:Label></td><td>Room:-<asp:Label ID="lblRoomNeme" runat="server"></asp:Label></td><td>Date:-<asp:Label ID="lblDate" runat="server"></asp:Label></td><td>Shift:-<asp:Label ID="lblshift" runat="server"></asp:Label></td></tr></table>
         
         <asp:GridView ID="grdArrange" runat="server" Width="98%" 
                 HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" 
                 BorderStyle="None" BorderWidth="1px" CellPadding="3">
          
             <FooterStyle BackColor="White" ForeColor="#000066" />
             <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
          
         <RowStyle HorizontalAlign="Center"
                 ForeColor="#000066" />
             <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#F1F1F1" />
             <SortedAscendingHeaderStyle BackColor="#007DBB" />
             <SortedDescendingCellStyle BackColor="#CAC9C9" />
             <SortedDescendingHeaderStyle BackColor="#00547E" />
           </asp:GridView></div></div>
         </center></asp:Panel><br />
        </div>
</asp:Content>

