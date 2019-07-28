<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="AppApproveView.aspx.cs" EnableEventValidation="true" Inherits="Acc_AppApproveView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">View Applciation Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />	
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td>
<td><asp:Label ID="lblCourierHome" runat="server" Text="View Application Forms" CssClass="redirecttabhome"></asp:Label></td></tr>
</table></div>
<div id="rightpanel2">
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"  Font-Bold="true"></asp:Label>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Application Forms Summary:- </h1></div><br />
<center>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session:&nbsp; <asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; <asp:TextBox ID="txtSession" runat="server" Width="70px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox>
    <br />
<br />&nbsp;View By:&nbsp;&nbsp;<asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="True" CssClass="txtbox" Width="145px" onselectedindexchanged="ddlSearch_SelectedIndexChanged">
<asp:ListItem Text="IMID" Value="IMID"></asp:ListItem>
<asp:ListItem Text="DiaryNo" Value="DiaryNo"></asp:ListItem>
<asp:ListItem Text="MembershipNo" Value="MembershipNo"></asp:ListItem>
<asp:ListItem Text="SerialNo" Value="SerialNo"></asp:ListItem>
<asp:ListItem Text="Name" Value="Name"></asp:ListItem>
<asp:ListItem Text="All" Value="All"></asp:ListItem>
</asp:DropDownList>
</center>
<br /><center>
<table class="tbl">
<tr>
<td>
<asp:Panel ID="panelselectapp" runat="server" CssClass="imbox" Width="250px">
    <br />
<asp:DropDownList ID="ddlAppType" runat="server" CssClass="txtbox" Width="240px" 
        AutoPostBack="True">
                        <asp:ListItem Selected="True" Text="------ Select Application Type------" 
                            Value="no"></asp:ListItem>
                        
                       <asp:ListItem Text="New Admission Application Forms " Value="NewAdmission" />
                        <asp:ListItem Text="Promotted Admission Form(SectionA)" Value="ReAdmission" />
                        <asp:ListItem Text="Examination Application Forms" Value="Exam" />
                        <asp:ListItem Text="ITI Application Forms" Value="ITI" />
                        <asp:ListItem Text="Composite Fees" Value="Composite" />
                        <asp:ListItem Text="Annual Subscription" Value="Subscription" />
                        <asp:ListItem Text="Exam Center Change" Value="ChangeCenter" />
                        <asp:ListItem Text="Re-Checking" Value="Rechecking" />
                        <asp:ListItem Text="Final Marksheet" Value="FinalPass" />
                        <asp:ListItem Text="Provisional Certificate" Value="ProvisionalCertificate" />
                        <asp:ListItem Text="Membership Certificate" Value="MembershipCertificate"></asp:ListItem>
                        <asp:ListItem Text="Marks Statement" Value="MarksStatement"></asp:ListItem>
                        <asp:ListItem Text="Project Proforma B" Value="ProformaB" />
                        <asp:ListItem Text="Project Proforma C" Value="ProformaC" />
                        <asp:ListItem Text="MCADLateFee" Value="MCADLateFee" />
                        <asp:ListItem Text ="MCADRegistration" Value="MCADRegistration"></asp:ListItem>
                        <asp:ListItem Text="NOC" Value="NOC" />
                        <asp:ListItem Text="DATEOFADMMCERTFEE" Value="DATEOFADMMCERTFEE" />
                        <asp:ListItem Text="ID Card" Value="IDCard"></asp:ListItem>
                        <asp:ListItem Text="Admit Card" Value="AdmitCard"></asp:ListItem>
                        <asp:ListItem Text="Old Question Papers" Value="OldSet" />
                        <asp:ListItem Text="Other Charges...." Value="Other" />
</asp:DropDownList>
</asp:Panel>
</td>
<td><asp:Panel ID="panelStatus" runat="server" CssClass="imbox" Width="250px">
    <br />
    <asp:DropDownList  ID="ddlStatus" runat="server" CssClass="txtbox" 
        Width="150px" onselectedindexchanged="ddlStatus_SelectedIndexChanged" 
        AutoPostBack="true">
        <asp:ListItem Text="---Select Status---"></asp:ListItem>
        <asp:ListItem Text="Not Approved" Value="NotApproved"></asp:ListItem>
        <asp:ListItem Text="Approved" Value="no"></asp:ListItem>
        <asp:ListItem Text="Filled" Value="Filled"></asp:ListItem>
        <asp:ListItem Text="Hold" Value="Hold" />
    </asp:DropDownList>
</asp:Panel>
</td></tr></table></center>
<center> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblIMID" runat="server"/><asp:Label ID="lblMembsID" runat="server"/><asp:Label ID="lblDiaryNo" runat="server"/><asp:Label ID="lblSrNo" runat="server"/><asp:Label ID="lblName" runat="server"/>
    <asp:TextBox ID="txtAll" runat="server" CssClass="txtbox" />
<asp:Button ID="btnOK" runat="server" CssClass="btnsmall" Text=" View Record(s) " 
        onclick="btnOK_Click1" />
</center>
<center>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </center><center><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></center><br />
<script>
    function toggleA1x(showHideDiv, switchImgTag) {
        var ele = document.getElementById(showHideDiv);
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
</div><div style="padding:5px; color:White; font-size:15px;">
Total Forms:<asp:Label ID="lblGridTitle" runat="server" ></asp:Label>
<br /><br />
</div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 100%; overflow:scroll; height:250px" >
<asp:GridView ID="grvViewApp" runat="server"
        Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
        BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" 
        HeaderStyle-HorizontalAlign="Center" OnRowDataBound="grvViewApp_RowDataBound">
                    <EmptyDataTemplate><center><b>Record Not Found.</b></center></EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
</asp:GridView>
</div>
</div></div>
</div><br />
</asp:Content>