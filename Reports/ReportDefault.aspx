<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ReportDefault.aspx.cs" Inherits="Reports_ReportDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>ICE Report Section</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
        <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
        <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div id="page">
    <div id="content">
    <div id="welcome"><asp:ImageButton ID="btnNoredird" runat="server" ImageUrl="~/images/invisible.gif"  AlternateText="." TabIndex="1" /><asp:ImageButton ID="ImageButton1" TabIndex="20" runat="server" ImageUrl="~/images/home.png" ToolTip="Home" AlternateText="Home" OnClick="ibtnHome_Click" Height="20px" Width="20px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" onclick="lbtnLogout_Click"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:40px;">
         <asp:Label ID="lbltest" runat="server" ></asp:Label>  <asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/logo.gif" AlternateText="ICE(I)"  width="50%" /></a><br />
    <div id="redline"></div>
    <div id="panelHeader" runat="server" >
  <div id="adminTableHeader"> </div>
    </div>
   <div id="leftpanel2"><br />
<div id="leftteg" >
<asp:Panel ID="pnlMembership" runat="server" >
    <script>
        function toggle(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="../images/plus.png">';
            }
            else {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="../images/minus.png">';

                var imgfo = document.getElementById("imageDivLink98");
                imgfo.innerHTML = '<img src="../images/plus.png">';
                imgfo = document.getElementById('imageDivLink99');
                imgfo.innerHTML = '<img src="../images/plus.png">';
                imgfo = document.getElementById('A3');
                imgfo.innerHTML = '<img src="../images/plus.png">';
                imgfo = document.getElementById('A2');
                imgfo.innerHTML = '<img src="../images/plus.png">';
                imgfo = document.getElementById('A1');
                imgfo.innerHTML = '<img src="../images/plus.png">';
                imgfo = document.getElementById('A4');
                imgfo.innerHTML = '<img src="../images/plus.png">';

                var cntfo = document.getElementById('contentDivImg98');
                cntfo.style.display = "none";
                cntfo = document.getElementById('contentDivImg99');
                cntfo.style.display = "none";
                cntfo = document.getElementById('Div2');
                cntfo.style.display = "none";
                cntfo = document.getElementById('Div6');
                cntfo.style.display = "none";
                cntfo = document.getElementById('Div8');
                cntfo.style.display = "none";
                cntfo = document.getElementById('Div4');
                cntfo.style.display = "none";
            }
           
        }
    </script>
  <div class="togelleft">
    <div class="headerDivImg">
    <a id="imageDivLink100" href="javascript:toggle('contentDivImg100', 'imageDivLink100');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Membership</h1>
