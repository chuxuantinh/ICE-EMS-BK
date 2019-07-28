<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="ReceiveOrder.aspx.cs" Inherits="Invent_ReceiveOrder" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Receive Order
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblText" Text="Receive Order" runat="server" CssClass="redirecttabhome"></asp:Label> 
         </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>Receive Order</h1></div>
    <asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
        <table width="90%" class="tbl">
                <tr>
                    <td align="left">
                        Session</td>
                    <td>
                        <asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" 
                            CssClass="txtbox" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged">
                            <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                            <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                        Year&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtYear" runat="server" AutoPostBack="true" CssClass="txtbox" 
                            OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Select Supplier</td>
                    <td>
                        <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" 
                            CssClass="txtbox" DataSourceID="SqlDataSource1" DataTextField="Name" 
                            DataValueField="Name" 
                            onselectedindexchanged="ddlSupplier_SelectedIndexChanged" Width="152px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                            SelectCommand="SELECT DISTINCT [Name] FROM [Supplier]"></asp:SqlDataSource>
                    </td>
                    <td colspan="2">Enter Order No:
                        <asp:TextBox ID="txtOrder" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txtOrder_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOrder">
                        </asp:FilteredTextBoxExtender>
                        <asp:Button ID="btnOrder"
                            runat="server" Text="Search" CssClass="btnsmall" 
                            onclick="btnOrder_Click" /></td>
                </tr>
                <tr><td></td><td><asp:Panel ID="pnlSupplier" runat="server" BackColor="#FFCC99" Visible="False">
                        <table>
                            <tr>
                                <td>
                                    Name:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address</td>
                                <td colspan="3">
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    City<br />
                                    <asp:Label ID="lblCity" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    State<br />
                                    <asp:Label ID="lblState" runat="server"></asp:Label>
                                </td>
                                <td>
                                    PinCode<br />
                                    <asp:Label ID="lblPincode" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel></td></tr>
         </table><script>
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
    </script><br /><br />
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Order placed</h1>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" /> <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px;"  >
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="25"
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
            DataSourceID="SqlDataSource3"  GridLines="Horizontal" 
            HorizontalAlign="Center" Width="100%" 
            onrowdatabound="GridView1_RowDataBound" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="SID" HeaderText="SID" 
                    SortExpression="SID" />
                <asp:BoundField DataField="Supplier" HeaderText="Supplier" 
                    SortExpression="Supplier" />
                <asp:BoundField DataField="OrderType" HeaderText="OrderType" 
                    SortExpression="OrderType" />
                <asp:BoundField DataField="OrderNo" HeaderText="OrderNo" 
                    SortExpression="OrderNo" />
              
                    <asp:BoundField DataField="RequiredQt" HeaderText="RequiredQt" 
                    SortExpression="RequiredQt" />
                      <asp:BoundField DataField="SupplyQt" HeaderText="SupplyQt" 
                    SortExpression="SupplyQt" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" 
                    SortExpression="Amount" />
                <asp:BoundField DataField="Session" HeaderText="Session" 
                    SortExpression="Session" />
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
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            
            SelectCommand="SELECT DISTINCT [SID], [Supplier], [OrderType], [OrderNo], [RequiredQt], [SupplyQt], [Amount], [Session] FROM [Purches] WHERE (([Supplier] = @Supplier) AND ([Session] = @Session) and ([Status]='Ordered')) ORDER BY[OrderNo] DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlSupplier" Name="Supplier" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="lblHiddenSeason" Name="Session" 
                    PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
          <br /></div></div></div>
       </asp:Panel>
         <asp:Panel id="pnlProspectus" runat="server" BackColor="#FFF7E7"> <br /><center><table class="tbl" width="60%"><tr><td align="center">
             <asp:Label ID="Label3" runat="server" Text="Order No" ForeColor="Maroon"></asp:Label><br /> <asp:Label ID="lblPros" runat="server" Text=""></asp:Label></td><td align="center"> 
                 <asp:Label ID="Label1" runat="server" Text="Quantity Ordered" 
                     ForeColor="Maroon"></asp:Label><br />  
                 <asp:Label ID="lblReqPros" runat="server"></asp:Label></td><td align="center"> 
                 <asp:Label ID="Label2" 
                     runat="server" Text="Quantity Received" ForeColor="Maroon"></asp:Label><br />
                 <asp:TextBox ID="txtSupPros"
                runat="server" CssClass="txtbox"></asp:TextBox></td><td><asp:Button 
                ID="btnPros" runat="server" Text="Add" CssClass="btnsmall" 
                onclick="btnPros_Click"/> <asp:Label ID="lblExcep" runat="server"></asp:Label></td></tr></table></center><br /></asp:Panel>
    <asp:Panel ID="pnlList" runat="server" Visible="false" BorderStyle="None">
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
        <br />
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A2" href="javascript:toggleA1x('Div2', 'A2');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Received Quantity For Order No::<asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label></h1>
<div id="Div2" style="display:block;">
  <input id="Hidden1" runat="server" type="hidden" value="0" /> <div id="div2" style="width: 100%; overflow:scroll; height:250px;"  >
       <asp:GridView ID="grdShow" runat="server" AllowPaging="True" PageSize="100"
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
            DataSourceID="SqlDataSource2" GridLines="Horizontal" HorizontalAlign="Center" 
            onrowdatabound="grdShow_RowDataBound" Width="100%">
            <Columns>
                <asp:BoundField DataField="OrderNo" HeaderText="OrderNo" 
                    SortExpression="OrderNo" />
                <asp:BoundField DataField="SubjectCode" HeaderText="SubjectCode" 
                    SortExpression="SubjectCode" />
                <asp:BoundField DataField="SubjectName" HeaderText="SubjectName" 
                    SortExpression="SubjectName" />
                <asp:BoundField DataField="RequiredQt" HeaderText="RequiredQt" 
                    SortExpression="RequiredQt" />
                <asp:BoundField DataField="SupplyQt" HeaderText="SupplyQt" 
                    SortExpression="SupplyQt" />

                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text=" Receive Quantity"></asp:Label>
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
            
            <asp:Label ID="lblException" runat="server" ForeColor="Maroon" Visible="False"></asp:Label>
            <asp:Label ID="lblSupply" runat="server" Visible="False"></asp:Label><br /> <asp:Button ID="btnSave" CssClass="btnsmall" runat="server" 
                    Text="Save" onclick="btnSave_Click" />&nbsp;&nbsp;<asp:Button 
                    ID="btnCancel" CssClass="btnsmall" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click1" /></center><br />
       
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                
                SelectCommand="SELECT DISTINCT [SubjectCode], [OrderNo], [SubjectName], [RequiredQt], [SupplyQt],[CourseID] FROM [PurchesList] WHERE ([OrderNo] = @OrderNo)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblOrderNo" Name="OrderNo" PropertyName="Text" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
                 </asp:Panel>
</div>
<br />
 
</asp:Content>

