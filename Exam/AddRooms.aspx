<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="AddRooms.aspx.cs" Inherits="Exam_AddRooms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exam Center</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    

    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Exam Center Rooms" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Exam Center Profile</h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSessionOK" runat="server" CssClass="btnsmall" OnClick="btnSessionOK_OnClick" Text="View" /></td></tr></table></center>
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
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Examination Center:</div>
<div id="Div1x" style="display:block;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:200px" >
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="Black" 
        GridLines="Vertical" onselectedindexchanged="GridView1_SelectedIndexChanged" 
        Width="100%" DataKeyNames="ID">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="30%" 
                SortExpression="Name" >
<ItemStyle Width="30%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="ToSeat" HeaderText="ToSeat" 
                SortExpression="ToSeat" />
            <asp:BoundField DataField="Season" HeaderText="Season" 
                SortExpression="Season" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                         SelectCommand="SELECT DISTINCT [ID], [Name], [City], [ToSeat], [Season] FROM [ExamCenter] WHERE ([Season] = @Season)">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                                 PropertyName="Text" Type="String" />
                         </SelectParameters>
                     </asp:SqlDataSource>
    
   </div>
   </div></div>

    <hr />

<div  class="fromRegisterlbl">
<h1 style="float:right; margin-right:30px;" >Center Code:&nbsp;<asp:Label ID="lblCenteCode" runat="server"></asp:Label></h1><h1> Building Facilities Available</h1>
<br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
        <asp:Panel ID="panelRoom" runat="server"  >
        <center>
        <%--<asp:ImageButton ID="btnshowseason" runat="server" AlternateText="OK" OnClick="btnShowSEason_Click" />--%>
        <table><tr><td>Room No.:</td><td><asp:Label  ID="txtRoomNo" runat="server"></asp:Label></td></tr>
      <tr><td>Room Name:</td><td><asp:TextBox ID="txtRoomName" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
        <tr><td>Seating Capacity:</td><td><asp:TextBox ID="txtRoomCapacity" runat="server" CssClass="txtbox"></asp:TextBox><asp:FilteredTextBoxExtender
                ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRoomCapacity" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            </td></tr><tr><td>No. of Studnet Column(s):</td><td><asp:DropDownList ID="ddlRoomColumn" runat="server" CssClass="txtbox"><asp:ListItem Value="2" Text="2"></asp:ListItem><asp:ListItem Value="3" Text="3" /><asp:ListItem Value="4" Text="4" /><asp:ListItem Value="5" Text="5" /><asp:ListItem Value="6" Text="6" /><asp:ListItem Value="7" Text="7" /><asp:ListItem Value="8" Text="8" /><asp:ListItem Value="9" Text="9" /><asp:ListItem Value="10" Text="10" /></asp:DropDownList>
          &nbsp;&nbsp;  <asp:Button ID="btnRoom" runat="server" CssClass="btnsmall" OnClick="btnRoom_click" 
                Text="Add" ValidationGroup="ADD" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="btnsmall" Text="Delete" OnClick="btnDelete_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCacel" runat="server" Text="Cancel" OnClick="btnCAncel_Click" />
            </td></tr>
        </table>
    <asp:Label ID="lblExceptionRoom" runat="server" ></asp:Label>
        </center>
        <br />
            <asp:GridView ID="gvroomshow" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"
                DataSourceID="SqlDataSource3" ForeColor="Black" 
        GridLines="Vertical" 
                Width="100%" onselectedindexchanged="gvroomshow_SelectedIndexChanged">
             <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                <Columns>
                   <asp:CommandField SelectText="Select" HeaderText="Delete"  ShowSelectButton="true"/>
                    <asp:BoundField DataField="Season" HeaderText="Season" 
                        SortExpression="Season" />
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="RoomNo" HeaderText="RoomNo" 
                        SortExpression="RoomNo" />
                        <asp:BoundField DataField="RoomName" HeaderText="RoomName" 
                        SortExpression="RoomName" />
                    <asp:BoundField DataField="Capacity" HeaderText="Capacity" 
                        SortExpression="Capacity" />
                    <asp:BoundField DataField="Columns" HeaderText="Columns" 
                        SortExpression="Columns" />
                </Columns>
              <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
                SelectCommand="SELECT DISTINCT  [Season], [ID], [RoomNo],[RoomName], [Capacity], [Columns] FROM [Rooms] WHERE (([ID] = @ID) AND ([Season] = @Season))">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblEnrolment" Name="ID" PropertyName="Text" 
                        Type="String" />
                    <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br /><br />
        </asp:Panel>
        </ContentTemplate>
         </asp:UpdatePanel>
 </div>
 <div id="invisible" runat="server" style="height:300px;" ></div>
</div>
</asp:Content>

