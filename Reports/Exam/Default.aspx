<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reports_Exam_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Examination Reporting</title>
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
        <link rel="stylesheet" href="../../style.css" type="text/css" charset="utf-8" />
        <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div id="page">
    <div id="content">
    <div id="welcome"><asp:ImageButton ID="btnNoredird" runat="server" ImageUrl="~/images/invisible.gif"  AlternateText="." TabIndex="1" /><asp:ImageButton ID="ibtnHomeIcon" TabIndex="20" runat="server" ImageUrl="~/images/home.png" ToolTip="Home" AlternateText="Home" Height="20px" Width="20px" OnClick="ibtnHOme_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSettings" runat="server" Text="Settings"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:40px;">
         <asp:Label ID="lbltest" runat="server" ></asp:Label>  
            <asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/logo.gif" AlternateText="ICE(I)"  width="50%" /></a><br />
    <div id="redline"></div>
    <div id="panelHeader" runat="server" >
                          <div id="usermanage" runat="server"  >
    <table width="40%"><tr align="center"><td align="center"><asp:ImageButton ID="imgbtnCreate" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            ImageUrl="~/images/createcolor.png"/><br />Create New</td><td>
            <asp:ImageButton ID="imgbtnRecover" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
            ImageUrl="~/images/user_update.png" onclick="imgbtnRecover_Click"/><br />Recover Password</td><td><asp:ImageButton ID="imgbtnDelete" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
             ImageUrl="~/images/user_delete.png"/><br />Disactive</td><td>
            <asp:ImageButton ID="imgbtnManage" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Create New" 
             ImageUrl="~/images/Report.png" onclick="imgbtnManage_Click"/><br />All Reports</td></tr></table>
    </div>        
    </div>
 <hr />
   <div id="leftpanel2">
