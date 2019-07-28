<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ViewAutoCadForms.aspx.cs" Inherits="Admission_ViewAutoCadForms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">View Auto-Cad Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" />
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text=" View Auto-Cad Forms" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdPnlMain" runat="server"><ContentTemplate>
<br />
<center>
<asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" CssClass="txtbox" onselectedindexchanged="ddlSelect_SelectedIndexChanged">
<asp:ListItem>Membership</asp:ListItem><asp:ListItem Value="RegistrationNo">Registration No</asp:ListItem><asp:ListItem Value="BatchID">Batch ID</asp:ListItem>
</asp:DropDownList>&nbsp;<asp:Label ID="lblStatus" runat="Server" Visible="false" Text="Status:" />&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox">
<asp:ListItem>Registered</asp:ListItem>
<asp:ListItem Value="LateFee">Late Fee</asp:ListItem>
<asp:ListItem>Re-Registered</asp:ListItem>
<asp:ListItem>Completed</asp:ListItem>
</asp:DropDownList>&nbsp;:&nbsp;
<asp:TextBox ID="txtSID" runat="server" Font-Bold="true" CssClass="txtbox" Width="80px" />&nbsp;<asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" onclick="btnView_Click" />
<br /><br />
<asp:Label ID="lblException" ForeColor="Brown" runat="server" Font-Bold="true" />
</center>
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
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
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>View M-CAD Forms:</b><br />
<br />
<div id="Div1" style="display: block;">
<div  id="divAutoCad" style="width: 100%; overflow:scroll; height:240px;">
<asp:GridView ID="GridAutoCad" runat="server" BackColor="#DEBA84" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" 
        CellSpacing="2" Width="100%" 
        onselectedindexchanged="GridAutoCad_SelectedIndexChanged" 
        onrowdatabound="GridAutoCad_RowDataBound" >
        <Columns>
            <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
        </Columns>
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div></div>
<br />
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
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
<asp:Panel ID="pnlFees" runat="server" Visible="false">
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A2" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>View M-CAD Fees:</b><br />
<br />
<div id="Div2" style="display: block;">
<div  id="div3" style="width: 100%; overflow:scroll; height:240px;">
<asp:GridView ID="GridViewAutoCad" runat="server" BackColor="#DEBA84" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" 
        CellSpacing="2" Width="100%" 
        onrowdatabound="GridViewAutoCad_RowDataBound" >
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div></div>
</asp:Panel>
<br />
<asp:Panel ID="pnlspc" runat="server" Height="300px"/>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>