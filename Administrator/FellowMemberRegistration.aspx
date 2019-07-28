<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" CodeFile="FellowMemberRegistration.aspx.cs" Inherits="Administrator_FellowMemberRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">ICE Member Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
     <script src="../jquery.tools.min.js" type="text/javascript"></script>
     	<link rel="stylesheet" type="text/css" href="../tooltip-generic.css"/> 	
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
   <div style="float:right;"> <b>Insert Membership ID:&nbsp;&nbsp;</b><asp:TextBox ID="txtNewIdView" runat="server" >
     </asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnViewNewId" runat="server" Text="View" ValidationGroup="strip" onclick="btnViewNewId_Click" CssClass="btnsmall" /></div>
<div id="leftpanel2">
<div id="leftteg" >
    <script>
        function toggle100(showHideDiv, switchImgTag) {
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
  
   
  <div class="togelleft">
    <div class="headerDivImg">
    
    <a id="imageDivLink100" href="javascript:toggle('contentDivImg100', 'imageDivLink100');"><%--<li><asp:LinkButton ID="lbtnetc" runat="server" Text="Personal stuff" 
              CssClass="leftlink" onclick="lbtnetc_Click" ></asp:LinkButton></li>--%></a>
</div><center><h1>ID:&nbsp;&nbsp;<asp:Label ID="lblMemberType" runat="server" ></asp:Label></h1></center>
<div id="contentDivImg100" style="display: block;"> 
<div  style="padding-top:55px;" id="photostrip" ></div>
<center>
 <asp:DataList ID="DataList2" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "Handler.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
            </asp:DataList>
</center>
        </div>
   <br />
   
    </div>
   </div>
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
                  }
              }
    </script>
   
   <div class="togelleft">
    <div class="headerDivImg">
    
    <a id="imageDivLink99" href="javascript:toggle99('contentDivImg99', 'imageDivLink99');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Manage Profile</h1>
<div id="contentDivImg99" style="display: block;"> 
    
   <br />
    <div id="leftLink">
   <ul><li><asp:LinkButton ID="lbtnCreateAdmin" runat="server" Text="Personal Profile" 
           CssClass="leftlink" onclick="lbtnCreateAdmin_Click" ></asp:LinkButton></li>
      <li><asp:LinkButton ID="lbtnChagePassword" runat="server" 
              Text="Education Qualification" CssClass="leftlink" 
              onclick="lbtnChagePassword_Click" ></asp:LinkButton></li>
      <li><asp:LinkButton ID="lbtnDeactivate" runat="server" Text="Education Experience" 
              CssClass="leftlink" onclick="lbtnDeactivate_Click"></asp:LinkButton></li>
              <li><asp:LinkButton ID="lbtnEditPicture" runat="server"  Text="Profile Image" onclick="lbtnEditPicture_Click" 
                      CssClass="leftlink" 
                    ></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lbtnSuspendAcc" runat="server" Text="Active/DisActive Membership" 
              CssClass="leftlink" onclick="lbtnSuspendAcc_Click"></asp:LinkButton></li>
   
   </ul></div></div></div><script>
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
                                  }
                              }
    </script><div class="togelleft">
    <div class="headerDivImg">
    
    <a id="imageDivLink98" href="javascript:toggle98('contentDivImg98', 'imageDivLink98');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Membership Profile View</h1><div id="contentDivImg98" style="display: block;"> 
    
  <br />
    <div class="leftlist">
   <ul><li>
       <asp:LinkButton ID="lbtnViewPersonal" runat="server" Text="Personal Profile" 
           CssClass="leftlink" onclick="lbtnViewPersonal_Click" ></asp:LinkButton></li><li><asp:LinkButton ID="lbtnRenewAcc" runat="server" Text="Experience Profile" 
              CssClass="leftlink" onclick="lbtnRenewAcc_Click" ></asp:LinkButton></li><li><asp:LinkButton ID="lbtnViewAccount" runat="server" 
              Text="Membership Status" CssClass="leftlink" onclick="lbtnViewAccount_Click" 
               ></asp:LinkButton></li><%--<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtExpYear" FilterType="Numbers"></dev:FilteredTextBoxExtender>--%></ul></div></div><br />
    
