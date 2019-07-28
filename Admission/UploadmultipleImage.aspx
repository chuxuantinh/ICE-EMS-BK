<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="UploadmultipleImage.aspx.cs" Inherits="Admission_UploadmultipleImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Upload Student Photo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Upload Documents" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Upload Student Photo</h1></div>
<br />
<center><asp:Label ID="lblExceptionUpload" runat="server" Font-Bold="true"></asp:Label><br /><br />
Membership No.:&nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;<asp:FileUpload ID="filMyFile" runat="server" Width="200px" BorderColor="AliceBlue"  BorderStyle="Groove" BorderWidth="3px" />&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnUpload" runat="server" Text="Save" OnClick="btnUpload_Click" CssClass="btnsmall" /><br />
<asp:Label ID="lblInfo" runat="server" ></asp:Label><br /><br />

<div style="background:#ffecb5;"><h3 style="color:Red;">Instruction</h3><ul><li>Insert Membership No. Of whom You want to upload pic.</li><li>Browse the image Path.</li><li>Pic is saved to the destination Folder.</li></ul></div>
</center><br /><br />
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1><h1>Update Pics from tif to jpg</h1></div>
<br />
<center><br />Update Student Pics in Database from tif format to jpg format. </center>
<table class="tbl"><tr align="center"><td><asp:Label ID="lblImgTitle" runat="server"></asp:Label></td></tr>
<tr align="center"><td><b>Select Membership Range </b></td><td><asp:TextBox ID="txtMem1" runat="server" CssClass="txtbox"></asp:TextBox> </td><td> <b>TO</b><asp:TextBox ID="txtMem2" runat="server" CssClass="txtbox"></asp:TextBox></td><td> <asp:Button ID="btnSave" runat="server" Text="Upload"  CssClass="btnsmall" 
        onclick="btnSave_Click"/></td></tr></table><br /><asp:Label ID="lblImgException" runat="server" ForeColor="Red" ></asp:Label>
<asp:Label ID="lblExcept" runat="server" ForeColor="Maroon" ></asp:Label><asp:Label ID="lblImgStatus" runat="server" Visible="false"></asp:Label><asp:Label ID="lblDocsStatus" runat="server" Visible="false"></asp:Label>
<br /><br /><center>
<div id="leftlink" style="background:#ffecb5;"><h3  style="color:Red;">Instruction</h3><ul><li>Select the range of Membership of whom you want to upload the pics.</li><li>Pics are uploaded to the database corresponding to their ids.</li></ul></div></center>
<br />
</div>
<br />
<br />
<br />
<br />
<br />
<br />
</asp:Content>

