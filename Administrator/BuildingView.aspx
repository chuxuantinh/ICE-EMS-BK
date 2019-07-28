<%@ Page Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="BuildingView.aspx.cs" Inherits="Administrator_BuildingView" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">IM Building Info
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
        
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Institutional Member Building Infrastructure</h1></div><br />
 <div id="invisible" runat="server" style="height:500px;" ><center>Please Inter Student Enrolment No.in search box.</center></div>
 <div id="visisble" runat="server">

 <table  class="tbl">
 <tr><td>Name of Center:</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtName" runat="server"   Font-Bold="true"  ></asp:Label></td></tr>
 </table><table class="tbl">
 
<tr><td>Permanent Address:</td><td colspan="3"><asp:Label ID="txtPAddress"  runat="server" Font-Bold="true"  ></asp:Label></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtPCity" runat="server" Font-Bold="true" ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtPState" runat="server" Font-Bold="true"  ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtPPincode" runat="server" Font-Bold="true"></asp:Label></td></tr>

<%--
<tr><td>Correspondence Address:</td><td colspan="3"><asp:Label ID="txtCAddress" TextMode="MultiLine" Height="35px" runat="server"  Width="60%"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td>City:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCCity" runat="server" ></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;State:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCState" runat="server"  ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;Pin:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtCPin" runat="server" ></asp:Label><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtCPin" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextExtender ID="FilteredLabelEender2" runat="server" TargetControlID="txtCPin" FilterType="Numbers"></dev:FilteredTextExtender></td></tr>--%>


<tr><td>Phone:</td><td colspan="3"><asp:Label ID="txtPhoneNo" runat="server" Font-Bold="true"></asp:Label></td></tr>

<tr><td>Fax:</td><td colspan="3"><asp:Label ID="txtFaxNo" runat="server"  Font-Bold="true"></asp:Label></td></tr>


<tr><td>Mobile:</td><td><asp:Label ID="txtMobile" runat="server" Font-Bold="true" ></asp:Label>
</td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtEmail" runat="server" Font-Bold="true" ></asp:Label></td></tr>
     </table>  <br />
 
    
    
        <br />
      <div class="fromRegisterlbl"><h1>Existing Infrastructure</h1></div>
        <br />
        <table class="tbl"><tr><td>Name of Building:</td><td>&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtBName" runat="server"   Font-Bold="true"></asp:Label></td></tr>
        <tr><td>Type:</td><td>&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="ddlBType" runat="server"  Font-Bold="true"></asp:Label></td></tr>
        <tr><td>Plinth Area:</td><td>&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtBPArea" runat="server" Font-Bold="true"></asp:Label>&nbsp;Sq.ft.</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Covered Area:&nbsp;&nbsp;&nbsp;<asp:Label ID="txtBCArea" runat="server" Font-Bold="true"></asp:Label>&nbsp;Sq.ft.</td></tr>
        </table>
        
        
