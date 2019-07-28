<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ViewStatus.aspx.cs" Inherits="Admin_ViewStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">View Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblCity" runat="server" Text="View System Process" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;"></h1><h1><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label>
   System Process &nbsp;&nbsp;&nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" Width="50px" CssClass="txtbox"></asp:TextBox></h1></div><br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<br />
             <script>
                 function toggleAdd(showHideDiv, switchImgTag) {
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
 <a id="A1Add" href="javascript:toggleAdd('Div1Add', 'A1Add');"><img src="../images/plus.png" alt="Show"></a>
</div><div class="divshow">
        <asp:Label ID="lblAdd" 
        runat="server" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>
            Admission Forms</h2></div>
<div id="Div1Add" style="display:none;">
  <input id="Hidden2" runat="server" type="hidden" value="0" />
                 <div id="div3" style="width: 100%; overflow:visible; height:auto" >
               <table style="width:95%;  text-align:center;">
<tr><td>Diary Submitted</td><td>Submitted</td><td>Approved</td><td>Filled</td><td>Active</td><td>DisActive(All)</td></tr>
<tr>
<td><h3><asp:Label ID="lblAddDiarySub" runat="server" ></asp:Label></h3></td>
<td><h3><asp:Label ID="lblAddSub" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblAddApproved" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblAddFilled" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblAddActive" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblAddDisactive" runat="server"></asp:Label></h3></td></tr>
</table>
   </div>
   </div></div>
            
            <script>
                function toggleExam(showHideDiv, switchImgTag) {
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
 <a id="A1Exam" href="javascript:toggleExam('Div1Exam', 'A1Exam');"><img src="../images/plus.png" alt="Show"></a>
</div><div class="divshow"> <asp:Label ID="lblExam" 
        runat="server" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>
            Examination Forms</h2></div>
<div id="Div1Exam" style="display:none;">
  <input id="Hidden1" runat="server" type="hidden" value="0" />
                 <div id="div2" style="width: 100%; overflow:visible; height:auto" >
               <table style="width:95%;  text-align:center;">
<tr><td>Diary Submitted</td><td>Submitted</td><td>Approved</td><td>Filled</td><td>Hold</td><td>RollNo Generated</td><td>Admit Card</td></tr>
<tr>
<td><h3><asp:Label ID="lblToExamDiary" runat="server" ></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormSub" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormApproved" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormFilled" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamHold" runat="server" ></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormRollNo" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblExamFormAdmitCard" runat="server"></asp:Label></h3></td></tr>
</table>
                 
   </div>
   </div></div>
    <script>
        function toggleASF(showHideDiv, switchImgTag) {
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
 <a id="A1ASF" href="javascript:toggleASF('Div1ASF', 'A1ASF');"><img src="../images/plus.png" alt="Show"></a>
</div><div class="divshow">
        <asp:Label ID="lblASF" 
        runat="server" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>
            AnnualSubscription Fee</h2></div>
<div id="Div1ASF" style="display:none;">
  <input id="Hidden4" runat="server" type="hidden" value="0" />
                 <div id="div5" style="width: 100%; overflow:visible; height:auto" >
               <table style="width:95%;  text-align:center;">
<tr><td>Submitted</td><td>Approved</td></tr>
<tr>

<td><h3><asp:Label ID="lblASFSub" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblASFApp" runat="server"></asp:Label></h3></td>
</tr>
</table>
                 
   </div>
   </div></div>
      <script>
          function toggleComp(showHideDiv, switchImgTag) {
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
 <a id="A1Comp" href="javascript:toggleComp('Div1Comp', 'A1Comp');"><img src="../images/plus.png" alt="Show"></a>
</div><div class="divshow">
        <asp:Label ID="lblComp" 
        runat="server" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>
            Composite Fee</h2></div>
<div id="Div1Comp" style="display:none;">
  <input id="Hidden5" runat="server" type="hidden" value="0" />
                 <div id="div6" style="width: 100%; overflow:visible; height:auto" >
               <table style="width:95%;  text-align:center;">
<tr><td>Submitted</td><td>Approved</td></tr>
<tr>
<td><h3><asp:Label ID="lblCompSub" runat="server"></asp:Label></h3></td>
<td><h3><asp:Label ID="lblCompApp" runat="server"></asp:Label></h3></td>
</tr>
</table>
                 
   </div>
   </div></div>
      <script>
          function togglePro1(showHideDiv, switchImgTag) {
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
 <a id="A1Pro1" href="javascript:togglePro1('Div1Pro1', 'A1Pro1');"><img src="../images/plus.png" alt="Show"></a>
</div><div class="divshow">
        <asp:Label ID="lblProTotalStudent" 
        runat="server" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>
            Synopsis, Proforma A, Proforma B</h2></div>
<div id="Div1Pro1" style="display:none;">
  <input id="Hidden6" runat="server" type="hidden" value="0" />
                 <div id="div7" style="width: 100%; overflow:visible; height:auto" >
               <table class="tbl" width="100%"><tr align="center"><td>Total Student</td><td>Proforma A</td><td>Proforma B</td></tr>
<tr align="center"><td><asp:Label ID="lblProToStudent" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label></td><td>Submitted:&nbsp;<asp:Label ID="lblProformaASub" runat="server" Font-Bold="true" ForeColor="Maroon" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Approved:&nbsp;<asp:Label ID="lblProformaAApp" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td>
<td>Submitted:&nbsp;<asp:Label ID="lblProformaBSub" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Approved:&nbsp;<asp:Label ID="lblProformaBApp" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td>
</tr>
</table>
   </div>
   </div></div>

         <script>
             function togglePro2(showHideDiv, switchImgTag) {
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
 <a id="A1Pro2" href="javascript:togglePro2('Div1Pro2', 'A1Pro2');"><img src="../images/plus.png" alt="Show"></a>
</div><div class="divshow">
        <h2>
          Copy Submission and Proforma C</h2></div>
<div id="Div1Pro2" style="display:none;">
  <input id="Hidden7" runat="server" type="hidden" value="0" />
                 <div id="div8" style="width: 100%; overflow:visible; height:auto" >
                <table class="tbl" width="100%">
               <tr align="center"><td>Copy Submission</td><td>Proforma C</td><td>ReSubmit/DisApproved</td></tr>
<tr align="center"><td>Pending:&nbsp;<asp:Label ID="lblCopyPending" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Submitted:&nbsp;<asp:Label ID="lblCopySubmitted" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label></td><td><asp:Label ID="lblPorformaC" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td><td><asp:Label ID="lblProResubmit" runat="server"  Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
</table>
                 
   </div>
   </div></div>
   <script>
       function toggleA1x(showHideDiv, switchImgTag) {
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
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div class="divshow"><asp:Label ID="lblD2D" 
        runat="server" Text="" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>Dairy Status
    </h2></div>
<div id="Div1x" style="display:none;">
  <input id="Hidden3" runat="server" type="hidden" value="0" />
                 <div id="div1" style="width: 100%; overflow:visible; height:auto" >
               <table style="width:95%;  text-align:center;">
<tr><td>Diary Entry</td><td colspan="2">Diary Count</td><td colspan="2">Diary Supply</td></tr>
<tr><td><h3><asp:Label ID="lblDiaryEntry" runat="server" ></asp:Label></h3></td><td>Received<br /><h3><asp:Label ID="lblCountReceived" runat="server"></asp:Label></h3></td>
    <td>
        Dispatched<br /> <h3>
        <asp:Label ID="lblCountDispatch" runat="server"></asp:Label>
    </h3></td>
    <td>Received<br /><h3><asp:Label ID="lblAccReceive" runat="server"></asp:Label></h3></td>
    <td>
        Supplied<br /><h3><asp:Label ID="lblOpen" runat="server"></asp:Label></h3></td>
                   </tr>
</table>
   </div>
   </div></div>
<div class="divshow">
        <asp:Label ID="lblAddApp" 
        runat="server" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>
            Add Application</h2></div><br /><div class="divshow">
        <asp:Label ID="lblApp" 
        runat="server" style="float:right; margin-right:5px;padding-top:5px;"></asp:Label><h2>
            Approve Application</h2></div>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>