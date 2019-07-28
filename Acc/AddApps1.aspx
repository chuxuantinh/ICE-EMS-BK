<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="AddApps1.aspx.cs" Inherits="Acc_AddApps" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Add Application Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server" ><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fee Master:&nbsp;<asp:DropDownList ID="ddlFeeMaster" runat="server" CssClass="txtbox" Width="60px"><asp:ListItem Value="Home" Text="Home" /><asp:ListItem Value="Overseas" Text="Overseas" /></asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Application Forms Entry:</h1></div>
<center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlsession" runat="server" 
        OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="80px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox></center>
<table class="tbl">
<tr><td>IMID:&nbsp;&nbsp;</td><td><asp:TextBox ID="txtIMID" Width="100px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtIMID_TextChanged"></asp:TextBox></td>
<td><asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;Status:&nbsp;<asp:Label ID="lblIMStatus" runat="server"></asp:Label></td></tr>
<tr><td>Diary No:&nbsp;&nbsp;</td><td><asp:TextBox ID="txtDiaryNo" Width="100px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtDiaryNo_TextChaged"></asp:TextBox><asp:Image ID="ibtnViewDairy" ImageUrl="~/images/dairycount.gif"  runat="server" AlternateText="Dairy" /></td>
<td>Diary Date:&nbsp;&nbsp;<asp:Label ID="txtDiaryRcvDate" runat="server" Font-Bold="true"></asp:Label> &nbsp;&nbsp;&nbsp;Session:&nbsp;&nbsp;
    <asp:Label ID="lblSessionHiddend" runat="server" Font-Bold="true"></asp:Label></td></tr>
