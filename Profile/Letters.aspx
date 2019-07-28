<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/ProfileMaster.master" AutoEventWireup="true" CodeFile="Letters.aspx.cs" Inherits="Profile_Letters" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Letters
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblText" Text="Letters" runat="server" CssClass="redirecttabhome"></asp:Label> 
         </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; 
    <asp:ImageButton src="../images/unread.png" runat="server" height="20" 
        width="20"  ID="lnkUnread" Text="Unread" runat="server" 
        onclick="lnkUnread_Click" ToolTip="NotOpen" />
    &nbsp;
    <asp:ImageButton src="../images/read.gif"  height="20" width="20" ID="lnkOpen" 
        Text="Open" runat="server" onclick="lnkOpen_Click" ToolTip="Open" /> 
    &nbsp; 
    <asp:ImageButton src="../images/search.png"  height="20" width="20" 
        ID="lnkSearch" Text="Search" runat="server" 
        onclick="lnkSearch_Click" ToolTip="Search" />&nbsp;</h1>
    <h1> <asp:Label ID="lblTitle" runat="server"></asp:Label>:<asp:Label ID="lblTotal" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </h1></div>
    <asp:Panel ID="pnlMain" runat="server" BorderStyle="None">
     <div align="center"> <asp:DropDownList ID="ddlSearch" runat="server" 
             AutoPostBack="True" CssClass="txtbox" 
             onselectedindexchanged="ddlSearch_SelectedIndexChanged1">
         <asp:ListItem>Name</asp:ListItem>
         <asp:ListItem>Date</asp:ListItem>
         <asp:ListItem>DiaryNo</asp:ListItem>
         <asp:ListItem>IMID</asp:ListItem>
         </asp:DropDownList>
          &nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server"  CssClass="txtbox" 
               ></asp:TextBox>
         <asp:Button ID="btnOk" runat="server" Text="Search" CssClass="btnsmall" 
             onclick="btnOk_Click1" />
            <dev:CalendarExtender ID="devdage" runat="server" Format="dd/MM/yyyy" 
                PopupButtonID="txtSearch" PopupPosition="BottomRight" TargetControlID="txtSearch">
            </dev:CalendarExtender>
         </div>
     </asp:Panel>
     <asp:Panel ID="pnlNme" runat="server">
     <div id="div2" style="width: 100%; overflow:scroll; height:250px;"  >
    <asp:GridView ID="grdLetters" runat="server" CellPadding="4" Width="100%" 
         onrowdatabound="grdLetters_RowDataBound" 
            onselectedindexchanged="grdLetters_SelectedIndexChanged" ForeColor="#333333" 
            GridLines="Vertical">
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          <Columns>
              <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="Black"/>
              
          </Columns>
          <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
          <EditRowStyle BackColor="#999999" />
          <EmptyDataRowStyle  HorizontalAlign="Center" BackColor="#F7F7DE"/>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
          <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <SortedAscendingCellStyle BackColor="#E9E7E2" />
          <SortedAscendingHeaderStyle BackColor="#506C8C" />
          <SortedDescendingCellStyle BackColor="#FFFDF8" />
          <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView></div>
      </asp:Panel>
  <asp:Panel ID="pnlSelect" runat="server"><center>
    <table class="tbl">
    <tr><td>Remarks:</td><td><asp:TextBox ID="txtRemarks" 
            runat="server" TextMode="MultiLine" CssClass="txtbox" Height="52px" 
            Width="250px"></asp:TextBox></td></tr>
    <tr><td></td><td>
        <asp:Button ID="btnSave" runat="server" 
            Text="Save" CssClass="btnsmall" 
            Width="170px" onclick="btnSave_Click" /><asp:Button ID="btnDispatch" runat="server" 
            Text="Send For Dispatch" CssClass="btnsmall" onclick="btnDispatch_Click" 
            Width="170px" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSupply" runat="server" 
            Text="Supply to Account" CssClass="btnsmall" onclick="btnSupply_Click" 
            Width="170px" /></td></tr></table></center>
    </asp:Panel><br />  
</div>
<br />
          <asp:Label ID="lblName" runat="server" Text="" Visible="false"></asp:Label>
   <asp:Label ID="lblDesignation" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>


