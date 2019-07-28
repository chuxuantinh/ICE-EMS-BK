<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="EditCourierDispatch.aspx.cs" Inherits="FO_EditCourierDispatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Update Courier and Consignment No
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 86px;
        }
        .style4
    {
        width: 89px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
        
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td><td><asp:Label ID="lblEditCourieDispatch" runat="server" Text="Update Courier and Consignment No." CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1>Update Courier and Consignment No.</h1></div><br />
                            <center>Search By:&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlsearch" CssClass="txtbox" OnSelectedIndexChanged="ddlSearch_OnSelectedIndexChanged" runat="server"   Width="200" AutoPostBack="True">
                                     <asp:ListItem Value="IMID" Text="IMID"></asp:ListItem>
                                     <asp:ListItem Value="CourierService" Text="Courier Service"></asp:ListItem>
                                     <asp:ListItem Value="RefrenceNo" Text="Refrence No"></asp:ListItem>
                                     <asp:ListItem Value="ConsignmentNo" Text="Consignment No" />
                                </asp:DropDownList></center><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
                                <br />  
                         <div style="float:right; margin-right:30px;"><asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" Height="30px" Width="80px" Font-Size="15px"  Font-Bold="true" OnClick="btnView_OnClick"/></div>
                           <asp:Panel ID="pnlsession" runat="server" ><center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>
                               &nbsp;Year:&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="70px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox>
                           &nbsp;&nbsp;&nbsp;<asp:Label ID="lblName" runat="server" ></asp:Label>:&nbsp;&nbsp;<asp:TextBox ID="txtName" runat="server" Width="100px" CssClass="txtbox"></asp:TextBox>
                               <asp:DropDownList ID="ddlCourier" runat="server" CssClass="txtbox" Width="150px"
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
    <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblGridTitle" runat="server" ></asp:Label>
</div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px" >
<asp:GridView ID="GridCourier" runat="server" PageSize="30"   
                                AllowPaging="True" 
                         onpageindexchanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_OnRowDataBound"
                            Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                    BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" 
                         onselectedindexchanged="GridCourier_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
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


                 <asp:Label ID="lblException" runat="server" ForeColor="#006600" ></asp:Label>


   <br /><br />
                 <asp:Panel ID="Panel1" runat="server" Height="300px">
                 </asp:Panel>
                 <asp:Panel ID="pnlEdit" Visible="false" runat="server">
                 
   <table class="tbl"><tr><td>Select Session:</td><td><asp:DropDownList ID="DropDownList1" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox1" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
                          <tr><td>Send To:</td><td>
                              <asp:TextBox ID="txtRecivefrom" CssClass="txtbox" runat="server"></asp:TextBox></td></tr>
                       <tr><td><asp:Label ID="lblFromName" runat="server" ></asp:Label></td><td><asp:TextBox ID="txtSName" AutoPostBack="true" OnTextChanged="txtSName_TExtChnaged" runat="server" CssClass="txtbox" Width="200px" Font-Bold="true"></asp:TextBox></td></tr>
                         </table><center><asp:Label ID="lblExceptiontbl" runat="server" ></asp:Label></center>
                         <table id="tbllabel" class="tbl" runat="server"><tr><td><asp:Label ID="Label1" runat="server" ></asp:Label></td><td><asp:Label ID="lblCode" runat="server" ></asp:Label></td><td><asp:Label ID="lblCourseAddress" runat="server" ></asp:Label></td></tr></table>
                 <asp:Panel ID="pnlOther" Visible="false" runat="server">
                 
                 <table class="tbl" runat="server"><tr><td class="style4" > Address:</td><td colspan="3"><asp:TextBox ID="txtAddress1" 
       runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td class="style4" ></td><td colspan="3"><asp:TextBox ID="txtAddress2" 
       runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td class="style4" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td><td>City:<br /><asp:TextBox ID="txtCity" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCity" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage=" Insert City Name">*</asp:RequiredFieldValidator></td>
    <td >State:<br /><asp:TextBox ID="txtState" runat="server" CssClass="txtbox" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtState" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert State Name">*</asp:RequiredFieldValidator></td><td>PinCode:<br /><asp:TextBox ID="txtPincode" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td></tr>
    <tr><td class="style4" >Phone:</td><td colspan="3"><asp:TextBox ID="txtPhoneNo" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox></td></tr>
    
</table></asp:Panel>
                        
                         <table class="tbl">
                           
                          <tr><td>Date:</td><td><asp:TextBox ID="txtDiaryDate" Width="150px" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtDiaryDate" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
                              <asp:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender22" PopupButtonID="Im11" TargetControlID="txtDiaryDate" PopupPosition="BottomRight" runat="server">
                              </asp:CalendarExtender>
                              <img src="../images/cal.png" id="Im11" runat="server"  alt="Cal" /></td></tr>
                          <tr><td class="style1">
   Courier Service :<br /> </td><td>
       <asp:TextBox ID="txtCourierService" CssClass="txtbox" runat="server"></asp:TextBox>
        &nbsp;</td><td>Reference No:&nbsp;<asp:Label ID="lblCourierNo" runat="server"></asp:Label></td></tr>
                           <tr><td>Courier Type:</td><td><asp:TextBox ID="txtDiraryType" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox></td><td>Consignment No:&nbsp;<asp:TextBox runat="server" ID="txtConsignmentNo" CssClass="txtbox" Width="150px"></asp:TextBox></td></tr>
                          <tr><td>Weight:</td><td><asp:TextBox ID="txtWt" runat="server" CssClass="txtbox" Width="150px"></asp:TextBox>&nbsp;&nbsp;Kg.</td><td>Amount:&nbsp;&nbsp;<asp:TextBox ID="txtAmt" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;&nbsp;Rs.</td></tr>
                           <tr><td></td><td></td><td class="style1" align="center">
                               <asp:Button ID="Save" runat="server" Text="Save" onclick="Save_Click" 
                                    CssClass="btnsmall" /></td></tr></table><br />
                               </asp:Panel>

                
             </div></div>
</asp:Content>