</table><dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-350" OffsetY="0" PopupControlID="pnlDairyCount" TargetControlID="ibtnViewDairy" ></dev:PopupControlExtender>
<asp:Panel ID="pnlDairyCount" runat="server" Width="350px" CssClass="pnlpopup" >
<div class="redsubtitle"><center>Application Form Count</center></div>
<table width="100%"><tr><td>Applications</td><td>Total Received</td><td>Total Submitted</td></tr>
<tr><td>Academic DD</td><td><asp:Label ID="lblADDRcv" ForeColor="White" runat="server" ></asp:Label></td><td><asp:Label ForeColor="White" ID="lblADDSub" runat="server" ></asp:Label></td></tr>
<tr><td>Others DD</td><td><asp:Label ID="lblODDRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblODDSub"  ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Admission </td><td><asp:Label ID="lblAdmissionRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblAdmissionSub" ForeColor="White"  runat="server" ></asp:Label></td></tr>
<tr><td>Examination</td><td><asp:Label ID="lblExamRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblExamSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>ITI </td><td><asp:Label ID="lblITIRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblITISub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Auto-CAD</td><td><asp:Label ID="lblCADRcv" runat="server" ForeColor="White"></asp:Label></td><td><asp:Label ID="lblCADSub" runat="server" ForeColor="White"></asp:Label></td></tr>
<tr><td>Others Form</td><td><asp:Label ID="lblOthersFormRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblOthersFormSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Provisional</td><td><asp:Label ID="lblProvisionalRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProvisionalSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Final Pass</td><td><asp:Label ID="lblFinalPassRcv" runat="server"  ForeColor="White"></asp:Label></td><td><asp:Label ID="lblFinalPassSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Re-Checking</td><td><asp:Label ID="lblReCheckingRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblReCheckingSub" runat="server"  ForeColor="White" ></asp:Label></td></tr>
<tr><td>Duplicate Docs</td><td><asp:Label ID="lblDuplicateRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblDuplicateSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project DD</td><td><asp:Label ID="lblProjectRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProjectSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaC</td><td><asp:Label ID="lblProformaCRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaCSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Project ProformaB</td><td><asp:Label ID="lblProformaBRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProformaBSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Membership DD</td><td><asp:Label ID="lblMembershipRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblMembershipSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Books DD</td><td><asp:Label ID="lblBooksRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblBooksSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
<tr><td>Prospectus DD</td><td><asp:Label ID="lblProsRcv" runat="server" ForeColor="White" ></asp:Label></td><td><asp:Label ID="lblProsSub" runat="server" ForeColor="White"  ></asp:Label></td></tr>
</table>
</asp:Panel>
<center><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true"></asp:Label></center><hr />
</ContentTemplate></asp:UpdatePanel>
<asp:UpdatePanel ID="updatepanel3" runat="server" >
<Triggers><asp:PostBackTrigger ControlID="btnAddApproveVisible" /></Triggers>
<ContentTemplate>
<asp:Panel ID="panelRightBox" runat="server" CssClass="rightbox" >
<center>
<table runat="server" id="tblMembershipNo"><tr><td><asp:Label ID="lblEnrolName" runat="server" Text="Membership No."></asp:Label>&nbsp;</td><td><asp:TextBox ID="txtEnrolID" Width="80px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtEnrolID_TextChanged"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="chkAddwithAdmisiosn" runat="server" Text="With Admission" AutoPostBack="true" OnCheckedChanged="chkAddWithAdmission_OnCheckChanged" /></td></tr></table>
<div id="name" runat="server"><asp:Label ID="lblFulldName" runat="server" Font-Bold="true"></asp:Label><br />
<asp:Label ID="lblFullCourse" runat="server" ></asp:Label></div>
<asp:Label ID="lblITISerialNo" runat="server" Font-Bold="true"></asp:Label>
<asp:Label ID="lblserialNo" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br /><asp:Label ID="lblExceptionEnrolID" runat="server" ForeColor="Maroon" Font-Bold="true" ></asp:Label></center>
</asp:Panel><center>
<asp:Label  ID="lblExceptionCount" runat="server" ForeColor="Red"></asp:Label></center>
<asp:Panel ID="panelselectapp" runat="server" CssClass="imbox">
Select Application Forms Type:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
<asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="true" CssClass="txtbox" Width="250px" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged">
<asp:ListItem Text="------ Select Application Type------" Selected="True"></asp:ListItem>
<asp:ListItem Value="ITI" Text="ITI Application Forms" />
<asp:ListItem Value="Composite" Text="Student Composite Fees" />
<asp:ListItem Value="Subscription" Text="Student Annual Subscription" />
<asp:ListItem Value="Exmp" Text="Exemption Form" />
</asp:DropDownList>
</asp:Panel>
<asp:Panel ID="PanelProfile" runat="server">
<br />
<table class="tbl"><tr><td>Stream:</td><td colspan="2"><asp:Label ID="lblStreamDDL" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></td></tr>
<tr><td>Course:</td><td colspan="2"><asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SeelctedIndexchanged" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil" /><asp:ListItem Value="Architecture" Text="Architectural" /></asp:DropDownList></td>
<td>Part/Section:&nbsp;</td><td><asp:DropDownList  ID="ddlPart" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" runat="server" CssClass="txtbox"><asp:ListItem Value="" Text="--Select--" /><asp:ListItem Value="PartI" Text="PartI" /><asp:ListItem Value="PartII" Text="PartII" /><asp:ListItem Value="SectionA" Text="SectionA" /><asp:ListItem Value="SectionB" Text="SectionB" /></asp:DropDownList></td></tr>
<tr><td>Student Name:</td><td colspan="2"><asp:TextBox ID="txtName" runat="server" 
        CssClass="txtbox" ontextchanged="txtName_TextChanged"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="txtName" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert  Name" >*</asp:RequiredFieldValidator></td><td>Father's/Husband's Name:</td><td><asp:TextBox ID="txtFName" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtFName" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Father Name" >*</asp:RequiredFieldValidator></td><%--<asp:TextBox ID="txtLName" runat="server" CssClass="txtbox"></asp:TextBox>--%></tr>