<div id="contentDivImg100" style="display: none;"> 
   <br />
   <div id="leftLink">
    <ul>
   <li><asp:LinkButton ID="lbtnIM" runat="server" Text="IM Profile" CssClass="leftlink" OnClick="lbtnIM_OnClick">Member's Profile Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnSubscription" runat="server" CssClass="leftlink" OnClick="lbtnSubscription_Click">Subscription Reports</asp:LinkButton></li>
  <%--  <li><asp:LinkButton ID="lbtnIMProfile" runat="server" CssClass="leftlink" Text="IM Profile Report" PostBackUrl=""></asp:LinkButton></li>
    <li><asp:LinkButton ID="lbtnIMInspection" runat="server" CssClass="leftlink" Text="IM Inspection Report" PostBackUrl=""></asp:LinkButton></li>--%>
     </ul>
    </div>
    </div>
   </div>
   </asp:Panel>
  <asp:Panel ID="pnlFO" runat="server" >
   <script>
       function toggle98(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               var imgfo = document.getElementById('imageDivLink100');
               imgfo.innerHTML = '<img src="../images/plus.png">';
               imgfo = document.getElementById('imageDivLink99');
               imgfo.innerHTML = '<img src="../images/plus.png">';
               imgfo = document.getElementById('A3');
               imgfo.innerHTML = '<img src="../images/plus.png">';
               imgfo = document.getElementById('A2');
               imgfo.innerHTML = '<img src="../images/plus.png">';
               imgfo = document.getElementById('A1');
               imgfo.innerHTML = '<img src="../images/plus.png">';
               imgfo = document.getElementById('A4');
               imgfo.innerHTML = '<img src="../images/plus.png">';

               var cntfo = document.getElementById('contentDivImg100');
               cntfo.style.display = "none";
               cntfo = document.getElementById('contentDivImg99');
               cntfo.style.display = "none";
               cntfo = document.getElementById('Div2');
               cntfo.style.display = "none";
               cntfo = document.getElementById('Div6');
               cntfo.style.display = "none";
               cntfo = document.getElementById('Div8');
               cntfo.style.display = "none";
               cntfo = document.getElementById('Div4');
               cntfo.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="imageDivLink98" href="javascript:toggle98('contentDivImg98', 'imageDivLink98');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Front Office</h1>
<div id="contentDivImg98" style="display: none;"> 
  <br />
   <div class="leftlist">
  <ul>
       <li><asp:LinkButton ID="lbtnDiaryToDepart" runat="server" CssClass="leftlink" OnClick="lbtnD2DRpt_Click" Text="Diary Supply Details"></asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnprintrcourier" runat="server" Text="Dairy Report " OnClick="lbtnViewPerort_OnClick" CssClass="leftlink"></asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnDiaryEntry" runat="server" CssClass="leftlink" OnClick="lbtnDiaryTypeRpt_click">Diary Entry Reports</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnCourierRpt" runat="server" Text="Courier Dispatch " OnClick="lbttCourierRpt_OnClick" CssClass="leftlink"></asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnCounsellingReport" runat="server" Text="Counselling Reports" CssClass="leftlink" OnClick="lbtnCounsellingReport_OnClick"></asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnVisitorsReport" runat="server" Text="Visitors Reports" CssClass="leftlink" OnClick="lbtnVisitorsReport_OnClick"></asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnDiaryLetters" runat="server" CssClass="leftlink" OnClick="lbtnDiaryLetterRpt_Click">Diary Letters Reports</asp:LinkButton></li>
       <li><asp:LinkButton ID="lkbtnCourierService" runat="server" CssClass="leftlink" OnClick="lbtnCourierServiceRpt_click">Diary Entry CourierService</asp:LinkButton></li>
       <li><asp:LinkButton ID="lkbtnStatus" runat="server" CssClass="leftlink" OnClick="lbtnDiaryStatusRpt_Click" >Diary Status Reports</asp:LinkButton></li>
       <li><asp:LinkButton ID="lkbtnMemberType" runat="server" CssClass="leftlink" OnClick="lbtnMemberType_Click">Diary Entry Via Member Type</asp:LinkButton></li>
        <li><asp:LinkButton ID="lbtnForm" runat="server" CssClass="leftlink" OnClick="lbtnForm_Click">Forms On Hold</asp:LinkButton></li>
   </ul>
    </div></div>
    </div>
    </asp:Panel>
    <div id="pnlAdmission" runat="server" >
          <script>
              function toggle99(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../images/minus.png">';

                      var imgfo = document.getElementById("imageDivLink98");
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink100');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A3');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A2');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A1');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A4');
                      imgfo.innerHTML = '<img src="../images/plus.png">';

                      var cntfo = document.getElementById('contentDivImg98');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg100');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div2');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div6');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div8');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div4');
                      cntfo.style.display = "none";
                  }
              }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
    <a id="imageDivLink99" href="javascript:toggle99('contentDivImg99', 'imageDivLink99');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Admission</h1>
<div id="contentDivImg99" style="display: none;"> 
   <div class="leftlist"><br />
  <ul>
        <li><asp:LinkButton ID="lbtnGenMembership" runat="server" Text="Student Membership Report" CssClass="leftlink" OnClick="lbtnGenMembership_OnClick">Generated Membership Report</asp:LinkButton> </li>
        <li><asp:LinkButton ID="lbtnCourse" runat="server" Text="Admission Details" CssClass="leftlink" OnClick="lbtnCourse_OnClick">Admission Details Via Session</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnStudentProfile" runat="server" CssClass="leftlink" OnClick="lbtnStudentProfile_Click">Student Profile Reports</asp:LinkButton></li>
       <li><asp:LinkButton ID="lkbtnStudentAccount" runat="server" CssClass="leftlink" OnClick="lkbtnStudentAccount_click">Student's Application Report</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnMembershipGtd" runat="server" CssClass="leftlink" OnClick="lbtnMembershipGtd_click">Generated Memebership</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnStudentRemark" runat="server" CssClass="leftlink" OnClick="lbtnStudentRemark_click">Student Remarks</asp:LinkButton></li>
        <li> <asp:LinkButton ID="lbtnReAdmission" runat="server" CssClass="leftlink" OnClick="lbtnReAdmission_click">ReAdmission</asp:LinkButton></li>
         <li> <asp:LinkButton ID="lbtnStuExp" runat="server"  CssClass="leftlink" OnClick="lbtnStuExp_click" >Student Experience/Doc Status</asp:LinkButton></li>
        <li>ITI
        <ul>
        <li><asp:LinkButton ID="lbtnITIForms" runat="server" CssClass="leftlink" OnClick="lbtnITIForms_click">ITI Forms</asp:LinkButton></li>
        <li><asp:LinkButton ID="lbtnITILetters" runat="server" CssClass="leftlink" OnClick="lbtnITILetters_click">ITI Letter</asp:LinkButton></li>
   <li> <asp:LinkButton ID="lbtnITIExam" runat="server" CssClass="leftlink" OnClick="lbtnITIExam_click">ITI ExamDate</asp:LinkButton></li>
  <li> <asp:LinkButton ID="lbtnITIResult" runat="server" CssClass="leftlink" OnClick="lbtnITIResult_click">ITI Result</asp:LinkButton></li>
 </ul>
  </li>
      </ul>
    </div>
    </div>
   </div>
  </div>
  <div id="pnlAccounts" runat="server" >
          <script>
              function toggle21(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../images/minus.png">';

                      var imgfo = document.getElementById("imageDivLink98");
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink100');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink99');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A3');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A2');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A4');
                      imgfo.innerHTML = '<img src="../images/plus.png">';

                      var cntfo = document.getElementById('contentDivImg98');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg100');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg99');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div6');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div8');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div4');
                      cntfo.style.display = "none";
                  }
              }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
    <a id="A1" href="javascript:toggle21('Div2', 'A1');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Accounts</h1>
