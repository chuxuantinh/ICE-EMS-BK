<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="UploadDocs.aspx.cs" Inherits="User_UploadDocs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Upload Student Documents
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">    function fixform() {
        if (opener.document.getElementById("aspnetForm").target != "_blank") return;
        opener.document.getElementById("aspnetForm").target = "";
        opener.document.getElementById("aspnetForm").action = opener.location.href;
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Upload Documents" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1> Documents/Profile Image</h1></div>
<div class="docsbar"><div style="float:right;"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/docstitle.png" OnClick="ibtnDocsPnlShow_Click" />&nbsp;&nbsp;&nbsp;&nbsp;</div><asp:ImageButton ID="btnImgPnlShow" runat="server" ImageUrl="~/images/imgtitle.png" OnClick="ibtnImgPnlShow_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<img id="imgdocbar" runat="server" src="../images/docbar.png" alt="Student Documents" /><img id="imgimgbar" runat="server"  src="../images/imgbar.png" alt="Student Documents" /></div>
<asp:Panel ID="panelDocs" runat="server" >
<asp:UpdatePanel ID="updatepanelseelct1210" runat="server" ><ContentTemplate>
<br /><center><asp:RadioButton ID="rbtn10" runat="server" AutoPostBack="true"  GroupName="Architecture" Text="Upload 10th Marksheet" oncheckedchanged="rbtn10_CheckedChanged" />&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtn12" runat="server" Text="Upload 12th Marksheet" AutoPostBack="true" GroupName="Architecture" oncheckedchanged="rbtn12_CheckedChanged" />&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="Architecture" ID="rbtnother" runat="server" Text="Other Documents"  AutoPostBack="true" oncheckedchanged="rbtnother_CheckedChanged" /><p runat="server" id="pNewdoc">Document Name:&nbsp;&nbsp;<asp:TextBox Width="200px" ID="txtDocsNameOther" runat="server" CssClass="txtbox" ontextchanged="txtDocsNameOther_TextChanged"></asp:TextBox></p></center>
</ContentTemplate></asp:UpdatePanel>
<div style="float:right; margin-right:50px; margin-top:10px;  width:40%; height:auto;"><center>
<asp:Label ID="lblDocstitle" runat="server" ></asp:Label><br />
<asp:FileUpload ID="filMyFile" runat="server" Width="200px" BorderColor="AliceBlue"  BorderStyle="Groove" BorderWidth="3px" /><br />
<asp:Label ID="lblInfo" runat="server" ></asp:Label><br /><br />
<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="cmdSendNew2_Click" CssClass="btnsmall" />
</center></div>
<div style="margin-left:5%; width:40%; height:auto; " >
<br /><asp:Label ID="lblDownlloadInfo" runat="server" ></asp:Label><br /><center>
<div id="blnkk"  onload="fixform()">
<asp:Panel ID="doc10" runat="server">
<asp:Label ID="lbl10Name" Width="250px" runat="server" Text="10th Marksheet" CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtn10view" runat="server" Text="View" OnClick="lbtn10View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtn10Download" runat="server" Text="Download" OnClick="lbtn10Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel>
<asp:Panel ID="doc12" runat="server" >
<asp:Label ID="lbl12Name" Width="250px" runat="server" Text="12th Marksheet" CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtn12View" runat="server" Text="View" OnClick="lbtndoc12View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtndoc12download" runat="server" Text="Download" OnClick="lbtn12Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel>
<asp:Panel ID="doc1" runat="server" >
<asp:Label ID="lbldoc1Name" Width="250px" runat="server"  CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtndoc1View" runat="server" Text="View" OnClick="lbtndoc1View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtndoc1Download" runat="server" Text="Download" OnClick="lbtndoc1Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel>
<asp:Panel ID="doc2" runat="server" >
<asp:Label ID="lbldoc2Name" Width="250px" runat="server"  CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtndoc2View" runat="server" Text="View" OnClick="lbtndoc2View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtndoc2Download" runat="server" Text="Download" OnClick="lbtndoc2Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel><asp:Panel ID="doc3" runat="server" >
<asp:Label ID="lbldoc3Name" Width="250px" runat="server"  CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtndoc3View" runat="server" Text="View" OnClick="lbtndoc3View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtndoc3Download" runat="server" Text="Download" OnClick="lbtndoc3Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel><asp:Panel ID="doc4" runat="server" >
<asp:Label ID="lbldoc4Name" Width="250px" runat="server"  CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtndoc4View" runat="server" Text="View" OnClick="lbtndoc4View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtndoc4Download" runat="server" Text="Download" OnClick="lbtndoc4Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel><asp:Panel ID="doc5" runat="server" >
<asp:Label ID="lbldoc5Name" Width="250px" runat="server" CssClass="lbl"></asp:Label><asp:LinkButton ID="lbtndoc5View" runat="server" Text="View" OnClick="lbtndoc5View_Click" OnClientClick="aspnetForm.target='_blank';" CssClass="lbllnk"></asp:LinkButton><asp:LinkButton ID="lbtndoc5Download" runat="server" Text="Download" OnClick="lbtndoc5Download_Click"  CssClass="lbllnk"></asp:LinkButton><br /><hr /><br /></asp:Panel></div></center>
<asp:Label ID="lbllnk10" runat="server" Visible="false"></asp:Label><asp:Label ID="lbllnk12" runat="server" Visible="false"></asp:Label><asp:Label ID="lbllnk1" runat="server" Visible="false"></asp:Label><asp:Label ID="lbllnk2" runat="server" Visible="false"></asp:Label><asp:Label ID="lbllnk3" runat="server" Visible="false"></asp:Label><asp:Label ID="lbllnk4" runat="server" Visible="false"></asp:Label><asp:Label ID="lbllnk5" runat="server" Visible="false"></asp:Label>
</div><br /><br /><br /><br /><br /><br /><br /><br />
</asp:Panel>
<asp:Panel ID="panelImage" runat="server" >
<br /><div style="float:right; margin-right:50px; margin-top:100px;  width:40%; height:auto;"><center>
<center><asp:Label ID="lblImgTitle" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Size="15px" ></asp:Label></center><br /><br /><br />
<asp:FileUpload ID="fileuploadImage" runat="server"/><br /><br /><br />
<asp:Label ID="lblImgException" runat="server" ></asp:Label><asp:Label ID="lblImgStatus" runat="server" Visible="false"></asp:Label><asp:Label ID="lblDocsStatus" runat="server" Visible="false"></asp:Label><br /><br />
<asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" CssClass="btnsmall" /><br /><br /></center>
</div>
<div style="margin-left:5%; width:40%; height:auto; " >
<asp:Image runat="server" ID="imgDefault"  ImageUrl="~/images/userbg.png" Height="250" Width="200px" />
<asp:DataList ID="DataList1" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
<ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"SID") %>'   Height="250px" Width="200px"  />
</ItemTemplate>
</asp:DataList><br /><br />
<asp:DataList ID="DataList2" runat="server"  RepeatColumns="1" RepeatDirection="Horizontal">
<ItemTemplate>     
<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "ImageHandler2.ashx?ImID="+ DataBinder.Eval(Container.DataItem,"SID") %>'   Height="150px" Width="200px"  />
</ItemTemplate>
</asp:DataList>
</div><br /><br />
</asp:Panel>
<asp:ValidationSummary ID="VSummary1" CssClass="expbox" runat="server" DisplayMode="BulletList" ValidationGroup="Architecture" ForeColor="Red" />
</div>
</asp:Content>
