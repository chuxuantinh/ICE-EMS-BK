<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="DeleteAdmission.aspx.cs" Inherits="Admission_DeleteAdmission_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Delete Application
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" 
        CssClass="redirecttab" onclick="lblHomeRedirect_Click"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Cancel Admission" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Cancel Admission</h1></div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers><asp:PostBackTrigger ControlID="btnOk" /></Triggers>
<ContentTemplate>

<asp:Panel ID="panlSession" runat="server" >
<br />
<center><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox"   Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;Year: <asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="txtbox" ></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"/>
Serial No:&nbsp;&nbsp;<asp:TextBox ID="txtEnter" runat="server" CssClass="txtbox"></asp:TextBox>
   

<asp:Label ID="lblMessageExc" runat="server" ForeColor="Red"></asp:Label>

&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOk" runat="server" CssClass="btnsmall" onclick="btnOk_Click" Text="View" />
    <br />
   
   </div>
    </center>
   
</asp:Panel>


</ContentTemplate></asp:UpdatePanel>

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


    <br />
    <%--<div class="fromRegisterlbl"><h1>Delete</h1></div>--%>
    <div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
<br />
<br />
</div><div style="padding:1px;"><h1>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;</h1></div>

<br />
<div id="Div12" style=" overflow:scroll;display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
 
<div id="divdatagrid2" style=" height:200px">
<asp:GridView ID="grviti" runat="server" AllowPaging="True" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  
         ForeColor="Black" GridLines="Vertical" 
        Width="100%"  
        Height="20%" 
        onrowdatabound="grviti_RowDataBound"  >
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
       <EmptyDataRowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
       <EmptyDataTemplate>No records found</EmptyDataTemplate>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div>

</div>



   </div>
 
   
   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<Triggers><asp:PostBackTrigger ControlID="btnOk"/></Triggers>

<ContentTemplate>
       <center>
           <asp:RadioButton ID="rbtnNewAdmission" runat="server" AutoPostBack="true" 
               GroupName="dev" 
               Text="New Admission" />
           <asp:RadioButton ID="rbtnOldAdmission" runat="server" AutoPostBack="true" 
               GroupName="dev" 
               Text="Readmission" />
      
   

   &nbsp;&nbsp; &nbsp;&nbsp; <asp:Label ID="lblTotalAmount" runat="server" ForeColor="Black" Text="Total Amount:"></asp:Label>
    <asp:Label ID="lblAmount" runat="server" ForeColor="Black"></asp:Label>
    <br />
    <br />
  Set Old Course:&nbsp;&nbsp;<asp:Label ID="lblCourse" runat="server" ForeColor="Black" Text="Course" Visible="false" ></asp:Label>
    &nbsp;<asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox"   Width="150px" Visible="false" ><asp:ListItem Value="Civil" Text="Civil" /><asp:ListItem Value="Architecture" Text="Architecture"  /></asp:DropDownList>
   <asp:DropDownList ID="ddlpart" runat="server" CssClass="txtbox"   Width="150px" Visible="false" ><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Value="PartI" Text="PartI" /><asp:ListItem Value="SectionA" Text="SectionA" /><asp:ListItem Value="SectionB" Text="SectionB" /></asp:DropDownList>
    &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="btnsmall" 
         Text="Cancel" onclick="btnDelete_Click" />
    </center> <br /> 
    </ContentTemplate>
<Triggers><asp:PostBackTrigger ControlID="btnDelete"/>
<asp:PostBackTrigger ControlID="btnOk"></asp:PostBackTrigger>
       </Triggers>
    </asp:UpdatePanel>
    <br />
   </div>
</asp:Content>