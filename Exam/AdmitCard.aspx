<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="AdmitCard.aspx.cs" Inherits="Exam_AdmitCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Student Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Admit Cards" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;"><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Admit Card</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox> 
   &nbsp;&nbsp; <asp:Button ID="Showdata" runat="server" 
        Text="Show Admit Card Data" onclick="Showdata_Click" /></td></tr></table></center><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label>
<table width="90%" class="tbl"></table>
<asp:Label ID="lblExceptionOK" runat="server" ></asp:Label><br />
  AdmitCard Printed Date:  <asp:TextBox ID="TxtDate" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;<asp:Button 
        ID="Glot" runat="server" Text="Generate Lote No" onclick="Glot_Click" />&nbsp;&nbsp;&nbsp;Lot Number is:<asp:TextBox ID="LotNo" runat="server"></asp:TextBox>
    &nbsp;&nbsp; 
    <asp:Button ID="UpdateList" runat="server" Text="OK" 
        onclick="UpdateList_Click" />
    <br /><center></center><br />
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
<asp:Label ID="lblAdmit" runat="server" Text="List Of Students"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Admit Card Printed: &nbsp;&nbsp;&nbsp;<asp:Label ID="TotalAdmitCard"
        runat="server"></asp:Label>
</div>
 <div  id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px">
 <asp:GridView ID="DataShow" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" 
         style="margin-top: 6px; font-weight: 700; text-align: center; font-family: Verdana; font-size: small;" BackColor="White" 
         BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
         Height="173px" Width="100%">
         <EmptyDataTemplate><center>Records Not Found!!!<div style="height:400px;"></div></center></EmptyDataTemplate>
        <Columns>
           
            <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" />
            <asp:BoundField DataField="RollNo" HeaderText="RollNo" 
                SortExpression="RollNo" />
            <asp:BoundField DataField="IMID" HeaderText="IMID" SortExpression="IMID" />
            <asp:BoundField DataField="Course" HeaderText="Course" 
                SortExpression="Course" />
            <asp:BoundField DataField="Part" HeaderText="Part" SortExpression="Part" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
        </Columns>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#330099" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <SortedAscendingCellStyle BackColor="#FEFCEB" />
        <SortedAscendingHeaderStyle BackColor="#AF0101" />
        <SortedDescendingCellStyle BackColor="#F6F0C0" />
        <SortedDescendingHeaderStyle BackColor="#7E0000" />
    </asp:GridView>
   
   </div>
</div>
    <center>
    <strong>
    Select Lot No To Print Admit Card:&nbsp;&nbsp;&nbsp;<asp:DropDownList 
            ID="ddlRSN" runat="server" DataSourceID="SqlDataSource2" 
            DataTextField="RSN" DataValueField="RSN" Width="100px">
        </asp:DropDownList> 
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ICEDataConnectionString %>" 
            SelectCommand="SELECT DISTINCT [RSN] FROM [ExamForms] WHERE (([ExamSeason] = @ExamSeason))">
            <SelectParameters>
            <asp:ControlParameter ControlID="lblExamSeasonHidden" Name="ExamSeason" 
                PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        &nbsp;&nbsp;<asp:Button ID="Padmit" runat="server" Text="Print Admit Card" 
            onclick="Padmit_Click" />
        </strong>
        </center>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ICEDataConnectionString %>" 
        
        SelectCommand="SELECT [SID], [RollNo], [IMID], [Course], [Part], [City] FROM [ExamForms] WHERE (([Status] = @Status) AND ([ExamSeason] = @ExamSeason))">
        <SelectParameters>
            <asp:Parameter DefaultValue="RollNoGenerated" Name="Status" Type="String" />
            <asp:ControlParameter ControlID="lblExamSeasonHidden" Name="ExamSeason" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
   
</asp:Content>