<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamDefault.aspx.cs" Inherits="ExamDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server" >Examination Department
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server"  Text="Home" CssClass="redirecttab" 
        onclick="lblHomeRedirect_Click"></asp:LinkButton></td><td>
        <asp:Label ID="lbtnNext1Redirect" runat="server" Text="Examination Department" CssClass="redirecttabhome" 
             ></asp:Label> </td></tr></table></div>
             <div id="rightpanel2"><div id="header">
             <div class="fromRegisterlbl"><h1>Examination</h1></div>
             <img src="../images/exam.jpg" alt="Examination" width="100%" />
            <div style="height:200px;"></div>
             </div></div>
</asp:Content>

