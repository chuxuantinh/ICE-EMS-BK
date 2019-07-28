<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="IMStatus.aspx.cs" Inherits="Administrator_IMStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="float:right; margin-right:50px;">Insert IM ID:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnViewEnroll" runat="server" Text="View Profile"  OnClick="btnView_Click" /></div>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
           
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Profile Status</h1></div><br />
 <asp:Panel ID="panelSubspendAcc" runat="server" >
 <%--<div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton5" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>--%>
 <center><h2 style="color:Maroon; font-size:18px; font-family:Times New Roman;" >Status: <b><asp:Label runat="server" ID="lblStatus2" ></asp:Label></b></h2></center>
 <br />
 <center><asp:Button ID="btnchangeStatus" runat="server" OnClick="btnChnageStatsu" CssClass="bigbutton" /></center>
 </asp:Panel>
    
   <asp:Panel ID="panelViewAccount" runat="server" >
 <%--<div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton3" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>--%>
 <center><h2 style="color:Maroon; font-size:18px; font-family:Times New Roman;" >Status: <b><asp:Label runat="server" ID="lblStatus" ></asp:Label></b></h2></center>
 
                            <table>
                            <tr><td>Registration Date:</td><td><asp:Label ID="lblEnrollDate" runat="server" ></asp:Label></td></tr>
                           
                            <tr><td>Renwal Date:</td><td><asp:Label ID="lblRenuwalDate" runat="server" ></asp:Label></td></tr>
                            <tr><td> Membership Expire Date:</td><td><asp:Label ID="lblExpDate" runat="server" ></asp:Label></td></tr>
                            <tr><td><br /></td></tr>
                            <%--<tr><td>Total Submitted Amount:</td><td><asp:Label ID="lbltotleAmt" runat="server" ></asp:Label></td></tr>
                             <tr><td>Last Submission Date:</td><td><asp:Label ID="lblLastDate" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Amount:&nbsp;<asp:Label ID="lblLastAmt" runat="server" ></asp:Label></td></tr>--%>
                            </table><center>This Membership Subscription up to: &nbsp;&nbsp;<asp:Label ID="lblyear" runat="server"></asp:Label></center>
                            <asp:Label ID="lblActive" runat="server" Visible="false"></asp:Label><br /><br />
     <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
         AllowSorting="True" AutoGenerateColumns="False" 
         BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
         CellPadding="2" DataSourceID="SqlDataSource2" ForeColor="Black" 
         GridLines="None" Width="100%">
         <Columns>
             <asp:TemplateField ShowHeader="False">
                 
             </asp:TemplateField>
              <asp:ButtonField CommandName="Select" DataTextField="Amt" />
             
             
             <asp:BoundField DataField="FeeType" HeaderText="Amt Type" 
                 SortExpression="FeeType" />
             <asp:BoundField DataField="SubDate" HeaderText="Submitted" 
                 SortExpression="SubDate" />
             <asp:BoundField DataField="SubType" HeaderText="Subission Type" 
                 SortExpression="SubType" />
             <asp:BoundField DataField="AcountNo" HeaderText="A/C No" 
                 SortExpression="AcountNo" />
             <asp:BoundField DataField="DD" HeaderText="DD No." SortExpression="DD" />
             <asp:BoundField DataField="Bank" HeaderText="Bank Name" SortExpression="Bank" />
             <asp:BoundField DataField="YearFrom" HeaderText="Year From" 
                 SortExpression="YearFrom" />
             <asp:BoundField DataField="YearTo" HeaderText="Year To" 
                 SortExpression="YearTo" />
            
         </Columns>
         <FooterStyle BackColor="Tan" />
         <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
             HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
         <HeaderStyle BackColor="Tan" Font-Bold="True" />
         <AlternatingRowStyle BackColor="PaleGoldenrod" />
     </asp:GridView>
     <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
         ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
         SelectCommand="SELECT [MType], [Amt], [FeeType], [SubDate], [SubType], [AcountNo], [DD], [Bank], [YearFrom], [YearTo] FROM [MemberFee] WHERE ([ID] = @ID) ORDER BY [SN]">
         <SelectParameters>
             <asp:SessionParameter Name="ID" SessionField="FeeID" Type="String" />
         </SelectParameters>
     </asp:SqlDataSource>
     <br />
         <hr />                   
   <div style="margin-top:350px;" >.</div>
                              
       </asp:Panel>
    
 </div>  
</asp:Content>

