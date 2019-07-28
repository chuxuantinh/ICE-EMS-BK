<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="ITIForm.aspx.cs" Inherits="Admission_ITIForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">ITI Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Approve ITI Forms" CssClass="redirecttabhome"></asp:Label></td></tr></table></div>
<div id="rightpanel2">

<div class="fromRegisterlbl"><h1>ITI Application</h1></div>
<center><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Session:&nbsp;<asp:DropDownList ID="ddlSessionSelect" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_ONSelectediNdexCanged" Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;Year: <asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYear_OnTextChanged"></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"/></div></center>
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
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
     &nbsp;&nbsp;&nbsp;&nbsp;<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"/></a>
</div>
<h1>ITI Forms:</h1>
    <br />
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
 
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:350px">
<asp:GridView ID="grviti" runat="server" AllowPaging="True" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="grviti_PageIndexChanging"
        CellPadding="4"  PageSize="25" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onselectedindexchanged="grviti_SelectedIndexChanged" 
        Height="80%" onrowdatabound="grviti_RowDataBound" >
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div>
</div>
<div><asp:Panel ID="pnlStudent" runat="server" Visible="false">
<div class="togalfees" style="width:100%">
 <div class="headerDivImgfees">
    
     &nbsp;&nbsp;&nbsp;&nbsp;<a id="A1" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"/></a>
</div><h1>Update Forms:</h1>
</div>
<center><h1 style="color:Maroon">Edit ITI Form Details</h1></center>
&nbsp;
<center>
<table class="tbl">
   
       
        <tr>
            <td align="left">
                Name:</td>
            <td align="left">
                <asp:TextBox ID="txtName" runat="server" CssClass="txtbox"></asp:TextBox>
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
            <td align="left">
                SID:</td>
            <td align="left">
                <asp:TextBox ID="txtSID" runat="server" CssClass="txtbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                Father Name:</td>
            <td align="left">
                <asp:TextBox ID="txtFName" runat="server" CssClass="txtbox"></asp:TextBox>
            </td>
            <td align="left">
                &nbsp;</td>
            <td align="left">
                Session:</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" 
                    Width="146px">
                    <asp:ListItem Value="Win">Winter Examination</asp:ListItem>
                    <asp:ListItem Value="Sum">Summer Examination</asp:ListItem>
                </asp:DropDownList>&nbsp;<asp:TextBox ID="txtYearUpdate" runat="server" CssClass="txtbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                Stream:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlStream" runat="server" CssClass="txtbox" Width="146px">
                    <asp:ListItem Value="Tech">Technician</asp:ListItem>
                    <asp:ListItem Value="Asso">Associate</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left">
                &nbsp;</td>
            <td align="left">
                Course:
            </td>
            <td colspan="2" align="left">
                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" Width="146px">
                    <asp:ListItem>Civil</asp:ListItem>
                    <asp:ListItem>Architecture</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                Part:
            </td>
            <td align="left">
               <asp:DropDownList ID="ddlPart" runat="server" CssClass="txtbox" Width="146px">
                   <asp:ListItem Value="PartI">Part I</asp:ListItem>
                   <asp:ListItem Value="PartII">Part II</asp:ListItem>
</asp:DropDownList>

            </td>
            <td align="left">
                &nbsp;</td>
            <td align="left">
                Subscription Date:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtSubDate" runat="server" CssClass="txtbox"></asp:TextBox>
                <dev:CalendarExtender ID="devdage" runat="server" Format="dd/MM/yyyy" 
                PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtSubDate">
            </dev:CalendarExtender>
                <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" />
            </td>
        </tr>
        <tr>
            <td align="left">
                Amount:
            </td>
            <td align="left">
                <asp:TextBox ID="txtAmount" runat="server" CssClass="txtbox"></asp:TextBox>
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
            <td align="left">
                DOB:
            </td>
            <td align="left">
                <asp:TextBox ID="txtDOB" runat="server" CssClass="txtbox"></asp:TextBox>
                <dev:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                PopupButtonID="calDOB" PopupPosition="BottomRight" TargetControlID="txtDOB">
            </dev:CalendarExtender>
                <img src="../images/cal.png" id="calDOB" runat="server"  alt="Cal" />
            </td>
        </tr>
        <tr>
            <td align="left">
                IMID:
            </td>
            <td align="left">
                <asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox>
            </td>
            <td align="left">
                &nbsp;</td>
            <td align="left">
                Application No:</td>
            <td>
                <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="txtbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="5">
                <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btnsmall" onclick="btnSave_Click" />
            </td>
            <td align="left" colspan="2">
                &nbsp;</td>
        </tr>
        <tr><td colspan="5" align="center">
            <asp:Label ID="lblStatus" runat="server" ForeColor="Green"></asp:Label>
            </td></tr>
</table></center></asp:Panel>
<asp:Panel ID="panApprove" runat="server" Visible="false">
<center><h1 style="color:Maroon">Update ITI Form Status</h1></center>
&nbsp;
<center>
<table class="tbl">
<tr><td>Student ID:</td><td colspan="3"><%--<asp:Label ID="lblStuID" runat="server" 
        ForeColor="Black"></asp:Label>--%><asp:TextBox ID="txtStuID" runat="server" CssClass="txtbox"></asp:TextBox>
            </td></tr>
<tr><td>Status:</td><td>  
    <asp:DropDownList ID="ddlITIStatus" runat="server" 
        CssClass="txtbox" Width="146px">
                    <asp:ListItem>ReadyForExam</asp:ListItem>
                    <asp:ListItem>Cancel</asp:ListItem>
                </asp:DropDownList></td></tr>
                <tr><td colspan="5" align="center"> <asp:Button ID="btnApprove" runat="server" CssClass="btnsmall" Text="Update" 
    onclick="btnApprove_Click" />
            </td> 
                </tr>
                 <tr><td colspan="2" align="center"><asp:Label ID="lblITIStatus" runat="server" ForeColor="Green"></asp:Label></td></tr>
</table>
</center>
    </asp:Panel></div>
</div>
</div>
</asp:Content>

