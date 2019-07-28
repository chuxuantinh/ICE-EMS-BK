<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="StockView.aspx.cs" Inherits="Invent_StockView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Stock
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblText" Text="Stock View" runat="server" CssClass="redirecttabhome"></asp:Label>  </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>Stock View</h1></div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
<br /><table class="tbl" width="90%"><tr><td colspan="4">CourseID:<asp:DropDownList ID="ddlCourseId" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="CourseID" DataValueField="CourseID" onselectedindexchanged="ddlCourseId_SelectedIndexChanged" CssClass="txtbox"></asp:DropDownList>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT DISTINCT [CourseID] FROM [CivilSubMaster]"></asp:SqlDataSource>
</td><td>Course:<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" AutoPostBack="True" onselectedindexchanged="ddlCourse_SelectedIndexChanged" >
<asp:ListItem Value="Civil ">Civil Engineering</asp:ListItem>
<asp:ListItem Value="Architecture ">Architecture Engineering</asp:ListItem>
</asp:DropDownList></td><td>          
            Part :<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" AutoPostBack="True" onselectedindexchanged="ddlPart_SelectedIndexChanged">
<asp:ListItem>PartI</asp:ListItem>
<asp:ListItem>PartII</asp:ListItem>
<asp:ListItem>SectionA</asp:ListItem>
<asp:ListItem>SectionB</asp:ListItem>
</asp:DropDownList>
</td></tr></table>
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
</div><h1>Stock List &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;Total Quantity=
            <asp:Label ID="lblQuantity" runat="server"></asp:Label>
            </h1>
            <div ID="Div3" style="display:block;">
                <input id="scrollPos" runat="server" type="hidden" value="0" />
               <center><div id="div2" style="width: 100%; overflow:scroll; height:250px;"  >
       <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
            DataSourceID="SqlDataSource1"  GridLines="Horizontal" 
            HorizontalAlign="Center" Width="100%"
                    onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="SubjectCode" HeaderText="SubjectCode" 
                    SortExpression="SubjectCode" />
                <asp:BoundField DataField="SubjectName" HeaderText="SubjectName" 
                    SortExpression="SubjectName" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="Stoke" HeaderText="Stoke" SortExpression="Stoke" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView></center></div></div>
 <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [SubjectCode], [SubjectName], [Price], [Stoke] FROM [SubjectMaster] WHERE (([CourseID] = @CourseID) AND ([Course] = @Course) AND ([Part] = @Part))">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCourseId" Name="CourseID" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ddlCourse" Name="Course" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ddlPart" Name="Part" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource> </asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<br />   
</div>
<br />
</asp:Content>