</div>

     </div>
     <div id="rightpanel2" >
<div id="header"><asp:Panel ID="panelCreate" runat="server" CssClass="panel" >
<div class="fromRegister">
<!-- profile panel ---><h1><asp:Label ID="lbltitle" runat="server" ></asp:Label></h1>
<asp:Panel ID="panelEditPersonaldate" runat="server" >
<div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton2" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>


<center><asp:Label ID="lblException" runat="server" ></asp:Label></center><br />
<table><tr><td>First Name:</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtform"></asp:TextBox></td><td>Last Name:</td><td><asp:TextBox ID="txtLName" runat="server" ></asp:TextBox></td></tr>
         <tr><td>Father's/Husband's:</td><td><asp:TextBox ID="txtFather" runat="server" CssClass="txtform"></asp:TextBox></td>
         
         
         </tr>
         <tr> <td>Designation</td><td><asp:TextBox ID="txtDesignation" runat="server" CssClass="txtform"></asp:TextBox></td></tr>
         <tr><td>Address:</td><td colspan="3">  <asp:TextBox ID="txtAddress"  Width="70%" runat="server" CssClass="txtform"></asp:TextBox></td></tr>
         <tr><td></td><td colspan="3"><asp:TextBox ID="txtAddress2" runat="server" CssClass="txtform"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox></td></tr>
         <tr><td>State:</td><td>
             <asp:DropDownList ID="ddlState" runat="server" 
                 onselectedindexchanged="ddlState_SelectedIndexChanged">
             </asp:DropDownList>
             </td><td>Pin Code:</td><td><asp:TextBox ID="txtPincode" runat="server" ></asp:TextBox></td></tr>
         <tr><td>Tel No.:</td><td><asp:TextBox ID="txtTelNo" runat="server" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtTelNo" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Tel No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Tel No can not be greater than 14 No." ValueToCompare="99999999999999" ControlToValidate="txtTelNo" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtTelNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>

<td>Office:</td><td><asp:TextBox ID="txtOfficeNo" runat="server" ></asp:TextBox>
<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Office Tel. can not be greater than 14 No." ValueToCompare="99999999999999" ControlToValidate="txtOfficeNo" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtOfficeNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>

</tr>
         <tr>
         
         <td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>
<td>Residential:</td><td><asp:TextBox ID="txtResidentialNo" runat="server" ></asp:TextBox>
<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Residential Phone No. can not be greater than 14 No." ValueToCompare="99999999999999" ControlToValidate="txtResidentialNo" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtResidentialNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td>

</tr>

         <tr><td>Date of Birth:</td><td><asp:TextBox ID="txtDOB" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDOB" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date of Birth" >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDOB"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td><td>
             &nbsp;Age:</td><td><asp:TextBox ID="txtAge" runat="server" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtAge" Display="Dynamic" ValidationGroup="A1" errormessage="Please Insert Age." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Invalid Age." ValueToCompare="150" ControlToValidate="txtAge" Operator="LessThanEqual" Type="Double" ValidationGroup="A1">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtAge" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr></table>
         <asp:ValidationSummary ID="VSummary1" CssClass="expbox" runat="server" DisplayMode="BulletList" ValidationGroup="A1" ForeColor="Red" />
        <center><asp:Label ID="lblException1" runat="server" ></asp:Label><br /> <asp:Button ID="btnSavePersonal" runat="server" Text="Save" ValidationGroup="A1" 
        onclick="btnSavePersonal_Click" CssClass="btnsmall"/>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="btnClearPersonal" runat="server" Text="Clear" 
        onclick="btnClearPersonal_Click"  CssClass="btnsmall"/></center> <br />
    <div style="margin-top:300px;">.</div>   </asp:Panel>  <!--- end personal panel -->
         
   <asp:Panel ID="panelEditQualification" runat="server" >
   
   <div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton1" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click" ValidationGroup="strip"   ></asp:LinkButton></div><br /><asp:UpdatePanel ID="updateEducationEdit" runat="server" ><ContentTemplate>
         <table>
         <tr><td>Highest Qualification:</td><td><asp:TextBox ID="txtQualicicatin" runat="server" ></asp:TextBox></td></tr>
         
         
      
