<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="AddAppsPro.aspx.cs" Inherits="Acc_AddAppsPro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Add Project Forms</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblAount" runat="server" Text="Project Proforma B/C" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fee Master:&nbsp;<asp:Label ID="lblFeemaster" runat="server"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Submit Project Proforma:</h1></div>
<center>Diary No:&nbsp;&nbsp;<asp:TextBox ID="txtDiaryNo" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Image ID="ibtnViewDairy" ImageUrl="~/images/dairycount.gif"  runat="server" AlternateText="Dairy" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnViewDiary" runat="server" Text="View" OnClick="btnViewDiary_Click" CssClass="btnsmall" /></center>
<center><asp:Label ID="lblException" runat="server" ></asp:Label></center>
<dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-550" OffsetY="0" PopupControlID="pnlDairyCount" TargetControlID="ibtnViewDairy" ></dev:PopupControlExtender>
<asp:Panel ID="pnlDairyCount" runat="server" Width="350px" CssClass="pnlpopup">
<div class="redsubtitle"><center>Application Form Count</center></div>
<table width="100%"><tr><td>Applications</td><td>Total Received</td><td>Total Submitted</td></tr>
<tr><td>Academic DD</td><td><asp:Label ID="lblADDRcv" ForeColor="White" runat="server" ></asp:Label></td><td><asp:Label ForeColor="White" ID="lblADDSub" runat="server" ></asp:Label></td></tr>
<tr><td>Others DD</td><td><asp:Label ID="lblODDRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblODDSub"  ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Admission Form</td><td><asp:Label ID="lblAdmissionRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblAdmissionSub" ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Exam Form</td><td><asp:Label ID="lblExamRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblExamSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>ITI Form</td><td><asp:Label ID="lblITIRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblITISub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Others Form</td><td><asp:Label ID="lblOthersFormRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblOthersFormSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Provisional</td><td><asp:Label ID="lblProvisionalRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProvisionalSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Final Pass</td><td><asp:Label ID="lblFinalPassRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblFinalPassSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Re-Checking</td><td><asp:Label ID="lblReCheckingRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblReCheckingSub" runat="server"  ForeColor="White" ></asp:Label></td></tr>
<tr><td>Duplicate Docs</td><td><asp:Label ID="lblDuplicateRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblDuplicateSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project DD</td><td><asp:Label ID="lblProjectRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProjectSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaA</td><td><asp:Label ID="lblProformaARcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaASub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaB</td><td><asp:Label ID="lblProformaBRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaBSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaC</td><td><asp:Label ID="lblProformaCRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaCSub" runat="server" ForeColor="White"></asp:Label></td></tr>
<tr><td>Membership DD</td><td><asp:Label ID="lblMembershipRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblMembershipSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Books DD</td><td><asp:Label ID="lblBooksRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblBooksSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Prospectus DD</td><td><asp:Label ID="lblProsRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProsSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
</table>
</asp:Panel>
<asp:Panel ID="pnlMain" runat="server">
<table class="tbl"><tr><td>IMID:</td><td><asp:Label ID="lblIMID" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
<tr><td>Diary Date:</td><td><asp:Label ID="lblDairyDate" runat="server"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;Session:&nbsp;&nbsp;<asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label></td></tr>
</table>
<hr />
<center>Membership No:&nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btnsmall" />
<br /><asp:Label ID="lblStudentName" runat="server" CssClass="txt2" ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPrifix" runat="server" CssClass="txt2"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblFName" runat="server" CssClass="txt2"></asp:Label>
<asp:Label ID="lblCourse" runat="server" CssClass="txt1"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPart" runat="server" CssClass="txt1"></asp:Label>
</center>
<asp:Panel ID="pnlSub" runat="server">
<table class="tbl" width="100%"><tr><td>Project Status:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblProStatus" runat="server" ></asp:Label></td><td>Synopsis Status:&nbsp;&nbsp;<asp:Label ID="lblSynopsisStatus" runat="server"></asp:Label></td></tr>
<tr><td>[Approval]Proforma B Fees:&nbsp;&nbsp;<asp:Label ID="lblProformaBFees" runat="server"></asp:Label></td><td>[Evaluation]Proforma C Fees:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblProformaCFees" runat="server"></asp:Label></td></tr>
<tr><td colspan="2" align="center">
    <asp:CheckBox ID="chkDupForm" runat="server" AutoPostBack="True" 
        oncheckedchanged="chkDupForm_CheckedChanged" Text="With  Form" />
    &nbsp;<asp:Label ID="lblDupForm" runat="server" Forecolor="Brown" Text="YES"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Select Proforma:&nbsp;&nbsp;<asp:DropDownList 
        ID="ddlProforma" runat="server"
        CssClass="txtbox" AutoPostBack="true" 
        onselectedindexchanged="ddlProforma_SelectedIndexChanged" ><asp:ListItem Value="ProformaB" Text="ProformaB" /><asp:ListItem Value="ProformaC" Text="ProformaC" /></asp:DropDownList></td></tr>
</table>

<!-- Hidden Fields -->
<asp:Label ID="lblFeeLevel" runat="server" ></asp:Label><asp:Label ID="lblDoB" runat="server" Visible="false"></asp:Label>
<!-- End Hidden Fields Region -->
<center><asp:Label ID="lblserialNo" runat="server" ></asp:Label>&nbsp;<asp:Label ID="lblProjSerialNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="txtbox" OnClick="btnSubmit_Click"  OnClientClick="return confirm('Confirm Submit Form ?');"    /></center>
<hr />
<asp:Panel ID="PanelProjects" runat="server" >
  <center><font style="color:Maroon; font-size:18px; font-family:Times New Roman; padding:0px;">Project Fee Master</font></center><br />
  <table width="80%" class="tbl"><tr><td>Approval Fees:&nbsp;&nbsp;<asp:Label ID="lblProjectApproval" runat="server" Font-Bold="true" ForeColor="Maroon" ></asp:Label>&nbsp;</td><td>&nbsp;&nbsp;Evaluation Fees:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblProjectEvaluation" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label>&nbsp;</td></tr></table>
</asp:Panel>
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
        &nbsp;&nbsp;&nbsp;&nbsp;<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>
Project Fees:&nbsp;&nbsp;<asp:Label ID="lblSubmitFees" runat="server"></asp:Label></h1>
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:200px">
    <asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true" OnRowDataBound="GridAppTable_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" 
        CellSpacing="5" Width="100%">
        <Columns>
        </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
   </div>
</div>
</div>
</asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlHeight" runat="server" Height="400px"></asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>