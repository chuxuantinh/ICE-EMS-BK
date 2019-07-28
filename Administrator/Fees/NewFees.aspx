<%@ Page Language="C#" MasterPageFile="~/Administrator/Fees/FeeMaster.master" AutoEventWireup="true" CodeFile="NewFees.aspx.cs" Inherits="Administrator_Fees_NewFees" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Manage Fees Schedule
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link href="../../style.css" rel="stylesheet" type="text/css" />
        <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
<asp:Label ID="lblexepinfo" runat="server" ></asp:Label>
  <script>
        function toggle(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="../../images/plus.png">';
            }
            else {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="../../images/minus.png">';
            }
        }
    </script>
   <div class="togalfees">
    <div class="headerDivImgfees">
    
    <a id="imageDivLink1" href="javascript:toggle('contentDivImg1', 'imageDivLink1');"><img src="../../images/minus.png" alt="Show"></a>
</div><h1>Create New Fees Schedule</h1>
<div id="contentDivImg1" style="display: block;">
    
  <asp:UpdatePanel ID="updatepanelMembership" runat="server" ><ContentTemplate>
  <div ><br />
  <center><table><tr><td>Fees Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox></td></tr></table>
  <asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label><br />
  <div class="hl3" runat="server" id="dvlvl"><h3>New FeesSchedule Level:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNewid" runat="server" ></asp:Label></h3></div>
  <asp:Label ID="lblExceptionID" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br /><br />
  <div><asp:Button ID="btnCreateNew" CssClass="bigbutton" OnClick="btnCreateNew_Onclick" runat="server" Text="Generate Schedule" /></div><br />
  </center>
  
  </div>
  <div class="fromRegisterlbl"><h1>Control Fees Schedule</h1></div>
  <br /><br />
  
  <div class="feestable">
  
  
  </div>
    </ContentTemplate></asp:UpdatePanel>
    </div>
   </div>
   
</asp:Content>

