<%@ Page Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ChangeIMID.aspx.cs" Inherits="Admission_ChangeIMID" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Submit NOC Form
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Submit NOC Form" CssClass="redirecttabhome"></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Submit NOC Form </h1></div><br />
 <asp:UpdatePanel ID="updatepanleIM" runat="server" ><ContentTemplate>
<center>Session:&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:TextBox ID="txtSession" Visible="false" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<br /><asp:Label ID="lblExceptionApp" runat="server" ForeColor="Brown"></asp:Label><asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label> </center><hr />
<center>
<panel>
Student ID: <asp:TextBox ID="txtSID" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="txtSID_OnTextChanged" Width="100px"></asp:TextBox>&nbsp;
New IMID: <asp:TextBox ID="txtIMID" runat="server" AutoPostBack="true" CssClass="txtbox" OnTextChanged="txtIMID_OnTextChanged" Width="100px"></asp:TextBox>
</center>
</panel>
<br /><center>
<asp:Panel ID="pnlChange" runat="server" Visible="false" CssClass="imbox" Width="400px">
<center>
<table width="100%">
<tr><td>&nbsp;<asp:Label ID="lblStudentName" runat="server" Font-Bold="true"></asp:Label><br /><asp:Label ID="lblOldIMID" runat="server" Font-Bold="true" ForeColor="Brown"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblOldIMIDName" runat="server" ></asp:Label></td>
<td>&nbsp;<asp:Label ID="lblIMName" runat="server" Font-Bold="true"></asp:Label>
<br /><b>&nbsp;<asp:Label ID="lblGroupID" runat="server" ForeColor="Brown"></asp:Label>&nbsp;</b><br /></td>
</tr></table>
    <asp:CheckBox ID="chkNOC" runat="server" Text="With NOC" Visible="False" />
</center>
</asp:Panel>
</center>
<br />
<center>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRemark" runat="server" Width="268px" TextMode="MultiLine" Height="40px" CssClass="txtbox"></asp:TextBox>
<dev:TextBoxWatermarkExtender ID="txtRemark_TextBoxWatermarkExtender" runat="server" TargetControlID="txtRemark" WatermarkText="Remark for IM Change.!">
</dev:TextBoxWatermarkExtender>
<br /><asp:Label ID="lblException" runat="server" Font-Bold="true" ForeColor="Brown"></asp:Label><br /><asp:Button ID="btnChange" runat="server" CssClass="btnsmall" Text="Change IM" OnClick="btnChange_Onclick" /></center><hr />
<br />
<b>Instruction:</b><br />
<ul><li>Application Submission IMID Not Changed during NOC Submission Process. Please Change IMID of submitted Application Forms in account section.</li></ul>
<center><asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="txtbox"><asp:ListItem Text="All" Value="All"></asp:ListItem><asp:ListItem Value="Student" Text="Student" /></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtStudent" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtStudent_OnctextChaged" />
<asp:DropDownList ID="ddlViewType" runat="server" AutoPostBack="True" CssClass="txtbox"><asp:ListItem Value="IMID" Text="IM Changed" /><asp:ListItem Value="Course" Text="Student Promotion" /><asp:ListItem Value="Other" Text="Other Changes" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" OnClick="btnView_Onclick" Text="View" CssClass="btnsmall" />
</center>
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
<div class="headerDivImgfees">
<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
<br />
<br />
</div><div style="padding:1px;"><h1>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;</h1></div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 98%; overflow:scroll; height:300px" >
<asp:GridView ID="GridChange" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="True"
        CellPadding="8" CellSpacing="8" PageSize="50" 
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%" 
        onselectedindexchanged="GridChange_SelectedIndexChanged">
        <EmptyDataTemplate><center> No Record found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
   </div>
   </div></div>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>

