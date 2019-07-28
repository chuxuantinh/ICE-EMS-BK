<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="PromoteAdmission.aspx.cs" Inherits="Admission_Promote_Admission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Promote Admission</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Promote Student" CssClass="redirecttabhome"></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers><asp:PostBackTrigger ControlID="btnOk" /></Triggers>
<ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Promote Student For Next Session</h1></div>
<br />
<asp:Panel ID="panlSession" runat="server" ><table width="90%" class="tbl"><tr>
<td class="style2" align="right">Session:</td>
<td colspan="3" class="style3"><asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_ONSelectediNdexCanged" Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>
&nbsp;&nbsp; Year: <asp:TextBox ID="txtYear" runat="server" Width="50px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYear_OnTextChanged"></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"></asp:Label></td></tr><tr>
    <td class="style1" align="right">Select Criteria:</td>
    <td colspan="3">
    <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="True" onselectedindexchanged="ddlSelect_SelectedIndexChanged" CssClass="txtbox" Width="150px">
        <asp:ListItem>Membership</asp:ListItem>
        <asp:ListItem>IMID</asp:ListItem>
        <asp:ListItem Value="SerialNo">Serial No</asp:ListItem>
        <asp:ListItem>All</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtEnter" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
        &nbsp;&nbsp;<asp:Button ID="btnOk" runat="server" CssClass="btnsmall" onclick="btnOk_Click" Text="OK" />
        &nbsp;&nbsp; 
    </td></tr></table></asp:Panel>
</ContentTemplate></asp:UpdatePanel>
<br />
<center><asp:Label ID="lblMessageE" runat="server" ForeColor="Red"></asp:Label></center>
<center><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="Promote" OnClick="btnVeiw_OnClick" /></center>
        <br />
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
<div class="togalfees" style="width:100%">
 <div class="headerDivImgfees">
    
   <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
   
    <a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div>
    <h1>Total Forms:<asp:Label 
            ID="lblTotalForms" runat="server"></asp:Label>
    <asp:Button ID="btnSelectAll" CssClass="btnsmall" runat="server" Text="Select All" OnClick="btnSelectAll_Onclick"  /></h1>
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:450px">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  PageSize="25" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onrowdatabound="GridView_RowDataBound" 
        onselectedindexchanged="GridView_SelectedIndexChanged" >
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <EmptyDataTemplate><center>No Record Found.</center></EmptyDataTemplate>
        <Columns>
           <asp:TemplateField ><HeaderTemplate>Approve</HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
           
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
   
   <asp:GridView ID="grd" runat="server" AllowPaging="True" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  PageSize="25" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" >
         <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
         
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" /></asp:GridView>
   </div>
</div>
</div>
<asp:Panel ID="pnlspc" runat="server" Height="108px"></asp:Panel>
</div>
</asp:Content>

