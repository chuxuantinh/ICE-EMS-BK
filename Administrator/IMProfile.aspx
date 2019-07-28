<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="IMProfile.aspx.cs" Inherits="Administrator_IMProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Institutional Member Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    

    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="float:right; margin-right:50px;">Insert IM ID:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnViewEnroll" runat="server" Text="View Profile"  OnClick="btnView_Click"  CssClass="btnsmall"/></div>
<br />
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>

<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Profile</h1></div>

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
<div class="togalfees" style="width:99%;">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:3px;"><h1>IM Profile</h1></div>
<div id="Div1x" style="display:none;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px" 
            onscroll='javascript:setScroll(this, <% =scrollPos.ClientID %> );'>
<asp:GridView ID="GridDuplicacy" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="False"
        CellPadding="8" CellSpacing="8" 
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%" AllowPaging="True" 
                         DataSourceID="SqlDataSource1" AllowSorting="True" 
                         onselectedindexchanged="GridDuplicacy_SelectedIndexChanged">
        <EmptyDataTemplate><center> Duplicate Record Not found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="ID" HeaderText="IM ID" SortExpression="ID" />
            <asp:BoundField DataField="PCity" HeaderText="City" SortExpression="PCity" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" 
                SortExpression="Mobile" />
            <asp:BoundField DataField="RegDate" HeaderText="Reg. Date" 
                SortExpression="RegDate" />
            <asp:BoundField DataField="Active" HeaderText="Status" 
                SortExpression="Active" />
            <asp:BoundField DataField="DisActiveDate" HeaderText="Subscription Date" 
                SortExpression="DisActiveDate" />
            <asp:BoundField DataField="GID" HeaderText="Group ID" SortExpression="GID" />
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                         SelectCommand="SELECT [Name], [ID], [PCity], [Phone], [Email], [Mobile], [RegDate], [Active], [DisActiveDate], [GID] FROM [IM] ORDER BY [ID]">
                     </asp:SqlDataSource>
   </div>
   </div></div>
<div id="Div2" class="fromRegisterlbl" runat="server" ><h1>Profile:</h1></div>
<table  class="tbl" width="80%">
 <tr><td>Name of Institute:&nbsp;&nbsp;<asp:Label ID="txtName" runat="server"   Font-Bold="true" ></asp:Label></td><td>Group ID:&nbsp;&nbsp;<asp:Label ID="lblGID" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
 </table>
    <table class="tbl">
 
<tr><td>Permanent Address:</td><td colspan="3"><asp:Label ID="txtPAddress" runat="server"  Width="60%" Font-Bold="true"></asp:Label></td></tr>
<tr><td></td><td colspan="3"><asp:Label ID="txtAddress2" runat="server"  Width="60%" Font-Bold="true"></asp:Label></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPCity" runat="server"  Font-Bold="true"></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPState" runat="server" Font-Bold="true" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPPincode" runat="server" Font-Bold="true" ></asp:Label></td></tr>


<tr><td>Correspondence Address:</td><td><asp:Label ID="txtCAddress"  Font-Bold="true"  runat="server"></asp:Label></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCCity" Font-Bold="true" runat="server" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCState" runat="server"  Font-Bold="true" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCPin" runat="server"  Font-Bold="true"></asp:Label>
    </td></tr>


<tr><td></td><td colspan="3">Phone:&nbsp;&nbsp;<asp:Label ID="txtPhonecode" runat="server"  Width="50px"  Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
    
    </td></tr>

<tr><td></td><td colspan="3">Fax:&nbsp;&nbsp;<asp:Label ID="txtFaxCode" runat="server"  Font-Bold="true" Width="50px"></asp:Label>&nbsp;&nbsp;&nbsp;
    
    </td></tr>


<tr><td></td><td>Mobile:&nbsp;&nbsp;<asp:Label ID="txtMobile" runat="server"  Font-Bold="true"></asp:Label>

    </td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp; <asp:Label ID="txtEmail" runat="server" Font-Bold="true" ></asp:Label></td></tr>
     </table><br />
     <div id="Div1" class="fromRegisterlbl" runat="server" ><h1>Location of Center:</h1></div>
     <table class="tbl" width="90%"><tr><td>
       <asp:Label ID="lblRemoteArea" runat="server" Font-Bold="true" ></asp:Label>
       </td><td><asp:Label ID="lblEassayAccess" runat="server"  Font-Bold="true"></asp:Label></td></tr><tr><td><asp:Label ID="lblResidentialArea" Font-Bold="true" runat="server" ></asp:Label></td><td><asp:Label ID="lblCommArea" runat="server"  Font-Bold="true"></asp:Label></td></tr>
       <tr><td><asp:Label ID="lblWithCity" runat="server"  Font-Bold="true"></asp:Label></td><td><asp:Label ID="lblOutCity" runat="server"  Font-Bold="true"></asp:Label></td></tr></table><br />
      <table class="tbl" width="90%"><tr><td>Distance from Railway Stn.</td><td><asp:Label ID="txtDRStn" runat="server" Width="95px"  Font-Bold="true" ></asp:Label>Kms</td><td>Name of the city</td><td><asp:Label ID="txtNCity" runat="server"  Width="119px"  Font-Bold="true"></asp:Label></td></tr><tr><td>Distance from bus stop</td><td ><asp:Label ID="txtBStop" runat="server" Width="93px"   Font-Bold="true" ></asp:Label>Kms</td><td >Name of the area</td><td ><asp:Label ID="txtNArea" runat="server"  Width="119px"  Font-Bold="true" ></asp:Label></td></tr><tr><td >Year of Establishment</td><td><asp:Label ID="txtYEstablishment" runat="server" Width="106px"  Font-Bold="true" ></asp:Label></td></tr><tr><td style="width: 151px">Acedemic Status of Institution</td><td> <asp:Label ID="txtASInstitution" runat="server" Width="107px"   Font-Bold="true" ></asp:Label></td></tr><tr><td>Type of Institution govt.</td><td >
      <asp:Label ID="txttypeig" runat="server" ></asp:Label>
      </td></tr><tr><td>Courses being conducted presently<br />are recognized by </td><td><asp:Label ID="txtCRecognizedby" runat="server"  Font-Bold="true" ></asp:Label></td></tr><tr><td style="height: 40px; width: 288px;">No. of Students at present</td><td>
          <asp:Label ID="txtNSPresent" runat="server"  Font-Bold="true" ></asp:Label></td></tr></table>
          <br />
          <hr />
</div>
</asp:Content>

