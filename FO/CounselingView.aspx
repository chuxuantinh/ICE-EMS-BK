<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="CounselingView.aspx.cs" Inherits="FO_CounselingView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Student Counselling FollowUp
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        &nbsp;</td><td><asp:Label ID="lblCounsellingView" runat="server" Text="Counselling View" CssClass="redirecttabhome" ></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
        
          <div class="fromRegisterlbl"><h1><div style="float:right; margin-right:30px;">Counselling No.:&nbsp;<asp:Label ID="lblCID" runat="server" ></asp:Label></div>Counselling Followup</h1></div>
<asp:Panel ID="pnlhome" runat="server" ><div style="height:20px;"></div></asp:Panel>
<script>
    function toggleA1x(showHideDiv, switchImgTag) {
        var ele = document.getElementById(showHideDiv);
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
</div><div style="padding:5px; color:White; font-size:15px;">
<asp:RadioButton ID="rbtndate" runat="server" Text="Counselling Date" GroupName="dev" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnFollowupDate" runat="server" Text="Followup Date" GroupName="dev" />&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="calimg" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender>&nbsp;&nbsp;&nbsp;<img src="../images/cal.png" id="calimg" runat="server"  alt="Cal" /> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_OnClick" />
</div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:500%" >
<asp:GridView ID="GridCounselling" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" OnSelectedIndexChanged="Grid_OnselectedIndexChanged" OnRowDataBound="GridCounselling_OnRowDataBound"        GridLines="Horizontal" HorizontalAlign="Center" Width="100%" 
                         EmptyDataText="N/A" >
        <EmptyDataTemplate><center> Counselling Record Not found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <Columns>
        <asp:ButtonField CommandName="Select" HeaderText="View Status" Text="select" />
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" 
            HorizontalAlign="Center" />
        <EditRowStyle HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
                     
   </div>
   </div></div>
   <br />
   <br />
   <asp:Panel ID="pnlCounselling" runat="server" >
<table class="tbl"><tr><td>Student Name:</td><td><asp:Label ID="lblSName" runat="server" ></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:Label ID="lblCourse" runat="server" ></asp:Label></td></tr>
<tr><td>Address:</td><td><asp:Label ID="lblAddress1" runat="server" ></asp:Label></td></tr>
<tr><td></td><td><asp:Label ID="lblAddress2" runat="server" ></asp:Label></td></tr>
<tr><td></td><td>&nbsp;<asp:Label ID="lblCity" runat="server" >,&nbsp;&nbsp;<asp:Label ID="lblState" runat="server" ></asp:Label>&nbsp;-&nbsp;<asp:Label ID="lblPincode" runat="server" ></asp:Label></asp:Label></td></tr>
<tr><td>Contact:</td><td><asp:Label ID="lblcontact" runat="server" ></asp:Label></td><td>Mobile:&nbsp;&nbsp;<asp:Label ID="lblMobile" runat="server" ></asp:Label></td></tr>
<tr><td>Email:</td><td><asp:Label ID="lblEmail" runat="server" ></asp:Label></td></tr>
<tr><td>Date:</td><td><asp:Label ID="lblDate" runat="server" ></asp:Label></td><td>Session:&nbsp;&nbsp;<asp:Label ID="lblSession" runat="server" ></asp:Label></td></tr>
<tr><td><b>Current Status</b>:</td><td><b><asp:Label ID="lblStatus" runat="server" ></asp:Label></b></td></tr>
</table>
<br />
<hr />
<center>Change Status:&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" Width="200px" ><asp:ListItem Value="" Text="----Select Status--------" /><asp:ListItem Value="Running" Text="Running" /><asp:ListItem Value="Converted" Text="Converted" /><asp:ListItem Value="NotConverted" Text="NotConverted" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnchangeStatus" runat="server" Text="Change" OnClick="btnchangeStatus_Onclick" CssClass="btnsmall" /> </center>
<hr />
<br />
</asp:Panel>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br /><br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
   </div>
             </div><br />
</asp:Content>