</table><b>Education Qualification (Engineering Only:)</b>
<table><tr><td><b> </b></td><td><b>B.E./B.Tech/B.Arch.</b></td><td><b>M.E./M.Tech./M.Arch.</b></td><td><b>&nbsp;&nbsp;Ph.D.</b></td><td><b>Other..</b></td></tr>
       <tr><td>Discipline:</td><td><asp:TextBox ID="txtBDiscipline" runat="server"  Width="130px"></asp:TextBox></td><td><asp:TextBox ID="txtMDisipline" runat="server"  Width="130px"></asp:TextBox> </td><td><asp:TextBox ID="txtPDiscipline" runat="server"  Width="130px"></asp:TextBox></td><td><asp:TextBox ID="txtODiscipline" runat="server"  Width="130px"></asp:TextBox></td></tr>
       <tr><td>College:</td><td><asp:TextBox ID="txtBCollege" runat="server"  Width="130px"></asp:TextBox></td><td><asp:TextBox ID="txtMCollege" runat="server"  Width="130px"></asp:TextBox> </td><td><asp:TextBox ID="txtPcollege" runat="server"  Width="130px" ></asp:TextBox></td><td><asp:TextBox ID="txtOCollege" runat="server"  Width="130px" ></asp:TextBox></td></tr>
   <tr><td>Percentage:</td><td><asp:TextBox ID="txtBPercentage" runat="server"  Width="130px" ></asp:TextBox></td><td><asp:TextBox ID="txtMPercentage" runat="server"  Width="130px"></asp:TextBox> </td><td><asp:TextBox ID="txtPPercentage" runat="server"  Width="130px" ></asp:TextBox></td><td><asp:TextBox ID="txtOPercentage" runat="server"  Width="130px"></asp:TextBox></td></tr>
   <tr><td>University:</td><td><asp:TextBox ID="txtBUviversity" runat="server"  Width="130px"></asp:TextBox></td><td><asp:TextBox ID="txtMUviversity" runat="server"  Width="130px"></asp:TextBox> </td><td><asp:TextBox ID="txtPUniversity" runat="server"  Width="130px"></asp:TextBox></td><td><asp:TextBox ID="txtOUniveristy" runat="server"  Width="130px" ></asp:TextBox></td></tr>
   <tr><td>YearofPassing:</td><td><asp:TextBox ID="txtBPassingyear" runat="server"  Width="130px"></asp:TextBox></td><td><asp:TextBox ID="txtMPassingyear" runat="server"  Width="130px" ></asp:TextBox> </td><td><asp:TextBox ID="txtPPassingyear" runat="server"  Width="130px"></asp:TextBox></td><td><asp:TextBox ID="txtOPassingyear" runat="server"  Width="130px"></asp:TextBox></td></tr>
</table>

<center><asp:Label ID="lblExepEditEducation" runat="server" ></asp:Label><br /> 
    <asp:Button ID="btnSaveEducation" runat="server" Text="Save" ValidationGroup="percent" onclick="ButtonSaveEducaitoin_Click" 
         CssClass="btnsmall"/>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="Button2" runat="server" Text="Clear" 
        onclick="btnClearPersonal_Click" ValidationGroup="percent" CssClass="btnsmall" /></center>
        </ContentTemplate></asp:UpdatePanel><div style="margin-top:350px;">.</div>
