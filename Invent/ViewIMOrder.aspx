<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="ViewIMOrder.aspx.cs" Inherits="Invent_ViewIMOrder" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View IM Order
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
<asp:Label ID="lblText" Text="View IMOrder" runat="server" CssClass="redirecttabhome"></asp:Label> 
         </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>View IM Order</h1></div>
    <asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
        <table width="90%" class="tbl">
               
                <tr>
                    <td align="left">
                        Session:</td>
                    <td>
                        <asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" 
                            CssClass="txtbox" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged">
                            <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                            <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                        Year:&nbsp;&nbsp;<asp:TextBox ID="txtYear" runat="server" AutoPostBack="true" CssClass="txtbox" 
                            OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr><tr>
                    <td>
                        Select Search Criteria:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlSelect_SelectedIndexChanged" CssClass="txtbox" 
                                Width="152px">
                                <asp:ListItem Value="Type">Order Type</asp:ListItem>
                                <asp:ListItem Value="OrderNo">Order No</asp:ListItem>
                                <asp:ListItem>IMID</asp:ListItem>
                                <asp:ListItem Value="Consignment">Consignment No</asp:ListItem>
                                <asp:ListItem>Status</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                   
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                    <td colspan="4">
                        <asp:TextBox ID="txtOrder" runat="server" CssClass="txtbox" Visible="False"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txtOrder_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOrder">
                        </asp:FilteredTextBoxExtender>
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
                            Visible="False" Width="152px" CssClass="txtbox">
                            <asp:ListItem>Books</asp:ListItem>
                            <asp:ListItem>Prospectus</asp:ListItem>
                        </asp:DropDownList>
                    
                        <asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Visible="False"></asp:TextBox>
                          <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                            Visible="False" CssClass="txtbox">
                            <asp:ListItem>Ordered</asp:ListItem>
                            <asp:ListItem>Delivered</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnOrder"
                            runat="server" Text="Search" CssClass="btnsmall" 
                            onclick="btnOrder_Click" /></td>
                </tr>
                
         </table>
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
    </script><br /><br />
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Order placed</h1>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" /> <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px;"  >
           <center>
               <asp:GridView ID="GridView1" runat="server" 
                        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="5" CellSpacing="5"  GridLines="Horizontal" 
            HorizontalAlign="Center" Width="100%"  
            onrowdatabound="GridView1_RowDataBound" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
                   <Columns>
                       <asp:CommandField HeaderText="Select" ShowHeader="True" 
                           ShowSelectButton="True" />
                   </Columns>
            <EmptyDataTemplate><center> Record Not Found !</center></EmptyDataTemplate>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510"  HorizontalAlign="Center"/>
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView></center>
        
          <br /></div></div></div>
       </asp:Panel><asp:Panel ID="pnlUpdate" runat="server" Visible="false"><center> Update Status:<asp:DropDownList ID="ddlStatusUp" runat="server" CssClass="txtbox" AutoPostBack="True">
                <asp:ListItem>Ordered</asp:ListItem>
                <asp:ListItem>Delivered</asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;Enter Consignment No:<asp:TextBox ID="txtConsignment" CssClass="txtbox" runat="server"></asp:TextBox><asp:Button
               ID="btnOK" runat="server" Text="OK" CssClass="btnsmall" 
            onclick="btnOK_Click" />
        <asp:Label ID="lblUpdate" runat="server" ForeColor="Maroon"></asp:Label>
   </center></asp:Panel>
        <asp:Panel ID="pnlList" runat="server" Visible="false" BorderStyle="None">
           <script>
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
    </script>
   <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A2" href="javascript:toggleA2x('Div2', 'A2');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Books List&nbsp; for Order
           <asp:Label ID="lblOrderNo" runat="server"></asp:Label>, Membership No
           <asp:Label ID="lblIMID" runat="server"></asp:Label>
       </h1>
            <div ID="Div2" style="display:block;">
                <input id="Hidden1" runat="server" type="hidden" value="0" />
                <div ID="div2" 
                    onscroll="javascript:setScroll(this, <% =scrollPos.ClientID %> );" 
                    style="width: 100%; overflow:scroll; height:250px;">
                   
                    <asp:GridView ID="grdShow" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
             GridLines="Horizontal" HorizontalAlign="Center" 
           Width="100%" DataSourceID="SqlDataSource1">
           
                        <Columns>
                            <asp:BoundField DataField="SubjectCode" HeaderText="SubjectCode" 
                                SortExpression="SubjectCode" />
                            <asp:BoundField DataField="SubjectName" HeaderText="SubjectName" 
                                SortExpression="SubjectName" />
                            <asp:BoundField DataField="Required" HeaderText="Required" 
                                SortExpression="Required" />
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
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                        SelectCommand="SELECT DISTINCT [SubjectCode], [SubjectName], [Required], [CourseID] FROM [IMOrderList] WHERE (([IMID] = @IMID) AND ([OrderNo] = @OrderNo))">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblIMID" Name="IMID" PropertyName="Text" 
                                Type="String" />
                            <asp:ControlParameter ControlID="lblOrderNo" Name="OrderNo" PropertyName="Text" 
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                   
                       </div>
            </div></div>
                   
    </asp:Panel> 
    
</div>
<br />
</asp:Content>

