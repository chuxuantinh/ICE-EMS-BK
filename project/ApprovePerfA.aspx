<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ApprovePerfA.aspx.cs" Inherits="project_ApprovePerfA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Proforma A Approval
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Proforma A Approval" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnlcomp" runat="server">
<ContentTemplate>
<div class="fromRegisterlbl"><h1>Proforma A Approval</h1></div>
<center>
<p>Project Status:&nbsp;<b>ProformaASubmitted</b> and Synopsis Status:&nbsp;<b>NotSubmitted</b></p>
Membership NO.:&nbsp;<asp:TextBox ID="txtSID" Width="100px" runat="server" CssClass="txtbox" ontextchanged="txtSID_TextChanged" AutoPostBack="True"/>
<br />
<asp:Label ID="lblExceptionOK" runat="server" Font-Bold="True" ForeColor="Brown" /></center><br />
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
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;">
IMID:<asp:TextBox runat="server" ID="txtIMID" CssClass="txtbox"/>
&nbsp;<asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" />&nbsp;<asp:Label runat="server" ID="lblNotIM" ForeColor="Red" />
<br />
<div id="Div1x" style="display: block;">
<div  id="divApprPerfA" style="width: 100%; overflow:scroll; height:180px;">
<asp:GridView ID="GridAppPerfmA" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" Width="100%" onrowdatabound="GridAppPerfmA_RowDataBound" onselectedindexchanged="GridAppPerfmA_SelectedIndexChanged">
        <Columns>
            <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
        </Columns>
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div><br />
<asp:Panel ID="pnlData" runat="server" Visible="false">
<center><asp:Label ID="lblSessionHiddend" runat="server" Visible="false" />
<table class="tbl">
<tr><td align="left">Student Name</td><td>:</td><td align="left" colspan="2"><asp:Label ID="lblName" runat="server" ForeColor="Brown" Font-Bold="true"/></td></tr><tr>
<td align="left">Option 1</td><td>:</td><td align="left" colspan="2"><asp:Label ID="lblOpn1" runat="server"/></td>
</tr><tr><td align="left">Option 2</td><td>:</td><td align="left" colspan="2"><asp:Label ID="lblOpn2" runat="server"/></td></tr><tr>
<td align="left">Option 3</td><td>:</td><td align="left" colspan="2"><asp:Label ID="lblOpn3" runat="server"/></td></tr>
<tr><td align="left">Final Option</td><td>:</td><td align="left" colspan="2"><asp:DropDownList ID="ddlFinalOpn" runat="server" CssClass="txtbox" Width="400px" ForeColor="#9900FF" Font-Bold="true"/></td></tr>
<tr><td align="left">Course Status:</td><td>:</td><td colspan="2"><asp:Label ID="lblCourSt" runat="server" ForeColor="Brown" Font-Bold="true"/></td>
</tr><tr><td align="left">Remarks</td><td>:</td><td align="left" colspan="2"><asp:TextBox ID="txtSynRemarks" runat="server" Height="50px" TextMode="MultiLine" Width="230px" CssClass="txtbox"/></td></tr>
<tr><td align="left">Project No</td><td>:</td><td align="left"><asp:TextBox ID="txtProjectNo" runat="server" CssClass="txtbox" Font-Bold="true" ReadOnly="true" /></td>
<td align="left"><asp:LinkButton ID="lbtnNewProjectNo" runat="server" OnClick="lbtnNewProjectNo_OnClick" Text="New Project No" /></td></tr>
</table></center><br />
<center><asp:Label ID="lblApprvExcep" runat="server" ForeColor="Brown" Font-Bold="true"/></center><br />
<center><asp:Button ID="btnSubmit" runat="server" CssClass="btnsmall" Text="Submit" onclick="btnSubmit_Click"/>
&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btnsmall" onclick="btnCancel_Click" Text="Cancel" />
<p>Update Project Status: &nbsp;<b>ProformaAApproved</b></p>
</center></asp:Panel>
<asp:Panel ID="pnlSpace" runat="server" Height="200px"/>
</div>
</ContentTemplate></asp:UpdatePanel>
</div><br />
</asp:Content>