</asp:Panel><!-- end edit qualificaiton--->




<asp:UpdatePanel ID="updatePanelExpEdit" runat="server" ><ContentTemplate>
   
<script>
    $("#paneleditExperice :input").tooltip({

        // place tooltip on the right edge
        position: "center right",

        // a little tweaking of the position
        offset: [-2, 10],

        // use the built-in fadeIn/fadeOut effect
        effect: "fade",

        // custom opacity setting
        opacity: 0.7

    });
   
</script>
<asp:Panel ID="paneleditExperice" runat="server" >
<div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="lbtnviewProgfile" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>
<b>Detial of Experience:</b>
<br />
<table><tr><td>Total Experience:</td><td>&nbsp;<asp:TextBox ID="txtExpYear" 
        ToolTip="Example:-5.6 [Years.Months]"  AutoPostBack="true" runat="server" 
        ontextchanged="txtExpYears_TextChanged" ></asp:TextBox>&nbsp;<asp:LinkButton ID="ibtnShowEligibility" runat="server" Text="Eligibility" OnClick="ibtnShowEligibilgy_Click"></asp:LinkButton> <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="txtExpYear" Display="Dynamic" ValidationGroup="A2" errormessage="Please Insert Experience Year." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Invalid Experience Year" ValueToCompare="15" ControlToValidate="txtExpYear" Operator="LessThanEqual" Type="Double" ValidationGroup="A2">*</asp:CompareValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtExpYear"
                                          runat="server" ErrorMessage="Experience year required numeric values." ValidationGroup="A2" Display="Dynamic" ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator> <%--<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtExpYear" FilterType="Numbers"></dev:FilteredTextBoxExtender>--%>&nbsp;&nbsp;&nbsp;<br /><i style="color:Gray; font-size:10px;">Example: 5.6 [years=5, and months=6]</i><asp:TextBox ID="txtExpMonth" Visible="false" runat="server" ></asp:TextBox></td></tr>

</table><div id="NarrationBox" runat="server" class="expbox"><center><h4><asp:Label ID="lblEligibilityTitle" runat="server"></asp:Label>  Eligibility</h4></center>
<asp:Label ID="lblEligibilgy" runat="server" ></asp:Label><br /><br />
<i style="color:Gray;">Give reason in exceptional cases.</i><br />
<asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" Width="70%" Height="50px" BackColor="#BEE4E5"></asp:TextBox><br />
<center><asp:Button ID="btnNarrationOK" runat="server" Text=" OK " OnClick="btnNarration_Click" /></center>
</div>
<br /><br />
<center><asp:Panel ID="panelShowExpEdit" runat="server" Width="70%" >
Post Held:&nbsp;&nbsp;<b><asp:Label ID="lblShowPostHeld" runat="server" ></asp:Label></b><br />
<b><asp:Label ID="lblShowExpName" runat="server" ></asp:Label></b><br />
Address:&nbsp;<b><asp:Label ID="lblShowExpAddress" runat="server" ></asp:Label></b><br />
Date From:&nbsp;&nbsp;<asp:Label ID="lblShowExpDateFrom" runat="server" ></asp:Label>&nbsp;&nbsp; TO &nbsp;&nbsp;<asp:Label ID="lblShowDateExpTo" runat="server" ></asp:Label><br /><asp:Button ID="btnAddMeorExp" runat="server" OnClick="btnAddMoreExp_Click" Text="Add More" />
<br /><br /><asp:LinkButton ID="lbtnExpStatus" runat="server"></asp:LinkButton>
</asp:Panel></center>
<center><asp:Panel runat="server" ID="PanelInsertExperience" Width="70%">
<fieldset><legend>&nbsp;<img src="../images/leftlink.jpg" />&nbsp;Insert Experience Detials:&nbsp;</legend>
<br /><center><asp:Label ID="lblInsertExpInfo" runat="server" ></asp:Label></center>
<br /><table><tr><td>Post Held:</td><td><asp:TextBox ID="txtPostHeld" runat="server" ToolTip="Enter your Post" ></asp:TextBox><asp:RequiredFieldValidator ID="rexEmpName" runat="server" ControlToValidate="txtPostHeld" Display="Dynamic"  ErrorMessage="Insert Post held" ValidationGroup="A2" ForeColor="Red">*</asp:RequiredFieldValidator></td></tr>
<tr><td>Org/Institute Name:</td><td><asp:TextBox ID="txtEmployerName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmployerName" ErrorMessage="Please Insert Name of Institute or organization" Display="Dynamic" ValidationGroup="A2" ForeColor="Red">*</asp:RequiredFieldValidator></td></tr><tr>
<td>Address:</td><td><asp:TextBox ID="txtExpAddress" runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtExpAddress" ErrorMessage="Please Insert  Address" Display="Dynamic" ValidationGroup="A2" ForeColor="Red">*</asp:RequiredFieldValidator></td></tr>
<tr><td>From(Date):</td><td><telerik:RadDatePicker runat="server" ID="txtExpDAteFrom" ZIndex="30001"></telerik:RadDatePicker></td></tr>
<tr><td>To:</td><td><telerik:RadDatePicker runat="server" ID="txtExpDateTo" ZIndex="3001" ></telerik:RadDatePicker></td></tr>

