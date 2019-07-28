<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Fees/FeeMaster.master" AutoEventWireup="true" CodeFile="TechnicianFeeEdit.aspx.cs" Inherits="Administrator_Fees_TechnicianFeeEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Technician Fees Master</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
<link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
<asp:Label ID="lblexepinfo" runat="server" ></asp:Label>
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
</div><h1>Technician Membership Examination</h1>
<div id="contentDivImg1" style="display: block;">
<asp:UpdatePanel ID="updatepanelMembership" runat="server" ><ContentTemplate>
<div class="feestable"><center style="color:Gray;">The Composite fee as stated above includes teh cost of Study Material & is payable by all the candidates ( Existing & Prospective both )</center><br />
Membership Fee:<asp:TextBox ID="lblMemFeesA" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="lblMemFeesA" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Membership Fee For Section A" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="val_cmp_Fee" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblMemFeesA" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="lblMemFeesA" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Payable at the time of submission of Enrollment Form.)<br /><br />
Composite Fee:&nbsp;&nbsp;<asp:TextBox ID="lblComFeeA" runat="server" CssClass="feelabel" ToolTip="The Composite fee as stated above includes teh cost of Study Material & is payable by all the candidates ( Existing & Prospective both )"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="lblComFeeA" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Composite Fee  Section A" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblComFeeA" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="lblComFeeA" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Payable at the time of entry in Section B.)
<br />
</div>
<div class="fromRegisterlbl"><h1>Fee For Candidates seeking Membership directly to PartII.</h1></div>
<div class="feestable">
<br /> Membership Fee:<asp:TextBox ID="lblMemFeesB" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="regxmemfeeB" controltovalidate="lblMemFeesB" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Membership Fee For Direct Admission in Section B" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblMemFeesB" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="lblMemFeesB" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Payable at the time of submission of Enrollment Form.)<br /><br />
Composite Fee:&nbsp;&nbsp;<asp:TextBox ID="lblComFeeB" runat="server" CssClass="feelabel" ToolTip="The Composite fee as stated above includes teh cost of Study Material & is payable by all the candidates ( Existing & Prospective both )"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="lblComFeeB" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Composite Fee For Direct Admission in Section B" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblComFeeB" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="lblComFeeB" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Payabel After:&nbsp;<asp:DropDownList ID="ddlCompositeDuration" runat="server" ><asp:ListItem Value="6" Text="6 Months" /><asp:ListItem Value="12" Text="12 Months" /><asp:ListItem Value="18" Text="18 Months"></asp:ListItem></asp:DropDownList><br />
<asp:ValidationSummary ID="VSummary1" CssClass="expbox" runat="server" DisplayMode="BulletList" ValidationGroup="A1" ForeColor="Red" />
<br />
<center><asp:Label ID="lblsavemembership" runat="server" ForeColor="Green"></asp:Label></center><br />
<center><asp:Button ID="btnSaveMembership" ValidationGroup="A1" runat="server" Text="Save" onclick="btnSaveMembership_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear1" runat="server" Text="Clear" onclick="btnClear1_Click" /></center>
</div>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
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
<asp:UpdatePanel ID="updatepanelExamSchedule" runat="server"><ContentTemplate>
<div class="feestable">
<table><thead> <tr><td></td><td>Summer Examination (dd/MMM)</td><td>Winter Examination (dd/MMM)</td><td>Late Fee:&nbsp;&nbsp;<img src="../../images/rs.jpg" alt="Rs." /></td></tr></thead>
<tbody><tr><td>Without late fee:</td><td><asp:TextBox ID="txtSumWOL" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtSumWOL"></dev:CalendarExtender> <img src="../../images/cal.png" id="cal" runat="server"  alt="Cal" /> </td><td><asp:TextBox ID="txtWinWOL" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="Img1" PopupPosition="BottomRight" runat="server" TargetControlID="txtWinWOL"></dev:CalendarExtender><img src="../../images/cal.png" id="Img1" runat="server"  alt="Cal" /></td><td></td></tr>
<tr><td rowspan="3">With Late Fee:</td><td><asp:TextBox ID="txtSumWL1" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" PopupButtonID="Img2" PopupPosition="BottomRight" runat="server" TargetControlID="txtSumWL1"></dev:CalendarExtender><img src="../../images/cal.png" id="Img2" runat="server"  alt="Cal" /></td><td><asp:TextBox ID="txtWinWL1" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender3" PopupButtonID="Img3" PopupPosition="BottomRight" runat="server" TargetControlID="txtWinWL1"></dev:CalendarExtender><img src="../../images/cal.png" id="Img3" runat="server"  alt="Cal" /></td><td><asp:TextBox ID="lblLatefee1" runat="server" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="lblLatefee1" Display="Dynamic" ValidationGroup="A2" errormessage="Please Insert Examination Schedule Late Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblLatefee1" Operator="LessThanEqual" Type="Double" ValidationGroup="A2">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="lblLatefee1" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td><asp:TextBox ID="txtSumWL2" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender4" PopupButtonID="Img4" PopupPosition="BottomRight" runat="server" TargetControlID="txtSumWL2"></dev:CalendarExtender><img src="../../images/cal.png" id="Img4" runat="server"  alt="Cal" /></td><td><asp:TextBox ID="txtWinWL2" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender5" PopupButtonID="Img5" PopupPosition="BottomRight" runat="server" TargetControlID="txtWinWL2"></dev:CalendarExtender><img src="../../images/cal.png" id="Img5" runat="server"  alt="Cal" /></td><td><asp:TextBox ID="lblLatefee2" runat="server" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="lblLatefee2" Display="Dynamic" ValidationGroup="A2" errormessage="Please Insert Examination Schedule Late Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblLatefee2" Operator="LessThanEqual" Type="Double" ValidationGroup="A2">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="lblLatefee2" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td><asp:TextBox ID="txtSumWL3" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender6" PopupButtonID="Img6" PopupPosition="BottomRight" runat="server" TargetControlID="txtSumWL3"></dev:CalendarExtender><img src="../../images/cal.png" id="Img6" runat="server"  alt="Cal" /></td><td><asp:TextBox ID="txtWinWL3" runat="server" CssClass="txtbox"></asp:TextBox><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender7" PopupButtonID="Img7" PopupPosition="BottomRight" runat="server" TargetControlID="txtWinWL3"></dev:CalendarExtender><img src="../../images/cal.png" id="Img7" runat="server"  alt="Cal" /></td><td><asp:TextBox ID="lblLatefee3" runat="server" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="lblLatefee3" Display="Dynamic" ValidationGroup="A2" errormessage="Please Insert Examination Schedule Late Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblLatefee3" Operator="LessThanEqual" Type="Double" ValidationGroup="A2">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="lblLatefee3" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
</tbody>
<tfoot><tr><td>Examination Schedule:</td><td><asp:TextBox ID="lblSumSchedule" runat="server" ></asp:TextBox></td><td><asp:TextBox ID="lblWinSchedule" runat="server" ></asp:TextBox></td><td></td></tr></tfoot>
</table><asp:ValidationSummary ID="validatoinSummary2" runat="server" DisplayMode="BulletList" ValidationGroup="A2" CssClass="expbox" /><br />
<center><asp:Label ID="lblExamScheduleInfo" runat="server" ForeColor="Green"></asp:Label></center>
<br />
<center><asp:Button ID="btnSaveExamSchedule" ValidationGroup="A2" runat="server" Text="Save" onclick="btnSaveExamSchedule_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear2" runat="server" Text="Clear" onclick="btnClear2_Click" /></center>
</div> 
</ContentTemplate></asp:UpdatePanel>
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
<asp:UpdatePanel ID="updatepanleExemptionFee" runat="server" ><ContentTemplate>
<br /><center style="color:Gray;">Last Date for Receipt of forms in ICE(I) Office.</center><br />
<div class="feestable">
Exemption Fee:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="lblExemptionFee" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" controltovalidate="lblExemptionFee" Display="Dynamic" ValidationGroup="A3" errormessage="Please Insert Exemption Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblExemptionFee" Operator="LessThanEqual" Type="Double" ValidationGroup="A3">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="lblExemptionFee" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Per Subject).<br />
<br />
Annual Subscription Fee:<asp:TextBox ID="lblAnnualSubscriptnFee" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="lblAnnualSubscriptnFee" Display="Dynamic" ValidationGroup="A3" errormessage="Please Insert Annual Subscription Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblAnnualSubscriptnFee" Operator="LessThanEqual" Type="Double" ValidationGroup="A3">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="lblAnnualSubscriptnFee" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Payable alognwith Examination fee after completion of one year Membership.)<br />
</div> 
<div class="fromRegisterlbl"><h1>Change of Exam Center & Re-Chacking Fee</h1></div>
 <div class="feestable">
