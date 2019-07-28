<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="IMOrderEntry.aspx.cs" Inherits="Invent_IMOrderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">IM Order Entry
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
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>IM ORDER ENTRY</h1></div>
    <asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
        &nbsp;&nbsp; &nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" >
    <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
    <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
    </asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
<br />
<table width="60%" class="tbl">
<tr>
<td colspan="5">
<h3 style="color: #800000"> Order For Print</h3></td>
</tr>
<tr><td align="right">Date:</td><td align="left"><asp:TextBox ID="txtDate" runat="server" CssClass="txtbox"></asp:TextBox> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender>
    </td>
    <td align="left">
        <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            controltovalidate="txtDate" Display="Dynamic" errormessage="Insert Date " 
            ValidationGroup="A">*</asp:RequiredFieldValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </td>
<td align="left">Order No: </td><td>
                       <asp:Label ID="lblOrderNo" runat="server" ForeColor="Maroon"></asp:Label></td></tr>
            <tr>
                <td align="right">
                    Select Order Type:</td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="ddlOType" runat="server" AutoPostBack="True" 
                        CssClass="txtbox" onselectedindexchanged="ddlOType_SelectedIndexChanged" 
                        Width="150px">
                        <asp:ListItem>Books</asp:ListItem>
                        <asp:ListItem>Prospectus</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
    </tr>
    </table><br />
                <asp:Panel ID="pnlIm" runat="server"><script>
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
 <a id="A2x" href="javascript:toggleA1x('Div2x', 'A2x');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>
    AutoGenarated Order
    <asp:Image ID="ibtnViewDairy" ImageUrl="~/images/dairycount.gif"  runat="server" AlternateText="Dairy" /><br /></h1>
<dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-150" OffsetY="0" PopupControlID="pnlDairyCount" TargetControlID="ibtnViewDairy" ></dev:PopupControlExtender>
<asp:Panel ID="pnlDairyCount" runat="server"  CssClass="pnlpopup" >
<div class="redsubtitle"><center>Stock Count</center></div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="5"
        DataSourceID="SqlDataSource4" Width="90%" Height="100px">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="CourseID" 
                SortExpression="CourseID" />
            <asp:BoundField DataField="Course" HeaderText="Course" 
                SortExpression="Course" />
            <asp:BoundField DataField="Part" HeaderText="Part" SortExpression="Part" />
            <asp:BoundField DataField="SubjectCode" HeaderText="SubjectCode" 
                SortExpression="SubjectCode" />
            <asp:BoundField DataField="Stoke" HeaderText="Stoke" SortExpression="Stoke" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT DISTINCT [CourseID], [Course], [Part], [SubjectCode], [Stoke] FROM [SubjectMaster]">
    </asp:SqlDataSource>