</table><br /><asp:ValidationSummary ID="ValidatonSummaryExp" runat="server" DisplayMode="BulletList" CssClass="expbox" ValidationGroup="A2"  />
<center><asp:Button ID="btnSaveExp" runat="server" Text="Add" ValidationGroup="A2"
        onclick="btnSaveExp_Click" CssClass="btnsmall" />&nbsp;&nbsp;</center>
</fieldset></asp:Panel></center>
<table><tr><td></td></tr></table>
<div  style="width:auto; height:350px;"></div>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
 
 </asp:Panel> <!-- end edit panel ------------------------- -->
 <asp:Panel ID="panelview" runat="server" >
 <center><asp:Label ID="lblExceptionViewProfiel" runat="server" ></asp:Label></center>
 <div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="lblviewpostback" ForeColor="Gray" runat="server" Text="View"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lblEditView" ForeColor="Gray" runat="server" Text="Edit" OnClick="lbtnCreateAdmin_Click"></asp:LinkButton></div>
 <table class="panelview"><tr align="justify"><td> Name: &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblName" runat="server" Font-Bold="true" ></asp:Label></td><td></td></tr>
         <tr><td>Father's/Husband's:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFName" runat="server" Font-Bold="true" ></asp:Label></td><td></td></tr>
         <tr><td>Designation::&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesignation" runat="server"  Font-Bold="true"></asp:Label></td></tr>
         <tr><td>Address:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAddress" runat="server" Font-Bold="true" ></asp:Label></td><td></td></tr>
         <tr><td>State:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblState" runat="server"  Font-Bold="true"></asp:Label></td><td></td><td>Pin Code:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPincode" runat="server" Font-Bold="true" ></asp:Label></td><td></td></tr>
         <tr><td>Tel No.:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTelNo" runat="server" Font-Bold="true" ></asp:Label></td><td></td><td>Office:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblOffice" runat="server" Font-Bold="true" ></asp:Label></td><td></td><td>Residential:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblResidential" runat="server" Font-Bold="true" ></asp:Label></td><td></td></tr>
         <tr><td>Mobile:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMobile" runat="server" Font-Bold="true" ></asp:Label></td><td></td><td>Email:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmail" runat="server" Font-Bold="true" ></asp:Label></td><td></td></tr>
         <tr><td>Date of Birth:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDateBirth" runat="server" Font-Bold="true"></asp:Label></td><td> </td><td>Age :&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAge" runat="server" Font-Bold="true"></asp:Label></td></tr></table>
         <br />
 <div class="fromRegisterlbl"><h1 >Education Detial:</h1></div>
   <table>
         <tr><td>Highest Qualification:</td><td><asp:Label ID="lblHeigestQualification" Font-Bold="true"  runat="server" ></asp:Label></td></tr>
         
         
      
