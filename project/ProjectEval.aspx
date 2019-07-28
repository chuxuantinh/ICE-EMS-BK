<%@ Page Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ProjectEval.aspx.cs" Inherits="project_ProjectEval" Title="Project Evaluation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Evaluation</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"/></td><td><asp:Label ID="lblNext" runat="server" Text="Project Evaluation" CssClass="redirecttabhome"/></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="upanel" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Project Evaluation</h1></div><br />
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"/>
<center>Session:<asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"/><asp:ListItem Text="Winter Examination" Value="Win"/></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" runat="server" Width="70px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged" /><br /><br />
Membership No :&nbsp;<asp:TextBox ID="txtSid" runat="server" CssClass="txtbox" ForeColor="Maroon" Font-Bold="true" ontextchanged="txtSid_TextChanged" Width="80px" AutoPostBack="true"/><br /><br />
<asp:Label ID="lblexception" runat="server" ForeColor="Red"/><br /></center>
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
<div class="togalfees" style="width:100%"><div class="headerDivImgfees">
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>&nbsp;Project Evaluation</b><br /><br />
<div id="Div1x" style="display: block;"><div  id="divGridPrCpySub" style="width: 100%; overflow:scroll; height:180px;">
<asp:GridView ID="GridEval" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" Width="100%" onselectedindexchanged="GridEval_SelectedIndexChanged">
<Columns><asp:CommandField HeaderText="Select" ShowSelectButton="True" /></Columns>
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center"/>
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div></div><hr />
<asp:Panel ID="Pnlresult" runat="server" Visible="False"><center><h3 class="hl3"><asp:Label ID="lblprojecttitle" runat="server" /></h3>
<asp:Label ID="lbldescription" runat="server"  Font-Bold="true"/><br />
<asp:Panel ID="pnlGroupM" runat="server" CssClass="imbox" Width="400px">
<table class="tbl" width="95%">
<tr><td><asp:RadioButton ID="rgroupmate1" runat="server"  AutoPostBack="true" Text="GroupMate: #1" oncheckedchanged="rgroupmate1_CheckedChanged" GroupName="a" /></td><td>
<asp:RadioButton ID="rgroupmate2" runat="server" Text="GroupMate: #2" AutoPostBack="true"  oncheckedchanged="rgroupmate2_CheckedChanged" GroupName="a"/></td><td>
<asp:RadioButton ID="rgroupmate3" runat="server" Text="GroupMate: #3" AutoPostBack="true"  oncheckedchanged="rgroupmate3_CheckedChanged" GroupName="a"/></td></tr>
<tr><td>&nbsp;&nbsp;<asp:Label ID="lblgroupmate1" runat="server" Text="" Enabled="false" Font-Bold="true" ForeColor="#993300"/></td><td>
&nbsp;&nbsp;<asp:Label ID="lblgroupmate2" runat="server" Text="" Enabled="false" Font-Bold="true" ForeColor="#993300"/></td><td>
&nbsp;&nbsp; <asp:Label ID="lblgroupmate3" runat="server" Text="" Enabled="false" Font-Bold="true" ForeColor="#993300"/></td></tr>
</table></asp:Panel><br />
<asp:Panel ID="pnlComp" runat="server" Visible="false"><table class="tbl">
<tr><td align="left">Student Name</td><td>:</td><td align="left" colspan="2"><asp:Label ID="lblstuname" runat="server" Font-Bold="True" ForeColor="#993300" /></td><td align="left">IMID</td><td>:</td><td align="left"><asp:Label ID="lblIMID" runat="server" Font-Bold="True" ForeColor="#993300" /></td></tr>
<tr><td align="left">Stream</td><td>:</td><td align="left" colspan="2"><asp:Label ID="lblstream" runat="server"/></td><td align="left">Course</td><td>:</td><td align="left"><asp:Label ID="lblcourse" runat="server" Font-Bold="True" /></td></tr>
<tr><td align="left">Submission Date</td><td>:</td><td align="left" colspan="2"><asp:Label ID="lbldate" runat="server" Font-Bold="True"/></td><td align="left">Part</td><td>:</td><td align="left"><asp:Label ID="lblpart" runat="server" Font-Bold="True" /></td></tr>
<tr><td align="left">Evaluation Date</td><td>:</td><td align="left"><asp:TextBox ID="txtcDate" runat="server" CssClass="txtbox" Font-Bold="true" Width="100px" /><dev:CalendarExtender ID="txtcDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtcDate" /></td><td align="left"><img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" controltovalidate="txtcDate" Display="Dynamic" errormessage="Insert Approval Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator>&nbsp;</td><td align="left" colspan="3"><asp:Label ID="lblgroupmateid" runat="server" /></td></tr>
</table></asp:Panel><br />
<asp:Label ID="lblexeption" runat="server" ForeColor="Red" Font-Bold="true"/><br /><br />
<asp:Button ID="btnApprove" runat="server" Text="Approved" CssClass="btnsmall" onclick="btnApprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRej" runat="server" Text="Rejected" CssClass="btnsmall" onclick="btnRej_Click" /></center><br /></asp:Panel>
<asp:Label ID="lblSubm" runat="server" ForeColor="Green" Font-Bold="True" Font-Size="Medium"/></center><asp:Panel ID="pnlSpace" runat="server" Height="150px"/>
</ContentTemplate></asp:UpdatePanel>
</div><br /></asp:Content>