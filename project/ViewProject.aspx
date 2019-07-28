<%@ Page Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ViewProject.aspx.cs" Inherits="project_ViewProject" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">View Allocation Project
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 153px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="View Project Status" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp; 
<asp:Image ID="imgStatus" runat="server" AlternateText="Project Status" ImageUrl="~/images/ProjectIcon.png" Width="25px"  /></h1><h1>View Project Status</h1></div>&nbsp;
<dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-550" OffsetY="0" PopupControlID="pnlViewStatus" TargetControlID="imgStatus" ></dev:PopupControlExtender>
<asp:Panel ID="pnlViewStatus" runat="server" CssClass="pnlpopup">
<table class="tbl" width="95%">
<tr><td class="style2">Total Students</td><td><asp:Label ID="lblProToStudent" runat="server" ForeColor="White" Font-Bold="True"></asp:Label></td></tr>
<tr><td class="style2">Proforma A</td><td>Submitted:&nbsp;<asp:Label ID="lblProformaASub" runat="server" Font-Bold="True" ForeColor="White" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Approved:&nbsp;<asp:Label ID="lblProformaAApp" runat="server" Font-Bold="True" ForeColor="White"></asp:Label></td></tr>
<tr><td class="style2">Proforma B</td><td>Submitted:&nbsp;<asp:Label ID="lblProformaBSub" runat="server"  Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Approved:&nbsp;<asp:Label ID="lblProformaBApp" runat="server" Font-Bold="True" ForeColor="White"></asp:Label></td></tr>
<tr ><td class="style2">Copy Submission</td><td>Pending:&nbsp;<asp:Label ID="lblCopyPending" runat="server"  Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Submitted:&nbsp;<asp:Label ID="lblCopySubmitted" runat="server"  Font-Bold="True" ForeColor="White"></asp:Label></td></tr><tr>
<td class="style2">Proforma C</td><td><asp:Label ID="lblPorformaC" runat="server" Font-Bold="True" ForeColor="White"></asp:Label></td></tr>
<tr><td class="style2">ReSubmit/DisApproved</td><td><asp:Label ID="lblProResubmit" runat="server"  Font-Bold="True" ForeColor="White"></asp:Label></td></tr>
</table>
</asp:Panel><asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label><center><table class="tbl"><tr>
<td>Session:&nbsp;&nbsp;</td><td colspan="2"><asp:DropDownList ID="ddlExamSeason" runat="server"  OnTextChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;<asp:Textbox ID="txtYearSeason" runat="server"  AutoPostBack="true" Width="60px" CssClass="txtbox" OnTextChanged="txtYearSeason_TextChanged"></asp:Textbox></td></tr>
<tr><td> Search:</td><td><asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="True" CssClass="txtbox" onselectedindexchanged="ddlSearch_SelectedIndexChanged">
<asp:ListItem Value="Status" Text="Status"></asp:ListItem>
<asp:ListItem Value="IMID" Text="IMID"></asp:ListItem>
<asp:ListItem Value="Enrolment" Text="Membership" Selected="True"></asp:ListItem>
<asp:ListItem Value="InstitutionID" Text="InstitutionID"></asp:ListItem>
</asp:DropDownList></td><td id="pnltxt" runat="server"> <asp:TextBox ID="txtID" runat="server" CssClass="txtbox" AutoPostBack="True" ontextchanged="txtID_TextChanged"></asp:TextBox></td>
<td id="pnlStatus" runat="server">Project Status:<asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox" AutoPostBack="True"  onselectedindexchanged="ddlStatus_SelectedIndexChanged">
<asp:ListItem Value="Selected" Text="Selected" />
<asp:ListItem Value="ProformaASubmitted" Text="ProformaASubmitted" />
<asp:ListItem Value="ProformaAApproved" Text="ProformaAApproved" />
<asp:ListItem Value="ProformaBSubmitted" Text="ProformaBSubmitted" />
<asp:ListItem Value="ProformaBApproved" Text="ProformaBApproved" ></asp:ListItem>
<asp:ListItem Value="CopyPending" Text="CopyPending" ></asp:ListItem>
<asp:ListItem Value="CopySubmitted" Text="CopySubmitted" ></asp:ListItem>
<asp:ListItem Value="CopyDispatched" Text="CopyDispatched" ></asp:ListItem>
<asp:ListItem Value="Approved" Text="Approved" ></asp:ListItem>
<asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>
</asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
<asp:DropDownList ID="ddlSysStatus" runat="server" CssClass="txtbox" AutoPostBack="True" onselectedindexchanged="ddlSysStatus_SelectedIndexChanged">
<asp:ListItem Value="NotSubmitted" Text="NotSubmitted" />
<asp:ListItem Value="Submitted" Text="Submitted"></asp:ListItem>
<asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
<asp:ListItem Value="ReSubmit" Text="ReSubmit" />
<asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>
</asp:DropDownList></td></tr></table></center>
<br />
<script>
    function toggleA1a(showHideDiv, switchImgTag) {
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
<a id="A1a" href="javascript:toggleA1a('Div1a', 'A1a');"><img src="../images/minus.png" alt="Show"></a>
</div> <br /><br />
<div id="Div1a" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid" style="width: 100%; overflow:scroll; height:200px" >
<br />
<asp:GridView ID="GridToBeApprove" runat="server" BackColor="#DEBA84"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" 
        CellSpacing="5" Width="100%" OnRowDeleting="DeleteRecord" AutoGenerateColumns="false"
        onselectedindexchanged="GridToBeApprove_SelectedIndexChanged" DataKeyNames="SN" >
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Select"/>
             <asp:TemplateField HeaderText="SN">
            <ItemTemplate><%# Eval("SN") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="SID">
            <ItemTemplate><%# Eval("SID") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Student Name">
            <ItemTemplate><%# Eval("StudentName") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="IMID">
            <ItemTemplate><%# Eval("IMID") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Course">
            <ItemTemplate><%# Eval("Course") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Part">
            <ItemTemplate><%# Eval("Part") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Session">
            <ItemTemplate><%# Eval("Session") %></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Entry Status">
            <ItemTemplate><%# Eval("EntryStatus") %></ItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Delete?">
            <ItemTemplate>
            <span onclick="return confirm('Are you sure to Delete the record?')">
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="../images/delete.png" />
            </span>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView></div></div></div><br /><br />
<asp:Panel ID="pnldetails" runat="server">
<table class="tbl" width="95%" bgcolor="#CCCCCC"><tr><td>Status:</td><td><asp:Label ID="txtStatus" runat="server" ForeColor="Brown" Font-Bold="true" ></asp:Label></td><td>SynopsisStatus:</td><td><asp:Label ID="txtSynopsisStatus" runat="server" ForeColor="Brown" Font-Bold="true" ></asp:Label></td></tr> <tr>
<td colspan="5"><hr style="width: 687px" /></td>
    </tr><tr><td align="left" colspan="5"><strong>PROFORMA A</strong></td></tr>
<tr><td>Membership No:</td><td><asp:Label ID="txtSid" runat="server" AutoPostBack="true" OnTextChanged="txtSID_OnTextChanted"   ForeColor="Brown" Font-Bold="true" /></td>
<td>IMID:</td><td colspan="2"><asp:Label ID="lblIMID" runat="server" Font-Bold="true" ForeColor="Brown" /></td></tr>
<tr><td>Student Name:</td><td colspan="4"><asp:Label ID="lblStuName" runat="server" Font-Bold="true" ForeColor="Brown" /></td></tr>
<tr><td>Course:</td><td colspan="1"><asp:Label ID="ddlCourse" runat="server"  ForeColor="Brown" Font-Bold="true"></asp:Label>
</td>
<td>Part:</td><td colspan="2"><asp:Label ID="ddlPart" runat="server"  ForeColor="Brown" Font-Bold="true" >
</asp:Label></td>
</tr><tr>  <td align="left">Diary No(A):</td><td><asp:Label ID="txtDA" runat="server" /></td>
<td align="left" >Group ID:</td><td colspan="2"><asp:Label ID="txtGP" runat="server" 
        Enabled="false" Width="90px" />
        </td>
        </tr>
        <tr>
            <td>
                Group Mate 1:</td>
            <td>
                <asp:Label ID="txtGm1" runat="server"  Width="90px" /></td>
            <td>
                GroupMate 2:</td><td colspan="2">
                <asp:Label ID="txtGm2" runat="server"  Width="90px" /></td>
        </tr>
        <tr>
            <td>
                GroupMate 3:</td>
            <td><asp:Label ID="txtGm3" runat="server"  Width="90px" /></td>
          
            <td align="left">
                AICTE Ins. ID:</td><td colspan="2">
                <asp:Label ID="txtInstID" runat="server"  Width="90px" /></td>
        </tr>
        <tr>
            <td align="left">AICTE Institute:</td><td colspan="3"><asp:Label ID="ddlOpn4" runat="server"  ForeColor="Brown" Font-Bold="true"/></td>
        </tr>
        <tr>
            <td align="left">Option (1):</td><td colspan="3"><asp:Label ID="ddlOpn1" runat="server"  Font-Bold="true"/></td>
        </tr>
        <tr>
            <td align="left">Option (2):</td><td colspan="3"><asp:Label ID="ddlOpn2" runat="server"  Font-Bold="true"/></td>
        </tr>
        <tr>
            <td align="left">Option (3):</td><td colspan="3"><asp:Label ID="ddlOpn3" runat="server"   Font-Bold="true"/></td>
        </tr>
        
        <tr>
            <td align="left">Synopsis Title:</td><td ><asp:Label ID="txtSynTtl" 
                runat="server"
                 /></td><td align="left">Synopsis Date:</td><td colspan="2"><asp:Label ID="txtSynDate" 
            runat="server" />
    </td>
        </tr>
        <tr>
            <td>
                Course Status:</td>
            <td><asp:Label ID="ddlCourseStatus" runat="server"  ForeColor="Brown" Font-Bold="true" >

</asp:Label></td>
            <td align="left">
             Synopsis Remarks:</td>
            <td colspan="2">
                <asp:Label ID="txtSynRemark" runat="server"   /></td>
        </tr>
     <tr>
         <td colspan="5">
             <hr style="width: 687px" /></td>
    </tr>
        <tr>
            <td align="left" colspan="5">
                <strong>PROFORMA B</strong></td>
        </tr>
        <tr>
            <td>
                Diary No(B):</td>
            <td><asp:Label ID="txtDB" runat="server" /></td>
            <td align="right">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Project No:</td>
            <td><asp:Label ID="txtProNo" runat="server" /></td>
            <td align="left">
                &nbsp;Duration:</td>
            <td colspan="2">
                <asp:Label ID="txtDuration" runat="server"  /></td>
        </tr>
        <tr>
            <td>
                Project Title:</td>
            <td colspan="4"><asp:Label ID="txtProTtl" runat="server" ForeColor="Brown" Font-Bold="true" 
                   /></td>
        </tr>
        <tr>
            <td>
                Description:</td>
            <td><asp:Label ID="txtDes" runat="server" 
                    /></td>
            <td align="left">
                Remarks:</td>
            <td colspan="2">
                <asp:Label ID="txtRemark" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                Project Date:</td>
            <td><asp:Label ID="txtProDate" runat="server"  /></td>
            <td align="left">
                Approval Date:</td>
            <td colspan="2">
                <asp:Label ID="txtProAppDate" runat="server"  /></td>
        </tr>
        <tr>
            <td>
                Letter Remarks:</td>
            <td><asp:Label ID="txtLetRemark" runat="server"  /></td>
            <td align="left">
                Letter Issue Date:</td>
            <td colspan="2">
                <asp:Label ID="txtLetIsDate" runat="server"  /></td>
        </tr>
        <tr>
            <td>
                Approval Fees:</td>
            <td><asp:Label ID="txtAppFees" runat="server"  /></td>
            <td align="left">
                Evaluation Fees:</td>
            <td colspan="2">
                <asp:Label ID="txtEvalFees" runat="server"  /></td>
        </tr>
        <tr>
            <td>
                Training Fees:</td>
            <td><asp:Label ID="txtTraFees" runat="server"  /></td>
            <td align="left">
                Guidence Fees:</td>
            <td colspan="2">
                <asp:Label ID="txtGuidFees" runat="server"  /></td>
        </tr>
        <tr>
         <td colspan="5">
             <hr style="width: 687px" /></td>
    </tr>
        <tr>
            <td align="left" colspan="5">
                <strong>COPY Submission and Dispatch</strong></td>
        </tr>
        <tr>
            <td>
                No Of Copies:</td>
            <td><asp:Label ID="ddlCopies" runat="server"  ForeColor="Brown" Font-Bold="true">
</asp:Label></td>
            <td align="left">
                Dispatch No:</td>
            <td colspan="2">
                <asp:Label ID="txtDNo" runat="server"  /></td>
        </tr>
        <tr>
            <td>
                Send Date:</td>
            <td><asp:Label ID="txtSenDate" runat="server"  /></td>
            <td align="left">
                Copy Submit Date:</td>
            <td colspan="2">
<asp:Label ID="txtCpySubDate" runat="server"  /></td>
        </tr>
        <tr>
            <td>
                Grade Date:</td>
            <td><asp:Label ID="txtGDate" runat="server"  /></td>
            <td align="left">
                Grade:</td>
            <td colspan="2">
                <asp:Label ID="ddlGrade" runat="server"   ForeColor="Brown" Font-Bold="true">
</asp:Label></td>
        </tr><tr>
         <td colspan="5">
             <hr style="width: 687px" /></td>
    </tr>
        <tr>
            <td align="left" colspan="5">
                <strong>PROFORMA C</strong></td>
        </tr>
        <tr>
            <td>
                Evaluation Date:</td>
            <td><asp:Label ID="txtEvalDate" runat="server"  /></td>
            <td align="left">
                Diary No(C):</td>
            <td colspan="2">
                <asp:Label ID="txtDC" runat="server"   /></td>
        </tr>
        </table><br /></asp:Panel>
        <asp:Panel ID="pnlspace" runat="server" Height="300px"></asp:Panel>
</div>
<br />
</asp:Content>