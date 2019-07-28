<%@ Page Title="" Language="C#" MasterPageFile="~/Invent/MasterInventory.master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="Inventory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">New Student Admission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
  
           
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Under Development.............. </h1></div><br />
 <asp:UpdatePanel ID="updatepanleIM" runat="server" ><ContentTemplate>
<center>Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:TextBox ID="txtSession" Visible="false" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<br /><asp:Label ID="lblExceptionApp" runat="server"></asp:Label><asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label> </center><hr />
<br />
<table><tr><td>Student ID:&nbsp;<asp:TextBox ID="txtSID" runat="server" AutoPostBack="true" Width="100px" CssClass="txtbox" OnTextChanged="txtSID_OnTextChanged"></asp:TextBox><br /><asp:Label ID="lblStudentName" runat="server" Font-Bold="true"></asp:Label><br />[<asp:Label ID="lblOldIMID" runat="server" Font-Bold="true"></asp:Label>]&nbsp;&nbsp;<asp:Label ID="lblOldIMIDName" runat="server" ></asp:Label></td>
<td>IMID:&nbsp;<asp:TextBox ID="txtIMID" runat="server" AutoPostBack="true" Width="100px" CssClass="txtbox" OnTextChanged="txtIMID_OnTextChanged"></asp:TextBox><br /><b><asp:Label ID="lblGroupID" runat="server" ></asp:Label>&nbsp;</b><asp:Label ID="lblIMName" runat="server" Font-Bold="true"></asp:Label></td>
</tr></table>
<br />
<center><asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine" Height="40px" Text="Remark for IM Change......."></asp:TextBox><br /><asp:Label ID="lblException" runat="server" Font-Bold="true"></asp:Label><br /><asp:Button ID="btnChange" Visible="false" runat="server" Text="Change IM" OnClick="btnChange_Onclick" /></center><hr />

<center><asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" ><asp:ListItem Text="All" Value="All"></asp:ListItem><asp:ListItem Value="Student" Text="Student" /></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtStudent" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtStudent_OnctextChaged" /><asp:DropDownList ID="ddlViewType" runat="server" ><asp:ListItem Value="IMID" Text="IM Changed" /><asp:ListItem Value="Course" Text="Student Promotion" /><asp:ListItem Value="Other" Text="Other Changes" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" OnClick="btnView_Onclick" Text="View" />

</center>
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
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:1px;"><h1>&nbsp;&nbsp;:&nbsp; &nbsp;&nbsp;&nbsp;</h1></div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:400px" 
            >
<br /><asp:GridView ID="GridChange" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" PageSize="50" 
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%">
        <Columns>
       
        </Columns>
      
        <EmptyDataTemplate><center> No Record found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
   </div>
   </div></div>

</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>

