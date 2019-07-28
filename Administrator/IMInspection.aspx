<%@ Page Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="IMInspection.aspx.cs" Inherits="Administrator_IMInspection" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Inspection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .binaryImage img
        {
            border: 1px solid;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager" runat="server" ></asp:ScriptManager>
      <div style=" float:right; margin:10px; margin-top:0px; width:49%;"><asp:Panel ID="panelChangeStatus" runat="server" >
       <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Change Membership Status:-</font></legend>
     
 <center><h2 style="color:Maroon; font-size:18px; font-family:Times New Roman;" >Status: <b><asp:Label runat="server" ID="lblStatus2" ></asp:Label></b></h2></center>
 <center id="sesionfrom" runat="server" >Session:&nbsp;<asp:DropDownList ID="ddlSessionFrom" runat="server" ><asp:ListItem Text="Summer" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtYearFrom" runat="server" CssClass="txtbox" Width="80px"></asp:TextBox>
                 </center>
 <center><asp:Label ID="lblExceptionActive" runat="server" Font-Bold="true" ></asp:Label><br /><asp:Button ID="btnchangeStatus" runat="server" OnClick="btnChnageStatsu_OnClick" CssClass="bigbutton" /></center>
      </fieldset><fieldset><legend>Fees Module:</legend>
                       Inspection Charges:&nbsp;&nbsp;<b><asp:Label ID="lblInfectionFee" runat="server" ></asp:Label> &nbsp;Rs.</b><br />
                       Membership Fee(Annual):&nbsp;<b><asp:Label ID="lblEnrollFee" runat="server" ></asp:Label> Rs.</b> &nbsp;&nbsp;&nbsp;Subscription Fee(Annual):&nbsp;<b><asp:Label ID="lblSubFee" runat="server" ></asp:Label> Rs.</b>
                       </fieldset></asp:Panel>
      </div>
      <div style="width:49%; margin:10px; margin-top:0px;">
       <asp:Panel ID="panelProfile" runat="server" >
              
               <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Personal Information:-</font></legend>
               <table><tr><td>Member Type:</td><td><asp:Label ID="lblMemberTyep" runat="server"></asp:Label></td><td>ID:&nbsp;&nbsp;<asp:Label ID="lblID" runat="server"></asp:Label></td></tr>
               <tr><td>Name:</td><td align="left"><asp:Label ID="lblName" runat="server"></asp:Label></td><td></td></tr>
               <tr><td>Address:</td><td><asp:Label ID="lblAddress" runat="server" ></asp:Label></td></tr>
               <tr><td>City:</td><td><asp:Label ID="lblCity" runat="server"></asp:Label></td><td>Email:&nbsp;&nbsp;<asp:Label ID="lblEmail" runat="server" ></asp:Label></td></tr>
               <tr><td>Mobile:</td><td><asp:Label ID="lblMobile" runat="server"></asp:Label></td><td>Phone:&nbsp;&nbsp;<asp:Label ID="lblPhonne" runat="server" ></asp:Label></td></tr>
                 <tr><td>Registration Date:</td><td><asp:Label ID="lblEnrollDate" runat="server" ></asp:Label></td>
                            <td>Renwal Date: &nbsp;&nbsp;<asp:Label ID="lblRenuwalDate" runat="server" ></asp:Label></td></tr>
                            <tr><td>Expiry Date:</td><td><asp:Label ID="lblExpDate" runat="server" ></asp:Label></td></tr>
               </table>
               </fieldset>
      </asp:Panel></div>
       <div style=" float:right; margin:10px; margin-top:0px; width:49%;"><asp:Panel ID="panelSubscribe" runat="server" >
       <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Change Inspection Status :-</font></legend>
                <center>Session:&nbsp;<asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="80px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox>
              <asp:Label ID="lblSessionHiddend" Visible="false" runat="server" Font-Bold="true"></asp:Label><br />
                 Current Balance (Rs):&nbsp;<asp:Label ID="lblBalance" runat="server" Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="lblBalanceType" runat="server" ></asp:Label>
                 </center>
                 <asp:Panel ID="panelSubspendAcc" runat="server" >
 <center><h2 style="color:Maroon; font-size:12px; font-family:Times New Roman;" ><b><asp:DropDownList ID="ddlChangeStatus" runat="server" Width="150px" CssClass="txtbox"><asp:ListItem Value="Approve" Text="Approve" /><asp:ListItem Value="NotApprove" Text="Not Approve" /><asp:ListItem Value="Pending" Text="Pending" /></asp:DropDownList></b></h2></center>
 <br />
 <center>Inspection No.:&nbsp;<asp:Label ID="lblInspectionNo" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblExceptionChngApprove" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br />
 <asp:Button ID="btnSubscribe" Text="Change" runat="server" OnClick="btnSubscribe_Onclick" CssClass="bigbutton" /></center>
 <br /><hr />
   <table>
    </table><center>This Membership Subscription up to: &nbsp;&nbsp;<asp:Label ID="lblyear" runat="server"></asp:Label> &nbsp;&nbsp;To &nbsp;&nbsp;<asp:Label ID="lblYearto" runat="server" ></asp:Label></center>
                            <asp:Label ID="lblActive" runat="server" Visible="false"></asp:Label><br />
     <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width:420px; overflow:scroll; height:150px">
     <asp:GridView ID="GridBalance" runat="server" AllowPaging="True" 
         AutoGenerateColumns="true" 
         BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
         CellPadding="2"  ForeColor="Black" OnSelectedIndexChanged="gridInspection_SelectedIndexChanged"
         GridLines="None" onrowdatabound="GridBalance_RowDataBound" >
           
        <EmptyDataRowStyle Width="100%"  HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                      Inspection Record Not Found
                    </EmptyDataTemplate>
         <Columns>
           <asp:ButtonField CommandName="Select" Text="select" />  
          <%-- <asp:BoundField  HeaderText="ID"  /> --%>
         </Columns>
         <FooterStyle BackColor="Tan" />
         <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
             HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
         <HeaderStyle BackColor="Tan" Font-Bold="True" />
         <AlternatingRowStyle BackColor="PaleGoldenrod" />
     </asp:GridView>
      </div>    
       </asp:Panel> 
       </fieldset></asp:Panel>
       </div>
       <div style=" width:49%; margin-left:10px;"><asp:Panel ID="panelInspection" runat="server" >
       <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">IM Inspection Report:-</font></legend>
     <center><asp:Label ID="lblExceptionInspection" ForeColor="Red" Font-Bold="true" runat="server" ></asp:Label></center>
      <table>
      <tr><td>Name:</td><td><asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
      <tr><td>Address:</td><td><asp:TextBox ID="txtAddress" runat="server" Width="200px" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ValidationGroup="D" ErrorMessage="Insert Permanent Address"  >*</asp:RequiredFieldValidator></td></tr>
      <tr><td></td><td><asp:TextBox ID="txtAddress2" runat="server" Width="200px" CssClass="txtbox"></asp:TextBox></td></tr>      
      <tr><td>State:&nbsp; </td><td><asp:DropDownList ID="ddlState" runat="server" CssClass="txtbox" onselectedindexchanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> &nbsp;&nbsp;City:&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddlCity" runat="server" onselectedindexchanged="ddlCity_SelectedIndexChanged"  AutoPostBack="true"> </asp:DropDownList> </td></tr>
      
      <tr><td>PinCode:</td><td><asp:TextBox ID="txtPinCode" runat="server" Width="100px" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPinCode" Operator="LessThanEqual" Type="Double" ValidationGroup="D">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPinCode" FilterType="Numbers"></dev:FilteredTextBoxExtender>&nbsp;&nbsp;Phone:&nbsp;<asp:TextBox ID="txtPhone" runat="server" Width="100px" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtPhone" Display="Dynamic" ValidationGroup="D" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
      <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtPhone" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPhone" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
      <tr><td>Investigator:</td><td><asp:TextBox ID="txtInvertigetor" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInvertigetor" Display="Dynamic" ValidationGroup="D" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
      <tr><td>Designation:</td><td><asp:TextBox ID="txtDesignation" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDesignation" Display="Dynamic" ValidationGroup="D" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
      <tr><td>Status:</td><td><asp:CheckBox ID="chkBuilding" runat="server" Text="Building Infra." />&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkEdu" runat="server" Text="Education" /></td></tr>
      <tr><td>FeedBack:</td><td><asp:TextBox ID="txtFeedback" TextMode="MultiLine" Height="50px" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox></td></tr>
      <tr><td>ApprovalStatus:</td><td><asp:DropDownList ID="ddlIMStatus" runat="server" Width="150px" CssClass="txtbox"><asp:ListItem Value="Approve" Text="Approve" /><asp:ListItem Value="NotApprove" Text="Not Approve" /><asp:ListItem Value="SubToApprove" Text="Subject To Approve" /></asp:DropDownList></td></tr>
      <tr><td>Date:</td><td><asp:TextBox ID="txtDate" runat="server" CssClass="txtbox" 
              ontextchanged="txtDate_TextChanged"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate" Display="Dynamic" ValidationGroup="D" errormessage="Insert Date of Birth" >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td></tr>
      </table>
       <br /><br />
       <center><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnsmall"  ValidationGroup="D"  CausesValidation="true" OnClientClick="return confirm('Are you sure, submit Inspection Report ?');"   OnClick="btnSave_ONclick"/>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClean" runat="server"  CssClass="btnsmall" Text="Clean" OnClick="btnClean_Onclick" /></center>
       <br /><br /> <center> <asp:ValidationSummary runat="server" ID="ValidationSummary" ValidationGroup="D" DisplayMode="BulletList" /></center><br />
       </fieldset></asp:Panel>
       </div>
       <br />
       <br />
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
    <div class="headerDivImgfees"> <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:15px;">Membership Type:&nbsp;&nbsp;<asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox" Width="120px">
     <asp:ListItem Value="All" Text="All Member" /><asp:ListItem Value="Fellow" Text="Fellow Member" /><asp:ListItem Value="Member" Text="Member" /><asp:ListItem Value="Honorary" Text="Honorary Member" /><asp:ListItem Text="Institutional Member" Value="IM" /></asp:DropDownList>
&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="120px" CssClass="txtbox"><asp:ListItem Value="Disactive" Text="Disactive" /><asp:ListItem Value="Active" Text="Active" /><asp:ListItem Value="Registered" Text="New Registered" /><asp:ListItem Value="Blocked" Text="Blocked" /></asp:DropDownList>
   &nbsp;&nbsp;<asp:Button ID="btnViewGrid" runat="server" Text="View" OnClick="btnViewGrid_Onclick"  CssClass="btnsmall"/>
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
    Inspection Details:&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" AutoPostBack="true"  Width="100px" OnTextChanged="txtIMID_TextChaned" CssClass="txtbox"></asp:TextBox>
   <br />
   </div>
<div id="Div1x" style="display:block;">
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:350px;">
<asp:GridView ID="GridDuplicacy" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="8" CellSpacing="8" OnSelectedIndexChanged="Grid_OnselectedIndexChanged"
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%"  EmptyDataText="N/A" 
                         AllowPaging="True" ShowHeaderWhenEmpty="true" 
                         onpageindexchanging="GridDuplicacy_PageIndexChanging" onrowdatabound="GridDuplicacy_RowDataBound"  >
        <EmptyDataRowStyle Width="100%"  HorizontalAlign="Center" />
     <EmptyDataTemplate> No Data Found </EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <Columns>
        <asp:ButtonField CommandName="Select" HeaderText="View Status" Text="select" />
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" 
            HorizontalAlign="Center"/>
        <EditRowStyle HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
   </div>
   </div></div>
</asp:Content>

