<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ApproveSynopsis.aspx.cs" Inherits="project_ApproveSynopsis" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
Approve Synopsis Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnl" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Synopsis Submission [Project Allocation]</h1></div><br />
<asp:Panel ID="panelRight" runat="server" >
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<table class="tbl"><tr><td>Select Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; <asp:TextBox ID="txtSession" runat="server" Width="70px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></td></tr>
<tr><td>Serial No:</td><td ><asp:TextBox ID="txtsearch" runat="server" CssClass="txtbox" Width="178px"  ontextchanged="txtsearch_TextChanged"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnshow" runat="server" CssClass="btnsmall" 
        onclick="btnshow_Click" Text="ok" />
    <asp:Label ID="lblshow" runat="server" ForeColor="#CC3300"></asp:Label>
</td></tr>
<tr><td></td><td> 
    &nbsp;</td></tr>
</table><center> <asp:Label ID="lblexception" runat="server" ForeColor="Red" Text=""></asp:Label></center>
<br /><hr /><asp:Panel ID="pnlresult" runat="server" Visible="false">
<asp:UpdatePanel ID="upanel" runat="server"><Triggers><asp:PostBackTrigger  ControlID="btbClear"/></Triggers>
<ContentTemplate>
<asp:Panel ID="pan1" runat="server"  >
<center><asp:Label ID="lblAppNo" runat="server"  Font-Bold="true" Visible="false"></asp:Label>Membership No:<asp:Label ID="lblMem" runat="server"  Font-Bold="true" ></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="lblprojecttitle" runat="server" Width="350px" CssClass="txtbox"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Search" onclick="Button1_Click" CssClass="btnsmall" /><br />
<asp:Label ID="lbldescription" runat="server"  Font-Bold="true" ></asp:Label><br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <dev:PopupControlExtender ID="popupex" runat="server" Position="Right" OffsetX="-550" OffsetY="0" PopupControlID="Panel1" TargetControlID="Button1" ></dev:PopupControlExtender>
    <asp:Panel ID="Panel1" runat="server" Visible="False">
    <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
    </asp:Panel></center>
<table class="tbl" width="95%">
<tr><td>&nbsp;</td><td>&nbsp;GroupID:<asp:Label ID="lblgroupid" runat="server"></asp:Label>
    </td><td>
        &nbsp;</td><td>
        &nbsp;</td></tr>
        <caption>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;<asp:RadioButton ID="rgroupmate1" runat="server" AutoPostBack="true" 
                        GroupName="a" oncheckedchanged="rgroupmate1_CheckedChanged" 
                        Text="GroupMate: #1" />
                </td>
                <td>
                    <asp:RadioButton ID="rgroupmate2" runat="server" AutoPostBack="true" 
                        GroupName="a" oncheckedchanged="rgroupmate2_CheckedChanged" 
                        Text="GroupMate: #2" />
                </td>
                <td>
                    <asp:RadioButton ID="rgroupmate3" runat="server" AutoPostBack="true" 
                        GroupName="a" oncheckedchanged="rgroupmate3_CheckedChanged" 
                        Text="GroupMate: #3" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;ID:&nbsp;<asp:Label ID="lblgroupmate1" runat="server" Enabled="false" 
                        Font-Bold="true" Text=""></asp:Label>
                </td>
                <td>
                    &nbsp;ID:&nbsp;<asp:Label ID="lblgroupmate2" runat="server" Enabled="false" 
                        Font-Bold="true" Text=""></asp:Label>
                </td>
                <td>
                    &nbsp; ID:&nbsp;
                    <asp:Label ID="lblgroupmate3" runat="server" Enabled="false" Font-Bold="true" 
                        Text=""></asp:Label>
                </td>
            </tr>
    </caption>
</table>
    </asp:Panel>
    <br />
<asp:Panel ID="Pan2" runat="server"  CssClass="expbox" Width="400px">
<table class="tbl"><tr><td>Student Name:</td><td><asp:Label ID="lblstuname" runat="server" Font-Bold="true"></asp:Label></td>
    <td>
        &nbsp;</td>
    <td>IMID:</td><td><asp:Label ID="lblIMID" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Stream:</td><td><asp:Label ID="lblstream" runat="server" Font-Bold="true"></asp:Label></td>
    <td>
        &nbsp;</td>
    <td>Course:</td><td><asp:Label ID="lblcourse"  runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Submission Date:</td><td><asp:Label ID="lbldate" runat="server" Font-Bold="true"></asp:Label></td>
    <td>
        &nbsp;</td>
    <td>
    Part:</td><td>
        <asp:Label ID="lblpart" runat="server" Font-Bold="true"></asp:Label>
    </td></tr>
<tr><td>Date:</td><td><asp:Textbox ID="lblcdate" runat="server" Font-Bold="true" CssClass="txtbox"></asp:Textbox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="lblcdate" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Approval Date " >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="lblcdate"></dev:CalendarExtender> 
</td>
    <td>
        <img id="cal" runat="server" alt="Cal" 
            src="../images/cal.png" />
    </td>
    <td>
        Status:</td>
    <td>
        <asp:Label ID="lblstatus" runat="server" Font-Bold="true"></asp:Label>
    </td>
    </tr>
</table>
</asp:Panel>
<br /><center>

    <asp:Label ID="lblexeption" runat="server" Text=""></asp:Label><br />
        <asp:Button ID="btnSave" runat="server" Text="Approve Project" 
            onclick="btnSave_Click" CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
            ID="btbClear" runat="server" Text="Cancel" onclick="btbClear_Click" CssClass="btnsmall"  /></center><br /><br />
<br />
<br /><br /></ContentTemplate></asp:UpdatePanel>
</asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlSpace" runat="server" Height="250px"></asp:Panel>
</div>
</ContentTemplate></asp:UpdatePanel>
    </div>
    <br />
</asp:Content>

