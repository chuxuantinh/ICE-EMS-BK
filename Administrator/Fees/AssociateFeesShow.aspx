<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Fees/FeeMaster.master" AutoEventWireup="true" CodeFile="AssociateFeesShow.aspx.cs" Inherits="Administrator_Fees_AssociateFeesShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Associate Fees Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
        <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="exeption1" runat="server" style="border:1px solid #F5821D; width:60%; margin-left:20%; margin-bottom:10px; background-color:#F5CAA4; height:auto; text-align:center; padding:3px;"><asp:Label ID="lblExeptionInfo1" runat="server" ></asp:Label></div>
  <script>
        function toggle(showHideDiv, switchImgTag) {
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
   <div class="togalfees">
    <div class="headerDivImgfees">
    
    <a id="imageDivLink1" href="javascript:toggle('contentDivImg1', 'imageDivLink1');"><img src="../../images/minus.png" alt="Show"></a>
</div><h1>Associate Membership Examination</h1>
<div id="contentDivImg1" style="display: block;">
    
  
  <div class="feestable"><center style="color:Gray;">The Composite fee as stated above includes teh cost of Study Material & is payable by all the candidates ( Existing & Prospective both )</center><br />
  Membership Fee:<asp:Label ID="lblMemFeesA" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Payable at the time of submission of Enrollment Form.)<br />
  Composite Fee:<asp:Label ID="lblComFeeA" runat="server" CssClass="feelabel" ToolTip="The Composite fee as stated above includes teh cost of Study Material & is payable by all the candidates ( Existing & Prospective both )"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Payable at the time of entry in Section B.)
  <br />
  </div>
  <div class="fromRegisterlbl"><h1>Fee For Candidates seeking Membership directly to Section B.</h1></div>
  <div class="feestable">
  <br />
  Membership Fee:<asp:Label ID="lblMemFeesB" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Payable at the time of submission of Enrollment Form.)<br />
  Composite Fee:<asp:Label ID="lblComFeeB" runat="server" CssClass="feelabel" ToolTip="The Composite fee as stated above includes teh cost of Study Material & is payable by all the candidates ( Existing & Prospective both )"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Payable After: &nbsp;<b><asp:Label ID="lblCompositeDuration" runat="server" ></asp:Label>&nbsp;Months)</b><br />
  </div>
    
    </div>
   </div>
   
    <script>
        function toggleA1(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display != "block") {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="../../images/minus.png">';
            }
            else {
                
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="../../images/plus.png">';
            }
        }
    </script>
   <div class="togalfees">
    <div class="headerDivImgfees">
    
    <a id="A1" href="javascript:toggleA1('Div1', 'A1');"><img src="../../images/plus.png" alt="Show"></a>
</div><h1>Examination Fee Schedule</h1>
<div id="Div1" style="display: none;">
    
 <br /><center style="color:Gray;">Last Date for Receipt of forms in ICE(I) Office.</center><br />
 <div class="feestable">
 <table><thead> <tr><td></td><td>Summer Examination (Date)</td><td>Winter Examination (Date)</td><td>Late Fee:&nbsp;&nbsp;<img src="../../images/rs.jpg" alt="Rs." /></td></tr></thead>
 <tbody><tr><td>Without late fee:</td><td><asp:Label ID="lblDateSumWOFee" runat="server" >d</asp:Label></td><td><asp:Label ID="lblDateWinWOFee" runat="server" >d</asp:Label></td><td></td></tr>
 <tr><td rowspan="3">Without Late Fee:</td><td><asp:Label ID="lblDateSumWLFee1" runat="server">d</asp:Label></td><td><asp:Label ID="lblDateWinWLFee1" runat="server" >d</asp:Label></td><td><asp:Label ID="lblLatefee1" runat="server" >d</asp:Label></td></tr>
 <tr><td><asp:Label ID="lblDateSumWLFee2" runat="server" >d</asp:Label></td><td><asp:Label ID="lblDateWinWLFee2" runat="server" >d</asp:Label></td><td><asp:Label ID="lblLatefee2" runat="server" >d</asp:Label></td></tr>
 <tr><td><asp:Label ID="lblDateSumWLFee3" runat="server" >d</asp:Label></td><td><asp:Label ID="lblDateWinWLFee3" runat="server" >d</asp:Label></td><td><asp:Label ID="lblLatefee3" runat="server" >d</asp:Label></td></tr>
 
 
 </tbody>
 <tfoot><tr><td>Examination Schedule:</td><td><asp:Label ID="lblSumSchedule" runat="server" ></asp:Label></td><td><asp:Label ID="lblWinSchedule" runat="server" ></asp:Label></td><td></td></tr></tfoot>
 </table>
 <br />
 </div> 
    
    </div>
   </div>
   <script>
       function toggleA2(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display != "block") {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../../images/minus.png">';
           }
           else {

               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../../images/plus.png">';
           }
       }
    </script>
   <div class="togalfees">
    <div class="headerDivImgfees">
    
    <a id="A2" href="javascript:toggleA2('Div2', 'A2');"><img src="../../images/plus.png" alt="Show"></a>