<tr><td>Date of Birth:&nbsp;&nbsp;</td><td colspan="2">
<asp:Label ID="lblExceptionDateOfBirth" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br /><asp:TextBox AutoPostBack="true" OnTextChanged="txtDate_TechChanged" ID="txtBirth" runat="server" CssClass="txtbox" ></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtBirth" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date of Birth" >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="Img1" PopupPosition="BottomRight" runat="server" TargetControlID="txtBirth"></dev:CalendarExtender> <img src="../images/cal.png" id="Img1" runat="server"  alt="Cal" />
</td><td>
<asp:LinkButton OnClick="lbtnCheckDuplicate_Onclick" ID="lbtnCheckDues" runat="server" Text="Check Duplicate"  ForeColor="Red" Font-Bold="true"></asp:LinkButton></td>
</tr>
</table></asp:Panel>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="true" DisplayMode="BulletList" CssClass="expbox" ValidationGroup="Architecture" />
<!-- Invisible Controls -->
<asp:Label ID="lblFeeLevel" runat="server" Visible="false" ></asp:Label><asp:Label ID="lblCourseLevel" Text="081" runat="server"  Visible="false"></asp:Label><asp:Label ID="lblCompositeDuration" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblcomStatus" runat="server"  Visible="false"></asp:Label><asp:Label ID="lblExamStatus" runat="server"  Visible="false"></asp:Label><asp:Label ID="lblenrolStatus" runat="server"  Visible="false"></asp:Label><asp:Label ID="lblUnderAge" runat="server"  Visible="false"></asp:Label>
  <asp:Label ID="lblCourse" runat="server" Visible="false"></asp:Label><asp:Label ID="lblpart" runat="server" Visible="false"></asp:Label><asp:Label ID="lblHiddendStream" runat="server" Visible="false" ></asp:Label><asp:Label ID="lbllavel" runat="server" Visible="false"></asp:Label>
   <asp:Label ID="lblFormType" runat="server" Visible="false"></asp:Label><asp:Label ID="lblPartIISID" runat="server" Visible="false"></asp:Label>
   <asp:Label ID="lblASF" runat="server" Visible="false"></asp:Label>
  <!-- Invisible controls End-->
  <asp:Panel ID="PanelITI" runat="server" >
  <center><font style="color:Maroon; font-size:18px; font-family:Times New Roman; padding:0px;">ITI Fees Module</font></center>
    <table width="100%"><tr><td>ITI Fees:&nbsp;&nbsp;Rs.&nbsp;<asp:Label 
            ID="txtITIFees"  runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label></td></tr></table>
  <br /><hr />
  </asp:Panel>
    <asp:Panel ID="PanelExamFee" runat="server" >
    <center><font style="color:Maroon; font-size:18px; font-family:Times New Roman; padding:0px;">Examination Fees Module</font></center><br />
    <table width="100%">
    <tr><td>Examination Fee:&nbsp;<asp:Label ID="lblExamFee" runat="server" 
            Font-Bold="True" ForeColor="Maroon" ></asp:Label>&nbsp;Rs.</td><td>Late Fee Apply:&nbsp;&nbsp;<asp:Label 
                ID="lblLFee" runat="server" Font-Bold="False" ForeColor="Maroon" ></asp:Label>&nbsp;Rs.</td></tr><tr><td>Exemption Fee:&nbsp;<asp:Label 
                ID="lblExmpFee" runat="server" Font-Bold="True" ForeColor="Maroon" ></asp:Label>&nbsp;Rs.</td><td>No. of Exempted Subject[If Have]:&nbsp;&nbsp;<asp:TextBox AutoPostBack="true" OnTextChanged="lblExampNo_TextChanged"  ID="lblExmpNo" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox></td></tr>
    </table>
    <center><asp:Label ID="lblException" runat="server" ></asp:Label></center><hr />
    </asp:Panel>
    <asp:Panel ID="PanelComposite" runat="server" >
  <center><font style="color:Maroon; font-size:18px; font-family:Times New Roman; padding:0px;">Student Composite Fees Module</font></center><br />
  <table width="80%"><tr><td>Composite Fees Applied:&nbsp;<asp:Label ID="lblCompositeFeesFromExam" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label> &nbsp;&nbsp;Rs.</td></tr>
    </table>
    <fieldset><legend>&nbsp;Composite Fees Master:</legend>
    <table class="tbl" width="80%"><tr><td>Part I/Section A&nbsp;&nbsp;<asp:Label 
          ID="lblCompoisteA" runat="server" Font-Bold="True" ForeColor="Maroon" ></asp:Label> &nbsp;Rs.</td><td>Part II/Section B &nbsp;&nbsp;<asp:Label 
            ID="lblCompositeB" runat="server" Font-Bold="True" ForeColor="Maroon" ></asp:Label> &nbsp;Rs.</td></tr></table>
    </fieldset>
    </asp:Panel>
     <asp:Panel ID="PanelSubscriptin" runat="server" >
  <center><font style="color:Maroon; font-size:18px; font-family:Times New Roman; padding:0px;">Student Annual Subscription Fees Module</font></center><br />
  <center>Annual Subscription Fees:&nbsp;&nbsp;<asp:Label ID="lblAnnualSubscriptin" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;Rs.<br /></center>
    </asp:Panel>
   <asp:Panel ID="panelAnnualFromExam" runat="server" >
 <table width="80%" class="tbl"><tr><td>   Annual Subscription Fees:&nbsp;&nbsp;Rs.&nbsp;<asp:Label ID="lblAnnualFeesFromExam" runat="server" Font-Bold="true"></asp:Label></td><td>Year(s):&nbsp;&nbsp;<asp:Label ID="lblAnnualYear" runat="server" Font-Bold="true"></asp:Label></td></tr></table>
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
    <center>
        <asp:Label ID="lblFeesStore" runat="server" Visible="false"></asp:Label><asp:Label ID="lblExceptionAppTable" runat="server"></asp:Label><br /><asp:Label ID="lblExceptionCheck" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></center>
   <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
        &nbsp;&nbsp;&nbsp;&nbsp;<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><table style="color:White; font-weight:bold;"><tr><td>
           <asp:Button ID="btnAddApproveVisible" CssClass="btnsmall" runat="server"  OnClientClick="return confirm('Confirm Submit Form ?');"   
               Text="Add Record" OnClick="btnAddToApproveTable_Click" 
               ValidationGroup="Architecture" Height="22px" /></td><td>
           &nbsp;</td><td> <asp:Label ID="lblFeesType" runat="server" ForeColor="White" Font-Bold="true"></asp:Label></td><td> Amount:&nbsp;<asp:Label ID="lblThisFormAmtAppTable" runat="server" Font-Bold="true" ForeColor="White"></asp:Label>&nbsp;Rs.   </td><td>Late Fees:&nbsp;&nbsp;<asp:Label ID="lblLateFeeChargedApptable" runat="server" Font-Bold="true" ForeColor="White" ></asp:Label>&nbsp;Rs.</td></tr></table>
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
  <div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
    <a id="A14" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/plus.png" alt="Show"></a>
