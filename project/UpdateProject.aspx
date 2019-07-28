<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="UpdateProject.aspx.cs" Inherits="project_UpdateProject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
Update Project Allocation 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table>
</div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Synopsis Submission </h1></div><br />
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<table class="tbl"><tr><td>Select Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; <asp:TextBox ID="txtSession" runat="server" Width="70px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></tr>
</table>
    <%--<asp:Panel ID="panelProjectTitle" runat="server" CssClass="expbox">
        <div style="float:right;"><asp:ImageButton ID="ibtnCloseTitle" runat="server" OnClick="ibtnCloseTitle_Onclick" AlternateText="Close" ImageUrl="~/images/closeIcon.png" /></div>
        <table class="tbl"><tr><td>Title:&nbsp;&nbsp;</td><td><asp:TextBox ID="txtNewProject" runat="server" CssClass="txtbox" Width="300px"></asp:TextBox></td></tr>
        <tr><td>Description:</td><td><asp:TextBox ID="txtDescriptionTitle" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="50px" Width="300px"></asp:TextBox></td></tr>
        <tr><td></td><td><asp:Label ID="lblexceptionTitle" runat="server" ></asp:Label></td></tr>
        <tr><td></td><td><asp:Button ID="btnSaveTitle" runat="server" Text="Save" OnClick="btnSaveTitle_Onclick" />&nbsp;&nbsp;<asp:Button ID="btnCleartitle" runat="server" Text="Clear" OnClick="btnClearTitle_Onclick" /></td></tr>
 </table></asp:Panel>--%>
 <center><table class="tbl" width="90%">
<tr><td align="right">Insert MemberShip ID:&nbsp;&nbsp;</td><td align="left"><asp:TextBox ID="txtIMID" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" Text=" OK " OnClick="btnOK_Click" CssClass="btnsmall" /></td><td>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
<tr><td  align="right">Diary Number.:&nbsp;&nbsp;</td><td><asp:TextBox ID="txtDiaryNo" Width="100px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtDiaryNo_TextChaged"></asp:TextBox></td><td>
    <asp:Label ID="lblAdd" Text="Address:" runat="server" Visible="false"/>&nbsp;&nbsp;<asp:Label ID="lblIMAddress" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td align="right">
    <asp:Label Text="Diary Receiving Date:" runat="server" ID="lblRcv" Visible="false" />&nbsp;&nbsp;</td><td><asp:Label ID="txtDOB" runat="server" Font-Bold="true"></asp:Label> </td><td><asp:Label ID="lblIMCity" runat="server" ></asp:Label></td></tr>
<tr><td align="right" visible="false">&nbsp;&nbsp;</td><td visible="false"><asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label></td><td>
    <asp:Label Text="Group ID:" runat="server" ID="lblGrp" Visible="false" />&nbsp; 
<asp:Label ID="Label3" runat="server" ForeColor="MediumBlue"></asp:Label></td></tr>
</table><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true"></asp:Label></center><br /><hr />
<asp:Panel runat="server" ID="pnlMember" Visible="false">
<table class="tbl"><tr><td>Serial No:</td><td><asp:TextBox ID="txtmemshipno" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnmember" runat="server" Text="ok" onclick="btnmember_Click" CssClass="btnsmall" /></td>
    <td>
        <asp:Label ID="lblEx" runat="server" ForeColor="Maroon"></asp:Label>
    </td>
    <td><asp:Label ID="lblAppNo" runat="server" Visible="false" ></asp:Label></td></tr>
    <tr>
        <td>
             <asp:Label
        Text="Membership No:" runat="server" ID="lblmemship" Visible="false" /></td>
        <td>
            <asp:Label ID="lblMem" runat="server"></asp:Label>
        </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label
        Text="IMID:" runat="server" ID="lblIm" Visible="false" /></td>
        <td>
            <asp:Label ID="txtimcode" runat="server" ></asp:Label></td>
        
    </tr>
<tr><td>
    <asp:Label Text="Student Name:" runat="server" ID="lblStuName" Visible="false" /></td><td ><asp:Label ID="txtstuname" runat="server"  ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
        Text="Course:" runat="server" ID="lblCourses" Visible="false" /> </td><td><asp:Label ID="txtstream" runat="server"></asp:Label> &nbsp;<asp:Label ID="txtcourse"  runat="server"></asp:Label>&nbsp;<asp:Label ID="lblpart" runat="server" Text=""></asp:Label></td>
