<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="VisitorView.aspx.cs" Inherits="VisitorView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Visitors View Detail ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
        
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td><td><asp:Label ID="lblVisitorsView" runat="server" Text="Visitors View Details" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
            <asp:UpdatePanel ID="updatepanel" runat="server" ><ContentTemplate>
             <div class="fromRegisterlbl"><h1>Visitors View Details:</h1>&nbsp;&nbsp; <br />
                            <center><strong>Select Type:</strong>&nbsp;
                                <asp:DropDownList ID="ddlType" runat="server"  CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"  >
                                <asp:ListItem Value="Date" Text="Date" />
                                   <asp:ListItem Value="Name" Text="Name" />
                                </asp:DropDownList></div>
                           <br />
                            <center>
                           <div id="dates" runat="server" >
                                             <asp:TextBox Width="100px" ID="dropp" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="dropp" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="dropp"></dev:CalendarExtender><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
                                         
                             &nbsp;TO
                                                                                     <asp:TextBox Width="100px" ID="drop2" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="drop2" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="cald" PopupPosition="BottomRight" runat="server" TargetControlID="drop2"></dev:CalendarExtender><img src="../images/cal.png" id="cald" runat="server"  alt="Cald" />
                                     </div>&nbsp;&nbsp;<asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
                      <asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox>
                       <asp:Label ID="lblName" runat="server"></asp:Label>  &nbsp;&nbsp;<asp:TextBox ID="txtDiaryNo" runat="server" Width="100px" CssClass="txtbox" ></asp:TextBox>
    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btnsmall" onclick="btnShow_Click" /></center>
                            <br />
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
</div><div align="left" style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblGridTitle" runat="server" ></asp:Label>
</div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px" 
            onscroll='javascript:setScroll(this, <% =scrollPos.ClientID %> );'>
<asp:GridView ID="GridView1" runat="server" PageSize="30"   
                                AllowPaging="True" 
                         onpageindexchanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_OnRowDataBound"
                            Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                    BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" 
                         onselectedindexchanged="GridView1_SelectedIndexChanged">
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
  </ContentTemplate></asp:UpdatePanel>      
             </div></div>
         </asp:Content>