</asp:Panel>
<div id="Div2x" style="display:none;">
  <input id="scrollPos" runat="server" type="hidden" value="0" /> <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px;"  >
        <asp:GridView ID="grdIM" runat="server" AutoGenerateColumns="True" 
            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" CellSpacing="2"  Width="100%" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
               
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
        </asp:GridView></div></div></div>
               <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT IMID, CPartI, CPartII,CPartIIE, CSectionA, CSectionB, APartI, APartII,APartIIE, ASectionA, ASectionB, CourseID FROM IMBooks WHERE (CPartI &lt;&gt; 0) OR (CPartII &lt;&gt; 0) OR (CPartIIE &lt;&gt; 0) OR (CSectionA &lt;&gt; 0) OR (CSectionB &lt;&gt; 0) OR (APartI &lt;&gt; 0) OR (APartII &lt;&gt; 0) OR (APartIIE &lt;&gt; 0) OR (ASectionA &lt;&gt; 0) OR (ASectionB &lt;&gt; 0)"></asp:SqlDataSource>--%>
            </asp:Panel><table class="tbl" width="85%"><tr><td align="right">Insert 
        IMID: </td><td align="left">
                <asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox> 
                <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="btnsmall" 
                    onclick="btnOK_Click" />
                <asp:Label ID="lblIM" runat="server" ForeColor="Maroon"></asp:Label>
            </td><td align="center" style="color:Maroon; background-color:InfoBackground"><h4>Account Details</h4>Books Amount=&nbsp;Rs.
                <asp:Label ID="lblBooksAmt" runat="server" Text=""></asp:Label><br />Prospectus Amount=&nbsp;Rs.
                <asp:Label ID="lblProspectusAmt" runat="server" Text=""></asp:Label><br />Total Amount=&nbsp;Rs.
                <asp:Label ID="lblTotalAmt" runat="server" Text=""></asp:Label></td></tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIMName" runat="server" Text=""></asp:Label>
                    </td>
            </tr>
                </table>
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
    </script> <center><asp:Panel ID="pnlCourse" runat="server"><table><tr><td colspan="4">CourseID:<asp:DropDownList ID="ddlCourseId" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource3" DataTextField="CourseID" 
                        DataValueField="CourseID">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp; Course:<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" 
                        AutoPostBack="True">
                        <asp:ListItem Value="Civil">Civil Engineering</asp:ListItem>
                        <asp:ListItem Value="Architecture">Architecture Engineering</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp; Part: <asp:DropDownList ID="ddlPart" 
                            runat="server" CssClass="txtbox" 
                        AutoPostBack="True" onselectedindexchanged="ddlPart_SelectedIndexChanged">
                        <asp:ListItem>PartI</asp:ListItem>
                        <asp:ListItem>PartII</asp:ListItem>
                        <asp:ListItem>SectionA</asp:ListItem>
                        <asp:ListItem>SectionB</asp:ListItem>
                    </asp:DropDownList>
            </td>  </tr></table></asp:Panel></center>
            <asp:Panel runat="server" ID="pnlsupply"><center><table><tr><td>
                &nbsp;</td></tr></table></center></asp:Panel>
                <br /><asp:Panel ID="pnlGrid" runat="server">
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA3x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>List Of Books</h1>
<div id="Div1x" style="display:block;">
  <input id="Hidden1" runat="server" type="hidden" value="0" /> <div id="div2" style="width: 100%; overflow:scroll; height:250px;"  >
        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
            DataSourceID="SqlDataSource2"  GridLines="Horizontal" 
            HorizontalAlign="Center" Width="100%" 
            onrowdatabound="GridView2_RowDataBound" 
           >
            <Columns>
                <asp:BoundField DataField="SubjectCode" HeaderText="SubjectCode" 
                    SortExpression="SubjectCode" />
                <asp:BoundField DataField="SubjectName" HeaderText="SubjectName" 
                    SortExpression="SubjectName" />
                      
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:TemplateField>
                     <HeaderTemplate>
                         <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></HeaderTemplate>
                   <ItemTemplate>
                       <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtbox"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>  <asp:BoundField DataField="Stoke" HeaderText="Stoke" SortExpression="Stoke" />
                <asp:BoundField DataField="SubjectType" HeaderText="SubjectType" 
                    SortExpression="SubjectType" />
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
        </asp:GridView></div></div></div><center>
        <asp:Button ID="btnAddList" CssClass="btnsmall" runat="server" Text="Add" 
                            onclick="btnAddList_Click" /></center>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [SubjectCode], [SubjectName],[SubjectType], [Price],[Stoke] FROM [SubjectMaster] WHERE (([CourseID] = @CourseID) AND ([Course] = @Course) AND ([Part] = @Part))">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCourseId" Name="CourseID" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ddlCourse" Name="Course" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ddlPart" Name="Part" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        </asp:Panel>
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
 <a id="A2" href="javascript:toggleA2x('Div3', 'A2');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Order List 
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;Total Quantity=
            <asp:Label ID="lblQuantity" runat="server"></asp:Label>&nbsp;&nbsp;
       TotalAmount=Rs. <asp:Label ID="lblTotal"
                runat="server"></asp:Label>
            </h1>
            <div ID="Div3" style="display:block;">
                <input id="Hidden2" runat="server" type="hidden" value="0" />
                <div ID="div4" 
                    onscroll="javascript:setScroll(this, <% =scrollPos.ClientID %> );" 
                    style="width: 100%; overflow:scroll; height:250px;">
                   
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="true" 
                        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="5" CellSpacing="5" Width="100%" 
                        onrowdatabound="GridView3_RowDataBound">
                        <EmptyDataTemplate>
                            <center>
                                Record(s) Not Added !</center>
                        </EmptyDataTemplate>
                        <Columns>
                        </Columns>
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div></div>
                        <asp:Button ID="btnSend" runat="server" CssClass="btnsmall" 
                             Text="Generate Order" onclick="btnSend_Click" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btnsmall" 
                            Text="Cancel" onclick="btnCancel_Click" /><br /><asp:Label ID="lblException" runat="server" 
                            ForeColor="Maroon"></asp:Label>
               
    </asp:Panel>
        <asp:Panel id="pnlProspectus" runat="server" BackColor="#FFF7E7"> <br /><center>
            <asp:Label ID="lblPros" runat="server" Text="Enter Number Of Quantity"></asp:Label>
            &nbsp;:&nbsp;<asp:TextBox ID="txtPros"
                runat="server" CssClass="txtbox" ontextchanged="txtPros_TextChanged"></asp:TextBox>&nbsp;<asp:Button 
                ID="btnPros" runat="server" Text="Add" CssClass="btnsmall" 
                onclick="btnPros_Click" Width="52px"/>
            <asp:Label ID="lblExceptPros" runat="server" ForeColor="Maroon"></asp:Label>
            </center><br /></asp:Panel>
        
    </asp:Panel><br />
     <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                        SelectCommand="SELECT DISTINCT [CourseID] FROM [CivilSubMaster]">
                    </asp:SqlDataSource>
</div>
<br />
</asp:Content>