</tr></table>
</asp:Panel>
<asp:Panel runat="server" ID="pnlSelectInst" Visible="false">
<table class="tbl"><tr><td> Select Institution:</td><td colspan="2" >&nbsp;&nbsp;<asp:DropDownList Width="250px" ID="ddlInstitute" runat="server" onselectedindexchanged="ddlInstitute_SelectedIndexChanged" CssClass="txtbox">
</asp:DropDownList>
</td><td>&nbsp;<asp:Label ID="lblinstype" runat="server" Text="Label" Visible="False"></asp:Label>&nbsp;&nbsp;<asp:Button ID="btnshow" runat="server" Text="ok" onclick="btnshow_Click" CssClass="btnsmall" /></td></tr>
</table>
</asp:Panel>
<asp:Panel ID="Panin" runat="server" Visible="false">
<table class="tbl" ><tr><td>ID :</td><td colspan="1">
<asp:Label ID="lblid" runat="server" Text=""  Font-Bold="true" ></asp:Label></td><td>Name :</td><td>&nbsp; <asp:Label ID="lblname" Font-Bold="true" runat="server" Text="" ></asp:Label></td></tr><tr>
<td>Address :</td>
<td colspan="3"><asp:Label ID="lbladdress" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp; <asp:Label ID="lblcity" Font-Bold="true" runat="server" Text="" ></asp:Label>&nbsp;<asp:Label ID="lblstate" Font-Bold="true" runat="server"  Text=""></asp:Label></td>
</tr>
<tr><td>Pincode :</td>
<td><asp:Label ID="lblpincode" Font-Bold="true" runat="server"  Text=""></asp:Label></td>
<td>Contact No. :</td>
<td>&nbsp;<asp:Label ID="lblcontact" runat="server" Font-Bold="true"  Text=""></asp:Label></td>
</tr>
<tr><td>Email :</td><td colspan="1">
<asp:Label ID="lblemail" runat="server" Text=""  Font-Bold="true"></asp:Label></td> <td>Mobile :</td>
<td>&nbsp;<asp:Label ID="lblmobile" runat="server" Font-Bold="true" Text=""></asp:Label>
</td></tr>
</table></asp:Panel>
<center><asp:Label ID="lblExcepitnProject" runat="server" ></asp:Label></center>
<asp:UpdatePanel ID="updatepanleTitle" runat="server" ><Triggers><asp:PostBackTrigger ControlID="btbClear" /></Triggers><ContentTemplate>
<asp:Panel ID="pnlProjTitle" runat="server" Visible="false">
<table class="tbl">
<tr><td>Project Title:<br />
<asp:TextBox ID="Txttitle" runat="server" CssClass="txtbox"></asp:TextBox>
&nbsp;</td><td rowspan="2">
<asp:Panel ID="Pnlgrid" runat="server" Visible="False">
<asp:GridView ID="GridView1" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                ForeColor="Black" GridLines="Vertical" 
                onselectedindexchanged="GridView1_SelectedIndexChanged"> <EmptyDataTemplate><div>Record Not Found</div></EmptyDataTemplate>
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="Gray" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
</asp:GridView>
</asp:Panel>
</td></tr><tr><td>Description:<br />
<asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" CssClass="txtbox" 
            Height="50px"></asp:TextBox>&nbsp;<asp:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="150" OffsetY="-100" PopupControlID="Pnlgrid" TargetControlID="LinkButton1" ></asp:PopupControlExtender><asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Search Related Topics</asp:LinkButton>
<br /></td></tr>
</table>
</asp:Panel>
<asp:Label ID="lblDuration" runat="server" visible="false"></asp:Label><asp:Label ID="lblRemarks" runat="server" visible="false"></asp:Label>
        <%--<asp:Panel ID="panelProjectTitle" runat="server" CssClass="expbox">
        <div style="float:right;"><asp:ImageButton ID="ibtnCloseTitle" runat="server" OnClick="ibtnCloseTitle_Onclick" AlternateText="Close" ImageUrl="~/images/closeIcon.png" /></div>
        <table class="tbl"><tr><td>Title:&nbsp;&nbsp;</td><td><asp:TextBox ID="txtNewProject" runat="server" CssClass="txtbox" Width="300px"></asp:TextBox></td></tr>
        <tr><td>Description:</td><td><asp:TextBox ID="txtDescriptionTitle" runat="server" TextMode="MultiLine" CssClass="txtbox" Height="50px" Width="300px"></asp:TextBox></td></tr>
        <tr><td></td><td><asp:Label ID="lblexceptionTitle" runat="server" ></asp:Label></td></tr>
        <tr><td></td><td><asp:Button ID="btnSaveTitle" runat="server" Text="Save" OnClick="btnSaveTitle_Onclick" />&nbsp;&nbsp;<asp:Button ID="btnCleartitle" runat="server" Text="Clear" OnClick="btnClearTitle_Onclick" /></td></tr>
 </table></asp:Panel>--%>
<asp:Panel ID="pnlshow" runat="server" Visible="false">
<table class="tbl" width="90%">
<tr>
<td class="style1">Date:<br /><asp:TextBox ID="txtapprovaldate" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Calt" TargetControlID="txtapprovaldate">
</asp:CalendarExtender>
<img src="../images/cal.png" id="Calt" runat="server"  alt="Calt" /></td></tr>
<tr><td>Group ID:<asp:Label ID="lblGroupID" runat="server"></asp:Label>
<br />
</td><td>&nbsp;</td>
</tr><tr>
<td>Group Mate: #1<asp:TextBox ID="txtgmate1" Font-Bold="true" runat="server" CssClass="txtbox" AutoPostBack="True" ontextchanged="txtgmate1_TextChanged"></asp:TextBox></td>
<td>Group Mate: #2<asp:TextBox ID="txtgmate2" runat="server" Font-Bold="true" CssClass="txtbox" AutoPostBack="True" ontextchanged="txtgmate2_TextChanged"></asp:TextBox></td>
<td>Group Mate: #3<asp:TextBox ID="txtgmate3" runat="server" Font-Bold="true" CssClass="txtbox" AutoPostBack="True" ontextchanged="txtgmate3_TextChanged"></asp:TextBox></td>
</tr>
</table><br /><center><asp:Label ID="lblexeption" runat="server" Text="" ForeColor="#CC3300"></asp:Label></center>
<br /><center>
<br /><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btbClear" runat="server" Text="Next" OnClick="btnNext_Click" CssClass="btnsmall" /></center><br /><br />
<br />
<br /><br />
</asp:Panel></ContentTemplate></asp:UpdatePanel>
<asp:Panel runat="server" ID="pnlSpace" Height="200px">
</asp:Panel>
</div>
<br />
    </div>
</asp:Content>

