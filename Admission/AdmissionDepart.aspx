<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="AdmissionDepart.aspx.cs" Inherits="Admission_AdmissionDepart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="contenttitle" runat="server" ContentPlaceHolderID="contenttitle">Admission Department</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div style="float:right; margin-right:50px;">Insert Enrollment No.:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnViewEnroll" runat="server" Text="View Profile"  OnClick="btnView_Click"  CssClass="btnsmall"/></div>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="View Profile" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Student&nbsp;Profile</h1></div>
<div id="invisible" runat="server" style="height:270px;" >
<img src="../images/admissionss.jpg"  height="250px" width="100%" alt=""/>
</div>
<asp:Panel ID="Panel1" runat="server" Height="100px" Visible="false"></asp:Panel>
<asp:Panel ID="pnlProfile" runat="server">
<div id="visisble" runat="server">
<asp:UpdatePanel ID="updatepanleIM" runat="server" ><ContentTemplate>
<div class="rightbox">
<br />
<asp:Label ID="lblRegisDate" runat="server"  Font-Bold="true" ForeColor="RED" Font-Size="Large"></asp:Label>
<br />
<asp:Label ID="lblStream" runat="server"  Font-Bold="true" ForeColor="Gray" Font-Size="Large"></asp:Label><br /><br />
<asp:Label ID="lblNameCourse" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="15px" ></asp:Label><br />
<asp:Label ID="lblPartNo" runat="server" ></asp:Label>
<br />
<br />
</div>
<asp:Panel ID="panelIM" runat="server" CssClass="imbox" >
<br /><br />     
Institutional Member ID:&nbsp;&nbsp;<asp:Label ID="txtIDIM" runat="server" Font-Bold="true" ForeColor="Gray" Font-Size="Large"></asp:Label>          <br />
<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="15px" ></asp:Label><br />
<asp:Label ID="lblIMAddress" runat="server" ></asp:Label><br />
<asp:Label ID="lblIMCity" runat="server" ></asp:Label><br />
<br />
</asp:Panel></ContentTemplate></asp:UpdatePanel>
<br />
<br />
<panel class="rightpanel">
<div style="margin-left:5%; width:40%; height:auto; " >
<asp:DataList ID="DataList1" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
<ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"SID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
</asp:DataList><br /><br />

</div>
</panel>
<table class="tbl">
<tbody>
<tr>
    <td>Full Name of Candidate:</td>
    <td colspan="2"><asp:Label ID="txtName" runat="server"  CssClass="lblbox"></asp:Label></td>
</tr>
<tr>
    <td>Father&#39;s/Husband&#39;s Name:</td>
    <td colspan="2"><asp:Label ID="txtFather" runat="server" CssClass="lblbox"></asp:Label></td>
</tr>
<tr>
    <td>Mother&#39;s Name:</td>
    <td colspan="2">
        <asp:Label ID="txtMother" runat="server" CssClass="lblbox"></asp:Label>
    </td>
</tr>
<tr>
<td>Date of Birth:</td>
<td colspan="2">
    <asp:Label ID="txtDOB" runat="server" CssClass="lblbox"></asp:Label>
        </td>
</tr>
<tr>
<td>Phone:</td>
<td colspan="2">
    <asp:Label ID="txtPhoneNo" runat="server" CssClass="lblbox"></asp:Label>
    </td>
</tr>
<tr>
<td>Mobile:</td>
<td colspan="2">
    <asp:Label ID="txtMobile" runat="server" CssClass="lblbox"></asp:Label>
    </td>
</tr>
<tr>
<td>Email:&nbsp;&nbsp;</td>
<td colspan="2">
    <asp:Label ID="txtEmail" runat="server" CssClass="lblbox"></asp:Label>
    </td>
</tr>
<tr>
<td>Age:</td>
<td colspan="2">
    <asp:Label ID="txtAge" runat="server" CssClass="lblbox"></asp:Label>
    </td>
</tr>
<tr>
<td>Nationality:</td>
<td colspan="2">
    <asp:Label ID="ddlNationality" runat="server" CssClass="lblbox"></asp:Label>
    </td>
</tr>
<tr>
<td>Category:</td>
<td colspan="2"><asp:Label ID="ddlCategory" runat="server" CssClass="lblbox" ></asp:Label></td>
</tr>
<tr>
    <td>Permanent Address:</td>
    <td>
        <asp:Label ID="txtPAddress" runat="server" CssClass="lblbox" Width="60%"></asp:Label>
    </td>
    <td><asp:Label ID="lblPAddress2" runat="server" CssClass="lblbox" Width="60%"></asp:Label></td>
</tr>
<tr>
<td></td>
<td colspan="2"><asp:Label ID="txtPCity" runat="server" CssClass="lblbox"></asp:Label>
    ,<asp:Label ID="txtPState" runat="server" CssClass="lblbox"></asp:Label>
    </td>
</tr>
<tr>
<td>Corr.. Address:</td>
<td colspan="2"><asp:Label ID="txtCAddress" runat="server" CssClass="lblbox" Width="60%"></asp:Label></td>
</tr>
<tr>
<td></td>
<td colspan="2"><asp:Label ID="txtCCity" runat="server" CssClass="lblbox"></asp:Label>
    ,<asp:Label ID="txtCState" runat="server" CssClass="lblbox" ></asp:Label></td>
</tr>

    <tr>
        <td>
            Remarks:</td>
        <td colspan="2">
            <asp:Label ID="lblRemarks" runat="server"/>
        </td>
    </tr>

    <tr>
        <td>
            Exmp Remarks:</td>
        <td colspan="2">
            <asp:Label ID="lblExmpRemarks" runat="server" />
        </td>
    </tr>

</tbody>
</table>

<br />
<br />
Application Form No. &nbsp;<asp:Label ID="lblAppID" runat="server" ></asp:Label>
</div>
</asp:Panel>
</div>
</asp:Content>

