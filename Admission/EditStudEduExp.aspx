<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="EditStudEduExp.aspx.cs" Inherits="Admission_EditStudEduExp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Add & Edit Admission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" />
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" CssClass="redirecttab" onclick="lblHomeRedirect_Click" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Add &amp; Edit Student Credentials" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1>Add &amp; Edit Student Education Credential &amp; Experience&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdbtnEdu" runat="server" Text="Education Details" GroupName="gpEdit" Checked="true" oncheckedchanged="rdbtnEdu_CheckedChanged" AutoPostBack="true" />&nbsp;<asp:RadioButton ID="rdbtnExp" GroupName="gpEdit" runat="server" Text="Experience & Sponsorship" oncheckedchanged="rdbtnExp_CheckedChanged" AutoPostBack="true" /></h1></div>
<br /><center>
<table class="tbl"><tr><td>Membership No:</td>
<td><asp:TextBox ID="txtSID" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" ontextchanged="txtSID_TextChanged" Width="90px" /></td>
<td>Name:</td><td><asp:Label ID="lblName" runat="server" Font-Bold="True"/></td>
<td>Course:</td><td><asp:Label ID="lblCourse" runat="server" Font-Bold="True"/></td>
<td>Part:</td><td><asp:Label ID="lblPart" runat="server" Font-Bold="True" /></td>
<td>Session:</td><td><asp:Label ID="lblSession" runat="server" Font-Bold="True" /></td></tr>
</table></center>
<hr />
<center><asp:Label ID="lblException" runat="server" Font-Bold="true" ForeColor="Brown" /></center>
<asp:Panel ID="pnlEdu" runat="server" Visible="false">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"><asp:CheckBox ID="chkDoc" runat="server" Text="Do you have Submitted Original Document?" Font-Bold="true" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" /></h1><h1>Student Education Details</h1></div>
<center>
<table class="tbl">
<tr>
<td>Type:</td>
<td><asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox" Width="82px">
<asp:ListItem>10th</asp:ListItem>
<asp:ListItem>12th</asp:ListItem>
<asp:ListItem>ITI</asp:ListItem>
<asp:ListItem>Poly. Dip.</asp:ListItem>
<asp:ListItem>Degree</asp:ListItem>
<asp:ListItem>Exp. Cert.</asp:ListItem>
<asp:ListItem>Others</asp:ListItem>
</asp:DropDownList></td>
<td>Board:</td><td><asp:DropDownList ID="ddlBoard" runat="server" CssClass="txtbox" /></td></tr>
<tr><td>Score:</td><td><asp:TextBox ID="txtScore" runat="server" CssClass="txtbox" Width="60px" MaxLength="3" /><asp:FilteredTextBoxExtender ID="FiltertxtScore" runat="server" TargetControlID="txtScore" FilterType="Numbers" /></td>
<td>Year:</td><td><asp:TextBox ID="txtYear" runat="server" CssClass="txtbox" Width="60px" MaxLength="4" /><asp:FilteredTextBoxExtender ID="FiltertxtYear" runat="server" TargetControlID="txtYear" FilterType="Numbers" /></td></tr>
<tr><td colspan="2"><asp:CheckBox ID="chkOMark" runat="server" Text="Original Marksheet" /></td>
<td colspan="2"><asp:CheckBox ID="chkOrCert" runat="server" Text="Original Certificate" /></td></tr>
<tr><td colspan="2"><asp:CheckBox ID="chkAtMark" runat="server" Text="Attested Marksheet" /></td>
<td colspan="2"><asp:CheckBox ID="chkAtCert" runat="server" Text="Attested Certificate" /></td></tr>
<tr><td align="right" colspan="2"><asp:Button ID="btnAdd" runat="server" Text="Add Details" CssClass="btnsmall" onclick="btnAdd_Click" /></td>
<td><asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnsmall" onclick="btnUpdate_Click" /></td>
<td align="left"><asp:Button ID="btnDel" runat="server" Text="Delete" CssClass="btnsmall" onclick="btnDel_Click" /></td></tr>
</table>
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
</div><div style="padding:5px; color:White; font-size:15px;"><b>Education Credentials:</b><br />
<br />
<div id="Div1" style="display: block;">
<div  id="divEditStudent" style="width: 100%; overflow:scroll; height:300px;">
<asp:GridView ID="GridEditEdu" runat="server" BackColor="#DEBA84" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" 
        CellSpacing="2" Width="100%" 
        onselectedindexchanged="GridEditEdu_SelectedIndexChanged">
        <Columns>
            <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
        </Columns>
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div>
</asp:Panel>
<asp:Panel ID="pnlSponsor" runat="server" Visible="false" >
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"><asp:CheckBox ID="chkExp" runat="server" Font-Bold="true" Text="Do you have Experience?" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrol" runat="server" /></h1><h1>Student Experience & Sponsorship</h1></div>
<br />
<center><i style="color:Gray;">Certificate of sponsorship to be obtained from <b>Corporate Member of ICE(I)/ Principal/ H.O.D.</b> (Civil/Architectural Engineering) of an Engineering College.</i>
<br /><br />
<table class="tbl">
<tr><td>Type:</td><td><asp:DropDownList ID="ddlSponExpType" runat="server" CssClass="txtbox" Width="110px" AutoPostBack="True" onselectedindexchanged="ddlSponExpType_SelectedIndexChanged">
<asp:ListItem>Experience</asp:ListItem><asp:ListItem>Sponsorship</asp:ListItem></asp:DropDownList></td>
<td>Organization:</td><td><asp:TextBox ID="txtOrg" runat="server" Width="170px" CssClass="txtbox" /></td></tr>
<tr><td>Year:</td><td><asp:TextBox ID="txtYearExp" runat="server" CssClass="txtbox" Width="70px" MaxLength="4"/><asp:FilteredTextBoxExtender ID="FiltertxtYearExp" runat="server" TargetControlID="txtYearExp" FilterType="Numbers" /></td>
<td>Designation:</td><td><asp:TextBox ID="txtDesig" runat="server" CssClass="txtbox" Width="170px" /></td></tr>
<tr><td>Location:</td><td colspan="3"><asp:TextBox ID="txtLocation" runat="server" Width="270px" CssClass="txtbox" /></td></tr>
<tr><td colspan="2" align="right"><asp:Button ID="btnAddExp" runat="server" Text="Add Details" CssClass="btnsmall" onclick="btnAddExp_Click" /></td>
<td align="left"><asp:Button ID="btnUpdateDetails" runat="server" Text="Update" CssClass="btnsmall" onclick="btnUpdateDetails_Click" /></td>
<td align="left"><asp:Button ID="btnDelExp" runat="server" CssClass="btnsmall" onclick="btnDelExp_Click" Text="Delete" /></td></tr>
</table></center>
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
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A2" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>Experience &amp; Sponsorship Details:</b><br />
<br />
<div id="Div3" style="display: block;">
<div  id="div4" style="width: 100%; overflow:scroll; height:277px;">
<asp:GridView ID="GridSponExp" runat="server" BackColor="#DEBA84" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" 
        CellSpacing="2" Width="100%" 
        onselectedindexchanged="GridSponExp_SelectedIndexChanged" >
        <Columns>
            <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
        </Columns>
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<asp:Panel ID="pnlspc" runat="server" Height="100px" />
</div>
</asp:Content>