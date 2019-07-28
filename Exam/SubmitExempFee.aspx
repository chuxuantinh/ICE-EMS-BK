<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="SubmitExempFee.aspx.cs" Inherits="Exam_SubmitExempFee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exemption Form
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" CssClass="redirecttab" onclick="lblHomeRedirect_Click"></asp:LinkButton></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab" onclick="lbtnNext1Redirect_Click"></asp:LinkButton></td><td>
<asp:Label ID="lblPageName" runat="server" Text="Submit Exemption Form" CssClass="redirecttabhome"></asp:Label></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updatePanel1" runat="server" >
<ContentTemplate>
<div class="fromRegisterlbl"><h1>Exemption Form:&nbsp;&nbsp;</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;Session ID:&nbsp;<asp:Label ID="lblExamSeasonHidden" runat="server"></asp:Label></td></tr></table>
<br />
</center>
<div class="togalfees" style="width:100%">
    <div style="padding:5px; color:White; font-size:15px;">
Membership No:<asp:TextBox ID="txtMembership" runat="server" AutoPostBack="True" 
            ontextchanged="txtMembership_TextChanged" ></asp:TextBox>
</div>
<div id="Div1" style="display:block;">
 <input id="scrollPos" runat="server" type="hidden" value="0" />
 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px">
<asp:GridView ID="gridSubmitExmpFee" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" 
        onselectedindexchanged="gridSubmitExmpFee_SelectedIndexChanged" 
        onrowdatabound="gridSubmitExmpFee_RowDataBound" Width="100%" >
<EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
<AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
<Columns>
<asp:CommandField ShowSelectButton="True" />
</Columns>
<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#E9E7E2" />
<SortedAscendingHeaderStyle BackColor="#506C8C" />
<SortedDescendingCellStyle BackColor="#FFFDF8" />
<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>
</div></div></div></div>
<center>
<br /><asp:Button ID="btnPromote" runat="server" CssClass="btnsmall" onclick="btnPromote_Click" Text="Promote" Visible="false"/>

<br /></center>
<asp:Panel ID="panelVisible" runat="server" >
<b>
<asp:GridView ID="GridCivilArch" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" HorizontalAlign="Center" Width="100%">
<RowStyle BackColor="#F7F7DE" />
<Columns>
<asp:BoundField DataField="SubID" HeaderText="Subject Code" SortExpression="SubID" />
<asp:BoundField DataField="SubName" HeaderText="Subject Name" SortExpression="SubName" />
<asp:TemplateField>
<HeaderTemplate>
<asp:Label ID="lblAppSub" runat="server" Text="Select Subjects"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="chkAppSubjectA" runat="server"/>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SubjectType" HeaderText="Type" SortExpression="SubjectType" />
</Columns>
<FooterStyle BackColor="#CCCC99" />
<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
<EmptyDataTemplate>
<center>No Subject found !!!</center>
</EmptyDataTemplate>
<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
</asp:GridView>
</b><br />
<br /><center>
<table class="tbl"><tr><td align="right"><b>Remarks :&nbsp;&nbsp;&nbsp;&nbsp; </b></td>
<td align="left"><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="63px" Width="254px"></asp:TextBox>
    &nbsp;</td>
    <td align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave0" runat="server" CssClass="btnsmall" 
            onclick="btnSave_Click" Text="Save" />
    </td>
</tr>
</table>
<br />
        &nbsp;&nbsp;
</b><asp:Label ID="lblExcept" runat="server" ForeColor="Maroon"></asp:Label></center>
</asp:Panel><asp:Panel ID="panelInVisible" runat="server" Height="250px"></asp:Panel>
</b>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>
