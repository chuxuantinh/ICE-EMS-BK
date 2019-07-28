<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="PlaceOrder.aspx.cs" Inherits="Invent_PlaceOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Place Order</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblText" Text="Place Order" runat="server" CssClass="redirecttabhome"></asp:Label> 
         </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>Purchase Order</h1></div>
    <asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
        <table width="90%" class="tbl">
            <tr>
                <td colspan="5">
                    Order To:
                    <asp:DropDownList ID="ddlSupplier" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" 
                        AutoPostBack="True" CssClass="txtbox" 
                        onselectedindexchanged="ddlSupplier_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                        SelectCommand="SELECT DISTINCT [Name] FROM [Supplier]"></asp:SqlDataSource>
                    <asp:LinkButton ID="lbtnSupplier" runat="server" onclick="lbtnSupplier_Click">Add New Supplier</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblException" runat="server" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
            <tr><td></td>
                <td colspan="4">
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnAdd" /></Triggers><ContentTemplate>
                    <asp:Panel ID="pnlSupplier" runat="server" Visible="False"  >
                    <table style="background:#FFCC99" class="tbl"><tr><td></td></tr><tr><td>Name:</td><td colspan="3"><asp:TextBox ID="txtName" runat="server" 
                            CssClass="txtbox"  Width="314px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Invent" 
                            ErrorMessage="Insert Name">*</asp:RequiredFieldValidator><br /></td>
                        <td>
                            &nbsp;</td>
                        </tr>
                    <tr><td>Address</td><td colspan="3">
                        <asp:TextBox ID="txtAddress1" runat="server" 
                           Width="314px" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtAddress1" Display="Dynamic" ValidationGroup="Invent" 
                            ErrorMessage="Insert Address">*</asp:RequiredFieldValidator></td></tr>
                        <tr><td>&nbsp;</td><td colspan="3">
                            <asp:TextBox ID="txtAddress2" runat="server" 
                                 Width="314px" CssClass="txtbox"></asp:TextBox></td>
                        </tr>
                        <tr><td></td><td>State<br /> 
                                 <asp:DropDownList ID="ddlState" runat="server" CssClass="txtbox" 
                                    AutoPostBack="True" onselectedindexchanged="ddlState_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                           
                            <td>City<br />
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="txtbox" >
                            </asp:DropDownList>
                            </td>
                            <td>
                                PinCode<br /> 
                                <asp:TextBox ID="txtPinCode" runat="server"
                                   CssClass="txtbox" Height="20px"></asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPinCode" Operator="LessThanEqual" Type="Double" ValidationGroup="Invent">*</asp:CompareValidator><dev:FilteredTextBoxExtender
                                       ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtPinCode">
                                   </dev:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr><td></td><td>PhoneNo<br /> <asp:TextBox ID="txtPhone" runat="server" 
                                CssClass="txtbox"  ></asp:TextBox> 
                            <dev:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtPhone">
                            </dev:FilteredTextBoxExtender>
                            </td>
                            <td>Fax<br />
                                    <asp:TextBox ID="txtFax" runat="server" CssClass="txtbox"></asp:TextBox>
                                <dev:FilteredTextBoxExtender ID="txtFax_FilteredTextBoxExtender" runat="server" 
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtFax">
                                </dev:FilteredTextBoxExtender>
                            </td>
                               <td>Mobile<br /><asp:TextBox ID="txtMobile" runat="server" 
                                CssClass="txtbox"  ></asp:TextBox><asp:RequiredFieldValidator runat="server" 
                                       id="RequiredFieldValidator8" controltovalidate="txtMobile" Display="Dynamic" 
                                       ValidationGroup="Invent" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" 
                                       ErrorMessage="Mobile No. can not be greater than 12 No." 
                                       ValueToCompare="999999999999" ControlToValidate="txtMobile" 
                                       Operator="LessThanEqual" Type="Double" ValidationGroup="Invent">*</asp:CompareValidator>
                                   <dev:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" 
                                       runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMobile">
                                   </dev:FilteredTextBoxExtender>
                            </td>
                        </tr>
                                <tr><td></td><td colspan="4">Email Id<br /><asp:TextBox ID="txtEmail" 
                                        runat="server" Width="272px" 
                                    CssClass="txtbox"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="txtEmail" ErrorMessage="*" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        ValidationGroup="Invent"></asp:RegularExpressionValidator>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btnsmall" 
                                        onclick="btnAdd_Click" Text="Add" ValidationGroup="Invent" />
                                    &nbsp;
                                    </td></tr><tr><td></td></tr></table>
                    </asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</td></tr></table>
