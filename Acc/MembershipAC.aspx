<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="MembershipAC.aspx.cs" Inherits="Acc_MembershipAC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Membership Account
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblMembershipAC" runat="server" Text="IM Membership Account" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><asp:Panel ID="pnlid" runat="server"><h1 style="float:right; margin-right:30px;"> Membership ID:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtId" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="txtViewOk" runat="server" Text="View" CssClass="btnsmall" OnClick="lbtnViewAC_Click"></asp:Button></h1></asp:Panel><h1>IM Membership Account </h1></div>
        <asp:Panel ID="panelReNew" runat="server" >
     <center>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</center>        
       <center><asp:Label ID="lblException" runat="server" ></asp:Label></center>
               <asp:Panel ID="panelSubmitAmt" runat="server" >
               <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Personal Information:-</font></legend>
               <table width="100%"><tr><td>Member Type:</td><td><asp:Label ID="lblMemberTyep" runat="server"></asp:Label></td><td>ID:&nbsp;&nbsp;<asp:Label ID="lblID" runat="server"></asp:Label></td></tr>
               <tr><td>Name:</td><td align="left"><asp:Label ID="lblName" runat="server"></asp:Label></td><td></td></tr>
               <tr><td>Address:</td><td><asp:Label ID="lblAddress" runat="server" ></asp:Label></td></tr>
               <tr><td>City:</td><td><asp:Label ID="lblCity" runat="server"></asp:Label></td><td>Email:&nbsp;&nbsp;<asp:Label ID="lblEmail" runat="server" ></asp:Label></td></tr>
               <tr><td>Mobile:</td><td><asp:Label ID="lblMobile" runat="server"></asp:Label></td><td>Phone:&nbsp;&nbsp;<asp:Label ID="lblPhonne" runat="server" ></asp:Label></td></tr>
               <tr><td>Registration Date:</td><td><asp:Label ID="lblRegistrationdAte" runat="server"></asp:Label></td><td>Status:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server"></asp:Label></td></tr>
               <tr><td>Subscription Date:&nbsp;</td><td><asp:Label ID="lblSubscriptionDate" runat="server"></asp:Label></td><td>Last Subscription:&nbsp;&nbsp;<asp:Label ID="lblSubFrom" runat="server"></asp:Label>&nbsp;&nbsp;To:&nbsp;&nbsp;<asp:Label ID="lblSubTo" runat="server"></asp:Label></td></tr>
               </table>
               </fieldset>
      </asp:Panel>
    </asp:Panel>
        <asp:Panel ID="panelSubmitamount" runat="server" > <center><asp:Label ID="lblTitleInfo" runat="server" Font-Bold="true" ForeColor="Gray" ></asp:Label></center><br />
                <div style="float:right; margin:10px; width:40%;">
                 <fieldset><legend><font style="color:#B21235; font-size:12px; font-family:Verdana;">Select Session-</font></legend>
                <br />
                <center>Session:&nbsp;<asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="50px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox>
                 <br /><br />
                 Session:&nbsp;<asp:Label ID="lblSessionHiddend" runat="server" Font-Bold="true"></asp:Label><br /><br />
                 Current Balance (Rs):&nbsp;<asp:Label ID="lblBalance" runat="server" Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="lblBalanceType" runat="server" ></asp:Label>
                 </center>
                 <asp:GridView ID="GridDiaryNo" runat="server"  HorizontalAlign="Center" onselectedindexchanged="GridDiaryNo_SelectedIndexChanged" Width="200px" PageSize="100" OnRowDataBound="GridDiaryNo_OnRowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
<Columns>
<asp:CommandField ShowSelectButton="True" SelectText="Select" HeaderText="Select" />
</Columns>
<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
</asp:GridView>
<asp:Label ID="extra" runat="server" Visible="false">extra</asp:Label>
<table><tr>
<td>Membership DD<br /> <asp:Label ID="lblmemSub" ForeColor="Maroon" runat="server" ></asp:Label>/<asp:Label 
        ID="lblmemRcv" runat="server" ForeColor="Maroon"></asp:Label> </td>
