<%@ Page Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="DiaryEntry.aspx.cs" Inherits="FO_DiaryEntry" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Courier Dispatch Entry ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td><td><asp:Label ID="lblCourierEntry" runat="server" Text="Courier Dispatch Entry" CssClass="redirecttabhome"></asp:Label></td></tr></table>
             </div>
              <div id="rightpanel2"><div id="header">
    
                           <asp:UpdatePanel ID="updatediray" runat="server" ><ContentTemplate>
                           <div id="Div1" class="fromRegisterlbl" runat="server" ><h1>Courier Dispatch Entry</h1></div>
                           <br /><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
                           <table class="tbl"><tr><td>Select Session:&nbsp;&nbsp;&nbsp;</td><td>
                               <asp:DropDownList ID="ddlExamSeason" runat="server" 
                                   OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" 
                                   CssClass="txtbox" Width="180px" 
                                   onselectedindexchanged="ddlExamSeason_SelectedIndexChanged1" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>
                                   Year:&nbsp; </td>
                               <td>
                                   <asp:TextBox ID="txtYearSeason" runat="server" AutoPostBack="true" 
                                       CssClass="txtbox" OnTextChanged="txtYearSeason_TextChanged" Width="100px"></asp:TextBox>
                               </td>
                               </tr>
                          <tr><td>Send To:</td><td><asp:DropDownList ID="ddlRecivefrom" AutoPostBack="true" 
                                  OnSelectedIndexChanged="ddlRecive_SelectedIndexChanged" runat="server" 
                                  CssClass="txtbox" Width="180px"><asp:ListItem Text="IM" Selected="True" Value="IM"></asp:ListItem><asp:ListItem Text="Student" Value="Student"></asp:ListItem><asp:ListItem Value="Other" Text="Other"></asp:ListItem></asp:DropDownList></td></tr>
                       <tr><td><asp:Label ID="lblFromName" runat="server" ></asp:Label>:</td><td>
                           <asp:TextBox ID="txtSName" AutoPostBack="true" 
                               OnTextChanged="txtSName_TExtChnaged" runat="server" CssClass="txtbox" 
                               Width="175px" Font-Bold="true"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert ">*</asp:RequiredFieldValidator></td></tr>
                         </table><center><asp:Label ID="lblExceptiontbl" runat="server" ></asp:Label></center>
                         <table id="tbllabel" class="tbl" runat="server"><tr><td><asp:Label ID="lblName" runat="server" ></asp:Label></td><td><asp:Label ID="lblCode" runat="server" ></asp:Label></td><td><asp:Label ID="lblCourseAddress" runat="server" ></asp:Label><asp:Label ID="lblState" runat="server" Visible="false" ></asp:Label></td></tr></table>
                      <table class="tbl" id="tbltext" runat="server"><tr><td> Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td colspan="3">
                          <asp:TextBox ID="txtAddress1" 
       runat="server" CssClass="txtbox" Width="180px" ontextchanged="txtAddress1_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td class="style4"></td><td colspan="3">
    <asp:TextBox ID="txtAddress2" 
       runat="server" CssClass="txtbox" Width="180px"></asp:TextBox></td></tr>
