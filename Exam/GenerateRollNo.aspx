<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="GenerateRollNo.aspx.cs" Inherits="Exam_GenerateRollNo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Roll No. Generation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanage4r4" runat="server" ></asp:ScriptManager>
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Roll No. Generation</h1></div><br />
<center>Select Examination Session:&nbsp;&nbsp;<asp:DropDownList 
            ID="ddlExamSeason" runat="server" AutoPostBack="true" CssClass="txtbox"
            onselectedindexchanged="ddlExamSeason_SelectedIndexChanged"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" Width="100px"></asp:TextBox></center>
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
<div class="togalfees" style="width:99%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Select Examination Center:</div>
<div id="Div1x" style="display:none;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:200px" >
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
            <asp:BoundField DataField="ToSeat" HeaderText="ToSeat" SortExpression="ToSeat" />
            <asp:BoundField DataField="RollNo" HeaderText="RollNo" SortExpression="RollNo" />
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
    <table class="tbl" style="float:right; margin-right:10px; width:50%;"><tr><td>Exam Center Code:&nbsp;&nbsp;<asp:Label ID="lblCenterCode" runat="server" Font-Bold="true" ForeColor="Black" ></asp:Label></td></tr><tr><td><asp:Label ID="lblCenteNaem" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblCenterAddress" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblCenterAddress2" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>,</td></tr><tr><td><asp:Label ID="lblCenterCity" runat="server" Font-Bold="true"></asp:Label>, &nbsp;(<asp:Label ID="lblCenterState" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label> &nbsp;)-<asp:Label ID="lblPinCode" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td></tr>
<tr><td>Total Capacity:&nbsp;<asp:Label ID="lblCapacity" runat="server" ></asp:Label></td></tr>
</table>
<table class="tbl" width="35%"><tr><td>Select Exam Center<asp:DropDownList 
            ID="ddlExamCity" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlExamCity_SelectedIndexChanged"></asp:DropDownList></td>
        <td>
  ExamCenter Code:&nbsp;<br /><asp:TextBox ID="txtExamCode" runat="server" 
        CssClass="txtbox" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtExamCode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td>&nbsp;</td></tr>
