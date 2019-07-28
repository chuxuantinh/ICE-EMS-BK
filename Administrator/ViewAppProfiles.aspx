<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ViewAppProfiles.aspx.cs" Inherits="Administrator_ViewAppProfiles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">View All Membership Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .binaryImage img
        {
            border: 1px solid;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <asp:ScriptManager ID="scrptmanager" runat="server"> </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="update" runat="server" ><Triggers><asp:PostBackTrigger  ControlID ="Button1" /><asp:PostBackTrigger  ControlID ="GridDuplicacy" />  </Triggers><ContentTemplate>
<div></b>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblSearch" runat="server" Text="Search By:"></asp:Label>
&nbsp;
    <asp:DropDownList ID="ddlSearch" runat="server" onselectedindexchanged="ddlSearch_SelectedIndexChanged" AutoPostBack="true" CSSClass="txtBox">
        <asp:ListItem>Status</asp:ListItem>
        <asp:ListItem>Name</asp:ListItem>
         <asp:ListItem>IMID</asp:ListItem>
          <asp:ListItem>State</asp:ListItem>
          <asp:ListItem>All Members</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="lblname" runat="server" Text="Name" CSSClass="txtBox"> </asp:Label>
    &nbsp;<asp:TextBox ID="txtname" runat="server" CSSClass="txtBox" 
        ontextchanged="txtname_TextChanged"></asp:TextBox>
    &nbsp;
    <asp:Label ID="lblid" runat="server" Text="Enter ID"></asp:Label>
    <asp:TextBox ID="txtid" runat="server" CSSClass="txtBox" 
        ontextchanged="txtid_TextChanged"></asp:TextBox>
    &nbsp;
    <asp:Label ID="Lblstate" runat="server" Text="Enter State"></asp:Label>
    <asp:DropDownList ID="ddlState" runat="server" CSSClass="txtBox" 
        onselectedindexchanged="ddlState_SelectedIndexChanged">   
    </asp:DropDownList>
    <asp:RadioButton ID="rbtnactive" runat="server" Text="Registered"  
        GroupName="status" oncheckedchanged="rbtnactive_CheckedChanged1"/>
    <asp:RadioButton ID="rbtnblock" runat="server" 
        oncheckedchanged="RadioButton2_CheckedChanged" Text="Block" GroupName="status" />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="View" Width="70px" CssClass="btnsmall"/>
  &nbsp;&nbsp;
    </div></ContentTemplate></asp:UpdatePanel>
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
    <div class="headerDivImgfees"><asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div>
<%--<div style="padding:5px;">Membership Info</div>--%>
<div id="Div1x" style="display:block;">

                     <br /><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:350px">
                 <asp:GridView ID="GridDuplicacy" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" 
        BorderWidth="1px"  AutoGenerateColumns="False"
        CellPadding="8" CellSpacing="8" OnRowDataBound="GridDuplicate_OnRowDateBound"
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%" EmptyDataText="N/A" 
                         onrowcommand="GridDuplicacy_RowCommand" 
                         onpageindexchanging="GridDuplicacy_PageIndexChanging" 
                         onselectedindexchanged="GridDuplicacy_SelectedIndexChanged">
        <EmptyDataTemplate><center>  Record Not found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText=" Membership ID" SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
           <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" /> 
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Type" HeaderText="Member Type" SortExpression="Type" Visible="false" />
            <asp:BoundField DataField="Active" HeaderText="Status" 
                SortExpression="Active" />
                <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkedit" runat="server" CommandName="select1" CommandArgument=<%#Eval("ID") %> >Edit</asp:LinkButton></ItemTemplate></asp:TemplateField>
            <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkview" runat="server" CommandName="select2"  CommandArgument=<%#Eval("ID") %> > View </asp:LinkButton></ItemTemplate></asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" 
            HorizontalAlign="Center" />
        <EditRowStyle HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>

   </div>
   </div>
   
   </div><br />
</asp:Content>

