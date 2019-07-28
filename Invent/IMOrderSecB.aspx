<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="IMOrderSecB.aspx.cs" Inherits="Invent_IMOrderSecB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">IM Order Entry(Section B)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblText" Text="IM Order Entry(Sec B)" runat="server" CssClass="redirecttabhome"></asp:Label> 
         </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>IM ORDER ENTRY(Sec B)</h1></div>
    <asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
        &nbsp;&nbsp; &nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" >
    <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
    <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
    </asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>

<br /> <script>
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
    </script><br /> <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA3x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>List Of Forms</h1>
<div id="Div1x" style="display:block;">
  <input id="Hidden1" runat="server" type="hidden" value="0" /> <div id="div2" style="width: 100%; overflow:scroll; height:250px;"  >
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSource1" Width="100%" 
            HorizontalAlign="Center" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" />
                <asp:BoundField DataField="IMID" HeaderText="IMID" SortExpression="IMID" />
                <asp:BoundField DataField="Course" HeaderText="Course" 
                    SortExpression="Course" />
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView></div></div></div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT [SID], [IMID], [Course] FROM [IMOrderSecB] WHERE (([Status] = @Status) AND ([Session] = @Session)) order By IMID">
            <SelectParameters>
                <asp:Parameter DefaultValue="NotSubmitted" Name="Status" Type="String" />
                <asp:ControlParameter ControlID="lblHiddenSeason" Name="Session" 
                    PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
     <asp:Panel ID="pnlView" runat="server" >   <script>
            function toggleA2x(showHideDiv, switchImgTag) {
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
    </script><br /> <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A2x" href="javascript:toggleA3x('Div2x', 'A2x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Select Subject From SectionB(Extra) List</h1>
<div id="Div2x" style="display:block;">
  <input id="Hidden2" runat="server" type="hidden" value="0" /> <div id="div3" 
        style="width: 100%; overflow:scroll; height:198px;"  >
  <asp:GridView ID="GridView2" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" CellSpacing="2"  Width="100%" HorizontalAlign="Center" 
            onrowdatabound="GridView2_RowDataBound" ><Columns>
        <asp:TemplateField ><HeaderTemplate>Approve</HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
    </Columns>
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
        </div></div></div>
         <script>
             function toggleA3x(showHideDiv, switchImgTag) {
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
    </script><br /> <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A3x" href="javascript:toggleA3x('Div3x', 'A3x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1> Subject List SectionB(Regular)</h1>
<div id="Div3x" style="display:block;">
  <input id="Hidden3" runat="server" type="hidden" value="0" /> <div id="div4" 
        style="width: 100%; overflow:scroll; height:198px;"  >
  <asp:GridView ID="GridView3" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" CellSpacing="2"  Width="100%" HorizontalAlign="Center" 
            onrowdatabound="GridView3_RowDataBound" >
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
        </div></div></div><center>
        <asp:Button ID="btnSubmit" Text="Submit" runat="server"  CssClass="btnsmall" 
                onclick="btnSubmit_Click" /></center>
                <asp:GridView ID="GridView4" runat="server" 
            onrowdatabound="GridView4_RowDataBound" Visible="False" ></asp:GridView><asp:Label ID="lblTotal" Text="" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblQuantity" Text="" runat="server" Visible="false"></asp:Label></asp:Panel>
    </asp:Panel><br />
   
</div>
<br />
</asp:Content>

