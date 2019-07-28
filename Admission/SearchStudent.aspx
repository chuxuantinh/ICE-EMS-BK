<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="SearchStudent.aspx.cs" Inherits="Admission_SearchStudent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Search Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"/></td><td>
<asp:Label ID="lblNext" runat="server" Text="Search Student" CssClass="redirecttabhome"/></td></tr></table></div>
<div id="rightpanel2">
<asp:ScriptManager ID="ScriptManager1" runat="server"/>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"><asp:Label ID="lblEnrolment" runat="server"/>
</h1><h1>Search Student</h1></div>
<center>
Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" Width="150px"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>
Year:&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged"/>
<asp:CheckBox ID="CheckBox1" runat="server" oncheckedchanged="CheckBox1_CheckedChanged" Text="Session" />
</center>
<br /><center>
<table class="tbl">
<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Search Student:</td>
<td>
<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="txtbox" onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="150px" ForeColor="Maroon" Font-Bold="true">
<asp:ListItem Text="Membership" Value="SID" />
<asp:ListItem Text="Name" />
<asp:ListItem Text="IMID" />
<asp:ListItem Text="Status" />
<asp:ListItem Text="Serial No" Value="SerialNo" />
</asp:DropDownList>
</td>
<td><asp:Label ID="lblDrpName" runat="server"/></td>
<td><asp:TextBox ID="txtSearch" runat="server" CssClass="txtbox" Width="105px" Font-Bold="true" ForeColor="Maroon"/></td>
<td><asp:Button ID="btnView" runat="server" onclick="btnView_Click" Text="View" CssClass="btnsmall" /></td>
<td align="right"><asp:CheckBox ID="chkIMID" runat="server" Text="IMID" Visible="False" /></td>
<td><asp:DropDownList ID="DropDownList2" runat="server" Visible="False" CssClass="txtbox">
<asp:ListItem Text="NotApproved"/>
<asp:ListItem Text="Pending"/>
<asp:ListItem Text="ToBeApprove"/>
</asp:DropDownList>
</td>
</tr></table>
</center>
<br />
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">
<asp:Label ID="lblGridTitle" runat="server" />
<br />
</div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<br />
<div style="overflow:scroll;Height:180px">
<asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="GridView1_RowDataBound" onselectedindexchanged="GridView1_SelectedIndexChanged" >
<EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
<AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
<Columns>
<asp:CommandField ShowSelectButton="True" />
</Columns>
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
</asp:GridView>
</div>
</div>
</div>
<panel class="SearchStudentpanel">
<center>
<table width="100%">
<tr><td>
<table>
<tr><th align="center">-Admission Course-</th>
</tr>
<tr>
<th align="center">
<asp:Label ID="lblAdStream" runat="server" ForeColor="Maroon" Font-Size="12px" />
,<asp:Label ID="lblAdCourse" runat="server"  ForeColor="Maroon" Font-Size="12px"/>
<asp:Label ID="lblAdPart" runat="server" ForeColor="Maroon" Font-Size="12px" /></th>
</tr>
<tr><th align="center"><hr /></th></tr>
<tr><th align="center">-Current Course-</th></tr>
<tr><th align="center">
<asp:Label ID="lblStream" runat="server" ForeColor="Maroon" Font-Size="12px" />
,<asp:Label ID="lblCourse" runat="server"  ForeColor="Maroon" Font-Size="12px"/>
<asp:Label ID="lblPart" runat="server" ForeColor="Maroon" Font-Size="12px" />
</th>
</tr>
<tr><th align="center"><hr /></th></tr>
<tr><th align="center">Admission Status</th></tr>
<tr><th align="center"><asp:Label ID="lblAdStatus" runat="server" Font-Size="12px" ForeColor="Maroon" /></th></tr>
<tr><td><table class="tbl"><tr>
<td><asp:ImageButton ID="iResults" runat="server" AlternateText="Results" ImageUrl="~/images/SearchResult.png" />
<dev:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="pnlResult" TargetControlID="iResults" Position="Center" OffsetX="5" OffsetY="50"/>
</td>
<td><asp:ImageButton ID="img2" runat="server" ImageUrl="~/images/SearchAdd.png" />
<dev:PopupControlExtender ID="PopupControlExtender3" runat="server" PopupControlID="pnl2" TargetControlID="img2"  Position="Center" OffsetX="-90" OffsetY="50"/>
</td>
<td><asp:ImageButton ID="Img3" runat="server" ImageUrl="~/images/SearchFees.png" />
<dev:PopupControlExtender ID="PopupControlExtender4" runat="server" PopupControlID="pnl3" TargetControlID="img3"  Position="Center" OffsetX="-100" OffsetY="50"/>
</td>
<td><asp:ImageButton ID="img4" runat="server" ImageUrl="~/images/SearchIM.png" />
<dev:PopupControlExtender ID="PopupControlExtender5" runat="server" PopupControlID="pnl4" TargetControlID="img4" Position="Center" OffsetX="-90" OffsetY="50"/>
</td></tr></table></td></tr>
</table>
</td>
<td>
<table width="100%">
<tr align="center"><td>
<asp:Label ID="lblMembership" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Size="Medium"/></td>
</tr>
<tr align="center"><td>
<asp:DataList ID="DataList1" runat="server"   >
<ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"SID") %>'   Height="150px" Width="150px"  />
</ItemTemplate>
</asp:DataList></td>
</tr>
<tr align="center"><td>
<asp:Label ID="lblName" runat="server" ForeColor="Maroon" Font-Bold="true"  Font-Size="Medium"/></td>
</tr>
</table>
</td>
</tr>
</table>
</center>
</panel>
<center><asp:Panel ID="pnlResult" runat="server" CssClass="pnlpopup2">
Results<br />
PartI:&nbsp;<asp:Label ID="lblPartICount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
PartII:&nbsp;<asp:Label ID="lblPartIICount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
SectionA:&nbsp;<asp:Label ID="lblSectionACount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
SectionB:&nbsp;<asp:Label ID="lblSectionBCount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<br />
Exam Status:&nbsp;<asp:Label ID="lblExamStatus" runat="server" />
<asp:GridView ID="GridResult" runat="server" AutoGenerateColumns="true" OnRowDataBound="GridResult_RowDataBound"></asp:GridView><br />
</asp:Panel></center>
<center><asp:Panel ID="pnl2" runat="server" CssClass="pnlpopup2">
<table class="tbl">
<tbody>
<tr>
<th align="left">Permanent Address:</th>
<td><asp:Label ID="lblPAddress" runat="server" CssClass="lblbox" /></td>
<td><asp:Label ID="lblPAddress2" runat="server" CssClass="lblbox" /></td>
</tr>
<tr>
<td></td>
<td colspan="2"><asp:Label ID="lblPCity" runat="server" CssClass="lblbox" />,<asp:Label ID="lblPState" runat="server" CssClass="lblbox"/>-<asp:Label ID="lblPPincode" runat="server" CssClass="lblbox"/></td>
</tr>
<tr>
<th align="left">Correspondence Address:</th>
<td colspan="2"><asp:Label ID="lblCAddress" runat="server" CssClass="lblbox" />,</td>
</tr>
<tr>
<td></td>
<td colspan="2"><asp:Label ID="lblCCity" runat="server" CssClass="lblbox" />,</td>
</tr>
<tr><td>&nbsp;</td>
<td colspan="2">
<asp:Label ID="lblCState" runat="server" CssClass="lblbox" />
,-<asp:Label ID="lblCPin" runat="server" CssClass="lblbox" />
</td>
</tr>
<tr><th align="left">Phone:</th>
<td colspan="2"><asp:Label ID="lblPhone" runat="server" CssClass="lblbox" /></td>
</tr>
<tr><th align="left">Mobile:</th>
<td colspan="2"><asp:Label ID="lblMob" runat="server" CssClass="lblbox" ForeColor="Maroon" Font-Bold="true" /></td>
</tr>
</tbody>
</table>
</asp:Panel></center>
<center><asp:Panel ID="Pnl3" runat="server" CssClass="pnlpopup2">
<table>
<tbody>
<tr>
<th align="left">Total Submit Amount:</th>
<td>[&nbsp;<asp:Label ID="lblSeasonHidden" runat="server"/>&nbsp;]<asp:Label ID="lblAnnualSubsSession" runat="server"/></td>
</tr>
<tr>
<th align="left">Total Submit Amount:</th>
<td><asp:Label ID="lblTotalSubAmnt" runat="server" /></td>
</tr>
<tr>
<th align="left">Total Late Fee Amount:</th>
<td><asp:Label ID="lblTotalLAteAmnt" runat="server" /></td>
</tr>
<tr>
<th align="left">Annual Subscription:</th>
<td><asp:Label ID="lblAnnualSubs" runat="server" /></td>
</tr>
</tbody>
</table>
</asp:Panel></center>
<center><asp:Panel ID="pnl4" runat="server" CssClass="pnlpopup2">
<table>
<tbody>
<tr>
<th align="left">IMID:</th>
<td><asp:Label ID="lblIMID" runat="server" Text="[IMID]" /></td>
</tr>
<tr>
<th align="left">Composite Status:</th>
<td><asp:Label ID="lblComp" runat="server" /></td>
</tr>
<tr>
<th align="left">Enrollment Session:</th>
<td><asp:Label ID="lblSess" runat="server" /></td>
</tr>
<tr>
<th align="left">Session Duration:</th>
<td><asp:Label ID="lblSessionDuration" runat="server" /></td>
</tr>
<tr>
<th align="left">Project Status:</th>
<td><asp:Label ID="lblProjectStatus" runat="server" /></td>
</tr>
</tbody>
</table>
</asp:Panel></center>
<br />
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
<a id="A1" href="javascript:toggleA1x('Div1xx', 'A1');"><img src="../images/minus.png" alt="Show"/></a>
</div>
<div style="padding:5px; color:White; font-size:15px;">
&nbsp;<asp:Label ID="Label1" runat="server" Text="Student Application Forms" Font-Bold="true"/>
<br />
</div>
<div id="Div1xx" style="display:block;">
<input id="Hidden1" runat="server" type="hidden" value="0" />
<br />
<div style="overflow:scroll">
<asp:GridView ID="grdStudentAccount" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="grdStudentAccount_RowDataBound" HeaderStyle-HorizontalAlign="Center">
<EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
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
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>