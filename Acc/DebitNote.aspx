<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="DebitNote.aspx.cs" Inherits="Acc_DebitNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Debit Note Requests
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="redirect">	
<table><tr>
<td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" CssClass="redirecttab" onclick="lblHomeRedirect_Click"/></td>
<td><asp:Label ID="lblText" Text="Debit Note" runat="server" CssClass="redirecttabhome"/></td>
</tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnlDebit" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="lnlRequest" 
                    runat="server" Text="Requested" onclick="lnlRequest_Click" Height="20px" 
                    ImageUrl="~/images/request.png" Width="20px" ToolTip="Requested" /> 
      
                &nbsp;<asp:ImageButton ID="lnlApproved" runat="server" Text="Approved" 
                    onclick="lnlApproved_Click" Height="20px" 
                    Width="20px" ImageUrl="~/images/approve.png" ToolTip="Approved" /> 
    &nbsp;<asp:ImageButton ID="lnkProcess" runat="server" Text="Processed" 
                    onclick="lnkProcess_Click"  Height="20px" 
                    Width="20px" ImageUrl="~/images/processq.png" ToolTip="Processed" />
    &nbsp; <asp:ImageButton ID="imgHold" runat="server" Text="Hold" 
                    Height="20px" 
                    Width="20px" ImageUrl="~/images/hold1.png" ToolTip="Hold" 
        onclick="imgHold_Click" /></h1>
<h1>Debit Note</h1>
    <p>
        <table class="tbl"><tr><td>&nbsp;&nbsp;&nbsp;&nbsp; Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" Width="70px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td><td align="right">
                &nbsp;</td></tr></table>
    </p>
    </div>
<div id="div2" style="height:300px; overflow:scroll;"  >
<asp:GridView ID="gridDebitNote" runat="server" CellPadding="4" Width="100%" 
        onrowdatabound="gridDebitNote_RowDataBound" ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
<Columns>

</Columns>
<EmptyDataTemplate>No Records Found</EmptyDataTemplate>
    <EditRowStyle BackColor="#999999" />
<EmptyDataRowStyle  HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
<FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#E9E7E2" />
<SortedAscendingHeaderStyle BackColor="#506C8C" />
<SortedDescendingCellStyle BackColor="#FFFDF8" />
<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView></div>

<br /><asp:Panel ID="pnlSpace" runat="server" Height="300px"></asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>

