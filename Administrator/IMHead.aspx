<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/IMMaster.master" AutoEventWireup="true" CodeFile="IMHead.aspx.cs" Inherits="Administrator_IMHead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Head of IM
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Head of Institution</h1></div><br />
<asp:UpdatePanel ID="updatepanel3" runat="server" ><Triggers><asp:PostBackTrigger ControlID="lblBuildingTitle" /></Triggers><ContentTemplate>
<center><asp:Label ID="lblRegisterTitle" runat="server"  ForeColor="Green" Font-Bold="true" Text="Register"></asp:Label>&nbsp;&nbsp;<<&nbsp;&nbsp;<asp:Label ID="lbtnIMHeadTitel" runat="server" ForeColor="Green" Font-Bold="true"  Text="IM Head"></asp:Label>&nbsp;&nbsp;>>&nbsp;&nbsp;<asp:LinkButton ID="lblBuildingTitle" runat="server"  ForeColor="Gray" Font-Bold="true" Text="
 Building Details" OnClick="lbtnBuildingTitel_Click"></asp:LinkButton><br /><asp:Label ID="lblTitleInfo" runat="server" ForeColor="Red"></asp:Label></center><br /><br />
<table class="tbl"><tr><td>Full Name :</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="reqfiled" runat="server" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Please Insert Candidate Name">*</asp:RequiredFieldValidator></td><td colspan="2">Designation.:<br /><asp:TextBox ID="txtDEsignation" runat="server" CssClass="txtbox" ></asp:TextBox></td></tr>


<tr><td>Permanent Address:</td><td colspan="3"><asp:TextBox ID="txtPAddress" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Permanent Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtAddressHead2" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td></td><td>State:<br /><asp:DropDownList ID="ddlstate" runat="server" 
        onselectedindexchanged="ddlstate_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlstate" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage=" Insert City Name">*</asp:RequiredFieldValidator></td><td>City:<br />
        <asp:DropDownList 
            ID="ddlcity" runat="server" 
            onselectedindexchanged="ddlcity_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcity" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert State Name">*</asp:RequiredFieldValidator></td><td>PinCode:<br /><asp:TextBox 
            ID="txtPPincode" runat="server" CssClass="txtbox" AutoPostBack="True" 
            ontextchanged="txtPPincode_TextChanged"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtPPincode" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPPincode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
            <tr><td></td>
    <td> 
        </td><td><asp:Label ID="lblcity" runat="server">Other City</asp:Label><br />
        <asp:TextBox ID="txtothercity" runat="server" CssClass="txtbox" 
            AutoPostBack="True" ontextchanged="txtothercity_TextChanged"></asp:TextBox></td>
    <td class="style1"></td></tr>
<tr><td></td><td><asp:CheckBox ID="chkBothAddressSame" runat="server" AutoPostBack="true" OnCheckedChanged="chkBothAddressSame_CheckChanged" Text="Both Address Same" /></td></tr>

<tr><td>Correspondence Address:</td><td colspan="3"><asp:TextBox ID="txtCAddress" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td colspan="3"><asp:TextBox ID="txtCAddressHead2" runat="server" CssClass="txtbox" Width="60%"></asp:TextBox></td></tr>
<tr><td></td><td>City:<br /><asp:TextBox ID="txtCCity" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCAddress" Display="Dynamic" ValidationGroup="Architecture" ErrorMessage="Insert Correspondence Address">*</asp:RequiredFieldValidator></td><td>State:<br /><asp:TextBox ID="txtCState" runat="server" CssClass="txtbox" ></asp:TextBox></td><td>PinCode:<br /><asp:TextBox ID="txtCPin" runat="server" CssClass="txtbox"></asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="PIN CODE limit exit." ValueToCompare="999999" ControlToValidate="txtCPin" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxEender2" runat="server" TargetControlID="txtCPin" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>


<tr><td>Phone:</td><td colspan="3"><asp:TextBox ID="txtPhonecode"  runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhoneNo" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPhonecode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>

<tr><td>Fax:</td><td colspan="3"><asp:TextBox ID="txtFaxCode" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFaxNo" Width="150px" runat="server" CssClass="txtbox"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFaxCode" FilterType="Numbers"></dev:FilteredTextBoxExtender><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFaxNo" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>


