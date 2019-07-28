<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="IMOrderSupply.aspx.cs" Inherits="Invent_IMOrderSupply" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">IM ORDER SUPPLY
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblText" Text="IM Order Entry" runat="server" CssClass="redirecttabhome"></asp:Label> 
         </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>IM ORDER SUPPLY</h1></div>
    <asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
        &nbsp;&nbsp; &nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" >
    <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
    <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
    </asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
<br />
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
    </script><br /><br /><center><asp:Label ID="lblException" runat="server" ForeColor="Maroon"></asp:Label></center>
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Order placed</h1>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" /> <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px;"  >
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="25"
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
            DataSourceID="SqlDataSource1"  GridLines="Horizontal" 
            HorizontalAlign="Center" Width="100%" 
            onrowdatabound="GridView1_RowDataBound" onselectedindexchanged="GridView1_SelectedIndexChanged" 
            >
            <Columns> <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="IMID" HeaderText="IMID" SortExpression="IMID" />
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                <asp:BoundField DataField="OrderNo" HeaderText="OrderNo" 
                    SortExpression="OrderNo" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" 
                    SortExpression="Amount" />
                <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" 
                    SortExpression="OrderDate" />
                <asp:BoundField DataField="RequiredQt" HeaderText="RequiredQt" 
                    SortExpression="RequiredQt" />
                <asp:BoundField DataField="SupplyQt" HeaderText="SupplyQt" 
                    SortExpression="SupplyQt" />
                <asp:BoundField DataField="OrderType" HeaderText="OrderType" 
                    SortExpression="OrderType" />
               
               
            </Columns>
            <EmptyDataTemplate><center> Record Not Found !</center></EmptyDataTemplate>
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
        
          <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT IMID, Type, OrderNo, Amount, OrderDate, RequiredQt, SupplyQt, OrderType FROM IMOrder WHERE (Session = @Session) AND (RequiredQt &lt;&gt; SupplyQt)">
              <SelectParameters>
                  <asp:ControlParameter ControlID="lblHiddenSeason" Name="Session" 
                      PropertyName="Text" Type="String" />
              </SelectParameters>
        </asp:SqlDataSource>
        
          <br /></div></div></div>
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
        <br /><asp:Panel ID="pnlBooks" runat="server">
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A2" href="javascript:toggleA1x('Div2', 'A2');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Received Quantity For IMID :<asp:Label ID="lblIMID" runat="server" Text=""></asp:Label> and Order No::<asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label></h1>
<div id="Div2" style="display:block;">
  <input id="Hidden1" runat="server" type="hidden" value="0" /> <div id="div2" style="width: 100%; overflow:scroll; height:250px;"  >
       <asp:GridView ID="grdShow" runat="server" AllowPaging="True" PageSize="100"
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
            DataSourceID="SqlDataSource2" GridLines="Horizontal" HorizontalAlign="Center" 
             Width="100%" onrowdatabound="grdShow_RowDataBound" 
            onselectedindexchanged="grdShow_SelectedIndexChanged">
            <Columns>
                
                
                <asp:BoundField DataField="Course" HeaderText="Course" 
                    SortExpression="Course" />
                <asp:BoundField DataField="Part" HeaderText="Part" 
                    SortExpression="Part" />
                <asp:BoundField DataField="SubjectCode" HeaderText="SubjectCode" 
                    SortExpression="SubjectCode" />

                <asp:BoundField DataField="SubjectName" HeaderText="SubjectName" 
                    SortExpression="SubjectName" />
                <asp:BoundField DataField="Required" HeaderText="Required" 
                    SortExpression="Required" />
                <asp:BoundField DataField="Supply" HeaderText="Supply" 
                    SortExpression="Supply" />
                    <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text=" Supply Quantity"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:Label ID="lblQuantity" runat="server" Text="" Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseID" HeaderText="CourseID" 
                    SortExpression="CourseID" />
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
            </div></div></div><center>
        <asp:Button ID="btnSupply" runat="server" Text="Supply Order" CssClass="btnsmall" 
                onclick="btnSupply_Click" />
                <br />
                </center></asp:Panel>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                
                
            SelectCommand="SELECT DISTINCT OrderNo, Course, Part, SubjectCode, SubjectName, Required, Supply, CourseID FROM IMOrderList WHERE (OrderNo = @OrderNo)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblOrderNo" Name="OrderNo" PropertyName="Text" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource> 
            <asp:Panel id="pnlProspectus" runat="server" BackColor="#FFF7E7"> <br /><center><table class="tbl" width="60%"><tr><td align="center">
             <asp:Label ID="Label3" runat="server" Text="Order No" ForeColor="Maroon"></asp:Label><br /> <asp:Label ID="lblPros" runat="server" Text=""></asp:Label></td><td align="center"> 
                 <asp:Label ID="Label1" runat="server" Text="Quantity Ordered" 
                     ForeColor="Maroon"></asp:Label><br />  
                 <asp:Label ID="lblReqPros" runat="server"></asp:Label></td><td align="center"> 
                 <asp:Label ID="Label2" 
                     runat="server" Text="Quantity Supplied" ForeColor="Maroon"></asp:Label><br />
                 <asp:TextBox ID="txtSupPros"
                runat="server" CssClass="txtbox"></asp:TextBox></td><td><asp:Button 
                ID="btnPros" runat="server" Text="Add" CssClass="btnsmall" 
                onclick="btnPros_Click"/> <asp:Label ID="lblExcep" runat="server"></asp:Label></td></tr></table></center><br /></asp:Panel>
    </asp:Panel><br />
</div>
<br />
</asp:Content>

