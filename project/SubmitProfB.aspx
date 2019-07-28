<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="SubmitProfB.aspx.cs" Inherits="project_SubmitProfB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Proforma B
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Submit Proforma B" CssClass="redirecttabhome"/></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Submit Proforma B [Synopsis Submission]</h1></div>
<center><p>Select Session to View Project Status:&nbsp;<b>ProformaAApproved</b> and Synopsis Status:<b> NotSubmitted/Pending</b></p></center>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<center>Status:&nbsp;<asp:DropDownList ID="ddlProfBStatus" runat="server" CssClass="txtbox"><asp:ListItem>ProformaBSubmitted</asp:ListItem><asp:ListItem>ProformaBApproved</asp:ListItem></asp:DropDownList>&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlsession" runat="server" AutoPostBack="true" CssClass="txtbox" onselectedindexchanged="ddlsession_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" runat="server" Width="72px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"/>
&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btnsmall" /><br />
<asp:Label ID="lblExceptionOK" runat="server" Font-Bold="True" ForeColor="Red" /></center><br />
<script>
    function toggleA1x(showHideDiv, switchImgTag) 
    {
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
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"/></a>
</div>
<h1>Membership No: &nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSIDView" Text="View" runat="server" OnClick="btnSIDView_Click" CssClass="btnsmall" /></h1>
<div id="Div1x" style="display:none;">
<div  id="divApprProformaB" style="width: 100%; overflow:scroll; height:280px;">
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
<Columns> 
<asp:CommandField ShowSelectButton="True" /> 
</Columns>
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div>
<asp:Panel ID="pnlmain" runat="server">
<asp:Panel ID="pnlINfo" runat="server"><center>
<table class="tbl">
<tr><td align="left">Student Name</td><td align="left">:</td><td align="left"><asp:Label ID="lblName" runat="server" ForeColor="maroon" Font-Bold="true"/></td>
<td align="left">Synopsis Status</td><td align="left">:</td><td align="left"><asp:Label ID="lblSynopsisStatus" runat="server" ForeColor="Brown" Font-Bold="True"/></td></tr>
<tr><td align="left"><b>Course</b></td><td align="left">:</td><td align="left"><b><asp:Label ID="lblCourse" runat="server" Font-Bold="True" />-<asp:Label ID="lblpart" runat="server" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></td>
<td align="left">Proforma B Fees:</td><td align="left">:</td><td align="left"><b><asp:Label ID="lblProformaBFees" runat="server" /></b></td></tr>
<tr><td align="left">Institute Alloted</td><td align="left">:</td><td align="left" colspan="4"><b>(<asp:Label ID="lblInstID" runat="server" ForeColor="Maroon" />)<asp:Label ID="lblINstName" runat="server" ForeColor="Maroon"/></b></td></tr>
</table></center></asp:Panel><br />
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
&nbsp;&nbsp;&nbsp;&nbsp;<a ID="A1x3" href="javascript:toggleA1x('Div1x', 'A1x3');"></a> </div><div style="padding:5px;"><asp:Label ID="Label2" runat="server" Text="Submit Proforma B" style="color:White;font-size:15px;"></asp:Label><br /><br /><br />
<div id="Div1" style="display: block;">
<asp:Panel ID="pnlUpdate" runat="server"><center>
<table class="tbl">
<tr><td align="left">DiaryNo</td><td align="left">:</td><td align="left" colspan="1"><asp:TextBox ID="txtDNo" runat="server" CssClass="txtbox" /></td><td align="right">&nbsp;</td></tr>
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
<tr><td align="left">Synopsis Title</td><td align="left">:</td>
<td colspan="2" align="left"><asp:TextBox ID="lblSynopsisTtl" runat="server" TextMode="MultiLine" Height="50px" CssClass="txtbox" Width="450px" ForeColor="Maroon" Font-Bold="true" /></td><td><img id="Img1" runat="server" alt="Add Same Project Title" src="~/images/AddTitle.png" title="Add Same Project Title" /><asp:LinkButton ID="lnkAdd" Text="Add Same Project Title" runat="server" OnClick="lnkAdd_Click" ForeColor="Blue" Font-Bold="True" Font-Size="Small" Font-Underline="False" /></td></tr>
<tr><td align="left">&nbsp;</td><td align="left">&nbsp;</td><td align="left" colspan="3"><img id="lnkAddnew" runat="server" alt="Add New Synopsis Title" src="~/images/AddTitle.png" title="Add New Synopsis Title" /><b>Add New Synopsis Title</b></td></tr>
<tr><td align="left">Project Title</td><td align="left">:</td><td colspan="2" align="left"><asp:TextBox ID="lblprojecttitle" TextMode="MultiLine" Height="50px" runat="server" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon" Width="450px"/></td><td><asp:LinkButton ID="lnlCheck" Text="Check" runat="server" ForeColor="Blue" OnClick="lnKCheckButton_Click" Font-Bold="True" Font-Size="Small" Font-Underline="False" /></td></tr>
<tr><td colspan="5"><div style="min-height:0px;max-height:200px;overflow:scroll; width:100%;">
    <asp:GridView ID="grdChecktitle" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FFF1D4" />
    <SortedAscendingHeaderStyle BackColor="#B95C30" />
    <SortedDescendingCellStyle BackColor="#F1E5CE" />
    <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView></div></td></tr>
<tr><td align="left">Description</td><td align="left">:</td><td colspan="2" align="left"><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="100px" Width="300px" CssClass="txtbox" /></td></tr>
<tr><td>Synopsis Status</td><td>:</td><td  colspan="2"><asp:DropDownList ID="ddlSynopsisStatus" runat="server" CssClass="txtbox" onselectedindexchanged="ddlSynopsisStatus_SelectedIndexChanged" AutoPostBack="true"><asp:ListItem Value="Submitted" Text="Submitted" /><asp:ListItem Value="Pending" Text="Pending" /><asp:ListItem Value="Approved" Text="Approved" /><asp:ListItem Value="Rejected" Text="Rejected" /><asp:ListItem Value="Submitted" Text="Submitted"></asp:ListItem></asp:DropDownList>&nbsp;Letter Issue Date:&nbsp;&nbsp;<asp:TextBox ID="txtLetterIssueDate" runat="server" CssClass="txtbox" ></asp:TextBox>&nbsp;&nbsp;<img id="Img2" runat="server" alt="Cal" src="../images/cal.png" /><dev:CalendarExtender ID="CalendarExtender4" TargetControlID="txtLetterIssueDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="Img2" /></td>
<td align="right"><asp:label ID="lblSyn" runat="server"/>&nbsp;:&nbsp;<b><asp:TextBox ID="txtDate" runat="server" CssClass="txtbox"/><dev:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="cal1" /><asp:TextBox ID="txtProAppDate" runat="server" CssClass="txtbox"/><dev:CalendarExtender ID="CalendarExtender2" TargetControlID="txtProAppDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="cal2" /></b></td>
<td align="left"><img id="cal1" runat="server" alt="Cal" src="../images/cal.png" /><img id="cal2" runat="server" alt="Cal" src="../images/cal.png" /></td></tr>
<tr><td>Project No</td><td>:</td><td><asp:TextBox ID="txtProjectNO" runat="server" CssClass="txtbox" Font-Bold="true" ForeColor="Maroon" /><asp:LinkButton ID="lbtnNewProjectNo" runat="server" OnClick="lbtnNewProjectNo_OnClick" Text="New Project No" /></td></tr>
<tr><td align="left">Letter Remarks</td><td align="left">:</td><td align="left" colspan="2"><asp:TextBox ID="txtLetterRemarsk" runat="server" CssClass="txtbox" Width="300px" /></td>
</tr>
<tr><td align="left">Remarks</td><td align="left">:</td><td align="left">Synopsis Remarks<br /><asp:TextBox ID="txtSynopsisRemarks" runat="server" CssClass="txtbox" Height="50px" TextMode="MultiLine" Width="100%" /></td>
<td>Project Remarks:<br /><asp:TextBox ID="txtRemarks" runat="server" CssClass="txtbox" Height="50px" TextMode="MultiLine" Width="100%" /></td></tr>
<tr><td colspan="2">&nbsp;</td><td align="left" colspan="4"><b><asp:Button ID="btnsave" runat="server" CssClass="btnsmall" onclick="btnsave_Click" Text="Submit" /><asp:Label ID="lblException" runat="server" ForeColor="Maroon" /></b></td></tr>
<tr><td colspan="2">&nbsp;</td><td align="left" colspan="4"></td></tr>
</table>
<p>Update Project Status: from <b>ProformaAApproved </b> to <b>ProformaBSubmitted</b></p>
</center></asp:Panel></div></div></div></asp:Panel>
<dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-60" OffsetY="25" PopupControlID="pnlGroup" TargetControlID="lnkAddnew"/>
<asp:Panel ID="pnlGroup" runat="server" CssClass="pnlpopup"><br /><center><b>- Add New Synopsis Title -</b><br /><br />
Synopsis Date:&nbsp;<asp:TextBox ID="txtNewSynDate" runat="server" CssClass="txtbox" /><dev:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtNewSynDate" PopupPosition="BottomRight" TargetControlID="txtNewSynDate" /></center>
<table class="tbl">
<tr><td>Synopsis Title:</td><td><asp:TextBox ID="txtNewTitle" runat="server" TextMode="MultiLine" Height="50px" CssClass="txtbox" Width="450px" /></td></tr>
<tr><td>Description:</td><td><asp:TextBox ID="txtNewDescrip" runat="server" TextMode="MultiLine" Height="50px" CssClass="txtbox" Width="450px" /></td></tr>
<tr><td colspan="4" align="center"><asp:Button Text="Submit New Title" ID="btnSubmit" runat="server" onclick="btnSubmit_Click" /></td></tr>
</table>
<div style="min-height:0px;max-height:200px;overflow:scroll; width:600px;">
    <asp:GridView ID="GridNewTitle" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"  GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView></div>
</asp:Panel>
<br /></div><br /></asp:Content>