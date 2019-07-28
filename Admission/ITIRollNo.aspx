<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ITIRollNo.aspx.cs" Inherits="Admission_ITIRollNo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">ITI Roll No
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Generate ITI RollNo" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>ITI Application</h1></div>
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
<asp:ImageButton ID="ibtnExportDoc" runat="server" Visible="false" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTable_click" Height="25px" Width="25px" />&nbsp;&nbsp;<asp:ImageButton ID="ibtnExportExcel" runat="server" AlternateText="Excel" Visible="false" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTable_Click"  Height="25px" Width="25px"/>&nbsp;&nbsp;<asp:ImageButton ID="ibtnExportPDF" Visible="false" runat="server"  AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTable_Click"  Height="25px" Width="25px"/>    
     &nbsp;&nbsp;&nbsp;&nbsp;<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"/></a>
</div><h1>Generate RollNo:</h1>
    <br />
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
 <center>
 <table >

<tr><td>Session:&nbsp;<asp:DropDownList ID="ddlSessionSelect" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_ONSelectediNdexCanged" Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;Year: <asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYear_OnTextChanged"></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"/></td><td colspan="3">Select Status:
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" 
        Width="146px" AutoPostBack="True" 
        onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                    <asp:ListItem>ReadyForExam</asp:ListItem>
                    <asp:ListItem Value="Roll No Generated">RollNoGenerated</asp:ListItem>
                </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            </td></tr>
</table>
</center>
 <script type="text/javascript">
     function selectAll(invoker) {
         var inputElements = document.getElementsByTagName('input');
         for (var i = 0; i < inputElements.length; i++) {
             var myElement = inputElements[i];
             if (myElement.type === "checkbox") {
                 myElement.checked = invoker.checked;
             }
         }
     } 
</script>

<script type="text/javascript" language="javascript">
    function ConfirmApp() {
        if (confirm("Are you sure you want to Gnerate Roll No ?") == true)
            return true;
        else
            return false;
    }
   </script>
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:350px">
<asp:GridView ID="grviti" runat="server" BackColor="White" OnPageIndexChanging="grviti_PageIndexChanging" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" 
        Height="80%" onrowdatabound="grviti_RowDataBound" DataKeyNames="SID"  >
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="cbSelectAll" runat="server" OnClick="selectAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkStatus" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
</asp:GridView>


</div>
<div><center><asp:Panel ID="panRollNo" runat="server" Visible="false">  
    <center><h1 style="color:Maroon">Generate RollNo</h1></center>
<table><tr><td align="center">Exam Date:</td><td><asp:TextBox ID="txtExamDate" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender3" PopupButtonID="Img2" PopupPosition="BottomRight" runat="server" TargetControlID="txtExamDate"></dev:CalendarExtender>
<img src="../images/cal.png" id="Img2" runat="server"  alt="Cal" />&nbsp;<asp:Button 
        ID="btnSave" runat="server" Text="Generate RollNo " CssClass="btnsmall"  OnClientClick="return ConfirmApp();"
                    onclick="btnSave_Click" />
            </td></tr>
            <tr><td colspan="2" align="center"><asp:Label ID="lblStatus" runat="server" ForeColor="Green"></asp:Label></td></tr></table>

</asp:Panel></center>
</div>
<div><asp:Panel ID="pnlStudent" runat="server" Visible="false">
<center><h1 style="color:Maroon">Update Status</h1></center>
&nbsp;
<center>&nbsp;
                <asp:Button ID="btnPass" runat="server" CssClass="btnsmall" 
                    onclick="btnApprove_Click" Text="Pass" />
          &nbsp;&nbsp;
                <asp:Button ID="btnfail" runat="server" CssClass="btnsmall" 
                     Text="Fail" onclick="btnfail_Click" />
                    </center>
          <br />
</asp:Panel>
</div>
</div>
    </div>
    </div>
</asp:Content>

