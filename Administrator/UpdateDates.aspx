<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="UpdateDates.aspx.cs" Inherits="Administrator_UpdateDates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Update IM Registration Date
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="float:right; margin-right:50px;">Insert IM ID:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="btnViewEnroll" runat="server" Text="View Profile" 
        onclick="btnViewEnroll_Click"  CssClass="btnsmall"  /></div>
<div id="redirect" runat="server">	
<table class="tbl"><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" onclick="lbtnNext1Redirect_Click" 
            ></asp:LinkButton> </td></tr></table></div><br /> 
            <div id="rightpanel2">
            <div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Update Registration Date</h1></div><br />
<div style="margin-left:200px;margin-top:50px;">
 <table  class="tbl">
 <tr><td>Session:</td><td><asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox"><asp:ListItem Value="Sum" Text="Summer Examination"></asp:ListItem><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList></td><td><asp:TextBox ID="txtYear" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
   <tr> <td><asp:Label ID="lblregi" runat="server" Text="Registration Date :"></asp:Label> </td><td>
       <asp:TextBox ID="txtregi" runat="server" CssClass="txtbox"></asp:TextBox> 
       </td></tr> 
   <tr> <td><asp:Label ID="Lblrenew" runat="server" Text="Subscription Date"></asp:Label> &nbsp;:</td><td> 
       <asp:TextBox ID="Txtrenew" runat="server" CssClass="txtbox"></asp:TextBox> </td></tr>
    <tr> <td><asp:Label ID="Lblexpiry" runat="server" Text="Expiry Date"></asp:Label> &nbsp;:</td><td>
        <asp:TextBox ID="Txtexpiry" runat="server" CssClass="txtbox"></asp:TextBox> </td></tr>
    </table></div><br /> 
              <center><asp:Label ID="Lblmesg" runat="server"></asp:Label><br /><asp:Button ID="btnupdate" runat="server" Text="Update" 
                      onclick="btnupdate_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;  </center>
                      <br />  <br /> <br />  <br /> <br />  <br /> <br />
      <br /> <br />  <br /> <br />  <br /> <br /></div>
</asp:Content>

