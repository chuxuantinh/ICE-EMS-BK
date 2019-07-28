<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SessionDuration.aspx.cs" Inherits="Admin_SessionDuration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">System Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:Label ID="lblHomelink" runat="server" CssClass="redirecttabhome" Text="Session Duration"></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Course Session Duration</h1></div>

<asp:UpdatePanel ID="update" runat="server"><Triggers><asp:PostBackTrigger ControlID="Btnupdate"/>  </Triggers> <ContentTemplate>
<asp:Panel ID="pnnlSpace" runat="server" Height="200px">
<table style="margin-left:120px;margin-top:50px;"> <tr> <td>  <asp:Label ID="Lblcourse" runat="server" Text="Label">Course:</asp:Label></td><td><asp:DropDownList ID="ddlCourse" runat="server" 
        onselectedindexchanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox">
        <asp:ListItem>Civil</asp:ListItem>
        <asp:ListItem>Architecture</asp:ListItem>
    </asp:DropDownList> </td>
<td> <asp:Label ID="LblPart" runat="server" Text="Label">Part:</asp:Label></td><td><asp:DropDownList ID="ddlPart" runat="server" 
        onselectedindexchanged="ddlPart_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem>PartI</asp:ListItem>
        <asp:ListItem>PartII</asp:ListItem>
        <asp:ListItem>SectionA</asp:ListItem>
        <asp:ListItem>SectionB</asp:ListItem>
    </asp:DropDownList> </td></tr>
<tr> <td>
    <asp:Label ID="LblCoursduration" runat="server" Text="Label">Course Duration</asp:Label> </td><td><asp:Label ID="Lblsubjectname" runat="server" Text="LblSubjectName"></asp:Label> </td></tr>
    <tr> <td> </td></tr> <tr> <td> </td></tr>
    <tr><td> <asp:Label ID="Lbldetail" runat="server" ForeColor="Maroon" Font-Size="14px" >Update Duration</asp:Label></td> </tr>
    <tr> <td> </td></tr>
<tr> <td> <asp:Label ID="Lblupdate" runat="server" Text="New Value">Course Duration :</asp:Label></td><td><asp:TextBox ID="Txtupdate" runat="server" CssClass="txtbox"></asp:TextBox> </td></tr>
 </table>
  </asp:Panel>

  </ContentTemplate></asp:UpdatePanel>
  <br />
  <center>
    <asp:Label ID="Lblmsg" runat="server" Text="Label" ForeColor="Green"></asp:Label> </center>
  <center> <asp:Button ID="Btnupdate" runat="server" Text="Update" onclick="Btnupdate_Click"  CssClass="btnsmall"/> </center>
      &nbsp;&nbsp;&nbsp;&nbsp;
   

     <br />  <br />  <br />  <br /> 
    <br />  <br />  <br />  <br />  <br /> 
</div>
<br />
</asp:Content>

