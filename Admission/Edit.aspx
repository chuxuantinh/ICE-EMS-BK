<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="User_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Edit Student Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<FORM> <table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" Visible="true" Text="Admission" CssClass="redirecttab"></asp:LinkButton> </td></tr></table></FORM></div>
<div id="rightpanel2">
            <div id="rightborder">

    
     <div class="fromRegisterlbl"><h1>this tst  test</h1></div>
    </div>
    <br /><br />
    <div class="fromRegisterlbl"><h1>this test</h1></div>
    <br /><br />
</div>
</asp:Content>


