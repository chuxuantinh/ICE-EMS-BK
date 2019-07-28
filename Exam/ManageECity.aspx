<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ManageECity.aspx.cs" Inherits="Exam_ManageECity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Manage ExamCenter City
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content><asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Exam Center" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>
    Manage ExamCenter City</h1></div><br />
    <br /><br />
<asp:UpdatePanel ID="updatePanel1" runat="server" >
<ContentTemplate>
    <center>Exam Center List:&nbsp;&nbsp;<asp:DropDownList ID="ddlCity" runat="server" CssClass="txtbox" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedInexChanged"></asp:DropDownList><br /><br />
    City Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    Center Code:&nbsp;&nbsp;<asp:TextBox ID="txtCoentecode" runat="server" cssClass="txtbox"></asp:TextBox>
    <br /><br />
<asp:Button ID="btnAdd" runat="server" CssClass="btnsmall" Text="Add" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;
   <asp:Button ID="btnUpdate" runat="server" CssClass="btnsmall" Text="Update" OnClick="btnUpdate_OnClick" />&nbsp;&nbsp;&nbsp;
   <asp:Button ID="btnDelete" runat="server" CssClass="btnsmall" Text="Delete" OnClick="btnDelete_OnClick" />
    </center>
    </ContentTemplate> </asp:UpdatePanel>  
    <br />
    <center>Exam Center Code must be tow degit. Ex. CenterCode "1" sholuld be "01".</center>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />    <br />    <br />    <br />    <br />    <br />    <br />    <br />    <br />    <br />    <br />    <br />
    <br />    <br />    <br />    <br />    <br />
    </div>
    
</asp:Content>