<div id="Div2" style="display: none;"> 
   <div class="leftlist"><br />
  <ul><li><asp:LinkButton ID="lbtnDDDate" runat="server" Text=" DD Report" CssClass="leftlink" OnClick="lbtnDDDateAcc_OnClick">DD Report Via Date/Diary</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnDDEntry" runat="server" Text="Account DD Entry" CssClass="leftlink" OnClick="lbtnDDAcc_OnClick">Account DD Entry Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAcc" runat="server" Text="Account Transection" CssClass="leftlink" OnClick="lbtnAcc_OnClick">Account Transection Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnMembership" runat="server" Text="Membership Account Report" CssClass="leftlink" OnClick="lbtnMSAcc_OnClick">Membership AC Report</asp:LinkButton> </li>       
       <li><asp:LinkButton ID="lbtnAppApprove" runat="server" Text="Application Approve Report" CssClass="leftlink" OnClick="lbtnAppApproveAcc_OnClick">Application Approve Report</asp:LinkButton> </li>       
       <li><asp:LinkButton ID="lbtnConsolidateRpt" runat="server" Text="Consolidated Amount Report" CssClass="leftlink" OnClick="lbtnConsolidateRptAcc_OnClick">Consolidated Amount Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAllExport" runat="server" Text="All Account Export" CssClass="leftlink" OnClick="lbtnAllExportAcc_OnClick">All Account Export</asp:LinkButton> </li>       
       <li><asp:LinkButton ID="lbtnExamBill" runat="server" CssClass="leftlink" OnClick="lbtnExamBill_Click" Enabled="false">Exam Bill Report</asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton9" runat="server" CssClass="leftlink" OnClick="lbtnApplicationStatus_Click" Enabled="True">Application Status</asp:LinkButton></li>
         <li><asp:LinkButton ID="LinkButton10" runat="server" CssClass="leftlink" OnClick="lbtnApplicationStatusSum_Click" Enabled="True">Application Status Sum</asp:LinkButton></li>
         <li><asp:LinkButton ID="LinkButton11" runat="server" CssClass="leftlink" OnClick="lbtnApplicationStatusCourse_Click" Enabled="True">Application Status Course</asp:LinkButton></li>
   </ul>
    </div>
    </div>
   </div>
  </div>
  <div id="pnlExam" runat="server" >
          <script>
              function toggle42(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../images/minus.png">';
                      var imgfo = document.getElementById("imageDivLink98");
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink100');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink99');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A3');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A1');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A4');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      var cntfo = document.getElementById('contentDivImg98');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg100');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg99');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div2');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div6');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div8');
                      cntfo.style.display = "none";
                  }
              }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
    <a id="A2" href="javascript:toggle42('Div4', 'A2');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Examination</h1>
