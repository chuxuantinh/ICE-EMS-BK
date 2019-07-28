<%@ Page Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="AllocateProject.aspx.cs" Inherits="project_AllocateProject"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Copy Submission</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Project Copy Submission" CssClass="redirecttabhome"/> </td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="upanel" runat="server"><Triggers><asp:PostBackTrigger ControlID="btbClear" /></Triggers>
<ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Project Copy Submission</h1></div>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<center><p>Select Session and View Project Status:&nbsp;<b>ProformaBApproved</b></p>
Session:<asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox">
<asp:ListItem Text="Summer Examination" Value="Sum"/>
<asp:ListItem Text="Winter Examination" Value="Win"/>
</asp:DropDownList>&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" runat="server" Width="70px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox><br />
Project Status:&nbsp;&nbsp;<asp:DropDownList ID="ddlProjectStatus" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectStatus_OnSelectedINdexChanged"><asp:ListItem Text="ProformaBApproved" Value="ProformaBApproved" />
<asp:ListItem Text="CopyPending" Value="CopyPending" />
<asp:ListItem Text="CopySubmitted" Value="CopySubmitted" />
</asp:DropDownList>
<br />
<asp:Label ID="lblexception" runat="server" ForeColor="Red"/></center>
<br />
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
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
</div><h1>Membership No: &nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSIDView" Text="View" runat="server" OnClick="btnSIDView_Click" CssClass="btnsmall" /></h1>
<div id="Div1x" style="display: block;">
<div  id="divGridPrCpySub" style="width: 100%; overflow:scroll; height:180px;">
<asp:GridView ID="GridPrCpySub" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" onselectedindexchanged="GridPrCpySub_SelectedIndexChanged" Width="100%">
<Columns>
<asp:CommandField HeaderText="Select" ShowSelectButton="True" />
</Columns>
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center"/>
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView>
</div>
</div>
</div>
<hr />
<asp:Panel ID="Pnlresult" runat="server" Visible="False">
<center><asp:Label ID="lbldescription" runat="server"  Font-Bold="true"/><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br /><h3>Membership No.:&nbsp;&nbsp;<asp:Label ID="lblSID" runat="server" /></h3>
<asp:Panel ID="pnlComp" runat="server" Visible="false">
<table class="tbl">
<tr><td align="left">Diary No</td><td>:</td>
<td align="left"><asp:TextBox ID="txtDiary" runat="server" CssClass="txtbox" ForeColor="Maroon" /></td>
<td align="left">Submission Date:</td>
<td align="left"><asp:TextBox ID="txtcDate" runat="server" CssClass="txtbox" 
        Font-Bold="true" Width="100px" /><dev:CalendarExtender ID="txtcDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtcDate" /><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" controltovalidate="txtcDate" Display="Dynamic" errormessage="Insert Approval Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Group ID:</td><td>:</td><td align="left">GID:<asp:Label ID="lblGID" runat="server" Font-Bold="True" ForeColor="maroon" /><br /><asp:TextBox ID="txtgmate1" runat="server" CssClass="txtbox" ForeColor="GREEN" ReadOnly="True" /></td>
<td align="left"><b>GroupMate 2<br /><asp:TextBox ID="txtgmate2" runat="server" AutoPostBack="True" CssClass="txtbox"  BackColor="Yellow"  ontextchanged="txtgmate2_TextChanged" /></b></td>
<td align="left" colspan="2"><b>GroupMate 3</b><br /><b><asp:TextBox ID="txtgmate3" runat="server" AutoPostBack="True"  ForeColor="White"  BackColor="GrayText"  CssClass="txtbox"  ontextchanged="txtgmate3_TextChanged" /></b></td></tr>   
<tr><td colspan="4" style="text-align:center;"><asp:Label ID="errorgmate2" runat="server" ForeColor="Red"></asp:Label></td></tr><tr ><td></td><td></td>
<td align="left"><asp:TextBox ID="tgmate21" runat="server" CssClass="txtbox"  ReadOnly="True" BackColor="Yellow" /></td>
<td align="left"><b><asp:TextBox ID="tgmate22" runat="server"  CssClass="txtbox"  /></b></td>
<td align="left" colspan="2"><asp:TextBox ID="tgmate23" runat="server"  CssClass="txtbox"   BackColor="GrayText" ForeColor="White" /></td></tr>  
   <tr><td></td><td></td>
<td align="left"><asp:TextBox ID="tgmate31" runat="server" CssClass="txtbox" ReadOnly="True"   BackColor="GrayText"  ForeColor="White" /></td>
<td align="left"><b><asp:TextBox ID="tgmate32" runat="server" CssClass="txtbox"  BackColor="Yellow"/></b></td>
<td align="left" colspan="2"><b><asp:TextBox ID="tgmate33" runat="server" CssClass="txtbox"  /></b></td></tr>  
<tr><td>Project Title</td><td>:</td><td  align="left" colspan="2"><asp:TextBox ID="txtProjectTitle" runat="server" CssClass="txtbox" Height="50px" Width="300px" TextMode="MultiLine" ForeColor="Maroon" Font-Bold="true"></asp:TextBox></td></tr>

    <tr><td>No. Of Copies</td><td>:</td>
<td align="left"><asp:DropDownList ID="ddlCopy" runat="server" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon">
<asp:ListItem Text="0" />
<asp:ListItem Text="1" />
<asp:ListItem Text="2" Selected="True" />
<asp:ListItem Text="3" />
<asp:ListItem Text="4" />
</asp:DropDownList></td></tr>
<tr><td>Status</td><td>:</td><td align="left"><asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label><br /><asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox"><asp:ListItem Value="CopySubmitted" Text="CopySubmitted" /><asp:ListItem Value="CopyPending" Text="CopyPending" /><asp:ListItem Value="Reject" Text="Reject" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;</td></tr>
<tr><td align="left">Remarks</td><td align="left">:</td>
<td>Synopsis Remarks<br /><asp:TextBox ID="txtSynopsisRemarks" runat="server" TextMode="MultiLine" Height="50px" Width="100%" CssClass="txtbox" /></td>
<td>Project Remarks:<br /><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="50px" Width="100%" CssClass="txtbox" /></td>
</tr>
<tr><td>Project No.</td><td>:</td><td><asp:TextBox ID="lblProjectNo" CssClass="txtbox" runat="server" Font-Bold="true" ForeColor="Maroon" ></asp:TextBox> &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnNewProjectNo" runat="server" Text="New Project No" OnClick="lbtnNewProjectNo_OnClick"></asp:LinkButton> </td></tr>
</table></asp:Panel>
<br />
<asp:Label ID="lblexeption" runat="server" ForeColor="Red" Font-Bold="true"/><br /><br />
<asp:Button ID="btnSave" runat="server" Text="Submit" OnClientClick="return confirm('Confirm Submit Project Copy ?');" onclick="btnSave_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btbClear" runat="server" Text="Cancel" onclick="btbClear_Click" CssClass="btnsmall"/><br />
<p>Update Project Status from &nbsp;<b>ProformaBApproved</b> To <b>CopySubmitted</b> to </p>
</center><br />
</asp:Panel>
<asp:Label ID="lblSubm" runat="server" ForeColor="Green" Font-Bold="True" Font-Size="Medium"/></center>
<asp:Panel ID="pnlSpace" runat="server" Height="180px"/>
</ContentTemplate></asp:UpdatePanel>
</div><br />
</asp:Content>