</div><h1>Exemption Fee, Annual Subscription Fee, Change of Exam Center</h1>
<div id="Div2" style="display: none;">
    
 <br /><center style="color:Gray;">Last Date for Receipt of forms in ICE(I) Office.</center><br />
 <div class="feestable">
 <br />
 Exemption Fee:<asp:Label ID="lblExemptionFee" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Per Subject).<br />
 <br />
 Annual Subscription Fee:<asp:Label ID="lblAnnualSubscriptnFee" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Payable alognwith Examination fee after completion of one year Membership.)<br />
 
 
 </div> 
 <div class="fromRegisterlbl"><h1>Change of Exam Center & Re-Chacking Fee</h1></div>
  <div class="feestable">
 
    Change of Exam Center:<asp:Label ID="lblChangeExamCenter" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Upto 10 days before the start of exam.)<br />
    Re-checking Fee:<asp:Label ID="lblReCheckingFee" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Per Subject with in one month from the date of declaration of Result. )
   <br /> </div>
    </div>
   </div>
  
     <script>
         function toggleA3(showHideDiv, switchImgTag) {
             var ele = document.getElementById(showHideDiv);
             var imageEle = document.getElementById(switchImgTag);
             if (ele.style.display != "block") {
                 ele.style.display = "block";
                 imageEle.innerHTML = '<img src="../../images/minus.png">';
             }
             else {

                 ele.style.display = "none";
                 imageEle.innerHTML = '<img src="../../images/plus.png">';
             }
         }
    </script>
   <div class="togalfees">
    <div class="headerDivImgfees">
    
    <a id="A3" href="javascript:toggleA3('Div3', 'A3');"><img src="../../images/plus.png" alt="Show"></a>
</div><h1>Project Fees & Certificate Fee </h1>
<div id="Div3" style="display: none;">
 <div class="feestable">
 <br />
 Project Approval Fee:<asp:Label ID="lblProApproval" runat="server" CssClass="feelabel">ss</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<br />
 
 Project Evaluation Fee:<asp:Label ID="lblProEvaluation" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<br />
 
 
 </div> 
 <div class="fromRegisterlbl"><h1>Provisional Certificate & Certificate Fee.</h1></div>
  <div class="feestable">
  
    Provisional Certificate Fee:<asp:Label ID="lblProCertificate" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<br />
    Certification Fee:<asp:Label ID="lblCertificationFee" runat="server" CssClass="feelabel"></asp:Label>
   <br /></div>
   <div class="fromRegisterlbl"><h1>Duplicate Documents Fee</h1></div>
   <div class="feestable">
   Identity Card:<asp:Label ID="lblIDCard" runat="server" CssClass="feelabel"></asp:Label><br />
   Admit Card:<asp:Label ID="lblAdminCard" runat="server" CssClass="feelabel"></asp:Label><br />
   Marks Statement:<asp:Label ID="lblMStatement" runat="server" CssClass="feelabel"></asp:Label><br />
   Membership Certificate:<asp:Label ID="lblMCeritficate" runat="server" CssClass="feelabel"></asp:Label><br />
   </div>
    </div>
   </div>
     <script>
         function toggleA4(showHideDiv, switchImgTag) {
             var ele = document.getElementById(showHideDiv);
             var imageEle = document.getElementById(switchImgTag);
             if (ele.style.display != "block") {
                 ele.style.display = "block";
                 imageEle.innerHTML = '<img src="../../images/minus.png">';
             }
             else {

                 ele.style.display = "none";
                 imageEle.innerHTML = '<img src="../../images/plus.png">';
             }
         }
    </script>
   <div class="togalfees">
    <div class="headerDivImgfees">
    
    <a id="A4" href="javascript:toggleA4('Div4', 'A4');"><img src="../../images/plus.png" alt="Show"></a>
</div><h1>Examination Fee, ITI Fees & Set of Old Question Papers</h1>
<div id="Div4" style="display: none;">
 <div class="feestable">
 <br /><center>Including&nbsp;<img src="../../images/rs.jpg" alt="Rs." />&nbsp;<asp:Label ID="lblPostalCharge" runat="server" ></asp:Label> &nbsp;Postal Charges.</center>
 Section A:<asp:Label ID="lblSecA" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Per Attempt)<br />
 
 Section B:<asp:Label ID="lblSecB" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Per Attempt)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ITI Fees(Rs.):&nbsp;<asp:Label ID="lblITIFees" runat="server" Font-Bold="true"></asp:Label><br />
 
 
 </div> 
 <div class="fromRegisterlbl"><h1>Sets of Old Question Papers.</h1></div>
  <div class="feestable">
  
    Section A:<asp:Label ID="lblOldSecA" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Per Set)<br />
 
 Section B:<asp:Label ID="lblOldSecB" runat="server" CssClass="feelabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(Per Set)<br />
   <br /></div>
   
    </div>
   </div>
</asp:Content>

