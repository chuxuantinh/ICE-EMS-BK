<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="Acc_SiteMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ICE(India)---Site Map Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />  
        <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath> <br /><br />
        <asp:TreeView ID="TreeView1" runat="server" DataSourceId="SiteMapDataSource1" />
    </div>
    </form>
</body>
</html>
