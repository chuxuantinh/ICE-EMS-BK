<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="CourierHome.aspx.cs" Inherits="FO_CourierHome" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Diary Entry ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"/></td><td>
&nbsp;</td><td><asp:Label ID="lblCourierHome" runat="server" Text="Courier Home" CssClass="redirecttabhome"/></td></tr></table></div>
<div id="rightpanel2"><div id="header">
<div class="fromRegisterlbl"><div style="float:right; margin-right:30px;"><h1><td>Receive Date:&nbsp;&nbsp;
<asp:Label ID="txtDate" runat="server" /></h1></div><h1>Add New Courier and Diary Entry:</h1></div>
<asp:UpdatePanel ID="updatePanel" runat="server" >
<Triggers><asp:PostBackTrigger ControlID="btnCancel" /></Triggers>
<ContentTemplate>
<br />&nbsp;&nbsp; <asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
<table class="tbl">
<tr>
<td>Select Session:</td>
<td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="ddlExamSeason_SelectedIndexChanged" Width="200px">
<asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
<asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
</asp:DropDownList>
</td>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Year:</td>
<td><asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="txtYearSeason_TextChanged" Width="100px"/></td>
</tr>
<tr><td>Receive From:</td>
<td><asp:DropDownList ID="ddlRecivefrom" runat="server" AutoPostBack="true" CssClass="txtbox" OnSelectedIndexChanged="ddlRecive_SelectedIndexChanged" Width="200px">
<asp:ListItem Selected="True" Text="IM" Value="IM"/>
<asp:ListItem Text="Student" Value="Student"/>
<asp:ListItem Text="Other" Value="Other"/>
</asp:DropDownList>
</td></tr>
<tr><td><asp:Label ID="lblFromName" runat="server"/></td>
<td colspan="3"><asp:TextBox ID="txtSName" runat="server" AutoPostBack="true" CssClass="txtbox" Font-Bold="true" OnTextChanged="txtSName_TExtChnaged" Width="197px"/>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtSName" FilterType="Numbers" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSName" Display="Dynamic" ErrorMessage="Click to Create New Diary" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
<asp:TextBox ID="txtStName" runat="server" CssClass="txtbox" Font-Bold="true" Visible="false" Width="200px"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStName" Display="Dynamic" ErrorMessage="Click to Create New Diary" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
</td></tr>
<tr><td></td>
<td colspan="3"><asp:Label ID="lblName" runat="server"/>
<asp:Label ID="lblCode" runat="server"/>
<asp:Label ID="lblCourseAddress" runat="server"/>&nbsp;
<asp:Label ID="lblExceptiontbl" runat="server" ForeColor="Red"/></td></tr>
<tr><td colspan="4"><asp:Panel ID="otherpnl" runat="server">
<table class="tbl" >
<tr><td align="left">Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;</td>
<td colspan="2"><asp:TextBox ID="txtAddress1" runat="server" CssClass="txtbox" Width="198px"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Insert Permanent Address" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
</td></tr>
<tr>
<td align="left">City:</td>
<td><asp:TextBox ID="txtCity" runat="server" CssClass="txtbox"/></td>
<td><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCity" ErrorMessage="Enter City." ValidationGroup="Architecture"></asp:RequiredFieldValidator>
</td>
<tr><td>State:</td>
<td colspan="2"><asp:DropDownList ID="ddlState" runat="server" CssClass="txtbox" Width="198px" /></td>
<td>&nbsp;&nbsp;PinCode:&nbsp; </td>
<td><asp:TextBox ID="txtPincode" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPincode" FilterType="Numbers" />
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPincode" ErrorMessage="PIN CODE limit exit." Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture" ValueToCompare="999999">*</asp:CompareValidator>
</td></tr></tr>
<tr><td>Phone:</td>
<td colspan="4"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"/>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers" />
&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox" Width="138px"></asp:TextBox>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers" />
</td></tr></table></asp:Panel></td>
<tr>
<td>Dispatch Date:</td>
<td><asp:TextBox ID="txtDDate" runat="server" CssClass="txtbox" AutoPostBack="True" ontextchanged="txtDDate_TextChanged"/>
<dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="cel2" PopupPosition="BottomRight" TargetControlID="txtDDate">
</dev:CalendarExtender>
<img src="../images/cal.png" id="cel2" runat="server"  alt="Cal" />
<asp:Label ID="lblExceptiondAte" runat="server" ForeColor="Maroon"></asp:Label>
</td></tr>
<tr><td>Courier Service :</td>
<td ><asp:LinkButton ID="btnNewCourierService" runat="server" Font-Bold="true" ForeColor="Red" OnClick="ibtnNewCourier_Onclick" Text="New Courier Service"/>
<br />
<asp:DropDownList ID="ddlCourierService" runat="server" CssClass="txtbox" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" Width="195px">
</asp:DropDownList>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Courier') ORDER BY Name"></asp:SqlDataSource>
</td>
<td>&nbsp;Reference No:&nbsp;&nbsp;</td>
<td><asp:TextBox ID="txtCourierNo" runat="server" AutoPostBack="true" CssClass="txtbox"  Width="100px"/></td>
</tr>
<tr><td></td>
<td align="left" colspan="3"><asp:Panel ID="panelCourier" runat="server" align="left" CssClass="expbox" Visible="false">
Name of Courier Serivce:&nbsp;<asp:TextBox ID="txtNewCourier" runat="server" CssClass="txtbox" Font-Bold="true" Width="200px"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNewCity" runat="server" CssClass="txtbox" Visible="false" Width="150px"/>
<br />
<asp:Label ID="lblExceptionNewCourier" runat="server" Font-Bold="true" ForeColor="Red"/>
<br /><br />
<asp:Button ID="btnSaveNewCourier" runat="server" OnClick="btnSAveNew_Onclick" Text="Save" />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencel" runat="server" OnClick="btnCencelnew_Onclick" Text="Close" />
</asp:Panel>
</td></tr>
<tr><td>Consignment No:</td>
<td><asp:TextBox ID="txtConsignment" runat="server" CssClass="txtbox" Width="198px"/></td><td>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Weight:</td>
<td><asp:TextBox ID="txtwtkg" runat="server" CssClass="txtbox" Width="51px"/>Kg 
    <asp:TextBox ID="txtwtgm" runat="server" CssClass="txtbox" Width="42px"/>gms</td>