<table class="tbl" width="100%">
            <tr>
                <td colspan="5">
                   <h3 style="color: #800000"> Order For Print</h3></td>
            </tr>
            <tr>
     <td align="left">Session:</td><td>
                <asp:DropDownList ID="ddlExamSeason" 
         runat="server" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" 
         AutoPostBack="true" CssClass="txtbox" Width="165px" 
           ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td align="left">
                    Year:</td><td><asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" AutoPostBack="true" 
                        OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label></td></tr>
            <tr><td align="left">Date:</td><td><asp:TextBox ID="txtDate" runat="server" 
                    CssClass="txtbox" Width="132px"></asp:TextBox> 
            <dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender>
               <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    controltovalidate="txtDate" Display="Dynamic" errormessage="Insert Date " 
                    ValidationGroup="A">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;</td>
                <td align="left">Order No: 
                   </td><td>
                       <asp:Label ID="lblOrderNo" runat="server" ForeColor="Maroon"></asp:Label></td></tr>
            <tr>
                <td align="left">
                    Order Type:</td>
                <td><asp:LinkButton ID="lbtnAddItems" runat="server" Text="Add New Item" OnClick="lbtnAddItems_Click"></asp:LinkButton><br />
                    <asp:DropDownList ID="ddlOType" runat="server" CssClass="txtbox" 
                        AutoPostBack="True" onselectedindexchanged="ddlOType_SelectedIndexChanged" 
                        Width="165px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td align="left">
                    Course Level:</td>
                <td>
                    <asp:DropDownList ID="ddlCourseId" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource3" DataTextField="CourseID" 
                        DataValueField="CourseID" 
                        onselectedindexchanged="ddlCourseId_SelectedIndexChanged" Width="100px" CssClass="txtbox" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                        SelectCommand="SELECT DISTINCT [CourseID] FROM [CivilSubMaster]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Course</td>
                <td>
                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlCourse_SelectedIndexChanged" Width="165px">
                        <asp:ListItem Value="Civil ">Civil Engineering</asp:ListItem>
                        <asp:ListItem Value="Architecture ">Architecture Engineering</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td align="left">
                    Part:</td>
                <td>
                    <asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" 
                        AutoPostBack="True" onselectedindexchanged="ddlPart_SelectedIndexChanged" 
                        Width="100px">
                        <asp:ListItem>PartI</asp:ListItem>
                        <asp:ListItem>PartII</asp:ListItem>
                        <asp:ListItem>SectionA</asp:ListItem>
                        <asp:ListItem>SectionB</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <center><asp:Panel ID="pnlAddItem" runat="server" CssClass="imbox">
        <h5>Add New Item</h5>
        <table><tr align="left"><td>
        Insert Item Name:</td><td><asp:TextBox ID="txtItemName" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
        <tr align="left"><td>Purches Price:</td><td><asp:TextBox ID="txtPurchesPrice" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;</td></tr>
        <tr align="left"><td>Sell Price:</td><td><asp:TextBox ID="txtSellPrice" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
        <tr><td><asp:Button ID="btnSaveItem" Text="Save" runat="server" CssClass="btnsmall" OnClick="btnSaveItem_Click" />&nbsp;&nbsp;&nbsp;&nbsp;</td><td><asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btnsmall" /></td></tr></table><br />
        </asp:Panel></center>
        <asp:Panel id="pnlProspectus" runat="server" BackColor="#FFF7E7"> <br /><center>
           Item Purches Price:&nbsp;&nbsp;<asp:TextBox ID="txtItemPrice" runat="server" CssClass="txtbox"></asp:TextBox><br />
            <asp:Label ID="lblPros" runat="server" Text="Enter Number Of Quantity"></asp:Label>&nbsp;<asp:TextBox ID="txtPros"
                runat="server" CssClass="txtbox" ontextchanged="txtPros_TextChanged"></asp:TextBox>&nbsp;<asp:Button 
                ID="btnPros" runat="server" Text="Add" CssClass="btnsmall" 
                onclick="btnPros_Click" Width="52px"/>
            <asp:Label ID="lblExceptPros" runat="server" ForeColor="Maroon"></asp:Label>
            </center><br /></asp:Panel>
        <asp:Panel ID="pnlGrid" runat="server">
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
    </script><br />
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>List Of Books</h1>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" /> <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px;"  >
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
            DataSourceID="SqlDataSource2"  GridLines="Horizontal"  PageSize="30"
            HorizontalAlign="Center" Width="100%" 
            onrowdatabound="GridView1_RowDataBound" >
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
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><center> Record Not Found !</center></EmptyDataTemplate>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView></div></div></div><center><asp:Button ID="btnAddList" CssClass="btnsmall" runat="server" Text="Add" OnClick="btnShowDetial_Click" /></center>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [SubjectCode], [SubjectName], [Price] FROM [SubjectMaster] WHERE (([CourseID] = @CourseID) AND ([Course] = @Course) AND ([Part] = @Part))">
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
    </asp:Panel><br />
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
</div><h1>Order List 
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;Total Quantity=
            <asp:Label ID="lblQuantity" runat="server"></asp:Label>&nbsp;&nbsp;
       TotalAmount=Rs. <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </h1>
            <div ID="Div2" style="display:block;">
                <input id="Hidden1" runat="server" type="hidden" value="0" />
                <div ID="div111" style="width: 100%; overflow:scroll; height:250px;">
                    <br />
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true" 
                        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="5" CellSpacing="5" Width="100%">
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
                    <center>
                        <asp:Button ID="btnSend" runat="server" CssClass="btnsmall" 
                            onclick="btnSend_Click" Text="Send" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btnsmall" 
                            onclick="btnCancel_Click" Text="Cancel" /></center>
               
    </asp:Panel>
</div>
<br />
</asp:Content>