<div id="Div4" style="display: none;"> 
   <div class="leftlist"><br />
 <ul>
 <li>Exam Form<ul>
       <li><asp:LinkButton ID="lbtnExamForms" runat="server" Text="ExamForm Details" CssClass="leftlink" OnClick="lbtnExamForms_OnClick"></asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnExamform" runat="server" Text="Fee Status" CssClass="leftlink" OnClick="lbtnExamForm_OnClick">ExamForm Report</asp:LinkButton></li>      
       <li><asp:LinkButton ID="lbtnExamSN" runat="server" Text="Exam Serial No" CssClass="leftlink" OnClick="lbtnExamSNRpt_OnClick">Exam Form Serial No</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnExamSNApp" runat="server" Text="Exam Serial No App" CssClass="leftlink" OnClick="lbtnExamSNApp_OnClick">Exam Form Serial No App</asp:LinkButton></li>
       <li><asp:LinkButton ID="lbtnToBeFillExamForm" runat="server" Text="Exam Form Not  Filled and Approved" CssClass="leftlink" OnClick="lbtnToBeExamFormFill_OnClick">Exam Form Not Filled and Approved</asp:LinkButton></li>
  </ul></li>
    <li>Exam Center<ul>
       <li><asp:LinkButton ID="lbtnAdmitCardExam" runat="server"  Text="Admit Card" CssClass="leftlink" OnClick="lbtnAdmitCardExam_OnClick">Admit Card Report</asp:LinkButton> </li>
       <li><asp:LinkButton ID="lbtnAttedanceSheet" runat="server"  Text="Attendance Sheet" CssClass="leftlink" OnClick="lbtnAttendanceSheet_OnClick">Attendance Sheet Report</asp:LinkButton> </li>
       </ul></li>
      <li><asp:LinkButton ID="lbtnExemSubject" runat="server" Text="Exam Form Submitted" CssClass="leftlink" OnClick="lbtnExemSubject_OnClick">Exemption Subject</asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnFeeStatus" Visible="false" runat="server" Text="Fee Status" CssClass="leftlink" OnClick="lbtnFeeStatusExam_OnClick">Fee Status Report</asp:LinkButton> </li>    
   <li><asp:LinkButton ID="lbtnExamAdmission" runat="server" Text="Admission Report" CssClass="leftlink" OnClick="lbtnExamAdmissionRpt_OnClick">Admission Report</asp:LinkButton></li>
   <li><asp:LinkButton ID="lbtnFormType" runat="server" Text="Form Type" CssClass="leftlink" OnClick="lbtnFormType_OnClick">Form Type</asp:LinkButton> </li>
    <li> <asp:LinkButton ID="lbtnExamFormSubmitted" runat="server" Text="Exam Form Submitted" CssClass="leftlink" OnClick="llbtnExamFormSubmitted_OnClick">Exemption Form</asp:LinkButton></li>
      <li><asp:LinkButton ID="lbtnMarksStatementsExam" runat="server" Text="Marks Statements" CssClass="leftlink" OnClick="lbtnMarksStatementsExam_OnClick">Marks Statements</asp:LinkButton> </li>
      <asp:LinkButton ID="lbtnExportResult" runat="server" Text="Export Result" CssClass="leftlink" OnClick="lbtnExportResult_OnClick">Export Result</asp:LinkButton>
 </ul>
    </div>
    </div>
   </div>
  </div>
  <div id="pnlInventory" runat="server" >
          <script>
              function toggle63(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../images/minus.png">';

                      var imgfo = document.getElementById("imageDivLink98");
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink100');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink99');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A2');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A1');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A4');
                      imgfo.innerHTML = '<img src="../images/plus.png">';

                      var cntfo = document.getElementById('contentDivImg98');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg100');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg99');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div2');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div8');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div4');
                      cntfo.style.display = "none";
                  }
              }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
    <a id="A3" href="javascript:toggle63('Div6', 'A3');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Inventory</h1>
<div id="Div6" style="display:none ;"> 
   <div class="leftlist"><br />