<td>Books DD<br /><asp:Label ID="lblBookSub" ForeColor="Maroon" runat="server" ></asp:Label>/<asp:Label ID="lblBookRcv" runat="server" ForeColor="Maroon"></asp:Label> </td>
<td>Prospectus DD<br /><asp:Label ID="lblProspectusSub" ForeColor="Maroon" runat="server" ></asp:Label>/<asp:Label ID="lblProspectusRcv" runat="server" ForeColor="Maroon"></asp:Label> </td>
 </tr><tr><td>Total<br /><asp:Label ID="lblTDDSub" Font-Bold="true" ForeColor="Maroon" runat="server" ></asp:Label>/<asp:Label ID="lblTDDRcv" Font-Bold="true" runat="server" ForeColor="Maroon"></asp:Label> </td>
</tr></table>
                 </fieldset>
                </div>
 <asp:UpdatePanel ID="updatePanelSubmitAmt" runat="server" >
           <Triggers><asp:PostBackTrigger ControlID="ddlCurrancy" /></Triggers>
       <%-- <Triggers><asp:PostBackTrigger ControlID="txtAmt" /></Triggers>--%>
         <Triggers><asp:PostBackTrigger ControlID="txtCurrancyValue" /></Triggers>
         <Triggers> <asp:PostBackTrigger ControlID="btnSubscribe" /></Triggers>
         <Triggers> <asp:PostBackTrigger ControlID="ddlsession" /></Triggers>
         <Triggers><asp:PostBackTrigger ControlID="txtDate" /></Triggers>
         <Triggers><asp:PostBackTrigger ControlID="txtDOB" /></Triggers>
        <ContentTemplate>
                <table width="50%">
                <tr><td>Amount Type:</td><td><asp:DropDownList ID="ddlAmtType" runat="server" 
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlAmtType_SelectedIndexChanged" CssClass="txtbox" ><asp:ListItem Value="DD" Text="Demand Draft"></asp:ListItem><asp:ListItem Value="Cash" Text="Cash"></asp:ListItem><asp:ListItem Text="Credit Card" Value="CC"></asp:ListItem><asp:ListItem Value="Online" Text="Online Transection"></asp:ListItem></asp:DropDownList></td></tr>
                        <tr><td><asp:Label ID="lblDDNNO" runat="server" ></asp:Label></td><td>
                            <asp:TextBox ID="txtDDNO" runat="server" CssClass="txtbox" ></asp:TextBox></td></tr><tr><td><asp:Label ID="lblAccountNo" runat="server" ></asp:Label></td><td>
                        <asp:TextBox ID="txtACNO" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
                        <tr><td>Bank:</td><td>
                            <asp:DropDownList ID="ddlBank" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" 
            Width="200px" CssClass="txtbox" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Bank') ORDER BY Name">
        </asp:SqlDataSource></td></tr>
                        <tr><td>Submission Date:</td><td><asp:TextBox ID="txtDate" runat="server" AutoPostBack="true" OnTextChanged="txtDateSub_TextChanged" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="txtDate" Display="Dynamic" ValidationGroup="amt" errormessage="Insert Date of Submission, Othrwise default present date." >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="Img1" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender> <img src="../images/cal.png" id="Img1" runat="server"  alt="Cal" /></td>
                       </tr><tr> <td>DD Date:</td><td><asp:TextBox ID="txtDOB" AutoPostBack="true" OnTextChanged="txtDate_TechChanged" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDOB" Display="Dynamic" ValidationGroup="amt" errormessage="Insert Date of DD." >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDOB"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /><br /><asp:Label ID="lblExceptiondAte" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td></tr>
                     <tr><td>Narration Box:&nbsp;</td><td><asp:TextBox ID="txtNarration" CssClass="txtbox" runat="server" TextMode="MultiLine" Width="80%" Height="50px"></asp:TextBox></td></tr>
                       <tr><td>Amount:</td><td>
                           <asp:TextBox ID="txtAmt" runat="server" AutoPostBack="True" 
                                ontextchanged="txtAmt_TextChanged" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="filteramt" runat="server"  TargetControlID="txtAmt" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
                        <tr><td>Account Type:</td><td><asp:DropDownList ID="ddlAccountType" runat="server" CssClass="txtbox"><asp:ListItem Value="IM" Text="IM" /></asp:DropDownList></td></tr>
                        <tr><td>Currency:&nbsp;</td><td><asp:DropDownList ID="ddlCurrancy" runat="server" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCurrancy_Changed" 
                                CssClass="txtbox" ><asp:ListItem Value="RS" Selected="True" Text="Rupees"></asp:ListItem><asp:ListItem Value="DL" Text="Dolar"></asp:ListItem><asp:ListItem Value="OT" Text="Other"></asp:ListItem></asp:DropDownList></td></tr>
                        <tr><td><asp:Label ID="lblCurrancyText" runat="server" ></asp:Label>&nbsp;</td><td><asp:Label ID="lblCurrancyName" runat="server" ></asp:Label>&nbsp;<asp:TextBox ID="txtCurrancyValue" runat="server" AutoPostBack="true" OnTextChanged="txtCurrancyValue_TextChanged" CssClass="txtbox" Width="50px"></asp:TextBox>RS.<dev:FilteredTextBoxExtender ID="filtercurrent" TargetControlID="txtCurrancyValue" runat="server" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
                </table>
                <center>Total Amount:&nbsp;<asp:Label ID="lblTAmt" runat="server" Font-Bold="true" ></asp:Label> Rs.<br /><asp:Label ID="chkLessAmt" runat="server" ></asp:Label></center>
               <br /><center><asp:Label ID="Label1" runat="server" ForeColor="Maroon" ></asp:Label><br />
                    <asp:Button ID="btnSubscribe" runat="server" Text="Submit" CssClass="btnsmall" 
                        OnClick="btnSubscribe_Click" UseSubmitBehavior="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnClear" runat="server" Text="Refresh" CssClass="btnsmall" onclick="btnClear_Click" />
                        <fieldset><legend>Fees Module:</legend>
                       Membership Type:&nbsp;<b> <asp:Label ID="lblMemberType" runat="server"></asp:Label></b>&nbsp;&nbsp;
                       Enrollment Fee(Annual):&nbsp;<b><asp:Label ID="lblEnrollFee" runat="server" ></asp:Label> Rs.</b> &nbsp;&nbsp;&nbsp;Subscription Fee(Annual):&nbsp;<b><asp:Label ID="lblSubFee" runat="server" ></asp:Label> Rs.</b>
                       </fieldset></center>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" Width="100%" 
                onrowdatabound="GridView1_RowDataBound">
            <RowStyle BackColor="#F7F7DE" />
           <EmptyDataTemplate><center>  Record Not Found !</center></EmptyDataTemplate>
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
         </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnClear" />
             <asp:PostBackTrigger ControlID="ddlCurrancy" />
             <asp:PostBackTrigger ControlID="txtAmt" />
             <asp:PostBackTrigger ControlID="txtCurrancyValue" />
             <asp:PostBackTrigger ControlID="btnSubscribe" />
             <asp:PostBackTrigger ControlID="ddlsession" />
             <asp:PostBackTrigger ControlID="txtDate" />
             <asp:PostBackTrigger ControlID="txtDOB" />
         </Triggers>
        </asp:UpdatePanel>
     </asp:Panel> 
        <asp:Panel ID="panelspace" runat="server" Height="500px">
        <center> <img src="../images/membership.jpg"  width="100%" height="250px"/><br /><br /><br /><br /><br />
       <asp:Label ID="lblMessage" runat="server" />
      <h1> Membership ID:&nbsp;&nbsp;&nbsp;<asp:TextBox 
                ID="txtIMID" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button 
                ID="btnView" runat="server" Text="View" CssClass="btnsmall" 
                OnClick="btnViewAC_Click"></asp:Button></h1> </center>
        </asp:Panel>
 </div><br />
</asp:Content>

