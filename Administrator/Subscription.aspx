<%@ Page Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Subscription.aspx.cs" Inherits="Administrator_Subscription" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"> View Subscription Details</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .binaryImage img
        {
            border: 1px solid;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" ></asp:ScriptManager><asp:Label ID="lblsession" runat="server" Visible="false"> </asp:Label>
      <div style=" float:right; margin:10px; margin-top:0px; width:49%;">
    <%--  <asp:UpdatePanel ID="updatepanleIM" runat="server" ><ContentTemplate>--%>
      <asp:Panel ID="panelSubscribe" runat="server" >
       <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Subscribe Membership :-</font></legend>
      <br />
                <center>Subscribe for Session:&nbsp;&nbsp;<asp:Label ID="lblSubscriptionFrom" runat="server"></asp:Label>&nbsp; To:&nbsp;<asp:Label ID="lblSubscriptionTo" runat="server"></asp:Label>
               <br />  Current Balance (Rs):&nbsp;<asp:Label ID="lblBalance" runat="server" Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="lblBalanceType" runat="server" ></asp:Label>
                 </center>  
                 <asp:Panel ID="panelSubspendAcc" runat="server" >
 <center><h2 style="color:Maroon; font-size:12px; font-family:Times New Roman;" ><b><asp:Label runat="server" ID="lblSubscribtion" ></asp:Label></b></h2></center>
 <br />
 <center><asp:Button ID="btnSubscribe" Text="Subscribe" runat="server" 
         OnClientClick="return confirm('Are you sure Subscribe Membership ?');" 
         OnClick="btnSubscribe_Onclick" CssClass="bigbutton" Width="160px" /></center>
 <br />Subscription Charges:&nbsp;&nbsp;<b><asp:Label ID="lblSubFee" runat="server" ></asp:Label> &nbsp;Rs.</b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sub. Date:&nbsp;<asp:TextBox ID="txtSubDate" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtSubDate"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
 <hr />
   <table>
                            </table><center>This Membership Subscription up to: &nbsp;&nbsp;<asp:Label ForeColor="Blue" ID="lblfrom" runat="server"></asp:Label>&nbsp;&nbsp;TO&nbsp;&nbsp;<asp:Label ForeColor="Blue" ID="lblTo" runat="server"></asp:Label></center>
                            <hr />
                            <asp:Label ID="lblActive" runat="server" Visible="false"></asp:Label><br />
     <input id="scrollPos2" runat="server" type="hidden" value="0" />
  
<div id="divdatagrid2" style="width:420px; overflow:scroll; height:150px" >
            
     <asp:GridView ID="GridBalance" runat="server" AllowPaging="True" 
          AutoGenerateColumns="true" PageSize="2" OnPageIndexChanging="GridBalance_PageIndexChanging"
         BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
         CellPadding="2"  ForeColor="Black"
         GridLines="None"  OnRowDataBound="GridBalance_RowDataBound">
         <EmptyDataTemplate>
         No Data Found
         </EmptyDataTemplate>
         <Columns>
             
         </Columns>
         <FooterStyle BackColor="Tan" />
         <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
             HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
         <HeaderStyle BackColor="Tan" Font-Bold="True" />
         <AlternatingRowStyle BackColor="PaleGoldenrod" />
     </asp:GridView>
      </div>    
       </asp:Panel> 
       </fieldset></asp:Panel>
      <%-- </ContentTemplate></asp:UpdatePanel>--%>
       </div>
      <div style="width:49%; margin:10px; margin-top:0px;">
       <asp:Panel ID="panelProfile" runat="server" >
              
               <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Personal Information:-</font></legend>
              
              
               <table><tr><td>Member Type:</td><td><asp:Label ID="lblMemberTyep" runat="server"></asp:Label></td></tr><tr><td>ID:</td><td><asp:Label ID="lblID" runat="server"></asp:Label></td></tr>
               <tr><td>Name:</td><td align="left"><asp:Label ID="lblName" runat="server"></asp:Label></td><td></td></tr>
               <tr><td>Address:</td><td><asp:Label ID="lblAddress" runat="server" ></asp:Label></td></tr>
               <tr><td></td><td><asp:Label ID="lbladdress2" runat="server" ></asp:Label></td></tr>
               <tr><td>City:</td><td><asp:Label ID="lblCity" runat="server"></asp:Label></td></tr><tr><td>Email:</td><td><asp:Label ID="lblEmail" runat="server" ></asp:Label></td></tr>
               
               <tr><td>Mobile:</td><td><asp:Label ID="lblMobile" runat="server"></asp:Label></td></tr><tr><td>Phone:</td><td><asp:Label ID="lblPhonne" runat="server" ></asp:Label></td></tr>
               <tr><td>Registration Date:</td><td><asp:Label ID="lblEnrollDate" runat="server" ></asp:Label></td>
                         </tr><tr>   <td>Renwal Date:</td><td><asp:Label ID="lblRenuwalDate" runat="server" ></asp:Label></td></tr>
                            <tr><td> Expire Date:</td><td><asp:Label ID="lblExpDate" runat="server" ></asp:Label></td></tr>
                         
               </table>
              <br />
               
               </fieldset>
                      
                            
      </asp:Panel></div>
       <div style="margin-left:10px;">
       
       <asp:Panel ID="panelChangeStatus" runat="server" >
       <fieldset><legend><font style="color:#B21235; margin-left:10px; font-size:18px; font-family:Verdana;">Change Membership Status:-</font></legend>
     
 <center><h2 style="color:Maroon; font-size:18px; font-family:Times New Roman;" >Status: <b><asp:Label runat="server" ID="lblStatus2" ></asp:Label></b></h2></center>
 <br />
 <center><asp:Label ID="lblExceptionActive" runat="server" Font-Bold="true" ></asp:Label><br /><asp:Button ID="btnchangeStatus" runat="server" OnClick="btnChnageStatsu" CssClass="bigbutton" /></center>
      </fieldset></asp:Panel></div>
       <br /><br />
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
           <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
     <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">Select:&nbsp;&nbsp;<asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox" Width="120px"><asp:ListItem Text="Institutional Member" Value="IM" />
      <asp:ListItem Value="Fellow" Text="Fellow Member" /><asp:ListItem Value="Member" Text="Member" /><asp:ListItem Value="Honorary" Text="Honorary Member" /></asp:DropDownList>
&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="120px" CssClass="txtbox"><asp:ListItem Value="Subscription" Text="Subscription" ></asp:ListItem><asp:ListItem Value="Expired" Text="Expired"></asp:ListItem><asp:ListItem Value="Disactive" Text="Disactive" /></asp:DropDownList>
   &nbsp;&nbsp;<asp:Button ID="btnViewGrid" runat="server" Text="View" OnClick="btnViewGrid_Onclick"  CssClass="btnsmall"/>
     &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
    Membership ID:&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" AutoPostBack="true" OnTextChanged="txtIMID_TextChaned" Width="100px" CssClass="txtbox"></asp:TextBox><br />
   </div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:350px;" >
<asp:GridView ID="GridDuplicacy" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" OnSelectedIndexChanged="Grid_OnselectedIndexChanged"
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%"  OnRowDataBound="GridDuplicacy_RowDataBound"
                         EmptyDataText="N/A" AllowPaging="True"  ShowHeaderWhenEmpty="true">
                         <EmptyDataRowStyle  width="100%"  HorizontalAlign="Center" /> 
        <EmptyDataTemplate><center> Membership Record Not found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <Columns>
        <asp:ButtonField CommandName="Select" HeaderText="View Status" Text="select" />
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" 
            HorizontalAlign="Center" />
        <EditRowStyle HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
   </div>
   </div></div>
 
</asp:Content>

