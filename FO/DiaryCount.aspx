<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="DiaryCount.aspx.cs" Inherits="FO_DiaryCount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Courier/Diary Supply To Department ICE(I)</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanger1" runat="server"/>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"/></td><td>
</td><td><asp:Label ID="lblCourierHome" runat="server" Text="Letter/ Diary Entry" CssClass="redirecttabhome"/></td></tr></table>
</div>
<script type="text/javascript">
    function selectAlll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    } 

</script>
<script type="text/javascript" language="javascript">
    function ConfirmApp() {
        if (confirm("Are you sure you want to Receive selected diary?") == true)
            return true;
        else
            return false;
    }
    </script> 
<div id="rightpanel2"><div id="header">
<asp:UpdatePanel ID="updatediray" runat="server" ><ContentTemplate>
<div id="Div1" class="fromRegisterlbl" runat="server" ><h1>Diary/ Letter Entry</h1></div>
<br /><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"/>
<table class="tbl">
<tr><td><input id="Hidden1" runat="server" type="hidden" value="0" />
<div id="div3" style="width: 100%; overflow:scroll; height:150px">
<asp:GridView ID="GrdCountReceived" runat="server" HorizontalAlign="Center" 
        onselectedindexchanged="GridDiaryNo_SelectedIndexChanged" Width="200px" 
        PageSize="100"  CellPadding="4" ForeColor="#333333" GridLines="None" 
        onrowdatabound="GrdCountReceived_RowDataBound">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
<EmptyDataTemplate><center>No Records Found!</center></EmptyDataTemplate>
<EmptyDataRowStyle BackColor="Cyan" ForeColor="#333333" />
<Columns>
        <asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
</Columns>
<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div></td><td><asp:Button ID="btnReceive" runat="server" Text="Receive" 
            CssClass="btnsmall" onclick="btnReceive_Click"  OnClientClick="return ConfirmApp();"/></td><td><input id="scrollPos4" runat="server" type="hidden" value="0" />
<div id="div2" style="width: 100%; overflow:scroll; height:150px">
<asp:GridView ID="GridDiaryNo" runat="server" HorizontalAlign="Center" 
        onselectedindexchanged="GridDiaryNo_SelectedIndexChanged" Width="200px" 
        PageSize="100"  CellPadding="4" ForeColor="#333333" GridLines="None" 
        onrowdatabound="GridDiaryNo_RowDataBound">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
<EmptyDataTemplate><center>No Records Found!</center></EmptyDataTemplate>
<EmptyDataRowStyle BackColor="Cyan" ForeColor="#333333" />
<Columns>
<asp:CommandField ShowSelectButton="True" SelectText="Select" HeaderText="Select" />
</Columns>
<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div></td></tr></table><table width="98%">
<tr><td>
<table class="tbl">
<tr><td>Select Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" onselectedindexchanged="ddlExamSeason_SelectedIndexChanged"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td>
<td>
    <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="80px" 
        AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" 
        Height="16px"/></td></tr>
<tr><td>Diary No.:</td><td><asp:TextBox ID="txtDiaryNo" runat="server" 
        CssClass="txtbox" Width="130px" AutoPostBack="true" 
        OnTextChanged="txtDiaryNo_TextChanged"/></td></tr>
  <tr><td>Submit To:</td><td colspan="3"><asp:DropDownList ID="ddlSupplyTo" 
          runat="server"  Width="198px" CssClass="txtbox" AutoPostBack="True" 
          onselectedindexchanged="ddlSupplyTo_SelectedIndexChanged1">
<asp:ListItem Text="Account" Value="Account" />
<asp:ListItem Text="Examination" Value="Examination"/>
<asp:ListItem Text="Project" Value="Project"/>
<asp:ListItem Text="Membership" Value="Director"/>
<asp:ListItem Text="Inventory" Value="Inventory"/>
<asp:ListItem Text="Front Office" Value="FrontOffice" />
<asp:ListItem Text="Student" Value="Student" />
<asp:ListItem Text="Chairman" Value="ChairMan"/>
<asp:ListItem Text="Director" Value="Director"/>
<asp:ListItem Value="ExDirector" Text="Exe-Director" />
<asp:ListItem Text="Secretary General" Value="Secretary" />
<asp:ListItem Value="PRO" Text="PRO" />
      <asp:ListItem Value="AdminOfficer" Text="AdminOfficer"></asp:ListItem>
</asp:DropDownList>
</td><td><asp:Label ID="lblRemarks" Text="Diary Type" runat="server" />:</td>
    <td>
<asp:DropDownList ID="ddlDiaryType" runat="server" CssClass="txtbox" Width="162px" 
            onselectedindexchanged="ddlDiaryType_SelectedIndexChanged" AutoPostBack="True">
<asp:ListItem Value="select" Text="--Select--"></asp:ListItem>
<asp:ListItem Value="Latters" Text="LETTERS"/>
<asp:ListItem Text="FORMS" Value="Forms" />
    <asp:ListItem></asp:ListItem>
</asp:DropDownList>
</td></tr>
<tr><td>Date:</td><td colspan="3"><asp:Label ID="txtDate" runat="server"/></td>
  <td><asp:Label ID="lblIm" runat="server" Visible="false" Text="IMID:"/><asp:Label ID="lblIMID" Text="" runat="server" /></td></tr>
