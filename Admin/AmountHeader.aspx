<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="AmountHeader.aspx.cs" Inherits="AmountHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Amount Header
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="redirect">
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lblHomeRedirect" runat="server" OnClick="lblHomeRedirect_Click"
                        Text="Home" CssClass="redirecttab"></asp:LinkButton>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="rightpanel2">
        <div class="fromRegisterlbl">
            <h1 style="float: right; margin-right: 50px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblEnrolment" runat="server"></asp:Label></h1>
            <h1>
                Manage Amount Header</h1>
        </div>
        <asp:Panel Height="600px" runat="server" ID="pnlSpace">
            <center>
                <br />
                <br />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                    OnRowUpdating="GridView1_RowUpdating">
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("Aname") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Aname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="update">Update</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="cancel">Cancel</asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="save">Save</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="edit">Edit</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No Data Available
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:DropDownList ID="DropDownList1" runat="server" Visible="false">
                </asp:DropDownList>
                <br />
                <br />
                <br />
                <table>
                    <div class="fromRegisterlbl">
                        <h1 style="float: right; margin-right: 50px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;</h1>
                        <h1>
                            Set Limit of Account Approve</h1>
                    </div>
                    <br />
                    <br />
                    <br />
                    <tbody>
                        <tr>
                            <th>
                                SET Limit:
                            </th>
                            <td>
                                <asp:TextBox ID="txtLimit" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtLimit" >
                                </asp:FilteredTextBoxExtender>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ControlToValidate="txtLimit" ErrorMessage="RequiredFieldValidator" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLimit"
                                    Display="Dynamic" ErrorMessage="*" ValidationExpression="^\d+$" ValidationGroup="check">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                    ValidationGroup="Architecture" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </center>
        </asp:Panel>
    </div>
    <br />
</asp:Content>
