<%@ Page Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="AppEdit.aspx.cs" Inherits="Acc_AppEdit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Edit Application Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblAppEdit" runat="server" Text="Application Forms Edit" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Edit Count&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Update Application Forms</h1></div><br />
<asp:Panel ID="pnlEdit" runat="server">
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>
<center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="80px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></center>
      <center>Form Type:&nbsp;&nbsp;<asp:DropDownList ID="ddlFormType" runat="server" CssClass="txtbox"><asp:ListItem Text="Exam" Value="Exam" /><asp:ListItem Text="Admission" Value="Admission"></asp:ListItem><asp:ListItem Value="ReAdmission" Text="ReAdmission" /></asp:DropDownList>Membership No:&nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" ></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" onClick="btnView_Click" Text="View" CssClass="btnsmall" /></center>  
        <script>
            function toggleA1w(showHideDiv, switchImgTag) {
                var ele = document.getElementById(showHideDiv);
                var imageEle = document.getElementById(switchImgTag);
                var imageEle = document.getElementById(switchImgTag);
                if (ele.style.display == "block")
                {
                    ele.style.display = "none";
                    imageEle.innerHTML = '<img src="../images/plus.png">';
                }
                else 
                {
                    ele.style.display = "block";
                    imageEle.innerHTML = '<img src="../images/minus.png">';
                }
            }
 </script>
 <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
        &nbsp;&nbsp;&nbsp;&nbsp;<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div>
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll;">
    <asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" OnRowDataBound="GridAppTable_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5"  OnSelectedIndexChanged="GridAppTable_OnSelectedIndexChanged"
        CellSpacing="5" Width="100%">
        <Columns>
            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
   </div>
</div>
</div><center>
<asp:Panel ID="panelFee1" runat="server">
<b><asp:Label runat="server" ID="lblID" ></asp:Label>:&nbsp;<asp:Label ID="lblName" runat="server"></asp:Label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Application Status:&nbsp;<asp:Label ID="lblAppsStatus" runat="server"></asp:Label><br />
Total Amount:&nbsp;&nbsp;<asp:Label ID="lblTAmount" runat="server"></asp:Label><br />
<asp:CheckBox ID="chkAdmissinForm" runat="server" Text="Admission"  Enabled="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
<asp:CheckBox ID="chkExamForm" runat="server" Text="Exam"  Enabled="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
<asp:CheckBox ID="chkRedmission" runat="server" Text="ReAdmission"  Enabled="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
<asp:CheckBox ID="chkITI" runat="server" Text="ITI"  Enabled="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
<br /></asp:Panel>
<asp:Panel ID="panelFee2" runat="server">
<asp:CheckBox ID="chkComposite" runat="server" Text="Composite Fees"  Enabled="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
<asp:CheckBox ID="chkASF" runat="server" Text="ASF"  Enabled="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
<asp:CheckBox ID="chkExmp" runat="server" Text="Exemption Fees"  Enabled="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
</asp:Panel>
<br />
Updated Course:&nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil" /><asp:ListItem Value="Architecture" Text="Architecture" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;Part:&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlPart" CssClass="txtbox">
<asp:ListItem Value="PartI" Text="PartI"></asp:ListItem>
<asp:ListItem Value="PartII" Text="PartII"></asp:ListItem>
<asp:ListItem Value="SectionA" Text="SectionA"></asp:ListItem>
<asp:ListItem Value="SectionB" Text="SectionB"></asp:ListItem>
</asp:DropDownList>
<hr />
<asp:Label ID="lblException" runat="server"></asp:Label>
<hr />
Remarks:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRemarks" runat="server" Height="30px" TextMode="MultiLine" Width="200px"></asp:TextBox>
<br /><br />
<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btnsmall" /><br /><br />
</center>
 </asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<asp:Panel ID="pnlSpace" runat="server" Height="300px"></asp:Panel>
</div><br />
</asp:Content>