Change of Exam Center:<asp:TextBox ID="lblChangeExamCenter" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="lblChangeExamCenter" Display="Dynamic" ValidationGroup="A3" errormessage="Please Insert Change of ExamCenter Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblChangeExamCenter" Operator="LessThanEqual" Type="Double" ValidationGroup="A3">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="lblChangeExamCenter" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Upto 10 days before the start of exam.)<br /><br />
Re-checking Fee:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="lblReCheckingFee" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="lblReCheckingFee" Display="Dynamic" ValidationGroup="A3" errormessage="Please Insert Re-Checking Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblReCheckingFee" Operator="LessThanEqual" Type="Double" ValidationGroup="A3">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="lblReCheckingFee" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Per Subject with in one month from the date of declaration of Result. )
<br /> </div><asp:ValidationSummary ID="Validatoinsummary3" runat="server" DisplayMode="BulletList" ForeColor="Red" ValidationGroup="A3" CssClass="expbox" /><br />
<center><asp:Label ID="lblExemptionfeeInfo" runat="server" ForeColor="Green"></asp:Label><br /> <asp:Button ID="btnSaveExemption" runat="server" Text="Save" ValidationGroup="A3" onclick="btnSaveExemption_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear3" runat="server" Text="Clear" onclick="btnClear3_Click" /></center>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
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
<asp:UpdatePanel ID="updatepanelProject" runat="server" ><ContentTemplate>
<div class="feestable">
<br />
Project Approval Fee:<asp:TextBox ID="lblProApproval" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="lblProApproval" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Project Approval Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblProApproval" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="lblProApproval" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Project Evaluation Fee:<asp:TextBox ID="lblProEvaluation" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator12" controltovalidate="lblProEvaluation" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Project Evaluation Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblProEvaluation" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="lblProEvaluation" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;<br />
</div> 
<div class="fromRegisterlbl"><h1>Provisional Certificate & Certificate Fee.</h1></div>
<div class="feestable">
Provisional Certificate Fee:<asp:TextBox ID="lblProCertificate" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" controltovalidate="lblProCertificate" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Provisional Certificate Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblProCertificate" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="lblProCertificate" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Certification Fee:<asp:TextBox ID="lblCertificationFee" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" controltovalidate="lblCertificationFee" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Certification Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="Fee can not be greater than 99999.99" ValueToCompare="99999.99" ControlToValidate="lblCertificationFee" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="lblCertificationFee" FilterType="Numbers"></dev:FilteredTextBoxExtender>
<br /></div>
<div class="fromRegisterlbl"><h1>Duplicate Documents Fee</h1></div>
<div>
<table width="95%">
<tr>
<td align="right">Identity Card:</td>
<td><asp:TextBox ID="lblIDCard" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" controltovalidate="lblIDCard" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Identity Card Fee" >*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblIDCard" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="lblIDCard" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
<td align="right">Admit Card:</td>
<td><asp:TextBox ID="lblAdminCatd" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" controltovalidate="lblAdminCatd" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Admit Card Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblAdminCatd" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="lblAdminCatd" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
</tr>
<tr>
<td align="right">Marks Statement:</td>
<td><asp:TextBox ID="lblMarksStatment" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator17" controltovalidate="lblMarksStatment" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Marks Statement Fee" >*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblMarksStatment" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="lblMarksStatment" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
<td align="right">Membership Certificate:</td>
<td><asp:TextBox ID="lblMemCertificate" runat="server" CssClass="feelabel"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator18" controltovalidate="lblMemCertificate" Display="Dynamic" ValidationGroup="A4" errormessage="Please Insert Membership Certificate Fee" >*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator18" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblMemCertificate" Operator="LessThanEqual" Type="Double" ValidationGroup="A4">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="lblMemCertificate" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
</tr>
</table>
<br />
</div><asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="BulletList" CssClass="expbox" ForeColor="Red" ValidationGroup="A4" /><br /><br />
<center><asp:Label ID="lblProjectInfo" runat="server" ForeColor="Green"></asp:Label></center><br />
<center><asp:Button ID="btnProjects" ValidationGroup="A4" runat="server" Text="Save" onclick="btnProjects_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear4" runat="server" Text="Clear" onclick="btnClear4_Click" />
</center>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
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
<asp:UpdatePanel ID="upatepanelExamFee" runat="server" ><ContentTemplate>
 <div class="feestable">
 <br /><center>Including&nbsp;<img src="../../images/rs.jpg" alt="Rs." />&nbsp;<asp:TextBox ID="lblPostalCharge" runat="server" ></asp:TextBox> 
 <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator19" controltovalidate="lblPostalCharge" Display="Dynamic" ValidationGroup="A5" errormessage="Please Insert Postal Charge Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator19" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblPostalCharge" Operator="LessThanEqual" Type="Double" ValidationGroup="A5">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="lblPostalCharge" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;Postal Charges.</center>
 Part I:<asp:TextBox ID="lblSecA" runat="server" CssClass="feelabel"></asp:TextBox>
 <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator20" controltovalidate="lblSecA" Display="Dynamic" ValidationGroup="A5" errormessage="Please Insert Section A Examination Fee Per Attempt" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator20" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblSecA" Operator="LessThanEqual" Type="Double" ValidationGroup="A5">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="lblSecA" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Per Attempt)<br /><br />
 Part II:<asp:TextBox ID="lblSecB" runat="server" CssClass="feelabel"></asp:TextBox>
 <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator21" controltovalidate="lblSecB" Display="Dynamic" ValidationGroup="A5" errormessage="Please Insert Section B Examination Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator21" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblSecB" Operator="LessThanEqual" Type="Double" ValidationGroup="A5">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" TargetControlID="lblSecB" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Per Attempt)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ITI Fees(Rs):&nbsp;<asp:TextBox ID="txtITIFees" Width="100px" runat="server" Font-Bold="true" CssClass="txtbox"></asp:TextBox><br />
 </div> 
 <div class="fromRegisterlbl"><h1>Sets of Old Question Papers.</h1></div>
  <div class="feestable">
    Part I:<asp:TextBox ID="lblOldSecA" runat="server" CssClass="feelabel"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="lblOldSecA" Display="Dynamic" ValidationGroup="A5" errormessage="Please Insert Section A Old Papers Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator22" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblOldSecA" Operator="LessThanEqual" Type="Double" ValidationGroup="A5">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="lblOldSecA" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Per Set)<br /><br />
 Part II:<asp:TextBox ID="lblOldSecB" runat="server" CssClass="feelabel"></asp:TextBox>
 <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" controltovalidate="lblOldSecB" Display="Dynamic" ValidationGroup="A5" errormessage="Please Insert Section B Old Papers Fee" >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator23" runat="server" ErrorMessage="Fee can not be greater than 9999.99" ValueToCompare="9999.99" ControlToValidate="lblOldSecB" Operator="LessThanEqual" Type="Double" ValidationGroup="A5">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="lblOldSecB" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;&nbsp;(Per Set)<br />
   <br /></div>
   <asp:ValidationSummary ID="validationSummary5" runat="server" DisplayMode="BulletList" CssClass="expbox" ValidationGroup="A5" ForeColor="Red" />
   <br />
   <center><asp:Label ID="lblSecInfo" runat="server" ForeColor="Green"></asp:Label></center>
   <br />
   <center> <asp:Button ID="btnSaveExamFee" runat="server" Text="Save" ValidationGroup="A5"
          onclick="btnSaveExamFee_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
          ID="btnClear5" runat="server" Text="Clear" onclick="btnClear5_Click" /></center>
          </ContentTemplate></asp:UpdatePanel>
    </div>
    <br />
   </div>
</asp:Content>

