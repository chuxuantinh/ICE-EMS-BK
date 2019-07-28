<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" CodeFile="ManageGroups.aspx.cs" Inherits="Administrator_ManageGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Groups
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updatepanel" runat="server" ><ContentTemplate><br />
<center>
                            <br />
                          <table class="tbl"><tr><td>&nbsp;Enter IMID:</td><td colspan="2"><asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox></td>
<td><asp:Button ID="btnOk" runat="server" Text="OK"  CssClass="btnsmall" 
        onclick="btnOk_Click"/></td><td><asp:Label ID="lblException" runat="server" ForeColor="Maroon"></asp:Label></td></tr>
</table><asp:Panel ID="pnlIM" runat="server"><table class="tbl">
<tr><td>Group ID:</td><td><asp:Label ID="lblGID" runat="server" ForeColor="Maroon"></asp:Label></td><td>List Of IM with GroupID</td><td>
    <asp:DropDownList ID="ddlIM" runat="server" CssClass="txtbox"></asp:DropDownList>
    &nbsp;</td></tr><tr><td><asp:LinkButton ID="lbtnGID" runat="server" 
            Text="Generate New GroupId" onclick="lbtnGID_Click" ForeColor="Black"></asp:LinkButton></td>  <td>
                                        <asp:LinkButton ID="lbtnNewIM" runat="server" 
            Text="Add New IM" onclick="lbtnNewIM_Click" ForeColor="Black"></asp:LinkButton></td></tr>
</table></asp:Panel>
<asp:Panel ID="pnlGID" runat="server"><table class="tbl">
<tr><td>Group ID:</td><td><asp:TextBox ID="lblGroup" runat="server" CssClass="txtbox" ></asp:TextBox></td><td><asp:Button ID="btnSave" runat="server" CssClass="btnsmall" Text="Save GID" 
        onclick="btnSave_Click" /></td></tr>

</table></asp:Panel>
<asp:Panel ID="pnlNewIM" runat="server"><table class="tbl">
<tr><td>IMID:</td><td><asp:TextBox ID="txtIMI" runat="server" cssClass="txtbox"></asp:TextBox></td><td>
    <asp:Button ID="btnAdd" runat="server" CssClass="btnsmall" Text="ADD" 
        onclick="btnAdd_Click" /></td> <td><asp:Button ID="btnDelete" runat="server" 
            CssClass="btnsmall" Text="Delete" onclick="btnDelete_Click" /></td></tr>
</table></asp:Panel> </center>
</ContentTemplate></asp:UpdatePanel>
</asp:Content>