</div><table style=" color:White; padding:5px; width:80%; font-weight:bold;"><tr><td> &nbsp;<asp:Label Visible="false" ID="lblExamSnFo" runat="server" ></asp:Label></td><td><asp:Label Visible="false" ID="lblAdmiSnFO" runat="server" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total Amount:&nbsp;<asp:Label ID="lblToAmtFo" runat="server" ForeColor="White" Font-Bold="true"></asp:Label>&nbsp;Rs. </td><td>&nbsp;&nbsp;&nbsp;Total Late Fees:&nbsp;<asp:Label ID="lblToLateFo" runat="server" ForeColor="White" Font-Bold="true"></asp:Label>&nbsp;Rs. </td></tr></table>
   <div id="Div4" style="display: none;">
   </div></div>
<asp:Panel ID="panelpop" runat="server">
<hr />Search In:&nbsp;&nbsp;<asp:DropDownList ID="ddlSearchIn" runat="server" CssClass="txtbox" ><asp:ListItem Value="All" Text="All" /><asp:ListItem Value="IM" Text="IM" /><asp:ListItem Value="App" Text="Apps"></asp:ListItem></asp:DropDownList>
&nbsp;&nbsp;<asp:DropDownList ID="ddlSearchFor" runat="server" CssClass="txtbox" ><asp:ListItem Value="Name" Text="Name" /><asp:ListItem Value="FName" Text="Father Name" /><asp:ListItem Value="Membership" Text="Membership No." /><asp:ListItem Value="SNO" Text="App. Serial No"></asp:ListItem><asp:ListItem Value="DOB" Text="Birth Date" /></asp:DropDownList>&nbsp;&nbsp;>>>&nbsp;&nbsp;<asp:TextBox ID="txtNameSearch" Width="200px" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:Button ID="btnSearch" OnClick="btnSearch_Onclick" runat="server" CssClass="btnsmall" Text="Search" />
<br /><script>
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
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px;"><h1><asp:Label ID="lblSearchlabel" runat="server" ></asp:Label></h1></div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 97%; overflow:scroll; height:auto">
<asp:GridView ID="GridDuplicacy" runat="server"
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
        CellPadding="3" 
        GridLines="Vertical" HorizontalAlign="Center" Width="100%" onrowdatabound="GridDuplicacy_RowDataBound" ShowFooter="True" 
                         ForeColor="Black">
        <EmptyDataTemplate><center> Duplicate Record Not found !</center></EmptyDataTemplate>
        <RowStyle HorizontalAlign="Center" />
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
   </div>
   </div></div>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>