</table><br /><B>Education Qualification (Engineering Only:)</B>
<br />
<table class="panelview">
<tr><td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesipliceBE" Font-Bold="true" runat="server" ></asp:Label></td></tr>
 <tr><td>College:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCollegeBE" runat="server" ></asp:Label></td></tr>
 <tr><td>University&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUniversityBE" runat="server" ></asp:Label></td></tr>
 <tr><td>Percentage&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPercentageBE" runat="server"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Passing Year:<asp:Label ID="lblYearBE" runat="server" ></asp:Label></td></tr></table>
 <hr /><br /><table class="panelview">
 
<tr><td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesipliineMTech" Font-Bold="true" runat="server" ></asp:Label></td></tr>
 <tr><td>College:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCollegeMTech" runat="server" ></asp:Label></td></tr>
 <tr><td>University:&nbsp;&nbsp;&nbsp;<asp:Label ID="lbluniversityMTech" runat="server" ></asp:Label></td></tr>
 <tr><td>Percentage:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPercentageMTech" runat="server" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Passing Year:<asp:Label ID="lblyearMTech" runat="server" ></asp:Label></td></tr></table>
 <hr /><br /><table class="panelview">
 
<tr><td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesiplinePhd" runat="server"  Font-Bold="true"></asp:Label></td></tr>
 <tr><td>College&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCollegePhd" runat="server" ></asp:Label></td></tr>
 <tr><td>University&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUniversityPhd" runat="server" ></asp:Label></td></tr>
 <tr><td>Percentage:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPercentagePhd" runat="server"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Passing Year:<asp:Label ID="lblyearphd" runat="server" ></asp:Label></td></tr></table>
 <hr /><br /><table class="panelview">
 
<tr><td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lbldesipleineohter" runat="server" Font-Bold="true" ></asp:Label></td></tr>
 <tr><td>College:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCollegeohter" runat="server" ></asp:Label></td></tr>
 <tr><td>University:&nbsp;&nbsp;&nbsp;<asp:Label ID="lbluniversigtyohter" runat="server" ></asp:Label></td></tr>
 <tr><td>Percentage:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpercentageohter" runat="server"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Passing Year:<asp:Label ID="lblyearother" runat="server" ></asp:Label></td></tr>
      
