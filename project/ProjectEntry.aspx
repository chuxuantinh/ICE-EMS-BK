<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ProjectEntry.aspx.cs" Inherits="project_ProjectEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Project Entry" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
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
</div><div style="padding:5px; color:White; font-size:15px;"><b>&nbsp;Student Membership No:&nbsp;<asp:TextBox ID="txtSid" runat="server" AutoPostBack="true" OnTextChanged="txtSID_OnTextChanted"  CssClass="txtbox" ForeColor="Brown" Font-Bold="true" /></b><br /><br />
<div id="Div1x" style="display: block;">
<div  id="divGridPrCpySub" style="width: 100%; overflow:scroll; height:80px;">
<asp:GridView ID="GridEval" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" Width="100%" onselectedindexchanged="GridEval_SelectedIndexChanged">
<Columns>
<asp:CommandField HeaderText="Select" ShowSelectButton="True" />
</Columns>
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center"/>
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div></div><hr />
<table class="tbl"><tr><td align="left" colspan="5"><strong>PROFORMA A</strong></td></tr>
<tr><td align="right">IMID:</td><td><asp:Label ID="lblIMID" runat="server" Font-Bold="true" ForeColor="Brown" /></td></tr>
<tr><td>Student Name:</td><td colspan="4"><asp:Label ID="lblStuName" runat="server" Font-Bold="true" ForeColor="Brown" /></td></tr>
<tr><td>Course:</td><td colspan="1"><asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" ForeColor="Brown" Font-Bold="true" Width="120px"><asp:ListItem Value="Civil">Civil</asp:ListItem><asp:ListItem Value="Architecture">Architecture</asp:ListItem></asp:DropDownList></td><td>Part:&nbsp;<asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" ForeColor="Brown" Font-Bold="true" Width="90px"><asp:ListItem Value="PartII">Part II</asp:ListItem><asp:ListItem Value="SectionB">Section B</asp:ListItem></asp:DropDownList></td><td align="right">Synopsis Date:</td><td><asp:TextBox ID="txtSynDate" runat="server" CssClass="txtbox" Width="90px" /><dev:CalendarExtender ID="cal1" TargetControlID="txtSynDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtSynDate"/></td></tr>
<tr><td align="left">Diary No(A):</td><td colspan="2"><asp:TextBox ID="txtDA" runat="server" CssClass="txtbox" Width="90px" /></td><td align="right">Group ID:</td><td><asp:TextBox ID="txtGP" Enabled="false" runat="server" CssClass="txtbox" Width="90px" /></td></tr>
<tr><td>Group Mate 1:</td><td><asp:TextBox ID="txtGm1" runat="server" CssClass="txtbox" Width="90px" /></td><td align="right">GroupMate 2:</td><td colspan="2"><asp:TextBox ID="txtGm2" runat="server" CssClass="txtbox" Width="90px" /></td></tr>
<tr><td>GroupMate 3:</td><td><asp:TextBox ID="txtGm3" runat="server" CssClass="txtbox" Width="90px" /></td><td align="right">AICTE Ins. ID:</td><td><asp:TextBox ID="txtInstID" runat="server" CssClass="txtbox" Width="90px" /></td></tr>
<tr><td align="left">AICTE Institute:</td><td colspan="4"><asp:DropDownList ID="ddlOpn4" runat="server" CssClass="txtbox" Width="550px" ForeColor="Brown" Font-Bold="true"/></td></tr>
<tr><td align="left">Option (1):</td><td colspan="4"><asp:DropDownList ID="ddlOpn1" runat="server" CssClass="txtbox" Width="550px" Font-Bold="true"/></td></tr>
<tr><td align="left">Option (2):</td><td colspan="4"><asp:DropDownList ID="ddlOpn2" runat="server" CssClass="txtbox" Width="550px" Font-Bold="true"/></td></tr>
<tr><td align="left">Option (3):</td><td colspan="4"><asp:DropDownList ID="ddlOpn3" runat="server" CssClass="txtbox" Width="550px" Font-Bold="true"/></td></tr>
<tr><td align="left">Synopis Title:</td><td colspan="4"><asp:TextBox ID="txtSynTtl" runat="server" CssClass="txtbox" Width="550px" /></td></tr>
<tr><td>Course Status:</td><td><asp:DropDownList ID="ddlCourseStatus" runat="server" CssClass="txtbox" ForeColor="Brown" Font-Bold="true" Width="140px"><asp:ListItem>FinalPass</asp:ListItem><asp:ListItem>Promotted</asp:ListItem><asp:ListItem>AfterRechecking</asp:ListItem></asp:DropDownList></td><td align="right">Synopsis Remarks:</td><td colspan="2"><asp:TextBox ID="txtSynRemark" runat="server" CssClass="txtbox" Height="30px" TextMode="MultiLine" Width="150px" /></td></tr>
<tr><td align="left" colspan="5"><strong>PROFORMA B</strong></td></tr>
<tr><td>Diary No(B):</td><td><asp:TextBox ID="txtDB" runat="server" CssClass="txtbox" Width="100px" /></td><td align="right">&nbsp;</td><td colspan="2">&nbsp;</td></tr>
<tr><td>Project No:</td><td><asp:TextBox ID="txtProNo" runat="server" CssClass="txtbox" Width="90px" /></td><td align="right">&nbsp;Duration:</td><td colspan="2"><asp:TextBox ID="txtDuration" runat="server" CssClass="txtbox" /></td></tr>
<tr><td>Project Title:</td><td colspan="4"><asp:TextBox ID="txtProTtl" runat="server" CssClass="txtbox" Width="550px" /></td></tr>
<tr><td>Description:</td><td><asp:TextBox ID="txtDes" runat="server" CssClass="txtbox" Height="31px" TextMode="MultiLine" Width="200px" /></td><td align="right">Remarks:</td><td colspan="2"><asp:TextBox ID="txtRemark" runat="server" CssClass="txtbox" Height="31px" TextMode="MultiLine" Width="150px" /></td></tr>
<tr><td>Project Date:</td><td><asp:TextBox ID="txtProDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="Cal2" TargetControlID="txtProDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtProDate"/></td><td align="right">Project Approve Date:</td><td colspan="2"><asp:TextBox ID="txtProAppDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="cal3" TargetControlID="txtProAppDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtProAppDate"/></td></tr>
<tr><td>Letter Remarks:</td><td><asp:TextBox ID="txtLetRemark" runat="server" CssClass="txtbox" Height="51px" TextMode="MultiLine" Width="200px" /></td><td align="right">Letter Issue Date:</td><td colspan="2"><asp:TextBox ID="txtLetIsDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="cal4" TargetControlID="txtLetIsDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtLetIsDate"/></td></tr>
<tr><td>Approval Fees:</td><td><asp:TextBox ID="txtAppFees" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FAppFees" runat="server" TargetControlID="txtAppFees" FilterType="Numbers" /></td><td align="right">Evaluation Fees:</td><td colspan="2"><asp:TextBox ID="txtEvalFees" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FEvalFees" runat="server" TargetControlID="txtEvalFees" FilterType="Numbers" /></td></tr>
<tr><td>Training Fees:</td><td><asp:TextBox ID="txtTraFees" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FTransFees" runat="server" TargetControlID="txtTraFees" FilterType="Numbers" /></td><td align="right">Guidence Fees:</td><td colspan="2"><asp:TextBox ID="txtGuidFees" runat="server" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FGuidFees" runat="server" TargetControlID="txtGuidFees" FilterType="Numbers" /></td></tr>
<tr><td align="center" colspan="5"><strong>-- COPY Submission and Dispatch --</strong></td></tr>
<tr><td>No Of Copies:</td><td><asp:DropDownList ID="ddlCopies" runat="server" CssClass="txtbox" Width="60px" ForeColor="Brown" Font-Bold="true"><asp:ListItem>0</asp:ListItem><asp:ListItem>1</asp:ListItem><asp:ListItem>2</asp:ListItem><asp:ListItem>3</asp:ListItem><asp:ListItem>4</asp:ListItem></asp:DropDownList></td><td align="right">Dispatch No:</td><td colspan="2"><asp:TextBox ID="txtDNo" runat="server" CssClass="txtbox" /></td></tr>
<tr><td>Send Date:</td><td><asp:TextBox ID="txtSenDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="cal6" TargetControlID="txtSenDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtSenDate"/></td><td align="right">Copy Submit Date:</td><td colspan="2"><asp:TextBox ID="txtCpySubDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="cal5" TargetControlID="txtCpySubDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtCpySubDate"/></td></tr>
<tr><td>Grade Date:</td><td><asp:TextBox ID="txtGDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="cal7" TargetControlID="txtGDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtGDate"/></td><td align="right">Grade:</td><td colspan="2"><asp:DropDownList ID="ddlGrade" runat="server" CssClass="txtbox" Width="60px" ForeColor="Brown" Font-Bold="true"><asp:ListItem>N/A</asp:ListItem><asp:ListItem>A</asp:ListItem><asp:ListItem>A+</asp:ListItem><asp:ListItem>B</asp:ListItem><asp:ListItem>B+</asp:ListItem><asp:ListItem>Absent</asp:ListItem></asp:DropDownList></td></tr>
<tr><td align="left" colspan="5"><strong>PROFORMA C</strong></td></tr>
<tr><td>Evaluation Date:</td><td><asp:TextBox ID="txtEvalDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="cal8" TargetControlID="txtEvalDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="txtEvalDate"/></td><td align="right">Diary No(C):</td><td colspan="2"><asp:TextBox ID="txtDC" runat="server" CssClass="txtbox" Width="100px" /></td></tr>
<tr><td>Status:</td><td><asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" Width="170px" ForeColor="Brown" Font-Bold="true"><asp:ListItem>Selected</asp:ListItem><asp:ListItem>ProformaASubmitted</asp:ListItem><asp:ListItem>ProformaAApproved</asp:ListItem><asp:ListItem>ProformaBSubmitted</asp:ListItem><asp:ListItem>ProformaBApproved</asp:ListItem><asp:ListItem>CopySubmitted</asp:ListItem><asp:ListItem>CopyPending</asp:ListItem><asp:ListItem>CopyDispatched</asp:ListItem><asp:ListItem>Approved</asp:ListItem><asp:ListItem>Rejected</asp:ListItem></asp:DropDownList></td><td align="right">Synopsis Status:</td><td colspan="2"><asp:DropDownList ID="ddlSynStatus" runat="server" CssClass="txtbox" Width="170px" ForeColor="Brown" Font-Bold="true"><asp:ListItem>NotSubmitted</asp:ListItem><asp:ListItem>Submitted</asp:ListItem><asp:ListItem>Approved</asp:ListItem><asp:ListItem>ReSubmit</asp:ListItem><asp:ListItem>Rejected</asp:ListItem></asp:DropDownList></td></tr>
</table><br /><br />
<center><asp:Button ID="btnSave" runat="server" Text="Insert Data" CssClass="btnsmall" onclick="btnSave_Click" /><br />
<asp:Label ID="lblException" runat="server" ForeColor="Brown" />
<br /><br /></center>
</div><br />
</asp:Content>