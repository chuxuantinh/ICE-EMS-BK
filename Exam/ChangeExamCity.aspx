<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ChangeExamCity.aspx.cs" Inherits="Exam_ChangeExamCity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">
 Change Exam City
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanage4r4" runat="server" ></asp:ScriptManager>
    <div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Change Exam Center" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Center Change & Generate New Roll No. </h1></div><br />
<center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" Width="100px"></asp:TextBox><asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label><br />
Upload File &nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnsmall" onclick="btnSave_Click" /><br />
New Exam City:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedInexChanged"  CssClass="txtbox"></asp:DropDownList>&nbsp;&nbsp;<asp:Label runat="server" ID="txtExamID" ></asp:Label>
&nbsp;&nbsp;&nbsp;<asp:Button ID="btnChangeCity" runat="server" Text="Change City" OnClick="btnChangeCity_Click" CssClass="btnsmall" />
<br /><asp:Label ID="lblmessage" runat="server" ></asp:Label></center>
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
<div class="togalfees" style="width:99%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Uploaded Membership No:</div>
<div id="Div1x" style="display:block;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid1" style="width: 100%; overflow:scroll; height:400px" >
                 <asp:GridView runat="server" ID="GridSID" AutoGenerateColumns="true"></asp:GridView>
   </div>
   </div></div>
</div><br />
</asp:Content>

