<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="IMHeadView.aspx.cs" Inherits="Administrator_IMHeadView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"> View Head of IM
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
            
            
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Head/Academic Head Profile</h1></div>
<table class="tbl"><tr><td>Full Name :</td><td><asp:Label ID="txtName" runat="server" ></asp:Label></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Designation.:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtDEsignation" runat="server"  ></asp:Label></td></tr>


<tr><td>Permanent Address:</td><td colspan="3"><asp:Label ID="txtPAddress" runat="server"  Width="60%"></asp:Label></td></tr>
<tr><td></td><td colspan="3"><asp:Label ID="txtAddressHead2" runat="server"  Width="60%"></asp:Label></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPCity" runat="server" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPState" runat="server"  ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPPincode" runat="server" ></asp:Label></td></tr>


<tr><td>Correspondence Address:</td><td colspan="3"><asp:Label ID="txtCAddress" runat="server"  Width="60%"></asp:Label></td></tr>
<tr><td></td><td colspan="3"><asp:Label ID="txtCAddressHead2" runat="server"  Width="60%"></asp:Label></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCCity" runat="server" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCState" runat="server"  ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCPin" runat="server" ></asp:Label></td></tr>


<tr><td>Phone:</td><td colspan="3"><asp:Label ID="txtPhonecode" runat="server"  Width="50px"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="txtPhoneNo" runat="server" ></asp:Label></td></tr>

<tr><td>Fax:</td><td colspan="3"><asp:Label ID="txtFaxCode" runat="server"  Width="50px"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="txtFaxNo" runat="server" ></asp:Label></td></tr>


<tr><td>Mobile:</td><td><asp:Label ID="txtMobile" runat="server" ></asp:Label>
</td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp; <asp:Label ID="txtEmail" runat="server" ></asp:Label></td></tr>


<tr><td>Date of Birth:</td><td><asp:Label ID="txtDOB" runat="server" ></asp:Label> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Age:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtAge" runat="server" ></asp:Label></td></tr>



</table>
<table class="tbl"><tr><td><b>Education Qualification</b></td><td><asp:Label ID="txtEducationQ" runat="server" ></asp:Label></td></tr><tr><td>Professional Experience [year]</td><td><asp:Label ID="txtExperience" runat="server"  Width="100px"></asp:Label>&nbsp;Years.</td></tr></table>

<div class="fromRegisterlbl"><h1>Photo of IM Head:</h1></div>

<asp:Panel ID="panelImage" runat="server" >
<div id="imheadleft"><center><b>Head of Management</b><br />
  <asp:Image runat="server" ID="imgDefault"  ImageUrl="~/images/userbg.png" Height="250" Width="200px" />
        <asp:DataList ID="DataList1" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
            </asp:DataList><br /><br />
            <asp:DataList ID="dlsign" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler2.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="150px" Width="200px"  />
</ItemTemplate>
            </asp:DataList><br /><br />
<br /><br /><br /><br /></center><br /><div style="height:200px" ></div>
</div>
<div id="imheadright">
<center><b>Academic Head of Institute</b><br />
  <asp:Image runat="server" ID="Image2"  ImageUrl="~/images/userbg.png" Height="250" Width="200px" />
        <asp:DataList ID="DataList3" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
       
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler3.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
            </asp:DataList><br /><br />
            <asp:DataList ID="DataList4" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler4.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="150px" Width="200px"  />
</ItemTemplate>
            </asp:DataList>
<br /></center>


</div>

<br />
<br /><br />
</asp:Panel>

</div><br /> 
</asp:Content>