<div id="leftteg" >
<asp:Panel ID="pnlFO" runat="server" >
   <script>
       function toggle98(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../../images/minus.png">';
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="imageDivLink98" href="javascript:toggle98('contentDivImg98', 'imageDivLink98');"><img src="../../images/minus.png" alt="Show"></a>
</div><h1>Examination Reports</h1>
<div id="contentDivImg98" style="display: block;"> 
  <br />
   <div class="leftlist">
   
  <ul>
  <li>Exam Form<ul><li><asp:LinkButton ID="lbtnExamform" runat="server" Text="Fee Status" CssClass="leftlink" OnClick="lbtnExamForm_OnClick">ExamForm Report</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnExamDate" runat="server" Text="Exam Date" CssClass="leftlink" OnClick="lbtnExamDate_OnClick">Exam Form Via Exam Date Report</asp:LinkButton></li>    
       <li><asp:LinkButton ID="lbtnExamSNApp" runat="server" Text="Exam Serial No App" CssClass="leftlink" OnClick="lbtnExamSNApp_OnClick">Exam Form Serial No (Approved)</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnToBeFillExamForm" runat="server" Text="Exam Form Not  Filled and Approved" CssClass="leftlink" OnClick="lbtnToBeExamFormFill_OnClick">Exam Form Not Filled and Approved</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnExamSN" runat="server" Text="Exam Serial No" CssClass="leftlink" OnClick="lbtnExamSNRpt_OnClick">Exam Form Serial No</asp:LinkButton></li>      
       
  </ul> </li>
  <li >Exam Center<ul>  
  <li><asp:LinkButton ID="lbtnAdmitCard" runat="server"  Text="Admit Card" CssClass="leftlink" OnClick="lbtnAdmitCard_OnClick">Admit Card </asp:LinkButton> </li>
  <li><asp:LinkButton ID="lbtnAdmitCArdDuplicate" runat="server"  Text="Admit Card Duplicate" CssClass="leftlink" OnClick="lbtnAdmitCardDuplicate_OnClick">Admit Card Duplicate</asp:LinkButton> </li>
  
  <li><asp:LinkButton ID="lbtnAdmitCardExam" runat="server"  Text="Admit Card Old" CssClass="leftlink" OnClick="lbtnAdmitCardExam_OnClick">Admit Card Old</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAttendanceRooms" runat="server"  Text="Attendance Sheet" CssClass="leftlink" OnClick="lbtnAttendanceSheetRoom_OnClick">Attendance Sheet Rooms</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAttedanceSheet" runat="server"  Text="Attendance Sheet" CssClass="leftlink" OnClick="lbtnAttendanceSheet_OnClick">Attendance Sheet Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAttendanceSheetSummary" runat="server"  Text="Attendance Sheet Summary" CssClass="leftlink" OnClick="lbtnExamCenterSummary_OnClick">Attendance Sheet Summary</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lblExamCenter" runat="server"  Text="Exam Center" CssClass="leftlink" OnClick="lbtnExamCenter_OnClick">Student Count in Center</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lblSubjectStudentCount" runat="server"  Text="Center Paperwise" CssClass="leftlink" OnClick="lblSubjectStudentCount_OnClick">Student Count in Paper Code</asp:LinkButton></li>
       <li><asp:LinkButton ID="lblCenterStudentCount" runat="server"  Text="Center Date Student Count" CssClass="leftlink" OnClick="lbtnCenterDateStudentCount_OnClick">Student Count in Shift/Date</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lblCenterPaperwise" runat="server"  Text="Center Paperwise" CssClass="leftlink" OnClick="lblCenterPaperwise_OnClick">Student Count in Paper Code/Date/Center</asp:LinkButton></li>
       <li><asp:LinkButton ID="lblMatrixReprot" runat="server"  Text="" CssClass="leftlink" OnClick="lblMatrixCenterReport_OnClick">Matrix Student Count via Shift/Date</asp:LinkButton></li>
       <li><asp:LinkButton ID="lblPaperCode" runat="server"  Text="" CssClass="leftlink" OnClick="lblPaperCode_OnClick">Booklet Range ICE(I)</asp:LinkButton></li>
       <li><asp:LinkButton ID="lblPaperCodeExamCenter" runat="server"  Text="" CssClass="leftlink" OnClick="lblPaperCodeExamCenter_OnClick">Booklet Range Exam Center</asp:LinkButton></li>
       </ul></li>
<li>Re-Checking <ul>
    <li><asp:LinkButton ID="lbtnReCheckingFormSubmitted" runat="server"  Text="" CssClass="leftlink" OnClick="lbtnReCheckingFormSubmitted_OnClick">Re-Checking Form Submitted</asp:LinkButton> </li>
    <li><asp:LinkButton ID="lbtnReCheckingResult" runat="server"  Text="" CssClass="leftlink" OnClick="lbtnReCheckingResult_OnClick">Re-Checking Result</asp:LinkButton> </li>
</ul></li>
           <li><asp:LinkButton ID="lbtnExamAdmission" runat="server" Text="Admission Form Report" CssClass="leftlink" OnClick="lbtnExamAdmissionRpt_OnClick">Admission Report</asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnFeeStatus" Visible="false" runat="server" Text="Fee Status" CssClass="leftlink" OnClick="lbtnFeeStatusExam_OnClick">Fee Status Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnMarksStatementsExam" runat="server" Text="Marks Statements" CssClass="leftlink" OnClick="lbtnMarksStatementsExam_OnClick">Marks Statements</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnExportResult" runat="server" Text="Export Result" CssClass="leftlink" OnClick="lbtnExportResult_OnClick">Export Result</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnFormType" runat="server" Text="Form Type" CssClass="leftlink" OnClick="lbtnFormType_OnClick">Application Forms Via Status</asp:LinkButton> </li>
    <li> <asp:LinkButton ID="lbtnExamFormSubmitted" runat="server" Text="Exam Form Submitted" CssClass="leftlink" OnClick="llbtnExamFormSubmitted_OnClick">Exemption Form at A/C Section</asp:LinkButton></li>
      <li><asp:LinkButton ID="lbtnExemSubject" runat="server" Text="Exam Form Submitted" CssClass="leftlink" OnClick="lbtnExemSubject_OnClick">Exempted Subjects Record</asp:LinkButton></li>
   </ul>
    </div></div>
    </div>
    </asp:Panel>
</div>
</div>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server"  Text="Home" CssClass="redirecttab" 
        onclick="lblHomeRedirect_Click"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="All Reports" 
            CssClass="redirecttab" onclick="lbtnNext1Redirect_Click" 
             ></asp:LinkButton> </td><td><asp:Label ID="lblEnquiryHome" runat="server" Text="Exam Reports" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1>All Exam Reports</h1></div>
                 <hr />
                 <table width="100%" style="text-align:center;"><tr><td><asp:ImageButton ID="PrintCha" runat="server"  ImageUrl="~/images/Challan.jpg"
            CssClass="imgbtncreate1"  AlternateText="Print Challan" /><br /> <strong>Print Chalan</strong>  </td><td><asp:ImageButton ID="PrintAttandanceSheet" runat="server" ImageUrl="~/images/Entry.jpg"
            CssClass="imgbtncreate1"  AlternateText="PrintAttandance" 
            /><br /><strong>Print Attandance Sheet</strong></td><td><asp:ImageButton ID="AdmitCard" runat="server" ImageUrl="~/images/Printadmit.jpg"
            CssClass="imgbtncreate1"  AlternateText="Print Admit Card" 
            /><br /><strong>Print Admit Card </strong></td>
            <td><asp:ImageButton ID="PrintPaperCode" runat="server" 
            CssClass="imgbtncreate1"  AlternateText="PrintAdmitCard" 
            ImageUrl="~/images/Report.png"/><br /><strong>Print PaperCode Status</strong></td>
           <td> 
               <asp:ImageButton ID="PrintExam" runat="server" 
            CssClass="imgbtncreate1"  AlternateText="PrintAdmitCard" 
            ImageUrl="~/images/Examcenter.jpg" onclick="PrintExam_Click"/><br /><strong>Print Exam Center Status</strong></td>

            
            </tr></table>


<br /><br />  <br /> <br /> <br /> <br /> <hr /> <br /> <br /> <br />


 
            <table width="100%" style="text-align:center;"><tr><td><asp:ImageButton ID="PrintTenat" runat="server"  ImageUrl="~/images/printList.jpg"
            CssClass="imgbtncreate1"  AlternateText="PrintTentitiveCandidate" /><br /><strong>Print Tentitive Candidate List</strong> </td><td><asp:ImageButton ID="PrintSetting" runat="server" ImageUrl="~/images/Print1.jpg"
            CssClass="imgbtncreate1"  AlternateText="Print Seeting Arrangment" 
            /><br /><strong>Print Seeting Arrangment</strong></td><td><asp:ImageButton ID="PrintMattrix" runat="server" ImageUrl="~/images/Print2.jpg"
            CssClass="imgbtncreate1"  AlternateText="Print Matrix Student Count" 
            /><br /><strong>Print Mattrix Student Count</strong></td>
            <td><asp:ImageButton ID="ExportExamdata" runat="server" 
            CssClass="imgbtncreate1"  AlternateText="ExportExamData" 
            ImageUrl="~/images/index1.jpg"/><br /><strong>Export Exam Data</strong></td>
           <td> <asp:ImageButton ID="BookletRange" runat="server" 
            CssClass="imgbtncreate1"  AlternateText="PrintBooklet" 
            ImageUrl="~/images/BookLet.jpg"/><br /><Strong>Print Booklet Range</Strong></td>

            
            </tr></table>





<br /><br /><br /><br /><br /><br /><br /><br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
    </div>
             </div>
   <br /><br />
    </div><br />
    </div>
    <!-- footer -->
    <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Help & Support</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    <div>
    </div>
    </form>
</body>
</html>
