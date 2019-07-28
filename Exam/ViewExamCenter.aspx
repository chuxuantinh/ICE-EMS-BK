<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ViewExamCenter.aspx.cs" Inherits="Exam_ViewExamCenter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exam Center 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 84px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblPageName" runat="server" CssClass="redirecttabhome" Text="View Exam Center"></asp:Label>
</td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Exam Center Profile</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSessionOK" runat="server" CssClass="btnsmall" OnClick="btnSessionOK_OnClick" Text="View" /></td></tr></table></center>
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>
<br />
<script>
      function toggleA1x(showHideDiv, switchImgTag) {
          var ele = document.getElementById(showHideDiv);
          var imageEle = document.getElementById(switchImgTag);
          var imageEle = document.getElementById(switchImgTag);
          if (ele.style.display == "block") {
              ele.style.display = "none";
              imageEle.innerHTML = '<img src="../images/plus.png">';
          }
          else {
              ele.style.display = "block";
              imageEle.innerHTML = '<img src="../images/minus.png">';
          }
      }
</script>
<div class="togalfees" style="width:99%">
<div class="headerDivImgfees">
<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Examination Center:</div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 100%; overflow:scroll; height:200px">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" onselectedindexchanged="GridView1_SelectedIndexChanged" Width="100%" DataKeyNames="ID" onrowcommand="GridView1_RowCommand1">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Select" >
            <ItemStyle ForeColor="Black" />
            </asp:CommandField>
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="30%" SortExpression="Name" >
        <ItemStyle Width="30%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="ToSeat" HeaderText="ToSeat" SortExpression="ToSeat" />
            <asp:BoundField DataField="RollNo" HeaderText="RollNo" Visible="false" SortExpression="RollNo" />
            <asp:TemplateField HeaderText="Edit Admin">
            <ItemTemplate>
            <asp:LinkButton CommandName="Select1" ID="lkView" runat="server" ForeColor="Black">Edit Admin</asp:LinkButton>
            </ItemTemplate>
            <ItemStyle ForeColor="Black" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT [ID], [Name], [City], [State], [Email], [ToSeat], [RollNo] FROM [ExamCenter] WHERE ([Season] = @Season) ORDER BY [ID]">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
   </div>
   </div></div>
<asp:Panel ID="panView5" runat="server">
     <table class="tbl" style="float:right; margin-right:10px; width:50%;"><tr><td>Exam Center Code:&nbsp;&nbsp;<asp:Label ID="lblCenterCode" runat="server" Font-Bold="true" ForeColor="Black" ></asp:Label></td></tr><tr><td>&nbsp;&nbsp;<asp:Label ID="lblCenteNaem" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblCenterAddress" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblCenterAddress2" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>,</td></tr><tr><td>&nbsp;<asp:Label ID="lblCenterCity" runat="server" Font-Bold="true"></asp:Label>, &nbsp;(<asp:Label ID="lblCenterState" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label> &nbsp;)-<asp:Label ID="lblPinCode" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td></tr>
<tr><td>Total Capacity:&nbsp;<asp:Label ID="lblCapacity" runat="server" ></asp:Label></td></tr>
</table>
</asp:Panel>
<table class="tbl" width="35%"><tr><td>
Enter Exam Center Code:&nbsp;<br /><asp:TextBox ID="txtExamCode" runat="server" CssClass="txtbox" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtExamCode" FilterType="Numbers"></dev:FilteredTextBoxExtender><asp:Button ID="btnOKCenterCode" runat="server" CssClass="btnsmall" OnClick="btnOkCenterCode_OnClick" Text="OK" />
    <asp:Button ID="btnAdd" runat="server" CssClass="btnsmall" 
        onclick="btnAdd_Click" Text="Add" Visible="False" />
    </td></tr>
</table><center><asp:Label ID="lblExceptionCode" runat="server" ></asp:Label></center>
<div style="float:left; margin-right:5px; width:48%;">
<asp:Panel ID="pnlAcInf" runat="server" Visible="false" CssClass="imbox" Width="287px">
<table class="tbl">
<tr><td align="center" colspan="2"><b>Account Information</b></td>
</tr>
<tr><td align="left" class="style1">A/C No:</td>
<td align="left"><asp:Label ID="lblACNo" runat="server" Font-Bold="true" ForeColor="Maroon" /></td>
</tr>
<tr><td align="left" class="style1">IFSC Code:</td>
<td align="left"><asp:Label ID="lblIFSCCode" runat="server"/></td>
</tr>
<tr><td align="left" class="style1">Name:</td>
<td align="left"><asp:Label ID="lblBankName" runat="server" ForeColor="Maroon" Font-Bold="true"/></td>
</tr>
<tr><td align="left" class="style1">Address:</td>
<td align="left"><asp:Label ID="lblbAdd" runat="server"/></td>
</tr>
<tr><td align="left" class="style1">DD In Favour:</td>
<td align="left"><asp:Label ID="lblDDInFvr" runat="server" ForeColor="Maroon" Font-Bold="true"/></td>
</tr>
<tr><td align="left" class="style1">Payable At:</td>
<td align="left"><asp:Label ID="lblPaybleAt" runat="server"/></td>
</tr>
<tr><td align="left" class="style1">A/C Title:</td>
<td align="left"><asp:Label ID="lblAcTtl" runat="server"/></td>
</tr>
<tr><td align="left" class="style1">Courier Service:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
<td align="left"><asp:Label ID="lblcourier" runat="server"/></td>
</tr>
</table>
</asp:Panel>
</div>
<div style="float:right; margin-right:5px; width:50%;">
<asp:Panel ID="PanView" runat="server">
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" Visible="true"
        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSource4" 
        onselectedindexchanged="GridView3_SelectedIndexChanged" Width="100%">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" Visible="false" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false" />
            <asp:BoundField DataField="RoomNo" HeaderText="RoomNo" 
                SortExpression="RoomNo" />
            <asp:BoundField DataField="Capacity" HeaderText="Capacity" 
                SortExpression="Capacity" />
            <asp:BoundField DataField="Columns" HeaderText="Columns" 
                SortExpression="Columns" />
        </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT [ID], [RoomNo], [Capacity], [Columns] FROM [Rooms] WHERE ([ID] = @ID) ORDER BY [RoomNo]">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblCenterCode" Name="ID" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Panel>
    <br />
</div>
<br /><br /><br /><br /><br /><br />
<div id="divdatagrid3" style="width: 100%; overflow:scroll; height:100px" >
    <asp:GridView ID="gridInvig" runat="server" Visible="true"
        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" CellSpacing="2" Width="100%" onrowdatabound="gridInvig_RowDataBound">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
    </asp:GridView>
    </div>
    <br /><br /><br /><br /><br /><br /><br /><hr />
</div>
</asp:Content>