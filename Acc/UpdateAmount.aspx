<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true"
    CodeFile="UpdateAmount.aspx.cs" Inherits="Acc_UpdateAmount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Update IM Amount
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="redirect">
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lblHomeRedirect" runat="server" OnClick="ibtnHome_Click" Text="Home"
                        CssClass="redirecttab"></asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="lblACManage" runat="server" Text="Manage IM Account" CssClass="redirecttabhome"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="rightpanel2">
        <asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server">
            <ContentTemplate>
                <div class="fromRegisterlbl">
                    <h1 style="float: right; margin-right: 50px;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lblEnrolment" runat="server"></asp:Label></h1>
                    <h1>
                        Manage IM Account
                    </h1>
                </div>
                <center>
                    <br />
                    <br />
                    <asp:Label ID="Lblmem" runat="server" Text="Membership No."></asp:Label>
                    <asp:TextBox ID="Txtmember" runat="server"  CssClass="txtbox"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    <br />
                    <br />
                    <asp:Panel ID="pnlAmt" runat="server" Height="500px" Visible="false">
                    <table><tr><td>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        Fees :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFee" runat="server" Width="140px" onselectedindexchanged="ddlFee_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amounts :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmounts" runat="server" CssClass="txtbox"></asp:TextBox>
                                          <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtAmounts" >
                                </dev:FilteredTextBoxExtender>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ControlToValidate="txtAmounts" ErrorMessage="Amount Required" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Architecture" />
                                    </td>
                                </tr>
                            </tbody>
                        </table></td>
                        <td>
                        <asp:GridView ID="GridAmount" runat="server" CellPadding="4" ForeColor="#333333" 
                            GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                       </asp:GridView>
                        </td>
                        </tr></table>
                </asp:Panel> 
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    </div>
    <br />
    <br />
</asp:Content>