<tr><td class="style4"></td><td>State:<br />
    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" 
        CssClass="txtbox" onselectedindexchanged="ddlState_SelectedIndexChanged" 
        Width="144px">
    </asp:DropDownList>
    </td>
    <td class="style3">City:<br />
        <asp:DropDownList ID="ddlCity" runat="server" CssClass="txtbox" Width="144px" 
            AutoPostBack="True" onselectedindexchanged="ddlCity_SelectedIndexChanged">
        </asp:DropDownList><br />
    </td><td>PinCode:<br /><asp:TextBox ID="txtPincode" runat="server" 
            CssClass="txtbox" ontextchanged="txtPincode_TextChanged"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td></tr>
            <tr><td></td><td align="right"><asp:Label ID="lblOtherCity" runat="server" Text="Enter City:" ></asp:Label></td><td>
                <asp:TextBox ID="txtNewCity" runat="server" Width="138px" CssClass="txtbox" ></asp:TextBox>
                </td></tr>
    <tr><td class="style4">Phone:</td><td colspan="3"><asp:TextBox ID="txtPhonecode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox>
    <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPhoneNo" FilterType="Custom, Numbers"  ValidChars="+-=/*()."  >
               </dev:FilteredTextBoxExtender>
               <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txtPhoneNo" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
    </td></tr>
</table>
<asp:Panel ID="panelCourier" runat="server" CssClass="expbox">
<center>
    Name of Courier Serivce:&nbsp;<asp:TextBox ID="txtNewCourier" runat="server" CssClass="txtbox" Width="200px" Font-Bold="true"></asp:TextBox>
<br /> <asp:Label ID="lblExceptionNewCourier" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br /><br /><asp:Button ID="btnSaveNewCourier" runat="server" Text="Save" OnClick="btnSAveNew_Onclick" />    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencel"  runat="server" Text="Cancel" OnClick="btnCencelnew_Onclick" />
</center>
</asp:Panel>
<table class="tbl">
<tr><td>Date:</td><td><asp:TextBox ID="txtDiaryDate" Width="150px" runat="server" CssClass="txtbox"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtDiaryDate" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date " >*</asp:RequiredFieldValidator> 
<dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender22" PopupButtonID="Im11" PopupPosition="BottomRight" runat="server" TargetControlID="txtDiaryDate"></dev:CalendarExtender><img src="../images/cal.png" id="Im11" runat="server"  alt="Cal" /></td></tr>
<tr><td>
   Courier Service :</td><td><asp:LinkButton ID="btnNewCourierService" runat="server" Font-Bold="true" ForeColor="Red" Text="New Courier Service" OnClick="ibtnNewCourier_Onclick"></asp:LinkButton><br /> 
        <asp:DropDownList ID="ddlCourierService" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" 
            Width="180px" CssClass="txtbox">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Courier') ORDER BY Name">
        </asp:SqlDataSource>
        &nbsp;</td><td>Reference No:&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox runat="server" ID="txtCourierNo" CssClass="txtbox" Width="150px"></asp:TextBox></td></tr>
                           <tr><td>Courier Type:</td><td style="margin-left: 40px">
                               <asp:TextBox ID="txtDiraryType" runat="server" CssClass="txtbox" Width="175px"></asp:TextBox></td><td>Consignment No:&nbsp;<asp:TextBox runat="server" ID="txtConsignmentNo" CssClass="txtbox" Width="150px"></asp:TextBox></td></tr>
                            <tr><td>Remark:</td><td colspan="2"><asp:TextBox ID="txtRemoark" runat="server" 
                                    TextMode="MultiLine" Height="40px" CssClass="txtbox" Width="175px"></asp:TextBox></td></tr>
                          <tr><td>Weight:</td><td><asp:TextBox ID="txtWt" runat="server" CssClass="txtbox" Width="150px"></asp:TextBox>
                          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="txtWt" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Enter" >*</asp:RequiredFieldValidator>

                          Kg.
                              </td>
                              </td>
               <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtWt" FilterType="Numbers" />
                         </tr>
                           <caption>
                               &nbsp;&nbsp;<tr>
                                   <td>
                                       Amount:&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td>
                                           <asp:TextBox ID="txtAmt" runat="server" CssClass="txtbox" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtWt" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Enter" >*</asp:RequiredFieldValidator>
                                           Rs.</td>
                              </td>
               <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmt" FilterType="Numbers" />
                                   <caption>
                                      
                                       </td>
                                   </caption>
                                   </tr>
    </caption>
                           </table><br />
                           <center><asp:Label ID="lblExcepitonDiary" runat="server" Font-Bold="True" 
                                   Font-Size="Medium" ></asp:Label><br /><asp:Button ID="btnSaveDiary" runat="server" CssClass="btnsmall" ValidationGroup="Architecture" Text="Send" OnClick="btnSAveDiray_Click" />&nbsp;&nbsp;<asp:Button ID="btnCencelde" runat="server" Text="Cancel" OnClick="btnCencel_Onclick" CssClass="btnsmall" /></center>
                           <asp:Label ID="lblDiaryyHiddend" runat="server" Visible="false"></asp:Label>
 &nbsp;&nbsp;
 <br /></ContentTemplate></asp:UpdatePanel>
          <br /><br /><br /><br /><br /><br /><br />   </div></div>
   <br /><br />
  </asp:Content>
   
   
   