<ul>
   <li><asp:LinkButton ID="lbtnSupplier" runat="server" Text="Supplier Report" CssClass="leftlink" OnClick="lbtnSupplier_OnClick"></asp:LinkButton></li>
 <li><asp:LinkButton ID="lbtnIMBooks" runat="server" Text="Auto Generated Order[IM]" CssClass="leftlink" OnClick="lbtnIMBooks_OnClick"></asp:LinkButton></li>
 <li><asp:LinkButton ID="lbtnSumMaster" runat="server" Text="Books Stoke  Details" CssClass="leftlink" OnClick="lbtnSumMaster_OnClick"></asp:LinkButton></li>
 <li><asp:LinkButton ID="lbtnPurches" runat="server" Text="Purchase Order ICE(I)" CssClass="leftlink" OnClick="lbtnPurches_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnIMOrder" runat="server" Text="IM Order Details" CssClass="leftlink" OnClick="lbtnIMOrder_OnClick"></asp:LinkButton></li>
   <li><asp:LinkButton ID="lbtnIMOrderList" runat="server" Text="IM OrderList Report" CssClass="leftlink" OnClick="lbtnIMOrderList_OnClick"></asp:LinkButton></li>
   <li><asp:LinkButton ID="lbtnIMStock" runat="server" Text="IM Stock Report" CssClass="leftlink" OnClick="lbtnIMStock_OnClick"></asp:LinkButton></li>
   </ul>
    </div>
    </div>
   </div>
  </div>
  <div id="pnlProjects" runat="server" >
          <script>
              function toggle84(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../images/minus.png">';
                      var imgfo = document.getElementById("imageDivLink98");
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink100');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('imageDivLink99');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A3');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A2');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      imgfo = document.getElementById('A1');
                      imgfo.innerHTML = '<img src="../images/plus.png">';
                      var cntfo = document.getElementById('contentDivImg98');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg100');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('contentDivImg99');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div2');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div6');
                      cntfo.style.display = "none";
                      cntfo = document.getElementById('Div4');
                      cntfo.style.display = "none";
                     
                  }
              }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
    <a id="A4" href="javascript:toggle84('Div8', 'A4');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Projects</h1>
<div id="Div8" style="display:none;"> 
   <div class="leftlist"><br />
 <ul>
 <li>Letter
  <ul>
   <li><asp:LinkButton ID="lbtnIMLetter" runat="server" Text="IM Letter Letter" CssClass="leftlink" OnClick="lbtnIMLetter_OnClick"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnAicteLetter" runat="server"  Text="AICTE Letter Report" CssClass="leftlink" onclick="lbtnAicteLetter_Click"></asp:LinkButton></li>
         <li><asp:LinkButton ID="lbtnStudentLetter" runat="server" 
          Text="Student Letter Report" CssClass="leftlink" 
          ></asp:LinkButton>
          <ul><li><asp:LinkButton ID="lbtnStuRejectedLetter" runat="server"  
          Text="Project Rejected Letter" CssClass="leftlink" onclick="lbtnStuRejectedLetter_Click" 
           ></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnStuApproved" runat="server" 
          Text="Project Approval Letter" CssClass="leftlink" onclick="lbtnStuApproved_Click"
           ></asp:LinkButton></li>
            <li><asp:LinkButton ID="lbtnStuAppRemarks" runat="server" 
          Text="Project Approval with Remarks Letter" CssClass="leftlink" onclick="lbtnStuAppRemarks_Click"
           ></asp:LinkButton></li>
           </ul>
                
           </li>
          
           
  </ul>
  </li>
  <li><asp:LinkButton ID="lbtnProjectApproveRpt" runat="server" Text="Project Approved From Account" CssClass="leftlink" OnClick="lbtnProjectApprovedAc_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnProjectDe" runat="server" Text="Project Export" CssClass="leftlink" OnClick="lbtnProjectDe_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnProject" runat="server" Text="Project Report" CssClass="leftlink" OnClick="lbtnProject_OnClick"></asp:LinkButton></li>
 <li><asp:LinkButton ID="lbtnProjectStatus" runat="server" Text="Project Status Report" CssClass="leftlink" OnClick="lbtnProjectStatus_OnClick"></asp:LinkButton></li>
 <li><asp:LinkButton ID="lbtnInstituteRe" runat="server" Text="Institution Registraion" CssClass="leftlink" OnClick="lbtnInstituteRe_OnClick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnSynopsisApprovalReport" runat="server" Text="Synopsis Approval Report" CssClass="leftlink" OnClick="lbtnSynopsisApproveRpt_OnClick"></asp:LinkButton></li>   
 </ul>
    </div>
    </div>
   </div>
  </div>
     <!-- End Panel---->
  </div>
</div>  <!--- end Leftpanel2----->
   <br />
   <div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="lbtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:Label ID="lbtnNext1Redirect" runat="server" Text="Reports Home" CssClass="redirecttabhome"></asp:Label> </td>
        </tr></table>
   </div>
             <div id="rightpanel2" ><div id="header">
             <div class="fromRegisterlbl"><h1>Reports</h1></div>
             <!--Content------>
             <img src="../images/reportdefault.png" alt="Reports" width="100%" /><hr />
             <div style="height:300px;"></div>
             </div>
             </div><br />
    </div><br />
    </div>
    <!-- footer -->
    <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Help & Support</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    </form>
</body>
</html>

