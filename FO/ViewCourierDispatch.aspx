<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="ViewCourierDispatch.aspx.cs" Inherits="FO_ViewCourierDispatch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Courier Dispatch Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td><td><asp:Label ID="lblViewCourierDispatch" runat="server" Text="Courier Dispatch" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1>Courier Dispatch Details</h1></div><br />
                            <center>Search By:&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlsearch" CssClass="txtbox" OnSelectedIndexChanged="ddlSearch_OnSelectedIndexChanged" runat="server"   Width="200" AutoPostBack="True">
                                     <asp:ListItem Value="IMID" Text="IMID"></asp:ListItem>
                                     <asp:ListItem Value="CourierService" Text="Courier Service"></asp:ListItem>
                                     <asp:ListItem Value="RefrenceNo" Text="Refrence No"></asp:ListItem>
                                     <asp:ListItem Value="ConsignmentNo" Text="Consignment No" />
                                </asp:DropDownList></center>
                                <asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
                                <br />
                         <div style="float:right; margin-right:30px;"><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" Height="30px" Width="80px" Font-Size="15px"  Font-Bold="true" OnClick="btnView_OnClick"/></div>
                           <asp:Panel ID="pnlsession" runat="server" ><center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>
                               &nbsp;Year:&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="70px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox>
                               <br />
                               <br />
                               &nbsp;<asp:Label ID="lblName" runat="server" ></asp:Label>:&nbsp;<asp:TextBox ID="txtName" runat="server" Width="100px" CssClass="txtbox"></asp:TextBox>
                               <asp:DropDownList ID="ddlCourier" runat="server" CssClass="txtbox" Width="170px"
                                   DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                               </asp:DropDownList>
                               <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                   ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                                   SelectCommand="SELECT DISTINCT [Name] FROM [ServiceNameMaster] WHERE ([Type] = @Type) ORDER BY [Name]">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="Courier" Name="Type" Type="String" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </center></asp:Panel>
                           <asp:Panel ID="pnlNo" runat="server" ><center><asp:Label ID="lblNo" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNo" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox></center></asp:Panel>
        <br /><br />
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
    <div  class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblGridTitle" runat="server" ></asp:Label>
</div>
<div id="Div1x"  style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px" 
            onscroll='javascript:setScroll(this, <% =scrollPos.ClientID %> );'>
            
<asp:GridView  ID="GridCourier"  runat="server" PageSize="30"   
                                AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_OnRowDataBound"
                            Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                    BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                                                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                                            <FooterStyle BackColor="Tan" />
                                                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                                                HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                            <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                                            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                                            <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                                            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                                            </asp:GridView>
                     
   </div>
   </div></div>
             </div></div>
</asp:Content>