<tr><td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" controltovalidate="txtMobile" Display="Dynamic" ValidationGroup="Architecture" errormessage="Please Insert Mobile No." >*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Mobile No. can not be greater than 12 No." ValueToCompare="999999999999" ControlToValidate="txtMobile" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobile" FilterType="Numbers"></dev:FilteredTextBoxExtender></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Architecture" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td></tr>


<tr><td>Date of Birth:</td><td><asp:TextBox ID="txtDOB" runat="server" 
        CssClass="txtbox" AutoPostBack="True" ontextchanged="txtDOB_TextChanged"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDOB" Display="Dynamic" ValidationGroup="Architecture" errormessage="Insert Date of Birth" >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDOB"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Age:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox 
        ID="txtAge" runat="server" CssClass="txtbox" ontextchanged="txtAge_TextChanged"></asp:TextBox></td></tr>

</table> </ContentTemplate></asp:UpdatePanel>
<asp:Panel ID="panelRight" runat="server" >

<table class="tbl"><tr><td><b>Education Qualification</b></td><td><asp:TextBox ID="txtEducationQ" runat="server" CssClass="txtbox"></asp:TextBox></td></tr><tr><td>Professional Experience</td><td><asp:TextBox ID="txtExperience" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;Years.</td></tr></table>
<br /><center><asp:Label ID="lblExceptionSave" runat="server" ></asp:Label><br /><asp:Button ID="btnSave" runat="server" CssClass="btnsmall" Text="Submit" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" Text="Next" CssClass="btnsmall" OnClick="btnNext_Click" /></center><br /><br />
<div class="fromRegisterlbl"><h1>Photo of IM Head:</h1></div>
<asp:Panel ID="panelImage" runat="server" >
<div id="imheadleft"><center><b>Head of Management</b><br />
  <asp:Image runat="server" ID="imgDefault"  ImageUrl="~/images/userbg.png" Height="250" Width="200px" />
        <asp:DataList ID="DataList1" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
            </asp:DataList><br /><br />
            <asp:DataList ID="dlsign" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler2.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="150px" Width="200px"  />
</ItemTemplate>
            </asp:DataList><br /><br />
<center><asp:Label ID="lblImgTitle" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Size="15px" ></asp:Label></center><br /><br /><br />
<asp:FileUpload ID="fileuploadImage" runat="server" /><br />
<asp:RadioButton ID="rbtnHImage" Checked="true" runat="server" Text="IM Head Photo" GroupName="Architecture" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnHSign" runat="server" Text="IM Head Signature" GroupName="Architecture" />
<br />
<asp:Label ID="lblImgException" runat="server" ></asp:Label><asp:Label ID="lblImgStatus" runat="server" Visible="false"></asp:Label><asp:Label ID="lblDocsStatus" runat="server" Visible="false"></asp:Label><br />
<asp:Button ID="btnUpload" runat="server" CssClass="btnsmall" Text="Upload" onclick="btnUpload_Click" /><br /><br /></center><br /><div style="height:200px" ></div>
</div>
<div id="imheadright">
<center><b>Academic Head of Institute</b><br />
  <asp:Image runat="server" ID="Image2"  ImageUrl="~/images/userbg.png" Height="250" Width="200px" />
        <asp:DataList ID="DataList3" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
       
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler3.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
            </asp:DataList><br /><br />
            <asp:DataList ID="DataList4" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
                <ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler4.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"ID") %>'   Height="150px" Width="200px"  />
</ItemTemplate>
            </asp:DataList>


<center><asp:Label ID="lblImgTitle2" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Size="15px" ></asp:Label></center><br /><br /><br />
<asp:FileUpload ID="fileupload1" runat="server" /><br />

<asp:RadioButton ID="rbtnAImg" Checked="true" runat="server" Text="Academic Head Photo" GroupName="B" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnASign" runat="server" Text="Academic Head Signature" GroupName="B" /><br />
<asp:Label ID="lblImgException2" runat="server" ></asp:Label><asp:Label ID="lblDocsStatus2" runat="server" Visible="false"></asp:Label><asp:Label ID="lblImgStatus2" runat="server" Visible="false"></asp:Label><br />
<asp:Button ID="btnUpload2" CssClass="btnsmall" runat="server" Text="Upload" onclick="btnUpload2_Click" /><br /><br /></center>


</div>

<br />
<br /><br />
</asp:Panel>
</asp:Panel>
</div>

<br /> 
</asp:Content>

