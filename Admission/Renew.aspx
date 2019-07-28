<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="Renew.aspx.cs" Inherits="User_Renew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Approve Admission Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Approve Admission Forms" CssClass="redirecttabhome"></asp:Label></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnlcomplete" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Approve Admission Forms</h1></div>
<br /><center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="60px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblExceptionApp" runat="server"></asp:Label>
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Status:&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="150px" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged">
<asp:ListItem Value="NotApproved" Text="Not Approved" />
<asp:ListItem Value="Approved" Text="Approved" /><asp:ListItem Value="Pending" Text="Pending" /></asp:DropDownList>&nbsp;&nbsp;
<asp:Button ID="btnViewa" runat="server" CssClass="btnsmall" OnClick="btnView_Onclick" Text="View" /><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true" ></asp:Label>
</center><br />
<br />
<script>
     function toggleA1(showHideDiv, switchImgTag) {
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
<a id="A1" href="javascript:toggleA1('Div1', 'A1');"><img src="../images/minus.png" alt="Show"></a>
</div> <br /><br /><br />
<div id="Div1" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid" style="width: 100%; overflow:scroll; height:200px" >
<script type="text/javascript">
       // Let's use a lowercase function name to keep with JavaScript conventions
       function selectAll(invoker) {
           // Since ASP.NET checkboxes are really HTML input elements
           //  let's get all the inputs
           var inputElements = document.getElementsByTagName('input');
           for (var i = 0; i < inputElements.length; i++) {
               var myElement = inputElements[i];
               // Filter through the input types looking for checkboxes
               if (myElement.type === "checkbox") {
                   // Use the invoker (our calling element) as the reference 
                   //  for our checkbox status
                   myElement.checked = invoker.checked;
               }
           }
       } 
</script>
<asp:GridView ID="GridToBeApprove" runat="server" BackColor="#DEBA84" OnRowDataBound="GridToBeApprove_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" 
        CellSpacing="5" Width="100%" 
        onselectedindexchanged="GridToBeApprove_SelectedIndexChanged">
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns><asp:ButtonField ButtonType="Link" CommandName="Select" Text="Approve" Visible="false" />
        <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView>
</div>
<asp:Panel ID="PnlStudInfo" runat="server" Visible="False" Height="180px">
<br />
<div class="rightbox">
       <h3>
           <asp:Label ID="lblEnrol" runat="server" Visible="False"></asp:Label>
           <u>Student Info</u></h3>
            &nbsp;&nbsp;&nbsp;&nbsp;<table class="style1">
                <tr>
                <td align="left"><b>Name:</b></td>
                <td align="left"><asp:Label ID="lblName" runat="server"></asp:Label></td>
                </tr>
                <tr><td align="left"><b>DOB:</b></td>
                <td align="left"><asp:Label ID="lblDob" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td align="left"><b>Course:</b></td>
                <td align="left"><asp:Label ID="lblCourse" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td align="left"><b>Stream:</b></td>
                <td align="left"><asp:Label ID="lblStream" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td align="left"><b>Part:</b></td>
                <td align="left"><asp:Label ID="lblPart" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td align="left"><b>Status:</b></td>
                <td align="left"><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td align="left"><b>Remarks:</b></td>
                <td align="left"><asp:Label ID="Remarks" runat="server"></asp:Label></td>
                </tr>
       </table>
</div>
<div style="padding-top:50px;"><center>
<asp:Button ID="btnApprove" runat="server" Text="Approve" onclick="btnApprove_Click" CssClass="btnsmall"/></center>
<br /><br /><br /><br /></div>
</asp:Panel>
<asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
</div>
</div>
</ContentTemplate></asp:UpdatePanel>
<asp:Panel ID="pnlspace" Height="150PX" runat="server"></asp:Panel>
</div>
</asp:Content>

