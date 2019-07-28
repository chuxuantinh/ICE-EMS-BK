<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sql.aspx.cs" Inherits="Sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Type Query</h2>
            <asp:TextBox ID="txtQry" Width="500px" Height="200px" runat="server" TextMode="MultiLine" Rows="5" Columns="5">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnExecute" runat="server" Text="Execute" OnClick="btnExecute_Click" />
            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
