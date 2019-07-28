<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ApproveProfB.aspx.cs" Inherits="project_ApproveProfB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Proforma B
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Approve Proforma B" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdPnlComplete" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1>Approve Proforma B </h1></div><br />
<center>Select batch Session:&nbsp;Project Status: &nbsp;<b>ProformaBsubmitted</b> and Synopsis Status: <b>Submitted/Pending</b></center>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<center> Session:&nbsp;<asp:DropDownList ID="ddlsession" runat="server" AutoPostBack="true" CssClass="txtbox" onselectedindexchanged="ddlsession_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; 
<asp:TextBox ID="txtSession" runat="server" Width="72px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"/>
<br />
    <br />
<asp:Label ID="lblExceptionOK" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></center><br />
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
</div> <h1>Membership No: &nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSIDView" Text="View" runat="server" OnClick="btnSIDView_Click" CssClass="btnsmall" /></h1>
<div id="Div1x" style="display: block;">
<div  id="divApprProformaB" style="width: 100%; overflow:scroll; height:250px;">
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
<asp:GridView ID="GridShow" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" Width="100%"  onselectedindexchanged="GridShow_SelectedIndexChanged">
<Columns> <asp:CommandField ShowSelectButton="True" /></Columns>
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div>
</div></div><asp:Panel ID="pnlmain" runat="server"><asp:Panel ID="pnlINfo" runat="server">
<center><h3>GroupID <asp:Label ID="lblGID" runat="server"></asp:Label></h3></center>
<table class="tbl" style="background-color:#ffdead" width="95%"><tr><td>GroupMate1:</td><td>
<asp:Label ID="lblgmate" runat="server" Font-Bold="True" ForeColor="Maroon"/></td><td>Group Mates:&nbsp;<asp:ImageButton AlternateText="GroupMates" runat="server" ID="iResults"  ImageUrl="~/images/member.jpg" width="15px" Height="15px"/></td></tr>
<tr><td>Synopsis Title:</td><td><asp:Label ID="lblSynopsistitle" runat="server"/></td></tr>
<tr><td>Institution:</td><td>(<asp:Label ID="lblINstID" runat="server"/> )<asp:Label ID="lblINStname" runat="server"/></td></tr></table>
<asp:Panel ID="pnlgroup" runat="server" style="background-color:silver" ><table><tr> <td>
<table class="tbl"><tr><td>GroupMate2:</td><td><asp:Label ID="lblgmate1" runat="server" Font-Bold="True" ForeColor="Maroon"/></td></tr>
<tr><td>Synopsis Title:</td><td><asp:Label ID="lblSynopsisTitle1" runat="server"></asp:Label></td></tr>
<tr><td>Project Title:</td><td><asp:Label ID="lblProjectTitle1" runat="server"></asp:Label></td></tr>
<tr><td>IMID</td><td><asp:Label ID="lblIMID1" runat="server" /><asp:Label ID="lblCourse1" runat="server" /><asp:Label ID="lblPart1" runat="server" /></td></tr>
<tr><td>Institution:</td><td>(<asp:Label ID="lblInstID1" runat="server"/> )<asp:Label ID="lblINstName1" runat="server"/></td></tr></table></td>
<td><table class="tbl"><tr><td>GroupMate3:</td><td>
<asp:Label ID="lblgmate2" runat="server" Font-Bold="True" ForeColor="Maroon"/>></td></tr>
<tr><td>Synopsis Title:</td><td><asp:Label ID="lblSynopsisTitle2" runat="server"/></td></tr>
<tr><td>Project Title:</td><td><asp:Label ID="lblprojectTitle2" runat="server"/></td></tr>
<tr><td>IMID</td><td><asp:Label ID="lblIMID2" runat="server" />
<asp:Label ID="lblCourse2" runat="server"></asp:Label><asp:Label ID="lblPart2" runat="server"/></td></tr>
<tr><td>Institution:</td><td>(<asp:Label ID="lblINstID2" runat="server"/> )<asp:Label ID="lblINstName2" runat="server"/></td></tr></table></td>
</tr></table></asp:Panel><dev:PopupControlExtender ID="PopupControlExtender1" runat="server" Position="Center" OffsetX="-200" OffsetY="0" PopupControlID="pnlgroup" TargetControlID="iResults" ></dev:PopupControlExtender>
</asp:Panel>
<asp:Label ID="lblexcep" runat="server" ForeColor="Red" Font-Bold="true"/><br />
<center>Project Title:&nbsp;&nbsp;<asp:TextBox ID="lblprojecttitle" TextMode="MultiLine" Height="50px" runat="server" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon" Width="500px"/> &nbsp;&nbsp;<asp:LinkButton ID="lnlCheck" Text="Check" runat="server" ForeColor="Maroon" OnClick="lnKCheckButton_Click" Font-Bold="True" Font-Size="Small" Font-Underline="False" />
<div style="min-height:0px;max-height:200px;overflow:scroll;":><asp:GridView ID="grdChecktitle" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FFF1D4" />
    <SortedAscendingHeaderStyle BackColor="#B95C30" />
    <SortedDescendingCellStyle BackColor="#F1E5CE" />
    <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView></div>
<table class="tbl">
<tr><td>Description:</td><td colspan="2"><asp:TextBox ID="txtProjectDescription" runat="server" TextMode="MultiLine" Width="350px" Height="200px" CssClass="txtbox"></asp:TextBox></td></tr>
<tr><td>Letter Remarks:</td><td colspan="2"><asp:TextBox ID="txtLetterRemarsk" runat="server" CssClass="txtbox" Width="300px"></asp:TextBox></td></tr>
<tr><td align="left">Synopsis Status</td><td align="left"><asp:DropDownList ID="ddlSysStatus" runat="server" CssClass="txtbox" Width="130px" Font-Bold="True" ForeColor="Brown"><asp:ListItem Text="Approved"/><asp:ListItem Text="Rejected"/><asp:ListItem Text="Pending"></asp:ListItem></asp:DropDownList></td><td>Synopsis ApproveDate:</td><td align="left"><b><asp:TextBox ID="txtDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDate" PopupButtonID="cal1"/></b>&nbsp;<img src="../images/cal.png" id="cal1" runat="server"  alt="Cal" /></td></tr>
<tr><td align="left">Remarks</td><td colspan="2" align="left">Synopsis Remarks<br /><asp:TextBox ID="txtSynRemarks" runat="server" TextMode="MultiLine" Height="50px" Width="100%" CssClass="txtbox" /></td><td align="left" colspan="2">Project Remarks:<br /><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="50px" Width="100%" CssClass="txtbox" /></td></tr>
</table>
<center><asp:Button ID="btnApprove" runat="server" CssClass="btnsmall" onclick="btnApprove_Click" Text="Approve" />&nbsp;<br />
<p>Update Project Status form <b>ProformaBSubmitted </b> To &nbsp;<b>ProformaBApproved</b></p>
</td></tr></table><br /><br /></center></asp:Panel><br />
</ContentTemplate></asp:UpdatePanel>
<asp:Panel runat="server" ID="pnlspc" Height="100px"/>
</div><br />
</asp:Content>