</tr>
<tr><td>Remark:</td>
<td colspan="3"><asp:TextBox ID="txtRemoark" runat="server" CssClass="txtbox" Height="40px" TextMode="MultiLine" Width="198px"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRemoark" Display="Dynamic" ErrorMessage="Click to Create New Diary" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
</td></tr>
<tr><td><asp:LinkButton ID="ibtnGenDiary" runat="server" CssClass="redirecttab" ForeColor="Maroon" OnClick="ibtnGenDiary_ONClick" Text="New Diary" /></td>
<td colspan="3"><asp:TextBox ID="lblDiaryNo" runat="server" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon" Width="198px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="lblDiaryNo" Display="Dynamic" ErrorMessage="Click to Create New Diary" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>&nbsp;&nbsp;(Click to Create New Diary)</td>
</tr>
<tr><td></td><td align="center" colspan="3"><asp:Label ID="lblExcepitonDiary" runat="server" Font-Bold="true"/>
</td></tr>
<tr><td></td>
<td colspan="3">
<asp:Button ID="btnSaveDiary" runat="server" CssClass="btnsmall" OnClick="btnSAveDiray_Click" Text="Save" ValidationGroup="Architecture" />
&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnCancel" runat="server" CssClass="btnsmall" OnClick="btnCancel_Onclick" Text="Cancel" />
</td></tr>
</tr></table> 
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
<a ID="A1x0" href="javascript:toggleA1x('Div1x', 'A1x');"><img alt="Show" src="../images/minus.png" /></a>
</div>
<div style="padding:5px; color:White; font-size:15px;"><h3>Search Student/IMID</h3>
</div>
<center>
<br /><asp:Label ID="lblgh" runat="server" Visible="false"></asp:Label>
<asp:DropDownList ID="ddlSearchIn" runat="server" CssClass="txtbox" >
<asp:ListItem Text="IM" Value="IM" />
<asp:ListItem>Student</asp:ListItem>
</asp:DropDownList>
<asp:TextBox ID="txtNameSearch" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox>
<asp:Button ID="btnSearchDiary" runat="server" CssClass="btnsmall" Text="Search" onclick="btnSearchDiary_Click" />
</center>
</div>
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
<asp:Label ID="lblGridTitle" runat="server" ></asp:Label>
<br />
</div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
<div  id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px" onscroll='javascript:setScroll(this, <% =scrollPos.ClientID %> );'>
<asp:GridView ID="grvCourierHome" runat="server" PageSize="30" AllowPaging="True" 
        onpageindexchanging="grvCourierHome_PageIndexChanging" Width="100%" 
        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
        CellPadding="2" ForeColor="Black" GridLines="None" 
        HeaderStyle-HorizontalAlign="Center" 
        onrowdatabound="grvCourierHome_RowDataBound">
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
</asp:GridView>
</div>
<asp:Label ID="lblDiaryyHiddend" runat="server" Visible="false"/></div></div>
</ContentTemplate></asp:UpdatePanel>
</div></div>
</asp:Content>
   
   
   