</table><center><asp:Label ID="lblExceptionCode" runat="server" ></asp:Label></center><br /><br /><br /><hr />
<script>
    function toggleA1j(showHideDiv, switchImgTag) {
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
 <a id="A1j" href="javascript:toggleA1j('Div1j', 'A1j');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Select City:&nbsp;&nbsp;&nbsp; Examination Center Alloted:&nbsp;<asp:Label ID="lblExamCapacity"  runat="server"  ForeColor="White"></asp:Label></div>
<div id="Div1j" style="display:none;"><br />
  <input id="scrollPos2" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid2" style="width: 99%; overflow:scroll; height:200px" >
<asp:GridView ID="GridView2" runat="server"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            DataSourceID="SqlDataSource3" ForeColor="Black" GridLines="Vertical" 
            onrowdatabound="GridView2_RowDataBound" Width="100%">
            <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" />
                <asp:BoundField DataField="Status" HeaderText="Status" 
                    SortExpression="Status" />
                <asp:BoundField DataField="ExamSeason" HeaderText="ExamSeason" 
                    SortExpression="ExamSeason" />
                <asp:BoundField DataField="IMID" HeaderText="IMID" SortExpression="IMID" />
                <asp:BoundField DataField="Course" HeaderText="Course" 
                    SortExpression="Course" />
                <asp:BoundField DataField="Part" HeaderText="Part" SortExpression="Part" />
                <asp:BoundField DataField="RollNo" HeaderText="RollNo" 
                    SortExpression="RollNo" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="City2" HeaderText="City2" SortExpression="City2" />
                <asp:BoundField DataField="RSN" HeaderText="RollNo" SortExpression="RSN" Visible="false" />
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
                         SelectCommand="SELECT SID, Status, ExamSeason, IMID, Course, Part, RollNo, City, City2, RSN FROM ExamForms WHERE (SID IN (SELECT SID FROM Student WHERE (Status = 'Active'))) AND (City = @City) AND (ExamSeason = @ExamSeason) AND (Status='Submitted') order by SID">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblSeasonHidden" Name="ExamSeason" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ddlExamCity" Name="City" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
   </div>
   </div></div><hr />
   <br /><center><strong>Total Examination Forms:&nbsp;&nbsp;<asp:Label ID="lblToExamForms" runat="server" ></asp:Label></strong></center>
   <table class="tbl" width="95%"><tr><td style="color:Maroon; font-size:15px; font-family:Times New Roman;">Civil Engineering</td></tr>
   <tr><td><strong>Part I:&nbsp;&nbsp;<asp:Label ID="lblToPart1" runat="server" ></asp:Label></strong></td><td><strong>Part II:&nbsp;&nbsp;<asp:Label ID="lbltoPartII" runat="server" ></asp:Label></strong></td><td><strong>Section A:&nbsp;&nbsp;<asp:Label ID="lblToSectionA" runat="server" ></asp:Label></strong></td><td><strong>Section B:&nbsp;&nbsp;<asp:Label ID="lblToSectinB" runat="server" ></asp:Label></strong></td></tr>
   <tr><td></td></tr>
   <tr><td style="color:Maroon; font-size:15px; font-family:Times New Roman;">Architectural Engineering</td></tr>
   <tr><td><strong>Part I:&nbsp;&nbsp;<asp:Label ID="lblToPPPartI" runat="server" ></asp:Label></strong></td><td><strong>Part II:&nbsp;&nbsp;<asp:Label ID="lblToPPartII" runat="server" ></asp:Label></strong></td><td><strong>Section A:&nbsp;&nbsp;<asp:Label ID="lblToSSectionA" runat="server" ></asp:Label></strong></td><td><strong>Section B:&nbsp;&nbsp;<asp:Label ID="lblToSSectinB" runat="server" ></asp:Label></strong></td></tr>
   </table><hr /><br />
  <table class="tbl" width="95%"><tr><td>Course:&nbsp;&nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList></td><td>Part/Section:&nbsp;&nbsp;<asp:DropDownList 
          ID="ddlPart" runat="server" CssClass="txtbox"  AutoPostBack="true"
          onselectedindexchanged="ddlPart_SelectedIndexChanged">
      <asp:ListItem>--Select--</asp:ListItem>
      <asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList></td><td>No. of Forms:&nbsp;&nbsp;<asp:TextBox 
              ID="txtNOOfForms" runat="server" CssClass="txtbox" Width="50px" 
              ontextchanged="txtNOOfForms_TextChanged"></asp:TextBox></td></tr></table>
  <center><asp:Label ID="lblExceptionSelect" runat="server" ForeColor="Red" ></asp:Label><br /></center><br />
 
 <center>   <asp:Label ID="Label1" runat="server" Text="Start Roll No From"> 
         </asp:Label> &nbsp;&nbsp;  <asp:TextBox ID="StartRol" runat="server"></asp:TextBox> &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="End Roll No With"></asp:Label>
 &nbsp;&nbsp;<asp:TextBox ID="EndRollno" runat="server"></asp:TextBox> </center>
  <br />
   
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
    <table style="color:White; font-size:18px; font-family:Times New Roman;"><tr><td>Examination Forms for Roll No. Generation:&nbsp;&nbsp:&nbsp;</td><td style="color:White; font-size:20px; font-family:Vardana;">Record Found:-&nbsp;&nbsp;&nbsp;<asp:Label ID="TotalRecord" runat="server" Text=""></asp:Label></td></tr></table>
<div id="Div12" style="display:block;">
 <input id="scrollPos3" runat="server" type="hidden" value="0" />
<div id="divdatagrid3" style="width: 100%; overflow:scroll; height:200px">
    <asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5"
        CellSpacing="5" Width="100%">
        <Columns>
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
<center><asp:Button ID="btnGenerateRollNo" runat="server" Text="Generate Roll No." CssClass="btnsmall" OnClick="btnGenerateRollNo_Click" /></center>

<div>
   <table border="3" bgcolor="#ffffcc" width="100%" cellspacing="2" cellpadding="2">
    <tr>
    <td>
       <center><strong> <asp:Label ID="Input" runat="server" Text="Enter To Show Roll NO Or Hold List"></asp:Label> </strong></center>
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <center>  <asp:TextBox ID="checkno" runat="server" ontextchanged="TextBox1_TextChanged" 
            Width="150px"></asp:TextBox></td></center>
    
   
        <td>  
         <strong> Put Hold/Unhold:<asp:FileUpload ID="FileUpload1" runat="server" /> </strong>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp
          <asp:ImageButton ID="Saveimp" runat="server" ImageUrl="~/images/Saveimg.jpg"
            Height="30px" Width="30px"  AlternateText="Saveim" onclick="Saveimp_Click" /> </td> <td><asp:DropDownList ID="holdcmb" runat="server">
                <asp:ListItem>--Select--</asp:ListItem>
                <asp:ListItem>Hold</asp:ListItem>
                <asp:ListItem>Submitted</asp:ListItem>
            </asp:DropDownList></td><td> 
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/chang.png"
            Height="30px" Width="30px"  AlternateText="changeci" onclick="ImageButton4_Click" 
            />  </td>
        
        
        <td><asp:ImageButton ID="AdmitCard" runat="server" ImageUrl="~/images/Printadmit.jpg"
            Height="30px" Width="30px"  AlternateText="Print Admit Card" onclick="AdmitCard_Click" 
            /><br /><strong>Blank Roll No </strong></td>
    
    
    </tr>
    </table>
</div>

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
    <div class="headerDivImgfees"> <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />

</div><div style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblAdmit" runat="server" Text="List of Generated RollNo"></asp:Label>
</div>
<div id="Div1" style="display:block;">
  <input id="Hidden1" runat="server" type="hidden" value="0" />
<div  id="div2" style="width: 100%; overflow:scroll; height:200px">
    <asp:GridView ID="GridView3" runat="server" AllowPaging="true"
        AutoGenerateColumns="true" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4"  PageSize="25"
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onpageindexchanging="GridView3_PageIndexChanging">
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
           
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView></div>
   </div>
</div>
    </div>
</asp:Content>

