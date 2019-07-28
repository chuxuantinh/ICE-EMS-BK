<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master"
    AutoEventWireup="true" CodeFile="IMDocuments.aspx.cs" Inherits="Administrator_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">IM Documents
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script type="text/javascript">
      function Showalert() {
          alert('Call JavaScript Message');
      }
</script>

    <asp:Panel ID="Panel1" runat="server" Height="550px"><br /><br />
        <table width="550px" align="center" class="">
            <tbody>
                <tr>
                    <th colspan="4" align="left">
                        <asp:Label ID="Label1" runat="server" Text="IM Documents Upload" 
                            Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label><br /><br />
                    </th>
                    <tr>
                        <td>IM ID :</td>
                        <td>
                            <asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"  ControlToValidate="txtIMID"
                                ErrorMessage="RequiredFieldValidator" ValidationGroup="Architecture" >*</asp:RequiredFieldValidator>
                            <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click"
                                Text="Search"   />
                        </td>
                    </tr>
                    <tr>
                    <td>IM Name :</td>
                    <td><asp:TextBox ID="txtIMName" runat="server" Width="215px" CssClass="txtbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"  ControlToValidate="txtIMName"
                            ErrorMessage="RequiredFieldValidator" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Documents Select :
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="200px"  />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"  ControlToValidate="FileUpload1"
                            ErrorMessage="RequiredFieldValidator" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                    <td class="style1">
                    </td><td class="style1">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btnsmall" onclick="btnUpload_Click" ValidationGroup="Architecture" /></td>
                    </tr>
                </tr>
                <tr>
                <td colspan="4"><br /><br /><br />
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Medium" 
                        ForeColor="Maroon" Text="Documents View"></asp:Label><br /><br />
                </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvDetails" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="FilePath" 
                            ForeColor="#333333" GridLines="None" 
                            onpageindexchanging="gvDetails_PageIndexChanging">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="FileName" HeaderText="FileName" />
                                <asp:TemplateField HeaderText="FilePath">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" 
                                            Text="Download"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
       
    </asp:Panel>
</asp:Content>
