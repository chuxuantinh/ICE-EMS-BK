<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="AdmissionForm.aspx.cs" Inherits="Acc_AdmissionForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Admission Form ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblAdmissionForm" runat="server" Text="Institutional Member Account" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Account </h1></div><br />
<br /><div class="rightbox"><center>
<table><tr><td>Group ID:&nbsp;</td><td><asp:Label ID="lblGroupID" runat="server" ></asp:Label></td></tr>
<tr><td>Group Amount:&nbsp;</td><td><b><asp:Label ID="lblGAmt" runat="server" ></asp:Label> &nbsp;Rs.</b></td></tr>
<tr><td>Renewal Date:&nbsp;</td><td><asp:Label ID="lblRenewalDate" runat="server" ></asp:Label></td></tr>
<tr><td></td><td><asp:LinkButton  ID="ibtnviewGroup" runat="server" Text="View" OnClick="lbtnViewGroup_Click"></asp:LinkButton></td></tr>
</table><br />
Total Application form:&nbsp;<b><asp:Label ID="lblTFromNo" runat="server"></asp:Label></b>&nbsp;Approve:&nbsp;<asp:TextBox ID="txtFromNo" Width="30px" runat="server" CssClass="txtbox"></asp:TextBox><br />
Amount for Admission:&nbsp;<b><asp:Label ID="lblReqAmt" runat="server"></asp:Label>&nbsp; Rs.</b>
</center></div>


<asp:Panel ID="panelIM" runat="server" CssClass="imbox" >
           <br />     
Insert IM ID:&nbsp;&nbsp;<asp:TextBox ID="txtIDIM" runat="server" BorderColor="Gray" 
         Width="200px" AutoPostBack="true" BorderStyle="Solid" BorderWidth="2px" 
         ForeColor="Blue" ontextchanged="txtIDIM_TextChanged" ></asp:TextBox>          <br />
<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="15px" ></asp:Label><br />
<asp:Label ID="lblIMAddress" runat="server" ></asp:Label><br />
<asp:Label ID="lblIMCity" runat="server" ></asp:Label><br />
<br />
Total Amount in A/C:&nbsp;&nbsp;<b><asp:Label ID="lblTAmt" runat="server" ></asp:Label>&nbsp; Rs.</b>
</asp:Panel>


<div style="margin-left:100px;">
Total Late Fee of ICE(I):&nbsp;<b><asp:Label ID="lblTLate" runat="server" ></asp:Label> Rs.<br /></b>
Total Late Fee Of IM:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTLateofIM" runat="server" ></asp:Label><br />
Late FEE Added:&nbsp;&nbsp;<b><asp:Label ID="lblLTaken" runat="server" ></asp:Label><br />
Late Fee Remaining:&nbsp;<asp:Label ID="lblLRem" runat="server" ></asp:Label></b><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox  AutoPostBack="true"
               ID="chkLateFEE" runat="server" Text="Add Late Fee" 
               oncheckedchanged="chkLateFEE_CheckedChanged" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessage1" runat="server"></asp:Label><br /><br />
               Total Amt for Pay:&nbsp;&nbsp;<b><asp:Label ID="lblTotalAmtPay" runat="server" ></asp:Label><br /></b>
Total Appraval Form:&nbsp;<b><asp:Label ID="lblToBeApprovadNo" runat="server" ></asp:Label></b><br />
Amount of  Form:&nbsp;<b><asp:Label ID="lblToBeAmount" runat="server" ></asp:Label></b><br /><br />

<center>
<b><asp:Label ID="lblPayMessage" runat="server" ></asp:Label></b><br />
<asp:Button ID="btnApprove" runat="server" Text="Calculate" OnClick="btnApprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPay" runat="server" Text="Approve" OnClick="btnPay_click" /></center>
</div>
<asp:Label ID="lbltotalRow" runat="server" ></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" 
        Width="100%" onselectedindexchanged="GridView1_SelectedIndexChanged" 
        onload="GridView1_Load">
        <RowStyle BackColor="#F7F7DE" />
       
        <Columns>
       <%-- <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lbtn" CommandName="Select" Text='@Bind("Name")' runat="server" ></asp:LinkButton></ItemTemplate></asp:TemplateField>--%>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Name" HeaderText="Student Name" SortExpression="Name" />
            <asp:BoundField DataField="AppId" HeaderText="Form No." SortExpression="AppId" />
            <asp:BoundField DataField="Stream" HeaderText="Stream" 
                SortExpression="Stream" />
            <asp:BoundField DataField="Course" HeaderText="Course" 
                SortExpression="Course" />
            <asp:BoundField DataField="Part" HeaderText="Part/Section" SortExpression="Part" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT [Name], [AppId], [Stream], [Course], [Part], [Status] FROM [Student] WHERE (([IMID] = @IMID) AND ([Status] = @Status))">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblEnrolment" Name="IMID" PropertyName="Text" 
                Type="String" />
            <asp:Parameter DefaultValue="no" Name="Status" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
 </ContentTemplate></asp:UpdatePanel>

 </div>
</asp:Content>

