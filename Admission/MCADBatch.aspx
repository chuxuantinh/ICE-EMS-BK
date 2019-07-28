<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="MCADBatch.aspx.cs" Inherits="Admission_MCADBatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">M-CAD Batch Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td></tr></table></div>
<div id="rightpanel2">
<asp:ScriptManager runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 class="right">Current Batch:&nbsp;<asp:Label ID="lblCurrentBatch" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>Create/Manage M-CAD Batch</h1></div>
<center>                         Batch No:&nbsp;&nbsp;<asp:TextBox ID="lblBatchNo" Font-Bold="true" ForeColor="Maroon" runat="server" CssClass="txtbox" ></asp:TextBox>
        </center>
        <table class="tbl">
            <tbody>
                 <tr><td>
                  <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Home Fees(Rs):-</font></legend>
                 <table class="tbl"><tr><td>Registraiton Fees:</td><td><asp:TextBox ID="txtRegistrationFee" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            controltovalidate="txtRegistrationFee" Display="Dynamic" 
                            errormessage="Insert Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers"  TargetControlID="txtRegistrationFee" runat="server"></asp:FilteredTextBoxExtender>
                           </td>
                           </tr>
                            
                            <tr><td>Late Fees:</td><td>
                        <asp:TextBox ID="txtLatefee" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            controltovalidate="txtLatefee" Display="Dynamic" errormessage="Insert Date " 
                            ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers"  TargetControlID="txtLatefee" runat="server"></asp:FilteredTextBoxExtender>
                    </td></tr></table>
             </fieldset>
                 </td>
                 <td>
                 <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Overseas Fees(dolor):-</font></legend>
                 <table class="tbl"><tr><td>Registraiton Fees:</td><td>
                        <asp:TextBox ID="txtRegistrationFeeOverseas" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            controltovalidate="txtRegistrationFeeOverseas" Display="Dynamic" 
                            errormessage="Insert Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtRegistrationFeeOverseas"  Mask="99,999.99"   MessageValidatorTip="true"  MaskType="Number"   InputDirection="RightToLeft"   AcceptNegative="Left" 
                            DisplayMoney="Left"
                            ErrorTooltipEnabled="True" />
                            
                        </td></tr>
                            
                            <tr><td>Late Fees:</td><td>
                        <asp:TextBox ID="txtLatefeeOverseas" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            controltovalidate="txtLatefeeOverseas" Display="Dynamic" errormessage="Insert Date " 
                            ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                            TargetControlID="txtLatefeeOverseas"  Mask="99,999.99"   MessageValidatorTip="true"  MaskType="Number"   InputDirection="RightToLeft"   AcceptNegative="Left" 
                            DisplayMoney="Left"
                            ErrorTooltipEnabled="True" />
                   </td></tr></table>
             </fieldset>
                 </td>
                </tr>
                <tr>
                    <td>
                      Starting Date:&nbsp;&nbsp;  <asp:TextBox ID="txtStartingDate" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="Im11" PopupPosition="BottomRight" 
                            TargetControlID="txtStartingDate">
                        </asp:CalendarExtender><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            controltovalidate="txtStartingDate" Display="Dynamic" 
                            errormessage="Insert Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                     <img src="../images/cal.png" id="Im11" runat="server"  alt="Cal" />
                            
                    </td>
                    <td>
                        Ending Date:&nbsp;&nbsp;<asp:TextBox ID="txtEndingDate" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtEndingDate">
                        </asp:CalendarExtender><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            controltovalidate="txtEndingDate" Display="Dynamic" 
                            errormessage="Insert Date " ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
                        <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <center><asp:Button ID="btnSave" runat="server" ValidationGroup="Architecture" CssClass="btnsmall" 
                            onclick="btnSave_Click" Text="Create" />
                        <asp:Button ID="btnupdate" runat="server" ValidationGroup="Architecture" CssClass="btnsmall" 
                            onclick="btnupdate_Click" Text="Edit" />
                        <asp:Label ID="lblException" runat="server"></asp:Label></center>
    <br /><br />
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
        <div class="togalfees" style="width:100%">
            <div class="headerDivImgfees">
                <a ID="A12" href="javascript:toggleA1w('Div12', 'A12');">
                <img alt="Show" src="../images/minus.png"></img></a>
                <br />
                <br />
            </div>
            <div style="padding:1px;">
                <h1>
                    Batch Details</h1>
            </div>
            <br />
            <div ID="Div12" style=" overflow:scroll;display:block;">
                <input id="scrollPos2" runat="server" type="hidden" value="0" />
                <div ID="divdatagrid2" style=" height:300px">
                <center>
                    <asp:GridView ID="grvAutocad" runat="server" AllowPaging="True" 
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="20%" 
                        onpageindexchanging="grvAutocad_PageIndexChanging" 
                        onrowdatabound="grvAutocad_OnRowDataBound" 
                        onselectedindexchanged="grvAutocad_OnselectedIndexChanged" Width="100%">
                        <Columns>
                            <asp:CommandField ShowSelectButton="true" />
                        </Columns>
                        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                        <EmptyDataRowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            No records found
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </center>
                </div>
            </div>
        </div>
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>