</table>
<br />
 </asp:Panel>
 <asp:Panel ID="panelSubspendAcc" runat="server" >
 <div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton5" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>
 <center><h2 style="color:Maroon; font-size:18px; font-family:Times New Roman;" >Status: <b><asp:Label runat="server" ID="lblStatus2" ></asp:Label></b></h2></center>
 <br />
 <center><asp:Button ID="btnchangeStatus" runat="server" OnClick="btnChnageStatsu" CssClass="bigbutton" /></center>
 <div  style="margin-top:50px;">.</div></asp:Panel>
 
 <asp:Panel ID="panelViewAccount" runat="server" >
 <div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton3" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>
 <center><h2 style="color:Maroon; font-size:18px; font-family:Times New Roman;" >Status: <b><asp:Label runat="server" ID="lblStatus" ></asp:Label></b></h2></center>
 
                            <table>
                            <tr><td>Registration Date:</td><td><asp:Label ID="lblEnrollDate" runat="server" ></asp:Label></td></tr>
                           
                            <tr><td>Renwal Date:</td><td><asp:Label ID="lblRenuwalDate" runat="server" ></asp:Label></td></tr>
                            <tr><td> Membership Expire Date:</td><td><asp:Label ID="lblExpDate" runat="server" ></asp:Label></td></tr>
                            <tr><td><br /></td></tr>
                            <%--<tr><td>Total Submitted Amount:</td><td><asp:Label ID="lbltotleAmt" runat="server" ></asp:Label></td></tr>
                             <tr><td>Last Submission Date:</td><td><asp:Label ID="lblLastDate" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Amount:&nbsp;<asp:Label ID="lblLastAmt" runat="server" ></asp:Label></td></tr>--%>
                            </table><center>This Membership Subscription up to: &nbsp;&nbsp;<asp:Label ID="lblyear" runat="server"></asp:Label></center>
                            <asp:Label ID="lblActive" runat="server" Visible="false"></asp:Label><br /><br />
     <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
         AllowSorting="True" AutoGenerateColumns="False" 
         BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
         CellPadding="2" DataSourceID="SqlDataSource2" ForeColor="Black" 
         GridLines="None" Width="100%">
         <Columns>
             <asp:TemplateField ShowHeader="False">
                 
             </asp:TemplateField>
             <asp:BoundField DataField="YearFrom" HeaderText="Session From" 
                 SortExpression="YearFrom" />
             <asp:BoundField DataField="YearTo" HeaderText="Session To" 
                 SortExpression="YearTo" />
            
             <asp:BoundField DataField="FeeType" HeaderText="Remark" 
                 SortExpression="FeeType" />
             <asp:BoundField DataField="SubDate" HeaderText="Submitted"  ApplyFormatInEditMode="true" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" 
                 SortExpression="SubDate" />
             <asp:BoundField DataField="SubType" HeaderText="Subission Type" 
                 SortExpression="SubType" Visible="false" />
             <asp:BoundField DataField="AcountNo" HeaderText="A/C No" Visible="false"
                 SortExpression="AcountNo" />
             <asp:BoundField DataField="DD" HeaderText="DD No." SortExpression="DD" Visible="false" />
             <asp:BoundField DataField="Bank" HeaderText="Bank Name" SortExpression="Bank" Visible="false" />
                                      
             <asp:BoundField DataField="Amt" HeaderText="Amount(Rs.)" 
                 SortExpression="Amt" />