<br /><br />
<div class="fromRegisterlbl"><h1> Building Facilities Available</h1></div>
    
       
        <br />
        <table style="width:100%;">
      <tr style="text-align:center;"><td style="height: 23px; width: 76px;">Particulars</td><td style="height: 23px; width: 165px;">No. of Rooms
                </td>
                <td style="height: 23px; width: 170px;">
                    Seating Capacity</td>
                <td style="width: 163px; height: 23px">
                    Total Area (Sq.ft.)</td>
            </tr>
            <tr>
                <td style="height: 23px; width: 76px;">
                    Class Rooms</td>
                <td style="height: 23px; width: 165px; text-align:center;">
                    <asp:Label ID="txtBNRoom1" runat="server"   Font-Bold="true"></asp:Label>
                    
                   
                </td>
                <td style="height: 23px; width: 170px; text-align:center;"><asp:Label ID="txtBSCapacity1" Font-Bold="true"  runat="server" ></asp:Label>
                    
                   
                </td>
                <td style="width: 163px; height: 23px;text-align:center;"><asp:Label ID="txtBTotal1" Font-Bold="true" runat="server" ></asp:Label>
                    
                   
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Labs</td>
                <td style="height: 23px; width: 165px;text-align:center;"><asp:Label ID="txtBNRoom2" Font-Bold="true" runat="server" ></asp:Label>
                    
                   
                </td>
                <td style="height: 23px; width: 170px;text-align:center;"><asp:Label ID="txtBSCapacity2" Font-Bold="true"  runat="server" ></asp:Label>
                    
                  
                </td>
                <td style="width: 163px; height: 23px;text-align:center;"><asp:Label ID="txtBTotal2" Font-Bold="true"  runat="server" ></asp:Label>
                    
                 
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Computer Lab</td>
                <td style="height: 23px; width: 165px;text-align:center;"><asp:Label ID="txtBNRoom3" Font-Bold="true"  runat="server" ></asp:Label>
                    
                    
                </td>
                <td style="height: 23px; width: 170px;text-align:center;"><asp:Label ID="txtBSCapacity3" Font-Bold="true" runat="server" ></asp:Label>
                    
                   
                </td>
                <td style="width: 163px; height: 23px;text-align:center;"><asp:Label ID="txtBTotal3" Font-Bold="true"  runat="server" ></asp:Label>
                    
                    
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Library</td>
                <td style="height: 23px; width: 165px;text-align:center;"><asp:Label ID="txtBNRoom4" Font-Bold="true" runat="server" ></asp:Label>
                    
                   
                </td>
                <td style="height: 23px; width: 170px;text-align:center;"><asp:Label ID="txtBSCapacity4" Font-Bold="true" runat="server" ></asp:Label>
                    
                   
                </td>
                <td style="width: 163px; height: 23px;text-align:center;"><asp:Label ID="txtBTotal4" Font-Bold="true" runat="server" ></asp:Label>
                    
                   
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Reception</td>
                <td style="height: 23px; width: 165px;text-align:center;"><asp:Label ID="txtBNRoom5" Font-Bold="true" runat="server"  ></asp:Label>
                    
                  
                </td>
                <td style="height: 23px; width: 170px;text-align:center;"><asp:Label ID="txtBSCapacity5" Font-Bold="true" runat="server"  ></asp:Label>
                    
                   
                </td>
                <td style="width: 163px; height: 23px;text-align:center;"><asp:Label ID="txtBTotal5" Font-Bold="true"  runat="server" ></asp:Label>
                    
                    
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Admin Block</td>
                <td style="height: 23px; width: 165px;text-align:center;"><asp:Label ID="txtBNRoom6" Font-Bold="true" runat="server"  ></asp:Label>
                    
                    
                </td>
                <td style="height: 23px; width: 170px;text-align:center;"><asp:Label ID="txtBSCapacity6" Font-Bold="true"  runat="server"  ></asp:Label>
                    
                 
                </td>
                <td style="width: 163px; height: 23px;text-align:center;"><asp:Label ID="txtBTotal6" Font-Bold="true"  runat="server" ></asp:Label>
                    
                  
                </td>
            </tr> <tr>
                <td style="height: 23px; width: 76px;">
                    Any Other</td>
                <td style="height: 23px; width: 165px;text-align:center;"><asp:Label ID="txtBNRoom7" Font-Bold="true"  runat="server" ></asp:Label>
                    &nbsp;
                </td>
                <td style="height: 23px; width: 170px;text-align:center;"><asp:Label ID="txtBSCapacity7" Font-Bold="true"  runat="server" ></asp:Label>
                 
                </td>
                <td style="width: 163px; height: 23px;text-align:center;"><asp:Label ID="txtBTotal7" Font-Bold="true"  runat="server"    ></asp:Label>
                s
                </td>
            </tr>
        </table>
      <br /><br />
 </div>
 </div>
</asp:Content>


