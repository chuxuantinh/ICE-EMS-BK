<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="TrackDiary.aspx.cs" Inherits="Admin_Track_Diary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Look Diary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblCity" runat="server" Text="Look Diary(Courier) Details" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"></h1><h1>
   Look Diary Status &nbsp;&nbsp;&nbsp;&nbsp;</h1></div>
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <center> Diary No:&nbsp; <asp:TextBox ID="txtDiary" runat="server" CssClass="txtbox"></asp:TextBox> &nbsp;&nbsp; 
    <asp:Button ID="btnTrack" runat="server" CssClass="btnsmall" Text="Track" 
        onclick="btnTrack_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</center>
    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Medium" 
        ForeColor="Maroon"></asp:Label><br /><br /><br />
        <asp:Panel ID="pnlData" runat="server">
        <script>
            function toggleAe(showHideDiv, switchImgTag) {
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
 <a id="Ae" href="javascript:toggleAe('Dive', 'Ae');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:1px;"><h1>Diary Details</h1></div>
<div id="Dive" style="display:block;">
  <input id="Hidden2" runat="server" type="hidden" value="0" />
                 <div id="div3" style="width:100%; overflow:scroll; height:auto" ><br />
   <table class="tbl"><tr><td><b>Diary Type:</b></td><td><asp:Label ID="lbltype" runat="server"></asp:Label></td><td>
       &nbsp; </td> <td>
           <b>Diary From:</b></td><td><asp:Label ID="lblMemberType" runat="server"></asp:Label></td><td>
       <strong>Submitted To:</strong><asp:Label ID="lblSubmittedTo" runat="server"></asp:Label></td></tr>
   <tr><td><b>Membership:</b></td><td><asp:Label ID="lblMembership" runat="server"></asp:Label></td><td>
       &nbsp;&nbsp;</td> <td>
           <b>Name:</b></td><td><asp:Label ID="lblName" runat="server"></asp:Label></td><td></td></tr>
   <tr><td><b>Courier Service:</b></td><td><asp:Label ID="lblCourierService" runat="server"></asp:Label></td><td>
       &nbsp;&nbsp;</td> <td>
           <b>Courier No:</b></td><td colspan="2"><asp:Label ID="lblCourierNo" runat="server"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;<strong>Consignment No:</strong><asp:Label ID="lblConsignment" runat="server"></asp:Label></td></tr>
   <tr><td><b>Dispatch Date:</b></td><td><asp:Label ID="lblDispatch" runat="server"></asp:Label></td><td>
       &nbsp;&nbsp;</td> <td>
           <b>Opened Date:</b></td><td><asp:Label ID="lblOpenDate" runat="server"></asp:Label></td><td></td></tr>
   </table>
   <asp:Panel ID="pnlletters" runat="server"><table class="tbl"><tr><td><strong>Letter 
       From</strong><b>:</b></td><td><asp:Label ID="lblLetterFrom" runat="server"></asp:Label></td><td>
       &nbsp; </td> <td>
           <b>Letter To:</b></td><td><asp:Label ID="lblLetterTo" runat="server"></asp:Label></td><td></td></tr>
   <tr><td><b>Subject:</b></td><td><asp:Label ID="lblSubject" runat="server"></asp:Label></td><td>
       &nbsp;&nbsp;</td> <td>
           &nbsp;</td><td>&nbsp;</td><td></td></tr>
   <tr><td><b>Receive Date:</b></td><td><asp:Label ID="ReceiveDate" runat="server"></asp:Label></td><td>
       &nbsp;&nbsp;</td> </tr>
  
   </table></asp:Panel></div></div></div><br /><br />
   
    <script>
        function toggleAa(showHideDiv, switchImgTag) {
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
 <a id="A1a" href="javascript:toggleAa('Div1a', 'A1a');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:1px;"><h1>Count in Diary <asp:Label ID="lblDF" runat="server"></asp:Label></h1></div>
<div id="Div1a" style="display:none;">
  <input id="Hidden1" runat="server" type="hidden" value="0" />
                 <div id="div2" style="overflow:scroll; height:auto" >
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <br /><table width="100%"><tr><td><table width="100%" style="background:gray; height: 227px;"><tr><td colspan="3" align="center" style="background:Maroon;"><h2 style="color:Black">DD Count</h2></td></tr><tr><td></td><td>Received</td><td>Submitted</td></tr>
<tr><td>Academic DD</td><td><asp:Label ID="lblADDRcv" ForeColor="White" runat="server" ></asp:Label></td><td><asp:Label ForeColor="White" ID="lblADDSub" runat="server" ></asp:Label></td></tr>
<tr><td>Others DD</td><td><asp:Label ID="lblODDRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblODDSub"  ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Project DD</td><td><asp:Label ID="lblProjectRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProjectSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Membership DD</td><td><asp:Label ID="lblMembershipRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblMembershipSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
            <tr><td>Books DD</td><td><asp:Label ID="lblBooksRcv" runat="server" 
                    ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblBooksSub" 
                        runat="server" ForeColor="White"  ></asp:Label></td></tr>
            <tr><td>Prospectus DD</td><td><asp:Label ID="lblProsRcv" runat="server" 
                    ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProsSub" 
                        runat="server" ForeColor="White"  ></asp:Label></td></tr></table></td><td><table width="100%" height="100%" style="background:gray"><tr><td colspan="3" align="center" style="background:Maroon"> 
                             <h2 style="color:Black">Form Count</h2></td></tr><tr><td></td><td>Received</td><td>Submitted</td></tr>
<tr><td>Admission Form</td><td><asp:Label ID="lblAdmissionRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblAdmissionSub" ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Exam Form</td><td><asp:Label ID="lblExamRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblExamSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>ITI Form</td><td><asp:Label ID="lblITIRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblITISub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Others Form</td><td><asp:Label ID="lblOthersFormRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblOthersFormSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Provisional</td><td><asp:Label ID="lblProvisionalRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProvisionalSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Final Pass</td><td><asp:Label ID="lblFinalPassRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblFinalPassSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Re-Checking</td><td><asp:Label ID="lblReCheckingRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblReCheckingSub" runat="server"  ForeColor="White" ></asp:Label></td></tr>
<tr><td>Duplicate Docs</td><td><asp:Label ID="lblDuplicateRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblDuplicateSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>

<tr><td>Project ProformaB</td><td><asp:Label ID="lblProformaBRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaBSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaC</td><td><asp:Label ID="lblProformaCRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaCSub" runat="server" ForeColor="White"></asp:Label></td></tr>

</table></td></tr></table></div></div></div>

 
   
   <br /><br />

<script>
    function toggleAd(showHideDiv, switchImgTag) {
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
 <a id="Ad" href="javascript:toggleAd('Divd', 'Ad');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:1px;"><h1>Forms in Diary <asp:Label ID="lblDNo" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Approved:<asp:Label ID="lblApp" runat="server"></asp:Label> &nbsp;&nbsp; 
        NotApproved:<asp:Label ID="lblNotApp" runat="server"></asp:Label> &nbsp;&nbsp;&nbsp; Hold:<asp:Label ID="lblHold" runat="server"></asp:Label> </h1></div>
<div id="Divd" style="display:none;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width:100%; overflow:scroll; height:400px" >
<asp:GridView ID="grdForms" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" 
                BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Width="100%" 
                         onrowdatabound="grdForms_RowDataBound">
    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
    <EditRowStyle Wrap="True" />
    <EmptyDataRowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FFF1D4" />
    <SortedAscendingHeaderStyle BackColor="#B95C30" />
    <SortedDescendingCellStyle BackColor="#F1E5CE" />
    <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView></div></div></div>
</asp:Panel><asp:Panel ID="pnlSpace" runat="server" Height="400px"></asp:Panel>
</div>
<br /><br /><br /><br /><br /><br />
</asp:Content>

