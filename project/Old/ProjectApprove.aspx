<%@ Page Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ProjectApprove.aspx.cs" Inherits="project_ProjectApprove" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Approved Project
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Project Evaluation</h1></div><br />
<asp:Panel ID="panelRight" runat="server" ><asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<table class="tbl"><tr><td>Select Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; <asp:TextBox ID="txtSession" runat="server" Width="70px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></tr>
<tr><td>Serial No:</td><td ><asp:TextBox ID="txtsearch" 
        runat="server" CssClass="txtbox" Width="178px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnshow" runat="server" CssClass="btnsmall" 
        onclick="btnshow_Click" Text="ok" />
    <asp:Label ID="lblshow" runat="server" ForeColor="#CC3300"></asp:Label>
</td></tr>
<tr><td></td><td> 
    &nbsp;</td></tr>
</table><center> <asp:Label ID="lblexception" runat="server" ForeColor="Red" Text=""></asp:Label></center>
<br /><hr />
    <asp:Panel ID="Pnlresult" runat="server" Visible="False">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<asp:Panel ID="pan1" runat="server"  >
<center><asp:Label ID="lblAppNo" runat="server"  Font-Bold="true" Visible="false"></asp:Label>Membership No:<asp:Label ID="lblMem" runat="server"  Font-Bold="true" ></asp:Label>&nbsp;&nbsp;<h3 class="hl3"><asp:Label ID="lblprojecttitle" runat="server"  ></asp:Label></h3>
<asp:Label ID="lbldescription" runat="server"  Font-Bold="true" ></asp:Label><br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </center>
<table class="tbl" width="95%">
<tr><td></td><td><asp:RadioButton ID="rgroupmate1" runat="server"  AutoPostBack="true"
        Text="GroupMate: #1" oncheckedchanged="rgroupmate1_CheckedChanged" GroupName="a" /></td><td>
        <asp:RadioButton ID="rgroupmate2" runat="server" Text="GroupMate: #2" AutoPostBack="true"
            oncheckedchanged="rgroupmate2_CheckedChanged" GroupName="a"/></td><td>
        <asp:RadioButton ID="rgroupmate3" runat="server" Text="GroupMate: #3" AutoPostBack="true"
            oncheckedchanged="rgroupmate3_CheckedChanged" GroupName="a"/></td></tr>
<tr><td></td><td>&nbsp;ID:&nbsp;<asp:Label ID="lblgroupmate1" runat="server" Text="" Enabled="false" Font-Bold="true"></asp:Label></td><td>
        &nbsp;ID:&nbsp;<asp:Label ID="lblgroupmate2" runat="server" Text="" Enabled="false" Font-Bold="true"></asp:Label></td><td>
          &nbsp; ID:&nbsp; <asp:Label ID="lblgroupmate3" runat="server" Text="" Enabled="false" Font-Bold="true"></asp:Label></td></tr>
</table>
    </asp:Panel>
    <br />
<asp:Panel ID="Pan2" runat="server"  CssClass="expbox" Width="450px">
<table class="tbl"><tr><td>Student Name:</td><td><asp:Label ID="lblstuname" runat="server" Font-Bold="true"></asp:Label></td>
    <td colspan="2">
        IMID:</td>
    <td>
        <asp:Label ID="lblIMID" runat="server" Font-Bold="true"></asp:Label>
    </td></tr>
<tr><td>Stream:</td><td><asp:Label ID="lblstream" runat="server" Font-Bold="true"></asp:Label></td>
    <td colspan="2">
        Course:</td>
    <td>
        <asp:Label ID="lblcourse" runat="server" Font-Bold="true"></asp:Label>
    </td></tr>
<tr><td>Submission Date:</td><td><asp:Label ID="lbldate" runat="server" Font-Bold="true"></asp:Label></td>
    <td colspan="2">
        Part:</td>
    <td>
        <asp:Label ID="lblpart" runat="server" Font-Bold="true"></asp:Label>
    </td></tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td colspan="2">
            <asp:Label ID="lblCopy" runat="server"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
<tr><td class="style1">Date:</td><td class="style1"><asp:Textbox ID="lblcdate" runat="server" Font-Bold="true" CssClass="txtbox"></asp:Textbox><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="lblcdate"></dev:CalendarExtender> 
    &nbsp;</td>
    <td class="style1">
        <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            controltovalidate="lblcdate" Display="Dynamic" 
            errormessage="Insert Approval Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
        &nbsp;</td>
    <td class="style1">
        <asp:Label ID="lblgroupmateid" runat="server"></asp:Label>
    </td>
    </tr>
<tr><td class="style1">Grades:</td>
<td class="style1">
    <asp:TextBox ID="txtgrade" runat="server"></asp:TextBox></td>
    <td>
        Status:</td>
    <td>
        <asp:Label ID="lblstatus" runat="server" Font-Bold="true"></asp:Label>
    </td>
    </tr>
</table>
</asp:Panel>
</ContentTemplate>
    </asp:UpdatePanel>
<br /><center>
    <asp:Label ID="lblexeption" runat="server" Text=""></asp:Label><br />
        <asp:Button ID="btnSave" runat="server" Text="Submit" 
            onclick="btnSave_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
            ID="btbClear" runat="server" Text="Cancel" onclick="btbClear_Click" CssClass="btnsmall"  /></center><br /><br />
<br />
<br /><br />
</asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlSpace" runat="server" Height="250px"/>
</div>
<br />
</asp:Content>