<asp:BoundField DataField="TransType" HeaderText="TransType" 
                 SortExpression="TransType" />
                 <asp:BoundField DataField="Balance" HeaderText="Balance(Rs)" 
                 SortExpression="Balance" />
         </Columns>
         <FooterStyle BackColor="Tan" />
         <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
             HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
         <HeaderStyle BackColor="Tan" Font-Bold="True" />
         <AlternatingRowStyle BackColor="PaleGoldenrod" />
     </asp:GridView>
     <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
         ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
         SelectCommand="SELECT [MType], [Amt], [FeeType], [SubDate], [SubType], [AcountNo], [DD], [Bank], [YearFrom], [YearTo], [TransType], [Balance] FROM [MemberFee] WHERE ([ID] = @ID) ORDER BY [TransID] DESC">
         <SelectParameters>
             <asp:SessionParameter Name="ID" SessionField="FeeID" Type="String" />
         </SelectParameters>
     </asp:SqlDataSource>
     <br />
         <hr />                   
   <div style="margin-top:350px;" >.</div>
                              
       </asp:Panel>
 <asp:Panel ID="panelViewRenewAcc" runat="server" >
 
 <div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton4" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>
          <br />
          <div style="float:right; margin-right:50px; width:30%; " class="expbox" ><center>Expetional Case</center><br /><asp:Label ID="lblExpCase" runat="server" ></asp:Label></div>
          <center>   Total Year of Experience:&nbsp;&nbsp;<asp:Label ID="lblTotalExpYear" runat="server" ForeColor="Blue"></asp:Label></center>  <br /><br />
             <table class="tbl"><tr><td><b><asp:Label ID="lblPost1" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></b></td><td></td></tr>
               <tr><td>Org./Institute Name:</td><td><asp:Label ID="lblOrg1" runat="server" ></asp:Label></td></tr>
               <tr><td><asp:Label ID="lblf1" runat="server" ></asp:Label>&nbsp;&nbsp;To&nbsp;&nbsp</td><td><asp:Label ID="lblAdd1" runat="server" ></asp:Label></td></tr>
               <tr><td></td><td><asp:Label ID="lblt1" runat="server" ></asp:Label></td></tr>
               
               <tr><td><b><asp:Label ID="lblPost2" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></b></td><td></td></tr>
               <tr><td>Org./Institute Name:</td><td><asp:Label ID="lblOrg2" runat="server" ></asp:Label></td></tr>
               <tr><td><asp:Label ID="lblf2" runat="server" ></asp:Label>&nbsp;&nbsp;To&nbsp;&nbsp</td><td><asp:Label ID="lblAdd2" runat="server" ></asp:Label></td></tr>
               <tr><td></td><td><asp:Label ID="lblt2" runat="server" ></asp:Label></td></tr>
               
               
               <tr><td><b><asp:Label ID="lblPost3" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></b></td><td></td></tr>
               <tr><td>Org./Institute Name:</td><td><asp:Label ID="lblOrg3" runat="server" ></asp:Label></td></tr>
               <tr><td><asp:Label ID="lblf3" runat="server" ></asp:Label>&nbsp;&nbsp;To&nbsp;&nbsp</td><td><asp:Label ID="lblAdd3" runat="server" ></asp:Label></td></tr>
               <tr><td></td><td><asp:Label ID="lblt3" runat="server" ></asp:Label></td></tr>
               
               <tr><td><b><asp:Label ID="lblPost4" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></b></td><td></td></tr>
               <tr><td>Org./Institute Name:</td><td><asp:Label ID="lblOrg4" runat="server" ></asp:Label></td></tr>
               <tr><td><asp:Label ID="lblf4" runat="server" ></asp:Label>&nbsp;&nbsp;To&nbsp;&nbsp;</td><td><asp:Label ID="lblAdd4" runat="server" ></asp:Label></td></tr>
               <tr><td></td><td><asp:Label ID="lblt4" runat="server" ></asp:Label></td></tr>
               
               <tr><td><b><asp:Label ID="lblPost5" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></b></td><td></td></tr>
               <tr><td>Org./Institute Name:</td><td><asp:Label ID="lblOrg5" runat="server" ></asp:Label></td></tr>
               <tr><td><asp:Label ID="lblf5" runat="server" ></asp:Label>&nbsp;&nbsp;To&nbsp;&nbsp;</td><td><asp:Label ID="lblAdd5" runat="server" ></asp:Label></td></tr>
               <tr><td></td><td><asp:Label ID="lblt5" runat="server" ></asp:Label></td></tr>
             </table>             
      <div style="height:300px"></div>
 </asp:Panel>
 
 <asp:Panel ID="panelOther" runat="server" >
 <div style="float:right; margin-right:30px; color:Gray; text-decoration:none;"><asp:LinkButton ID="LinkButton6" runat="server" Text="View Profile" OnClick="lbtnViewPersonal_Click"></asp:LinkButton></div>
 <center><br>
  <asp:DataList ID="DataList1" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "Handler.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
            </asp:DataList>
 
<asp:Label ID="lblImgTitle" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Size="15px" ></asp:Label><br /><br /><br />
<asp:FileUpload ID="fileuploadImage" runat="server" /><br />
&nbsp;&nbsp;&nbsp;&nbsp;
<br />
<asp:Label ID="lblImgException" runat="server" ></asp:Label><asp:Label ID="lblImgStatus" runat="server" Visible="false"></asp:Label><asp:Label ID="lblDocsStatus" runat="server" Visible="false"></asp:Label><br />
<asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" /><br /><br /></center>
           
 <div  style="margin-top:500px;">.</div></asp:Panel>
     


</div>

</div>
</asp:Content>