</table>
<center><h3><asp:Label ID="lblExceptionDiary" runat="server" ForeColor="#CC3300"/></h3></center>
</td>
<td>
</td></tr></table>
<asp:Panel ID="panDiary" runat="server" Width="100%" >
<div style="margin-left: 14px; color:#800000;"><h3>DD/Forms Entry:-</h3></div>
<table class="tbl">
<tr><td ><asp:Label ID="lblDD" Text="Total No. of DD:" runat="server" /></td>
<td ><asp:TextBox ID="txtDD" runat="server" CssClass="txtbox" Width="130px"/><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtDD"/></td>     
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblForm" Text="Total No. of Forms:" runat="server" /></td><td>
<asp:TextBox ID="txtForms" runat="server" CssClass="txtbox" Width="130px"/><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtForms"/></td>
<td><asp:Button ID="btnSubmit" runat="server" CssClass="btnsmall" Font-Size="Small" onclick="btnSubmit_Click" Text="Submit" ValidationGroup="Letter5" /></td>
</tr></table></asp:Panel>
<script>
    function toggleA1(showHideDiv, switchImgTag) {
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
    <br />
<asp:Panel ID="panelLetter" runat="server" Width="100%" >
<div  style="margin-left: 14px; color: #800000;">
<h3>Letter Entry:</h3>
</div>
<table class="tbl">
<tr><td>Letter From:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td><td> <asp:TextBox ID="txtLetterFrom" runat="server" 
        CssClass="txtbox" Width="130px"></asp:TextBox> </td>
<td>Subject:</td><td><asp:TextBox ID="txtSubject" runat="server" 
        CssClass="txtbox" Width="130px"/></td>
<td><asp:Button ID="btnLetterSubmit" runat="server" CssClass="btnsmall" Font-Size="Small" OnClick="btnLetterSubmit_Onclick" Text="Submit" ValidationGroup="Letter" /></td>
</tr></table></asp:Panel>
    <br />
    <asp:Panel ID="pnlcr" runat="server">
        <div class="togalfees" style="width: 100%">
            <div class="headerDivImgfees">
                <a ID="A1" href="javascript:toggleA1('DivAx', 'A1');">
                <img alt="Show" src="../images/plus.png"></img></a><br />
            </div>
            <h1>
                Enter Diary No. &nbsp; <asp:TextBox ID="txtDiaryNoUp" runat="server" AutoPostBack="true" 
                                CssClass="txtbox" ontextchanged="txtDiaryNoUp_TextChanged" Width="100px" /></h1>
            <div ID="DivAx" style="display: Block;">
                <br />
                <table class="tbl">
                    <tr>
                           <td>Submit To:</td><td>
                           <asp:DropDownList ID="ddlSubmitUpdate" 
          runat="server"  Width="198px" CssClass="txtbox" AutoPostBack="True" 
                               onselectedindexchanged="ddlSubmitUpdate_SelectedIndexChanged">
<asp:ListItem Text="Account" Value="Account" />
<asp:ListItem Text="Examination" Value="Examination"/>
<asp:ListItem Text="Project" Value="Project"/>
<asp:ListItem Text="Membership" Value="Membership"/>
<asp:ListItem Text="Inventory" Value="Inventory"/>
<asp:ListItem Text="Front Office" Value="FrontOffice" />
<asp:ListItem Text="Student" Value="Student"/>
<asp:ListItem Text="Chairman" Value="ChairMan"/>
<asp:ListItem Text="Director" Value="Director" />
<asp:ListItem Value="ExDirector" Text="Exe-Director"/>
<asp:ListItem Text="Secretary General" Value="Secretary"/>
<asp:ListItem Text="PRO" Value="PRO" />
      <asp:ListItem Value="AdminOfficer" Text="AdminOfficer"></asp:ListItem>
                           <asp:ListItem>N/A</asp:ListItem>
                           </asp:DropDownList><asp:Label ID="lblIMIDUp" runat="server" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDDUp" runat="server" Text="Total No of DD:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDDUp" runat="server" CssClass="txtbox" Width="100px" />
                            <dev:FilteredTextBoxExtender ID="filtercurrentcy" runat="server" 
                                FilterType="Numbers" TargetControlID="txtDDUp" />
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Label ID="lblFormUp" runat="server" Text="Total No of Forms:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtFormsUp" runat="server" CssClass="txtbox" Width="100px" />
                            <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                FilterType="Numbers" TargetControlID="txtFormsUp" />
                        </td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btnsmall" Font-Size="Small" 
                                onclick="btnUpdate_Click" Text="Update" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
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
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>&nbsp;&nbsp; Letter View</b><br />
        <br />
            <div ID="Div1x" style="display: block;">
        <div ID="divdatagrid1" style="width: 100%; overflow:scroll; height:120px;">
            <asp:GridView ID="GridLetFrom" runat="server" AllowPaging="True" 
                AutoGenerateColumns="true" BackColor="White" BorderColor="#E7E7FF" 
                BorderStyle="None" BorderWidth="1px" CellPadding="8" CellSpacing="8" 
                EmptyDataText="N/A" GridLines="Horizontal" HorizontalAlign="Center" 
                ShowHeaderWhenEmpty="true" Width="100%" 
                onrowdatabound="GridLetFrom_RowDataBound">
                <EmptyDataRowStyle HorizontalAlign="Center" width="100%" />
                <EmptyDataTemplate>
                    <center>
                        Membership Record Not found !</center>
                </EmptyDataTemplate>
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" 
                    HorizontalAlign="Center" />
                <EditRowStyle HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
        </div>
</div>
    </div></div>
</ContentTemplate></asp:UpdatePanel>
</div></div>
</asp:Content>
